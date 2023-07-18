<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="pick_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="pick_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="pick_label">Tote ID Entry</h4>
            </div>
            <div class="modal-body" style="padding-bottom:0px;">
                <input type="hidden" value="@Model.BMSettings(3)" id="NextToteID" />
                <input type="hidden" value="@(IIF(Model.BMSettings(4), "TRUE", "FALSE"))" id="AutoPrintLabels" />
                <div class="row">
                    <div class="col-md-4">
                        <label>Order Number</label>
                    </div>
                    <div class="col-md-2">
                        <label>Tote Number</label>
                    </div>
                    <div class="col-md-4">
                        Tote ID
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-12" id="ToteEntryContainer">
                        
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary" id="PickCreate">Create Tote IDs</button>
                        <button type="button" class="btn btn-primary" id="PickClear">Clear All</button>
                        <button type="button" class="btn btn-primary" id="PickPrint">Print All Labels</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-default" id="pick_dismiss">Cancel</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal" id="pick_submit">Submit</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>