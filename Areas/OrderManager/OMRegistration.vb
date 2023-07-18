' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Public Class OMRegistration
    Inherits AreaRegistration

    Public Overrides ReadOnly Property AreaName() As String
        Get
            Return "OrderManager"
        End Get
    End Property

    Public Overrides Sub RegisterArea(ByVal context As System.Web.Mvc.AreaRegistrationContext)
        context.MapRoute( _
            "OrderManager_default", _
           "OM/{controller}/{action}/{id}", _
            New With {.action = "Index", .controller = "Menu", .id = UrlParameter.Optional}, _
            New String() {"PickPro_Web.OrderManager.Controllers"}
        )
    End Sub
End Class

