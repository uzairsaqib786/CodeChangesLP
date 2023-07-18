<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="description_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="description_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="description_label"><span hidden="hidden" id="descEdit_label">Edit</span> Description</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <input type="text" hidden="hidden" id="description_sender">
                        <textarea rows="5" class="no-horizontal form-control readonly-cursor-background" id="description_textarea" placeholder="Description" maxlength="255"></textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="description_dismiss">Close</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="description_submit" style="display: none;">Submit</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/DescriptionModal.js"></script>