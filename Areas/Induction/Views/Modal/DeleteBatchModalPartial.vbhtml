<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="batch_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="batch_label" aria-hidden="true">
    <div class="modal-dialog" style="width:800px;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-6">
                        <h4 class="modal-title" id="batch_label">Batch - Delete and DeAllocate.</h4>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-danger pull-right" id="DelAll">Delete All Batches</button>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Batch ID</label>
                        <input type="text" class="form-control" id="DelBatchID" readonly="readonly" />
                    </div>
                    <div class="col-md-6">
                        <label>Tote ID</label>
                        <input type="text" class="form-control" id="DelToteID" readonly="readonly" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Transaction Type</label>
                        <input type="text" class="form-control" id="DelTransType" readonly="readonly" />
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <label class="pull-right">Clear Batch <input type="radio" name="BatchOrTote" value="B" id="clearBatch" /></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="pull-right">Clear Tote <input type="radio" name="BatchOrTote" value="T" id="clearTote" /></label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <label class="pull-right">DeAllocate & Clear <input type="radio" name="ClearType" value="D" checked="checked" /></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="pull-right">Clear Only <input type="radio" name="ClearType" value="C" /></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Currently Selected Command</label>
                        <input type="text" class="form-control" id="DelCmd" readonly="readonly" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default " id="batch_dismiss">Close</button>
                <button type="button" class="btn btn-danger" id="ExecuteDelete">Clear/DeAllocate</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/Modal/DeleteBatchModal.js"></script>