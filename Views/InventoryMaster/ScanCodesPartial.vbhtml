<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12" id="scancode_alerts">

    </div>
</div>

<div class="row">
    <div class="col-md-6">
        <div class="row">
            <div class="col-md-6">
                <label>Scan Code:</label>
            </div>
            <div class="col-md-6">
                <label>Scan Type:</label>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="row">
            <div class="col-md-4">
                <label>Scan Range</label>
            </div>
            <div class="col-md-4">
                <label>Start Position</label>
            </div>
            <div class="col-md-4">
                <label>Code Length</label>
            </div>
        </div>
    </div>
    <div class="col-md-2">
        <div class="pull-right">
            <button id="addNewScan" type="button" data-toggle="tooltip" data-placement="top" title="" class="btn btn-primary" style="margin-bottom:10px;" data-original-title="Add New Item" disabled="disabled">
                <span class="glyphicon glyphicon-plus"></span>
            </button>
        </div>
    </div>
</div>
<div class="row" style="padding-top:15px;">
    <div class="col-md-12" id="scanCodeContainer">
    </div>
</div>
@Html.Partial("~/Views/ModalPartials/ScanCodeModalPartial.vbhtml")