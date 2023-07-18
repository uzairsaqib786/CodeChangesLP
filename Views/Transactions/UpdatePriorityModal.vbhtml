<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="UpdatePriorityModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="PriorityModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="PriorityLabel">Change Priority</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class=" col-md-offset-4 col-md-4">
                        <label>Order Number</label>
                        <input type="text" disabled="disabled" id="OrderNumber" class="form-control" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Priority</label>
                        <input type="text" disabled="disabled" id="Priority" class="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label>Please Enter New Priority</label>
                        <input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="NewPriority" class="form-control" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="col-md-offset-8 col-md-2">
                    <button id="Close" data-dismiss="modal" class="btn btn-default">Close</button>
                </div>
                <div class="col-md-2">
                    <button id="Save" data-dismiss="modal" class="btn btn-primary">Save</button>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="/Scripts/Transaction Journal/PriorityModal.js"></script>
