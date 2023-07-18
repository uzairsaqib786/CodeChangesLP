' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Admin.Controllers
    <Authorize()>
    Public Class SystemReplenishmentController
        Inherits PickProController

        ' GET: SystemReplenishment
        Function Index() As ActionResult
            Dim countInfo As Object = SystemReplenishment.selReplenCountInfo(Session("WSID"), User.Identity.Name)
            If Not SystemReplenishmentLocationAssignment.ReplenishmentsInProgress Then
                Try
                    RunActionSP("delReplenQueue", Session("WSID"), {{"nothing"}})
                Catch ex As Exception
                    Debug.Print(ex.Message)
                    insertErrorMessages("SystemReplenishmentsController", "Index", ex.Message, User.Identity.Name, Session("WSID"))
                End Try
            End If
            Return View(New With {.pickCount = countInfo.pickcount, .putCount = countInfo.putcount})
        End Function

        ''' <summary>
        ''' Gets the current System Replenishment table
        ''' </summary>
        ''' <param name="data">The table object that contains the needed data to get the table records</param>
        ''' <returns>A table object that contains the needed data to draw the datatable</returns>
        ''' <remarks></remarks>
        Function systRepTable(ByVal data As TableObjectSent) As ActionResult
            If data.searchColumn = Nothing Then
                data.searchColumn = ""
            End If
            If data.searchString = Nothing Then
                data.searchString = ""
            End If
            Return Json(SystemReplenishment.selSystRepTable(data.draw, data.start + 1, data.length + data.start, data.searchString, _
                                                            data.searchColumn, data.sortColumn, _
                                                          data.sortDirection, data.status, data.filter, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the new System Replenishments table
        ''' </summary>
        ''' <param name="data">The table object that contains the needed data to get the data records</param>
        ''' <returns>A table object that contains the needed data to draw the datatable</returns>
        ''' <remarks></remarks>
        Function newReplenishmentOrdersTable(data As TableObjectSent) As ActionResult

            If data.searchColumn = Nothing Then
                data.searchColumn = ""
            End If
            If data.searchString = Nothing Then
                data.searchString = ""
            End If
            Return Json(SystemReplenishment.getNewSystemReplenishmentOrdersTable(data.draw, data.start + 1, data.length + data.start, data.searchString, _
                                                            data.searchColumn, Request.QueryString.Get("order[0][column]"), _
                                                          Request.QueryString.Get("order[0][dir]"), data.reorder, data.filter, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the system replenishments typeahead
        ''' </summary>
        ''' <param name="searchString">The value that will be searched</param>
        ''' <param name="searchCol">The column which the value will be checked under</param>
        ''' <returns>A list of string that contaisn the suggestions</returns>
        ''' <remarks></remarks>
        Function currReplenSearchDropDown(searchString As String, searchCol As String) As ActionResult
            Dim options As New List(Of String)
            Select Case searchCol
                Case "Carsl"
                    searchCol = "Carousel"
            End Select

            If searchCol <> "" Then
                options = SystemReplenishment.selCurrReplenSearchTypeahead(searchString, searchCol, Session("WSID"), User.Identity.Name)
            End If

            Return Json(options, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the typeahead for the search box on the System Replenishment New Orders
        ''' </summary>
        ''' <param name="query">Searched string</param>
        ''' <param name="column">Column to search on</param>
        ''' <returns>A list of string that contains the possible values</returns>
        ''' <remarks></remarks>
        Function getSystemReplenishmentNewTypeahead(query As String, column As String) As ActionResult
            Return Json(SystemReplenishment.getSysReplnNewTypeahead(query, column, Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the typeahead for deleting a range of system replenishments for the ending Batch Pick ID, Pick Location or Put Away Location
        ''' </summary>
        ''' <param name="query">The value to search for</param>
        ''' <param name="delCol">The column which the query will be searched under</param>
        ''' <returns>A list of string that contans the possible suggestions</returns>
        ''' <remarks></remarks>
        Function getDeleteBegin(query As String, delCol As String) As ActionResult
            Return Json(SystemReplenishment.selDeleteRangeBegin(query, delCol, Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the typeahead for deleting a range of system replenishments for the ending Batch Pick ID, Pick Location or Put Away Location
        ''' </summary>
        ''' <param name="query">The value to be searched for</param>
        ''' <param name="begin">Limits the possible  suggestions to those after it</param>
        ''' <param name="delCol">The column which the value will be searched under</param>
        ''' <returns>A list of string that contains the suggestions</returns>
        ''' <remarks></remarks>
        Function getDeleteEnd(query As String, begin As String, delCol As String) As ActionResult
            Return Json(SystemReplenishment.selDeleteRangeEnd(query, delCol, begin, Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function


        ''' <summary>
        ''' Will either print the replenishment label or replenishment order report
        ''' </summary>
        ''' <param name="searchString">The current search string value</param>
        ''' <param name="searchCol">The current search column vlaue</param>
        ''' <param name="status">The current status of the displayed orders</param>
        ''' <param name="ident">Identifier to seperate the label and list</param>
        ''' <returns>A boolean to tell if the print job completed succesfully</returns>
        ''' <remarks></remarks>
        Public Function printReplenishmentReportLabels(searchString As String, searchCol As String, status As String, ident As String, Optional filter As String = "", Optional PrintAll As Integer = 1, Optional Sort As String = "") As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance


            Try
                ' sends print request to the windows service
                Dim filename As String = IIf(ident = "Orders", "ReplenOrder.lst", "Replen.lbl")

                If IsNothing(searchCol) Then
                    searchCol = ""
                Else
                    Select Case searchCol
                        Case "Trans Type"
                            searchCol = "Transaction Type"
                        Case "Carsl"
                            searchCol = "Carousel"
                        Case "Trans Qty"
                            searchCol = "Transaction Quantity"
                        Case "UofM"
                            searchCol = "Unit of Measure"
                        Case "Comp Date"
                            searchCol = "Completed Date"
                    End Select
                End If

                If IsNothing(searchString) Then
                    searchString = ""
                End If
                filter = filter.Replace("[", "[ReplenishmentReport].[").Replace("Trans Type", "Transaction Type").Replace("Carsl", "Carousel").Replace("Trans Qty", "Transaction Quantity").Replace("UofM", "Unit of Measure").Replace("Comp Date", "Completed Date")

                Dim ReportString = If(ident = "Orders", "Replenishment Orders Report", "Replenishment Labels")
                Dim SPName = If(ident = "Orders", "selReplenOrderLL", "selReplenLabelLL")
                Dim Params As String(,)

                If ident = "Orders" Then
                    Params = {{"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, {"@Status", status, strVar}, {"@filter", filter, strVar}}
                Else
                    Params = {{"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, {"@Status", status, strVar}, {"@PrintAll", PrintAll, intVar}, {"@filter", filter, strVar}, {"@Sort", Sort, strVar}}
                End If


                Dim ReportType = If(ident = "Orders", "List", "Label")

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Location Label", ReportType, filename, SPName, Params)
                Clients.Print(m)

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SystemReplenishmentController", "printReplenishmentReportLabels", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints a Replenishment Report (create new replenishments screen print button)
        ''' </summary>
        ''' <param name="reorder">Reorder field, available qty less than replenishment qty</param>
        ''' <returns>A boolean to tell if the print job completed successfully</returns>
        ''' <remarks></remarks>
        Public Function printNewReplenishmentReport(reorder As Boolean) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                ' sends print request to the windows service
                Dim sp As String = "selReplenishmentNewOrdersPrint"
                Dim params As String(,) = {{"@reorder", IIf(reorder, 1, 0), intVar}}
                Dim filename As String = "ReplenList.lst", LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "New Replen Report", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SystemReplenishmentController", "printNewReplenishmentReport", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace