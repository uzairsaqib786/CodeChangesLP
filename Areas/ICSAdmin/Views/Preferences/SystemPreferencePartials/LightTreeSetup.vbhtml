<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Light Tree Setup
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-3">
                                <label>Zone</label>
                                <select class="form-control" id="LT_Zone">
                                    <option value="" selected></option>
                                    @For Each zone In Model.TreeZones
                                        @<option value="@zone.Zone">@zone.Zone | @zone.Name</option>
                                    Next
                                </select>

                            </div>
                            <div class="col-md-3">
                                <label>Carousel</label>
                                <select class="form-control" id="LT_Carousel">
                                    <option value="" selected></option>
                                    @For Each car In Model.TreeCars
                                        @<option value="@car">@car</option>
                                    Next
                                </select>
                            </div>
                            <div class="col-md-3">
                                <label>Bin Location</label>
                                <select class="form-control" id="LT_Bin">
                                    <option value="" selected></option>
                                    @For Each bin In Model.TreeBinLocs
                                        @<option value="@bin">@bin</option>
                                    Next
                                </select>
                            </div>
                            <div class="col-md-3">
                                <label>Shelf Direction</label>
                                <div class="pull-right toggles toggle-modern" id="LT_Shelf" data-toggle-ontext="DESC" data-toggle-offtext="ASC"></div>
                            </div>
                        </div>
                        <div class="row" style="padding-top:5px;">
                            <div class="col-md-3">
                                <button type="button" class="btn btn-primary" id="LT_Reset" disabled="disabled" data-toggle="tooltip" data-placement="top" data-original-title="Set Alternate Light to 0 in Zone and Carousel">Reset All Alternate Light Positions</button>
                            </div>
                            <div class="col-md-3">
                                <button type="button" class="btn btn-primary" id="LT_Refresh">Display Bin Light Position</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Alternate Light Settings
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-3">
                                <label>Shelf</label>
                            </div>
                            <div class="col-md-3">
                                <label>Alternate Light Position</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="LT_Container">

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>