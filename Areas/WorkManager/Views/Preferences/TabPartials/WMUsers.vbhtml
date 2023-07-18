<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group has-feedback has-warning">
                            <label>Last Name</label>
                            <input maxlength="30" type="text" class="form-control" id="UserToSelect" placeholder="Last Name" />
                            <span class="glyphicon glyphicon-warning-sign form-control-feedback" data-toggle="tooltip" data-original-title="You must select a worker from the dropdown." data-placement="top"></span>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label>First Name</label>
                        <input type="text" class="form-control" id="FirstName" disabled="disabled" />
                    </div>
                    <div class="col-md-4">
                        <label>Username</label>
                        <input type="text" class="form-control" id="Username" disabled="disabled"  />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-8">
                        <table class="table table-condensed table-bordered table-striped" style="background-color:white;">
                            <thead>
                                <tr>
                                    <td class="text-left"><strong>Trans. Type</strong></td>
                                    <td class="text-left"><strong>Allow Transactions</strong></td>
                                    <td class="text-left"><strong>Auto Batch</strong></td>
                                    <td class="text-left"><strong>Default Orders</strong></td>
                                    <td class="text-left"><strong>Max Orders</strong></td>
                                    <td class="text-left"><strong>Max Lines</strong></td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><strong>Picks</strong></td>
                                    <td><div class="toggles disabled text-left toggle-modern" id="ATPick"></div></td>
                                    <td><div class="toggles disabled text-left toggle-modern" id="ABPick"></div></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="DOPick" /></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="MOPick" /></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="MLPick" /></td>
                                </tr>
                                <tr>
                                    <td><strong>Put Aways</strong></td>
                                    <td><div class="toggles disabled text-left toggle-modern" id="ATPut"></div></td>
                                    <td><div class="toggles disabled text-left toggle-modern" id="ABPut"></div></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="DOPut"  /></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="MOPut"  /></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="MLPut"  /></td>
                                </tr>
                                <tr>
                                    <td><strong>Counts</strong></td>
                                    <td><div class="toggles disabled text-left toggle-modern" id="ATCount"></div></td>
                                    <td><div class="toggles disabled text-left toggle-modern" id="ABCount"></div></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="DOCount"  /></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="MOCount"  /></td>
                                    <td><input disabled="disabled" maxlength="5" type="text" class="form-control input-sm" id="MLCount"  /></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                    <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                        <li>
                                            <a href="#" id="PrintWorker">Print Selected Worker</a>
                                            <a href="#" id="PrintAll">Print All Workers</a>
                                        </li>
                                    </ul>
                                </div>
                                <button type="button" class="btn btn-primary" id="SaveUser" data-toggle="tooltip" data-original-title="Save Changes to Worker" data-placement="top"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                                <button type="button" class="btn btn-danger" id="RemoveWorker" data-toggle="tooltip" data-original-title="Remove Worker" data-placement="top"><span class="glyphicon glyphicon-remove"></span></button>
                                <button type="button" class="btn btn-danger" id="ClearBatch">Clear  Worker's Batches</button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Active</label>
                                <div class="toggles disabled toggle-modern" id="Active"></div>
                            </div>
                            <div class="col-md-6">
                                <label>WM Settings</label>
                                <div class="toggles disabled toggle-modern" id="WMSettings"></div>
                            </div>
                            <div class="col-md-6">
                                <label>Organize Work</label>
                                <div class="toggles disabled toggle-modern" id="OrganizeWork"></div>
                            </div>
                            <div class="col-md-6">
                                <label>Reports</label>
                                <div class="toggles disabled toggle-modern" id="Reports"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">WM Users</h3>
            </div>
            <div class="panel-body">
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <table class="table table-condensed table-bordered table-striped" style="background-color:white;" id="WMUserTable">
                            <thead>
                                <tr>
                                    <td rowspan="2" style="vertical-align:middle!important;">Username</td>
                                    <td rowspan="2" style="vertical-align:middle!important;">First Name</td>
                                    <td rowspan="2" style="vertical-align:middle!important;">Last Name</td>
                                    <td rowspan="2" style="vertical-align:middle!important;">Active</td>
                                    <td rowspan="2" style="vertical-align:middle!important;">WM Settings</td>
                                    <td rowspan="2" style="vertical-align:middle!important;">Organize Work</td>
                                    <td rowspan="2" style="vertical-align:middle!important;">Reports</td>
                                    <td colspan="5">Picks</td>
                                    <td colspan="5">Put Aways</td>
                                    <td colspan="5">Counts</td>
                                </tr>
                                <tr>
                                    <td>Allow</td>
                                    <td>Allow Batch</td>
                                    <td>Default Orders</td>
                                    <td>Max Orders</td>
                                    <td>Max Lines</td>

                                    <td>Allow</td>
                                    <td>Allow Batch</td>
                                    <td>Default Orders</td>
                                    <td>Max Orders</td>
                                    <td>Max Lines</td>

                                    <td>Allow</td>
                                    <td>Allow Batch</td>
                                    <td>Default Orders</td>
                                    <td>Max Orders</td>
                                    <td>Max Lines</td>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>