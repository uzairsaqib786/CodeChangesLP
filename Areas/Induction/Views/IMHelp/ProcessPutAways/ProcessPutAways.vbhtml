<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="ProcessPutAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ProcessPutAccordion" data-target="#ProcessPutOverview">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ProcessPutOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This is the <strong>Process Put Aways</strong> page. It allows the user to assign put away transactions to a batch and/or tote.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            This is the screen you will see if you already have batches created for your workstation.
                            <img src="~/Areas/Induction/Images/HelpProcPutBatches/processputs.png" style="width:100%" alt="Process Put Aways tab" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            This is the screen you will see if you do not have batches created for your workstation.  You will need to set up a batch before you can continue to the other tab.
                            <img src="~/Areas/Induction/Images/HelpProcPutBatches/totesetup.png" style="width:100%" alt="Tote Setup tab" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ProcessPutAccordion" data-target="#ProcessPutToteAccordion">
                        <h3 class="panel-title">
                            Tote Setup
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ProcessPutToteAccordion">
                    <div class="row">
                        <div class="col-md-12">
                            This panel refers to the <strong>Tote Setup</strong> tab on the Process Put Aways screen.
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-12">
                            The <strong>Tote Setup</strong> area is made up of three major sections:
                            <ol>
                                <li><a data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" data-target="#ProcessPutTote1Accordion">Batch Setup</a> - This area allows you to create and select batches to edit from your workstation's queue.</li>
                                <li><a data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" data-target="#ProcessPutTote2Accordion">Zone Setup</a> - This area allows you to set zones on a particular batch, as well as reset default cell quantities for totes assigned to the current batch.  Lastly, it allows you to process a batch so that it makes its way into Open Transactions.</li>
                                <li><a data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" data-target="#ProcessPutTote3Accordion">Tote Setup</a> - This area allows you to set the details of totes that you wish to assign to the current batch as well as print those details.</li>
                            </ol>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <img src="~/Areas/Induction/Images/HelpProcPutBatches/totes.png" usemap="#totesetupmap" alt="Process Put Aways - Tote Setup" />
                        </div>
                    </div>
                    <div id="ProcessPutToteInnerAccordion" class="panel-group">
                        @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Setup/BatchSetupPanel.vbhtml")
                        @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Setup/TotesPanel.vbhtml")
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ProcessPutAccordion" data-target="#ProcessPutBatchAccordion">
                        <h3 class="panel-title">
                            Process Put Aways
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ProcessPutBatchAccordion">
                    <div class="row">
                        <div class="col-md-12">
                            This panel refers to the <strong>Process Put Aways</strong> tab on the Process Put Aways screen.
                            @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Batch/Batch.vbhtml")
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<map name="totesetupmap">
    <area shape="rect" coords="0, 50, 370, 100" data-target="#ProcessPutTote1Accordion" data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" />
    <area shape="rect" coords="0, 110, 370, 185" data-target="#ProcessPutTote2Accordion" data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" />
    <area shape="rect" coords="395, 50, 720, 130" data-target="#ProcessPutTote3Accordion" data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" />
</map>