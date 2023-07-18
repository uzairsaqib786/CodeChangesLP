<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->
@If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Or (PickPro_Web.Config.getSecurityEnvironment.Contains("AD") AndAlso PickPro_Web.Config.getSecurityEnvironment = "ADLDS") Then
    @<div Class="modal fade" id="addemployee_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="addemployee_label" aria-hidden="true">
        <div Class="modal-dialog" style="width:900px;">
            <div Class="modal-content">
                <div Class="modal-header">
                    <h4 Class="modal-title" id="addemployee_label">Add New Employee</h4>
                </div>
                <div Class="modal-body">
                    <div Class="row">
                        <div Class="col-md-12">
                            <div id="addemployee_alerts">
                            </div>
                        </div>
                    </div>
                    <div Class="row">
                        <div Class="col-md-6">
                            <Label> Last Name</Label>
                            <input type="text" Class="form-control" id="em_lastname" placeholder="Last Name" maxlength="30" />
                        </div>
                        <div Class="col-md-5">
                            <Label> First Name</Label>
                            <input type="text" Class="form-control" id="em_firstname" placeholder="First Name" maxlength="30" />
                        </div>
                        <div Class="col-md-1">
                            <Label> MI</Label>
                            <input type="text" Class="form-control" id="em_middleinitial" placeholder="MI" maxlength="10" />
                        </div>
                    </div>
                    <div Class="row">
                        <div Class="col-md-4">
                            <Label> Username</Label>
                            <input type="text" Class="form-control" id="em_username" placeholder="Username" maxlength="50" />
                        </div>
                        <div Class="col-md-4">
                            <Label> Access Level</Label>
                            <select Class="form-control" id="em_access">
                                <option selected="selected"> Staff Member</option>
                                <option> Administrator</option>
                            </select>
                        </div>
                        <div Class="col-md-4">
                            <Label> Password</Label>
                            <input type="password" Class="form-control" id="em_password" placeholder="Password" maxlength="16" />
                        </div>
                    </div>
                </div>
                <div Class="modal-footer">
                    <Button type="button" Class="btn btn-default" data-dismiss="modal" id="addemployee_dismiss">Close</Button>
                    <Button type="button" Class="btn btn-primary" id="addemployee_submit">Submit</Button>
                </div>
            </div>
        </div>
    </div>
End If

<script src="~/Areas/ICSAdmin/Scripts/Employees/AddNewEmployeePartial.js"></script>
