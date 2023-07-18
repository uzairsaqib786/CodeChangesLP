' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports ActiveDirectorySecurityDriver

Public Class Config
    ''' <summary>
    ''' Maximum number of users allowed to be online at a time.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared maxUsers As Integer = 5
    ''' <summary>
    ''' Currently connected users based on Application
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared AppUsers As New Concurrent.ConcurrentDictionary(Of String, Concurrent.ConcurrentDictionary(Of String, ConnectedUser))
    ''' <summary>
    ''' Dictionary containing all Pickpro Pages a workstation currently has open
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared WorkstationCurrentPages As New Concurrent.ConcurrentDictionary(Of String, Concurrent.ConcurrentDictionary(Of String, String))
    ''' <summary>
    ''' Broadcaster for the config hub
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ConfigBroadcaster As GlobalConfigBroadcaster = GlobalConfigBroadcaster.Instance

    ''' <summary>
    ''' List of all Applications and their Licensing Status
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared AppLicenses As New Dictionary(Of String, AppLicense)

    ''' <summary>
    ''' Mapping of Appdata based on URL
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared AppDatabyURL As New Dictionary(Of String, AppData)

    ''' <summary>
    ''' Function running on a timer, that Checks to see if a user is actually still connected to the application
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub checkIdleUsers()
        Try
            Dim currentTime = Now()
            Dim removeList As New List(Of Tuple(Of String, String))
            For Each App In AppUsers
                For Each User In App.Value
                    If (currentTime - User.Value.heartbeat).TotalSeconds >= 60 Then
                        Debug.WriteLine("Removing User")
                        removeList.Add(New Tuple(Of String, String)(App.Key, User.Key))
                    End If
                Next
            Next
            For Each toRemove In removeList
                Dim removed As ConnectedUser = Nothing
                AppUsers(toRemove.Item1).TryRemove(toRemove.Item2, removed)
                If removed IsNot Nothing Then
                    ConfigBroadcaster.removeConnectedUser(toRemove.Item2)
                End If
            Next
        Catch ex As Exception
            insertErrorMessages("Config", "checkIdleUsers", ex.Message, "CONFIG", "CONFIG")
        End Try
    End Sub


    ''' <summary>
    ''' Converts the short url for an App to the App name used for development/licensing
    ''' </summary>
    ''' <param name="shortApp">URL for Application</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getFullAppName(shortApp As String) As String
        If AppDatabyURL.ContainsKey(shortApp) Then
            Return AppDatabyURL(shortApp).Name
        Else
            Return shortApp
        End If
    End Function

    Friend Shared Function getLAConnStr() As String

        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("getConnectionString", "CONFIG", {{"@WSID", "IEWSID", strVar}})
            If DataReader.HasRows Then
                DataReader.Read()

                Return DataReader("Connection Name")
            Else
                Return "Connection Not Set"
            End If
        Catch ex As Exception
            Debug.WriteLine("Cannot find connection String")
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return "Connection Not Set"
    End Function

    ''' <summary>
    ''' Checks if the WSID provided can acces the specified application
    ''' </summary>
    ''' <param name="WSID"></param>
    ''' <param name="AppName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getWSAppPermission(WSID As String, AppName As String) As Boolean
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selWorkstationAvailAppSingle", "CONFIG", {{"@WSID", WSID, strVar}, {"@AppName", AppName, strVar}})
            If DataReader.HasRows Then
                Return True
            End If
        Catch ex As Exception
            insertErrorMessages("Config", "getWSAppPermission", ex.Message, "CONFIG", "CONFIG")
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Gets a list of all App permissions for the specified workstation
    ''' </summary>
    ''' <param name="WSID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getWSAllAppPermission(WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim returnList As New List(Of String)
        Try
            DataReader = RunSPArray("selWorkstationAvailAppSingle", "CONFIG", {{"@WSID", WSID, strVar}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    returnList.Add(DataReader(1))
                End While
            End If
        Catch ex As Exception
            insertErrorMessages("Config", "getWSAppPermission", ex.Message, "CONFIG", "CONFIG")
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return returnList
    End Function

    ''' <summary>
    ''' Gets the configuration logon details
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getLogonInfo() As Object
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selLogonInfo", "CONFIG", {{"nothing"}})
            If DataReader.HasRows Then
                DataReader.Read()
                Return New With {.Username = DataReader(0), .Password = DataReader(1)}
            End If
        Catch ex As Exception
            insertErrorMessages("Config", "GetLogonInfo", ex.Message, "CONFIG", "CONFIG")
        Finally

            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return New With {.Username = "", .Password = ""}
    End Function

    ''' <summary>
    ''' Gets the Config Database String from the Web.Config File
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getConfigDBString() As String
        Dim myConfiguration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Return myConfiguration.ConnectionStrings.ConnectionStrings("ConfigConnection").ConnectionString
    End Function

    ''' <summary>
    ''' Gets the IE Database String from the Web.Config File
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getIEDBString() As String
        Dim myConfiguration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Return myConfiguration.ConnectionStrings.ConnectionStrings("IEConnection").ConnectionString
    End Function

    Public Shared Function getIEWSID() As String
        Return System.Configuration.ConfigurationManager.AppSettings("IE_WSID")
    End Function

    ''' <summary>
    ''' Sets the Config Database String in the Web.Config File
    ''' </summary>
    ''' <param name="Servername"></param>
    ''' <param name="DBName"></param>
    ''' <remarks></remarks>
    Public Shared Sub setConfigDBString(Servername As String, DBName As String)
        Dim myCOnfiguration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        myCOnfiguration.ConnectionStrings.ConnectionStrings("ConfigConnection").ConnectionString = "Data Source=" & Servername & ";Initial Catalog=" & DBName & ";Integrated Security=SSPI;"
        myCOnfiguration.Save()
    End Sub

    ''' <summary>
    ''' Adds a connection to the CONFIG database for PickPro that allows users on different workstations to work with different databases
    ''' </summary>
    ''' <param name="ident"></param>
    ''' <param name="ConnectionName"></param>
    ''' <param name="DBName"></param>
    ''' <param name="ServerName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function addUpdateConnectionString(ident As Integer, oldConnection As String, ConnectionName As String, DBName As String, ServerName As String) As String
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("addUpdateConnectionString", "CONFIG", {{"@Ident", ident, intVar},
                                                                            {"@oldConnectionName", oldConnection, strVar},
                                                                            {"@ConnectionName", ConnectionName, strVar},
                                                                            {"@DBName", DBName, strVar},
                                                                            {"@ServerName", ServerName, strVar}})
            If DataReader.HasRows Then
                DataReader.Read()
                Return "EXISTS"
            Else
                Return "SUCCESS"
            End If
        Catch ex As Exception
            Debug.WriteLine("Cannot find connection String")
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return "ERROR"
    End Function

    ''' <summary>
    ''' Adds or Updates a Workstation Connection String, depending on if it already existed or not
    ''' </summary>
    ''' <param name="WSID"></param>
    ''' <param name="ConnectionName"></param>
    ''' <remarks></remarks>
    Public Shared Sub addupdateWSConnectionString(WSID As String, ConnectionName As String)
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("addupdateWSConnectionString", "CONFIG", {{"@WSID", WSID, strVar},
                                                                              {"@ConnectionName", ConnectionName, strVar}})
        Catch ex As Exception
            Debug.WriteLine("Error Occcured on Setting WS Connection String")
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Deletes a Connection String from the config database
    ''' </summary>
    ''' <param name="Connection"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function deleteConnectionString(Connection As String) As Boolean
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("deleteConnectionString", "CONFIG", {{"@ConnectionName", Connection, strVar}})
        Catch ex As Exception
            Debug.WriteLine("Error Occcured on Deleting WS Connection String")
            Return False
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Gets a list of all Connection Strings from the Config Database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAllConnectionStrings() As List(Of Object)
        Dim DataReader As SqlDataReader = Nothing
        Dim returnList As New List(Of Object)
        Try
            DataReader = RunSPArray("getAllConnections", "CONFIG", {{"nothing"}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    returnList.Add(New With {.name = DataReader(0), .dbName = DataReader(2), .serverName = DataReader(1)})
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine("Cannot Get All Connection Strings")
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return returnList
    End Function

    ''' <summary>
    ''' Gets a Workstation Connection String from the Config Database
    ''' </summary>
    ''' <param name="WSID"></param>
    ''' <param name="sessionRef"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getConnectionString(WSID As String, sessionRef As HttpSessionStateBase) As String
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("getConnectionString", "CONFIG", {{"@WSID", WSID, strVar}})
            If DataReader.HasRows Then
                DataReader.Read()

                If Not IsNothing(sessionRef) Then
                    sessionRef("ConnectionString") = DataReader("Connection Name")
                End If

                Dim Fields As List(Of String) = Enumerable.Range(0, DataReader.FieldCount).Select(Function(x) DataReader.GetName(x)).ToList()

                If Fields.Contains("Username") AndAlso Fields.Contains("Password") Then
                    If DataReader("Username") <> "" AndAlso DataReader("Password") <> "" Then
                        Return "Data Source=" & DataReader.Item("Server Name") & ";Initial Catalog=" & DataReader.Item("Database Name") & ";Integrated Security=False;User ID=" + DataReader("Username") + ";Password=" + DataReader("Password")
                    End If
                End If

                Return "Data Source=" & DataReader.Item("Server Name") & ";Initial Catalog=" & DataReader.Item("Database Name") & ";Integrated Security=SSPI;"
            Else
                Return "NO ENTRY"
            End If
        Catch ex As Exception
            Debug.WriteLine("Cannot find connection String")
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return "DNE"
    End Function

    ''' <summary>
    ''' Checks the AD preference to see if SQL or AD Login should be used
    ''' </summary>
    ''' <param name="WSID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function isActiveDirectory(WSID As String) As Boolean
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selSysPrefDomainAuth", WSID, {{"nothing"}})
            If DataReader.HasRows Then
                DataReader.Read()
                Return DataReader("Domain Authentication")
            End If
        Catch ex As Exception
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Runs a query to get all valid workstations from the Config DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAllWorkstations() As Dictionary(Of String, String)
        Dim reader As SqlDataReader = Nothing
        Dim foundWorkstations As New Dictionary(Of String, String)
        Try
            reader = RunSPArray("selValidWorkstationsAll", "CONFIG", {{"nothing"}})
            If reader.HasRows Then
                While reader.Read()
                    ' reader 0 = pc name, reader 1 = wsid
                    If Not IsDBNull(reader(0)) And Not IsDBNull(reader(1)) Then
                        foundWorkstations.Add(reader(0), reader(1))
                    End If
                End While
            End If
        Catch ex As Exception
            insertErrorMessages("Config", "GetAllWorkstations", ex.Message, "CONFIG", "CONFIG")
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try

        Return foundWorkstations
    End Function

    ''' <summary>
    ''' Returns the current default application for the workstation
    ''' </summary>
    ''' <param name="WSID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getWSDefaultApp(WSID As String) As String
        Dim reader As SqlDataReader = Nothing
        Try
            reader = RunSPArray("selWSDefaultApp", "CONFIG", {{"@WSID", WSID, strVar}})
            If reader.HasRows Then
                While reader.Read()
                    Return reader(0)
                End While
            End If
        Catch ex As Exception
            insertErrorMessages("Config", "getWSDefaultApp", ex.Message, "CONFIG", "CONFIG")
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try
        Return ""
    End Function

    Public Shared Function getSecurityEnvironment() As String
        Return SecurityConfig.getSecurityEnvironment()
    End Function

End Class
