' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Data.SqlClient
Imports System.Threading.Tasks

<Authorize()> _
<Hubs.HubName("columnSequence")>
Public Class ColumnSequenceHub
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
    ''' Saves the column sequence to the user's profile in the table (TJ Column Sequence)
    ''' </summary>
    ''' <param name="columnPrefs">Column sorting order specified by the user to be the default for that user.</param>
    ''' <param name="viewName">View that the column sequence will apply to.  (transactions, inventory map, etc.)</param>
    ''' <returns>Task to reduce probability of timeouts.</returns>
    ''' <remarks></remarks>
    Public Function saveColumns(columnPrefs As List(Of String), viewName As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             Dim user As String = Context.User.Identity.Name
                                             RunActionSP("delTJColSeq", Context.QueryString.Get("WSID"), _
                                                         {{"@User", user, strVar}, {"@ViewName", viewName, strVar}})
                                             For x As Integer = 0 To columnPrefs.Count - 1
                                                 RunActionSP("insTJColSeq", Context.QueryString.Get("WSID"), _
                                                             {{"@User", user, strVar}, {"@View", viewName, strVar}, {"@Column", columnPrefs(x), strVar}, {"@Location", x, intVar}})
                                             Next
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.Message)
                                             insertErrorMessages("Column Sequence", "saveColumns", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Clears the default column sequence for the calling user.
    ''' </summary>
    ''' <param name="viewName">View to delete user preferences from.  (transactions, inventory map, etc.)</param>
    ''' <returns>Task to avoid timeout issues.</returns>
    ''' <remarks></remarks>
    Public Function clearAllColumns(viewName As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             RunActionSP("delTJColSeq", Context.QueryString.Get("WSID"), _
                                                         {{"@User", Context.User.Identity.Name, strVar}, {"@ViewName", viewName, strVar}})
                                         Catch ex As Exception
                                             insertErrorMessages("Column Sequence", "clearAllColumns", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Returns DB values for the column sequence for the calling user so that they can discard any change to the column sequence they made since the last time they saved.
    ''' </summary>
    ''' <param name="viewName">View to reset the column sequence for.  (transactions, inventory map, etc.)</param>
    ''' <returns>Task to avoid issues with timeouts.</returns>
    ''' <remarks></remarks>
    Public Function restoreDefaults(viewName As String) As Task
        Return Task.Factory.StartNew(Function()
                                         Dim DataReader As SqlDataReader = Nothing, defaultList As New List(Of String)
                                         Try
                                             DataReader = RunSPArray("selTJColSeq", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, {"@View", viewName, strVar}})
                                             If DataReader.HasRows Then
                                                 While DataReader.Read()
                                                     For x As Integer = 0 To DataReader.FieldCount - 1
                                                         defaultList.Add(DataReader(x))
                                                     Next
                                                 End While
                                             End If
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.Message)
                                             insertErrorMessages("Column Sequence", "restoreDefaults", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         Finally
                                             If Not IsNothing(DataReader) Then
                                                 DataReader.Dispose()
                                             End If
                                         End Try
                                         Return defaultList
                                     End Function)
    End Function
End Class