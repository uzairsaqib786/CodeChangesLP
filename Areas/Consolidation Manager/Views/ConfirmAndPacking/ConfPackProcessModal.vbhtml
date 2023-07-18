<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="ConfPackProcessModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="ConfPackProcessModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="ConfPackProcessLabel">Confirm and Packing Process Transaction</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Container ID</label><input maxlength="50" type="text" id="ContIDProcModal" class="form-control" disabled="disabled"  />
                    </div>
                    <div class="col-md-3" style="padding-top:25px;">
                        <button id="ItemLabelModal" class="btn btn-primary btn-block">Item Label</button>
                    </div>
                    <div class="col-md-3" style="padding-top:25px;">
                        <button id="SplitLineProcModal" class="btn btn-primary btn-block">Split Line</button>
                    </div>
                    <div class="col-md-3" style="padding-top:25px;">
                        <button id="AdjustQauntModal" class="btn btn-primary btn-block">Adjust Ship Quantity</button>
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="ProcessModalTable">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Item Number</th>
                                    <th>Line Number</th>
                                    <th>Tote ID</th>
                                    <th>Order Quantity</th>
                                    <th>Completed Quantity</th>
                                    <th>Ship Quantity</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2">
                        <button type="button" class="btn btn-default btn-block" data-dismiss="modal" id="ConfPackProcessDismiss">Close</button>
                    </div>
                    <div class="col-md-offset-8 col-md-2">
                        <button id="DoneModal" data-dismiss="modal" class="btn btn-primary btn-block">Done</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/ConfirmAndPacking/ConfPackProcessModal.js"></script>