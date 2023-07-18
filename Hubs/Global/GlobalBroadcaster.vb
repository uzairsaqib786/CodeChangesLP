' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.Threading
Imports Microsoft.AspNet.SignalR
Imports Microsoft.AspNet.SignalR.Hubs
Imports System.Data.SqlClient

Public Class GlobalBroadcaster
    Dim Clients As IHubContext
    ''' <summary>
    ''' Ensures a singleton instance of the broadcaster
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly _instance As New Lazy(Of GlobalBroadcaster)(Function() New GlobalBroadcaster(GlobalHost.ConnectionManager.GetHubContext(Of GlobalHub)()))

    ''' <summary>
    ''' Finds and sets all clients currently connected to the hub
    ''' </summary>
    ''' <param name="myClients">Current clients connected to the page who should be connected to in the hub</param>
    ''' <remarks></remarks>
    Private Sub New(ByVal myClients As IHubContext)
        Clients = myClients
    End Sub

    ''' <summary>
    ''' Returns the singleton instance of the broadcaster
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Instance() As GlobalBroadcaster
        Get
            Return _instance.Value
        End Get
    End Property

    ''' <summary>
    ''' Alerts certain groups of users that there are emergency transactions
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub alertEmergencyOrders(data As Dictionary(Of String, List(Of Object)))
        Dim keys As List(Of String) = data.Keys.ToList()
        For Each key In keys
            Clients.Clients.Group(key).alertEmergencyOrders(data(key))
        Next
    End Sub


End Class
