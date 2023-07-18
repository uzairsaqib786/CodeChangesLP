<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="notes_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="notes_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="notes_label">Notes</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" hidden="hidden" id="notes_sender" />
                <div class="row">
                    <div class="col-md-12">
                        <textarea rows="5" class="no-horizontal form-control readonly-cursor-background" id="notes_textarea" placeholder="Notes"></textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="notes_dismiss">Close</button>
                <button hidden="hidden" type="button" class="btn btn-primary" data-dismiss="modal" id="notes_submit">Submit</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/NotesModal.js"></script>