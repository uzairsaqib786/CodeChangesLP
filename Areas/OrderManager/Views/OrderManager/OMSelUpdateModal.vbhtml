<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="OMSelUpdateModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="OMSelUpdateModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="OMSelUpdateLabel">Apply Changes to All Transactions</h4>
            </div>
            <div class="modal-body">
                <div class="row top-spacer">
                    <div class="col-md-12">
                        Check any fields that you wish to save the values for for <strong>EVERY</strong> transaction in the order you have selected.  Leave the fields unchecked if you only wish to change the value for the selected transaction.
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>Required Date</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckRequiredDate" />
                    </div>
                    <div class="col-md-2">
                        <label>Notes</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckNotes" />
                    </div>
                    <div class="col-md-2">
                        <label>Priority</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckPriority" />
                    </div>
                    <div class="col-md-2">
                        <label>User Field1</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField1" />
                    </div>
                    <div class="col-md-2">
                        <label>User Field2</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField2" />
                    </div>
                    <div class="col-md-2">
                        <label>User Field3</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField3" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>User Field4</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField4" />
                    </div>
                    <div class="col-md-2">
                        <label>User Field5</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField5" />
                    </div>
                    <div class="col-md-2">
                        <label>User Field6</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField6" />
                    </div>
                    <div class="col-md-2">
                        <label>User Field7</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField7" />
                    </div>
                    <div class="col-md-2">
                        <label>User Field8</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField8" />
                    </div>
                    <div class="col-md-2">
                        <label>User Field9</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField9" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>User Field10</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckUserField10" />
                    </div>
                    <div class="col-md-2">
                        <label>Emergency</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckEmergency" />
                    </div>
                    <div class="col-md-2">
                        <label>Label</label>
                        <input type="checkbox" disabled="disabled" id="OMCheckLabel" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="col-md-offset-8 col-md-2">
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="OMSelUpdateDismiss">Close</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" data-dismiss="modal" id="OMSelUpdateButt">Update</button>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/OrderManager/Scripts/OrderManager/OMSelUpdateModal.js"></script>
