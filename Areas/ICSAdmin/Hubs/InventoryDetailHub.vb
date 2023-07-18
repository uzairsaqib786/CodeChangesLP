' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Namespace Admin
    Public Class InventoryDetailHub
        Inherits Hub

        ''' <summary>
        ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
        ''' </summary>
        ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
        ''' <remarks></remarks>
        Public Overrides Function OnConnected() As Task
            'Adds a user to their own unique group by a value passed in during connection
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
            'Calls the Original Onconnected Function pass control back to the server
            Return MyBase.OnConnected()
        End Function

        ''' <summary>
        ''' Gets the information for the selected item number
        ''' </summary>
        ''' <param name="ItemNum">The item number whose info will be displayed</param>
        ''' <returns>An object containing the info for the given item number</returns>
        ''' <remarks></remarks>
        Public Function selectItemNumberInfo(ItemNum As String) As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function() As Object
                                                        Dim itemNumDetails As New List(Of Object)
                                                        Dim desiredData As New Object
                                                        Dim top As InvMastTopObj
                                                        Dim setup As InvMastItemSetupObj
                                                        Dim other As InvMastOtherObj
                                                        Dim reel As InvMastReelTrackObj
                                                        Dim weigh As InvMastWeighScaleObj
                                                        Dim model As String = ""
                                                        Try
                                                            itemNumDetails = InventoryMaster.selInventoryMasterData(ItemNum, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            top = itemNumDetails(0)
                                                            setup = itemNumDetails(1)
                                                            other = itemNumDetails(4)
                                                            reel = itemNumDetails(5)
                                                            weigh = itemNumDetails(6)
                                                            model = itemNumDetails(8).model


                                                            desiredData = New With {.itemnum = ItemNum, .description = top.Description, .supplierid = top.SupplyItemID, .category = top.Category, _
                                                                                    .subcategory = top.SubCategory, .unitofmeasure = top.UnitMeas, .reorderpoint = top.ReorPoint, _
                                                                                    .reorderquantity = top.ReorQuant, .replenishmentpoint = top.RepPoint, .replenishmentlevel = top.RepLevel, _
                                                                                    .fifo = setup.FIFO, .datesensitive = setup.DateSense, .warehousesensitive = setup.WareSense, _
                                                                                    .primarypickzone = setup.PrimeZone, .secondarypickzone = setup.SecZone, .pickfenceqty = setup.PickFenceQty, _
                                                                                    .splitcase = setup.SplitCase, .casequantity = setup.CaseQuant, .active = setup.Active, _
                                                                                    .picksequence = setup.PickSeq, .unitcost = other.UnitCost, .suppliernumber = other.SupplierID, _
                                                                                    .manufacturer = other.ManufactID, .model = model, .specialfeatures = other.SpecFeats, _
                                                                                    .carouselcellsize = setup.CarCell, .carouselvelocity = setup.CarVel, .carouselminqty = setup.CarMin, _
                                                                                    .carouselmaxqty = setup.CarMax, .bulkcellsize = setup.BulkCell, .bulkvelocity = setup.BulkVel, _
                                                                                    .bulkminqty = setup.BulkMin, .bulkmaxqty = setup.BulkMax, .cfcellsize = setup.CfCell, _
                                                                                    .cfvelocity = setup.CfVel, .cfminqty = setup.CfMin, .cfmaxqty = setup.CfMax, _
                                                                                    .inlcudeinautortsupdate = reel.IncAutoUpd, .avgpieceweight = weigh.AvgPieceWeight, _
                                                                                    .samplequantity = weigh.SampleQuan, .usescale = weigh.UseScale, .minusescalequantity = weigh.MinUseScale, _
                                                                                    .minimumrtsreelquantity = reel.MinRTSReelQuan}
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.Message)
                                                            insertErrorMessages("InventoryDetailHub", "selectItemNumberInfo", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        End Try
                                                        Return desiredData
                                                    End Function)
        End Function
    End Class
End Namespace

