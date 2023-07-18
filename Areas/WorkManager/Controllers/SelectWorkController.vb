' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.IO
Imports Microsoft.AspNet.SignalR.Hubs
Imports System.Data.SqlClient

Namespace WorkManager.Controllers
    <Authorize()>
    Public Class SelectWorkController
        Inherits PickProController

        ' GET: SelectWork
        ''' <summary>
        ''' Gets the select work pages
        ''' </summary>
        ''' <param name="OrganizeWork">If select work page was reached from the organize work page</param>
        ''' <returns>A view object that contains the information for the html page</returns>
        Function Index(Optional OrganizeWork As Boolean = False) As ActionResult
            Dim m As Object = New With {.UserPrefs = WorkManager.WMPreferences.GetUserSettings(User.Identity.Name, User.Identity.Name, Session("WSID")), _
                                         .WSPrefs = WorkManager.WMPreferences.GetGeneralBatchSettings(User.Identity.Name, Session("WSID")), _
                                         .Counts = WorkManager.SelectWork.GetWorkCounts(User.Identity.Name, "pick", Session("WSID")), _
                                         .OrganizeWork = OrganizeWork, _
                                        .Alias = New AliasModel(User.Identity.Name, Session("WSID"))}
            Return View(model:=m)
        End Function

        ''' <summary>
        ''' Gets the (un)selected work tables for the datatable plugin
        ''' </summary>
        ''' <param name="data">Object that contaisn the data needed to get the records for the table</param>
        ''' <returns>A table object that contains the data for the table</returns>
        ''' <remarks></remarks>
        Function GetSelectWorkTables(data As TableObjectSent) As ActionResult
            Dim sortCol = Request.QueryString.Get("order[0][column]")
            If IsNothing(sortCol) Then sortCol = -1
            Dim sortOrder = Request.QueryString.Get("order[0][dir]")
            If IsNothing(sortOrder) Then sortOrder = ""
            Return Json(WorkManager.SelectWork.GetSelectWorkTables(data.draw, data.start, data.start + data.length, data.which, data.selected, data.transType, data.batchid, _
                                                                   Replace(data.entryFilter, "[Username]", "[Open Transactions].[Username]"), sortCol, _
                                                      sortOrder, User.Identity.Name, Session("WSID"), data.users), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the transaction detail modal table for select work
        ''' </summary>
        ''' <param name="data">Object that contaisn the data needed to get the records for the table</param>
        ''' <returns>A table object that contains the data for the table</returns>
        ''' <remarks></remarks>
        Function GetTransDetailTable(data As TableObjectSent) As ActionResult
            Dim reader As SqlDataReader = Nothing, table As New List(Of List(Of String)), innerlist As New List(Of String)
            Dim details As New TableObject(data.draw, 0, 0, New List(Of List(Of String)))
            Dim sortCol = Request.QueryString.Get("order[0][column]")
            If IsNothing(sortCol) Then sortCol = 0
            Dim sortOrder = Request.QueryString.Get("order[0][dir]")
            If IsNothing(sortOrder) Then sortOrder = ""
            Dim cols As New List(Of String) From {"[Transaction Type]", "[Batch Pick ID]", "[Order Number]", "[Line Number]", "[Item Number]", "[Location]", "[Description]", "[Required Date]", "[Priority]", "[Unit of Measure]", "[Lot Number]", "[Expiration Date]", _
                "[Serial Number]", "[Revision]", "[Transaction Quantity]", "[Tote ID]", "[Tote Number]", "[Warehouse]", "[Zone]", "[Carousel]", "[Row]", "[Shelf]", "[Bin]", "[Inv Map ID]", "[Notes]", "[Label]", "[User Field1]", _
                "[User Field2]", "[User Field3]", "[User Field4]", "[User Field5]", "[User Field6]", "[User Field7]", "[User Field8]", "[User Field9]", "[User Field10]", "[Cell]", "[Host Transaction ID]", "[Emergency]"}
            Dim sortColStr As String = cols(sortCol)
            Try
                reader = RunSPArray("selWMTransDetails", Session("WSID"), {{"@SortCol", sortColStr, strVar}, {"@SortOrder", sortOrder, strVar}, {"@StartRow", data.start, intVar}, _
                                                                           {"@EndRow", (data.start + data.length), intVar}, {"@User", data.users, strVar}, {"@TransType", data.transType, strVar}, _
                                                                           {"@PluginWhere", data.entryFilter, strVar}, {"@BatchOrderTote", data.batchid, strVar}, {"@BOTValue", data.ordernum, strVar}})
                For i As Integer = 0 To 3
                    If reader.HasRows Then
                        While reader.Read
                            Select Case i
                                Case 0
                                    If Not IsDBNull(reader(0)) Then details.recordsTotal = reader(0)
                                Case 1
                                    If Not IsDBNull(reader(0)) Then details.recordsFiltered = reader(0)
                                Case Else
                                    For x As Integer = 0 To reader.FieldCount - 1
                                        innerlist.Add(CheckDBNull(reader(x)))
                                    Next
                                    details.data.Add(innerlist)
                                    innerlist = New List(Of String)
                            End Select
                        End While
                        reader.NextResult()
                    End If
                Next
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("SelectWorkController", "GetTransDetailTable", ex.Message, User.Identity.Name, Session("WSID"))
            End Try
            Return Json(details, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints a work manager work list for the current user of the work selected
        ''' </summary>
        ''' <param name="transType">The transaction type of the selected work</param>
        ''' <param name="batchOrderTote">Tells if the batch id, order number, or tote id identify the selected work</param>
        ''' <param name="selected">The work that si going to be printed</param>
        ''' <param name="printDirect">If the print job should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintWMWorkList(transType As String, batchOrderTote As String, selected As String, printDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selWMPrintWorkList"
                Dim params As String(,) = {{"@TransType", transType, strVar}, {"@BatchOrderTote", batchOrderTote, strVar}, {"@Selected", selected, strVar}, {"@User", username, strVar}}
                Dim filename As String = "", LLType As String = "List"
                Select Case transType.ToLower()
                    Case "pick"
                        filename = "WorkPickOrders.lst"
                    Case "put away"
                        filename = "WorkPutOrders.lst"
                    Case Else
                        filename = "WorkCountOrders.lst"
                End Select
                If printDirect Then
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Work List -" & transType, "List", filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("SelectWorkController", "PrintWMWorkList", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints item labels for the transactions selected by the user in Select Work in Work Manager
        ''' </summary>
        ''' <param name="transType">The trasnaction type of the selected work</param>
        ''' <param name="batchOrderTote">Tells if the batch id, order number, or tote id identify the selected work</param>
        ''' <param name="selected">The selected work to print</param>
        ''' <param name="PrintDirect">If the print job should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the oepration completed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintWMItemLabel(transType As String, batchOrderTote As String, selected As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim sp As String = "selWMItemLabel"
                Dim params As String(,) = {{"@TransType", transType, strVar}, {"@BatchOrderTote", batchOrderTote, strVar}, {"@Selected", selected, strVar}, {"@User", username, strVar}}
                Dim filename As String = "", LLType As String = "Label"
                Select Case transType.ToLower()
                    Case "pick"
                        filename = "PickLabel.lbl"
                    Case "put away"
                        filename = "PutLabel.lbl"
                    Case Else
                        filename = "CountLabel.lbl"
                End Select
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Item Label - " & transType, LLType, filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("SelectWorkController", "PrintWMItemLabel", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints tote labels for the transactions selected by the user in Select Work in Work Manager
        ''' </summary>
        ''' <param name="transType">The transaction type of the work</param>
        ''' <param name="batchOrderTote">Tells if the batch id, order number, or tote id identify the selected work</param>
        ''' <param name="selected">The selected work to be printed</param>
        ''' <param name="PrintDirect">If the print job will be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintWMToteLabel(transType As String, batchOrderTote As String, selected As String, PrintDirect As Boolean)
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim sp As String = "selWMToteLabel"
                Dim params As String(,) = {{"@TransType", transType, strVar}, {"@BatchOrderTote", batchOrderTote, strVar}, {"@Selected", selected, strVar}, {"@User", username, strVar}}
                Dim filename As String = "", LLType As String = "Label"
                Select Case transType.ToLower()
                    Case "pick"
                        filename = "PickToteLabel.lbl"
                    Case Else
                        filename = "PutToteLabel.lbl"
                End Select
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Label - " & transType, LLType, filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("SelectWorkController", "PrintWMToteLabel", ex.Message, username, WSID)
                Return False
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace