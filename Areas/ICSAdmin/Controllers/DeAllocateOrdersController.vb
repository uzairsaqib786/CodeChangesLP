' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Admin.Controllers
    <Authorize()>
    Public Class DeAllocateOrdersController
        Inherits ICSAdminController

        ' GET: DeAllocateOrders
        ''' <summary>
        ''' Returns the DeAllocate Orders view
        ''' </summary>
        ''' <returns>A view object that has the data for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Return View(DeAllocateOrders.getAllAllocatedOrders("", "", "All", User.Identity.Name, Session("WSID")))
        End Function

        ''' <summary>
        ''' Gets the Order Items datatable information and data
        ''' </summary>
        ''' <param name="data">Filters for selecting the table data</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        ''' <remarks></remarks>
        Function getOrderItemsTable(data As TableObjectSent) As ActionResult
            Return Json(DeAllocateOrders.getTableData(data.draw, data.start + 1, data.length + data.start, Request.QueryString.Get("order[0][column]"), _
                                                      Request.QueryString.Get("order[0][dir]"), data.transType, User.Identity.Name, data.nameStamp, data.ordernum, data.filter, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the deallocate orders page typeahead for orders
        ''' </summary>
        ''' <param name="query">User input order number to get suggestions for.</param>
        ''' <returns>A list of string that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function getAllocatedOrders(query As String) As ActionResult
            Return Json(DeAllocateOrders.getAllocatedOrders(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the deallocate orders page typeahead for items
        ''' </summary>
        ''' <param name="query">Item Number from user to filter on for suggestions</param>
        ''' <returns>A list of string that contains the suggestions fo rthe typeahead</returns>
        ''' <remarks></remarks>
        Function getAllocatedItems(query As String) As ActionResult
            Return Json(DeAllocateOrders.getAllocatedItems(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace