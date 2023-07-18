' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Controllers
    <Authorize()>
    Public Class InventoryMapController
        Inherits PickProController

        ' GET: InventoryMap
        ''' <summary>
        ''' Returns the Inventory Map view
        ''' </summary>
        ''' <param name="ItemNumber">Item Number to filter on initially</param>
        ''' <returns>A view object that contains the information needed for the html page</returns>
        ''' <remarks></remarks>
        Function Index(ByVal ItemNumber As String, App As String) As ActionResult
            ' rename the columns that are named differently in the table vs. PickPro
            Dim rename As List(Of String) = GlobalFunctions.getDefaultColumnSequence("Inventory Map", User.Identity.Name, Session("WSID"))
            Return View(New InventoryMapModel(rename, TryCast(Session("Permissions"), List(Of String)), If(Session("Admin"), "Administrator", "Not Administrator"), ItemNumber, App, New AliasModel(User.Identity.Name, Session("WSID"))))
        End Function

        ''' <summary>
        ''' Selects filtered information from Inventory Map for display.
        ''' </summary>
        ''' <param name="data">Sent DataTables information as custom object.</param>
        ''' <returns>A table object that contains all the data needed for the datatabes</returns>
        ''' <remarks></remarks>
        Function invMap(ByVal data As TableObjectSent) As ActionResult
            If data.searchString = Nothing Then
                data.searchString = ""
            End If
            Return Json(InventoryMap.selInventoryMap(data.draw, data.start + 1, data.length + data.start, _
                                                     data.searchString, data.searchColumn, Request.QueryString.Get("order[0][column]"), _
                                                          Request.QueryString.Get("order[0][dir]"), data.filter, User.Identity.Name, data.OQA, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the list of reccomendations for the type ahead
        ''' </summary>
        ''' <param name="location">The location to lookup by</param>
        ''' <param name="zone">The zone to look up by</param>
        ''' <returns>List of object that contains the typeahead data</returns>
        Function getInvMapLocZoneTypeahead(location As String, zone As String) As ActionResult
            Return Json(InventoryMap.getLocationZoneTypeahead(location, zone, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Handles search box typeahead
        ''' </summary>
        ''' <param name="query">User input from client side.</param>
        ''' <returns>Json object with the typeahead data contained within.</returns>
        ''' <remarks></remarks>
        Function getInvMapSearchStringTypeahead(query As String, column As String)
            Return Json(InventoryMap.getTypeaheadInvMap(column, query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function


       ''' <summary>
        ''' Calls Windows service function through the Hub connection in order to pritn an Inventory Map report with the specified filters.
        ''' </summary>
        ''' <param name="invMapID">Inv Map ID to print.  Set to 0 or less if more than one entry is to be printed.</param>
        ''' <param name="groupLikeLoc">Group like locations boolean which specifies whether matching locations that have multiple entries in the database should be printed anyway.</param>
        ''' <param name="startLoc">Starting Location (alphanumerically sorted)</param>
        ''' <param name="endLoc">Ending Location (alphanumerically sorted)</param>
        ''' <returns>Task to prevent any issues with timeouts.</returns>
        ''' <remarks></remarks>
        Public Function printIMReport(invMapID As Integer, groupLikeLoc As Boolean, startLoc As String, endLoc As String) As ActionResult
            ' calls windows service procedure for printing after setting up the print
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selInvMapLabelsLLNew"
                Dim params As String(,) = {{"@invMapID", invMapID, intVar}, _
                                           {"@groupLikeLoc", groupLikeLoc, boolVar}, {"@beginLoc", startLoc, strVar}, _
                                           {"@endLoc", endLoc, strVar}, {"@User", username, strVar}}
                Dim filename As String = "LocLabel.lbl", LLType As String = "Label"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Location Label", LLType, filename, sp, params)
                Clients.Print(m)

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("InventoryMapController", "printIMReport", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace