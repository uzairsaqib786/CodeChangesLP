<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <img src="~/images/Help/CycleCount/batch.png" alt="Create Count Batches tab" style="width: 100%"/>
    </div>
    <div class="col-md-12">
        The landing page for the <strong>Create Count Batches</strong> page allows you to select a subset (or all) items by various methods in order to create count transactions in a batch for verification of stock.
    </div>
</div>
<div class="panel-group" id="CCB">
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#CCB" data-target="#CCBC">
                <h3 class="panel-title">
                    Create Counts tab <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="CCBC">
            <div class="row">
                <div class="col-md-12">
                    You can choose items and/or locations with the following filters:
                    <ul>
                        <li>Count Type</li>
                        <li>Warehouse</li>
                    </ul>
                    Warehouse consists of every warehouse registered in the application and the option "No Warehouse."  This allows you to choose items by specific warehouse locations.
                    Count type is made up of:
                    <ul>
                        <li>
                            Location Range
                            <ul>
                                <li>To/From Location - Location range to select items to count.</li>
                                <li>Include Empty Locations?</li>
                                <li>Include Other Locations?</li>
                            </ul>
                        </li>
                        <li>Item Number - Allows a range of item numbers to be selected with the two inputs.</li>
                        <li>Description - Search for an item by its description in the Inventory Master.</li>
                        <li>Category - Search for items based on their Category/Sub-Category information from Inventory Master.</li>
                        <li>Not Counted Since - Search by the last count date.</li>
                        <li>Picked Date Range - Date last picked in a range.</li>
                        <li>Put Away Date Range - Date last put away in a range.</li>
                        <li>Cost Range - Search by cost range from the Inventory Master part cost field.</li>
                    </ul>
                    Once filters have been added you can select all the current entries from the table by clicking the <button class="btn btn-primary">Insert into Queue</button> button.  Individual entries can be
                    excluded by clicking the <button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button> button.  After adding the items to the Queue you can repeat the process to get
                    more entries into the queue or you can click the <strong>Count Queue</strong> tab.
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-target="#CCBQ" data-parent="#CCB" data-toggle="collapse">
                <h3 class="panel-title">
                    Count Queue tab <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="CCBQ">
            <div class="row">
                <div class="col-md-12">
                    <img src="~/images/Help/CycleCount/batchqueue.png" alt="Count Queue tab" style="width: 100%"/>
                </div>
                <div class="col-md-12">
                    The image above is the Count Queue.  The transactions in the queue will be included in the Cycle Count when the <button data-toggle="tooltip" data-original-title="Create Cycle Count" class="btn btn-primary"><span class="glyphicon glyphicon-saved"></span></button> button is clicked.  
                    Individual entries may be excluded by clicking the <button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button> button.
                </div>
                <div class="col-md-12">
                    If a greater number of items/locations should be audited then clicked the <strong>Create Counts</strong> tab and refer to the help documentation for it.
                </div>
            </div>
        </div>
    </div>
</div>