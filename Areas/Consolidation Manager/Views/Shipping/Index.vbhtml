<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Shipping"
    ViewData("PageName") = "&nbsp; | &nbsp; Shipping"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2 class="panel-title">Shipping</h2>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label>Order Number</label><input maxlength="50" type="text" id="ShippingOrderNumber" class="form-control" disabled="disabled" value="@Model.OrderNum" />
                        </div>
                        <div class="col-md-offset-1 col-md-2" style="padding-top:25px;">
                            @If Model.ShipComp Then
                                @<button id="ShippingCompShip" disabled class="btn btn-primary btn-block">Complete Shipment</button>
                            Else
                                @<button id="ShippingCompShip" class="btn btn-primary btn-block">Complete Shipment</button>
                            End If

                        </div>
                        <div class="col-md-offset-1 col-md-2" style="padding-top:25px;">
                            <button id="ShippingPrintAll" class="btn btn-primary btn-block Print-Report">Print All</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2 class="panel-title">Shipment</h2>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-1 col-md-push-11">
                            @If Model.ShipComp Then
                                @<button id="ShippingAddNew" class="btn btn-primary btn-block" disabled data-toggle="tooltip" data-placement="top" title="Add Container"><span class="glyphicon glyphicon-plus"></span></button>
                            Else
                                @<button id="ShippingAddNew" class="btn btn-primary btn-block" data-toggle="tooltip" data-placement="top" title="Add Container"><span class="glyphicon glyphicon-plus"></span></button>
                            End If

                        </div>
                        <div class="col-md-3 col-md-pull-1">
                            <label>Container ID</label>
                        </div>
                        <div class="col-md-3 col-md-pull-1">
                            <label>Carrier</label>
                        </div>
                        <div class="col-md-3 col-md-pull-1">
                            <label>Tracking Number</label>
                        </div>
                        <div class="col-md-2 col-md-pull-1">
                            <label>Print, Save, Delete</label>
                        </div>
                        <div class="col-md-1 col-md-pull-1 @(If(Model.ShipPrefs.Freight, "", "hide"))">
                            <label>Freight</label>
                        </div>
                        <div class="col-md-1 col-md-pull-1 @(If(Model.ShipPrefs.Freight1, "", "hide"))">
                            <label>Freight1</label>
                        </div>
                        <div class="col-md-1 col-md-pull-1 @(If(Model.ShipPrefs.Freight2, "", "hide"))">
                            <label>Freight2</label>
                        </div>
                        <div class="col-md-1 col-md-pull-1 @(If(Model.ShipPrefs.Weight, "", "hide"))">
                            <label>Weight</label>
                        </div>
                        <div class="col-md-1 col-md-pull-1 @(If(Model.ShipPrefs.Length, "", "hide"))">
                            <label>Length</label>
                        </div>
                        <div class="col-md-1 col-md-pull-1 @(If(Model.ShipPrefs.Width, "", "hide"))">
                            <label>Width</label>
                        </div>
                        <div class="col-md-1 col-md-pull-1 @(If(Model.ShipPrefs.Height, "", "hide"))">
                            <label>Height</label>
                        </div>
                        <div class="col-md-1 col-md-pull-1 @(If(Model.ShipPrefs.Cube, "", "hide"))">
                            <label>Cube</label>
                        </div>
                    </div>
                    @For Each item As Object In Model.ShipData
                        @<div class="row bottom-spacer-half">
                            <div class="col-md-3">
                                <input maxlength="50" type="text" id="@(item.ID & "_ContID")" class="form-control" disabled="disabled" value="@item.ContainerID" />
                            </div>
                            <div class="col-md-3">
                                @If Model.ShipComp Then
                                    @<select class="form-control" disabled id="@(item.ID & "_Carrier")">
                                        @For Each carrier As String In Model.Carriers
                                        If carrier = item.Carrier Then
                                            @<option value="@carrier" selected>@carrier</option>
                                        Else
                                            @<option value="@carrier">@carrier</option>
                                        End If
                                        Next
                                        @If item.Carrier = "" Then
                                            @<option value="" selected></option>
                                        Else
                                            @<option value=""></option>
                                        End If
                                    </select>
                                Else
                                    @<select class="form-control" id="@(item.ID & "_Carrier")">
                                        @For Each carrier As String In Model.Carriers
                                        If carrier = item.Carrier Then
                                            @<option value="@carrier" selected>@carrier</option>
                                        Else
                                            @<option value="@carrier">@carrier</option>
                                        End If
                                        Next
                                        @If item.Carrier = "" Then
                                            @<option value="" selected></option>
                                        Else
                                            @<option value=""></option>
                                        End If
                                    </select>
                                End If
                            </div>
                            <div class="col-md-3">
                                @If Model.ShipComp Then
                                    @<input maxlength="50" type="text" id="@(item.ID & "_TrackNum")" disabled class="form-control" value="@item.TrackingNum" />
                                Else
                                    @<input maxlength="50" type="text" id="@(item.ID & "_TrackNum")" class="form-control" value="@item.TrackingNum" />
                                End If
                            </div>
                            <div class="col-md-1">
                                <button class="btn btn-primary btn-block print-list Print-Report" name="@item.ID" data-toggle="tooltip" data-placement="top" title="Print List"><span class="glyphicon glyphicon-print"></span></button>
                            </div>
                            <div class="col-md-1">
                                @If Model.ShipComp Then
                                    @<button class="btn btn-primary btn-block save-item" disabled name="@item.ID" data-toggle="tooltip" data-placement="top" title="Save Changes"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                                Else
                                    @<button class="btn btn-primary btn-block save-item" name="@item.ID" data-toggle="tooltip" data-placement="top" title="Save Changes"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                                End If
                            </div>
                            <div class="col-md-1">
                                @If Model.ShipComp Then
                                    @<button class="btn btn-danger btn-block delete-item" disabled name="@item.ID" data-toggle="tooltip" data-placement="top" title="Delete Container"><span class="glyphicon glyphicon-remove"></span></button>
                                Else
                                    @<button class="btn btn-danger btn-block delete-item" name="@item.ID" data-toggle="tooltip" data-placement="top" title="Delete Container"><span class="glyphicon glyphicon-remove"></span></button>
                                End If
                            </div>
                        </div>
                        @<div class="row bottom-spacer">
                            <div class="col-md-1 @(If(Model.ShipPrefs.Freight, "", "hide"))">
                                @If Model.ShipComp Then
                                    @<input type="text" maxlength="50" disabled oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Freight")" class="form-control" value="@item.Freight" />
                                Else
                                    @<input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Freight")" class="form-control" value="@item.Freight" />
                                End If
                            </div>
                            <div class="col-md-1 @(If(Model.ShipPrefs.Freight1, "", "hide"))">
                                @If Model.ShipComp Then
                                    @<input type="text" maxlength="50" disabled oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Freight1")" class="form-control" value="@item.Freight1" />
                                Else
                                    @<input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Freight1")" class="form-control" value="@item.Freight1" />
                                End If
                            </div>
                            <div class="col-md-1 @(If(Model.ShipPrefs.Freight2, "", "hide"))">
                                @If Model.ShipComp Then
                                    @<input type="text" maxlength="50" disabled oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Freight2")" class="form-control" value="@item.Freight2" />
                                Else
                                    @<input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Freight2")" class="form-control" value="@item.Freight2" />
                                End If
                            </div>
                            <div class="col-md-1 @(If(Model.ShipPrefs.Weight, "", "hide"))">
                                @If Model.ShipComp Then
                                    @<input type="text" maxlength="50" disabled oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Weight")" class="form-control" value="@item.Weight" />
                                Else
                                    @<input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Weight")" class="form-control" value="@item.Weight" />
                                End If
                            </div>
                            <div class="col-md-1 @(If(Model.ShipPrefs.Length, "", "hide"))">
                                @If Model.ShipComp Then
                                    @<input type="text" maxlength="50" disabled oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Length")" class="form-control length" name="@item.ID" value="@item.Length" />
                                Else
                                    @<input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Length")" class="form-control length" name="@item.ID" value="@item.Length" />
                                End If
                            </div>
                            <div class="col-md-1 @(If(Model.ShipPrefs.Width, "", "hide"))">
                                @If Model.ShipComp Then
                                    @<input type="text" maxlength="50" disabled oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Width")" class="form-control width" name="@item.ID" value="@item.Width" />
                                Else
                                    @<input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Width")" class="form-control width" name="@item.ID" value="@item.Width" />
                                End If
                            </div>
                            <div class="col-md-1 @(If(Model.ShipPrefs.Height, "", "hide"))">
                                @If Model.ShipComp Then
                                    @<input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" disabled id="@(item.ID & "_Height")" class="form-control height" name="@item.ID" value="@item.Height" />
                                Else
                                    @<input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Height")" class="form-control height" name="@item.ID" value="@item.Height" />
                                End If
                            </div>
                            <div class="col-md-1 @(If(Model.ShipPrefs.Cube, "", "hide"))">
                                @If Model.ShipComp Then
                                    @<input type="text" disabled maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Cube")" class="form-control" value="@item.Cube" />
                                Else
                                    @<input type="text" disabled maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="@(item.ID & "_Cube")" class="form-control" value="@item.Cube" />
                                End If
                            </div>
                        </div>
                    Next
                </div>
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/Consolidation Manager/Views/Shipping/InsertShippingItemModal.vbhtml")
<script src="~/Areas/Consolidation Manager/Scripts/Shipping/Shipping.js"></script>
