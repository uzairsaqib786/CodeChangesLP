' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace WorkManager.Controllers
    <Authorize()>
    Public Class OrganizeWorkController
        Inherits PickProController

        ' GET: /OrganizeWork
        ''' <summary>
        ''' Returns the model to the html page
        ''' </summary>
        ''' <returns>A view object that contains the data for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Dim permissions As List(Of Boolean) = WorkManager.WMPreferences.GetWMPermissions(User.Identity.Name, Session("WSID"))
            If permissions.Count = 0 OrElse permissions(1) <> True Then Return Redirect("~/WM/")
            Return View(model:=New With {.Info = WorkManager.OrganizeWork.getOrgainzeWorkData(User.Identity.Name, Session("WSID")), .Perms = permissions})
        End Function

        ''' <summary>
        ''' Gets the data for the worker typeahead
        ''' </summary>
        ''' <param name="worker">The value to get suggestions for</param>
        ''' <returns>json object containing the typeahead data</returns>
        ''' <remarks></remarks>
        Function GetWorkerTA(worker As String, all As Boolean) As ActionResult
            Return Json(WorkManager.OrganizeWork.GetWorkerTA(worker, User.Identity.Name, Session("WSID"), IIf(all, 1, 0)), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the data for the organize wortk table
        ''' </summary>
        ''' <param name="data">Object that contains the data in order to get the data for the table</param>
        ''' <returns>json object containing the table data</returns>
        ''' <remarks></remarks>
        Function getOrgainzeWorkTable(ByVal data As TableObjectSent) As ActionResult
            Return Json(WorkManager.OrganizeWork.getOrgainzeWorkTable(data.draw, data.start + 1, data.length + data.start, _
                                                                        Request.QueryString.Get("order[0][column]"), _
                                                                        Request.QueryString.Get("order[0][dir]"), data.filter, _
                                                                        User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace