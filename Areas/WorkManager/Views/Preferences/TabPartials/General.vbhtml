<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-body">
        <div class="row">
            <div class="col-md-6">
                <label>Print Directly?</label>
                <div class="toggles toggle-modern text-left" id="PrintDirect" style="width:60px;"  data-start-value="@Model("Print Direct").ToString().ToLower()"></div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <label>Default Pick Batch</label>
                <input type="text" class="form-control" id="Pick" placeholder="Default Pick Batch" value="@Model("Default Pick Batch")" />
            </div>
            <div class="col-md-4">
                <label>Default Put Batch</label>
                <input type="text" class="form-control" id="Put" placeholder="Default Put Batch" value="@Model("Default Put Batch")" />
            </div>
            <div class="col-md-4">
                <label>Default Count Batch</label>
                <input type="text" class="form-control" id="Count" placeholder="Default Count Batch" value="@Model("Default Count Batch")" />
            </div>
        </div>
    </div>
</div>