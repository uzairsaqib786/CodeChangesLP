<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="ToteIDUpdateModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="ToteIDUpdateModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="ToteIDUpDateLabel">Packing Container ID Entry</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Order Number: </label><input type="text" class="form-control" id="UpdateOrderNumber" disabled="disabled" />
                    </div>
                    <div class="col-md-6">
                        <label>Tote ID to Update: </label><input type="text" class="form-control" id="UpdateToteID" disabled="disabled" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Shipping Container ID: </label><input type="text" class="form-control" maxlength="50" id="ContainerID" />
                    </div>
                    <div class="col-md-offset-3 col-md-3" style="padding-top:25px;">
                        <button type="button" disabled="disabled" class="btn btn-primary" data-dismiss="modal" id="ToteIDUpdateButt">Update ToteID</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="ToteIDUpdateDismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/ShippingTransactions/ToteIDUpdateModal.js"></script>
