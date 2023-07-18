' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Admin
    Public Class EmployeesStats
        ''' <summary>
        ''' Grabs the table information for the employees statistics table
        ''' </summary>
        ''' <param name="userList">List of selected usernames</param>
        ''' <param name="startDate">start date filter</param>
        ''' <param name="endDate">End date filter</param>
        ''' <param name="startRow">the starting row for number of entries</param>
        ''' <param name="endRow">the ending row for number of entries</param>
        ''' <param name="draw">when to draw the table</param>
        ''' <returns>Table Object for Employees Stats</returns>
        ''' <remarks></remarks>
        Public Shared Function getEmployeeStats(userList As String, startDate As Date, endDate As Date, startRow As Integer, endRow As Integer, draw As Integer, user As String, WSID As String) As TableObject
            Dim DataReader As SqlDataReader = Nothing
            Dim ReturnData As New List(Of List(Of String)), Stats As New TableObject(draw, 0, 0, New List(Of List(Of String))), counts As Integer = 0, index As Integer = 0

            Try
                If userList = "" Or userList = Nothing Then
                    Return Stats
                End If

                DataReader = RunSPArray("selUsertStatsDT", WSID, {{"@UserList", userList, strVar}, {"@Sdate", startDate, dteVar}, {"@Edate", endDate, dteVar}, {"@Srow", startRow, intVar}, {"@Erow", endRow, intVar}})


                While DataReader.HasRows
                    While DataReader.Read()
                        Dim data As New List(Of String)
                        If index = 0 Then
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                If Not IsDBNull(DataReader(x)) Then
                                    data.Add(DataReader(x))
                                Else
                                    data.Add("")
                                End If
                            Next
                            ReturnData.Add(data)
                        Else
                            If Not IsDBNull(DataReader(0)) Then
                                counts = DataReader(0)
                            End If
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While

                Stats = New TableObject(draw:=draw, recordsTotal:=counts, recordsFiltered:=counts, data:=ReturnData)
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("EmployeesStats", "getEmployeeStats", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Stats
        End Function
    End Class

End Namespace
