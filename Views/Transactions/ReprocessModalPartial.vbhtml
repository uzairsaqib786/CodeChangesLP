<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.TransactionsModel
<div class="modal fade" id="reprocess_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="reprocess_label" aria-hidden="true">
    <div class="modal-dialog" style="width:1200px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="reprocess_label">Reprocess Transaction Detail</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12" id="reprocess_alerts">

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <legend class="text-center">Editable Fields</legend>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Trans. Qty</label>
                        <input type="text" class="form-control enabled" id="modal_transqty" maxlength="11" oninput="setNumericInRange($(this), 0, SqlLimits.numerics.int.max)" />
                    </div>
                    <div class="col-md-4">
                        <label>@Model.aliases.UoM</label>
                        <select id="modal_uom" class="form-control enabled">
                            @For Each entry In Model.UoMs
                                @<option>@entry</option>
                            Next
                        </select>
                    </div>
                    <div class="col-md-4">
                        <label>Serial Number</label>
                        <input type="text" class="form-control enabled" id="modal_sn" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Lot Number</label>
                        <input type="text" class="form-control enabled" id="modal_lot" maxlength="50" />
                    </div>
                    <div class="col-md-4">
                        <label>Expiration Date</label>
                        <input type="text" class="form-control enabled date-picker" id="modal_expdate" maxlength="50" />
                    </div>
                    <div class="col-md-4">
                        <label>Revision</label>
                        <input type="text" class="form-control enabled" id="modal_revision" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Notes</label>
                        <textarea class="form-control no-horizontal enabled" placeholder="Notes" maxlength="255" id="modal_reprocessnotes"></textarea>
                    </div>
                    <div class="col-md-4">
                        <label>@Model.aliases.UserFields(0)</label>
                        <textarea class="form-control no-horizontal enabled" placeholder="@Model.aliases.UserFields(0)" maxlength="255" id="modal_uf1"></textarea>
                    </div>
                    <div class="col-md-4">
                        <label>@Model.aliases.UserFields(1)</label>
                        <textarea class="form-control no-horizontal enabled" placeholder="@Model.aliases.UserFields(0)" maxlength="255" id="modal_uf2"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Host Transaction ID</label>
                        <input type="text" class="form-control enabled" id="modal_htid" maxlength="50" />
                    </div>
                    <div class="col-md-4">
                        <label>Required Date</label>
                        <input type="text" class="form-control enabled date-picker" id="modal_reqdate" />
                    </div>
                    <div class="col-md-4">
                        <label>Batch Pick ID</label>
                        <input type="text" class="form-control enabled" id="modal_batch" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Line Number</label>
                        <input type="text" class="form-control enabled" id="modal_linenum" maxlength="11" />
                    </div>
                    <div class="col-md-4">
                        <label>Line Sequence</label>
                        <input type="text" class="form-control enabled" id="modal_lineseq" maxlength="11" />
                    </div>
                    <div class="col-md-4">
                        <label>Revision</label>
                        <input type="text" class="form-control enabled" id="modal_revision" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Priority</label>
                        <input type="text" class="form-control enabled" id="modal_priority" maxlength="5" />
                    </div>
                    <div class="col-md-4">
                        <label>Warehouse</label>
                        <select class="form-control enabled" id="modal_warehouse">
                            @For Each entry In Model.warehouses
                                @<option>@entry</option>
                            Next
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label>Label</label>
                        <input type="checkbox" id="modal_label" class="enabled" />
                    </div>
                    <div class="col-md-2">
                        <label>Emergency</label>
                        <input type="checkbox" id="modal_emergency" class="enabled" />
                    </div>
                </div>
                <div class="row">
                    
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <legend class="text-center">Locked Fields</legend>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Order Number</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_order">
                    </div>
                    <div class="col-md-4">
                        <label>@Model.aliases.ItemNumber</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_item">
                    </div>
                    <div class="col-md-4">
                        <label>Transaction Type</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_transtype">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Transaction Date/Time</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_dt">
                    </div>
                    <div class="col-md-4">
                        <label>Record Created By</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_user">
                    </div>
                    <div class="col-md-4">
                        <label>Zone</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_zone">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Carousel</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_carousel">
                    </div>
                    <div class="col-md-3">
                        <label>Row</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_row">
                    </div>
                    <div class="col-md-3">
                        <label>Shelf</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_shelf">
                    </div>
                    <div class="col-md-3">
                        <label>Bin</label>
                        <input disabled="disabled" type="text" class="form-control" id="modal_bin">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Reason</label>
                        <textarea id="modal_reason" class="form-control no-horizontal" rows="3" disabled="disabled"></textarea>
                    </div>
                    <div class="col-md-4">
                        <label>Reason Message</label>
                        <textarea id="modal_reasonmessage" class="form-control no-horizontal" rows="3" disabled="disabled"></textarea>
                    </div>
                    <div class="col-md-4">
                        <label>Item Description</label>
                        <textarea id="modal_description" class="form-control no-horizontal" rows="3" disabled="disabled"></textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="reprocess_dismiss">Close without Saving Changes</button>
                <button type="button" class="btn btn-primary" id="reprocess_submit">Save Changes and Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Transaction Journal/Reprocess/ReprocessModal.js"></script>