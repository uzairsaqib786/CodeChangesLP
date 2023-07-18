﻿' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2021

Imports System.Data.SqlClient
Imports CommonSD
Imports RepositorySD

Public Module MarkoutFunctions
    Dim ConnString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ToString

    Private Function GetLCFFromUser(wsid As String, user As String) As String
        Dim lastcommafirst = ""
        Try
            Dim emr = New EmployeesRepo(New Database(GetUserCs(wsid)))
            emr.FetchUsername(user)
            With emr.Records(0)
                lastcommafirst = .LastName + "," + .FirstName
            End With


        Catch ex As Exception
            insertErrorMessages("Markout", "GetLCFFromUser", ex.ToString(), "user", wsid)
        End Try
        Return lastcommafirst

    End Function


    Public Function SelectParamByName(wsid As String, ParamName As String)
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("ST_selParamsByParamName", wsid, {{"@ParamName", ParamName, strVar}})
            If DataReader IsNot Nothing AndAlso DataReader.HasRows Then
                DataReader.Read()
                Return DataReader("ParamValue").ToString
            End If

        Catch ex As Exception
            insertErrorMessages("Markout", "SelectParamByName", ex.ToString(), "user", wsid)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try

    End Function

    Public Function GetToteData(wsid As String, ToteID As String, user As String) As ToteDataModel
        Dim toteData As New ToteDataModel
        Try
            'Dim DisplayAltLoc As Boolean = ConfigurationManager.AppSettings("DisplayAltLocation")
            Dim displayAltLoc As Boolean = False
            Dim toteDataList As New List(Of Object)
            Dim otr = New OpenTransactionsRepo(New Database(GetUserCs(wsid)))
            Dim thr = New TransactionHistoryRepo(New Database(GetUserCs(wsid)))
            Dim destinedByHost = False
            otr.FetchByTote(ToteID)
            thr.FetchToteIDAndHours(ToteID, 72)
            Dim orderNumber As String
            If otr.Records.Count > 0 Then
                orderNumber = otr.Records.First.OrderNumber
            Else
                If thr.Records.Count = 0 Then
                    Return toteData
                End If
                orderNumber = thr.Records.OrderByDescending(Function(x) x.CompletedDate).First.OrderNumber
            End If

            otr.FetchOrderNumberAndTransType(orderNumber, "Pick")
            thr.FetchOrderNumberAndTransType(orderNumber, "Pick")
            otr.Records.OrderByDescending(Function(x) x.ToteID)
            thr.Records.OrderByDescending(Function(x) x.ToteID)
            Dim shortQuant As Integer
            For Each r In otr.Records
                shortQuant = r.TransactionQuantity - r.CompletedQuantity
                toteDataList.Add(New With {r.ID, r.ToteID,
                                 .Item = r.ItemNumber, .Loc = If(displayAltLoc, r.UserField1, r.Location),
                                 .TransQty = r.TransactionQuantity, .CompQty = r.CompletedQuantity,
                                 .ShortQty = shortQuant,
                                 .Status = GetLineStatus(wsid, shortQuant, r.CompletedBy.Equals(""), r.CompletedDate.Equals(""), user),
                                 .OrderNum = r.OrderNumber,
                                 r.CompletedDate})
                If (r.Revision = "Markout") Then
                    destinedByHost = True
                End If

            Next
            For Each r In thr.Records
                shortQuant = r.TransactionQuantity - r.CompletedQuantity
                toteDataList.Add(New With {r.ID, .ToteId = r.ToteID,
                                 .Item = r.ItemNumber, .Loc = If(displayAltLoc, r.UserField1, r.Location),
                                 .TransQty = r.TransactionQuantity, .CompQty = r.CompletedQuantity,
                                 .ShortQty = shortQuant,
                                 .Status = GetLineStatus(wsid, shortQuant, r.CompletedBy.Equals(""), r.CompletedDate.Equals(""), user),
                                 .OrderNum = r.OrderNumber,
                                 r.CompletedDate})
                If (r.Revision = "Markout") Then
                    destinedByHost = True
                End If

            Next

            toteData.Data = toteDataList.OrderBy(Function(x) x.CompletedDate).OrderByDescending(Function(x) x.ShortQty).OrderBy(Function(x) x.ToteId).ToList
            toteData.ToteStatus = GetToteStatus(wsid, ToteID, user)
            If destinedByHost Then
                toteData.MarkoutStatus = "Assigned to Markout"
            End If
            Dim TempTote = toteData.Data.First.ToteId
            toteData.BlossomCount = 0
            For Each t In toteData.Data
                If t.ToteId <> TempTote Then
                    TempTote = t.ToteId
                    toteData.BlossomCount += 1
                End If
            Next
        Catch ex As Exception
            insertErrorMessages("Markout", "GetToteData", ex.ToString(), "user", wsid)
        End Try

        Return toteData
    End Function

    Private Function GetLineStatus(wsid As String, ShortQuant As Integer, CompByNull As Boolean, CompDateNull As Boolean, user As String) As String
        Dim Status As String = ""

        Try

            If ShortQuant = 0 Then
                Status = "Complete"
            ElseIf CompByNull Then
                Status = "Missed"
            ElseIf CompDateNull Then
                Status = "Short"
            Else
                Status = "Ship Short"
            End If

        Catch ex As Exception
            insertErrorMessages("Markout", "GetLineStatus", ex.ToString(), "user", wsid)
        End Try

        Return Status
    End Function

    Private Function GetToteStatus(wsid As String, ToteID As String, user As String) As String
        Try
            Dim otr = New OpenTransactionsRepo(New Database(GetUserCs(wsid)))
            otr.FetchByTote(ToteID)
            For Each record In otr.Records
                If record.CompletedDate.Equals("") Then
                    Return "Analyze"
                End If
            Next
            Return "Complete"

        Catch ex As Exception
            insertErrorMessages("Markout", "GetToteStatus", ex.ToString(), "user", wsid)
        End Try
        Return "" ' Is this needed? Should this be in the catch instead?

    End Function

    Public Function UpdateQuantity(wsid As String, OTID As Integer, Quant As Integer, user As String) As Boolean

        Try
            Dim uow = New UnitOfWork(ConnString)
            Dim LastCommaFirst = GetLCFFromUser(wsid, user)
            Dim otr = New OpenTransactionsRepo(New Database(GetUserCs(wsid)))
            otr.Fetch(OTID)

            For Each record In otr.Records
                record.CompletedQuantity = Quant
                record.CompletedBy = LastCommaFirst
                If record.TransactionQuantity = Quant Then
                    record.CompletedDate = Now.ToString.ToDateString
                End If
            Next

            uow.Add(otr)
            uow.Complete()
        Catch ex As Exception
            insertErrorMessages("Markout", "UpdateQuantity", ex.ToString(), "user", wsid)
            Return False
        End Try
        Return True
    End Function

    Public Function CompleteTransaction(wsid As String, OTID As Integer, user As String) As Boolean
        Try
            Dim uow = New UnitOfWork(ConnString)
            Dim LastCommaFirst = GetLCFFromUser(wsid, user)
            Dim otr = New OpenTransactionsRepo(New Database(GetUserCs(wsid)))
            otr.Fetch(OTID)

            For Each record In otr.Records
                With record
                    .CompletedQuantity = .TransactionQuantity
                    .CompletedBy = LastCommaFirst
                    .CompletedDate = Now.ToString.ToDateString
                End With
            Next
            uow.Add(otr)
            uow.Complete()
        Catch ex As Exception
            insertErrorMessages("Markout", "CompleteTransaction", ex.ToString(), "user", wsid)
            Return False
        End Try
        Return True
    End Function

    Public Function ShipShortTransaction(wsid As String, OTID As Integer, user As String) As Boolean
        Try
            Dim uow = New UnitOfWork(ConnString)
            Dim LastCommaFirst = GetLCFFromUser(wsid, user)
            Dim otr = New OpenTransactionsRepo(New Database(GetUserCs(wsid)))
            otr.Fetch(OTID)

            For Each record In otr.Records
                record.CompletedBy = LastCommaFirst
                record.CompletedDate = Now.ToString.ToDateString
            Next
            uow.Add(otr)
            uow.Complete()
        Catch ex As Exception
            insertErrorMessages("Markout", "ShipShortTransaction", ex.ToString(), "user", wsid)
            Return False
        End Try

        Return True
    End Function

    Public Function ValidateTote(wsid As String, ToteID As String) As Boolean
        Try
            Dim otr = New OpenTransactionsRepo(New Database(GetUserCs(wsid)))
            otr.FetchByTote(ToteID)
            For Each record In otr.Records
                If record.CompletedDate.Equals("") Then
                    Return False
                End If
            Next
            Return True

        Catch ex As Exception
            insertErrorMessages("Markout", "ValidateTote", ex.ToString(), "user", wsid)
            Return False
        End Try
    End Function

    Private Function CompleteRecord(_Record As OpenTransactionsRepo.Record, CompBy As String, CompQuant As Integer) As OpenTransactionsRepo.Record
        With _Record
            .CompletedDate = Now.ToString.ToDateString
            .CompletedBy = CompBy
            .CompletedQuantity = CompQuant
        End With
        Return _Record
    End Function

    Public Function BlossomTote(wsid As String, OTIDs As List(Of List(Of Integer)), NewTote As String, User As String, IsBlossComp As Boolean) As Boolean

        Try
            Dim uow = New UnitOfWork(ConnString)
            Dim LastCommaFirst = GetLCFFromUser(wsid, User)
            Dim otr = New OpenTransactionsRepo(New Database(GetUserCs(wsid)))
            Dim TopRecord As OpenTransactionsRepo.Record
            Dim NewRecord As OpenTransactionsRepo.Record
            Dim InsertedRecords As New List(Of OpenTransactionsRepo.Record)
            Dim BlossQty As Integer
            Dim TransMinusBlossQty As Integer

            For Each OTID In OTIDs
                otr.Fetch(OTID(0))

                TopRecord = otr.Records.First
                NewRecord = otr.Clone(TopRecord)

                BlossQty = OTID(1)                                              'blossom complete: New Tote Qty | blossom: Old Tote Qty
                TransMinusBlossQty = TopRecord.TransactionQuantity - BlossQty   'blossom complete: Old Tote Qty | blossom: New Tote Qty

                For Each OrigRecord In otr.Records
                    If IsBlossComp Then
                        'Complete new tote
                        'Update original tote to have remaining
                        If TransMinusBlossQty = 0 Then 'None go in the original tote
                            With OrigRecord 'Assign transaction to new tote
                                .ToteID = NewTote
                                .StatusCode = "Split to Old"
                            End With
                            OrigRecord = CompleteRecord(OrigRecord, LastCommaFirst, BlossQty) 'Complete the altered transaction
                        Else 'Transaction is split up
                            OrigRecord.TransactionQuantity = TransMinusBlossQty
                            With NewRecord 'Create new tote
                                .ToteID = NewTote
                                .TransactionQuantity = BlossQty
                                .StatusCode = "Split to Old"
                            End With
                            NewRecord = CompleteRecord(NewRecord, LastCommaFirst, BlossQty) 'Complete the new transaction
                            InsertedRecords.Add(NewRecord)
                        End If
                    Else
                        'Complete original tote
                        'Assign remaining to new tote
                        If BlossQty = 0 Then 'None go in original tote, no need to complete it
                            OrigRecord.ToteID = NewTote 'Assign transaction to new tote
                        Else 'Transaction is split up
                            OrigRecord.TransactionQuantity = BlossQty
                            OrigRecord = CompleteRecord(OrigRecord, LastCommaFirst, BlossQty) 'Complete original transaction
                            If TransMinusBlossQty <> 0 Then
                                With NewRecord 'Create new transaction
                                    .ToteID = NewTote
                                    .TransactionQuantity = TransMinusBlossQty
                                End With
                                InsertedRecords.Add(NewRecord)
                            End If
                        End If
                    End If
                Next
                otr.Records.AddRange(InsertedRecords)
                uow.Add(otr)
                uow.Complete()
            Next

        Catch ex As Exception
            insertErrorMessages("Markout", "BlossomTote", ex.ToString(), User, wsid)
            Return False
        End Try

        Return True
    End Function
End Module
