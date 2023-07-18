<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="PrintConfig_Modal" tabindex="-1" role="dialog" aria-labelledby="PrintConfig_label" aria-hidden="true" data-backdrop="static">
    <div class="modal-dialog" style="width:80%;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="PrintConfig_label">Configure Workstation Printers</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Report Printer</label>
                        <select id="ReportPrintSel" class="form-control"></select>
                    </div>
                    <div class="col-md-6">
                        <label>Label Printer</label>
                        <select id="LabelPrintSel" class="form-control"></select>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="PrintConfig_submit">Save Changes</button>
                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Global/PrinterConfig.js"></script>
