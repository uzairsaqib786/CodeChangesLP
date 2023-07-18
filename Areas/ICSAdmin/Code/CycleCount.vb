' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Admin
    Public Class CycleCount
        ''' <summary>
        ''' Returns records matching filters on Create Cycle Count Page, before being inserted into the ccQueue
        ''' </summary>
        ''' <param name="QueryData">The data to determine which stored procedure gets run</param>
        ''' <param name="user">The user that is logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>Either a string tellingthat an error occurred or a list of list of string containing the table data </returns>
        ''' <remarks></remarks>
        Public Shared Function BatchResultTable(QueryData As BatchResultQuery, user As String, WSID As String) As Object
            Dim returnData As New List(Of List(Of String))

            Dim Datareader As SqlDataReader = Nothing
            Dim CompareString = QueryData.CountType.Replace("#", "").Replace("Div", "")
            Try
                Select Case CompareString
                    Case "LocationRange"
                        Datareader = RunSPArray("selInvMapCountLocationNew", WSID, {{"@FromLocation", QueryData.FromLocation, strVar},
                                                                                 {"@ToLocation", QueryData.ToLocation, strVar},
                                                                                 {"@IncludeEmpty", QueryData.IncludeEmpty, boolVar},
                                                                                 {"@IncludeOther", QueryData.IncludeOther, boolVar}})
                    Case "ItemNumber"
                        Datareader = RunSPArray("selInvMapCountItemNew", WSID, {{"@FromItem", QueryData.FromItem, strVar},
                                                                             {"@ToItem", QueryData.ToItem, strVar}})
                    Case "Description"
                        Datareader = RunSPArray("selInvMapCountDescriptionNew", WSID, {{"@Description", QueryData.Description, strVar}})
                    Case "Category"
                        Datareader = RunSPArray("selInvMapCountCategoryNew", WSID, {{"@Category", QueryData.Category, strVar},
                                                                                 {"@SubCategory", QueryData.SubCategory, strVar}})
                    Case "NotCountedSince"
                        Datareader = RunSPArray("selInvMapCountCountDateNew", WSID, {{"@LastCount", QueryData.NotCounted, dteVar}})

                    Case "PickedRange"
                        Datareader = RunSPArray("selInvMapCountPickedNew", WSID, {{"@FromDate", QueryData.PickStart, dteVar},
                                                                                 {"@ToDate", QueryData.PickEnd, dteVar}})

                    Case "PutAwayRange"
                        Datareader = RunSPArray("selInvMapCountPutAwayNew", WSID, {{"@FromDate", QueryData.PutAwayStart, dteVar},
                                                                                 {"@ToDate", QueryData.PutAwayEnd, dteVar}})
                    Case "CostRange"
                        Datareader = RunSPArray("selInvMapCountCostNew", WSID, {{"@FromCost", QueryData.CostStart, strVar},
                                                                                 {"@ToCost", QueryData.CostEnd, strVar}})
                End Select
                If Datareader.HasRows Then
                    While Datareader.Read
                        If String.IsNullOrEmpty(QueryData.WarehouseFilter) OrElse QueryData.WarehouseFilter = CheckDBNull(Datareader("Warehouse")) Then
                            Dim rowList As New List(Of String)
                            rowList.Add("")
                            rowList.Add(CheckDBNull(Datareader("Inv Map ID")))
                            rowList.Add(CheckDBNull(Datareader("Item Number")))
                            rowList.Add(CheckDBNull(Datareader("Description")))
                            rowList.Add(CheckDBNull(Datareader("Item Quantity")))
                            rowList.Add(CheckDBNull(Datareader("Unit of Measure")))
                            rowList.Add(CheckDBNull(Datareader("Warehouse")))
                            rowList.Add(CheckDBNull(Datareader("Location")))
                            rowList.Add(CheckDBNull(Datareader("Golden Zone")))
                            rowList.Add(CheckDBNull(Datareader("Cell Size")))
                            rowList.Add(CheckDBNull(Datareader("Serial Number")))
                            rowList.Add(CheckDBNull(Datareader("Lot Number")))
                            rowList.Add(CheckDBNull(Datareader("Expiration Date")))
                            returnData.Add(rowList)
                        End If
                    End While
                End If
                Return returnData
            Catch ex As Exception
                insertErrorMessages("Cycle Count", "BatchResultTable", ex.Message, user, WSID)
            End Try

            Return "Error"
        End Function

        ''' <summary>
        ''' Selects Datatable info for ccQueue table of Create Count Batches page
        ''' </summary>
        ''' <param name="draw">If the table is currently being drawn</param>
        ''' <param name="sRow">The first row that is being displayed</param>
        ''' <param name="eRow">The last row that is being displayed</param>
        ''' <param name="sortColumnIndex">The column that is being sorted on</param>
        ''' <param name="sortOrder">The order that the column is being ordered by</param>
        ''' <param name="user">The user that is currently logged on</param>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <returns>A table object that contains the data for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function ccQueueTable(draw As Integer, sRow As Integer, eRow As Integer, sortColumnIndex As Integer, sortOrder As String, user As String, wsid As String) As TableObject
            Dim Datareader As SqlDataReader = Nothing
            Dim datalist As New List(Of List(Of String))
            Dim innerlist As New List(Of String)
            Dim recordsTotal As Integer
            Dim recordsFiltered As Integer
            Try
                Datareader = RunSPArray("selccQueueDT", wsid, {{"@sRow", sRow, intVar}, _
                                                               {"@eRow", eRow, intVar}, _
                                                               {"@sortColumn", CycleCountSortColumn(sortColumnIndex), strVar}, _
                                                               {"@sortOrder", sortOrder, strVar}, _
                                                               {"@WSID", wsid, strVar}})

                Dim index = 0
                While Datareader.HasRows

                    While Datareader.Read()
                        ' recordsTotal/Filtered are for the datatable plugin
                        If index = 0 Then
                            recordsTotal = Datareader(0)
                        ElseIf index = 1 Then
                            recordsFiltered = Datareader(0)
                        Else
                            innerlist.Add("")
                            innerlist.Add(CheckDBNull(Datareader("Inv Map ID")))
                            innerlist.Add(CheckDBNull(Datareader("Item Number")))
                            innerlist.Add(CheckDBNull(Datareader("Description")))
                            innerlist.Add(CheckDBNull(Datareader("Item Quantity")))
                            innerlist.Add(CheckDBNull(Datareader("Unit of Measure")))
                            innerlist.Add(CheckDBNull(Datareader("Warehouse")))
                            innerlist.Add(CheckDBNull(Datareader("Location")))
                            innerlist.Add(CheckDBNull(Datareader("Golden Zone")))
                            innerlist.Add(CheckDBNull(Datareader("Cell Size")))
                            innerlist.Add(CheckDBNull(Datareader("Serial Number")))
                            innerlist.Add(CheckDBNull(Datareader("Lot Number")))
                            innerlist.Add(CheckDBNull(Datareader("Expiration Date")))
                            datalist.Add(innerlist)
                            innerlist = New List(Of String)
                        End If
                        index += 1
                    End While
                    Datareader.NextResult()
                End While
            Catch ex As Exception
                insertErrorMessages("Cycle Count", "ccQueueTable", ex.Message, user, wsid)
            Finally
                If Not IsNothing(Datareader) Then
                    Datareader.Close()
                End If
            End Try
            Return New TableObject(draw, recordsTotal, recordsFiltered, datalist, recordsTotal)
        End Function

        ''' <summary>
        ''' Returns Sort Column name by Index it is displayed on page to users
        ''' </summary>
        ''' <param name="index">The position of the column in the column list</param>
        ''' <returns>The column at the given index</returns>
        ''' <remarks></remarks>
        Public Shared Function CycleCountSortColumn(index As Integer)
            Dim columns = {"Inv Map ID", "Item Number", "Description", "Location Quantity", "Unit of Measure", "Warehouse", "Location", "Velocity Code", "Cell Size", "Serial Number", "Lot Number", "Expiration Date"}
            Return columns(index)
        End Function

        ''' <summary>
        ''' Gets Description Info for Typeahead on Create Cycle Count page
        ''' </summary>
        ''' <param name="desc">The vallue that is currently typed in</param>
        ''' <param name="User">The user that is currently logged on</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of object containing all the descriptiosn that being with the given value</returns>
        ''' <remarks></remarks>
        Public Shared Function CycleCountDescriptionTA(desc As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim descriptions As New List(Of Object)
            Try
                DataReader = RunSPArray("selInvCountDescriptionTA", WSID, {{"@Description", desc + "%", strVar}})

                If DataReader.HasRows() Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            descriptions.Add(New With {.Description = DataReader(x)})
                        Next
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("Cycle Count", "CycleCountDescriptionTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return descriptions
        End Function
        ''' <summary>
        ''' Gets a list of categories and their subcategories for the typeahead
        ''' </summary>
        ''' <param name="category">The value that was inputted inot the text box</param>
        ''' <param name="User">The suer that is logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of object containg the data for the typeahead whose category begins with the given value</returns>
        ''' <remarks></remarks>
        Public Shared Function CycleCountCategoryTA(category As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim categories As New List(Of Object)

            Try
                DataReader = RunSPArray("selInvCountCategoryTA", WSID, {{"@Category", category + "%", strVar}})

                If DataReader.HasRows() Then
                    While DataReader.Read()
                        categories.Add(New With {.Category = DataReader(0), .SubCategory = DataReader(1)})
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("Cycle Count", "CycleCountCategoryTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return categories
        End Function
        ''' <summary>
        ''' Gets the form cost typeahead values. First part of from cost to cost typeahead combination
        ''' </summary>
        ''' <param name="beginCost">The vlaue that is entered</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>List of object containing the data that starts with the given value</returns>
        ''' <remarks></remarks>
        Public Shared Function CycleCountFromCostTA(beginCost As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim fromcost As New List(Of Object)

            Try
                DataReader = RunSPArray("selInvCountFromCostTA", WSID, {{"@FromCost", beginCost + "%", strVar}})

                If DataReader.HasRows() Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            fromcost.Add(New With {.FromCost = DataReader(x)})
                        Next
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("Cycle Count", "CycleCountFromCostTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return fromcost
        End Function
        ''' <summary>
        ''' Gets the to cost typeahead values. Second part of from cost to cost typeahead combination
        ''' </summary>
        ''' <param name="endCost">The piutted value</param>
        ''' <param name="beginCost">The being cost that was entered in order to show only coats that comae after it</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of object containing the data that starts with the given value</returns>
        ''' <remarks></remarks>
        Public Shared Function CycleCountToCostTA(endCost As String, beginCost As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim tocost As New List(Of Object)

            Try
                DataReader = RunSPArray("selInvCountToCostTA", WSID, {{"@FromCost", beginCost, strVar}, _
                                                                      {"@ToCost", endCost + "%", strVar}})

                If DataReader.HasRows() Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            tocost.Add(New With {.ToCost = DataReader(x)})
                        Next
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("Cycle Count", "CycleCountToCostTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return tocost
        End Function
        ''' <summary>
        ''' Selects all the order numbers and their total count in open transactions
        ''' </summary>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of object containing the order number and its number of transactions in OT</returns>
        ''' <remarks></remarks>
        Public Shared Function CycleCountOrderNumbers(User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim ordernums As New List(Of Object)

            Try
                DataReader = RunSPArray("selOTCountOrderNum", WSID, {{"nothing"}})

                If DataReader.HasRows() Then
                    While DataReader.Read()
                        ordernums.Add(New With {.OrderNumber = DataReader(0), .Count = DataReader(1)})
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("Cycle Count", "CycleCountOrderNumTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return ordernums
        End Function
        ''' <summary>
        ''' Selects the datatable data for discrepancy table
        ''' </summary>
        ''' <param name="User">The user that is logged in</param>
        ''' <param name="WSID">The workstation that is being wokred on</param>
        ''' <returns>A list of list of string containing the data for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getCycleCountDiscrepantDT(User As String, WSID As String) As List(Of List(Of String))
            Dim DataReader As SqlDataReader = Nothing
            Dim Disctable As New List(Of List(Of String))
            Dim row As New List(Of String)

            Try
                DataReader = RunSPArray("selCcDiscLeftDT", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            row.Add(CheckDBNull(DataReader(x)))
                        Next
                        row.Insert(2, "")
                        Disctable.Add(row)
                        row = New List(Of String)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Cycle Count", "getCycleCountDiscrepantDT", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Disctable
        End Function
        ''' <summary>
        ''' Gets the desired data for the xfer field map modal
        ''' </summary>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is curently being worked on</param>
        ''' <returns>An object containing the backup, transaction data, and table data for the modal</returns>
        ''' <remarks></remarks>
        Public Shared Function getFieldMapModalData(User As String, WSID As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim TableData As New List(Of List(Of String))
            Dim inners As New List(Of String)
            Dim TransInfo As New List(Of String)
            Dim Backup As String = ""
            Dim index As Integer = 0

            Try
                DataReader = RunSPArray("selXferFeildMapAudit", WSID, {{"nothing"}})

                While DataReader.HasRows
                    While DataReader.Read()
                        If index = 0 Then
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                inners.Add(CheckDBNull(DataReader(x)))
                            Next
                            TableData.Add(inners)
                            inners = New List(Of String)
                        ElseIf index = 1 Then
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                TransInfo.Add(CheckDBNull(DataReader(x)))
                            Next
                        Else
                            Backup = CheckDBNull(DataReader(0))
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Cycle Count", "getFieldMapModalData", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return New With {.Backup = Backup, .TransData = TransInfo, .TableInfo = TableData}

        End Function

    End Class

End Namespace
