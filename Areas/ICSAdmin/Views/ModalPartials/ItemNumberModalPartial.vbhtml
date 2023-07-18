<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="item_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="item_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="item_label">Item Number</h4>
            </div>
            <div class="modal-body">
                <input type="text" hidden="hidden" id="item_sender" />
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                        <label>Item Number</label>
                        <input type="text" class="form-control" id="input-item" placeholder="Item Number" maxlength="50" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="item_dismiss">Close</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="item_submit">Set</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/ItemNumberModal.js"></script>
