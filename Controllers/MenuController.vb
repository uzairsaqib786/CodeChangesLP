' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Controllers
    <Authorize()>
    Public Class MenuController
        Inherits PickProController

        ' GET: Menu
        ''' <summary>
        ''' Returns the menu page
        ''' </summary>
        ''' <returns>An empty view object for the menu html page</returns>
        Function Index() As ActionResult
            Return View()
        End Function
    End Class
End Namespace