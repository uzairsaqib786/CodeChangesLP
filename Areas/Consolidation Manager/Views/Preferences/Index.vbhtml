<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.CMPrefrenceModel
@Code
    ViewData("Title") = "Preferences"
    ViewData("PageName") = "&nbsp; | &nbsp; Preferences"
End Code

<div class="container-fluid">
    <div role="tabpanel">
        <ul class="nav nav-tabs" role="tablist">
            <li class="active" role="presentation">
                <a href="#consolidationTab" aria-controls="consolidationTab" role="tab" data-toggle="tab" id="prefConsolidate">
                    Consolidation
                </a>
            </li>
            <li role="presentation">
                <a href="#shippingTab" aria-controls="shippingTab" role="tab" data-toggle="tab" id="prefShipping">
                    Shipping
                </a>
            </li>
        </ul>
    </div>
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="consolidationTab">
            <div class="row" style="padding-top:10px;">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Preferences
                            </h3>
                        </div>
                        <div class="panel-body" id="PrefPanel">
                            <div class="row">
                                <div class="col-md-4">
                                    <textarea style="resize:none" rows="4" cols="50" class="form-control" disabled> Select a default Packing List format for printing from the Consolidation window. Press the Reports button to design the desired packing list reports</textarea>
                                </div>
                                <div class="col-md-4 ">
                                    <label>Packing List Default</label>
                                    <select class="form-control" id="DefPackList">
                                        @If Model.DefaultPackingList = "Packing List" Then
                                            @<option value="Packing List" selected>Packing List</option>
                                            @<option value="Packing List 2">Packing List 2</option>
                                        Else
                                            @<option value="Packing List">Packing List</option>
                                            @<option value="Packing List 2" selected>Packing List 2</option>
                                        End If
                                    </select>
                                </div>
                                <div class="col-md-4">
                                    <label>Packing List Sort</label>
                                    <select class="form-control" id="PackListSort">
                                        @If Model.PackingList = "[Item Number], [Line Number]" Then
                                            @<option value="[Item Number], [Line Number]" selected>[Item Number],[Line Number]</option>
                                            @<option value="[Line Number], [Item Number]">[Line Number],[Item Number]</option>
                                            @<option value="[Tote ID], [Item Number], [Line Number]">[Tote ID],[Item Number],[Line Number]</option>
                                            @<option value="[Tote ID], [Line Number], [Item Number]">[Tote ID],[Line Number],[Item Number]</option>
                                        ElseIf Model.PackingList = "[Line Number], [Item Number]" Then
                                            @<option value="[Item Number], [Line Number]">[Item Number],[Line Number]</option>
                                            @<option value="[Line Number], [Item Number]" selected>[Line Number],[Item Number]</option>
                                            @<option value="[Tote ID], [Item Number], [Line Number]">[Tote ID],[Item Number],[Line Number]</option>
                                            @<option value="[Tote ID], [Line Number], [Item Number]">[Tote ID],[Line Number],[Item Number]</option>
                                        ElseIf Model.PackingList = "[Tote ID], [Item Number], [Line Number]" Then
                                            @<option value="[Item Number], [Line Number]">[Item Number],[Line Number]</option>
                                            @<option value="[Line Number], [Item Number]">[Line Number],[Item Number]</option>
                                            @<option value="[Tote ID], [Item Number], [Line Number]" selected>[Tote ID],[Item Number],[Line Number]</option>
                                            @<option value="[Tote ID], [Line Number], [Item Number]">[Tote ID],[Line Number],[Item Number]</option>
                                        Else
                                            @<option value="[Item Number], [Line Number]">[Item Number],[Line Number]</option>
                                            @<option value="[Line Number], [Item Number]">[Line Number],[Item Number]</option>
                                            @<option value="[Tote ID], [Item Number], [Line Number]">[Tote ID],[Item Number],[Line Number]</option>
                                            @<option value="[Tote ID], [Line Number], [Item Number]" selected>[Tote ID],[Line Number],[Item Number]</option>
                                        End If
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:30px">
                                    <label>Blind Verify</label>
                                    <select class="form-control" id="BlindVerify">
                                        @If Model.BlindVerifyItems = "Yes" Then
                                            @<option value="Yes" selected>Yes</option>
                                            @<option value="No">No</option>
                                        Else
                                            @<option value="Yes">Yes</option>
                                            @<option value="No" selected>No</option>
                                        End If
                                    </select>
                                </div>
                                <div class="col-md-4" style="padding-top :30px;">
                                    <label>Verify Each Item</label>
                                    <select class="form-control" id="VerifyItems">
                                        @If Model.VerifyItems = "Yes" Then
                                            @<option value="Yes" selected>Yes</option>
                                            @<option value="No">No</option>
                                        Else
                                            @<option value="Yes">Yes</option>
                                            @<option value="No" selected>No</option>
                                        End If
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:20px;">
                                    <label>Allow Print/View a list <br />of un-verified Line Items</label>
                                    <select class="form-control" id="PrintUnVerified">
                                        @If Model.PrintUnVerified = "Yes" Then
                                            @<option value="Yes" selected>Yes</option>
                                            @<option value="No">No</option>
                                        Else
                                            @<option value="Yes">Yes</option>
                                            @<option value="No" selected>No</option>
                                        End If
                                    </select>
                                </div>
                                <div class="col-md-4" style="padding-top:18px;">
                                    <label>
                                        Allow Print/View the packing List when<br />
                                        un-verfified Line Items exist?
                                    </label>
                                    <select class="form-control" id="PrintVerified">
                                        @If Model.PrintVerified = "Yes" Then
                                            @<option value="Yes" selected>Yes</option>
                                            @<option value="No">No</option>
                                        Else
                                            @<option value="Yes">Yes</option>
                                            @<option value="No" selected>No</option>
                                        End If
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:20px;">
                                    <label>Default Lookup Type</label>
                                    <select class="form-control" id="DefLookType">
                                        @code
                                            Dim lookupTypeList = New List(Of String) From {"Supplier Item ID", "Lot Number", "User Field 1", "Item Number", "Serial Number", "Any Code", "Tote ID"}
                                            For Each Type In lookupTypeList
                                                If Model.DefaultLookupType = Type Then
                                            @<option value="@Type" selected>@Type</option>
                                                Else
                                            @<option value="@Type">@Type</option>
                                                End If
                                            Next
                                        End Code
                                        
                                    </select>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <div class="col-md-3">
                                    @If Model.EmailPackingSlip Then
                                        @<b>Email Packing Slip: </b>@<input type="checkbox" checked="checked" id="EmailPackSlip" />
                                    Else
                                        @<b>Email Packing Slip: </b>@<input type="checkbox" id="EmailPackSlip" />
                                    End If
                                </div>
                                <div class="col-md-5">
                                    @If Model.AutoCompShipComplete Then
                                        @<b>Auto Complete Backorders at "Ship Complete": </b>@<input type="checkbox" checked="checked" id="backOrders" />
                                    Else
                                        @<b>Auto Complete Backorders at "Ship Complete": </b>@<input type="checkbox" id="backOrders" />
                                    End If
                                </div>
                               
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-3">
                                    @If Model.NonPickproOrders Then
                                        @<b>Stage Non Pickpro Orders: </b>@<input type="checkbox" checked="checked" id="nonPickpro" />
                                    Else
                                        @<b>Stage Non Pickpro Orders: </b>@<input type="checkbox" id="nonPickpro" />
                                    End If
                                </div>
                                <div class="col-md-3">
                                    @If Model.ValidateStagingLocations Then
                                        @<b>Validate Staging Locations?: </b>@<input type="checkbox" checked="checked" id="validateStagingLocs" />
                                    Else
                                        @<b>Validate Staging Locations?: </b>@<input type="checkbox" id="validateStagingLocs" />
                                    End If
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div role="tabpanel" class="tab-pane" id="shippingTab">
            <div class="row" style="padding-top:10px;">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Choose the desired Consolidation options for your business.</h3>
                        </div>
                        <div class="panel-body" id="ShipPrefPanel">
                            <div class="row">
                                <div class="col-md-4">
                                    @If Model.Shipping Then
                                        @<b>Allow Shipping: </b>@<input type="checkbox" name="shipping" checked="checked" id="allowShip" />
                                    Else
                                        @<b>Allow Shipping: </b>@<input type="checkbox" name="shipping" id="allowShip" />
                                    End If
                                </div>
                                <div class="col-md-4 pull-right">
                                    <button type="button" class="btn btn-primary form-control" id="Carriers">Carriers</button>
                                </div>
                            </div>
                            <div class="row" style="padding-top:15px;">
                                <div class="col-md-4">
                                    @If Model.Packing Then
                                        @<b>Allow Packing: </b>@<input type="checkbox" name="packing" checked="checked" id="allowPack" />
                                    Else
                                        @<b>Allow Packing: </b>@<input type="checkbox" name="packing" id="allowPack" />
                                    End If
                                </div>
                            </div>
                            <div class="row" id="afterPackingCheck">
                                <div class="row" id="confirmPackChecked">
                                    <div class="col-md-4" style="padding-top:15px; padding-left:30px;">
                                        @If Model.ConfirmPacking Then
                                            @<b>Confirm and Packing: </b>@<input type="checkbox" name="confirm" checked="checked" id="confirmPack" />
                                        Else
                                            @<b>Confirm and Packing: </b>@<input type="checkbox" name="confirm" id="confirmPack" />
                                        End If
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="restOf">
                                <div class="row">
                                    <div class="col-md-4" style="padding-top:15px; padding-left:30px;">
                                        @If Model.AutoPrintContPL Then
                                            @<b>Auto Print Container PL: </b>@<input type="checkbox" name="printCont" checked="checked" id="printCont" />
                                        Else
                                            @<b>Auto Print Container PL: </b>@<input type="checkbox" name="printCont" id="printCont" />
                                        End If
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4" style="padding-top:15px; padding-left:30px;">
                                        @If Model.AutoPrintOrderPL Then
                                            @<b>Auto Print Order PL: </b>@<input type="checkbox" name="printOrd" checked="checked" id="printOrd" />
                                        Else
                                            @<b>Auto Print Order PL: </b>@<input type="checkbox" name="printOrd" id="printOrd" />
                                        End If
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4" style="padding-top:15px; padding-left:30px;">
                                        @If Model.AutoPrintContLabel Then
                                            @<b>Auto Print Container Label: </b>@<input type="checkbox" checked="checked" name="printContLabel" id="printContLabel" />
                                        Else
                                            @<b>Auto Print Container Label: </b>@<input type="checkbox" name="printContLabel" id="printContLabel" />
                                        End If
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4" style="padding-top:15px; padding-left:30px;">
                                        @If Model.EnterContainerID Then
                                            @<b>Enter Container ID: </b>@<input type="checkbox" checked="checked" name="contID" id="contID" />
                                        Else
                                            @<b>Enter Container ID: </b>@<input type="checkbox" name="contID" id="contID" />
                                        End If
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4" style="padding-top:15px; padding-left:30px;">
                                        @If Model.ConfirmPackingQuant Then
                                            @<b>Confirm Quantity: </b>@<input type="checkbox" name="confirmQTY" checked="checked" id="confirmQTY" />
                                        Else
                                            @<b>Confirm Quantity: </b>@<input type="checkbox" name="confirmQTY" id="confirmQTY" />
                                        End If
                                    </div>
                                </div>
                                <div class="row" style="padding-top:10px;">
                                    <div class="col-md-4" style="padding-left:30px;">
                                        <label>Default Text for Container ID</label>
                                        <input maxlength="50" class="form-control" type="text" name="contIDText" id="contIDText" style="width:300px;" value="@Model.ContainerIDDefault" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Select from the field list those that you will be collecting data for in Shipping.</h3>
                        </div>
                        <div class="panel-body" id="OtherShipPrefTab">
                            <div class="row">
                                <div class="col-md-4" style="padding-top:15px;">
                                    @If Model.Freight Then
                                        @<b>Freight: </b>@<input type="checkbox" checked="checked" id="freight" />
                                    Else
                                        @<b>Freight: </b>@<input type="checkbox" id="freight" />
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:15px;">
                                    @If Model.Freight1 Then
                                        @<b>Freight1: </b>@<input type="checkbox" checked="checked" id="freight1" />
                                    Else
                                        @<b>Freight1: </b>@<input type="checkbox" id="freight1" />
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:15px;">
                                    @If Model.Freight2 Then
                                        @<b>Freight2: </b>@<input type="checkbox" checked="checked" id="freight2" />
                                    Else
                                        @<b>Freight2: </b>@<input type="checkbox" id="freight2" />
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:15px;">
                                    @If Model.Weight Then
                                        @<b>Weight: </b>@<input type="checkbox" checked="checked" id="weight" />
                                    Else
                                        @<b>Weight: </b>@<input type="checkbox" id="weight" />
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:15px;">
                                    @If Model.Length Then
                                        @<b>Length: </b>@<input type="checkbox" checked="checked" id="length" />
                                    Else
                                        @<b>Length: </b>@<input type="checkbox" id="length" />
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:15px;">
                                    @If Model.Width Then
                                        @<b>Width: </b>@<input type="checkbox" checked="checked" id="width" />
                                    Else
                                        @<b>Width: </b>@<input type="checkbox" id="width" />
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:15px;">
                                    @If Model.Height Then
                                        @<b>Height: </b>@<input type="checkbox" checked="checked" id="height" />
                                    Else
                                        @<b>Height: </b>@<input type="checkbox" id="height" />
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top:15px;">
                                    @If Model.Cube Then
                                        @<b>Cube: </b>@<input type="checkbox" checked="checked" id="cube" />
                                    Else
                                        @<b>Cube: </b>@<input type="checkbox" id="cube" />
                                    End If
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/Consolidation Manager/Views/Preferences/CarriersModalPartials.vbhtml")
<script src="~/Areas/Consolidation Manager/Scripts/Preferences/Preferences.js"></script>


