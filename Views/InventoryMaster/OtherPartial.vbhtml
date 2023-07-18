<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div id="nanOther_alerts">

        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <label>Unit Cost:</label>
        <div class="input-group">
            <span class="input-group-addon">$</span>
            <input maxlength="11" type="text" class="form-control inv-edit inv-num" data-colname="[Inventory].[Unit Cost]" placeholder="Unit Cost" id="uCost" value="0">
        </div>
    </div>
    <div class="col-md-4">
        <label>Supplier ID: </label><input maxlength="50" type="text" data-colname="[Inventory].[Supplier Number]" class="form-control inv-edit inv-text" id="supID" placeholder="Supplier ID" />
    </div>
    <div class="col-md-4">
        <label>Manufacturer ID: </label><input maxlength="50" type="text" data-colname="[Inventory].[Manufacturer]" class="form-control inv-edit inv-text" id="manuID" placeholder="Manufacturer ID" />
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <label>Special Features: </label>
        <textarea maxlength="255" class="form-control no-horizontal inv-edit inv-text" data-colname="[Inventory].[Special Features]" placeholder="Special Features" rows="5" id="specialFeatures"></textarea>
    </div>
</div>
