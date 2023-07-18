<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.CreateCountBatchesModel
@Code
    ViewData("Title") = "Cycle Count"
    ViewData("PageName") = "&nbsp; | &nbsp; Cycle Count"
    ViewData("override_back") = "cycleCountBackBtn()"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs" role="tablist">
                <li class="active"><a id="cCounts" href="#CreateCountsTab" role="tab" data-toggle="tab">Create Counts</a></li>
                <li><a id="CountQueue" href="#CountQueueTab" role="tab" data-toggle="tab">Count Queue</a></li>
            </ul>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="tab-content">
                        <div class="tab-pane active" id="CreateCountsTab">
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Count Type</label>
                                    <select style="display:inline;width:auto;" class="form-control" id="CountFilterType">
                                        <option value="#LocationRangeDiv">Location Range</option>
                                        <option value="#ItemNumberDiv">Item Number</option>
                                        <option value="#DescriptionDiv">Description</option>
                                        <option value="#CategoryDiv">Category</option>
                                        <option value="#NotCountedSinceDiv">Not Counted Since</option>
                                        <option value="#PickedRangeDiv">Picked Date Range</option>
                                        <option value="#PutAwayRangeDiv">Put Away Date Range</option>
                                        <option value="#CostRangeDiv">Cost Range</option>
                                    </select>
                                    <label>Warehouse</label>
                                    <select class="form-control" style="width:auto;display:inline" id="Warehouse">
                                        <option value="">No Warehouse</option>
                                        @For Each warehouse In Model.Warehouses
                                            @<option value="@warehouse">@warehouse</option>
                                        Next
                                    </select>
                                </div>
                                <div id="CountResultInputs" class="col-md-8">
                                    <div id="LocationRangeDiv">
                                        <div class="col-md-3">
                                            <input id="FromLocation" class="form-control" type="text" placeholder="From Location" maxlength="50" />
                                        </div>
                                        <div class="col-md-3">
                                            <input id="ToLocation" class="form-control" type="text" placeholder="To Location" maxlength="50" />
                                        </div>
                                        <div class="col-md-3">
                                            <label>Include Empty locations?</label><input id="IncludeEmpty" type="checkbox" />
                                        </div>
                                        <div class="col-md-3">
                                            <label>Include Other Locations?</label><input id="IncludeOther" type="checkbox" />
                                        </div>
                                    </div>
                                    <div id="ItemNumberDiv" hidden="hidden">
                                        <div class="col-md-6">
                                            <input id="FromItem" class="form-control" type="text" placeholder="From Item Number" maxlength="50" />
                                        </div>
                                        <div class="col-md-6">
                                            <input id="ToItem" class="form-control" type="text" placeholder="To Item Number" maxlength="50" />
                                        </div>  
                                    </div>
                                    <div id="DescriptionDiv" hidden="hidden">
                                        <div class="col-md-4">
                                            <input id="Description" class="form-control" type="text" placeholder="Description" maxlength="255" />
                                        </div>
                                    </div>
                                    <div id="CategoryDiv" hidden="hidden">
                                        <div class="col-md-6">
                                            <input id="Category" class="form-control" type="text" placeholder="Category" maxlength="50" />
                                        </div>
                                        <div class="col-md-6">
                                            <input id="SubCategory" class="form-control" disabled="disabled" type="text"  maxlength="50" />
                                        </div>
                                    </div>
                                    <div id="NotCountedSinceDiv" hidden="hidden">
                                        <div class="col-md-6">
                                            <input id="NotCounted" class="form-control date-picker" type="text" maxlength="50" />
                                        </div>
                                    </div>
                                    <div id="PickedRangeDiv" hidden="hidden">
                                        <div class="col-md-6">
                                            <input id="PickedStart" class="form-control date-picker" type="text" placeholder="From Date" maxlength="50" />
                                        </div>
                                        <div class="col-md-6">
                                            <input id="PickedEnd" class="form-control date-picker" type="text" placeholder="To Date" maxlength="50" />
                                        </div>
                                    </div>
                                    <div id="PutAwayRangeDiv" hidden="hidden">
                                        <div class="col-md-6">
                                            <input id="PutStart" class="form-control date-picker" type="text" placeholder="From Date" maxlength="50" />
                                        </div>
                                        <div class="col-md-6">
                                            <input id="PutEnd" class="form-control date-picker" type="text" placeholder="To Date" maxlength="50" />
                                        </div>
                                    </div>
                                    <div id="CostRangeDiv" hidden="hidden">
                                        <div class="col-md-6">
                                            <input id="CostStart" class="form-control" type="text" placeholder="From Cost" maxlength="50" />
                                        </div>
                                        <div class="col-md-6">
                                            <input id="CostEnd" class="form-control" type="text" placeholder="To Cost" maxlength="50" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                           <div class="row">
                               <div class="col-md-12" style="margin-top:10px;">
                                   <button id="InsccQueue" class="btn btn-primary" disabled="disabled">Insert into Queue</button>
                               </div>
                               <div class="col-md-12">
                                   <table id="CountResultTable" class="table table-bordered table-condensed table-striped" style="background-color:white;margin-top:5px;">
                                       <thead>
                                           <tr>
                                               <th></th>
                                               <th>Inv Map ID</th>
                                               <th>Item Number</th>
                                               <th>Description</th>
                                               <th>Location Quantity</th>
                                               <th>UM</th>
                                               <th>Warehouse</th>
                                               <th>Location</th>
                                               <th>Velocity Code</th>
                                               <th>Cell Size</th>
                                               <th>Serial Number</th>
                                               <th>Lot Number</th>
                                               <th>Expir. Date</th>
                                           </tr>
                                       </thead>
                                       <tbody></tbody>
                                   </table>
                               </div>
                           </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label>Current Count Orders</label>
                                </div>
                                <div class="col-md-4">
                                    <label>Print CC Details Directly:</label>
                                    <input type="checkbox" checked="checked" id="PrintCCDetailsDirect" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <select class="form-control" id="CycleOrderNumber">
                                        @For Each order In Model.CycleOrders
                                            @<option value="@order.OrderNumber">@order.OrderNumber Count: @order.Count</option>
                                        Next
                                    </select>
                                    
                                </div>
                                <div class="col-md-2">
                                    <button id="PrintCycleCount" title="Print Count Order Report" class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button>
                                    <div class="btn-group">
                                        <button class="btn btn-danger dropdown-toggle" data-toggle="dropdown">Delete Order<span class="caret"></span></button>
                                        <ul class="dropdown-menu">
                                            <li><a href="#" id="DelIncomplete">Delete Incomplete</a></li>
                                            <li><a href="#" id="DelAll">Delete All</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="CountQueueTab">
                            <div class="col-md-12" style="margin-top:10px;">
                                <select id="pageLength" class="form-control" style="width:auto; display:inline;">
                                    <option>10</option>
                                    <option>15</option>
                                    <option selected="selected">20</option>
                                    <option>25</option>
                                    <option>50</option>
                                    <option>100</option>
                                </select>
                                <label>Results per page</label>
                                <button id="CreateCounts" class="btn btn-primary" data-toggle="tooltip" title="Create Cycle Count "><span class="glyphicon glyphicon-saved"></span></button>
                                <button id="DeleteccQueue" class="btn btn-danger" style="margin-right:5px;" data-toggle="tooltip" title="Delete all From Queue"><span class="glyphicon glyphicon-remove"></span></button>
                            </div>
                            <div class="col-md-12">
                                <table id="CountQueueTable" class="table table-bordered table-condensed table-striped" style="background-color:white;margin-top:5px;">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Inv Map ID</th>
                                            <th>Item Number</th>
                                            <th>Description</th>
                                            <th>Location Quantity</th>
                                            <th>UM</th>
                                            <th>Warehouse</th>
                                            <th>Location</th>
                                            <th>Velocity Code</th>
                                            <th>Cell Size</th>
                                            <th>Serial Number</th>
                                            <th>Lot Number</th>
                                            <th>Expir. Date</th>
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
    <div class="modal fade" id="CountBatch_Modal" tabindex="-1" role="dialog" aria-labelledby="CountBatch_label" aria-hidden="true">
        <div class="modal-dialog" style="width:900px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="Message_label">Cycle Count</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <p class="text-center"><strong id="CountBatch_Message" style="font-size:24px;"></strong></p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal" id="CountBatch_submit">Yes</button>
                    <button type="button" class="btn btn-primary" data-dismiss="modal" id="CountBatch_cancel">No</button>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/CycleCount/CountBatches.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/CycleCount/CountQueue.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/CycleCount/CountBatchesTA.js"></script>