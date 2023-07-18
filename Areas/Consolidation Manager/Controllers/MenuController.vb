' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Consolidation.Controllers
    <Authorize()>
    Public Class MenuController
        Inherits PickProController

        '
        ' GET: /Home
        ''' <summary>
        ''' Gets the home page
        ''' </summary>
        ''' <returns>Empty view object</returns>
        Function Index() As ActionResult
            Return View()
        End Function

        ''' <summary>
        ''' Gets the staging locations page
        ''' </summary>
        ''' <returns>Empty view object</returns>
        Function StagingLocations() As ActionResult
            Return View()
        End Function

    End Class
End Namespace
