' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace WorkManager.Controllers
    <Authorize()>
    Public Class PreferencesController
        Inherits PickProController

        ' GET: Preferences
        ''' <summary>
        ''' Gets the preferences in order to pass them to the html page on page load
        ''' </summary>
        ''' <returns>A view object that contains data for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Dim permissions As List(Of Boolean) = WorkManager.WMPreferences.GetWMPermissions(User.Identity.Name, Session("WSID"))
            If permissions.Count = 0 OrElse permissions(0) <> True Then Return Redirect("~/WM/")
            Return View(model:=WorkManager.WMPreferences.GetGeneralBatchSettings(User.Identity.Name, Session("WSID")))
        End Function

        ''' <summary>
        ''' Gets the information for the wmUsers datatable
        ''' </summary>
        ''' <param name="data">Object that contains the information in order to get the records for the table</param>
        ''' <returns>json object containing the infomration for the datatable</returns>
        ''' <remarks></remarks>
        Function GetWMUsersTable(data As TableObjectSent) As ActionResult
            Return Json(WorkManager.WMPreferences.GetWMUsersTable(data.draw, data.start, data.start + data.length, data.entryFilter, Request.QueryString.Get("order[0][column]"),
                                                                    Request.QueryString.Get("order[0][dir]"), Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the datatable information for the location range table
        ''' </summary>
        ''' <param name="data">Object that contains the information in order to get the records for the table</param>
        ''' <returns>json object containing the data</returns>
        ''' <remarks></remarks>
        Function getWMLocationRangesTable(ByVal data As TableObjectSent) As ActionResult
            Return Json(WorkManager.WMPreferences.getLocationRangesTable(data.draw, data.start + 1, data.length + data.start,
                                                                        Request.QueryString.Get("order[0][column]"),
                                                                        Request.QueryString.Get("order[0][dir]"), data.filter,
                                                                        User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the start location range typahead data
        ''' </summary>
        ''' <param name="query">The value to get suggestions for</param>
        ''' <returns>json object with the typeahead data</returns>
        ''' <remarks></remarks>
        Function getStartLocRangeTA(query As String) As ActionResult
            Return Json(WorkManager.WMPreferences.StartLocationRangeTypeahead(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the end location range typeahead data
        ''' </summary>
        ''' <param name="query">The value to get the suggestions for</param>
        ''' <param name="StartRange">The value that limits the suggestions to those that come after it</param>
        ''' <returns>josn object containing the data</returns>
        ''' <remarks></remarks>
        Function getEndLocRangeTA(query As String, StartRange As String) As ActionResult
            Return Json(WorkManager.WMPreferences.EndLocationRangeTypeahead(StartRange, query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the data for the employee typeahead
        ''' </summary>
        ''' <param name="lastname">The value to get suggestions for</param>
        ''' <returns>json object containing the typeahead data</returns>
        ''' <remarks></remarks>
        Function GetEmployeeTA(lastname As String) As ActionResult
            Return Json(WorkManager.WMPreferences.GetEmployeeTypeahead(lastname, Session("WSID"), User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the table data for the worker ranges table
        ''' </summary>
        ''' <param name="data">Object that contains the information in order to get the records for the table</param>
        ''' <returns>json object containing the table data</returns>
        ''' <remarks></remarks>
        Function getWMWorkerRangesTable(ByVal data As TableObjectSent) As ActionResult
            Return Json(WorkManager.WMPreferences.getWorkerRangesTable(data.draw, data.start + 1, data.length + data.start,
                                                                        Request.QueryString.Get("order[0][column]"),
                                                                        Request.QueryString.Get("order[0][dir]"), data.filter,
                                                                        User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the data for the users typeahead
        ''' </summary>
        ''' <param name="query">The value to get suggestions for</param>
        ''' <returns>json object containing the data for the typeahead</returns>
        ''' <remarks></remarks>
        Function getWMUsersTypeahead(query As String) As ActionResult
            Return Json(WorkManager.WMPreferences.selectWMUsersLastNameTypeahead(query, 0, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the data for the location range typeahead 
        ''' </summary>
        ''' <param name="query">The value to get suggestions for</param>
        ''' <returns>json object containing the data for the typeahead</returns>
        ''' <remarks></remarks>
        Function getWMLocRangeRangeNameTypeahead(query As String) As ActionResult
            Return Json(WorkManager.WMPreferences.selectWMLocRangesRangeNameTypeahead(query, 0, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints or previews the User Ranges report 
        ''' </summary>
        ''' <returns>boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Function PrintWMUserRanges(printDirect As Boolean) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim filename As String = "UserRange.lst", sp As String = "selWMUserRangesLL", params As String(,) = {{"nothing"}}

                If PrintDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "User Ranges List", "List", filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PreferencesController", "PrintWMUserRanges", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try

            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prins the worker report
        ''' </summary>
        ''' <param name="printDirect">If the print should be sent directly to the printer</param>
        ''' <param name="userToPrint">The user whose info is being printed</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        Function PrintWorker(printDirect As Boolean, userToPrint As String) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Try
                Dim filename As String = "wmUsers.lst", sp As String = "selWMPrintUser", params As String(,) = {{"@Username", userToPrint, strVar}}

                If printDirect Then
                    Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                    Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "User Detail", "List", filename, sp, params)
                    Clients.Print(m)
                Else
                    Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                    Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("PreferencesController", "PrintWMUserRanges", ex.ToString(), username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try

            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace