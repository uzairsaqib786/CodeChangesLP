' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports System.ServiceProcess

Public Class GlobalConfigHub
    Inherits Hub

    Public Overrides Function OnConnected() As Task
        ' checks if the print service is the one connecting
        Dim name As String = Context.QueryString.Get("currentUser")
        Dim WSID As String = Context.QueryString.Get("WSID")
        If name = PrintService.PrintServiceHubName Then
            PrintService.PrintServiceRunning = True
            Clients.All.alertPrintServiceStateChange(True)
        End If
        'Adds a user to their own unique group by a value passed in during connection
        Groups.Add(Context.ConnectionId, name)
        Groups.Add(Context.ConnectionId, WSID)
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
        'Calls the Original Onconnected Function pass control back to the server
        Return MyBase.OnConnected()
    End Function

    ''' <summary>
    ''' Starts the print service
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function startService() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return PrintService.StartPrintService(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Stops the print service
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function stopService() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return PrintService.StopPrintService(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function
    ''' <summary>
    ''' Starts the CCSIF service
    ''' </summary>
    ''' <returns></returns>
    Public Function startCCSIF() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return CCSIFService.StartService()
                                                 End Function)
    End Function
    ''' <summary>
    ''' Stops the CCSIF service
    ''' </summary>
    ''' <returns></returns>
    Public Function stopCCSIF() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return CCSIFService.StopService()
                                                 End Function)
    End Function

    ''' <summary>
    ''' Restarts the print service
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function restartService() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return PrintService.RestartPrintService(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function


    ''' <summary>
    ''' Starts the STE service
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function startSTEService() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return STEService.StartSTEService(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Stops the STE service
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function stopSTEService() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return STEService.StopSTEService(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function


    ''' <summary>
    ''' Restarts the STE service
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function restartSTEService() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return STEService.RestartSTEService(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Deletes a connection string
    ''' </summary>
    ''' <param name="Connection">The connection to delete</param>
    ''' <returns>A boolean telling if the operation completed succcessfully</returns>
    ''' <remarks></remarks>
    Public Function deleteConnection(Connection As String) As Task(Of Boolean)
        Return Task.Factory.StartNew(Function()
                                         Return Config.deleteConnectionString(Connection)
                                     End Function)
    End Function

    ''' <summary>
    ''' Saves a connection string
    ''' </summary>
    ''' <param name="oldConnection">The connection that is getting replaced</param>
    ''' <param name="newConnection">The new connection</param>
    ''' <param name="db">The database for the connection</param>
    ''' <param name="server">The server for the connection</param>
    ''' <returns>A string telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveConnection(oldConnection As String, newConnection As String, db As String, server As String) As Task(Of String)
        Return Task.Factory.StartNew(Function()
                                         Return Config.addUpdateConnectionString(IIf(oldConnection = "New", 1, 0), oldConnection, newConnection, db, server)
                                     End Function)
    End Function


    ''' <summary>
    ''' Sets Location Assignments SQL connection string
    ''' </summary>
    ''' <param name="connectionName"></param>
    ''' <remarks></remarks>
    Public Sub setLAConnectionString(connectionName As String)

        Config.addupdateWSConnectionString("IEWSID", connectionName)
        If userCS.ContainsKey("IEWSID") Then
            Dim connVal As String = ""
            userCS.TryRemove("IEWSID", connVal)
        End If
    End Sub

    ''' <summary>
    ''' Inserts a new printer entry that the service instance can access
    ''' </summary>
    ''' <param name="printer">The printer name</param>
    ''' <param name="address">The address to the printer</param>
    ''' <param name="label">If the printer is for labels</param>
    ''' <returns>A string telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function insertNewPrinter(printer As String, address As String, label As Boolean) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        DataReader = RunSPArray("insPrinter", "CONFIG", {{"@PrinterName", printer, strVar}, {"@PrinterString", address, strVar},
                                                                                                          {"@Label", IIf(label, 1, 0), intVar}})
                                                        If DataReader.HasRows Then
                                                            Return "Printer name already exists!"
                                                        End If

                                                    Catch ex As Exception
                                                        Debug.WriteLine(ex.ToString())
                                                        insertErrorMessages("GlobalConfigHub", "insertNewPrinter", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Return "Unknown error occurred.  Contact Scott Tech for support if this persists."
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return ""
                                                End Function)
    End Function

    ''' <summary>
    ''' Updates a printer name, address and type
    ''' </summary>
    ''' <param name="currPrinter">The printer name that is going to be replaced</param>
    ''' <param name="newPrinter">Thew new printer name</param>
    ''' <param name="address">The new adress for the printer</param>
    ''' <param name="label">If the new printer is a a label printer</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateCurrentPrinter(currPrinter As String, newPrinter As String, address As String, label As Boolean) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Dim success As Boolean = True

                                                    Try
                                                        DataReader = RunSPArray("updatePrinters", "CONFIG", {{"@currentName", currPrinter, strVar}, {"@PrinterName", newPrinter, strVar}, _
                                                                                                              {"@PrinterString", address, strVar}, {"@Label", IIf(label, 1, 0), intVar}})
                                                        If DataReader.HasRows Then
                                                            success = False
                                                        End If
                                                    Catch ex As Exception
                                                        Debug.WriteLine(ex.ToString())
                                                        insertErrorMessages("GlobalConfigHub", "updateCurrentPrinter", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Return "Unknown error occurred.  Contact Scott Tech for support if this persists."
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    If success Then
                                                        For Each conn In Config.getAllConnectionStrings()
                                                            RunActionSP("UpdateWSPrinters", {{"@OldPrinter", currPrinter, strVar}, {"@NewPrinter", newPrinter, strVar}}, "Data Source=" & conn.serverName & ";Initial Catalog=" & conn.dbName & ";Integrated Security=SSPI;")
                                                        Next
                                                    End If

                                                    Return ""
                                                End Function)

    End Function

    ''' <summary>
    ''' Deletes the specified printer from the sql table
    ''' </summary>
    ''' <param name="printer">The printer to delete</param>
    ''' <returns>Boolean telling if the oepration completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deletePrinter(printer As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success = True

                                                     Try
                                                         RunActionSP("DeletePrinter", "CONFIG", {{"@PrinterName", printer, strVar}})
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("GlobalConfigHub", "deletePrinter", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         success = False
                                                     End Try

                                                     If success Then
                                                         For Each conn In Config.getAllConnectionStrings()
                                                             RunActionSP("UpdateWSPrinters", {{"@OldPrinter", printer, strVar}, {"@NewPrinter", "No Printer", strVar}}, "Data Source=" & conn.serverName & ";Initial Catalog=" & conn.dbName & ";Integrated Security=SSPI;")
                                                         Next
                                                     End If


                                                     Return success
                                                 End Function)
    End Function
    ''' <summary>
    ''' Deletes a workstation
    ''' </summary>
    ''' <param name="WSID">The workstation to delete</param>
    ''' <returns>A string telling if the operation was successful</returns>
    Public Function DeleteWS(WSID As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("DelWS", "CONFIG", {{"@WSID", WSID, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("GlobalConfigHub", "DeleteWS", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Updates and validates a license
    ''' </summary>
    ''' <param name="LicenseString">The new license string</param>
    ''' <param name="AppName">The app name for the license</param>
    ''' <param name="DisplayName">The name for the license</param>
    ''' <param name="AppUrl">The url for the application</param>
    ''' <returns>The number of licenses</returns>
    ''' <remarks></remarks>
    Public Function SaveValidateLicense(LicenseString As String, AppName As String, DisplayName As String, AppUrl As String) As Integer
        Try
            RunActionSP("updAppLicense", "CONFIG", {{"@LicenseString", LicenseString, strVar}, _
                                                                           {"@AppURL", AppUrl, strVar}, _
                                                                           {"@DisplayName", DisplayName, strVar}, _
                                                                           {"@AppName", AppName, strVar}})

            Dim numLicenses = AESCrypter.ValidateLicense(LicenseString, AppName)
            Config.AppLicenses = AESCrypter.GetAppPermissions()
            Return numLicenses
        Catch ex As Exception
            insertErrorMessages("GlobalConfigHub", "SaveValidateLicense", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
        End Try
        Return -1
    End Function

    ''' <summary>
    ''' Adds an app to the specified workstation
    ''' </summary>
    ''' <param name="WSID">The workstation which is getting the new app</param>
    ''' <param name="App">The app to be added</param>
    ''' <returns>String telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function AddWSApp(WSID As String, App As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("insWSAvailApps", "CONFIG", {{"@WSID", WSID, strVar}, _
                                                                                                 {"@AppName", App, strVar}})


                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("GlobalConfigHub", "AddWSApp", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Deletes an app from the specified workstation
    ''' </summary>
    ''' <param name="WSID">The workstation whose app is getting removeds</param>
    ''' <param name="App">The app to remove</param>
    ''' <returns>A string telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function DeleteWSApp(WSID As String, App As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("delWSAvailApps", "CONFIG", {{"@WSID", WSID, strVar}, _
                                                                                                 {"@AppName", App, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("GlobalConfigHub", "DeleteWSApp", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Selects all apps available to the specified workstation
    ''' </summary>
    ''' <param name="WSID">The desird workstation</param>
    ''' <returns>An object that contains the app infor the given workstation</returns>
    ''' <remarks></remarks>
    Public Function SelectWSApp(WSID As String) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Dim WSApps As New List(Of String)
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        DataReader = RunSPArray("selWSAvailApps", "CONFIG", {{"@WSID", WSID, strVar}})

                                                        If DataReader.HasRows Then
                                                            While DataReader.Read()
                                                                For x As Integer = 0 To DataReader.FieldCount - 1
                                                                    WSApps.Add(CheckDBNull(DataReader(x)))
                                                                Next
                                                            End While
                                                        End If
                                                    Catch ex As Exception
                                                        insertErrorMessages("Consolidation Hub", "selConsolLeftTableData", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Close()
                                                        End If
                                                    End Try
                                                    Return New With {.WSApps = WSApps, .DefaultApp = Config.getWSDefaultApp(WSID)}
                                                End Function)
    End Function

    ''' <summary>
    ''' Sets an app to be the specified workstation's default
    ''' </summary>
    ''' <param name="AppName">The app to be set as default</param>
    ''' <param name="WSID">The desired workstation</param>
    ''' <returns>String telling if the operation completed succesfully</returns>
    ''' <remarks></remarks>
    Public Function setDefaultApp(AppName As String, WSID As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String

                                                    Try
                                                        RunActionSP("updWSDefaultApp", "CONFIG", {{"@WSID", WSID, strVar}, {"@AppName", AppName, strVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("Consolidation Hub", "selConsolLeftTableData", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Return "Error"
                                                    End Try
                                                    Return ""
                                                End Function)
    End Function

    ''' <summary>
    ''' Sends a series of test export/print requests based on user choice to the service and returns the immediate results of the request.
    ''' </summary>
    ''' <param name="type">Custom, System or both types of reports</param>
    ''' <param name="app">Application Name ("IM", "WM", "Admin", etc. or "all")</param>
    ''' <param name="allWorkstations">WSID = "DEFAULT" or use every WSID</param>
    ''' <param name="exportType">Directly print, export to file, or do both.</param>
    ''' <param name="printers">List of the printers to be included in this test.  Has no effect if exportType is not Print or Both</param>
    ''' <param name="printIsLabel">List of the printer types (matched by index to printers) -> True = Label Printer, else List/Report Printer</param>
    ''' <returns>New With {.ExpectedResults = List(of TestPrintResult), .Errors = List(of Object[New With {.Message = ex.ToString(), .Type = ex.GetType(), .sp = Stored Procedure/SQL, .file = file w/out path, .area = failure line #}])}</returns>
    ''' <remarks></remarks>
    Public Function TestPrint(type As ServiceTest.ServiceTestOutputType, app As String, allWorkstations As Boolean, _
                            exportType As ServiceTest.ServiceTestPrintExport, printers As List(Of String), printIsLabel As List(Of Boolean)) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Return ServiceTest.Test(type, Context, Clients, app, allWorkstations, exportType, printers, printIsLabel)
                                                End Function)
    End Function

    ''' <summary>
    ''' Method invoked by the print service once a single request has been processed.
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Public Sub TestPrintResult(ByVal result As TestPrintResult)
        If Not IsNothing(result.ExportDataReceived) Then
            result.ExportDataReceived.ExportPath = IO.Path.GetFileName(result.ExportDataReceived.ProjectFile)
            result.Printer += (" (" & result.ExportDataReceived.DesiredFormat & ")")
        End If
        If Not IsNothing(result.PrintDataReceived) Then
            result.PrintDataReceived.ReportLocation = IO.Path.GetFileNameWithoutExtension(result.PrintDataReceived.ReportLocation)
        End If
        Clients.All.testPrintResult(result)
    End Sub

    ''' <summary>
    ''' Attempts to delete the files generated as a result of the test print function
    ''' </summary>
    ''' <returns>A boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function DeleteLeftovers() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim user As String = Context.User.Identity.Name, WSID As String = Context.QueryString.Get("WSID")
                                                     Try
                                                         Dim location As String = Context.Request.GetHttpContext.Server.MapPath("~/temp/")
                                                         recursiveDeleteFiles(location, user, WSID)
                                                     Catch ex As Exception
                                                         Debug.Print(ex.ToString())
                                                         insertErrorMessages("GlobalConfigHub", "DeleteLeftovers", ex.ToString(), user, WSID)
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Recurses on the path provided in userDirectory to attempt to delete every folder/file within the ServiceTest directories.
    ''' </summary>
    ''' <param name="userDirectory">The directory which contains the files</param>
    ''' <param name="user">The user who is currenlty logged in</param>
    ''' <param name="WSID">The workstation that is currently being worked on</param>
    ''' <remarks></remarks>
    Private Shared Sub recursiveDeleteFiles(userDirectory As String, user As String, WSID As String)
        ' delete top level files
        If IO.Path.GetDirectoryName(userDirectory).ToLower() = "servicetest" Then
            For Each f As String In IO.Directory.GetFiles(userDirectory)
                Try
                    System.IO.File.Delete(f)
                Catch ioex As IO.IOException
                    ' ignore this, the file is locked or being downloaded or something
                Catch uae As UnauthorizedAccessException
                    ' ignore this, file could be locked, or we don't have permission or it's an exe in use, read-only, etc.
                Catch ex As Exception
                    ' we have an issue with the path being unmapped or directory not found or delete(nothing) or delete("") or path isn't supported or path is too long
                    Debug.WriteLine(ex.ToString())
                    insertErrorMessages("LLPreviewExportController", "recursiveDeleteFiles", ex.ToString(), user, WSID)
                    Exit Sub
                End Try
            Next
        End If

        ' parse sub directories, try deleting the whole folder first and if it fails, try deleting individual files.  This will likely never be an issue, because the designer shouldn't 
        ' be creating new directories within the folder and the export process shouldn't either.  So this block most likely isn't necessary, but it will clean up anything that shouldn't be
        ' there
        For Each d As String In IO.Directory.GetDirectories(userDirectory)
            If IO.Path.GetDirectoryName(d).ToLower() = "servicetest" Then
                Try
                    IO.Directory.Delete(d)
                Catch ioex As IO.IOException
                    recursiveDeleteFiles(d, user, WSID)
                Catch uae As UnauthorizedAccessException
                    recursiveDeleteFiles(d, user, WSID)
                Catch ex As Exception
                    ' we have an issue with the path being unmapped or directory not found or delete(nothing) or delete("") or path isn't supported or path is too long
                    Debug.WriteLine(ex.ToString())
                    insertErrorMessages("LLPreviewExportController", "recursiveDeleteFiles", ex.ToString(), user, WSID)
                    Exit Sub
                End Try
            Else
                recursiveDeleteFiles(d, user, WSID)
            End If
        Next
    End Sub

    ''' <summary>
    '''Selects SQL Auth Login info for given connection
    ''' </summary>
    ''' <param name="Connection">The connection to get he login info for</param>
    ''' <returns>An object that returns the login info</returns>
    ''' <remarks></remarks>
    Public Function SelectConnectionUserPass(Connection As String) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Dim LoginInfo As Object = New With {.User = "", .Pass = ""}
                                                    Try
                                                        DataReader = RunSPArray("selDataSourcesUserPass", "CONFIG", {{"@ConnectionName", Connection, strVar}})

                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            LoginInfo.User = DataReader("Username")
                                                            LoginInfo.Pass = DataReader("Password")
                                                        End If
                                                    Catch ex As Exception
                                                        LoginInfo = New With {.User = "Error", .Pass = "Error"}
                                                        insertErrorMessages("Consolidation Hub", "SelectConnectionUserPass", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Close()
                                                        End If
                                                    End Try
                                                    Return LoginInfo
                                                End Function)
    End Function


    ''' <summary>
    '''Selects SQL Auth Login info for given connection
    ''' </summary>
    ''' <param name="Connection">The connection to get he login info for</param>
    ''' <returns>An object that returns the login info</returns>
    ''' <remarks></remarks>
    Public Function UpdateConnectionUserPass(Connection As String, Username As String, Password As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean

                                                     Dim LoginInfo As Object = New With {.User = "", .Pass = ""}
                                                     Try
                                                         RunActionSP("updDataSourcesUserPass", "CONFIG", {{"@ConnectionName", Connection, strVar}, {"@User", Username, strVar},
                                                                                                          {"@Pass", Password, strVar}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("Consolidation Hub", "SelectConnectionUserPass", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function
End Class
