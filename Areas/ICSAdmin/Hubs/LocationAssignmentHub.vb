' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web.Admin
Public Class LocationAssignmentHub
    Inherits Hub

    ''' <summary>
    ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
    ''' </summary>
    ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
    ''' <remarks></remarks>
    Public Overrides Function OnConnected() As Task

        'Adds a user to their own unique group by a value passed in during connection
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
        'Calls the Original Onconnected Function pass control back to the server
        Return MyBase.OnConnected()
    End Function
    ''' <summary>
    ''' Gets the count transaction type table data
    ''' </summary>
    ''' <returns>list of list of string with the table data</returns>
    ''' <remarks></remarks>
    Public Function getCountTableData() As Task(Of List(Of List(Of String)))
        Return Task(Of List(Of List(Of String))).Factory.StartNew(Function() As List(Of List(Of String))
                                                                      Return LocationAssignment.getLocAssCountTable(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                  End Function)
    End Function
    ''' <summary>
    ''' Gets the pick transaction type table data
    ''' </summary>
    ''' <returns>list of list of string with the table data</returns>
    ''' <remarks></remarks>
    Public Function getPickTableData(orderNumberSearch As String) As Task(Of List(Of List(Of String)))
        Return Task(Of List(Of List(Of String))).Factory.StartNew(Function() As List(Of List(Of String))
                                                                      Return LocationAssignment.getLocAssPickTable(Context.User.Identity.Name, Context.QueryString.Get("WSID"), orderNumberSearch)
                                                                  End Function)

    End Function
    ''' <summary>
    ''' Gets the put away transaction type table data
    ''' </summary>
    ''' <returns>list of list of string with the table data</returns>
    ''' <remarks></remarks>
    Public Function getPutAwayTableData() As Task(Of List(Of List(Of String)))
        Return Task(Of List(Of List(Of String))).Factory.StartNew(Function() As List(Of List(Of String))
                                                                      Return LocationAssignment.getLocAssPutAwayTable(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                  End Function)
    End Function
    ''' <summary>
    ''' gets the count information for each transaction type
    ''' </summary>
    ''' <returns>object containing the count data</returns>
    ''' <remarks></remarks>
    Public Function getLocAssTransTypeCount() As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Return LocationAssignment.getLocAssCountData(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Runs the desired location asssignment function depending on the transaction type
    ''' </summary>
    ''' <param name="TransType">The transaction type of the orders</param>
    ''' <param name="Orders">The orders to insert for prcoessing</param>
    ''' <returns>a boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function InsertOrdersForLocAss(TransType As String, Orders As List(Of String)) As Task(Of Boolean)
        'This is not implemented
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim SPs As New List(Of SQLCommandInfo)

                                                     Try
                                                         'Convert trasn type to database representation
                                                         Select Case TransType
                                                             Case "pick"
                                                                 TransType = "Pick"
                                                             Case "putaway"
                                                                 TransType = "Put Away"
                                                             Case "count"
                                                                 TransType = "Count"
                                                         End Select

                                                         For Each Ord In Orders
                                                             SPs.Add(New SQLCommandInfo With {.SP = "insOrdersForLocAss", .Params = {{"@OrderNum", Ord, strVar}, {"@TransType", TransType, strVar}}})
                                                         Next

                                                         RunActionSPMulti(SPs, Context.QueryString.Get("WSID"))
                                                         Return True
                                                     Catch ex As Exception
                                                         insertErrorMessages("LocationAssignmentHub", "InsertOrdersForLocAss", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return False
                                                 End Function)
    End Function

End Class
