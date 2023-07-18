<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-12">
                <div class="panel-group" id="OrderStatusAccordion">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-target="#OSOverview" data-parent="#OrderStatusAccordion"><h3 class="panel-title">Overview <span class="caret"></span></h3></a>
                        </div>
                        <div class="panel-body collapse in accordion-toggle" id="OSOverview">
                            <div class="row">
                                <div class="col-md-12">
                                    This is the Order Status page.  It contains an easy way to look up orders by order number or tote ID and supports searching on various columns displayed in the table.
                                    The black outlines can be clicked to navigate to an area with details about the clicked panel.
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <img src="/images/Help/Transactions/OrderStatus/OrderStatusHome.PNG" style="width: 85%" usemap="#orderstatus" alt="Order Status page" />
                                </div>
                            </div>
                            <div class="panel-group" id="OrderStatusOverviewDetail" style="padding-top:5px;">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#OrderStatusOverviewDetail" data-target="#panelOS_1">
                                            <h3 id="selectOrderPanel" class="panel-title">
                                                1 | Select Order Panel
                                                <span class="caret"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" style="background-color:white;" id="panelOS_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                The Order Status Select Order Panel allows the user to filter their results based on order number and/or tote ID.
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <ul>
                                                    <li>The <strong>Delete Order</strong> button allows the user to delete an order.  Clicking the button will prompt the user to confirm the action before an order is deleted.</li>
                                                    <li>The <strong>Clear</strong> button allows the user to empty both the order number and tote ID fields.</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Filters:</strong>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <ul>
                                                    <li>
                                                        <strong>Filter Info By Tote</strong> allows the user to filter the other areas of the page.
                                                        The panels listed below will contain data related ONLY to the tote if <strong>Filter Info By Tote</strong> is checked, otherwise all data related to the order is displayed.

                                                        <ul>
                                                            <li><strong>Order Info</strong></li>
                                                            <li><strong>Carousel Location Zones</strong></li>
                                                            <li><strong>Off-Carousel Location Zones</strong></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label><strong>Fields:</strong></label>
                                                <ul>
                                                    <li><strong>Completed Lines</strong>:  Number of completed components of the selected order.</li>
                                                    <li><strong>Re-Process Lines</strong>:  Number of components of the selected order that must go through a reprocess.</li>
                                                    <li><strong>Open Lines</strong>:  Number of components of the selected order currently allocated to Open Transactions.</li>
                                                    <li><strong>Order Type</strong>:  Type of transaction in the selected order or "Multiple" if there is more than one type of transaction in the order.</li>
                                                    <li><strong>Total Lines</strong>:  Total number of components of the selected order, including Open, Completed and Re-Process.</li>
                                                    <li><strong>Current Status</strong>:  State of the selected order.  "Completed", "In Process" or "In Process / BO".  "In Process / BO" refers to back order or reprocess.</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info" style="background-color:white;">
                                    <div class="panel-heading">
                                        <a data-target="#panelOS_2" data-toggle="collapse" data-parent="#OrderStatusOverviewDetail">
                                            <h3 class="panel-title" id="carouselLocationPanel">
                                                2 | Carousel Location Zones Panel
                                                <span class="caret"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="panelOS_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                The Carousel Location Zones panel displays locations allocated to the selected order that are carousels.
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Fields:</strong>
                                                <ul>
                                                    <li><strong>Zone</strong>:  Zone where the selected order is allocated.</li>
                                                    <li><strong>Location Name</strong>:  Type of location where the selected order is allocated.  (Example: Bulk)</li>
                                                    <li><strong>Total Lines</strong>:  Total components of the selected order allocated to the <strong>Zone</strong> and <strong>Location Name</strong>.</li>
                                                    <li><strong>Open</strong>:  Components of the selected order in the <strong>Zone</strong> and <strong>Location Name</strong> that are not completed and not in reprocess.</li>
                                                    <li><strong>Completed</strong></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info" style="background-color:white;">
                                    <div class="panel-heading">
                                        <a data-target="#panelOS_3" data-toggle="collapse" data-parent="#OrderStatusOverviewDetail">
                                            <h3 class="panel-title" id="offcarLocationPanel">
                                                3 | Off-Carousel Location Zones Panel
                                                <span class="caret"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="panelOS_3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                The Off-Carousel Location Zones panel displays locations allocated to the selected order that are not carousels.
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Fields:</strong>
                                                <ul>
                                                    <li>Zone</li>
                                                    <li>Location Name</li>
                                                    <li>Total Lines</li>
                                                    <li>Open</li>
                                                    <li>Completed</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info" style="background-color:white;">
                                    <div class="panel-heading">
                                        <a data-target="#panelOS_4" data-toggle="collapse" data-parent="#OrderStatusOverviewDetail">
                                            <h3 class="panel-title" id="orderstatusTablePanel">
                                                4 | Order Status Table
                                                <span class="caret"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="panelOS_4">
                                        <div class="row">
                                            <div class="col-md-12">
                                                The Order Status table displays all allocated lines in the selected order and related fields.  See <strong>Tables</strong> for more detailed information.
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Features:</strong>
                                                <ul>
                                                    <li>
                                                        Custom coloring for rows
                                                        <ul>
                                                            <li><strong>Open</strong> lines are <span style="background-color: #fcf8e3;">yellow.</span></li>
                                                            <li><strong>Completed</strong> lines are <span style="background-color: #dff0d8;">green.</span></li>
                                                            <li><strong>Re-Process</strong> lines are <span style="background-color: #f2dede;">red.</span></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        Custom coloring for <strong>Type</strong> column
                                                        <ul>
                                                            <li><strong>Put Away</strong> transactions are <span class="transaction-putAway">blue.</span></li>
                                                            <li><strong>Pick</strong> transactions are <span class="transaction-pick">purple.</span></li>
                                                            <li><strong>Complete</strong> transactions are <span class="transaction-complete">beige.</span></li>
                                                            <li><strong>Count</strong> transactions are <span class="transaction-count">orange.</span></li>
                                                            <li><strong>Location Change</strong> transactions are <span class="transaction-locationChange">olive.</span></li>
                                                            <li><strong>Shipping</strong> transactions are <span class="transaction-shipping">dark gray.</span></li>
                                                            <li><strong>Shipping Complete</strong> transactions are <span class="transaction-shippingComplete">pink.</span></li>
                                                            <li><strong>Adjustment</strong> transactions are <span class="transaction-adjustment">dark green.</span></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info" style="background-color:white;">
                        <div class="panel-heading">
                            <a data-target="#OSSelectOrder" data-toggle="collapse" data-parent="#OrderStatusAccordion">
                                <h3 class="panel-title">
                                    Select data by order number or tote ID
                                    <span class="caret"></span>
                                </h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="OSSelectOrder">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <img src="/images/Help/Transactions/OrderStatus/select.png" alt="Select an Order Number" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h3><strong>Order Number</strong></h3>
                                            <ol>
                                                <li>Begin typing an order number in box #1.</li>
                                                <li>
                                                    Choose an order number or continue.  An order number may be clicked from the suggestions list that appears below (box #4). In addition the completed date
                                                    of each order is shown. This is to filter completed orders with the same order number   (see
                                                    <strong>Typeaheads</strong> for more information concerning the method of selection and functionality of the suggestions.)
                                                    <ul>
                                                        <li><img src="/images/Help/Transactions/OrderStatus/select_expanded.png" alt="Select an Order Number" /></li>
                                                    </ul>
                                                </li>
                                                <li>As soon as a valid order number is entered the data for that order is retrieved and populates the table and other fields.</li>
                                            </ol>
                                            <h3><strong>Tote ID</strong></h3>
                                            <ol>
                                                <li>Type a tote ID (box #2).</li>
                                                <li><strong>Order Number</strong> will be populated if there is only one order in the entered tote.</li>
                                                <li>As soon as a valid tote ID is entered the data for that tote is retrieved and populates the table and other fields.</li>
                                                <li>
                                                    <strong>Filter Info By Tote</strong> allows the user to filter the other areas of the page. The panels listed below will
                                                    contain data related ONLY to the tote if <strong>Filter Info By Tote</strong> is checked, otherwise all data related to the order is displayed.
                                                    Fields affected by this filter include:
                                                    <ul>
                                                        <li><strong>Order Info</strong></li>
                                                        <li><strong>Carousel Location Zones</strong></li>
                                                        <li><strong>Off-Carousel Location Zones</strong></li>
                                                    </ul>
                                                </li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info" style="background-color:white;">
                        <div class="panel-heading">
                            <a data-target="#OSPrint" data-toggle="collapse" data-parent="#OrderStatusAccordion">
                                <h3 class="panel-title">
                                    Print an order
                                    <span class="caret"></span>
                                </h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="OSPrint">
                            <div class="row">
                                <div class="col-md-12">
                                    <strong>Steps:</strong>
                                    <ol>
                                        <li>
                                            Click the print button <button type="button" data-toggle="tooltip" data-placement="top" title="" class="btn btn-primary Print-Report" data-original-title="Print Report">
                                                <span class="glyphicon glyphicon-print"></span>
                                            </button>
                                        </li>
                                    </ol>
                                    <strong>Results:</strong>
                                    <ul>
                                        <li>The currently selected data (filtered by <strong>Order Number</strong> and <strong>Tote ID</strong>) are printed to the workstation's report printer. (see <strong>Preferences</strong>)</li>
                                        <li>See <strong>Printing</strong> for more details.</li>
                                        <li>Note:  Selected data includes <strong>ALL</strong> results in the <strong>Order Status</strong> table.</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info" style="background-color:white;">
                        <div class="panel-heading">
                            <a data-target="#OSDelete" data-toggle="collapse" data-parent="#OrderStatusAccordion">
                                <h3 class="panel-title">Delete an order <span class="caret"></span></h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="OSDelete">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <img src="/images/Help/Transactions/OrderStatus/delete.PNG" alt="Click Delete" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <strong>Steps:</strong>
                                            <ol>
                                                <li>Click <strong>Delete</strong></li>
                                                <li>
                                                    Confirm or deny the delete action
                                                    <ul>
                                                        <li><img src="/images/Help/Transactions/OrderStatus/delete_confirm.png" alt="Confirm Delete Order" /></li>
                                                        <li>Click <strong>OK</strong> to delete the order.</li>
                                                        <li>Click <strong>Cancel</strong> return to the screen without deleting the order.</li>
                                                    </ul>
                                                </li>
                                            </ol>
                                            <strong>Results:</strong>
                                            <ul>
                                                <li>
                                                    If <strong>OK</strong> was clicked
                                                    <ul>
                                                        <li>Order (Box #2 in first image) is deleted from the database.</li>
                                                        <li>An entry is made in the <strong>Event Log</strong> with relevant data about the order and user who confirmed the action.</li>
                                                    </ul>
                                                </li>
                                                <li>If <strong>Cancel</strong> was clicked no action is taken.</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info" style="background-color:white;">
                        <div class="panel-heading">
                            <a data-target="#OSClear" data-toggle="collapse" data-parent="#OrderStatusAccordion">
                                <h3 class="panel-title">Clear order number and tote ID filters <span class="caret"></span></h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="OSClear">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <img src="/images/Help/Transactions/OrderStatus/clear.PNG" alt="Click Clear" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <strong>Steps:</strong>
                                            <ol>
                                                <li>Click <strong>Clear</strong> or use the <strong>C</strong> key when outside an input box.</li>
                                            </ol>
                                            <strong>Results:</strong>
                                            <ul>
                                                <li>Order Number filter will be emptied.</li>
                                                <li>Tote ID filter will be emptied.</li>
                                                <li>The Order Status table will be emptied.</li>
                                                <li>Carousel and Off-Carousel Location Zones will be emptied.</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info" style="background-color:white;">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-parent="#OrderStatusAccordion" data-target="#ViewShipInfo">
                                <h3 class="panel-title">
                                    View Shipping Information
                                    <span class="accordion-caret-down"></span>
                                </h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="ViewShipInfo">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel-group" id="ViewShipInfoAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ViewShipInfoAccordion" data-target="#ViewShipInfoOverview">
                                                    <h3 class="panel-title">
                                                        Overview
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ViewShipInfoOverview">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>View Shipping Info</strong> page
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <img src="/images/Help/Transactions/OrderStatus/ViewShippingInfo.PNG" style="width: 85%" alt="View Shipping Info Page" />
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="ViewShipInfoOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#ViewShipInfoOverviewAccordion" data-target="#ViewShipInfoInfo">
                                                                                <h3 class="panel-title">
                                                                                    Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="ViewShipInfoInfo">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Order Number:</strong> The order number whose shipping information is being displayed</li>
                                                                                        <li>
                                                                                            <strong>Packing List:</strong> Prints the packing list for the order number
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">Packing List</button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li><strong>Packing Information Table:</strong> Displays all transactions that are packed for the order number</li>
                                                                                        <li><strong>Shipping Information:</strong> Displays all containers that are associated with this order number</li>
                                                                                    </ul>
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ViewShipInfoAccordion" data-target="#ViewShipInfoOpen">
                                                    <h3 class="panel-title">
                                                        Opening the Page
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ViewShipInfoOpen">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This page discusses how to open up the <strong>View Ship Info</strong> page
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Within the <strong>Order Status</strong> page select the desired order number</li>
                                                            <li>
                                                                Once selected the <strong>Shipping Complete</strong> button (shown below) will be enabled. If it is not, then the selected order number is not shipping complete. Press this button
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Shipping Complete</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the <strong>Shipping Complete</strong> button is pressed, the page opens and fills for the order number that was displayed on the <strong>Order Status</strong> page</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ViewShipInfoAccordion" data-target="#ViewShipInfoPrint">
                                                    <h3 class="panel-title">
                                                        Printing the Packing List
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ViewShipInfoPrint">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to print the <strong>Packing List</strong> for the order number
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Open the <strong>View Shipping Info</strong> page</li>
                                                            <li>
                                                                Press the <strong>Packing List</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Packing List</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once pressed the packing list report is printed</li>
                                                        </ol>
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
</div>
<map name="orderstatus">
    <area shape="rect" coords="8, 44, 487, 251" data-target="#panelOS_1" data-toggle="collapse" data-parent="#OrderStatusOverviewDetail" />
    <area shape="rect" coords="510, 43, 989, 244" data-target="#panelOS_2" data-toggle="collapse" data-parent="#OrderStatusOverviewDetail" />
    <area shape="rect" coords="1015, 42, 1497, 247" data-target="#panelOS_3" data-toggle="collapse" data-parent="#OrderStatusOverviewDetail" />
    <area shape="rect" coords="2, 261, 1496, 424" data-target="#panelOS_4" data-toggle="collapse" data-parent="#OrderStatusOverviewDetail" />
</map>