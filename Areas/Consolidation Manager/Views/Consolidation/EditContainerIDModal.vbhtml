<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->
<div class="modal fade" id="EditContainerIDModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="EditContainerIDModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="EditContainerIDModalTitle">Set Container ID for Selected Transaction</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Container ID: </label><input type="text" class="form-control" maxlength="50" id="ContainerIDToSet" />
                    </div>
                    <div class="col-md-1" style="padding-top:25px;">
                        <button type="button" data-toggle="tooltip" title="Clear Container ID" data-placement="top" class="btn btn-danger" id="ClearSetContainerID"><span class="glyphicon glyphicon-remove"></span></button>
                    </div>
                    <div class="col-md-5" style="padding-top:25px;">
                        <button type="button" disabled="disabled" class="btn btn-block btn-success" data-dismiss="modal" id="SetContainerIDButt">Set Container ID</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="EditContainerIDModalDismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/Consolidation/EditContainerIDModal.js"></script>