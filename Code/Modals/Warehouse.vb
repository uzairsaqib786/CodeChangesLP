' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class Warehouse

    ''' <summary>
    ''' Gets all warehouses
    ''' </summary>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>Warehouses in list of string</returns>
    ''' <remarks></remarks>
    Public Shared Function getWarehouses(user As String, WSID As String) As List(Of String)
        Dim result As New List(Of String)
        Dim DataReader As SqlDataReader = Nothing

        Try
            DataReader = RunSPArray("selWarehouses", WSID, {{"nothing"}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    result.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Warehouse", "getWarehouses", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Deletes a warehouse
    ''' </summary>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="warehouse">Warehouse to delete.</param>
    ''' <param name="WSID">The workstation that is currently being worked on</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function deleteWarehouse(user As String, warehouse As String, WSID As String) As Boolean
        Try
            RunActionSP("delWarehouse", WSID, {{"@Warehouse", warehouse, strVar}, {"@User", user, strVar}, {"@WSID", WSID, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Warehouse", "deleteWarehouse", ex.Message, user, WSID)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Saves a warehouse
    ''' </summary>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="warehouse">Warehouse to save.</param>
    ''' <param name="oldWarehouse">Warehouse to be updated.  "" if saving a new warehouse.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function saveWarehouse(user As String, warehouse As String, oldWarehouse As String, WSID As String) As Boolean
        Dim SP As String = IIf(oldWarehouse = "", "saveNewWarehouse", "updWarehouse")
        Dim params As String(,) = IIf(oldWarehouse = "", {{"@Warehouse", warehouse, strVar}}, _
                                      {{"@Warehouse", warehouse, strVar}, {"@OldWarehouse", oldWarehouse, strVar}})
        Try
            RunActionSP(SP, WSID, params)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Warehouse", "saveWarehouse", ex.Message, user, WSID)
            Return False
        End Try
        Return True
    End Function

End Class
