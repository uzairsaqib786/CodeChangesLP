' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Public Class AdminRegistration
    Inherits AreaRegistration

    Public Overrides ReadOnly Property AreaName() As String
        Get
            Return "ICSAdmin"
        End Get
    End Property

    Public Overrides Sub RegisterArea(ByVal context As System.Web.Mvc.AreaRegistrationContext)
        context.MapRoute( _
            "ICSAdmin_default", _
           "Admin/{controller}/{action}/{id}", _
            New With {.action = "Index", .controller = "Menu", .id = UrlParameter.Optional}, _
            New String() {"PickPro_Web.Admin.Controllers"}
        )
    End Sub
End Class

