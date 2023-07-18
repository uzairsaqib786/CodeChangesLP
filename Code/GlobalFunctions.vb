' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports combit.ListLabel21
Imports System.Drawing.Printing
Imports ActiveDirectorySecurityDriver

Public Class GlobalFunctions
    ' initialized in global.asax application_start
    Public Shared Testing As Boolean = Debugger.IsAttached



    Public Shared AppSharedPages As New Dictionary(Of String, List(Of String))


    'List of OM Columns since they are not defined by a single table
    Public Shared OMColumns As New List(Of String) From {"Transaction Type", "Order Number", "Priority", "Required Date", "User Field1",
                                                             "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8",
                                                             "User Field9", "User Field10", "Item Number", "Description", "Line Number", "Transaction Quantity", "Allocated Picks",
                                                             "Allocated Puts", "Available Quantity", "Stock Quantity", "Warehouse", "Zone", "Line Sequence",
                                                             "Tote ID", "Tote Number", "Unit of Measure", "Batch Pick ID", "Category", "Sub Category", "Import By", "Import Date",
                                                             "Import Filename", "Expiration Date", "Lot Number", "Serial Number", "Notes", "Revision", "Supplier Item ID", "ID",
                                                             "Host Transaction ID", "Emergency", "Location", "Label", "Cell"}

    'List of OM Create Columns since they are not defined by a single table
    Public Shared OMCreateColumns As New List(Of String) From {"Transaction Type", "Order Number", "Priority", "Required Date", "User Field1",
                                                             "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8",
                                                             "User Field9", "User Field10", "Item Number", "Description", "Line Number", "Transaction Quantity",
                                                             "Warehouse", "Line Sequence", "In Process", "Processing By",
                                                             "Unit of Measure", "Import By", "Import Date",
                                                             "Import Filename", "Expiration Date", "Lot Number", "Serial Number", "Notes", "Revision", "ID",
                                                             "Host Transaction ID", "Emergency", "Label", "Batch Pick ID", "Tote ID", "Cell"}


    ''' <summary>
    ''' Instance of the globalbroadcaster for alerting users of emergency orders, etc.  Need instance, because this class is not a hub
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared GlobalHubBroadcaster As GlobalBroadcaster = GlobalBroadcaster.Instance()

    ''' <summary>
    ''' Returns the layout file to use for screens shared between multiple Apps
    ''' </summary>
    ''' <param name="appName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function chooseLayoutFile(appName As String) As String
        Return "~/Areas/" & Config.getFullAppName(appName) & "/Views/_" & appName & "Layout.vbhtml"
        Select Case appName
            Case "CM"
                Return "~/Areas/Consolidation Manager/Views/_ConsolidationLayout.vbhtml"
            Case "Admin"
                Return "~/Areas/ICSAdmin/Views/_AdminLayout.vbhtml"
            Case "IE"
                Return "~/Areas/ImportExport/Views/_ImportExportLayout.vbhtml"
            Case "OM"
                Return "~/Areas/OrderManager/Views/_OMLayout.vbhtml"
            Case "IM"
                Return "~/Areas/Induction/Views/_InductionLayout.vbhtml"
            Case "WM"
                Return "~/Areas/WorkManager/Views/_WMLayout.vbhtml"
            Case Else
                Return "~/Views/Shared/_" & appName & "Layout.vbhtml"
        End Select
    End Function


    ''' <summary>
    ''' Checks if current browser is connection from Android
    ''' </summary>
    ''' <param name="request"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function isAndroid(request As HttpRequestBase) As Boolean
        Return request.UserAgent.ToLower().Contains("android")
    End Function

    ''' <summary>
    ''' Returns the next Batch ID returned from Sql Server
    ''' </summary>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <param name="User">The user that is currently logged in</param>
    ''' <returns>A string containing the next batch id</returns>
    ''' <remarks></remarks>
    Public Shared Function getNextBatchID(WSID As String, User As String) As String
        Dim batch As String = ""
        Try
            batch = GetResultSingleCol("selNextBatchID", WSID, {{"nothing"}})
        Catch ex As Exception
            insertErrorMessages("MoveItems", "getNextBatchID", ex.Message, User, WSID)
        End Try
        Return batch
    End Function


    ''' <summary>
    ''' Get all Users Permissions and store's in as a session variable for use across the application
    ''' </summary>
    ''' <param name="user">User to get Permissions for</param>
    ''' <param name="sessionRef">Reference to a user's session to store the data</param>
    ''' <remarks></remarks>
    Private Shared Sub setUserPermissions(user As String, sessionRef As HttpSessionStateBase, WSID As String)
        sessionRef("Permissions") = GlobalFunctions.getUserRights(user, WSID)
    End Sub



    'Used in PickProController for any pages that require specific permissions to enter them.
    'Also re-sets session variables if the are lost(Only happens in Debug mode, issue with Visual Studio)
    ''' <summary>
    ''' Initlialzes all session variables for a user on login/server reset
    ''' </summary>
    ''' <param name="user">User name to set session variables for</param>
    ''' <param name="sessionRef">Reference to the users session</param>
    ''' <remarks></remarks>
    Public Shared Function initializeSession(user As String, sessionRef As HttpSessionStateBase,
                                              ByRef tempRef As System.Web.Mvc.TempDataDictionary, clientCert As HttpClientCertificate, WSID As String, request As HttpRequestBase) As List(Of String)
        If Not clientCert.IsPresent And Not Testing And Not (isAndroid(request) And request.Cookies.AllKeys.Contains("androidWSID")) Then
            Return New List(Of String) From {"Index", "Certificates"}
        End If

        Try

            'Gets the Workstations WSID
            If Not Testing Then
                If isAndroid(request) Then
                    sessionRef("WSID") = request.Cookies.Get("androidWSID").Value
                Else
                    sessionRef("WSID") = Certificates.resolveWSID(clientCert)
                End If

                WSID = sessionRef("WSID")
            Else
                sessionRef("WSID") = "TESTWSID"
                WSID = "TESTWSID"
            End If

            If IsNothing(sessionRef("Permissions")) And Not String.IsNullOrEmpty(user) Then
                GlobalFunctions.setUserPermissions(user, sessionRef, WSID)
            End If


            'Checks to make sure App has a Config Database String
            If Config.getConfigDBString() = "" Then
                tempRef("Conn") = False
                Return New List(Of String) From {"Index", "GlobalConfig"}
            End If

            'Get the Current version of this Workstations connection string, otherwise sends user to set string
            If (Not userCS.ContainsKey(WSID) Or sessionRef("ConnectionString") Is Nothing) AndAlso Not request.Path.ToLower.Contains("globalconfig") Then
                Dim conn = Config.getConnectionString(WSID, sessionRef)
                If conn = "NO ENTRY" Then
                    tempRef("Conn") = False
                    Return New List(Of String) From {"Logout", "Logon"}
                End If
                userCS.TryAdd(WSID, conn)
            End If



            getWorkStatPrinters(WSID, user, sessionRef)

        Catch ex As Exception
            SQLFunctions.insertErrorMessages("Globalfunctions", "initializeSession", ex.Message, "User not logged in at this point.", WSID)
        End Try

        'Returns Nothing if Successful
        Return Nothing
    End Function

    ''' <summary>
    ''' Cleans input against the blacklist.  If an illegal term is found, return an empty string
    ''' </summary>
    ''' <param name="inString">String to be cleaned</param>
    ''' <returns>The string that is now cleaned or an empty string</returns>
    ''' <remarks></remarks>
    Public Shared Function DangerAhead(inString As String) As String
        Dim returnString As String = ""
        Dim trimString As String = ""

        If [String].IsNullOrWhiteSpace(inString) Then Return ""

        ' Escape apostrophe character by adding another.  LIKE in SQL requires this or can't finish the query properly.
        ' The search functionality uses a like so anywhere search is used the input must be cleaned by this function
        For x As Integer = 0 To inString.Length - 1
            If inString(x) = "'" Then
                returnString += inString(x) + "'"
            Else
                returnString += inString(x)
            End If
        Next

        For Each strVal In blackList
            If returnString.ToLower.Contains(strVal) Then
                Return ""
            End If
        Next
        Return returnString
    End Function

    'Should be use anywhere Dynamic SQL could not be prevented in SP's(Datatable Queries usually include this, also any query with dynamic columns)
    ''' <summary>
    ''' Cleans the search to prevent injections
    ''' </summary>
    ''' <param name="searchString">The string that user wnast to search for</param>
    ''' <returns>A cleaned version of the string</returns>
    ''' <remarks></remarks>
    Public Shared Function cleanSearch(searchString As String) As String
        ' Cleans input from the search function field so that special characters are escaped.
        Dim returnString As String = ""
        For c As Integer = 0 To searchString.Length - 1
            Select Case searchString(c)
                ' if input is potentially dangerous add escape character (!)
                Case "%", "_", "[", "]", "!"
                    returnString += searchString(c) + "!"
                Case "'"
                    returnString += searchString(c) + "'"
                Case Else
                    returnString += searchString(c)
            End Select
        Next

        Return returnString.Trim()
    End Function

    ' reorders the column sequence according to user defaults, set in TJColumnSequence
    ''' <summary>
    ''' Sets the column sequence by user
    ''' </summary>
    ''' <param name="tableName">The current table</param>
    ''' <param name="user">The current user</param>
    ''' <returns>A list of the columns</returns>
    ''' <remarks></remarks>
    Public Shared Function getDefaultColumnSequence(tableName As String, user As String, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader
        Dim userDefaultList As New List(Of String)

        Try
            DataReader = RunSPArray("selTJColSeq", WSID, {{"@User", user, strVar}, {"@View", tableName, strVar}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        userDefaultList.Add(DataReader(x))
                    Next
                End While
                DataReader.Dispose()
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

        ' if no user defaults are defined return all of the columns in sql's order
        If userDefaultList.Count = 0 Then
            Return getColumns(tableName, user, WSID)
        End If

        ' Open Transactions needs these as a minimum in order for the deletion SP to work properly
        If tableName = "Open Transactions" Then
            If Not userDefaultList.Contains("ID") Then userDefaultList.Add("ID")
            If Not userDefaultList.Contains("Order Number") Then userDefaultList.Add("Order Number")
            If Not userDefaultList.Contains("Transaction Type") Then userDefaultList.Add("Transaction Type")
            If Not userDefaultList.Contains("Item Number") Then userDefaultList.Add("Item Number")
            If Not userDefaultList.Contains("Line Number") Then userDefaultList.Add("Line Number")
        End If

        If tableName = "Open Transactions Temp" Then
            'If userDefaultList.Contains("Reason Message") Then userDefaultList.Remove("Reason Message")
            'If userDefaultList.Contains("Reason") Then userDefaultList.Remove("Reason")
            'If userDefaultList.Contains("Date Stamp") Then userDefaultList.Remove("Date Stamp")
            'If userDefaultList.Contains("Name Stamp") Then userDefaultList.Remove("Name Stamp")

            If userDefaultList.Contains("Reprocess") Then userDefaultList.Remove("Reprocess")
            If userDefaultList.Contains("Post as Complete") Then userDefaultList.Remove("Post as Complete")
            If userDefaultList.Contains("Send to History") Then userDefaultList.Remove("Send to History")
            userDefaultList.Remove("ID")
            userDefaultList.Add("ID")
        End If

        Return userDefaultList
    End Function


    ''' <summary>
    ''' Gets all User Permissions for the Application
    ''' </summary>
    ''' <param name="username">Username to grab permissions for</param>
    ''' <returns>List of permissions and Strings</returns>
    ''' <remarks></remarks>
    Public Shared Function getUserRights(username As String, WSID As String) As List(Of String)
        Dim rightsList As New List(Of String), DataReader As SqlDataReader = Nothing
        Try
            If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                rightsList = AuthorizationDriver.GetRolesForUser("UserGroups", "ACL_Functions", username.Trim()).ToList()
            Else
                DataReader = RunSPArray("selStaffAccessUser", WSID, {{"@UserName", username, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        rightsList.Add(DataReader(0))
                    End While
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("GlobalFunctions", "getUserRights", ex.Message, username, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return rightsList
    End Function

    ''' <summary>
    ''' Returns the extension of the company logo
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A string that contains the extension of the desired logo</returns>
    ''' <remarks></remarks>
    Public Shared Function getCompanyLogoExtension(user As String, WSID As String) As String
        Dim logo As String = "NO EXTENSION FOUND"
        Try
            logo = CheckDBNull(GetResultSingleCol("selCompanyLogo", WSID))
            If String.IsNullOrWhiteSpace(logo) Then
                logo = ".png" ' default to png, because why not
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Preferences", "getCompanyLogoExtension", ex.Message, user, WSID)
        End Try
        Return logo
    End Function

    ''' <summary>
    ''' Gets a User's Access level(Administrator, User, etc)
    ''' </summary>
    ''' <param name="username">Username to get Acces Level of</param>
    ''' <returns>String that contains the User's access level</returns>
    ''' <remarks></remarks>
    Public Shared Function getAccessLevel(username As String, WSID As String) As String
        Dim level As String = ""
        Try
            level = GetResultSingleCol("selEmployeesAccessLevel", WSID, {{"@UserName", username, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("GlobalFunctions", "getAccessLevel", ex.Message, username, WSID)
        End Try
        Return level
    End Function

    ' returns column names from tableName table in the default order that SQL would return them in
    ''' <summary>
    ''' Gets the columns of the currently selected tbale 
    ''' </summary>
    ''' <param name="tableName">The tbale that is currently displayed</param>
    ''' <returns>A list of all the columns in the table</returns>
    ''' <remarks></remarks>
    Public Shared Function getColumns(tableName As String, user As String, WSID As String) As List(Of String)
        If tableName = "Order Manager" Then
            Return OMColumns
        ElseIf tableName = "Order Manager Create" Then
            Return OMCreateColumns
        End If
        Dim innerList = New List(Of String), DataReaderColumns As SqlDataReader = Nothing
        Try
            DataReaderColumns = RunSPArray("selTableCols", WSID, {{"@tableName", tableName, strVar}})
            If DataReaderColumns.HasRows Then
                While DataReaderColumns.Read()
                    innerList.Add(DataReaderColumns(0))
                End While
            End If

            If tableName = "Inventory Map" Then
                ' aliases
                innerList(innerList.IndexOf("Location ID")) = "Alternate Light"
                innerList(innerList.IndexOf("Golden Zone")) = "Velocity Code"
                ' additional columns that are selected not from inventory map or concatenated
                innerList.Add("Laser X")
                innerList.Add("Laser Y")
                innerList.Add("Location Number")
            End If

            If tableName = "Open Transactions Temp" Then
                'nnerList.Remove("Reason Message")
                'innerList.Remove("Reason")
                'innerList.Remove("Date Stamp")
                'innerList.Remove("Name Stamp")
                innerList.Remove("Reprocess")
                innerList.Remove("Post as Complete")
                innerList.Remove("Send to History")
                innerList.Remove("ID")
                innerList.Add("ID")
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("GlobalFunctions", "getColumns", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReaderColumns) Then DataReaderColumns.Dispose()
        End Try
        Return innerList
    End Function

    ''' <summary>
    ''' Returns List of all Printers currently Registered with the Application
    ''' </summary>
    ''' <param name="WSID">The workstation that is currently being worked on</param>
    ''' <param name="user">The user that is currently logged in</param>
    ''' <returns>A list of objects that contains the printers and their info</returns>
    ''' <remarks></remarks>
    Public Shared Function getAllPrinters(WSID As String, user As String) As List(Of Object)
        Dim DataReader As SqlDataReader = Nothing
        Dim printer As String = ""
        Dim printerAdd As String = ""
        Dim label As String = ""
        Dim printerInfo As New List(Of Object)

        Try
            DataReader = RunSPArray("getPrinterDropdown", "CONFIG", {{"nothing"}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    printer = CheckDBNull(DataReader(0))
                    printerAdd = CheckDBNull(DataReader(1))
                    If Not IsDBNull(DataReader(2)) AndAlso DataReader(2) Then
                        label = "Able to Print Labels"
                    Else
                        label = "Not Able to Print Labels"
                    End If
                    printerInfo.Add(New With {.printer = printer, .printeradd = printerAdd, .label = label})
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Preferences", "grabPrinterDropdown", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try

        Return printerInfo
    End Function

    ''''''''''''''''''''''''CURRENTLY DISABLED in Global.asax
    ''' <summary>
    ''' Fires on the timer event in global.asax after 2000 ms.  Also fires on individual calls from additions to the open transactions table (like manual trans).
    ''' Checks if there are emergency transactions in open transactions and calls the globalbroadcaster's alertEmergencyOrders() function to alert clients of any emergency transactions.
    ''' </summary>
    ''' <param name="sender">Timer parameter</param>
    ''' <param name="e">Timer parameter event</param>
    ''' <param name="user">Optional user:= allows a user to be assigned to the method when called via a hub function</param>
    ''' <remarks></remarks>
    Public Shared Sub checkEmergencyTransactions(Optional sender As Object = Nothing, Optional e As System.Timers.ElapsedEventArgs = Nothing, Optional user As String = "checkEmergencyTransactions (no user)", Optional WSID As String = "")
        Dim DataReader As SqlDataReader = Nothing
        Dim emergencies As New Dictionary(Of String, List(Of Object))
        If Config.getConfigDBString() = "" Then
            Exit Sub
        End If
        Dim DBs = Config.getAllConnectionStrings()
        For Each db In DBs
            Try
                DataReader = RunSPArray("selOTEmergency", {{"nothing"}}, "Data Source=" & db.serverName & ";Initial Catalog=" & db.dbName & ";Integrated Security=SSPI;")

                If DataReader.HasRows Then
                    While DataReader.Read()
                        ' WSID
                        If Not IsDBNull(DataReader("WSID")) Then
                            If emergencies.ContainsKey(DataReader("WSID")) Then
                                emergencies(DataReader("WSID")).Add(New With {.Carousel = CheckDBNull(DataReader("Carousel")),
                                                                         .Zone = CheckDBNull(DataReader("Zone"))})
                            Else
                                emergencies.Add(DataReader("WSID"), New List(Of Object) From {New With {.Carousel = CheckDBNull(DataReader("Carousel")),
                                                                         .Zone = CheckDBNull(DataReader("Zone"))}})
                            End If
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("GlobalBroadcaster", "alertEmergencyOrders", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then DataReader.Dispose()
                If emergencies.Count > 0 Then GlobalHubBroadcaster.alertEmergencyOrders(emergencies)
            End Try
        Next
    End Sub



    ''' <summary>
    ''' Sets the session variables for the report and label printers
    ''' </summary>
    ''' <param name="WSID">The current workstation id</param>
    ''' <param name="User">The user currently signed on</param>
    ''' <param name="sessionRef">The User session</param>
    ''' <remarks></remarks>
    Public Shared Function getWorkStatPrinters(WSID As String, User As String, Optional sessionRef As HttpSessionStateBase = Nothing)
        Dim ReportPrinter As String = "", LabelPrinter As String = ""

        Try
            Dim result As Dictionary(Of String, Object) = GetResultMap("selWorkStatPrefsPrinters", WSID, {{"@WSID", WSID, strVar}})
            If result.ContainsKey("Print Report Location") AndAlso Not IsNothing(result("Print Report Location")) Then
                ReportPrinter = result("Print Report Location")
            Else
                ReportPrinter = "No Printer"
            End If

            If result.ContainsKey("Print Label Location") AndAlso Not IsNothing(result("Print Label Location")) Then
                LabelPrinter = result("Print Label Location")
            Else
                LabelPrinter = "No Printer"
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("GlobalFunctions", "getWorkStatPrinters", ex.Message, User, WSID)
        End Try
        If Not IsNothing(sessionRef) Then
            sessionRef("ReportPrinter") = ReportPrinter
            sessionRef("LabelPrinter") = LabelPrinter
        End If
        Return New With {.ReportPrinter = ReportPrinter, .LabelPrinter = LabelPrinter}
    End Function

    ''' <summary>
    ''' Gets the active workstation's printing directory and copies any missing reports over to the workstation's directory
    ''' </summary>
    ''' <param name="Server">Reference to the IIS server Directory Structure</param>
    ''' <param name="user">The suer that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A string that contains the directory for the printer</returns>
    ''' <remarks></remarks>
    Public Shared Function getWSDirectory(Server As HttpServerUtilityBase, user As String, WSID As String) As String
        copyIfNewReportDesigns(user, WSID, Server)
        Return Server.MapPath("~/Reports/LLDesign/" & WSID)
    End Function

    ''' <summary>
    ''' Copies all report designs from the root /Reports directory to the subdirectory for the connecting workstation if the files do not already exist.
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="WSID"></param>
    ''' <param name="Server">Reference to the IIS server Directory Structure</param>
    ''' <remarks></remarks>
    Public Shared Sub copyIfNewReportDesigns(user As String, WSID As String, Server As HttpServerUtilityBase)
        Try
            Dim reportDirectory As String = Server.MapPath("~/Reports/LLDesign/")
            Dim workstationDirectory As String = System.IO.Path.Combine(reportDirectory, WSID)

            'recursiveLLDirectCopy(reportDirectory, workstationDirectory)

            If Not System.IO.Directory.Exists(workstationDirectory) Then
                System.IO.Directory.CreateDirectory(workstationDirectory)
            End If

            For Each f As String In System.IO.Directory.GetFiles(reportDirectory)
                Dim filename As String = System.IO.Path.GetFileName(f)
                Dim copy As String = System.IO.Path.Combine(workstationDirectory, filename)
                If (System.IO.Path.GetExtension(f) = ".lst" Or System.IO.Path.GetExtension(f) = ".lbl") And Not System.IO.File.Exists(copy) Then
                    System.IO.File.Copy(f, copy)
                End If
            Next
        Catch ex As Exception
            insertErrorMessages("copyIfNewReportDesigns", "Index", ex.Message, user, WSID)
        End Try

    End Sub

    ''' <summary>
    ''' Returns the request url, like http://pickpro.scotttech.co/help, etc.
    ''' </summary>
    ''' <param name="request">Current request being made from the user</param>
    ''' <returns>A string that contains the request url</returns>
    ''' <remarks></remarks>
    Public Shared Function getRequestURL(request As HttpRequestBase) As String
        Return "https://" & request.ServerVariables("HTTP_HOST") & HttpContext.Current.Request.RawUrl
    End Function

    ''' <summary>
    ''' Returns the http host (http://pickpro.scotttech.co) as opposed to the full request URL
    ''' </summary>
    ''' <param name="request"></param>
    ''' <returns>A string that cotnains the host url</returns>
    ''' <remarks></remarks>
    Public Shared Function getBaseURL(request As HttpRequestBase) As String
        Return "https://" & request.ServerVariables("HTTP_HOST")
    End Function

    ''' <summary>
    ''' Gets map of what Shared pages can be accessed from which applications
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub InitializeSharedPages()
        Dim reader As SqlDataReader = Nothing
        AppSharedPages.Clear()
        Try
            reader = RunSPArray("selSharedPages", "CONFIG", {{"nothing"}})
            If reader.HasRows Then
                While reader.Read
                    If AppSharedPages.ContainsKey(reader("App Name")) Then
                        AppSharedPages.Item(reader("App Name")).Add(reader("Page"))
                    Else
                        AppSharedPages.Add(reader("App Name"), New List(Of String) From {reader("Page")})
                    End If
                End While
            End If
        Catch ex As Exception
            Debug.Print(ex.Message)
            insertErrorMessages("GlobalFunctions", "InitializeSharedPages", ex.Message, "GlobalFunctions", "CONFIG")
        Finally
            If Not IsNothing(reader) Then reader.Dispose()
        End Try
    End Sub

    'Function that we attempted to see if printer existed before registering it with the Application, did not work as inteded
    'Left in to be modified in future if used
    ' ''' <summary>
    ' ''' Checks if supplied printer exists. Returns true if it does and the default printer's name if it does not exist.
    ' ''' </summary>
    ' ''' <param name="printer"></param>
    ' ''' <returns></returns>
    ' ''' <remarks>Assumes that the Print Service and this application are running under the same domain and access restrictions for printers.  If they're running on different boxes this code
    ' ''' or where it is used will need to be changed.</remarks>
    'Public Shared Function PrinterExists(printer As String) As Boolean
    '    If Not [String].IsNullOrWhiteSpace(printer) Then
    '        For Each printerName As String In PrinterSettings.InstalledPrinters
    '            If printerName.ToUpper().Trim() = printer.ToUpper().Trim() Then
    '                Return True
    '            End If
    '        Next
    '    End If

    '    Return False
    'End Function

    ''' <summary>
    ''' Returns empty string if the value is nothing, otherwise returns value.
    ''' </summary>
    ''' <param name="value">Value to check if it is nothing</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NothingToStr(value) As Object
        If IsNothing(value) Then
            Return ""
        Else
            Return value
        End If
    End Function

    ''' <summary>
    ''' Exports the data contained in the dictData variable to a csv file in the location specified by exportFileLoc
    ''' </summary>
    ''' <param name="exportFileLoc"></param>
    ''' <param name="overwrite">If the file exists and this is set to false then no export will take place.</param>
    ''' <param name="user"></param>
    ''' <param name="WSID"></param>
    ''' <param name="headers">Optional header definition for the csv</param>
    ''' <param name="listData">Data in a list([row]list([col]string)) format to put into the csv.</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Shared Function ExportToCSV(exportFileLoc As String, overwrite As Boolean, user As String, WSID As String,
                                       ByVal listData As List(Of List(Of String)), Optional headers As List(Of String) = Nothing) As Boolean
        Try
            Dim fExists As Boolean = IO.File.Exists(exportFileLoc)
            If fExists And Not overwrite Then
                Return False
            ElseIf fExists Then
                IO.File.Delete(exportFileLoc)
            End If
            Dim dquote As String = """"
            Using newFile As New IO.StreamWriter(exportFileLoc, False)
                If Not IsNothing(headers) Then
                    Dim colStr As String = ""
                    For x As Integer = 0 To headers.Count - 1
                        colStr += (dquote & headers(x) & dquote & ",")
                    Next
                    newFile.WriteLine(colStr.Substring(0, colStr.Length - 1))
                End If
                For row As Integer = 0 To listData.Count - 1
                    Dim colStr As String = ""
                    For col As Integer = 0 To listData(row).Count - 1
                        colStr = (colStr & dquote & listData(row)(col) & dquote & ",")
                    Next
                    newFile.WriteLine(colStr.Substring(0, colStr.Length - 1))
                Next
            End Using
        Catch ex As Exception
            Debug.Print(ex.Message)
            insertErrorMessages("GlobalFunctions", "ExportToCSV", ex.Message, user, WSID)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Exports the data contained in the dictData variable to a csv file in the location specified by exportFileLoc
    ''' </summary>
    ''' <param name="exportFileLoc"></param>
    ''' <param name="overwrite">If the file exists and this is set to false then no export will take place.</param>
    ''' <param name="user">The suer that is currently logged on</param>
    ''' <param name="WSID">The workstation that is currently being worked on</param>
    ''' <param name="dictData">The first set of keys is used as headers, the data within the dictionaries are used as rows in the csv</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Shared Function ExportToCSV(exportFileLoc As String, overwrite As Boolean, user As String, WSID As String,
                                      ByVal dictData As List(Of Dictionary(Of String, Object))) As Boolean
        Try
            Dim fExists As Boolean = IO.File.Exists(exportFileLoc)
            If fExists And Not overwrite Then
                Return False
            ElseIf fExists Then
                IO.File.Delete(exportFileLoc)
            End If
            Dim dquote As String = """"
            Using newFile As New IO.StreamWriter(exportFileLoc, False)
                If dictData.Count > 0 Then
                    Dim colStr As String = ""
                    For Each key As String In dictData(0).Keys
                        colStr = (colStr & dquote & key & dquote & ",")
                    Next
                    newFile.WriteLine(colStr.Substring(0, colStr.Length - 1))
                End If
                For row As Integer = 0 To dictData.Count - 1
                    Dim colStr As String = ""
                    For Each key As String In dictData(row).Keys
                        colStr = (colStr & dquote & NothingToStr(dictData(row)(key)) & dquote & ",")
                    Next
                    newFile.WriteLine(colStr.Substring(0, colStr.Length - 1))
                Next
            End Using
        Catch ex As Exception
            Debug.Print(ex.Message)
            insertErrorMessages("GlobalFunctions", "ExportToCSV", ex.Message, user, WSID)
            Return False
        End Try
        Return True
    End Function

End Class