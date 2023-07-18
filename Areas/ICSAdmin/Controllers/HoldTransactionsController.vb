' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Admin.Controllers
    <Authorize()>
    Public Class HoldTransactionsController
        Inherits ICSAdminController

        ' GET: /HoldTransactions
        ''' <summary>
        ''' Returns the Hold Transactions view
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Return View(New With {.alias = New AliasModel(User.Identity.Name, Session("WSID")), .reels = HoldTransactions.getReelCount(User.Identity.Name, Session("WSID"))})
        End Function

        ''' <summary>
        ''' Gets the data for the Hold Transactions datatable
        ''' </summary>
        ''' <param name="data">Necessary information to filter the data selected for the Hold Transactions Table</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        ''' <remarks></remarks>
        Function HoldTransactionsTable(data As TableObjectSent) As ActionResult
            Return Json(HoldTransactions.selHoldTransactionsData(data.draw, data.start + 1, data.length + data.start, _
                                                                 Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"), _
                                                                 User.Identity.Name, data.entryFilter, data.reel, data.OrderItem, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the hold transactions typeahead
        ''' </summary>
        ''' <param name="type">Order Number or Item Number as a string to identify the contents of orderitem parameter</param>
        ''' <param name="orderitem">Order Number or Item Number value</param>
        ''' <param name="reel">reel, non or both as string to filter on</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function holdTransactionsOrderItem(type As String, orderitem As String, reel As String) As ActionResult
            Return Json(HoldTransactions.getTypeaheadHoldTransactions(type, orderitem, reel, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function


    End Class
End Namespace
