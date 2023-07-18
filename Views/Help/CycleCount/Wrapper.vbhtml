<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel-group" id="CC">
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#CC" data-target="#CCOverview">
                <h3 class="panel-title">
                    Overview <span class="accordion-caret-up"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body in collapse accordion-toggle" id="CCOverview">
            <div class="row">
                <div class="col-md-12">
                    There are two methods for dealing with a <strong>Cycle Count</strong>.
                    <ol>
                        <li>Discrepancies - Configurable to allow the use of the host system's information concerning how many of which item is stored at the site to compare against PickPro's known quantities.</li>
                        <li>Batches - Allows batching of count transactions of specific items grouped by their details or by location, cost or put away/last count date.</li>
                    </ol>
                    You will land on the Discrepancies page shown below.
                </div>
                <div class="col-md-12">
                    <img src="~/images/Help/CycleCount/cyclecount.png" alt="Cycle Count Discrepancies" style="width: 100%"/>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#CC" data-target="#CCDisc">
                <h3 class="panel-title">
                    1 | Discrepancies <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="CCDisc">
            @Html.Partial("~/Views/Help/CycleCount/CycleCount.vbhtml")
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#CC" data-target="#CCBatch">
                <h3 class="panel-title">
                    2 | Batches <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="CCBatch">
            @Html.Partial("~/Views/Help/CycleCount/CycleCountBatch.vbhtml")
        </div>
    </div>
</div>
