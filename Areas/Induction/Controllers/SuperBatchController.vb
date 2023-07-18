' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports System.Web.Mvc

Namespace Induction.Controllers
    <Authorize>
    Public Class SuperBatchController
        Inherits PickProController

        ' GET: SuperBatch
        ''' <summary>
        ''' Gets the super batch controller page
        ''' </summary>
        ''' <returns>A view object that contains data for the html page</returns>
        Function Index() As ActionResult
            Return View(New With {.BatchZones = GetResultMapList("selOTSuperBatchLeftNew", Session("WSID")),
                                  .BatchZonesByTote = GetResultMapList("selOTSuperBatchLeftToteNew", Session("WSID")),
                                  .Preferences = Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name),
                                  .SuperBatches = GetResultMapList("selOTSuperBatches", Session("WSID")),
                                  .ItemNums = GetResultMapList("selOTSuperBatchItemNums", Session("WSID"))})

        End Function

        ''' <summary>
        ''' Creates a super batch
        ''' </summary>
        ''' <param name="Zone">The desired zone to batch</param>
        ''' <param name="ToBatch">The number of things to batch</param>
        ''' <param name="ToteID">The desired tote id to batch</param>
        ''' <returns>A string that tells how mnay orders are remaining</returns>
        Function CreateSuperBatch(Zone As String, ToBatch As Integer, ToteID As String, ItemNum As String, batchByOrder As Integer) As JsonResult
            Dim remainingOrders As String = ""
            Try
                If batchByOrder = 1 Then
                    remainingOrders = GetResultSingleCol("updOTSuperBatchInductionNew", Session("WSID"), {{"@Zone", Zone, strVar}, {"@ToteID", ToteID, strVar}, {"@BatchCount", ToBatch, intVar}, {"@BatchID", DateTime.Now().ToString("yyyyMMddhhmm") & "S-", strVar}})
                ElseIf batchByOrder = 0 Then
                    remainingOrders = GetResultSingleCol("updOTSuperBatchInductionToteNew", Session("WSID"), {{"@Zone", Zone, strVar}, {"@ToteID", ToteID, strVar}, {"@BatchCount", ToBatch, intVar}, {"@BatchID", DateTime.Now().ToString("yyyyMMddhhmm") & "S-", strVar}})
                Else
                    remainingOrders = GetResultSingleCol("updOTSuperBatchInductionItemNum", Session("WSID"), {{"@Zone", Zone, strVar}, {"@ToteID", ToteID, strVar}, {"@BatchCount", ToBatch, intVar}, {"@BatchID", DateTime.Now().ToString("yyyyMMddhhmm") & "S-", strVar}, {"@ItemNum", ItemNum, strVar}})
                End If
            Catch ex As Exception
                insertErrorMessages("SuperBatch", "CreateSuperBatch", ex.Message, User.Identity.Name, Session("WSID"))
                remainingOrders = "Error"
            End Try
            Return Json(remainingOrders)
        End Function


        Function SelectItemZoneData(Type As String, ItemNumber As String) As JsonResult
            Dim DataReader As SqlDataReader = Nothing
            Dim Data As New List(Of Object)

            Try
                If Type = "Item" Then
                    DataReader = RunSPArray("selOTSuperBatchItemZones", Session("WSID"), {{"@ItemNum", ItemNumber, strVar}})
                ElseIf Type = "Order" Then
                    DataReader = RunSPArray("selOTSuperBatchLeftNew", Session("WSID"), {{"nothing"}})
                Else
                    DataReader = RunSPArray("selOTSuperBatchLeftToteNew", Session("WSID"), {{"nothing"}})
                End If


                If DataReader.HasRows Then
                    While DataReader.Read()
                        Data.Add(New With {.Zone = DataReader("Zone"), .TotalTrans = DataReader("Total Transactions")})
                    End While
                End If

            Catch ex As Exception
                insertErrorMessages("SuperBatch", "SelectItemZoneData", ex.Message, User.Identity.Name, Session("WSID"))
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Json(Data)
        End Function

        ''' <summary>
        ''' Selects required date per zone data in order to populate the sb required date status modal
        ''' </summary>
        ''' <returns>List of list of string that encapsulates data</returns>
        Function SelectReqDateData() As JsonResult
            Dim DataReader As SqlDataReader = Nothing
            Dim Data As New List(Of List(Of String))

            Try
                DataReader = RunSPArray("selOTSuperBatchInductionReqDates", Session("WSID"), {{"nothing"}})

                If DataReader IsNot Nothing AndAlso DataReader.HasRows Then
                    While DataReader.Read()
                        Dim InnerData As New List(Of String)
                        InnerData.Add(DataReader("ReqDate"))
                        InnerData.Add(DataReader("Zone"))
                        InnerData.Add(DataReader("CountToInduct"))
                        Data.Add(InnerData)
                    End While
                End If

            Catch ex As Exception
                insertErrorMessages("SuperBatch", "SelectReqDateData", ex.Message, User.Identity.Name, Session("WSID"))
                Data = New List(Of List(Of String))
                Data.Add(New List(Of String) From {"Error", "Error", "Error"})
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Json(Data)
        End Function

        ''' <summary>
        ''' Inserts the tote ID of a newly created Super Batch into the Totes to Print table.
        ''' </summary>
        ''' <param name="toteID">Tote ID of the new Super Batch</param>
        ''' <returns></returns>
        Function insTotePrintTable(toteID As String) As JsonResult
            Dim errorMessage As String = ""
            Try
                RunActionSP("insTotePrintSuperBatchNew", Session("WSID"), {{"@toteID", toteID, strVar}})
            Catch ex As Exception
                insertErrorMessages("SuperBatch", "insTotePrintTable", ex.Message, User.Identity.Name, Session("WSID"))
                errorMessage = "Error"
            End Try
            Return Json(errorMessage)
        End Function


        ''' <summary>
        ''' Prints Super Batch Label
        ''' </summary>>
        ''' <param name="ToteID">The batch to print for</param>
        ''' <param name="printDirect">If the print job should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintSuperBatchLabel(ToteID As String, Optional printDirect As Boolean = True) As ActionResult
            Dim username As String = User.Identity.Name, WSID As String = Session("WSID")

            Try
                Dim Parms As String(,) = {{"@ToteID", ToteID, strVar}}
                Dim sp As String = "selOTSuperBatchLL"
                Dim filename As String = "IMPickTote.lbl"

                If printDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Batch Pick ID", "Label", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SuperBatchController", "PrintSuperBatchLabel", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try

            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function


        ''' <summary>
        ''' Print or Previews the pick item label for all tote ids within the list
        ''' </summary>
        ''' <param name="ToteID">The batch id that is to be printed/previewed</param>
        ''' <param name="PrintDirect">If the rpoert should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintSuperBatchOrderLabel(ToteID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), Username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@ToteID", ToteID, strVar}}
                Dim filename As String = "IMPickItem.lbl", sp As String = "selOTSuperBatchOrderLL"

                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(Username, WSID, Server, "Batch Orders Label", "Label", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(Username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("SuperBatchController", "PrintSuperBatchOrderLabel", ex.Message, Username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles printing the case label
        ''' </summary>
        ''' <param name="ToteID">The batch id to print for</param>
        ''' <param name="PrintDirect">if print should be sent to printer</param>
        ''' <returns>Json object telling if print was successful</returns>
        Function PrintPrevInZoneCaseLabelToteID(ToteID As String, PrintDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim Parms As String(,) = {{"@ToteID", ToteID, strVar}}
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
                Debug.Print(ex.Message)
                insertErrorMessages("PickToteSetupController", "PrintPrevInZoneCaseLabell", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function


    End Class
End Namespace