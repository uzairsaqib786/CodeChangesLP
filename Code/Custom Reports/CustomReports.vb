' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports combit.ListLabel21

Public Class CustomReports
    ''' <summary>
    ''' Gets the report names for the customreports - basic reports tab
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="WSID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getReportNames(user As String, WSID As String) As List(Of String)
        Dim reader As SqlDataReader = Nothing, reports As New List(Of String)
        Try
            reader = RunSPArray("selLLCustomReports", WSID, {{"@WSID", WSID, strVar}})

            If reader.HasRows Then
                While reader.Read()
                    If Not IsDBNull(reader(0)) Then reports.Add(reader(0))
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReports", "getReportNames", ex.Message, user, WSID)
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try
        Return reports
    End Function

    ''' <summary>
    ''' Gets the Report Titles and field filters for the basic reports tab for the workstation and report specified
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="WSID"></param>
    ''' <param name="report"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getSelectedReportData(user As String, WSID As String, report As String) As List(Of String)
        Dim reader As SqlDataReader = Nothing, details As New List(Of String)
        Try
            reader = RunSPArray("selLLCustomReportDetail", WSID, _
                                {{"@WSID", WSID, strVar}, {"@Report", report, strVar}})

            If reader.HasRows AndAlso reader.Read() Then
                For x As Integer = 0 To reader.FieldCount - 1
                    details.Add(CheckDBNull(reader(x)))
                Next
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReportsHub", "getSelectedReportData", ex.Message, user, WSID)
        Finally
            If Not IsNothing(reader) Then reader.Dispose()
        End Try
        Return details
    End Function

    ''' <summary>
    ''' Gets the datareader's column names for the specified datasource.
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="user"></param>
    ''' <param name="WSID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getReportFields(report As String, user As String, WSID As String) As List(Of String)
        Dim obj As CustomReportsModel = chooseView(report, user, WSID, False)
        Dim reader As SqlDataReader = Nothing

        Try
            reader = RunSPArray("selReportData", WSID, _
                                {{"@WHERE", obj.WHERE, strVar}, {"@ORDERBY", obj.ORDERBY, strVar}, {"@VIEW", obj.REPORTVIEW, strVar}, _
                                 {"@REPORT", obj.reportName, strVar}, {"@WSID", WSID, strVar}})

            Return getDataReadersKnownColumns(reader)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReports", "getReportFields", ex.Message, user, WSID)
        Finally
            If Not IsNothing(reader) Then reader.Dispose()
        End Try
        Return New List(Of String)
    End Function


    ''' <summary>
    ''' Gets Report Data
    ''' </summary>
    ''' <param name="report">Report name to check data for</param>
    ''' <param name="user">User Account current making a request</param>
    ''' <param name="WSID">Unique Identifier for PC</param>
    ''' <param name="preview">Decides if we need to check number of records per page</param>
    ''' <param name="server">Used to map path for report files</param>
    ''' <param name="WHERE">Filter Paramaters to include with query</param>
    ''' <param name="LLType">Either Report or Label</param>
    ''' <param name="isBasicReport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function chooseView(report As String, user As String, WSID As String, preview As Boolean,
                                      Optional server As System.Web.HttpServerUtilityBase = Nothing, Optional WHERE As String = "", Optional LLType As LlProject = LlProject.List,
                                      Optional isBasicReport As Boolean = True) As CustomReportsModel
        Dim view As String = "", orderby As String = "", type As LlProject = LlProject.Card, reportDesign As String = "", userDirectory As String = ""
        Dim actualReportName As String = report, numRecords As Integer = 0

        If isBasicReport Then
            Select Case report.ToLower()
                Case "all reports"
                    view = "vAllReports"
                    orderby = " [ReportName] ASC "
                    type = LlProject.List

                    If WHERE.Trim() = "" Then
                        WHERE = " [WSID] = '" & WSID & "'"
                    Else
                        WHERE += " AND [WSID] = '" & WSID & "'"
                    End If
                Case "carousel batch times"
                    view = "vCarouselBatchTimes"
                    type = LlProject.List
                    orderby = " [Transaction Type], [Location Name], [Completed By] "
                Case "carousel locations cell velocity check"
                    actualReportName = "AllLocationsCellVelocityCheck"
                    view = "vCarouselCellVelocityCheck"
                    type = LlProject.List
                    orderby = ""
                Case "carton flow locations cell velocity check"
                    actualReportName = "AllLocationsCellVelocityCheck"
                    view = "vCartonFlowCellVelocityCheck"
                    orderby = ""
                    type = LlProject.List
                Case "off-carousel locations cell velocity check"
                    actualReportName = "AllLocationsCellVelocityCheck"
                    view = "vBulkCellVelocityCheck"
                    orderby = ""
                    type = LlProject.List
                Case "employees"
                    view = "vEmployees"
                    orderby = ""
                    type = LlProject.List
                Case "reprocess transactions"
                    view = "vReprocessTransactions"
                    orderby = ""
                    type = LlProject.List
                Case "cycle count"
                    view = "vCycleCountReport"
                    orderby = ""
                    type = LlProject.List
                Case "order status"
                    view = "vOrderStatus"
                    orderby = ""
                    type = LlProject.List
                Case "inventory detail"
                    view = "vInventoryDetail"
                    orderby = ""
                    type = LlProject.List
                Case "inventory map"
                    view = "vInventoryMap"
                    orderby = ""
                    type = LlProject.List
                Case "inventory map with user fields"
                    view = "vInventoryMap"
                    orderby = ""
                    type = LlProject.List
                Case "inventory summary"
                    view = "vInventoryDetail"
                    orderby = ""
                    type = LlProject.List
                Case "kit detail"
                    view = "vKitDetail"
                    orderby = ""
                    type = LlProject.List
                Case "location assignment reprocess"
                    view = "Open Transactions Temp"
                    orderby = ""
                    type = LlProject.List
                Case "lot number current quantity"
                    view = "vLotNumberCurrentQuantity"
                    orderby = ""
                    type = LlProject.List
                Case "on hand values"
                    view = "vOnHandValues"
                    orderby = " [Loc Type], [Zone], [Location Name] "
                    type = LlProject.List
                Case "open transactions spreadsheet"
                    view = "Open Transactions"
                    orderby = " [Open Transactions].[Transaction Type], [Open Transactions].[Order Number], [Open Transactions].[Line Number] "
                    type = LlProject.List
                Case "pick shortage report"
                    view = "vPickShortageReport"
                    orderby = " [Warehouse], [Order Number], [Item Number], [SumOfItemQuantity] DESC"
                    type = LlProject.List
                Case "pick counts by zone"
                    view = "vPickCountsByZone"
                    orderby = " [Zone], [Item Number], [Warehouse], [Qty of Picks] DESC"
                    type = LlProject.List
                Case "reels out to manufacturing"
                    view = "vReelsOutToManufacturing"
                    orderby = ""
                    type = LlProject.List
                Case "quarantined items"
                    view = "vInventoryMap"
                    orderby = " [Warehouse], [Zone], [Carousel], [Row], [Shelf], [Bin] "
                    type = LlProject.List
                    ' only want quarantined items
                    If WHERE.Trim() = "" Then
                        WHERE = " [Warehouse] LIKE 'Quarantine%'"
                    Else
                        WHERE += " AND [Warehouse] LIKE 'Quarantine%'"
                    End If
                Case "all locations cell velocity check"
                    view = "vAllLocationsCellVelocityCheck"
                    orderby = " [Location Type] ASC, [Zone] ASC, [Item Number] ASC"
                    type = LlProject.List
                Case "transactions waiting to be cleared"
                    actualReportName = "Transactions Ready to Process"
                    view = "vTransactionsWaitingToBeCleared"
                    orderby = ""
                    type = LlProject.List
                Case "transactions ready to process"
                    view = "vTransactionsReadyToProcess"
                    orderby = "vTransactionsReadyToProcess.[Completed Date] DESC, vTransactionsReadyToProcess.[Completed By] DESC"
                    type = LlProject.List
                Case "transactions by zone summary"
                    view = "vTransactionsByZoneSummary"
                    orderby = " vTransactionsByZoneSummary.Zone,vTransactionsByZoneSummary.Location, vTransactionsByZoneSummary.[Transaction Type], vTransactionsByZoneSummary.[Completed By], vTransactionsByZoneSummary.[Completed Date]"
                    type = LlProject.List
                Case "employee transactions by zone summary"
                    actualReportName = "EmpTransByZone"
                    view = "vEmployeeTransByZoneSummary"
                    orderby = "vEmployeeTransByZoneSummary.[Completed Date] DESC, vEmployeeTransByZoneSummary.Zone, vEmployeeTransByZoneSummary.[Completed By], vEmployeeTransByZoneSummary.[Transaction Type]"
                    type = LlProject.List
                Case "transactions by zone"
                    view = "vTransactionsByZoneSummary"
                    orderby = " vTransactionsByZoneSummary.Zone,vTransactionsByZoneSummary.Location, vTransactionsByZoneSummary.[Transaction Type], vTransactionsByZoneSummary.[Completed By], vTransactionsByZoneSummary.[Completed Date]"
                    type = LlProject.List
                Case "transactions by employee summary"
                    view = "vTransactionsByEmployeeSummaryReport"
                    orderby = " 1, 2"
                    type = LlProject.List
                Case "transactions by employee"
                    view = "vTransactionsByEmployeeSummary"
                    orderby = " 1, 2"
                    type = LlProject.List
                Case "transactions by carousel summary"
                    view = "vTransactionsByCarouselByOperator"
                    orderby = ""
                    type = LlProject.List
                Case "transactions by carousel shelf summary"
                    view = "vTransactionsByCarouselShelfSummary"
                    orderby = ""
                    type = LlProject.List
                Case "transactions by carousel by operator summary"
                    view = "vTransactionsByCarouselByOperator"
                    orderby = ""
                    type = LlProject.List
                Case "transactions by carousel bin summary"
                    view = "vTransactionsByCarouselBinSummary"
                    orderby = ""
                    type = LlProject.List
                Case "transactions by carousel"
                    view = "vTransactionsByZoneSummary"
                    orderby = " vTransactionsByZoneSummary.Zone,vTransactionsByZoneSummary.Location, vTransactionsByZoneSummary.[Transaction Type], vTransactionsByZoneSummary.[Completed By], vTransactionsByZoneSummary.[Completed Date]"
                    type = LlProject.List
                Case "transaction rates summary"
                    view = "vTransactionRatesSummary"
                    orderby = ""
                    type = LlProject.List
                Case "transaction history spreadsheet"
                    view = "Transaction History"
                    orderby = " [Transaction Type], [Order Number], [Line Number]"
                    type = LlProject.List
                Case "transaction history"
                    view = "Transaction History"
                    orderby = " [Transaction Type], [Order Number], [Line Number]"
                    type = LlProject.List
                Case "tote report"
                    view = "vToteReport"
                    orderby = "vToteReport.[Tote ID], vToteReport.[Tote Number], vToteReport.Cell, vToteReport.[Batch Pick ID], vToteReport.[Order Number] DESC"
                    type = LlProject.List
                Case "ship variance"
                    view = "vShipVariance"
                    orderby = ""
                    type = LlProject.List
                Case "replenishment summary"
                    view = "vReplenishmentSummary"
                    orderby = "vReplenishmentSummary.[Location Qty]"
                    type = LlProject.List
                Case "reorder report"
                    view = "vReorderReport"
                    orderby = ""
                    type = LlProject.List
                Case Else
                    insertErrorMessages("CustomReports", "chooseView", "Report not found in chooseView method Select Case block. " & report, user, WSID)
            End Select
        Else
            ' complex report, not basic
            type = IIf(IO.Path.GetExtension(actualReportName) = ".lst", LlProject.List, LlProject.Label)
            actualReportName = IO.Path.GetFileNameWithoutExtension(actualReportName) ' get rid of extension .lbl or .lst
        End If
        Try
            If Not IsNothing(server) Then
                reportDesign = server.MapPath("~\Reports\LLDesign\") _
                    & WSID & "\" & IO.Path.GetFileNameWithoutExtension(actualReportName.Replace(" ", "")) & IIf(type = LlProject.List, ".lst", ".lbl")
                If Not System.IO.File.Exists(reportDesign) Then
                    PrintService.insertPrintLog(1, "Report Design not found", user, WSID,
                                                userDirectory, "chooseView: " & report, IIf(type = 3, 0, type), reportDesign)
                End If
                userDirectory = server.MapPath("~\temp\") & WSID
                If Not System.IO.Directory.Exists(userDirectory) Then
                    System.IO.Directory.CreateDirectory(userDirectory)
                End If
            End If
            Dim recordsPerPageInReport As New Dictionary(Of String, Integer) From {{"all locations cell velocity check", 30},
                                                                                   {"carousel locations cell velocity check", 30},
                                                                                   {"off-carousel locations cell velocity check", 30},
                                                                                   {"all reports", 18}, {"carousel batch times", 7},
                                                                                   {"carton flow locations cell velocity check", 30},
                                                                                   {"employees", 35}, {"reprocess transactions", 2},
                                                                                   {"cycle count", 3}, {"order status", 31}, {"inventory detail", 4},
                                                                                   {"inventory map", 3}, {"inventory map with user fields", 40},
                                                                                   {"inventory summary", 42}, {"kit detail", 30}, {"location assignment reprocess", 3},
                                                                                   {"lot number current quantity", 42}, {"on hand values", 6},
                                                                                   {"open transactions spreadsheet", 1}, {"pick counts by zone", 30},
                                                                                   {"pick shortage report", 50}, {"quarantined items", 45},
                                                                                   {"reels out to manufacturing", 40}, {"reorder report", 40},
                                                                                   {"replenishment summary", 50}, {"ship variance", 20},
                                                                                   {"tote report", 40}, {"transaction history", 5}, {"transaction history spreadsheet", 1},
                                                                                   {"transaction rates summary", 30}, {"transactions by carousel", 30},
                                                                                   {"transactions by employee", 30}, {"transactions by zone", 30},
                                                                                   {"employee transactions by zone summary", 30},
                                                                                   {"transactions by zone summary", 30}, {"transactions ready to process", 6},
                                                                                   {"transactions waiting to be cleared", 5}, {"transactions by carousel bin summary", 30},
                                                                                   {"transactions by employee summary", 30}, {"transactions by carousel by operator summary", 30},
                                                                                   {"transactions by carousel shelf summary", 30}, {"transactions by carousel summary", 30}}

            If recordsPerPageInReport.ContainsKey(report.ToLower().Trim()) And preview Then
                ' restrict select to ~50 pages if it's a preview, otherwise it takes forever to render and there's a lot of space that gets eaten up in memory/file system
                numRecords = recordsPerPageInReport(report.ToLower().Trim()) * 50
            Else
                ' don't restrict it
                numRecords = 0
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReports", "chooseView", ex.Message, user, WSID)
        End Try

        Return New CustomReportsModel(reportDesign, user, userDirectory, WSID, IIf(WSID = "", "", userCS(WSID)),
                                      type, view, orderby, WHERE, actualReportName, IIf(numRecords = 0, "", "TOP " & numRecords))
    End Function

    ''' <summary>
    ''' Builds a where clause for a dynamic select statement for the report filters on the basic reports and labels tab
    ''' </summary>
    ''' <param name="where"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function buildWhereClause(where As List(Of List(Of String))) As String
        If IsNothing(where) Then
            Return ""
        End If
        Dim whereString As String = ""
        Dim legalClause1 As New List(Of String) From {"=", "<", ">", "<=", ">=", "<>", "LIKE", "NOT LIKE", "BETWEEN", "NOT BETWEEN", "IN", "NOT IN"}

        Dim appended As Boolean = False
        For Each clause In where
            If Not IsNothing(clause(0)) And legalClause1.IndexOf(clause(1)) > -1 And clause(2).Trim() <> "" Then
                clause(0) = GlobalFunctions.cleanSearch(clause(0))
                clause(2) = GlobalFunctions.cleanSearch(clause(2))
                clause(3) = GlobalFunctions.cleanSearch(clause(3))
                If (clause(1) = "BETWEEN" Or clause(1) = "NOT BETWEEN") And clause(3).Trim() <> "" Then
                    whereString += IIf(appended, " AND", "") & " [" & clause(0) & "] " & clause(1) & " '" & clause(2) & "' AND '" & clause(3) & "'"
                    appended = True
                ElseIf clause(1) = "LIKE" Or clause(1) = "NOT LIKE" Then
                    whereString += IIf(appended, " AND", "") & " [" & clause(0) & "] " & clause(1) & " '%" & clause(2) & "%'"
                ElseIf clause(1) = "IN" Then
                    whereString += IIf(appended, " AND ", "") & " [" & clause(0) & "] " & clause(1) & " (" & CommaSplitForInClause(clause(2)) & ")"
                    appended = True
                ElseIf clause(1) <> "BETWEEN" And clause(1) <> "NOT BETWEEN" Then
                    whereString += IIf(appended, " AND", "") & " [" & clause(0) & "] " & clause(1) & " '" & clause(2) & "'"
                    appended = True
                End If
            End If
        Next
        Return whereString
    End Function

    ''' <summary>
    ''' Gets the custom reports tab's title/filename pairs.
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="WSID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getCustomReportTitles(user As String, WSID As String, App As String) As Object
        Dim reader As SqlDataReader = Nothing, SysTitles As New List(Of Object), UserTitles As New List(Of Object), Titles As Object
        Try
            reader = RunSPArray("selListsAndLabelsReportNames", WSID, {{"@WSID", WSID, strVar}, {"@App", App, strVar}})

            If reader.HasRows Then
                While reader.Read()
                    If Not IsDBNull(reader(0)) And Not IsDBNull(reader(1)) And Not IsDBNull(reader(2)) Then
                        If reader(2) Then
                            SysTitles.Add(New With {.Title = reader(0), .Filename = reader(1)})
                        Else
                            UserTitles.Add(New With {.Title = reader(0), .Filename = reader(1)})
                        End If
                    End If
                End While
            End If

            Titles = New With {.Sys = SysTitles, .User = UserTitles}
        Catch ex As Exception
            Titles = New With {.Sys = New List(Of Object) From {New With {.Title = "Error", .File = ""}}, .User = New List(Of Object) From {New With {.Title = "Error", .File = ""}}}
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReports", "getCustomReportTitles", ex.Message, user, WSID)
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try

        Return Titles
    End Function

    ''' <summary>
    ''' Gets the report details for the custom reports and labels tab.  Includes test and design data, test/design data type, filename, etc.
    ''' </summary>
    ''' <param name="Filename"></param>
    ''' <param name="user"></param>
    ''' <param name="WSID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getReportDetails(Filename As String, user As String, WSID As String) As List(Of String)
        Dim reader As SqlDataReader = Nothing, details As New List(Of String)

        Try
            reader = RunSPArray("selListsAndLabelsReportDetails", WSID, {{"@WSID", WSID, strVar}, {"@Filename", Filename, strVar}})

            If reader.HasRows Then
                If reader.Read() Then
                    For x As Integer = 0 To reader.FieldCount - 1
                        details.Add(IIf(IsDBNull(reader(x)), "", reader(x)))
                    Next
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CustomReports", "getReportDetails", ex.Message, user, WSID)
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try
        Return details
    End Function

End Class