' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class CompPickBatchHub
    Inherits Hub

    Public Function ShortTrans(OTID As Integer, ShortQty As Integer, ShortMethod As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         Dim Params As String(,) = {{"@OTID", OTID, intVar}, {"@ShortQty", ShortQty, intVar}, {"@User", Context.User.Identity.Name, strVar}}
                                                         Dim SP As String = ""

                                                         Select Case ShortMethod
                                                             Case "Complete"
                                                                 SP = "updOTCompShortTrans"
                                                             Case "Markout"
                                                                 SP = "updOTMarkoutShortPick"
                                                             Case "Deallocate"
                                                                 SP = "updOTSplitDealloShortTrans"
                                                         End Select

                                                         RunActionSP(SP, Context.QueryString.Get("WSID"), Params)
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("CompBatchPickHub", "ShortTrans", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    Public Function CompleteTrans(OTID As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("updOTCompTrans", Context.QueryString.Get("WSID"), {{"@OTID", OTID, intVar}, {"@User", Context.User.Identity.Name, strVar}})
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("CompBatchPickHub", "CompleteTrans", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    Public Function ValidateToteID(NewTote As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim DataReader As SqlDataReader = Nothing

                                                     Try
                                                         DataReader = RunSPArray("selOTValidTote", Context.QueryString.Get("WSID"), {{"@ToteID", NewTote, strVar}})

                                                         If DataReader.HasRows Then
                                                             Return False
                                                         End If

                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("CompBatchPickHub", "ValidateToteID", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     Finally
                                                         If Not IsNothing(DataReader) Then
                                                             DataReader.Dispose()
                                                         End If
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function


    Public Function BlossomTote(BlossData As List(Of List(Of Integer)), NewTote As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         'OTID => (ID, TransQty, OldToteQty)
                                                         For Each OTID In BlossData
                                                             'If qty in old tote = trans qty complete it
                                                             If OTID(1) = OTID(2) Then
                                                                 RunActionSP("updOTCompTrans", Context.QueryString.Get("WSID"), {{"@OTID", OTID(0), intVar},
                                                                                                                                {"@User", Context.User.Identity.Name, strVar}})
                                                             Else
                                                                 'Blossom trans
                                                                 RunActionSP("updOTBlossomTote", Context.QueryString.Get("WSID"), {{"@OTID", OTID(0), intVar},
                                                                             {"@BlossQty", OTID(2), intVar}, {"@NewTote", NewTote, strVar}, {"@User", Context.User.Identity.Name, strVar}})
                                                             End If
                                                         Next


                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("CompBatchPickHub", "BlossomTote", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function


    Public Function CompleteBatch(BatchID As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("updOTCompBatch", Context.QueryString.Get("WSID"), {{"@BatchID", BatchID, strVar}, {"@User", Context.User.Identity.Name, strVar}})

                                                     Catch ex As Exception
                                                         Debug.Print(ex.ToString())
                                                         insertErrorMessages("CompBatchPickHub", "CompleteBatch", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

End Class
