<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <img src="~/images/Help/CycleCount/cyclecount.png" alt="Cycle Count Discrepancies" style="width: 100%"/>
    </div>
    <div class="col-md-12">
        This is the Cycle Count Discrepancies page.  It allows you to configure PickPro to find discrepancies between the host system and itself as they relate to item counts.  
        If you wish to create count batches to execute a cycle count instead, or if you do not have audit files from your host system then you may click the <button class="btn btn-primary">Create Count Batches</button> button. 
        An audit file from the host is required for this page.  Click the <button class="btn btn-primary"><span class="glyphicon glyphicon-cog"></span></button> button to configure the file import.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        <div class="panel-group" id="CCAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#CCFields" data-parent="#CCAccordion">
                        <h3 class="panel-title">
                            Audit File Field Mapping <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="CCFields">
                    <div class="row">
                        <div class="col-md-12">
                            Mapping the audit files fields is required to use this page.  PickPro needs to know how to read the audit files in order to find discrepancies between the host system and itself.
                        </div>
                        <div class="col-md-12">
                            <img src="~/images/Help/CycleCount/configure.png" alt="Configuration for host file import" style="width: 65%"/>
                        </div>
                        <div class="col-md-12">
                            The image above is the modal that will appear after clicking the settings button (<button class="btn btn-primary"><span class="glyphicon glyphicon-cog"></span></button>).  The expected format is configured in this modal.
                            Fields:
                            <ul>
                                <li>Backup File Path - location of where backups of the audit files are kept.</li>
                                <li>Import File Path - location of the new audit files to use.  This is a folder, not a file.</li>
                                <li>Import File Extension - file extension of the import files (txt, csv, tsv, etc.)</li>
                                <li>Active - whether the current path is the up-to-date location of the audit files.</li>
                            </ul>
                            The following PickPro fields are required.
                            <ul>
                                <li>Item Number</li>
                                <li>Host Quantity</li>
                                <li>Warehouse</li>
                                <li>Serial Number</li>
                                <li>Lot Number</li>
                                <li>Expiration Date</li>
                            </ul>
                            The fields for each PickPro field are:
                            <ul>
                                <li>Start Position - first character position for the particular PickPro field.</li>
                                <li>Field Length - how many characters are expected for the particular PickPro field.</li>
                                <li>End Position - calculated last character position based on Start Position and Field Length.</li>
                                <li>Pad Field from Left</li>
                                <li>Field Type - the type of data that is contained in this particular PickPro field.</li>
                                <li>Import Format</li>
                            </ul>
                            Once all fields have been completed you may click the <button class="btn btn-primary">Save</button> button.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#CCAccordion" data-target="#Discrepancies">
                        <h3 class="panel-title">
                            Import Discrepancies <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="Discrepancies">
                    <div class="row">
                        <div class="col-md-12">
                            Click the <button class="btn btn-primary">Import Discrepancies</button> button to use the configuration set up in the previous panel to import the audit files and their item counts.  
                            Discrepancies can be selected after import by clicking on them.  The left table is for unselected items, while the right table is for selected items.
                        </div>
                        <div class="col-md-12">
                            <ul>
                                <li>The <button class="btn btn-primary">Append All</button> button can be used to append all the unselected items to the selected list.</li>
                                <li>The <button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button> button is used to print a discrepancy report.</li>
                                <li>The <button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button> buttons can be used to delete an item from the discrepancy list.</li>
                                <li>The <button class="btn btn-primary">Create Transactions</button> button will create count transactions for each of the items in the right hand list so that the counts can be verified and the discrepancies resolved.</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>