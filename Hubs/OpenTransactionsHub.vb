' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports combit.ListLabel21

Public Class OpenTransactionsHub
    Inherits Hub

    ''' <summary>
    ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function OnConnected() As Task
        'Adds a user to their own unique group by a value passed in during connection
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
        'Calls the Original Onconnected Function pass control back to the server
        Return MyBase.OnConnected()
    End Function

    ''' <summary>
    ''' Deletes order(s) from Open Transactions based on ID.
    ''' </summary>
    ''' <param name="transType">Transaction Type to be deleted if ID = -1</param>
    ''' <param name="orderNumber">Order Number to delete if ID != -1</param>
    ''' <param name="ID">Identifier to determine whether an individual order should be deleted or if all transactions with the same type should be deleted (ID = - 1)</param>
    ''' <param name="itemNumber">Event logging parameter Item Number in deleted order.</param>
    ''' <param name="lineNumber">Event logging parameter Line Number in deleted order.</param>
    ''' <returns>Task to prevent timeouts.</returns>
    ''' <remarks></remarks>
    Public Function deleteOrder(transType As String, orderNumber As String, ID As Integer, itemNumber As String, lineNumber As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             RunActionSP("delOTOrder", Context.QueryString.Get("WSID"), {{"@transType", transType, strVar}, _
                                                                        {"@orderNumber", orderNumber, strVar}, _
                                                                        {"@ID", ID, intVar}, _
                                                                        {"@xferBy", Context.User.Identity.Name, strVar}, _
                                                                        {"@itemNumber", itemNumber, strVar}, _
                                                                        {"@lineNumber", lineNumber, strVar}, _
                                                                        {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine("ERROR : " & ex.Message)
                                             insertErrorMessages("Open Transactions", "deleteOrder", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Sends any completed transactions in Open Transactions to Transaction History (completed date != null)
    ''' </summary>
    ''' <returns>Task to prevent timeouts.</returns>
    ''' <remarks></remarks>
    Public Function sendCompletedToTH() As Task
        Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                     Dim DataReader As SqlDataReader

                                                     Try
                                                         ' incomplete:  picktotote, car/offcar manifest need to be set in preferences which hasn't been created yet.
                                                         ' completed of type 'pick' may not be sent to history yet
                                                         DataReader = RunSPArray("insTHFromOT", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                               {"@xferBy", Context.User.Identity.Name, strVar}})
                                                         If DataReader.HasRows Then
                                                             While DataReader.Read()
                                                                 Return DataReader(0)
                                                             End While
                                                         End If
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.Message)
                                                         insertErrorMessages("Open Transactions", "sendCompletedToTH", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try

                                                     Return 0
                                                 End Function)
    End Function

    
End Class