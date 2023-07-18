<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="zonelabel_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="zonelabel_label" aria-hidden="true">
    <div class="modal-dialog" style="width:1000px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="zonelabel_label">Select Zones</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-offset-6 col-md-2">
                        <button type="button" class="btn btn-primary btn-block" id="zonelabel_selectnonstaging">Select Non-Staging</button>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary btn-block" id="zonelabel_selectstaging">Select Staging</button>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary btn-block" id="zonelabel_selectall">Select All</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>Zone</label>
                    </div>
                    <div class="col-md-3">
                        <label>Location Description</label>
                    </div>
                    <div class="col-md-3">
                        <label>Location Type</label>
                    </div>
                    <div class="col-md-2">
                        <label>Staging Zone</label>
                    </div>
                    <div class="col-md-2">
                        <label>Selected</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="zone_container">
                        
                    </div>
                </div>                
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="zonelabel_dismiss">Cancel</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="zonelabel_submit">Submit</button>
            </div>
        </div>
    </div>
</div>
<link href="~/Content/toggles.css" rel="stylesheet" />
<link href="~/Content/toggles-modern.css" rel="stylesheet" />
<script src="~/Scripts/toggles.min.js"></script>
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Setup/ZoneLabelModal.js"></script>
