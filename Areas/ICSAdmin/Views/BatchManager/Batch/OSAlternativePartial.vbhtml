<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.AliasModel
<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">
            Batch Manager Detail View
        </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-2">
                <label>Return to Batch Manager</label>
                <button type="button" class="btn btn-primary btn-block" id="osBack">Go Back</button>
            </div>
            <div class="col-md-6">
                <label>Order Number</label>
                <input type="text" class="form-control" disabled="disabled" id="osOrderNumber" maxlength="50" />
            </div>
            <div class="col-md-4">
                <label>Transaction Type</label>
                <input type="text" class="form-control" disabled="disabled" id="osTransType" maxlength="50" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <table class="table table-condensed table-bordered table-striped" style="background-color:white;" id="OSAlt">
                    <thead>
                        <tr>
                            <th>@Model.ItemNumber</th>
                            <th>Description</th>
                            <th>Trans. Qty</th>
                            <th>@Model.UoM</th>
                            <th>Lot #</th>
                            <th>Exp. Date</th>
                            <th>Serial #</th>
                            <th>Notes</th>
                            <th>Location</th>
                            <th>Whse</th>
                            <th>@Model.UserFields(0)</th>
                            <th>@Model.UserFields(1)</th>
                            <th>Tote ID</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>