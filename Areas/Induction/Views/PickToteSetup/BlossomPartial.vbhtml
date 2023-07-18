<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="BlossomModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="BlossomModal" aria-hidden="true">
    <div class="modal-dialog" style="width:50%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="PickToteManLabel">
                    Blossom Tote
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Original Tote ID</label>
                        <input type="text" maxlength="50" id="OriginalBlossTote" placeholder="Old Tote ID" class="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label>New Tote ID</label>
                        <input type="text" maxlength="50" id="NewBlossTote" placeholder="New Tote ID" class="form-control" />
                    </div>
                    <div class="col-md-4" style="padding-top:2.5%">
                        <button type="button" class="btn btn-primary btn-block" id="BlossUseNextToteID">Use Next Tote ID</button>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-4">
                        <button type="button" class="btn btn-success btn-block" id="SubmitBlossom">Submit Blossom</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="BlossomModalDismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/PickToteSetup/BlossomPartial.js"></script>