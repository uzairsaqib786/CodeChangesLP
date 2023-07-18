' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System
Imports System.Web
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Namespace Admin
    Public Class InventoryMasterHub
        Inherits Hub

        Public Overrides Function OnConnected() As Task
            'Adds a user to their own unique group by a value passed in during connection
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
            'Calls the Original Onconnected Function pass control back to the server
            Return MyBase.OnConnected()
        End Function

        ''' <summary>
        ''' Adds a new item number and description to the inventory table
        ''' </summary>
        ''' <param name="itemNumber">The item number to be created</param>
        ''' <param name="description">The description for the new item number</param>
        ''' <returns>A boolean true if the operation was successful, and false if the operation was unsuccessful</returns>
        ''' <remarks></remarks>
        Public Function addNewItem(itemNumber As String, description As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Try
                                                             DataReader = RunSPArray("insInventory", Context.QueryString.Get("WSID"), {{"@ItemNum", itemNumber, strVar}, _
                                                                                           {"@Description", description, strVar}})
                                                             If DataReader.HasRows Then
                                                                 Return False
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "addNewItem", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function


        ''' <summary>
        ''' Toggles quarantine status on an item.
        ''' </summary>
        ''' <param name="item">Item Number/Stock Code/SKU to toggle quarantine.</param>
        ''' <param name="unquar">Boolean - true when unquarantining an item.</param>
        ''' <param name="append">Boolean - true when user wants to append all reprocess transactions of same item number to open transactions.</param>
        ''' <returns>Task to prevent timeout issues</returns>
        ''' <remarks></remarks>
        Public Function quarantineItem(item As String, unquar As Boolean, append As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             Dim confirm As Integer = IIf(append, 1, 0), SP As String = ""
                                             Dim params As String(,)

                                             If unquar Then
                                                 SP = "updInvMapOTUnQuarantineInvMast"
                                                 params = {{"@ItemNum", item, strVar}, {"@Confirm", confirm, intVar}, {"@XferBy", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                             Else
                                                 SP = "updInvMapOTQuarantineInvMast"
                                                 params = {{"@ItemNum", item, strVar}, {"@XferBy", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                             End If

                                             Try
                                                 RunActionSP(SP, Context.QueryString.Get("WSID"), params)
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.ToString())
                                                 insertErrorMessages("Inventory Master", "quarantineItem", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Deletes specified item number from inventory if it is not present in Inventory Map
        ''' </summary>
        ''' <param name="item">Item to be deleted</param>
        ''' <returns>Boolean - whether delete was successful.  False if the item is not deallocated from Inventory Map.</returns>
        ''' <remarks></remarks>
        Public Function deleteItem(item As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing, success As Boolean = False

                                                         Try
                                                             DataReader = RunSPArray("delInventoryItemNum", Context.QueryString.Get("WSID"), {{"@ItemNum", item, strVar},
                                                                                           {"@XferBy", Context.User.Identity.Name, strVar},
                                                                                           {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                             If DataReader.HasRows Then
                                                                 If DataReader.Read() Then
                                                                     If Not IsDBNull(DataReader(0)) Then
                                                                         If DataReader(0) = 0 Then
                                                                             success = True
                                                                         End If
                                                                     End If
                                                                 End If
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "deleteItem", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return success
                                                     End Function)
        End Function

        ''' <summary>
        ''' Calls the fucntion to grab all the data for an item number
        ''' </summary>
        ''' <param name="itemNum"> The item number whose datat will be displayed</param>
        ''' <returns>A list of object that contains all the data for the selected item number</returns>
        ''' <remarks></remarks>
        Public Function selInvMasterData(itemNum As String) As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function()
                                                                 Return InventoryMaster.selInventoryMasterData(itemNum, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             End Function)
        End Function

        ''' <summary>
        ''' Retrieves data for modals by viewName.  (Cell Sizes, Velocity Codes, etc.)
        ''' </summary>
        ''' <param name="viewName">Modal name to retrieve data for.</param>
        ''' <returns>Returns data for specified viewName</returns>
        ''' <remarks></remarks>
        Public Function getEditView(viewName As String) As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function()
                                                        Dim DataReader As SqlDataReader = Nothing, vals As New Object
                                                        Try
                                                            Select Case viewName
                                                                Case "Cell Size"
                                                                    vals = New List(Of Object)
                                                                    Dim CellSize As String = ""
                                                                    Dim CellType As String = ""
                                                                    DataReader = RunSPArray("selCellSize", Context.QueryString.Get("WSID"), {{"Nothing"}})
                                                                    If DataReader.HasRows Then
                                                                        While DataReader.Read()
                                                                            CellSize = ""
                                                                            CellType = ""
                                                                            If Not IsDBNull(DataReader(0)) Then
                                                                                CellSize = (DataReader(0))
                                                                            Else
                                                                                CellSize = ""
                                                                            End If
                                                                            If Not IsDBNull(DataReader(1)) Then
                                                                                CellType = (DataReader(1))
                                                                            Else
                                                                                CellType = ""
                                                                            End If
                                                                            vals.add(New With {.CellSize = CellSize, .CellType = CellType})
                                                                        End While
                                                                    End If

                                                                Case "Velocity Code"
                                                                    vals = New List(Of String)
                                                                    DataReader = RunSPArray("selGoldZone", Context.QueryString.Get("WSID"), {{"Nothing"}})
                                                                    If DataReader.HasRows Then
                                                                        While DataReader.Read()
                                                                            For x As Integer = 0 To DataReader.FieldCount - 1
                                                                                If Not IsDBNull(DataReader(x)) Then
                                                                                    vals.Add(DataReader(x))
                                                                                Else
                                                                                    vals.Add("")
                                                                                End If
                                                                            Next
                                                                        End While
                                                                    End If
                                                                Case "Category"
                                                                    vals = New List(Of Object)
                                                                    Dim Category As String = ""
                                                                    Dim SubCategory As String = ""
                                                                    DataReader = RunSPArray("selInvCategories", Context.QueryString.Get("WSID"), {{"Nothing"}})
                                                                    If DataReader.HasRows Then
                                                                        While DataReader.Read()
                                                                            Category = ""
                                                                            SubCategory = ""
                                                                            If Not IsDBNull(DataReader(0)) Then
                                                                                Category = (DataReader(0))
                                                                            Else
                                                                                Category = ""
                                                                            End If
                                                                            If Not IsDBNull(DataReader(1)) Then
                                                                                SubCategory = (DataReader(1))
                                                                            Else
                                                                                SubCategory = ""
                                                                            End If
                                                                            vals.add(New With {.Category = Category, .SubCategory = SubCategory})
                                                                        End While
                                                                    End If
                                                                Case "Unit of Measure"
                                                                    vals = New List(Of String)
                                                                    DataReader = RunSPArray("selUoM", Context.QueryString.Get("WSID"), {{"Nothing"}})
                                                                    If DataReader.HasRows Then
                                                                        While DataReader.Read()
                                                                            For x As Integer = 0 To DataReader.FieldCount - 1
                                                                                If Not IsDBNull(DataReader(x)) Then
                                                                                    vals.Add(DataReader(x))
                                                                                Else
                                                                                    vals.Add("")
                                                                                End If
                                                                            Next
                                                                        End While
                                                                    End If
                                                                Case "Scan Type"
                                                                    vals = New List(Of String)
                                                                    DataReader = RunSPArray("selScanCodeTypes", Context.QueryString.Get("WSID"), {{"Nothing"}})
                                                                    If DataReader.HasRows Then
                                                                        While DataReader.Read()
                                                                            For x As Integer = 0 To DataReader.FieldCount - 1
                                                                                If Not IsDBNull(DataReader(x)) Then
                                                                                    vals.Add(DataReader(x))
                                                                                Else
                                                                                    vals.Add("")
                                                                                End If
                                                                            Next
                                                                        End While
                                                                    End If
                                                            End Select
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.ToString())
                                                            insertErrorMessages("Inventory Master", "getEditView", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try


                                                        Return vals
                                                    End Function)
        End Function
        ''' <summary>
        ''' Deletes sender(1) (column value) from sender(0) (column name).
        ''' </summary>
        ''' <param name="sender">(column name), (column value) to delete the value from.</param>
        ''' <returns>Task to prevent timeouts.</returns>
        ''' <remarks>Example use: deletePopUp(["Category", "Reel Tracking"]) will delete Reel Tracking from Categories.</remarks>
        Public Function deletePopUp(sender As List(Of String)) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 Select Case sender(0)
                                                     Case "Category"
                                                         RunActionSP("delInvCategories", Context.QueryString.Get("WSID"), {{"@Cat", sender(1), strVar}, {"@SubCat", sender(2), strVar}})
                                                     Case "Velocity Code"
                                                         RunActionSP("delGoldZone", Context.QueryString.Get("WSID"), {{"@Gold", sender(1), strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                     Case "Cell Size"
                                                         RunActionSP("delCellSize", Context.QueryString.Get("WSID"), {{"@Size", sender(1), strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                     Case "Unit of Measure"
                                                         RunActionSP("delUoM", Context.QueryString.Get("WSID"), {{"@UniMeas", sender(1), strVar}})
                                                     Case "Scan Type"
                                                         RunActionSP("delScanCodeTypes", Context.QueryString.Get("WSID"), {{"@Type", sender(1), strVar}})
                                                 End Select
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.ToString())
                                                 insertErrorMessages("Inventory Master", "deletePopUp", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function
        ''' <summary>
        ''' Adds or edits a field in the specified "view"/table.
        ''' </summary>
        ''' <param name="info">info(0) = column/view in table, info(1) = "New" or value to update to, info(2) = optional, value to add when info(1) is "New"</param>
        ''' <returns>True if save success, else false.</returns>
        ''' <remarks></remarks>
        Public Function updatePopUp(info As List(Of String)) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing

                                                         Try
                                                             Select Case info(0)
                                                                 Case "Category"
                                                                     If (info(1) = "New") Then
                                                                         DataReader = RunSPArray("insInvCategories", Context.QueryString.Get("WSID"), {{"@Cat", info(2), strVar}, {"@SubCat", info(3), strVar}})
                                                                     Else
                                                                         DataReader = RunSPArray("updInvCategories", Context.QueryString.Get("WSID"), {{"@OldCat", info(1), strVar}, {"@NewCat", info(2), strVar}, {"@OldSubCat", info(3), strVar}, {"@NewSubCat", info(4), strVar}})
                                                                     End If
                                                                     If DataReader.HasRows And info(1) <> "New" Then
                                                                         If DataReader.Read() Then
                                                                             If DataReader(0) = 0 Then
                                                                                 DataReader.Dispose()
                                                                                 Return False
                                                                             Else
                                                                                 DataReader.Dispose()
                                                                                 Return True
                                                                             End If
                                                                         End If
                                                                     Else
                                                                         Return True
                                                                     End If
                                                                 Case "Unit of Measure"
                                                                     If (info(1) = "New") Then
                                                                         DataReader = RunSPArray("insScanCodeTypes", Context.QueryString.Get("WSID"), {{"@UniMeas", info(2), strVar}})
                                                                     Else
                                                                         DataReader = RunSPArray("updUnitOfMeasure", Context.QueryString.Get("WSID"), {{"@OldUniMeas", info(1), strVar}, {"@NewUniMeas", info(2), strVar}})
                                                                     End If
                                                                     If DataReader.HasRows Then
                                                                         Return False
                                                                     Else
                                                                         Return True
                                                                     End If
                                                                 Case "Scan Type"
                                                                     If (info(1) = "New") Then
                                                                         DataReader = RunSPArray("insScanCodeTypes", Context.QueryString.Get("WSID"), {{"@Type", info(2), strVar}})
                                                                     Else
                                                                         DataReader = RunSPArray("updScanCodeTypes", Context.QueryString.Get("WSID"), {{"@OldType", info(1), strVar}, {"@NewType", info(2), strVar}})
                                                                     End If
                                                                     If DataReader.HasRows Then
                                                                         Return False
                                                                     Else
                                                                         Return True
                                                                     End If
                                                                 Case "Velocity Code"
                                                                     If (info(1) = "New") Then
                                                                         DataReader = RunSPArray("insGoldenZone", Context.QueryString.Get("WSID"), {{"@Gold", info(2), strVar}})
                                                                     Else
                                                                         DataReader = RunSPArray("updGoldZone", Context.QueryString.Get("WSID"), {{"@Gold", info(2), strVar}, {"@OldGold", info(1), strVar}})
                                                                     End If
                                                                     If DataReader.HasRows Then
                                                                         Return False
                                                                     Else
                                                                         Return True
                                                                     End If
                                                                 Case "Cell Size"
                                                                     If (info(1) = "New") Then
                                                                         DataReader = RunSPArray("insCelllSize", Context.QueryString.Get("WSID"), {{"@Size", info(2), strVar}, {"@Description", info(3), strVar}})
                                                                     Else
                                                                         DataReader = RunSPArray("updCellSize", Context.QueryString.Get("WSID"), {{"@Size", info(2), strVar}, {"@OldSize", info(1), strVar}, {"@NewDesc", info(3), strVar}})
                                                                     End If
                                                                     If DataReader.HasRows Then
                                                                         Return False
                                                                     Else
                                                                         Return True
                                                                     End If
                                                             End Select
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "updatePopUp", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try

                                                         Return False
                                                     End Function)
        End Function
        ''' <summary>
        ''' Selects the kit data for an item number
        ''' </summary>
        ''' <param name="item">The desired item number</param>
        ''' <returns>New With {.ItemNum = list of "Item Number", .Desc = list of "Item Description", .KitQuan = list of Int("Kit Quantity")}</returns>
        ''' <remarks></remarks>
        Public Function refreshKits(item As String) As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function()
                                                        Dim DataReader As SqlDataReader = Nothing
                                                        Dim kitItem As New List(Of String)
                                                        Dim kitDesc As New List(Of String)
                                                        Dim kitQty As New List(Of String)
                                                        Dim kitSpecFeats As New List(Of String)

                                                        Try
                                                            DataReader = RunSPArray("selKitInventory", Context.QueryString.Get("WSID"), {{"@ItemNum", item, strVar}})

                                                            If DataReader.HasRows Then
                                                                While DataReader.Read()
                                                                    ' Item Number
                                                                    kitItem.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                                                                    ' Description
                                                                    kitDesc.Add(IIf(IsDBNull(DataReader(1)), "", DataReader(1)))
                                                                    ' Quantity
                                                                    kitQty.Add(IIf(IsDBNull(DataReader(2)), "", DataReader(2)))
                                                                    ' Special  Features
                                                                    kitQty.Add(IIf(IsDBNull(DataReader(3)), "", DataReader(3)))
                                                                End While
                                                            End If
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.ToString())
                                                            insertErrorMessages("Inventory Master", "refreshKits", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try

                                                        Return New With {.ItemNum = kitItem, .Desc = kitDesc, .KitQuan = kitQty}
                                                    End Function)
        End Function

        ''' <summary>
        ''' Adds a new kit item to a kit.
        ''' </summary>
        ''' <param name="kit">Kit to have items added to. (Stock Code)</param>
        ''' <param name="kitItem">Item to be saved to the kit.</param>
        ''' <param name="kitQty">Quantity of item to be saved to the kit.</param>
        ''' <returns>False if the save was unsuccessful, else True.</returns>
        ''' <remarks></remarks>
        Public Function addKit(kit As String, kitItem As String, kitQty As Integer, SpecFeats As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Dim save As Boolean = True

                                                         Try
                                                             DataReader = RunSPArray("insKitInventory", Context.QueryString.Get("WSID"), {{"@ItemNum", kit, strVar},
                                                                                                       {"@KitNum", kitItem, strVar},
                                                                                                       {"@Quant", kitQty, intVar},
                                                                                                       {"@SpecFeats", SpecFeats, strVar}})
                                                             If DataReader.HasRows Then
                                                                 save = False
                                                             End If
                                                         Catch ex As Exception
                                                             save = False
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "addKit", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return save
                                                     End Function)
        End Function

        ''' <summary>
        ''' Selects description of a specified item.
        ''' </summary>
        ''' <param name="item">Item to select the description from.</param>
        ''' <returns>Description of the item specified.</returns>
        ''' <remarks></remarks>
        Public Function selDescription(item As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim DataReader As SqlDataReader = Nothing, description As String = "ERROR: ITEM NOT FOUND."

                                                        Try
                                                            DataReader = RunSPArray("selInvDescriptionItemNum", Context.QueryString.Get("WSID"), {{"@item", item, strVar}})
                                                            If DataReader.HasRows Then
                                                                If DataReader.Read() Then
                                                                    If Not IsDBNull(DataReader(0)) Then
                                                                        description = DataReader(0)
                                                                    Else
                                                                        description = ""
                                                                    End If
                                                                End If
                                                            End If

                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.ToString())
                                                            insertErrorMessages("Inventory Master", "selDescription", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsDBNull(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try
                                                        Return description
                                                    End Function)
        End Function

        ''' <summary>
        ''' Selects the scan code data for an item number
        ''' </summary>
        ''' <param name="item">The desired item number</param>
        ''' <returns>New With {.ScanCode = list of ScanCode, .ScanType = list of ScanType, .ScanRange = list of ScanRange, .StartPos = list of StartPos, .CodeLen = list of Codelen}</returns>
        ''' <remarks></remarks>
        Public Function refreshScanCode(item As String) As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function()
                                                        Dim DataReader As SqlDataReader = Nothing, ScanCode As New List(Of String), ScanType As New List(Of String), ScanRange As New List(Of String)
                                                        Dim StartPos As New List(Of String), Codelen As New List(Of String)
                                                        Try
                                                            DataReader = RunSPArray("selScanCodes", Context.QueryString.Get("WSID"), {{"@ItemNum", item, strVar}})

                                                            If DataReader.HasRows Then
                                                                While DataReader.Read()
                                                                    ' Scan Code
                                                                    ScanCode.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                                                                    ' Scan Type
                                                                    ScanType.Add(IIf(IsDBNull(DataReader(1)), "", DataReader(1)))
                                                                    ' Scan Range
                                                                    ScanRange.Add(IIf(IsDBNull(DataReader(2)), "", DataReader(2)))
                                                                    ' Start Position
                                                                    StartPos.Add(IIf(IsDBNull(DataReader(3)), "", DataReader(3)))
                                                                    ' Code Length
                                                                    Codelen.Add(IIf(IsDBNull(DataReader(4)), "", DataReader(4)))
                                                                End While
                                                            End If
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.ToString())
                                                            insertErrorMessages("Inventory Master", "refreshScanCode", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try

                                                        Return New With {.ScanCode = ScanCode, .ScanType = ScanType, .ScanRange = ScanRange, .StartPos = StartPos, .CodeLen = Codelen}
                                                    End Function)
        End Function

        ''' <summary>
        ''' Adds a new scan code to the scan code list.
        ''' </summary>
        ''' <param name="ItemNum">Item to have scan code added to. (Stock Code)</param>
        ''' <param name="ScanCode">Scan Code to be saved.</param>
        ''' <param name="ScanType">Scan Type to be saved.</param>
        ''' <param name="ScanRange">Scan range to be saved.</param>
        ''' <param name="StartPos">Start Position to be saved.</param>
        ''' <param name="CodeLen">Code length to be saved.</param>
        ''' <returns>False if the save was unsuccessful, else True.</returns>
        ''' <remarks></remarks>
        Public Function addScanCode(ItemNum As String, ScanCode As String, ScanType As String, ScanRange As String, StartPos As Integer, CodeLen As Integer) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Try
                                                             RunActionSP("insScanCodes", Context.QueryString.Get("WSID"), {{"@ItemNum", ItemNum, strVar}, _
                                                                                                    {"@ScanCode", ScanCode, strVar}, _
                                                                                                    {"@ScanType", ScanType, strVar}, _
                                                                                                    {"@ScanRange", ScanRange, strVar}, _
                                                                                                    {"@StartPos", StartPos, intVar}, _
                                                                                                    {"@CodeLen", CodeLen, intVar}})

                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "addScanCode", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function
        ''' <summary>
        ''' Deletes KitNum (item within the kit) from ItemNum (the kit)
        ''' </summary>
        ''' <param name="ItemNum">Item Number of the Kit.</param>
        ''' <param name="KitNum">Item Number the Kit contains.</param>
        ''' <returns>True if success, false if failure</returns>
        ''' <remarks></remarks>
        Public Function deleteKit(ItemNum As String, KitNum As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Try
                                                             RunActionSP("delKitInv", Context.QueryString.Get("WSID"), {{"@ItemNum", ItemNum, strVar}, _
                                                                                             {"@KitNum", KitNum, strVar}, _
                                                                                             {"@XferBy", Context.User.Identity.Name, strVar}, _
                                                                                            {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "deleteKit", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function
        ''' <summary>
        ''' Deletes scan code from an item number.
        ''' </summary>
        ''' <param name="ItemNum">Item Number to delete scan code from.</param>
        ''' <param name="ScanCode">Scan code to be deleted.</param>
        ''' <returns>True if successful, false if fialure</returns>
        ''' <remarks></remarks>
        Public Function deleteScanCode(ItemNum As String, ScanCode As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Try
                                                             RunActionSP("delScanCodes", Context.QueryString.Get("WSID"), {{"@ItemNum", ItemNum, strVar}, _
                                                                                              {"@ScanCode", ScanCode, strVar}, _
                                                                                              {"@XferBy", Context.User.Identity.Name, strVar},
                                                                                              {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "deleteScanCode", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function
        ''' <summary>
        ''' Updates a Kit.
        ''' </summary>
        ''' <param name="Kit">Kit to update.</param>
        ''' <param name="OldKitItem">Previous value of the item number within the Kit in case it changed.</param>
        ''' <param name="NewKitItem">New value of the item number within the kit.</param>
        ''' <param name="quant">New quantity of the item within the kit.</param>
        ''' <returns>Returns true if successful. Returns false if error occurred.</returns>
        ''' <remarks></remarks>
        Public Function updateKit(Kit As String, OldKitItem As String, NewKitItem As String, quant As Integer, SpecFeats As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Try
                                                             RunActionSP("updKitInventory", Context.QueryString.Get("WSID"), {{"@ItemNum", Kit, strVar},
                                                                                           {"@OldKitNum", OldKitItem, strVar},
                                                                                           {"@NewKitNum", NewKitItem, strVar},
                                                                                           {"@Quant", quant, intVar},
                                                                                           {"@SpecFeats", SpecFeats, strVar}})

                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "updateKit", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function
        ''' <summary>
        ''' Updates the desired scan code. The parameter check to mak sure the right scan code is updated. 
        ''' </summary>
        ''' <param name="ItemNum">Item Number which is identified by the scan code.</param>
        ''' <param name="OldScan">The old scan code.</param>
        ''' <param name="NewScan">The new scan code.</param>
        ''' <param name="Type">The new scan type.</param>
        ''' <param name="OldRange">The old scan range</param>
        ''' <param name="Range">The new scan range.</param>
        ''' <param name="OldStart">The old start position</param>
        ''' <param name="Start">The new start position.</param>
        ''' <param name="OldCodeLen">The old code length</param>
        ''' <param name="CodeLen">the new code length.</param>
        ''' <returns>True if save was successful, else false.</returns>
        ''' <remarks></remarks>
        Public Function updateScanCode(ItemNum As String, OldScan As String, NewScan As String, Type As String, OldRange As String, Range As String, _
                                       OldStart As Integer, Start As Integer, OldCodeLen As Integer, CodeLen As Integer) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Try
                                                             RunActionSP("updScanCodes", Context.QueryString.Get("WSID"), {{"@ItemNum", ItemNum, strVar}, {"@OldScanCode", OldScan, strVar}, {"@NewScanCode", NewScan, strVar}, _
                                                                                                {"@ScanType", Type, strVar}, {"@OldScanRange", OldRange, strVar}, _
                                                                                               {"@ScanRange", Range, strVar}, {"@OldStartPos", OldStart, intVar}, {"@StartPos", Start, intVar}, _
                                                                                               {"@OldCodeLen", OldCodeLen, strVar}, {"@CodeLength", CodeLen, intVar}})

                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "updateKit", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function
        ''' <summary>
        ''' Updates all fields of a given item number. 
        ''' </summary>
        ''' <param name="ItemNum">The item number that is to be updated</param>
        ''' <param name="SuppID">The new supplier item id</param>
        ''' <param name="Descript">The new description</param>
        ''' <param name="Category">The new category</param>
        ''' <param name="SubCategory">The new sub-category</param>
        ''' <param name="UnitMeas">The new unit of measure</param>
        ''' <param name="RepPoint">The new replenishment point</param>
        ''' <param name="RepLevel">The new replenishment lvel</param>
        ''' <param name="ReorPoint">The new reorder point</param>
        ''' <param name="ReorQuant">The new reorder quantity</param>
        ''' <param name="DateSens">The new date sensitive value</param>
        ''' <param name="WareSens">The new warehouse sensitive value</param>
        ''' <param name="FIFO">The new FIFO value</param>
        ''' <param name="PrimeZone">The new primary pick zone</param>
        ''' <param name="SecondZone">The new secondary pick zone</param>
        ''' <param name="PickFenceQty">The new pick fence quantity</param>
        ''' <param name="SplitCase">The new split case value</param>
        ''' <param name="CaseQuant">The new case quantity</param>
        ''' <param name="PickSeq">The new pick sequence</param>
        ''' <param name="Active">The new Active value</param>
        ''' <param name="CellSize">The new carousel cell size</param>
        ''' <param name="GoldZone">The new carousel velocity code (gold zone)</param>
        ''' <param name="MinQuant">The new carousel minimum quantity</param>
        ''' <param name="MaxQuant">The new carousel maximum quantity</param>
        ''' <param name="BulkCell">Thew new bulk cell size</param>
        ''' <param name="BulkGold">The new bulk velcoity code (golden zone)</param>
        ''' <param name="BulkMin">The new bulk minimum quantity</param>
        ''' <param name="BulkMax">The new bulk maximum quantity</param>
        ''' <param name="CfCell">The new carton flow cell size</param>
        ''' <param name="CfGold">The new carton flow velocity code (gold zone)</param>
        ''' <param name="CfMin">The new carton flow min quantity</param>
        ''' <param name="CfMax">The new carton flow max quantity</param>
        ''' <param name="UnitCost">The new unit cost</param>
        ''' <param name="SuppNum">The new supplier number</param>
        ''' <param name="Manufact">The new manufacturer</param>
        ''' <param name="SpecFeats">The new special feature</param>
        ''' <param name="UseScal">The new use scale value</param>
        ''' <param name="AvgWeight">the new average piece weight</param>
        ''' <param name="SampleQuant">The new sample quantity</param>
        ''' <param name="UseScaleQuant">The new min use scale quantity</param>
        ''' <param name="currentData">2D list of [html id, value]</param>
        ''' <returns>A boolean if the update was successful, or not. True=sucess, false= not success</returns>
        ''' <remarks></remarks>
        Public Function updateInvMastAll(ItemNum As String, SuppID As String, Descript As String, Category As String, SubCategory As String, UnitMeas As String, RepPoint As Integer, _
                                         RepLevel As Integer, ReorPoint As Integer, ReorQuant As Integer, DateSens As Boolean, WareSens As Boolean, FIFO As Boolean, FIFODate As String, PrimeZone As String, _
                                         SecondZone As String, PickFenceQty As Integer, SplitCase As Boolean, CaseQuant As Integer, PickSeq As Integer, Active As Boolean, CellSize As String, _
                                         GoldZone As String, MinQuant As Integer, MaxQuant As Integer, BulkCell As String, BulkGold As String, BulkMin As Integer, BulkMax As Integer, _
                                         CfCell As String, CfGold As String, CfMin As Integer, CfMax As Integer, UnitCost As Decimal, SuppNum As String, Manufact As String, _
                                         SpecFeats As String, UseScal As Boolean, AvgWeight As Decimal, SampleQuant As Integer, UseScaleQuant As Integer, currentData As List(Of List(Of String)), _
                                         KanBanPoint As Integer, KanBanLevel As Integer) As Task(Of Boolean)

            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim DSens As Integer = CastAsSqlBool(DateSens)
                                                         Dim WSens As Integer = CastAsSqlBool(WareSens)
                                                         Dim FIFOval As Integer = CastAsSqlBool(FIFO)
                                                         Dim Split As Integer = CastAsSqlBool(SplitCase)
                                                         Dim Act As Integer = CastAsSqlBool(Active)
                                                         Dim Scal As Integer = CastAsSqlBool(UseScal) 'The White Mamba!
                                                         PrimeZone = IIf(IsNothing(PrimeZone), "", PrimeZone)
                                                         SecondZone = IIf(IsNothing(SecondZone), "", SecondZone)
                                                         FIFODate = IIf(IsNothing(FIFODate), "", FIFODate)


                                                         Try
                                                             Dim data As List(Of Object) = InventoryMaster.selInventoryMasterData(ItemNum, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Dim top As InvMastTopObj = data(0), setup As InvMastItemSetupObj = data(1), other As InvMastOtherObj = data(4)
                                                             Dim weigh As InvMastWeighScaleObj = data(6)

                                                             For Each entry In currentData
                                                                 Select Case entry(0)
                                                                     Case "itemDescription"
                                                                         If entry(1).ToLower <> top.Description.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "supplierID"
                                                                         If entry(1).ToLower <> top.SupplyItemID.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "UoM"
                                                                         If entry(1).ToLower <> top.UnitMeas.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "reorderPoint"
                                                                         If CInt(entry(1).ToLower) <> top.ReorPoint Then
                                                                             Return False
                                                                         End If
                                                                     Case "replenishPoint"
                                                                         If CInt(entry(1).ToLower) <> top.RepPoint Then
                                                                             Return False
                                                                         End If
                                                                     Case "category"
                                                                         If entry(1).ToLower <> top.Category.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "reorderQty"
                                                                         If CInt(entry(1).ToLower) <> top.ReorQuant Then
                                                                             Return False
                                                                         End If
                                                                     Case "replenishLevel"
                                                                         If CInt(entry(1).ToLower) <> top.RepLevel Then
                                                                             Return False
                                                                         End If
                                                                     Case "subCategory"
                                                                         If entry(1).ToLower <> top.SubCategory.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "dateSensitive"
                                                                         If CBool(entry(1).ToLower) <> setup.DateSense Then
                                                                             Return False
                                                                         End If
                                                                     Case "warehouseSensitive"
                                                                         If CBool(entry(1).ToLower) <> setup.WareSense Then
                                                                             Return False
                                                                         End If
                                                                     Case "pZone"
                                                                         If entry(1).ToLower <> setup.PrimeZone.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "sZone"
                                                                         If entry(1).ToLower <> setup.SecZone.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "FIFO"
                                                                         If CBool(entry(1).ToLower) <> setup.FIFO Then
                                                                             Return False
                                                                         End If
                                                                     Case "pFenceQty"
                                                                         If CInt(entry(1).ToLower) <> setup.PickFenceQty Then
                                                                             Return False
                                                                         End If
                                                                     Case "splitCase"
                                                                         If CBool(entry(1).ToLower) <> setup.SplitCase Then
                                                                             Return False
                                                                         End If
                                                                     Case "caseQty"
                                                                         If CInt(entry(1).ToLower) <> setup.CaseQuant Then
                                                                             Return False
                                                                         End If
                                                                     Case "pSequence"
                                                                         If CInt(entry(1).ToLower) <> setup.PickSeq Then
                                                                             Return False
                                                                         End If
                                                                     Case "activeCheckbox"
                                                                         If CBool(entry(1).ToLower) <> setup.Active Then
                                                                             Return False
                                                                         End If
                                                                     Case "carCell"
                                                                         If entry(1).ToLower <> setup.CarCell.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "bulkCell"
                                                                         If entry(1).ToLower <> setup.BulkCell.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "cartonCell"
                                                                         If entry(1).ToLower <> setup.CfCell.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "carVel"
                                                                         If entry(1).ToLower <> setup.CarVel.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "bulkVel"
                                                                         If entry(1).ToLower <> setup.BulkVel.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "cartonVel"
                                                                         If entry(1).ToLower <> setup.CfVel.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "carMinQty"
                                                                         If CInt(entry(1).ToLower) <> setup.CarMin Then
                                                                             Return False
                                                                         End If
                                                                     Case "bulkMinQty"
                                                                         If CInt(entry(1).ToLower) <> setup.BulkMin Then
                                                                             Return False
                                                                         End If
                                                                     Case "cartonMinQty"
                                                                         If CInt(entry(1).ToLower) <> setup.CfMin Then
                                                                             Return False
                                                                         End If
                                                                     Case "carMaxQty"
                                                                         If CInt(entry(1).ToLower) <> setup.CarMax Then
                                                                             Return False
                                                                         End If
                                                                     Case "bulkMaxQty"
                                                                         If CInt(entry(1).ToLower) <> setup.BulkMax Then
                                                                             Return False
                                                                         End If
                                                                     Case "cartonMaxQty"
                                                                         If CInt(entry(1).ToLower) <> setup.CfMax Then
                                                                             Return False
                                                                         End If
                                                                     Case "useScale"
                                                                         If CBool(entry(1).ToLower) <> weigh.UseScale Then
                                                                             Return False
                                                                         End If
                                                                     Case "avgWeight"
                                                                         If CDec(entry(1).ToLower) <> weigh.AvgPieceWeight Then
                                                                             Return False
                                                                         End If
                                                                     Case "sampleQty"
                                                                         If CInt(entry(1).ToLower) <> weigh.SampleQuan Then
                                                                             Return False
                                                                         End If
                                                                     Case "minScaleQty"
                                                                         If CInt(entry(1).ToLower) <> weigh.MinUseScale Then
                                                                             Return False
                                                                         End If
                                                                     Case "uCost"
                                                                         If CDec(entry(1).ToLower) <> other.UnitCost Then
                                                                             Return False
                                                                         End If
                                                                     Case "supID"
                                                                         If entry(1).ToLower <> other.SupplierID.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "manuID"
                                                                         If entry(1).ToLower <> other.ManufactID.ToLower Then
                                                                             Return False
                                                                         End If
                                                                     Case "specialFeatures"
                                                                         If entry(1).ToLower <> other.SpecFeats.ToLower Then
                                                                             Return False
                                                                         End If
                                                                 End Select
                                                             Next

                                                             RunActionSP("updInventory", Context.QueryString.Get("WSID"), {{"@ItemNum", ItemNum, strVar}, {"@SuppID", SuppID, strVar}, {"@Desc", Descript, strVar}, {"@Cat", Category, strVar},
                                                                                                                           {"@SubCat", SubCategory, strVar}, {"@UniMeas", UnitMeas, strVar}, {"@RepPoint", RepPoint, intVar},
                                                                                                                           {"@RepLevel", RepLevel, intVar}, {"@ReorPoint", ReorPoint, intVar}, {"@ReorQuant", ReorQuant, intVar},
                                                                                                                           {"@KanBanPoint", KanBanPoint, intVar}, {"@KanBanLevel", KanBanLevel, intVar},
                                                                                                                           {"@DateSens", DSens, intVar}, {"@WareSens", WSens, intVar}, {"@FIFO", FIFOval, intVar}, {"@FIFODate", FIFODate, strVar},
                                                                                                                           {"@PrimePick", PrimeZone, strVar}, {"@SecPick", SecondZone, strVar}, {"@PickFenQty", PickFenceQty, intVar},
                                                                                                                           {"@SplitCase", Split, intVar}, {"@CaseQuant", CaseQuant, intVar}, {"@PickSeq", PickSeq, intVar},
                                                                                                                           {"@Active", Act, intVar}, {"@CellSize", CellSize, strVar}, {"@GoldZone", GoldZone, strVar},
                                                                                                                           {"@MinQuant", MinQuant, intVar}, {"@MaxQuant", MaxQuant, intVar}, {"@BulkCellSize", BulkCell, strVar},
                                                                                                                           {"@BulkGoldZone", BulkGold, strVar}, {"@BulkMinQuant", BulkMin, intVar}, {"@BulkMaxQuant", BulkMax, intVar},
                                                                                                                           {"@CfCellSize", CfCell, strVar}, {"@CfGoldZone", CfGold, strVar}, {"@CfMinQuant", CfMin, intVar},
                                                                                                                           {"@CfMaxQuant", CfMax, intVar}, {"@UniCost", UnitCost, decVar}, {"@SuppNum", SuppNum, strVar},
                                                                                                                           {"@Manufact", Manufact, strVar}, {"@SpecFeats", SpecFeats, strVar}, {"@UseScale", Scal, intVar},
                                                                                                                           {"@AvgWeight", AvgWeight, decVar}, {"@SampQuant", SampleQuant, intVar}, {"@MinUseScalQuant", UseScaleQuant, intVar},
                                                                                                                           {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "updateInvMastAll", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try

                                                         Return True
                                                     End Function)

        End Function
        ''' <summary>
        '''Updates the Minimum RTS and include in auto check box for a desired item number
        ''' </summary>
        ''' <param name="ItemNum">The item number whose values are being updated</param>
        ''' <param name="MinRTS">The new MinRTS quantity</param>
        ''' <param name="IncAuto">The new vlaue of he chekcbox</param>
        ''' <returns>A boolean if the update was successful, or not. True=sucess, false= not success</returns>
        ''' <remarks></remarks>
        Public Function updateReelQuant(ItemNum As String, MinRTS As Integer, IncAuto As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Try
                                                             RunActionSP("updInventoryReelTrack", Context.QueryString.Get("WSID"), _
                                                                         {{"@ItemNum", ItemNum, strVar}, {"@MinRTS", MinRTS, intVar}, {"@IncAutoRTS", CastAsSqlBool(IncAuto), intVar}})


                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "updateReelQuant", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try

                                                         Return True
                                                     End Function)

        End Function
        ''' <summary>
        ''' Updates RTS Reel Tracking Quantity for all records with a subcategory of Reel Tracking, and the include in auto rts update field is true. 
        ''' The amount is calculated based upon the unit cost of the item. 
        ''' </summary>
        ''' <param name="RTSAmount">The minimum dollar amount to RTS</param>
        ''' <param name="RTSQuant">The threshhold Maximum Quantity</param>
        ''' <returns>A boolean if the update was successful, or not. True=sucess, false= not success</returns>
        ''' <remarks></remarks>
        Public Function updateReelAll(RTSAmount As Decimal, RTSQuant As Integer) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Try
                                                             RunActionSP("updInventoryReelTrackAll", Context.QueryString.Get("WSID"), {{"@RTSAmount", RTSAmount, decVar}, {"@RTSQuant", RTSQuant, intVar}})

                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "updateReelAll", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try

                                                         Return True
                                                     End Function)

        End Function

        ''' <summary>
        ''' Reselects RTS Min Qty.  Intended to be used after RTS Update All.
        ''' </summary>
        ''' <param name="item">Item to get RTS Min Qty for.</param>
        ''' <returns>RTS Min Qty for Item.</returns>
        ''' <remarks></remarks>
        Public Function refreshRTS(item As String) As Task(Of Integer)
            Return Task(Of Integer).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing, result As Integer = -1

                                                         Try
                                                             DataReader = RunSPArray("selInvMinRTSReelQuant", Context.QueryString.Get("WSID"), {{"@ItemNumber", item, strVar}})

                                                             If DataReader.HasRows Then
                                                                 If DataReader.Read() Then
                                                                     result = IIf(IsDBNull(DataReader(0)), -1, CInt(DataReader(0)))
                                                                 End If
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "refreshRTS", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return result
                                                     End Function)
        End Function

        ''' <summary>
        ''' Ensures the kit to add an item to is not contained by that item.
        ''' </summary>
        ''' <param name="kit">Kit to add item to.</param>
        ''' <param name="item">Item to add to kit.</param>
        ''' <returns>True if there is no conflict.</returns>
        ''' <remarks>1-104 is kit of 1-104N, 1-104N cannot be a kit of item 1-104.  This returns false if the addition or edit would cause this infinite relationship.</remarks>
        Public Function validateKit(kit As String, item As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Try
                                                             DataReader = RunSPArray("updateKitValidate", Context.QueryString.Get("WSID"), {{"@Kit", kit, strVar}, {"@ItemNumber", item, strVar}})

                                                             If Not DataReader.HasRows Then
                                                                 Return True
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Master", "validateKit", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try

                                                         Return False
                                                     End Function)
        End Function
        ''' <summary>
        ''' Updates the the desired ol item nunmber to the nwe value
        ''' </summary>
        ''' <param name="OldItemNum"></param>
        ''' <param name="NewItemNum"></param>
        ''' <returns>Changed if successful, Exists if the new item number already exists in the inventory table, and error if it errored out</returns>
        ''' <remarks></remarks>
        Public Function updItemNumber(OldItemNum As String, NewItemNum As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim result As String = ""
                                                        Dim DataReader As SqlDataReader = Nothing

                                                        Try
                                                            DataReader = RunSPArray("updInventoryItemNum", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                                                                             {"@User", Context.User.Identity.Name, strVar}, _
                                                                                                                                             {"@NewItemNum", NewItemNum, strVar}, _
                                                                                                                                             {"@OldItemNum", OldItemNum, strVar}})
                                                            If DataReader.HasRows Then
                                                                DataReader.Read()
                                                                result = DataReader(0)
                                                            End If
                                                        Catch ex As Exception
                                                            result = "Error"
                                                            Debug.WriteLine(ex.ToString())
                                                            insertErrorMessages("Inventory Master", "updItemNumber", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try
                                                        Return result
                                                    End Function)
        End Function
        ''' <summary>
        ''' Gets the orevious item number (for the arrow navigation)
        ''' </summary>
        ''' <param name="ItemNum">The current item number</param>
        ''' <param name="filter">Any filters that are applied</param>
        ''' <param name="firstItem">If it's the first item number</param>
        ''' <returns>a string telling what the previous item number is</returns>
        Public Function previousItemNum(ItemNum As String, filter As String, firstItem As Integer) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Return InventoryMaster.selectNextItemNum(ItemNum, filter, firstItem, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Function)
        End Function
        ''' <summary>
        ''' Gets the next item number (for the arrow navigation)
        ''' </summary>
        ''' <param name="ItemNum">The current item number</param>
        ''' <param name="filter">The filter that is currently apllied</param>
        ''' <returns>A string telling what the next item number is</returns>
        Public Function nextItemNum(ItemNum As String, filter As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Return InventoryMaster.selectNextItemNum(ItemNum, filter, 2, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Function)
        End Function
        ''' <summary>
        ''' Gets the count information for the arrows
        ''' </summary>
        ''' <param name="ItemNum">The current item number</param>
        ''' <param name="filter">The filter that is currently applied</param>
        ''' <returns>A object that contains the count information for the arrow functionality</returns>
        Public Function getItemNumCount(ItemNum As String, filter As String) As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function() As Object
                                                        Return InventoryMaster.selectItemNumFilterCount(ItemNum, filter, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Function)
        End Function
    End Class
End Namespace
