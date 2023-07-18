<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="manual_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="manual_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="manual_label">Temporary Manual Order Number - Add New</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div id="addNewMT_alerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Order Number</label>
                        <input type="text" class="form-control" id="input-manual" placeholder="Temporary Manual Order Number" maxlength="50" />
                    </div>
                    <div class="col-md-6">
                        <label>Transaction Type</label>
                        <select class="form-control" id="transType-manual">
                            <option>Pick</option>
                            <option>Put Away</option>
                            <option>Count</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Item Number</label>
                        <input type="text" class="form-control" id="item-manual" placeholder="Item Number" maxlength="50" />
                    </div>
                    <div class="col-md-6">
                        <label>Set Location by Item Quantity</label>
                        <select id="setLocByQty" class="form-control">
                            <option value="0" selected="selected">No Location</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="manual_dismiss">Close</button>
                <button type="button" class="btn btn-primary" id="manual_submit">Save New Transaction</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/ManualTransactions/AddNewModal.js"></script>