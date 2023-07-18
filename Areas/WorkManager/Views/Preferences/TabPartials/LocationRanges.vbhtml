<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4 class="panel-title">
                    Location Ranges
                </h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <button type="button" data-toggle="tooltip" data-placement="top" title="Refresh Ranges" class="btn btn-primary btn-block" id="RefreshLocationRanges"><span class="glyphicon glyphicon-refresh"></span></button>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary btn-block edit-modal" disabled id="EditLocationRange">Edit Location</button>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <button type="button" class="btn btn-primary btn-block edit-modal" data-toggle="tooltip" data-placement="top" title="Add New Location Range" id="AddLocationRange"><span class="glyphicon glyphicon-plus"></span></button>
                        </div>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <table id="LocationRangesTable" class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                            <thead>
                                <tr>
                                    <th class="text-center">ID</th>
                                    <th class="text-center">Range Name</th>
                                    <th class="text-center">Start Location</th>
                                    <th class="text-center">End Location</th>
                                    <th class="text-center">Multi Worker Range</th>
                                    <th class="text-center">Active</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>