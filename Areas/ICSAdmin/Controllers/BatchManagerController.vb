' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient
Namespace Admin.Controllers
    <Authorize()>
    Public Class BatchManagerController
        Inherits ICSAdminController

        ' GET: /BatchManager
        Function Index() As ActionResult
            Return View(New With {.BMTable = BatchManager.getBMTable("Pick", User.Identity.Name, Session("WSID")),
                                  .BMSettings = BatchManager.getBMSettings(User.Identity.Name, Session("WSID")),
                                  .Batches = BatchManager.selectBatchesDeleteDrop("Pick", User.Identity.Name, Session("WSID")),
                                  .Aliases = New AliasModel(User.Identity.Name, Session("WSID"))})
        End Function

        ''' <summary>
        ''' Gets the left super batch table
        ''' </summary>
        ''' <param name="data">The data needed to get the records for the datatable</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        ''' <remarks></remarks>
        Function getSBSingleLines(data As TableObjectSent) As ActionResult
            Return Json(BatchManager.getSBTables(data.draw, User.Identity.Name, Session("WSID"), True), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the right super batch table
        ''' </summary>
        ''' <param name="data">The data needed to get the records for the datatable</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        ''' <remarks></remarks>
        Function getSBBatchedTables(data As TableObjectSent) As ActionResult
            Return Json(BatchManager.getSBTables(data.draw, User.Identity.Name, Session("WSID"), False), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints batch label(s) specified
        ''' </summary>
        ''' <param name="transType">The transaction type of the batch</param>
        ''' <param name="orders">The order numbers wihtin the batch</param>
        ''' <param name="BatchID">The id of the batch to be rointed</param>
        ''' <returns>A boolean telling if the print job was successful</returns>
        ''' <remarks></remarks>
        Public Function PrintBatchLabel(transType As String, orders As String, BatchID As String) As ActionResult
            Dim reader As SqlDataReader = Nothing, OnePerQty As Boolean = False, WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                reader = RunSPArray("selLabelOnePerQty", WSID, {{"nothing"}})
                If reader.HasRows Then
                    If reader.Read() Then
                        If Not IsDBNull(reader(0)) Then
                            OnePerQty = CBool(reader(0))
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("BatchManagerController", "PrintBatchLabel", ex.Message, username, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try

            Try
                ' sends print request to the windows service
                Dim sp As String = "selOTIDLL"
                Dim params As String(,) = {{"@TransType", transType, strVar}, {"@Orders", orders, strVar}}
                Dim filename As String = IIf(transType.ToLower() = "pick", "BMPickLabel.lbl", "BMPutLabel.lbl"), LLType As String = "Label"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Batch Labels", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("BatchManagerController", "PrintBatchLabel", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints a batch report of the selected data
        ''' </summary>
        ''' <param name="transType">The transaction typ eof the batch</param>
        ''' <param name="orders">The order numbers within the bacth</param>
        ''' <param name="batchID">The id of the batch to be printed</param>
        ''' <returns>A boolean telling if the print job was successful</returns>
        ''' <remarks></remarks>
        Public Function PrintBatchReport(transType As String, orders As String, batchID As String) As ActionResult
            Dim filename As String = ""
            Select Case transType.ToLower().Trim()
                Case "pick"
                    filename = "BMPickList.lst"
                Case "put away"
                    filename = "BMPutList.lst"
                Case Else
                    filename = "BMCountList.lst"
            End Select

            Dim ZoneLabel As String = "Zones:"
            Dim DataReader As SqlDataReader = Nothing
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name

            Try
                DataReader = RunSPArray("selBMReportZoneLabel", WSID, {{"@Orders", orders, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        ZoneLabel = ZoneLabel + " " + CStr(DataReader(0)) + "(" + CStr(DataReader(1)) + ")"
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("BatchManagerController", "Zone Label Section", ex.Message, username, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try


            Dim Params As String(,) = {{"@TransType", transType, strVar}, {"@Orders", orders, strVar}, {"@BatchID", batchID, strVar}, {"@ZoneLabel", ZoneLabel, strVar}}

            Try
                Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                ' sends print request to the windows service

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Batch Report", "List", filename, "selBMReportForPrint", Params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Inventory Map", "PrintBatchLabel", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace
