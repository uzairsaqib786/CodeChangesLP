<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Hold Transactions Manager"
    ViewData("PageName") = "&nbsp; | &nbsp; Hold Transactions Manager"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Filters</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Transaction Type</label>
                                   
                                    <div class="row">
                                        <div class="col-md-12">
                                            @If Model.reels Then
                                                @<label class="radio-inline">
                                                    <input checked type="radio" name="reel" id="inlineRadio1" value="non"> Non-Reel
                                                </label>
                                                @<label class="radio-inline">
                                                    <input type="radio" name="reel" id="inlineRadio2" value="reel"> Reel
                                                </label>
                                                @<label class="radio-inline">
                                                    <input type="radio" name="reel" id="inlineRadio3" value="both"> Both
                                                </label>
                                            Else
                                                @<div class="row">
                                                    <div class="col-md-12">
                                                        <span>(There are no reels to display)</span>
                                                    </div>
                                                </div>
                                                @<div class="row">
                                                    <div class="col-md-12">
                                                        <label class="radio-inline">
                                                            <input checked type="radio" name="reel" id="inlineRadio1" value="non" disabled="disabled"> Non-Reel
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" name="reel" id="inlineRadio2" value="reel" disabled="disabled"> Reel
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" name="reel" id="inlineRadio3" value="both" disabled="disabled"> Both
                                                        </label>
                                                    </div>
                                                </div>
                                            End If
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Entry Filter Type</label>
                                    <select class="form-control" id="EntryType">
                                        <option value="Order Number">Order Number</option>
                                        <option value="Item Number">@Model.alias.ItemNumber</option>
                                    </select>
                                </div>
                                <div class="col-md-4">
                                    <label>Order Number</label>
                                    <input type="text" class="form-control" id="ItemOrder" placeholder="Order Number" name="@Model.alias.ItemNumber" maxlength="50" />
                                </div>
                            </div>
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
                    <h3 class="panel-title">Transactions</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <label># Entries</label>
                            <select id="pageLength" class="form-control" style="width:auto; display:inline;">
                                <option>10</option>
                                <option>15</option>
                                <option>20</option>
                                <option>25</option>
                                <option>50</option>
                                <option>100</option>
                            </select>
                            <div class="btn-group">
                                <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" id="holdTransactions">Hold and Deallocate <span class="caret"></span></button>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a href="#" id="holdSelected">Selected ONLY</a></li>
                                    <li><a href="#" id="holdBy">ALL by Order Number</a></li>
                                </ul>
                            </div>
                            <button type="button" class="btn btn-primary" id="viewReprocess">View Hold Transactions (Reprocess)</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table id="holdTransactionsTable" class="table table-hover table-bordered table-striped table-condensed" style="background-color: white;">
                                <thead>
                                    <tr>
                                        <th>Order Number</th>
                                        <th>Item Number</th>
                                        <th>Warehouse</th>
                                        <th>Location</th>
                                        <th>Transaction Type</th>
                                        <th>Transaction Quantity</th>
                                        <th>Serial Number</th>
                                        <th>Lot Number</th>
                                        <th>Line Number</th>
                                        <th>Host Transaction ID</th>
                                        <th>Tote ID</th>
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
<script src="~/Areas/ICSAdmin/Scripts/HoldTransactions/HoldTransactions.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>

@Html.Partial("DeallocateModal")