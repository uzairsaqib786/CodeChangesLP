<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="XferFileFieldMapModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="XferFileFieldMapModal" aria-hidden="true">
    <div class="modal-dialog" style="width:90%;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="modal-title" id="XferFileFieldMapLabel">
                            Transfer File Field Mapping
                        </h4>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Table</label>
                        <select class="form-control" id="XferFileFieldMapTable">
                            <option value="Inventory">Inventory</option>
                            <option value="Inventory Map">Inventory Map</option>
                        </select>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-5">
                        <label>System Fieldname</label>
                    </div>
                    <div class="col-md-5">
                        <label>Field Name</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="XferFileFieldMapContainer">

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="XferFileFieldMap_Dismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ImportExport/Scripts/XferFileFieldMap.js"></script>