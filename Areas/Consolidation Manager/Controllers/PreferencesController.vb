' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace Consolidation.Controllers
    <Authorize()>
    Public Class PreferencesController
        Inherits PickProController

        '
        ' GET: /Preferences
        ''' <summary>
        ''' Gets the prefrences page
        ''' </summary>
        ''' <returns>View object that contains the information needed for the html page</returns>
        Function Index() As ActionResult
            Return View(Preferences.selectCMPrefs(User.Identity.Name, Session("WSID")))
        End Function

    End Class
End Namespace
