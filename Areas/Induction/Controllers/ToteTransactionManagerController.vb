' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Namespace Induction.Controllers
    <Authorize()>
    Public Class ToteTransactionManagerController
        Inherits PickProController

        ' GET: /ToteTransactionManager
        ''' <summary>
        ''' Loads the page with the print direct preference
        ''' </summary>
        ''' <returns>A view object that contians the information needed for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Return View(New With {.printDir = Induction.Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name).PrintDirect})
        End Function

        ''' <summary>
        ''' Gets the tote transaction manager datatable
        ''' </summary>
        ''' <param name="data">The datat being returned from the js to get the correct results within the datatable</param>
        ''' <returns>Table object that contains the data needed to populate the datatable</returns>
        ''' <remarks></remarks>
        Function ToteTransManDT(ByVal data As TableObjectSent) As ActionResult
            Return Json(Induction.ToteTransactionsManager.selectToteTransManTable(data.draw, data.batchid, data.start + 1, data.length + data.start,
                                                                                  Request.QueryString.Get("order[0][column]"),
                                                                                  Request.QueryString.Get("order[0][dir]"), data.filter,
                                                                                  User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the data for the batch pick id typeahead
        ''' </summary>
        ''' <param name="query">The vlaue wthat is entered within the typeahead</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function CreateBatchPickIDTypeahead(query As String) As ActionResult
            Return Json(Induction.ToteTransactionsManager.selectBatchPickTA(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or previews the pick or put away list for the given tote id 
        ''' </summary>
        ''' <param name="ToteID">The tote id to print/preview</param>
        ''' <param name="ZoneLabel">The zone label for the tote id</param>
        ''' <param name="TransType">The transaction type of the tote id</param>
        ''' <param name="PrintDirect">If the print should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print jib was successful</returns>
        ''' <remarks></remarks>
        Public Function PrintPrevToteContents(ToteID As String, ZoneLabel As String, TransType As String, PrintDirect As Boolean) As ActionResult
            Dim username As String = User.Identity.Name, WSID As String = Session("WSID")
            Try
                Dim filename As String = "", sp As String = "selOTContentsToteTranLL", params As String(,) = {{"@ToteID", ToteID, strVar}, {"@ZoneLab", ZoneLabel, strVar}}
                If TransType.ToLower() = "pick" Then
                    filename = "IMPickList.lst"
                Else
                    filename = "IMPutList.lst"
                End If
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents List", "List", filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("CustomReportsHub", "PrintPrevToteContents", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or previews the off carousel pick or put away list for the given tote id
        ''' </summary>
        ''' <param name="ToteID">The tote id to print/preview</param>
        ''' <param name="TransType">The transaction type of the tote id</param>
        ''' <param name="PrintDirect">If the print should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintPrevOffCarList(ToteID As String, TransType As String, PrintDirect As Boolean) As ActionResult
            Dim username As String = User.Identity.Name, WSID As String = Session("WSID")
            Try
                Dim filename As String = ""
                If TransType.ToLower() = "pick" Then
                    filename = "IMOCPick.lst"
                Else
                    filename = "IMOCPut.lst"
                End If
                Dim sp As String = "selOTOffCarToteTranLL", params As String(,) = {{"@ToteID", ToteID, strVar}, {"@TransType", TransType, strVar}}

                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Off Carousel List", "List", filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("CustomReportsHub", "PrintPrevOffCarList", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace