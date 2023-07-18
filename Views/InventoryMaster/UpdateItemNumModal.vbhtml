<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="UpdateItemNumModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="UpdateItemNumModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="UpdateItemNumLabel">Edit Item Number</h4>
            </div>
            <div class="modal-body">
                <div class="row" hidden id="DuplicateItemNumRow">
                    <div class="col-md-12">
                        <div class="alert alert-danger" role="alert">
                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                            The new item number value already exists
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-5">
                        <strong>Old Item Number:</strong><input type="text" id="OldItemNumUpdate" class="form-control" disabled="disabled" maxlength="50" />
                    </div>
                    <div class="col-md-5">
                        <strong>New Item Number:</strong><input type="text" id="NewItemNumUpdate" class="form-control" maxlength="50" />
                    </div>
                    <div class="col-md-2" style="padding-top:20px;">
                        <button type="button" disabled class="btn btn-primary btn-block" id="SaveNewItemNum">Save</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="category_dismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Inventory Master/UpdateItemNumModal.js"></script>