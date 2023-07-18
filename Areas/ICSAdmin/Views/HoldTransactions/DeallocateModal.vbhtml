<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="deallocate_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="deallocate_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="deallocate_label">Hold Reason</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12" id="deallocate_alerts">

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <textarea rows="5" class="no-horizontal form-control" id="deallocate_textarea" maxlength="255" placeholder="Hold Reason">Operator placed transaction on hold.</textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="deallocate_dismiss">Cancel Hold</button>
                <button type="button" class="btn btn-primary" id="deallocate_submit">Hold and Deallocate</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ICSAdmin/Scripts/HoldTransactions/DeallocateModal.js"></script>