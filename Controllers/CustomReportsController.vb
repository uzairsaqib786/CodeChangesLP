' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports combit.ListLabel21
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports combit.ListLabel21.Web.WebDesigner.Server
Imports combit.ListLabel21.DataProviders
Imports System.Web.Script.Serialization

Namespace Controllers
    <Authorize()>
    Public Class CustomReportsController
        Inherits PickProController

        ' GET: Reports
        Function Index(Optional App As String = "Admin") As ActionResult
            Return View(model:=New With {.reports = CustomReports.getReportNames(User.Identity.Name, Session("WSID")),
                                         .ReportTitles = CustomReports.getCustomReportTitles(User.Identity.Name, Session("WSID"), App),
                                         .app = App})
        End Function

        ''' <summary>
        ''' Imports the desired report file
        ''' </summary>
        ''' <param name="fileData">The information required for the file to be imported</param>
        ''' <returns>A string that is empty will mean that the import was successful</returns>
        <HttpPost>
        Function importFile(fileData As HttpPostedFileBase) As ActionResult
            Try
                Dim wsid As String = Session("WSID")
                Dim fileName As String = fileData.FileName
                If fileName.Contains("\") Then ' IE likes to send the whole file path as the path, so get just the file name
                    fileName = fileName.Split("\").Last()
                End If
                Dim fPath = Path.Combine(Server.MapPath("~/Reports/LLDesign/" & wsid & "/" & fileName))
                fileData.SaveAs(fPath)
                Return Json("")
            Catch ex As Exception
                insertErrorMessages("CustomReportsController", "importFile", ex.Message, User.Identity.Name, Session("WSID"))
                Debug.WriteLine(ex.Message)
                Return Json(ex.Message)
            End Try
        End Function

        ''' <summary>
        ''' Gets the designer object or embed tag for list and label for a specified report to edit.
        ''' </summary>
        ''' <param name="data">The information required for the LL previewer</param>
        ''' <returns>A new partial view that contains the preview for the report</returns>
        ''' <remarks></remarks>
        Function getLLDesigner(data As PreviewExportPrintModel) As ActionResult
            Dim ds As DataSet = CustomReportsDesigner.getDataSource(data.reportName, User.Identity.Name, Session("WSID"))
            Dim dscollection As New combit.ListLabel21.DataProviders.DataProviderCollection()
            dscollection.Add(New AdoDataProvider(ds))
            ' vertical bar "|" uri encoded
            Dim vBar As String = "%7C"
            Dim designer As New WebDesignerOptions With {.DataSource = dscollection, .ProjectType = data.type, .ProjectFile = System.IO.Path.Combine(Server.MapPath("~/Reports/LLDesign/" & Session("WSID")), data.reportName),
                                   .DataSourceIDs = New List(Of String) From {data.reportName.Replace(".", "") & vBar & Session("WSID") & vBar & User.Identity.Name}, .Height = "60%", .Width = "100%", .TempDirectory = Server.MapPath("~/temp/")}
            Return Me.PartialView("~/Views/CustomReports/_LLDesigner.vbhtml", designer)
        End Function

        ''' <summary>
        ''' Creates a new file to design with for the list and label designer before returning the designer object or embed tag
        ''' </summary>
        ''' <param name="data">The objectthat contains the information required for the ll designer</param>
        ''' <returns>A new partial view that contains the ll designer</returns>
        ''' <remarks></remarks>
        <System.Web.Mvc.HttpPost()>
        Function getLLDesignerNewDesign(data As NewLLDesignModel) As ActionResult
            ' combit.ListLabel21.Web.WebDesigner.Server.WebDesignerConfig.WebDesignerSetupFile = Server.MapPath("~/WebDesigner/LL21WebDesignerSetup.exe")
            ' above not needed here, is used in global.asax
            Try
                ' create blank file for design
                Using System.IO.File.Create(Server.MapPath("~/Reports/LLDesign/" & Session("WSID")) & "/" & data.filename)
                    ' need to force it to release the filestream with this or by closing filestream after storing it in memory, otherwise the file cannot be accessed
                    ' or used
                End Using

                ' add design to the database with all the details
                RunActionSP("insLLNewDesign", Session("WSID"), {{"@WSID", Session("WSID"), strVar}, {"@Description", data.description, strVar}, {"@Filename", data.filename, strVar},
                                                                {"@DataSource", data.dataSource, strVar}, {"@DataType", data.dataType, strVar},
                                                                {"@OutputType", IIf(data.outputType = LlProject.List, "Report", "Label"), strVar},
                                                                {"@ExportFilename", data.exportFilename, strVar}, {"@Application", data.appName, strVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("LLPreviewExportController", "getLLDesignerNewDesign", ex.Message, User.Identity.Name, Session("WSID"))
            End Try

            Dim ds As DataSet = CustomReportsDesigner.getDataSource(data.filename, User.Identity.Name, Session("WSID"))
            Dim dscollection As New combit.ListLabel21.DataProviders.DataProviderCollection()
            dscollection.Add(New AdoDataProvider(ds))
            ' vertical bar "|" uri encoded
            Dim vBar As String = "%7C"
            Dim designer As New WebDesignerOptions With {.DataSource = dscollection, .ProjectType = data.outputType, .ProjectFile = System.IO.Path.Combine(Server.MapPath("~/Reports/LLDesign/" & Session("WSID")), data.filename),
                                   .DataSourceIDs = New List(Of String) From {data.filename.Replace(".", "") & vBar & Session("WSID") & vBar & User.Identity.Name}, .Height = "60%", .Width = "100%", .TempDirectory = Server.MapPath("~/temp/")}
            Return Me.PartialView("~/Views/CustomReports/_LLDesigner.vbhtml", designer)
        End Function

        ''' <summary>
        ''' Prints a category report.
        ''' </summary>
        ''' <returns>A boolean telling if the print job completed successfully</returns>
        ''' <remarks></remarks>
        Public Function printCategoriesReport() As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selInvCategoriesLL"
                Dim params As String(,) = {{"nothing"}}
                Dim filename As String = "Categories.lst", LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Categories Report", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("CustomReportsController", "PrintCategoriesReport", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Called when previewing a report by _LLViewer.vbhtml
        ''' </summary>
        ''' <returns>A view object for the ll previewer</returns>
        ''' <remarks></remarks>
        Function LLViewWriter() As ActionResult
            Return View("_LLViewWriter")
        End Function

        ''' <summary>
        ''' Handles Exporting of Custom Reports to either Preview or multiple other formats
        ''' </summary>
        ''' <param name="data">Object that encapsulates paramteres needed for exporting</param>
        ''' <returns>A boolean telling if the preview was successful</returns>
        ''' <remarks></remarks>
        Public Function PreviewReport(data As PreviewExportPrintModel) As ActionResult
            Dim username As String = User.Identity.Name, WSID As String = Session("WSID")
            Try
                Dim isBasicReport As Boolean = Not data.reportName.Contains(".")
                If IsNothing(data.fields) Then data.fields = New List(Of List(Of String))
                If IsNothing(data.reportTitles) Then data.reportTitles = New List(Of String)
                Dim filts As New Object
                Dim whereString As String = ""

                'deserializes the filter data so that it is able to be utilized
                If Not IsNothing(data.objFields) Then
                    filts = New JavaScriptSerializer().Deserialize(Of List(Of Object))(data.objFields)
                End If
                'Builds the where clause for the report
                If IsNothing(data.objFields) Then
                    whereString = IIf(isBasicReport, CustomReports.buildWhereClause(data.fields), "")
                Else
                    Dim params As New List(Of List(Of String))
                    Dim inners As New List(Of String)

                    For Each obj In filts
                        inners.Add(obj("field"))
                        inners.Add(obj("exptype"))
                        inners.Add(obj("expone"))
                        inners.Add(obj("exptwo"))
                        params.Add(inners)
                        inners = New List(Of String)
                    Next

                    whereString = IIf(isBasicReport, CustomReports.buildWhereClause(params), "")
                End If

                If IsNothing(data.backFilename) Then data.backFilename = ""
                If data.backFilename.Trim() <> "" Then
                    data.backFilename = Path.GetFileNameWithoutExtension(data.backFilename) & ListLabelHelperFunctions.GetExportExtension(data.desiredFormat)
                End If

                Dim model As CustomReportsModel = CustomReports.chooseView(data.reportName, username, WSID, data.desiredFormat = "preview", Server, whereString, data.type, isBasicReport)
                Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, data.desiredFormat, "", {{"nothing"}}, Path.GetFileName(model.location), model)

                Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ExportReport", "ExportReport", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

    End Class
End Namespace