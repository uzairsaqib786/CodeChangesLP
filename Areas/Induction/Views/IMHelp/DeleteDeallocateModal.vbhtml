<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="DeleteDeAlloModalAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#DeleteDeAlloModalAccordion" data-target="#DeleteDeAlloModalOverview">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="DeleteDeAlloModalOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This is the <strong>Delete and DeAllocate</strong> modal. It is used for deleting and/or deallocating batches/tote ids
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/DeleteDeAlloModalNew.png" style="width: 80%" alt="Delete DeAllocate Modal" />
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="DeleteDeAlloModalInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#DeleteDeAlloModalInfo" data-target="#DeleteDeAlloModalInfo_1">
                                            <h3 class="panel-title">
                                                Information
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="DeleteDeAlloModalInfo_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Information:</strong>
                                                <ul>
                                                    <li><strong>Batch ID:</strong> The batch id of the currently selected record</li>
                                                    <li><strong>Tote ID:</strong> The tote id of the currently selected record</li>
                                                    <li><strong>Transaction Type:</strong> The transaction type of the currently selected record</li>
                                                    <li><strong>Currently Selected Command:</strong> Tells the operation that is going to be performed</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                                <ol>
                                                    <li><strong>Clear Batch/ Clear Tote:</strong> Radio buttons to tell if the batch or tote id is going to be operated on
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearBatchToteButt.png" alt="Clear Batch or Tote Buttons" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>DeAllocate & Clear/ Clear Only:</strong> Radio buttons telling which delete operation will be performed
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/DeleteOptionButt.png" alt="Delete Option Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Delete All Batches:</strong> Will delete all existing batches
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/DelAllBatchButt.png" alt="Delete All Batches Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Clear/DeAllocate:</strong> Will perform the desired command on the given batch or tote id
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearDeAlloButt.png" alt="Clear/Deallocate Button" /></li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#DeleteDeAlloModalAccordion" data-target="#DeleteDeAlloModalFunction">
                        <h3 class="panel-title">
                            Functionality
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="DeleteDeAlloModalFunction">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses the functionality within the modal
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="DeleteDeAlloModalFunctionInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#DeleteDeAlloModalFunctionInfo" data-target="#DeleteDeAlloModalFunctionInfo_1">
                                            <h3 class="panel-title">
                                                Opening the Modal
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="DeleteDeAlloModalFunctionInfo_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps from Tote Transaction Manager:</strong>
                                                <ol>
                                                    <li>Open the Tote Transaction Manager page</li>
                                                    <li>Within the Tote Transactions Table click on the record to be cleared and/or deallocated</li>
                                                    <li>Once clicked, click on the <strong>Clear Info</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearInfoButt.png" alt="Clear Info Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once clicked a pop up (shown below) appears, confirming to delete. Click <strong>OK</strong> to continue
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearInfoPopUpNew.png" style="width:45%" alt="Clear Info Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once clicked the modal will appear with the information of the clicked record</li>
                                                </ol>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps from the Process Put Aways page:</strong>
                                                <ol>
                                                    <li>Open the Process Put Aways page</li>
                                                    <li>Have the Process Put Aways tab section displayed</li>
                                                    <li>Use the Batch ID Typeahead to select the desired batch or the batch that contains the desired tote id</li>
                                                    <li>Once displayed, click on the desired tote id if a tote id is going to be deleted</li>
                                                    <li>Press the <strong>Delete Batch</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/DelBatchButt.png" alt="Delete Batch Button" /></li>
                                                        </ul>
                                                    </li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#DeleteDeAlloModalFunctionInfo" data-target="#DeleteDeAlloModalFunctionInfo_2">
                                            <h3 class="panel-title">
                                                Delete All Batches 
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="DeleteDeAlloModalFunctionInfo_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Open the Process Put Aways page</li>
                                                    <li>Have the Process Put Aways tab section displayed</li>
                                                    <li>Press the <strong>Delete Batch</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/DelBatchButt.png" alt="Delete Batch Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the modal opens up. Within the modal press the <strong>Delete All Batches</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/DelAllBatchButt.png" alt="Delete All Batches Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>When pressed a pop up (shown below) appears. To continue with the delete press <strong>OK</strong>
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/DelAllBatchPopUpNew.png" style="width:45%" alt="Delete All Batches pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>When confirmed all existing batches are deleted</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#DeleteDeAlloModalFunctionInfo" data-target="#DeleteDeAlloModalFunctionInfo_3">
                                            <h3 class="panel-title">
                                                Clear and/or DeAllocate
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="DeleteDeAlloModalFunctionInfo_3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps from Tote Transaction Manager:</strong>
                                                <ol>
                                                    <li>Go to either the Tote Transaction Manager page</li>
                                                    <li>Find the desired record and click on it</li>
                                                    <li>Click on the <strong>Clear Info</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearInfoButt.png" alt="Clear Info Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once clicked a pop up (shown below) appears, confirming to delete. Press <strong>OK</strong> to continue
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearInfoPopUpNew.png" style="width:45%" alt="Clear Info Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the modal opens up. Configure the modal to the desired delete command</li>
                                                    <li>Once configured press the <strong>Clear/DeAllocate</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearDeAlloButt.png" alt="Clear/Deallocate Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed a pop up (shown below) appears, confirming the delete action. Press <strong>OK</strong> to continue
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearDeAlloPopUpNew.png" style="width:45%" alt="Clear/Deallocate Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once confirmed the delete command is executed</li>
                                                </ol>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps from Process Put Aways:</strong>
                                                <ol>
                                                    <li>Go to either the Process Put Aways page</li>
                                                    <li>Use the Batch ID typeahead to select the desired batch id that is going to be deleted or contains the tote id</li>
                                                    <li>
                                                        Click on the <strong>Delete Batch</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/DelBatchButt.png" alt="Delete Batch Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the modal opens up. Configure the modal to the desired delete command</li>
                                                    <li>
                                                        Once configured press the <strong>Clear/DeAllocate</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearDeAlloButt.png" alt="Clear/Deallocate Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        Once pressed a pop up (shown below) appears, confirming the delete action. Press <strong>OK</strong> to continue
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpDeleteDeAlloModal/ClearDeAlloPopUpNew.png" style="width:45%" alt="Clear/Deallocate Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once confirmed the delete command is executed</li>
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