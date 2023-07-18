<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="PageDiv">
    <div class="col-md-12">
        <div class="panel-group" id="SuperBatchAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SuperBatchAccordion" data-target="#SuperBatchOverview">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SuperBatchOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This page is the <strong>Super Batch</strong> page. This is were you can batch single line orders together under a new tote ID.
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <img src="/Areas/Induction/Images/SuperBatchScreen.png" style="width: 100%" alt="Super Batch" usemap="#superBatchScreen" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SuperBatchAccordion" data-target="#SuperBatchFunctionAccordion">
                        <h3 class="panel-title">
                            Functionality
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SuperBatchFunctionAccordion">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses the functionality within the <strong>Super Batch</strong> page
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-group" id="SuperBatchFunctionInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#SuperBatchFunctionAccordion" data-target="#SuperBatchFunctionFilt">
                                            <h3 class="panel-title">
                                                Filters
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="SuperBatchFunctionFilt">
                                        <div class="row">
                                            <div class="col-md-12">
                                                There are two filters for Super Batch.<br />
                                                <strong>Order Number: </strong> Gets all records where an order number only appears once in open transactions. Also called a <strong>Single Line Order</strong>.<br />
                                                <strong>Tote ID: </strong> Gets all records where a Tote ID only appears once in open transactions. Also called a <strong>Single Line Tote Order</strong>.<br />
                                                You can change the default filter in Induction <span class="glyphicon glyphicon-arrow-right"></span> Admin <span class="glyphicon glyphicon-arrow-right"></span> Preferences <span class="glyphicon glyphicon-arrow-right"></span> Misc Setup.
                                                <div class="row" style="padding-top:5px;">
                                                    <div class="col-md-6">
                                                        <img src="/Areas/Induction/Images/SuperBatchMiscSetupFilt.png" style="width: 100%" alt="Super Batch" usemap="#SuperBatchMiscSetupFilt" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#SuperBatchFunctionAccordion" data-target="#SuperBatchTableFields">
                                            <h3 class="panel-title">
                                                Table Fields
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="SuperBatchTableFields">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <ul>
                                                    <li><strong>Zone: </strong>This field shows you where the Single Line Orders are located.</li>
                                                    <li><strong>Single Line Orders: </strong>This is the number of single line orders for the zone.</li>
                                                    <li><strong>Orders to Batch: </strong>This is how many single line orders that will be batched at a time. You can change the default number in Induction <span class="glyphicon glyphicon-arrow-right"></span> Admin <span class="glyphicon glyphicon-arrow-right"></span> Preferences <span class="glyphicon glyphicon-arrow-right"></span> Misc Setup. <br />
                                                        <strong>Example: </strong> If you have 45 Single Line Orders and your Orders to Batch is set to 30, after you successfully batch those orders there will be 15 Single Line Orders left.</li>
                                                    <li><strong>New Tote ID: </strong>This is the new Tote ID that will be assigned to the orders you are batching.</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#SuperBatchFunctionAccordion" data-target="#SuperBatchCreateBatch">
                                            <h3 class="panel-title">
                                                Create Batch
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="SuperBatchCreateBatch">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps: </strong>
                                                <ol>
                                                    <li>Verify the <strong>Orders to Batch</strong> number is correct. </li>
                                                    <li>Select a <strong>Tote ID</strong> that currently does not exist.</li>
                                                    <li>Click <strong>Batch Orders</strong></li>
                                                </ol>
                                            </div>
                                            <div class="row" style="padding-top:5px;">
                                                <div class="col-md-12">
                                                    <img src="/Areas/Induction/Images/SuperBatchCreateBatch.png" style="width: 100%" alt="Super Batch" usemap="#SuperBatchMiscSetupFilt" />
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
        </div>
    </div>
</div>
