' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization
Imports combit.ListLabel21.Web
Imports combit.ListLabel21.Web.WebDesigner.Server
Imports combit.ListLabel21
Imports combit.ListLabel21.DataProviders

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    'Global Application Timers. Any processes you want started on App start can be started here on seperate threads on or timers
    Public Shared emergencyTimer As System.Timers.Timer
    Public Shared connectedTimer As System.Timers.Timer
    Public Shared llDesignerTimer As System.Timers.Timer

    Sub Application_Start()
        Try
            'LL Designer and VIewer Handlers
            AddHandler Html5ViewerConfig.OnListLabelRequest, AddressOf ListLabelHelperFunctions.Services_OnListLabelRequest
            AddHandler WebDesignerConfig.OnRequestDataProvider, AddressOf ListLabelHelperFunctions.Services_OnRequestDataProvider
            ' register the independent exe designer's location so that during a request if there is no handler for the designer link we can offer to download it
            WebDesignerConfig.WebDesignerSetupFile = Server.MapPath("~/WebDesigner/LL21WebDesignerSetup.exe")
            WebDesignerConfig.LicensingInfo = ListLabelHelperFunctions.LLLicenseString

            'These registrations are required by MVC framework
            AreaRegistration.RegisterAllAreas()
            WebApiConfig.Register(GlobalConfiguration.Configuration)
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
            RouteConfig.RegisterRoutes(RouteTable.Routes)
            BundleConfig.RegisterBundles(BundleTable.Bundles)

            'Section registers any timed events required on the server
            emergencyTimer = New System.Timers.Timer(60000)
            connectedTimer = New System.Timers.Timer(30000)
            llDesignerTimer = New System.Timers.Timer(1000 * 60 * 60) ' 60 minutes/1 hour
            ' AddHandler emergencyTimer.Elapsed, AddressOf GlobalFunctions.checkEmergencyTransactions
            AddHandler connectedTimer.Elapsed, AddressOf Config.checkIdleUsers
            AddHandler llDesignerTimer.Elapsed, AddressOf ListLabelHelperFunctions.DeleteOldExports

            'emergencyTimer.Start()
            connectedTimer.Start()
            llDesignerTimer.Start()


            ' Gets Shared Pages each app has access to
            GlobalFunctions.InitializeSharedPages()

            'Gets Licensing Information for all Apps
            Config.AppLicenses = AESCrypter.GetAppPermissions()

        Catch ex As Exception
            insertErrorMessages("Appstart", "AppStartErr", ex.ToString(), "No User", "Error In Global.asax Application_Start")
        End Try


    End Sub


    'Overriden Sub that is called at the very beginning of a request
    'Redirects user if production, otherwise uses standard http for testing(https not support by Visual Studio)
    Protected Sub Application_BeginRequest(sender As Object, e As EventArgs)
        If Not HttpContext.Current.Request.IsSecureConnection And Not HttpContext.Current.Request.IsLocal And Not GlobalFunctions.Testing And Not HttpContext.Current.Request.Url.ToString.Contains("llupl") Then
            Response.Redirect("https://" & Request.ServerVariables("HTTP_HOST") & HttpContext.Current.Request.RawUrl)
        End If
    End Sub
End Class
