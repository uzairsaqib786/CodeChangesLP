<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">View Current Replenishment Orders</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-2">
                <label>Number of Picks</label>
            </div>
            <div class="col-md-2">
                <label>Number of Put Aways</label>
            </div>
        </div>
        <div class="row" style="padding-bottom: 10px;">
            <div class="col-md-2">
                <input class="form-control" id="pickCount" value="@Model.pickCount" disabled="disabled" />
            </div>
            <div class="col-md-2">
                <input class="form-control" id="putCount" value="@Model.putCount" disabled="disabled" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6">
                <label># Entries</label>
                <select id="curOrderslength" class="form-control" style="width:auto; display:inline;">
                    <option>10</option>
                    <option>15</option>
                    <option>20</option>
                    <option>25</option>
                    <option>50</option>
                    <option>100</option>
                </select>
                <label>
                    Show:
                    <select id="status" class="form-control" style="display:inline;width:auto;">
                        <option>All</option>
                        <option>Open</option>
                        <option>Completed</option>
                    </select>
                </label>
                <div class="btn-group">
                    <button id="goTo" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                    <ul class="dropdown-menu" role="menu">
                        <li><a id="printReport" class="Print-Report">Print Orders</a></li>
                        <li><a id="printLabel" class="Print-Label">Print Labels</a></li>
                    </ul>
                </div>
                <div class="btn-group">
                    <button id="delete" class="btn btn-danger dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-trash"></span> <span class="caret"></span></button>
                    <ul class="dropdown-menu" role="menu">
                        <li><a id="deleteAll">Delete All Orders</a></li>
                        <li><a id="deleteShown">Delete Shown Orders</a></li>
                        <li><a id="deleteRange">Delete Range</a></li>
                        <li><a id="deleteSelectedOrder">Delete Selected Order</a></li>
                    </ul>
                </div>
                <div class="btn-group">
                    <button id="ViewByPrint" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">View Orders <span class="caret"></span></button>
                    <ul class="dropdown-menu" role="menu">
                        <li><a id="ViewAllCurrent">View All Orders</a></li>
                        <li><a id="ViewNotPrintedCurrent">View Unprinted Orders</a></li>
                    </ul>
                </div>
            </div>
            <div class="col-xs-6" id="searchStringTypeAhead">
                <label class="pull-right">
                    Search
                    <select id="curOrdersColumns" class="form-control" style="display:inline;width:auto;" aria-controls="example">
                        <option value=""></option>
                        <option value="Item Number">Item Number</option>
                        <option value="Trans Type">Trans Type</option>
                        <option value="Warehouse">Warehouse</option>
                        <option value="Zone">Zone</option>
                        <option value="Carsl">Carsl</option>
                        <option value="Row">Row</option>
                        <option value="Shelf">Shelf</option>
                        <option value="Bin">Bin</option>
                        <option value="Cell">Cell</option>
                        <option value="Lot Number">Lot Number</option>
                        <option value="Trans Qty">Trans Qty</option>
                        <option value="Description">Description</option>
                        <option value="Order Number">Order Number</option>
                        <option value="UofM">UofM</option>
                        <option value="Batch Pick ID">Batch Pick ID</option>
                        <option value="Serial Number">Serial Number</option>
                        <option value="Comp Date">Comp Date</option>
                        <option value="Print Date">Print Date</option>
                    </select>
                    By
                    <input id="curOrderSearch" class="form-control typeahead" type="text" style="width:auto; display:inline;" placeholder="Search" maxlength="255" />
                </label>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <table id="currentOrders" class="table table-bordered table-condensed table-striped" cellspacing="0" role="grid" style="background-color:white;">
                    <thead>
                        <tr>
                            <th class="sorting">Item Num</th>
                            <th class="sorting">Trans Type</th>
                            <th class="sorting">Warehouse</th>
                            <th class="sorting">Zone</th>
                            <th class="sorting">Carsl</th>
                            <th class="sorting">Row</th>
                            <th class="sorting">Shelf</th>
                            <th class="sorting">Bin</th>
                            <th class="sorting">Cell</th>
                            <th class="sorting">Lot Number</th>
                            <th class="sorting">Trans Qty</th>
                            <th class="sorting">Description</th>
                            <th class="sorting">Order Number</th>
                            <th class="sorting">UofM</th>
                            <th class="sorting">Batch Pick ID</th>
                            <th class="sorting">Serial Number</th>
                            <th class="sorting">Comp Date</th>
                            <th class="sorting">Print Date</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
</div>
@Html.Partial("DeleteModal")
@Html.Partial("~/Areas/ICSAdmin/Views/SystemReplenishment/ReplenLabelPrintModalPartial.vbhtml")