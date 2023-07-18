<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="ItemSelectedModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="ItemSelectedModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="ItemSelectedLabel">Item Selected</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label id="IdentLabel">Order Number</label>
                        <input type="text" class="form-control" id="IdentModal" maxlength="50" disabled="disabled"/>
                    </div>
                    <div class="col-md-6">
                        <label id="ColLabel"></label>
                        <input type="text" class="form-control" id="ColumnModal" disabled="disabled" maxlength="50" />
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="ItemSelectedTable">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Item Number</th>
                                    <th>Supplier Item ID</th>
                                    <th>Warehouse</th>
                                    <th>Completed Qty</th>
                                    <th>Tote ID</th>
                                    <th>Serial Number</th>
                                    <th>User Field 1</th>
                                    <th>Lot Number</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-offset-10 col-md-2">
                    <button type="button" class="btn btn-success" id="btnVerifyAll">Verify All</button>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row"
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="ItemSelectedDismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>  
<script src="~/Areas/Consolidation Manager/Scripts/Consolidation/ItemSelectedModal.js"></script>