' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Threading
Imports Microsoft.AspNet.SignalR
Imports Microsoft.AspNet.SignalR.Hubs
Imports System.Data.SqlClient

Public Class GlobalConfigBroadcaster
    Dim Clients As IHubConnectionContext(Of Object)
    ''' <summary>
    ''' Ensures a singleton instance of the broadcaster
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly _instance As New Lazy(Of GlobalConfigBroadcaster)(Function() New GlobalConfigBroadcaster(GlobalHost.ConnectionManager.GetHubContext(Of GlobalConfigHub)()))

    ''' <summary>
    ''' Finds and sets all clients currently connected to the hub
    ''' </summary>
    ''' <param name="myClients">Current clients connected to the page who should be connected to in the hub</param>
    ''' <remarks></remarks>
    Private Sub New(ByVal myClients As IHubContext)
        Clients = myClients.Clients
    End Sub

    ''' <summary>
    ''' Returns the singleton instance of the broadcaster
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Instance() As GlobalConfigBroadcaster
        Get
            Return _instance.Value
        End Get
    End Property

    ''' <summary>
    ''' Removes a user on the client side when they are logged out, disconnected, etc.
    ''' </summary>
    ''' <param name="wsid"></param>
    ''' <remarks></remarks>
    Public Sub removeConnectedUser(wsid As String)
        Clients.All.removeConnected(wsid)
    End Sub

    ''' <summary>
    ''' Adds a user on the client side when they log in or reconnect
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="wsid"></param>
    ''' <remarks></remarks>
    Public Sub addConnectedUser(name As String, wsid As String)
        Clients.All.addConnected(name, wsid)
    End Sub

End Class
