<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        Cross docking is a process which allows users to place put away transactions in "Cross Dock" where they can be moved into positions that will satisfy reprocess transactions.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-6">
        <label>Reels</label>
        <ul>
            <li>Cannot be split quantities.  The entire transaction must be allocated to a reprocess transaction, even if the reel quantity is greater than the needed reprocess quantity.</li>
            <li>May be assigned to a transaction which will remain short and in reprocess.</li>
            <li>Transactions are split by serial number.  Each serial number may only be assigned to a single reprocess transaction regardless of short or leftover quantity.</li>
        </ul>
    </div>
    <div class="col-md-6">
        <label>Non-Reels</label>
        <ul>
            <li>May be split between multiple reprocess transactions.</li>
        </ul>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        The image below is the <strong>Cross Dock</strong> modal.
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <img src="~/Areas/Induction/Images/HelpProcPutBatches/crossdock.png" alt="Cross Dock Modal" />
        <ol>
            <li>This area shows the item details and the quantity needed to satisfy all reprocess as well as the current available put away quantity.</li>
            <li>
                This area provides functions for assigning details to the transaction.
                <ul>
                    <li>Click the <button class="btn btn-primary">Next Tote</button> button to assign the next tote ID from System Preferences to the highlighted (blue) reprocess transaction.</li>
                    <li>Click the <button class="btn btn-primary">User Fields</button> button to assign the User Fields for the reprocess transaction.</li>
                    <li>Click the 
                        <div class="btn-group">
                            <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                            <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                <li>
                                    <a href="#" id="PrintTote" class="Print-Report">Print Tote Label</a>
                                    <a href="#" id="PrintItem" class="Print-Report">Print Item Label</a>
                                </li>
                            </ul>
                        </div> button to show the print options for this reprocess transaction.
                    </li>
                    <li>Click the 
                        <div class="btn-group">
                            <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-share"></span> <span class="caret"></span></button>
                            <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                <li>
                                    <a href="#" id="ViewOS">View Order Status</a>
                                    <a href="#" id="ViewRP">View Reprocess</a>
                                </li>
                            </ul>
                        </div> button to view the reprocess order in <strong>Order Status</strong> or the reprocess transaction detail.
                    </li>
                    <li>Click the <button class="btn btn-primary">Complete Pick</button> button to complete the reprocess transaction for the quantity you provided in the <strong>Completed Quantity</strong> field in section 3.</li>
                </ul>
            </li>
            <li>
                This area allows you to select a particular reprocess transaction to cross dock with.  Once you have selected a transaction (by clicking on its row):
                <ul>
                    <li>
                        You can assign a Tote ID to the transaction by: 
                        <ul>
                            <li>Clicking the <strong>Tote ID</strong> input and selecting a tote.</li>
                            <li>Clicking the <button class="btn btn-primary">Next Tote</button> button in section 2.</li>
                        </ul>
                    </li>
                    <li>
                        You can assign a completed quantity to the transaction.
                        <ul>
                            <li>If the item is a reel tracking item the completed quantity must be the entire transaction quantity.</li>
                            <li>If the item is not reel tracking the completed quantity can be any number of parts up to the total transaction quantity seen in the <strong>Qty Available</strong> field in section 1.</li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li>This area allows you to navigate the available records and send any remaining quantity for put away back to the transaction you were creating before and assign it to the tote you selected.  
            You will be prompted again for any cross dock opportunities if you have not exhausted the transaction quantity.  You may also select <button class="btn btn-primary">Close</button> if you have completed the cross dock transactions required.</li>
        </ol>
    </div>
</div>