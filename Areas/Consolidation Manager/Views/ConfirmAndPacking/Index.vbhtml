<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "ConfirmAndPacking"
    ViewData("PageName") = "&nbsp; | &nbsp; Confirm And Packing"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

<div class="container-fluid">
    @If CStr(Model.shipComp) <> "" Then
        @<div class="row">
            <div class="col-md-12">
                <div class="alert alert-danger" role="alert" style="font-size:x-large; text-align:center;">Order Number: @Model.orderNumber is marked as Shipping Complete</div>
            </div>
        </div>
    ElseIf Model.complete <> "" Then
        @<div class="row">
            <div class="col-md-12">
                <div class="alert alert-success" role="alert" style="font-size:x-large; text-align:center;">Order Number: @Model.orderNumber is confirmed and packed</div>
            </div>
        </div>
    End If
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2 class="panel-title">Ship Info</h2>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label>Order Number: </label><input maxlength="50" type="text" id="ConfPackOrderNum" class="form-control" disabled="disabled" value="@Model.orderNumber" />
                        </div>
                        <div class="col-md-3" style="padding-top:25px;">
                            @If permissions.Contains("Consolidation Mgr Confirm All") Then
                                If CStr(Model.complete) <> "" Then
                                    @<button id="ConfirmAll" class="btn btn-primary btn-block" disabled>Confirm All</button>
                                Else
                                    @<button id="ConfirmAll" class="btn btn-primary btn-block">Confirm All</button>
                                End If
                            End If
                        </div>
                        <div class="col-md-3" style="padding-top:25px;">
                            <button id="PrintPackList" class="btn btn-primary btn-block Print-Report">Print Packing List</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            @If CStr(Model.complete) <> "" Then
                                @<label>Container ID: </label>@<input maxlength="50" type="text" id="ContID" class="form-control" disabled="disabled" />
                            Else
                                @<label>Container ID: </label>@<input maxlength="50" type="text" id="ContID" class="form-control" value="@Model.contID" />
                            End If
                        </div>
                        <div class="col-md-6" style="padding-top:25px;">
                            @If CStr(Model.complete) <> "" Then
                                @<button id="NextContID" class="btn btn-primary btn-block" disabled>Next Container ID</button>
                            Else
                                @<button id="NextContID" class="btn btn-primary btn-block">Next Container ID</button>
                            End If
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label>Containers in Order: </label><select class="form-control" id="ContIDPrint">
                                <option value="" selected="selected"></option>
                                @For Each t As String In Model.contIDDrop
                                    @<option value="@t">@t</option>
                                Next
                            </select>
                        </div>
                        <div class="col-md-3" style="padding-top:25px;">
                            <div class="btn-group">
                                <button class="btn btn-primary" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span><span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                    <li><a class ="ContIDPrint">Print List</a></li>
                                    <li><a class="ContIDPrint">Print Label</a></li>
                                    <li><a class="ContIDPrint">Print Both</a></li>
                                </ul>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2 class="panel-title">Tote Information <span class="label label-default">@Model.toteTable.Count</span></h2>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="ToteInfoTable">
                                <thead>
                                    <tr>
                                        <th>Tote ID</th>
                                        <th>Location</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    @For Each tr As List(Of String) In Model.toteTable
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
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2 class="panel-title">Shipping Transactions</h2>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            @If Model.complete <> "" Then
                                @<label>Scan Item: </label>@<input maxlength="50" type="text" id="ScanItem" class="form-control" disabled="disabled" />
                            Else
                                @<label>Scan Item: </label>@<input maxlength="50" type="text" id="ScanItem" class="form-control" />
                            End If

                        </div>
                        <div class="col-md-3" style="padding-top:25px;">
                            <button id="ItemLabel" class="btn btn-primary btn-block Print-Label" disabled>Item Label</button>
                        </div>
                        <div class="col-md-3" style="padding-top:25px;">
                            <button id="UnPack" class="btn btn-primary btn-block" disabled>UnPack</button>
                        </div>
                    </div>
                    <div class="row" style="padding-top:15px;">
                        <div class="col-md-12">
                            <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="ShipTransTable">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Item Number</th>
                                        <th>Line Number</th>
                                        <th>Order Quantity</th>
                                        <th>Picked Quantity</th>
                                        <th>Container ID</th>
                                        <th>Ship Quantity</th>
                                        <th class="hide">Complete</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    @For Each tr As List(Of String) In Model.transTable
                                        Dim x = 0
                                        @<tr>
                                            @For Each td As String In tr
                                            If x = 7 Then
                                                @<td class="hide">@td</td>
                                            Else
                                                @<td>@td</td>
                                            End If
                                            x += 1
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
    <input type="hidden" id="AutoContPL" value="@CStr(Model.PrintPrefs.PrintContPL)" />
    <input type="hidden" id="AutoOrderPL" value="@CStr(Model.PrintPrefs.PrintOrderPL)" />
    <input type="hidden" id="AutoContLabel" value="@CStr(Model.PrintPrefs.PrintContLabel)" />
</div>
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/SplitLineModal.vbhtml")
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/AdjustShipQuantityModal.vbhtml")
@Html.Partial("~/Areas/Consolidation Manager/Views/ConfirmAndPacking/ConfPackSelectModal.vbhtml")
@Html.Partial("~/Areas/Consolidation Manager/Views/ConfirmAndPacking/ConfPackProcessModal.vbhtml")

<script src="~/Areas/Consolidation Manager/Scripts/ConfirmAndPacking/ConfirmAndPacking.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>