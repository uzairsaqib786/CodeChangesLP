<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="emlocations_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="emlocations_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="emlocations_label">Locations</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div id="emlocations_alerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Start Location</label>
                        <input type="text" class="form-control" id="emlocations_start" placeholder="Start Location" maxlength="50" />
                    </div>
                    <div class="col-md-6">
                        <label>End Location</label>
                        <input type="text" class="form-control" id="emlocations_end" placeholder="End Location" maxlength="50" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="emlocations_dismiss">Close</button>
                <button type="button" class="btn btn-primary" id="emlocations_submit">Submit</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ICSAdmin/Scripts/Employees/EmLocationsModal.js"></script>
