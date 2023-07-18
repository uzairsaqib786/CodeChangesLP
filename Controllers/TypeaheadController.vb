' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient
<Authorize()>
Public Class TypeaheadController
    Inherits PickProController

    ''' <summary>
    ''' Gets Locations and zones for typeahead
    ''' </summary>
    ''' <param name="query">Location being typed in by user</param>
    ''' <returns>A list of object that contains hte typeahead information</returns>
    ''' <remarks></remarks>
    Function getLocations(query As String) As ActionResult
        Return Json(Locations.getLocationDrop(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Gets Item Numbers for typeahead
    ''' </summary>
    ''' <param name="query">Item Number for typeahead</param>
    ''' <returns>A list of the ItemNumberDropObj model that contains the data for the typeahead</returns>
    ''' <remarks></remarks>
    Function getItem(query As String, Optional beginItem As String = "---") As ActionResult
        Return Json(ItemNumber.getItem(query, User.Identity.Name, beginItem, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Gets location suggestions for begin for printing on inventory map page
    ''' </summary>
    ''' <param name="query">Location to get suggestions for</param>
    ''' <param name="unique">Group by location field</param>
    ''' <returns>A list of object that contains the beginning location typeahead information</returns>
    ''' <remarks></remarks>
    Function getLocationBegin(query As String, unique As Boolean) As ActionResult
        Return Json(PrintLocations.getLocationBegin(query, unique, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Gets location suggestions for end for printing on inventory map page
    ''' </summary>
    ''' <param name="query">Location to get suggestions for</param>
    ''' <param name="unique">Group by location field</param>
    ''' <returns>A list of object that contains the end location typeahead information</returns>
    ''' <remarks></remarks>
    Function getLocationEnd(query As String, unique As Boolean, beginLoc As String) As ActionResult
        Return Json(PrintLocations.getLocationEnd(query, beginLoc, unique, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Gets typeahead for supplier item id
    ''' </summary>
    ''' <param name="query">supplier item id</param>
    ''' <returns>A list of object that contains the information for the supplier item id typeahead</returns>
    ''' <remarks></remarks>
    Function getSupplierItemID(query As String) As ActionResult
        Return Json(SupplierItemID.getSupplierItemIDTypeahead(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Gets the Reprocess Transactions typeahead suggestions
    ''' </summary>
    ''' <param name="ItemNumber">Item Number to match against</param>
    ''' <param name="OrderNumber">Order Number to match against</param>
    ''' <returns>A list of object that contains the data for the Reprocess typeahead</returns>
    ''' <remarks></remarks>
    Function getReprocessTypeahead(ItemNumber As String, OrderNumber As String) As ActionResult
        Return Json(ReprocessTransactions.getReprocessTypeahead(ItemNumber, OrderNumber, User.Identity.Name, Session("WSID"), False), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Gets the Reprocess Transactions typeahead suggestions
    ''' </summary>
    ''' <param name="ItemNumber">Item Number to match against</param>
    ''' <param name="OrderNumber">Order Number to match against</param>
    ''' <returns>A list of object that contains the data for the Reprocess history typeahead</returns>
    ''' <remarks></remarks>
    Function getReprocessHistTypeahead(ItemNumber As String, OrderNumber As String) As ActionResult
        Return Json(ReprocessTransactions.getReprocessTypeahead(ItemNumber, OrderNumber, User.Identity.Name, Session("WSID"), True), JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Gets adjustment reasons for the item quantity modal in inventory map
    ''' </summary>
    ''' <param name="query">Adjustment reason to match against</param>
    ''' <returns>A list of string that contains the reasons for the typeahead</returns>
    ''' <remarks></remarks>
    Function getAdjustmentReasons(query As String) As ActionResult
        Dim datareader As SqlDataReader = Nothing
        Dim typeahead As New List(Of String)
        Try
            datareader = RunSPArray("selAdjustmentReasonTypeahead", Session("WSID"), {{"@Reason", query & "%", strVar}})

            If datareader.HasRows Then
                While datareader.Read()
                    If Not IsDBNull(datareader(0)) Then
                        typeahead.Add(datareader(0))
                    End If
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("TypeaheadController", "getAdjustmentReasons", ex.ToString(), User.Identity.Name, Session("WSID"))
        Finally
            If Not IsNothing(datareader) Then
                datareader.Dispose()
            End If
        End Try
        Return Json(typeahead, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' Gets the typeahead for user fields 1/2
    ''' </summary>
    ''' <param name="query">Value to get suggestions for</param>
    ''' <param name="ufs">1/2 for uf 1/2</param>
    ''' <returns>A list of string that contains the data for the user fields typeahead</returns>
    ''' <remarks></remarks>
    Function getUFs(query As String, ufs As Integer) As ActionResult
        Return Json(UserFields.getTypeahead(query, ufs, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
    End Function


    ''' <summary>
    ''' Selects Typeahead Data for Basic Reports Filters
    ''' </summary>
    ''' <param name="query">The value to get sugguestions for</param>
    ''' <param name="report">The selected report</param>
    ''' <param name="column">THe column that the suggestions are being pulled from</param>
    ''' <returns>A list of string that contains the data for the typeahead</returns>
    ''' <remarks></remarks>
    Function getReportFilterTypeahead(query As String, report As String, column As String)
        Dim reportdata As CustomReportsModel = CustomReports.chooseView(report, User.Identity.Name, Session("WSID"), False)
        Dim returnList As New List(Of String)

        Dim DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selGenericTA", Session("WSID"), {{"@View", reportdata.REPORTVIEW, strVar}, {"@Col", column, strVar}, {"@Val", query, strVar}})
            If DataReader.HasRows Then
                Dim type As String = getDataReaderFieldTypes(DataReader)(0)
                While (DataReader.Read())
                    If type.ToUpper() = "BIT" Then
                        returnList.Add(IIf(DataReader(0), 1, 0))
                    Else
                        returnList.Add(DataReader(0))
                    End If
                End While
            End If
        Catch ex As Exception
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return Json(returnList, JsonRequestBehavior.AllowGet)
    End Function



End Class