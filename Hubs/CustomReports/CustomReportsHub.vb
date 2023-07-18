' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports combit.ListLabel21
Imports System.IO


Public Class CustomReportsHub
    Inherits Hub
    ''' <summary>
    ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
    ''' </summary>
    ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
    ''' <remarks></remarks>
    Public Overrides Function OnConnected() As Task
        'Adds a user to their own unique group by a value passed in during connection
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("WSID"))
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
        'Calls the Original Onconnected Function pass control back to the server
        Return MyBase.OnConnected()
    End Function

    ''' <summary>
    ''' Causes changes saved to a single user's copy of a listlabel report to be copied to every other workstation's reports.
    ''' </summary>
    ''' <param name="fileName">The file that was changed</param>
    ''' <returns>A string telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function PushReportChanges(fileName As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Dim location As HttpServerUtilityBase = Context.Request.GetHttpContext.Server
                                                    Dim userDirectory As String = GlobalFunctions.getWSDirectory(location, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Dim mainDir As String = Context.Request.GetHttpContext.Server.MapPath("~/Reports/LLDesign/")
                                                    Try
                                                        File.Copy(userDirectory & "\" & fileName, mainDir & fileName, True)

                                                        For Each Direc In Directory.GetDirectories(mainDir)
                                                            If Direc <> userDirectory Then
                                                                File.Copy(userDirectory & "\" & fileName, Direc & "\" & fileName, True)
                                                            End If
                                                        Next

                                                        RunActionSP("insPushLLReport", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Filename", fileName, strVar}})

                                                    Catch ex As Exception
                                                        Debug.WriteLine(ex.ToString())
                                                        insertErrorMessages("CustomReportsHub", "PushReportChanges", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "error"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Gets data for a selected report.
    ''' </summary>
    ''' <param name="report">The rpeort to grab the data for</param>
    ''' <returns>A object that contaisn the data and the fields</returns>
    ''' <remarks></remarks>
    Public Function getSelectedReportData(report As String) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function()
                                                    Dim user As String = Context.User.Identity.Name, WSID As String = Context.QueryString.Get("WSID")
                                                    Return New With {.reportData = CustomReports.getSelectedReportData(user, WSID, report), _
                                                                     .fields = CustomReports.getReportFields(report, user, WSID)}
                                                End Function)
    End Function

    ''' <summary>
    ''' Saves changes to a report.
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="titles">List(of String) length 4 which contains report titles in order 1-4.</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function saveReportTitles(report As String, titles As List(Of String)) As Task
        Return Task.Factory.StartNew(Sub()
                                         Dim WSID As String = Context.QueryString.Get("WSID"), user As String = Context.User.Identity.Name
                                         Try
                                             RunActionSP("updReportTitles", WSID, _
                                                         {{"@RT1", titles(0), strVar}, {"@RT2", titles(1), strVar}, {"@RT3", titles(2), strVar}, {"@RT4", titles(3), strVar}, _
                                                          {"@WSID", WSID, strVar}, {"@Report", report, strVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.ToString())
                                             insertErrorMessages("CustomReportsHub", "saveReportTitles", ex.ToString(), user, WSID)
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Saves the expressions utilized within the report
    ''' </summary>
    ''' <param name="report">The report that was editted</param>
    ''' <param name="fields">The fields that were used foo the expressions</param>
    ''' <param name="exps">The expressions that are used with the fields</param>
    ''' <returns>None task of sub</returns>
    Public Function saveReportFieldsExps(report As String, fields As List(Of String), exps As List(Of String)) As Task
        Return Task.Factory.StartNew(Sub()
                                         Dim user As String = Context.User.Identity.Name, WSID As String = Context.QueryString.Get("WSID")
                                         Try
                                             RunActionSP("updReportFieldsExps", WSID, {{"@F1", fields(0), strVar}, {"@F2", fields(1), strVar}, {"@F3", fields(2), strVar}, _
                                                                                       {"@F4", fields(3), strVar}, {"@F5", fields(4), strVar}, {"@F6", fields(5), strVar}, _
                                                                                       {"@E1", exps(0), strVar}, {"@E2", exps(1), strVar}, {"@E3", exps(2), strVar}, _
                                                                                       {"@E4", exps(3), strVar}, {"@E5", exps(4), strVar}, {"@E6", exps(5), strVar}, _
                                                                                       {"@WSID", WSID, strVar}, {"@Report", report, strVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.ToString())
                                             insertErrorMessages("CustomReportsHub", "saveReportFieldExps", ex.ToString(), user, WSID)
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Saves the field values for the given report
    ''' </summary>
    ''' <param name="report">The reports whose values are going to be saved</param>
    ''' <param name="v1">The frist list of values</param>
    ''' <param name="v2">The second list of values</param>
    ''' <returns>None task of sub</returns>
    Public Function saveFieldValues(report As String, v1 As List(Of String), v2 As List(Of String)) As Task
        Return Task.Factory.StartNew(Sub()
                                         Dim WSID As String = Context.QueryString.Get("WSID"), user As String = Context.User.Identity.Name
                                         Try
                                             RunActionSP("updReportFieldValues", WSID, {{"@V1", v1(0), strVar}, {"@V2", v1(1), strVar}, {"@V3", v1(2), strVar}, _
                                                                                       {"@V4", v1(3), strVar}, {"@V5", v1(4), strVar}, {"@V6", v1(5), strVar}, _
                                                                                       {"@V1A", v2(0), strVar}, {"@V2A", v2(1), strVar}, {"@V3A", v2(2), strVar}, _
                                                                                       {"@V4A", v2(3), strVar}, {"@V5A", v2(4), strVar}, {"@V6A", v2(5), strVar}, _
                                                                                       {"@WSID", WSID, strVar}, {"@Report", report, strVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.ToString())
                                             insertErrorMessages("CustomReportsHub", "saveFieldValues", ex.ToString(), user, WSID)
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Sends print command to print service for printing custom reports
    ''' </summary>
    ''' <param name="report">The report to print</param>
    ''' <param name="where">The where clause for the report</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function printCustomReport(report As String, where As List(Of List(Of String))) As Task
        Return Task.Factory.StartNew(
            Sub()
                Dim whereString As String = ""
                If Not IsNothing(where) Then
                    whereString = CustomReports.buildWhereClause(where)
                End If

                Dim reportObj As CustomReportsModel = _
                    CustomReports.chooseView(report, Context.User.Identity.Name, Context.QueryString.Get("WSID"), False, _
                                             Context.Request.GetHttpContext.Server, whereString, isBasicReport:=report.IndexOf(".") = -1)

                Try
                    ' sends print request to the windows service
                    Clients.Group(PrintService.PrintServiceHubName).PrintCustomReport(reportObj)
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString())
                    insertErrorMessages("CustomReportsHub", "PrintCustomReport", _
                                        ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                End Try
            End Sub)
    End Function

    ''' <summary>
    ''' Handles Exporting of Custom Reports to either Preview or multiple other formats
    ''' </summary>
    ''' <param name="data">Object that encapsulates paramteres needed for exporting</param>
    ''' <returns>Boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ExportReport(data As PreviewExportPrintModel) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean
                Dim username As String = Context.User.Identity.Name, WSID As String = Context.QueryString.Get("WSID")
                Try
                    Dim isBasicReport As Boolean = Not data.reportName.Contains(".")
                    If IsNothing(data.fields) Then data.fields = New List(Of List(Of String))
                    If IsNothing(data.reportTitles) Then data.reportTitles = New List(Of String)
                    Dim whereString As String = IIf(isBasicReport, CustomReports.buildWhereClause(data.fields), "")

                    If IsNothing(data.backFilename) Then data.backFilename = ""
                    If data.backFilename.Trim() <> "" Then
                        data.backFilename = Path.GetFileNameWithoutExtension(data.backFilename) & ListLabelHelperFunctions.GetExportExtension(data.desiredFormat)
                    End If

                    Dim model As CustomReportsModel = CustomReports.chooseView(data.reportName, Context.User.Identity.Name, Context.QueryString.Get("WSID"), data.desiredFormat = "preview", Context.Request.GetHttpContext.Server, whereString, data.type, isBasicReport)
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Context.Request.GetHttpContext.Server, data.desiredFormat, "", {{"nothing"}}, Path.GetFileName(model.location), model)

                    If (data.desiredFormat = "CSV") Then
                        ExportLLToCSV(m.ExportModel, m.ExportPath, True)
                    Else
                        Clients.Group(PrintService.PrintServiceHubName).ExportReport(m)
                    End If

                Catch ex As Exception
                    Debug.Print(ex.ToString())
                    insertErrorMessages("ExportReport", "ExportReport", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    Return False
                End Try
                Return True
            End Function)
    End Function

    ''' <summary>
    ''' Exports the report as a csv file
    ''' </summary>
    ''' <param name="reportModel">The report information</param>
    ''' <param name="exportPath">The path to the export file location</param>
    ''' <param name="isReportPage">Optional parameter for data gathering</param>
    Private Sub ExportLLToCSV(reportModel As CustomReportsModel, exportPath As String, Optional isReportPage As Boolean = False)
        Dim reportReader As SqlDataReader = Nothing
        Dim columns As New List(Of String)
        Dim data As New List(Of List(Of String))
        If isReportPage Then
            reportReader = CreateDataSet.CreateDataSet(reportModel, True)
            For x = 0 To reportReader.FieldCount - 1
                columns.Add(reportReader.GetName(x))
            Next
            While reportReader.Read
                Dim rowData As New List(Of String)
                For x = 0 To reportReader.FieldCount - 1
                    rowData.Add(CheckDBNull(reportReader(x)))
                Next
                data.Add(rowData)
            End While
        Else

        End If
        GlobalFunctions.ExportToCSV(exportPath, True, reportModel.user, reportModel.WSID, data, columns)
        ExportReportFinished(IO.Path.GetFileName(exportPath), IO.Path.GetExtension(exportPath), reportModel.WSID)
    End Sub

    ''' <summary>
    ''' Event raised by the print service whenever an export or print command has been completed.  This event raises an event on the client side where we update the user as to the result of their export (failed or succeeded, or to cause the user to retrieve the file in the case of a non-preview export).
    ''' </summary>
    ''' <param name="filename">The file that was created</param>
    ''' <param name="desiredFormat">The format of the report</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <remarks></remarks>
    Public Sub ExportReportFinished(filename As String, desiredFormat As String, WSID As String)
        Dim file As String = IO.Path.GetFileName(filename)
        file = Context.Request.GetHttpContext.Server.MapPath("~/temp/") & WSID & "\" & file
        Clients.Group(WSID).ExportFinished(filename, desiredFormat, IO.File.Exists(file))
    End Sub

    ''' <summary>
    ''' Gets the report details for the custom reports and labels tab.  Includes test and design data, test/design data type, filename, etc.
    ''' </summary>
    ''' <param name="Filename">The file that contains the info</param>
    ''' <returns>A list of string that contaisn all the information about the report</returns>
    ''' <remarks></remarks>
    Public Function getReportDetails(filename As String) As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Return CustomReports.getReportDetails(filename, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Updates a report
    ''' </summary>
    ''' <param name="oldfilename">The old report name</param>
    ''' <param name="newfilename">The new name for the report</param>
    ''' <param name="description">The description of the report</param>
    ''' <param name="datasource">The source for the data within the report</param>
    ''' <param name="output">What type of report it is</param>
    ''' <param name="testDataType">The data for testing it</param>
    ''' <param name="eFilename">THe file name for exporting</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function updateReportDetails(oldfilename As String, newfilename As String, description As String, datasource As String, output As String, _
                                        testDataType As String, eFilename As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         If IsNothing(testDataType) Then
                                             testDataType = nullVar
                                         ElseIf testDataType.Trim() = "" Then
                                             testDataType = nullVar
                                         End If
                                         If IsNothing(datasource) Then
                                             datasource = ""
                                         End If
                                         Dim WSID As String = Context.QueryString.Get("WSID")
                                         Try
                                             RunActionSP("updListsAndLabelsReportDetails", WSID, _
                                                         {{"@WSID", WSID, strVar}, {"@OldFilename", oldfilename, strVar}, {"@NewFilename", newfilename, strVar}, _
                                                          {"@Description", description, strVar}, {"@DataSource", datasource, strVar}, {"@OutputType", output, strVar}, _
                                                          {"@TestDataType", testDataType, strVar}, {"@ExportFilename", eFilename, strVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.ToString())
                                             insertErrorMessages("CustomReportsHub", "updateReportDetails", ex.ToString(), Context.User.Identity.Name, WSID)
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Validates the sql and filename of a new report to be added to the custom reports section
    ''' </summary>
    ''' <param name="fileDetails">A list of string that contains the information for the new report</param>
    ''' <returns>An object telling if the new report is valid and returns its information</returns>
    ''' <remarks></remarks>
    Public Function validateNewDesign(fileDetails As List(Of String)) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function()
                                                    Dim fileObj As Object = Nothing
                                                    Dim server As HttpServerUtilityBase = Context.Request.GetHttpContext.Server
                                                    Dim WSID As String = Context.QueryString.Get("WSID"), user As String = Context.User.Identity.Name
                                                    Dim statusObj As New CustomReportsValidationModel(New List(Of String), True, Nothing, Nothing)

                                                    Try
                                                        For x As Integer = 0 To fileDetails.Count - 1
                                                            If IsNothing(fileDetails(x)) Then
                                                                fileDetails(x) = ""
                                                            End If
                                                        Next

                                                        fileObj = New With {.description = fileDetails(0), _
                                                                            .filename = fileDetails(1), _
                                                                            .dataSource = fileDetails(2), _
                                                                            .dataType = fileDetails(3), _
                                                                            .outputType = fileDetails(4), _
                                                                            .exportFilename = fileDetails(5)}

                                                        Dim validFileCheckResult As Object = CustomReportsDesigner.validateNewDesignFilename(server, user, WSID, fileObj.filename)
                                                        Dim validSQL As Object = CustomReportsDesigner.validateNewDesignSQL(server, user, WSID, fileObj.dataSource, fileObj.dataType)

                                                        statusObj.errs.AddRange(validFileCheckResult.errs.ToArray())
                                                        statusObj.errs.AddRange(validSQL.errs.ToArray())
                                                        statusObj.fileObj = validFileCheckResult
                                                        statusObj.sqlObj = validSQL
                                                    Catch ex As Exception
                                                        Debug.WriteLine(ex.ToString())
                                                        insertErrorMessages("CustomReportsHub", "saveNewDesign", ex.ToString(), user, WSID)
                                                        statusObj.errs.Add("Unknown error occurred in validation of sql/filename for add new.  If this continues please contact Scott Tech.")
                                                        statusObj.success = False
                                                    End Try

                                                    Return statusObj
                                                End Function)
    End Function

    ''' <summary>
    ''' Deletes the user's copy of the report in [Lists and Labels]
    ''' </summary>
    ''' <param name="filename">The report file to delete</param>
    ''' <param name="keepFile">If true the file is kept, but the reference to it in [Lists and Labels] is lost.  If false, both the reference and the file are removed.  The file is backed up to the user's LLDesign/Backup folder in case it needs to be recovered.</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteReport(filename As String, keepFile As Boolean) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim WSID As String = Context.QueryString.Get("WSID")
                                                     Dim user As String = Context.User.Identity.Name
                                                     Dim server As HttpServerUtilityBase = Context.Request.GetHttpContext.Server
                                                     Try
                                                         RunActionSP("delLLReport", WSID, _
                                                                     {{"@WSID", WSID, strVar}, {"@Filename", filename, strVar}, {"@User", user, strVar}, _
                                                                      {"@KeepFile", CastAsSqlBool(keepFile), intVar}})

                                                         ' deletes file from both /reports/lldesign and /reports/lldesign/<wsid> directories 
                                                         ' and makes a backup of the /<wsid> copy in the /reports/lldesign/backup folder
                                                         If Not keepFile Then
                                                             Dim backupPath As String = server.MapPath("~/Reports/LLDesign/Backup/")
                                                             If Not System.IO.Directory.Exists(backupPath) Then
                                                                 System.IO.Directory.CreateDirectory(backupPath)
                                                             End If
                                                             Dim backupFile As String = _
                                                                 System.IO.Path.Combine(backupPath, _
                                                                                        System.IO.Path.GetFileNameWithoutExtension(filename) & "_" _
                                                                                        & WSID & "_" & Now().ToString("MMddyyyy") & System.IO.Path.GetExtension(filename))

                                                             Dim WSpath As String = System.IO.Path.Combine(System.IO.Path.Combine(server.MapPath("~/Reports/LLDesign/"), WSID) & "/", filename)
                                                             If System.IO.File.Exists(WSpath) Then
                                                                 System.IO.File.Copy(WSpath, backupFile, True)
                                                                 System.IO.File.Delete(WSpath)
                                                             End If

                                                             Dim path As String = System.IO.Path.Combine(server.MapPath("~/Reports/LLDesign/"), filename)
                                                             If System.IO.File.Exists(path) Then
                                                                 System.IO.File.Delete(path)
                                                             End If
                                                         End If
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("CustomReportsHub", "deleteReport", ex.ToString(), user, WSID)
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Restores the [Lists and Labels] reference to the report that was previously deleted.  If all is set to true then all reports are set to their default [Lists and Labels] values.
    ''' </summary>
    ''' <param name="all">Tells if this action should be done for all reports</param>
    ''' <param name="data">The restore data for a single report</param>
    ''' <returns>A boolean telling if the oepration completed successfully</returns>
    ''' <remarks></remarks>
    Public Function restoreDesign(all As Boolean, data As NewLLDesignModel) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim WSID As String = Context.QueryString.Get("WSID")
                                                     Try
                                                         RunActionSP("insLLRestoreDesign", WSID, {{"@WSID", WSID, strVar}, {"@Description", data.description, strVar}, {"@Filename", data.filename, strVar}, _
                                                            {"@DataSource", data.dataSource, strVar}, {"@DataType", data.dataType, strVar}, _
                                                            {"@OutputType", IIf(data.outputType = LlProject.List, "Report", "Label"), strVar}, _
                                                            {"@ExportFilename", data.exportFilename, strVar}, {"@All", CastAsSqlBool(all), intVar}})
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("CustomReportsHub", "restoreDesign", ex.ToString(), Context.User.Identity.Name, WSID)
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function


End Class