<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="SBReqDateModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="SBReqDateModal" aria-hidden="true">
    <div class="modal-dialog" style="width:1200px;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="modal-title" id="SBReqDateModal_label">Super Batch Required Date Status</h4>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-2">
                        <button type="button" class="btn btn-block btn-primary" id="SBReDateRefresh">Refresh Data</button>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-12">
                        <div style="overflow-y:scroll;max-height:600px;">
                            <table id="PickToteOrdersTable" style="background-color:white;" class="table table-bordered table-condensed table-striped">
                                <thead>
                                    <tr>
                                        <td>Required Date</td>
                                        <td>Zone</td>
                                        <td>SB Transactions to Induct</td>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="SBReqDateModalDismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/SuperBatch/SuperBatchRequiredDateModal.js"></script>