' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Induction.Controllers
    <Authorize()>
    Public Class IMHelpController
        Inherits PickProController

        ' GET: IMHelp
        ''' <summary>
        ''' Gets the help view
        ''' </summary>
        ''' <param name="initialPage">The page that came before the help page</param>
        ''' <param name="viewToShow">The help section to view</param>
        ''' <returns>View object that contains the information for the html page</returns>
        Function Index(Optional initialPage As String = "Help", Optional viewToShow As Integer = 0) As ActionResult
            '20170214 - JMP - Added missing sections
            Dim pages As New Dictionary(Of String, String) From {{"Preferences", "preferences"}, {"Tote Transactions Manager", "totetransactionmanager"}, {"Process Put Away Batches", "processputaways"},
                                                                 {"Pick Tote Setup", "picktotesetup"}, {"Delete / DeAllocate Modal", "deletedeallocatemodal"}, {"Inventory", "inventory"}, {"Inventory Map", "inventorymap"},
                                                                 {"Manual Transactions", "manualtransactions"}, {"Super Batch", "superbatch"}, {"Tote Manager", "totemanager"}, {"Transaction Journal", "transactionjournal"},
                                                                 {"Reports", "reports"}}
            Return View(model:=New List(Of Object) From {initialPage, viewToShow, New AliasModel(User.Identity.Name, Session("WSID")), pages})
        End Function

        ''' <summary>
        ''' Returns the preferences help partial
        ''' </summary>
        ''' <returns>Partial view for the preferences help page</returns>
        Function Preferences() As ActionResult
            Return PartialView("~/Areas/Induction/Views/IMHelp/Preferences.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the tote transaction manager help partial
        ''' </summary>
        ''' <returns>Partial view for the tote transaction manager help page</returns>
        Function ToteTransactionManager() As ActionResult
            Return PartialView("~/Areas/Induction/Views/IMHelp/ToteTransMgr.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the process put aways help partial
        ''' </summary>
        ''' <returns>Partial view for the tote transaction manager help page</returns>
        Function ProcessPutAways() As ActionResult
            Return PartialView("~/Areas/Induction/Views/IMHelp/ProcessPutAways/ProcessPutAways.vbhtml", New AliasModel(User.Identity.Name, Session("WSID")))
        End Function

        ''' <summary>
        ''' Returns the pick tote setup help partial
        ''' </summary>
        ''' <returns>Partial view for the pick tote setup help page</returns>
        Function PickToteSetup() As ActionResult
            Return PartialView("~/Areas/Induction/Views/IMHelp/PickToteSetup.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the detailed help page partial
        ''' </summary>
        ''' <returns>Partial view for the help page navigation help page</returns>
        ''' <remarks></remarks>
        Function Help() As ActionResult
            Return PartialView("~/Areas/Induction/Views/IMHelp/Help.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the delete deallocate modal help partial
        ''' </summary>
        ''' <returns>Partail view for the delete deallocate modal help page</returns>
        Function DeleteDeallocateModal() As ActionResult
            Return PartialView("~/Areas/Induction/Views/IMHelp/DeleteDeAllocateModal.vbhtml")
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
        ''' Returns the manual transactions modal help partial
        ''' </summary>
        ''' <returns>Partail view for the manual transactions modal help page</returns>
        Function ManualTransactions() As ActionResult
            Return PartialView("~/Views/Help/ManualTransPartial.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the superbatch modal help partial
        ''' </summary>
        ''' <returns>Partail view for the superbatch modal help page</returns>
        Function SuperBatch() As ActionResult
            Return PartialView("~/Areas/Induction/Views/IMHelp/SuperBatch.vbhtml")
        End Function

        ''' <summary>
        ''' Returns the tote manager modal help partial
        ''' </summary>
        ''' <returns>Partail view for the tote manager modal help page</returns>
        Function ToteManager() As ActionResult
            Return PartialView("~/Areas/Induction/Views/IMHelp/ToteManager.vbhtml")
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