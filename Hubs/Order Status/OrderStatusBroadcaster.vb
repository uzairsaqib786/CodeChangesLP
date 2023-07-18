' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Threading
Imports Microsoft.AspNet.SignalR
Imports Microsoft.AspNet.SignalR.Hubs


Public Class OrderStatusBroadcaster
    Dim Clients As IHubContext
    ''' <summary>
    ''' Ensures a singleton instance of the broadcaster
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly _instance As New Lazy(Of OrderStatusBroadcaster)(Function() New OrderStatusBroadcaster(GlobalHost.ConnectionManager.GetHubContext(Of OrderStatusHub)()))

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
    Public Shared ReadOnly Property Instance() As OrderStatusBroadcaster
        Get
            Return _instance.Value
        End Get
    End Property

    ''' <summary>
    ''' Calls client side update function to update the Order Status view's details without refreshing the page.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="currentUser"></param>
    ''' <remarks></remarks>
    Public Sub Update(ByVal data As OrderStatInfo, currentUser As String)
        Clients.Clients.Group(currentUser).dispOrderStat(data)
    End Sub
End Class
