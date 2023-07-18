' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class Locations

    ''' <summary>
    ''' Gets Location and Zone typeahead
    ''' </summary>
    ''' <param name="loc">The value that is being enterd into the typeahead</param>
    ''' <param name="user">The user that is currently logged on</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of object that contains the location and zone data</returns>
    ''' <remarks></remarks>
    Public Shared Function getLocationDrop(loc As String, user As String, WSID As String) As List(Of Object)
        Dim locInfo As New List(Of Object)
        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selLocZonesTA", WSID, {{"@Loca", loc & "%", strVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    locInfo.Add(New With {.Location = IIf(IsDBNull(DataReader(0)), "", DataReader(0)), .Zone = IIf(IsDBNull(DataReader(1)), "", DataReader(1))})
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Inventory Map", "getLocationDrop", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return locInfo
    End Function

    ''' <summary>
    ''' Gets Zones from Location
    ''' </summary>
    ''' <param name="location">Location to get zones for.</param>
    ''' <param name="user">The user that is currently logged on</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of string that contains possible zones</returns>
    ''' <remarks></remarks>
    Public Shared Function getZones(location As String, user As String, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim result As New List(Of String)

        Try
            DataReader = RunSPArray("selLocZoneLoc", WSID, {{"@Location", location, strVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    result.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("GlobalHub", "getZones", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return result
    End Function
End Class
