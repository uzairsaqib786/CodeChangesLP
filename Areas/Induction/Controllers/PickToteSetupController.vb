' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Induction.Controllers
    <Authorize()>
    Public Class PickToteSetupController
        Inherits PickProController

        ' GET: /PickToteSetup
        ''' <summary>
        ''' Populates the page with the current preferences setup and count info for the mixed, carousel, and off carousel count
        ''' </summary>
        ''' <returns>A view object that contains data for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Return View(New With {.CountInfo = Induction.PickToteSetup.getCountInfo(Session("WSID"), User.Identity.Name),
                                  .Prefs = Induction.Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name),
                                  .PickBatches = GetResultMapList("selOTPickBatches", Session("WSID"))})
        End Function

        ''' <summary>
        ''' View for sperate screen depedning on preference
        ''' </summary>
        ''' <returns></returns>
        Function InZonePickScreen() As ActionResult
            Return View(New With {.Prefs = Induction.Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name), .WSZones = Induction.PickToteSetup.SelectWSZones(User.Identity.Name, Session("WSID"))})
        End Function

        ''' <summary>
        ''' Creates the typeahead for the filter text field within the pick tote batch manager modal
        ''' </summary>
        ''' <param name="query">the value currently typed into the typeahead</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function CreateFilterTypeahead(query As String) As ActionResult
            Return Json(Induction.PickToteSetup.selectFilterTA(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the data for the transaction datatable within the pick tote batch manager modal
        ''' </summary>
        ''' <param name="data">The fields that are being passed back in orde r to fill the table with the correct results</param>
        ''' <returns>A table object that contains the data to populate the datatable</returns>
        ''' <remarks></remarks>
        Function PickToteTransDT(ByVal data As TableObjectSent) As ActionResult
            Return Json(Induction.PickToteSetup.selectPickToteManTransTable(data.draw, data.ordernum, data.start + 1, data.length + data.start, _
                                                                                  Request.QueryString.Get("order[0][column]"), _
                                                                                  Request.QueryString.Get("order[0][dir]"), data.filter, _
                                                                                  User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or previews the off carousel pick list for all tote ids contained in the list
        ''' </summary>
        ''' <param name="Positions"></param>
        ''' <param name="ToteIDs"></param>
        ''' <param name="OrderNums"></param>
        ''' <param name="BatchID"></param>
        ''' <param name="PrintDirect"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function PrintPrevIMPickList(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@Positions", String.Join(",", Positions.ToArray), strVar}, _
                                          {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar}, _
                                          {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar}, _
                                          {"@BatchID", BatchID, strVar}}
                Dim filename As String = "IMOCPick.lst", sp As String = "selOTPickTotePickListLL"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents List", "List", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickList", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Print or Previews the pick item label for all tote ids within the list
        ''' </summary>
        ''' <param name="Positions">The tote numbers within the batch id</param>
        ''' <param name="ToteIDs">The tote ids wihtin the batch id</param>
        ''' <param name="OrderNums">The order numbers wihtin the batch id</param>
        ''' <param name="BatchID">The batch id that is to be printed/previewed</param>
        ''' <param name="PrintDirect">If the rpoert should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintPrevIMPickItemLabel(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), Username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@Positions", String.Join(",", Positions.ToArray), strVar}, _
                                          {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar}, _
                                          {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar}, _
                                          {"@BatchID", BatchID, strVar}, _
                                          {"@WSID", WSID, strVar}}
                Dim filename As String = "IMPickItem.lbl", sp As String = "selOTPickTotePickItemLabLL"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(Username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(Username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickItemLabel", ex.ToString(), Username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or Previews the pick tote label for all tote ids within the list
        ''' </summary>
        ''' <param name="Positions">The tote numbers for the tote dis</param>
        ''' <param name="ToteIDs">The tote ids to print or preview</param>
        ''' <param name="OrderNums">The order numbers for each tote id</param>
        ''' <param name="PrintDirect">If the print job should be direclty sent to the printer</param>
        ''' <returns>A boolean telling if the print job was executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintPrevIMPickToteLabelButt(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@Positions", String.Join(",", Positions.ToArray), strVar}, _
                                          {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar}, _
                                          {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar}}
                Dim filename As String = "IMPickTote.lbl", sp As String = "selOTPickTotePrintToteLabLL"
                If PrintDirect Then
                    Dim clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms)
                    clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickToteLabelButt", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints the off carousel pick list for the all tote ids contained in the list
        ''' </summary>
        ''' <param name="Positions">The tote numbers for the tote ids</param>
        ''' <param name="ToteIDs">The tote ids to be printed or previewed</param>
        ''' <param name="OrderNums">The order numbers for each tote id</param>
        ''' <param name="PrintDirect">If the print job should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job completed successfully</returns>
        ''' <remarks></remarks>
        Function PrintPrevOffCarPickList(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), Username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@Positions", String.Join(",", Positions.ToArray), strVar}, _
                                          {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar}, _
                                          {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar}}
                Dim sp As String = "selOTPickToteProcessOffCarPickListLL", filename As String = "IMOCPick.lst"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents List", "List", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(Username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("CustomReportsHub", "PrintPrevOffCarPickList", ex.ToString(), Username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or previews the pick tote label for all tote ids in the list
        ''' </summary>
        ''' <param name="Positions">The tote numbers for the tote ids</param>
        ''' <param name="ToteIDs">The tote ids to print or preview</param>
        ''' <param name="OrderNums">The order numbers for each tote id</param>
        ''' <param name="BatchID">The batch id that the tote ids are wihtin</param>
        ''' <param name="PrintDirect">If the print job should be sent directly to the printer</param>
        ''' <returns>A boolean that tells if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintPrevPickToteLabel(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), Username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@Positions", String.Join(",", Positions.ToArray), strVar},
                                          {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar},
                                          {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar},
                                          {"@BatchID", BatchID, strVar}}
                Dim sp As String = "selOTPickToteProcessToteLabelLL", filename As String = "IMPickTote.lbl"
                If PrintDirect Then
                    Dim clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    clients.Print(ListLabelHelperFunctions.GetStandardLLPrintProperties(Username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms))
                Else
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", ListLabelHelperFunctions.GetStandardLLExportProperties(Username, WSID, Server, "preview", sp, Parms, filename))
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("CustomReportsHub", "PrintPrevPickToteLabel", ex.ToString(), Username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or Previews the pick tote label for all tote ids within the list
        ''' </summary>
        ''' <param name="BatchID">The bathc ot print for</param>
        ''' <param name="PrintDirect">If the print job should be direclty sent to the printer</param>
        ''' <returns>A boolean telling if the print job was executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintPrevIMPickBatchToteLabel(BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@BatchID", BatchID, strVar}}
                Dim filename As String = "IMPickTote.lbl", sp As String = "selOTPickPrintToteBatchLL"
                If PrintDirect Then
                    Dim clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms)
                    clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickBatchToteLabel", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Print or Previews the pick item label for all tote ids within the list
        ''' </summary>
        ''' <param name="BatchID">The batch id that is to be printed/previewed</param>
        ''' <param name="PrintDirect">If the rpoert should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintPrevIMPickBatchItemLabel(BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), Username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@BatchID", BatchID, strVar},
                                          {"@WSID", WSID, strVar}}
                Dim filename As String = "IMPickItem.lbl", sp As String = "selOTPickPrintBatchItemLL"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(Username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(Username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickBatchItemLabel", ex.ToString(), Username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function


        ''' <summary>
        ''' Prints or previews the off carousel pick list for all tote ids contained in the list
        ''' </summary>
        ''' <param name="BatchID"></param>
        ''' <param name="PrintDirect"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function PrintPrevIMPickBatchList(BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@BatchID", BatchID, strVar}}
                Dim filename As String = "IMOCPick.lst", sp As String = "selOTPickPrintListBatchLL"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents List", "List", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickBatchList", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Function for bloddhound order number typeahead
        ''' </summary>
        ''' <param name="query">The filter value</param>
        ''' <returns>json object containing typeahead data</returns>
        Function OrderNumberTypeahead(query As String) As ActionResult
            Return Json(PickToteSetup.SelectOrderNumberTA(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Function for bloodhound order number typeahead
        ''' </summary>
        ''' <param name="query">The filter value</param>
        ''' <returns>json object containing typeahead data</returns>
        Function BatchPickIDTypeahead(query As String) As ActionResult
            Return Json(PickToteSetup.SelectBatchPickIDTA(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Function for getting in zone datatable
        ''' </summary>
        ''' <param name="data">The data for the table request</param>
        ''' <returns>Json object containing the resulting table data</returns>
        Function InZoneTransDT(ByVal data As TableObjectSent) As ActionResult
            Return Json(PickToteSetup.selectInZoneTransDT(data.draw, data.ordernum, data.start + 1, data.length + data.start,
                                                            Request.QueryString.Get("order[0][column]"),
                                                            Request.QueryString.Get("order[0][dir]"), data.filter,
                                                            User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles printing the inzone tote label
        ''' </summary>
        ''' <param name="Positions">Tote positions</param>
        ''' <param name="ToteIDs">The totes needed for printing</param>
        ''' <param name="OrderNums">Order numbers assigned to each tote</param>
        ''' <param name="PrintDirect">if print should be sent to printer</param>
        ''' <returns>Json object telling if print was successful</returns>
        Function PrintPrevInZoneToteLabel(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@Positions", String.Join(",", Positions.ToArray), strVar},
                                          {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar},
                                          {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar},
                                          {"@WSID", WSID, strVar}}
                Dim filename As String = "IMPickTote.lbl", sp As String = "selOTPickToteInZoneLL"
                If PrintDirect Then
                    Dim clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms)
                    clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickToteLabelButt", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' handles printing in zone item label
        ''' </summary>
        ''' <param name="Positions">Tote positions</param>
        ''' <param name="ToteIDs">The totes needed for printing</param>
        ''' <param name="OrderNums">Order numbers assigned to each tote</param>
        ''' <param name="BatchID">The batch id that these are being assigned to</param>
        ''' <param name="PrintDirect">if print should be sent to printer</param>
        ''' <returns>Json object telling if print was successful</returns>
        Function PrintPrevInZoneItemLabel(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), Username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@Positions", String.Join(",", Positions.ToArray), strVar},
                                          {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar},
                                          {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar},
                                          {"@BatchID", BatchID, strVar},
                                          {"@WSID", WSID, strVar}}
                Dim filename As String = "IMPickItem.lbl", sp As String = "selOTPickItemInZoneLL"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(Username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(Username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickItemLabel", ex.ToString(), Username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' handles printing in zone pick list
        ''' </summary>
        ''' <param name="Positions">Tote positions</param>
        ''' <param name="ToteIDs">The totes needed for printing</param>
        ''' <param name="OrderNums">Order numbers assigned to each tote</param>
        ''' <param name="BatchID">The batch id that these are being assigned to</param>
        ''' <param name="PrintDirect">if print should be sent to printer</param>
        ''' <returns>Json object telling if print was successful</returns>
        Function PrintPrevInZonePickList(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@Positions", String.Join(",", Positions.ToArray), strVar},
                                          {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar},
                                          {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar},
                                          {"@BatchID", BatchID, strVar},
                                          {"@WSID", WSID, strVar}}
                Dim filename As String = "IMOCPick.lst", sp As String = "selOTInZonePickListLL"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents List", "List", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickList", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles printing the in zone batch tote label
        ''' </summary>
        ''' <param name="BatchID">The batch id to print for</param>
        ''' <param name="PrintDirect">if print should be sent to printer</param>
        ''' <returns>Json object telling if print was successful</returns>
        Function PrintPrevInZoneBatchToteLabel(BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@BatchID", BatchID, strVar}, {"@WSID", WSID, strVar}}
                Dim filename As String = "IMPickTote.lbl", sp As String = "selOTInZoneToteBatchLL"
                If PrintDirect Then
                    Dim clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms)
                    clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevIMPickBatchToteLabel", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles printing the in zone case label
        ''' </summary>
        ''' <param name="BatchID">The batch id to print for</param>
        ''' <param name="PrintDirect">if print should be sent to printer</param>
        ''' <returns>Json object telling if print was successful</returns>
        Function PrintPrevInZoneCaseLabel(BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@BatchID", BatchID, strVar}}
                Dim filename As String = "CaseLabel.lbl", sp As String = "selOTInZoneCaseLL"
                If PrintDirect Then
                    Dim clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Case Label", "Label", filename, sp, Parms)
                    clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevInZoneCaseLabell", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        Function PrintPrevPickBatchList(BatchID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@BatchID", BatchID, strVar}}
                Dim filename As String = "IMPickBatch.lst", sp As String = "selOTPickBatchListLL"
                If PrintDirect Then
                    Dim clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Pick Batch List", "List", filename, sp, Parms)
                    clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PickToteSetupController", "PrintPrevPickBatchList", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function


    End Class
End Namespace