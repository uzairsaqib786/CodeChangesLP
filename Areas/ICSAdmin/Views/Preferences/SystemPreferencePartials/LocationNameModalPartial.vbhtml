<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="locname_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="locname_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="locname_label">Location Name - Add, Edit, Delete, Set</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-10">
                        <strong>Location Name</strong>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary pull-right" id="addNewLocationName" data-toggle="tooltip" data-placement="top" data-original-title="Add New Location Name"><span class="glyphicon glyphicon-plus"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="locname_container">

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="locname_dismiss">Close</button>
                <button type="button" class="btn btn-primary" id="locname_setnone">Clear Location Name</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/LocationZones/LocationNameModal.js"></script>