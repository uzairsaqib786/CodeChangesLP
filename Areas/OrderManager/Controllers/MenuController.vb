' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace OrderManager.Controllers
    <Authorize()>
    Public Class MenuController
        Inherits PickProController

        ' GET: Menu
        ''' <summary>
        ''' Gets the menu page
        ''' </summary>
        ''' <returns>A view object that contains the data for the html page</returns>
        Function Index() As ActionResult
            Return View(New With {.CountData = OrderManager.Menu.selectOMCountData(Session("WSID"), User.Identity.Name)})
        End Function
    End Class
End Namespace