<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="rp_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="rp_label" aria-hidden="true">
    <div class="modal-dialog" style="width:1000px;margin-top:10px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="rp_label">Reprocess Transaction Detail</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Order Number</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPOrder" />
                    </div>
                    <div class="col-md-3">
                        <label>Item Number</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPItem"  />
                    </div>
                    <div class="col-md-3">
                        <label>Transaction Type</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPTransType"  />
                    </div>
                    <div class="col-md-3">
                        <label>Line Number</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPLine"  />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Description</label>
                        <textarea rows="2" class="form-control no-horizontal" readonly="readonly" id="RPDescription"></textarea>
                    </div>
                    <div class="col-md-6">
                        <label>Notes</label>
                        <textarea class="form-control no-horizontal" id="RPNotes" rows="2" readonly="readonly"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Import Date</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPImportDate"  />
                    </div>
                    <div class="col-md-3">
                        <label>Import By</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPImportBy" />
                    </div>
                    <div class="col-md-3">
                        <label>Import Filename</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPFilename" />
                    </div>
                    <div class="col-md-3">
                        <label>Required Date</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPRequired"  />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Unit of Measure</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPUoM" />
                    </div>
                    <div class="col-md-3">
                        <label>Lot Number</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPLot" />
                    </div>
                    <div class="col-md-3">
                        <label>Expiration Date</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPExpDate" />
                    </div>
                    <div class="col-md-3">
                        <label>Serial Number</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPSerialNumber" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Revision</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPRevision" />
                    </div>
                    <div class="col-md-3">
                        <label>Warehouse</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPWarehouse" />
                    </div>
                    <div class="col-md-3">
                        <label>Location</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPLocation"  />
                    </div>
                    <div class="col-md-3">
                        <label>Transaction Quantity</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPTransQty"  />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Priority</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPPriority"  />
                    </div>
                    <div class="col-md-3">
                        <label>Label</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPLabel"  />
                    </div>
                    <div class="col-md-3">
                        <label>Host Transaction ID</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPHTID"  />
                    </div>
                    <div class="col-md-3">
                        <label>Emergency</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPEmergency"  />
                    </div>
                </div>
                <div class="row">
                    @For x As Integer = 0 to Model.aliases.UserFields.count - 1
                        @<div class="col-md-3">
                            <label>@Model.aliases.UserFields(x)</label>
                            <textarea class="form-control no-horizontal" id="@("RPUF" & (x + 1))"  readonly="readonly" rows="2"></textarea>
                        </div>
                    Next
                    <div class="col-md-3">
                        <label>Reason</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPReason"  />
                    </div>
                    <div class="col-md-3">
                        <label>Reason Message</label>
                        <input type="text" class="form-control" readonly="readonly" id="RPMessage"  />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default " data-dismiss="modal" id="rp_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>
