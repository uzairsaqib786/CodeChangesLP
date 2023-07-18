' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

<Authorize()>
Public Class HelpController
    Inherits PickProController

    ' GET: /Help
    ''' <summary>
    ''' Returns the Help view
    ''' </summary>
    ''' <param name="initialPage">The page that came before the help page</param>
    ''' <param name="viewToShow">The help section to show</param>
    ''' <returns>View object that contains the information for the html page</returns>
    ''' <remarks></remarks>
    Function Index(Optional initialPage As String = "help", Optional viewToShow As Integer = 0) As ActionResult
        Dim pages As New Dictionary(Of String, String) From {{"Transactions", "transactions"}, {"Event Log", "eventlog"}, {"Inventory Map", "inventorymap"}, {"Inventory", "inventorymaster"},
                                                                         {"Logon", "logon"}, {"Employees", "employees"}, {"Manual Transactions", "manualtransactions"}, {"Modals (Pop-ups)", "modal"},
                                                                         {"Tables", "datatable"}, {"Multi-Line Paging", "stpaging"}, {"Typeaheads", "typeahead"}, {"Hold Transactions", "holdtransactions"},
                                                                         {"De-Allocate Orders", "deallocateorders"}, {"Preferences", "preferences"}, {"System Replenishments", "systemreplenishment"},
                                                                         {"Batch Manager", "batchmanager"}, {"Reports", "reports"}, {"Date Pickers", "datepicker"},
                                                                         {"Global Config", "globalconfig"}, {"Location Assignment", "locationassignment"}, {"Column Sequence", "columnsequence"},
                                                                         {"Cycle Count", "cyclecount"}, {"Move Items", "moveitems"}}
        Return View(model:=New List(Of Object) From {initialPage, viewToShow, New AliasModel(User.Identity.Name, Session("WSID")), pages})
    End Function

    'All functions below are to make the help page run as fast as a girl running towards a pumpkin spice latte from Starbucks

    ''' <summary>
    ''' Returns the datatable help partial
    ''' </summary>
    ''' <returns>Patial view for the datatable help page</returns>
    ''' <remarks></remarks>
    Function datatable() As ActionResult
        Return PartialView("~/Views/Help/Plugins/DataTablePartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the cycle count help partial
    ''' </summary>
    ''' <returns>Partial view for the cycle count help page</returns>
    Function CycleCount() As ActionResult
        Return PartialView("~/Views/Help/CycleCount/Wrapper.vbhtml")
    End Function

    ''' <summary>
    ''' Retruns the date picker help partial
    ''' </summary>
    ''' <returns>Partial view for the data picker help page</returns>
    Function DatePicker() As ActionResult
        Return PartialView("~/Views/Help/Plugins/DatePickers.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the modal help partial
    ''' </summary>
    ''' <returns>Partial view for the modal help page</returns>
    ''' <remarks></remarks>
    Function modal() As ActionResult
        Return PartialView("~/Views/Help/Plugins/ModalPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the typeahead help partial
    ''' </summary>
    ''' <returns>Partial view for the typeahead help page</returns>
    ''' <remarks></remarks>
    Function typeahead() As ActionResult
        Return PartialView("~/Views/Help/Plugins/TypeaheadPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the preferences help partial
    ''' </summary>
    ''' <returns>Partial view for the preferences help page</returns>
    ''' <remarks></remarks>
    Function preferences() As ActionResult
        Return PartialView("~/Views/Help/Preferences/PreferencesPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the system replenishments help partial
    ''' </summary>
    ''' <returns>Partial view for the system replenishment help page</returns>
    ''' <remarks></remarks>
    Function systemreplenishment() As ActionResult
        Return PartialView("~/Views/Help/SystemReplenishment/SystemReplenishmentPartial.vbhtml", New AliasModel(User.Identity.Name, Session("WSID")))
    End Function

    ''' <summary>
    ''' Returns the transaction help partial, passing which individual partial should be shown first (0 = order status, 1 = open trans, 2 = trans hist, 3 = reprocess)
    ''' </summary>
    ''' <param name="viewToShow">Determiens which transaction page to view</param>
    ''' <returns>Partial vview for the transactions help page</returns>
    ''' <remarks></remarks>
    Function transactions(Optional viewToShow As Integer = 0) As ActionResult
        Return PartialView("~/Views/Help/Transactions/TransactionsPartial.vbhtml", viewToShow)
    End Function

    ''' <summary>
    ''' Returns the deallocate orders help partial
    ''' </summary>
    ''' <returns>Partial view for the deallocate orders help page</returns>
    ''' <remarks></remarks>
    Function deallocateorders() As ActionResult
        Return PartialView("~/Views/Help/DeAllocatePartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the employees help partial
    ''' </summary>
    ''' <returns>Partial view for the employees help page</returns>
    ''' <remarks></remarks>
    Function employees() As ActionResult
        Return PartialView("~/Views/Help/EmployeesPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the event log help partial
    ''' </summary>
    ''' <returns>Partial view for the event log help page</returns>
    ''' <remarks></remarks>
    Function eventlog() As ActionResult
        Return PartialView("~/Views/Help/EventLogPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the detailed help page partial
    ''' </summary>
    ''' <returns>Partial view for the help page navigation help page</returns>
    ''' <remarks></remarks>
    Function help() As ActionResult
        Return PartialView("~/Views/Help/HelpPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the hold transactions help page partial
    ''' </summary>
    ''' <returns>Partial view for the hold transactions help page</returns>
    ''' <remarks></remarks>
    Function holdtransactions() As ActionResult
        Return PartialView("~/Views/Help/HoldTransPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the inventory map help partial
    ''' </summary>
    ''' <returns>Partial view for the inventory map help page</returns>
    ''' <remarks></remarks>
    Function inventorymap() As ActionResult
        Return PartialView("~/Views/Help/InventoryMapPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the inventory/inventory master help partial
    ''' </summary>
    ''' <returns>Partial view for the inventory master help page</returns>
    ''' <remarks></remarks>
    Function inventorymaster() As ActionResult
        Return PartialView("~/Views/Help/InventoryMasterPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the logon help partial
    ''' </summary>
    ''' <returns>Partial view for the logon help page</returns>
    ''' <remarks></remarks>
    Function logon() As ActionResult
        Return PartialView("~/Views/Help/LogonPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the manual transactions help partial
    ''' </summary>
    ''' <returns>Partial view for the manual transaction help page</returns>
    ''' <remarks></remarks>
    Function manualtransactions() As ActionResult
        Return PartialView("~/Views/Help/ManualTransPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the batch manager help partial
    ''' </summary>
    ''' <returns>Partial view for the batch manager help page</returns>
    ''' <remarks></remarks>
    Function batchmanager() As ActionResult
        Return PartialView("~/Views/Help/BatchManager/BatchManager.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the reports help partial
    ''' </summary>
    ''' <returns>Partial view for the custom reporst help page</returns>
    ''' <remarks></remarks>
    Function reports() As ActionResult
        Return PartialView("~/Views/Help/CustomReports/CustomReports.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the global config help partial
    ''' </summary>
    ''' <returns>Partial view for the golabl config help page</returns>
    Function GlobalConfig() As ActionResult
        Return PartialView("~/Views/Help/GlobalConfig/GCOverview.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the location assignment help partial
    ''' </summary>
    ''' <returns>Partial view for the location assignment help page</returns>
    Function LocationAssignment() As ActionResult
        Return PartialView("~/Views/Help/LocationAssignmentPartial.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the paging help partial
    ''' </summary>
    ''' <returns>Partial view for the paging help page</returns>
    Function STPaging() As ActionResult
        Return PartialView("~/Views/Help/Plugins/STPaging.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the column sequence help partial
    ''' </summary>
    ''' <returns>Partial view for the column sequecne help page</returns>
    Function ColumnSequence() As ActionResult
        Return PartialView("~/Views/Help/ColumnSequence.vbhtml")
    End Function

    ''' <summary>
    ''' Returns the move items help partial
    ''' </summary>
    ''' <returns>Partial view for the column sequecne help page</returns>
    Function MoveItems() As ActionResult
        Return PartialView("~/Views/Help/MoveItemsPartial.vbhtml")
    End Function
End Class