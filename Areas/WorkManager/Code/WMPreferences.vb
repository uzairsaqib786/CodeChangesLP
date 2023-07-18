' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace WorkManager
    Public Class WMPreferences
        ''' <summary>
        ''' Gets the settings for work manager
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>list of string containing the settings</returns>
        ''' <remarks></remarks>
        Public Shared Function GetGeneralBatchSettings(user As String, WSID As String) As Dictionary(Of String, Object)
            Try
                Return GetResultMap("selWMGeneralBatch", WSID, {{"@WSID", WSID, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "GetGeneralBatchSettings", ex.Message, user, WSID)
            End Try
            Return New Dictionary(Of String, Object)
        End Function

        ''' <summary>
        ''' Gets the data for the wmUsers datatable
        ''' </summary>
        ''' <param name="draw">Parameter needed for the table</param>
        ''' <param name="sRow">the row to start grabbing records from</param>
        ''' <param name="eRow">The row to stop getting records from</param>
        ''' <param name="pluginWhere">The where clause that is applied to the table</param>
        ''' <param name="sortCol">The column index that is being sorted on</param>
        ''' <param name="sortOrder">The direction of the sort</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <returns>tableobject containing the data for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function GetWMUsersTable(draw As Integer, sRow As Integer, eRow As Integer, pluginWhere As String, sortCol As Integer, sortOrder As String, WSID As String, user As String) As TableObject
            Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
            Dim cols As New List(Of String) From {"Username", "First Name", "Last Name", "Active", "WM Settings", "Organize Work", "Reports", "Allow Picks", "Auto Batch Picks", "Default Pick Orders", "Max Pick Orders",
                "Max Pick Lines", "Allow Puts", "Auto Batch Put Aways", "Default Put Orders", "Max Put Orders", "Max Put Lines",
                "Allow Counts", "Auto Batch Counts", "Default Count Orders", "Max Count Orders", "Max Count Lines"}
            Try
                table = GetJQueryDataTableResult(draw, "selWMUsersDT", WSID, user, {{"@PluginWhere", pluginWhere, strVar}, {"@StartRow", sRow, intVar}, {"@EndRow", eRow, intVar}, {"@SortCol", "[" & cols(sortCol) & "]", strVar}, {"@SortOrder", sortOrder, strVar}}, 2, 1, 0)
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "GetWMUsersTable", ex.Message, user, WSID)
            End Try
            Return table
        End Function

        ''' <summary>
        ''' Gets the data for the location ranges datatable
        ''' </summary>
        ''' <param name="draw">Parameter needed for the table</param>
        ''' <param name="startRow">The row to start grabbing records from</param>
        ''' <param name="endRow">The row to stop getting records from</param>
        ''' <param name="sortCol">The column index that is being sorted on</param>
        ''' <param name="sortOrder">The direction of the sort</param>
        ''' <param name="filter">The filter that is currently applied to the table</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>tableobject containing all the desired data for the table</returns>
        ''' <remarks></remarks>
        Public Shared Function getLocationRangesTable(draw As Integer, startRow As Integer, endRow As Integer, sortCol As Integer,
                                                      sortOrder As String, filter As String, User As String, WSID As String) As TableObject
            Dim columnSequence As New List(Of String) From {"ID", "Range Name", "Start Location", "End Location", "Multi Worker Range", "Active"}
            Dim column As String = columnSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim data As New List(Of List(Of String))
            Dim RecordCount As Integer = 0
            Dim FilterCount As Integer = 0

            Try
                DataReader = RunSPArray("selWMLocationRangesDT", WSID, {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar},
                                                                        {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar},
                                                                        {"@filter", filter, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        FilterCount = DataReader("FilterCount")
                        For Each col In columnSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        data.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "GetGeneralBatchSettings", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, RecordCount, FilterCount, data)
        End Function

        ''' <summary>
        ''' Gets the data for the employee typeahead
        ''' </summary>
        ''' <param name="employee">The value ot get suggestions for</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <returns>list of object contianing the data for the typeahead</returns>
        ''' <remarks></remarks>
        Public Shared Function GetEmployeeTypeahead(employee As String, WSID As String, user As String) As List(Of Object)
            Dim reader As SqlDataReader = Nothing
            Dim employees As New List(Of Object)
            Try
                reader = RunSPArray("selWMEmployeesTA", WSID, {{"@Lastname", employee, strVar}})
                If reader.HasRows Then
                    Dim extraData As New List(Of String)
                    While reader.Read
                        ' exclude our three "special" properties (first three) and exclude our custom order by field (last)
                        For x As Integer = 3 To reader.FieldCount - 2
                            extraData.Add(CheckDBNull(reader(x)))
                        Next
                        employees.Add(New With {.ln = CheckDBNull(reader("Last Name")), .fn = CheckDBNull(reader("First Name")), .un = CheckDBNull(reader("Username")),
                                                .extraData = extraData})
                        extraData = New List(Of String)
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "GetEmployeeTypeahead", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return employees
        End Function

        ''' <summary>
        ''' Gets the data for the start location range typeahead
        ''' </summary>
        ''' <param name="StartRange">The value to get suggestiosn for</param>
        ''' <param name="User">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>list of object cotiang the start location, type, location</returns>
        ''' <remarks></remarks>
        Public Shared Function StartLocationRangeTypeahead(StartRange As String, User As String, WSID As String) As List(Of Object)
            Dim data As New List(Of Object)
            Dim DataReader As SqlDataReader = Nothing


            Try
                DataReader = RunSPArray("selInvMapStartLocRangeTA", WSID, {{"@StartRange", StartRange, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        data.Add(New With {.StartLoc = DataReader("Start Location"), .Type = DataReader("Type"), .Location = DataReader("Location")})
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "StartLocationRangeTypeahead", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return data
        End Function

        ''' <summary>
        ''' Gets the data for the end location range typeahead
        ''' </summary>
        ''' <param name="StartRange">The value that will limit suggestions those that are after it</param>
        ''' <param name="EndRange">The value ot get suggestions for</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>lists of object containg the data for the typeahead</returns>
        ''' <remarks></remarks>
        Public Shared Function EndLocationRangeTypeahead(StartRange As String, EndRange As String, User As String, WSID As String) As List(Of Object)
            Dim data As New List(Of Object)
            Dim DataReader As SqlDataReader = Nothing

            Try
                DataReader = RunSPArray("selInvMapEndLocRangeTA", WSID, {{"@StartRange", StartRange, strVar}, {"@EndRange", EndRange, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        data.Add(New With {.EndLoc = DataReader("End Location"), .Type = DataReader("Type"), .Location = DataReader("Location")})
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "EndLocationRangeTypeahead", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return data
        End Function

        ''' <summary>
        ''' inserts a new user or wsid 
        ''' </summary>
        ''' <param name="username">The user name ot be inserted</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function InsertNewUserOrWSID(username As String, user As String, WSID As String) As Boolean
            Try
                RunActionSP("insWMUser", WSID, {{"@User", username, strVar}, {"@WSID", WSID, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("MenuController", "Index", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Gets the data for the Worker Ranges datatable 
        ''' </summary>
        ''' <param name="draw">Parameter needed for the table</param>
        ''' <param name="startRow">The row to start grabbing records from</param>
        ''' <param name="endRow">The row to stop getting records from</param>
        ''' <param name="sortCol">The column index that is being sorted on</param>
        ''' <param name="sortOrder">The direction of the sort</param>
        ''' <param name="filter">The filter that is currently applied to the table</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>tableobject contain all the desired data for the table</returns>
        ''' <remarks></remarks>
        Public Shared Function getWorkerRangesTable(draw As Integer, startRow As Integer, endRow As Integer, sortCol As Integer,
                                                  sortOrder As String, filter As String, User As String, WSID As String) As TableObject
            Dim columnSequence As New List(Of String) From {"ID", "Username", "First Name", "Last Name", "Range Name", "Start Location", "End Location", "Active", "Date Stamp"}
            Dim column As String = columnSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim data As New List(Of List(Of String))
            Dim RecordCount As Integer = 0
            Dim FilterCount As Integer = 0


            Try
                DataReader = RunSPArray("selWMUserRangesDT", WSID, {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar},
                                                                    {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar},
                                                                    {"@filter", filter, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        FilterCount = DataReader("FilterCount")
                        For Each col In columnSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        data.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "getWorkerRangesTable", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, RecordCount, FilterCount, data)
        End Function

        ''' <summary>
        ''' Selects the data for the last name typeahead
        ''' </summary>
        ''' <param name="LastName">The value to get suggestions for</param>
        ''' <param name="GetData">If 0 a like clause is used within sql, otherwise uses an equals</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>returns a list of object containing the data for the typeahead</returns>
        ''' <remarks></remarks>
        Public Shared Function selectWMUsersLastNameTypeahead(LastName As String, GetData As Integer, User As String, WSID As String) As List(Of Object)
            Dim data As New List(Of Object)
            Dim DataReader As SqlDataReader = Nothing

            Try
                DataReader = RunSPArray("selWMUsersLastNameTA", WSID, {{"@LastName", LastName, strVar}, {"@GetData", GetData, intVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        data.Add(New With {.LastName = DataReader("Last Name"), .FirstName = DataReader("First Name"), .Username = DataReader("Username")})
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "selectWMUsersLastNameTypeahead", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return data
        End Function

        ''' <summary>
        ''' selects the data for the range name typeahead
        ''' </summary>
        ''' <param name="RangeName">The value to get suggestions for</param>
        ''' <param name="GetData">If 0 a like clause is used within sql, otherwise uses an equals</param>
        ''' <param name="User">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>list of object containing the data for the typeahead</returns>
        ''' <remarks></remarks>
        Public Shared Function selectWMLocRangesRangeNameTypeahead(RangeName As String, GetData As Integer, User As String, WSID As String) As List(Of Object)
            Dim data As New List(Of Object)
            Dim DataReader As SqlDataReader = Nothing

            Try
                DataReader = RunSPArray("selWMLocationRangesRangeNameTA", WSID, {{"@RangeName", RangeName, strVar}, {"@GetData", GetData, intVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        data.Add(New With {.RangeName = DataReader("Range Name"), .StartLocation = DataReader("Start Location"), .EndLocation = DataReader("End Location")})
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "selectWMUsersLastNameTypeahead", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return data
        End Function

        ''' <summary>
        ''' Gets the settings for the given username
        ''' </summary>
        ''' <param name="username">The user name ot get the setting for</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that currently being worked on</param>
        ''' <returns>List of string containing all the wm preferences for the user</returns>
        ''' <remarks></remarks>
        Public Shared Function GetUserSettings(username As String, user As String, WSID As String) As List(Of String)
            Dim reader As SqlDataReader = Nothing
            Dim settings As New List(Of String)
            Try
                reader = RunSPArray("selWMPreferences", WSID, {{"@Username", username, strVar}})
                If reader.HasRows Then
                    If reader.Read Then
                        For x As Integer = 0 To reader.FieldCount - 1
                            settings.Add(CheckDBNull(reader(x)))
                        Next
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "GetUserSettings", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return settings
        End Function

        ''' <summary>
        ''' Gets permissions in a list of boolean (WM Settings, Organize Work, Reports)
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of boolean that contains the values for each of the permissions</returns>
        ''' <remarks></remarks>
        Public Shared Function GetWMPermissions(user As String, WSID As String) As List(Of Boolean)
            Dim reader As SqlDataReader = Nothing
            ' [WM Settings], [Organize Work], [Reports]
            Dim permissions As New List(Of Boolean)
            Try
                reader = RunSPArray("selWMPermissions", WSID, {{"@User", user, strVar}})
                If reader.HasRows Then
                    reader.Read()
                    permissions.Add(reader("WM Settings"))
                    permissions.Add(reader("Organize Work"))
                    permissions.Add(reader("Reports"))
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("WMPreferences", "GetWMPermissions", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then reader.Dispose()
            End Try
            Return permissions
        End Function
    End Class
End Namespace