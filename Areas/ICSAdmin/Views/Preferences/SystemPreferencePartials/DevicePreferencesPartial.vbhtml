<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row hide">
    <div class="col-md-2">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Interface Status <span id="InterfaceStatus" class="label label-danger">Inactive</span>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <button type="button" class="btn btn-primary btn-block btn-sm" id="ToggleInterface">Start Interface</button>
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
                        <h3 class="panel-title">
                            Zone Filter
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <input type="text" class="form-control input-sm" placeholder="Zone Filter" id="ZoneFilter" maxlength="2" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <button type="button" class="btn btn-primary btn-sm btn-block" id="ClearZone" data-toggle="tooltip" data-placement="top" data-original-title="Clear Zone Filter">View All</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Carousel Lights
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>POD</label>
                        <input type="text" class="form-control input-sm"  id="Dev_POD" disabled="disabled" />
                    </div>
                    <div class="col-md-4">
                        <label>Carousel</label>
                        <input type="text" class="form-control input-sm" placeholder="Carousel" id="Dev_Carousel" value="1" maxlength="1" />
                    </div>
                    <div class="col-md-4">
                        <label>Bin</label>
                        <input type="text" class="form-control input-sm" placeholder="Bin" id="Dev_Bin" value="001" maxlength="3" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Shelf</label>
                        <input type="text" class="form-control input-sm" placeholder="Shelf" id="Dev_Shelf" value="02" maxlength="2" />
                    </div>
                    <div class="col-md-4">
                        <label>Cell</label>
                        <input type="text" class="form-control input-sm" placeholder="Cell" id="Dev_Cell" value="001" maxlength="3" />
                    </div>
                    <div class="col-md-4">
                        <label>Qty</label>
                        <input type="text" class="form-control input-sm" placeholder="Quantity" id="Dev_Qty" maxlength="5" value="9999" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>@Model.aliases.UoM</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <input type="text" class="form-control input-sm" placeholder="@Model.aliases.UoM" id="Dev_UoM" value="EA" maxlength="50" />
                    </div>
                    <div class="col-md-8">
                        <button id="Dev_SendCarousel" type="button" class="btn btn-primary btn-block btn-sm" data-toggle="tooltip" data-placement="top" data-original-title="Send Carousel Light Commands">Send Light Commands</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Carton Flow Lights
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>CF Zone</label>
                        <select class="form-control input-sm" id="Dev_CF"></select>
                    </div>
                    <div class="col-md-6">
                        <label>Device Number</label>
                        <input type="text" class="form-control input-sm" placeholder="Device #" id="Dev_DeviceNumber" value="0A1" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Cell</label>
                        <input type="text" class="form-control input-sm" placeholder="Cell" id="Dev_CellCF" maxlength="50" value="0A1" />
                    </div>
                    <div class="col-md-6">
                        <label>Light Address</label>
                        <input type="text" class="form-control input-sm" placeholder="Light Address" id="Dev_Light" value="12" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Qty</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <input type="text" class="form-control input-sm" placeholder="Quantity" id="Dev_QtyCF" value="9999" maxlength="5" />
                    </div>
                    <div class="col-md-6">
                        <button data-toggle="tooltip" data-placement="top" data-original-title="Send Carton Flow Light Commands" type="button" class="btn btn-primary btn-block btn-sm" id="Dev_SendLightCF">Send Light Commands</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Sort Bar Lights
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Tote Position</label>
                        <input type="text" class="form-control input-sm" placeholder="Tote Position" id="Dev_Tote" value="1" maxlength="5" />
                    </div>
                    <div class="col-md-6">
                        <label>Qty</label>
                        <input type="text" class="form-control input-sm" placeholder="Quantity" id="Dev_QtySB" value="9999" maxlength="5" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <button id="Dev_SendTote" type="button" class="btn btn-primary btn-block btn-sm" data-toggle="tooltip" data-placement="top" data-original-title="Send Tote Position Command">Send Tote Position</button>
                    </div>
                    <div class="col-md-6">
                        <button id="Dev_SendSB" data-toggle="tooltip" data-placement="top" data-original-title="Send Light Commands to All Sort Bar Positions" type="button" class="btn btn-primary btn-block btn-sm">Send All Sort Bar Positions</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Carousel</label>
                        <input type="text" class="form-control input-sm" placeholder="Carousel" id="Dev_CarouselSpin" value="1" maxlength="1" />
                    </div>
                    <div class="col-md-6">
                        <label>Bin</label>
                        <input type="text" class="form-control input-sm" placeholder="Bin" id="Dev_BinSpin" maxlength="3" value="001" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <button data-toggle="tooltip" data-placement="top" data-original-title="Spin to Carousel Bin" type="button" class="btn btn-primary btn-block btn-sm" id="Dev_SpinToBin">Send Spin Command</button>
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
                <h3 class="panel-title">
                    Device Preferences
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <button id="AddNewDevice" type="button" class="btn btn-primary btn-sm" data-toggle="tooltip" data-placement="top" data-original-title="Add New Device"><span class="glyphicon glyphicon-plus"></span></button>
                        <button id="DeleteSelectedDevice" disabled="disabled" type="button" class="btn btn-danger btn-sm" data-toggle="tooltip" data-placement="top" data-original-title="Delete Selected Device"><span class="glyphicon glyphicon-trash"></span></button>
                        <button id="EditSelectedDevice" disabled="disabled" type="button" class="btn btn-primary btn-sm" data-toggle="tooltip" data-placement="top" data-original-title="Edit Selected Device">Edit Selected Device <span class="glyphicon glyphicon-resize-full"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="DevPrefsTable">
                            <thead>
                                <tr>
                                    <th>Zone</th>
                                    <th>Device Type</th>
                                    <th>Device #</th>
                                    <th>Device Model</th>
                                    <th>Controller Type</th>
                                    <th>Controller Term Port</th>
                                    <th>Arrow Direction</th>
                                    <th>Light Direction</th>
                                    <th>Use Laser Pointer</th>
                                    <th>Use Light Tree #</th>
                                    <th>First Address</th>
                                    <th>Positions</th>
                                    <th>Display Characters</th>
                                    <th>Device ID</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/DevicePreferences/DevicePreferencesModalPartial.vbhtml")
@Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/DevicePreferences/DevicePreferencesTrayTiltModalPartial.vbhtml")