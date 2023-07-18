<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="iqModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="iqModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="iqModalLabel">Adjust Location Quantity</h4>
            </div>
            <div class="modal-body">
                <div id="modalAlert">

                </div>
                <div class="panel panel-default" style="background-color:white;">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Item ID</label>
                                    <input id="itemNumModal" type="text" class="form-control" readonly />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Description</label>
                                    <input id="descriptionModal" type="text" class="form-control" readonly />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Location Zone</label>
                                    <input id="locZoneModal" type="text" class="form-control" readonly />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Qty Pick</label>
                                    <input id="qtyPickModal" type="text" class="form-control" readonly />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Location</label>
                                    <input id="locationModal" type="text" class="form-control" readonly />
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Qty Put Away</label>
                                    <input id="qtyPutModal" type="text" class="form-control" readonly />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Current Location Qty</label>
                                    <input id="currentLocModal" type="text" class="form-control" readonly />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Current Max Qty</label>
                                    <input id="maxQtyCurrent" type="text" class="form-control" readonly />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Current Min Qty</label>
                                    <input id="minQtyCurrent" type="text" class="form-control" readonly />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Enter New Quantity</label>
                            <input maxlength="9" oninput="setNumericInRange($(this), 0, SqlLimits.numerics.int.max)" id="newQtyModal" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Reason for Adjustment</label>
                            <input id="qtyreasonModal" class="form-control" maxlength="255" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default modalClose" data-dismiss="modal">Close</button>
                <button id="qtySave" type="button" class="btn btn-primary">Save changes</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/ItemQtyModal.js"></script>