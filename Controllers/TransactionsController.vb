' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

<Authorize()>
Public Class TransactionsController
    Inherits PickProController
    ''' <summary>
    ''' Returns Transactions page on the selected View (Order Status, Open Trans, Trans History, or Reprocess) with specified Features turned on
    ''' </summary>
    ''' <param name="viewToShow">View to show </param>
    ''' <param name="Location">include this param if you want to show a specific location in open transactions</param>
    ''' <param name="ItemNumber">include this param if you want to show a specific Item Number in open trans</param>
    ''' <param name="Holds">include this param if you want to view hold transactions in reprocess</param>
    ''' <param name="OrderStatusOrder">include this param if you want to display a specific Order number in Order Status</param>
    ''' <returns>The view object that contains the information for the transactions html page</returns>
    ''' <remarks></remarks>ando
    Function Index(Optional viewToShow As Integer = 1, Optional Location As String = "", Optional ItemNumber As String = "", Optional Holds As Boolean = False, Optional OrderStatusOrder As String = "", Optional App As String = "Admin") As ActionResult

        Dim toShow As String = "", currentUser As String = User.Identity.Name, values = New List(Of String), variables = Request.QueryString()
        Select Case viewToShow
            Case 1
                toShow = "Order Status"
                values.AddRange({OpenTransactions.getOldestDate(ItemNumber, User.Identity.Name, Session("WSID")), variables("eDateOpen"), variables("orderNumberOpen"), variables("toteIDOpen"),
                                 variables("transactType"), variables("transactStat")})
            Case 2
                toShow = "Open Transactions"
                If ItemNumber = "" Then
                    values.AddRange({variables("sDateOpen"), variables("eDateOpen"), variables("orderNumberOpen"), variables("toteIDOpen"), variables("transactType"), variables("transactStat")})
                Else
                    values.AddRange({OpenTransactions.getOldestDate(ItemNumber, User.Identity.Name, Session("WSID")), variables("eDateOpen"), variables("orderNumberOpen"), variables("toteIDOpen"),
                                 variables("transactType"), variables("transactStat")})
                End If
            Case 3
                toShow = "Transaction History"
                If ItemNumber = "" And Location = "" Then
                    values.AddRange({variables("sDateTrans"), variables("eDateTrans"), variables("orderNumberTrans"), variables("orderNumberTrans"), variables("eDateTrans"), variables("orderNumberTrans")})
                ElseIf Location <> "" Then
                    values.AddRange({Now.AddYears(-50), variables("eDateTrans"), variables("orderNumberTrans"), variables("orderNumberTrans"), variables("eDateTrans"), variables("orderNumberTrans")})
                Else
                    values.AddRange({TransactionHistory.getOldestDate(ItemNumber, User.Identity.Name, Session("WSID")), variables("eDateTrans"), variables("orderNumberTrans"),
                                     variables("eDateTrans"), variables("orderNumberTrans"), variables("eDateTrans"), variables("orderNumberTrans")})
                End If
            Case 4
                toShow = "Reprocess Transactions"
                values.AddRange({OpenTransactions.getOldestDate(ItemNumber, User.Identity.Name, Session("WSID")), variables("RecToView"), variables("ReasFilt"), variables("OrderNumTemp"), variables("ItemNumTemp"),
                                 variables("ItemNumTemp")})
            Case 5
                toShow = "Reprocessed Transactions"
                values.AddRange({"", "", "", "", "", ""})
            Case Else
                toShow = "Order Status"
                values.AddRange({"", "", "", "", "", ""})
        End Select

        If App = "IE" And (toShow = "Order Status" Or toShow = "Transaction History") Then toShow = "Open Transactions"

        Return View(New TransactionsModel(toShow, GlobalFunctions.getDefaultColumnSequence("Open Transactions", currentUser, Session("WSID")),
                                          GlobalFunctions.getDefaultColumnSequence("Transaction History", currentUser, Session("WSID")),
                                          GlobalFunctions.getDefaultColumnSequence("Open Transactions Temp", currentUser, Session("WSID")),
                                          GlobalFunctions.getDefaultColumnSequence("Reprocessed", currentUser, Session("WSID")),
                                          values, Location, ItemNumber,
                                          New AliasModel(User.Identity.Name, Session("WSID")), UnitOfMeasure.getUoM(User.Identity.Name, Session("WSID")), Warehouse.getWarehouses(User.Identity.Name, Session("WSID")), Holds, OrderStatusOrder, App))
    End Function

    ''' <summary>
    ''' Handles selecting table data for Open Transactions
    ''' </summary>
    ''' <param name="data">Custom DataTables object containing filters.</param>
    ''' <returns>Json object for DataTables plugin.</returns>
    ''' <remarks></remarks>
    Function openTrans(ByVal data As TableObjectSent) As ActionResult
        ' SQL can't handle null/Nothing so check before passing
        If data.ordernum = Nothing Then
            data.ordernum = ""
        End If
        If data.toteid = Nothing Then
            data.toteid = ""
        End If
        If data.searchColumn = Nothing Then
            data.searchColumn = ""
        End If
        If data.searchString = Nothing Then
            data.searchString = ""
        End If

        Return Json(OpenTransactions.updateOpenTransTable(data.draw, data.sDate, data.eDate, data.transType, data.transStatus,
                                                          data.searchString, data.searchColumn,
                                                          data.start + 1, data.length + data.start, data.ordernum, data.toteid,
                                                          Request.QueryString.Get("order[0][column]"),
                                                          Request.QueryString.Get("order[0][dir]"), data.filter, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Handles Transaction History view table.
    ''' </summary>
    ''' <param name="data">Custom DataTables object with filters.</param>
    ''' <returns>Json object with data from Transaction History for DataTables plugin.</returns>
    ''' <remarks></remarks>
    Function transHist(ByVal data As TableObjectSent) As ActionResult
        ' SQL can't handle null/Nothing so check before passing
        If data.ordernum = Nothing Then
            data.ordernum = ""
        End If
        If data.searchColumn = Nothing Then
            data.searchColumn = ""
        End If
        If data.searchString = Nothing Then
            data.searchString = ""
        End If
        Return Json(TransactionHistory.updateTransHistTable(data.draw, data.sDate, data.eDate,
                                                          data.searchString, data.searchColumn,
                                                          data.start + 1, data.length + data.start, data.ordernum,
                                                          Request.QueryString.Get("order[0][column]"),
                                                          Request.QueryString.Get("order[0][dir]"), data.filter, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Handles selecting Order Status table data for DataTables plugin.
    ''' </summary>
    ''' <param name="data">Custom object for DataTables plugin filters.</param>
    ''' <returns>Json object for DataTables plugin.</returns>
    ''' <remarks></remarks>
    Function orderStat(ByVal data As TableObjectSent) As ActionResult
        Dim ident As Integer
        'Sets the identifier in order for the OrderStatusTable function call
        'to display all the data the user requested
        '0 implies just order number
        '1 implies just tote id
        '2 impies both
        '3 implies nothing
        If data.ordernum = "" And data.toteid <> "" Then
            ident = 1
            data.ordernum = ""
        ElseIf data.ordernum <> "" Then
            ident = 0
            data.toteid = ""
        Else
            ident = 3
        End If

        If data.searchColumn = Nothing Then
            data.searchColumn = ""
        End If
        If data.searchString = Nothing Then
            data.searchString = ""
        End If
        Return Json(OrderStatus.OrderStatusTable(data.draw, data.ordernum, data.toteid, data.compdate, ident, data.start + 1, data.length + data.start,
                                                          data.searchString, data.searchColumn,
                                                          data.checkvalue, Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"), data.filter,
                                                         User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Handles typeahead for order numbers
    ''' </summary>
    ''' <param name="query">String to suggest for.</param>
    ''' <returns>Suggestions of order numbers.</returns>
    ''' <remarks></remarks>
    Function nextOrders(query As String)
        Dim Orders As New List(Of Object)
        Orders = OrderStatus.OrderNumberTA(query, User.Identity.Name, Session("WSID"))
        Return Json(Orders, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Handles typeahead for Open Transactions order numbers
    ''' </summary>
    ''' <param name="query">Order Number input.</param>
    ''' <returns>Suggestions for similar orders to the one entered.</returns>
    ''' <remarks></remarks>
    Function nextOrdersOpen(query As String)
        Dim order As New List(Of String)
        If query <> "" Then
            order = OpenTransactions.getNextOrderNumbers(query, User.Identity.Name, Session("WSID"))
        End If

        Return Json(order, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Handles typeahead for Transaction History.
    ''' </summary>
    ''' <param name="query">Order Number from TH input box.</param>
    ''' <returns>Suggestions for similar order numbers in Transaction History.</returns>
    ''' <remarks></remarks>
    Function nextOrdersTrans(query As String)
        Dim order As New List(Of String)
        If query <> "" Then
            order = TransactionHistory.getNextOrderNumbersTrans(query, User.Identity.Name, Session("WSID"))
        End If

        Return Json(order, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Handles typeahead for searching on Transactions views.
    ''' </summary>
    ''' <param name="query">Value to be searched for.</param>
    ''' <param name="table">Table to search in.</param>
    ''' <param name="column">Column to search on.</param>
    ''' <returns>Json list of string with suggestions for typeahead.</returns>
    ''' <remarks></remarks>
    Function nextSuggestionsTrans(query As String, table As Integer, column As String) As ActionResult
        Dim suggestions As New List(Of String)
        Dim DataReader As SqlDataReader = Nothing

        Dim view As String = ""
        Select Case table
            Case 1
                view = "vAllTransactions"
            Case 2
                view = "Open Transactions"
            Case 3
                view = "Transaction History"
            Case 4
                view = "Open Transactions Temp"
            Case 5
                view = "Open Transactions Temp History"
            Case 6
                view = "Reprocessed"
        End Select

        Try
            DataReader = RunSPArray("selTableTA", Session("WSID"), {{"@table", view, strVar}, {"@column", column, strVar}, {"@value", query, strVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    suggestions.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                End While
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Transactions: " & table, "nextSuggestionsTrans", ex.ToString(), User.Identity.Name, Session("WSID"))
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return Json(suggestions, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Returns the datatables instance information
    ''' </summary>
    ''' <param name="data">The infomration required for the datatable</param>
    ''' <returns>The object that contains the required data for the datatables plugin</returns>
    ''' <remarks></remarks>
    Function reprocTrans(data As TableObjectSent) As ActionResult
        If data.ordernum = Nothing Then
            data.ordernum = ""
        End If
        Return Json(ReprocessTransactions.getReprocessTable(data.draw, data.start + 1, data.length + data.start,
                                                        Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"), data.ordernum,
                                                       data.itemNumber, data.hold, data.searchColumn, data.searchString, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Returns the results of the filter that is given for the reprocess table
    ''' </summary>
    ''' <param name="data">The data required for the table to filter</param>
    ''' <returns>An object that contains the infomration needed for the datatable to dispaly the filtered results</returns>
    Function getReprocTransSearchResults(data As TableObjectSent) As ActionResult
        Return Json(ReprocessTransactions.getReprocTransSearchResults(data.draw, data.start + 1, data.length + data.start,
                                                           Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"),
                                                        data.filter, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Returns the datatables instance information about an order
    ''' </summary>
    ''' <param name="data">The object that contains the data for the table function</param>
    ''' <returns>An object that contains the infomration needed for the datatable to display the order number results</returns>
    ''' <remarks></remarks>
    Function reprocHistTrans(data As TableObjectSent) As ActionResult
        If data.ordernum = Nothing Then
            data.ordernum = ""
        End If
        Return Json(ReprocessTransactions.getReprocessHistTable(data.draw, data.start + 1, data.length + data.start,
                                                           Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"), data.ordernum,
                                                       data.itemNumber, data.searchColumn, data.searchString, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Sends a command through the hub to the windows service to print selected orders.
    ''' </summary>
    ''' <param name="orderNumber">Order Number filter for printing.</param>
    ''' <param name="toteID">Tote ID filter for printing.</param>
    ''' <returns>Task to prevent timeouts.</returns>
    ''' <remarks></remarks>
    Public Function printOSReport(orderNumber As String, toteID As String) As ActionResult
        Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
        Try
            Dim identifier As Integer = 2
            If orderNumber = Nothing Then
                orderNumber = ""
            End If
            If toteID = Nothing Then
                toteID = ""
            End If
            If orderNumber <> "" And toteID = "" Then
                identifier = 0
            ElseIf orderNumber = "" And toteID <> "" Then
                identifier = 1
            ElseIf orderNumber = "" And toteID = "" Then
                identifier = 3
            End If



            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Dim sp As String = "selOrderStatusLL"
            Dim params As String(,) = {{"@OrderNum", orderNumber, strVar}, {"@ToteID", toteID, strVar}, {"@Identifier", identifier, intVar}}
            Dim filename As String = "OrderStatus.lst", LLType As String = "List"



            Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Order Status Report", LLType, filename, sp, params)
            Clients.Print(m)

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("TransactionsController", "printOSReport", ex.ToString(), username, WSID)
        End Try
        Return Json(True, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Sends a message to the printing service to print reprocess transactions which fit the filters.
    ''' </summary>
    ''' <param name="ID">Open Transactions Temp ID field for specific entry</param>
    ''' <param name="OrderNumber">Order Number to filter</param>
    ''' <param name="reason">Reason to filter</param>
    ''' <param name="message">Message to filter</param>
    ''' <param name="datestamp">Date to filter</param>
    ''' <returns>A boolean telling if the print was successful</returns>
    ''' <remarks></remarks>
    Public Function printReprocessTransactions(history As Boolean, ID As Integer, OrderNumber As String,
                                               ItemNumber As String, reason As String, message As String, datestamp As String) As ActionResult
        Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
        Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

        Try
            Dim sp As String = "selReprocessForPrint"
            Dim filename As String = "ReprocessTransactions.lst", LLType As String = "List"
            Dim params As String(,) = {{"@ID", ID, intVar}, {"@OrderNumber", OrderNumber, strVar}, {"@ItemNumber", ItemNumber, strVar}, {"@Reason", reason, strVar},
                            {"@Message", message, strVar}, {"@Date", datestamp, strVar}, {"@History", IIf(history, 1, 0), intVar}}


            Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Reprocess Transactions Report", LLType, filename, sp, params)
            Clients.Print(m)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("TransactionsController", "printReprocessTransactions", ex.ToString(), username, WSID)
        End Try
        Return Json(True, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Sends a hub request to the print service to print a cycle count report
    ''' </summary>
    ''' <returns>A boolean telling if the print operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function printCycleCountReport() As ActionResult
        Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
        Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

        Try

            Dim sp As String = "selCycleCountReport"
            Dim params As String(,) = {{"nothing"}}
            Dim filename As String = "CycleCount.lst", LLType As String = "List"

            ' sends print request to the windows service
            Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Cycle Count Report", LLType, filename, sp, params)
            Clients.Print(m)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("OpenTransactionsHub", "printCycleCountReport", ex.ToString(), username, WSID)
        End Try
        Return Json(True, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Returns the datatables instance information
    ''' </summary>
    ''' <param name="data">The information required for the datatable</param>
    ''' <returns>The object that contains the required data for the datatables plugin</returns>
    ''' <remarks></remarks>
    Function reprocedTrans(data As TableObjectSent) As ActionResult
        If data.ordernum = Nothing Then
            data.ordernum = ""
        End If
        Return Json(Reprocessed.getReprocessedTable(data.draw, data.start + 1, data.length + data.start,
                                                        Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"),
                                                    data.searchColumn, data.searchString, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

End Class