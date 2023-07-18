' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Namespace Admin.Controllers
    <Authorize()>
    Public Class InventoryDetailController
        Inherits ICSAdminController

        Function Index() As ActionResult
            Return View(New With {.Alias = New AliasModel(User.Identity.Name, Session("WSID"))})
        End Function

        ''' <summary>
        ''' Gets the item number typeahead for the inventory detail page.
        ''' </summary>
        ''' <param name="itemNum">The value to get the suggestions by</param>
        ''' <returns>A list of string that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function itemNumberTypeahead(itemNum As String) As ActionResult
            Return Json(InventoryDetail.selectItemNumberTypeahead(itemNum, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

    End Class
End Namespace

