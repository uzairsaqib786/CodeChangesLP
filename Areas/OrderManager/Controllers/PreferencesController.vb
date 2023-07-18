' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace OrderManager.Controllers
    <Authorize()>
    Public Class PreferencesController
        Inherits PickProController

        ' GET: /Preferences
        ''' <summary>
        ''' Gets the preferences page
        ''' </summary>
        ''' <returns>A view object that contains the information for the html page</returns>
        Function Index() As ActionResult
            Return View(New With {.Prefs = OrderManager.Preferences.selectOMPreferences(Session("WSID"), User.Identity.Name)})
        End Function
    End Class
End Namespace