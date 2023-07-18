<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="SplitLineModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="SplitLineModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="SplitLineLabel">Split Line</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Enter the desired quantity for the new split line</label>
                        <input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" class="form-control" id="SplitLineQuant" />
                    </div>
                    <div class="col-md-offset-4 col-md-2" style="padding-top:25px;">
                        <button type="button" disabled="disabled" class="btn btn-primary" data-dismiss="modal" id="SplitLineSave">Split Line</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="SplitLineDismiss">Close</button>
                    </div> 
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/Consolidation/SplitLineModal.js"></script>