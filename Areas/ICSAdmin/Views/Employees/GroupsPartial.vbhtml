<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->
@modeltype Object
<div class="row">
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Employee Group Management</h3>
            </div>
            <div class="panel-body">
                <div class="row" style="padding-top:5px;">
                    <div class="col-md-12">
                        <button type="button" style="margin-right: 2px;margin-bottom:5px;" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Add New Group" id="addGroup"><span class="glyphicon glyphicon-plus"></span></button>
                        <button type="button" style="margin-right: 2px;margin-bottom:5px;" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Save Group" id="saveGroup" disabled="disabled"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                        <div class="btn-group">
                            <button style="margin-bottom: 5px;" id="goTo" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                            <ul class="dropdown-menu" role="menu">
                                <li><a id="printByGroup" class="Print-Report">Print Selected Group</a></li>
                                <li><a id="printByAll" class="Print-Report">Print All Groups</a></li>
                            </ul>
                        </div>
                        @If PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Then
                            @<button type="button" style="margin-right: 2px;margin-bottom:5px;" Class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Clone Group" id="cloneGroup" disabled="disabled"><span Class="glyphicon glyphicon-book"></span></button>
                        End If
                        <button type="button" style="margin-right: 2px;margin-bottom:5px;" class="btn btn-danger" data-toggle="tooltip" data-placement="top" data-original-title="Delete Group" id="delGroups" disabled="disabled"><span class="glyphicon glyphicon-trash"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Select Employee Group</label>

                    </div>
                </div>
                <div class="row" style="padding-top:5px;">
                    <div class="col-md-10">
                        <select id="currentEmployeeGroup" class="form-control">
                            <option value=" "> </option>
                            @For Each emGroup In Model.allGroups
                                @<option>@emGroup</option>
                            Next
                        </select>
                    </div>
                    <div class="col-md-2">
                        <button type="button" style="margin-right: 2px;" class="btn btn-primary" id="clearAll"><span class="glyphicon glyphicon-remove"></span></button>
                    </div>
                </div>
                <div class="row" style="padding-top:5px;">
                    <div class="col-md-12">
                        @If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Then
                            @<button type="button" style="margin-right: 2px;" Class="btn btn-primary btn-block" id="updateGroup" disabled="disabled">Update Employees In Group</button>
                        End If
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div Class="col-md-9">
        <div Class="panel panel-info">
            <div Class="panel-heading">
                <h3 Class="panel-title">Assigned Functions</h3>
            </div>
            <div Class="panel-body" style="overflow-y:scroll;">
                <div Class="row">
                    <div Class="col-md-4">
                        <ul id="unassigned" Class="list-unstyled btn-group-vertical ui-sortable linked">
                            <li Class="static btn btn-black btn-sm">Unassigned Employee Functions</li>
                        </ul>
                    </div>
                    <div Class="col-md-4">
                        <ul id="assigned" Class="list-unstyled btn-group-vertical ui-sortable linked">
                            <li Class="static btn btn-black btn-sm">Assigned Employee Functions</li>
                        </ul>
                    </div>
                    <div Class="col-md-4">
                        <div Class="row">
                            <div Class="col-md-12">
                                <Button type="button" style="margin-right: 2px;" Class="btn btn-primary btn-block" id="addAll" disabled="disabled">Add All Functions</Button>
                            </div>
                            <div Class="col-md-12">
                                <Button type="button" style="margin-right: 2px; margin-top:5px;" Class="btn btn-danger btn-block" id="removeAll" disabled="disabled">Remove All Functions</Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div Class="modal fade" id="employee_group_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="employee_group_label" aria-hidden="true">
    <div Class="modal-dialog" style="width:900px;">
        <div Class="modal-content">
            <div Class="modal-header">
                <h4 Class="modal-title" id="employee_group_label">Add Employee Group</h4>
            </div>
            <div Class="modal-body">
                <div Class="row">
                    <div Class="col-md-6">
                        <Label> Carousel(s) :     </Label>
                        <div id="employee_group_car"> -</div>
                    </div>
                    <div Class="col-md-6">
                        <Label> Zone(s) :     </Label>
                        <div id="employee_group_zone"> -</div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="employee_group_dismiss">Close</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="employee_group_submit">Go To Transactions</button>
            </div>
        </div>
    </div>
</div>
@Html.Partial("AddEmployeeGroupPartial")
@If PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Then
    @Html.Partial("CloneEmployeeGroupPartial")
End If

