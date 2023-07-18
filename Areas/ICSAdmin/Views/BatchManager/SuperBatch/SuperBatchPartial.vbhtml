<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Super Batch Manager Controls
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-5">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-8">
                                        <label>Priorities to Include (comma separated):</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <input type="text" class="form-control" id="Priorities" placeholder="Example:  1, 2" />
                                    </div>
                                    <div class="col-md-4">
                                        <button type="button" class="btn btn-primary btn-block" id="ClearPriorities">Clear</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Batch Single Line Orders in Groups of:</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <input maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" type="text" class="form-control" id="Groups" value="100" placeholder="Number of single lines in a group" />
                                    </div>
                                    <div class="col-md-4">
                                        <button type="button" class="btn btn-primary btn-block" id="SuperBatchCreate">Batch Orders</button>
                                    </div>
                                    <div class="col-md-4">
                                        <button type="button" class="btn btn-danger btn-block" id="ClearSuperBatch">Clear All Super Batches</button>
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
    <div class="col-md-6">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Single Line Orders
                </h3>
            </div>
            <div class="panel-body">
                <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="leftSuperBMTable">
                    <thead>
                        <tr>
                            <th>Zone</th>
                            <th>Location Name</th>
                            <th>Location Type</th>
                            <th>Total Transactions</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Super Batches
                </h3>
            </div>
            <div class="panel-body">
                <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="rightSuperBMTable">
                    <thead>
                        <tr>
                            <th>Zone</th>
                            <th>Location Name</th>
                            <th>Location Type</th>
                            <th>Total Transactions</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>