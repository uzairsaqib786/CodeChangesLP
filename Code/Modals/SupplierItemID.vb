' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class SupplierItemID
    ''' <summary>
    ''' Gets Supplier Item ID typeahead
    ''' </summary>
    ''' <param name="id">supplier item id</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of objects that contains the information for the supplier item ids</returns>
    ''' <remarks></remarks>
    Public Shared Function getSupplierItemIDTypeahead(id As String, user As String, WSID As String) As List(Of Object)
        Dim DataReader As SqlDataReader = Nothing
        Dim results As New List(Of String)
        Dim returnResult As New List(Of Object)
        Try
            DataReader = RunSPArray("selSupplierItemIDTypeahead", WSID, {{"@SupplierItemID", id & "%", strVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        results.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                    Next
                    returnResult.Add(New With {.SupplierItemID = results(0), .Description = results(1), .ItemNumber = results(2)})
                    results.Clear()
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SupplierItemID", "getSupplierItemID", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return returnResult
    End Function

    ''' <summary>
    ''' Gets Description, max, min qty, date, warehouse sensitive, unit of measure, cell size, velocity code, item number and expiration date from supplier item id
    ''' </summary>
    ''' <param name="id">Supplier Item Id to filter on.</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A object containing the data for the desired supplier item id</returns>
    ''' <remarks></remarks>
    Public Shared Function getSupplierItemIDInfo(id As String, user As String, WSID As String) As Object
        Dim DataReader As SqlDataReader = Nothing
        Dim returnObj As Object = Nothing

        Try
            DataReader = RunSPArray("selSupplierIDInfo", WSID, {{"@ID", id, strVar}})

            If DataReader.HasRows Then
                If DataReader.Read() Then
                    returnObj = New With {.Description = IIf(IsDBNull(DataReader(0)), "", DataReader(0)), _
                                          .MaxQty = IIf(IsDBNull(DataReader(1)), "", DataReader(1)), _
                                          .MinQty = IIf(IsDBNull(DataReader(2)), "", DataReader(2)), _
                                          .DateSensitive = IIf(IsDBNull(DataReader(3)), "", DataReader(3)), _
                                          .uom = IIf(IsDBNull(DataReader(4)), "", DataReader(4)), _
                                          .cell = IIf(IsDBNull(DataReader(5)), "", DataReader(5)), _
                                          .velocity = IIf(IsDBNull(DataReader(6)), "", DataReader(6)), _
                                          .ItemNumber = IIf(IsDBNull(DataReader(7)), "", DataReader(7)), _
                                          .expDate = IIf(IsDBNull(DataReader(8)), "", DataReader(8)), _
                                          .WarehouseSensitive = IIf(IsDBNull(DataReader(9)), "", DataReader(9))}
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SupplierItemID", "getSupplierItemIDInfo", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return returnObj
    End Function
End Class
