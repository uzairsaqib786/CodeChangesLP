<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<!-- HTML FOR UNIT OF MEASURE MODAL -->
<!-- USE:
    <div class="form-group has-feedback" style="margin-bottom:0px;">
        <label class="control-label">Unit of Measure</label>
        <input type="text" readonly="readonly" class="form-control uom-modal modal-launch-style" placeholder="Unit of Measure" id="UnitOfMeasure" />
        <i class="glyphicon glyphicon-resize-full form-control-feedback uom-modal modal-launch-style"></i>
    </div>
-->
<div class="modal fade" id="uom_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="uom_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="uom_label">Units of Measure - Add, Edit, Delete, and Set.</h4>
            </div>
            <div class="modal-body">
                <input type="text" hidden="hidden" id="uom_sender">
                <div class="row">
                    <div class="col-md-12">
                        <div id="uom_alerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <strong>Unit of Measure</strong>
                    </div>
                    <div class="col-md-1">
                        <button id="uom_add" type="button" data-toggle="tooltip" data-placement="top" title="Add New Unit of Measure" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div id="uom_container">

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="uom_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/UnitOfMeasureModal.js"></script>