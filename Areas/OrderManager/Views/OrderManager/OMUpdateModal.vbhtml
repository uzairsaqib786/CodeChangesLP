<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="OMUpdateModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="OMUpdateModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="OMUpdateLabel">Order Manager Update Record(s)</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Order Number:</label>
                        <input type="text" class="form-control" id="OMUpdOrderNumber" disabled="disabled" />
                    </div>
                    <div class="col-md-4">
                        <label>Item Number:</label>
                        <input type="text" class="form-control" id="OMUpdItemNumber" disabled="disabled" />
                    </div>
                    <div class="col-md-4">
                        <label>Supplier Item ID:</label>
                        <input type="text" class="form-control" id="OMUpdSuppItemID" disabled="disabled" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Required Date:</label>
                        <input type="text" class="form-control date-picker" id="OMUpdRequiredDate" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Notes:</label>
                        <input type="text" class="form-control" id="OMUpdNotes" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>Priority:</label>
                        <input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.smallint)" class="form-control" id="OMUpdPriority" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field1:</label>
                        <input type="text" class="form-control" id="OMUpdUserField1" maxlength="255" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>User Field2:</label>
                        <input type="text" class="form-control" id="OMUpdUserField2" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field3:</label>
                        <input type="text" class="form-control" id="OMUpdUserField3" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field4:</label>
                        <input type="text" class="form-control" id="OMUpdUserField4" maxlength="255" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>User Field5:</label>
                        <input type="text" class="form-control" id="OMUpdUserField5" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field6:</label>
                        <input type="text" class="form-control" id="OMUpdUserField6" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field7:</label>
                        <input type="text" class="form-control" id="OMUpdUserField7" maxlength="255" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>User Field8:</label>
                        <input type="text" class="form-control" id="OMUpdUserField8" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field9:</label>
                        <input type="text" class="form-control" id="OMUpdUserField9" maxlength="255" />
                    </div>
                    <div class="col-md-4">
                        <label>User Field10:</label>
                        <input type="text" class="form-control" id="OMUpdUserField10" maxlength="255" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Emergency</label>
                        <select class="form-control" id="OMUpdEmergency">
                            <option value="True">True</option>
                            <option value="False">False</option>
                        </select>
                    </div>
                    <div class="col-md-4">
                        <label>Label</label>
                        <select class="form-control" id="OMUpdLabel">
                            <option value="True">True</option>
                            <option value="False">False</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="col-md-offset-8 col-md-2">
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="OMUpdateDismiss">Close</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" id="OMUpdateModalButt">Update</button>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/OrderManager/Scripts/OrderManager/OMUpdateModal.js"></script>