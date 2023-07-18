<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="CompPickBatchShortModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="CompPickBatchShortModal" aria-hidden="true">
    <div class="modal-dialog" style="width:75%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">
                    Short Transaction
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Order Number:</label>
                        <input type="text" class="form-control" id="ShortOrdNum" readonly="readonly"/>
                    </div>
                    <div class="col-md-4">
                        <label>Tote ID:</label>
                        <input type="text" class="form-control" id="ShortToteID" readonly="readonly" />
                    </div>
                    <div class="col-md-4">
                        <label>Item Number:</label>
                        <input type="text" class="form-control" id="ShortItemNum" readonly="readonly" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-4">
                        <label>Location:</label>
                        <input type="text" class="form-control" id="ShortLoc" readonly="readonly" />
                    </div>
                    <div class="col-md-4">
                        <label>Trans Qty:</label>
                        <input type="text" class="form-control" id="ShortTransQty" readonly="readonly" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-4">
                        <label>Tote Qty:</label>
                        <input type="number" min="0" class="form-control" data-toggle="tooltip" title="Current Quantity in Tote" id="ShortQty" />
                    </div>
                    <div class="col-md-4" style="padding-top:1.75%">
                        <button type="button" class="btn btn-success btn-block" id="ExecShortTran">Short Transaction</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <input type="text" hidden id="ShortOTID" />
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="ShortModalDismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/CompPickBatch/ShortTransPartial.js"></script>