<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="reeloverview_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="reeloverview_label" aria-hidden="true">
    <div class="modal-dialog" style="width:99%;margin-top:10px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="reeloverview_label">Reel Transactions</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>@Model.aliases.ItemNumber</label>
                        <input type="text" class="form-control" readonly="readonly" id="ReelItem"  />
                    </div>
                    <div class="col-md-9">
                        <label>Description</label>
                        <textarea class="form-control no-horizontal" readonly="readonly" id="ReelDescription"  rows="1"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Total Parts to be Inducted</label>
                        <input type="text" class="form-control" id="TotalParts" placeholder="Total Parts" />
                    </div>
                    <div class="col-md-3">
                        <label>Number of Parts Not Assigned</label>
                        <input type="text" class="form-control" id="PartsRemaining" readonly="readonly"  />
                    </div>
                    <div class="col-md-3">
                        <label>Number of Reels to be Inducted</label>
                        <input type="text" class="form-control" id="NumReels" placeholder="Number of Reels" value="1" />
                    </div>
                    <div class="col-md-3">
                        <label style="visibility:hidden;">Auto</label>
                        <button type="button" class="btn btn-primary btn-block" id="CreateReels">Auto Generate Reels & Serial Numbers</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Reel Serial Number</label>
                    </div>
                    <div class="col-md-1 col-md-offset-2">
                        <label>Reel Part Quantity</label>
                    </div>
                </div>
                <div class="row">
                    <input type="hidden" id="HReelOrderNumber" />
                    <input type="hidden" id="HLotNumber" />
                    <input type="hidden" id="HUF1" />
                    <input type="hidden" id="HUF2" />
                    <input type="hidden" id="HWarehouse" />
                    <input type="hidden" id="HExpDate" />
                    <input type="hidden" id="HNotes" />
                    <div class="col-md-12" id="reel_container" style="overflow-y:scroll;">

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default " data-dismiss="modal" id="reeloverview_dismiss">Cancel</button>
                <button type="button" class="btn btn-primary" id="reeloverview_submit">Submit</button>
            </div>
        </div>
    </div>
</div>
@Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/Reels/ReelsOverlayPartial.vbhtml")
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Reels/Reels.js"></script>