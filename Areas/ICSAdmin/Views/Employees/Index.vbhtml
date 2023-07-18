<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->
@modeltype Object
@code
    ViewData("Title") = "Employees"
    ViewData("PageName") = "&nbsp; | &nbsp; Employees"
End code
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs" role="tablist">
                <li class="active"><a href="#employees" role="tab" data-toggle="tab">Employees</a></li>
                <li><a href="#groups" role="tab" data-toggle="tab">Groups</a></li>
                <li><a href="#statistics" role="tab" data-toggle="tab">Statistics</a></li>
                @If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Then
                    @<li><a href="#buttons" role="tab" data-toggle="tab">Button Access</a></li>
                End If
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
                <div class="tab-pane active" id="employees" style="margin-top:5px;">
                    @Html.Partial("EmployeePartial", Model)
                </div>
                <div class="tab-pane" id="groups" style="margin-top:5px;">
                    @Html.Partial("GroupsPartial", Model)
                </div>
                <div class="tab-pane" id="statistics" style="margin-top:5px;">
                    @Html.Partial("EmployeeStatsPartial", Model)
                </div>
                <div class="tab-pane" id="buttons">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-5">
                                    <label> Control Name</label>
                                </div>
                                <div class="col-md-5">
                                    <label> Function</label>
                                </div>
                                <div class="col-md-2">
                                    <label> Admin Level</label>
                                </div>
                            </div>
                            <div style="max-height:625px;overflow-y:scroll;">
                                @For Each test In Model.allControls
                                    @<div class="row">
                                        <div class="col-md-5">
                                            <input disabled class="form-control" type="text" value="@test.Item1" maxlength="50" />
                                        </div>
                                        <div class="col-md-5">
                                            <input disabled class="form-control" type="text" value="@test.Item2" maxlength="50" />
                                        </div>
                                        <div class="col-md-2">
                                            @If test.Item3 Then
                                                @<input class="form-control controlChange" type="checkbox" checked name="@test.Item1" />
                                            Else
                                                @<input class="form-control controlChange" type="checkbox" name="@test.Item1" />
                                            End If
                                        </div>
                                    </div>
                                Next
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Modal -->
@If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Then
    @<div Class="modal fade" id="controlNameModal" tabindex="-1" role="dialog" aria-labelledby="controlNameLabel" aria-hidden="true">
        <div Class="modal-dialog" style="width:900px;">
            <div Class="modal-content">
                <div Class="modal-header">
                    <Button type="button" Class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span Class="sr-only">Close</span></Button>
                    <h4 Class="modal-title" id="controlNameLabel">Set Control Name</h4>
                </div>
                <div Class="modal-body">
                    <div id="controlAlert" Class="row">
                    </div>
                    <div Class="row">
                        <div Class="col-md-6 col-md-offset-3">
                            <div Class="form-group">
                                <Label Class="text-center">Control Names</Label>
                                <input id="controlNameTypeahead" Class="typeahead form-control" type="text" maxlength="50" />
                            </div>
                        </div>
                    </div>
                </div>
                <div Class="modal-footer">
                    <Button id="controlNameSubmit" type="button" Class="btn btn-primary">Submit</Button>
                    <Button type="button" Class="btn btn-default" data-dismiss="modal">Close</Button>
                </div>
            </div>
        </div>
    </div>
End If
<div Class="modal fade" id="groupModal" tabindex="-1" role="dialog" aria-labelledby="controlNameLabel" aria-hidden="true">
    <div Class="modal-dialog" style="width:900px;">
        <div Class="modal-content">
            <div Class="modal-header">
                <Button type="button" Class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span Class="sr-only">Close</span></Button>
                <h4 Class="modal-title" id="groupNameLabel">Set group</h4>
            </div>
            <div Class="modal-body">
                <div id="groupAlert" Class="row">
                </div>
                <div Class="row">
                    <div Class="col-md-6 col-md-offset-3">
                        <div Class="form-group">
                            <Label Class="text-center">Group Names</Label>
                            <input id="groupNameTypeahead" Class="typeahead form-control" type="text" maxlength="50" />
                        </div>
                    </div>
                </div>
            </div>
            <div Class="modal-footer">
                <Button id="groupNameSubmit" type="button" Class="btn btn-primary">Submit</Button>
                <Button type="button" Class="btn btn-default" data-dismiss="modal">Close</Button>
            </div>
        </div>
    </div>
</div>
<Script src="~/Scripts/jquery.dataTables.js"></Script>
<Script src="~/Scripts/dataTables.bootstrap.js"></Script>
<Script src="~/Areas/ICSAdmin/Scripts/Employees/Employees.js"></Script>
<Script src="~/Areas/ICSAdmin/Scripts/Employees/EmployeesStats.js"></Script>
<Script src="~/Areas/ICSAdmin/Scripts/Employees/EmployeesHub.js"></Script>
<Script src="~/Scripts/jquery-ui-1.10.4.custom.js"></Script>
<Script src="~/Areas/ICSAdmin/Scripts/Employees/EmployeeSorting.js"></Script>
<Script src="~/Areas/ICSAdmin/Scripts/Employees/EmployeeGroups.js"></Script>
@Html.Partial("~/Areas/ICSAdmin/Views/Employees/AddNewEmployeeModalPartial.vbhtml")
@Html.Partial("~/Areas/ICSAdmin/Views/Employees/EmployeeLocationsPartial.vbhtml")
