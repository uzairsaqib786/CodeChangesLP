<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="invmap_scheduler_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="invmap_scheduler_label" aria-hidden="true">
    <div class="modal-dialog" style="width:99%;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="modal-title" id="invmap_scheduler_label">Inventory Map Export Scheduler</h4>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Last Export Date/Time</label>
                        <input disabled="disabled" class="form-control" id="LastExport" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-1">
                        <label>Mon</label>
                    </div>
                    <div class="col-md-1">
                        <label>Tue</label>
                    </div>
                    <div class="col-md-1">
                        <label>Wed</label>
                    </div>
                    <div class="col-md-1">
                        <label>Thu</label>
                    </div>
                    <div class="col-md-1">
                        <label>Fri</label>
                    </div>
                    <div class="col-md-1">
                        <label>Sat</label>
                    </div>
                    <div class="col-md-1">
                        <label>Sun</label>
                    </div>
                    <div class="col-md-1">
                        <label>Export Hour</label>
                    </div>
                    <div class="col-md-1">
                        <label>Export Minute</label>
                    </div>
                    <div class="col-md-1">
                        <label>AM/PM</label>
                    </div>
                    <div class="col-md-1 col-md-offset-1">
                        <button class="btn btn-primary" id="AddNewExport"><span class="glyphicon glyphicon-plus"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="ScheduleContainer">

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ImportExport/Scripts/InvMapExportScheduler.js"></script>
