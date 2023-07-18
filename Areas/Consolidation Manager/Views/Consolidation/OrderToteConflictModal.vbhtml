<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="OrderToteConflictModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="ItemSelectedModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="ItemSelectedLabel">Consolidation Order/Tote Conflict</h4>
            </div>
            <div class="modal-body">
                <h3>A Order and Tote were both found that match the entered value, select which value you entered.</h3>
                <div class="row">
                    <div class="col-md-6">
                        <button id="valIsOrder" class="btn btn-primary btn-lg btn-block">Order Number</button>
                    </div>
                    <div class="col-md-6">
                        <button id="valIsTote" class="btn btn-primary btn-lg btn-block">Tote ID</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/Consolidation/OrderToteConflictModal.js"></script>
