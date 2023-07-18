<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="location_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="location_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="location_label">Location and Zone</h4>
            </div>
            <div class="modal-body">
                <input type="text" hidden="hidden" id="location_sender" />
                <div class="row">
                    <div class="col-md-6">
                        <label>Location</label>
                        <input type="text" class="form-control" id="input-location" placeholder="Location" maxlength="50" />
                    </div>
                    <div class="col-md-6">
                        <label>Zone</label>
                        <select id="ZoneSelect" class="form-control">

                        </select>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="location_dismiss">Close</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="location_submit">Set</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/LocationModal.js"></script>
