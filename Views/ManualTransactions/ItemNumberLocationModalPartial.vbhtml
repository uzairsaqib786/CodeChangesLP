<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="manualitem_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="manualitem_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="manualitem_label">Set Item Number and Location</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div id="itemMT_alerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Item Number</label>
                        <input type="text" class="form-control" id="itemmanual-modal" placeholder="Item Number" maxlength="50" />
                    </div>
                    <div class="col-md-6">
                        <label>Set Location by Item Quantity</label>
                        <input type="text" class="form-control" id="itemLocation" maxlength="50" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="manualitem_dismiss">Close</button>
                <button type="button" class="btn btn-primary" id="manualitem_submit">Set Item and Location</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/ManualTransactions/ItemLocationModal.js"></script>
