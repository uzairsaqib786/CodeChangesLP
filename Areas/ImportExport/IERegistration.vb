' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Public Class IERegistration
    Inherits AreaRegistration

    Public Overrides ReadOnly Property AreaName() As String
        Get
            Return "ImportExport"
        End Get
    End Property

    Public Overrides Sub RegisterArea(ByVal context As System.Web.Mvc.AreaRegistrationContext)
        context.MapRoute( _
            "ImportExport_default", _
           "IE/{controller}/{action}/{id}", _
            New With {.action = "Index", .controller = "Menu", .id = UrlParameter.Optional}, _
            New String() {"PickPro_Web.ImportExport.Controllers"}
        )
    End Sub
End Class

