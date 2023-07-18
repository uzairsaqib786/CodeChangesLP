' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class PalletReceivingHub
    Inherits Hub

    Public Function validateTote(ToteID As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Dim ValidTote As Boolean = True
                                                     Dim DataReader As SqlDataReader = Nothing

                                                     Try
                                                         DataReader = RunSPArray("selOTValidTote", Context.QueryString.Get("WSID"), {{"@ToteID", ToteID, strVar}})

                                                         If DataReader.HasRows Then
                                                             ValidTote = False
                                                         End If

                                                     Catch ex As Exception
                                                         ValidTote = False
                                                         insertErrorMessages("PalletReceivingHub", "validateTote", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     Finally
                                                         If Not IsNothing(DataReader) Then
                                                             DataReader.Dispose()
                                                         End If
                                                     End Try

                                                     Return ValidTote
                                                 End Function)
    End Function

    Public Function validateItem(Item As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Dim ValidItem As Boolean = False
                                                     Dim DataReader As SqlDataReader = Nothing

                                                     Try
                                                         DataReader = RunSPArray("selInvValidItem", Context.QueryString.Get("WSID"), {{"@ItemNum", Item, strVar}})

                                                         If DataReader.HasRows Then
                                                             ValidItem = True
                                                         End If

                                                     Catch ex As Exception
                                                         ValidItem = False
                                                         insertErrorMessages("PalletReceivingHub", "validateTote", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     Finally
                                                         If Not IsNothing(DataReader) Then
                                                             DataReader.Dispose()
                                                         End If
                                                     End Try

                                                     Return ValidItem
                                                 End Function)
    End Function

    Public Function processPallet(ToteID As String, ItemNum As String, Quant As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()

                                                     Try
                                                         RunActionSP("insIMOTPutAway", Context.QueryString.Get("WSID"), {{"@ToteID", ToteID, strVar}, {"@ItemNum", ItemNum, strVar},
                                                                                                                         {"@Quant", Quant, intVar}, {"@User", Context.User.Identity.Name, strVar}})


                                                     Catch ex As Exception
                                                         insertErrorMessages("PalletReceivingHub", "validateTote", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function


End Class
