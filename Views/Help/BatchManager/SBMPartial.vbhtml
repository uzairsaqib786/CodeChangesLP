<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info" style="margin-bottom:10px;">
            <div class="panel-heading">
                <a data-toggle="collapse" data-target="#SBM_Overview">
                    <h3 class="panel-title">
                        Overview <span class="accordion-caret-down"></span>
                    </h3>
                </a>
            </div>
            <div class="panel-body collapse accordion-toggle in" id="SBM_Overview">
                <div class="row">
                    <div class="col-md-12">
                        Super Batches allow operators to combine single line orders into batches.  The number of single line orders in a batch order can be specified and the Priority field
                        in Open Transactions can be used to filter certain orders for batching.  Super batches are built by zone to keep orders together in workstation zones.
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-group" id="SBMAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SBMAccordion" data-target="#SBM_1">
                        <h3 class="panel-title">
                            1 | Controls <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SBM_1">
                    <div class="row">
                        <div class="col-md-12">
                            <h4>Set priorities to include in super batch</h4>
                            Comma separated priority values can be used to filter which orders should be super batched.  The <button type="button" class="btn btn-primary">Clear</button> 
                            button will clear the filter text box.
                            <h4>Set the number of orders in a super batch</h4>
                            Enter the number desired in the <strong>Batch Single Line Orders in Groups of:</strong> text box.
                            <h4>Batch Orders</h4>
                            Click the <button type="button" class="btn btn-primary">Batch Orders</button> button and confirm that you want to continue.
                            <h4>Clear Super Batches</h4>
                            Click the <button type="button" class="btn btn-danger">Clear All Super Batches</button> button and confirm that you want to continue.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SBMAccordion" data-target="#SBM_2">
                        <h3 class="panel-title">
                            2 | Super Batch Tables <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SBM_2">
                    <div class="row">
                        <div class="col-md-12">
                            <h4>Single Line Orders</h4>
                            Displays the number of transactions by zone that can be super batched.
                            <h4>Super Batches</h4>
                            Displays the transactions that have been super batched.
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>