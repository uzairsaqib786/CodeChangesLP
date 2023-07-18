<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="cell_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="cell_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="cell_label">Cell Sizes - Add, Edit, Delete, and Set.</h4>
            </div>
            <div class="modal-body">
                <input type="text" hidden="hidden" id="cell_sender">
                <div class="row">
                    <div class="col-md-12">
                        <div id="cell_alerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <strong>Cell Size</strong>
                    </div>
                    <div class="col-md-1">
                        <button id="cell_add" type="button" data-toggle="tooltip" data-placement="top" title="Add New Cell Size" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div id="cell_container">

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="cell_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/CellSizeModal.js"></script>
