<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="LocAssAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#LocAssAccordion" data-target="#LocAssOverview">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="LocAssOverview">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This page is the <strong>Location Assignment</strong> screen. It displays pick, put away, and count transactions that are able to be assigned to a location
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <img src="/images/Help/LocationAssignment/InitialScreen.png" style="width: 80%" alt="Location Assignment Initial Screen" usemap="#locassmap" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#LocAssAccordion" data-target="#LocAssModal">
                        <h3 class="panel-title">
                            1 | Location Assignment Quantities Modal
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="LocAssModal">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses the information within the <strong>Location Assignment Quantities</strong> modal
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <img src="~/images/Help/LocationAssignment/LocAssQuantModal.png" style="width: 60%" alt="Location Assignment Quanttities Modal" />
                                    <ul>
                                        <li>
                                            <strong>Count:</strong> Will open the tab designated for count transactions. The number within the button tells how many count transactions exist
                                            <ul>
                                                <li><button class="btn btn-primary">Count <span class="badge">2</span></button></li>
                                            </ul>
                                        </li>
                                        <li>
                                            <strong>Pick: </strong>Will open the tab designated for pick transactions. The number within the button tells how many pick transactions exist
                                            <ul>
                                                <li><button class="btn btn-primary">Pick <span class="badge">2</span></button></li>
                                            </ul>
                                        </li>
                                        <li>
                                            <strong>Put Away:</strong> Will open the tab designated for put away transactions. The number within the button tells how many put away transactions exist
                                            <ul>
                                                <li><button class="btn btn-primary">Put Away <span class="badge">2</span></button></li>
                                            </ul>
                                        </li>
                                        <li>
                                            <strong>Print Pick Shortage:</strong> Will open a preview of the pick shortage report
                                            <ul>
                                                <li><button class="btn btn-primary">Print Pick Shortage</button></li>
                                            </ul>
                                        </li>
                                        <li>
                                            <strong>Print Pick Shortage Forward Pick Zones:</strong> Will open a preview of the pick shortage forward pick zones report
                                            <ul>
                                                <li><button class="btn btn-primary">Print Pick Shortage Forward Pick Zones</button></li>
                                            </ul>
                                        </li>
                                    </ul>
                                    The numbers next to the Pick, Put Away and Count buttons indicate the number of orders that can be location assigned at the current time.  To continue the process 
                                    you must click one of the buttons.  You may switch between transaction types in the next area as well as choosing here, so there is no need to renavigate to this page.  
                                    Switching between transaction types after the modal has closed can be accomplished by clicking the desired transaction type in the tabbed menu at the top left of the screen.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#LocAssCounts" data-parent="#LocAssAccordion">
                        <h3 class="panel-title">
                            2 | Location Assign Count Transactions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse" id="LocAssCounts">
                    <div class="row">
                        <div class="col-md-12">
                            <img src="~/images/Help/LocationAssignment/counts.png" style="width: 80%" alt="Location Assignment Count Transactions" />
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-12">
                            All order numbers with items eligible for a count transaction are listed in this area.  The left table is made up of the unselected transactions, while the right table is those 
                            that have been selected for location assignment.
                        </div>
                        <div class="col-md-12">
                            Clicking an order in either table will move it to the other table.  Alternatively the <button class="btn btn-primary">Select All</button> and <button class="btn btn-primary">Clear Selected Orders</button> buttons 
                            may be used to move all the orders from one side to the other.
                        </div>
                        <div class="col-md-12">
                            Clicking the <button class="btn btn-primary">Location Assignment</button> button will location assign the right table's orders.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#LocAssPicks" data-parent="#LocAssAccordion">
                        <h3 class="panel-title">
                            3 | Location Assign Pick Transactions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse" id="LocAssPicks">
                    <div class="row">
                        <div class="col-md-12">
                            <img src="~/images/Help/LocationAssignment/picks.png" style="width: 80%" alt="Location Assignment Pick Transactions" />
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-12">
                            All order numbers with items eligible for a pick transaction are listed in this area.  The left table is made up of the unselected transactions, while the right table is those
                            that have been selected for location assignment.
                        </div>
                        <div class="col-md-12">
                            Clicking an order in either table will move it to the other table.  Alternatively the <button class="btn btn-primary">Select All</button> and <button class="btn btn-primary">Clear Selected Orders</button> buttons
                            may be used to move all the orders from one side to the other.
                        </div>
                        <div class="col-md-12">
                            Clicking the <button class="btn btn-primary">Location Assignment</button> button will location assign the right table's orders.
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-12">
                            Rows in the unselected table have different colors depending on what the order is made up of.  The colors include: 
                            <ul>
                                <li>Green: All in Forward Pick Zones</li>
                                <li>Red: Short Lines</li>
                                <li>Blue: All Short</li>
                                <li>White: In Stock</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#LocAssPuts" data-parent="#LocAssAccordion">
                        <h3 class="panel-title">
                            4 | Location Assign Put Away Transactions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse" id="LocAssPuts">
                    <div class="row">
                        <div class="col-md-12">
                            <img src="~/images/Help/LocationAssignment/puts.png" style="width: 80%" alt="Location Assignment Put Away Transactions" />
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-12">
                            All order numbers with items eligible for a put away transaction are listed in this area.  The left table is made up of the unselected transactions, while the right table is those
                            that have been selected for location assignment.
                        </div>
                        <div class="col-md-12">
                            Clicking an order in either table will move it to the other table.  Alternatively the <button class="btn btn-primary">Select All</button> and <button class="btn btn-primary">Clear Selected Orders</button> buttons
                            may be used to move all the orders from one side to the other.
                        </div>
                        <div class="col-md-12">
                            Clicking the <button class="btn btn-primary">Location Assignment</button> button will location assign the right table's orders.
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<map name="locassmap"></map>