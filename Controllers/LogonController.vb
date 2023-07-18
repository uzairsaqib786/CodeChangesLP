' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.DirectoryServices
Imports System.DirectoryServices.ActiveDirectory
Imports System.DirectoryServices.AccountManagement
Imports DocumentFormat.OpenXml.Office2010.ExcelAc
Imports ActiveDirectorySecurityDriver

Public Class LogonController
    Inherits Controller

    Private Shared ConfigBroadcaster As GlobalConfigBroadcaster = GlobalConfigBroadcaster.Instance

    ' Index login
    ''' <summary>
    ''' Checks for Valid Username and password, signing in the user if it is found
    ''' </summary>
    ''' <param name="username">Current user from logon attempt.</param>
    ''' <param name="password">Submitted password from logon attempt.</param>
    ''' <param name="ReturnUrl">URL to redirect to upon successful logon.</param>
    ''' <returns>The view for either a successful or failed log in</returns>
    ''' <remarks></remarks>
    <HttpPost()>
    Function Index(ByVal username As String, ByVal password As String, ByVal ReturnUrl As String) As ActionResult
        ' check the input data against the database
        Dim DataReader As SqlDataReader = Nothing
        Dim logonInfo As New List(Of String)

        'If Config.isActiveDirectory(Session("WSID")) AndAlso Membership.ValidateUser(username, password) Then
        '    TempData("Connections") = Config.getAllConnectionStrings
        '    ViewData("Error") = "AD Authentication Worked"
        '    Return View(logonData())
        'End If

        ' ADLDS LOGIN AUTHENTICATION THROUGH MEMBERSHIP PROVIDER
        If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
            Try
                Dim message As String = AuthenticationDriver.AuthenticateUser(Trim(username), Trim(password))
                If message = "Success" Then
                Else
                    Debug.WriteLine(message)
                    insertErrorMessages("LogonController", "Index", message, User.Identity.Name, Session("WSID"))
                    TempData("Connections") = Config.getAllConnectionStrings
                    ViewData("Error") = message
                    Return View(logonData())
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("LogonController", "Index", ex.ToString(), User.Identity.Name, Session("WSID"))
                TempData("Connections") = Config.getAllConnectionStrings
                ViewData("Error") = ex.Message
                Return View(logonData())
            End Try
            username = username.Split("@"c)(0)
        End If

        If (Trim(username) <> "" And Trim(password) <> "") Then
            Try
                DataReader = RunSPArray("selEmployeesLoginCheck", Session("WSID"), {{"@Username", username, strVar}, {"@Password", password, strVar}, {"@WSID", Session("WSID"), strVar}, {"@SecurityEnvironment", SecurityConfig.getSecurityEnvironment, strVar}})
                If DataReader.HasRows Then
                    'If Logon Succeeded
                    DataReader.Read()
                    If DataReader(0) <> "" Then
                        logonInfo.AddRange({DataReader(0), DataReader(1)})
                    End If
                    ' if it's a new workstation then make copies of all the report designs that can get customized
                    GlobalFunctions.copyIfNewReportDesigns(User.Identity.Name, Session("WSID"), Server)
                    FormsAuthentication.SetAuthCookie(logonInfo(0), False)

                    'Session Variables get Set on Logon
                    GlobalFunctions.initializeSession(user:=logonInfo(0),
                                                           sessionRef:=Session, tempRef:=TempData, WSID:=Session("WSID"), clientCert:=Request.ClientCertificate, request:=Request)

                    'Updates list of Connected Users on Global Config Page
                    ConfigBroadcaster.addConnectedUser(logonInfo(0), Session("WSID"))

                    Dim DefaultApp = Config.getWSDefaultApp(Session("WSID"))

                    If (DefaultApp <> "") Then
                        Return RedirectToAction("Index", "Menu", New With {.Area = DefaultApp})
                    End If

                    'redirects user to a page if they were redirected to logon
                    If Url.IsLocalUrl(ReturnUrl) Then
                        Return Redirect(ReturnUrl)
                    Else
                        Return RedirectToAction("Index", "Menu", New With {.Area = ""})
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("LogonController", "Index", ex.ToString(), User.Identity.Name, Session("WSID"))
                TempData("Connections") = Config.getAllConnectionStrings
                ViewData("Error") = "Username or Password was incorrect"
                Return View(logonData())
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
        Else
            'If No Username of Password were Entered
            ViewData("Error") = "No Username or Password was Entered"
        End If
        'Failed Login
        TempData("Connections") = Config.getAllConnectionStrings
        ViewData("Error") = "Incorrect Username or Password"
        Return View(logonData())
    End Function

    ' return empty form if there has not been a login yet
    ''' <summary>
    ''' Initial return of the view of Logon when no POST action has been taken.
    ''' </summary>
    ''' <param name="returnUrl">Redirection requested after successful logon.</param>
    ''' <returns>Returns view of Logon.</returns>
    ''' <remarks></remarks>
    Function Index(ByVal returnUrl As String) As ActionResult
        'If no certificate was supplied, redirect to Cert Generate page
        If Not Request.ClientCertificate.IsPresent And Not GlobalFunctions.Testing And Not (GlobalFunctions.isAndroid(Request) And Request.Cookies.AllKeys.Contains("androidWSID")) Then
            Return RedirectToAction("Index", "Certificates")
        End If


        ' if we're already loggged in then don't go to the logon page
        If User.Identity.IsAuthenticated Then
            If returnUrl <> "" Then
                Return Redirect(returnUrl)
            Else
                Return Redirect("/Menu/")
            End If
        End If

        'If No Global Config String, redirect
        If Config.getConfigDBString() = "" Then
            TempData("Conn") = False
            Return RedirectToAction("Index", "GlobalConfig")
        End If

        TempData("Connections") = Config.getAllConnectionStrings

        If Not GlobalFunctions.Testing Then
            If GlobalFunctions.isAndroid(Request) Then
                Session("WSID") = Request.Cookies.Get("androidWSID").Value
            Else
                Session("WSID") = Certificates.resolveWSID(Request.ClientCertificate)
            End If

        Else
            Session("WSID") = "TESTWSID"
        End If


        Dim containsKey = userCS.ContainsKey(Session("WSID"))
        If Not containsKey Or Not TempData("Conn") Then
            Dim conn = Config.getConnectionString(Session("WSID"), Session)
            If conn = "NO ENTRY" Then
                TempData("Conn") = False
                Return View(New List(Of String))
            Else
                TempData("Conn") = True
            End If
            If Not containsKey Then
                userCS.TryAdd(Session("WSID"), conn)
            Else
                userCS(Session("WSID")) = conn
            End If
        End If

        ViewData("ReturnURL") = returnUrl
        Return View(logonData())
    End Function

    ' logout handling
    ''' <summary>
    ''' Handles logging out a user.
    ''' </summary>
    ''' <returns>Returns a redirection to the logon screen.</returns>
    ''' <remarks></remarks>
    Function Logout() As ActionResult
        Dim wsid As String = Session("WSID")
        'Updates list of Connected Users on Global Config Page
        ConfigBroadcaster.removeConnectedUser(wsid)


        If (wsid <> "" And wsid <> Nothing) Then
            Try
                Dim connVal As String = ""
                userCS.TryRemove(wsid, connVal)
            Catch ex As Exception
                Debug.WriteLine("Key Does not Exist")
            End Try
            Dim removedUser As ConnectedUser = Nothing
        End If

        FormsAuthentication.SignOut()
        Session.Abandon()
        Return RedirectToAction("Index")
    End Function

    ''' <summary>
    ''' Returns company name, address, city, state.
    ''' </summary>
    ''' <returns>Returns company name, address, city, state from System Preferences table.</returns>
    ''' <remarks></remarks>
    Private Function logonData() As List(Of String)
        Dim model As New List(Of String)
        Dim DataReader As SqlDataReader = Nothing

        Try
            DataReader = RunSPArray("CompanyInfo", Session("WSID"), {{"nothing"}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        If Not IsDBNull(DataReader(x)) Then
                            model.Add(DataReader(x))
                        Else
                            model.Add("")
                        End If
                    Next
                End While
            End If

            model.Add(GlobalFunctions.getCompanyLogoExtension(User.Identity.Name, Session("WSID")))
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Logon", "Index(returnURL as String)", ex.ToString(), "Unauthenticated User", Session("WSID"))
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return model
    End Function
End Class