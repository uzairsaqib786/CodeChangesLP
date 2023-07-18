<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="transfer_field_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="transfer_field_label" aria-hidden="true">
    <div class="modal-dialog" style="width:90%;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="modal-title" id="transfer_field_label">Transfer File Field Mapping</h4>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-2">
                        <select class="form-control" id="XferTransField">
                            <option value="">(None)</option>
                            @For Each dic In Model("Transfer Field Mapping")
                                @<option data-xfer-type="@dic("Xfer Type")" value="@dic("Transaction Type")">@dic("Transaction Type") | @dic("Xfer Type")</option>
                            Next
                        </select>
                    </div>
                    <div class="col-md-2">
                        <div class="btn-group">
                            <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown">Sort <span class="caret"></span></button>
                            <ul class="dropdown-menu" role="menu">
                                <li><a id="sortAlpha">Alphabetically</a></li>
                                <li><a id="sortPosition">By Start Position</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <label>PickPro Field</label>
                    </div>
                    <div class="col-md-1">
                        <label>Start Position</label>
                    </div>
                    <div class="col-md-1">
                        <label>Field Length</label>
                    </div>
                    <div class="col-md-1">
                        <label>End Position</label>
                    </div>
                    <div class="col-md-1 import-hide">
                        <label>Pad Char.</label>
                    </div>
                    <div class="col-md-1">
                        <label>Pad Field from Left</label>
                    </div>
                    <div class="col-md-2">
                        <label>Field Type</label>
                    </div>
                    <div class="col-md-2">
                        <label id="IEFormatLabel">Export Format</label>
                    </div>
                    <div class="col-md-1">
                        <button class="btn btn-primary pull-right" id="AddBlank" data-toggle="tooltip" data-placement="top" data-original-title="Add Blank"><span class="glyphicon glyphicon-plus"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="XferContainer">
                        
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="transfer_field_dismiss">Close</button>
                        <button type="button" class="btn btn-primary" id="UpdateAllMaps">Update All Other Maps</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ImportExport/Scripts/TransferFileFieldMapping.js"></script>
