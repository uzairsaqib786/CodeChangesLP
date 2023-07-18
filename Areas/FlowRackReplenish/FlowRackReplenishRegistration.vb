' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022


Public Class FlowRackReplenishRegistration
    Inherits Mvc.AreaRegistration

    Public Overrides ReadOnly Property AreaName() As String
        Get
            Return "FlowRackReplenish"
        End Get
    End Property

    Public Overrides Sub RegisterArea(ByVal context As System.Web.Mvc.AreaRegistrationContext)
        context.MapRoute(
            "FlowRackReplenish_default",
           "FRR/{controller}/{action}",
            New With {.action = "Index", .controller = "Menu"},
            New String() {"PickPro_Web.FlowRackReplenish.Controllers"}
        )
    End Sub

End Class