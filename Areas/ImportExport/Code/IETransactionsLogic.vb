' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module IETransactionsLogic
        ''' <summary>
        ''' Gets the data for the datatable
        ''' </summary>
        ''' <param name="draw">parameter for datatables</param>
        ''' <param name="table">Which table the records are being selected for</param>
        ''' <param name="sDate">The data to start from</param>
        ''' <param name="eDate">The date to end from</param>
        ''' <param name="pluginWhere">The where clasue that is being applied to the table</param>
        ''' <param name="sortCol">The column index that is being sorted on</param>
        ''' <param name="sortOrder">The direction of the sort</param>
        ''' <param name="sRow">The row to start getting records from</param>
        ''' <param name="eRow">The row to stop getting records from</param>
        ''' <param name="user">The suer that is currently logged on</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A table object that contains the data in order to populate the datatable</returns>
        Public Function GetIETransactions(
                draw As Integer, table As String, sDate As DateTime, eDate As DateTime, pluginWhere As String, sortCol As Integer,
                sortOrder As String, sRow As Integer, eRow As Integer, user As String, WSID As String) As TableObject
            '----------------
            Dim data As New TableObject(draw, 0, 0, New List(Of List(Of String)))

            Dim column As String = "", cols As String()
            If table.ToLower() = "import transactions" Or table.ToLower() = "import transactions history" Then
                cols = {"Transaction Type", "Import Date", "Order Number", "Item Number", "Description", "Transaction Quantity", "Priority", "Required Date",
                        "Line Number", "Serial Number", "Lot Number", "Expiration Date", "Batch Pick ID",
                        "Unit of Measure", "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7",
                        "User Field8", "User Field9", "User Field10", "Revision", "Tote ID", "Tote Number", "Cell", "Host Transaction ID",
                        "Zone", "Warehouse", "Import By", "Import Filename", "Notes", "Emergency"}
            Else
                cols = {"Transaction Type", "Import Date", "Order Number", "Item Number", "Description", "Transaction Quantity", "Priority", "Required Date",
                        "Line Number", "Serial Number", "Lot Number", "Expiration Date", "Completed Quantity", "Completed Date", "Completed By", "Batch Pick ID",
                        "Unit of Measure", "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7",
                        "User Field8", "User Field9", "User Field10", "Revision", "Tote ID", "Tote Number", "Cell", "Host Transaction ID", "ID",
                        "Zone", "Carousel", "Row", "Shelf", "Bin", "Warehouse", "Inv Map ID", "Import By", "Import Filename", "Notes", "Emergency",
                        "Master Record", "Master Record ID", "Export Batch ID", "Export Date", "Export File Name", "Exported By", "Status Code"}
            End If

            column = cols(sortCol)

            Try
                data = GetJQueryDataTableResult(draw, "selIETransactions", "IE", user,
                                                {{"@Table", table, strVar}, {"@SDate", sDate, dteVar}, {"@EDate", eDate, dteVar}, {"@SRow", sRow, intVar}, {"@ERow", eRow, intVar},
                                                 {"@PluginWhere", pluginWhere, strVar}, {"@SortCol", column, strVar}, {"@SortOrder", sortOrder, strVar}})
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("IETransactionsLogic", "GetIETransactions", ex.ToString(), user, WSID)
            End Try
            Return data
        End Function

        ''' <summary>
        ''' Gets the minimum date for the desired table
        ''' </summary>
        ''' <param name="table">The table top get the date for</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A string telling what the minimum date is</returns>
        Public Function GetIETransMinDate(table As String, user As String, WSID As String) As String
            Dim result As String = ""
            Try
                result = CheckDBNull(GetResultSingleCol("selIETransMinDate", "IE", {{"@Table", table, strVar}}))
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("IETransactionsLogic", "GetIETransMinDate", ex.ToString(), user, WSID)
            End Try
            If result = "" Then result = Now.ToString("MM/dd/yyyy")
            Return result
        End Function

        ''' <summary>
        ''' Deletes everything frm the desired table
        ''' </summary>
        ''' <param name="table">The table to delete from</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A boolena telling if the oepration completed successfully</returns>
        Public Function DeleteTransactions(table As String, user As String, WSID As String) As Boolean
            Try
                RunActionSP("delIETransactions", "IE", {{"@Table", table, strVar}})
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("IETransactionsLogic", "DeleteTransactions", ex.ToString(), user, WSID)
                Return False
            End Try
            Return True
        End Function
    End Module
End Namespace