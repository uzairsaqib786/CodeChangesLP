<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div id="nanReel_alerts">

        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <label>Minimum RTS Reel Quantity: </label><input maxlength="9" data-colname="[Inventory].[Minimum RTS Reel Quantity]" type="text" class="form-control inv-num" id="minReelQty" placeholder="Minimum RTS Reel Qty" />
    </div>
</div>
<div class="row" style="padding-top:15px;">
    <div class="col-md-12">
        <label>Include in Auto RTS Update <input type="checkbox" data-colname="[Inventory].[Include In Auto RTS Update]" class="inv-bool" id="RTSUpdate" /></label>
    </div>
</div>
<div class="row" style="padding-top:15px;">
    <div class="col-md-12">
        <button type="button" class="btn btn-primary" id="updateRTSReel">Update Minimum RTS Reel Quantity For All <i style="top:0;" class="glyphicon glyphicon-resize-full"></i></button>
    </div>
</div>