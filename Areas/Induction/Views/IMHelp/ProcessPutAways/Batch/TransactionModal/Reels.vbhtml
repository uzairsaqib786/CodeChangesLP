<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        Items with a <strong>Sub Category</strong> of <strong>Reel Tracking</strong> in <strong>Inventory</strong> are addressed by this panel.  These items must be serialized in a reel transaction before they can be added to a tote.  The only way to add these items to a tote is by scanning in their <strong>Serial Number</strong> in the <strong>Input Value</strong> textbox.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-12">
                In order to add reel transactions that have not been serialized:
                <ol>
                    <li>Enter their unique item identifier into the <strong>Input Value</strong> textbox and choose the appropriate <strong>Input Type</strong> if necessary.</li>
                    <li>Click the <button class="btn btn-primary">Assign Transaction to Tote</button> button.</li>
                </ol>
                The <strong>New Reel Transaction</strong> modal will be launched, as well as the <strong>Reel Detail</strong> modal.
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <img src="~/Areas/Induction/Images/HelpProcPutBatches/reeldetail.png" style="width:95%" alt="Reel Detail Modal" /><br />
                The above image is the <strong>Reel Detail</strong> modal.  It contains the basic details behind the transaction that will be created through this process.  Fill out the fields.  These fields
                 will be the default values of each individual reel transaction that is created by this process.  The number of parts to be inducted is the TOTAL number of parts to be inducted at the current time.  After submitting 
                this modal for the first time it will refer to an individual serialized reel's assigned number of parts.  Each of these fields will be editable per serialized reel after this modal is used for the first time.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                To complete the <strong>Reel Detail</strong> modal dialog you must click the <button class="btn btn-primary">Submit</button> button.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                Once the <strong>Reel Detail</strong> modal dialog has been completed you may complete the <strong>Reel Transactions</strong> modal fields.  The <strong>Reel Transactions</strong> modal is in the image below:
                <img src="~/Areas/Induction/Images/HelpProcPutBatches/reeltransactions.png" style="width:95%" alt="Reel Transactions Modal" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                To create the serialized reel transaction from the <strong>Reel Transactions</strong> modal:
                <ol>
                    <li>Provide the number of reels to be inducted by filling out the <strong>Number of Reels to be Inducted</strong> field.</li>
                    <li>Click the <button class="btn btn-primary">Auto Generate Reels & Serial Numbers</button> button.</li>
                    <li>
                        Change the <strong>Reel Serial Number</strong> and <strong>Reel Part Quantity</strong> fields as desired.  Notes:
                        <ul>
                            <li>The <strong>Total Parts to be Inducted</strong> field will restrict the total number of parts that can be serialized.  It can be changed if necessary.  Any remaining quantity will be made known to you before you are allowed to continue with the process.</li>
                            <li>The <strong>Number of Parts Not Assigned</strong> field indicates how many parts are not currently assigned to a reel.  The <strong>Auto Generate Reels & Serial Numbers</strong> button evenly distributes the quantity provided over the number of reels.  There may be leftover quantity that isn't automatically assigned to a reel.</li>
                            <li>You may print a reel label by clicking the <button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button> button next to the reel you wish to print.</li>
                            <li>You may edit the transaction details of a reel by clicking the <button class="btn btn-primary"><span class="glyphicon glyphicon-pencil"></span></button> button.</li>
                            <li>You may assign the next serial number from Preferences by clicking the <button class="btn btn-primary">Next Serial Number</button> button.</li>
                        </ul>
                    </li>
                    <li>Take note of the serial numbers that you wish to induct and/or print reel labels for the reels you have created.  You will need these numbers/labels to scan into the system in order to add these newly created transactions to a tote/batch.</li>
                    <li>Click the <button class="btn btn-primary">Submit</button> button.</li>
                    <li>Confirm your desire to continue the process by clicking OK or cancel the action by clicking Cancel.</li>
                    <li>Click OK to print a reel label for each of the transactions you just created or cancel to skip printing the labels.</li>
                </ol>
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                Once you have created reel transactions (or have a serial number for the reel transaction that you wish to include in a batch/tote):
                <ol>
                    <li>Enter the Serial Number into the <strong>Input Value</strong> textbox and ensure that the <strong>Input Type</strong> field is set to "Any" or "Serial Number" so that the transaction can be found.</li>
                    <li>Click the <button class="btn btn-primary">Assign Transaction to Tote</button> button.</li>
                    <li>The main <strong>Transaction</strong> modal will launch.</li>
                </ol>
            </div>
        </div>
    </div>
</div>