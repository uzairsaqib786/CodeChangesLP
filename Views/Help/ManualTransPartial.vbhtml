<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    Layout = Nothing
End Code
<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="ManualTransAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#ManualTransOverview" data-parent="#ManualTransAccordion">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ManualTransOverview">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This page is the <strong>Manual Transactions</strong>. It displays information about transactions created by users and allows users to create new transactions
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <img src="/images/Help/ManualTrans/InitialScreen.png" style="width: 50%" alt="Manual Transactions Load Screen" usemap="#mantransmap" />
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="ManualTransOverviewAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ManualTransOverviewAccordion" data-target="#ManTransOverview_1">
                                                    <h3 class="panel-title">
                                                        1 | Temporary Manual Order Number Section
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ManTransOverview_1" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the information within the <strong>Temporary Manual Order Number</strong> section
                                                                <div class="row" style="padding-top:5px;">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/ManualTrans/TempManOrderNumScreen.png" style="width: 80%" alt="Manual Transactions Temporary Manual Order Number Section Screen" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="TempManOrderNumAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#TempManOrderNumAccordion" data-target="#TempManOrderNum_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="TempManOrderNum_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information displayed in the <strong>Temporary Manual Order Number</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information Displayed:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Order Number:</strong> The order number of the currently selected transaction</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#TempManOrderNumAccordion" data-target="#TempManOrderNum_2">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Buttons
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="TempManOrderNum_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the buttons displayed in the <strong>Temporary Manual Order Number</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Clear:</strong> Empties the Order Number field
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">Clear</button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Add New Transaction:</strong> Opens up the pop up for adding a new transaction
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Save:</strong> Saves any changes made to the selected transaction
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Print:</strong> Prints the report for the <strong>Manual Transactions</strong> page
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete Transaction:</strong> Deletes the selected transaction
                                                                                            <ul>
                                                                                                <li><button class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Post Transaction:</strong> Will post the selected transaction to Open Transaction and either save or delete
                                                                                            the transaction from Manual Transactions
                                                                                            <ul>
                                                                                                <li>
                                                                                                    <div class="btn-group">
                                                                                                        <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown">Post Transaction <span class="caret"></span></button>
                                                                                                        <ul class="dropdown-menu" role="menu">
                                                                                                            <li><a>Post and Delete Transaction</a></li>
                                                                                                            <li><a>Post and Save Transaction</a></li>
                                                                                                        </ul>
                                                                                                    </div>
                                                                                                </li>
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#ManTransOverview_2" data-parent="#ManualTransOverviewAccordion">
                                                    <h3 class="panel-title">
                                                        2 | Item Details Section
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ManTransOverview_2" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the information within the <strong>Item Details</strong> section
                                                                <div class="row" style="padding-top:5px;">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/ManualTrans/ItemDetails.png" style="width: 70%" alt="Manual Transactions Item Details Section" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="ItemDetailsAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-target="#ItemDetails_1" data-toggle="collapse" data-parent="#ItemDetailsAccordion">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="ItemDetails_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel dicusses the information displayed in the <strong>Item Details</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Item Number:</strong> The item number of the selected transaction</li>
                                                                                        <li><strong>Supplier Item ID:</strong> The supplier item id assigned to the item number</li>
                                                                                        <li><strong>Expiration Date:</strong> The date that this transaction expires</li>
                                                                                        <li><strong>Revision: </strong> The revision field for thsi transaction</li>
                                                                                        <li><strong>Description:</strong> The description of the item number</li>
                                                                                        <li><strong>Lot Number:</strong> The lot number assigned to this transaction</li>
                                                                                        <li><strong>UM:</strong> The Unit of Measure assigned for the transaction</li>
                                                                                        <li><strong>Notes:</strong> Information for this transaction</li>
                                                                                        <li><strong>Serial Number:</strong> The serial number for this transaction</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-target="#ItemDetails_2" data-parent="#ItemDetailsAccordion">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Buttons
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="ItemDetails_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the buttons displayed within the <strong>Item Details</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>View/Set User Fields:</strong> Opens the pop up that allows the user fields to be set
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">View/Set User Fields</button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-target="#ItemDetails_3" data-parent="#ItemDetailsAccordion">
                                                                                <h3 class="panel-title">
                                                                                    Modals
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="ItemDetails_3">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            This panel discusses the modals(pop ups) found in the <strong>Item Details</strong> section
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="padding-top:5px;">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel-group" id="ItemDetailsModals">
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-target="#ItemDetailsModal_1" data-parent="#ItemDetailsModals">
                                                                                                            <h3 class="panel-title">
                                                                                                                Item Number Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="ItemDetailsModal_1">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the modal from clicking on the <strong>Item Number</strong> field
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/ManualTrans/SKUModal.png" style="width: 50%" alt="Manual Transactions Item Number Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Item Number:</strong> Either the current or the new desired item number for the transaction</li>
                                                                                                                    <li><strong>Location:</strong> Either the current or the new desired location for the transaction</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Close:</strong> Closes the modal <strong>without</strong> applying any changes</li>
                                                                                                                    <li><strong>Set Item and Location</strong> Closes the modal and applies any changes made</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-target="#ItemDetailsModal_2" data-parent="#ItemDetailsModals">
                                                                                                            <h3 class="panel-title">
                                                                                                                Supplier Item ID Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="ItemDetailsModal_2">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the modal from clicking on the <strong>Supplier Item ID</strong> field
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/ManualTrans/SuppItemIDModal.png" style="width: 50%" alt="Manual Transactions Item Number Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Supplier Item ID:</strong> Either the current or the new desired supplier item id for this transaction</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Close:</strong> Closes the modal <strong>without</strong> applying any changes</li>
                                                                                                                    <li><strong>Set:</strong> Closes the and applies any changes made</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#ItemDetailsModals" data-target="#ItemDetailsModal_3">
                                                                                                            <h3 class="panel-title">
                                                                                                                Description Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body accordion-toggle collapse" id="ItemDetailsModal_3">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the modal from clicking on the <strong>Description</strong> field
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/ManualTrans/DescriptionModal.png" style="width: 50%" alt="Manual Transactions Description Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Description:</strong> Shows the entire description for the item. This is <strong>not</strong> editable </li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Close:</strong> Closes the modal</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#ItemDetailsModals" data-target="#ItemDetailsModal_4">
                                                                                                            <h3 class="panel-title">
                                                                                                                Notes Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="ItemDetailsModal_4">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the modal from clicking on the <strong>Notes</strong> field
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/ManualTrans/NotesModal.png" style="width: 50%" alt="Manual Transactions Notes Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Notes:</strong> The desired notes for this item. This is editable</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Close:</strong> Closes the modal <strong>without</strong> saving any entered text</li>
                                                                                                                    <li><strong>Submit:</strong> Closes the modal and saves the typed text as the notes field for the selected item</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#ItemDetailsModals" data-target="#ItemDetailsModal_5">
                                                                                                            <h3 class="panel-title">
                                                                                                                Unit of Measure Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="ItemDetailsModal_5">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the modal from clicking on the <strong>Unit of Measure</strong> field
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/ManualTrans/UnitOfMeasureModal.png" style="width: 50%" alt="Manual Transactions Unit of Measure Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Unit of Measure:</strong> The options that are available when setting an item's unit of measure. This field is editable</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Add New:</strong> Adds a new row to enter a new unit of measure able to be selected
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Delete:</strong> Removes the corresponding unit of measure from the list
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Select:</strong> Selects the corresponding unit of measure value for the selected item's unit of measure and closes the modal
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Save:</strong> Saves any edits made to the corresponding unit of measure value
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li><strong>Close:</strong> Closes the modal <strong>without</strong> saving any changes made to any of the unit of measures</li>
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#ManTransOverview_3" data-parent="#ManualTransOverviewAccordion">
                                                    <h3 class="panel-title">
                                                        3 | Location Section
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ManTransOverview_3" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses information within the <strong>Location</strong> section
                                                                <div class="row" style="padding-top:5px;">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/ManualTrans/LocationSection.png" alt="Manual Transactions Location Section" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="LocationAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#LocationAccordion" data-target="#LocationOverview_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="LocationOverview_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information displayed in the <strong>Location</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Zone:</strong> The zone where this item is located</li>
                                                                                        <li><strong>Carousel:</strong> The carousel where this item is located</li>
                                                                                        <li><strong>Row:</strong> The row where this item is located</li>
                                                                                        <li><strong>Bin:</strong> This bin where this item is located</li>
                                                                                        <li><strong>Quantity Allocated Pick:</strong> The number of allocated picks of this item</li>
                                                                                        <li><strong>Quantity Allocated Put Away:</strong> The number of allocated put aways of this item</li>
                                                                                        <li><strong>Total Quantity:</strong> The total amount of this item</li>
                                                                                        <li><strong>Inventory Map ID:</strong> The ID of this item within the Inventory Map</li>
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
                                                <a data-toggle="collapse" data-parent="#ManualTransOverviewAccordion" data-target="#ManTransOverview_4">
                                                    <h3 class="panel-title">
                                                        4 | Transaction Details Section
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ManTransOverview_4" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses information within the <strong>Transaction Details</strong> section
                                                                <div class="row" style="padding-top:5px;">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/ManualTrans/TransactionDetailsSection.png" style="width: 85%" alt="Manual Transactions Transaction Details Section" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="TransDetailsAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#TransDetailsAccordion" data-target="#TransDetailOverview_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body accordion-toggle collapse" id="TransDetailOverview_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information displayed within the <strong>Transaction Details</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Transaction Type:</strong> The transaction type (pick, put away, or count) of this transaction</li>
                                                                                        <li><strong>Transaction Quantity:</strong> The amount of the item are needed for this transaction</li>
                                                                                        <li><strong>Host Transaction ID:</strong> The host id for this transaction</li>
                                                                                        <li><strong>Warehouse:</strong> The warehouse where this transaction is taking place</li>
                                                                                        <li><strong>Required Date:</strong> The date that the transaction has to be completed by</li>
                                                                                        <li><strong>Priority:</strong> The level of importance that this transaction has</li>
                                                                                        <li><strong>Batch Pick ID:</strong> The id of this transaction within Batch Pick</li>
                                                                                        <li><strong>Tote ID:</strong> The tote id assigned to this transaction</li>
                                                                                        <li><strong>Line Number:</strong> The number on the line of this transaction</li>
                                                                                        <li><strong>Line Sequence:</strong> The sequence on the line of this transaction</li>
                                                                                        <li><strong>Emergency:</strong> Marks this transaction as an emergency transaction</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#TransDetailsAccordion" data-target="#TransDetailOverview_2">
                                                                                <h3 class="panel-title">
                                                                                    Modals
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="TransDetailOverview_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            This panel discusses the modals found within the <strong>Transaction Details</strong> section
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="padding-top:5px;">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel-group" id="TransDetailsModals">
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-target="#TransDetailModal_1" data-parent="#TransDetailsModals">
                                                                                                            <h3 class="panel-title">
                                                                                                                Warehouse Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="TransDetailModal_1">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the modal from clicking on the <strong>Warehouse</strong> field
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/ManualTrans/WarehouseModal.png" style="width: 50%" alt="Manual Transactions Transaction Details Section Warehouse Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Warehouse:</strong> List of warehouses that are able to be selected</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Add New:</strong> Adds a new row to enter a new warehouse able to be selected
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Delete:</strong> Removes the corresponding warehouse from the list
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Select:</strong> Selects the corresponding warehouse value for the selected item's warehouse and closes the modal
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Save:</strong> Saves any edits made to the corresponding warehouse value
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li><strong>Close:</strong> Closes the modal <strong>without</strong> saving any changes made to any of the Warehouses</li>
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
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#TempManFunctions" data-parent="#ManualTransAccordion">
                        <h3 class="panel-title">
                            Temporary Manual Order Number Section Functions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="TempManFunctions">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses functionality within the <strong>Temporary Manual Order Number</strong> section
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="TempManFunctionsAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#TempManFunctionsAccordion" data-target="#TempManFunctions_1">
                                                    <h3 class="panel-title">
                                                        Selecting an Existing Temporary Order Number
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TempManFunctions_1" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to select an existing temporary order number from the typeahead
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Click within the <strong>Order Number</strong> field</li>
                                                            <li>Begin typing the characters of the desired order number</li>
                                                            <li>
                                                                As you type a dropdown (shown below) will appear containing any order numbers that begin with the typed characters. From here either keep
                                                                typing or if you see the desired order number just click on it
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/OrderNumberDropDown.png" alt="Manual Transactions Order NUmber Drop Down" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the desired order is selected the page will load the information from that order</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#TempManFunctionsAccordion" data-target="#TempManFunctions_2" style="background-color:white;">
                                                    <h3 class="panel-title">
                                                        Clearing an Order Number
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TempManFunctions_2" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to clear the <strong>Order Number</strong> field
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have an order number selected</li>
                                                            <li>
                                                                Press the <strong>Clear</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Clear</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once pressed, all fields on the page are reset to their default values, as the <strong>Order Number</strong> field is now empty</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#TempManFunctionsAccordion" data-target="#TempManFunctions_3" style="background-color:white;">
                                                    <h3 class="panel-title">
                                                        Adding a New Transaction
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TempManFunctions_3" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses adding a new manual transaction
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Press the <strong>Add New Transaction</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed the modal (shown below) appears. Input the desired values for the fields of the modal
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/AddNewModal.png" style="width: 50%" alt="Manual Transactions Add New Transaction Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once all the fields are filled out press the <strong>Save New Transaction</strong> button to save the new transaction, and close the modal.
                                                                If the <strong>close</strong> button is pressed, the modal will close <strong>without</strong> saving the new transaction
                                                            </li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#TempManFunctions_4" data-parent="#TempManFunctionsAccordion">
                                                    <h3 class="panel-title">
                                                        Saving any Changes Made to the Selected Transaction
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TempManFunctions_4" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to save any edits made to the selected transaction
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <strong>Steps:</strong>
                                                            <ol>
                                                                <li>Select the desired transaction to be edited</li>
                                                                <li>Edit the any of the valid desired fields</li>
                                                                <li>
                                                                    Once all edits are completed press the <strong>Save</strong> button (shown below).
                                                                    <ul>
                                                                        <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                    </ul>
                                                                </li>
                                                            </ol>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#TempManFunctions_5" data-parent="#TempManFunctionsAccordion">
                                                    <h3 class="panel-title">
                                                        Printing the Manual Transaction Label
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TempManFunctions_5" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to print the label for the selected manual transaction
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired manual transaction whose label is to be printed</li>
                                                            <li>
                                                                Once the transaction is selected press the <strong>Print</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>The labels will now be printed to the label printer specified for this work station</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#TempManFunctions_6" data-parent="#TempManFunctionsAccordion">
                                                    <h3 class="panel-title">
                                                        Deleting a Transaction
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TempManFunctions_6" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses deleting the selected transaction
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired transaction to be deleted</li>
                                                            <li>
                                                                Press the <strong>Delete</strong> transaction button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed a pop up (shown below) appears. To continue with the delete press <strong>Okay</strong>. TO stop the delete press <strong>Cancel</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/DeletePopUp.png" alt="Manual Transactions Delete Transaction Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>When <strong>Okay</strong> is pressed the transaction is deleted and the page resets (all fields are empty)</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#TempManFunctionsAccordion" data-target="#TempManFunctions_7">
                                                    <h3 class="panel-title">
                                                        Post Selected Transaction
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TempManFunctions_7" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses posting the selected transaction to <strong>Open Transactions</strong>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired transaction to post</li>
                                                            <li>
                                                                Press on the <strong>Post Transaction</strong> button (shown below)
                                                                <ul>
                                                                    <li>
                                                                        <div class="btn-group">
                                                                            <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown">Post Transaction <span class="caret"></span></button>
                                                                            <ul class="dropdown-menu" role="menu">
                                                                                <li><a>Post and Delete Transaction</a></li>
                                                                                <li><a>Post and Save Transaction</a></li>
                                                                            </ul>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed a dropdown appears (shown below). Select either to post and delete or post and save the transaction. Post and delete will
                                                                post the transaction to <strong>Open Transactions</strong> and then delete it from the manual transactions page. While post and save will
                                                                post the transaction to <strong>Open Transactions</strong> but keep it in the manual transactions page
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/PostDropDown.png" alt="Manual Transactions Post Transaction Button DropDown" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once an option is selected a pop up appears to confirm the desired action. To continue with the post press <strong>Okay</strong></li>
                                                            <li>Once the pop up is given the okay, the desired operation is implemented</li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ManualTransAccordion" data-target="#ItemDetailsFunctions">
                        <h3 class="panel-title">
                            Item Details Functions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ItemDetailsFunctions">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses the functionality found within the <strong>Item Details</strong> section
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="ItemDetailsFuncs">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ItemDetailsFuncs" data-target="#ItemDetailsFuncs_1">
                                                    <h3 class="panel-title">
                                                        Editing the Item Number and Location
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ItemDetailsFuncs_1" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses using a modal to edit the <strong>Item Number</strong> and <strong>Location</strong> fields
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the transaction that is going to be edited</li>
                                                            <li>
                                                                Click on the <strong>Item Number</strong> field to open the modal (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/SKUModal.png" style="width: 50%" alt="Manual Transactions Item Number Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Within the modal use the typehead on the <strong>Item Number</strong> field to select he desired item number (Item Number)
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/SKUModalItemNumberDropDown.png" alt="Manual Transactions Item Number Modal Item Number Drop Down" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once the item number is selected. Use the drop down to set the location by the item quantity at that location
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/SKUModalLocDropDown.png" alt="Manual Transactions Item Number Modal Location Drop Down" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once these fields have their desired values press the <strong>Set Item and Location</strong> button to close the modal and record these changes</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ItemDetailsFuncs" data-target="#ItemDetailsFuncs_2">
                                                    <h3 class="panel-title">
                                                        Editing the Supplier Item ID
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ItemDetailsFuncs_2" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses using a modal to edit the <strong>Supplier Item ID</strong> field
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the transaction that is to be edited</li>
                                                            <li>
                                                                Click on the <strong>Supplier Item ID</strong> field to bring up the modal (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/SuppItemIDModal.png" style="width: 50%" alt="Manual Transactions Supplier Item ID Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Within the modal use the typeahead to select the desired ID
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/SuppItemIDModalDropDown.png" alt="Manual Transactions Supplier Item ID Modal Drop Down" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the desired ID is selected press the <strong>Set</strong> button to record this for the selected transaction</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#ItemDetailsFuncs_3" data-parent="#ItemDetailsFuncs">
                                                    <h3 class="panel-title">
                                                        Editing the Notes Field
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ItemDetailsFuncs_3" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to edit the <strong>Notes</strong> field using a modal
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the transaction that is to be edited</li>
                                                            <li>
                                                                Click on the <strong>Notes</strong> field to bring the modal up
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/NotesModal.png" style="width: 50%" alt="Manual Transactions Notes Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the modal is up, type the desired info into the <strong>Notes</strong> section</li>
                                                            <li>
                                                                Once the desired information is completely typed press the <strong>Submit</strong> button to assign these notes to the selected
                                                                transaction
                                                            </li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ItemDetailsFuncs" data-target="#ItemDetailsFuncs_4">
                                                    <h3 class="panel-title">
                                                        Unit of Measure Functions
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ItemDetailsFuncs_4" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the functionality presented in the modal from clicking on the <strong>Unit of Measure</strong> (UM) field. In
                                                                order to be able to bring up this modal and transaction needs to be selected.
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <img src="/images/Help/ManualTrans/UnitOfMeasureModal.png" style="width: 50%" alt="Manual Transactions Unit of Measure Modal" />
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="UMFunctions">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#UMFunctions" data-target="#UMFuncs_1">
                                                                                <h3 class="panel-title">
                                                                                    Add New Unit of Measure
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="UMFuncs_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses adding a new <strong>Unit of Measure</strong>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>
                                                                                            Press the <strong>Add New</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once this button is pressed a new row will appear. Within the text area enter the new unit of measure that is able to be selected</li>
                                                                                        <li>
                                                                                            Once the new unit of measure is entered press the <strong>Save</strong> button (shown below) that is assigned to the new unit of measure
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once the <strong>Save</strong> button is pressed the newly added unit of measure is saved and will now appear when the modal is reloaded</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#UMFunctions" data-target="#UMFuncs_2">
                                                                                <h3 class="panel-title">
                                                                                    Deleting a Unit of Measure
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="UMFuncs_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to delete a unit of measure
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>
                                                                                            Press the delete button (shown below) associated with the unit of measure value that is going to deleted
                                                                                            <ul>
                                                                                                <li><button class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Once this button is pressed the entire row is deleted and the unit of measure value can no longer be selected unless it is re-added. Deleting
                                                                                            a unit of measure <strong>does not</strong> edit any transactions containing the deleted unit of measure
                                                                                        </li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-target="#UMFuncs_3" data-parent="#UMFunctions">
                                                                                <h3 class="panel-title">
                                                                                    Selecting a Unit of Measure
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="UMFuncs_3">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to select a unit of measure for the selected transactions
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>
                                                                                            Press the <strong>Select</strong> button (shown below) designated to the desired unit of measure
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once pressed the designated value is set for the selected transaction's unit of measure and the modal is closed</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#UMFunctions" data-target="#UMFuncs_4">
                                                                                <h3 class="panel-title">
                                                                                    Saving Any Changes
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="UMFuncs_4">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to save any changes made to a unit of measure
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Add a new or make a change to a unit of measure</li>
                                                                                        <li>
                                                                                            Press the <strong>Save</strong> button (shown below) for the row that was changed
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once this button is pressed the change is now saved and will appear whenever the modal is reloaded</li>
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ItemDetailsFuncs" data-target="#ItemDetailsFuncs_5">
                                                    <h3 class="panel-title">
                                                        Viewing and Setting User Fields
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ItemDetailsFuncs_5" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how view and set the user fields
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select a transaction in order enable the page</li>
                                                            <li>
                                                                Press on the <strong>View/Set User Fields</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary">View/Set User Fields</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed a modal (shown below) appears with all user fields listed out. From here type the desired value into the designated user field
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/UserFieldsModal.png" style="width: 50%" alt="Manual Transactions View/Set User Fields Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                As user fields are set you can either press the <strong>Save and Close</strong> button to save all the fields and close the modal, or press
                                                                the normal save button (shown below) to save the user fields and keep the modal open
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                </ul>
                                                            </li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#TransDetailsFunctions" data-parent="#ManualTransAccordion">
                        <h3 class="panel-title">
                            Transaction Detail Functions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="TransDetailsFunctions">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses functionality within the <strong>Transaction Details</strong> section
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="TransDetailFuncs">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#TransDetailFuncs" data-target="#TransDetailFuncs_1">
                                                    <h3 class="panel-title">
                                                        Editing the Transaction Type
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TransDetailFuncs_1" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to use the dropdown to select the <strong>Transaction Type</strong>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired transaction to edit</li>
                                                            <li>
                                                                Press on the <strong>Transaction Type</strong> field to show the dropdown (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/ManualTrans/TransTypeDropDown.png" alt="Manual Transactions Transaction Type Drop Down" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the dropdown appears select the desired type by clicking on the desired option</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#TransDetailFuncs" data-target="#TransDetailFuncs_2">
                                                    <h3 class="panel-title">
                                                        Warehouse Functions
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TransDetailFuncs_2" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the functionality presented in the modal from clickon on the <strong>Warehouse</strong> field.
                                                                In order to be able to bring this modal up a transaction needs to be selected
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <img src="/images/Help/ManualTrans/WarehouseModal.png" style="width: 50%" alt="Manual Transactions Warehouse Modal" />
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="WarehouseFuncs">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#WarehouseFuncs" data-target="#WarehouseFuncs_1">
                                                                                <h3 class="panel-title">
                                                                                    Adding a New Warehouse
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="WarehouseFuncs_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to add a new warehouse that is able to be selected
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>
                                                                                            Press the <strong>Add New</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once pressed a new empty row appears. Within the text are enter the new warehouse's value</li>
                                                                                        <li>
                                                                                            Once the desired value is entered press the <strong>Save</strong> button (shown below) desginated for the new row
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-target="#WarehouseFuncs_2" data-toggle="collapse" data-parent="#WarehouseFuncs">
                                                                                <h3 class="panel-title">
                                                                                    Deleting a Warehouse
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="WarehouseFuncs_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to delete a warehouse
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>
                                                                                            Press the <strong>Delete</strong> button (shown below) designated for the warehouse to be deleted
                                                                                            <ul>
                                                                                                <li><button class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Once this button is pressed the entire row is deleted and the warehouse value can no longer be selected unless it is re-added.
                                                                                            Deleting a warehouse <strong>does not</strong> edit any transactions containing the deleted warehouse
                                                                                        </li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-target="#WarehouseFuncs_3" data-parent="#WarehouseFuncs">
                                                                                <h3 class="panel-title">
                                                                                    Selecting a Warehouse
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="WarehouseFuncs_3">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to select the desired warehouse
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>
                                                                                            Press the <strong>Select</strong> button (shown below) desigated for the desired warehouse
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once selected the desired warehouse value is set for the selected transaction's warehouse value and the modal is closed</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#WarehouseFuncs" data-target="#WarehouseFuncs_4">
                                                                                <h3 class="panel-title">
                                                                                    Saving Any Changes Made to a Warehouse
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="WarehouseFuncs_4">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses saving any changes made to a warehouse
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Edit any of the existing warehouse values or add a new one and set its value</li>
                                                                                        <li>
                                                                                            Press the <strong>Save</strong> button (shown below) in order to keep any changes
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once this button is pressed the change is now saved and will appear whenever the modal is reloaded</li>
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#TransDetailFuncs_3" data-parent="#TransDetailFuncs">
                                                    <h3 class="panel-title">
                                                        Marking the Transaction as Emergency
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="TransDetailFuncs_3" style="background-color:white;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to mark a transaction as an emergency transaction and what will result
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the transaction that is going to be marked as emergency</li>
                                                            <li>Check or make sure that the <strong>Emergency</strong> checkbox is checked</li>
                                                            <li>The transaction is now marked as an emergency</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Results:</strong>
                                                        <ul>
                                                            <li>A pop up will appear saying that an emergency transaction is present and needs to be completed</li>
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
<map name="mantransmap">
    <area shape="rect" coords="3, 40, 590, 107" data-target="#ManTransOverview_1" data-toggle="collapse" data-parent="#ManualTransOverviewAccordion" />
    <area shape="rect" coords="7, 126, 485, 338" data-target="#ManTransOverview_2" data-toggle="collapse" data-parent="#ManualTransOverviewAccordion" />
    <area shape="rect" coords="502, 121, 716, 389" data-target="#ManTransOverview_3" data-toggle="collapse" data-parent="#ManualTransOverviewAccordion" />
    <area shape="rect" coords="6, 409, 718, 615" data-target="#ManTransOverview_4" data-toggle="collapse" data-parent="#ManualTransOverviewAccordion" />
</map>
