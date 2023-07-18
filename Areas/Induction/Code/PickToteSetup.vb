' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports PickPro_Web
Namespace Induction
    Public Class PickToteSetup

        ''' <summary>
        ''' Gets the count of transactiosn that are mixed, on a carousel, and off a carousel
        ''' </summary>
        ''' <param name="WSID">Workstation id of current workstation</param>
        ''' <param name="User">user currently logged in</param>
        ''' <returns>Object containing the count data for the three types</returns>
        ''' <remarks></remarks>
        Public Shared Function getCountInfo(WSID As String, User As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim CountData As New Object

            Try
                DataReader = RunSPArray("selOTPickToteSetupCount", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        CountData = New With {.Mixed = DataReader("Mixed"), .Car = DataReader("Car"), .OffCar = DataReader("OffCar")}
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("PickToteSetup", "getCountInfo", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return CountData
        End Function

        ''' <summary>
        ''' Selects the filters whose names beginw ith the inpput value for the typeahead
        ''' </summary>
        ''' <param name="Filter">The typeheads value</param>
        ''' <param name="User">User logged in</param>
        ''' <param name="WSID">workstation id</param>
        ''' <returns>List of objects containg the filters</returns>
        ''' <remarks></remarks>
        Public Shared Function selectFilterTA(Filter As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim ListFilts As New List(Of Object)

            Try
                DataReader = RunSPArray("selPickBatchFilterTA", WSID, {{"@WSID", WSID, strVar}, {"@Filter", Filter, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        ListFilts.Add(New With {.Filter = DataReader("Batch Description")})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("PickToteSetup", "selectFilterTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return ListFilts
        End Function

        ''' <summary>
        ''' Selects the data for pick tote batch manager transaction table
        ''' </summary>
        ''' <param name="draw">whtehr table is beign drawn</param>
        ''' <param name="OrderNumbers">list of ordernnumbers that re begin displayed</param>
        ''' <param name="startRow">row to start on</param>
        ''' <param name="endRow">row to end on</param>
        ''' <param name="sortCol">column index that is being sorted on</param>
        ''' <param name="sortOrder">the order that the column is bieng sorted on</param>
        ''' <param name="filter">the filter that is currently applied to the table</param>
        ''' <param name="User">current user that is logged in</param>
        ''' <param name="WSID">workstation id</param>
        ''' <returns>object containg all the necessary table components</returns>
        ''' <remarks></remarks>
        Public Shared Function selectPickToteManTransTable(draw As Integer, OrderNumbers As String, startRow As Integer, endRow As Integer, sortCol As Integer, _
                                                       sortOrder As String, filter As String, User As String, WSID As String) As TableObject
            Dim columnSequence As New List(Of String) From {"Order Number", "Item Number", "Transaction Quantity", "Location", "Completed Quantity", "Description", _
                                                            "Import Date", "Priority", "Required Date", "Line Number", "Line Sequence", "Serial Number", "Lot Number", _
                                                            "Expiration Date", "Completed Date", "Completed By", "Batch Pick ID", "Unit of Measure", "User Field1", _
                                                            "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8", _
                                                            "User Field9", "User Field10", "Revision", "Tote ID", "Tote Number", "Cell", "Host Transaction ID", _
                                                            "ID", "Zone", "Carousel", "Row", "Shelf", "Bin", "Warehouse", "Inv Map ID", "Import By", "Import Filename", _
                                                            "Notes", "Emergency", "Master Record", "Master Record ID", "Export Batch ID", "Export Date", "Exported By", "Status Code"}
            Dim column As String = columnSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim Data As New List(Of List(Of String))
            Dim RecordCount As Integer = 0


            Try

                DataReader = RunSPArray("selOTPickToteManTransDT", WSID, {{"@OrderNums", OrderNumbers, strVar}, _
                                                                          {"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                          {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                          {"@filter", filter, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        For Each col In columnSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        Data.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("PickToteSetup", "selectPickToteManTransTable", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, RecordCount, Data.Count, Data)
        End Function

        ''' <summary>
        ''' Selects order numbers for the in zone order typeahead
        ''' </summary>
        ''' <param name="Filter">The starting value</param>
        ''' <param name="User">User that is logged in</param>
        ''' <param name="WSID">Workstation that is being worked on</param>
        ''' <returns>A list of object that contains the orders that begin with the filter</returns>
        Public Shared Function SelectOrderNumberTA(Filter As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim OrderFilts As New List(Of Object)

            Try
                DataReader = RunSPArray("selPickBatchOrdersTA", WSID, {{"@OrderVal", Filter, strVar}, {"@WSID", WSID, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        OrderFilts.Add(New With {.Order = DataReader("Order Number")})
                    End While
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("PickToteSetup", "selectPickToteManTransTable", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return OrderFilts
        End Function

        ''' <summary>
        ''' Selects batch ids for the in zone batch typeahead
        ''' </summary>
        ''' <param name="Filter">The starting value</param>
        ''' <param name="User">User that is logged in</param>
        ''' <param name="WSID">Workstation that is being worked on</param>
        ''' <returns>A list of object that contains the batches that begin with the filter</returns>
        Public Shared Function SelectBatchPickIDTA(Filter As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim BatchFilts As New List(Of Object)

            Try
                DataReader = RunSPArray("selOTPickBatchesTA", WSID, {{"@BatchVal", Filter, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        BatchFilts.Add(New With {.BatchID = DataReader("Batch Pick ID")})
                    End While
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("PickToteSetup", "SelectBatchPickIDTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return BatchFilts
        End Function


        Public Shared Function SelectWSZones(User As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Dim Zones As New List(Of String)

            Try
                DataReader = RunSPArray("selIMWorkstationPickZones", WSID, {{"@WSID", WSID, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        Zones.Add(DataReader("Zone"))
                    End While
                End If

            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("PickToteSetupHub", "selectWSPickZones", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Zones
        End Function

        ''' <summary>
        ''' Selects the table data for the order number and in zone preference
        ''' </summary>
        ''' <param name="draw">Tells if table being drawn</param>
        ''' <param name="OrderNumbers">The order numbers to display data for</param>
        ''' <param name="startRow">The paginiation start</param>
        ''' <param name="endRow">The pagination end</param>
        ''' <param name="sortCol">The column that is being sorted by</param>
        ''' <param name="sortOrder">The direction that the sort is by</param>
        ''' <param name="filter">Any filters that are applied ot the table</param>
        ''' <param name="User">User that is logged in</param>
        ''' <param name="WSID">Worktation being worked on</param>
        ''' <returns>Table object that contains info needed for datatable</returns>
        Public Shared Function selectInZoneTransDT(draw As Integer, OrderNumbers As String, startRow As Integer, endRow As Integer, sortCol As Integer,
                                                       sortOrder As String, filter As String, User As String, WSID As String) As TableObject
            Dim columnSequence As New List(Of String) From {"Order Number", "Item Number", "Transaction Quantity", "Location", "Completed Quantity", "Description",
                                                            "Import Date", "Priority", "Required Date", "Line Number", "Line Sequence", "Serial Number", "Lot Number",
                                                            "Expiration Date", "Completed Date", "Completed By", "Batch Pick ID", "Unit of Measure", "User Field1",
                                                            "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8",
                                                            "User Field9", "User Field10", "Revision", "Tote ID", "Tote Number", "Cell", "Host Transaction ID",
                                                            "ID", "Zone", "Carousel", "Row", "Shelf", "Bin", "Warehouse", "Inv Map ID", "Import By", "Import Filename",
                                                            "Notes", "Emergency", "Master Record", "Master Record ID", "Export Batch ID", "Export Date", "Exported By", "Status Code"}
            Dim column As String = columnSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim Data As New List(Of List(Of String))
            Dim RecordCount As Integer = 0


            Try

                DataReader = RunSPArray("selOTInZoneTransDT", WSID, {{"@OrderNums", OrderNumbers, strVar}, {"@WSID", WSID, strVar},
                                                                     {"@sRow", startRow, intVar}, {"@eRow", endRow, intVar},
                                                                     {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar},
                                                                     {"@filter", filter, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        For Each col In columnSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        Data.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("PickToteSetup", "selectPickToteManTransTable", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, RecordCount, Data.Count, Data)
        End Function

    End Class
End Namespace

