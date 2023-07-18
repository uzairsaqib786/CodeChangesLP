<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="ConfPackSelectModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="ConfPackSelectModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="ConfPackSelectLabel">Confirm and Packing Select Transaction</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Order Number</label><input maxlength="50" type="text" class="form-control" id="ConfPackSelectOrderNum" disabled="disabled"/>
                    </div>
                    <div class="col-md-6">
                        <label>Container ID</label><input maxlength="50" type="text" class="form-control" id="ConfPackSelectContID" disabled="disabled" />
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-striped" style="background-color:white;" id="ConfPackSelectTable">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Item Number</th>
                                    <th>Line Number</th>
                                    <th>Tote ID</th>
                                    <th>Order Quantity</th>
                                    <th>Picked Quantity</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="ConfPackSelectDismiss">Close</button>
                    </div> 
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/ConfirmAndPacking/ConfPackSelectModal.js"></script>