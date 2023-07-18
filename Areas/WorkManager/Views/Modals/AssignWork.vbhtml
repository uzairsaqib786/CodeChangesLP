<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="AssignWork_Modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="AssignWork_Label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="panel-title" id="AssignWork_Label">Assign Work</h4>
            </div>
            <div class="modal-body">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Worker Detail</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-4">
                                <label>Picks</label>
                                <input type="checkbox" class="form-control" id="AssignPicks" />
                            </div>
                            <div class="col-md-4">
                                <label>Put Aways</label>
                                <input type="checkbox" class="form-control" id="AssignPuts" />
                            </div>
                            <div class="col-md-4">
                                <label>Counts</label>
                                <input type="checkbox" class="form-control" id="AssignCounts" />
                            </div>
                        </div>
                        <div class="row top-spacer">
                            <div class="col-md-offset-4 col-md-4">
                                <button type="button" class="btn btn-block btn-primary" onclick="$('#AssignPicks,#AssignPuts,#AssignCounts').prop('checked', 'checked');">Select All Types</button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group has-feedback has-warning">
                                    <label>Select Worker</label>
                                    <input type="text" class="form-control" id="AssignToWorker" placeholder="Worker Name" maxlength="30" />
                                    <span class="glyphicon glyphicon-warning-sign form-control-feedback" data-toggle="tooltip" data-original-title="You must select a worker from the dropdown." data-placement="top"></span>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label style="visibility:hidden;">Clear Current Work</label>
                                <button type="button" class="btn btn-block btn-danger" id="ClearWork" disabled="disabled">Clear Current Work</button>
                            </div>
                            <div class="col-md-3">
                                <label style="visibility:hidden;">Clear Batch ID(s)</label>
                                <button type="button" class="btn btn-block btn-danger" id="ClearBatchIDs" disabled="disabled">Clear Batch ID(s)</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Create Batches</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label>Current Batches</label>
                                <select id="CurrBatchAction" class="form-control">
                                    <option value="left">Should be left alone</option>
                                    <option value="cleared">Should be cleared</option>
                                </select>
                            </div>
                            <div class="col-md-3">
                                <label style="visibility:hidden;">Batch ID and Username</label>
                                <button type="button" class="btn btn-primary btn-block" id="BatchAndUsername" disabled="disabled">Batch ID and Username</button>
                            </div>
                            <div class="col-md-3">
                                <label style="visibility:hidden;">Batch ID Only</label>
                                <button type="button" class="btn btn-primary btn-block" id="BatchOnly" disabled="disabled">Batch ID Only</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="tote_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/WorkManager/Scripts/Modals/AssignWork.js"></script> 