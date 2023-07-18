' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class PrintLocations
    ''' <summary>
    ''' Gets locations for typeahead for begin box
    ''' </summary>
    ''' <param name="location">Location to get suggestions for</param>
    ''' <param name="unique">Group by location field</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of object that contains the possible locations that begin with the given input</returns>
    ''' <remarks></remarks>
    Public Shared Function getLocationBegin(location As String, unique As Boolean, user As String, WSID As String) As List(Of Object)
        Dim DataReader As SqlDataReader = Nothing
        Dim locations As New List(Of Object)
        Dim uniq As Integer = 0
        If unique Then
            uniq = 1
        End If

        Try
            DataReader = RunSPArray("selInvMapStartLocNumTA", WSID, {{"@BeginLoc", location & "%", strVar}, _
                                                  {"@uniq", uniq, intVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        locations.Add(IIf(IsDBNull(DataReader(x)), New With {.Location = ""}, New With {.Location = DataReader(x)}))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("PrintLocations", "getLocationsBegin", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Debug.WriteLine(location)
        Return locations
    End Function

    ''' <summary>
    ''' Gets Ending Location for typeahead end box
    ''' </summary>
    ''' <param name="location">Current user input</param>
    ''' <param name="beginLoc">Beginning location typeahead value to restrict suggestions</param>
    ''' <param name="unique">Unique checkbox</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of object that contains possible locations that occur after the start location and begin with the user input value</returns>
    ''' <remarks></remarks>
    Public Shared Function getLocationEnd(location As String, beginLoc As String, unique As Boolean, user As String, WSID As String) As List(Of Object)
        Dim DataReader As SqlDataReader = Nothing
        Dim locations As New List(Of Object)
        Dim uniq As Integer = 0
        If unique Then
            uniq = 1
        End If

        Try
            DataReader = RunSPArray("selInvMapEndLocNumTA", WSID, {{"@BeginLoc", beginLoc, strVar}, _
                                                       {"@EndLoc", location & "%", strVar}, _
                                                  {"@uniq", uniq, intVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        locations.Add(IIf(IsDBNull(DataReader(x)), New With {.Location = ""}, New With {.Location = DataReader(x)}))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("PrintLocations", "getLocationsBegin", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return locations
    End Function

End Class
