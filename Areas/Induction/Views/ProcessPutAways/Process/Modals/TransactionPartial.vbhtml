<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="trans_modal" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="trans_label" aria-hidden="true">
    <div class="modal-dialog" style="width:99%;margin-top:10px;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-3">
                        <h4 class="modal-title" id="trans_label">Select Transaction for Tote</h4>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-12">
                                <label>@Model.aliases.ItemNumber:</label> <span id="trans_item"></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Description:</label> <span id="trans_desc"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>Batch ID:</label> <span id="modal_batch"></span>
                    </div>
                    <div class="col-md-3">
                        <label>Zones:</label> <span id="modal_zones"></span>
                    </div>
                </div>
            </div>
            <div class="modal-body modal-backdrop" style="background:#efedbd">
                <input type="hidden" id="trans_input_type" />
                <input type="hidden" id="OTID" />
                <input type="hidden" id="InvMapID" />
                @Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/TransListPartial.vbhtml")
                @Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/PutAwayDetailPartial.vbhtml")
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default " data-dismiss="modal" id="trans_dismiss">Cancel</button>
                <button type="button" class="btn btn-primary" id="trans_create">New Put Away for Same SKU</button>
                <button type="button" class="btn btn-primary pull-right" id="trans_different">Choose a Different Transaction</button>
            </div>
        </div>
    </div>
</div>