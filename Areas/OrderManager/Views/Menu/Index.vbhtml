<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Menu"
    ViewData("PageName") = "&nbsp; | &nbsp; Menu"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
<!--Import PickPro_Web-->
<!--Change Buttons below to a tags like: <a style="margin-bottom:30px;" href="Consolidation/Consolidation" type="button" class="btn btn-xl btn-block btn-primary">Consolidation</a>-->
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h2 class="panel-title text-center" style="font-size:20px;">Order Manager Menu</h2>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 bottom-spacer">
                                    @If permissions.Contains("Admin Release Orders") Then
                                        @<a href="/OM/OrderManager" type="button" class="btn btn-xl btn-block btn-primary">Order Manager</a>
                                    Else
                                        @<button disabled class="btn btn-xl btn-block btn-primary">Order Manager</button>
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 bottom-spacer">
                                    <a href="/Transactions?App=OM" class="btn btn-primary btn-xl btn-block">Order Status</a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 bottom-spacer">
                                    @If permissions.Contains("Admin Release Orders") Then
                                        @<a href="/EventLog?App=OM" type="button" class="btn btn-xl btn-block btn-primary">Event Log</a>
                                    Else
                                        @<button class="btn btn-xl btn-block btn-primary" disabled>Event Log</button>
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 bottom-spacer">
                                    @If permissions.Contains("Admin Inventory Master") Then
                                        @<a id="InventoryMaster" href="/InventoryMaster?App=OM" class="btn btn-xl btn-primary btn-block">Inventory Master Info</a>
                                    Else
                                        @<button id="InventoryMaster" disabled class="btn btn-xl btn-primary btn-block">Inventory Master Info</button>
                                    End If
                                </div>
                                <div class="col-md-6 bottom-spacer">
                                    @If permissions.Contains("Admin Stock Locations") Then
                                         @<a id="InventoryMap" href="/InventoryMap?App=OM" class="btn btn-xl btn-primary btn-block">Stock Location & Quantity</a>
                                    Else
                                         @<button id="InventoryMap" disabled class="btn btn-xl btn-primary btn-block">Stock Location & Quantity</button>
                                    End If
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 bottom-spacer">
                                    @If permissions.Contains("Admin Reports") Then
                                        @<a href="/CustomReports?App=OM" class="btn btn-xl btn-primary btn-block">Reports</a>
                                    Else
                                        @<button disabled class="btn btn-xl btn-primary btn-block">Reports</button>
                                    End If
                                </div>
                                <div class="col-md-6 bottom-spacer">
                                    @If permissions.Contains("Admin Preferences") Then
                                        @<a href="/OM/Preferences" type="button" class="btn btn-xl btn-block btn-primary">Preferences</a>
                                    Else
                                        @<button disabled class="btn btn-xl btn-block btn-primary">Preferences</button>
                                    End If
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6" >
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h2 class="panel-title text-center" style="font-size:20px;">Transaction Count Information</h2>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button id="OMRefreshStat" class="btn btn-primary btn-block">Refresh Status Info</button>
                                </div>
                            </div>
                            <div class="row top-spacer" >
                                <div class="col-md-6">
                                    <strong>Number of Open Picks: </strong><input type="text" id="OMOpenPicks" class="form-control" disabled="disabled" value="@Model.CountData.OpenPicks" />
                                </div>
                                <div class="col-md-6">
                                    <strong>Completed Picks to Export: </strong><input type="text" id="OMCompPicks" class="form-control" disabled="disabled" value="@Model.CountData.CompPicks" />
                                </div>
                            </div>
                            <div class="row top-spacer" >
                                <div class="col-md-6">
                                    <strong>Number of Open Put Aways: </strong><input type="text" id="OMOpenPuts" class="form-control" disabled="disabled" value="@Model.CountData.OpenPuts" />
                                </div>
                                <div class="col-md-6">
                                    <strong>Closed Put Aways to Export: </strong><input type="text" id="OMCompPuts" class="form-control" disabled="disabled" value="@Model.CountData.CompPuts" />
                                </div>
                            </div>
                            <div class="row top-spacer" >
                                <div class="col-md-6">
                                    <strong>Number of Open Counts: </strong><input type="text" id="OMOpenCounts" class="form-control" disabled="disabled" value="@Model.CountData.OpenCounts" />
                                </div>
                                <div class="col-md-6">
                                    <strong>Closed Counts to Export: </strong><input type="text" id="OMCompCounts" class="form-control" disabled="disabled" value="@Model.CountData.CompCounts" />
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-6">
                                    <strong>Number of Adjustments to Export: </strong><input type="text" id="OMCompAdjust" class="form-control" disabled="disabled" value="@Model.CountData.CompAdjust" />
                                </div>
                                <div class="col-md-6">
                                    <strong>Number of Location Changes to Export: </strong><input type="text" id="OMCompLocChange" class="form-control" disabled="disabled" value="@Model.CountData.CompLocChange" />
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-12">
                                    <strong>Transactionsto be Re-Processed: </strong><input type="text" id="OMReprocCount" class="form-control" disabled="disabled" value="@Model.CountData.ReprocCount" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/OrderManager/Scripts/Menu/Menu.js"></script>
