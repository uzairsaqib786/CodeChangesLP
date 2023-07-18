<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row top-spacer">
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4 class="panel-title">Choose Batch</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <label>Batch ID</label>
                        <input type="text" class="form-control" id="BatchID" placeholder="Batch ID" value="@Model.topBatch" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-danger" id="DeleteBatch">Delete Batch</button>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-success" id="CompleteBatch">Complete Batch</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4 class="panel-title">Assign Transaction to Batch/Tote</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <label>Input Type</label>
                        <select id="InputType" class="form-control">
                            @For Each opt As String In {"Any", "Item Number", "Serial Number", "Lot Number", "Host Transaction ID", "Scan Code", "Supplier Item ID"}
                                If Model.preferences.PutScanType.ToString().ToLower() = opt.ToLower() Then
                                    If opt = "Item Number" Then
                                        @<option selected="selected" value="@opt">@Model.aliases.ItemNumber</option>
                                    Else
                                        @<option selected="selected" value="@opt">@opt</option>
                                    End If
                                Else
                                    If opt = "Item Number" Then
                                        @<option value="@opt">@Model.aliases.ItemNumber</option>
                                    Else
                                        @<option value="@opt">@opt</option>
                                    End If
                                End If
                            Next
                        </select>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <label data-toggle="tooltip" data-placement="right" data-original-title="@(model.aliases.ItemNumber)/Serial Number/Lot Number/Host Transaction ID/Scan Code/Supplier Item ID">Input Value</label>
                        <input type="text" class="form-control" maxlength="50" id="Item" placeholder="@(Model.aliases.ItemNumber)/Serial Number" />
                        <input type="hidden" id="StripLength" value="@Model.preferences.StripNum" />
                        <input type="hidden" id="StripSide" value="@Model.preferences.StripSide" />
                        <input type="hidden" id="ApplyStrip" value="@Model.preferences.StripScan.ToString()" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <button disabled="disabled" type="button" class="btn btn-primary" id="AssignTrans">Assign Transaction to Selected Tote</button>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-6">
                        <label>Next Put to Location</label>
                        <input type="text" class="form-control" id="NextLoc" readonly="readonly" />
                    </div>
                    <div class="col-md-3">
                        <label>Position</label>
                        <input type="text" class="form-control" id="NextPosition" readonly="readonly" />
                    </div>
                    <div class="col-md-3">
                        <label>Next Cell</label>
                        <input type="text" class="form-control" id="NextCell"  readonly="readonly" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-4">
                        <button type="button" id="GoTo" class="btn btn-primary">Go To Next</button>
                    </div>
                </div>
                <div class="row" style="margin-top:10px">
                    <div class="col-md-6">
                        <label>Select Position</label>
                        <input type="number" class="form-control" id="SelectPosition" placeholder="Position" />
                    </div>
                    <div class="col-md-6">
                        <label>Select Tote</label>
                        <input type="text" class="form-control" id="SelectTote" placeholder="Tote ID" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary" id="GoSelected">Select</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-9">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4 class="panel-title">Totes</h4>
            </div>
            <div class="panel-body" id="TotePanel">
                <div class="row" style="padding-bottom:10px;">
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary btn-block" id="ViewToteTransInfo" disabled="disabled">View Tote Contents</button>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary btn-block" id="MarkToteFullButt" disabled="disabled">Mark Tote as Full</button>
                    </div>
                    <div class="col-md-8">
                        <button type="button" class="btn btn-primary Print-Report" data-toggle="tooltip" data-original-title="Print Off Carousel Put Away List" data-placement="right" id="PrintOffCar" disabled="disabled"><span class="glyphicon glyphicon-print"></span></button>
                    </div>
                </div>
                <table class="table table-hover table-condensed table-bordered table-striped" style="background:white;" id="TotesTable">
                    <thead>
                        <tr>
                            <th>Position</th>
                            <th>Tote ID</th>
                            <th>Tote Capacity</th>
                            <th>Current Qty</th>
                            <th>Zone To Put Away</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>
@Html.partial("~/Views/ModalPartials/WarehouseModalPartial.vbhtml")
@Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/TransactionPartial.vbhtml")
@Html.partial("~/Areas/Induction/Views/Modal/DeleteBatchModalPartial.vbhtml")
@Html.partial("~/Views/ModalPartials/CellSizeModalPartial.vbhtml")
@Html.partial("~/Views/ModalPartials/VelocityCodeModalPartial.vbhtml")
@html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/LocationAssignmentModal.vbhtml")
@Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/CrossDock/CrossDockModal.vbhtml")
@Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/RPDetailModal.vbhtml")
@Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/DedicatedModal.vbhtml")
@Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/Reels/ReelsPartial.vbhtml")
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/Process.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/FilterMenu/MultiLinePaging.js"></script>
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/Modals/ModalNavigation.js"></script>

<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/Modals/DataTable.js"></script>
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/Modals/PutAwayDetail.js"></script>

<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/Modals/LocationModal.js"></script>
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/Modals/DedicatedContainers.js"></script>

<script src="~/Scripts/FilterMenu/jquery.ui.position.js"></script>
<script src="~/Scripts/FilterMenu/jquery.contextMenu.js"></script>
<script src="~/Scripts/FilterMenu/FilterMenuNonTable.js"></script>
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/Filters.js"></script>