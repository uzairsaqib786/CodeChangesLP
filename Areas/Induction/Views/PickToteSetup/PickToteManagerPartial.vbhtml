<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype object
<div class="modal fade" id="PickToteManModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="PickToteManModal" aria-hidden="true">
    <div class="modal-dialog" style="width:99%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="PickToteManLabel">
                    Pick Tote Manager - Select Order Numbers
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label style="display:block">Batch By:</label>
                        <label class="radio-inline">
                            @If Model.Prefs.UseDefFilter Then
                                @<input type="radio" name="BatchBy" id="UseFilter" checked="checked" value="Filter" /> @<strong>Filter</strong>
                            Else
                                @<input type="radio" name="BatchBy" id="UseFilter" value="Filter" /> @<strong>Filter</strong>
                            End If
                        </label>
                        <label class="radio-inline">
                            @If Model.Prefs.UseDefZone Then
                                @<input type="radio" name="BatchBy" id="UseZone" checked="checked" value="Zone" /> @<strong>Zone</strong>
                            Else
                                @<input type="radio" name="BatchBy" id="UseZone" value="Zone" /> @<strong>Zone</strong>
                            End If
                        </label>
                    </div>
                </div>
                <div class="row" style="padding-top:5px;">
                    <div class="col-md-12">
                        <div role="tabpanel">
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation">
                                    <a href="#FilterTab" aria-controls="FilterTab" role="tab" data-toggle="tab" id="BatchFilterTab">Batch By Filters</a>
                                </li>
                                <li role="presentation">
                                    <a href="#ZoneTab" aria-controls="ZoneTab" role="tab" data-toggle="tab" id="BatchZoneTab">Batch By Zone</a>
                                </li>
                                <li role="presentation">
                                    <a href="#ResultTab" aria-controls="ResultTab" role="tab" data-toggle="tab" id="BatchResultsTab">Batch Results</a>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane" id="FilterTab">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        Filter Functions
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label>Select Saved Filter</label>
                                                            <input type="text" id="SelFilterVal" class="form-control typeahead" placeholder="Filter" />
                                                        </div>
                                                        <div class="col-md-10" style="padding-top:22px">
                                                            <button type="button" class="btn btn-primary" disabled id="RenameFilter">Rename Filter</button>
                                                            <button type="button" class="btn btn-primary" id="ViewDefault">View Default</button>
                                                            <button type="button" class="btn btn-primary" disabled id="SetDefault">Set Default</button>
                                                            <button type="button" class="btn btn-primary" id="ClearDefault">Clear Default</button>
                                                            <button type="button" class="btn btn-danger" disabled data-toggle="tooltip" data-placement="top" title="Delete Selected Filter" id="DeleteFilter"><span class="glyphicon glyphicon-remove"></span></button>
                                                            <button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="top" title="Add New Filter" id="AddNewFilter"><span class="glyphicon glyphicon-plus"></span></button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        Filter
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-1 pull-right">
                                                            <button type="button" class="btn btn-primary" disabled data-toggle="tooltip" data-placement="top" title="Add New Filter Row" id="AddFilterRow"><span class="glyphicon glyphicon-plus"></span></button>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-1">
                                                            <label>Sequence</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>Field</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>Criteria</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>Value</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>And/Or</label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12" id="PickBatchFilterContainer" style="overflow-y:scroll;">

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        Order By
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-2 pull-right">
                                                            <button type="button" class="btn btn-primary" data-toggle="tooltip" disabled data-placement="top" title="Add New Order Row" id="AddOrderBy"><span class="glyphicon glyphicon-plus"></span></button>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label>Sequence</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <label>Field</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>Sort Order</label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12" id="PickBatchOrderByContainer" style="overflow-y:scroll;">

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="ZoneTab">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Zone</label>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Batch Type</label>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Total Orders</label>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Total Locations</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" id="PickBatchZoneContainer" style="overflow-y:scroll;">

                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="ResultTab">
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-md-12">
                                            <label class="radio-inline">
                                                <input checked type="radio" name="transType" id="ViewSelectedOrders" value="All"> View Selected Order Lines
                                            </label>
                                            <label class="radio-inline">
                                                <input type="radio" name="transType" id="ViewAllOrders" value="Pick"> View All Order Lines
                                            </label>
                                            <button type="button" class="btn btn-primary" id="SelectAll">Select All</button>
                                            <button type="button" class="btn btn-primary" disabled id="SelectOrder">Select Order</button>
                                            <button type="button" class="btn btn-danger" id="ClearAll">Un-Select All Orders</button>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" id="SelectOrderNumbers">
                                            <legend class="text-center">Orders</legend>
                                            <div style="overflow-y:scroll;max-height:600px;">
                                                <table id="PickToteOrdersTable" style="background-color:white;" class="table table-bordered table-condensed table-striped">
                                                    <thead>
                                                        <tr>
                                                            <td>Order Number</td>
                                                            <td>Required Date</td>
                                                            <td>Priority</td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="col-md-9" id="SelectTransactions">
                                            <legend class="text-center">Transactions</legend>
                                            <div style="overflow-y:scroll;max-height:600px;">
                                                <table id="PickToteTransTable" style="background-color:white;" class="table table-bordered table-condensed datatable table-striped">
                                                    <thead>
                                                        <tr>
                                                            <th>Order Number</th>
                                                            <th>Item Number</th>
                                                            <th>Transaction Quantity</th>
                                                            <th>Location</th>
                                                            <th>Completed Quantity</th>
                                                            <th>Description</th>
                                                            <th>Import Date</th>
                                                            <th>Priority</th>
                                                            <th>Required Date</th>
                                                            <th>Line Number</th>
                                                            <th>Line Sequence</th>
                                                            <th>Serial Number</th>
                                                            <th>Lot Number</th>
                                                            <th>Expiration Date</th>
                                                            <th>Completed Date</th>
                                                            <th>Completed By</th>
                                                            <th>Batch Pick ID</th>
                                                            <th>Unit of Measure</th>
                                                            <th>User Field1</th>
                                                            <th>User Field2</th>
                                                            <th>User Field3</th>
                                                            <th>User Field4</th>
                                                            <th>User Field5</th>
                                                            <th>User Field6</th>
                                                            <th>User Field7</th>
                                                            <th>User Field8</th>
                                                            <th>User Field9</th>
                                                            <th>User Field10</th>
                                                            <th>Revision</th>
                                                            <th>Tote ID</th>
                                                            <th>Tote Number</th>
                                                            <th>Cell</th>
                                                            <th>Host Transaction ID</th>
                                                            <th>ID</th>
                                                            <th>Zone</th>
                                                            <th>Carousel</th>
                                                            <th>Row</th>
                                                            <th>Shelf</th>
                                                            <th>Bin</th>
                                                            <th>Warehouse</th>
                                                            <th>Inv Map ID</th>
                                                            <th>Import By</th>
                                                            <th>Import Filename</th>
                                                            <th>Notes</th>
                                                            <th>Emergency</th>
                                                            <th>Master Record</th>
                                                            <th>Master Record ID</th>
                                                            <th>Export Batch ID</th>
                                                            <th>Export Date</th>
                                                            <th>Exported By</th>
                                                            <th>Status Code</th>
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
            <div class="modal-footer">
                <button type="button" class="btn btn-default " data-dismiss="modal" id="tote_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/PickToteSetup/PickToteManagerPartial.js"></script>
<script src="~/Areas/Induction/Scripts/PickToteSetup/PickToteManTransFilters.js"></script>