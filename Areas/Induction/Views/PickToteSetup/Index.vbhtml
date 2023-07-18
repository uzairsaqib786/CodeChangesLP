<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype object
@Code
    ViewData("Title") = "Pick Tote Setup"
    ViewData("PageName") = "| Pick Tote Setup"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-5">
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
                                <div class="col-md-3" style="padding-top:22px">
                                    <button type="button" class="btn btn-primary btn-block" id="NewPickBatchID">New Batch with ID</button>
                                </div>
                                <div class="col-md-3" style="padding-top:22px">
                                    <button type="button" class="btn btn-primary btn-block" id="NewPickBatch">New Batch</button>
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-3">
                                    <button type="button" class="btn btn-primary btn-block" id="PickBatchManager">Pick Batch Manager</button>
                                </div>
                                <div class="col-md-3">
                                    <button type="button" class="btn btn-primary btn-block" id="RefreshOrderCounts">Refresh Order Counts</button>
                                </div>
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
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-3">
                                    <input type="radio" name="pickzone" id="MixedZonesOpt" checked value="MixedZones" /> <strong> Mixed Zones</strong>
                                </div>
                                <div class="col-md-3">
                                    <input type="text" id="MixedZonesCount" disabled="disabled" class="form-control" value="@Model.CountInfo.Mixed" />
                                </div>
                                <div class="col-md-3">
                                    <button type="button" class="btn btn-primary btn-block" id="FillAllToteID">Fill All Empty Tote IDs</button>
                                </div>
                                <div class="col-md-3">
                                    <button type="button" class="btn btn-primary btn-block" id="FillNextToteID">Fill Next Empty Tote ID</button>
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-3">
                                    <input type="radio" name="pickzone" id="CarouselOpt" value="Carousel" /> <strong> Carousel</strong>
                                </div>
                                <div class="col-md-3">
                                    <input type="text" id="CarouselCount" disabled="disabled" class="form-control" value="@Model.CountInfo.Car" />
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-3">
                                    <input type="radio" name="pickzone" id="OffCarouselOpt" value="OffCarousel" /> <strong> Off-Carousel</strong>
                                </div>
                                <div class="col-md-3">
                                    <input type="text" id="OffCarouselCount" disabled="disabled" class="form-control" value="@Model.CountInfo.OffCar" />
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
                                    <select class="form-control" id="PrintPickBatchInput">
                                        <option value=""></option>
                                        @for Each BatchData In Model.PickBatches
                                            @<option value="@BatchData("Batch Pick ID") ">@BatchData("Batch Pick ID")</option>
                                        Next
                                    </select>
                                </div>
                                <div class="col-md-3">
                                    <div class="dropdown">
                                        <button id="goTo" class="btn btn-primary dropdown-toggle btn-block" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a id="PrintBatchLocLabel" class="Print-Label">Print Tote/Location Label</a></li>
                                            <li><a id="PrintBatchPickLabel" class="Print-Label">Print Pick Label</a></li>
                                            <li><a id="PrintBatchPickList" class="Print-Report">Print Pick List</a></li>
                                            <li><a id="PrintCaseLabel" class="Print-Report">Print Case Label</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-7">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Tote Setup
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-1">
                            <label>Position</label>
                        </div>
                        <div class="col-md-3">
                            <label>Tote ID</label>
                        </div>
                        <div class="col-md-3">
                            <label>Order Number</label>
                        </div>
                        <div class="col-md-1">
                            <label>Priority</label>
                        </div>
                        <div class="col-md-4">
                            <button id="ClearAllOrders" class="btn btn-danger" data-toggle="tooltip" title="Clear All Orders"><span class="glyphicon glyphicon-remove"></span></button>
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
    <!--Insert Preferences Here-->
    <input type="text" hidden value="@Model.Prefs.PickBatchQuant" id="BatchQty" />
    <input type="text" hidden value="@Model.Prefs.AutoPickTote.ToString().ToLower()" id="AutoPickTote" />
    <input type="text" hidden value="@Model.Prefs.AutoPickOrderSel.ToString().ToLower()" id="AutoPickOrder" />
    <input type="text" hidden value="@Model.Prefs.UseBatchMan.ToString().ToLower()" id="UsePickBatchManager" />
    <input type="text" hidden value="@Model.Prefs.PrintDirect.ToString().ToLower()" id="PrintDirect" />
    <input type="text" hidden value="@Model.Prefs.UseDefFilter.ToString().ToLower" id="DefFilter" />
    <input type="text" hidden value="@Model.Prefs.UseDefZone.ToString().ToLower()" id="DefZone" />
    <input type="text" hidden value="@Model.Prefs.AutoPrintPickTote.ToString.ToLower" id="AutoPrintPickToteLabs" />
    <input type="text" hidden value="@Model.Prefs.AutoPrintOffCarPickList.ToString.ToLower" id="AutoPrintOffCarPickList" />
    <input type="text" hidden value="@Model.Prefs.AutoPrintCaseLabel.ToString.ToLower" id="AutoPrintCaseLabel" />
</div>
<script src="~/Areas/Induction/Scripts/PickToteSetup/PickToteSetup.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
@Html.Partial("~/Areas/Induction/Views/PickToteSetup/PickToteManagerPartial.vbhtml")