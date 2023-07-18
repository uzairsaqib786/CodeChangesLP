<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.AliasModel
@Code
    ViewData("Title") = "Manual Transactions"
    ViewData("PageName") = "&nbsp; | &nbsp; Manual Transactions"
    Layout = PickPro_Web.GlobalFunctions.chooseLayoutFile(Model.App)
End Code
<div class="container-fluid">
    <div class="row" style="padding-bottom:10px;">
        <div class="col-md-12">
            <ul class="nav nav-tabs" role="tablist">
                <li class="active"><a id="ManTranView" href="#ManTran" role="tab" data-toggle="tab">Generate Transaction</a></li>
                <li><a id="ManOrderView" href="#ManOrder" role="tab" data-toggle="tab">Generate Order</a></li>
            </ul>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="tab-content">
                <div class="tab-pane active" id="ManTran">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        Temporary Manual Order Number
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group has-feedback has-warning forced-typeahead" style="margin-bottom:0px;">
                                                <input maxlength="50" type="text" id="OrderNumber" placeholder="Order Number" class="form-control" data-placement="top" data-original-title="You must select an item from the suggestions provided in order to edit a transaction!" />
                                                <span class="glyphicon glyphicon-warning-sign form-control-feedback" data-toggle="tooltip" data-placement="top" style="top:0px;"></span>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <button class="btn btn-primary" type="button" id="ClearOrder">Clear</button>
                                            <button class="btn btn-primary manual-modal" data-toggle="tooltip" data-placement="top" data-original-title="Add New Transaction"><span class="glyphicon glyphicon-plus"></span></button>
                                            <button class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Save Transaction" id="SaveTransaction"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                                            <button class="btn btn-primary Print-Label" data-toggle="tooltip" data-placement="top" data-original-title="Print Label" id="PrintMT"><span class="glyphicon glyphicon-print"></span></button>
                                            <button class="btn btn-danger" data-toggle="tooltip" data-placement="top" data-original-title="Delete Transaction" id="deleteTransaction"><span class="glyphicon glyphicon-trash"></span></button>
                                            <div class="btn-group">
                                                <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown" id="PostTransaction">Post Transaction <span class="caret"></span></button>
                                                <ul class="dropdown-menu" role="menu">
                                                    <li><a id="PostDelete">Post and Delete Transaction</a></li>
                                                    <li><a id="PostSave">Post and Save Transaction</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="MTAlerts">

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Item Details</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group has-feedback" style="margin-bottom:0px;">
                                                <label class="control-label">@Model.ItemNumber</label>
                                                <input type="text" readonly="readonly" class="form-control modal-launch-style" id="ItemNumber" />
                                                <i class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style ItemNumberID"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group has-feedback" style="margin-bottom:0px;">
                                                <label class="control-label">Supplier Item ID</label>
                                                <input type="text" readonly="readonly" class="form-control supplieritemid-modal modal-launch-style" id="SupplierItemID" />
                                                <i class="glyphicon glyphicon-resize-full form-control-feedback supplieritemid-modal modal-launch-style"></i>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Expiration Date</label>
                                            <input type="text" class="form-control date-picker" id="ExpirationDate" maxlength="50" />
                                        </div>
                                        <div class="col-md-4">
                                            <label>Revision</label>
                                            <input class="form-control" id="Revision" placeholder="Revision" maxlength="50" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                                                        <label class="control-label">Description</label>
                                                        <input type="text" readonly="readonly" class="form-control description-modal modal-launch-style" id="ItemDescription" />
                                                        <i class="glyphicon glyphicon-resize-full form-control-feedback description-modal modal-launch-style"></i>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                                                        <label class="control-label">Notes</label>
                                                        <input type="text" readonly="readonly" class="form-control notes-modal-edit modal-launch-style" id="Notes" />
                                                        <i class="glyphicon glyphicon-resize-full form-control-feedback notes-modal-edit modal-launch-style"></i>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Lot Number</label>
                                                    <input class="form-control" id="LotNumber" placeholder="Lot Number" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Serial Number</label>
                                                    <input class="form-control" id="SerialNumber" placeholder="Serial Number" maxlength="50" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                                                        <label class="control-label">@Model.UoM</label>
                                                        <input type="text" readonly="readonly" class="form-control uom-modal modal-launch-style" id="UnitOfMeasure" />
                                                        <i class="glyphicon glyphicon-resize-full form-control-feedback uom-modal modal-launch-style"></i>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <label>User Fields</label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <button class="btn btn-primary userfields-modal-manual">View/Set User Fields </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Location</h3>
                                </div>
                                <div class="panel-body" style="font-size:16px;">
                                    <table style="background-color:white;" class="table table-condensed table-bordered table-striped">
                                        <tbody>
                                            <tr>
                                                <td style="width:50%">Zone</td>
                                                <td class="loc" id="Zone"></td>
                                            </tr>
                                            <tr>
                                                <td>Carousel</td>
                                                <td class="loc" id="Carousel"></td>
                                            </tr>
                                            <tr>
                                                <td>Row</td>
                                                <td class="loc" id="Row"></td>
                                            </tr>
                                            <tr>
                                                <td>Shelf</td>
                                                <td class="loc" id="Shelf"></td>
                                            </tr>
                                            <tr>
                                                <td>Bin</td>
                                                <td class="loc" id="Bin"></td>
                                            </tr>
                                            <tr>
                                                <td>Quantity Allocated Pick</td>
                                                <td class="loc" id="LocQtyPick"></td>
                                            </tr>
                                            <tr>
                                                <td>Quantity Allocated Put Away</td>
                                                <td class="loc" id="LocQtyPut"></td>
                                            </tr>
                                            <tr>
                                                <td>Total Quantity</td>
                                                <td class="loc" id="LocQtyTot"></td>
                                            </tr>
                                            <tr>
                                                <td>Inventory Map ID</td>
                                                <td class="loc" id="InvMapID"></td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Transaction Details</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Transaction Type</label>
                                                    <select class="form-control" id="TransactionType">
                                                        <option>Pick</option>
                                                        <option>Put Away</option>
                                                        <option>Count</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Transaction Quantity</label>
                                                    <input class="form-control" id="TransQty" placeholder="Transaction Quantity" maxlength="9" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Host Transaction ID</label>
                                                    <input class="form-control" id="HostTransID" placeholder="Host Transaction ID" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                                                        <label class="control-label">Warehouse</label>
                                                        <input type="text" name="required" readonly="readonly" class="form-control warehouse-modal modal-launch-style" id="Warehouse" />
                                                        <i class="glyphicon glyphicon-resize-full form-control-feedback warehouse-modal modal-launch-style"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Required Date</label>
                                                    <input type="text" class="form-control date-picker" id="RequiredDate" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Priority</label>
                                                    <input class="form-control" id="Priority" placeholder="Priority" maxlength="9" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Batch Pick ID</label>
                                                    <input class="form-control" id="BatchPickID" placeholder="Batch Pick ID" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Tote ID</label>
                                                    <input class="form-control" id="ToteID" placeholder="Tote ID" maxlength="50" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Line Number</label>
                                                    <input class="form-control" id="LineNumber" placeholder="Line Number" maxlength="9" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Line Sequence</label>
                                                    <input class="form-control" id="LineSequence" placeholder="Line Sequence" maxlength="9" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>Emergency</label>
                                                    <div class="toggles toggle-modern pull-right" id="Emergency" data-toggle-ontext="Yes" data-toggle-offtext="No"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="ManOrder">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                       Select Order
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Transaction Type</label>
                                            <select class="form-control" id="GenOrderTransType">
                                                <option>Pick</option>
                                                <option>Put Away</option>
                                            </select>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Order Number</label>
                                            <input maxlength="50" type="text" id="GenOrderNumber" placeholder="Order Number" class="form-control" />
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
                                    <h3 class="panel-title">
                                        Transactions
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Transaction Type</label>
                                            <input maxlength="50" type="text" id="GenOrderDispTransType" placeholder="Trans Type" class="form-control" disabled="disabled" />
                                        </div>
                                        <div class="col-md-2">
                                            <label>Displayed Order</label>
                                            <input maxlength="50" type="text" id="GenOrderDispOrder" placeholder="Order Number" class="form-control" disabled="disabled" />
                                        </div>
                                        <div class="col-md-2">
                                            <label>Tote ID for Order</label>
                                            <input maxlength="50" type="text" id="GenOrderToteID" placeholder="Tote ID" class="form-control" disabled="disabled"/>
                                        </div>
                                        <div class="col-md-6" style="padding-top:25px;">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <button class="btn btn-block btn-primary" id="GenAddTrans" disabled="disabled" data-toggle="tooltip" data-placement="top" data-original-title="Add New Transaction To Order"><span class="glyphicon glyphicon-plus"></span></button>
                                                </div>
                                                <div class="col-md-2">
                                                    <button class="btn btn-block btn-primary" id="GenEditTrans" disabled="disabled" data-toggle="tooltip" data-placement="top" data-original-title="Edit Selected Transaction"><span class="glyphicon glyphicon-pencil"></span></button>
                                                </div>
                                                <div class="col-md-2">
                                                    <button class="btn btn-block btn-danger" disabled="disabled" id="GenDeleteTrans">Delete Trans</button>
                                                </div>
                                                <div class="col-md-3">
                                                    <button class="btn btn-block btn-success" disabled="disabled" id="GenOrderPost">Post Order</button>
                                                </div>
                                                <div class="col-md-3">
                                                    <button class="btn btn-block btn-danger" data-toggle="tooltip" data-placement="top" data-original-title="Delete Order" id="GenDeleteOrder" disabled="disabled">Delete Order</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-md-12">
                                            <table class="table table-condensed table-bordered table-striped" id="GenOrderTransTable" style="background-color:#fff">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Item Number</th>
                                                        <th>Quantity</th>
                                                        <th>Line Number</th>
                                                        <th>Line Sequence</th>
                                                        <th>Priority</th>
                                                        <th>Required Date</th>
                                                        <th>Lot Number</th>
                                                        <th>Expiration Date</th>
                                                        <th>Serial Number</th>
                                                        <th>Warehouse</th>
                                                        <th>Batch Pick ID</th>
                                                        <th>Notes</th>
                                                        <th>Tote Number</th>
                                                        <th>Host Trans ID</th>
                                                        <th>Emergency</th>
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
    </div>
</div>
<link href="~/Content/toggles.css" rel="stylesheet" />
<link href="~/Content/toggles-modern.css" rel="stylesheet" />
<script src="~/Scripts/toggles.min.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/ManualTransactions/ManualTransactions.js"></script>
<script src="~/Scripts/ManualTransactions/ManualOrder.js"></script>
<script src="~/Scripts/ManualTransactions/ManualTransactionsHub.js"></script>
@Html.Partial("~/Views/ModalPartials/DescriptionModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/UnitOfMeasureModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/NotesModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/WarehouseModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/UserFieldsModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/SupplierItemIDModalPartial.vbhtml")
@Html.Partial("~/Views/ManualTransactions/AddNewModalPartial.vbhtml")
@Html.Partial("~/Views/ManualTransactions/GenOrderTransPartial.vbhtml")
@Html.Partial("~/Views/ManualTransactions/ItemNumberLocationModalPartial.vbhtml")