' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Concurrent
Imports System.Data.Entity.Core

'Imports System.Net.Mail
Public Module SQLFunctions
    ''' <summary>
    ''' Represents DB NULL in a string format
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly nullVar As String = "DBNULLENTRY"
    ''' <summary>
    ''' Equates to 12 AM in SQL server for the case when a "null" date is passed.
    ''' </summary>
    ''' <remarks>For comparing literal mm/dd/yyyy to a real date like 11/11/2008 to prevent system cast errors mm/dd/yyyy or any invalid date gets set to the 12 AM in VB.</remarks>
    Public ReadOnly nullDate As String = "12:00:00 AM"
    ''' <summary>
    ''' SQL Server minimum date for server 2008 datetime datatype.
    ''' </summary>
    ''' <remarks>Datetime2 SQL datatype extends this farther, but datetime is used in most table definitions.</remarks>
    Public ReadOnly minDate As DateTime = New DateTime(1753, 1, 1)
    ''' <summary>
    ''' SQL server maximum date for server 2008 datetime datatype.
    ''' </summary>
    ''' <remarks>Datetime2 SQL datatype extends this farther, but datetime is used in most table definitions.</remarks>
    Public ReadOnly maxDate As DateTime = New DateTime(9999, 12, 31)

    ''' <summary>
    ''' Blacklist of illegal or potentially dangerous SQL injection type strings.
    ''' </summary>
    ''' <remarks>Should be checked against when using a DYNAMIC stored procedure for cleaning purposes.</remarks>
    Public ReadOnly blackList As String() = {";", "drop table", "delete from", "--", "/*", "*/", "waitfor", "varchar", "nvarchar", "%"}

    ''' <summary>
    ''' Database SQL server connection string.
    ''' </summary>
    ''' <remarks></remarks>
    Public ConnectionString As String = "Data Source=SYRSTSVR2008\SQLSVR2008TEST;Initial Catalog=PickProSD_New;Integrated Security=SSPI;"

    ''' <summary>
    ''' Contains user connection strings if the end user has multiple pick pro databases that should use the same application with different workstations/different datasources.
    ''' </summary>
    ''' <remarks></remarks>
    Public userCS As New ConcurrentDictionary(Of String, String)

    ''' <summary>
    ''' Contains user IE connection strings if the end user has multiple ImportExport databases that should use the same application with different workstations/different datasources.
    ''' </summary>
    ''' <remarks></remarks>
    Public userIECS As New ConcurrentDictionary(Of String, String)

    ''' <summary>
    ''' Represents a string for use in casting in functions like RunSPArray that require SQL parameters and datatypes.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly strVar As String = "str"
    ''' <summary>
    ''' Represents an integer for use in casting in functions like RunSPArray that require SQL parameters and datatypes.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly intVar As String = "int"
    ''' <summary>
    ''' Represents a date or datetime for use in casting in functions like RunSPArray that require SQL parameters and datatypes.
    ''' </summary>
    ''' <remarks>Typically checked when using CDate() on a particular SQL parameter.</remarks>
    Public ReadOnly dteVar As String = "dte"
    ''' <summary>
    ''' Represents a Boolean type for use in casting in functions like RunSPArray that require SQL parameters and datatypes.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly boolVar As String = "bool"

    ''' <summary>
    ''' Represents as Decimal type for use in casting functions like RunSPArray that require sql parameters and datatypes
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly decVar As String = "dec"

    Public Function GetUserCs(wsid As String) As String
        Dim connString As String
        If Not userCS.TryGetValue(wsid, connString)
            Throw New ObjectNotFoundException($"wsid: {wsid}")
        End If
        Return connString
    End Function

    ''' <summary>
    ''' Runs SPName stored procedure and returns the SqlDataReader used.
    ''' </summary>
    ''' <param name="SPName">Stored procedure name to be run.</param>
    ''' <param name="Parms">Parameters for stored procedure.  Structured as {{"@parametername", parametervalue, parametertype}, etc.}</param>
    ''' <returns>Returns SqlDataReader with results of the stored procedure.</returns>
    ''' <remarks>DataReader MUST be manually closed with .Dispose() after use.</remarks>
    Public Function RunSPArray(ByRef SPName As String, ByRef WSID As String, ByRef Parms(,) As String) As SqlDataReader
        Dim cmd As SqlCommand
        Dim dbConn As SqlConnection
        Dim DataReader As SqlDataReader = Nothing
        If (WSID = "CONFIG") Then
            dbConn = New SqlConnection(Config.getConfigDBString)
        ElseIf (WSID = "IE") Then
            dbConn = New SqlConnection(Config.getIEDBString)
        Else
            'If Server "Hiccups" and Loses Connection String Dictionary, get connection string again
            Try
                dbConn = New SqlConnection(userCS(WSID))
            Catch ex As Exception
                Dim connString = Config.getConnectionString(WSID, Nothing)
                insertErrorMessages("SQLFunctions", "RunSPArray", "Connection String not found: " + ex.ToString(), SPName, WSID)
                userCS.TryAdd(WSID, connString)
                dbConn = New SqlConnection(userCS(WSID))
            End Try
        End If
        Try
            'Run Stored procedure
            cmd = New SqlCommand(SPName, dbConn)
            cmd.CommandType = CommandType.StoredProcedure

            'Set timeout to 60 seconds to allow locass mroe time to run
            If SPName.ToLower() = "locassputaway" Then
                cmd.CommandTimeout = 60
            End If

            If (Parms(0, 0) <> "nothing") Then
                For x = 0 To UBound(Parms)
                    If Parms(x, 1) Is Nothing OrElse Parms(x, 1).ToUpper() = nullVar OrElse Parms(x, 2).ToUpper() = nullVar Then
                        cmd.Parameters.AddWithValue(Parms(x, 0), DBNull.Value)
                    Else
                        Select Case Parms(x, 2)
                            Case strVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), Parms(x, 1))
                            Case intVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CInt(Parms(x, 1)))
                            Case dteVar
                                If Parms(x, 1) = "" Or Parms(x, 1) = nullDate Then
                                    cmd.Parameters.AddWithValue(Parms(x, 0), DBNull.Value)
                                Else
                                    cmd.Parameters.AddWithValue(Parms(x, 0), CDate(Parms(x, 1)))
                                End If
                            Case boolVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CBool(Parms(x, 1)))
                            Case decVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CDec(Parms(x, 1)))
                        End Select
                    End If
                Next
            End If
            cmd.Connection.Open()
            DataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            Debug.WriteLine("Run SP Array exception Attempting SP " & SPName & " error: " & ex.ToString())
            insertErrorMessages("SQLFunctions", "RunSPArray", "SP: " & SPName & " Message: " & ex.ToString(), "SQLFunctions", WSID)
            If DataReader IsNot Nothing Then
                DataReader.Close()
            End If
        End Try
        Return DataReader
    End Function

    ''' <summary>
    ''' Runs SPName stored procedure and returns the SqlDataReader used, with Specified connection string
    ''' </summary>
    ''' <param name="SPName">Stored procedure name to be run.</param>
    ''' <param name="Parms">Parameters for stored procedure.  Structured as {{"@parametername", parametervalue, parametertype}, etc.}</param>
    ''' <param name="Connection">Specified connection to Run SP at</param>
    ''' <returns>Returns SqlDataReader with results of the stored procedure.</returns>
    ''' <remarks>DataReader MUST be manually closed with .Dispose() after use.</remarks>
    Public Function RunSPArray(ByRef SPName As String, ByRef Parms(,) As String, Connection As String) As SqlDataReader
        Dim cmd As SqlCommand
        Dim dbConn As SqlConnection
        Dim DataReader As SqlDataReader = Nothing

        dbConn = New SqlConnection(Connection)
        Try
            'Run Stored procedure
            cmd = New SqlCommand(SPName, dbConn)
            cmd.CommandType = CommandType.StoredProcedure
            If (Parms(0, 0) <> "nothing") Then
                For x = 0 To UBound(Parms)
                    If Parms(x, 1) Is Nothing OrElse Parms(x, 1).ToUpper() = nullVar OrElse Parms(x, 2).ToUpper() = nullVar Then
                        cmd.Parameters.AddWithValue(Parms(x, 0), DBNull.Value)
                    Else
                        Select Case Parms(x, 2)
                            Case strVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), Parms(x, 1))
                            Case intVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CInt(Parms(x, 1)))
                            Case dteVar
                                If Parms(x, 1) = "" Or Parms(x, 1) = nullDate Then
                                    cmd.Parameters.AddWithValue(Parms(x, 0), DBNull.Value)
                                Else
                                    cmd.Parameters.AddWithValue(Parms(x, 0), CDate(Parms(x, 1)))
                                End If
                            Case boolVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CBool(Parms(x, 1)))
                            Case decVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CDec(Parms(x, 1)))
                        End Select
                    End If
                Next
            End If
            cmd.Connection.Open()

            DataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        Catch ex As Exception
            Debug.WriteLine("Run SP Array exception Attempting SP " & SPName & " error: " & ex.ToString())
            insertErrorMessages("SQLFunctions", "RunSPArray", "SP: " & SPName & " Message: " & ex.ToString(), "SQLFunctions", "")
            If DataReader IsNot Nothing Then
                DataReader.Close()
            End If
        End Try
        Return DataReader
    End Function

    ''' <summary>
    ''' Runs Stored procedure by Connection String
    ''' </summary>
    ''' <param name="SPName">Stored procedure name.</param>
    ''' <param name="Parms">Stored procedure parameters as {{"@paramname", paramval, paramtype}, etc.}</param>
    ''' <param name="connString">Connection String to run Function under</param>
    ''' <remarks></remarks>
    Public Sub RunActionSP(ByRef SPName As String, ByRef Parms(,) As String, ByRef connString As String)
        Dim cmd As SqlCommand = Nothing
        Dim connection As SqlConnection

        connection = New SqlConnection(connString)
        Try
            'Run Stored procedure
            cmd = New SqlCommand(SPName, connection)
            cmd.CommandType = CommandType.StoredProcedure
            If (Parms(0, 0) <> "nothing") Then
                For x = 0 To UBound(Parms)
                    If Parms(x, 1) Is Nothing OrElse Parms(x, 1).ToUpper() = nullVar OrElse Parms(x, 2).ToUpper() = nullVar Then
                        cmd.Parameters.AddWithValue(Parms(x, 0), DBNull.Value)
                    Else
                        Select Case Parms(x, 2)
                            Case strVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), Parms(x, 1))
                            Case intVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CInt(Parms(x, 1)))
                            Case dteVar
                                If Parms(x, 1) = "" Or Parms(x, 1) = nullDate Then
                                    cmd.Parameters.AddWithValue(Parms(x, 0), DBNull.Value)
                                Else
                                    cmd.Parameters.AddWithValue(Parms(x, 0), CDate(Parms(x, 1)))
                                End If
                            Case boolVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CBool(Parms(x, 1)))
                            Case decVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CDec(Parms(x, 1)))
                        End Select
                    End If
                Next
            End If
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Debug.WriteLine("RunActionSP exception Attempting SP " & SPName & " error: " & ex.ToString())
            insertErrorMessages("SQLFunctions", "RunActionSP", "SP: " & SPName & " Message: " & ex.ToString(), "SQLFunctions", "CONFIG")
        Finally
            connection.Close()
            connection.Dispose()
            If Not IsNothing(cmd) Then
                cmd.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Runs Stored Procedure by WSID
    ''' </summary>
    ''' <param name="SPName">Stored procedure name.</param>
    ''' <param name="WSID">Current user to get connection String for</param>
    ''' <param name="Parms">Stored procedure parameters as {{"@paramname", paramval, paramtype}, etc.}</param>
    ''' <remarks></remarks>
    Public Sub RunActionSP(ByRef SPName As String, ByRef WSID As String, ByRef Parms(,) As String)
        Dim cmd As SqlCommand = Nothing
        Dim connection As SqlConnection

        If (WSID = "CONFIG") Then
            connection = New SqlConnection(Config.getConfigDBString)
        ElseIf (WSID = "IE") Then
            connection = New SqlConnection(Config.getIEDBString)
        Else
            'If Server "Hiccups" and Loses Connection String Dictionary, get connection string again
            Try
                connection = New SqlConnection(userCS(WSID))
            Catch ex As Exception
                Dim connString = Config.getConnectionString(WSID, Nothing)
                userCS.TryAdd(WSID, connString)
                connection = New SqlConnection(userCS(WSID))
            End Try
        End If
        Try
            'Run Stored procedure
            cmd = New SqlCommand(SPName, connection)
            cmd.CommandType = CommandType.StoredProcedure
            If (Parms(0, 0) <> "nothing") Then
                For x = 0 To UBound(Parms)
                    If Parms(x, 1) Is Nothing OrElse Parms(x, 1).ToUpper() = nullVar OrElse Parms(x, 2).ToUpper() = nullVar Then
                        cmd.Parameters.AddWithValue(Parms(x, 0), DBNull.Value)
                    Else
                        Select Case Parms(x, 2)
                            Case strVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), Parms(x, 1))
                            Case intVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CInt(Parms(x, 1)))
                            Case dteVar
                                If Parms(x, 1) = "" Or Parms(x, 1) = nullDate Then
                                    cmd.Parameters.AddWithValue(Parms(x, 0), DBNull.Value)
                                Else
                                    cmd.Parameters.AddWithValue(Parms(x, 0), CDate(Parms(x, 1)))
                                End If
                            Case boolVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CBool(Parms(x, 1)))
                            Case decVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CDec(Parms(x, 1)))
                        End Select
                    End If
                Next
            End If
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Debug.WriteLine("RunActionSP exception Attempting SP " & SPName & " error: " & ex.ToString())
            insertErrorMessages("SQLFunctions", "RunActionSP", "SP: " & SPName & " Message: " & ex.ToString(), "SQLFunctions", WSID)
            Throw ex
        Finally
            connection.Close()
            connection.Dispose()
            If Not IsNothing(cmd) Then
                cmd.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Runs passed string in SQL.
    ''' </summary>
    ''' <param name="SqlStr">SQL String to be executed.</param>
    ''' <remarks></remarks>
    Public Sub RunSqlStr(ByRef SqlStr As String, ByRef WSID As String)

        Dim connection As SqlConnection
        Dim command As SqlCommand = Nothing

        connection = New SqlConnection(ConnectionString)

        Try
            'Run Stored procedure
            command = New SqlCommand(SqlStr, connection)
            command.Connection.Open()
            command.ExecuteNonQuery()
            command.Dispose()

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SQLFunctions", "RunSqlStr", "SQL String: " & SqlStr & " Message: " & ex.ToString(), "SQLFunctions", WSID)
        Finally
            connection.Close()
            connection.Dispose()
            If Not IsNothing(command) Then
                command.Dispose()
            End If

        End Try
    End Sub

    ''' <summary>
    ''' Runs SPName and returns the output parameter from the stored procedure as an integer.
    ''' </summary>
    ''' <param name="SPName">Stored procedure name.</param>
    ''' <param name="Parms">Input and output parameters as {{"@paramname", paramval, paramtype}, etc.}</param>
    ''' <returns>Integer value of the output parameter in the stored procedure.</returns>
    ''' <remarks>Stored procedure must have an output parameter that can be cast to a VB integer.  Output parameter must specify "output" as datatype.</remarks>
    Public Function RunSQLGetIdentity(ByRef SPName As String, ByRef WSID As String, ByRef Parms(,) As String) As Integer

        Dim cmd As SqlCommand = Nothing
        Dim connection As SqlConnection
        Dim Prm As SqlParameter = Nothing

        connection = New SqlConnection(ConnectionString)

        Try
            'Run Stored procedure
            cmd = New SqlCommand(SPName, connection)
            cmd.CommandType = CommandType.StoredProcedure
            If (Parms(0, 0) <> "nothing") Then
                For x = 0 To UBound(Parms)
                    'Input Parameters
                    If (Parms(x, 2) <> "output") Then
                        Select Case Parms(x, 2)
                            Case strVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), Parms(x, 1))
                            Case intVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CInt(Parms(x, 1)))
                            Case dteVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CDate(Parms(x, 1)))
                            Case boolVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CBool(Parms(x, 1)))
                            Case decVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CDec(Parms(x, 1)))
                        End Select
                    Else
                        Prm = New SqlParameter(Parms(x, 0), SqlDbType.Int)
                        cmd.Parameters.Add(Prm)
                        Prm.Direction = ParameterDirection.Output
                    End If
                Next
            End If

            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SQLFunctions", "RunSQLGetIdentity", "SP: " & SPName & " Message: " & ex.ToString(), "SQLFunctions", WSID)
        Finally
            connection.Close()
            connection.Dispose()
            If Not IsNothing(cmd) Then
                cmd.Dispose()
            End If
        End Try

        Return CInt(Prm.Value)
    End Function

    ''' <summary>
    ''' Runs SPName stored procedure and returns the output parameter value as a string.
    ''' </summary>
    ''' <param name="SPName">Stored procedure name.</param>
    ''' <param name="Parms">Input and output parameters as {{"@paramname", paramval, paramtype}, etc.}</param>
    ''' <returns>String value of the output SQL parameter.</returns>
    ''' <remarks>Output parameter must be specified in stored procedure.  Returned as SQLDbType.NVARCHAR(50) and casted to string.</remarks>
    Public Function RunSQLReturnString(SPName As String, user As String, WSID As String, Parms(,) As String) As String

        Dim cmd As SqlCommand = Nothing
        Dim connection As SqlConnection
        Dim Prm As SqlParameter = Nothing

        connection = New SqlConnection(ConnectionString)

        Try
            'Run Stored procedure
            cmd = New SqlCommand(SPName, connection)
            cmd.CommandType = CommandType.StoredProcedure
            If (Parms(0, 0) <> "nothing") Then
                For x = 0 To UBound(Parms)
                    'Input Parameters
                    If (Parms(x, 2) <> "output") Then
                        Select Case Parms(x, 2)
                            Case strVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), Parms(x, 1))
                            Case intVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CInt(Parms(x, 1)))
                            Case dteVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CDate(Parms(x, 1)))
                            Case boolVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CBool(Parms(x, 1)))
                            Case decVar
                                cmd.Parameters.AddWithValue(Parms(x, 0), CDec(Parms(x, 1)))
                        End Select
                    Else
                        Prm = New SqlParameter(Parms(x, 0), SqlDbType.NVarChar, 50)
                        cmd.Parameters.Add(Prm)
                        Prm.Direction = ParameterDirection.Output
                    End If
                Next
            End If

            cmd.Connection.Open()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SQLFunctions", "RunSQLReturnString", ex.ToString(), user, WSID)
        Finally
            connection.Close()
            connection.Dispose()
            If Not IsNothing(cmd) Then
                cmd.Dispose()
            End If
        End Try

        Return CStr(Prm.Value)
    End Function



    'Public Sub SendEmail(message As String, emailCase As String)
    '    Dim DataReader As SqlDataReader
    '    Dim DataRead As SqlDataReader
    '    Dim recipients As New List(Of String)
    '    Dim subject As String
    '    Dim smtpServer As New SmtpClient()
    '    Dim email As New MailMessage()
    '    Dim delim As String = ";"

    '    'Select Case emailCase
    '    '    Case "adjust"

    '    '    Case "Replenish"

    '    '    Case "Reprocess"


    '    'End Select


    '    Try
    '        DataReader = RunSPArray("GrabAdjustEmail", {{"Nothing"}})
    '        DataRead = RunSPArray("GrabHostEmail", {{"Nothing"}})
    '        If DataRead.HasRows Then
    '            smtpServer.Host = DataRead(0)
    '        End If

    '        If DataReader.HasRows Then
    '            subject = DataReader(6)
    '            email.From = New MailAddress(DataReader(3))
    '            smtpServer.Port = 25

    '            While DataReader.Read()
    '                recipients.Add(DataReader(4))
    '            End While

    '            For Each r In recipients
    '                email.To.Add(r + delim)
    '            Next

    '            email.Subject = subject
    '            email.Body = message
    '            'Enable this code Line to Send Emails to administrators in Production
    '            smtpServer.Send(email)
    '        End If

    '    Catch e As Exception
    '        Debug.WriteLine(e.ToString)
    '    End Try

    'End Sub

    ''' <summary>
    ''' Inserts error messages into Pick Pro Errors table via InsPPErrorLog. Use: Try/Catch blocks when an exception is thrown.
    ''' </summary>
    ''' <param name="view">Sending page (menu, transactions, inventory map/master, etc.)</param>
    ''' <param name="method">Sending method from within the VB that threw the exception.</param>
    ''' <param name="message">Message to be added to the log.  Typically includes Exception.Message at a minimum.</param>
    ''' <param name="user">Requesting logged in user, if available.  If not available a variety of specifications can be used.  Typically view can be substituted if user is unaccessible.</param>
    ''' <remarks>Needs RunActionSP method as global or in same file.</remarks>
    Public Sub insertErrorMessages(view As String, method As String, message As String, user As String, WSID As String)
        If Config.getConfigDBString <> "" Then
            Try
                RunActionSP("InsPPErrorLog", "CONFIG", {{"@View", view, strVar}, {"@Method", method, strVar}, {"@Message", message, strVar}, {"@User", user, strVar}, {"@WSID", WSID, strVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
            End Try
        End If
    End Sub


    Public Sub insertEventLog(view As String, method As String, message As String, user As String, WSID As String)
        Try

            RunSP("insEventLog", WSID, {
              {"@EVENTTYPE", "Logging", strVar},
              {"@EVENTCODE", 23, intVar},
              {"@EVENTMSG", message, strVar},
              {"@EVENTLOC", view, strVar},
              {"@EVENTPROC", "", strVar},
              {"@EVENTLINE", "", strVar},
              {"@EVENTID", 99, intVar}
              })
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Tries to cast value into a 1/0 t/f sql boolean
    ''' </summary>
    ''' <param name="value">value to cast</param>
    ''' <returns>1 if true, 0 if false, nullVar constant if the variable passed in was DBNull</returns>
    ''' <remarks></remarks>
    Public Function CastAsSqlBool(ByVal value As Object) ' As Integer (but allow DBNull to be returned if it is a problem)
        Dim boolVal As Boolean = False, returnVal As Integer = 0

        If IsDBNull(value) Or IsNothing(value) Then
            Return nullVar
        ElseIf value.ToString().ToUpper() = nullVar Then
            ' dbnull value as a string constant
            Return nullVar
        ElseIf value.ToString() = "1" Or value.ToString() = "0" Then
            ' return 1/0
            Return CInt(value.ToString())
        Else
            Try
                boolVal = CBool(value.ToString())
                returnVal = IIf(boolVal, 1, 0)
            Catch ex As Exception
                Throw New Exception("Method: CastAsSqlBool, Error: The value provided is not recognized as a boolean value.  Valid values are boolean, string (1, 0 or true, false)  Value: " _
                                    & value.ToString() & ".  Originating exception: " & ex.ToString())
            End Try
        End If
        Return returnVal
    End Function

    Public Function insertDataSetError(data As Object, err As Integer, view As String, message As String)
        PrintService.insertPrintLog(err, message, data.user, data.WSID, data.userDirectory, view, data.type, data.location)
        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the column names of the columns contained in the passed datareader.  The datareader must still be open when this is called.
    ''' </summary>
    ''' <param name="datareader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDataReadersKnownColumns(datareader As SqlDataReader) As List(Of String)
        Dim columns As New List(Of String)
        If Not IsNothing(datareader) And Not datareader.IsClosed Then
            For i As Integer = 0 To datareader.FieldCount - 1
                columns.Add(datareader.GetName(i))
            Next
        End If
        Return columns
    End Function

    ''' <summary>
    ''' Gets the field types of a datareader's columns (nvarchar, int, etc.)
    ''' </summary>
    ''' <param name="datareader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDataReaderFieldTypes(datareader As SqlDataReader) As List(Of String)
        Dim types As New List(Of String)
        If Not IsNothing(datareader) And Not datareader.IsClosed Then
            For i As Integer = 0 To datareader.FieldCount - 1
                types.Add(datareader.GetDataTypeName(i))
            Next
        End If
        Return types
    End Function

    ''' <summary>
    ''' Gets the field names and field types of a datareader's columns
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDataReaderSchemaInfo(reader As SqlDataReader) As List(Of Object)
        Dim details As New List(Of Object)
        If Not IsNothing(reader) And Not reader.IsClosed Then
            For i As Integer = 0 To reader.FieldCount - 1
                details.Add(New With {.columName = reader.GetName(i), .columnType = reader.GetDataTypeName(i)})
            Next
        End If
        Return details
    End Function

    ''' <summary>
    ''' Takes a comma separated string, deconstructs, cleans and then reconstructs the string for use in a pseudo-dynamic in clause in sql.
    ''' </summary>
    ''' <param name="commaSeparated"></param>
    ''' <returns></returns>
    ''' <remarks>For use with a sql statement that takes a comma separated string and builds it into a temporary table for use in an IN clause.</remarks>
    Public Function CommaSplitForInClause(commaSeparated As String) As String
        Dim stringArray As String() = commaSeparated.Split(",")
        Dim strList As List(Of String) = stringArray.ToList()
        Dim returnStr As String = ""
        For Each s As String In strList
            s = GlobalFunctions.cleanSearch(s)
            If s <> "" Then
                returnStr += "'" & s & "',"
            End If
        Next
        Return returnStr.Substring(0, returnStr.Length - 1)
    End Function

    Public Function CheckDBNull(checkVal As Object) As Object
        Return IIf(IsNothing(checkVal) OrElse IsDBNull(checkVal), "", checkVal)
    End Function

    Public Function CheckDBNull(Of T)(checkVal As Object) As T
        Dim defaultVal As T
        Return IIf(IsNothing(checkVal) OrElse IsDBNull(checkVal), defaultVal, checkVal)
    End Function

    Public Function CheckMoney(checkVal As Object) As Object
        Return IIf(IsDBNull(checkVal), "", CDec(checkVal).ToString("0.00"))
    End Function


    Public Sub MessageOut(ByVal sMssg As String)

        Dim DebugFileName As String
        Dim Tdy As String

        Try
            'Set filename for debug
            Tdy = FormatDateTime(Now(), DateFormat.ShortDate)
            Tdy = Tdy.Replace("/", "") & ".txt"

            'DebugFileName = "C:\Users\AJackson\Documents\Visual Studio 2015\Projects\PickPro Web\PickPro Web\PickPro Web\Log\" & "Message" & Tdy
            DebugFileName = "C:\ProgramData\Scotttech\" & "Message" & Tdy

            'Write all messages to the debug file
            'Create file if it doesn't exist
            If (System.IO.File.Exists(DebugFileName) = False) Then
                System.IO.File.CreateText(DebugFileName).Close()
            End If

            Dim FileWrite As New System.IO.StreamWriter(DebugFileName, True)
            FileWrite.WriteLine("*****")
            FileWrite.WriteLine(sMssg & "---" & Now())
            FileWrite.Close()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

        Exit Sub

    End Sub


    Public Sub RunActionSPMulti(SQLCommands As List(Of SQLCommandInfo), ByRef WSID As String)
        Dim cmd As SqlCommand = Nothing
        Dim connection As SqlConnection
        Dim Trans As SqlTransaction = Nothing

        If (WSID = "CONFIG") Then
            connection = New SqlConnection(Config.getConfigDBString)
        ElseIf (WSID = "IE") Then
            connection = New SqlConnection(Config.getIEDBString)
        Else
            'If Server "Hiccups" and Loses Connection String Dictionary, get connection string again
            Try
                connection = New SqlConnection(userCS(WSID))
            Catch ex As Exception
                Dim connString = Config.getConnectionString(WSID, Nothing)
                userCS.TryAdd(WSID, connString)
                connection = New SqlConnection(userCS(WSID))
            End Try
        End If

        Try
            connection.Open()
            Trans = connection.BeginTransaction("Multi")

            For Each SQlCommand In SQLCommands

                cmd = New SqlCommand(SQlCommand.SP, connection, Trans)
                cmd.CommandType = CommandType.StoredProcedure
                If (SQlCommand.Params(0, 0) <> "nothing") Then
                    For x = 0 To UBound(SQlCommand.Params)
                        Select Case SQlCommand.Params(x, 2)
                            Case strVar
                                cmd.Parameters.AddWithValue(SQlCommand.Params(x, 0), SQlCommand.Params(x, 1))
                            Case intVar
                                cmd.Parameters.AddWithValue(SQlCommand.Params(x, 0), CInt(SQlCommand.Params(x, 1)))
                            Case dteVar
                                cmd.Parameters.AddWithValue(SQlCommand.Params(x, 0), CDate(SQlCommand.Params(x, 1)))
                            Case boolVar
                                cmd.Parameters.AddWithValue(SQlCommand.Params(x, 0), CBool(SQlCommand.Params(x, 1)))
                            'C.T - Added nullVar to RunActionSP so can be used in place of RunSPArray
                            Case nullVar
                                cmd.Parameters.AddWithValue(SQlCommand.Params(x, 0), DBNull.Value)
                        End Select
                    Next
                End If

                cmd.ExecuteNonQuery()
            Next
            Trans.Commit()

        Catch ex As Exception
            insertErrorMessages("SQLFunctions", "RunActionSPMulti", "Message: " & ex.ToString(), "SQLFunctions", WSID)
            Throw New InvalidOperationException(ex.ToString())

            Try
                Trans.Rollback()
            Catch rollex As Exception
                insertErrorMessages("SQLFunctions", "RunActionSPMulti", "Message: " & rollex.ToString(), "SQLFunctions", WSID)
                Throw New InvalidOperationException(rollex.ToString())
            End Try

        Finally
            connection.Close()
            connection.Dispose()
            Trans.Dispose()
            cmd.Dispose()
        End Try
    End Sub

End Module
