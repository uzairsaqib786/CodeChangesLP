' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports System.IO
Public Class OMMenuHub
    Inherits Hub
    ''' <summary>
    ''' Gets the order manager menu count data in order to refresh it
    ''' </summary>
    ''' <returns>An object containing the count data for the order manager menu</returns>
    ''' <remarks></remarks>
    Public Function getOMCountData() As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Return OrderManager.Menu.selectOMCountData(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                End Function)
    End Function
End Class
