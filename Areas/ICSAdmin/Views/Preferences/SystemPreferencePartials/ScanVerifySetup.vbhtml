<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Scan Verification Setup
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-9">
                        <div class="alert alert-danger alert-custom" role="alert">The settings below will only affect individual locations in the Inventory Map.  Click "Manage Default Scan Verification(s)" at the right to edit default handling.</div>
                    </div>
                    <div class="col-md-3">
                        <button type="button" class="btn btn-primary" id="SVDefaults">Manage Default Scan Verification(s)</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="row">
                            <div class="col-md-2">
                                <label>Trans. Type</label>
                            </div>
                            <div class="col-md-2">
                                <label>Scan Sequence</label>
                            </div>
                            <div class="col-md-2">
                                <label>Field</label>
                            </div>
                            <div class="col-md-2">
                                <label>Verify Type</label>
                            </div>
                            <div class="col-md-2">
                                <label>Verify String Start</label>
                            </div>
                            <div class="col-md-2">
                                <label>Verify String Length</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary pull-right" data-toggle="tooltip" data-placement="top" data-original-title="Add New Scan Verification" id="addNewSV" disabled="disabled"><span class="glyphicon glyphicon-plus"></span></button>
                    </div>
                </div>
                <div style="display:none;">
                    <select id="SVNames">
                        @For Each name In Model.SVNames
                            @<option value="@name">@name</option>
                        Next
                    </select>
                </div>
                <div class="row">
                    <div class="col-md-12" id="SV_Container">

                    </div>
                </div>
                <table class="table table-condensed table-bordered table-striped" style="background-color:white;" id="ScanVerifyTable">
                    <thead>
                        <tr>
                            <th>Location</th>
                            <th>Location Name</th>
                            <th>Zone</th>
                            <th>Carousel</th>
                            <th>Row</th>
                            <th>Shelf</th>
                            <th>Bin</th>
                            <th>Warehouse</th>
                            <th>Cell Size</th>
                            <th>Velocity Code</th>
                            <th>Carousel Location</th>
                            <th>Carton Flow Location</th>
                            <th>@Model.aliases.ItemNumber</th>
                            <th>Description</th>
                            <th>Serial #</th>
                            <th>Lot #</th>
                            <th>Expiration Date</th>
                            <th>@Model.aliases.UoM</th>
                            <th>Max Qty</th>
                            <th>Qty Allocated Pick</th>
                            <th>Qty Allocated Put Away</th>
                            <th>Item Qty</th>
                            <th>Put Away Date</th>
                            <th>Date Sensitive</th>
                            <th>@Model.aliases.UserFields(0)</th>
                            <th>@Model.aliases.UserFields(1)</th>
                            <th>Dedicated</th>
                            <th>Master Location</th>
                            <th>Inv Map ID</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="scanverify_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="scanverify_label" aria-hidden="true">
    <div class="modal-dialog" style="width:1200px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="scanverify_label">Scan Verification Defaults</h4>
            </div>
            <div class="modal-body">
                <div class="alert alert-danger alert-custom" role="alert">These settings are defaults and will affect all locations that do not have a specific assignment.</div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-10">
                                <div class="row">
                                    <div class="col-md-2">
                                        <label>Trans. Type</label>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Scan Sequence</label>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Field</label>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Verify Type</label>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Verify String Start</label>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Verify String Length</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <button type="button" class="btn btn-primary pull-right" data-toggle="tooltip" data-placement="top" data-original-title="Add New Scan Verification" id="addNewModalSV"><span class="glyphicon glyphicon-plus"></span></button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="SVModal_Container">

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="scanverify_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>