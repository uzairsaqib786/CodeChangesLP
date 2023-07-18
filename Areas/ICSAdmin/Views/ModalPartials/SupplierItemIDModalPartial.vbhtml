<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<!-- HTML FOR Supplier Item ID MODAL -->
<!-- USE:
    <div class="form-group has-feedback" style="margin-bottom:0px;">
        <label class="control-label">Supplier Item ID</label>
        <input type="text" readonly="readonly" class="form-control supplieritemid-modal modal-launch-style" placeholder="Supplier Item ID" id="SupplierItemID" />
        <i class="glyphicon glyphicon-resize-full form-control-feedback supplieritemid-modal modal-launch-style"></i>
    </div>
-->
<div class="modal fade" id="supplieritemid_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="supplieritemid_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="supplieritemid_label">Supplier Item ID</h4>
            </div>
            <div class="modal-body">
                <input type="text" hidden="hidden" id="supplieritemid_sender" />
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                        <label>Supplier Item ID</label>
                        <input type="text" class="form-control" id="input-supplieritemid" placeholder="Supplier Item ID" maxlength="50" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="supplieritemid_dismiss">Close</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="supplieritemid_submit">Set</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/SupplierItemIDModal.js"></script>

