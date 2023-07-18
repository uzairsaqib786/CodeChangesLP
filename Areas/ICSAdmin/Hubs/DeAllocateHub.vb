' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks

Namespace Admin
    Public Class DeAllocateHub
        Inherits Hub

        ''' <summary>
        ''' Gets all allocated orders within the item and order number filters
        ''' </summary>
        ''' <param name="orderNum">The order number that is going to be displayed</param>
        ''' <param name="itemNum">The item number that is going to be displayed</param>
        ''' <returns>A list of string containing the orders that satisfy the current filter filter settings</returns>
        ''' <remarks></remarks>
        Public Function getAllOrders(orderNum As String, itemNum As String, transType As String) As Task(Of List(Of String))
            Return Task.Factory.StartNew(Function() As List(Of String)
                                             Return DeAllocateOrders.getAllAllocatedOrders(orderNum, itemNum, transType, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Function)
        End Function

        ''' <summary>
        ''' Deallocates specified orders or all orders if orderNum = "ALL"
        ''' </summary>
        ''' <param name="orderNums">Order number(s) to deallocate</param>
        ''' <returns>Nohing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deallocate(orderNums As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updOTDeallo", Context.QueryString.Get("WSID"), _
                                                             {{"@OrderNumbers", orderNums, strVar}, {"@User", Context.User.Identity.Name, strVar}, _
                                                              {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("DeallocateHub", "deallocate", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function
    End Class
End Namespace

