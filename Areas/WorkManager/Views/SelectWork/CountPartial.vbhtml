<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@If Model.OrganizeWork Then
    @<div class="row">
        <div class="col-md-2">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Worker</h3>
                </div>
                <div class="panel-body">
                    <div class="row" style="padding-bottom:5px;">
                        <div class="col-md-12">
                            <div class="form-group has-feedback has-warning">
                                <input type="text" class="form-control" id="WorkerTA" placeholder="Name" maxlength="30" />
                                <span style="top:0px;" class="glyphicon glyphicon-warning-sign form-control-feedback" data-toggle="tooltip" data-original-title="You must select a worker from the dropdown." data-placement="top"></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <input type="text" class="form-control" id="WorkerID" disabled="disabled" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-5">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Transaction Type</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <label>
                                View Picks
                                <input type="radio" data-next-x="@Model.WSPrefs("Default Pick Batch")" id="Picks" name="PickPutCount" checked="checked" />
                                <input type="text" class="form-control radio-click" readonly="readonly" id="PickLines"  value="@Model.Counts(1)" />
                            </label>
                        </div>
                        <div class="col-md-4">
                            <label>
                                View Put Aways
                                <input type="radio" data-next-x="@Model.WSPrefs("Default Put Batch")" id="Puts" name="PickPutCount" />
                                <input type="text" class="form-control radio-click" readonly="readonly" id="PutLines" value="@Model.Counts(2)" />
                            </label>
                        </div>
                        <div class="col-md-4">
                            <label>
                                View Counts
                                <input type="radio" id="Counts" data-next-x="@Model.WSPrefs("Default Count Batch")" name="PickPutCount" />
                                <input type="text" class="form-control radio-click" readonly="readonly" id="CountLines" value="@Model.Counts(0)" />
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-5">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        View by
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="col-md-4">
                        <label>
                            Batches
                            <input type="radio" id="Batches" name="BatchOrderTote" checked="checked" />
                            <input type="text" class="form-control radio-click" data-work-title="Batch Pick ID" readonly="readonly" id="BatchLines"  value="@Model.Counts(3)" />
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label>
                            Orders
                            <input type="radio" id="Orders" name="BatchOrderTote" />
                            <input type="text" class="form-control radio-click" data-work-title="Order Number" readonly="readonly" id="OrderLines"  value="@Model.Counts(4)" />
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label>
                            Totes
                            <input type="radio" id="Totes" name="BatchOrderTote" />
                            <input type="text" class="form-control radio-click" data-work-title="Tote ID" readonly="readonly" id="ToteLines"  value="@Model.Counts(5)" />
                        </label>
                    </div>
                </div>
            </div>
        </div>
    </div>
Else
    @<div class="row">
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Transaction Type</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <label>
                                View Picks
                                <input type="radio" data-next-x="@Model.WSPrefs("Default Pick Batch")" id="Picks" name="PickPutCount" checked="checked" />
                                <input type="text" class="form-control radio-click" readonly="readonly" id="PickLines"  value="@Model.Counts(1)" />
                            </label>
                        </div>
                        <div class="col-md-4">
                            <label>
                                View Put Aways
                                <input type="radio" data-next-x="@Model.WSPrefs("Default Put Batch")" id="Puts" name="PickPutCount" />
                                <input type="text" class="form-control radio-click" readonly="readonly" id="PutLines"  value="@Model.Counts(2)" />
                            </label>
                        </div>
                        <div class="col-md-4">
                            <label>
                                View Counts
                                <input type="radio" id="Counts" data-next-x="@Model.WSPrefs("Default Count Batch")" name="PickPutCount" />
                                <input type="text" class="form-control radio-click" readonly="readonly" id="CountLines"  value="@Model.Counts(0)" />
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        View by
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="col-md-4">
                        <label>
                            Batches
                            <input type="radio" id="Batches" name="BatchOrderTote" checked="checked" />
                            <input type="text" class="form-control radio-click" data-work-title="Batch Pick ID" readonly="readonly" id="BatchLines"  value="@Model.Counts(3)" />
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label>
                            Orders
                            <input type="radio" id="Orders" name="BatchOrderTote" />
                            <input type="text" class="form-control radio-click" data-work-title="Order Number" readonly="readonly" id="OrderLines"  value="@Model.Counts(4)" />
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label>
                            Totes
                            <input type="radio" id="Totes" name="BatchOrderTote" />
                            <input type="text" class="form-control radio-click" data-work-title="Tote ID" readonly="readonly" id="ToteLines"  value="@Model.Counts(5)" />
                        </label>
                    </div>
                </div>
            </div>
        </div>
    </div>
End If