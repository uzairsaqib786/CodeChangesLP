<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="SQLAuthModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="SQLAuthModal_label" aria-hidden="true">
    <div class="modal-dialog" style="width:600px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="SQLAuthModalTitle">SQL Auth Username and Password</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="alert alert-danger" role="alert">
                            <b>
                                Warning! Setting the user name and password fields will set this connection to use SQL Authentication.
                                To use Windows Authentication make sure these fields are empty.
                            </b>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8 col-md-offset-2">
                        <label id="SQLAuthConnectionName"></label>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-8 col-md-offset-2">
                        <strong>User Name:</strong>
                        <input type="text" class="form-control" id="SQLAuthUser" />
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-8 col-md-offset-2">
                        <strong>Password:</strong>
                        <input type="password" class="form-control" id="SQLAuthPass" />
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-4 col-md-offset-2">
                        <button type="button" id="ClearLoginInfo" class="btn btn-block btn-primary">Clear Login Info</button>
                    </div>
                </div>
                @*<input type="text" style="display:none" id="SQLAuthConnectionName" class="form-control" />*@
            </div>
            <div class="modal-footer">
                <button type="button" id="SaveSQLAuth" class="btn btn-success">Save Login</button>
                <button type="button" id="SQLAuthModalClose" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/GlobalConfig/SQLAuthModalPartial.js"></script>
