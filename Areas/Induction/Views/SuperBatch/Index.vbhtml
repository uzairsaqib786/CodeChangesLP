<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Super Batch"
    ViewData("PageName") = "| Super Batch"
End Code
<div class="container-fluid">
    <input type="hidden" id="AutoPrintBatchLabel" value="@Model.Preferences.AutoPrintPickTote.ToString().Trim().ToLower()" />
    <input type="hidden" id="AutoPrintOrderLabels" value="@Model.Preferences.AutoPrintPickLabels.ToString().ToLower()" />
    <input type="hidden" id="PrintDirect" value="@Model.Preferences.PrintDirect.ToString().Trim().ToLower()" />
    <input type="hidden" id="BatchSizePref" value="@Model.Preferences.DefaultSuperBatch" />
    <input type="text" hidden value="@Model.Preferences.AutoPrintCaseLabel.ToString.ToLower" id="AutoPrintCaseLabel" />

    <div class="panel panel-info">
        <div class="panel-heading">
            <h3 class="panel-title">Super Batch</h3>
        </div>
        <div class="row" style="margin-top:10px;">
            <div class="col-md-2" style="margin-left:10px">
                <button type="button" class="btn btn-block btn-success" id="SBReqDateStatusButt">Required Date Status</button>
            </div>
        </div>
        <div class="row" style="margin-top:10px;">
            <div class="col-md-3" style="margin-left:10px">
                <label>Super Batch By: </label>
                <label class="radio-inline">
                    <input type="radio" id="orderNumFilter" name="BatchFilter" checked="checked" />
                    Order Number
                </label>
                <label class="radio-inline">
                    <input type="radio" name="BatchFilter" id="toteFilter" />
                    Tote ID
                </label>
                <label class="radio-inline">
                    <input type="radio" name="BatchFilter" id="ItemNumFilter" />
                    Item Number
                </label>
            </div>
        </div>
        <div class="panel-body">
            <div id="displayBatchByOrderNum">
                <div class="row">
                    <div class="col-md-2">
                        <label style="width:100%" class="text-center">Zone</label>
                    </div>
                    <div class="col-md-2">
                        <label id="orderNumLabel" style="width:100%;" class="text-center">Single Line Orders</label>
                    </div>
                    <div class="col-md-3">
                        <label style="width:100%" class="text-center">Orders to Batch</label>
                    </div>
                    <div class="col-md-3">
                        <label style="width:100%" class="text-center">New Tote ID</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="BatchOrderContainer">
                        @For Each ZoneData In Model.BatchZones
                            @<div class="row top-spacer">
                                <div Class="col-md-2">
                                    <input name="Zone" Class="form-control text-center" type="text" disabled="disabled" ReadOnly value="@ZoneData("Zone")" />
                                </div>
                                <div Class="col-md-2">
                                    <input name="SingleLineOrders" Class="form-control text-center" type="text" disabled="disabled" ReadOnly value="@ZoneData("Total Transactions")" />
                                </div>
                                <div Class="col-md-3">
                                    <input name="BatchSize" Class="form-control text-center" type="number" min="2" value="@Model.Preferences.DefaultSuperBatch" />
                                </div>
                                <div Class="col-md-3">
                                    <input name="ToteID" Class="form-control text-center ToteID" type="text" value="" />
                                </div>
                                <div Class="col-md-2">
                                    <Button name="BatchOrders" Class="btn btn-primary btn-block BatchOrders">Batch Orders</Button>
                                </div>
                            </div>
                        Next
                    </div>
                </div>
            </div>
            <div style="display:none" id="displayBatchByTote">
                <div class="row">
                    <div class="col-md-2">
                        <label style="width:100%" class="text-center">Zone</label>
                    </div>
                    <div class="col-md-2">
                        <label id="toteLabel" style="width:100%; display:none" class="text-center">Single Line Tote Orders</label>
                    </div>
                    <div class="col-md-3">
                        <label style="width:100%" class="text-center">Orders to Batch</label>
                    </div>
                    <div class="col-md-3">
                        <label style="width:100%" class="text-center">New Tote ID</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="BatchToteContainer">
                        @For Each ToteZoneData In Model.BatchZonesByTote
                            @<div class="row top-spacer">
                                <div Class="col-md-2">
                                    <input name="Zone" Class="form-control text-center" type="text" disabled="disabled" ReadOnly value="@ToteZoneData("Zone")" />
                                </div>
                                <div Class="col-md-2">
                                    <input name="SingleLineOrders" Class="form-control text-center" type="text" disabled="disabled" ReadOnly value="@ToteZoneData("Total Transactions")" />
                                </div>
                                <div Class="col-md-3">
                                    <input name="BatchSize" Class="form-control text-center" type="number" min="2" value="@Model.Preferences.DefaultSuperBatch" />
                                </div>
                                <div Class="col-md-3">
                                    <input name="ToteID" Class="form-control text-center ToteID" type="text" value="" />
                                </div>
                                <div Class="col-md-2">
                                    <Button name="BatchOrders" Class="btn btn-primary btn-block BatchOrders">Batch Orders</Button>
                                </div>
                            </div>
                        Next
                    </div>
                </div>
            </div>
            <div style="display:none" id="displayBatchByItemNum">
                <div class="row">
                    <div class="col-md-2">
                        <label style="width:100%" class="text-center">Item Number</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <select class="form-control" id="BatchItemNum">
                            <option value=""></option>
                            @For Each ItemData In Model.ItemNums
                                @<option value="@ItemData("Item Number") ">@ItemData("Item Number") (@ItemData("Item Count"))</option>
                            Next
                        </select>
                    </div>
                </div>
                <div Class="row top-spacer">
                    <div Class="col-md-2">
                        <Label style="width:100%" Class="text-center">Zone</Label>
                    </div>
                    <div Class="col-md-2">
                        <Label id="ItemNumLabel" style="width:100%;" Class="text-center">Single Line Orders</Label>
                    </div>
                    <div Class="col-md-3">
                        <Label style="width:100%" Class="text-center">Orders To Batch</Label>
                    </div>
                    <div Class="col-md-3">
                        <Label style="width:100%" Class="text-center">New Tote ID</Label>
                    </div>
                </div>
                <div Class="row">
                    <div Class="col-md-12" id="BatchItemNumContainer">

                    </div>
                </div>
            </div>
            <div class="row top-spacer">
                <div class="col-md-12">
                    <label>Print Labels for Batch</label>
                    <div class="row">
                        <div class="col-md-3">
                            <select class="form-control" id="PrintBatchInput">
                                <option value=""></option>
                                @for Each BatchData In Model.SuperBatches
                                    @<option value="@BatchData("Tote ID") ">@BatchData("Tote ID")</option>
                                Next
                            </select>
                        </div>
                        <div class="col-md-3">
                            <button class="btn btn-primary form-control" id="PrintBatchButton">Print Batch Label</button>
                        </div>
                        <div class="col-md-3">
                            <button class="btn btn-primary form-control" id="PrintOrderButton">Print Order Labels</button>
                        </div>
                        <div class="col-md-3">
                            <button class="btn btn-primary form-control" id="PrintCaseButton">Print Case Labels</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<input id="ConfirmSuperBatch" type="hidden" value="@Model.Preferences.ConfirmSuperBatch.ToString" />
<input id="DefaultSBFilterTote" type="hidden" value="@Model.Preferences.SBByToteID.ToString" />
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/Induction/Scripts/SuperBatch/SuperBatch.js"></script>
@Html.Partial("~/Areas/Induction/Views/SuperBatch/SuperBatchRequiredDateModal.vbhtml")
