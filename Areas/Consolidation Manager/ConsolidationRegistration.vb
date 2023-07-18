' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Public Class ConsolidationRegistration
    Inherits AreaRegistration

    Public Overrides ReadOnly Property AreaName() As String
        Get
            Return "Consolidation Manager"
        End Get
    End Property

    Public Overrides Sub RegisterArea(ByVal context As System.Web.Mvc.AreaRegistrationContext)
        context.MapRoute( _
            "Consolidation_default", _
           "CM/{controller}/{action}", _
            New With {.action = "Index", .controller = "Menu"}, _
            New String() {"PickPro_Web.Consolidation.Controllers"}
        )
    End Sub
End Class

