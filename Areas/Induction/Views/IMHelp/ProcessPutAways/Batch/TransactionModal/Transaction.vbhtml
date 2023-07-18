<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.AliasModel
<div class="row">
    <div class="col-md-12">
        This is the panel describing the <strong>Transactions</strong> modal where <strong>Put Away</strong> transactions may be added to a tote and subsequently a batch.
    </div>
</div>

<div class="panel-group" id="TPanel">
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#TPanel" data-target="#StartPanel"><h3 class="panel-title">1 | Transaction List <span class="accordion-caret-down"></span></h3></a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="StartPanel">
            <div class="row top-spacer">
                <div class="col-md-12">
                    <img src="~/Areas/Induction/Images/HelpProcPutBatches/transactionlist.png" style="width:95%" alt="Transaction Modal - Transaction List" />
                    <ul>
                        <li>
                            The Transaction list will appear only if there are transactions for the input value entered.  There are three main cases:
                            <ol>
                                <li>The number of transactions is 0.  You will not see this area and will instead be prompted to create a new Put Away transaction.</li>
                                <li>The number of transactions is 1.  You may not see this area, depending on your site's preferences.  You may be directed to the Put Away area for the single transaction automatically.</li>
                                <li>The number of transactions is greater than 1.  You will see this area and will be required to select a transaction to assign to a tote/batch.</li>
                            </ol>
                        </li>
                        <li>The area labeled <strong>1</strong> in the image above shows details about the individual inventory item entry and current batch.</li>
                        <li>
                            The area labeled <strong>2</strong> in the image above shows individual transactions that can be selected for processing (putting into a tote and including in the current batch) by clicking <button class="btn btn-primary">Select</button>
                            <ul>
                                <li>The inputs in this area are filterable.  You can right click the input you wish to filter and select an option to use to compare the value to.  See <a href="../Help?initialPage=stpaging">Multi-Line Paging</a> in the main Help document for details about how this feature works.</li>
                            </ul>
                        </li>
                        <li>The area labeled <strong>3</strong> in the image above shows methods for paging the transactions available currently, or clearing filters applied to the transactions or advancing to the <strong>Put Away</strong> area in order to create a new transaction.</li>
                    </ul>
                </div>
            </div>
            <div class="row top-spacer">
                <div class="col-md-12">
                    To select a transaction:
                    <ol>
                        <li>Optionally filter the results to find the transaction you want to use by right clicking on an input to filter and choosing one of the search options and then adding a value if applicable.</li>
                        <li>Click the <button class="btn btn-primary">Select</button> button next to the desired transaction.</li>
                    </ol>
                    To create a new transaction to use instead click the <button class="btn btn-primary">Create a New Put Away</button> button.
                    <ul>
                        <li>If the item you have selected is a reel item then you will be redirected to the <strong>New Reel Transactions</strong> modal.</li>
                        <li>If the item you have selected is not a reel item then you will be redirected to the <strong>Put Away</strong> area.</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#TPanel" data-target="#PutPanel"><h3 class="panel-title">2 | Put Away Detail <span class="accordion-caret-down"></span></h3></a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="PutPanel">
            <div class="row">
                <div class="col-md-12">
                    The <strong>Put Away</strong> Detail panel explains the process of editing a put away transaction for use with a batch/tote.
                </div>
            </div>
            <div class="row top-spacer">
                <div class="col-md-12">
                    <img src="~/Areas/Induction/Images/HelpProcPutBatches/putaways.png" style="width:95%" alt="Put Away Detail" /><br />
                    The image above is the <strong>Put Away Detail</strong> area.  You can change details about the selected transaction here.  The sections of the page are:
                    <ol>
                        <li>
                            Transaction Information - details about the transaction you are creating or selected in the previous area.
                            You may edit the following fields:
                            <ul>
                                <li>User Field 1</li>
                                <li>User Field 2</li>
                                <li>Lot Number</li>
                                <li>Expiration Date</li>
                                <li>Serial Number</li>
                                <li>Warehouse (if a transaction is already location assigned this may be unavailable.)</li>
                                <li>Return to Stock</li>
                            </ul>
                            Sub Category belongs to the Item Number selected.
                            All other fields are not editable at this stage.  A new Put Away transaction will be automatically assigned any fields it needs other than those provided.
                        </li>
                        <li>
                            Item Information - details about the item that you are assigning to the Put Away.  Editable fields include:
                            <ul>
                                <li>Carousel, Bulk and Flow Rack Cell Sizes</li>
                                <li>Carousel, Bulk and Flow Rack Velocity Codes</li>
                            </ul>
                            These fields may be edited for this single transaction or for the item in the <strong>Inventory Master</strong>.  Click the <button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button> button
                            in order to save the update to the <strong>Inventory Master</strong>.  Other item details can be edited by clicking the <button class="btn btn-primary"><span class="glyphicon glyphicon-share"></span></button> button,
                            which will open the <strong>Inventory Master</strong> with the item preselected.
                        </li>
                        <li>
                            Location Information - where the item will be put away to.
                            <ul>
                                <li>You may choose to edit the location by clicking the <button class="btn btn-primary">Find Location</button> button to automatically assign the "ideal" location for this item.</li>
                                <li>You may also click the <button class="btn btn-primary">Choose Location</button> button if you want to assign a specific location.</li>
                                <li>Clicking the <button class="btn btn-primary">Full Shelf</button> button will allow you to mark the current location's shelf as full.</li>
                                <li>Clicking <button class="btn btn-primary">Choose a Different Transaction</button> will return you to the <strong>Select Transactions</strong> area.</li>
                                <li>Click the <button class="btn btn-primary">Complete Transaction</button> button will allow you to assign the transaction to the selected tote.  If a Cross Dock opporunity exists you will be prompted when clicking this button.</li>
                                <li>Clicking the <button class="btn btn-default">Cancel</button> button will exit the transaction assignment modal.</li>
                                <li>Clicking the <button class="btn btn-primary">Create a New Put Away</button> button will allow you to refresh this screen with empty data in order to create a new put away transaction to assign to the tote/batch.</li>
                            </ul>
                        </li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-target="#CrossDock" data-parent="#TPanel">
                <h3 class="panel-title">
                    3 | Cross-Docking <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="CrossDock">
            @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Batch/CrossDockModal/CrossDock.vbhtml")
        </div>
    </div>
</div>