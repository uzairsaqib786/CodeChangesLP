' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Namespace FlowRackReplenish
    Public Class FlowRackReplenishHub
        Inherits Hub

        ''' <summary>
        ''' Sees if the item number has a carton flow pick zone
        ''' </summary>
        ''' <param name="itemNum">The item number to check</param>
        ''' <returns>A string telling if the item number has a carton flow pick zone</returns>
        Public Function getCFData(itemNum As String) As String
            Dim newItemNum As String = ""
            newItemNum = getCFItemNum(itemNum)
            If (newItemNum Is "") Then
                newItemNum = Nothing
            End If
            Return newItemNum
        End Function

        'Selects the item number from the inventory table.
        ''' <summary>
        ''' Selects all the information for the given item number
        ''' </summary>
        ''' <param name="itemNum"></param>
        ''' <returns>A string that tells the item number</returns>
        Private Function getCFItemNum(itemNum As String) As String
            Dim newItemNum As String = ""
            newItemNum = GetResultSingleCol("selInventoryCFItemNumber", Context.QueryString.Get("WSID"), {{"@itemNumber", itemNum, strVar}})
            Return newItemNum
        End Function

        'Selects all the locations that the entered item exists in.
        ''' <summary>
        ''' Selects the locations and their info that the desired item is in 
        ''' </summary>
        ''' <param name="itemNum">The item number that is going to be searched for</param>
        ''' <returns>A list of dictionary that contains the information on all the locations that cotnaisn the desired item number</returns>
        Public Function getItemLoc(itemNum As String) As List(Of Dictionary(Of String, Object))
            Dim Locations = New List(Of Dictionary(Of String, Object))
            Locations = GetResultMapList("selInvMapItemLocByZone", Context.QueryString.Get("WSID"), {{"@Zone", getWSLoc(), strVar}, {"@itemNumber", itemNum, strVar}})
            Dim usedLocations = Locations.Where(Function(loc) loc("ItemQuantity") <> 0).OrderBy(Function(loc) loc("ItemQuantity")).ToList()
            Dim emptyLocations = Locations.Where(Function(loc) loc("ItemQuantity") = 0).ToList()
            usedLocations.AddRange(emptyLocations)
            If (usedLocations.Count > 0) Then
                usedLocations.AddRange(getOpenLoc())
            End If
            Return usedLocations
        End Function

        ''' <summary>
        ''' Selects a table of all open locations in the inventory map table.
        ''' </summary>
        ''' <returns>A list of dictionary that contains the information for all open locations</returns>
        Public Function getOpenLoc() As List(Of Dictionary(Of String, Object))
            Dim Locations = New List(Of Dictionary(Of String, Object))
            Locations = GetResultMapList("selInvMapOpenLoc", Context.QueryString.Get("WSID"), {{"@Zone", getWSLoc(), strVar}})
            For x As Integer = 0 To Locations.Count - 1
                For Each obj In Locations
                    obj("ItemQuantity") = "Open"
                Next
            Next
            Return Locations
        End Function

        ''' <summary>
        ''' Verifies the location entered is assigned to that item or has no item assigned to it.
        ''' </summary>
        ''' <param name="itemNum">The item number to check</param>
        ''' <param name="input">The location that is checked for the item number</param>
        ''' <returns>A boolean that tells if the location is assigned to that item</returns>
        Public Function verifyItemLoc(itemNum As String, input As String) As Boolean
            Dim records As Integer = 0
            records = GetResultSingleCol("selInvMapVerifyLoc", Context.QueryString.Get("WSID"), {{"@Zone", getWSLoc(), strVar}, {"@itemNumber", itemNum, strVar}, {"@input", input, strVar}})
            Return records = 1
        End Function

        ''' <summary>
        ''' Verifies that the item quantity being entered will not exceed the limit for that location.
        ''' </summary>
        ''' <param name="itemNum">The item numbe rthat is being added</param>
        ''' <param name="input">The location that is being checked</param>
        ''' <param name="itemQty">The quantity that is being added</param>
        ''' <returns>A boolean telling if the number of records will exceeed the location</returns>
        Public Function verifyItemQty(itemNum As String, input As String, itemQty As Integer) As Boolean
            Dim records As Integer = 0
            records = GetResultSingleCol("selInvMapVerifyQty", Context.QueryString.Get("WSID"), {{"@Zone", getWSLoc(), strVar}, {"@itemNumber", itemNum, strVar}, {"@input", input, strVar}, {"@itemQuantity", itemQty, intVar}})
            Return records = 1
        End Function

        ''' <summary>
        ''' Updates the item quantity of a record in the inventory map.
        ''' </summary>
        ''' <param name="itemNum">The item number for the location</param>
        ''' <param name="input">The location that is getting updated</param>
        ''' <param name="itemQty">The quantity that is getting added</param>
        Sub updateItemQty(itemNum As String, input As String, itemQty As Integer)
            GetResultSingleCol("updateInvMapQty", Context.QueryString.Get("WSID"), {{"@Zone", getWSLoc(), strVar}, {"@itemNumber", itemNum, strVar}, {"@input", input, strVar}, {"@itemQuantity", itemQty, intVar}})
        End Sub

        ''' <summary>
        ''' selects Workstation Preferences table and gets the Carton Flow ID.
        ''' </summary>
        ''' <returns>A string that tells the location for the given workstation id</returns>
        Public Function getWSLoc() As String
            Dim WSZone As String = Nothing
            WSZone = GetResultSingleCol("selWSPrefCFID", Context.QueryString.Get("WSID"), {{"@wsid", Context.QueryString.Get("WSID"), strVar}})
            Return WSZone
        End Function

        ''' <summary>
        ''' This method is currently not being used.
        ''' </summary>
        ''' <returns>A string of the zones for the given workstation id</returns>
        Public Function getWSPickZones() As String
            Return GetResultSingleCol("selWSPickZones", Context.QueryString.Get("WSID"), {{"@wsid", Context.QueryString.Get("WSID"), strVar}})
        End Function

    End Class
End Namespace

