<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="FTPPasswordModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="FTPasswordModal" aria-hidden="true">
    <div class="modal-dialog" style="width:60%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">
                    Change Password
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-5">
                        <label>New Password</label>
                    </div>
                    <div class="col-md-5">
                        <label>Confirm Password</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <input type="text" class=form-control  id="FTPNewPassword" maxlength="130"/>
                    </div>
                    <div class="col-md-5">
                        <input type="text" class=form-control id="FTPConfPassword" maxlength="130" />
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="top" title="Clear Password" id="ClearFTPPassword"><span class="glyphicon glyphicon-remove"></span></button>
                        <button type="button" data-toggle="tooltip" data-placement="top" disabled title="Save Password" class="btn btn-primary" id="SaveFTPPassword"><span class="glyphicon glyphicon-floppy-disk"></span></button> 
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default " data-dismiss="modal" id="ftppass_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>
