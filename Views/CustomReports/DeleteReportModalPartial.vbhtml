<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="deletereport_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="deletereport_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="deletereport_label">Choose Delete Method</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        The reference to the selected file will be deleted.  Choose whether the file itself will also be deleted or kept in case it may be needed sometime in the future.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-warning btn-block" id="DeleteKeepFile">Keep File</button>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-danger btn-block" id="DeleteDeleteFile">Erase File (PERMANENT!)</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="deletereport_dismiss">Cancel (The file and reference will BOTH be KEPT.)</button>
            </div>
        </div>
    </div>
</div>