<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.InventoryMapModel

<div class="modal fade" id="invmap_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="invmap_label" aria-hidden="true">
    <div class="modal-dialog" style="width:1100px;">
        <div class="modal-content">
            <div class="modal-body">
                <div class="legend-buttons">
                    <div class="row">
                        <div class="col-xs-8 legend-buttons-header">
                            Location Details
                        </div>
                        <div class="col-xs-4">
                            @If (Model.userRights.Contains("Inv Map Delete") And Model.AccessLevel = "Administrator") And Model.App = "Admin" Then
                                @<button id="deleteItemModal" data-toggle="tooltip" data-placement="top" title="Delete" type="button" class="btn pull-right btn-danger modal-hide" style="margin-left:5px;margin-right:5px;">
                                    <span class="glyphicon glyphicon-trash"></span>
                                </button>
                            End If
                            <button id="printSelectedModal" type="button" data-toggle="tooltip" data-placement="top" title="Print Label(s)" class="btn pull-right btn-primary modal-hide" style="margin-left:5px;margin-right:5px;">
                                <span class="glyphicon glyphicon-print"></span>
                            </button>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Location Number</label>
                        <input type="text" class="form-control" disabled="disabled" id="LocNum" />
                    </div>
                    <div class="col-md-3">
                        <label>Location Name</label>
                        <div class="form-group has-feedback has-warning forced-typeahead" style="margin-bottom:0px;">
                            <input type="text" class="form-control" id="LocName" placeholder="Location" maxlength="50" />
                            <span class="glyphicon glyphicon-warning-sign form-control-feedback" style="top:0px;"></span>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>Laser X</label>
                        <input type="text" class="form-control" id="LaserX" placeholder="Laser X" maxlength="9" />
                    </div>
                    <div class="col-md-3">
                        <label>Laser Y</label>
                        <input type="text" class="form-control" id="LaserY" placeholder="Laser Y" maxlength="9" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                            <label class="control-label">Warehouse</label>
                            <input type="text" readonly="readonly" class="form-control warehouse-modal modal-launch-style"  id="Warehouse">
                            <i class="glyphicon glyphicon-resize-full form-control-feedback warehouse-modal modal-launch-style"></i>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Zone</label>
                        <div class="form-group has-feedback has-warning forced-typeahead" style="margin-bottom:0px;">
                            <input type="text" class="form-control" id="Zone" placeholder="Zone" maxlength="2" />
                            <span class="glyphicon glyphicon-warning-sign form-control-feedback" style="top:0px;"></span>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <label>Carousel</label>
                        <input type="text" class="form-control" id="Carousel" placeholder="Carousel" maxlength="1" />
                    </div>
                    <div class="col-md-2">
                        <label>Row</label>
                        <input type="text" class="form-control" id="Row" placeholder="Row" maxlength="5" />
                    </div>
                    <div class="col-md-2">
                        <label>Shelf</label>
                        <input type="text" class="form-control" id="Shelf" placeholder="Shelf" maxlength="2" />
                    </div>
                    <div class="col-md-2">
                        <label>Bin</label>
                        <input type="text" class="form-control" id="Bin" placeholder="Bin" maxlength="3" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>@Model.Aliases.UoM</label>
                        <input type="text" disabled="disabled" class="form-control" id="UoM">
                    </div>
                    <div class="col-md-3">
                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                            <label class="control-label">Cell Size</label>
                            <input type="text" readonly="readonly" class="form-control cell-modal modal-launch-style inv-edit"  id="CellSz">
                            <i class="glyphicon glyphicon-resize-full form-control-feedback cell-modal modal-launch-style"></i>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                            <label class="control-label">Velocity Code</label>
                            <input type="text" readonly="readonly" class="form-control velocity-modal modal-launch-style inv-edit" id="Velocity">
                            <i class="glyphicon glyphicon-resize-full form-control-feedback velocity-modal modal-launch-style"></i>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>Alternate Light</label>
                        <input type="text" class="form-control" id="AltLight" maxlength="9" placeholder="Alternate Light" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>@Model.Aliases.UserFields(0)</label>
                        <input type="text" class="form-control" id="UF1" placeholder="@Model.Aliases.UserFields(0)" maxlength="255" />
                    </div>
                    <div class="col-md-6">
                        <label>@Model.Aliases.UserFields(1)</label>
                        <input type="text" class="form-control" id="UF2" placeholder="@Model.Aliases.UserFields(1)" maxlength="255" />
                    </div>
                </div>
                <div class="row modal-hide">
                    <div class="col-md-3">
                        <label>Quantity Allocated Pick</label>
                        <input type="text" disabled="disabled" class="form-control" id="QtyPick" />
                    </div>
                    <div class="col-md-3">
                        <label>Quantity Allocated Put Away</label>
                        <input type="text" disabled="disabled" class="form-control" id="QtyPut" />
                    </div>
                    <div class="col-md-3">
                        <label>Inv Map ID</label>
                        <input type="text" class="form-control" disabled="disabled" id="InvMapID" />
                    </div>
                    <div class="col-md-3">
                        <label>Master Inv Map ID</label>
                        <input type="text" class="form-control" disabled="disabled" id="MInvMapID" />
                    </div>
                </div>
                <div class="row" style="padding-top:10px;padding-bottom:20px;">
                    <div class="col-md-3 modal-hide">
                        <label>Master Location</label>
                        <div class="toggles toggle-modern pull-right" data-toggle-ontext="Yes" data-toggle-offtext="No" id="MasterLoc"></div>
                    </div>
                    <div class="col-md-3">
                        <label>Dedicated</label>
                        <div class="toggles toggle-modern pull-right" data-toggle-ontext="Yes" data-toggle-offtext="No" id="Dedicated"></div>
                    </div>
                    <div class="col-md-3">
                        <label>Date Sensitive</label>
                        <div class="toggles toggle-modern pull-right" data-toggle-ontext="Yes" data-toggle-offtext="No" id="DateSensitive"></div>
                    </div>
                </div>
                <legend>Assigned Item Details</legend>
                <div class="row">
                    <div class="col-md-4">
                        <label>@Model.Aliases.ItemNumber</label>
                        <input type="text" class="form-control" id="ItemNumber" maxlength="50" />
                    </div>
                    <div class="col-md-8">
                        <label>Description</label>
                        <textarea class="form-control no-horizontal" disabled="disabled" rows="1" id="Description" maxlength="255"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 modal-hide">
                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                            <label class="control-label">Item Quantity</label>
                            <input type="text" readonly="readonly" class="form-control itemqty-modal modal-launch-style"  id="ItemQty">
                            <i class="glyphicon glyphicon-resize-full form-control-feedback itemqty-modal modal-launch-style"></i>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>Maximum (Location) Quantity</label>
                        <input type="text" class="form-control" id="MaxQty" maxlength="9" placeholder="Maximum Quantity" />
                    </div>
                    <div class="col-md-3">
                        <label>Min (Location) Quantity</label>
                        <input type="text" class="form-control" id="MinQty" maxlength="9" placeholder="Min Quantity" />
                    </div>
                    <div class="col-md-3">
                        <label>Put Away Date</label>
                        <input type="text" class="form-control" id="PutDate" placeholder="Put Away Date" disabled="disabled" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Serial Number</label>
                        <input type="text" class="form-control" id="SerNum" disabled="disabled" value="0" />
                    </div>
                    <div class="col-md-3">
                        <label>Lot Number</label>
                        <input type="text" class="form-control" id="LotNum" disabled="disabled" value="0" />
                    </div>
                    <div class="col-md-3">
                        <label>Revision</label>
                        <input type="text" class="form-control" id="Revision" disabled="disabled" />
                    </div>
                    <div class="col-md-3">
                        <label>Expiration Date</label>
                        <input type="text" class="form-control" id="ExpDate"  disabled="disabled" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="invmap_dismiss">Close</button>
                @If (Model.userRights.Contains("Inv Map Delete") And Model.AccessLevel = "Administrator") And Model.App = "Admin" Then
                    @<button id="clearItem" type="button" class="btn btn-primary">
                        Clear
                    </button>
                End If
                @If (Model.userRights.Contains("Inv Map Dupe Location") And Model.AccessLevel = "Administrator") And Model.App = "Admin" Then
                    @<button id="duplicateItem" type="button" class="btn btn-primary modal-hide">
                        Duplicate
                    </button>
                End If
                @If Model.App = "Admin" Then
                    @<button id="quarItem" type="button" class="btn btn-primary modal-hide">Quarantine</button>
                    @<button type="button" class="btn btn-success" id="invmap_submit">Save Changes</button>
                End If
            </div>
        </div>
    </div>
</div>
<script>
    $(document).ready(function () {
        var curApp = '@Model.App';
        if (curApp == 'OM') {
            $('#invmap_modal').find('input').attr('disabled', 'disabled');
            $('#ItemQty').removeAttr('disabled');
        };
    });
</script>