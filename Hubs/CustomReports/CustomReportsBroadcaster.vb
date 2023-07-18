' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Threading
Imports Microsoft.AspNet.SignalR
Imports Microsoft.AspNet.SignalR.Hubs
Imports System.Data.SqlClient

Public Class CustomReportsBroadcaster
    Dim Clients As IHubConnectionContext(Of Object)
    ''' <summary>
    ''' Ensures a singleton instance of the broadcaster
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly _instance As New Lazy(Of CustomReportsBroadcaster)(Function() New CustomReportsBroadcaster(GlobalHost.ConnectionManager.GetHubContext(Of CustomReportsHub)()))

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
    Public Shared ReadOnly Property Instance() As CustomReportsBroadcaster
        Get
            Return _instance.Value
        End Get
    End Property

    ''' <summary>
    ''' Prints the report
    ''' </summary>
    ''' <param name="LL">llreportmodel instance to print from</param>
    ''' <remarks></remarks>
    Public Sub Print(LL As LLReportModel)
        Clients.Group(PrintService.PrintServiceHubName).PrintReport(LL)
    End Sub

    ''' <summary>
    ''' Exports the report
    ''' </summary>
    ''' <param name="m"></param>
    Sub Export(m As ExportServiceModel)
        Clients.Group(PrintService.PrintServiceHubName).ExportReport(m)
    End Sub

End Class
