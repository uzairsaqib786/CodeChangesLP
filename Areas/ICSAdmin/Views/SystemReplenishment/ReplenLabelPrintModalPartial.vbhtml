<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="ReplenLabelPrintModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="ReplenLabelPrintModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:1200px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="ReplenLabelPrintModalTitle">Print Replen Labels</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        <button type="button" class="btn btn-lg btn-block btn-primary" data-dismiss="modal" id="PrintReplenLabelAll">Print Labels for All Replens</button>
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-lg btn-block btn-primary" data-dismiss="modal" id="PrintReplenLabelNotPrinted">Print Labels for Unprinted Replens</button>
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-lg btn-block btn-primary" data-dismiss="modal" id="PrintReplenLabelFiltered">Print Labels for Current Display</button>
                    </div>
                </div>
                <div class="row" style="padding-top:1%">
                    <div class="col-md-4 col-md-offset-8">
                        <label>Current Display Sort:</label>
                        <select class="form-control" id="CurrDispSort">
                            <option value="ItemSort">Item, Order, Pick Loc</option>
                            <option value="PickLocSort">Pick Loc, Order, Item</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-push-6 col-md-6">
                        <button type="button" class="btn btn-block btn-default" data-dismiss="modal" id="ItemNumberFilterModalDismiss">Close</button>                   
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ICSAdmin/Scripts/SystemReplenishment/ReplenLabelPrintModal.js"></script>