' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel21
Imports combit.ListLabel21.DataProviders
Imports combit.ListLabel21.Web.WebDesigner.Server
Imports combit.ListLabel21.Web
Imports System.IO

Public Class ListLabelHelperFunctions
    ''' <summary>
    ''' Personal License Key so that we can run this program without having listlabel installed on the host machine.  Any LL21 license key will work.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property LLLicenseString As String
        Get
            Return "8thfDg"
        End Get
    End Property

    ''' <summary>
    ''' Converts a string representation of an export format into a List and Label enumeration
    ''' </summary>
    ''' <param name="format"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetFormat(format As String) As LlExportTarget
        Dim formats As New Dictionary(Of String, Integer) From {{"xls", 9}, {"xlsx", 10}, {"html", 1}, {"pdf", 0}, {"rtf", 2}, {"txt", 18}, {"xml", 17}, {"xps", 12}, {"preview", 21}}
        If formats.ContainsKey(format) Then
            Return formats(format)
        Else
            Return -1
        End If
    End Function

    ''' <summary>
    ''' Gets the export file extension for files that don't have their file extensions being passed as their format "name"
    ''' </summary>
    ''' <param name="format"></param>
    ''' <returns></returns>
    ''' <remarks>Includes the leading "." to the extension</remarks>
    Public Shared Function GetExportExtension(format As String) As String
        Select Case format.ToUpper()
            Case "PREVIEW"
                Return ".ll"
            Case "MHTML"
                Return ".mht"
            Case "BITMAP"
                Return ".bmp"
            Case Else
                Return "." & format.ToLower()
        End Select
    End Function

    ''' <summary>
    ''' Deletes all files from the /temp directory after they've been unmodified and not accessed for an hour.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub DeleteOldExports()
        Dim path As String = IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "temp")

        Try
            If IO.Directory.Exists(path) Then
                For Each subfolder As String In IO.Directory.GetDirectories(path)
                    For Each f As String In IO.Directory.GetFiles(subfolder)
                        Dim file As New IO.FileInfo(f)
                        If DateDiff(DateInterval.Hour, file.LastWriteTime(), Now()) > 1 And DateDiff(DateInterval.Hour, file.LastAccessTime(), Now()) > 1 Then
                            Try
                                IO.File.Delete(f)
                            Catch ioex As IO.IOException
                                ' can't delete it, but don't care, because it's open/locked
                            Catch unauthorizedEx As UnauthorizedAccessException
                                ' can't delete it, but don't care (executable, readonly, no access rights, etc.)
                            Catch ex As Exception
                                Debug.WriteLine(ex.Message)
                                insertErrorMessages("ListLabelExtension/HtmlHelperExtensions", "DeleteDesigns", ex.Message, "System Timer", "System")
                            End Try
                        End If
                    Next
                    For Each llfolder As String In IO.Directory.GetDirectories(subfolder)
                        Dim dir As New IO.DirectoryInfo(llfolder)
                        If DateDiff(DateInterval.Hour, dir.LastWriteTime(), Now()) > 1 And DateDiff(DateInterval.Hour, dir.LastAccessTime(), Now()) > 1 Then
                            Try
                                IO.Directory.Delete(llfolder)
                            Catch ioex As IO.IOException
                                ' can't delete it, but don't care, because it's open/locked
                            Catch unauthorizedEx As UnauthorizedAccessException
                                ' can't delete it, but don't care (executable, readonly, no access rights, etc.)
                            Catch ex As Exception
                                Debug.WriteLine(ex.Message)
                                insertErrorMessages("ListLabelExtension/HtmlHelperExtensions", "DeleteDesigns", ex.Message, "System Timer", "System")
                            End Try
                        End If
                    Next
                Next
            Else
                IO.Directory.CreateDirectory(path)
            End If
        Catch ex As Exception
            Debug.Print(ex.Message)
            insertErrorMessages("HtmlHelperExtenions", "DeleteDesigns", ex.Message, "", "")
        End Try
    End Sub

    'D: Dieses Ereignis wird vom WebDesigner-DataProvider ausgelöst
    'US: This Event will() be triggerd by the WebDesigner data provider
    Shared Sub Services_OnRequestDataProvider(sender As Object, e As RequestDataProviderEventArgs)
        Dim params As String() = e.DataSourceID.Split("|")
        Dim ds As DataSet = CustomReportsDesigner.getDataSource(params(0).Insert(params(0).Length - 3, "."), params(2), params(1))
        Dim dscollection As New combit.ListLabel21.DataProviders.DataProviderCollection()
        dscollection.Add(New AdoDataProvider(ds))
        e.DataProvider = dscollection
    End Sub

    'D: Dieses Ereignis wird vom Html5Viewer ausgelöst
    'US: This event will be triggerd by the Html5Viewer
    Shared Sub Services_OnListLabelRequest(sender As Object, e As ListLabelRequestEventArgs)
        Dim model As ExportServiceModel = TryCast(e.CustomData, ExportServiceModel)

        If Not IsNothing(model) Then
            Dim _LL As ListLabel = Nothing
            Try
                Dim ds As DataSet = Nothing
                If IsNothing(model.ExportModel) Then
                    ds = CreateDataSet.CreateDataSetNew(New LLReportModel() With {.DataSPName = model.SPName, .DataParameters = model.Params, .ConnectionString = model.ConnectionString})
                Else
                    ds = CreateDataSet.CreateDataSet(model.ExportModel)
                End If
                _LL = New ListLabel
                With _LL
                    .LicensingInfo = LLLicenseString
                    .DataSource = ds
                    .AutoDestination = LlPrintMode.Export
                    .AutoProjectFile = model.ProjectFile
                    .AutoShowSelectFile = False
                    .AutoShowPrintOptions = False
                    .ExportOptions.Add(LlExportOption.ExportQuiet, "1")
                    If model.DesiredFormat = "preview" Then
                        .Core.LlPreviewSetTempPath(model.UserDirectory)
                        .IncrementalPreview = False
                    End If
                End With
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("Global.asax.vb", "Services_OnListLabelRequest", ex.Message, model.User, model.WSID)
            End Try
            e.NewInstance = _LL
            e.ExportPath = model.ExportPath
        End If
    End Sub

    ''' <summary>
    ''' Gets the standard export model for an export through List and Label
    ''' </summary>
    ''' <param name="user">Requesting username</param>
    ''' <param name="em">Optional exportmodel for special customreportshub cases.</param>
    ''' <param name="server">Server from controller</param>
    ''' <param name="WSID">Workstation ID of requesting workstation</param>
    ''' <param name="format">Desired Format (preview, etc.)</param>
    ''' <param name="spname">Stored Procedure</param>
    ''' <param name="params">SP parameters (same as for RunSPArray()</param>
    ''' <param name="filename">filename, no path included.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetStandardLLExportProperties(user As String, WSID As String, server As HttpServerUtilityBase, format As String, spname As String, params As String(,), filename As String, _
                                                  Optional em As CustomReportsModel = Nothing) As ExportServiceModel
        Dim userDirectory = server.MapPath("~\temp\") & WSID
        Dim projectFile = server.MapPath("~\Reports\LLDesign\") & WSID & "\" & filename
        Dim pfilename = Path.GetFileNameWithoutExtension(projectFile) & ListLabelHelperFunctions.GetExportExtension(format)
        Dim cs As String = userCS(WSID)
        If IsNothing(em) Then
            Return New ExportServiceModel(format, userDirectory, Path.Combine(userDirectory, pfilename), projectFile, spname, params, cs, WSID, user)
        Else
            Return New ExportServiceModel(format, userDirectory, Path.Combine(userDirectory, pfilename), projectFile, spname, params, cs, WSID, user, em)
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="server">Server instance from a controller</param>
    ''' <param name="WSID">Workstation ID of the requesting workstation</param>
    ''' <param name="reportname">Report name, used in debugging/logging</param>
    ''' <param name="reportType">"list" or "label"</param>
    ''' <param name="filename">filename, no path included</param>
    ''' <param name="spName">stored procedure name</param>
    ''' <param name="params">stored procedure parameters, same as RunSPArray()</param>
    ''' <param name="printerName">optional printer override</param>
    ''' <param name="onePerQty">optional one print per qty (single record, trans qty = 54 --> 54 labels printed)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetStandardLLPrintProperties(user As String, WSID As String, server As HttpServerUtilityBase, reportname As String, reportType As String, filename As String, spName As String, params As String(,), _
                                                         Optional printerName As String = "", Optional onePerQty As Boolean = False) As LLReportModel
        Dim userDirectory As String = GlobalFunctions.getWSDirectory(server, user, WSID)
        Dim reportDesign As String = System.IO.Path.Combine(userDirectory, filename)
        Return New LLReportModel(reportname, reportDesign, userDirectory, reportType, spName, params, userCS(WSID), user, WSID, printerName, onePerQty)
    End Function
End Class