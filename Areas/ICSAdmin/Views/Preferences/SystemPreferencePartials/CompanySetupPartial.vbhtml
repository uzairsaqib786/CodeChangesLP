<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    Dim display As String
    If Model.Admin = "Administrator" Then
        display = ""
    Else
        display = "display:none;"
    End If
End Code
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Company Information
                </h3>
            </div>
            <div class="panel-body" id="panel1">
                <div class="row">
                    <div class="col-md-3">
                        <label>Company Name</label>
                        <input type="text" class="form-control" id="cname" placeholder="Company Name" maxlength="50" />
                    </div>
                    <div class="col-md-3">
                        <label>Address 1</label>
                        <input type="text" class="form-control" id="address" placeholder="Address" maxlength="50" />
                    </div>
                    <div class="col-md-3">
                        <label>City</label>
                        <input type="text" class="form-control" id="city" placeholder="City" maxlength="50" />
                    </div>
                    <div class="col-md-3">
                        <label>State</label>
                        <input type="text" class="form-control" id="state" placeholder="State" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Early Break Time</label>
                                <input type="time" class="form-control" id="EarlyBreakTime" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Early Break Duration (Minutes)</label>
                                <input type="number" class="form-control" id="EarlyBreakDuration" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Mid Break Time</label>
                                <input type="time" class="form-control" id="MidBreakTime" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Mid Break Duration (Minutes)</label>
                                <input type="number" class="form-control" id="MidBreakDuration" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Late Break Time</label>
                                <input type="time" class="form-control" id="LateBreakTime" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Late Break Duration (Minutes)</label>
                                <input type="number" class="form-control" id="LateBreakDuration" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Company Logo</label>
                        <div class="row">
                            <div class="col-md-6">
                                <div id="uploader-wrapper" style="display:none;">
                                    <div id="fileuploader">Choose Logo File</div>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-btn">
                                        <button type="button" class="btn btn-primary" id="imageSelect">Browse</button>
                                    </div>
                                    <input readonly="readonly" type="text" class="form-control readonly-cursor-background" id="selectedImage" />
                                </div>
                                
                            </div>
                            <div class="col-md-6">
                                <button type="button" disabled="disabled" class="btn btn-primary" id="upload">Upload</button> 
                                <button type="button" class="btn btn-primary" id="currentLogo">View Current Company Logo <span class="glyphicon glyphicon-resize-full"></span></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row" style="@display">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Database and User Authentication
                </h3>
            </div>
            <div class="panel-body" id="panel2">
                <div class="row">
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <label>Domain Authentication</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="toggles toggle-modern pull-right" id="DomainAuth"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="display:none;" id="NTLM_div">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <label>Use NTLM?</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="toggles toggle-modern pull-right" id="NTLM"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    System Logic Preferences
                </h3>
            </div>
            <div class="panel-body" id="panel3">
                <div class="row">
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Order Manifest</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="orderManifest"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>FIFO Pick Across Warehouse</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="FIFO"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Check For Valid Tote ID</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="CheckTote"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Replenish Dedicated Only</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="Replenish"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Pick Labels One Per Qty</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="PickLabels"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Short Pick Find New Location</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="ShortPickFind"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Zero Location Qty Check</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="ZeroLocationQty"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Request # Put Away Labels</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="RequestPutAwayLabels"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Multi Batch Cart Selection</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="MultiBatch"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Confirm Inventory Changes</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="ConfirmInvChanges"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>OT Temp to OT Pending</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="OtTempToOtPending"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Distinct Kit Orders</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="DistinctKitOrders"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Print Replen Put Labels</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="PrintReplenPutLabels"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Create Transactions When (Un)Quarantining</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="CreateTransQuarantine"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Show Trans Qty</label>
                        <select class="form-control" id="ShowTransQty">
                            <option>Display Always</option> 
                            <option>Recount Only</option>
                            <option>Never Display</option>
                        </select>
                    </div>
                    <div class="col-md-3">
                        <label>Next Tote ID </label>
                        <input type="text" class="form-control" placeholder="Next Tote ID" id="NextTote" maxlength="9" />
                    </div>
                    <div class="col-md-3">
                        <label>Next Serial Number</label>
                        <input type="text" class="form-control" placeholder="Next Serial Number" id="NextSerial" maxlength="13" />
                    </div>
                    <div class="col-md-3">
                        <label>Max Number of Put Away Labels</label>
                        <input type="text" class="form-control" placeholder="Max # Put Away Labels" maxlength="9" id="MaxPutAway" />
                    </div>
                    
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Pick Type</label>
                        <select class="form-control" id="PickType">
                            <option>Pick and Pass</option>
                            <option>Parallel Pick</option>
                        </select>
                    </div>
                    <div class="col-md-3" id="Carousel_div" style="display:none;">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Maintain Carousel Batch ID</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="CarouselBatchID"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Maintain Bulk Batch ID</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="BulkBatchID"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>Reel Tracking Pick Logic</label>
                        <select class="form-control" id="ReelTracking">
                            <option>Closest Demand Quantity</option>
                            <option>Dynamic</option>
                            <option>Serial Number Sort - FIFO</option>
                        </select>
                    </div>
                    <div class="col-md-3" id="DynReelWIP_div" style="display:none;">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Dynamic Reel Tracking Create WIP</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="DynReelWIP"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    POD Setup (System wide)
                </h3>
            </div>
            <div class="panel-body" id="panel4">
                <div class="row">
                    <div class="col-md-4">
                        <label>Order Sort</label>
                        <select id="OrderSort" class="form-control">
                            @For Each entry In Model.OrderSort
                                @<option>@entry</option>
                            Next
                        </select>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <label>Carton Flow Display</label>
                            <select id="CartonDisplay" class="form-control">
                                <option>Bin</option>
                                <option>Shelf</option>
                            </select>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Auto Display Images</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="DisplayImage"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="margin-top:10px" class="row">
                    <div class="col-md-4">
                        <label>Order Selection Color Filter</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Red Filter Field</label>
                        <select id="OSFilterRedField" class="form-control">
                            @For Each entry In Model.FilterFields
                                @<option id="Red_@entry.Key">@entry.Value</option>
                            Next
                        </select>
                        <label>Red Filter Value</label>
                        <input type="text" class="form-control" placeholder="Red Criteria" id="OSFilterRedValue">
                    </div>
                    <div class="col-md-4">
                        <label>Green Filter Field</label>
                        <select id="OSFilterGreenField" class="form-control">
                            @For Each entry In Model.FilterFields
                                @<option id="Green_@entry.Key">@entry.Value</option>
                            Next
                        </select>
                        <label>Green Filter Value</label>
                        <input type="text" class="form-control" placeholder="Green Criteria" id="OSFilterGreenValue">
                    </div>
                    <div class="col-md-4">
                        <label>Blue Filter Field</label>
                        <select id="OSFilterBlueField" class="form-control">
                            @For Each entry In Model.FilterFields
                                @<option id="Blue_@entry.Key">@entry.Value</option>
                            Next
                        </select>
                        <label>Blue Filter Value</label>
                        <input type="text" class="form-control" placeholder="Blue Criteria" id="OSFilterBlueValue">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>