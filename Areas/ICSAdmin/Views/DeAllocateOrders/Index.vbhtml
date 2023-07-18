<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype List(Of String)
@Code
    ViewData("Title") = "De-Allocate Orders"
    ViewData("PageName") = "&nbsp; | &nbsp; De-Allocate Orders"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-3">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Filters</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label>View Transaction Types</label>
                        </div>
                        <div class="col-md-12">
                            <label class="radio-inline">
                                <input checked type="radio" name="transType" id="allTransType" value="All"> All Trans. Type
                            </label>
                            <label class="radio-inline">
                                <input type="radio" name="transType" id="showPicks" value="Pick"> Picks
                            </label>
                            <label class="radio-inline">
                                <input type="radio" name="transType" id="showPuts" value="Put Away"> Put Aways
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Item Number</label>
                        </div>
                        <div class="col-md-12">
                            <input id="itemLookup" class="typeahead form-control" maxlength="50" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Order Number</label>
                        </div>
                        <div class="col-md-12">
                            <input id="orderLookup" class="typeahead form-control" maxlength="50" />
                        </div>
                    </div>
                    @If permissions.Contains("De-Allocate All Orders") Then
                        @<div class="row">
                            <div class="col-md-12">
                                <button id="deAllocateAll" style="margin-top:5px;margin-bottom:5px;" class="btn btn-primary btn-block">De-Allocate All</button>
                            </div>
                        </div>
                    End If

                    <div class="row">
                        <div class="col-md-12">
                            <button id="deAllocateSelected" disabled style="margin-bottom:5px;" class="btn btn-primary btn-block">De-Allocate Selected</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-9">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Order Information</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                            <div class="col-md-12">
                                <label>Order View Options</label>
                            </div>
                            <div class="col-md-12">
                                <label class="radio-inline">
                                    <input checked type="radio" name="showTrans" id="inlineRadio1" value="spec"> Orders
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="showTrans" id="inlineRadio2" value="All"> Transactions
                                </label>
                            </div>
                    </div>
                    <div class="row">
                        
                    </div>
                    <div class="row">
                        <div id="specOrdersDiv" class="col-md-3">
                            <legend class="text-center">Orders</legend>
                            <div style="overflow-y:scroll;max-height:600px;">
                                <table id="orders" style="background-color:white;" class="table tablehover table-bordered table-striped table-condensed datatable">
                                    <thead>
                                        <tr>
                                            <td>Order Number</td>
                                            <td>Select Order</td>
                                        </tr>
                                    </thead>
                                    <tbody id="orderBody">
                                        @For Each order In Model
                                            @<tr>
                                                <td>@order</td>
                                                <td><input name="@order" class="selectOrder" type="checkbox" /></td>
                                            </tr>
                                        Next
                                    </tbody>
                                </table>
                            </div>

                        </div>
                        <div id="showTransactionsDiv" class="col-md-9">
                            <legend class="text-center">Transactions</legend>
                            <table id="orderTable" style="background-color:white;" class="table table-bordered table-striped table-condensed datatable">
                                <thead>
                                    <tr>
                                        <td>Order Number</td>
                                        <td>Item Number</td>
                                        <td>Description</td>
                                        <td>Priority</td>
                                        <td>Transaction Quantity</td>
                                        <td>Unit of Measure</td>
                                        <td>Batch ID</td>
                                        <td>Trans. Type</td>
                                        <td>De-Alloc?</td>
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
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/De-Allocate%20Orders/De-Allocate%20Orders.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/De-Allocate%20Orders/De-AllocatedFilters.js"></script>
