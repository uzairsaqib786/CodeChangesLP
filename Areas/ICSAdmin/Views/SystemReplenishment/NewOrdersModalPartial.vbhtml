<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="trans_qty_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="trans_qty_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="trans_qty_label">Transaction Quantity - Edit</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="alert alert-warning alert-custom" role="alert">Transaction Quantity must be greater than 0 and less than or equal to Available Quantity and Replenishment Quantity.</div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Available Quantity</label>
                                <input type="text" class="form-control" id="availableQtyModal" disabled="disabled" />
                            </div>
                            <div class="col-md-6">
                                <label>Replenishment Quantity</label>
                                <input type="text" class="form-control"  id="replenishmentQtyModal" disabled="disabled" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 col-md-offset-3">
                                <label>Transaction Quantity</label>
                                <input type="text" class="form-control" placeholder="Transaction Qty" id="transQtyModal" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="trans_qty_dismiss">Close</button>
                <button type="button" class="btn btn-primary" id="trans_qty_submit">Submit</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ICSAdmin/Scripts/SystemReplenishment/NewReplenishmentTransQtyModal.js"></script>

<div class="modal fade" id="replen_status_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="replen_status_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="replen_status_label">Replenishment(s) Progress</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>Status: <span id="ReplenishmentStatus"></span></h3>
                        <div class="row">
                            <div class="col-md-offset-3 col-md-6">
                                <input type="text" class="form-control modal-launch-style input-lg" disabled="disabled" id="ReplenishmentsRemaining" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button class="btn btn-danger" id="StopReplenishments">Stop the Replenishment Process</button>
            </div>
        </div>
    </div>
</div>