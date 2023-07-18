' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace OrderManager
    Public Class CreateOrders
        ''' <summary>
        '''  Selects all of the warehouses from the table
        ''' </summary>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>A list containing all the warehouses within the database</returns>
        ''' <remarks></remarks>
        Public Shared Function selectWarehouses(WSID As String, User As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Dim warehouses As New List(Of String)

            Try
                DataReader = RunSPArray("selWarehouses", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        warehouses.Add(DataReader(0))
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("CreateOrders", "selectWarehouses", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return warehouses
        End Function

        ''' <summary>
        ''' selects the order numbers from open transaction pending for a typeahead
        ''' </summary>
        ''' <param name="ordernum">The inputted value</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of object containing the order numbers</returns>
        ''' <remarks></remarks>
        Public Shared Function selCreateOrdersTA(ordernum As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim ordernums As New List(Of Object)
            Try
                DataReader = RunSPArray("selOTPendTA", WSID, {{"@OrderNumber", ordernum, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        ordernums.Add(New With {.ordernum = DataReader(0)})
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("CreateOrders", "selCreateOrdersTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return ordernums
        End Function
    End Class
End Namespace
