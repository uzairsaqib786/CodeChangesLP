<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="GenOrderTransModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="GenOrderTransModal" aria-hidden="true">
    <div class="modal-dialog" style="width:99%;margin-top:10px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="GenOrderTransModalTitle">Add Transaction</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="alert alert-danger" id="GenorderTransModalAlert" style="display:none" role="alert"></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            Required Info
                                        </h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>Item Number</label>
                                                <input maxlength="50" type="text" id="GenOrderTransItemNum" placeholder="Item Number" class="form-control" />
                                            </div>
                                            <div class="col-md-6">
                                                <label>Quantity</label>
                                                <input maxlength="50" type="number" id="GenOrderTransQty" placeholder="Quantity" class="form-control" />
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
                                            Transaction Dates Info
                                        </h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>Required Date</label>
                                                <input type="text" class="form-control date-picker" id="GenOrderTransReqDate">
                                            </div>
                                            <div class="col-md-6">
                                                <label>Expiration Date</label>
                                                <input type="text" class="form-control date-picker" id="GenOrderTransExpDate">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Transaction Info
                                </h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Line Number</label>
                                        <input type="number" id="GenOrderTransLineNum" placeholder="Line Number" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Line Sequence</label>
                                        <input type="number" id="GenOrderTransLineSeq" placeholder="Line Sequnece" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Priority</label>
                                        <input type="number" id="GenOrderTransPriority" placeholder="Priority" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Tote Number</label>
                                        <input type="number" id="GenOrderTransToteNum" placeholder="Tote Number" class="form-control" />
                                    </div>
                                </div>
                                <div class="row" style="padding-top:15px;">
                                    <div class="col-md-3">
                                        <label>Batch Pick ID</label>
                                        <input type="text" id="GenOrderTransBatchPickID" placeholder="Batch Pick ID" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Warehouse</label>
                                        <input type="text" id="GenOrderTransWarehouse" placeholder="Warehouse" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Lot Number</label>
                                        <input type="text" id="GenOrderTransLotNumber" placeholder="Lot Number" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Serial Number</label>
                                        <input type="text" id="GenOrderTransSerialNumber" placeholder="Serial Number" class="form-control" />
                                    </div>
                                </div>
                                <div class="row" style="padding-top:15px;">
                                    <div class="col-md-3">
                                        <label>Host Transaction ID</label>
                                        <input type="text" id="GenOrderTransHostTransID" placeholder="Host Transaction ID" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Emergency</label>
                                        <select class="form-control" id="GenOrderTransEmergency">
                                            <option>No</option>
                                            <option>Yes</option>
                                        </select>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                                            <label class="control-label">Notes</label>
                                            <input type="text" readonly="readonly" class="form-control notes-modal-edit modal-launch-style" id="GenOrderTransNotes" />
                                            <i class="glyphicon glyphicon-resize-full form-control-feedback notes-modal-edit modal-launch-style"></i>
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
                                <h3 class="panel-title">
                                    Transaction User Fields Info
                                </h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>User Field 1</label>
                                        <input type="text" id="GenOrderTransUserField1" placeholder="User Field 1" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>User Field 2</label>
                                        <input type="text" id="GenOrderTransUserField2" placeholder="User Field 2" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>User Field 3</label>
                                        <input type="text" id="GenOrderTransUserField3" placeholder="User Field 3" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>User Field 4</label>
                                        <input type="text" id="GenOrderTransUserField4" placeholder="User Field 4" class="form-control" />
                                    </div>
                                </div>
                                <div class="row" style="padding-top:15px;">
                                    <div class="col-md-3">
                                        <label>User Field 5</label>
                                        <input type="text" id="GenOrderTransUserField5" placeholder="User Field 5" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>User Field 6</label>
                                        <input type="text" id="GenOrderTransUserField6" placeholder="User Field 6" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>User Field 7</label>
                                        <input type="text" id="GenOrderTransUserField7" placeholder="User Field 7" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>User Field 8</label>
                                        <input type="text" id="GenOrderTransUserField8" placeholder="User Field 8" class="form-control" />
                                    </div>
                                </div>
                                <div class="row" style="padding-top:15px;">
                                    <div class="col-md-3">
                                        <label>User Field 9</label>
                                        <input type="text" id="GenOrderTransUserField9" placeholder="User Field 9" class="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>User Field 10</label>
                                        <input type="text" id="GenOrderTransUserField10" placeholder="User Field 10" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-success" id="GenOrderTransModalSave">Save Transaction</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="GenOrderTransModalClose">Close</button>
            </div>
        </div>
    </div>
</div>