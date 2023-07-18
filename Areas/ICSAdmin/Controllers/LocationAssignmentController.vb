' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Admin.Controllers
    <Authorize()>
    Public Class LocationAssignmentController
        Inherits ICSAdminController

        ' GET: LocationAssignment
        ''' <summary>
        ''' Returns the Location Assignment view
        ''' </summary>
        ''' <returns>A view object that contains data for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Dim prefs = LocationAssignment.GetPreferences(Session("WSID"))
            Return View(New With {.CountData = LocationAssignment.getLocAssCountData(User.Identity.Name, Session("WSID")),
                                  .CountTable = LocationAssignment.getLocAssCountTable(User.Identity.Name, Session("WSID")),
                                  .PickTable = LocationAssignment.getLocAssPickTable(User.Identity.Name, Session("WSID"), ""),
                                  .PutAwayTable = LocationAssignment.getLocAssPutAwayTable(User.Identity.Name, Session("WSID")),
                                  .AutoLocPicks = CBool(prefs("AutoLocPicks")), .AutoLocPuts = CBool(prefs("AutoLocPutAways")),
                                  .AutoLocCounts = CBool(prefs("AutoLocCounts"))})
        End Function

        ''' <summary>
        ''' Previews the location assignment pick shortage report
        ''' </summary>
        ''' <returns>A boolean that tells if the preview job was successsful</returns>
        ''' <remarks></remarks>
        Public Function PreviewLocAssPickShort() As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selPickShortageReportLL"
                Dim params As String(,) = {{"nothing"}}
                Dim filename As String = "LocAssPickShortage.lst", LLType As String = "List"

                Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("LocationAssignmentController", "PreviewLocAssPickShort", ex.Message, username, WSID)

            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Previews the location assignment forward pick zone pick shortage report
        ''' </summary>
        ''' <returns>A boolean that tells if the preview job was successsful</returns>
        ''' <remarks></remarks>
        Public Function PreviewLocAssPickShortFPZ() As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selPickShortageReportFPZLL"
                Dim params As String(,) = {{"nothing"}}
                Dim filename As String = "LocAssPickShortageFPZ.lst", LLType As String = "List"

                Dim m As ExportServiceModel = ListLabelHelperFunctions.GetStandardLLExportProperties(username, WSID, Server, "preview", sp, params, filename)
                Return PartialView("~/Views/CustomReports/_LLViewer.vbhtml", m)

            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("LocationAssignmentController", "PreviewLocAssPickShortFPZ", ex.Message, username, WSID)

            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace