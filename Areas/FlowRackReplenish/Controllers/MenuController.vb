' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports PickPro_Web

Namespace FlowRackReplenish.Controllers
    <Authorize>
    Public Class MenuController
        Inherits PickProController

        ' GET: Menu
        ''' <summary>
        ''' Gets the mneu page
        ''' </summary>
        ''' <returns>An empty view object</returns>
        Function Index() As ActionResult
            Return View()
        End Function
    End Class
End Namespace