' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace Controllers
    <Authorize()>
    Public Class InventoryMasterController
        Inherits PickProController

        ' GET: /InventoryMaster
        ''' <summary>
        ''' Returns the Inventory Master view
        ''' </summary>
        ''' <param name="ItemNumber">Item Number (optional) to load with data already filled out</param>
        ''' <returns>A view object that contains the information for the html page</returns>
        ''' <remarks></remarks>
        Function Index(ByVal ItemNumber As String, App As String, Optional NewItem As Boolean = False) As ActionResult
            Return View(New With {.Alias = New AliasModel(User.Identity.Name, Session("WSID")), .ItemNum = ItemNumber, .App = App, _
                                  .FirstItemNum = InventoryMaster.selectNextItemNum("", "", 0, User.Identity.Name, Session("WSID")), _
                                  .CountData = InventoryMaster.selectItemNumFilterCount(ItemNumber, "1=1", User.Identity.Name, Session("WSID")), .NewItem = NewItem})
        End Function

        ''' <summary>
        ''' Handles typeahead for stock code (Item Number/Kit Number)
        ''' </summary>
        ''' <param name="stockCode">Item Number/Stock Code/Kit Number</param>
        ''' <returns>Suggestions for typeahead of Item Numbers, etc.</returns>
        ''' <remarks></remarks>
        Function stockCodeTypeahead(stockCode As String) As ActionResult
            Return Json(InventoryMaster.stockCodeTypeahead(stockCode, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Selects Inventory Master Locations table.
        ''' </summary>
        ''' <param name="data">DataTables custom object with parameters defined to restrict selected results.</param>
        ''' <returns>Inventory Master Locations table as a list of list of string as a Json object.</returns>
        ''' <remarks></remarks>
        Function locationsTable(data As TableObjectSent) As ActionResult
            Return Json(InventoryMaster.selInventoryMasterLocations(data.draw, data.itemNumber, data.start + 1, data.start + data.length, _
                                                                    User.Identity.Name, Request.QueryString.Get("order[0][column]"), _
                                                                    Request.QueryString.Get("order[0][dir]"), Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints a kit report.
        ''' </summary>
        ''' <param name="kitItemNumber">Kit to print.</param>
        ''' <returns>A boolean telling if the print job completed successfully</returns>
        ''' <remarks></remarks>
        Public Function printKitReport(kitItemNumber As String) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selKitInventoryLL"
                Dim params As String(,) = {{"@ItemNumber", kitItemNumber, strVar}}
                Dim filename As String = "KitDetail.lst", LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Inventory Kit Report", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("InventoryMasterController", "PrintKitReport", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        Public Function convertToItemNum(Val As String) As ActionResult
            Dim DataReader As SqlDataReader = Nothing, ItemNum As String = ""

            Try
                DataReader = RunSPArray("selInvItemFromVal", Session("WSID"), {{"@Value", Val, strVar}})

                If DataReader.HasRows Then
                    DataReader.Read()
                    ItemNum = DataReader("Item Number")
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Inventory Master", "ConvertToItemNum", ex.Message, User.Identity.Name, Session("WSID"))
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Json(ItemNum, JsonRequestBehavior.AllowGet)
        End Function

    End Class
End Namespace
