<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="delete_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="delete_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h3 class="modal-title" id="delete_label">Delete Range</h3>
            </div>
            <div class="modal-body" id="delete_body">
                <div class="row">
                    <div class="col-md-6">
                        <label>
                            Filter
                        </label>
                        <select id="deleteColumn" class="form-control">
                            <option>Batch Pick ID</option>
                            <option>Pick Location</option>
                            <option>Put Away Location</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6"><label>Begin</label><input type="text" class="form-control" id="BeginDelete" maxlength="50" /></div>
                    <div class="col-md-6"><label>End</label><input type="text" class="form-control" id="EndDelete" maxlength="50" /></div>
                </div>
            </div>
            <div class="modal-footer" id="delete_footer">
                <button id="deleteRangeSubmit" class="btn btn-primary delete">Delete</button>
                <button class="btn btn-primary cancel" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ICSAdmin/Scripts/SystemReplenishment/DeleteModal.js"></script>
