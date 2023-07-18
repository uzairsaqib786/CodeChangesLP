' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class UnitOfMeasure
    ''' <summary>
    ''' Returns all Units of Measure from Unit of Measure table
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list containg the unit of measure values</returns>
    ''' <remarks></remarks>
    Public Shared Function getUoM(user As String, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim result As New List(Of String)

        Try
            DataReader = RunSPArray("selUoM", WSID, {{"nothing"}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    result.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Unit of Measure", "getUnitsOfMeasure", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Deletes specified Unit of Measure.
    ''' </summary>
    ''' <param name="UoM">Unit of Measure to delete.</param>
    ''' <param name="user">User requesting delete.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Shared Function delUoM(UoM As String, user As String, WSID As String) As Boolean
        Try
            RunActionSP("delUoM", WSID, {{"@UoM", UoM, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Unit of Measure", "deleteUoM", ex.Message, user, WSID)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Updates a Unit of Measure
    ''' </summary>
    ''' <param name="OldValue">Value to update.</param>
    ''' <param name="NewValue">New value.</param>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Shared Function updUoM(OldValue As String, NewValue As String, user As String, WSID As String) As Boolean
        Try
            RunActionSP("updateUoM", WSID, {{"@Old", OldValue, strVar}, {"@New", NewValue, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Unit of Measure", "saveUoM", ex.Message, user, WSID)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Saves a new unit of measure
    ''' </summary>
    ''' <param name="value">New Unit of Measure</param>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Shared Function saveUoM(value As String, user As String, WSID As String) As Boolean
        Try
            RunActionSP("insUnitOfMeasure", WSID, {{"@UoM", value, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Unit of Measure", "saveUoM", ex.Message, user, WSID)
            Return False
        End Try
        Return True
    End Function
End Class
