' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Consolidation.Controllers
    <Authorize()>
    Public Class ConsolidationController
        Inherits PickProController

        '
        ' GET: /Consolidation
        ''' <summary>
        ''' Gets the consolidation page
        ''' </summary>
        ''' <param name="Ordernumber">The order number to be displayed on page load</param>
        ''' <returns>View object that contains the information for the html page</returns>
        Function Index(Ordernumber As String) As ActionResult
            Return View(New With {.OrderNumber = Ordernumber, .lookup = Consolidation.CMConsolidation.selDefLookupType(Session("WSID"), User.Identity.Name), .Preferences = Preferences.selectCMPrefs(User.Identity.Name, Session("WSID"))})
        End Function

        ''' <summary>
        ''' Gets the typeahead suggestions
        ''' </summary>
        ''' <param name="query">The value to be searched for</param>
        ''' <param name="col">The column that contains the value</param>
        ''' <param name="orderNum">The order number that is currently displayed</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        Function consolItemTA(query As String, col As String, orderNum As String) As ActionResult
            Return Json(CMConsolidation.selConsoleItemsTA(col, query, orderNum, Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the typeahead suggestions
        ''' </summary>
        ''' <param name="query">The value to be searched</param>
        ''' <param name="orderNum">The order number that is currently displayed</param>
        ''' <returns>A list of objects that contains the suggestions for the typeahead</returns>
        Function shipTranTA(query As String, orderNum As String) As ActionResult
            Return Json(Consolidation.ShippingTransactions.selItemNumShipTransTA(query, orderNum, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Previews or prints the Not Verified report for the given order number
        ''' </summary>
        ''' <param name="OrderNum">The order number to be previewed or printed</param>
        ''' <param name="Print">The the report is ot be printed or previewed</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintPrevNotVerified(OrderNum As String, Print As Integer) As ActionResult
           
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selCMNotVerifiedListLL"
                Dim params As String(,) = {{"@OrderNum", OrderNum, strVar}, {"@WSID", WSID, strVar}}
                Dim filename As String = "CMPackListNV.lst", LLType As String = "List"

                If Print = 1 Then
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Not Verified", LLType, filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If

            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ConsolidationController", "PrintPrevNotVerified", ex.Message, username, WSID)

            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Previews the desired pack list based on the cmpreference. This is based on the order number and what the where variable is set to (all or where)
        ''' </summary>
        ''' <param name="OrderNum">The order number to be printed or previewed</param>
        ''' <param name="Where">Either all or where. Determines if dates are taken into account for the select</param>
        ''' <param name="Print">If the report is being previewed or printed</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        <HttpPost()>
        Public Function PrintPrevCMPackList(OrderNum As String, Where As String, OrderBy As String, Print As Integer) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim PackListPref As String = ""
                Dim DataReader As SqlDataReader = Nothing
                Try
                    DataReader = RunSPArray("SelPackingListPref", WSID, {{"@WSID", WSID, strVar}})
                    If DataReader.HasRows AndAlso DataReader.Read() Then
                        PackListPref = DataReader(0)
                    End If
                Catch ex As Exception
                    Debug.WriteLine(ex.Message)
                    insertErrorMessages("ConsolidationController", "PrintPrevCMPackList: Inner Try", ex.Message, username, WSID)
                Finally
                    If Not IsNothing(DataReader) Then
                        DataReader.Close()
                    End If
                End Try

                Dim sp As String = "", filename As String = ""
                If PackListPref.ToLower() = "packing list" Then
                    sp = "selCMPackListLL"
                    filename = "CMPackList.lst"
                Else
                    sp = "selCMPackList2LL"
                    filename = "CMPackList2.lst"
                End If

                Dim params As String(,) = {{"@OrderNum", OrderNum, strVar}, {"@Where", Where, strVar}, {"@OrderBy", OrderBy, strVar}, {"@WSID", WSID, strVar}}

                If Print = 1 Then
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "CM Pack List", "List", filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If

            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("CustomReportsHub", "PreviewCMPackList", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

    End Class
End Namespace