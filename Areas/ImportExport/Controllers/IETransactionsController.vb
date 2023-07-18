' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace ImportExport.Controllers
    <Authorize()>
    Public Class IETransactionsController
        Inherits PickProController

        ' GET: IETransactions
        Function Index(Optional table As String = "Archive Transaction History") As ActionResult
            Return View(model:=New Dictionary(Of String, Object) From {{"Min Date", ImportExport.IETransactionsLogic.GetIETransMinDate(table, User.Identity.Name, Session("WSID"))}, _
                                                                       {"Init Table", table}})
        End Function

        Function GetIETransactionsTable(ByVal data As TableObjectSent) As ActionResult
            Return Json(ImportExport.IETransactionsLogic.GetIETransactions(data.draw, data.DictionaryData("Table"), data.DictionaryData("Start Date"), data.DictionaryData("End Date"),
                                                                      data.DictionaryData("Plugin Where"), Request.QueryString.Get("order[0][column]"),
                                                                      Request.QueryString.Get("order[0][dir]"), data.start, data.start + data.length,
                                                                      User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace