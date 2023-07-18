<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row" style="padding-top:10px;">
    <div class="col-md-7">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Pick Order Selection
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <h2 style="display:inline">Table Legend</h2> &nbsp;
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>White:</label> In Stock
                                        <label class="text-success">Green:</label> All in FPZ
                                        <label class="text-danger">Red:</label> Short Lines
                                        <label class="text-info">Blue:</label> All Short
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <button id="SelectPicks" class="btn btn-primary btn-lg btn-block">Select All</button>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="OrderNumberSearch">Order Search</label>
                                    <input id="OrderNumberSearch" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <button id="ViewPrintModal" class="btn btn-primary btn-block">Print Short Reports</button>
                    </div>
                </div>
                <div class="row"  style="padding-top:10px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="leftPickTable">
                            <thead>
                                <tr>
                                    <th>Order Number</th>
                                    <th>Quantity</th>
                                    <th>Priority</th>
                                    <th>Required Date</th>
                                    <th>Short</th>
                                    <th>All Short</th>
                                    <th>FPZ</th>
                                </tr>
                            </thead>
                            <tbody>
                                @For Each tr As List(Of String) In Model.PickTable
                                @<tr>
                                    @For Each td As String In tr
                                @<td>@td</td>
                                    Next
                                </tr>
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-5">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Pick Select Orders
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary" id="PickLocationAssignment">Location Assignment</button>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary" id="PickClearRightLocAssDT">Clear Selected Orders</button>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="rightPickTable">
                            <thead>
                                <tr>
                                    <th>Order Number</th>
                                    <th>Quantity</th>
                                    <th>Priority</th>
                                    <th>Required Date</th>
                                    <th>Short</th>
                                    <th>All Short</th>
                                    <th>FPZ</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
