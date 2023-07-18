<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<!-- HTML FOR velocity MODAL -->
<!-- USE:
    <div class="form-group has-feedback" style="margin-bottom:0px;">
        <label class="control-label">Velocity Code</label>
        <input type="text" readonly="readonly" class="form-control velocity-modal modal-launch-style" placeholder="Velocity Code" id="VelocityCode" />
        <i class="glyphicon glyphicon-resize-full form-control-feedback velocity-modal modal-launch-style"></i>
    </div>
-->
<div class="modal fade" id="velocity_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="velocity_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="velocity_label">Velocity Codes - Add, Delete, Edit and Set.</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" hidden="hidden" id="velocity_sender" />
                <div class="row">
                    <div class="col-md-12">
                        <div id="velocity_alerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-9">
                                <label>Velocity Code</label>
                            </div>
                            <div class="col-md-offset-11 col-md-1">
                                <button type="button" class="btn btn-primary" id="velocity_add"><span class="glyphicon glyphicon-plus"></span></button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="velocity_container">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="velocity_dismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/VelocityCodeModal.js"></script>
