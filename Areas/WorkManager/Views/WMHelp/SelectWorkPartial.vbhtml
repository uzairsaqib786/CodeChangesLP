<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel-group" id="SWPanel">
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-target="#Overview" data-parent="#SWPanel">
                <h3 class="panel-title">Overview <span class="accordion-caret-down"></span></h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="Overview">
            <div class="row">
                <div class="col-md-12">
                    The <strong>Select Work</strong> page allows individuals to edit work assigned to them.  They can print tote or item labels or a work list.  They can create or clear batches of the work they are assigned.
                </div>
            </div>
            <div class="row top-spacer">
                <div class="col-md-12">
                    Orders and batches can be filtered by their <strong>Transaction Type</strong> or by what type of container the transaction is in (tote, batch, order/none).
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#SWPanel" data-target="#Functions">
                <h3 class="panel-title">Functionality <span class="accordion-caret-down"></span></h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="Functions">
            <div class="panel-group" id="FunctionDetails">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <a data-toggle="collapse" data-target="#Common" data-parent="#FunctionDetails"><h3 class="panel-title">Common functions <span class="accordion-caret-down"></span></h3></a>
                    </div>
                    <div class="panel-body collapse accordion-toggle" id="Common">
                        <div class="row">
                            <div class="col-md-12">
                                <h5>Printing</h5>
                                <ol>
                                    <li>Select the desired records by clicking them in the left table.  When they appear in the right table they have been selected successfully.</li>
                                    <li>
                                        Click the
                                        <div class="btn-group">
                                            <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                            <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                                <li>
                                                    <a href="#" id="PrintWork">Print Work List</a>
                                                    <a href="#" id="PrintItem">Print Item Label</a>
                                                    <a href="#" id="PrintTote">Print Tote Label</a>
                                                </li>
                                            </ul>
                                        </div> button and select the appropriate type.
                                    </li>
                                </ol>
                            </div>
                        </div>
                        <div class="row top-spacer">
                            <div class="col-md-12">
                                The tables can be refreshed by clicking the <button class="btn btn-warning"><span class="glyphicon glyphicon-refresh"></span></button> button.  This will also unselect any batches/orders/totes.
                            </div>
                        </div>
                        <div class="row top-spacer">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        The <button class="btn btn-primary">Auto Assign Work Batches</button> button will launch a modal allowing the user to manage work assigned to them.
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <img src="~/Areas/WorkManager/Images/HelpSelectWork/assignworkmodal.png" style="width: 60%" alt="Auto Assign Work Batches Modal" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <ul>
                                            <li>The <strong>Picks</strong>, <strong>Put Aways</strong>, and <strong>Counts</strong> checkboxes allow the user to specify which Transaction Type the other actions on the modal will alter.</li>
                                            <li>The <button class="btn btn-primary">Select All Types</button> button is equivalent to clicking each checkbox.</li>
                                            <li>On the <strong>Select Work</strong> page workers are restricted to managing their own work and not that of others, so the <strong>Select Worker</strong> field is locked.</li>
                                            <li>The <button class="btn btn-danger">Clear Current Work</button> button allows the user to clear their assigned batches' [Export Batch ID] field.</li>
                                            <li>The <button class="btn btn-danger">Clear Batch ID(s)</button> button allows the user to clear their assigned batches' [Batch Pick ID] field.</li>
                                            <li>The <button class="btn btn-primary">Batch ID and Username</button> button allows the user to create a new batch assigned to themselves with the same number of orders as is defined in the [Default Pick/Put/Count Batch] preferences.</li>
                                            <li>The <button class="btn btn-primary">Batch ID Only</button> button allows the user to create a new batch not assigned to themselves with the same number of orders as is determined in the [Default Pick/Put/Count Batch] preferences.</li>
                                            <li>The <strong>Current Batches</strong> field determines whether clicking on the <button class="btn btn-primary">Batch ID Only</button> button or <button class="btn btn-primary">Batch ID and Username</button> button should also execute the action assigned to <button class="btn btn-danger">Clear Batch ID(s)</button> before execution.</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <a data-toggle="collapse" data-parent="#FunctionDetails" data-target="#Batches">
                            <h3 class="panel-title">Working with Batches <span class="accordion-caret-down"></span></h3>
                        </a>
                    </div>
                    <div class="panel-body collapse accordion-toggle" id="Batches">
                        <h4>Working with Batches</h4>
                        <ul>
                            <li>A batch can be selected by clicking it in the left (unselected) table.</li>
                            <li>A batch is can be unselected by clicking it in the right (selected) table.</li>
                            <li>Batches in the right table (selected) can be cleared by clicking the <button class="btn btn-danger">Clear Batches</button> button.</li>
                            <li>Batch transaction lines can be viewed by clicking the <button class="btn btn-primary btn-xs detail"><span class="glyphicon glyphicon-share"></span></button> button for the corresponding batch.</li>
                            <li>A batch may be assigned to the current user by clicking the <button class="btn btn-primary">Assign Selected to Me</button> button.  If you arrived from the <strong>Organize Work</strong> page and you have the proper permissions this will apply to the selected user, not the logged in one.  The button text will be "Assign Selected to Specified Worker" if this is the case.</li>
                        </ul>
                        <img src="~/Areas/WorkManager/Images/HelpSelectWork/batches.png" style="width: 70%" alt="Batches Selected" />
                    </div>
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <a data-toggle="collapse" data-parent="#FunctionDetails" data-target="#Orders">
                            <h3 class="panel-title">Working with Orders <span class="accordion-caret-down"></span></h3>
                        </a>
                    </div>
                    <div class="panel-body collapse accordion-toggle" id="Orders">
                        <h4>Working with Orders</h4>
                        <ul>
                            <li>An order can be selected by clicking it in the left (unselected) table.</li>
                            <li>An order can be unselected by clicking it in the right (selected) table.</li>
                            <li>A series of orders can be assigned to a new batch by clicking the <button class="btn btn-primary">Assign Batch ID</button> button.</li>
                            <li>Order lines can be viewed by clicking the <button class="btn btn-primary btn-xs detail"><span class="glyphicon glyphicon-share"></span></button> button for the corresponding order.</li>
                            <li>
                                The next group of orders can be selected all at once by clicking the <button class="btn btn-primary">Select Next #</button> button.
                                <ul>
                                    <li>The number of orders that are selected is determined by the corresponding preference (Default Pick/Put/Count Batch.)</li>
                                    <li>If there are too few orders to fulfill the number determined by the preference then the maximum number of orders is selected instead.</li>
                                </ul>
                            </li>
                        </ul>
                        <img src="~/Areas/WorkManager/Images/HelpSelectWork/orders.png" style="width: 70%" alt="Orders Selected" />
                    </div>
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <a data-toggle="collapse" data-target="#Totes" data-parent="#FunctionDetails">
                            <h3 class="panel-title">Working with Totes <span class="accordion-caret-down"></span></h3>
                        </a>
                    </div>
                    <div class="panel-body collapse accordion-toggle" id="Totes">
                        <h4>Working with Totes</h4>
                        <ul>
                            <li>A tote can be selected by clicking it in the left (unselected) table.</li>
                            <li>A tote can be unselected by clicking it in the right (selected) table.</li>
                            <li>A combination of totes can be assigned to a single batch by selecting them and then clicking the <button class="btn btn-primary">Assign Batch ID</button> button.</li>
                            <li>Tote transaction lines can be viewed by clicking the <button class="btn btn-primary btn-xs detail"><span class="glyphicon glyphicon-share"></span></button> button on the corresponding tote.</li>
                            <li>
                                The next group of totes can be selected all at once by clicking the <button class="btn btn-primary">Select Next #</button> button.
                                <ul>
                                    <li>The number of totes that are selected is determined by the corresponding preference (Default Pick/Put/Count Batch.)</li>
                                    <li>If there are too few totes to fulfill the number determined by the preference then the maximum number of totes is selected instead.</li>
                                </ul>
                            </li>
                        </ul>
                        <img src="~/Areas/WorkManager/Images/HelpSelectWork/totes.png" style="width: 70%" alt="Totes Selected" />
                    </div>
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <a data-toggle="collapse" data-target="#Organize" data-parent="#FunctionDetails">
                            <h3 class="panel-title">Relation to Organize Work <span class="accordion-caret-down"></span></h3>
                        </a>
                    </div>
                    <div class="panel-body collapse accordion-toggle" id="Organize">
                        <div class="row">
                            <div class="col-md-12">
                                The <strong>Select Work</strong> page is also accessible from the <strong>Organize Work</strong> page if you have sufficient permissions.  The only difference between the typical <strong>Select Work</strong> page and the one accessible through the <strong>Organize Work</strong> page is the
                                user that the selections are applied to.  There is an additional field which can be seen below:
                            </div>
                            <div class="col-md-12">
                                <img src="~/Areas/WorkManager/Images/HelpSelectWork/organize.png" alt="Select Work from Organize Work page" />
                            </div>
                        </div>
                        <div class="row top-spacer">
                            <div class="col-md-12">
                                By default the worker field will be the logged in user as it is on the normal <strong>Select Work</strong> page.  Changing the value in the Worker textbox will allow the user to manage work of others.
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>