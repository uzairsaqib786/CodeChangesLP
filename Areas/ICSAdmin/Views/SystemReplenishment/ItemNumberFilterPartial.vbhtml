<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="ItemNumberFilterModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="ItemNumberFilterModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:700px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="ItemNumberFilterModalTitle">Item Number Filters</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="alert alert-warning alert-custom" role="alert">This is used to copy and paste item numbers from an excel spreadsheet.</div>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-12">
                        <textarea class="form-control" style="max-width:100%" rows="25" id="ItemNumbersToFilter"></textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-push-6 col-md-3">
                        <button type="button" class="btn btn-block btn-default" data-dismiss="modal" id="ItemNumberFilterModalDismiss">Close</button>                   
                    </div>
                    <div class="col-md-push-6 col-md-3">
                        <button type="button" class="btn btn-block btn-success" id="ItemNumberFilterModalSubmit">Filter</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ICSAdmin/Scripts/SystemReplenishment/ItemNumberFilter.js"></script>