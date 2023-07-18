<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="print_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="print_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h3 class="modal-title" id="print_label">Print Label(s)</h3>
            </div>
            <div class="modal-body" id="print_body">
                <div class="row print-range">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <label>Begin Location</label>
                                <input type="text" class="form-control" placeholder="Begin Location" maxlength="50" id="BeginLocation" />
                            </div>
                            <div class="col-md-6">
                                <label>End Location</label>
                                <input type="text" class="form-control" placeholder="End Location" maxlength="50" id="EndLocation" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Qty Selected</label> <span class="label label-default" id="QtySelected">0</span>
                            </div>
                            <div class="col-md-6">
                                <label>Group By Location</label>
                                <div class="toggles toggle-modern" data-toggle-ontext="Yes" data-toggle-offtext="No" id="groupBy"></div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row print-selected">
                    <div class="col-md-12">
                        Click Print Selected to print only the selected label.  Click Print Range to choose a range of locations to print labels for.
                    </div>
                </div>
            </div>
            <div class="modal-footer" id="print_footer">
                <button class="btn btn-primary print-selected Print-Label" id="PrintSelected">Print Selected</button>
                <button class="btn btn-primary Print-Label" id="PrintRange">Print Range</button>
                <button class="btn btn-primary" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/InventoryMap/PrintModal.js"></script>