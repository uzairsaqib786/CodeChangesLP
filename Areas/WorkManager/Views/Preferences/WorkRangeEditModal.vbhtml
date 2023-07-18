<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="WorkerRangeEditModal" data-backdrop="static" tabindex="1" role="dialog" aria-labelledby="=WorkerRangeEditModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="panel-title" id="WorkerRangeEditLabel">
                    Edit Worker Range
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Last Name</label>
                        <input type="text" id="EditWorkRangeLastName" class="form-control" maxlength="30" />
                    </div>
                    <div class="col-md-3">
                        <label>First Name</label>
                        <input type="text" id="EditWorkRangeFirstName" disabled="disabled" class="form-control" maxlength="30" />
                    </div>
                    <div class="col-md-3">
                        <label>Username</label>
                        <input type="text" id="EditWorkRangeUsername" disabled="disabled" class="form-control" maxlength="12" />
                    </div>
                    <div class="col-md-3">
                        <label>Active</label>
                        <select class="form-control" id="EditWorkRangeActive">
                            <option value="True" selected>Yes</option>
                            <option value="False">No</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Range Name</label>
                        <input type="text" id="EditWorkRangeRangeName" class="form-control" maxlength="50" />
                    </div>
                    <div class="col-md-4">
                        <label>Start Location</label>
                        <input type="text" id="EditWorkRangeStartLocation" disabled="disabled" class="form-control" maxlength="50" />
                    </div>
                    <div class="col-md-4">
                        <label>End Location</label>
                        <input type="text" id="EditWorkRangeEndLocation" disabled="disabled" class="form-control" maxlength="50" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 pull-right">
                        <button type="button" class="btn btn-default btn-block" data-dismiss="modal" id="tote_dismiss">Close</button>
                    </div>
                    <div class="col-md-2 pull-right">
                        <button id="EditWorkRangeSave" data-dismiss="modal" title="Save Work Range" data-toggle="tooltip" class="btn btn-block btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div> 
<script src="~/Areas/WorkManager/Scripts/Preferences/WorkRangeEditModal.js"></script>