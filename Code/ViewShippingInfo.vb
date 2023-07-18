' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class ViewShippingInfo

    ''' <summary>
    ''' Gets the shipping info of packaging records for the datatable
    ''' </summary>
    ''' <param name="orderNum">The oder number whose records are going to be displayed</param>
    ''' <param name="WSID">the workstation that is being worked on</param>
    ''' <param name="User">The user that is currentley logged in</param>
    ''' <returns>A list of list of string that contains the infomration for the datatable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetOrderStatPack(orderNum As String, WSID As String, User As String) As List(Of List(Of String))
        Dim dataReader As SqlDataReader = Nothing
        Dim table As New List(Of List(Of String))
        Try
            dataReader = RunSPArray("selShipTransOrderStatPack", WSID, {{"@orderNum", orderNum, strVar}})
            If dataReader.HasRows Then
                While dataReader.Read()
                    Dim row As New List(Of String)
                    For x As Integer = 0 To dataReader.FieldCount - 1
                        row.Add(CheckDBNull(dataReader(x)))
                    Next
                    table.Add(row)
                End While
            End If
        Catch ex As Exception
            insertErrorMessages("ViewShipping.vb", "GetOrderStatPack", ex.ToString(), User, WSID)
        Finally
            If Not IsNothing(dataReader) Then
                dataReader.Close()
            End If
        End Try
        Return table
    End Function

    ''' <summary>
    ''' Gets the shipping info of shipping records for the datatable
    ''' </summary>
    ''' <param name="orderNum">The order number of the records to be displayed</param>
    ''' <param name="WSID">The workstation that is currently being worked on</param>
    ''' <param name="User">The user that is currently logged in</param>
    ''' <returns>A list if list of string that contains the records for the datatable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetOrderStatShip(orderNum As String, WSID As String, User As String) As List(Of List(Of String))
        Dim dataReader As SqlDataReader = Nothing
        Dim table As New List(Of List(Of String))
        Try
            dataReader = RunSPArray("selShipTransOrderStatShip", WSID, {{"@orderNum", orderNum, strVar}})
            If dataReader.HasRows Then
                While dataReader.Read()
                    Dim row As New List(Of String)
                    For x As Integer = 0 To dataReader.FieldCount - 1
                        row.Add(CheckDBNull(dataReader(x)))
                    Next
                    table.Add(row)
                End While
            End If
        Catch ex As Exception
            insertErrorMessages("ViewShipping.vb", "GetOrderStatShip", ex.ToString(), User, WSID)
        Finally
            If Not IsNothing(dataReader) Then
                dataReader.Close()
            End If
        End Try
        Return table
    End Function
End Class


