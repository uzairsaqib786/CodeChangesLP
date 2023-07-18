<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="dedicated_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="dedicated_label" aria-hidden="true">
    <div class="modal-dialog" style="width:99%;margin-top:10px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="dedicated_label">Container Locations</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" id="container_item" />
                <input type="hidden" id="container_locnum" />
                <input type="hidden" id="BadContainer" />
                <input type="hidden" id="IsDedicated" value="false" />
                <div class="row">
                    <div class="col-md-3">
                        <label>@Model.aliases.ItemNumber</label>
                        <input type="text" class="form-control input-sm" id="dedicatedItem"  readonly="readonly" />
                    </div>
                    <div class="col-md-3">
                        <label>Location Name</label>
                        <input type="text" class="form-control input-sm" id="dedicatedLoc"  readonly="readonly" />
                    </div>
                    <div class="col-md-2">
                        <label>Warehouse</label>
                        <input type="text" class="form-control input-sm" id="dedicatedWhse"  readonly="readonly" />
                    </div>
                    <div class="col-md-2">
                        <label>Velocity Code</label>
                        <input type="text" class="form-control input-sm" id="dedicatedVel"  readonly="readonly" />
                    </div>
                    <div class="col-md-2">
                        <label>Cell Size</label>
                        <input type="text" class="form-control input-sm" id="dedicatedCell"  readonly="readonly" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-4">
                        <button type="button" class="btn btn-primary" id="forceBulk">Force to Bulk</button>
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-primary" id="forceFull">Force Full Container</button>
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-primary" id="dedicateLocs">Dedicate Locations with Current Item</button>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <table class="table table table-hover table-condensed table-bordered table-striped" style="background:white;" id="loc_table">
                            <thead>
                                <tr>
                                    <th>Location</th>
                                    <th>Carousel</th>
                                    <th>Assigned Item</th>
                                    <th>Serial Number</th>
                                    <th>Qty</th>
                                    <th>Stock Date</th>
                                    <th>Alloc Pick</th>
                                    <th>Alloc Put</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <fieldset>
                    <legend class="text-center">Other Locations</legend>
                </fieldset>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <table class="table table table-hover table-condensed table-bordered table-striped" style="background:white;" id="other_loc_table">
                            <thead>
                                <tr>
                                    <th>Whse</th>
                                    <th>Located</th>
                                    <th>Loc Type</th>
                                    <th>Ser #</th>
                                    <th>Qty</th>
                                    <th>Stocked</th>
                                    <th>Alloc Pick</th>
                                    <th>Alloc Put</th>
                                    <th>Velocity</th>
                                    <th>Cell Size</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default " data-dismiss="modal" id="dedicated_dismiss">Cancel</button>
            </div>
        </div>
    </div>
</div>