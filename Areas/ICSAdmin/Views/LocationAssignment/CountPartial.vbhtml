<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row" style="padding-top:10px;">
    <div class="col-md-7">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Count Order Selection
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <button id="SelectCounts" class="btn btn-primary pull-right">Select All</button>
                    </div>
                    <div class="col-md-12">
                        <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="leftCountTable">
                            <thead>
                                <tr>
                                    <th>Order Number</th>
                                    <th>Quantity</th>
                                    <th>Priority</th>
                                    <th>Required Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                @For Each tr As List(Of String) In Model.CountTable
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
                    Count Select Orders
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary" id="CountLocationAssignment">Location Assignment</button>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary" id="CountClearRightLocAssDT">Clear Selected Orders</button>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="rightCountTable">
                            <thead>
                                <tr>
                                    <th>Order Number</th>
                                    <th>Quantity</th>
                                    <th>Priority</th>
                                    <th>Required Date</th>
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