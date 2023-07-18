' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class ScanCodes
    ''' <summary>
    ''' Gets Scan Types for modal
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of string that contains each can type</returns>
    ''' <remarks></remarks>
    Public Shared Function getScanTypes(user As String, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim vals As New List(Of String)

        Try
            DataReader = RunSPArray("SelScanType", WSID, {{"Nothing"}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    vals.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ScanCodes", "getScanCodes", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return vals
    End Function

    ''' <summary>
    ''' Deletes specified scan type
    ''' </summary>
    ''' <param name="scantype">Scan Type to delete</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <remarks></remarks>
    Public Shared Sub deleteScanType(scantype As String, user As String, WSID As String)
        Try
            RunActionSP("DelScanType", WSID, {{"@Type", scantype, strVar}, {"@User", user, strVar}, {"@WSID", WSID, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ScanCodes", "deleteScanType", ex.ToString(), user, WSID)
        End Try
    End Sub

    ''' <summary>
    ''' Saves a new or updates an old scan type
    ''' </summary>
    ''' <param name="scantype">Scan Type value to save.</param>
    ''' <param name="oldscantype">Scan Type value to overwrite (if new = "")</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <remarks></remarks>
    Public Shared Sub saveScanType(scantype As String, oldscantype As String, user As String, WSID As String)
        Dim SP As String = IIf(oldscantype = "", "insScanType", "updateScanType")
        Dim params As String(,) = IIf(oldscantype = "", {{"@Type", scantype, strVar}}, _
                                      {{"@NewType", scantype, strVar}, {"@OldType", oldscantype, strVar}})

        Try
            RunActionSP(SP, WSID, params)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ScanCodes", "saveScanType", ex.ToString(), user, WSID)
        End Try
    End Sub
End Class
