<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="InsertShippingItemModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="InsertShippingItemModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="InsertShippingItemLabel">Add New Item to Shipment</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Order Number</label><input maxlength="50" type="text" id="InsertShippingItemOrderNumber" class="form-control" disabled="disabled" />
                    </div>
                    <div class="col-md-4">
                        <label>Container ID</label><input maxlength="50" type="text" id="InsertShippingItemContID" class="form-control" />
                    </div>
                    <div class="col-md-offset-1 col-md-3" style="padding-top:25px;">
                        <button class="btn btn-primary form-control" id="AddShippingItem" data-dismiss="modal" disabled="disabled">Add Item to Shipment</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="InsertShippintItemDismiss">Close</button>
                    </div> 
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/Shipping/InsertShippingItemModal.js"></script>
