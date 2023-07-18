<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "ShippingTransactions"
    ViewData("PageName") = "&nbsp; | &nbsp; Shipping Transactions"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2 class="panel-title">Packing</h2>
                </div>
                <div class="panel-body">
                    <div class="row" style="margin-bottom:10px;">
                        <div class="col-md-4">
                            <label>Order Number</label><input maxlength="50" type="text" id="ShipOrderNumber" class="form-control" disabled="disabled" value="@Model.orderNumber" />
                        </div>
                        <div class="col-md-4" style="padding-top:25px;">
                            <button id="ShipPrintList" class="btn btn-primary btn-block Print-Report">Print List</button>
                        </div>
                        <div class="col-md-4" style="padding-top:25px;">
                            <button id="ShipCompletePacking" class="btn btn-primary btn-block">Complete Packing</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Tote ID to Update</label><input maxlength="50" type="text" id="ShipUpdateToteID" class="form-control" />
                        </div>
                        <div class="col-md-2" style="padding-top:25px;">
                            <button id="ToteIDButt" class="btn btn-primary form-control">Update ToteID</button>
                        </div>
                        <div class="col-md-6">
                            <label>Find Item Number</label>
                            <input maxlength="50" type="text" id="ShipItemNumber" class="form-control" />
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
                    <h2 class="panel-title">Transactions</h2>
                </div>
                <div class="panel-body">
                    <div class="row" style="margin-bottom:10px;">
                        <div class="col-md-3">
                            <button id="ShipSplitLine" disabled="disabled" class="btn btn-primary btn-block">Split Line</button>
                        </div>
                        <div class="col-md-3">
                            <button id="ShipPrintItemLabel" disabled="disabled" class="btn btn-primary btn-block Print-Report">Print Item Label</button>
                        </div>
                        <div class="col-md-3">
                            <button id="ShipEditContainerID" disabled="disabled" class="btn btn-primary btn-block">Edit Container ID</button>
                        </div>
                        <div class="col-md-3">
                            <button id="ShipEditQuantity" disabled="disabled" class="btn btn-primary btn-block">Edit Ship Quantity</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="ShipTtansactionsTable">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Item Number</th>
                                        <th>Line Number</th>
                                        <th>Tote ID</th>
                                        <th>Order Qty</th>
                                        <th>Picked Qty</th>
                                        <th>Container ID</th>
                                        <th>Ship Quantity</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    @For Each tr As List(Of String) In Model.tableData
                                        @<tr>
                                            @For Each td As String In tr
                                                @<td>@td</td>
                                            Next
                                        </tr>
                                    Next
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/SplitLineModal.vbhtml")
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/AdjustShipQuantityModal.vbhtml")
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/EditContainerIDModal.vbhtml")
@Html.Partial("~/Areas/Consolidation Manager/Views/ShippingTransactions/ToteIDUpdateModal.vbhtml")
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/Consolidation Manager/Scripts/ShippingTransactions/ShippingTransactions.js"></script>
