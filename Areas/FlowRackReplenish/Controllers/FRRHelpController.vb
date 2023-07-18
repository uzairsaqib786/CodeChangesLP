' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace FlowRackReplenish.Controllers
    Public Class FRRHelpController
        Inherits PickProController

        ' GET: FRRHelp
        ''' <summary>
        ''' Gets the help page
        ''' </summary>
        ''' <param name="initialPage">The page that came before</param>
        ''' <param name="viewToShow">The help section to show</param>
        ''' <returns></returns>
        Function Index(Optional initialPage As String = "Help", Optional viewToShow As Integer = 0) As ActionResult
            Dim pages As New Dictionary(Of String, String) From {{"General", "general"}}
            Return View(model:=New List(Of Object) From {initialPage, viewToShow, New AliasModel(User.Identity.Name, Session("WSID")), pages})
        End Function

        ''' <summary>
        ''' Returns the detailed help page partial
        ''' </summary>
        ''' <returns>Partial view for the help page navigation help page</returns>
        ''' <remarks></remarks>
        Function Help() As ActionResult
            Return PartialView("~/Areas/FlowRackReplenish/Views/FRRHelp/HelpPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the general help partial
        ''' </summary>
        ''' <returns>Partial view for the general halp page</returns>
        Function General() As ActionResult
            Return PartialView("~/Areas/FlowRackReplenish/Views/FRRHelp/GeneralPartial.vbhtml")
        End Function

    End Class
End Namespace