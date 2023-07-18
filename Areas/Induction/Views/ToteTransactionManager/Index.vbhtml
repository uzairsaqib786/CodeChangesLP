<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Tote Transaction Manager"
    ViewData("PageName") = "&nbsp; | &nbsp; Tote Transaction Manager"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Batch Picks</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <label>Batch Pick ID:</label>
                            <input type="text" class="form-control" id="ToteTransManBatchID" />
                        </div>
                        <div class="col-md-1" style="padding-top:20px;">
                            <button id="ViewAllToteTransMan" class="btn btn-primary"><span class="glyphicon glyphicon-remove"></span></button>
                        </div>
                        <div class="col-md-4" style="padding-top:20px;">
                            <button id="ClearPickToteInfo" class="btn btn-primary btn-block">Clear Pick Tote Info</button>
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
                    <h3 class="panel-title">Tote Transactions</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <button id="ClearBatchButt" class="btn btn-primary btn-block" disabled>Clear Info</button>
                        </div>
                        <div class="col-md-3">
                            <button id="OffCarListButt" class="btn btn-primary btn-block Print-Report" disabled>Print Off Carousel List</button>
                        </div>
                        <div class="col-md-3">
                            <button id="ToteContentButt" class="btn btn-primary btn-block Print-Report" disabled>Print Tote Contents</button>
                        </div>
                        <div class="col-md-3">
                            <button id="ToteLabelButt" class="btn btn-primary btn-block Print-Label" disabled> Print Tote Label</button>
                        </div>
                    </div>
                    <div class="row" style="padding-top:10px;">
                        <div class="col-md-12">
                            <table id="ToteTransManTable" class="table table-bordered table-condensed table-striped" style="background-color:white;margin-bottom:5px;">
                                <thead>
                                    <tr>
                                        <th class="text-center">Order Number</th>
                                        <th class="text-center">Batch ID</th>
                                        <th class="text-center">Pos Number</th>
                                        <th class="text-center">Tote ID</th>
                                        <th class="text-center">Zones</th>
                                        <th class="text-center">Trans Type</th>
                                        <th class="text-center">Host Trans ID</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <input hidden id="PrintDir" value="@Model.printDir.ToString().ToLower()" />
        <select hidden id="ToteTransManCols">
            <option value="Batch Pick ID">Batch Pick ID</option>
            <option value="Tote Number">Tote Number</option>
            <option value="Tote ID">Tote ID</option>
            <option value="Zone Label">Zone Label</option>
            <option value="Transaction Type">Transaction Type</option>
            <option value="Host Transaction ID">Host Transction ID</option>
        </select>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/ToteTransactionManager/ToteTransManFilters.js"></script>
<script src="~/Areas/Induction/Scripts/ToteTransactionManager/ToteTransMan.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
@Html.partial("~/Areas/Induction/Views/Modal/DeleteBatchModalPartial.vbhtml")