<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="XMLFieldMapModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="XMLFieldMapModal" aria-hidden="true">
    <div class="modal-dialog" style="width:90%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Open Trans Pick XML Field Mapping</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Open Trans Column</label>
                    </div>
                    <div class="col-md-3">
                        <label>XML Node</label>
                    </div>
                    <div class="col-md-3">
                        <label>Field Type</label>
                    </div>
                    <div class="col-md-3">
                        <button id="addNewXMLNode" type="button" class="btn btn-primary">
                            <span class="glyphicon glyphicon-plus"></span>
                        </button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="XMLFieldContainer">

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="XMLFieldMapModal_Dismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ImportExport/Scripts/XMLFieldMapping.js"></script>