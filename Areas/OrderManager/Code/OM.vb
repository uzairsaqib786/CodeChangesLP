' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace OrderManager
    Public Class OM
        ''' <summary>
        ''' Selects the max orders value from OMPreferences for the given WSID
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>An integer telling the max number of orders</returns>
        ''' <remarks></remarks>
        Public Shared Function selectMaxOrders(WSID As String, User As String) As Integer
            Dim DataReader As SqlDataReader = Nothing
            Dim MaxOrders As Integer = 0

            Try
                DataReader = RunSPArray("selOMPrefs", WSID, {{"@WSID", WSID, strVar}})

                If DataReader.HasRows Then
                    DataReader.Read()
                    MaxOrders = DataReader("Max Orders")
                End If
            Catch ex As Exception
                insertErrorMessages("OM.vb", "selectMaxOrders", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return MaxOrders
        End Function

        ''' <summary>
        ''' Gets the tbale data for the order manager datatable
        ''' </summary>
        ''' <param name="draw">Tells if the table is currently being drawn</param>
        ''' <param name="startRow">The row that the dattable is currently starting at</param>
        ''' <param name="endRow">The row that the datatable iis currently ending at</param>
        ''' <param name="sortCol">The column that is being sorted on</param>
        ''' <param name="sortOrder">The order that the column is sorted by</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="User">The user that is currently logged on</param>
        ''' <returns>Tableobject containing all the information needed for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function selectOrderManagerTempDT(draw As Integer, startRow As String, endRow As String, sortCol As Integer, sortOrder As String,
                                                        searchColumn As String, searchString As String, WSID As String, User As String) As TableObject
            Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
            Dim columnSequence = GlobalFunctions.getDefaultColumnSequence("Order Manager", User, WSID)
            Dim sortColumn As String = GlobalFunctions.getColumns("Order Manager", User, WSID)(sortCol)

            Try
                table = GetJQueryDataTableResult(draw, "selOrderManTempDataDTNew", WSID, User, {{"WSID", WSID, strVar},
                                                                        {"@sRow", startRow, strVar},
                                                                        {"@eRow", endRow, strVar},
                                                                        {"@sortColumn", sortColumn, strVar},
                                                                        {"@sortOrder", sortOrder, strVar},
                                                                        {"@searchCol", searchColumn, strVar},
                                                                        {"@searchStr", searchString, strVar}}, columnOrder:=columnSequence)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("OM.vb", "selectOrderManagerTempDT", ex.Message, User, WSID)

            End Try
            Return table
        End Function

        ''' <summary>
        ''' Deletes the data from the order manager temp table in order to add new data to it
        ''' </summary>
        ''' <param name="WSID">The workstation that is being workd on</param>
        ''' <param name="User">The suer that is currently logged in</param>
        ''' <remarks></remarks>
        Public Shared Sub delOrderManTemp(WSID As String, User As String)

            Try
                RunActionSP("delOrderManagerTemp", WSID, {{"@WSID", WSID, strVar}})
            Catch ex As Exception
                insertErrorMessages("OrderManager Hub", "delOrderManTemp", ex.Message, User, WSID)
            End Try
        End Sub

    End Class
End Namespace
