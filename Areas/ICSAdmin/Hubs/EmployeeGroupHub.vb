' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports ActiveDirectorySecurityDriver

Namespace Admin
    Public Class EmployeeGroupHub
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
        ''' Adds a new group 
        ''' </summary>
        ''' <param name="group">The name of the new group</param>
        ''' <returns>boolean telling if operation was completed</returns>
        ''' <remarks></remarks>
        '''E64 AUTHORIZATION
        Public Function insGroups(group As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Try
                                                             If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                                 Dim message As String = AuthorizationDriver.CreateGroup("UserGroups", group.Trim(), group.Trim())
                                                                 If message = "Success" Then
                                                                 Else
                                                                     Debug.WriteLine(message)
                                                                     insertErrorMessages("EmployeesGroupHub", "insGroups", message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                     Return False
                                                                 End If
                                                             Else
                                                                 DataReader = RunSPArray("insGroups", Context.QueryString.Get("WSID"), {{"@Group", group, strVar}})
                                                                 If DataReader.HasRows Then
                                                                     Return False
                                                                 End If
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex)
                                                             insertErrorMessages("EmployeesGroupHub", "insGroups", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function

        ''' <summary>
        ''' Deletes an entire group
        ''' </summary>
        ''' <param name="group">The group to delete</param>
        ''' <returns>boolean telling if operation was completed</returns>
        ''' <remarks></remarks>
        Public Function delGroups(group As String) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Try
                                                 If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                     Dim message As String = AuthorizationDriver.DeleteGroup("UserGroups", group.Trim())
                                                     If message = "True" Then
                                                     Else
                                                         Debug.WriteLine(message)
                                                         insertErrorMessages("EmployeesGroupHub", "insGroups", message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End If
                                                 Else
                                                     RunActionSP("delGroups", Context.QueryString.Get("WSID"), {{"@Group", group, strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                 End If
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("EmployeesGroupHub", "delGroups", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 Return False
                                             End Try
                                             Return True
                                         End Function)
        End Function

        ''' <summary>
        ''' gets the functions assigned to the group and any functions not assigned to the group 
        ''' </summary>
        ''' <param name="group">The group to grab the functions for</param>
        ''' <returns>object of assigned and unassigned functions</returns>
        ''' <remarks></remarks>
        Public Function getFunctionsByGroup(group As String) As Task(Of Object)
            Return Task.Factory.StartNew(Function() As Object
                                             Dim DataReader As SqlDataReader = Nothing, groupFuncts As New List(Of String), allFunctions As New List(Of String), index As Integer = 0
                                             Try
                                                 If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                     groupFuncts = AuthorizationDriver.GetSubGroupByGroup("UserGroups", "ACL_Functions", group.Trim()).ToList()
                                                     allFunctions = AuthorizationDriver.GetGroup("ACL_Functions").ToList()
                                                 Else
                                                     DataReader = RunSPArray("selGroupControls", Context.QueryString.Get("WSID"), {{"@Group", group, strVar}})
                                                     While DataReader.HasRows
                                                         While DataReader.Read()
                                                             If index = 1 Then
                                                                 For x As Integer = 0 To DataReader.FieldCount - 1
                                                                     If Not IsDBNull(DataReader(x)) Then
                                                                         groupFuncts.Add(DataReader(x))
                                                                     Else
                                                                         groupFuncts.Add("")
                                                                     End If
                                                                 Next
                                                             Else
                                                                 For x As Integer = 0 To DataReader.FieldCount - 1
                                                                     If Not IsDBNull(DataReader(x)) Then
                                                                         allFunctions.Add(DataReader(x))
                                                                     Else
                                                                         allFunctions.Add("")
                                                                     End If
                                                                 Next
                                                             End If

                                                         End While
                                                         index += 1
                                                         DataReader.NextResult()
                                                     End While
                                                 End If

                                                 For Each entry In groupFuncts
                                                     If allFunctions.Contains(entry) Then
                                                         allFunctions.Remove(entry)
                                                     End If
                                                 Next
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex)
                                                 insertErrorMessages("EmployeesGroupHub", "getFunctionsByGroup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(DataReader) Then
                                                     DataReader.Dispose()
                                                 End If
                                             End Try
                                             Return New With {.assigned = groupFuncts, .unassigned = allFunctions}
                                         End Function)

        End Function

        ''' <summary>
        ''' Adds new function to the group
        ''' </summary>
        ''' <param name="group">The group to add the new functions for</param>
        ''' <param name="controls">The list of controls to add to the group</param>
        ''' <returns>a boolean telling if the operation was completed successfully</returns>
        ''' <remarks></remarks>
        Public Function insGroupsFunction(group As String, controls As List(Of String)) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Try
                                                 If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                     AuthorizationDriver.RemoveGroupFromControl("UserGroups", "ACL_Functions", {group.Trim()}, AuthorizationDriver.GetSubGroupByGroup("UserGroups", "ACL_Functions", group.Trim()))
                                                     AuthorizationDriver.AssignGroupToControl("UserGroups", "ACL_Functions", {group.Trim()}, controls.ToArray)
                                                 Else
                                                     RunActionSP("delGroupControl", Context.QueryString.Get("WSID"), {{"@Group", group, strVar}})
                                                     For Each c In controls
                                                         Try
                                                             RunActionSP("insGroupControl", Context.QueryString.Get("WSID"), {{"@Group", group, strVar}, {"@Control", c, strVar}})
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("EmployeeGroupHub", "insGroupControl", "Group: " & group & " Control: " & c & " Message: " & ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try
                                                     Next
                                                 End If
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("EmployeesGroupHub", "insGroupsFunction", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 Return False
                                             End Try
                                             Return True
                                         End Function)
        End Function

        ''' <summary>
        ''' Updates all employees within the group to have the new controls
        ''' </summary>
        ''' <param name="group">The group which was updated</param>
        ''' <returns>A boolean telling if the operation was successsful</returns>
        ''' <remarks></remarks>
        Public Function updateEmployeesInGroup(group As String) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Try
                                                 RunActionSP("updStaffAccessGroups", Context.QueryString.Get("WSID"), {{"@Group", group, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("EmployeesGroupHub", "updateEmployeesInGroup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 Return False
                                             End Try
                                             Return True
                                         End Function)
        End Function

        ''' <summary>
        ''' Adds a new group 
        ''' </summary>
        ''' <param name="group">The name of the new group</param>
        ''' <returns>boolean telling if operation was completed</returns>
        ''' <remarks></remarks>
        '''E64 AUTHORIZATION
        Public Function cloneGroups(cloneGroup As String, newGroup As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim groupFunctions As String() = Nothing
                                                         Try
                                                             Dim message As String = AuthorizationDriver.CreateGroup("UserGroups", newGroup.Trim(), newGroup.Trim())
                                                             If message = "Success" Then
                                                                 groupFunctions = AuthorizationDriver.GetSubGroupByGroup("UserGroups", "ACL_Functions", cloneGroup.Trim())
                                                                 AuthorizationDriver.AssignGroupToControl("UserGroups", "ACL_Functions", {newGroup.Trim()}, groupFunctions)
                                                             Else
                                                                 Debug.WriteLine(message)
                                                                 insertErrorMessages("EmployeesGroupHub", "insGroups", message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Return False
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex)
                                                             insertErrorMessages("EmployeesGroupHub", "insGroups", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function
    End Class
End Namespace
