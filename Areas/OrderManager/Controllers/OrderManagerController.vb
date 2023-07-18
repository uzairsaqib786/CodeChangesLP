' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace OrderManager.Controllers
    <Authorize()>
    Public Class OrderManagerController
        Inherits PickProController

        ' GET: /OrderManager
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Function Index() As ActionResult
            OM.delOrderManTemp(Session("WSID"), User.Identity.Name)
            Return View(New With {.MaxOrder = OrderManager.OM.selectMaxOrders(Session("WSID"), User.Identity.Name), _
                                  .ColumnSequence = GlobalFunctions.getDefaultColumnSequence("Order Manager", User.Identity.Name, Session("WSID")), _
                                  .Preferences = Preferences.selectOMPreferences(Session("WSID"), User.Identity.Name)})
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Function CreateOrders() As ActionResult
            Return View(New With {.ColumnSequence = GlobalFunctions.getDefaultColumnSequence("Order Manager Create", User.Identity.Name, Session("WSID")),
                                  .Warehouses = OrderManager.CreateOrders.selectWarehouses(Session("WSID"), User.Identity.Name),
                                  .ProcessingBy = User.Identity.Name, .Preferences = Preferences.selectOMPreferences(Session("WSID"), User.Identity.Name)})
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="query"></param>
        ''' <returns></returns>
        Function CreateOrderTypeahead(query As String) As ActionResult
            Return Json(OrderManager.CreateOrders.selCreateOrdersTA(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="data"></param>
        ''' <returns></returns>
        Function orderManDT(ByVal data As TableObjectSent) As ActionResult
            Return Json(OM.selectOrderManagerTempDT(data.draw, data.start + 1, data.length + data.start,
                                                    Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"),
                                                    data.searchColumn, data.searchString, Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function


        ''' <summary>
        ''' Prints the release orders report from the order manager screen based on the ids in the table, the view, the desired table, and whcih page you are on
        ''' </summary>
        ''' <param name="View">either headers or lines</param>
        ''' <param name="Table">either pending or open</param>
        ''' <param name="Page">either order manager or create orders</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function PrintReleaseOrders(tabIDs As List(Of String), View As String, Table As String, Page As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name, filename As String = "ReleaseOrder.lst"
            Dim reportName As String = "Print Viewed " & Page, sp As String = "selOTPendOTLL"
            Dim params As String(,) = {{"@View", View, strVar}, {"@Table", Table, strVar}, {"@Page", Page, strVar}, {"@tabIDs", String.Join(",", tabIDs.ToArray), strVar}, {"@WSID", WSID, strVar}}
            Try
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, reportName, "List", filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("CustomReportsHub", "PrintReleaseOrders", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace
