' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Public Class OrderStatus
    Public Shared broadcaster As OrderStatusBroadcaster = OrderStatusBroadcaster.Instance()

    ''' <summary>
    ''' Calculates the total number of open and closed transactiosn per location
    ''' </summary>
    ''' <param name="ordernum">The order number that the info will be for.</param>
    ''' <param name="ident">An identifier that will perform different functionality within the stored procedure depending on its value.</param>
    ''' <param name="checkval">Identifies whether to take the order number ot the tote id</param>
    ''' <param name="toteID">The tote id that was given</param>
    ''' <param name="CompDate">If the order number has multple completed dates</param>
    ''' <param name="user">The user that is currently logged in</param>
    ''' <param name="WSID">The workstation that is currently logged in</param>
    ''' <returns>A list of string containg all of the values to be displayed.</returns>
    ''' <remarks></remarks>
    Public Shared Function LineMath(ordernum As String, ident As Integer, checkval As Boolean, toteID As String, user As String, WSID As String, CompDate As String) As List(Of List(Of String))
        Dim DataReader As SqlDataReader = Nothing, total As New List(Of List(Of String)), count As New List(Of String)
        Try
            ' checkVal = tote id checkbox state in html
            ' if tote then
            If checkval Then
                DataReader = RunSPArray("selVAllTransLocZoneCount", WSID, {{"@OrderNumber", "", strVar}, {"@ToteID", toteID, strVar}, _
                                                                           {"@CompDate", Now, dteVar}, {"@Identifier", ident, intVar}})
            ElseIf CompDate <> "" Then
                DataReader = RunSPArray("selVAllTransLocZoneCount", WSID, {{"@OrderNumber", ordernum, strVar}, {"@ToteID", "", strVar}, _
                                                                           {"@CompDate", CompDate, dteVar}, {"@Identifier", ident, intVar}})
            Else
                DataReader = RunSPArray("selVAllTransLocZoneCount", WSID, {{"@OrderNumber", ordernum, strVar}, {"@ToteID", "", strVar}, _
                                                                            {"@BeginDate", minDate, dteVar}, {"@Identifier", ident, intVar}})
            End If

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        count.Add(CheckDBNull(DataReader(x)))
                    Next
                    total.Add(count)
                    count = New List(Of String)
                End While
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Order Status", "LineMath", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return total

    End Function

    ''' <summary>
    ''' This funciton will return all data for the order info display and the order status table
    ''' </summary>
    ''' <param name="ordernum">the currernt order number whose information will be given</param>
    ''' <param name="toteID">the tote id that whose information will be returned</param>
    ''' <param name="ident">An identifier given to the stored procedure to return different reults</param>
    ''' <param name="sRow">The start row</param>
    ''' <param name="eRow">The end row </param>
    ''' <param name="searchString">The search string</param>
    ''' <param name="searchCol">The column for which the string will be searched over</param>
    ''' <param name="checkval">The value to determine if the order number or tote id information is given</param>
    ''' <param name="checkCol">The index for which column was given the sort</param>
    ''' <param name="direct">The direction of the sort. eiher ascending or descending</param>
    ''' <param name="compdate">If the order number has multiple completed dates</param>
    ''' <param name="filter">The filter being applied to the table</param>
    ''' <param name="user">The user that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A tuple containing all the data for the tbale and info parts of the order status screen</returns>
    ''' <remarks></remarks>
    Public Shared Function OrderInfo(ordernum As String, toteID As String, compdate As String, ident As Integer, sRow As Integer, _
                              eRow As Integer, searchString As String, searchCol As String, checkval As Boolean, checkCol As Integer, direct As String, filter As String, user As String, WSID As String) As Tuple(Of String, List(Of List(Of String)), List(Of List(Of String)))

        'This function will return a tuple consisting of a string, and two list of list of strings. One containg the table contents, the other containing the table information 
        'This utilizes the funciton OrderCounter
        'This utlizes the sql stored procedure selTableCols which uses the PREMADE table OrderStatusColTableTable(see below), which has the column sequence set up in PickPro
        'It returns all the columns that are utilized in PickPro and functioanlity. 
        'This also utilizes the stored procedure selOrderStatusDT which uses the tables Open Transactions, Transaction History, and Open Transactions Temp
        'Depending on the inputted identifier it will perfomr a uniion across all tables, or just the Open Transactions and Transaction History tables 
        'This stored procedure will return all of the information contianed in those tables for a selected order number, tote id, or both in this particular function 
        'The last stored procedure utilized is createTableForPickPro. The stored procedure returns nothing, and just creates OrderStatusColTableTable which contains the exact(correct order) row schema for 
        'the order status table. It is utilized in order ot grab the column that is being ordered by ascending or descending, and give this value to the table display call
        'This function takes this information all stored procedures and reads them, in order to get the values returned from the stored procedures
        'Once it has the information and read the table it will give the order type, total lines, current status, completed lines, open lines, and re-process lines 

        'Variable declarations
        Dim tableData As New List(Of List(Of String)), data As New List(Of List(Of String)), Values As New List(Of String), transType As String = "", totalLines As Integer = 0
        Dim status As String = "", compLines As Integer = 0, opLines As Integer = 0, reLines As Integer = 0, ordNumber As String = "", DataRead As SqlDataReader = Nothing
        Dim DataReads As SqlDataReader = Nothing, ColVal As String = "", count As Integer = 0, index As Integer = 0, allTransType As New List(Of String)


        'This try will grab the column schema from OrderStatusColTableTable, in order to  find the column that the user is trying to order on as it has the exact same schema as the selects in the selOrderStatusDT 
        'stored procedure
        Try

            'do replace here for filter. pass filter to stored procedure->done. use filter in sp
            filter = filter.Replace("Type", "Transaction Type").Replace("Batch Id", "Batch Pick ID")
            'IMPORTANT!!!!!!!!!!!!!!!!!: OrderStatusColTableTable is a premade table that has the same column order as the unions of Open Transactions, Transaction History, Open Transaction temp
            DataRead = RunSPArray("selTableCols", WSID, {{"@tableName", "OrderStatusColTable", strVar}})
            'IMPORTANT!!!!!!!!!!!!!!!!!: OrderStatusColTableTable is a premade table that has the same column order as the unions of Open Transactions, Transaction History, Open Transaction temp 

            If DataRead.HasRows Then
                While DataRead.Read()
                    'checks count is equal to the index of the column that is being ordered. If snot increment count 
                    If count = checkCol Then
                        ColVal = DataRead(0)
                    End If
                    count += 1
                End While

            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Order Status", "OrderInfo", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataRead) Then
                DataRead.Dispose()
            End If
        End Try

        'This try will grab all the information to be displayed in the table, by integrating th esearch functions which will filter what is being returned
        'This is done by reading over the output from the called stored procedure 
        Try
            If compdate <> "" Then
                DataReads = RunSPArray("selOrderStatusDT", WSID, {{"@OrderNum", ordernum, strVar}, {"@ToteID", toteID, strVar}, {"@CompDate", compdate, dteVar}, _
                                                                  {"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, _
                                                                  {"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, _
                                                                  {"@Identifier", ident, intVar}, {"@CountIdent", 0, intVar}, {"@sortColumn", ColVal, strVar}, {"@sortOrder", direct, strVar}, _
                                                                  {"@filter", filter, strVar}})
            Else
                DataReads = RunSPArray("selOrderStatusDT", WSID, {{"@OrderNum", ordernum, strVar}, {"@ToteID", toteID, strVar}, _
                                                                  {"@BeginDate", minDate, dteVar}, {"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, _
                                                                  {"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, _
                                                                  {"@Identifier", ident, intVar}, {"@CountIdent", 0, intVar}, {"@sortColumn", ColVal, strVar}, {"@sortOrder", direct, strVar}, _
                                                                  {"@filter", filter, strVar}})
            End If


            While DataReads.HasRows
                While DataReads.Read()
                    If index = 0 Then
                        Dim row As New List(Of String)
                        If Not allTransType.Contains(DataReads.Item("Transaction Type")) Then
                            allTransType.Add(DataReads.Item("Transaction Type"))
                        End If
                        If ordNumber = "" Then
                            ordNumber = DataReads.Item("Order Number")
                        End If

                        For x As Integer = 0 To DataReads.FieldCount - 1
                            If Not IsDBNull(DataReads(x)) Then
                                row.Add(DataReads(x))
                            Else
                                row.Add("")
                            End If
                        Next
                        tableData.Add(row)
                    ElseIf index = 1 Then
                        opLines = DataReads(0)
                    ElseIf index = 2 Then
                        compLines = DataReads(0)
                    ElseIf index = 3 Then
                        reLines = DataReads(0)
                    ElseIf index = 4 Then
                        totalLines = DataReads(0)
                    Else
                        totalLines = -1
                        compLines = -1
                        opLines = -1
                        reLines = -1
                    End If
                End While
                index += 1
                DataReads.NextResult()
            End While

            'Sets the status based upon if certain lines exist within an order 
            'Checks the values of the toal number of each type of line and compares on them to set status 
            If reLines <> 0 Then
                status = "In Process/ BO"
            ElseIf opLines <> 0 Then
                status = "In Process"
            ElseIf compLines <> 0 Then
                status = "Complete"
            End If


            If allTransType.Count > 1 Then
                transType = "Multiple"
            ElseIf allTransType.Count <> 0 Then
                transType = allTransType(0)
            Else
                transType = "None"
            End If
            Values.Add(transType)
            Values.Add(Convert.ToString(totalLines))
            Values.Add(status)
            Values.Add(Convert.ToString(compLines))
            Values.Add(Convert.ToString(opLines))
            Values.Add(Convert.ToString(reLines))
            Values.Add(ordNumber)
            data.Add(Values)


        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Order Status", "OrderInfo", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReads) Then
                DataReads.Dispose()
            End If
        End Try

        Return New Tuple(Of String, List(Of List(Of String)), List(Of List(Of String)))(Convert.ToString(totalLines), tableData, data)
    End Function

    ''' <summary>
    ''' Returns the total number of rows available for display with the given parameters
    ''' </summary>
    ''' <param name="ordernum">The order number to be filtered</param>
    ''' <param name="toteID">The tote id to be filtered</param>
    ''' <param name="ident">The identifier to determine which data is returned in the stored procedure</param>
    ''' <param name="sRow">the start row</param>
    ''' <param name="eRow">The end row</param>
    ''' <param name="searchString">The search string</param>
    ''' <param name="searchCol">The column that will be searched for the search string</param>
    ''' <param name="compdate">If the selected oder number has multiple completed dates</param>
    ''' <param name="filter">The filter being applied ot the table</param>
    ''' <param name="user">The user that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A count of the total number of records returned</returns>
    ''' <remarks></remarks>
    Public Shared Function OrderCounter(ordernum As String, toteID As String, ident As Integer, sRow As Integer, _
                              eRow As Integer, searchString As String, searchCol As String, user As String, WSID As String, compdate As String, filter As String) As Object
        'This function will return integer contining the total number of transactions (lines) that would be displayed in the table
        'This utlizes the sql stored procedure selOrderStatusDT, with the count identifier set to one.
        'This tells the store proceudre to run a COUNT on the table which would originanally be returned 
        Dim filtResult As Integer = -1
        Dim totalResult As Integer = -1
        Try
            Dim filtParms As String(,) = {{"nothing"}}
            Dim totalParms As String(,) = {{"nothing"}}
            If compdate <> "" Then
                filtParms = {{"@OrderNum", ordernum, strVar}, {"@ToteID", toteID, strVar}, {"@CompDate", compdate, dteVar}, _
                                                                   {"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, _
                                                                   {"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, _
                                                                   {"@Identifier", ident, intVar}, {"@CountIdent", 1, intVar}, {"@sortColumn", "", strVar}, {"@sortOrder", "", strVar}, _
                            {"@filter", filter, strVar}}
                totalParms = {{"@OrderNum", ordernum, strVar}, {"@ToteID", toteID, strVar}, {"@CompDate", compdate, dteVar}, _
                                                                   {"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, _
                                                                   {"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, _
                                                                   {"@Identifier", ident, intVar}, {"@CountIdent", 1, intVar}, {"@sortColumn", "", strVar}, {"@sortOrder", "", strVar}, _
                            {"@filter", "1=1", strVar}}
            Else
                filtParms = {{"@OrderNum", ordernum, strVar}, {"@ToteID", toteID, strVar}, _
                                                                   {"@BeginDate", minDate, dteVar}, {"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, _
                                                                   {"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, _
                                                                   {"@Identifier", ident, intVar}, {"@CountIdent", 1, intVar}, {"@sortColumn", "", strVar}, {"@sortOrder", "", strVar}, _
                            {"@filter", filter, strVar}}
                totalParms = {{"@OrderNum", ordernum, strVar}, {"@ToteID", toteID, strVar}, _
                                                                   {"@BeginDate", minDate, dteVar}, {"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, _
                                                                   {"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, _
                                                                   {"@Identifier", ident, intVar}, {"@CountIdent", 1, intVar}, {"@sortColumn", "", strVar}, {"@sortOrder", "", strVar}, _
                            {"@filter", "1=1", strVar}}
            End If
            filtResult = GetResultSingleCol("selOrderStatusDT", WSID, filtParms)
            totalResult = GetResultSingleCol("selOrderStatusDT", WSID, totalParms)
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Order Status", "OrderCounter", ex.Message, user, WSID)
        End Try
        Return New With {.Filter = filtResult, .Total = totalResult}
    End Function

    ''' <summary>
    ''' Grabs the orders for the ordernumber type ahead
    ''' </summary>
    ''' <param name="ordernum">The ordernumber that is currently typed in</param>
    ''' <param name="user">The user that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of all ordernumbers starting with the inputted order number</returns>
    ''' <remarks></remarks>
    Public Shared Function OrderNumberTA(ordernum As String, user As String, WSID As String) As List(Of Object)
        Dim datareader As SqlDataReader = Nothing, OrderDrop As New List(Of Object)

        Try
            datareader = RunSPArray("selOrderStatusTA", WSID, {{"@OrderNumber", ordernum + "%", strVar}})
            If datareader.HasRows Then
                While datareader.Read()
                    OrderDrop.Add(New With {.ordernum = CheckDBNull(datareader(0)), .compdate = CStr(CheckDBNull(datareader(1)))})
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Order Status", "GrabAllTheOrders", ex.Message, user, WSID)
        Finally
            If Not IsNothing(datareader) Then datareader.Dispose()
        End Try
        Return OrderDrop
    End Function

    ''' <summary>
    ''' This function calls the required fucntions to grab the inofrmation for the order status page 
    ''' </summary>
    ''' <param name="draw">DataTable parameter for keeping requests current and in sync</param>
    ''' <param name="ordernum">The order number whose data will be displayed</param>
    ''' <param name="toteid">The tote id whose inforation will be displayed</param>
    ''' <param name="ident">An identifier to change what the stored procedure returns</param>
    ''' <param name="sRow">The start row</param>
    ''' <param name="eRow">The end row</param>
    ''' <param name="searchString">The search string</param>
    ''' <param name="searchCol">The column to search for the search string</param>
    ''' <param name="checkval">This determines if the tote id or order number info is returned</param>
    ''' <param name="checkCol">The current column which needs to be ordered</param>
    ''' <param name="direct">The direction of the order</param>
    ''' <param name="currentUser">The current user that signed in</param>
    ''' <param name="compdate">If the order number has multiple completed dates</param>
    ''' <param name="filter">The filter applied ot the table</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A table object containg the informationf or the order stat table</returns>
    ''' <remarks></remarks>
    Public Shared Function OrderStatusTable(draw As Integer, ordernum As String, toteid As String, compdate As String, ident As Integer, sRow As Integer,
                                            eRow As Integer, searchString As String, searchCol As String, checkval As Boolean, checkCol As Integer,
                                            direct As String, filter As String, currentUser As String, WSID As String) As TableObject
        Dim orderstat As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Dim info As Tuple(Of String, List(Of List(Of String)), List(Of List(Of String)))
        Dim totalLines As Integer = 0
        Dim filterlines As Integer = 0

        'Ident is 3 when no valid combination is entred from transactions controller
        If ident = 3 Then
            Return orderstat
        End If

        Dim other As OrderStatInfo, mathsON As List(Of List(Of String)), mathsOff As List(Of List(Of String)), orderType As String = "", currStatus As String = ""
        Dim compLines As String = 0, opLines As String = 0, relInes As String = 0, lines As String = "", OrderNumber As String = "", toteNum As String = ""  'toteID

        ordernum = GlobalFunctions.DangerAhead(ordernum)
        toteid = GlobalFunctions.DangerAhead(toteid)
        searchString = GlobalFunctions.cleanSearch(searchString)

        Dim tableLines As Object = OrderCounter(ordernum, toteid, ident, sRow, eRow, searchString, searchCol, currentUser, WSID, compdate, filter)
        totalLines = tableLines.Total
        filterlines = tableLines.Filter

        If totalLines > 0 Then
            info = OrderInfo(ordernum, toteid, compdate, ident, sRow, eRow, searchString, searchCol, checkval, checkCol, direct, filter, currentUser, WSID)
            orderType = info.Item3(0)(0)
            lines = info.Item3(0)(1)
            currStatus = info.Item3(0)(2)
            compLines = info.Item3(0)(3)
            opLines = info.Item3(0)(4)
            relInes = info.Item3(0)(5)
            OrderNumber = info.Item3(0)(6)
            toteNum = toteid
            mathsON = LineMath(OrderNumber, 1, False, toteid, currentUser, WSID, compdate)
            mathsOff = LineMath(OrderNumber, 0, False, toteid, currentUser, WSID, compdate)
            other = New OrderStatInfo(type:=orderType, totalLines:=lines, status:=currStatus, compLines:=compLines, opLines:=opLines,
                                      reLines:=relInes, offData:=mathsOff, onData:=mathsON, orderNumber:=OrderNumber, toteID:=toteNum)
            ' update the client with new count data
            broadcaster.Update(other, currentUser)
            ' set the new table data to return
            orderstat = New TableObject(draw:=draw, recordsTotal:=(totalLines), recordsFiltered:=(filterlines), _
                                        data:=info.Item2)

        End If

        Return orderstat
    End Function

    ''' <summary>
    ''' Checks to see if the current App has access to Only Order Status or all of Transaction Journal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function isOrderStatusOnly(app As String)
        Select Case app
            Case "CM"
                Return True
            Case Else
                Return False
        End Select
    End Function
End Class
