' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Public Class InductionRegistration
    Inherits AreaRegistration

    Public Overrides ReadOnly Property AreaName() As String
        Get
            Return "Induction"
        End Get
    End Property

    Public Overrides Sub RegisterArea(ByVal context As System.Web.Mvc.AreaRegistrationContext)
        context.MapRoute( _
            "Induction_default", _
           "IM/{controller}/{action}", _
            New With {.action = "Index", .controller = "Menu"}, _
            New String() {"PickPro_Web.Induction.Controllers"}
        )
    End Sub
End Class

