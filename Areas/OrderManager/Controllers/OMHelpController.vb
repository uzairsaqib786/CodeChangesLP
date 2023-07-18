' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Namespace OrderManager.Controllers
    <Authorize()>
    Public Class OMHelpController
        Inherits PickProController

        ' GET: /OMHelp
        ''' <summary>
        ''' Returns the help view
        ''' </summary>
        ''' <param name="initialPage">The page that came before the help page</param>
        ''' <param name="viewToShow">The help section to show</param>
        ''' <returns>View object tgat contains the information for the html page</returns>
        Function Index(Optional initialPage As String = "Help", Optional viewToShow As Integer = 0) As ActionResult
            Dim pages As New Dictionary(Of String, String) From {{"Preferences", "preferences"}, {"Order Manager", "ordermanager"}, {"Transaction Journal", "transactionjournal"},
                                                                 {"Inventory", "inventory"}, {"Inventory Map", "inventorymap"}, {"Event Log", "eventlog"}, {"Reports", "reports"}}
            Return View(model:=New List(Of Object) From {initialPage, viewToShow, New AliasModel(User.Identity.Name, Session("WSID")), pages})
        End Function

        ''' <summary>
        ''' Returns the detailed help page partial
        ''' </summary>
        ''' <returns>Partial view for the help page navigation help page</returns>
        ''' <remarks></remarks>
        Function Help() As ActionResult
            Return PartialView("~/Areas/OrderManager/Views/OMHelp/HelpPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the preferences help partial
        ''' </summary>
        ''' <returns>Partial view for the preferences help page</returns>
        Function Preferences() As ActionResult
            Return PartialView("~/Areas/OrderManager/Views/OMHelp/PreferencesPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the order manager help partial
        ''' </summary>
        ''' <returns>Partial view for the order manager help page</returns>
        Function OrderManager() As ActionResult
            Return PartialView("~/Areas/OrderManager/Views/OMHelp/OrderManagerPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the ivnentory modal help partial
        ''' </summary>
        ''' <returns>Partail view for the inventory modal help page</returns>
        Function Inventory() As ActionResult
            Return PartialView("~/Views/Help/InventoryMasterPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the inventory map modal help partial
        ''' </summary>
        ''' <returns>Partail view for the inventory map modal help page</returns>
        Function InventoryMap() As ActionResult
            Return PartialView("~/Views/Help/InventoryMapPartial.vbhtml")
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

        ''' <summary>
        ''' Returns the event log help partial
        ''' </summary>
        ''' <returns>Partial view for the event log help page</returns>
        ''' <remarks></remarks>
        Function eventlog() As ActionResult
            Return PartialView("~/Views/Help/EventLogPartial.vbhtml")
        End Function
    End Class
End Namespace
