<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="UpdatePendingModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="UpdatePendingModal" aria-hidden="true">
    <div class="modal-dialog" style="width:1200px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="UpdatePendingLabel">Update Open Transactions Pending</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Order Number</label>
                        <input type="text" id="OrderNumber" class="form-control" maxlength="50" />
                    </div>
                    <div class="col-md-4">
                        <label>Transaction Type</label>
                        <select class="form-control" id="TransTypeDropdown">
                            <option value="Pick">Pick</option>
                            <option value="Count">Count</option>
                            <option value="Put Away">Put Away</option>
                        </select>
                    </div>
                    <div class="col-md-4">
                        <label>Warehouse</label>
                        <select id="OMWarehouses" class="form-control">
                            <option value=""></option>
                            @For Each warehouse In Model.Warehouses
                                @<option value="@warehouse">@warehouse</option>
                            Next
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Processing By</label>
                        <input type="text" id="ProcessBy" class="form-control" maxlength="50" value="@Model.ProcessingBy" />
                    </div>
                    <div class="col-md-6">
                        <label>Import By</label>
                        <input type="text" id="ImportBy" class="form-control" maxlength="50" value="@Model.ProcessingBy" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Import Date</label>
                        <input type="text" id="ImportDate" class="form-control date-picker"/>
                    </div>
                    <div class="col-md-6">
                        <label>Import File</label>
                        <input type="text" id="ImportFile" class="form-control" maxlength="50" value="Create Pending Transaction" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Item Number</label>
                        <input type="text" id="ItemNumber" maxlength="50" class="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label>Lot Number</label>
                        <input type="text" id="LotNumber" maxlength="50" class="form-control" value="0" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Serial Number</label>
                        <input type="text" id="SerialNum" maxlength="50" class="form-control" value="0" />
                    </div>
                    <div class="col-md-6">
                        <label>Revision</label>
                        <input type="text" id="Revision" maxlength="50" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Tote ID</label>
                        <input type="text" id="ToteID" maxlength="50" class="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label>Batch Pick ID</label>
                        <input type="text" id="BatchPickID" maxlength="50" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <label>Description</label>
                        <input type="text" id="description" disabled maxlength="255" class="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label>Expiration Date</label>
                        <input type="text" id="ExpDate" class="form-control date-picker" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Unit of Measure</label>
                        <input type="text" id="UnitMeasure" disabled="disabled" maxlength="50" class="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label>Transaction Quantity</label>
                        <input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="TransQTY" class="form-control" value="1" />
                    </div>
                    <div class="col-md-4">
                        <label>Line Number</label>
                        <input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="LineNumber" class="form-control" value="0" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Priority</label>
                        <input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.smallint)" id="priority" class="form-control" />
                    </div>
                    <div class="col-md-3">
                        <label>Host Transaction ID</label>
                        <input type="text" id="hostTransID" maxlength="50" class="form-control" />
                    </div>
                    <div class="col-md-3">
                        <label>Required Date</label>
                        <input type="text" id="ReqDate" class="form-control date-picker" />
                    </div>
                    <div class="col-md-2">
                        <label>Cell</label>
                        <input type="text" id="Cell" maxlength="50" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Notes</label>
                        <input type="text" id="Notes" class="form-control" maxlength="255" />
                    </div>
                    <div class="col-md-2" style="padding-top:25px;">
                        <label>Emergency</label>
                        <input type="checkbox" id="emergency" name="Emergency" />
                    </div>
                    <div class="col-md-2" style="padding-top:25px;">
                        <label>Print Label</label>
                        <input type="checkbox" id="printLabel" name="Print Label" />
                    </div>
                    <div class="col-md-2" style="padding-top:25px;">
                        <label>In Process</label>
                        <input type="checkbox" id="InProcess" name="In Progress"/>
                    </div>
                </div>
                <div class="row" style="padding-top:30px;">
                    <div class="col-md-4">
                        <label>User Field1</label>
                        <input type="text" id="UserField1" class="form-control" maxlength="255" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>User Field2</label>
                        <input type="text" id="UserField2" class="form-control" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field3</label>
                        <input type="text" id="UserField3" class="form-control" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field4</label>
                        <input type="text" id="UserField4" class="form-control" maxlength="255" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>User Field5</label>
                        <input type="text" id="UserField5" class="form-control" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field6</label>
                        <input type="text" id="UserField6" class="form-control" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field7</label>
                        <input type="text" id="UserField7" class="form-control" maxlength="255" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>User Field8</label>
                        <input type="text" id="UserField8" class="form-control" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field9</label>
                        <input type="text" id="UserField9" class="form-control" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field10</label>
                        <input type="text" id="UserField10" class="form-control" maxlength="255" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="col-md-offset-8 col-md-2">
                    <button id="CreateOrdersClose" data-dismiss="modal" class="btn btn-default">Close</button>
                </div>
                <div class="col-md-2">
                    <button id="CreateOrdersSave" class="btn btn-primary">Save</button>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/OrderManager/Scripts/OrderManager/UpdatePendingModal.js"></script>
