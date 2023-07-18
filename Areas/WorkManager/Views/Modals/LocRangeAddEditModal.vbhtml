<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="LocRangeAddEditModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="LocRangeAddEditModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="panel-title" id="LocRangeAddEditLabel">

                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Range Name</label>
                        <input type="text" id="LocRangeName" class="form-control" maxlength="50" />
                    </div>
                    <div class="col-md-3">
                        <label>Multi Worker Range</label>
                        <select class="form-control" id="LocRangeMultiWorker">
                            <option value="True">Yes</option>
                            <option value="False" selected>No</option>
                        </select>
                    </div>
                    <div class="col-md-3">
                        <label>Active</label>
                        <select class="form-control" id="LocRangeActive">
                            <option value="True" selected>Yes</option>
                            <option value="False">No</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Start Location</label>
                        <input type="text" id="LocRangeStart" class="form-control" maxlength="50" />
                    </div>
                    <div class="col-md-6">
                        <label>End Location</label>
                        <input type="text" id="LocRangeEnd" class="form-control" maxlength="50" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 pull-right">
                        <button type="button" class="btn btn-default btn-block" data-dismiss="modal" id="totedismiss">Close</button>
                    </div>
                    <div class="col-md-2 pull-right">
                        <button id="AddEditLocRangeSave" data-dismiss="modal" title="Save Range" data-toggle="tooltip" class="btn btn-block btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/WorkManager/Scripts/Modals/LocRangeAddEditModal.js"></script> 