<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="ToteTransManAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#ToteTransManOverview" data-parent="#ToteTransManAccordion">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ToteTransManOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This page is the <strong>Tote Transactions Manager</strong> page. It displays all inducted tote ids.
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <img src="/Areas/Induction/Images/HelpToteTransMan/ToteTransManPage.png" alt="Tote Transactions Manager Page" />
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="ToteTransManInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteTransManInfo" data-target="#ToteTransManInfo_1">
                                            <h3 class="panel-title">
                                                Information
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteTransManInfo_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Information:</strong>
                                                <ul>
                                                    <li><strong>Batch Pick ID:</strong> Typeahead in order to display transactions for the Batch</li>
                                                    <li><strong>Tote Transactions Table:</strong> Datatable containing all the inducted tote id transactions</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteTransManInfo" data-target="#ToteTransManInfo_2">
                                            <h3 class="panel-title">
                                                Buttons
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteTransManInfo_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                                <ul>
                                                    <li><strong>Clear Typeahead:</strong> Clears the typeahead value and displays all records in the table
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/ClearTAButt.png" alt="Tote Transactions Manager Clear Typeahead Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Clear Pick Tote Info:</strong> Clears the infomration for all pick transactions in the table
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/ClearPickButt.png" alt="Tote Transactions Manager Clear Pick Tote Info Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Clear Info:</strong> Opens the modal to clear and/or deallocate a batch or tote
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/ClearInfoButt.png" alt="Tote Transactions Manager Clear Info Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Print Off Carousel List:</strong> Prints the off carousel list report with data from the selected record
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/PrintOffCarButt.png" alt="Tote Transactions Manager Print Off Carousel Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Print Tote Contents:</strong> Prints the tote contents report with data from the selected record
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/PrintToteButt.png" alt="Tote Transactions Manager Print Tote Contents Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Print Tote Label:</strong> Print the tote label report with data from the selected record
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/PrintToteLabButt.png" alt="Tote Transactions Manager Print Tote Label Button" /></li>
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
                    <a data-toggle="collapse" data-parent="#ToteTransManAccordion" data-target="#ToteTransManFunctions">
                        <h3 class="panel-title">
                            Functionality
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ToteTransManFunctions">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses how to perform each function within  the <strong>Tote Transaction Manager</strong> page
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="ToteTransManFunctionsInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteTransManFunctionsInfo" data-target="#ToteTransManFunctionsInfo_1">
                                            <h3 class="panel-title">
                                                Using and Clearing Batch Pick ID Typeahead
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteTransManFunctionsInfo_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps to Use:</strong>
                                                <ol>
                                                    <li>Have the <strong>Tote Transactions Manager</strong> page opened</li>
                                                    <li>Within the text field begin typing the desired batch id</li>
                                                    <li>As characters are entered a dropdown menu will be shown with batch ids beginning with the inputted value. 
                                                        Select the batch id from this list when it is shown
                                                    </li>
                                                    <li>Once selected the text field will fill with the selected batch id 
                                                        and all records for that batch id will be shown in the table
                                                    </li>
                                                </ol>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps to Clear:</strong>
                                                <ol>
                                                    <li>Have the <strong>Tote Transactions Manager</strong> page opened</li>
                                                    <li>Have the Batch Pick ID typeahead populated</li>
                                                    <li>Press the <strong>Clear Typeahead</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/ClearTAButt.png" alt="Tote Transactions Manager Clear Typeahead Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the text field is cleared and the table is populated with all batch ids</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteTransManFunctionsInfo" data-target="#ToteTransManFunctionsInfo_2">
                                            <h3 class="panel-title">
                                                Clear Pick Tote Info
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteTransManFunctionsInfo_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the <strong>Tote Transaction Manager</strong> page opened</li>
                                                    <li>Press the <strong>Clear Pick Tote Info</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/ClearPickButt.png" alt="Tote Transactions Manager Clear Pick Tote Info Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed a pop up (shown below) appears. Press <strong>OK</strong> to continue the operation
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/ClearPickPopUp.png" alt="Tote Transactions Manager Clear Pick Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once confirmed all pick tote information is cleared and the table gets refilled with the new updated data</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteTransManFunctionsInfo" data-target="#ToteTransManFunctionsInfo_3">
                                            <h3 class="panel-title">
                                                Clear Info
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteTransManFunctionsInfo_3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the <strong>Tote Transactions Manager</strong> page open</li>
                                                    <li>Click on the desired record in the table (it will be highlighted blue)</li>
                                                    <li>Once highlighted press the <strong>Clear Info</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/ClearInfoButt.png" alt="Tote Transactions Manager Clear Info Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once clicked a pop up (shown below) appears. Press <strong>OK</strong> to continue with the operation
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/ClearInfoPopUp.png" alt="Tote Transactions Manager Clear info Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>When confirmed the <strong>Delete and DeAllocate</strong> modal opens for this record. Within here specify 
                                                        the details for the delete
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/DeleteModal.png" alt="Tote Transactions Manager Delete Modal" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once it is at the desired configuration press the <strong>Clear/DeAllocate</strong> button</li>
                                                    <li>When pressed a pop up (shown below) appears. To continue with the operation press <strong>OK</strong>
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpToteTransMan/DeleteModalPopUp.png" alt="Tote Transactions Manager Delete Modal Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once confirmed the desired delete operation is performed on the record</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteTransManFunctionsInfo" data-target="#ToteTransManFunctionsInfo_4">
                                            <h3 class="panel-title">
                                                Printing
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteTransManFunctionsInfo_4">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the <strong>Tote Transaction Manager</strong> page open</li>
                                                    <li>Click the desired record in the table (it will highlight blue)</li>
                                                    <li>Click one of the desired print buttons (shown below)
                                                        <ul>
                                                            <li>
                                                                <strong>Print Off Carousel List:</strong> Prints the off carousel list report with data from the selected record
                                                                <ul>
                                                                    <li><img src="/Areas/Induction/Images/HelpToteTransMan/PrintOffCarButt.png" alt="Tote Transactions Manager Print Off Carousel Button" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                <strong>Print Tote Contents:</strong> Prints the tote contents report with data from the selected record
                                                                <ul>
                                                                    <li><img src="/Areas/Induction/Images/HelpToteTransMan/PrintToteButt.png" alt="Tote Transactions Manager Print Tote Contents Button" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                <strong>Print Tote Label:</strong> Print the tote label report with data from the selected record
                                                                <ul>
                                                                    <li><img src="/Areas/Induction/Images/HelpToteTransMan/PrintToteLabButt.png" alt="Tote Transactions Manager Print Tote Label Button" /></li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                    </li>
                                                    <li>Once clicked the document will either be printed or a preview will open up. This depeends on 
                                                        the value of the setting Print Directly to Printer
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
