' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Induction.Controllers
    <Authorize()>
    Public Class MenuController
        Inherits PickProController

        ' GET: Menu
        ''' <summary>
        ''' Gets the menu page
        ''' </summary>
        ''' <returns>An empty view object</returns>
        Function Index() As ActionResult
            Return View(New With {.Preferences = Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name)})
        End Function

        ''' <summary>
        ''' Gets the admin menu page
        ''' </summary>
        ''' <returns>A view object that contains the information for the html page</returns>
        Function Admin() As ActionResult
            Return View(New With {.preferences = Induction.Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name)})
        End Function

        ''' <summary>
        ''' The from part of the from tote id to tote id typeahead
        ''' </summary>
        ''' <param name="query">typeahead value that is entered</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function CreateFromToteIDTypeahead(query As String) As ActionResult
            Return Json(Induction.ToteManager.selFromToteID(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' The to part of the from tote id to tote id typeahead
        ''' </summary>
        ''' <param name="query">typeahead value that is entered</param>
        ''' <param name="FromTote"> The from tote id which limits results to those that are after this tote id</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function CreateToToteIDTypeahead(query As String, FromTote As String) As ActionResult
            Return Json(Induction.ToteManager.selToToteID(query, FromTote, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace