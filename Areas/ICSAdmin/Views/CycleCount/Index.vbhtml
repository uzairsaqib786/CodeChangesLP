<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.CycleCountModel
@Code
    ViewData("Title") = "Cycle Count"
    ViewData("PageName") = "&nbsp; | &nbsp; Cycle Count"
End Code

<div class="container-fluid">
    <div id="CycleCountInfo">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <button type="button" data-toggle="tooltip" title="Set Import Field Mappings" class="btn btn-primary" id="ImportFieldMap"><span class="glyphicon glyphicon-cog"></span></button>
                        <button type="button" class="btn btn-primary" id="ImportHostFile">Import Discrepancies</button>
                        <a href="/Admin/CycleCount/CountBatches" class="btn btn-primary pull-right">Create Count Batches</a>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            
                        </div>
                        <div class="col-md-6">
                            <legend class="text-center">Discrepancies</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="btn btn-primary" id="AppendDiscButt">Append All</button>
                                    <button type="button" class="btn btn-primary Print-Report" title="Print Discrepancy Report" data-toggle="tooltip" id="PrintDiscRep"><span class="glyphicon glyphicon-print"></span></button>
                                    <button id="DeleteDiscrept" title="Delete All" data-toggle="tooltip" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button>
                                    <label>Print CC Disc Directly:</label>
                                    <input type="checkbox" checked="checked" id="PrintCCDiscDirect" />
                                </div>
                            </div>
                            <div class="row" style="padding-top: 10px;">
                                <div class="col-md-12">
                                    <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="CycleCountLeftTable">
                                        <thead>
                                            <tr>
                                                <th>Item Number</th>
                                                <th>Qty Difference</th>
                                                <th>Qty Location</th>
                                                <th>Warehouse</th>
                                                <th>Lot Number</th>
                                                <th>Expiration</th>
                                                <th>Serial Number</th>
                                                <th>ID</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            @For Each row As List(Of String) In Model.DiscTable
                                                @<tr>
                                                    @For Each colVal As String In row
                                                        @<td>@colVal</td>
                                                    Next
                                                </tr>
                                            Next
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <legend class="text-center">Count Queue</legend>
                            <div class="row">
                                <div class="col-md-4">
                                    <button type="button" class="btn btn-primary" id="CreateCountTrans">Create Transactions</button>
                                    <button type="button" class="btn btn-danger" title="Clear All" data-toggle="tooltip" id="ClearDiscButt"><span class="glyphicon glyphicon-remove"></span></button>
                                </div>
                                <div class="col-md-8">
                                    <div class="form-inline">
                                        <div class="form-group pull-right">
                                            <label class="">Total Locations:</label><input type="text" class="form-control" id="TotLocs" value="0" disabled maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <div class="col-md-12">
                                    <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="CycleCountRightTable">
                                        <thead>
                                            <tr>
                                                <th>Item Number</th>
                                                <th>Qty Difference</th>
                                                <th>Qty Location</th>
                                                <th>Warehouse</th>
                                                <th>Lot Number</th>
                                                <th>Expiration</th>
                                                <th>Serial Number</th>
                                                <th>ID</th>
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
        </div>
    </div>
</div>
@Html.Partial("~/Views/ModalPartials/ImportFieldMapModal.vbhtml")
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/CycleCount/CycleCount.js"></script>