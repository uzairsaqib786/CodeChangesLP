<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Consolidation"
    ViewData("PageName") = "&nbsp; | &nbsp; Consolidation"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2 class="panel-title">Order Info</h2>
                </div>
                <div class="panel-body">
                    <div class="row" style="margin-bottom:15px;">
                        <div class="col-md-4">
                            <div class="row">
                                <div class="col-md-12" style="margin-bottom:10px;">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-md-4">Order Number/Tote ID</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <input maxlength="50" class="form-control" type="text" id="TypeValue" value="@Model.OrderNumber" autofocus />
                                                    <span class="input-group-btn">
                                                        <button id="NextOrder" class="btn btn-primary text-nowrap" disabled>Next Order</button>
                                                    </span>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-xs-6" style="margin-bottom:10px;">
                                    <div class="dropdown">
                                        <button id="goTo" class="btn btn-primary dropdown-toggle btn-block" data-toggle="dropdown" disabled><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a id="PrintPack" class="Print-Report">Print Packing List</a></li>
                                            <li><a id="PreviewPack">Preview Packing List</a></li>
                                            <li><a id="PrintNotVerified" class="Print-Report">Print Not Verified List</a></li>
                                            <li><a id="PreviewNotVerified">Preview Not Verified List</a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-4 col-xs-6" style="margin-bottom:10px;">
                                    <button class="btn btn-primary btn-block" id="StagingLocations" disabled>Staging Locations</button>
                                </div>
                                <div class="col-md-4 col-xs-6" style="margin-bottom:10px;">
                                    <button id="PackingButt" class="btn btn-primary btn-block" disabled>Packing</button>
                                </div>
                                <div class="col-md-4 col-xs-6" style="margin-bottom:10px;">
                                    <button id="ShippingButt" class="btn btn-primary btn-block" disabled>Shipping</button>
                                </div>
                                <div class="col-md-4 col-xs-6" style="margin-bottom:10px;">
                                    <button onclick="window.location.href= '/Transactions?app=CM&OrderStatusOrder=' + $('#TypeValue').val()" class="btn btn-primary btn-block">Order Status</button>
                                </div>
                                <div class="col-md-4 col-xs-6 bottom-spacer">
                                    <a href=".collapseOrderData" aria-expanded="true" data-toggle="collapse" class="btn btn-primary btn-block">Hide Order Data</a>
                                </div>
                            </div>
                            <div class="row" style="">
                                <div id="" class="col-md-12 collapse in collapseOrderData">
                                    <table class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                                        <thead>
                                            <tr>
                                                <th class="text-warning text-center col-md-4">Open Lines</th>
                                                <th class="text-success text-center col-md-4">Completed Lines</th>
                                                <th class="text-danger text-center col-md-4">Backorder Lines</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="text-warning text-center"><strong id="OpenCount">0</strong></td>
                                                <td class="text-success text-center"><strong id="CompletedCount">0</strong></td>
                                                <td class="text-danger text-center"><strong id="BackOrderCount">0</strong></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="row">
                                <div class="col-md-12 collapse in collapseOrderData">
                                    <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="ToteIDInfoTable">
                                        <thead>
                                            <tr>
                                                <th>Tote ID</th>
                                                <th>Status</th>
                                                <th>Location</th>
                                                <th>Staged By</th>
                                                <th>Stage Date</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Unverified Items
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-push-6 col-md-3 col-xs-6 bottom-spacer">
                                                    <select class="form-control" id="SelectFilter">
                                                        @code
                                                            Dim lookupTypeList = New List(Of KeyValuePair(Of String, String)) From {New KeyValuePair(Of String, String)("1", "Item Number"),
                                                                                                                                    New KeyValuePair(Of String, String)("2", "Supplier Item ID"),
                                                                                                                                    New KeyValuePair(Of String, String)("10", "Lot Number"),
                                                                                                                                    New KeyValuePair(Of String, String)("8", "Serial Number"),
                                                                                                                                    New KeyValuePair(Of String, String)("9", "User Field 1"),
                                                                                                                                    New KeyValuePair(Of String, String)("0", "Any Code"),
                                                                                                                                    New KeyValuePair(Of String, String)("6", "Tote ID")}
                                                            For Each Type As KeyValuePair(Of String, String) In lookupTypeList
                                                                If Model.lookup = Type.Value Then
                                                                    @<option value="@Type.Key" selected>@Type.Value</option>
                                                                Else
                                                                    @<option value="@Type.Key">@Type.Value</option>
                                                                End If
                                                            Next
                                                        End Code

                                                    </select>
                                                </div>
                                                <div class="col-md-3 col-md-push-6 col-xs-6 bottom-spacer">
                                                    <input class="form-control" type="text" id="FilterValue" maxlength="50" />
                                                </div>
                                                <div class="col-md-3 col-xs-12 col-md-pull-6 bottom-spacer">
                                                    <button id="VerifyAll" class="btn btn-primary btn-block" disabled>Verify All</button>
                                                </div>
                                            </div>
                                            <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="UnVerifiedTable">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Item Number</th>
                                                        <th>Supplier Item ID</th>
                                                        <th>Line #</th>
                                                        <th>Zone</th>
                                                        <th>Completed Qty</th>
                                                        <th>Tote ID</th>
                                                        <th>Status</th>
                                                        <th>Serial #</th>
                                                        <th>User Field 1</th>
                                                        <th>Lot #</th>
                                                    </tr>
                                                </thead>
                                                <tbody></tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Verified Items
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-3 bottom-spacer">
                                                    <button id="UnVerifyAll" class="btn btn-primary btn-block" disabled>UnVerify All</button>
                                                </div>
                                            </div>
                                            <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="VerifiedTable">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Item Number</th>
                                                        <th>Supplier Item ID</th>
                                                        <th>Line #</th>
                                                        <th>Zone</th>
                                                        <th>Completed Qty</th>
                                                        <th>Tote ID</th>
                                                        <th>Status</th>
                                                        <th>Serial #</th>
                                                        <th>User Field 1</th>
                                                        <th>Lot #</th>
                                                    </tr>
                                                </thead>
                                            </table>
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
</div>
<input type="hidden" id="VerifyItems" value="@Model.Preferences.VerifyItems" />
<input type="hidden" id="BlindVerify" value="@Model.Preferences.BlindVerifyItems" />
<input type="hidden" id="EmailSlip" value="@Model.Preferences.EmailPackingSlip" />
<input type="hidden" id="PackListSort" value="@Model.Preferences.PackingList" />

@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/ItemSelectedModalPartial.vbhtml")
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/OrderToteConflictModal.vbhtml")
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/StagingLocationModal.vbhtml", False)
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/CMPackPrintModal.vbhtml")
<script src="~/Areas/Consolidation Manager/Scripts/Consolidation/Consolidation.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>