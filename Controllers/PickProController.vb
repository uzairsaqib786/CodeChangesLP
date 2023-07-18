' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Public MustInherit Class PickProController
    Inherits Controller
    Public isRedirecting As Boolean = False

    Protected Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
        MyBase.OnActionExecuting(filterContext)
        Try
            'Gets Controller(Page) request is made for
            Dim ControllerName As String = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName

            'Checks if Requested Page is shared or App Specific, and returns the App URL context if shared
            Dim appSharedPage As String = If(filterContext.ActionParameters.ContainsKey("App"), filterContext.ActionParameters("App"), "")


            'Marks if a page is opened in a seperate window
            ViewData("popup") = If(Request.QueryString.AllKeys.Contains("popup"), Request.QueryString.Get("popup"), False)
            ViewData("override_back") = IIf(Request.QueryString.AllKeys.Contains("override_back"), Request.QueryString.Get("override_back"), "")

            'Initializes session data, and checks if missing requirments for the workstation to connect to Webserver
            Dim result As List(Of String) = GlobalFunctions.initializeSession(user:=User.Identity.Name, _
                                                          sessionRef:=Session, tempRef:=TempData, WSID:=Session("WSID"), clientCert:=Request.ClientCertificate, request:=Request)


            'Gets the area name(App Name) to check if WebApp/Workstation has permission to access this page
            Dim appName As String = If(IsNothing(filterContext.RouteData.DataTokens("area")), Config.getFullAppName(appSharedPage), filterContext.RouteData.DataTokens("area"))

            'Returns status on if WS can connect to App(Either Success,App is Full,Invalid App License, Invalid WS License)
            Dim canAccessApp As AppAccess = checkAppPermission(appName, Session("WSID"), User.Identity.Name)

            'Sets PageUUID for determining multiple copies of forms open. Menu pages can have multiple copies, so ignore them if they come from Menu Controller
            If ControllerName.ToLower <> "menu" Then
                ViewData("PageUUID") = (ControllerName & "|" & filterContext.ActionDescriptor.ActionName & "|" & appName).ToLower
            Else
                ViewData("PageUUID") = ""
            End If

            'If workstation does not have an entry in WorkstationCurrentPages, add it
            If Not Config.WorkstationCurrentPages.ContainsKey(Session("WSID")) Then
                Config.WorkstationCurrentPages.TryAdd(Session("WSID"), New Concurrent.ConcurrentDictionary(Of String, String))
            End If


            'Redirects to setup pages if all requirements are not met for current workstation
            If Not IsNothing(result) AndAlso Not (result(1) = "GlobalConfig" And ControllerName.ToLower = "globalconfig") Then
                isRedirecting = True
                filterContext.Result = RedirectToAction(result(0), result(1), New With {.Area = ""})
            End If
            'Verifies you do not have multiple copies of the same form open at the same time
            If Not isRedirecting And Config.WorkstationCurrentPages(Session("WSID")).ContainsKey(ViewData("PageUUID")) Then
                isRedirecting = True
                TempData("Error") = "You cannot open multiple copies of the same form on this PC"
                filterContext.Result = RedirectToAction("Index", "Menu", New With {.Area = ""})
            End If


            If Not isRedirecting Then
                'If App was full or did not have a valid License, redirect to App Menu
                Select Case canAccessApp
                    Case AppAccess.Full
                        isRedirecting = True
                        TempData("Error") = Config.AppLicenses(appName).Info.DisplayName & " is currently at its License cap."
                        filterContext.Result = RedirectToAction("Index", "Menu", New With {.Area = ""})
                    Case AppAccess.InvalidLicense
                        isRedirecting = True
                        TempData("Error") = "You do not have a valid license to access " & Config.AppLicenses(appName).Info.DisplayName & "."
                        filterContext.Result = RedirectToAction("Index", "Menu", New With {.Area = ""})
                    Case AppAccess.WSDenied
                        isRedirecting = True
                        TempData("Error") = "This workstation does not have permission to access " & Config.AppLicenses(appName).Info.DisplayName & "."
                        filterContext.Result = RedirectToAction("Index", "Menu", New With {.Area = ""})
                End Select
            End If

            'If Acccessing a Shared Page, check if App has Shared page permission
            If appSharedPage <> "" And Not isRedirecting Then
                Dim canAccessPage = checkSharedPagePermission(appSharedPage, ControllerName, Session("Permissions"))
                'If Trying to Access a Shared page from an APp that cannot access it, redirect back to that App's home page
                If Not canAccessPage Then
                    isRedirecting = True
                    filterContext.Result = RedirectToAction("Index", "Menu", New With {.Area = Config.getFullAppName(appSharedPage)})
                End If
                'If appSharedPage is null, something else went wrong so redirect to main menu
            ElseIf appSharedPage Is Nothing Then
                isRedirecting = True
                filterContext.Result = RedirectToAction("Index", "Menu", New With {.Area = ""})
            End If
        Catch ex As Exception

        End Try
     
    End Sub

    ''' <summary>
    ''' Checks to see if app your are accessing from has Permission to access a shared page, will also check if you have a licsense for this application
    ''' </summary>
    ''' <param name="App">The app that is being connected to</param>
    ''' <param name="Controller">The controller that is handling the page that is trying ot be loaded</param>
    ''' <param name="Permissions">A list that contains the permissions</param>
    ''' <returns>A boolean that determines if the app is accessible for the user</returns>
    ''' <remarks></remarks>
    Public Function checkSharedPagePermission(App As String, Controller As String, Permissions As List(Of String)) As Boolean
        If GlobalFunctions.AppSharedPages.ContainsKey(App) Then
            Select Case App
                Case "Admin"
                        Return GlobalFunctions.AppSharedPages.Item(App).Contains(Controller)
                Case Else
                    Return GlobalFunctions.AppSharedPages.Item(App).Contains(Controller)
            End Select
        End If
        Return False
    End Function

    ''' <summary>
    ''' Returns if Workstation can currently connect to an App depending on Licensing and Workstation Settings
    ''' </summary>
    ''' <param name="App">The app that is being checked</param>
    ''' <param name="WSID">The workstation that is trying to connect to the app</param>
    ''' <param name="Username">The suer that is currently logged in</param>
    ''' <returns>An AppAccess that tells if the workstation can connect to the desired app</returns>
    ''' <remarks></remarks>
    Public Function checkAppPermission(App As String, WSID As String, Username As String) As AppAccess
        'App will be empty string if navigating through App Menu or Config Sections
        If App <> "" Then
            'If App has a Valid License entered, continue
            If Config.AppLicenses(App).isLicenseValid Then
                'If workstation does not have permission for this app, deny permission
                If Not Config.getWSAppPermission(WSID, App) Then
                    Return AppAccess.WSDenied
                End If

                Dim userExists As ConnectedUser = Nothing
                Config.AppUsers(App).TryGetValue(WSID, userExists)

                Dim currentNumUsers = Config.AppUsers(App).Keys.Count
                Dim AppLicenseInfo = Config.AppLicenses(App)
                If currentNumUsers >= AppLicenseInfo.numLicenses AndAlso IsNothing(userExists) Then
                    Return AppAccess.Full
                ElseIf currentNumUsers < AppLicenseInfo.numLicenses AndAlso IsNothing(userExists) Then
                    'Updates in-memory list of connected users
                    Config.AppUsers(App).TryAdd(WSID, New ConnectedUser(Username, Certificates.getPCName(Session("WSID")), Now()))
                End If
            ElseIf Not Config.AppLicenses(App).isLicenseValid Then
                Return AppAccess.InvalidLicense
            End If
        End If
        Return AppAccess.CanAccess
    End Function



    Public Enum AppAccess
        Full
        CanAccess
        InvalidLicense
        WSDenied
    End Enum
End Class
