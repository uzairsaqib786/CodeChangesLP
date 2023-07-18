<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.AliasModel
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <a data-toggle="collapse" data-target="#SRN_0">
                    <h3 class="panel-title">
                        Overview
                        <span class="accordion-caret-down"></span>
                    </h3>
                </a>
            </div>
            <div class="panel-body collapse accordion-toggle in" id="SRN_0">
                <div class="row">
                    <div class="col-md-12">
                        The New Orders section of System Replenishment allows the user to select item numbers to replenish from back locations to forward locations.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        A replenishment may only be created if the transaction quantity is between 1 and the lower of the two maximum fields (Available Quantity and Replenishment Quantity).
                        Invalid quantities will cause the Replenish checkbox to be disabled.  The Replenish checkbox indicates whether a replenishment transaction will be made for that entry.
                        The kanban checkbox indicates if this replenishment is marked as a kanban replenishment. The Exists checkbox indicates whether a replenishment transaction has already been created for that entry.  If there is an existing set of transactions in Open Transactions
                        the Exists checkbox will be checked and the Replenish checkbox will be disabled.  The Transaction Quantity field indicates how many of an item should be pushed to forward locations.
                        The Replenish checkbox will update the screen of every user on the System Replenishment - New Orders page.  Each time a Replenish checkbox is changed or the number of
                        selected replenishments changes the count will be redisplayed at the top left.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <img src="~/images/Help/SystemReplenishment/sysreplen_landing.png" style="width: 80%" usemap="SysReplenMap" />
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-group" id="SysReplenNewAccordion" style="padding-top:10px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenNewAccordion" data-target="#SRN_1">
                        <h3 class="panel-title">
                            1 | Create New Order List
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SRN_1">
                    <div class="row">
                        <div class="col-md-12">
                            The first step to the System Replenishment process is to create a new order list.  This is accomplished by clicking the
                            <button class="btn btn-primary">Create New Order List</button>
                            button.  The button will prompt for confirmation from the user that a new order list should be created.  If confirmed the system will create a new list and redraw the table
                            when the process completes.  Any previously selected items will not be selected.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenNewAccordion" data-target="#SRN_2">
                        <h3 class="panel-title">
                            2 | Select Items for Replenishment
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SRN_2">
                    <div class="row" style="padding-top:15px;">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <strong>Select an item for Replenishment</strong>
                                    <ol>
                                        <li>Choose an item number for replenishment.</li>
                                        <li>
                                            Click the Transaction Quantity input box for the selected entry and enter a valid transaction quantity in the modal pop over.  Note: If the
                                            transaction quantity cannot be greater than 0 (when Replenishment Quantity or Available Quantity are 0), then the item cannot be replenished.
                                        </li>
                                        <li>
                                            Check the Replenish checkbox for the entry selected.  Note:  If the Exists checkbox is checked the Replenish checkbox will be disabled, because
                                            there are already transactions for the selected replenishment in Open Transactions.
                                        </li>
                                    </ol>
                                    <div class="row">
                                        <div class="col-md-12">
                                            Alternatively the <strong>Select All</strong> button may be clicked to select ALL items in the list for replenishment.  <button type="button" class="btn btn-primary">
                                                <span class="glyphicon glyphicon-ok"></span>
                                            </button>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <strong>Remove an item from Replenishment list</strong>
                                            <ul>
                                                <li>Uncheck the Replenish checkbox for that entry.</li>
                                            </ul>
                                            Alternatively the <strong>Unselect All</strong> button may be clicked to unselect ALL items in the list for replenishment.
                                            <button class="btn btn-primary"><span class="glyphicon glyphicon-ban-circle"></span></button>.
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:15px;">
                                        <div class="col-md-12">
                                            Finally the <button class="btn btn-primary">Create New Order List</button> button will unselect all replenishments and refresh the list with new/updated data.
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenNewAccordion" data-target="#SRN_3">
                        <h3 class="panel-title">
                            3 | Process Replenishments
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SRN_3">
                    <div class="row" style="padding-top:15px;">
                        <div class="col-md-12">
                            <strong>Process Replenishments</strong>
                            <ol>
                                <li>Ensure that some replenishments are selected.</li>
                                <li>Click the <button class="btn btn-success">Process Replenishments</button> button.</li>
                                <li>Confirm that you want to begin processing.</li>
                                <li>
                                    Optional:  Cancel the replenishment process, by clicking the <button class="btn btn-danger">Stop the Replenishment Process</button> button.  This will halt the replenishment process
                                    and cancel any created replenishments and log the event in the Event Log.
                                </li>
                                <li>
                                    Optional:  If the replenishment process is allowed to complete then the user who initially clicked <button class="btn btn-success">Process Replenishments</button> will be given the
                                    option to print a Reprocess Report when there are reprocess transactions created due to lack of quantity.
                                </li>
                                <li>
                                    Optional:  Wait for the process to finish.  The replenishment process will continue even if the user navigates away from System Replenishment.  If the user
                                    remains on the page the replenishments modal will update the user with information about how far the process has progressed.
                                </li>
                            </ol>
                            Note:  If a user is currently processing replenishments any newly connected user will be updated with the modal indicating status of replenishments and will not be
                            allowed to execute any action within the replenishments screen while replenishments are being processed.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenNewAccordion" data-target="#SRN_4">
                        <h3 class="panel-title">
                            4 | Print Replenishments
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SRN_4">
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Print Replenishment List</strong>
                            <ol>
                                <li>Click the Print button.  <button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button> </li>
                                <li>Confirm that you want to print a report.</li>
                                <li>If confirmed the report will be printed to the workstation's default report printer.</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenNewAccordion" data-target="#SRN_5">
                        <h3 class="panel-title">
                            5 | View Selected Item in the Inventory Master
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SRN_5">
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Select an item to be viewed in the Inventory Master</strong>
                            <ol>
                                <li>Click the row with the desired @Model.ItemNumber</li>
                                <li>
                                    Click the
                                    <button type="button" class="btn btn-primary" data-toggle="tooltip" title="" data-original-title="View Selected Item in Inventory Master">
                                        <span class="glyphicon glyphicon-share"></span>
                                    </button> button
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenNewAccordion" data-target="#SRN_6">
                        <h3 class="panel-title">
                            6 | Change Transaction Quantity of Replenishments
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SRN_6">
                    <div class="row">
                        <div class="col-md-12">
                            The transaction quantity field in System Replenishments allows the user to adjust how many of an item are moved to a forward location as a result of the replenishment process.
                            The quantity must be between 1 and the lower of the two values in Available Quantity and Replenishment Quantity.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <ol>
                                <li>Click the transaction quantity input box of the item you want to adjust.</li>
                                <li>A modal will appear:  
                                    <ul>
                                        <li><img src="~/images/Help/SystemReplenishment/tqty_modal.PNG" style="width: 50%"/></li>
                                    </ul>
                                </li>
                                <li>Fill in the data in the modal and click Submit.  To cancel the adjustment click Close.</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<map name="SysReplenMap">
    <area shape="rect" coords="202,85,232,140" data-target="#SRN_4" data-toggle="collapse" data-parent="#SysReplenNewAccordion" />
    <area shape="rect" coords="232,85,258,140" data-target="#SRN_5" data-toggle="collapse" data-parent="#SysReplenNewAccordion" />
    <area shape="rect" coords="929,79,1071,106" data-target="#SRN_1" data-toggle="collapse" data-parent="#SysReplenNewAccordion" />
    <area shape="rect" coords="1074,60,1191,105" data-target="#SRN_3" data-toggle="collapse" data-parent="#SysReplenNewAccordion" />
    <area shape="rect" coords="1054,160,1132,486" data-target="#SRN_2" data-toggle="collapse" data-parent="#SysReplenNewAccordion" />
    <area shape="rect" coords="262,110,347,136" data-target="#SRN_2" data-toggle="collapse" data-parent="#SysReplenNewAccordion" />
    <area shape="rect" coords="881,141,1049,480" data-target="#SRN_6" data-toggle="collapse" data-parent="#SysReplenNewAccordion" />
</map>