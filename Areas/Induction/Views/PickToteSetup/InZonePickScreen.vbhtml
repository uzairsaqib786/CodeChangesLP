<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype object
@Code
    ViewData("Title") = "Pick Tote Setup"
    ViewData("PageName") = "| Pick Tote Setup"

    Dim ZoneString As String = "Zones: "

    For Each Zne In Model.WSZones
        ZoneString = ZoneString & Zne & " "
    Next

End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                Batch Setup
                            </h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Batch ID</label>
                                    <input type="text" maxlength="50" id="PickBatchID" placeholder="Batch ID" class="form-control" />
                                </div>
                                <div class="col-md-3" style="padding-top:2.5%">
                                    <button type="button" class="btn btn-primary btn-block" id="NewPickBatchID">New Batch with ID</button>
                                </div>
                                <div class="col-md-3" style="padding-top:2.5%">
                                    <button type="button" class="btn btn-primary btn-block" id="NewPickBatch">New Batch</button>
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-3">
                                    <div class="dropdown">
                                        <button id="goTo" class="btn btn-primary dropdown-toggle btn-block" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a id="PrintToteLabs" class="Print-Label">Print Tote/Location Labels</a></li>
                                            <li><a id="PrintPickLabels" class="Print-Label">Print Pick Labels</a></li>
                                            <li><a id="PrintPickList" class="Print-Report">Print Pick List</a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <button type="button" class="btn btn-success btn-block" id="PickProcess">Process</button>
                                </div>
                                <div class="col-md-3">
                                    <button type="button" class="btn btn-primary btn-block" id="BlossomTote">Blossom</button>
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
                            <div class="row">
                                <div class="col-md-6 text-left panel-title">
                                    Order Number Select
                                </div>
                                <div class="col-md-6 text-right panel-title" id="ZonesPanelTitle">
                                    @ZoneString
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <button type="button" class="btn btn-primary btn-block" id="SelectWSZones">Select Workstation Zones</button>
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-4">
                                    <label>Order Number</label>
                                    <input type="text" maxlength="50" id="OrderNumberTA" placeholder="Order Number" class="form-control" />
                                </div>
                                <div class="col-md-2" style="padding-top:2.5%">
                                    <button type="button" class="btn btn-primary btn-block" id="ViewAllOrders">View All Orders</button>
                                </div>
                                <div class="col-md-3" style="padding-top:2.5%">
                                    <button type="button" class="btn btn-primary btn-block" id="ViewNonReplenOrders">View Non Replen Orders</button>
                                </div>
                                <div class="col-md-3" style="padding-top:2.5%">
                                    <button type="button" class="btn btn-primary btn-block" id="ViewReplenOrders">View Replen Orders</button>
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
                            <h4 class="panel-title">
                                Print Existing Pick Batches
                            </h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-9">
                                    <label>Batch ID</label>
                                    <input type="text" maxlength="50" id="BatchPickIDTA" placeholder="Batch Pick ID" class="form-control" />
                                </div>
                                <div class="col-md-3" style="padding-top:2.5%">
                                    <div class="dropdown">
                                        <button id="goTo" class="btn btn-primary dropdown-toggle btn-block" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a id="PrintBatchLocLabel" class="Print-Label">Print Tote/Location Label</a></li>
                                            <li><a id="PrintBatchPickLabel" class="Print-Label">Print Pick Label</a></li>
                                            <li><a id="PrintBatchPickList" class="Print-Report">Print Pick List</a></li>
                                            <li><a id="PrintCaseLabel" class="Print-Report">Print Case label</a></li>
                                            <li><a id="PrintPickBatchList" class="Print-Report">Print Pick Batch List</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Tote Setup
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <button type="button" class="btn btn-primary btn-block" id="FillAllToteID">Fill All Empty Tote IDs</button>
                        </div>
                        <div class="col-md-3">
                            <button type="button" class="btn btn-primary btn-block" id="FillNextToteID">Fill Next Empty Tote ID</button>
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-1">
                            <label>Position</label>
                        </div>
                        <div class="col-md-3">
                            <label>Tote ID</label>
                        </div>
                        <div class="col-md-3">
                            <label>Order Number</label>
                        </div>
                        <div class="col-md-5">
                            <button id="ClearAllOrders" class="btn btn-danger">Clear All Orders</button>
                            <button id="ClearAllTotes" class="btn btn-danger">Clear All Totes</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" id="PickBatchContainer">

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="text" hidden value="@Model.Prefs.PickBatchQuant" id="BatchQty" />
    <input type="text" hidden value="" id="InZonePref" />
    <input type="text" hidden value="@Model.Prefs.AutoPickTote.ToString().ToLower()" id="AutoPickTote" />
    <input type="text" hidden value="@Model.Prefs.PrintDirect.ToString().ToLower()" id="PrintDirect" />
    <input type="text" hidden value="@Model.Prefs.AutoPrintPickTote.ToString.ToLower" id="AutoPrintPickToteLabs" />
    <input type="text" hidden value="@Model.Prefs.AutoPrintOffCarPickList.ToString.ToLower" id="AutoPrintOffCarPickList" />
    <input type="text" hidden value="@Model.Prefs.AutoPrintCaseLabel.ToString.ToLower" id="AutoPrintCaseLabel" />
    <input type="text" hidden value="@Model.Prefs.AutoPrintPickBatchList.ToString.ToLower" id="AutoPrintPickBatchList" />
</div>
<script src="~/Areas/Induction/Scripts/PickToteSetup/InZonePickScreen.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
@Html.Partial("~/Areas/Induction/Views/PickToteSetup/InZoneOrdersPartial.vbhtml")
@Html.Partial("~/Areas/Induction/Views/PickToteSetup/BlossomPartial.vbhtml")
@Html.Partial("~/Areas/Induction/Views/PickToteSetup/WorkstationZonesPartial.vbhtml")