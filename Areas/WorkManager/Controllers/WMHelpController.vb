' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Namespace WorkManager.Controllers
    <Authorize()>
    Public Class WMHelpController
        Inherits PickProController

        ' GET: /WMHelp
        ''' <summary>
        ''' Returns the Help view
        ''' </summary>
        ''' <param name="initialPage">The page that came before the help page</param>
        ''' <param name="viewToShow">The help section to show</param>
        ''' <returns>View object that contains the information for the html page</returns>
        ''' <remarks></remarks>
        Function Index(Optional initialPage As String = "Help", Optional viewToShow As Integer = 0) As ActionResult
            Dim pages As New Dictionary(Of String, String) From {{"Select Work", "selectwork"}, {"Organize Work", "organizework"}, {"Work Preferences", "workpreferences"}, {"Reports", "reports"}}
            Return View(model:=New List(Of Object) From {initialPage, viewToShow, New AliasModel(User.Identity.Name, Session("WSID")), pages})
        End Function

        ''' <summary>
        ''' Returns the detailed help page partial
        ''' </summary>
        ''' <returns>Partial view for the help page navigation help page</returns>
        ''' <remarks></remarks>
        Function Help() As ActionResult
            Return PartialView("~/Areas/WorkManager/Views/WMHelp/HelpPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the select work help partial
        ''' </summary>
        ''' <returns>Partial view for the select work help page</returns>
        Function SelectWork() As ActionResult
            Return PartialView("~/Areas/WorkManager/Views/WMHelp/SelectWorkPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the organize work help partial
        ''' </summary>
        ''' <returns>Partial view for the organize work help page</returns>
        Function OrganizeWork() As ActionResult
            Return PartialView("~/Areas/WorkManager/Views/WMHelp/OrganizeWorkPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the work preferences help partial
        ''' </summary>
        ''' <returns>Partial view for the work preferences help page</returns>
        Function WorkPreferences() As ActionResult
            Return PartialView("~/Areas/WorkManager/Views/WMHelp/WorkPreferencesPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the reports modal help partial
        ''' </summary>
        ''' <returns>Partail view for the reports modal help page</returns>
        Function Reports() As ActionResult
            Return PartialView("~/Views/Help/CustomReports/CustomReports.vbhtml")
        End Function
    End Class
End Namespace
