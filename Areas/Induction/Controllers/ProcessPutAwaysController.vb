﻿' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports System.Web.Mvc
Imports CommonSD
Imports Newtonsoft.Json
Imports RepositorySD

Namespace Induction.Controllers
    <Authorize()>
    Public Class ProcessPutAwaysController
        Inherits PickProController

        ' GET: /ProcessPutAways
        ''' <summary>
        ''' Gets the Process Put Away page
        ''' </summary>
        ''' <returns>A view object that contains the data for the html page</returns>
        Function Index() As ActionResult
            ' aliases: Item Number, User Field1, User Field2, etc. have different names depending on preferences in system preferences.
            ' preferences: IM Preferences table.
            ' topBatch: initialize to the first batch available to the workstation, otherwise offer to set up a batch immediately on page load.
            ' ReelLogic: system preference for how reels are handled (on this page we're mostly concerned with "dynamic" vs. not.)
            Return View(model:=New With {.aliases = New AliasModel(User.Identity.Name, Session("WSID")), .preferences = Induction.Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name),
                                         .topBatch = Induction.ProcessPutAways.GetTop1Batch(Session("WSID"), User.Identity.Name), .ReelLogic = Induction.Preferences.selectReelLogic(User.Identity.Name, Session("WSID"))})
        End Function

        ''' <summary>
        ''' Provides a publically exposed method for the batch ID field typeahead.
        ''' </summary>
        ''' <param name="BatchID">The value to get suggestions for</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function BatchIDTypeahead(BatchID As String) As ActionResult
            Return Json(Induction.ProcessPutAways.BatchIDTypeahead(BatchID, Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Allows the totes table to be refreshed and/or populated.
        ''' </summary>
        ''' <param name="data">Ibject that contians the infomration in order to get the table populated</param>
        ''' <returns>A table object that contains the data for the datatable</returns>
        ''' <remarks></remarks>
        Function GetTotesTable(ByVal data As TableObjectSent) As ActionResult
            If IsNothing(data.entryFilter) Then
                data.entryFilter = ""
            End If
            Return Json(Induction.ProcessPutAways.GetTotesTable(data.draw, data.entryFilter, Request.QueryString.Get("order[0][dir]"),
                                                                    Request.QueryString.Get("order[0][column]"), Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Returns a page with the specified batch's tote's contents
        ''' </summary>
        ''' <param name="BatchID">Batch id to be displayed</param>
        ''' <param name="ToteNum">The tote number for the batch id</param>
        ''' <param name="ToteID">Tote id within the batch id</param>
        ''' <param name="ZoneLabel">Zine label for the batch id</param>
        ''' <returns>View object that contains data for the html page</returns>
        ''' <remarks></remarks>
        Function ToteTransView(BatchID As String, ToteNum As Integer, ToteID As String, ZoneLabel As String) As ActionResult
            Return View(New With {.batchid = BatchID, .totenum = ToteNum, .toteid = ToteID, .zonelab = ZoneLabel, .prefs = Induction.Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name)})
        End Function

        ''' <summary>
        ''' Provides the datatable for tote contents
        ''' </summary>
        ''' <param name="data">Object that contains data in order to get records for the table</param>
        ''' <returns>A table object that contains the records for the datatable in order to populate it</returns>
        ''' <remarks></remarks>
        Function getToteTransViewTable(ByVal data As TableObjectSent) As ActionResult
            Return Json(Induction.ToteTransView.selectToteTransViewTable(data.draw, data.totenum, data.batchid, data.start + 1, data.length + data.start,
                                                                         Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"),
                                                                         User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Provides the typeahead for the batch-tote-transaction location assignment.
        ''' </summary>
        ''' <param name="location">The value to get suggestions for</param>
        ''' <param name="warehouse">>The warehouse value to filter invalid results</param>
        ''' <param name="serial">>The serial number value to filter invalid results</param>
        ''' <param name="lot">>The lot number value to filter invalid results</param>
        ''' <param name="ccell">>The cell size value to filter invalid results</param>
        ''' <param name="cvel">>The velocity code value to filter invalid results</param>
        ''' <param name="bcell">The bulk cell size value to filter invalid results></param>
        ''' <param name="bvel">>The bulk velocity code value to filter invalid results</param>
        ''' <param name="cfcell">>The carton flow cell size value to filter invalid results</param>
        ''' <param name="cfvel">>The carton flow velocity code value to filter invalid results</param>
        ''' <param name="item">>The item number value to filter invalid results</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function GetBatchLocationTypeahead(location As String, warehouse As String, serial As String, lot As String, ccell As String, cvel As String,
                                        bcell As String, bvel As String, cfcell As String, cfvel As String, item As String) As ActionResult
            Return Json(Induction.ProcessPutAways.GetBatchLocationTypeahead(location, warehouse, serial, lot, ccell, cvel, bcell, bvel, cfcell, cfvel, item, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the dedicated containers tables.
        ''' </summary>
        ''' <param name="data">Object that contains the data in order to get records for the table</param>
        ''' <returns>A table object that contains the data that the table will be populated with</returns>
        ''' <remarks></remarks>
        Function GetContainerTables(data As TableObjectSent) As ActionResult
            If IsNothing(data.location) Then data.location = ""
            If IsNothing(data.itemNumber) Then data.itemNumber = ""
            If IsNothing(data.InvMapID) Then data.InvMapID = 0
            Return Json(Induction.ProcessPutAways.GetContainerTables(data.draw, data.top, data.location, data.itemNumber, data.InvMapID,
                                                                     data.start, data.start + data.length, Request.QueryString.Get("order[0][column]"),
                                                                     Request.QueryString.Get("order[0][dir]"), User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints put away labels
        ''' </summary>
        ''' <param name="OTID">Open Transactions ID</param>
        ''' <param name="printDirect">Print directly to printer or preview</param>
        ''' <returns>A boolean telling if the print job executed succcessfully</returns>
        ''' <remarks></remarks>
        Function PrintPutAwayItemLabels(OTID As Integer, printDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim params(,) As String = {{"@OTID", OTID, intVar}}, filename As String = "IMPALabel.lbl", spname As String = "selIMPutItemLabels"
            Try
                Dim rn As String = "Put Away Item Labels"
                If printDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, rn, "Label", filename, spname, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", spname, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ProcessPutAwaysController", "printPutAwayItemLabels", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints bulk transaction list for a batch.
        ''' </summary>
        ''' <param name="batchID">The batch id to print</param>
        ''' <param name="printDirect">Direct or preview</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintOffCarList(batchID As String, printDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim params(,) As String = {{"@BatchID", batchID, strVar}}, filename As String = "IMOCPut.lst", spname As String = "selIMOffCarPutList"
            Try
                Dim rn As String = "Off Carousel Put Away List"
                If printDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, rn, "List", filename, spname, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", spname, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ProcessPutAwaysController", "printOffCarList", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints item or tote label for cross docking
        ''' </summary>
        ''' <param name="tote">The tote to print</param>
        ''' <param name="rpid">The id to print</param>
        ''' <param name="zonelabel">The zone label for the record</param>
        ''' <param name="printDirect">If the print should be sent directly to the printer</param>
        ''' <returns>A bollean telling if the print job completed successfully</returns>
        ''' <remarks></remarks>
        Function printCrossDock(tote As Boolean, rpid As Integer, zonelabel As String, toteId As String, completedQuantity As Integer, printDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim params(,) As String

            If tote Then
                params = {{"@RPID", rpid, intVar}, {"@ZoneLabel", zonelabel, strVar}, {"@ToteID", toteId, strVar}}
            Else
                params = {{"@RPID", rpid, intVar}, {"@ZoneLabel", zonelabel, strVar}}
            End If

            Dim filename As String = IIf(tote, "IMCDTote.lbl", "IMCDItem.lbl")
            Dim spname As String = IIf(tote, "selIMCrossDockToteLabel", "selIMCrossDockItemLabel")

            Dim otr = New OpenTransactionsTempRepo(New Database(GetUserCs(WSID)))
            otr.FetchByID(rpid)
            otr.CompletedQuantity = completedQuantity
            otr.ToteID = toteId
            otr.Save(New Database(GetUserCs(WSID)))

            Try
                Dim rn As String = IIf(tote, "Cross Dock Tote Label", "Cross Dock Item Label")
                If printDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, rn, "Label", filename, spname, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", spname, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ProcessPutAwaysController", "printCrossDock", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        Function CompleteCrossDocks(stringRecords As String, Direct As Boolean) As ActionResult
            Dim username = User.Identity.Name
            Dim WSID = Session("WSID")
            Try
                Dim records = JsonConvert.DeserializeObject(Of List(Of CrossDockComplete))(stringRecords)

                If records.Count = 0 Then
                    Return Json("No records received", JsonRequestBehavior.AllowGet)
                End If

                Dim er = New EmployeesRepo(New Database(GetUserCs(WSID)))
                er.FetchUsername(username)
                Dim lastNameFirst = $"{er.Records(0).LastName},  {er.Records(0).FirstName}"

                Dim ottr = New OpenTransactionsTempRepo(New Database(GetUserCs(WSID)))
                Dim otr = New OpenTransactionsRepo(New Database(GetUserCs(WSID)))
                Dim recordsToRemove As New List(Of OpenTransactionsTempRepo.Record)
                For Each r In records
                    ottr.FetchByID(r.Id)
                    recordsToRemove.Add(ottr.Records(0))

                    Dim rec = New OpenTransactionsRepo.Record(0)
                    otr.Records.Add(rec)
                    rec.ImportDate = ottr.Records(0).ImportDate
                    rec.ImportBy = ottr.Records(0).ImportBy
                    rec.ImportFilename = ottr.Records(0).ImportFilename
                    rec.TransactionType = ottr.Records(0).TransactionType
                    rec.OrderNumber = ottr.Records(0).OrderNumber
                    rec.ItemNumber = ottr.Records(0).ItemNumber
                    rec.TransactionQuantity = ottr.Records(0).TransactionQuantity
                    rec.UnitofMeasure = ottr.Records(0).UnitofMeasure
                    rec.LotNumber = ottr.Records(0).LotNumber
                    rec.ExpirationDate = ottr.Records(0).ExpirationDate
                    rec.SerialNumber = ottr.Records(0).SerialNumber
                    rec.Description = ottr.Records(0).Description
                    rec.Warehouse = ottr.Records(0).Warehouse
                    rec.Notes = ottr.Records(0).Notes
                    rec.StatusCode = ottr.Records(0).StatusCode
                    rec.MasterRecord = ottr.Records(0).MasterRecord
                    rec.UserField1 = ottr.Records(0).UserField1
                    rec.UserField2 = ottr.Records(0).UserField2
                    rec.UserField3 = ottr.Records(0).UserField3
                    rec.UserField4 = ottr.Records(0).UserField4
                    rec.UserField5 = ottr.Records(0).UserField5
                    rec.UserField6 = ottr.Records(0).UserField6
                    rec.UserField7 = ottr.Records(0).UserField7
                    rec.UserField8 = ottr.Records(0).UserField8
                    rec.UserField9 = ottr.Records(0).UserField9
                    rec.UserField10 = ottr.Records(0).UserField10
                    rec.Emergency = ottr.Records(0).Emergency
                    rec.Location = r.Location
                    rec.BatchPickID = r.BatchId
                    rec.ToteID = r.ToteId
                    rec.CompletedQuantity = r.CompletedQuantity
                    rec.CompletedBy = lastNameFirst
                    rec.CompletedDate = Now().ToString("yyyy/MM/dd HH:mm:ss:fff")
                Next

                ottr.Records.Clear()
                ottr.RemoveRecords.Clear()
                ottr.RemoveRecords = recordsToRemove

                Dim uow = New RepositorySD.UnitOfWork(New Database(GetUserCs(Session("WSID"))))
                uow.Add(ottr)
                uow.Add(otr)
                uow.Complete()

                Dim filename = "IMOCPickCrossDock.lst"
                Dim spname = "selOTCrossDockPickListLL"
                Dim rn = "Cross Dock Pick List"
                Dim params(,) As String = {{"@BatchPickId", otr.Records(0).BatchPickID, strVar}}


                If Direct Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username,
                                                                                                   Session("WSID"),
                                                                                                   Server, rn, "List",
                                                                                                   filename, spname,
                                                                                                   params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel =
                            ListLabelHelperFunctions.GetStandardLLExportProperties(username, Session("WSID"),
                                                                                   Server, "preview", spname, params,
                                                                                   filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
                Return Json(True, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ProcessPutAwaysController", "CompleteCrossDocks", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try

        End Function

        Function autoPrintCrossDock(tote As Boolean, otid As Integer, zonelabel As String, printDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim params(,) As String = {{"@OTID", otid, intVar}, {"@ZoneLabel", zonelabel, strVar}}, filename As String = IIf(tote, "IMCDTote.lbl", "IMCDItem.lbl")
            Dim spname As String = IIf(tote, "selIMCrossDockToteLabelAuto", "selIMCrossDockItemLabelAuto")
            Try
                Dim rn As String = IIf(tote, "Cross Dock Tote Label", "Cross Dock Item Label")
                If printDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, rn, "Label", filename, spname, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", spname, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ProcessPutAwaysController", "printCrossDock", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints reel put away labels
        ''' </summary>
        ''' <param name="OTIDs">Range printing with Open Transaction IDs</param>
        ''' <param name="SN">Single print based on serial number</param>
        ''' <param name="Order">Order Number</param>
        ''' <param name="Item">Item Number</param>
        ''' <param name="PrintDirect">Direct or preview</param>
        ''' <returns>A boollean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Function PrintReelLabels(OTIDs As List(Of Integer), SN As String, Order As String, Item As String, PrintDirect As Boolean) As ActionResult
            Dim WSID = Session("WSID"), username As String = User.Identity.Name, spname As String = "selIMReelLabels"
            Dim otList As String = "", filename As String = "IMReelLabel.lbl"
            For Each otid In OTIDs
                otList += (otid & ",")
            Next
            Dim params(,) As String = {{"@OTID", otList, strVar}, {"@SN", SN, strVar}, {"@Item", Item, strVar}, {"@Order", Order, strVar}}
            Try
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Reel Label", "Label", filename, spname, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", spname, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ProcessPutAwaysController", "printReelLabels", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' prints of previews the put contents list for the tote number in the batch id
        ''' </summary>
        ''' <param name="BatchID">The batch id to print</param>
        ''' <param name="ToteNum">The tote number for the batch id</param>
        ''' <param name="PrintDirect">If the print should be sent directly to the printer</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintPrevToteTransViewCont(BatchID As String, ToteNum As Integer, PrintDirect As Boolean) As ActionResult
            Dim username As String = User.Identity.Name, WSID As String = Session("WSID")
            Try
                Dim Parms As String(,) = {{"@BatchID", BatchID, strVar}, {"@ToteNum", ToteNum, intVar}}, sp As String = "selOTToteTransViewContLL"
                Dim filename As String = "IMPutContents.lst"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Item Label", "List", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("ProcessPutAwaysController", "PrintPrevToteTransViewCont", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or previews the put away label for the given id and tote number in the batch id
        ''' </summary>
        ''' <param name="ID">The id to print or preview</param>
        ''' <param name="PrintDirect">If the print job should be sent directly to the printer</param>
        ''' <param name="BatchID">The batch id that cotnaisn the id and tote number</param>
        ''' <param name="ToteNum">The tote number to print for</param>
        ''' <returns>A boolean that tells if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintPrevToteItemLabel(ID As Integer, PrintDirect As Boolean, BatchID As String, ToteNum As Integer) As ActionResult
            Dim username As String = User.Identity.Name, WSID As String = Session("WSID")
            Try
                Dim Parms As String(,) = {{"@ID", ID, intVar}, {"@BatchID", BatchID, strVar}, {"@ToteNum", ToteNum, intVar}}, sp As String = "selOTToteItemLL"
                Dim filename As String = "IMPALabel.lbl"
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Item Label", "Label", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("ProcessPutAwaysController", "PrintPrevToteItemLabel", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or previews the pick or put away tote contents label for the given tote id
        ''' </summary>
        ''' <param name="ToteID">The tote id to print/preview for</param>
        ''' <param name="ZoneLabel">The zone label for the ote id</param>
        ''' <param name="TransType">The transaction tpye for the tote id</param>
        ''' <param name="PrintDirect">IF the print job should be sent directly to the printer</param>
        ''' <param name="ID">Tells where the print command came from</param>
        ''' <param name="BatchID">The batch id that cotnaisn the tote id</param>
        ''' <returns>A boolean that tells if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintPrevToteContentsLabel(ToteID As String, ZoneLabel As String, TransType As String, PrintDirect As Boolean, ID As Integer,
                                                   BatchID As String) As ActionResult
            Dim username As String = User.Identity.Name, WSID As String = Session("WSID")
            Try
                Dim Parms As String(,)
                Dim sp As String
                'If ID = -1, means this function was called from the Setup Queue instead of open transactions
                If ID = -1 Then
                    sp = "selToteSetupToteLabelLL"
                    Parms = {{"@ToteID", If(ToteID = "-1", nullVar, ToteID), strVar}, {"@BatchID", BatchID, strVar}}
                Else
                    sp = "selOTContentsLabToteTranLL"
                    Parms = {{"@ToteID", ToteID, strVar}, {"@ZoneLab", ZoneLabel, strVar}, {"@ID", If(ID = -2, -1, ID), intVar}, {"@BatchID", BatchID, strVar}}
                End If

                Dim filename As String = ""
                If TransType.ToLower() = "pick" Then
                    filename = "IMPickTote.lbl"
                Else
                    filename = "IMPutTote.lbl"
                End If
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Contents Label", "Label", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("CustomReportsHub", "PrintPrevToteContentsLabel", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or previews the tote label for the given tote id or the range of tote ids
        ''' </summary>
        ''' <param name="ToteID">The tote id to print</param>
        ''' <param name="Ident">Tells whether to use the range or a single tote id</param>
        ''' <param name="FromTote">The starting tote id</param>
        ''' <param name="ToTote">The ending tote id</param>
        ''' <param name="PrintDirect">If the print job should be sent directly to the printer</param>
        ''' <param name="BatchID">The batch id that contains the tote id</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintPrevToteManLabel(ToteID As String, Ident As Integer, FromTote As String, ToTote As String, PrintDirect As Boolean, BatchID As String) As ActionResult
            Dim username As String = User.Identity.Name, WSID As String = Session("WSID")
            Try
                Dim sp As String = "selTotesLL", filename As String = "IMToteLabel.lbl"
                Dim Parms As String(,) = {{"@ToteID", ToteID, strVar}, {"@Ident", Ident, intVar}, {"@FromTote", FromTote, strVar}, {"@ToTote", ToTote, strVar}, {"@BatchID", BatchID, strVar}}
                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Tote Manager-Label", "Label", filename, sp, Parms)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, Parms, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("CustomReportsHub", "PrintPrevToteManLabel", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace