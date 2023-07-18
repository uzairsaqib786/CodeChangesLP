<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Order Manager"
    ViewData("PageName") = "&nbsp; | &nbsp; Order Manager"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
<input type="hidden" value="@Model.Preferences.PrintDirectly.ToString().ToLower()" id="PrintDirect" />
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <label>Find Order(s) By:</label>
                                    <select id="OMColumn" class="form-control">
                                        <option value="Category">Category</option>
                                        <option value="Description">Description</option>
                                        <option value="Host Transaction ID">Host Transaction ID</option>
                                        <option value="Import Date">Import Date</option>
                                        <option value="Item Number"> Item Number</option>
                                        <option value="Notes">Notes</option>
                                        <option value="Order Number" selected="selected">Order Number</option>
                                        <option value="Priority">Priority</option>
                                        <option value="Required Date">Required Date</option>
                                        <option value="Sub Category">Sub Category</option>
                                        <option value="Supplier Item ID">Supplier Item ID</option>
                                        <option value="User Field1">User Field1</option>
                                        <option value="User Field2">User Field2</option>
                                        <option value="User Field3">User Field3</option>
                                        <option value="User Field4">User Field4</option>
                                        <option value="User Field5">User Field5</option>
                                        <option value="User Field6">User Field6</option>
                                        <option value="User Field7">User Field7</option>
                                        <option value="User Field8">User Field8</option>
                                        <option value="User Field9">User Field9</option>
                                        <option value="Warehouse">Warehouse</option>
                                    </select>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <label style="display:block">Case</label>
                                    <label class="radio-inline">
                                        <input checked type="radio" name="OMWhereClause"  value="Equals"> Equals
                                    </label>
                                    <label class="radio-inline">
                                        <input type="radio" name="OMWhereClause" value="Between"> Between
                                    </label>
                                    <label class="radio-inline">
                                        <input type="radio" name="OMWhereClause" value="Like"> Like
                                    </label>
                                   
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <label>Value 1</label>
                                    <input type="text" class="form-control" id="OMVal1" placeholder="Find order by Value" value="" data-toggle="tooltip" data-placement="top" title="For dates use mm/dd/yy" />
                                    <input style="display:none" type="text" class="form-control" id="OMVal1D" value="@Date.Now.ToShortDateString"  />
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <label hidden="hidden" id="OMValLabel">Value 2</label>
                                    <input type="text" class="form-control" id="OMVal2" placeholder="Find order by End Value" value="" data-toggle="tooltip" data-placement="top" title="For dates use mm/dd/yy" />
                                    <input style="display:none;" type="text" class="form-control" id="OMVal2D" value="@Date.Now.ToShortDateString" />
                                </div>
                            </div>
                            <div class="row" style="margin-top:5px;">
                                <div class="col-md-3 col-xs-6">
                                    <label style="padding-left:10px;">Max Orders</label>
                                    <input type="text" id="maxOrders" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" value="@Model.MaxOrder" class="form-control">
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <label>Trans Type</label>
                                    <select id="OMtransType" class="form-control">
                                        <option value="Pick">Pick</option>
                                        <option value="Put Away">Put Away</option>
                                        <option value="Count">Count</option>
                                    </select>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <label>View Type</label>
                                    <select id="ViewType" class="form-control">
                                        <option value="Headers">Order Headers</option>
                                        <option value="Lines">Order Lines</option>
                                    </select>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <label>Order Type</label>
                                    <select id="OrderType" class="form-control">
                                        <option value="Open">Open</option>
                                        <option value="Pending">Pending</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12 bottom-spacer">
                                <button id="OMDispVals" class="btn btn-primary btn-block">Display Records</button>
                            </div>
                            <div class="col-md-12 bottom-spacer">
                                @If permissions.Contains("Admin Create Orders") Then
                                    @<button id="addEditOrder" class="btn btn-primary btn-block">
                                        Add/Edit Order
                                    </button>
                                End If
                            </div>
                            <div class="col-md-12 bottom-spacer">
                                <button id="orderStat" class="btn btn-primary btn-block">
                                    Order Status
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-6">
                            <button id="colSequence" onclick="$('#colSequenceForm').submit()" title="Set Column Sequence" data-toggle="tooltip" class="btn btn-primary">
                                <span class="glyphicon glyphicon-list"></span>
                            </button>
                            <button id="printViewed" class="btn btn-primary Print-Report" title="Print Viewed" data-toggle="tooltip">
                                <span class="glyphicon glyphicon-print"></span>
                            </button>
                            <button id="delViewed" class="btn btn-danger" title="Delete Viewed" data-toggle="tooltip">
                                <span class="glyphicon glyphicon-trash"></span>
                            </button>
                            <button id="releaseViewed" class="btn btn-primary">
                                Release Viewed
                            </button>
                            <button id="OMUpdateButt" class="btn btn-primary" disabled>Update Record(s)</button>
                        </div>
                        <div class="col-xs-3">
                            <select class="form-control" id="OMSearchCol">
                                @For Each column In Model.ColumnSequence
                                    @<option value="@column">@column</option>
                                Next
                            </select>
                        </div>
                        <div class="col-xs-3">
                            <input type="text" id="OMSearch" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top:10px;">
                        <div class="col-md-12">
                            <table id="OMDataTable" class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                                <thead>
                                    <tr>
                                        @For Each column In Model.ColumnSequence
                                            @<th class="text-center">@column</th>
                                        Next
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
@Code
    Dim Prefs As PickPro_Web.OrderManager.OMPreferenceModel = Model.Preferences
End Code
<input type="hidden" id="AllowInProc" value="@Prefs.AllowInProc.ToString" />
<input type="hidden" id="ReleasePartial" value="@Prefs.AllowPartRel.ToString" />
<select id="OMColumns" hidden="hidden">
    @For Each column In Model.ColumnSequence
        @<option value = "@column">@column</option>
    Next
</select>
<form id="colSequenceForm" method="post" action="/Admin/ColumnSequence">
    <input type="hidden" name="table" value="Order Manager" />
    <input type="hidden" name="App" value="OM" />
</form>
@Html.Partial("~/Areas/OrderManager/Views/OrderManager/OMUpdateModal.vbhtml")
@Html.Partial("~/Areas/OrderManager/Views/OrderManager/OMSelUpdateModal.vbhtml")
<script src="~/Areas/OrderManager/Scripts/OrderManager/OrderManager.js"></script>
<script src="~/Areas/OrderManager/Scripts/OrderManager/OrderManFilters.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>

