' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports ActiveDirectorySecurityDriver

Namespace Admin
    Public Class EmployeesHub
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
        ''' Sets whether the specified control is default for administrators
        ''' </summary>
        ''' <param name="controlName">Control</param>
        ''' <param name="newValue">Boolean as Admin = true or false</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function updateControl(controlName As String, newValue As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updControlNamesAdmin", Context.QueryString.Get("WSID"),
                                                             {{"@controlName", controlName, strVar}, {"@Value", CastAsSqlBool(newValue), intVar}})
                                                 Clients.Others.controlUpdated(controlName, newValue)
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex)
                                                 insertErrorMessages("Employees", "updateControl", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets all data for the specified employee
        ''' </summary>
        ''' <param name="name">Name of the employee to get data for</param>
        ''' <returns>An EmployeesModel which contains all the information for an employee</returns>
        ''' <remarks></remarks>
        Public Function getEmployeeData(name As String) As Task(Of EmployeesModel)
            Return Task.Factory.StartNew(Function() As EmployeesModel
                                             Return Employees.getEmployeeData(name, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Function)
        End Function

        ''' <summary>
        ''' Gets all bulk zones for employees
        ''' </summary>
        ''' <returns>A list of all the bulk zones</returns>
        ''' <remarks></remarks>
        Public Function getAllZones()
            Return Employees.getAllBulkZones(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
        End Function

        ''' <summary>
        ''' Gives specified user the specified control name right
        ''' </summary>
        ''' <param name="username">The user to give this control to</param>
        ''' <param name="controlName">The name o fthe control to give</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function submitControlName(username As String, controlName As String) As Task(Of String)
            Return Task.Factory.StartNew(Function() As String
                                             Dim DataReader As SqlDataReader = Nothing
                                             Try
                                                 DataReader = RunSPArray("insStaffAccess", Context.QueryString.Get("WSID"), {{"@Username", username, strVar}, {"@controlName", controlName, strVar}})
                                                 ' if datareader returned data
                                                 If DataReader.HasRows Then
                                                     DataReader.Read()
                                                     If DataReader(0) = "1" Then
                                                         Return "DNE"
                                                     Else
                                                         Return "EXISTS"
                                                     End If
                                                 End If
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex)
                                                 insertErrorMessages("Employees", "submitControlName", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(DataReader) Then
                                                     DataReader.Dispose()
                                                 End If
                                             End Try
                                             Return ""
                                         End Function)
        End Function

        ''' <summary>
        ''' Adds all controls from the accesslevel specified to the user specified
        ''' </summary>
        ''' <param name="username">The user to add the controls to</param>
        ''' <param name="accessLevel">The accesslevel which tells the controls to add</param>
        ''' <returns>A list of string containing the new rights for the given user</returns>
        ''' <remarks></remarks>
        Public Function addAllControls(username As String, accessLevel As String) As Task(Of List(Of String))
            Return Task.Factory.StartNew(Function() As List(Of String)
                                             Dim DataReader As SqlDataReader = Nothing
                                             Try
                                                 RunActionSP("insStaffAccessAll", Context.QueryString.Get("WSID"), {{"@Username", username, strVar}, {"@Access", accessLevel, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex)
                                                 insertErrorMessages("Employees", "submitControlName", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(DataReader) Then
                                                     DataReader.Dispose()
                                                 End If
                                             End Try
                                             Return GlobalFunctions.getUserRights(username, Context.QueryString.Get("WSID"))
                                         End Function)
        End Function

        ''' <summary>
        ''' Deletes specified control from specified user
        ''' </summary>
        ''' <param name="username">The suer to delete the control from</param>
        ''' <param name="control">The control to be deleted</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteControl(username As String, control As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             RunActionSP("delStaffAccess", Context.QueryString.Get("WSID"), {{"@Username", username, strVar}, {"@controlName", control, strVar}})
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gives specified user the specified control name right
        ''' </summary>
        ''' <param name="username">The user to give this control to</param>
        ''' <param name="controlName">The name o fthe control to give</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function submitGroupName(username As String, groupName As String) As Task(Of String)
            Return Task.Factory.StartNew(Function() As String
                                             Dim DataReader As SqlDataReader = Nothing
                                             Try
                                                 Dim message As String = AuthorizationDriver.AssignUserToGroup("UserGroups", username.Trim(), groupName.Trim())
                                                 If message = "Success" Then
                                                     Return ""
                                                 ElseIf message = "DNE" Then
                                                     Return "DNE"
                                                 ElseIf message = "The principal already exists in the store." Then
                                                     Return "EXISTS"
                                                 Else
                                                     Return "EXISTS"
                                                 End If
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex)
                                                 insertErrorMessages("Employees", "submitGroupName", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(DataReader) Then
                                                     DataReader.Dispose()
                                                 End If
                                             End Try
                                             Return ""
                                         End Function)
        End Function

        ''' <summary>
        ''' Deletes specified control from specified user
        ''' </summary>
        ''' <param name="username">The suer to delete the control from</param>
        ''' <param name="control">The control to be deleted</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteGroup(username As String, groupname As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             AuthorizationDriver.RemoveUserFromGroup("UserGroups", username, groupname)
                                         End Sub)
        End Function

        ''' <summary>
        ''' Saves a new employee
        ''' </summary>
        ''' <param name="ln">Last Name</param>
        ''' <param name="fn">First Name</param>
        ''' <param name="mi">Middle Initial</param>
        ''' <param name="username">Username</param>
        ''' <param name="access">Access Level</param>
        ''' <param name="password">Password</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function saveNewEmployee(ln, fn, mi, username, access, password) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim returnString As String = ""
                                                        Dim DataReader As SqlDataReader = Nothing
                                                        Try
                                                            If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                                Dim message As String = AuthenticationDriver.CreateUser(Trim(fn), Trim(ln), Trim(username), Trim(password))
                                                                If message = "User Added" Or message = "User already exists" Then
                                                                    returnString = message
                                                                Else
                                                                    Return message
                                                                End If

                                                                password = ""
                                                            Else
                                                                returnString = "User already exists"
                                                            End If

                                                            ' CREATE USER THROUGH DATABASE
                                                            DataReader = RunSPArray("insEmployee", Context.QueryString.Get("WSID"), {{"@Username", Trim(username), strVar}, {"@AccessLevel", access, strVar}, {"@Password", Trim(password), strVar}, {"@FN", Trim(fn), strVar}, {"@LN", Trim(ln), strVar}, {"@MI", Trim(mi), strVar}})
                                                            If DataReader.HasRows Then
                                                                DataReader.Read()
                                                                If DataReader(0) = "0" AndAlso returnString = "User already exists" Then
                                                                    returnString = "User already exists"
                                                                ElseIf DataReader(0) = "0" AndAlso returnString = "User Added" Then
                                                                    returnString = "Success"
                                                                ElseIf DataReader(0) = "1" Then
                                                                    returnString = "Success"
                                                                Else
                                                                    returnString = ""
                                                                End If
                                                            End If
                                                        Catch ex As Exception
                                                            ' DELETE USER THROUGH AD/ADLDS (Note: If ADLDS User created and due to exception database user is not created)
                                                            If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                                If returnString = "User Added" Then
                                                                    Dim deleteUser As Boolean = Membership.DeleteUser(username)
                                                                End If
                                                            End If

                                                            Debug.WriteLine(ex.Message)
                                                            insertErrorMessages("EmployeesHub", "saveNewEmployee", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            returnString = ex.Message
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try
                                                        Return returnString
                                                    End Function)
        End Function

        ''' <summary>
        ''' adds or updates an employee zone 
        ''' </summary>
        ''' <param name="username">The username whose zones are to be added or updated</param>
        ''' <param name="oldZone">The old zone if i is updates, or new if it is a new entry</param>
        ''' <param name="newZone">The new zone fir the add or update</param>
        ''' <returns>A boolean to indicate if the operation was successful</returns>
        ''' <remarks></remarks>
        Public Function updateEmployeeZone(username As String, oldZone As String, newZone As String) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Dim DataReader As SqlDataReader = Nothing
                                             Dim SP As String = "insZoneStaff", params As String(,) = {{"@Username", username, strVar}, {"@Zone", newZone, strVar}}
                                             If oldZone.ToLower() <> "new" Then
                                                 SP = "updZoneStaff"
                                                 params = {{"@Username", username, strVar}, {"@OldZone", oldZone, strVar}, {"@NewZone", newZone, strVar}}
                                             End If
                                             Try
                                                 DataReader = RunSPArray(SP, Context.QueryString.Get("WSID"), params)

                                                 If DataReader.HasRows Then
                                                     Return False
                                                 End If
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("EmployeesHub", "addNewEmployeeZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(DataReader) Then
                                                     DataReader.Dispose()
                                                 End If
                                             End Try
                                             Return True
                                         End Function)
        End Function

        ''' <summary>
        ''' Deletes the desired zone for the employee
        ''' </summary>
        ''' <param name="username">The username whose zone will be deleted</param>
        ''' <param name="zone">The zone to be deleted</param>
        ''' <returns>A boolean to indicate if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function deleteEmployeeZone(username As String, zone As String) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Try
                                                 RunActionSP("delZoneStaff", Context.QueryString.Get("WSID"), {{"@Username", username, strVar}, {"@Zone", zone, strVar}})

                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("EmployeesHub", "deleteEmployeeZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 Return False
                                             End Try
                                             Return True
                                         End Function)
        End Function

        ''' <summary>
        ''' Adds a new employee bulk pick range of locations
        ''' </summary>
        ''' <param name="username">Username of the employee to add to</param>
        ''' <param name="startLocation">Start Location in Bulk Pick Range</param>
        ''' <param name="endLocation">End Location in Bulk Pick Range</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function addEmployeeLocation(username As String, startLocation As String, endLocation As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("insBulkPickRange", Context.QueryString.Get("WSID"), {{"@Username", username, strVar}, {"@StartLocation", startLocation, strVar}, {"@EndLocation", endLocation, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("EmployeesHub", "addEmployeeLocation", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Deletes an employee's specified bulk pick range of locations
        ''' </summary>
        ''' <param name="username">Employee's username</param>
        ''' <param name="startLocation">Start Bulk Pick Location</param>
        ''' <param name="endLocation">End Bulk Pick Location</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteEmployeeLocation(username As String, startLocation As String, endLocation As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delBulkPickRange", Context.QueryString.Get("WSID"), {{"@Username", username, strVar}, {"@StartLocation", startLocation, strVar}, {"@EndLocation", endLocation, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("EmployeesHub", "deleteEmployeeLocation", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Updates the specified employee's location range at the specified values.
        ''' </summary>
        ''' <param name="username">Username of employee to update</param>
        ''' <param name="oldSL">Old Start Location</param>
        ''' <param name="newSL">New Start Location</param>
        ''' <param name="oldEL">Old End Location</param>
        ''' <param name="newEL">New End Location</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function updateEmployeeLocation(username As String, oldSL As String, newSL As String, oldEL As String, newEL As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updBulkPickRange", Context.QueryString.Get("WSID"), _
                                                             {{"@Username", username, strVar}, {"@StartLocation", newSL, strVar}, {"@EndLocation", newEL, strVar}, _
                                                              {"@OldStartLocation", oldSL, strVar}, {"@OldEndLocation", oldEL, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("EmployeesHub", "updateEmployeeLocation", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Adds or updates a pick level for the selected employee
        ''' </summary>
        ''' <param name="value">The indicator to either add oor update. if update then is the record to update</param>
        ''' <param name="username">The username whose record will be added or updated</param>
        ''' <param name="level">The pick level. ONly used in add functionality</param>
        ''' <param name="sShelf">The new start shelf vlaue</param>
        ''' <param name="eShelf">The new end shelf vlaue</param>
        ''' <returns>An object containing a boolean to see if the ioperation completed, adn the id of newly added item</returns>
        ''' <remarks></remarks>
        Public Function addUpdatePickLevel(value As String, username As String, level As Integer, sShelf As Integer, eShelf As Integer, sCar As Integer, eCaR As Integer) As Task(Of Object)
            Return Task.Factory.StartNew(Function() As Object
                                             Dim Success As Object = New With {.fail = True, .ID = -1}
                                             Dim Datareader As SqlDataReader = Nothing
                                             Try
                                                 If value.ToLower() = "new" Then
                                                     Datareader = RunSPArray("insEmployeePickLevel", Context.QueryString.Get("WSID"),
                                                                             {{"@Username", username, strVar}, {"@PickLevel", level, intVar},
                                                                              {"@StartShelf", sShelf, intVar}, {"@EndShelf", eShelf, intVar},
                                                                              {"@StartCar", sCar, intVar}, {"@EndCar", eCaR, intVar}})
                                                     Datareader.Read()
                                                     Success.ID = Datareader(0)
                                                 Else
                                                     RunActionSP("updEmployeePickLevel", Context.QueryString.Get("WSID"), {{"@LevelID", value, intVar}, {"@StartShelf", sShelf, intVar},
                                                                                                                          {"@EndShelf", eShelf, intVar}, {"@StartCar", sCar, intVar},
                                                                                                                          {"@EndCar", eCaR, intVar}})
                                                 End If
                                             Catch ex As Exception
                                                 Success.fail = False
                                                 insertErrorMessages("EmployeesHub", "addUpdatePickLevel", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(Datareader) Then
                                                     Datareader.Dispose()
                                                 End If
                                             End Try
                                             Return Success
                                         End Function)
        End Function

        ''' <summary>
        ''' Deletes the desired pick level for the user
        ''' </summary>
        ''' <param name="value">The record to be deleted</param>
        ''' <returns>A boolean telling if the operation was completed successfully</returns>
        ''' <remarks></remarks>
        Public Function deletePickLevel(value As String) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Try
                                                 RunActionSP("delEmployeePickLevel", Context.QueryString.Get("WSID"), {{"@LevelID", value, strVar}})

                                             Catch ex As Exception
                                                 insertErrorMessages("EmployeesHub", "deletePickLevel", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 Return False
                                             End Try
                                             Return True
                                         End Function)
        End Function

        ''' <summary>
        ''' Deletes the select employee from all tables displayed on the employees page
        ''' </summary>
        ''' <param name="username">The username to be deleted</param>
        ''' <returns>A boolean ot identity if the operation was perfomred</returns>
        ''' <remarks></remarks>
        Public Function deleteEmployee(username As String) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Try
                                                 ' DELETE USER THROUGH AD/ADLDS
                                                 If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                     Dim message As String = AuthenticationDriver.DeleteUser(Trim(username))
                                                     If message = "Success" Then
                                                     Else
                                                     End If
                                                 End If

                                                 ' DELETE USER THROUGH DATABASE
                                                 RunActionSP("delEmployee", Context.QueryString.Get("WSID"), {{"@Username", Trim(username), strVar}, {"@DeletedBy", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 insertErrorMessages("EmployeesHub", "deleteEmployee", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 Return False
                                             End Try
                                             Return True
                                         End Function)
        End Function

        ''' <summary>
        ''' Saves ane edits made to the selected employee
        ''' </summary>
        ''' <param name="firstName">The new first name value</param>
        ''' <param name="middle">The new middle initial value</param>
        ''' <param name="lastName">The new last name value</param>
        ''' <param name="userName">The username to be editted</param>
        ''' <param name="password">The new password value</param>
        ''' <param name="accessLevel">The new access level for this user</param>
        ''' <param name="group">The new group for this user</param>
        ''' <param name="email">The new email for the user</param>
        ''' <param name="maxOrders">The new maxOrders value</param>
        ''' <param name="active">The new value of the active checkbox</param>
        ''' <returns>A boolean identifying if the operation ran successfully</returns>
        ''' <remarks></remarks>
        Public Function saveChanges(firstName As String, middle As String, lastName As String, userName As String, password As String, accessLevel As String, group As String, email As String, maxOrders As Integer, active As Boolean) As Task(Of String)
            Return Task.Factory.StartNew(Function() As String
                                             Dim message As String
                                             Try
                                                 ' UPDATE USER THROUGH AD/ADLDS
                                                 If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                     message = AuthenticationDriver.UpdateUser(Trim(lastName), Trim(firstName), Trim(userName), Trim(password), active)
                                                     If message = "Success" Then
                                                     Else
                                                         Debug.WriteLine(message)
                                                         insertErrorMessages("EmployeesHub", "saveNewEmployee", message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return message
                                                     End If

                                                     password = ""
                                                 End If


                                                 ' UPDATE USER THROUGH DATABASE
                                                 RunActionSP("updEmployees", Context.QueryString.Get("WSID"), {{"@Username", userName, strVar}, {"@FirstName", firstName, strVar}, {"@Middle", middle, strVar}, {"@LastName", lastName, strVar},
                                                                                {"@Password", password, strVar}, {"@AccessLevel", accessLevel, strVar}, {"@Group", group, strVar}, {"@Email", email, strVar},
                                                                                {"@MaxOrders", maxOrders, intVar}, {"@active", CastAsSqlBool(active), intVar}})

                                                 message = "Success"
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 message = ex.Message
                                                 insertErrorMessages("EmployeesHub", "saveNewEmployee", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 Return message
                                             End Try
                                             Return message
                                         End Function)
        End Function

        ''' <summary>
        ''' Updates user with specified group's default control rights
        ''' </summary>
        ''' <param name="username">The user to be updated</param>
        ''' <param name="group">The group to get the controls from</param>
        ''' <returns>A list of string containing the new rights fo the user</returns>
        ''' <remarks></remarks>
        ''' E64 AUTHORIZATION
        Public Function updStaffAccessUserGroup(username As String, group As String) As Task(Of List(Of String))
            Return Task.Factory.StartNew(Function() As List(Of String)
                                             Dim DataReader As SqlDataReader = Nothing
                                             Try
                                                 If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                     Dim message As String = AuthorizationDriver.AssignUserToGroup("UserGroups", username, group)
                                                     If message = "Success" Then
                                                     Else
                                                         Debug.WriteLine(message)
                                                         insertErrorMessages("Employees", "updStaffAccessUserGroup", message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End If
                                                 Else
                                                     RunActionSP("updStaffAccessUserGroup", Context.QueryString.Get("WSID"), {{"@Username", username, strVar}, {"@Group", group, strVar}})
                                                 End If
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex)
                                                 insertErrorMessages("Employees", "updStaffAccessUserGroup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(DataReader) Then
                                                     DataReader.Dispose()
                                                 End If
                                             End Try
                                             Return GlobalFunctions.getUserRights(username, Context.QueryString.Get("WSID"))
                                         End Function)
        End Function
    End Class
End Namespace

