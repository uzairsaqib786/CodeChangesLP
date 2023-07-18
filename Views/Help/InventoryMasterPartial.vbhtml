<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
Layout = nothing
End Code
<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="InventoryMasterAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#InvMastOverview" data-parent="#InventoryMasterAccordion">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="InvMastOverview">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This page is the <strong>Inventory Master</strong>. It displays the information for an item number.
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <img src="/images/Help/InventoryMaster/InitialLoadScreen.png" style="width: 60%" alt="Inventory Master Load Screen" usemap="#invmastmap" />
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="InvMastOverviewAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#InvMastOverviewAccordion" data-target="#panelInvMast_1">
                                                    <h3 class="panel-title">
                                                        1 | Item Number Lookup
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelInvMast_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Buttons:</strong>
                                                        <ul>
                                                            <li>
                                                                <strong>Clear:</strong> This button will reset the page (including tabs).
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Clear</button></li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Filters:</strong>
                                                        <ul>
                                                            <li>The item number field will determine which item number's information is displayed.</li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-target="#DetailsTab" data-parent="#InvMastOverviewAccordion" data-toggle="collapse">
                                                    <h3 class="panel-title">
                                                        2 | Details Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="DetailsTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>Details</strong> tab.
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/InventoryMaster/DetailsTab.png" style="width: 80%" alt="Inventory Master Details Tab" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="DetailsOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#DetailsOverviewAccordion" data-target="#panelDetails_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelDetails_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information that is displayed within the <strong>Details</strong> tab.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information Displayed:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Item Number:</strong> The selected item number</li>
                                                                                        <li><strong>Item Description:</strong> The description of the item</li>
                                                                                        <li><strong>Supplier Item ID:</strong> The ID assigned to the supplier of the item</li>
                                                                                        <li><strong>Unit of Measure:</strong> The unit of measure assigned to the item</li>
                                                                                        <li><strong>Reorder Point:</strong> The number that indicates when to reorder the item</li>
                                                                                        <li><strong>Replenishment Point:</strong> The number that indicates when to replenish the item</li>
                                                                                        <li><strong>Category:</strong> The category assigned to this item number</li>
                                                                                        <li><strong>Reorder Quantity:</strong> The amount to reorder this item by</li>
                                                                                        <li><strong>Replenishment Level:</strong> The amount to replenish this item by</li>
                                                                                        <li><strong>Sub Category:</strong> The sub category attacked to the category</li>
                                                                                        <li><strong>KanBan Replenishment Level:</strong> The amount to replenish this item to if it's a KanBan</li>
                                                                                        <li><strong>KanBan Replenishment Point:</strong> The amount that tells when this item should be replenished if it's a KanBan</li>
                                                                                        <li><strong>Total Quantity:</strong> Total number of this item number</li>
                                                                                        <li><strong>Allocated Picks:</strong> Total number of picks</li>
                                                                                        <li><strong>Allocated Put Aways:</strong> Total number of put aways</li>
                                                                                        <li><strong>Open Transactions:</strong> Number of records in open transaction</li>
                                                                                        <li><strong>Transaction History:</strong> Number of records in transaction history</li>
                                                                                        <li><strong>Reprocess Transactions:</strong> Number of records in reprocess</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>View Open:</strong> Opens the open transaction page with all records associated with the item number displayed
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">View Open</button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>View History:</strong> Opens the transaction history page with all records associated with the item number displayed
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">View History</button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>View Reprocess:</strong> Opens the reprocess page with all records associated with the item number displayed
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">View ReProcess</button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-target="#DetailModals" data-parent="#DetailsOverviewAccordion" data-toggle="collapse">
                                                                                <h3 class="panel-title">
                                                                                    Modals
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body accordion-toggle collapse" id="DetailModals">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            This panel describes all the modals found within the <strong>Details</strong> tab.
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="padding-top:5px">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel-group" id="DetailModalsAccordion">
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#DetailModalsAccordion" data-target="#panelDetailModal_1">
                                                                                                            <h3 class="panel-title">
                                                                                                                Description Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="panelDetailModal_1">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel describes the modal for description. To bring up this modal click on the <strong>Description</strong> field.
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/InventoryMaster/DescriptionModal.png" style="width: 50%" alt="Inventory Master Description Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Submit:</strong> Sets the description for the item number to that of the one typed in the box.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary">Submit</button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Close:</strong> Cancels editing the description and any changes are lost.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-default">Close</button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#DetailModalsAccordion" data-target="#panelDetailModal_2">
                                                                                                            <h3 class="panel-title">
                                                                                                                Unit of Measure Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="panelDetailModal_2">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel describes the modal for Unit of Measure. To bring up this modal click  on the
                                                                                                                <strong>Unit of Measure</strong> field.
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/InventoryMaster/UnitOfMeasureModal.png" style="width: 50%" alt="Inventory Master Unit of Measure Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Add New:</strong> Inserts a new row to add a Unit of Measure that is able to be used.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Delete:</strong> Deletes the specified row. This <strong>does not clear the field</strong> where the deleted unit of measure is used.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Select:</strong> Selects the designated value for the item's unit of measure.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-share"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Save:</strong> Saves any changes made to the row's unit of measure name.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Close:</strong> Closes the modal
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-default">Close</button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#DetailModalsAccordion" data-target="#panelDetailModal_3">
                                                                                                            <h3 class="panel-title">
                                                                                                                Category and Sub Category Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="panelDetailModal_3">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel describes the modal for category and sub category. To open this modal click on either the <strong>Category</strong> or
                                                                                                                <strong>Sub Category</strong> fields.
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/InventoryMaster/CategoryAndSubCategoryModal.png" style="width: 50%" alt="Inventory Master Add Button Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Add New:</strong> Inserts a new row to add a Category and Sub Category that is able to be used.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Delete:</strong> Deletes the specified row. This <strong>does not clear the field</strong> where the deleted Category and Sub Category is used.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Select:</strong> Selects the designated value for the item's category and sub category.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-share"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Save:</strong> Saves any changes made to the row's category and/or sub category name.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Print:</strong> Prints the category report.
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Close:</strong> Closes the modal
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-default">Close</button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#DetailModalsAccordion" data-target="#panelDetailModal_4">
                                                                                                            <h3 class="panel-title">
                                                                                                                Item Number Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="panelDetailModal_4">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel describes the modal for changing an Item Number. To open this modal click on the <strong>Item Number</strong> field
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/InventoryMaster/EditItemNumModal.png" alt="Inventory Master Edit Item Number Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Old Item Number:</strong> The original item number</li>
                                                                                                                    <li><strong>New Item Number:</strong> The item number that is going to replace the old one</li>
                                                                                                                    <li><strong>Save</strong> button: Overwrites the old item number with the new item number
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary">Save</button></li>
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#InvMastOverviewAccordion" data-target="#ItemSetupTab">
                                                    <h3 class="panel-title">
                                                        3 | Item Setup Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body accordion-toggle collapse" id="ItemSetupTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel describes the <strong>Item Setup</strong> tab.
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/InventoryMaster/ItemSetupTab.png" style="width: 80%" alt="Inventory Master Item Setup Tab" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="ItemSetupOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#ItemSetupOverviewAccordion" data-target="#panelItemSetup_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelItemSetup_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information that displayed within the <strong>Item Setup Tab</strong>.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information Displayed:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Date Sensitive:</strong> Indicates whether the item is date sensitive</li>
                                                                                        <li><strong>Warehouse Sensitive:</strong> Indicates whether the item is warehouse sensitive</li>
                                                                                        <li><strong>Primary Zone:</strong> Displays the primary zone for the item</li>
                                                                                        <li><strong>Secondary Zone:</strong> Displays the secondary zone for the item</li>
                                                                                        <li><strong>FIFO:</strong> Indicates if the item is First In First Out</li>
                                                                                        <li><strong>Pick Fence Quantity:</strong> Displays the Pick Fence Quantity for the item. Also, disappears when FIFO checked</li>
                                                                                        <li><strong>Split Case:</strong> Indicates if the item is a split case. Also, disappears when FIFO checked</li>
                                                                                        <li><strong>Case Quantity:</strong> Displays the case quantity for the item</li>
                                                                                        <li><strong>Pick Sequence:</strong> Displays the pick sequence for the item</li>
                                                                                        <li><strong>Active:</strong> Indicates if the item is active</li>
                                                                                        <li>
                                                                                            <strong>Carousel:</strong>
                                                                                            <ul>
                                                                                                <li><strong>Cell Size:</strong> Carousel cell size for the item</li>
                                                                                                <li><strong>Velocity Code:</strong> Carousel velocity code for the item</li>
                                                                                                <li><strong>Minimum Cell Quantity:</strong> Carousel minimum cell quantity for the item</li>
                                                                                                <li><strong>Maximum Cell Quantity:</strong> Carousel maximum cell quantity for the item</li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Bulk</strong>
                                                                                            <ul>
                                                                                                <li><strong>Cell Size:</strong> Bulk cell size for the item</li>
                                                                                                <li><strong>Velocity Code:</strong> Bulk velocity code for the item</li>
                                                                                                <li><strong>Minimum Cell Quantity:</strong> Bulk minimum cell quantity for the item</li>
                                                                                                <li><strong>Maximum Cell Quantity:</strong> Bulk Maximum cell quantity for the item</li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Carton Flow:</strong>
                                                                                            <ul>
                                                                                                <li><strong>Cell Size:</strong> Carton Flow cell size for the item</li>
                                                                                                <li><strong>Velocity Code:</strong> Carton Flow velocity code for the item</li>
                                                                                                <li><strong>Minimum Cell Quantity:</strong> Carton Flow minimum cell quantity for the item</li>
                                                                                                <li><strong>Maximum Cell Quantity:</strong> Carton Flow maximum cell quantity for the item</li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#ItemSetupOverviewAccordion" data-target="#panelItemSetup_2">
                                                                                <h3 class="panel-title">
                                                                                    Modals
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelItemSetup_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            This panel describes the modals found on the <strong>Item Setup</strong> tab.
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="padding-top:5px;">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel-group" id="ItemSetupModals">
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#ItemSetupModals" data-target="#panelItemSetupModal_1">
                                                                                                            <h3 class="panel-title">
                                                                                                                Cell Size Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="panelItemSetupModal_1">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel describes the cell size modal found in the <strong>Item Setup</strong> tab. In order to
                                                                                                                open this modal, click on any of the <strong>Cell Size</strong> boxes. From this modal the cell sizes and types are
                                                                                                                able to be deleted, selected, saved (if edited), and inserted (new cell size and type).
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/InventoryMaster/CellSizeModal.png" style="width: 50%" alt="Inventory Master Item Setup Cell Size Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Add New:</strong> Adds a new row to insert a new cell size and type
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Delete:</strong> Deletes the selected row. <strong>Records with deleted cell size and type are not affected</strong>
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Select:</strong> Selects the desired row's cell size and type for the item number
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-share"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Save:</strong> Saves any edits made to the cell size or type of the desired row
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Close:</strong> Closes the modal. <strong>Any edits that are not saved are lost</strong>
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-default">Close</button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#ItemSetupModals" data-target="#panelItemSetupModal_2">
                                                                                                            <h3 class="panel-title">
                                                                                                                Velocity Code Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="panelItemSetupModal_2">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel describes the velocity code modal found in the <strong>Item Setup</strong> tab. In order to
                                                                                                                open this modal, click on any of the <strong>Velocity Code</strong> boxes. From this modal the velocity codes are
                                                                                                                able to be deleted, selected, saved (if edited), and inserted (new velocity code).
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/InventoryMaster/VelocityCodeModal.png" style="width: 50%" alt="Inventory Master Item Setup Velocity Code Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Add New:</strong> Adds a new row to insert a new cell size and type
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Delete:</strong> Deletes the selected row. <strong>Records with deleted cell size and type are not affected</strong>
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Select:</strong> Selects the desired row's cell size and type for the item number
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-share"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Save:</strong> Saves any edits made to the cell size or type of the desired row
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Close:</strong> Closes the modal. <strong>Any edits that are not saved are lost</strong>
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-default">Close</button></li>
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#InvMastOverviewAccordion" data-target="#KitItemsTab">
                                                    <h3 class="panel-title">
                                                        4 | Kit Items Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="KitItemsTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel describes the <strong>Kit Items</strong> Tab.
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/InventoryMaster/KitItemsTab.png" style="width: 80%" alt="Inventory Master Kit Items Tab" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="KitItemsOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#KitItemsOverviewAccordion" data-target="#panelKitItems_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelKitItems_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel describes the information and buttons displayed in the <strong>Kit Items</strong> tab.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information Displayed</strong>
                                                                                    <ul>
                                                                                        <li><strong>Item Number:</strong> The item number of the item that is contained within the kit</li>
                                                                                        <li><strong>Description:</strong> The description assigned to the item number</li>
                                                                                        <li><strong>Kit Quantity:</strong> The quantity of the item within the kit</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Print:</strong> Prints the kit items report
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Add New:</strong> Adds a new row for inserting a new item number into the kit
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete:</strong> Deletes row from the kit
                                                                                            <ul>
                                                                                                <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Save:</strong> Saves any edits made to any editable field within the row
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#KitItemsOverviewAccordion" data-target="#panelKitItems_2">
                                                                                <h3 class="panel-title">
                                                                                    Modals
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelKitItems_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            This panel describes the modals found within the <strong>Kit Items</strong> tab.
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="padding-top:5px;">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel-group" id="KitItemsModals">
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#KitItemsModals" data-target="#panelKitItemsModal_1">
                                                                                                            <h3 class="panel-title">
                                                                                                                Item Number Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="panelKitItemsModal_1">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel describes the item number modal. In order to open this modal click on any of
                                                                                                                the <strong>Item Number</strong> boxes. This modal is used to select the item number that
                                                                                                                is contained within the kit.
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/InventoryMaster/KitItemItemNumberModal.png" style="width: 50%" alt="Inventory Master Kit Items Item Number Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Filters:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Item Number:</strong> The item number to be contained within the kit</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Close:</strong> Closes the modal, and <strong>discards any changes made</strong>
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-default">Close</button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Submit:</strong> Closes the modal, and <strong>updates the item number field to the new selected item number</strong>
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-primary">Submit</button></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-parent="#KitItemsModals" data-target="#panelKitItemsModal_2">
                                                                                                            <h3 class="panel-title">
                                                                                                                Description Modal
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="panelKitItemsModal_2">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel describes the description modal. To open this modal click on any of the <strong>Description</strong> boxes.
                                                                                                                This modal displays the entire description in order to make it easier to read.
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/InventoryMaster/KitItemDescriptionModal.png" style="width: 50%" alt="Inventory Master Kit Items Description Modal" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Close:</strong> Closes the modal
                                                                                                                        <ul>
                                                                                                                            <li><button class="btn btn-default">Close</button></li>
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-target="#LocationsTab" data-parent="#InvMastOverviewAccordion" data-toggle="collapse">
                                                    <h3 class="panel-title">
                                                        5 | Locations Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="LocationsTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel describes the <strong>Location</strong> tab.
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/InventoryMaster/LocationsTab.png" style="width: 80%" alt="Inventory Master Locations Tab" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="LocationOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#LocationOverviewAccordion" data-target="#panelLocations_1">
                                                                                <h3 class="panel-title">
                                                                                    Information Displayed
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelLocations_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel displays all locations from <strong>Inventory Map</strong> with a matching item number within the data table.
                                                                                    For more information on <strong>Inventory Map</strong> see the <strong>Inventory Map</strong> help section. For more information
                                                                                    on data tables see the <strong>Tables</strong> Section.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Refresh Table:</strong> Refreshes the table in order to display the most accurate data.
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">Refresh Table</button></li>
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
                                                <a data-toggle="collapse" data-parent="#InvMastOverviewAccordion" data-target="#ReelTrackingTab">
                                                    <h3 class="panel-title">
                                                        6 | Reel Tracking Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ReelTrackingTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>Reel Tracking</strong> tab.
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/InventoryMaster/ReelTrackingTab.png" style="width: 80%" alt="Inventory Master Reel Tracking Tab" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="ReelTrackingOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#ReelTrackingOverviewAccordion" data-target="#panelReelTracking_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelReelTracking_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Minimum RTS Reel Quantity:</strong> The minimum rts reel quantity value for the item</li>
                                                                                        <li><strong>Include in Auto RTS Update:</strong> Checkbox that enables this item to be included in the update all sequence</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Update Minimum RTS Reel Quantity For All:</strong> Opens the modal for updating the <strong>Minimum RTS Reel Quantity</strong> for all items who meet the conditions.
                                                                                            See <strong>Editing</strong> and the modal panel below for more information.
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">Update Minimum RTS Reel Quantity For All</button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-target="#panelReelTracking_2" data-parent="#ReelTrackingOverviewAccordion" data-toggle="collapse">
                                                                                <h3 class="panel-title">
                                                                                    Update Minimum RTS Reel Quantity For All Modal
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelReelTracking_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the modal which occurs after pressing the <strong>Update Minimum RTS Reel Quantity For All</strong>
                                                                                    button. This modal is used to update the Minimum RTS Reel Quantity for all item numbers who have a sub-category of Reel Tracking and have
                                                                                    the Include in Auto RTS Update checkbox checked.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <img src="/images/Help/InventoryMaster/UpdateRTSModal.png" style="width: 50%" alt="Inventory Master Reel Tracking Tab Update RTS Modal" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Inputs:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Minimum Dollar Amount to RTS:</strong> The minimum amount of money that is allowed to be returned to stock.</li>
                                                                                        <li><strong>RTS Threshold Max Qty:</strong> The maximum quantity that is allowed to be returned to stock</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Close:</strong> Closes the modal, and <strong>discards any changes made</strong>
                                                                                            <ul>
                                                                                                <li><button class="btn btn-default">Close</button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Submit:</strong> Closes the modal, and updates the Minimum RTS Reel Quantity to the computed value based upon the inputs. For more information
                                                                                            see the <strong>Editing Reel Tracking</strong> section.
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary">Submit</button></li>
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
                                                <a data-toggle="collapse" data-parent="#InvMastOverviewAccordion" data-target="#ScanCodesTab">
                                                    <h3 class="panel-title">
                                                        7 | Scan Codes Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ScanCodesTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>Scan Codes</strong> tab.
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/InventoryMaster/ScanCodesTab.png" style="width: 80%" alt="Inventory Master Scan Codes Tab" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="ScanCodesOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#ScanCodesOverviewAccordion" data-target="#panelScanCodes_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelScanCodes_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information and buttons displayed within the <strong>Scan Codes</strong> tab.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Displayed Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Scan Code:</strong> The scan code for this shipment</li>
                                                                                        <li><strong>Scan Type:</strong> The carrier for this shipment</li>
                                                                                        <li><strong>Scan Range:</strong> Dropdown to select if this shipment has a scan range</li>
                                                                                        <li><strong>Start Position:</strong> The starting position of this shipment</li>
                                                                                        <li><strong>Code Length:</strong> The code length of this shipment</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Add New:</strong> Adds a new row for inserting a new scan code for the item.
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete:</strong> Deletes the selected scan code row from the item number.
                                                                                            <ul>
                                                                                                <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Save:</strong> Saves any edits made to any editable field within the row
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#ScanCodesOverviewAccordion" data-target="#panelScanCodes_2">
                                                                                <h3 class="panel-title">
                                                                                    Scan Type Modal
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelScanCodes_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the scan type modal. In order to open this modal click on any of the <strong>Scan Type</strong>
                                                                                    columns.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <img src="/images/Help/InventoryMaster/ScanTypeModal.png" style="width: 50%" alt="Inventory Master Scan Codes Tab Scan Type Modal" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Add New:</strong> Adds a new row to insert a new cell size and type
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete:</strong> Deletes the selected row. <strong>Records with deleted cell size and type are not affected</strong>
                                                                                            <ul>
                                                                                                <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Select:</strong> Selects the desired row's cell size and type for the item number
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Save:</strong> Saves any edits made to the cell size or type of the desired row
                                                                                            <ul>
                                                                                                <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Close:</strong> Closes the modal. <strong>Any edits that are not saved are lost</strong>
                                                                                            <ul>
                                                                                                <li><button class="btn btn-default">Close</button></li>
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
                                                <a data-toggle="collapse" data-parent="#InvMastOverviewAccordion" data-target="#WeighScaleTab">
                                                    <h3 class="panel-title">
                                                        8 | Weigh Scale Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="WeighScaleTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>Weigh Scale</strong> tab.
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/InventoryMaster/WeighScaleTab.png" style="width: 80%" alt="Inventory Master Weigh Scale Tab" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="WeighScaleOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#WeighScaleOverviewAccordion" data-target="#panelWeighScale_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelWeighScale_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information that is displayed within the <strong>Weigh Scale</strong> tab.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information Displayed:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Use Scale:</strong> Checkbox to indicate whether to use the scale for this item</li>
                                                                                        <li><strong>Average Piece Weight:</strong> The average weight of a sample for this item</li>
                                                                                        <li><strong>Sample Quantity:</strong> The amount of the item is in a sample</li>
                                                                                        <li><strong>Min Use Scale Trans Quantity:</strong> The minimum quantity of the item to be used with the scale</li>
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
                                                <a data-toggle="collapse" data-parent="#InvMastOverviewAccordion" data-target="#OtherTab">
                                                    <h3 class="panel-title">
                                                        9 | Other Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="OtherTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>Other</strong> tab.
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/InventoryMaster/OtherTab.png" style="width: 80%" alt="Inventory Master Other Tab" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="OtherOverviewAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#OtherOverviewAccordion" data-target="#panelOther_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="panelOther_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the displayed information within the <strong>Other</strong> tab.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information Displayed:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Unit Cost:</strong> The unit cost of the item.</li>
                                                                                        <li><strong>Supplier ID:</strong> The ID of the supplier of this item</li>
                                                                                        <li><strong>Manufacturer ID:</strong> The ID of the manufacturer of this item</li>
                                                                                        <li><strong>Special Features:</strong> The item's special features</li>
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
                                                <a data-toggle="collapse" data-parent="#InvMastOverviewAccordion" data-target="#panelInvMast_10">
                                                    <h3 class="panel-title">
                                                        10 | Functionality Buttons
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelInvMast_10">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses the functionality buttons.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Buttons:</strong>
                                                        <ul>
                                                            <li>
                                                                <strong>Add New:</strong> Will trigger modal to add a new item number
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                <strong>Save:</strong> Saves any edit made to any values in the tabs
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                <strong>Delete:</strong> Deletes the shown item number
                                                                <ul>
                                                                    <li><button class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                <strong>Quarantine/Un-Quarantine:</strong> Quarantines or Un-Quarantines the item depending on if it is already quarantined.
                                                                The text in this button changes to show which operation will occur.
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Quarantine Item</button></li>
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
                    <a data-toggle="collapse" data-target="#InvMastSelectInfo" data-parent="#InventoryMasterAccordion">
                        <h3 class="panel-title">
                            Displaying an Item Number's Information
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="InvMastSelectInfo">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses how to display an item number's information.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>Begin typing in the desired item number</li>
                                <li>
                                    Either continue typing in the item number or select the item number from the dropdown (see below)
                                    <ul>
                                        <li><img src="/images/Help/InventoryMaster/ItemNumberDropDown.png" alt="Inventory Master Item NUmber Typeahead Dropdown" /></li>
                                    </ul>
                                </li>
                                <li>Once the item number is selected all fields within the tabs are filled with the selected item number's information</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#ClearItem" data-parent="#InventoryMasterAccordion">
                        <h3 class="panel-title">
                            Clear Item Number
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ClearItem">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses clearing an item number.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>
                                    Press the <strong>Clear</strong> button (shown below) next to the item number inout box.
                                    <ul>
                                        <li><button class="btn btn-primary">Clear</button></li>
                                    </ul>
                                </li>
                                <li>Once the button is pressed the entire <strong>Inventory Master</strong> will have no data being displayed. As the item number is blank. <strong>Clearing is not deleting an item number</strong> </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#InventoryMasterAccordion" data-target="#AddingNewItem">
                        <h3 class="panel-title">
                            Adding New Records
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="AddingNewItem">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses adding a new item number, kit, and scan code.
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px">
                                <div class="col-md-12">
                                    <div class="panel-group" id="AddAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#AddAccordion" data-target="#panelAdd_1">
                                                    <h3 class="panel-title">
                                                        Adding a New Item Number
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelAdd_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to add a new item number.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Click on the add new button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                A modal should have appeared (shown below). Fill out the new item number and description for the new item.
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/AddNewItemModal.png" style="width: 50%" alt="Inventory Master Add New Item Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once these fields are filled out press the <strong>Add Item</strong> button (shown below).
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Add Item</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once this button is pressed the item number is added and displayed as the selected item number</li>
                                                            <li>From here any of the fields of the item are able to be assigned their correct values</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#AddAccordion" data-target="#panelAdd_2">
                                                    <h3 class="panel-title">
                                                        Adding New Item to Kit
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelAdd_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses adding a new item to the kit
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Click on the <strong>Add New</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                A new empty row will appear, click on the <strong>Item Number</strong> column of this row ini order to open the modal for selecting a new item number (shown below).
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/KitItemItemNumberModal.png" style="width: 50%" alt="Inventory Master Kit Items Item Number Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Begin typing into the box in order either select an item number form the dropdown (shown below), or type in the entire item number. For more information on typeheads see the <strong>Typeahead</strong> section
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/KitItemsItemNumberDropDown.png" style="width: 50%" alt="Inventory Master Kit Items Item Number Modal DropDown" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once an item number is selected press the submit button (shown below), and the item number and description columns will be filled within the row.
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Submit</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>The last step is to insert the number if this item in the kit under the <strong>Kit Quantity</strong> column.</li>
                                                            <li>
                                                                With all the information entered the last step is to press the save button (shown below) associated with the row. This will save the new item into the kit
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
                                                <a data-toggle="collapse" data-target="#panelAdd_3" data-parent="#AddAccordion">
                                                    <h3 class="panel-title">
                                                        Add New Scan Code
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelAdd_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to add a new scan code
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                The first step is to press the <strong>Add New</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                A new empty row now appears from here enter in the scan code, start position, code length,
                                                                select a scan type from the modal (shown below), and select the scan range (either yes or no) from the dropdown
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/ScanTypeModal.png" style="width: 50%" alt="Inventory Master Scan Codes Tab Scan Type Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once all the fields are entered press the save button (shown below), and the new scan code will saved to this item number
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
                    <a data-target="#EditingItem" data-toggle="collapse" data-parent="#InventoryMasterAccordion">
                        <h3 class="panel-title">
                            Editing an Item Number's Fields
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="EditingItem">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses editing and saving different parts of an item number. There are four different edits that happen they are:
                                    <ul>
                                        <li><strong>General Editing:</strong> Editing on most tabs</li>
                                        <li><strong>Kit Editing:</strong> Editing from the Kit Items Tab</li>
                                        <li><strong>ReelTracking Edit:</strong> Editing from the Reel Tracking Tabs</li>
                                        <li><strong>Scan Code Editing:</strong> Editing from the Scan Codes Tab</li>
                                        <li><strong>Item Number Editing:</strong> Changing an Item Number</li>
                                    </ul>
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="EditAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EditAccordion" data-target="#GeneralEditing">
                                                    <h3 class="panel-title">
                                                        General Editing
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GeneralEditing">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses editing and saving any changes made to an item number that are <strong>not within the kit items, reel tracking, and scan codes tabs</strong>.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps to Edit and Save</strong>
                                                        <ol>
                                                            <li>The first step is to change any of the fields (except for those in the tabs defined above) that require changing.</li>
                                                            <li>
                                                                Once all the desired fields are changed, the save button (shown below) needs to be pressed
                                                                <ul>
                                                                    <li>
                                                                        <strong>Save:</strong> Saves any edit made to any values in the tabs
                                                                        <ul>
                                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                        </ul>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                            <li>With the save button pressed all changes to that item number across all the tabs <strong>except kit items, reel tracking, and scan codes</strong> are saved</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EditAccordion" data-target="#KitEditing">
                                                    <h3 class="panel-title">
                                                        Kit Editing
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="KitEditing">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses saving any edits made to any kits. This is a separate saving tab, and as such is not included in the general save.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Edit the fields in any of the rows</li>
                                                            <li>
                                                                Press the <strong>Save</strong> button associated with the row that was edited.
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
                                                <a data-target="#ReelTrackingEdit" data-parent="#EditAccordion" data-toggle="collapse">
                                                    <h3 class="panel-title">
                                                        Reel Tracking Editing
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id=ReelTrackingEdit>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses saving edits made using the reel tracking modal. In order for an item number to be updated from the modal's results
                                                        the <strong>Include in Auto RTS Update</strong> checkbox needs to be checked, and the subcategory must be <strong>Reel Tracking</strong>. All values in this tab are also auto saved.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <img src="/images/Help/InventoryMaster/UpdateRTSModal.png" style="width: 50%" alt="Inventory Master Reel Tracking Tab Update RTS Modal" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Logic:</strong>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                There are three situations
                                                            </div>
                                                        </div>
                                                        <ul>
                                                            <li>
                                                                If the item number's unit cost is greater than 0 and the <strong>RTS Threshold Max Qty</strong> divided by the unit cost (each item numbers specific one) is greater than <strong>RTS Threshold Max Qty</strong>
                                                                <ul>
                                                                    <li>The <strong>Minimum RTS Reel Quantity</strong> is updated to the <strong>RTS Threshold Max Qty</strong> for all item numbers who satisfy the condition above.</li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                If the item number's unit cost is greater than 0 and the <strong>RTS Threshold Max Qty</strong> divided by the unit cost (each item numbers specific one) is less than <strong>RTS Threshold Max Qty</strong>
                                                                <ul>
                                                                    <li>The <strong>Minimum RTS Reel Quantity</strong> is updated to the <strong>RTS Threshold Max Qty</strong> divided by the unit cost (each item numbers specific one) for all item numbers who satisfy the condition above.</li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                If the item number's unit cost is 0
                                                                <ul>
                                                                    <li>The <strong>Minimum RTS Reel Quantity</strong> is updated to the <strong>RTS Threshold Max Qty</strong> for all item numbers who satisfy the condition above.</li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EditAccordion" data-target="#ScanCodeEdit">
                                                    <h3 class="panel-title">
                                                        Scan Codes Editing
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ScanCodeEdit">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses saving any edits made to any for the scan code rows. This is a separate saving tab as each row has its own save button, and as such
                                                        is not included in the general save.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Change any of the desired columns for a scan code</li>
                                                            <li>
                                                                Press the save button (shown below) to save the changes made to the according scan code row.
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
                                                <a data-toggle="collapse" data-parent="#EditAccordion" data-target="#ItemNumberEdit">
                                                    <h3 class="panel-title">
                                                        Item Number Editing
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ItemNumberEdit">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have the Inventory Master page open</li>
                                                            <li>Select the desired item, whose item number is going to change</li>
                                                            <li>Once selected click on the Item Number field found in the <strong>Details</strong> section</li>
                                                            <li>Once clicked the Item Number Edit Modal opens. Within this modal enter the new item number value for this item</li>
                                                            <li>Once entered press the <strong>Save</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once pressed a pop up (shown below) appears. To continue with the operation press <strong>OK</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/EditItemNumModalPopUp.png" alt="Inventory Master Edit Item Number First Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once confirmed another pop up (shown below) appears. To change the item number press <strong>OK</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/EditItemNumModalPopUp2.png" alt="Inventory Master Edit Item Number Second Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once confirmed the item number is changed for the selected item</li>
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
                    <a data-toggle="collapse" data-parent="#InventoryMasterAccordion" data-target="#Deleting">
                        <h3 class="panel-title">
                            Deleting Records
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="Deleting">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses deleting the folowing items:
                                    <ul>
                                        <li>Delete an Item Number</li>
                                        <li>Delete an Item from a kit</li>
                                        <li>Delete a scan code</li>
                                    </ul>
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="DeleteAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#DeleteAccordion" data-target="#panelDelete_1">
                                                    <h3 class="panel-title">
                                                        Deleting an Item Number
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelDelete_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses deleting an entire item number.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Click on the <strong>Delete Item</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                When deleted a pop up (shown below) will appear. IN order to delete the item number press okay on the pop up.
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/DeletePopUp.png" alt="Inventory Master Delete Item Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once given the okay, the item number is deleted, and an event log message is recorded.</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#DeleteAccordion" data-target="#panelDelete_2">
                                                    <h3 class="panel-title">
                                                        Deleting an Item from a Kit
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelDelete_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses deleting (removing) an item from the kit
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Press the delete button (shown below) associated with the desired item in the kit to be deleted.
                                                                <ul>
                                                                    <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once the delete button is pressed, a pop up (shown below) opens. To remove the item, the okay button on the modal has to be pressed
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/KitItemDeletePopUp.png" alt="Inventory Master Kit Items Delete Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once given the okay the row disappears from the <strong>Kit Items Tab</strong> and is deleted from the kit.</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#panelDelete_3" data-parent="#DeleteAccordion">
                                                    <h3 class="panel-title">
                                                        Deleting a Scan Code
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelDelete_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses deleting a scan code for an item number.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Press the delete button (shown below) associated with the scan code to be deleted
                                                                <ul>
                                                                    <li><button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once this occurs, a pop up (shown below) is opened to confirm the delete. Press okay on the pop up to delete the scan code
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/ScanCodeDeletePopUp.png" alt="Inventory Master Scan Codes Delete Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once given the okay, the row disappears from the <strong>Scan Codes Tab</strong> as the scan code no longer exists and was successfully deleted</li>
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
                    <a data-toggle="collapse" data-parent="#InventoryMasterAccordion" data-target="#QuarUnQuarItem">
                        <h3 class="panel-title">
                            Quarantine and Un-Quarantine an Item Number
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="QuarUnQuarItem">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses quarantining and un-quarantining an item number.
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="QuarStuffAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#QuarStuffAccordion" data-target="#panelQuarantine">
                                                    <h3 class="panel-title">
                                                        Quarantining an Item Number
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelQuarantine">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to quarantine an item. If an item is currently quarantined it is not able to be quarantined.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>The first step is to have a valid item number selected that currently is not quarantined.</li>
                                                            <li>
                                                                Next is to press the <strong>Quarantine/Un-Quarantine Button</strong>. Due to the currently shown item not begin quarantined the
                                                                button displays the text <strong>Quarantine Item</strong> (as shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/QuarantineShown.png" alt="Inventory Master Quarantine Button" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed, a pop up (shown below) will prompt to confirm that the item number is to be quarantined. Press submit in order
                                                                to continue quarantining the item number.
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/QuarantinePopUp.png" style="width: 50%" alt="Inventory Master Quarantine Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once the submit button is pressed, the item number is quarantined and a pinkish color bar is displayed across the top signifying so (see below).
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/QuarIdent.png" alt="Inventory Master Quarantine Identifier" /></li>
                                                                </ul>
                                                            </li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#panelUnQuarantine" data-parent="#QuarStuffAccordion">
                                                    <h3 class="panel-title">
                                                        Un-Quarantine Item Number
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelUnQuarantine">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to Un-Quarantine an Item Number
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>The first step is to select a valid item number that is quarantined.</li>
                                                            <li>The next step is to press on the <strong>Quarantine/Un-Quarantine Button</strong>. Since the selected item number is 
                                                            quarantined, the button will show the text <strong>Un-Quarantine Item</strong> (as shown below).
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/UnQuarantineShown.png" alt="Inventory Master UnQuarantine Button" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once pressed a pop up (shown below) will appear to confirm that item number is to be un-quarantined. Press submit 
                                                            in order to continue un-quarantining the item number. On the same pop up there is the option to <strong>Append Reprocess to Open Transaction</strong>.
                                                            If checked and then submitted any reprocess transactions with this item number are moved to open transactions.
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/UnQuarantinePopUp.png" style="width: 50%" alt="Inventory Master UnQuarantine Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the submit button is pressed, the item number is un-quarantined, and the pinkish bar at the top of the page is removed as the item 
                                                            number is no long quarantined.</li>
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
                    <a data-toggle="collapse" data-parent="#InventoryMasterAccordion" data-target="#PrintingReports">
                        <h3 class="panel-title">
                            Printing Reports
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="PrintingReports">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses printing the <strong>Category</strong> and <strong>Kit</strong> reports.
                                </div>
                            </div> 
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="PrintAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#PrintAccordion" data-target="#Print1">
                                                    <h3 class="panel-title">
                                                        Printing the Category Report
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="Print1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses printing the <strong>Category Report</strong>.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Click on the <strong>Category</strong> or <strong>Sub Category</strong> box within the <strong>Details Tab</strong>.
                                                                A pop up will now appear (shown below) that contains the print button.
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/CategoryAndSubCategoryModal.png" style="width: 50%" alt="Inventory Master Categories Modal" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Click on the print button (shown below) on the modal.
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the print button is pressed, the report is printed on the assigned printer.</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#Print2" data-parent="#PrintAccordion">
                                                    <h3 class="panel-title">
                                                        Printing the Kit Report
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="Print2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to print the kit report.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Make sure that selected item number is a kit. This can be checked if the <strong>Kit Tag</strong> (shown below) is displayed
                                                                <ul>
                                                                    <li><img src="/images/Help/InventoryMaster/KitTag.png" style="width: 50%" alt="Inventory Master Kit Tag" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Click the print button (shown below), found within the <strong>Kit Items tab</strong>
                                                                <ul>
                                                                    <li><button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the print button is pressed, the report is then printed on the assigned printer.</li>
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
                    <a data-toggle="collapse" data-parent="#InventoryMasterAccordion" data-target="#Filtering">
                        <h3 class="panel-title">
                            Filtering and Cycling through Items
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="Filtering">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-group" id="FilterInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#FilterInfo" data-target="#FilterInfo_1">
                                            <h3 class="panel-title">
                                                Filtering on Fields
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="FilterInfo_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Inventory Master Page opened</li>
                                                    <li>Right click with the mouse on the desired field to filter on</li>
                                                    <li>Once right clicked a menu will appear with filter options. Select the desired filter option</li>
                                                    <li>Once selected the filter is applied the next available item number that satisfies the filter gets selected, 
                                                        the count data in the bottom left gets refreshed showing how many items satisfy the filter, 
                                                        and the arrows (shown below) in the bottom left get enabled or disabled to show if you are at the beginning or the end of the items
                                                        <ul>
                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-arrow-right"></span></button></li>
                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-arrow-left"></span></button></li>
                                                        </ul>
                                                    </li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#FilterInfo" data-target="#FilterInfo_2">
                                            <h3 class="panel-title">
                                                Cycling through Items
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="FilterInfo_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Inventory Master page opened</li>
                                                    <li>Click on one of the arrow buttons (shown below). The top one is to go to the next item number, 
                                                        the one below is to go to the previous item number
                                                        <ul>
                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-arrow-right"></span></button></li>
                                                            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-arrow-left"></span></button></li>
                                                        </ul>
                                                    </li>
                                                    <li>When pressed either the next or previous item number is selected. 
                                                        If an arrow is disabled this means that you reached the first or last item number
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
</div>
<map name="invmastmap">
    <area shape="rect" coords="3, 33, 882, 109" data-target="#panelInvMast_1" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />
    <area shape="rect" coords="5, 121, 56, 146" data-target="#DetailsTab" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />
    <area shape="rect" coords="63, 120, 123, 148" data-target="#ItemSetupTab" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />
    <area shape="rect" coords="128, 120, 176, 148" data-target="#KitItemsTab" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />

    <area shape="rect" coords="183, 120, 240, 148" data-target="#LocationsTab" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />
    <area shape="rect" coords="242, 120, 319, 148" data-target="#ReelTrackingTab" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />
    <area shape="rect" coords="320, 120, 391, 148" data-target="#ScanCodesTab" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />
    <area shape="rect" coords="391, 120, 464, 148" data-target="#WeighScaleTab" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />

    <area shape="rect" coords="464, 120, 507, 148" data-target="#OtherTab" data-toggle="collapse" data-parent="#InvMastOverviewAccordion" />
</map>
