' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports Microsoft.AspNet.SignalR
Imports System.IO
Imports combit.ListLabel21

Public Class ServiceTest
    ''' <summary>
    ''' Enumeration used to determine which kind of [Output Type] we should consider from [Lists and Labels]
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ServiceTestOutputType
        Custom = 1
        System = 2
        Both = 3
    End Enum
    ''' <summary>
    ''' Enumeration used to determine whether a test should include printing, exporting or both simultaneously.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ServiceTestPrintExport
        Print = 1
        Export = 2
        Both = 3
    End Enum

    ''' <summary>
    ''' Sends a series of test export/print requests based on user choice to the service and returns the immediate results of the request.
    ''' </summary>
    ''' <param name="type">Custom, System or both types of reports</param>
    ''' <param name="AppName">Application Name ("IM", "WM", "Admin", etc. or "all")</param>
    ''' <param name="AllWSIDs">WSID = "DEFAULT" or use every WSID</param>
    ''' <param name="exportType">Directly print, export to file, or do both.</param>
    ''' <param name="printers">List of the printers to be included in this test.  Has no effect if exportType is not Print or Both</param>
    ''' <param name="printerIsLabel">List of the printer types (matched by index to printers) -> True = Label Printer, else List/Report Printer</param>
    ''' <returns>New With {.ExpectedResults = List(of TestPrintResult), .Errors = List(of Object[New With {.Message = ex.ToString(), .Type = ex.GetType(), .sp = Stored Procedure/SQL, .file = file w/out path, .area = failure line #}])}</returns>
    ''' <remarks></remarks>
    Public Shared Function Test(type As ServiceTestOutputType, Context As Hubs.HubCallerContext, ByRef Clients As Object, AppName As String, AllWSIDs As Boolean, _
                                exportType As ServiceTestPrintExport, printers As List(Of String), printerIsLabel As List(Of Boolean)) As Object
        Dim reader As SqlDataReader = Nothing, user As String = Context.User.Identity.Name, WSID As String = Context.QueryString.Get("WSID")
        Dim Errors As New List(Of Object)
        Dim ExportID As Integer = 0
        Dim ExpectedResults As New List(Of TestPrintResult)
        Try
            Dim location As HttpServerUtilityBase = Context.Request.GetHttpContext.Server
            Dim userDirectory = location.MapPath("~\temp\") & WSID & "\ServiceTest"
            reader = RunSPArray("selListLabelTest", WSID, {{"@SystemCustomBoth", type, intVar}, {"@Application", AppName, strVar}, {"@AllWSIDs", CastAsSqlBool(AllWSIDs), intVar}})
            ' checkedformats/checkedallprinters are used to minimize impact of the export process by only testing an export/print on every format/printer once
            Dim checkedFormats As Boolean = False, checkedAllPrinters As Boolean = False
            If reader.HasRows Then
                While reader.Read
                    Dim sp As String = "", filename As String = "", params As String(,), line As Integer = 0
                    Try
                        filename = CheckDBNull(reader("File Name"))
                        If reader("Test Data Type") = "SQL" Then
                            sp = "selListsAndLabelsDSSQLString"
                            params = {{"@SQLString", reader("Test Data"), strVar}}
                            line = 1
                        Else ' stored procedure is stored here
                            sp = reader("Test Data")
                            params = {{"nothing"}}
                            line = 2
                        End If

                        If exportType = ServiceTestPrintExport.Both Or exportType = ServiceTestPrintExport.Export Then
                            TestExport(checkedFormats, Clients, Context, reader("WSID"), filename, sp, params, ExpectedResults, ExportID)
                            line = 3
                            checkedFormats = True
                        End If
                        If exportType = ServiceTestPrintExport.Both Or exportType = ServiceTestPrintExport.Print Then
                            TestPrint(checkedAllPrinters, printers, printerIsLabel, Clients, Context, reader("Description"), filename, reader("WSID"), sp, params)
                            checkedAllPrinters = True
                            line = 4
                        End If
                    Catch ex As Exception
                        Errors.Add(New With {.Message = ex.ToString(), .Type = ex.GetType(), .sp = sp, .file = filename, .area = line})
                    End Try
                End While
            End If
        Catch ex As Exception
            Debug.Print(ex.ToString())
            insertErrorMessages("ServiceTest", "Test", ex.ToString(), user, WSID)
            Errors.Add(New With {.Message = ex.ToString(), .Type = ex.GetType(), .sp = "", .file = "", .area = -1})
        End Try
        Return New With {.Errors = Errors, .ExpectedResults = ExpectedResults}
    End Function

    ''' <summary>
    ''' Gets the standard properties for exporting
    ''' </summary>
    ''' <param name="Context"></param>
    ''' <param name="format"></param>
    ''' <param name="spname"></param>
    ''' <param name="params"></param>
    ''' <param name="filename"></param>
    ''' <param name="WSID"></param>
    ''' <param name="em"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStandardLLExportProperties(Context As Hubs.HubCallerContext, format As String, spname As String, params As String(,), filename As String, WSID As String, _
                                                          Optional em As CustomReportsModel = Nothing) As ExportServiceModel
        Dim user As String = Context.User.Identity.Name
        Dim location As HttpServerUtilityBase = Context.Request.GetHttpContext.Server
        Dim userDirectory = location.MapPath("~\temp\") & WSID & "\ServiceTest"
        Dim projectFile = location.MapPath("~\Reports\LLDesign\") & IIf(WSID = "DEFAULT", "", WSID & "\") & filename ' default WSID doesn't have a folder, it goes from the main folder
        Dim pfilename = Path.GetFileNameWithoutExtension(projectFile) & ListLabelHelperFunctions.GetExportExtension(format)
        Dim cs As String
        If WSID = "DEFAULT" Then
            cs = Config.getConnectionString(Context.QueryString.Get("WSID"), Nothing)
        Else
            cs = Config.getConnectionString(WSID, Nothing)
        End If
        If IsNothing(em) Then
            Return New ExportServiceModel(format, userDirectory, Path.Combine(userDirectory, pfilename), projectFile, spname, params, cs, WSID, user)
        Else
            Return New ExportServiceModel(format, userDirectory, Path.Combine(userDirectory, pfilename), projectFile, spname, params, Config.getConnectionString(WSID, Nothing), WSID, user, em)
        End If
    End Function

    ''' <summary>
    ''' Exports a file via a service invocation
    ''' </summary>
    ''' <param name="checkedFormats"></param>
    ''' <param name="Clients"></param>
    ''' <param name="Context"></param>
    ''' <param name="WSID"></param>
    ''' <param name="filename"></param>
    ''' <param name="sp"></param>
    ''' <param name="params"></param>
    ''' <param name="ExpectedResults"></param>
    ''' <param name="ExportID"></param>
    ''' <remarks></remarks>
    Shared Sub TestExport(checkedFormats As Boolean, ByRef Clients As Object, Context As Hubs.HubCallerContext, WSID As String, filename As String, sp As String, params As String(,), _
                          ByRef ExpectedResults As List(Of TestPrintResult), ByRef ExportID As Integer)
        Dim model As ExportServiceModel = Nothing
        If Not checkedFormats Then
            For Each f As String In {"xls", "xlsx", "html", "pdf", "rtf", "txt", "xml", "xps", "preview"}
                ExportID += 1
                model = ServiceTest.GetStandardLLExportProperties(Context, f, sp, params, filename, WSID)
                model.ExportID = ExportID
                Clients.Group(PrintService.PrintServiceHubName).TestExportReport(model)
                model.ExportPath = IO.Path.GetFileName(model.ExportPath)
                ExpectedResults.Add(New TestPrintResult("Export (" & f & ")", IIf(model.ProjectFile.EndsWith(".lst"), LlProject.List, LlProject.Label), model, Nothing, False, Nothing, True, 0, ExportID))
            Next
        Else
            ExportID += 1
            model = ServiceTest.GetStandardLLExportProperties(Context, "preview", sp, params, filename, WSID)
            model.ExportID = ExportID
            Clients.Group(PrintService.PrintServiceHubName).TestExportReport(model)
            model.ExportPath = IO.Path.GetFileName(model.ExportPath)
            ExpectedResults.Add(New TestPrintResult("Export (preview)", IIf(model.ProjectFile.EndsWith(".lst"), LlProject.List, LlProject.Label), model, Nothing, False, Nothing, True, 0, ExportID))
        End If
    End Sub

    ''' <summary>
    ''' Executes a test print via the service
    ''' </summary>
    ''' <param name="checkedAllPrinters"></param>
    ''' <param name="printers"></param>
    ''' <param name="printerIsLabel"></param>
    ''' <param name="Clients"></param>
    ''' <param name="Context"></param>
    ''' <param name="description"></param>
    ''' <param name="filename"></param>
    ''' <param name="WSID"></param>
    ''' <param name="sp"></param>
    ''' <param name="params"></param>
    ''' <remarks></remarks>
    Shared Sub TestPrint(checkedAllPrinters As Boolean, printers As List(Of String), printerIsLabel As List(Of Boolean), ByRef Clients As Object, Context As Hubs.HubCallerContext, description As String, filename As String, WSID As String, sp As String, params As String(,))
        Dim model As LLReportModel = ServiceTest.GetStandardLLPrintProperties(Context, description, IIf(filename.EndsWith("lst"), "List", "Label"), filename, WSID, sp, params)
        If Not checkedAllPrinters Then
            For x As Integer = 0 To printers.Count - 1
                If model.ReportType.ToLower() = "label" Then
                    If printerIsLabel(x) = True Then
                        model.PrinterName = printers(x)
                        Clients.Group(PrintService.PrintServiceHubName).TestPrintReport(model)
                    End If
                Else
                    If printerIsLabel(x) = False Then
                        model.PrinterName = printers(x)
                        Clients.Group(PrintService.PrintServiceHubName).TestPrintReport(model)
                    End If
                End If
            Next
        Else
            Clients.Group(PrintService.PrintServiceHubName).TestPrintReport(model)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Context">SignalR hub context</param>
    ''' <param name="reportname">Report name, used in debugging/logging</param>
    ''' <param name="reportType">"list" or "label"</param>
    ''' <param name="filename">filename, no path included</param>
    ''' <param name="spName">stored procedure name</param>
    ''' <param name="params">stored procedure parameters, same as RunSPArray()</param>
    ''' <param name="printerName">optional printer override</param>
    ''' <param name="onePerQty">optional one print per qty (single record, trans qty = 54 --> 54 labels printed)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetStandardLLPrintProperties(Context As Hubs.HubCallerContext, reportname As String, reportType As String, filename As String, WSID As String, spName As String, params As String(,), _
                                                         Optional printerName As String = "", Optional onePerQty As Boolean = False) As LLReportModel
        Dim location As HttpServerUtilityBase = Context.Request.GetHttpContext.Server
        Dim user As String = Context.User.Identity.Name
        Dim userDirectory As String = GlobalFunctions.getWSDirectory(location, user, WSID)
        If WSID = "DEFAULT" Then
            userDirectory = location.MapPath("~/Reports/LLDesign/")
        End If
        Dim reportDesign As String = System.IO.Path.Combine(userDirectory, filename)
        Return New LLReportModel(reportname, reportDesign, userDirectory, reportType, spName, params, Config.getConnectionString(WSID, Nothing), user, WSID, printerName, onePerQty)
    End Function
End Class