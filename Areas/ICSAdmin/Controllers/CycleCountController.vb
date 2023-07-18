' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Admin.Controllers
    <Authorize()>
    Public Class CycleCountController
        Inherits ICSAdminController

        ' GET: CycleCount
        Function Index() As ActionResult
            Return View(New CycleCountModel(DiscTable:=CycleCount.getCycleCountDiscrepantDT(User.Identity.Name, Session("WSID")), _
                                            ModalData:=CycleCount.getFieldMapModalData(User.Identity.Name, Session("WSID"))))
        End Function

        ''' <summary>
        ''' Gets the warehouse and cycle order list for the count batches view
        ''' </summary>
        ''' <returns>A view object that contains the warehouse and cycle order lists for the count batches page</returns>
        Function CountBatches() As ActionResult
            Dim warehouseList = Warehouse.getWarehouses(User.Identity.Name, Session("WSID"))
            Dim CycleOrderList = CycleCount.CycleCountOrderNumbers(User.Identity.Name, Session("WSID"))
            Return View(New CreateCountBatchesModel(warehouseList, CycleOrderList))
        End Function

        ''' <summary>
        ''' Gets the ccQueue datatable
        ''' </summary>
        ''' <param name="data">The data needed to get the records for the datatable</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        Function ccQueue(data As TableObjectSent) As ActionResult
            Return Json(CycleCount.ccQueueTable(data.draw, data.start + 1, data.start + data.length, Request.QueryString.Get("order[0][column]"), _
                                                                Request.QueryString.Get("order[0][dir]"), User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles search box typeahead
        ''' </summary>
        ''' <param name="query">User input from client side.</param>
        ''' <returns>Json object with the typeahead data contained within.</returns>
        ''' <remarks></remarks>
        Function getCycleCountDescriptionTypeahead(query As String)
            Return Json(CycleCount.CycleCountDescriptionTA(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles search box typeahead
        ''' </summary>
        ''' <param name="query">User input from client side.</param>
        ''' <returns>Json object with the typeahead data contained within.</returns>
        ''' <remarks></remarks>
        Function getCycleCountCategoryTypeahead(query As String)
            Return Json(CycleCount.CycleCountCategoryTA(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles search box typeahead
        ''' </summary>
        ''' <param name="query">User input from client side.</param>
        ''' <returns>Json object with the typeahead data contained within.</returns>
        ''' <remarks></remarks>
        Function getCycleCountFromCostTypeahead(query As String)
            Return Json(CycleCount.CycleCountFromCostTA(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles search box typeahead
        ''' </summary>
        ''' <param name="query">User input from client side.</param>
        ''' <returns>Json object with the typeahead data contained within.</returns>
        ''' <remarks></remarks>
        Function getCycleCountToCostTypeahead(query As String, fromCost As String)
            Return Json(CycleCount.CycleCountToCostTA(query, fromCost, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Exports the cycle count report
        ''' </summary>
        ''' <param name="orderNumber">The order number whose cycle count information will be exported</param>
        ''' <returns>A boolean telling if the export was comepleted successfully</returns>
        Public Function ExportCycleCountReport(orderNumber As String) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selOTCountOrderNumLL"
                Dim params As String(,) = {{"@OrderNum", orderNumber, strVar}}
                Dim filename As String = "CycleCountDetail.lst", LLType As String = "List"


                Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("CycleCountController", "ExportCycleCountReport", ex.ToString(), username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        Public Function PrintCycleCountReport(orderNumber As String) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selOTCountOrderNumLL"
                Dim params As String(,) = {{"@OrderNum", orderNumber, strVar}}
                Dim filename As String = "CycleCountDetail.lst", LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Cycle Count Detail", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("CycleCountController", "PrintCycleCountReport", ex.ToString(), username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        Public Function PrintDiscReport() As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selCcDiscrepLL"
                Dim params As String(,) = {{"nothing"}}
                Dim filename As String = "CycleCountDiscrepancy.lst", LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Cycle Count Disc", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("CycleCountController", "PrintDiscReport", ex.ToString(), username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Exports the Cycle Count Disc Report to preview
        ''' </summary>
        ''' <remarks></remarks>
        ''' <returns>A boolean to tell if the export was successful</returns> 
        Public Function ExportDiscReport() As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selCcDiscrepLL"
                Dim params As String(,) = {{"nothing"}}
                Dim filename As String = "CycleCountDiscrepancy.lst", LLType As String = "List"

                Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("CycleCountController", "ExportDiscReport", ex.ToString(), username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace