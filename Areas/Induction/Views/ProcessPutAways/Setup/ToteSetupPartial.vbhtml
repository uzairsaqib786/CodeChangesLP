<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-5">
        <div class="panel panel-info top-spacer">
            <div class="panel-heading clearfix">
                <h4 class="panel-title pull-left">Batch Setup</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-8">
                        <label>Batch ID</label>
                        <input type="text" maxlength="50" id="BatchIDSetup" placeholder="Batch ID" class="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label>Status</label>
                        <input type="text" class="form-control" readonly="readonly"  id="BatchStatus" value="Not Processed" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <input type="hidden" style="display:none;" value="@Model.preferences.PickBatchQuant" id="NumTotes" />
                        <button type="button" class="btn btn-primary" id="NewBatchWID">New Batch with next ID</button> 
                        <button type="button" class="btn btn-primary" id="NewBatch">New Batch</button>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-8">
                        <label>Assigned Zones</label>
                        <input type="text" id="AssignedZones"  class="form-control" readonly="readonly" />
                    </div>
                    <div class="col-md-4">
                        <label>Default Cells</label>
                        <input type="text" class="form-control" readonly="readonly" id="DefaultCells" value="@Model.preferences.DefCells" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-8">
                        <button type="button" class="btn btn-primary" id="AssignZones">Select Zones</button>
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-primary" id="UpdateToDefault">Set to Default Cell Quantity</button>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-success" id="Process">Process Batch</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-7">
        <div class="panel panel-info top-spacer">
            <div class="panel-heading">
                <h4 class="panel-title">Totes</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary" id="AssignNextIDAll">Assign all IDs</button> 
                        <button type="button" class="btn btn-primary" id="PrintToteLabels" disabled="disabled">Print Tote / Location Labels</button>
                    </div>
                    <div class="col-md-4">
                        <label>Enter Tote ID</label>
                        <input id="ScanNextTote" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <label>Position</label>
                    </div>
                    <div class="col-md-1">
                        <label>Cells</label>
                    </div>
                    <div class="col-md-3">
                        <label>Tote ID</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="batch_totes_container" style="overflow-y:scroll;">

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Setup/Setup.js"></script>
@Html.partial("~/Areas/Induction/Views/ProcessPutAways/Setup/ZoneLabelModalPartial.vbhtml")