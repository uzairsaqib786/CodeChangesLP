' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class VelocityCode
    ''' <summary>
    ''' Gets velocity codes from the db.
    ''' </summary>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>Velocity Codes in list of string</returns>
    ''' <remarks></remarks>
    Public Shared Function getVelocityCodes(user As String, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim result As New List(Of String)

        Try
            DataReader = RunSPArray("selGoldZone", WSID, {{"nothing"}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    result.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("VelocityCode", "getVelocityCodes", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Saves a velocity code
    ''' </summary>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="vel">New velocity code value.</param>
    ''' <param name="oldvel">Old velocity code value (only if update), otherwise should be set to ""</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A bollean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Shared Function saveVelocityCode(user As String, vel As String, oldvel As String, WSID As String) As Boolean
        Dim SP As String = IIf(oldvel = "", "saveNewVelocityCode", "updateVelocityCode")
        Dim params As String(,) = IIf(oldvel = "", {{"@Velocity", vel, strVar}}, {{"@Velocity", vel, strVar}, {"@OldVelocity", oldvel, strVar}})
        Try
            RunActionSP(SP, WSID, params)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("VelocityCode", "saveVelocityCode", ex.ToString(), user, WSID)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Deletes a velocity code
    ''' </summary>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="vel">Velocity code to delete.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A boolean telling if the operation compelted successfully</returns>
    ''' <remarks></remarks>
    Public Shared Function deleteVelocityCode(user As String, vel As String, WSID As String) As Boolean
        Try
            RunActionSP("delVelocityCode", WSID, {{"@Velocity", vel, strVar}, {"@User", user, strVar}, {"@WSID", WSID, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("VelocityCode", "deleteVelocityCode", ex.ToString(), user, WSID)
            Return False
        End Try
        Return True
    End Function
End Class
