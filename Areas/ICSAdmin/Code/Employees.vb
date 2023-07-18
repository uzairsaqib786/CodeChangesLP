' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports ActiveDirectorySecurityDriver

Namespace Admin
    Public Class Employees

        ''' <summary>
        ''' Returns a list containing all Access Permissions, along with their descriptions and current Status
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>List of tuples containing the control names</returns>
        ''' <remarks></remarks>
        Public Shared Function allAccess(user As String, WSID As String) As List(Of Tuple(Of String, String, Boolean))
            Dim DataReader As SqlDataReader = Nothing, accessVals As New List(Of Tuple(Of String, String, Boolean))
            Try
                DataReader = RunSPArray("selControlNames", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        accessVals.Add(New Tuple(Of String, String, Boolean)(DataReader(0), DataReader(1), DataReader(2)))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "allAccess", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return accessVals
        End Function

        ''' <summary>
        ''' Returns all employee Groups
        ''' </summary>
        ''' <param name="user">The user that currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>List of string containing all the groups</returns>
        ''' <remarks></remarks>
        Public Shared Function allGroups(user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, groups As New List(Of String)
            Try
                If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                    groups = AuthorizationDriver.GetGroup("UserGroups").ToList()
                Else
                    DataReader = RunSPArray("selallgroups", WSID, {{"nothing"}})
                    If DataReader.HasRows Then
                        While DataReader.Read()
                            groups.Add(DataReader(0))
                        End While
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "allGroups", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return groups
        End Function

        ''' <summary>
        ''' Returns a list containing all Access Permissions, along with their descriptions and current Status
        ''' </summary>
        ''' <param name="filter">The filter that is being applied</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of all Values</returns>
        ''' <remarks></remarks>
        Public Shared Function allAccessFilter(filter As String, user As String, WSID As String) As List(Of Tuple(Of String, String, Boolean))
            Dim DataReader As SqlDataReader = Nothing, accessVals As New List(Of Tuple(Of String, String, Boolean))
            Try
                DataReader = RunSPArray("selControlNamesTA", WSID, {{"@Filter", filter, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        accessVals.Add(New Tuple(Of String, String, Boolean)(DataReader(0), DataReader(1), DataReader(2)))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "allAccess", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return accessVals
        End Function

        ''' <summary>
        ''' Returns all employee Groups
        ''' </summary>
        ''' <param name="user">The user that currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>List of string containing all the groups</returns>
        ''' <remarks></remarks>
        Public Shared Function allGroupsFilter(filter As String, user As String, WSID As String) As List(Of Tuple(Of String))
            Dim DataReader As SqlDataReader = Nothing, groups As New List(Of Tuple(Of String))
            Try
                For Each result In AuthorizationDriver.GetGroup("UserGroups")
                    If result.ToLower().Contains(filter.ToLower()) Then
                        groups.Add(New Tuple(Of String)(result))
                    End If
                Next
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "allGroups", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return groups
        End Function

        ''' <summary>
        ''' Returns all designated Pick levels for the selected Employee
        ''' </summary>
        ''' <param name="username">username to get pick levels for</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of tuples containing the pick level data</returns>
        ''' <remarks></remarks>
        Private Shared Function getPickLevels(username As String, user As String, WSID As String) As List(Of Tuple(Of String, String, String, String, String, String))
            Dim DataReader As SqlDataReader = Nothing, accessVals As New List(Of Tuple(Of String, String, String, String, String, String))
            Try
                DataReader = RunSPArray("selPickLevels", WSID, {{"@Username", username, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        accessVals.Add(New Tuple(Of String, String, String, String, String, String)(DataReader("Pick Level"), DataReader("Start Carousel"), DataReader("End Carousel"), DataReader("Start Shelf"), DataReader("End Shelf"), DataReader("Level ID")))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "getPickLevels", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return accessVals
        End Function

        ''' <summary>
        ''' Returns all Handheld zones for the current Employee
        ''' </summary>
        ''' <param name="username">User to get Handheld zones for</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currnetly being worked on</param>
        ''' <returns>A list of string containing the handheld zones</returns>
        ''' <remarks></remarks>
        Private Shared Function getHandheldZones(username As String, user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, zones As New List(Of String)
            Try
                DataReader = RunSPArray("selZonesStaff", WSID, {{"@Username", username, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        zones.Add(DataReader(0))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "getHandheldZones", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return zones
        End Function


        ''' <summary>
        ''' Return Bulk Pick Range for Employee
        ''' </summary>
        ''' <param name="username">username to get pick levels for</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of tuples containing the bulk range data</returns>
        ''' <remarks></remarks>
        Private Shared Function getBulkRange(username As String, user As String, WSID As String) As List(Of Tuple(Of String, String))
            Dim DataReader As SqlDataReader = Nothing, bulkRange As New List(Of Tuple(Of String, String))
            Try
                DataReader = RunSPArray("selBulkPickRange", WSID, {{"@Username", username, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        bulkRange.Add(New Tuple(Of String, String)(DataReader(0), DataReader(1)))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "getBulkRange", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return bulkRange
        End Function

        ''' <summary>
        ''' Gets Employee Data for Lookup Typeahead
        ''' </summary>
        ''' <param name="lastname">Last name to filter on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of object containg employees whose lastname begins with the input</returns>
        ''' <remarks></remarks>
        Public Shared Function employeeLookup(lastname As String, user As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing, bulkRange As New List(Of Object)
            Try
                If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                    DataReader = RunSPArray("selEmployeesLastNameTA", WSID, {{"@Lastname", lastname, strVar}})

                    Dim dt1 = New DataTable()
                    dt1 = AuthenticationDriver.GetAllUsers()

                    Dim dt2 = New DataTable()
                    dt2.Load(DataReader)

                    Dim JoinResult = (From p In dt1.AsEnumerable() Join t In dt2.AsEnumerable() On p.Field(Of String)("Username") Equals t.Field(Of String)("Username") Select New With
                {
                 .lastName = t.Field(Of String)("Last Name"),
                 .firstName = t.Field(Of String)("First Name"),
                 .mInitial = t.Field(Of String)("MI"),
                 .userName = t.Field(Of String)("Username"),
                 .maxOrders = t.Field(Of Int16)("Maximum Orders"),
                 .active = t.Field(Of Boolean)("Active"),
                 .password = t.Field(Of String)("Password"),
                 .access = t.Field(Of String)("Access Level"),
                 .group = t.Field(Of String)("Group Name"),
                 .email = t.Field(Of String)("Email Address")
                }).ToList()

                    bulkRange.Add(JoinResult)
                Else
                    DataReader = RunSPArray("selEmployeesLastNameTA", WSID, {{"@Lastname", lastname, strVar}})
                    If DataReader.HasRows Then
                        While DataReader.Read()
                            bulkRange.Add(New With {.lastName = DataReader(0), .firstName = DataReader(1), .mInitial = DataReader(3), .userName = DataReader(2), .maxOrders = DataReader(5), .active = DataReader(4), .password = DataReader(6), .access = DataReader(7), .group = DataReader(8), .email = DataReader(9)})
                        End While
                    End If
                End If

            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "employeeLookup", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return bulkRange
        End Function

        ''' <summary>
        ''' Gets all the possible bulk zones 
        ''' </summary>
        ''' <param name="user">The user currently logged in</param>
        ''' <param name="WSID">The workstation being worked on</param>
        ''' <returns>A list of all the bulk zones</returns>
        ''' <remarks></remarks>
        Public Shared Function getAllBulkZones(user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, zones As New List(Of String)
            Try
                DataReader = RunSPArray("ST_selLocationZone", WSID, {{"nothing"}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        zones.Add(DataReader("Zone"))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "getAllBulkZones", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return zones
        End Function


        ''' <summary>
        ''' Grabs data for the given employee
        ''' </summary>
        ''' <param name="name">The employee name</param>
        ''' <param name="user">The employee username</param>
        ''' <param name="WSID">The workstation being worked on</param>
        ''' <returns>An EmployeesModel which contains all the information for an employee</returns>
        ''' <remarks></remarks>
        Public Shared Function getEmployeeData(name As String, user As String, WSID As String) As EmployeesModel
            Dim userPermissions = GlobalFunctions.getUserRights(name, WSID),
                usergroups = getUserGroups(name, WSID),
                pickLevels = getPickLevels(name, user, WSID),
                handheldZones = getHandheldZones(name, user, WSID)
            Dim bulkRange = getBulkRange(name, user, WSID), allZones = getAllBulkZones(user, WSID)
            Return New EmployeesModel With {.allZones = allZones, .groupsAllowed = usergroups, .functionsAllowed = userPermissions, .employeePickLevels = pickLevels, .bulkProZones = handheldZones, .bulkProRangeAssign = bulkRange}
        End Function

        ''' <summary>
        ''' Returns all employee Groups
        ''' </summary>
        ''' <param name="user">The user that currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>List of string containing all the groups</returns>
        ''' <remarks></remarks>
        Public Shared Function getUserGroups(user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, groups As New List(Of String)
            Try
                groups = AuthorizationDriver.GetUserGroups("UserGroups", user.Trim()).ToList()
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("Employees", "allGroups", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return groups
        End Function

    End Class

End Namespace
