<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="OrderManagerAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#OrderManagerOverview" data-parent="#OrderManagerAccordion">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="OrderManagerOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This page is the <strong>Order Manager</strong> page. It displays open or pending headers/lines for transactions, and performs actions with them.
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <img src="/Areas/OrderManager/Images/Help/OrderManager/OrderManager.png" style="width: 85%" alt="Order Manager Screen" usemap="#ordermanmap" />
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="OrderManagerInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#OrderManagerInfo" data-target="#OrderManagerInfo_1">
                                            <h3 class="panel-title">
                                                1 | Filters
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="OrderManagerInfo_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Information:</strong>
                                                <ul>
                                                    <li><strong>Find Order(s) By:</strong> The desired column to check for records by</li>
                                                    <li><strong>Case:</strong> The desired operation for the column</li>
                                                    <li><strong>Value 1:</strong> The value which will be checked by the case selection within the desired column</li>
                                                    <li><strong>Max Orders:</strong> The maximum number of orders to be shown within the table</li>
                                                    <li><strong>Trans Type:</strong> The transaction type of the desired order(s) to be displayed</li>
                                                    <li><strong>View Type:</strong> Whether to view headers for orders or the actual lines of the order</li>
                                                    <li><strong>Order Type:</strong> Whether to view Open or Pending orders</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                                <ul>
                                                    <li>
                                                        <strong>Display Records:</strong> Displays orders for the current filter configuration
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DispRecButt.png" alt="Order Manager Diplsay Records Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Add/Edit Order:</strong> Opens the Create Orders page which allows user to create orders
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/AddEditButt.png" alt="Order Manager Add Edit Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Order Status:</strong> Opens the Order Status screen
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/OrderStatButt.png" alt="Order Manager Order Status Button" /></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-target="#OrderManagerInfo_2" data-parent="#OrderManagerInfo">
                                           <h3 class="panel-title">
                                               2 | Orders Table
                                               <span class="accordion-caret-down"></span>
                                           </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="OrderManagerInfo_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Information:</strong>
                                                <ul>
                                                    <li><strong>Orders Table:</strong> Table containing all the orders for the given configuration of filters</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                                <ul>
                                                    <li>
                                                        <strong>Set Column Sequence:</strong> Sets the column sequence for the Orders Table
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/ColSeqButt.png" alt="Order Manager Column Sequence Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Print Viewed:</strong> Prints all records that are currently within the table
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/PrintButt.png" alt="Order Manager Print Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Delete Viewed:</strong> Deletes all records that are currently within the table
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DelButt.png" alt="Order Manager Delete Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Release Viewed:</strong> Release all records that are currently within the table
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/RelViewButt.png" alt="Order Manager Release Viewed Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Update Record(s):</strong> Opens the update modals which allow fields to be changed for the selected or all of the orders
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/UpdRecButt.png" alt="Order Manager Update Records Button" /></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#OrderManagerInfo" data-target="#OrderManagerInfo_3">
                                            <h3 class="panel-title">
                                                Functionality
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="OrderManagerInfo_3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                This panel discusses the functionality within the <strong>Order Manager</strong> page
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="OrderManagerFuncs">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManagerFuncs" data-target="#OMFuncs_1">
                                                                <h3 class="panel-title">
                                                                    Populating the Table
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OMFuncs_1">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Order Manager</strong> page open</li>
                                                                        <li>Select the desired column to find orders by</li>
                                                                        <li>Select the case for this column</li>
                                                                        <li>Select the value that should be checked for the column using this case</li>
                                                                        <li>Make sure the trans type is to the desired option</li>
                                                                        <li>Select the desired view type</li>
                                                                        <li>Select if this order is open or pending</li>
                                                                        <li>Once these are done, the table should be populated</li>
                                                                        <li>
                                                                            If the table is not press the <strong>Display Records</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DispRecButt.png" alt="Order Manager Diplsay Records Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManagerFuncs" data-target="#OMFuncs_2">
                                                                <h3 class="panel-title">
                                                                    Opening the Add/Edit Page
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OMFuncs_2">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Order Manager</strong> page open</li>
                                                                        <li>
                                                                            Press the <strong>Add/Edit Order</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/AddEditButt.png" alt="Order Manager Add Edit Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the <strong>Create Orders</strong> page will open</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManagerFuncs" data-target="#OMFuncs_3">
                                                                <h3 class="panel-title">
                                                                    Setting the Columnn Sequence
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OMFuncs_3">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Order Manager</strong> page opened</li>
                                                                        <li>
                                                                            Press the <strong>Column Sequence</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/ColSeqButt.png" alt="Order Manager Column Sequence Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the <strong>Column Sequence</strong> page will be opened</li>
                                                                        <li>
                                                                            Within this page use left side of the page to select the desired order of the columns.
                                                                            Which will be displayed on the right under <strong>Default Column Sequence</strong>
                                                                        </li>
                                                                        <li>
                                                                            Once the desired order is completed press the <strong>Save Selected as Default</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/SaveDefButt.png" alt="Order Manager Column Sequence Save Default Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManagerFuncs" data-target="#OMFuncs_4">
                                                                <h3 class="panel-title">
                                                                    Printing
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OMFuncs_4">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Order Manager</strong> page open</li>
                                                                        <li>Populate the table with the desired data</li>
                                                                        <li>
                                                                            Press the <strong>Print</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/PrintButt.png" alt="Order Manager Print Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed report is printed containing all records that are currently displayed in the table</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManagerFuncs" data-target="#OMFuncs_5">
                                                                <h3 class="panel-title">
                                                                    Deleting
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OMFuncs_5">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Order Manager</strong> page opened</li>
                                                                        <li>Populate the table with the desired orders that are going to be deleted. <strong>Notice:</strong> you cannot delete an open transaction</li>
                                                                        <li>
                                                                            Press the <strong>Delete</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DelButt.png" alt="Order Manager Delete Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>
                                                                            Once pressed a pop up (shown below) appears. To delete these records press <strong>OK</strong>
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DelPopUp.png" alt="Order Manager Delete Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once confirmed, all viewed orders from the table are deleted</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManagerFuncs" data-target="#OMFuncs_6">
                                                                <h3 class="panel-title">
                                                                    Releasing Orders
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OMFuncs_6">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Order Manager</strong> page opened</li>
                                                                        <li>Populate the table with the desired orders that are going to be released. <strong>Notice:</strong> you cannot release an open transaction</li>
                                                                        <li>
                                                                            Press the <strong>Release Viewed</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/RelViewButt.png" alt="Order Manager Release Viewed Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>
                                                                            Once pressed a pop up (shown below) appears. To release the orders press <strong>OK</strong>
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/RelPopUp.png" alt="Order Manager Release Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once confirmed all orders within the table are released</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManagerFuncs" data-target="#OMFuncs_7">
                                                                <h3 class="panel-title">
                                                                    Update Record(s)
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OMFuncs_7">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Order Manager</strong> page opened</li>
                                                                        <li>Populate the table with the desired order(s) to edit</li>
                                                                        <li>Within the table click on the order that is going to be changed</li>
                                                                        <li>
                                                                            Once clicked the <strong>Update Record(s)</strong> button (shown below) enables. Click this button
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/UpdRecButt.png" alt="Order Manager Update Records Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>
                                                                            Once pressed the <strong>Update</strong> modal appears. Within this modal change desired fields to their new values and press the <strong>Update</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/UpdButt.png" alt="Order Manager Update Modal Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>
                                                                            Once pressed the <strong>Update Select</strong> modal is opened. Within this modal check any fields whose value will be applied to all records in the table.
                                                                            For example if you change the priority field and want the other records in the table to have this same priority you would mark the priority checkbox
                                                                        </li>
                                                                        <li>
                                                                            When all the changed fields are either marked (change all other records in table to this value) or unmarked (only the selected record will change)
                                                                            press the <strong>Update Button</strong> (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/UpdButt.png" alt="Order Manager Update Modal Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the modals are closed and the desired record(s) is/are updated</li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#OrderManagerAccordion" data-target="#CreateEditOrdersOverview">
                        <h3 class="panel-title">
                            Create/Edit Orders Page Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="CreateEditOrdersOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This page is the <strong>Create Orders</strong> page. From this page you can create and edit any pending orders
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <img src="/Areas/OrderManager/Images/Help/OrderManager/CreateOrders.png" style="width: 80%" alt="Create Orders Page" />
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="CreateOrdersOverview">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#CreateOrdersOverview" data-target="#CreateOrdersOverview_1">
                                            <h3 class="panel-title">
                                                Information
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="CreateOrdersOverview_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Information:</strong>
                                                <ul>
                                                    <li><strong>Order Number:</strong> The order number whose records will populate the table</li>
                                                    <li><strong>Orders Table:</strong> Table populated with all pending records for the given order number</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                                <ul>
                                                    <li>
                                                        <strong>Order Status:</strong> Opens the <strong>Order Status</strong> page when clicked
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/OrderStatButt.png" alt="Create Orders Order Status Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Add New Order:</strong> Opens the <strong>Add/Edit</strong> modal in order to add a new pending order
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/AddNewOrderButt.png" alt="Create Orders Add New Order Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Add Transaction:</strong> Opens the <strong>Add/Edit</strong> modal in order to add a new transaction to the order number
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/AddTransButt.png" alt="Create Orders Add Transaction Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Default User Fields:</strong> Opens the <strong>Default User Fields</strong> modal
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DefUserFieldButt.png" alt="Create Order Default User Fields Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Column Sequence:</strong> Opens the <strong>Set Column Sequence</strong> page
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/ColSeqButt.png" alt="Create Orders Column Sequence Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Print:</strong> Prints the report containing all transactions within the table
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/PrintButt.png" alt="Create Orders Print Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Delete:</strong> Deletes all transactions within the table
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DelButt.png" alt="Create Orders Delete Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Release Orders:</strong> Releases all transactions within the table
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/RelOrderButt.png" alt="Create Orders Release Order Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <strong>Edit Transaction:</strong> Opens the <strong>Add/Edit</strong> modal in order to edit the desired order
                                                        <ul>
                                                            <li><img src="/Areas/OrderManager/Images/Help/OrderManager/EditTransButt.png" alt="Create Orders Edit Transaction Button" /></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#CreateOrdersOverview" data-target="#CreateOrdersOverview_2">
                                            <h3 class="panel-title">
                                                Functionality
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="CreateOrdersOverview_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                This panel discusses the functionality found within the <strong>Create Orders</strong> page
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="CreateOrdersFunctions">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_1">
                                                                <h3 class="panel-title">
                                                                    Populating the Table
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_1">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> screen open</li>
                                                                        <li>
                                                                            Use the typeahead on the order number field to select the desired field
                                                                            <ul>
                                                                                <li>
                                                                                    To use typeahead start typing the desired order number and if it is a
                                                                                    pending order number it will appear in a list under the textbox
                                                                                    and click on it in the list
                                                                                </li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once selected the table will populate with all pending transactions associated with this order number</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_2">
                                                                <h3 class="panel-title">
                                                                    Add a New Order
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_2">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> page opened</li>
                                                                        <li>
                                                                            Press the <strong>Add New Order</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/AddNewOrderButt.png" alt="Create Orders Add New Order Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>
                                                                            Once pressed the <strong>Add/Edit</strong> modal will appear. Within this modal enter in all the desired
                                                                            information for the new order number
                                                                        </li>
                                                                        <li>
                                                                            Once the desired information entered press the <strong>Save</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/SaveButt.png" alt="Create Orders Add New Save Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the modal is closed and the new order populates the table</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_3">
                                                                <h3 class="panel-title">
                                                                    Adding a New Transaction
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_3">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> page opened</li>
                                                                        <li>Populate the table with the desired order number, or add it if it does not exist</li>
                                                                        <li>
                                                                            Once populated the <strong>Add Transaction</strong> button (shown below) will enable. Press this button
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/AddTransButt.png" alt="Create Orders Add Transaction Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed, the <strong>Add/Edit</strong> modal appears. Insert the data for the new transaction into their respective fields</li>
                                                                        <li>
                                                                            When the data is entered press the <strong>Save</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/SaveButt.png" alt="Create Orders Add New Save Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the modal is closed, and the table adds the new transaction</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_4">
                                                                <h3 class="panel-title">
                                                                    Updating Default User Fields
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_4">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> page opened</li>
                                                                        <li>
                                                                            Press the <strong>Default User Fields</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DefUserFieldButt.png" alt="Create Order Default User Fields Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the <strong>Default User Fields</strong> modal opens. Update the desired user field values to their new values</li>
                                                                        <li>
                                                                            When all changes are made press the <strong>Save</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/SaveButt.png" alt="Create Orders Add New Save Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once saved the modal closes</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_5">
                                                                <h3 class="panel-title">
                                                                    Setting the Column Sequence
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_5">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> page opened</li>
                                                                        <li>
                                                                            Press the <strong>Column Sequence</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/ColSeqButt.png" alt="Order Manager Column Sequence Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the <strong>Column Sequence</strong> page will be opened</li>
                                                                        <li>
                                                                            Within this page use left side of the page to select the desired order of the columns.
                                                                            Which will be displayed on the right under <strong>Default Column Sequence</strong>
                                                                        </li>
                                                                        <li>
                                                                            Once the desired order is completed press the <strong>Save Selected as Default</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/SaveDefButt.png" alt="Order Manager Column Sequence Save Default Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_6">
                                                                <h3 class="panel-title">
                                                                    Printing
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_6">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> page opened</li>
                                                                        <li>Populate the table with the desired records to be printed</li>
                                                                        <li>
                                                                            Once the records are displayed press the <strong>Print</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/PrintButt.png" alt="Create Orders Print Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the report is printed</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_7">
                                                                <h3 class="panel-title">
                                                                    Deleting Viewed Records
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_7">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> page open</li>
                                                                        <li>Populate the table with the records to be deleted</li>
                                                                        <li>
                                                                            Once populated press the <strong>Delete</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DelButt.png" alt="Create Orders Delete Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>
                                                                            Once pressed a pop up (shown below) appears. To delete the records press <strong>OK</strong>
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/DelPopUpCreate.png" alt="Create Orders Delete Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once confirmed the records are deleted</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_8">
                                                                <h3 class="panel-title">
                                                                    Releasing Orders
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_8">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> page opened</li>
                                                                        <li>Populate the table with the desired order number</li>
                                                                        <li>
                                                                            Once populated press the <strong>Release Order</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/RelOrderButt.png" alt="Create Orders Release Order Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>
                                                                            Once press a pop up (shown below) appears. To continue releasing press <strong>OK</strong>
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/RelCreatePopUp.png" alt="Create Orders Release Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When confirmed the desired order number is released</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersFunctions" data-target="#CreateOrdersFuncs_9">
                                                                <h3 class="panel-title">
                                                                    Editing a Transaction
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersFuncs_9">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the <strong>Create Orders</strong> page open</li>
                                                                        <li>Populate the table with the desired record to be edited</li>
                                                                        <li>Click on the desired record within the table</li>
                                                                        <li>
                                                                            Once clicked on the <strong>Edit Transaction</strong> button (shown below) becomes enabled. Click this button
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/EditTransButt.png" alt="Create Orders Edit Transaction Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once clicked the <strong>Add/Edit</strong> modal appears. Update the desired information within here</li>
                                                                        <li>
                                                                            When all the desired info is updated press the <strong>Save</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/OrderManager/Images/Help/OrderManager/SaveButt.png" alt="Create Orders Add New Save Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed modal is closed and the record is updated</li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#OrderManagerAccordion" data-target="#ModalsOverview">
                        <h3 class="panel-title">
                            Modals Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ModalsOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusess the modals found within both the <strong>Order Manager</strong> and <strong>Create Orders</strong> pages
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-group" id="ModalsOverview">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ModalsOverview" data-target="#OrderManModals">
                                            <h3 class="panel-title">
                                                Order Manager Modals
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="OrderManModals">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="OrderManModalsOverview">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManModalsOverview" data-target="#OrderManModals_1">
                                                                <h3 class="panel-title">
                                                                    Update Record Modal
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OrderManModals_1">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    This panel discusses the <strong>Update Record</strong> modal from the Order Manager page
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <img src="/Areas/OrderManager/Images/Help/OrderManager/UpdModal.png" alt="Order Manager Update Record Modal" />
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <div class="panel-group" id="UpdateRecordModal">
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#UpdateRecordModal" data-target="#UpdateRecordModal_1">
                                                                                    <h3 class="panel-title">
                                                                                        Information
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="UpdateRecordModal_1">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Information:</strong>
                                                                                        <ul>
                                                                                            <li>This modal is used to edit the information of the desired record</li>
                                                                                            <li>This modal is pre populated with the selected record's current information</li>
                                                                                            <li>
                                                                                                The <strong>Update</strong> button is used to continue the update when all desired changes are made
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/OrderManager/Images/Help/OrderManager/UpdButt.png" alt="Order Manager Update Modal Button" /></li>
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
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#OrderManModalsOverview" data-target="#OrderManModals_2">
                                                                <h3 class="panel-title">
                                                                    Select Update Modal
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="OrderManModals_2">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    This panel discusses the information within the <strong>Select Update</strong> modal
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <img src="/Areas/OrderManager/Images/Help/OrderManager/SelectUpdateModal.png" alt="Order Manager Select Update Modal" />
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <div class="panel-group" id="SelectUpdateModal">
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#SelectUpdateModal" data-target="#SelectUpdateModal_1">
                                                                                    <h3 class="panel-title">
                                                                                        Information
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="SelectUpdateModal_1">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Information:</strong>
                                                                                        <ul>
                                                                                            <li>This modal contains checkboxes for all the editable fields in the <strong>Update Record</strong> modal</li>
                                                                                            <li>These checkboxes become enabled for any field whose value was changed in the <strong>Update Record</strong> modal</li>
                                                                                            <li>Checking a field indicates that this field will be updated to the new value for all records with the same order number as the clicked record</li>
                                                                                            <li>The <strong>Update</strong> button is used to finishing updating the desired record
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/OrderManager/Images/Help/OrderManager/UpdButt.png" alt="Order Manager Update Modal Button" /></li>
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
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ModalsOverview" data-target="#CreateOrdersModals">
                                            <h3 class="panel-title">
                                                Create Orders Modals
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="CreateOrdersModals">
                                        <div class="row">
                                            <div class="col-md-12">
                                                This panel discusses the modals found within the <strong>Create Orders</strong> page
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="CreateOrdersModalsOverview">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersModalsOverview" data-target="#CreateOrdersModals_1">
                                                                <h3 class="panel-title">
                                                                    Add/Edit Modal
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersModals_1">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    This panel discusses the information within the <strong>Add/Edit</strong> modal
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <img src="/Areas/OrderManager/Images/Help/OrderManager/AddEditModal.png" alt="Create Orders Add Edit Modal" />
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <div class="panel-group" id="AddEditModal">
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#AddEditModal" data-target="#AddEditModal_1">
                                                                                    <h3 class="panel-title">
                                                                                        Information
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="AddEditModal_1">
                                                                               <div class="row">
                                                                                   <div class="col-md-12">
                                                                                       <strong>Information:</strong>
                                                                                       <ul>
                                                                                           <li>This modal contains all editable information about either a new order, transaction, or selected transaction</li>
                                                                                           <li>The function that this modal is currently performing is defined by the title at the very top of the modal</li>
                                                                                           <li>The <strong>Save</strong> button will complete the function that is currently being performed
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/OrderManager/Images/Help/OrderManager/SaveButt.png" alt="Create Orders Add New Save Button" /></li>
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
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#CreateOrdersModalsOverview" data-target="#CreateOrdersModals_2">
                                                                <h3 class="panel-title">
                                                                    Default User Field Modal
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="CreateOrdersModals_2">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    This panel discusses the information within the <strong>Default User Field</strong>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <img src="/Areas/OrderManager/Images/Help/OrderManager/DefUserFieldsModal.png" alt="Create Orders Default User FIelds Modal" />
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <div class="panel-body" id="DefaultUserFieldModal">
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#DefaultUserFieldModal" data-target="#DefaultUserFieldModal_1">
                                                                                    <h3 class="panel-title">
                                                                                        Information
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="DefaultUserFieldModal_1">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Information:</strong>
                                                                                        <ul>
                                                                                            <li>This displays all the default user field data that's filled in when adding a new record or transaction</li>
                                                                                            <li>The <strong>Save</strong> button will update any changes made to any of the user fields
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/OrderManager/Images/Help/OrderManager/SaveButt.png" alt="Create Orders Add New Save Button" /></li>
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
</div>
<map name="ordermanmap">
    <area shape="rect" coords="3, 0, 1351, 108" data-target="#OrderManagerInfo_1" data-toggle="collapse" data-parent="#OrderManagerInfo" />
    <area shape="rect" coords="2, 122, 1346, 277" data-target="#OrderManagerInfo_2" data-toggle="collapse" data-parent="#OrderManagerInfo" />
</map>