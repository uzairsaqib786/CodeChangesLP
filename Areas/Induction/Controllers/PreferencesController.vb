' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Induction.Controllers
    <Authorize()>
    Public Class PreferencesController
        Inherits PickProController

        ' GET: /Preferences
        ''' <summary>
        ''' Gets the preferences for page load
        ''' </summary>
        ''' <returns>A view object that contains the information needed for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Return View(New With {.Prefs = Induction.Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name), _
                                  .UserData = Induction.Preferences.selectRTSUserFieldData(Session("WSID"), User.Identity.Name)})
        End Function
    End Class
End Namespace