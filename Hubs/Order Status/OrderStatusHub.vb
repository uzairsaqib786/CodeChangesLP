' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class OrderStatusHub
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
    ''' If Order Number is scanned in, check for multiple dates
    ''' </summary>
    ''' <param name="ordernum"></param>
    ''' <returns>List of string that contains order numbers if they have multiple dates attached with them</returns>
    ''' <remarks></remarks>
    Public Function ScanValidateOrder(ordernum As String) As Task(Of List(Of String))
        Return Task.Factory.StartNew(Function()
                                         Dim DataReader As SqlDataReader = Nothing
                                         Dim returnDates As New List(Of String)
                                         Try
                                             DataReader = RunSPArray("selOrderStatusScan", Context.QueryString.Get("WSID"), {{"@OrderNumber", ordernum, strVar}})
                                             If DataReader.HasRows Then
                                                 While DataReader.Read
                                                     returnDates.Add(IIf(IsDBNull(DataReader("Completed Date")), "Entire Order", DataReader("Completed Date")))
                                                 End While
                                             End If
                                         Catch ex As Exception
                                             Debug.WriteLine("ScanValidateOrder error: " & ex.Message)
                                             insertErrorMessages("OrderStatusHub", "ScanValidateOrder", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         Finally
                                             If Not DataReader Is Nothing Then
                                                 DataReader.Close()
                                             End If
                                         End Try
                                         Return returnDates
                                     End Function)
    End Function

    ''' <summary>
    ''' Deletes an order by order number from Open Transactions, Transaction History and reprocess (Open Transactions Temp)
    ''' </summary>
    ''' <param name="ordernum">Order Number to be deleted.</param>
    ''' <param name="totalLines">Total number of entries that were in the deleted order.</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function DeleteOrder(ordernum As String, totalLines As Integer) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             RunActionSP("DeleteOrdersByNumber", Context.QueryString.Get("WSID"), {{"@Ordernum", ordernum, strVar}, {"@xferBy", Context.User.Identity.Name, strVar}, {"@totalLines", totalLines, intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.Message)
                                             insertErrorMessages("OrderStatusHub", "DeleteOrder", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
    End Function
    ''' <summary>
    ''' Selects the order number if the order number is shipping complete
    ''' </summary>
    ''' <param name="OrderNum"></param>
    ''' <returns>The order number if it is shipping complete</returns>
    ''' <remarks></remarks>
    Public Function selShipComp(OrderNum As String) As String
        Dim DataReader As SqlDataReader = Nothing
        Dim retVal As String = ""

        Try
            DataReader = RunSPArray("selConfPackCheckShipComp", Context.QueryString.Get("WSID"), {{"@OrderNumber", OrderNum, strVar}})

            If DataReader.HasRows Then
                DataReader.Read()
                retVal = CheckDBNull(DataReader(0))
            End If
        Catch ex As Exception
            insertErrorMessages("ConfirmAndPacking", "selConfPackShipComp", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Close()
            End If
        End Try
        Return retVal
    End Function
    ''' <summary>
    ''' Updates the priority for the change priority button
    ''' </summary>
    ''' <param name="OrderNum"></param>
    ''' <param name="Priority"></param> the new priority
    ''' <returns>A string telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updOSPriority(OrderNum As String, Priority As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updOSPriority", Context.QueryString.Get("WSID"), {{"@OrderNum", OrderNum, strVar}, {"@User", Context.User.Identity.Name, strVar},
                                                                                                                       {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Priority", Priority, intVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderStatusHub", "updOSPriority", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Error"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

End Class