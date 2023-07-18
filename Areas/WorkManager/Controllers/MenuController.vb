' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace WorkManager.Controllers
    <Authorize()>
    Public Class MenuController
        Inherits PickProController

        ' GET: Menu
        ''' <summary>
        ''' Inserts a new user and/or workstation when this page loads for the first time.  Subsequent loads this just renders the menu.
        ''' </summary>
        ''' <returns>A view object that contains the data for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Dim WSID As String = Session("WSID"), u As String = User.Identity.Name
            WorkManager.WMPreferences.InsertNewUserOrWSID(u, u, WSID)
            Return View(model:=WorkManager.WMPreferences.GetWMPermissions(User.Identity.Name, Session("WSID")))
        End Function
    End Class
End Namespace