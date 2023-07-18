<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info" style="margin-bottom:10px;">
            <div class="panel-heading">
                <a data-toggle="collapse" data-target="#BM_Overview">
                    <h3 class="panel-title">
                        Overview
                        <span class="accordion-caret-down"></span>
                    </h3>
                </a>
            </div>
            <div class="panel-body collapse accordion-toggle in" id="BM_Overview">
                <div class="row">
                    <div class="col-md-12">
                        The Batch Manager allows users to combine multiple orders based on their own criteria into larger orders.  Zones can be marked in preferences as being Auto Batch Zones.
                        Auto Batch Zones are expected to be batched and a button for that is included.  The <button type="button" class="btn btn-primary">Select Orders in Auto Batch Zones</button>
                        button allows any orders eligible for batching to be selected.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        Three types of transactions can be batched together.  Picks, Put Aways and Counts can be combined within matching transaction types.  Selecting a transaction type will
                        allow the user to batch orders of that type together.  Any eligible transactions will be placed in the left hand table labeled <strong>Order Selection List</strong>.  Any
                        transactions of the same type that have already been selected to be batched together will appear in the right hand table.  Any transactions that are in progress and have
                        already been combined will appear in the <strong>Delete Batch</strong> dropdown, identified by their Batch Pick ID.  Details of transactions may be viewed via
                        <button type="button" class="btn btn-primary">View Detail</button>.  Either the <strong>Order Status</strong> screen or an alternative will be opened on click,
                        depending on the user's permission level.
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-group" id="BMAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#BM_1" data-parent="#BMAccordion">
                        <h3 class="panel-title">
                            1 | Controls
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="BM_1">
                    <div class="row">
                        <div class="col-md-12">
                            <img src="~/images/Help/Batch/bm_controls.PNG" style="width: 100%"/>
                            <hr />
                            <h4>Select a Transaction Type</h4>
                            Click the dropdown for transaction type and select the type you would like to batch.  The data will update when a new transaction type is selected.
                            <hr />
                            <h4>Current Pick Mode</h4>
                            Preference setting.
                            <hr />
                            <h4>Delete Batch(es)</h4>
                            <ol>
                                <li>Select a batch from the dropdown to delete.</li>
                                <li>Click the <button type="button" class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button> button</li>
                                <li>
                                    A modal will appear prompting the operator whether just the batch or the batch and the tote number should be deleted.  Alternatively the Close button can be
                                    clicked to cancel deletion.
                                </li>
                                <li>If confirmed the transactions in the batch will be updated to have no batch and/or no tote number depending on the option the operator selected.</li>
                            </ol>
                            Alternatively <button type="button" class="btn btn-danger">Delete by Transaction Type</button> button can be clicked in order to delete all currently incomplete batches of the
                            same transaction type.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#BM_2" data-parent="#BMAccordion">
                        <h3 class="panel-title">
                            2 | Order Selection List
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="BM_2">
                    <div class="row">
                        <div class="col-md-12">
                            <img src="~/images/Help/Batch/bm_orders.PNG" />
                            <h4>Methods to select orders to batch:</h4>
                            <ol>
                                <li>
                                    <button type="button" class="btn btn-primary">Append Max Orders</button>
                                    Click the Append Max Orders button to select the number of orders equal to the minimum of all carousels' maximum orders field in the operator's workstation's pick
                                    zones.
                                </li>
                                <li>
                                    <button type="button" class="btn btn-primary">Select Orders in Auto Batch Zones</button> Click the Select Order in Auto Batch Zones button to select any
                                    orders that are in zones marked as Auto Batch.
                                </li>
                                <li>Click the row of the order number anywhere except the View Detail button to add that order to the batch order.</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#BM_3" data-parent="#BMAccordion">
                        <h3 class="panel-title">
                            3 | Selected Orders
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="BM_3">
                    <div class="row">
                        <div class="col-md-12">
                            <img src="~/images/Help/Batch/bm_selectedorders.PNG" />
                            <h4>Removing a selected order</h4>
                            Click the row of the order number to remove.  To remove all rows at once:  Click the <button type="button" class="btn btn-primary">Remove All</button> button.
                            <h4>Printing a report or labels</h4>
                            <ol>
                                <li>
                                    Click the print button
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a>Print Batch Report</a></li>
                                            <li><a>Print Item Labels</a></li>
                                        </ul>
                                    </div>
                                </li>
                                <li>Click the dropdown entry for either labels or a report.</li>
                            </ol>
                            <h4>Create a batched order</h4>
                            Click the <button class="btn btn-success" type="button">Create Batch</button> button and confirm that you want to create the batch with the selected orders.
                            <h4>Set the batched orders Batch Pick ID</h4>
                            Enter the desired Batch ID in the textbox next to the <strong>Create Batch</strong> button.
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>