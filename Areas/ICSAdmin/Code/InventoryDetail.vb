' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Admin
    Public Class InventoryDetail
        ''' <summary>
        ''' Gets the typeahead data for the item number input
        ''' </summary>
        ''' <param name="itemNum">The entered value</param>
        ''' <param name="user">The user that currently logged on</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of string ocntaining all item numbers that begin with the input</returns>
        ''' <remarks></remarks>
        Public Shared Function selectItemNumberTypeahead(itemNum As String, user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Dim numbers As New List(Of String)

            Try
                DataReader = RunSPArray("selInventoryItemNumTA", WSID, {{"@ItemNum", itemNum & "%", strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        numbers.Add(IIf(IsDBNull(DataReader.Item("Item Number")), "", DataReader.Item("Item Number")))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Inventory Detail", "selectItemNumberTypeahead", ex.Message, user, WSID)
            End Try
            Return numbers
        End Function
    End Class
End Namespace

