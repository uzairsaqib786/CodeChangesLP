<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        The <strong>Choose Batch</strong> panel allows for three main functionalities:
        <ol>
            <li>Choosing a batch</li>
            <li>Deleting/clearing/deallocating a batch</li>
            <li>Completing a batch</li>
        </ol>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        Choose a batch by typing the existing batch ID in the <strong>Batch ID</strong> textbox.  Batches that were processed on your workstation are the only batches available to you here.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        In order to delete, deallocate or clear a batch:
        <ol>
            <li>Click the <button type="button" class="btn btn-danger">Delete Batch</button> button.</li>
            <li>
                In the <strong>Batch - Delete and DeAllocate</strong> modal choose the appropriate options to execute the action desired.
                <ul>
                    <li>If you have selected a tote in the <strong>Totes</strong> panel, by clicking it then you will have the option to clear that tote as well.</li>
                    <li>
                        The four radio buttons on the right of the modal are the options that can be set. Clear Batch/Tote indicate a single option, as do DeAllocate & Clear and Clear Only.  
                        <ul>
                            <li><strong>Clear Batch</strong> - Clears the batch properties from transactions fitting workstation and preference criteria.  May also delete records from Induction Tracking if the preference is set to keep track of induction transactions.</li>
                            <li><strong>Clear Tote</strong> - Clears the tote properties from transactions fitting workstation and preference criteria.  May also delete records from Induction tracking.</li>
                            <li><strong>DeAllocate & Clear</strong> - Clears the transactions as described for the previous two options.  Additionally clears location data for the transactions.</li>
                            <li><strong>Clear Only</strong> - Clears the transactions in the manner described by the first two options.  Does not clear location data for the transactions.</li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li>Click the <button type="button" class="btn btn-danger">Clear/DeAllocate</button> button.</li>
            <li>Click OK to confirm the action, or cancel to terminate the operation.</li>
        </ol>
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        You may complete a batch by clicking the <button type="button" class="btn btn-success">Complete Batch</button> button.
    </div>
</div>