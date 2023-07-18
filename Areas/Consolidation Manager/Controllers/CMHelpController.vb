' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Namespace Consolidation.Controllers
    <Authorize()>
    Public Class CMHelpController
        Inherits PickProController

        ' GET: /CMHelp
        ''' <summary>
        ''' Gets the help page
        ''' </summary>
        ''' <param name="initialPage">The page prior to the help page</param>
        ''' <param name="viewToShow">The help section to initially show</param>
        ''' <returns>A view object that contains the informationf or the html page</returns>
        Function Index(Optional initialPage As String = "Help", Optional viewToShow As Integer = 0) As ActionResult
            Dim pages As New Dictionary(Of String, String) From {{"Preferences", "preferences"}, {"Consolidation", "consolidation"}, {"Staging Locations", "staginglocations"}, {"Reports", "reports"},
                                                                 {"Transaction Journal", "transactionjournal"}}
            Return View(model:=New List(Of Object) From {initialPage, viewToShow, New AliasModel(User.Identity.Name, Session("WSID")), pages})
        End Function

        ''' <summary>
        ''' Returns the consolidation help partial
        ''' </summary>
        ''' <returns>Partial view for the consolidation help page</returns>
        Function Consolidation() As ActionResult
            Return PartialView("~/Areas/Consolidation Manager/Views/CMHelp/ConsolidationPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the preferences help partial
        ''' </summary>
        ''' <returns>Partial view for the preferences help page</returns>
        Function Preferences() As ActionResult
            Return PartialView("~/Areas/Consolidation Manager/Views/CMHelp/PreferencesPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the detailed help page partial
        ''' </summary>
        ''' <returns>Partial view for the help page navigation help page</returns>
        ''' <remarks></remarks>
        Function Help() As ActionResult
            Return PartialView("~/Areas/Consolidation Manager/Views/CMHelp/HelpPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the staging locations help partial
        ''' </summary>
        ''' <returns>Partial view for the staging locations help page</returns>
        Function StagingLocations() As ActionResult
            Return PartialView("~/Areas/Consolidation Manager/Views/CMHelp/StagingLocationsPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the transaction journal modal help partial
        ''' </summary>
        ''' <returns>Partail view for the transaction journal modal help page</returns>
        Function TransactionJournal(Optional viewToShow As Integer = 0) As ActionResult
            Return PartialView("~/Views/Help/Transactions/TransactionsPartial.vbhtml", viewToShow)
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