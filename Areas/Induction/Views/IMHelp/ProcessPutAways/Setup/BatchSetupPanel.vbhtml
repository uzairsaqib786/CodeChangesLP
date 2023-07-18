<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <a data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" data-target="#ProcessPutTote1Accordion">
            <h3 class="panel-title">
                1 | Batch Setup
                <span class="accordion-caret-down"></span>
            </h3>
        </a>
    </div>
    <div class="panel-body collapse accordion-toggle" id="ProcessPutTote1Accordion">
        <div class="row">
            <div class="col-md-12">
                The Batch Setup section of the <strong>Tote Setup</strong> tab allows users to create a new batch or select an existing one from their own workstation's queue.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                In order to select an existing batch:
                <ol>
                    <li>Type the batch ID desired into the typeahead input box labeled "Batch ID".</li>
                    <li>Optional: Click the batch ID desired from the typeahead dropdown.</li>
                    <li>The <strong>Totes</strong> panel will populate with the totes belonging to the existing batch entered.  The <strong>Assigned Zones</strong> textbox will update with the zones that are assigned to the selected batch.</li>
                </ol>
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                In order to create a new batch:
                <ol>
                    <li>
                        Choose whether you wish to enter the batch ID yourself or have one generated for you.
                        <ul>
                            <li>Click the <button type="button" class="btn btn-primary">New Batch With ID</button> button to create a new batch with an auto-generated ID.</li>
                            <li>Enter the batch ID you want to use in the <strong>Batch ID</strong> textbox if you wish to use your own batch ID, then click the <button type="button" class="btn btn-primary">New Batch</button> button.</li>
                        </ul>
                    </li>
                    <li>Enter the <strong>Tote IDs</strong> desired in the <strong>Totes</strong> panel.</li>
                </ol>
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                To determine if the batch you have selected has already been processed: Check the <strong>Status</strong> textbox in the batch section.
            </div>
        </div>
    </div>
</div>
<div class="panel panel-info">
    <div class="panel-heading">
        <a data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" data-target="#ProcessPutTote2Accordion">
            <h3 class="panel-title">
                2 | Zone Setup
                <span class="accordion-caret-down"></span>
            </h3>
        </a>
    </div>
    <div class="panel-body collapse accordion-toggle" id="ProcessPutTote2Accordion">
        <div class="row">
            <div class="col-md-12">
                The Zone Setup area allows the user to specify which zones will be assigned to the current batch when it is processed.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                In order to assign zones to the current batch:
                <ol>
                    <li>Click the <button class="btn btn-primary">Select Zones</button> button.</li>
                    <li>Click or drag the sliders for the zones you wish to include to "Yes."  Some zones may be labeled <span class="label label-default">Assigned to Another Batch</span> which means they cannot be assigned to your currently selected batch.</li>
                    <li>Click the <button class="btn btn-primary">Submit</button> button.</li>
                </ol>
                <div class="row">
                    <div class="col-md-12">
                        <img src="~/Areas/Induction/Images/HelpProcPutBatches/select_zones.png" style="width:95%" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                In order to reset the number of cells on every tote in the <strong>Totes</strong> area: Click the <button class="btn btn-primary">Set to Default Cell Quantity</button> button.  The number assigned to each tote is a preference available <a href="/IM/Preferences">here.</a>
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                In order to process your current batch:
                <ol>
                    <li>Ensure that the totes in the <strong>Totes</strong> panel are correct and complete.</li>
                    <li>Ensure that any tote or location labels that you desire have been printed already.</li>
                    <li>Click the <button class="btn btn-success">Process Batch</button> button.</li>
                    <li>Fix any outstanding errors that you are alerted to.</li>
                    <li>Click OK to continue to the <strong>Process Put Aways</strong> tab, or Cancel to continue entering batches on the <strong>Tote Setup</strong> tab.</li>
                </ol>
            </div>
        </div>
    </div>
</div>