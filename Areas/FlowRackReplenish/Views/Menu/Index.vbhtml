<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="col-md-12">
    <div class="panel panel-info">
        <div class="panel-heading">
            FlowRack Replenishment
        </div>
        <div style="margin: 10px 0 0 10px" >
            <font id ="CurrentZone" size="4">
                <strong>
                    Current Zone:
            </strong>
            </font>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="text-center col-md-4 col-md-offset-4">
                    <div class="col-md-12">
                        <div class="alert alert-warning"  id="alertarea" role="alert">

                        </div>
                    </div>
                    <div class="col-md-12">
                        <label class="control-label" for="itemnumscan">Scan Item</label>
                        <div class="input-group">
                            <input class="form-control" type="text" id="itemnumscan" placeholder="Item Number" maxlength="50" autofocus />
                            <span class="input-group-btn">
                                <button type="button" id="clearitemnum" class="btn btn-danger">
                                    <span class="glyphicon glyphicon-remove"></span>
                                </button>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-12" id="LocationRow" style="margin-top:20px">
                        <label class="control-label" for="itemLocation" style="margin-top:10px;">Select Location</label>
                        <input class="form-control typeahead" type="text" id="itemLocation" placeholder="Select Location" maxlength="50" />
                    </div>
                    <div class="col-md-12" id="itemQtyRow" style="margin-top:20px;">
                        <label class="control-label" for="itemQty" style="margin-top:10px;">Enter Quantity</label>
                        <div class="input-group">
                            <input class="form-control" type="number" id="itemQty" placeholder="Item Quantity" min="0" max="9999999999"/>
                            <span class="input-group-btn">
                                <button type="button" id="itemQtyNumPad" class="btn btn-default" data-numpad="numpad" data-input-target="#itemQty">
                                    <span class="glyphicon glyphicon-th-large"></span>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-md-offset-4 text-center">
                <button type="button" id="submitBtn" class="btn btn-primary btn-xl btn-block" style="margin-top:30px">Submit</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/FlowRackReplenish/Scripts/FlowRackReplenish.js"></script>
