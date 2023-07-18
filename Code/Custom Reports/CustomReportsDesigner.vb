' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class CustomReportsDesigner
    ''' <summary>
    ''' Loads a dataset with a table called data for list and label's designer
    ''' </summary>
    ''' <param name="filename">The filename to look up the data source for and retrieve the data</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting WSID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getDataSource(filename As String, user As String, WSID As String) As DataSet
        Dim ds As New DataSet, reader As SqlDataReader = Nothing, dataSource As String = "", dataType As String = "SP"

        Try
            reader = RunSPArray("selListsAndLabelsReportDS", WSID, {{"@WSID", WSID, strVar}, {"@Filename", filename, strVar}})

            If reader.HasRows Then
                If reader.Read() Then
                    If Not IsDBNull(reader(0)) Then
                        dataSource = reader(0)
                    End If
                    If Not IsDBNull(reader(1)) Then
                        dataType = reader(1)
                    End If
                End If
            End If
            reader.Dispose()
            reader = Nothing
            ' if stored procedure, else sql string
            If dataType = "SP" Then
                reader = RunSPArray(dataSource, WSID, {{"nothing"}})
            Else
                reader = RunSPArray("selListsAndLabelsDSSQLString", WSID, {{"@SQLString", dataSource, strVar}})
            End If

            ds.Load(reader, LoadOption.PreserveChanges, {"Data"})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReportsDesigner", "getDataSource", ex.Message, user, WSID)
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try

        Return ds
    End Function

    ''' <summary>
    ''' Validates a new design's filename.
    ''' </summary>
    ''' <param name="server">Equivalent to Server constant in controllers for mapping paths in the filesystem</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting WSID</param>
    ''' <param name="filename">Filename of the report we're trying to create</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function validateNewDesignFilename(server As HttpServerUtilityBase, user As String, WSID As String, filename As String) As Object
        Dim errObj As Object = New With {.errs = New List(Of String), .success = True, .canAddFileToDefaultTable = False, .canAddFileToWSTable = False}
        Dim reader As SqlDataReader = Nothing, existsObj As Object = New With {.isDefault = False, .isWorkstation = False}
        Try
            ' check to see that all report designs are up to date in the file system before checking stuff
            GlobalFunctions.copyIfNewReportDesigns(user, WSID, server)

            reader = RunSPArray("selLLReportExists", WSID, {{"@WSID", WSID, strVar}, {"@Filename", filename, strVar}})

            If reader.HasRows Then
                If reader.Read() Then
                    If Not IsDBNull(reader(0)) Then
                        existsObj.isDefault = IIf(reader(0) = 1, True, False)
                    End If
                    If Not IsDBNull(reader(1)) Then
                        existsObj.isWorkstation = IIf(reader(1) = 1, True, False)
                    End If
                End If
            End If
            ' checking the main directory (not workstation's directory)
            Dim mainDir As String = server.MapPath("~/Reports/LLDesign/")
            If System.IO.File.Exists(System.IO.Path.Combine(mainDir, filename)) Then
                ' if in the main directory and the table has an entry already
                If existsObj.isDefault Then
                    errObj.errs.Add("File already exists in main directory and in the table!  File cannot be created.")
                Else ' the sql table doesn't know about the report, but the filename is there, so we want to offer the ability to restore the sql table's knowledge of the file
                    errObj.errs.Add("File already exists in main directory!  The file is not on record as a report.")
                    errObj.canAddFileToDefaultTable = True
                End If
            End If
            ' checking the workstation directory (custom reports that aren't globally available)
            Dim wsDir As String = System.IO.Path.Combine(server.MapPath("~/Reports/LLDesign/"), WSID)
            If System.IO.File.Exists(System.IO.Path.Combine(wsDir, filename)) Then
                ' if the file exists and it's known as a workstation report/label
                If existsObj.isWorkstation Then
                    errObj.errs.Add("File already exists in workstation directory and in the table!  File cannot be created.")
                Else ' the file exists, but the sql table doesn't know that it's a report, so offer to restore it in the sql
                    errObj.errs.Add("File already exists in workstation directory!  The file is not on record as a report.")
                    errObj.canAddFileToWSTable = True
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReportsDesigner", "validateNewDesignFilename", ex.Message, user, WSID)
            errObj.success = False
            errObj.errs.Add("Unknown error occurred in filename validation.  Please contact Scott Tech support if the issue persists.")
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try

        Return errObj
    End Function

    ''' <summary>
    ''' Validates the SQL/SP being used for adding a new report/label design
    ''' </summary>
    ''' <param name="server">Equivalent to Server variable in controllers for mapping server paths</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting WSID</param>
    ''' <param name="dataSource">Proposed data source for the new design (either a sql string or the name of a stored procedure.)</param>
    ''' <param name="dataType">Whether the dataSource passed is supposed to be interpreted as a stored procedure or as a sql string</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function validateNewDesignSQL(server As HttpServerUtilityBase, user As String, WSID As String, dataSource As String, dataType As String) As Object
        Dim errObj As Object = New With {.errs = New List(Of String), .success = True, .getsData = False, .columns = New List(Of String), .numResultSets = 0, .sqlError = False}
        Dim reader As SqlDataReader = Nothing, foundSP As Boolean = False
        Try
            ' if stored procedure, else sql string
            If dataType = "SP" Then
                ' checks the database for a stored procedure of the passed name.
                reader = RunSPArray("selStoredProcedureExists", WSID, {{"@SP", dataSource, strVar}})
                If reader.HasRows AndAlso reader.Read() AndAlso Not IsDBNull(reader(0)) AndAlso reader(0) = 1 Then foundSP = True
                reader.Dispose()
                reader = Nothing
                ' if the procedure exists in the database then we can use it, otherwise we need to warn the user that it isn't a valid source
                If foundSP Then
                    Try
                        reader = RunSPArray(dataSource, WSID, {{"nothing"}})

                        errObj.sqlError = IsNothing(reader)
                    Catch ex As Exception
                        Debug.WriteLine(ex.Message)
                        insertErrorMessages("CustomReportsDesigner", "validateNewDesignSQL: SP", ex.Message, user, WSID)
                        errObj.sqlError = True
                    End Try
                Else
                    errObj.errs.Add("Stored Procedure provided (" & dataSource & ") not found!")
                End If
            Else ' else they passed a sql string to run
                Try
                    ' run the sql string (just takes in the @SQLString parameter and immediately executes it.
                    ' the end user could really screw up their database with this stored procedure, because we don't clean the string
                    ' this includes DROP, DELETE, UPDATE, INSERT, CREATE statements, so there can be some trouble
                    reader = RunSPArray("selListsAndLabelsDSSQLString", WSID, {{"@SQLString", dataSource, strVar}})

                    ' if there was a sql error then the reader comes back empty from RunSPArray
                    errObj.sqlError = IsNothing(reader)
                Catch ex As Exception
                    Debug.WriteLine(ex.Message)
                    insertErrorMessages("CustomReportsDesigner", "validateNewDesignSQL: SQLString", ex.Message, user, WSID)
                    errObj.sqlError = True
                End Try
            End If

            ' gather reader information
            If Not IsNothing(reader) Then
                errObj.getsData = reader.HasRows

                errObj.columns = getDataReadersKnownColumns(reader)

                While reader.HasRows
                    errObj.numResultSets += 1
                    reader.NextResult()
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReportsDesigner", "validateNewDesignSQL", ex.Message, user, WSID)
            errObj.success = False
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try

        Return errObj
    End Function
End Class