<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
    Dim seeOrderStatus As Boolean = permissions.IndexOf("Order Status") > -1
End Code
<div class="row" id="mainDiv">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Batch Manager Controls
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Transaction Type</label>
                        <select class="form-control" id="TransType">
                            <option selected>Pick</option>
                            <option>Put Away</option>
                            <option>Count</option>
                        </select>
                    </div>
                    <div class="col-md-4">
                        <label>Current Pick Mode</label>
                        <input type="text" disabled="disabled" class="form-control" id="currentPickMode" value="@Model.BMSettings(1)" maxlength="50" />
                    </div>
                    <div class="col-md-4">
                        <label>Delete Batch</label>
                        <div class="row">
                            <div class="col-md-6">
                                <select class="form-control" id="DeleteBatchDrop">
                                    <option value=""></option>
                                    @For Each batch In Model.Batches
                                        @<option value="@batch">@batch</option>
                                    Next
                                </select>
                            </div>
                            <div class="col-md-6">
                                <button type="button" data-toggle="tooltip" data-original-title="Delete selected batch id" class="btn btn-danger" id="DeleteSelBatch"><span class="glyphicon glyphicon-trash"></span></button>
                                @If permissions.Contains("Batch Manager Delete All") Then
                                    @<button type="button" class="btn btn-danger" id="DeleteAll">Delete by Transaction Type</button>
                                End If
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-7">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Order Selection List
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        @If Not (Model.BMSettings(5) = "" Or Model.BMSettings(5).ToString() = "0") Then
                            @<button type="button" Class="btn btn-primary" id="Append" name="@Model.BMSettings(5)">Append Next @Model.BMSettings(5) Orders</button>
                        End If
                        <button type="button" class="btn btn-primary" id="AutoBatch">Select Orders in Auto Batch Zones</button>
                    </div>
                    <div class="col-md-6">
                        <label>Order Number Lookup:</label>
                        <input type="text" autofocus class="form-control" id="BMOrderScan" />
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="leftBMTable">
                            <thead>
                                    <tr>
                                    <th>Order Number</th>
                                    <th>Priority</th>
                                    <th>Quantity</th>
                                    <th>Auto Batch</th>
                                    <th>Tote Number</th>
                                    <th>Detail</th>
                                </tr>
                            </thead>
                            <tbody>
                                @For Each tr As List(Of String) In Model.BMTable
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
    <div class="col-md-5">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Selected Orders
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <input type="text" class="form-control" placeholder="Next Batch Order Number" id="NextBatchID" value="@Model.BMSettings(0)" maxlength="50" />
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-success" id="Create">Create Batch</button>
                        <button type="button" class="btn btn-primary" id="RemoveAll">Remove All</button>
                        <div class="btn-group">
                            <button type="button" class="btn btn-primary dropdown-toggle" id="Print" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                            <ul class="dropdown-menu" role="menu">
                                <li><a id="PrintReport" class="Print-Report">Print Batch Report</a></li>
                                <li><a id="PrintLabels" class="Print-Label">Print Item Labels</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="rightBMTable">
                            <thead>
                                    <tr>
                                    <th>Order Number</th>
                                    <th>Priority</th>
                                    <th>Quantity</th>
                                    <th>Auto Batch</th>
                                    <th>Tote Number</th>
                                    <th>Detail</th>
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
<input type="hidden" value="@(IIf(Model.BMSettings(2), "TRUE", "FALSE"))" id="PickToTotes" />
<input type="hidden" value="@(IIf(seeOrderStatus, "TRUE", "FALSE"))" id="seeOrderStatus" />

@Html.Partial("~/Areas/ICSAdmin/Views/BatchManager/Batch/PickPartial.vbhtml")
@Html.Partial("~/Areas/ICSAdmin/Views/BatchManager/Batch/BatchManDeletePartial.vbhtml")
<div class="container-fluid" id="tableDiv" style="display:none;">
    @Html.Partial("~/Areas/ICSAdmin/Views/BatchManager/Batch/OSAlternativePartial.vbhtml", Model.Aliases)
</div>