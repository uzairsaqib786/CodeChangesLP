' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Namespace Admin
    Public Class EmployeeStatsHub
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
        ''' Gets all the employees in the proper format 
        ''' </summary>
        ''' <returns> a list of list of string containg the username, last name, and first name for an employee</returns>
        ''' <remarks></remarks>
        Public Function getEmployees() As Task(Of List(Of List(Of String)))
            Return Task.Factory.StartNew(Function() As List(Of List(Of String))
                                             Dim DataReader As SqlDataReader = Nothing, ReturnVals As New List(Of List(Of String))
                                             Try
                                                 DataReader = RunSPArray("EmployeeStatsSelUsers", Context.QueryString.Get("WSID"), {{"nothing"}})
                                                 If DataReader.HasRows Then
                                                     While DataReader.Read()
                                                         Dim data As New List(Of String)
                                                         For x As Integer = 0 To DataReader.FieldCount - 1
                                                             If x = 0 Then
                                                                 data.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                                                             ElseIf x = 1 Then
                                                                 If Not IsDBNull(DataReader(x)) Then
                                                                     If Not IsDBNull(DataReader(x + 1)) Then
                                                                         data.Add(DataReader(x) & ", " & DataReader(x + 1))
                                                                     Else
                                                                         data.Add(DataReader(x))
                                                                     End If
                                                                 Else
                                                                     data.Add(IIf(IsDBNull(DataReader(x + 1)), "", DataReader(x + 1)))
                                                                 End If
                                                             End If
                                                         Next
                                                         ReturnVals.Add(data)
                                                     End While
                                                 End If

                                             Catch ex As Exception
                                                 Debug.WriteLine(ex)
                                                 insertErrorMessages("EmployeesStatsHub", "getEmployees", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(DataReader) Then
                                                     DataReader.Dispose()
                                                 End If
                                             End Try
                                             Return ReturnVals
                                         End Function)
        End Function

    End Class

End Namespace
