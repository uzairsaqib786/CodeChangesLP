<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="export_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="export_label" aria-hidden="true">
    <div class="modal-dialog" style="width:600px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="export_label">Choose Export Type</h4>
            </div>
            <div class="modal-body" id="ExportType">
                <div class="row">
                    <div class="col-md-12">
                        <label>Export Filename</label>
                        <input type="text" class="form-control" id="ExportFilename" placeholder="Export Filename" maxlength="50" />
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block Print-Report">PDF</button>
                    </div>
                    <div class="col-md-6">
                        PDF Format
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block Print-Report">CSV</button>
                    </div>
                    <div class="col-md-6">
                        Comma Sepereated File(Openable in Excel)
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block Print-Report">HTML</button>
                    </div>
                    <div class="col-md-6">
                        Web page format
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block Print-Report">RTF</button>
                    </div>
                    <div class="col-md-6">
                        Rich Text Format
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block Print-Report">TXT</button>
                    </div>
                    <div class="col-md-6">
                        Text Format
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block Print-Report">XML</button>
                    </div>
                    <div class="col-md-6">
                        XML format
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block Print-Report">XPS</button>
                    </div>
                    <div class="col-md-6">
                        XPS Format
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="export_dismiss">Cancel</button>
            </div>
        </div>
    </div>
</div>
