' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Public Class MarkoutRegistration
    Inherits AreaRegistration

    Public Overrides ReadOnly Property AreaName() As String
        Get
            Return "Markout"
        End Get
    End Property

    Public Overrides Sub RegisterArea(ByVal context As System.Web.Mvc.AreaRegistrationContext)
        context.MapRoute( _
            "Markout_default", _
            "MO/{controller}/{action}", _
            New With {.action = "Index", .controller = "Menu"}, _
            New String() {"PickPro_Web.Markout.Controllers"}
            )
    End Sub
End Class

