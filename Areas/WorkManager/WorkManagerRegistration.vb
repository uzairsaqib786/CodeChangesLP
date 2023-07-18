' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Public Class WorkManagerRegistration
    Inherits AreaRegistration

    Public Overrides ReadOnly Property AreaName() As String
        Get
            Return "WorkManager"
        End Get
    End Property

    Public Overrides Sub RegisterArea(ByVal context As System.Web.Mvc.AreaRegistrationContext)
        context.MapRoute( _
            "WorkManager_default", _
           "WM/{controller}/{action}", _
            New With {.action = "Index", .controller = "Menu"}, _
            New String() {"PickPro_Web.WorkManager.Controllers"}
        )
    End Sub
End Class

