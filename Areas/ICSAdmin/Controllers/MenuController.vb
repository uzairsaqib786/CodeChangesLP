' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace Admin.Controllers
    <Authorize()>
    Public Class MenuController
        Inherits ICSAdminController

        ' GET: Menu
        ''' <summary>
        ''' Returns the Menu view.
        ''' </summary>
        ''' <returns>Returns the view with a MenuObj instance as a parameter for use as a Model in the HTML.</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Dim pod As String = Menu.getPOD(User.Identity.Name, Session("WSID"))
            Dim obj As Object = Menu.getMenuData(User.Identity.Name, Session("WSID"))
            Dim td As List(Of List(Of String)) = obj.table
            Dim counts As List(Of String) = obj.counts
            Dim diag As Boolean = Menu.getDiagMode(User.Identity.Name, Session("WSID"))
            Dim PickWorkstat As Boolean = Menu.grabPickWorkstation(User.Identity.Name, Session("WSID"))
            Return View(New MenuObj(td, counts, pod, diag, PickWorkstat))
        End Function

    End Class
End Namespace