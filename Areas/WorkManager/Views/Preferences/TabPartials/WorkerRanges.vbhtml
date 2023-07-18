<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row top-spacer">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4 class="panel-title">
                    Worker Ranges 
                </h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Select Worker To Add New Range</label>
                        <input type="text" id="SelNewWorkerRange" class="form-control" maxlength="30" />
                    </div>
                    <div class="col-md-2" style="padding-top:20px;">
                        <button type="button" class="btn btn-primary btn-block" disabled id="EditWorkerRange">Edit Worker Range</button>
                    </div>
                    <div class="col-md-2" style="padding-top:20px;">
                        <button type="button" class="btn btn-danger btn-block" data-toggle="tooltip" data-placement="top" disabled title="Delete Selected Worker Range" id="DeleteWorkerRange"><span class="glyphicon glyphicon-remove"></span></button>
                    </div>
                    <div class="col-md-2" style="padding-top:20px;">
                        <button type="button" class="btn btn-primary btn-block Print-Report" id="PrintWorkerRanges">Print Ranges</button>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <table id="WorkerRangesTable" class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                            <thead>
                                <tr>
                                    <th class="text-center">ID</th>
                                    <th class="text-center">Username</th>
                                    <th class="text-center">First Name</th>
                                    <th class="text-center">Last Name</th>
                                    <th class="text-center">Range Name</th>
                                    <th class="text-center">Start Location</th>
                                    <th class="text-center">End Location</th>
                                    <th class="text-center">Active</th>
                                    <th class="text-center">Date Stamp</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
