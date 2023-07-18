<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->
@modeltype Object

<div class="row">
    <div id="Errors" class="col-md-12">
        <div id="useradd_alert">
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title"> Employee Information</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                @If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Or (PickPro_Web.Config.getSecurityEnvironment.Contains("AD") AndAlso PickPro_Web.Config.getSecurityEnvironment = "ADLDS") Then
                                    @<button style="margin-right:2px;margin-bottom:10px;" type="button" Class="btn btn-primary" id="addButton" data-toggle="tooltip" data-placement="top" title="Add New Employee">
                                        <span Class="glyphicon glyphicon-plus"></span>
                                    </button>
                                End If
                                <Button disabled="disabled" style="margin-right:2px;margin-bottom:10px;" type="button" Class="btn btn-primary" id="saveButton" data-toggle="tooltip" data-placement="top" title="Save Changes"><span Class="glyphicon glyphicon-floppy-disk"></span></Button>
                                <Button style="margin-right:2px;margin-bottom:10px;" type="button" Class="btn btn-primary Print-Report" id="printButton" data-toggle="tooltip" data-placement="top" title="Print Employee List"><span Class="glyphicon glyphicon-print"></span></Button>
                                <Button disabled="disabled" style="margin-right:2px;margin-bottom:10px;" type="button" Class="btn btn-danger" id="deleteButton" data-toggle="tooltip" data-placement="top" title="Delete Employee"><span Class="glyphicon glyphicon-trash"></span></Button>
                            </div>
                        </div>
                        <div Class="row">
                            <div Class="col-md-12">
                                <Label> Employee Lookup</Label>
                            </div>
                        </div>
                        <div Class="row">
                            <div Class="col-md-9 col-xs-10">
                                <div Class="form-group has-warning has-feedback forced-typeahead">
                                    <input maxlength="50" type="text" Class="form-control typeahead" id="employeeLookup" placeholder="Employee" data-toggle="tooltip" data-placement="top" title="You must Select an Employee from the dropdown to continue" />
                                    <span Class="glyphicon glyphicon-warning-sign form-control-feedback" data-toggle="tooltip" data-placement="top" style="top:0;"></span>
                                </div>
                            </div>
                            <div Class="col-md-3 col-xs-2">
                                <Button id="clearButton" Class="btn btn-primary" data-toggle="tooltip" data-placement="top" title="Clear"><span Class="glyphicon glyphicon-remove"></span></Button>
                            </div>
                        </div>
                        <div id="employeeInfo" hidden>
                            <div Class="row">
                                <div Class="col-md-12">
                                    <div Class="checkbox">
                                        <Label>
                                            <input Class="employeeChange" disabled id="active" type="checkbox"> Active Employee?
                                        </Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10">
                                    <div class="form-group">
                                        <label for="">First Name</label>
                                        <input disabled type="text" class="form-control employeeChange" id="firstName" maxlength="30" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="">MI</label>
                                        <input disabled type="text" class="form-control employeeChange" id="MI" maxlength="10" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="">Last Name</label>
                                        <input disabled type="text" class="form-control employeeChange" id="lastName" maxlength="30" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="">Username</label>
                                        <input disabled type="text" class="form-control " id="userName" maxlength="12" />
                                    </div>
                                </div>

                            </div>
                            @If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Or (PickPro_Web.Config.getSecurityEnvironment.Contains("AD") AndAlso PickPro_Web.Config.getSecurityEnvironment = "ADLDS") Then
                                @<div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="">Change Password</label>
                                            <input disabled type="password" class="form-control employeeChange" id="password" maxlength="16" />
                                        </div>
                                    </div>
                                </div>
                            End If
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="">Access Level</label>
                                        <select disabled id="accessLevel" class="form-control employeeChange">
                                            <option value="Staff Member"> Staff Member</option>
                                            <option value="Administrator"> Administrator</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            @If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Then
                                @<div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="">Group</label>
                                            <select disabled class="form-control employeeChange" id="group">
                                                @For Each Group In Model.allGroups
                                                    @<option value="@Group">@Group</option>
                                                Next
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            End If
                            <div Class="row">
                                <div Class="col-md-12">
                                    <div Class="form-group">
                                        <label for="">Email Address</label>
                                        <input disabled type="email" Class="form-control employeeChange" id="email" maxlength="255" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
               
            </div>
        </div>
    </div>
    <div Class="col-md-9">
        <ul Class="nav nav-tabs" role="tablist">
            <li Class="active"><a href="#bulkPro" role="tab" data-toggle="tab">BulkPro Settings</a></li>
            <li> <a href="#pickLevels" role="tab" data-toggle="tab">Employee Pick Levels</a></li>
            @If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Then
                @<li> <a href="#functionsAllowed" role="tab" data-toggle="tab">Functions Allowed</a></li>
            Else
                @<li> <a href="#groupsAllowed" role="tab" data-toggle="tab">Groups Allowed</a></li>
            End If
        </ul>
        <!-- Tab panes -->
        <div Class="tab-content">
            <div Class="tab-pane active" id="bulkPro">
                <div Class="panel panel-info">
                    <div Class="panel-body">
                        <div Class="row">
                            <div Class="col-md-2">
                                <ul Class="nav nav-pills nav-stacked" style="padding-right:10px;border-right: 2px solid grey;" role="tablist">
                                    <li Class="active"><a href="#home" data-toggle="tab">Zones</a></li>
                                    <li> <a href="#profile" data-toggle="tab">Locations</a></li>
                                    <li> <a href="#messages" data-toggle="tab">Orders</a></li>
                                </ul>
                            </div>
                            <div Class="col-md-10">
                                <div Class="tab-content">
                                    <div Class="tab-pane active" id="home">
                                        <div Class="row">
                                            <div id="zoneAlert">
                                            </div>
                                        </div>
                                        <div Class="row">
                                            <div Class="col-xs-9">
                                                <strong> Zone</strong>
                                            </div>
                                            <div Class="col-xs-3">
                                                <Button disabled id="addNewZone" style="margin-bottom:5px;" Class="btn btn-primary"> <span Class="glyphicon glyphicon-plus"></span></Button>
                                            </div>
                                        </div>
                                        <div id="zonesAppend">
                                        </div>
                                    </div>
                                    <div Class="tab-pane" id="profile">
                                        <div Class="row">
                                            <div Class="col-xs-5">
                                                <strong> Start Location</strong>
                                            </div>
                                            <div Class="col-xs-5">
                                                <strong>End Location</strong>
                                            </div>
                                            <div Class="col-xs-2">
                                                <Button disabled style="margin-bottom:5px;" Class="btn btn-primary" id="addLocation"> <span Class="glyphicon glyphicon-plus"></span></Button>
                                            </div>
                                        </div>
                                        <div id="locationsAppend">
                                        </div>
                                    </div>
                                    <div Class="tab-pane" id="messages">
                                        <div Class="row">
                                            <div Class="col-md-12">
                                                <div Class="form-group">
                                                    <Label for="">Maximum Handheld Orders</Label>
                                                    <input disabled type="text" Class="form-control employeeChange" id="maxOrders" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="pickLevels">
                <div class="panel panel-info">
                    <div class="panel-body">
                        <div class="row">
                            <div id="pickLevelAlert">

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-2">
                                <strong>Pick Level</strong>
                            </div>
                            <div class="col-xs-2">
                                <strong>Start Carousel</strong>
                            </div>
                            <div class="col-xs-2">
                                <strong>End Carousel</strong>
                            </div>
                            <div class="col-xs-2">
                                <strong>Start Shelf</strong>
                            </div>
                            <div class="col-xs-2">
                                <strong>End Shelf</strong>
                            </div>
                            <div class="col-xs-1">
                                <button disabled id="addPickLevel" class="btn btn-primary" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-plus"></span></button>
                            </div>
                        </div>
                        <div id="pickLevelsAppend">

                        </div>
                    </div>
                </div>
            </div>
            @If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Then
                @<div Class="tab-pane" id="functionsAllowed">
                    <div Class="panel panel-info">
                        <div Class="panel-body">
                            <div Class="row">
                                <div Class="col-xs-9">
                                    <strong> Function</strong>
                                </div>
                                <div Class="col-xs-2">
                                    <Button disabled id="addAllControls" style="margin-bottom:5px;" Class="btn btn-primary btn-block">Add All</Button>
                                </div>
                                <div Class="col-xs-1">
                                    <Button disabled id="addNewFunction" onclick="$('#controlNameModal').modal('show')" style="margin-bottom:5px;" Class="btn btn-primary pull-right"><span Class="glyphicon glyphicon-plus"></span></Button>
                                </div>
                            </div>
                            <div id="functions" style="max-height:625px;overflow-y:scroll;">
                            </div>
                        </div>
                    </div>
                </div>
            Else
                @<div Class="tab-pane" id="groupsAllowed">
                    <div Class="panel panel-info">
                        <div Class="panel-body">
                            <div Class="row">
                                <div Class="col-xs-11">
                                    <strong> Group</strong>
                                </div>
                                <div Class="col-xs-1">
                                    <button disabled id="addNewGroup" onclick="$('#groupModal').modal('show')" style="margin-bottom:5px;" Class="btn btn-primary pull-right"><span Class="glyphicon glyphicon-plus"></span></button>
                                </div>
                            </div>
                            <div id="userGroups" style="max-height:625px;overflow-y:scroll;">
                            </div>
                        </div>
                    </div>
                </div>
            End If

        </div>
    </div>
</div>
@Html.Partial("GroupChangePartial")
