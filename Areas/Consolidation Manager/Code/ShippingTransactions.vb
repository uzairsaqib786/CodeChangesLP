' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Consolidation
    Public Class ShippingTransactions
        ''' <summary>
        ''' Selects the data for the shipping transactions datatable for the given order number
        ''' </summary>
        ''' <param name="orderNumber">The displayed order number</param>
        ''' <param name="user">The user that is currenlty logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>a list of list of string which contians all of the data for the table</returns>
        ''' <remarks></remarks>
        Public Shared Function insShipLabelData(orderNumber As String, user As String, WSID As String) As List(Of List(Of String))
            Dim dataReader As SqlDataReader = Nothing
            Dim table As New List(Of List(Of String))
            Try
                dataReader = RunSPArray("insShipLabelData", WSID, {{"@OrderNumber", orderNumber, strVar}, {"@User", user, strVar}})
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
                insertErrorMessages("ShippingTransaction.vb", "insShipLabelData", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return table
        End Function
        ''' <summary>
        ''' selects adjustment reasons for the reason dropdown 
        ''' </summary>
        ''' <param name="wsid">The workstation being worked on</param>
        ''' <param name="user">The user who is currently logged in</param>
        ''' <returns>List of string that contains all of the reasons within the adjustment lookup table</returns>
        ''' <remarks></remarks>
        Public Shared Function getAdjustmentReason(wsid As String, user As String) As List(Of String)
            Dim dataReader As SqlDataReader = Nothing
            Dim reason As New List(Of String)


            Try
                dataReader = RunSPArray("selAdjustmentLookup", wsid, {{"nothing"}})
                Debug.WriteLine("in function")
                If dataReader.HasRows Then
                    While dataReader.Read()
                        For x As Integer = 0 To dataReader.FieldCount - 1
                            reason.Add(dataReader(x))
                        Next
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("ShippingTransaction.vb", "getAdjustmentReason", ex.ToString(), user, wsid)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return reason
        End Function
        ''' <summary>
        ''' selects the item number and description for the item number typeahead on the page
        ''' </summary>
        ''' <param name="itemNum">The value entered into the typeahead</param>
        ''' <param name="orderNum">The displayed order number</param>
        ''' <param name="user">The user that is logged in</param>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <returns>a list of objects where each object is a seperate item number and description</returns>
        ''' <remarks></remarks>
        Public Shared Function selItemNumShipTransTA(itemNum As String, orderNum As String, user As String, wsid As String) As List(Of Object)
            Dim dataReader As SqlDataReader = Nothing
            Dim typeahead As New List(Of Object)
            Try
                dataReader = RunSPArray("selItemNumShipTransTA", wsid, {{"@OrderNumber", orderNum, strVar}, {"@ItemNumber", itemNum & "%", strVar}})
                If dataReader.HasRows Then
                    While dataReader.Read()
                        typeahead.Add(New With {.ItemNumber = CheckDBNull(dataReader(0)), .Description = CheckDBNull(dataReader(1))})
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("ShippingTransactions.vb", "selItemNumShipTransTA", ex.ToString(), user, wsid)
            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return typeahead
        End Function
        ''' <summary>
        ''' Selects the preferences that ar needed within the shipping transactions page
        ''' </summary>
        ''' <param name="wsid">The owrkstation that is being worked on</param>
        ''' <param name="user">The user that is logged in</param>
        ''' <returns>object containing the desired preferences' values</returns>
        ''' <remarks></remarks>
        Public Shared Function selShippingPreferences(wsid As String, user As String) As Object
            Dim dataReader As SqlDataReader = Nothing
            Dim prefs As Object = Nothing
            Try
                dataReader = RunSPArray("selShipPrefs", wsid, {{"@wsid", wsid, strVar}})
                If dataReader.HasRows Then
                    While dataReader.Read()
                        prefs = New With {.Freight = CheckDBNull(dataReader("Freight")), .Freight1 = CheckDBNull(dataReader("Freight1")), _
                        .Freight2 = CheckDBNull(dataReader("Freight2")), .Weight = CheckDBNull(dataReader("Weight")), _
                        .Length = CheckDBNull(dataReader("Length")), .Width = CheckDBNull(dataReader("Width")),
                        .Height = CheckDBNull(dataReader("Height")), .Cube = CheckDBNull(dataReader("Cube"))}
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("ShippingTransactions.vb", "selShippingPreferences", ex.ToString(), user, wsid)

            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return prefs
        End Function
    End Class
End Namespace

