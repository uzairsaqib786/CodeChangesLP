<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="reeldetail_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="reeldetail_label" aria-hidden="true">
    <div class="modal-dialog" style="width:800px;margin-top:10px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="reeldetail_label">Reel Detail</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Order Number</label>
                        <input type="text" class="form-control input-sm" id="ReelOrder" placeholder="Order Number" maxlength="50" />
                    </div>
                    <div class="col-md-6">
                        <label>Lot Number</label>
                        <input type="text" class="form-control input-sm" id="ReelLot" placeholder="Lot Number" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>@Model.aliases.UserFields(0)</label>
                        <input type="text" class="form-control input-sm" id="ReelUF1" placeholder="@Model.aliases.UserFields(0)" maxlength="255" />
                    </div>
                    <div class="col-md-6">
                        <label>@Model.aliases.UserFields(1)</label>
                        <input type="text" class="form-control input-sm" id="ReelUF2" placeholder="@Model.aliases.UserFields(1)" maxlength="255" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group has-feedback">
                            <label>Warehouse</label>
                            <input type="text" class="form-control input-sm modal-launch-style" id="ReelWarehouse" placeholder="Warehouse" readonly="readonly" data-no-edits="true" />
                            <i class="glyphicon glyphicon-resize-full form-control-feedback reel-warehouse modal-launch-style"></i>
                        </div>
                    </div> 
                    <div class="col-md-6">
                        <label>Expiration Date</label>
                        <input type="text" class="form-control input-sm date-picker" id="ReelExpDate" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Notes</label>
                        <textarea class="form-control no-horizontal input-sm" rows="3" placeholder="Notes" id="ReelNotes"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Parts to be Inducted</label>
                        <input type="text" class="form-control input-sm" id="ReelQty" placeholder="Number of Parts to be Inducted" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default " data-dismiss="modal" id="reeldetail_dismiss">Cancel</button>
                <button type="button" class="btn btn-primary" id="reeldetail_submit">Submit</button>
            </div>
        </div>
    </div>
</div>