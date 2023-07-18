' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Namespace Admin
    Public Class HoldTransactionsHub
        Inherits Hub

        ''' <summary>
        ''' Deallocates specified transactions and appends them to the reprocess queue and conditionally executes RT Pick Logic based on System Preference
        ''' </summary>
        ''' <param name="reel">reel, non, or both</param>
        ''' <param name="orderitem">Order Number or Item Number value</param>
        ''' <param name="order">Order Number or Item Number as string</param>
        ''' <param name="reason">Reason for deallocation</param>
        ''' <param name="id">ID of the single transaction in Open Transactions to put on hold or 0</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function deallocateTransactions(reel As String, orderitem As String, order As Boolean, reason As String, id As Integer) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim itemnum As String = IIf(order, "", orderitem), ordernum As String = IIf(order, orderitem, "")
                                                         Try
                                                             RunActionSP("insOTTempHold", Context.QueryString.Get("WSID"), _
                                                                         {{"@Reel", reel, strVar}, {"@ItemNum", itemnum, strVar}, {"@OrderNum", ordernum, strVar}, {"@Reason", reason, strVar}, _
                                                                          {"@User", Context.User.Identity.Name, strVar}, {"@ID", id, intVar}})
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("HoldTransactions", "deallocateTransactions", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function

    End Class
End Namespace

