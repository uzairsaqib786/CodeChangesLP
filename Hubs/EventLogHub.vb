' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Data.SqlClient
Imports System.Threading.Tasks
    Public Class EventLogHub
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
        ''' Deletes a range of entries in the Event Log table based on parameters.
        ''' </summary>
        ''' <param name="beginDate">Start date to delete from.</param>
        ''' <param name="endDate">End date to stop deleting at.</param>
        ''' <param name="message">Message to filter by during the delete.</param>
        ''' <param name="eLocation">Event Location to filter by during the delete.</param>
        ''' <param name="nStamp">User or Name Stamp to filter by during the delete.</param>
        ''' <returns>Task to lessen effects of timeouts.</returns>
        ''' <remarks></remarks>
    Public Function deleteRange(beginDate As DateTime, endDate As DateTime, message As String, eLocation As String, nStamp As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function()
                Try
                    RunActionSP("delEventLogDate", Context.QueryString.Get("WSID"), {{"@BeginDate", beginDate, dteVar}, {"@EndDate", endDate, dteVar}, _
                                             {"@message", message, strVar}, _
                                             {"@eLocation", eLocation, strVar}, _
                                             {"@nStamp", nStamp, strVar}})
                Catch ex As Exception
                    Debug.WriteLine(ex.Message)
                    insertErrorMessages("Event Log Manager", "deleteRange", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    Return False
                End Try
                Return True
            End Function)
    End Function

        ''' <summary>
        ''' Deletes the selected entry in the Event Log based on Event ID.
        ''' </summary>
        ''' <param name="eID">Event ID to delete.</param>
        ''' <returns>Task to avoid timeout issues.</returns>
        ''' <remarks></remarks>
        Public Function deleteSelected(eID As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delEventLogEID", Context.QueryString.Get("WSID"), {{"@eID", eID, intVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("Event Log Manager", "deleteSelected", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Selects and returns all data related entry's Message, Notes and Event Location for use in the modal.
        ''' </summary>
        ''' <param name="eID">Event ID to filter by.</param>
        ''' <returns>Task with Message, Notes and Event Location in a list of string.</returns>
        ''' <remarks>Table which launches the modal on the JS side removes characters after a certain length to keep table size to a minimum.  This selects any data that may have been removed in order to replace it while in the modal.</remarks>
        Public Function modalInfo(eID As Integer) As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                                 Dim DataReader As SqlDataReader = Nothing, results As New List(Of String)
                                                                 Try
                                                                     DataReader = RunSPArray("selEventLogMessNotesLoc", Context.QueryString.Get("WSID"), {{"@eID", eID, intVar}})

                                                                     If DataReader.HasRows Then
                                                                         If DataReader.Read() Then
                                                                             results.AddRange({IIf(IsDBNull(DataReader(0)), "", DataReader(0)), _
                                                                                               IIf(IsDBNull(DataReader(1)), "", DataReader(1)), _
                                                                                               IIf(IsDBNull(DataReader(2)), "", DataReader(2))})
                                                                         End If
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("Event Log", "modalInfo", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(DataReader) Then
                                                                         DataReader.Dispose()
                                                                     End If
                                                                 End Try
                                                                 Return results
                                                             End Function)
        End Function


    End Class
