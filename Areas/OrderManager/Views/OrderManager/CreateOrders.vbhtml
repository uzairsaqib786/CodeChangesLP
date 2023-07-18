<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Create Orders"
    ViewData("PageName") = "&nbsp; | &nbsp; Create Orders"
End Code
<!--Might need to change some of these to better reflect page-->
<input type="hidden" id="PrintDirect" value="@Model.Preferences.PrintDirectly.ToString().ToLower()" />
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label>Order Number:</label>
                            <input type="text" class="form-control" id="CreateOrdersOrderNum" />
                        </div>
                        <div class="col-md-3" style="padding-top:20px;">
                            <button onclick="location.href= '/Transactions?App=OM'" class="btn btn-primary btn-block">Order Status</button>
                        </div>
                        <div class="col-md-2" style="padding-top:20px;">
                            <button id="CreateOrdersAddOrder" class="btn btn-primary btn-block edit-modal">Add New Order</button>
                        </div>
                        <div class="col-md-2" style="padding-top:20px;">
                            <button id="CreateOdersAddTrans" class="btn btn-primary btn-block edit-modal" disabled>Add Transaction</button>
                        </div>
                        <div class="col-md-2" style="padding-top:20px;">
                            <button id="CreateOrdersUserFields" class="btn btn-primary btn-block">Default User Fields</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <button id="CreateOrdersColSequence" onclick="$('#colSequenceForm').submit()" class="btn btn-primary" title="Set Column Sequence" data-toggle="tooltip"><span class="glyphicon glyphicon-list"></span></button>
                            <button id="CreateOrdersPrint" class="btn btn-primary Print-Report" title="Print Viewed"><span class="glyphicon glyphicon-print"></span></button>
                            <button id="CreateOrdersDelete" class="btn btn-danger" title="Delete Viewed" data-toggle="tooltip"><span class="glyphicon glyphicon-trash"></span></button>
                            <button id="CreateOrdersRelease" class="btn btn-primary">Release Orders</button>
                            <button id="CreateOdersEditTrans" class="btn btn-primary edit-modal" disabled>Edit Transaction</button>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <select class="form-control" id="CreateOrdersSearchCol">
                                @For Each column In Model.ColumnSequence
                                    @<option value="@column">@column</option>
                                Next
                            </select>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <input type="text" id="CreateOrdersSearch" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top:10px;">
                        <div class="col-md-12">
                            <table id="CreateOrdersTable" class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                                <thead>
                                    <tr>
                                        @For Each column In Model.ColumnSequence
                                            @<th class="text-center">@column</th>
                                        Next
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
<select id="OMCreateColumns" hidden="hidden">
    @For Each column In Model.ColumnSequence
        @<option value="@column">@column</option>
    Next
</select>
@Code
    Dim Prefs As PickPro_Web.OrderManager.OMPreferenceModel = Model.Preferences
End Code
<input type="hidden" id="AllowInProc" value="@Prefs.AllowInProc.ToString" />
<form id="colSequenceForm" method="post" action="/Admin/ColumnSequence">
    <input type="hidden" name="table" value="Order Manager Create" />
    <input type="hidden" name="App" value="OM" />
</form>

@Html.Partial("~/Areas/OrderManager/Views/OrderManager/UpdatePendingModal.vbhtml")
@Html.Partial("~/Areas/OrderManager/Views/OrderManager/UserFieldDataModal.vbhtml")
<script src="~/Areas/OrderManager/Scripts/OrderManager/CreateOrders.js"></script>
<script src="~/Areas/OrderManager/Scripts/OrderManager/OrderManCreateFilters.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>