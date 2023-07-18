<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">
            Create New Replenishment Orders
        </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-12">
                        
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="alert alert-info text-center" id="KanbanBanner" style="padding:10px 0 0 0;margin: 0 0 0 0;display:none;"><h4>Kanban</h4></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-xs-6">
                        <label>Number of Selected Replenishments:</label>
                        <input class="form-control" id="numberSelectedNew" value="0" disabled="disabled" />
                    </div>
                    <div class="col-md-1">
                        <label>Kanban?</label>
                        <input type="checkbox" class="form-control" id="Kanban" />
                    </div>
                    <div class="col-md-offset-5 col-md-4">
                        <label style="visibility:hidden;" class="btn-block">Process:</label>
                        <button type="button" class="btn btn-success pull-right" id="processNew">Process Replenishments</button>
                        <button type="button" class="btn btn-primary pull-right" id="createNew" style="margin-right:5px;">Create New Order List</button>
                    </div>
                </div>                
            </div>
        </div>
        <div class="row" style="padding-top:10px;">
            <div class="col-xs-6">
                <label># Entries</label>
                <select id="pageLengthNew" class="form-control" style="width:auto; display:inline;">
                    <option>10</option>
                    <option>15</option>
                    <option>20</option>
                    <option>25</option>
                    <option>50</option>
                    <option>100</option>
                </select>
                <label>
                    Show:
                    <select class="form-control" style="display:inline;width:auto;" id="Reorder">
                        <option value="all">All</option>
                        <option value="reorder">Re-Order</option>
                    </select>
                </label>
                <button id="printNew" class="btn btn-primary Print-Report"><span class="glyphicon glyphicon-print"></span></button>
                <button type="button" class="btn btn-primary" id="goToInvMaster" data-toggle="tooltip" title="View Selected Item in Inventory Master"><span class="glyphicon glyphicon-share"></span></button> 
                <button type="button" class="btn btn-primary" id="selectNew" data-toggle="tooltip" title="Select All"><span class="glyphicon glyphicon-ok"></span></button>
                <button type="button" class="btn btn-primary" id="unselectNew" data-toggle="tooltip" title="Unselect All"><span class="glyphicon glyphicon-ban-circle"></span></button>
                <div class="btn-group">
                    <button id="ViewItemsBy" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">View Items</button>
                    <ul class="dropdown-menu" role="menu">
                        <li><a id="ViewAllItems">View All Items</a></li>
                        <li><a id="ViewSelectedItems">View Selected Items </a></li>
                    </ul>
                </div>
                <button type="button" class="btn btn-primary" id="ItemNumberFilterButt">Filter Item Numbers</button>
            </div>
            <div class="col-xs-6" id="searchStringTypeAheadNew">
                <label class="pull-right">
                    Search
                    <select id="columnSelectionNew" class="form-control" style="display:inline;width:auto;" aria-controls="example">
                        <option value=""></option>
                        <option value="Item Number">Item Number</option>
                        <option value="Description">Description</option>
                        <option value="Warehouse">Warehouse</option>
                        <option value="Stock Qty">Stock Qty</option>
                        <option value="Replenishment Point">Repln. Point</option>
                        <option value="Replenishment Level">Repln. Level</option>
                        <option value="Available Qty">Avail. Qty</option>
                        <option value="Replenishment Qty">Repln. Qty</option>
                        <option value="Case Qty">Case Qty</option>
                        <option value="Transaction Qty">Trans. Qty</option>
                        <option value="Replenish">Replenish</option>
                        <option value="Replenish Exists">Exists</option>
                        <option value="Alloc Pick">Alloc Pick</option>
                        <option value="Alloc Put">Alloc Put</option>
                    </select>
                    By
                    <input id="searchStringNew" class="form-control typeahead" type="text" style="width:auto; display:inline;" placeholder="Search" maxlength="255" />
                </label>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <table id="systemReplenishmentNewOrders" class="table table-bordered table-striped table-condensed" cellspacing="0" role="grid" style="background-color:white;">
                    <thead>
                        <tr>
                            <th>Item Num.</th>
                            <th>Description</th>
                            <th>Warehouse</th>
                            <th>Stock Qty</th>
                            <th>Repln. Point</th>
                            <th>Repln. Level</th>
                            <th>Avail. Qty.</th>
                            <th>Repln. Qty</th>
                            <th>Case Qty</th>
                            <th>Trans. Qty</th>
                            <th>Replenish</th>
                            <th>Exists</th>
                            <th>RPID</th>
                            <th>Alloc Pick</th>
                            <th>Alloc Put</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/ICSAdmin/Views/SystemReplenishment/NewOrdersModalPartial.vbhtml")
@Html.Partial("~/Areas/ICSAdmin/Views/SystemReplenishment/ItemNumberFilterPartial.vbhtml")