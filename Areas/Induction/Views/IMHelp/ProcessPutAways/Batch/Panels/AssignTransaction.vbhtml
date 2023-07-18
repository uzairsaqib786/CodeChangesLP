<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        This panel describes how to assign a transaction to a batch.  It also provides two methods for selecting a tote to place a transaction in.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        Selecting a particular tote position can be accomplished by entering the position or the corresponding tote ID into the appropriate textbox at the bottom of the panel.  Then click the <button class="btn btn-primary">Select</button> button.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        Selecting the next tote with available cells in it can be accomplished by clicking the <button class="btn btn-primary">Go To Next</button> button.  Above the button the next available tote is listed as well as its position and the current capacity (or next cell).
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        Selecting a transaction to assign to a tote can be accomplished by following these steps:
        <ol>
            <li>
                Choose the type of input that you will provide.  These inputs may be manually entered or scanned.  A preference for Induction Manager will determine which of these scan types is the default when you load the page. Valid choices include:
                <ul>
                    <li>Any</li>
                    <li>Item Number</li>
                    <li>Serial Number</li>
                    <li>Lot Number</li>
                    <li>Host Transaction ID</li>
                    <li>Scan Code</li>
                    <li>Supplier Item ID</li>
                </ul>
            </li>
            <li>
                Scan or enter the selected input type.  The input type "Any" can accept any type of input amongst the rest of the choices and will choose the "best" one.  The first match will be chosen in this order: 
                <ol>
                    <li>Serial Number</li>
                    <li>Item Number</li>
                    <li>Scan Code</li>
                    <li>Scan Code Range - Whole scan code is prioritized over a range.</li>
                    <li>Lot Number</li>
                    <li>Host Transaction ID</li>
                    <li>Supplier Item ID</li>
                    <li>New Item - Preference dependent.  If the preference for adding a new item is set you can be optionally redirected to the <strong>Inventory</strong> screen to add the new item.</li>
                    <li>Invalid Scan Code - Not a recognized input type.  There is nothing in the <strong>Open Transactions, Inventory or Scan Code</strong> areas that indicates the input is valid and the preference to add a new item is not turned on.</li>
                </ol>
                If you have issues with a misidentified item through the "Any" option you will want to consider specifying which type it is or altering preferences, item numbers or other fields in order to prevent the conflict from occurring.
            </li>
            <li>
                Enter or scan the value for the input selected into the <strong>Input Value</strong> textbox.
                <ul>
                    <li>Note: There are preferences that may cause this input to change.  The <strong>Strip Scan</strong> settings may cause the input to be truncated from either side and for a variable number of characters.  If you experience incorrect truncation check your preferences!</li>
                </ul>
            </li>
            <li>Click the <button class="btn btn-primary">Assign Transaction to Selected Tote</button> button.  You may need to skip this step if you scanned in your <strong>Input Value</strong> entry.  Using the enter key while the <strong>Input Value</strong> textbox has focus will also cause the modal to open.</li>
            <li>Advance to the <strong>Transaction Modal</strong> help dropdown for information on the next step in this process.</li>
        </ol>
    </div>
</div>