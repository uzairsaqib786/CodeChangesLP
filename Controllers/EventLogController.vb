' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports combit.ListLabel21

Namespace Controllers
    <Authorize()>
    Public Class EventLogController
        Inherits PickProController

        ' GET: EventLog
        ''' <summary>
        ''' Returns the Event Log view
        ''' </summary>
        ''' <returns>View object that contains the information for the html page</returns>
        ''' <remarks></remarks>
        Function Index(App As String) As ActionResult
            Return View(model:=App)
        End Function

        ''' <summary>
        ''' Handles typeaheads for Event Log
        ''' </summary>
        ''' <param name="query">Value of user input field.</param>
        ''' <param name="columnName">Column to search for the typeahead.</param>
        ''' <returns>A list of string that contains the suggestions for the desireed column name</returns>
        ''' <remarks></remarks>
        Function eventLogTypeAhead(query As String, columnName As String, sDate As Date, eDate As Date) As ActionResult
            Dim search ' varies as to what it is.  Can be list of string, list of object, etc.  Can't declare type without having more info
            sDate = sDate.AddDays(-1)
            eDate = eDate.AddDays(1)
            Select Case columnName
                Case "Message"
                    search = EventLog.getMessageDrop(query, sDate, eDate, User.Identity.Name, Session("WSID"))
                Case "Event Location"
                    search = EventLog.getEventLocDrop(query, sDate, eDate, User.Identity.Name, Session("WSID"))
                Case "Name Stamp"
                    search = EventLog.getNameStampDrop(query, sDate, eDate, User.Identity.Name, Session("WSID"))
                Case "Event Code"
                    search = EventLog.getEventCodeDrop(query, sDate, eDate, User.Identity.Name, Session("WSID"))
                Case "Event Type"
                    search = EventLog.getEventTypeDrop(query, sDate, eDate, User.Identity.Name, Session("WSID"))
                Case Else
                    Return Json(New List(Of String), JsonRequestBehavior.AllowGet)
            End Select

            Return Json(search, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Selects Event Log data for display.
        ''' </summary>
        ''' <param name="data">DataTables custom object with parameters to retrieve specific data (records 1-10 or 11-20, etc.)</param>
        ''' <returns>Json object render of the list of list of string of data in the Event Log selected by the SP in EventLog.selEventLog()</returns>
        ''' <remarks></remarks>
        Function getEventLog(ByVal data As TableObjectSent) As ActionResult

            Return Json(EventLog.selEventLog(data.draw, data.start + 1, data.length + data.start, data.messageFilter, data.eventLocation, IIf(IsNothing(data.transStatus), "", data.transStatus), IIf(IsNothing(data.transType), "", data.transType), _
                                             data.nameStamp, data.sDate, data.eDate, Request.QueryString.Get("order[0][column]"), _
                                             Request.QueryString.Get("order[0][dir]"), data.filter, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Exports records form the event log by the given filters
        ''' </summary>
        ''' <param name="sDate">The start date for the records</param>
        ''' <param name="eDate">The end date for the records</param>
        ''' <param name="message">The message that records have</param>
        ''' <param name="eLocation">The location that each record occurred in</param>
        ''' <param name="nStamp">The name stamp attached ot each record</param>
        ''' <returns>A boolean telling if the export was successful</returns>
        Function singleExport(sDate As String, eDate As String, message As String, eLocation As String, nStamp As String) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selEventLogExport"
                Dim params As String(,) = {{"@sDate", sDate, dteVar}, _
                                        {"@eDate", eDate, dteVar}, _
                                        {"@message", message, strVar}, _
                                        {"@eLocation", eLocation, strVar}, _
                                        {"@nStamp", nStamp, strVar}}
                Dim filename As String = "EventLogExport.lst", LLType As String = "List"


                Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "xlsx", sp, params, filename)
                Clients.Export(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("EventLogController", "printELReport", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Calls Windows Service function through the Hub connection to print an Event Log report.
        ''' </summary>
        ''' <param name="sDate">Start date filter.</param>
        ''' <param name="eDate">End date filter.</param>
        ''' <param name="eID">Event ID filter.  If set to -1 it is ignored as a filter, otherwise the report will contain only the data matching the Event ID.</param>
        ''' <param name="message">Message filter.</param>
        ''' <param name="eLocation">Event Location filter.</param>
        ''' <param name="nStamp">Name Stamp or User filter.</param>
        ''' <returns>Task to prevent issues with timeouts.</returns>
        ''' <remarks></remarks>
        Public Function printELReport(sDate As String, eDate As String, eID As Integer, message As String, eLocation As String, nStamp As String) As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance


            Try
                Dim sp As String = "selEventLogLL"
                Dim params As String(,) = {{"@sDate", sDate, dteVar}, {"@eDate", eDate, dteVar}, {"@eID", eID, intVar}, _
                                            {"@message", message, strVar}, {"@eLocation", eLocation, strVar}, {"@nStamp", nStamp, strVar}}
                Dim filename As String = "EventLog.lst", LLType As String = "List"


                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Event Log Report", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("EventLogController", "printELReport", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace