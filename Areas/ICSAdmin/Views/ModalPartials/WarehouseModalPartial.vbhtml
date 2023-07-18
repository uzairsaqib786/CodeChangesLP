<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<!-- HTML FOR warehouse MODAL -->
<!-- USE:
    <div class="form-group has-feedback" style="margin-bottom:0px;">
        <label class="control-label">warehouse </label>
        <input type="text" readonly="readonly" class="form-control warehouse-modal modal-launch-style" placeholder="warehouse " id="warehouse" />
        <i class="glyphicon glyphicon-resize-full form-control-feedback warehouse-modal modal-launch-style"></i>
    </div>
-->
<div class="modal fade" id="warehouse_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="warehouse_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="warehouse_label">Warehouses - Add, Delete, Edit and Set.</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" hidden="hidden" id="warehouse_sender" />
                <div class="row">
                    <div class="col-md-12">
                        <div id="warehouse_alerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-9">
                                <label>Warehouse</label>
                            </div>
                            <div class="col-md-offset-11 col-md-1">
                                <button type="button" class="btn btn-primary" id="warehouse_add" data-toggle="tooltip" data-placement="top" data-original-title="Add New Warehouse"><span class="glyphicon glyphicon-plus"></span></button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="warehouse_container">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="warehouse_dismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/WarehouseModal.js"></script>

