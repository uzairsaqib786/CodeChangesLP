<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.ConfigModel

@Code
    ViewData("Title") = "Global Config"
    Dim running As Boolean = PickPro_Web.PrintService.PrintServiceRunning
    Dim STERunning As Boolean = PickPro_Web.PrintService.PrintServiceRunning
    Dim ccsifRunning As Boolean = PickPro_Web.CCSIFService.ServiceStatus
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#Home" aria-controls="home" role="tab" data-toggle="tab">Home</a></li>
                <li role="presentation"><a href="#Connections" aria-controls="Connection" role="tab" data-toggle="tab">Database Connections</a></li>
                <li role="presentation"><a href="#Printers" aria-controls="Printers" role="tab" data-toggle="tab">Printers</a></li>
                <li role="presentation"><a href="#CCSIF" aria-controls="CCSIF" role="tab" data-toggle="tab">CCSIF</a></li>
                <li role="presentation"><a href="#Licensing" aria-controls="Licensing" role="tab" data-toggle="tab">Licensing</a></li>
                <li role="presentation"><a href="#WSLicensing" aria-controls="WSLicensing" role="tab" data-toggle="tab">Workstation Apps</a></li>
                <li role="presentation"><a href="#STEService" aria-controls="WSLicensing" role="tab" data-toggle="tab">STE Service</a></li>
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="Home">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <legend class="text-center">Global User Account</legend>
                                            <form method="post" action="/GlobalConfig/ChangeGlobalAccount" role="form" id="LogonForm">
                                                @Html.AntiForgeryToken()
                                                <div class="form-group">
                                                    <label for="username">Username</label>
                                                    <input type="text" class="form-control" id="username" name="username" placeholder="Username" value="@Model.logonInfo.Username" maxlength="12">
                                                </div>
                                                <div class="form-group">
                                                    <label for="password">Password</label>
                                                    <input type="password" class="form-control" id="password" name="password" placeholder="Password" value="@Model.logonInfo.Password" maxlength="8">
                                                </div>
                                                <button type="submit" class="btn btn-primary pull-right" id="changeInfoSubmit">Submit</button>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <legend class="text-center">Connected Users</legend>
                                    <div style="max-height:500px;overflow-y:scroll;">
                                        <table class="table table-bordered table-condensed table-striped" style="background-color:white">
                                            <thead>
                                                <tr>
                                                    <th>User</th>
                                                    <th>PC Name</th>
                                                    <th>App Name</th>
                                                </tr>
                                            </thead>
                                            <tbody id="ConnectedTable">
                                                @For Each PPApp In PickPro_Web.Config.AppUsers
                                                    For Each WSID In PickPro_Web.Config.AppUsers(PPApp.Key).Keys
                                                        Dim userdata As PickPro_Web.ConnectedUser = Nothing
                                                        PickPro_Web.Config.AppUsers(PPApp.Key).TryGetValue(WSID, userdata)
                                                        @<tr id="@WSID">
                                                            <td>@userdata.Name</td>
                                                            <td>@userdata.PCName</td>
                                                            <td>@PPApp.Key</td>
                                                        </tr>
                                                    Next
                                                Next
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="Connections">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <legend class="text-center">Configuration Database</legend>
                                    </div>
                                    <div class="col-md-3">
                                        <p>Location Assignment Connection: <strong id="lblLAConn">@Model.LAConnectionString</strong></p>
                                        <select class="form-control" id="LAconnectionDiv">
                                            <option></option>
                                            @For Each connection In Model.connections
                                                @<option maxlength="50">@connection.name</option>

                                            Next
                                        </select>
                                    </div>
                                    <div class="col-md-offset-1 col-md-4">
                                        <button data-toggle="modal" data-target="#Config" id="changeDatabase" class="btn btn-primary btn-block">Change Config Database</button>
                                    </div>
                                </div>
                                <legend style="margin-top:10px;" class="text-center">Connection Strings</legend>
                                <div class="row">
                                    <div class="col-md-3"><label>Connection Name</label></div>
                                    <div class="col-md-3"><label>Database Name</label></div>
                                    <div class="col-md-3"><label>Server Name</label></div>
                                    <div class="col-md-1"><button id="newConnection" class="btn btn-block btn-primary" style="margin-bottom:5px;"><span class="glyphicon glyphicon-plus"></span></button></div>
                                </div>
                                <div id="connectionDiv">
                                    @For Each connection In Model.connections
                                        @<div class="row">
                                            <div class="col-md-3"><input name="@(connection.name)" class="form-control cName" value="@connection.name" maxlength="50" /></div>
                                            <div class="col-md-3"><input name="@(connection.name)" class="form-control db" value="@connection.dbName" maxlength="50" /></div>
                                            <div class="col-md-3"><input name="@(connection.name)" class="form-control server" value="@connection.serverName" maxlength="50" /></div>
                                            <div class="col-md-1"><button name="@(connection.name)" disabled class="btn btn-block btn-primary save-connection"><span class="glyphicon glyphicon-floppy-disk" style="margin-bottom:5px;"></span></button></div>
                                            <div class="col-md-1"><button name="@(connection.name)" class="btn btn-block btn-warning sqlauth-connection">Set SQL Auth</button></div>
                                            <div class="col-md-1"><button name="@(connection.name)" class="btn  btn-block btn-danger delete-connection" style="margin-bottom:5px;"><span class="glyphicon glyphicon-remove"></span></button></div>
                                        </div>
                                    Next
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="Printers">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    @If running Then
                                        @<legend class="text-center">Print Service <strong style="color:green;" id="Status">Online</strong></legend>
                                    Else
                                        @<legend class="text-center">Print Service <strong style="color:red;" id="Status">Offline</strong></legend>
                                    End If
                                    <div class="row">
                                        <div class="col-md-4">
                                            <button type="button" class="btn btn-block @(IIf(running, "btn-danger", "btn-primary"))" id="ServiceToggle">@(IIf(running, "Stop Print Service", "Start Print Service"))</button>
                                        </div>
                                        <div class="col-md-4">
                                            <button type="button" @(IIf(running, "", "disabled='disabled'")) class="btn btn-block btn-warning" id="RestartService">Restart Print Service</button>
                                        </div>
                                        <div class="col-md-4">
                                            <button type="button" class="btn btn-block btn-primary" id="TestService">Test Print Service</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <legend class="text-center">Printers</legend>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Printer Name</label>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Printer Address</label>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Label Printer</label>
                                        </div>
                                        <div class="col-md-1">
                                            <button id="printer_add" type="button" data-toggle="tooltip" data-placement="top" title="Add New Printer" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="printerconfig_container">
                                                @For Each printer In Model.printers
                                                    @<div class="row" style="padding-top:10px;">
                                                        <div class="col-md-4"><input name="@(printer.printer)_name" class="form-control pName" value="@printer.printer" maxlength="50" /></div>
                                                        <div class="col-md-4"><input name="@(printer.printer)_add" class="form-control pAdd" value="@printer.printeradd" maxlength="100" /></div>
                                                        <div class="col-md-1"><div class="toggles toggle-modern Label" data-toggle-ontext="Yes" data-toggle-offtext="No" data-toggle-on="@IIf(printer.label = "Able to Print Labels", "true", "false")" data-name="@(printer.printer)"></div></div>
                                                        <div class="col-md-1"><button type="button" class="btn btn-primary test-print Print-Report" data-toggle="tooltip" data-placement="top" data-original-title="Print Test Page"><span class="glyphicon glyphicon-print"></span></button></div>
                                                        <div class="col-md-1"><button name="@(printer.printer)" class="btn btn-danger remove-printer" style="margin-bottom:5px;"><span class="glyphicon glyphicon-remove"></span></button></div>
                                                        <div class="col-md-1"><button name="@(printer.printer)" disabled class="btn btn-primary save-printer"><span class="glyphicon glyphicon-floppy-disk" style="margin-bottom:5px;"></span></button></div>
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
                <div role="tabpanel" class="tab-pane" id="CCSIF">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    @If ccsifRunning Then
                                        @<legend class="text-center">CCSIF Service <strong style="color:green;" id="CCSIFStatus">Online</strong></legend>
                                    Else
                                        @<legend class="text-center">CCSIF Service <strong style="color:red;" id="CCSIFStatus">Offline</strong></legend>
                                    End If
                                    <div class="row">
                                        <div class="col-md-4 col-md-offset-4">
                                            <button type="button" class="btn btn-block @(IIf(ccsifrunning, "btn-danger", "btn-primary"))" id="CCSIFToggle">@(IIf(ccsifRunning, "Stop CCSIF Service", "Start CCSIF Service"))</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="Licensing">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend class="text-center">App Licensing</legend>
                                    </fieldset>
                                    <div class="row">
                                        <div class="col-md-2 col-xs-6">
                                            <label>App Name</label>
                                        </div>
                                        <div class="col-md-2 col-xs-6">
                                            <label>Display Name</label>
                                        </div>
                                        <div class="col-md-3 col-xs-4">
                                            <label>License</label>
                                        </div>
                                        <div class="col-md-2 col-xs-2">
                                            <label>Num. Licenses</label>
                                        </div>
                                        <div class="col-md-1 col-xs-1">
                                            <label>Status</label>
                                        </div>
                                        <div class="col-md-1 col-xs-4">
                                            <label>App URL</label>
                                        </div>
                                        <div class="col-md-1 col-xs-1">
                                            <button id="app_add" type="button" data-toggle="tooltip" data-placement="top" title="Add New App" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button>
                                        </div>
                                    </div>

                                    @For Each PPApp In PickPro_Web.Config.AppLicenses
                                        @<div class="row" style="padding-top:10px;">
                                            <div class="col-md-2 col-xs-6 bottom-spacer-half"><input disabled class="form-control app appName" value="@PPApp.Key" maxlength="50" /></div>
                                            <div class="col-md-2 col-xs-6 bottom-spacer-half"><input class="form-control app appDisplay" value="@PPApp.Value.Info.DisplayName" maxlength="50" /></div>
                                            <div class="col-md-3 col-xs-4"><input class="form-control app appLicense" value="@PPApp.Value.Info.LicenseString" maxlength="255" /></div>
                                            <div class="col-md-2 col-xs-2"><input disabled class="form-control app appNumLicense" value="@PPApp.Value.numLicenses" /></div>
                                            <div class="col-md-1 col-xs-1">
                                                <label class="appValid @IIf(PPApp.Value.isLicenseValid, "text-success", "text-Danger")">@IIf(PPApp.Value.isLicenseValid, "Valid", "Invalid")</label>
                                            </div>
                                            <div class="col-md-1  col-xs-4"><input class="form-control app appURL" value="@PPApp.Value.Info.URL" maxlength="50" /></div>
                                            <div class="col-md-1  col-xs-1"><button disabled class="btn btn-primary app appSave"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>
                                        </div>
                                    Next
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="WSLicensing">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <fieldset>
                                        <legend class="text-center">Workstations</legend>
                                    </fieldset>
                                    <div class="row">
                                        <div style="max-height:800px;overflow-y:scroll;">
                                            <table id="WSPermission" class="table table-bordered table-hover white-bg table-condensed table-striped">
                                                <tbody>
                                                    @For Each workstation In Model.Workstations
                                                        @<tr>
                                                            <td data-wsid="@workstation.Value">@workstation.Key</td>
                                                        </tr>
                                                    Next
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <fieldset>
                                        <legend class="text-center">Apps Selected WS:<strong id="SelectedWS"></strong></legend>
                                    </fieldset>
                                    <div class="row">
                                        <button id="ClientCertCopy" class="btn btn-danger">Get Client Certificate Copy</button>
                                        <button id="deleteWorkstation" class="btn btn-danger">Delete Workstation</button>
                                        <table id="WSApp" class="table table-bordered table-hover white-bg table-condensed table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center">App Name</th>
                                                    <th class="text-center">Can Access?</th>
                                                    <th class="text-center">Default App?</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                @For Each PPApp In PickPro_Web.Config.AppLicenses
                                                    @<tr class="tr-disabled">
                                                        <td>@PPApp.Value.Info.DisplayName</td>
                                                        <td><input data-appname="@PPApp.Key" type="checkbox" disabled="disabled" /></td>
                                                        <td><input value="@PPApp.Key" type="radio" name="defaultapp" disabled="disabled" /></td>
                                                    </tr>
                                                Next
                                            </tbody>
                                        </table>
                                        <button id="clearDefaultApp" class="btn btn-primary pull-right">Clear Default App</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="STEService">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    @If STERunning Then
                                        @<legend class="text-center">STE Service <strong style="color:green;" id="STEStatus">Online</strong></legend>
                                    Else
                                        @<legend class="text-center">STE Service <strong style="color:red;" id="STEStatus">Offline</strong></legend>
                                    End If
                                    <div class="row">
                                        <div class="col-md-push-2 col-md-4">
                                            <button type="button" class="btn btn-block @(If(STERunning, "btn-danger", "btn-primary"))" id="STEServiceToggle">@(If(STERunning, "Stop STE Service", "Start STE Service"))</button>
                                        </div>
                                        <div class="col-md-push-2 col-md-4">
                                            <button type="button" @(If(STERunning, "", "disabled='disabled'")) class="btn btn-block btn-warning" id="STERestartService">Restart STE Service</button>
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
    <div class="modal fade" id="Config" tabindex="-1" role="dialog" aria-labelledby="ConfigLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ConfigLabel">Set Config Database</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="alert alert-danger" role="alert">
                                This is changing the config database connection. Improper values will cause issues with the website. Please close this pop up if you are not changing this.
                            </div>
                        </div>
                    </div>
                    <form id="dbConfigForm" method="post" action="/GlobalConfig/setConfigDB">
                        @Html.AntiForgeryToken()
                        <div class="form-group">
                            <label>Server Name</label>
                            <input type="text" name="ServerName" placeholder="Sever Name" class="form-control" value="" maxlength="50" />
                        </div>
                        <div class="form-group">
                            <label>Database Name</label>
                            <input type="text" name="DBName" placeholder="DB Name" class="form-control" value="" maxlength="50" />
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="CloseConfig" type="button" class="btn btn-default pull-left"data-dismiss="modal">Close</button>
                    <button id="SubmitConfig" type="button" disabled="disabled" class="btn btn-primary" onclick="changeConfigString()">Submit</button>
                </div>
            </div>
        </div>
    </div>
</div>
<link href="~/Content/toggles.css" rel="stylesheet" />
<link href="~/Content/toggles-modern.css" rel="stylesheet" />
<script src="~/Scripts/toggles.min.js"></script>

<script src="~/Scripts/GlobalConfig/Connections.js"></script>
<script src="~/Scripts/GlobalConfig/ConnectedUsers.js"></script>
<script src="~/Scripts/GlobalConfig/GlobalConfigPrinters.js"></script>
<script src="~/Scripts/GlobalConfig/GCPrintService.js"></script>
<script src="~/Scripts/GlobalConfig/GCCCSIF.js"></script>
<script src="~/Scripts/GlobalConfig/General.js"></script>
<script src="~/Scripts/GlobalConfig/GCTestPrint.js"></script>
<script src="~/Scripts/GlobalConfig/AppLicense.js"></script>
<script src="~/Scripts/GlobalConfig/STEService.js"></script>
@Html.Partial("~/Views/GlobalConfig/TestPrintServiceModal.vbhtml")
@Html.Partial("~/Views/GlobalConfig/SQLAuthModalPartial.vbhtml")