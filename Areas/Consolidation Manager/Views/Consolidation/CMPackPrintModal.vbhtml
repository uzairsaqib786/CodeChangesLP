<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="CMPackPrintModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="CMPackPrintModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="CMPackPrintLabel">Print Options</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <p>New lines exists for this order number. To print only new lines press the <b>Print New Lines</b> button. 
                        To Print all lines press the <b>Print All Lines</b> button. To cancel printing press the <b>Close</b> button
                        on the bottom right.
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" id="PrintNewLines">Print New Lines</button>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" id="PrintAllLines">Print All Lines</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="CMPackPrintDismiss">Close</button>
                    </div> 
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/Consolidation/CMPackPrintModal.js"></script>