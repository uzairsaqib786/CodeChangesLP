<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        The <strong>Process Put Aways</strong> tab allows users to assign transactions to specific totes and then a batch of their creation.  The batches are created in the <strong>Tote Setup</strong> tab, but totes are assigned transactions in this tab.
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        The <strong>Process Put Aways</strong> tab is made up of three major sections:
        <ol>
            <li><a data-target="#BatchPanel" data-toggle="collapse" data-parent="#PPAPanels">Choose Batch</a></li>
            <li><a data-target="#AssignPanel" data-toggle="collapse" data-parent="#PPAPanels">Assign Transaction To Batch/Tote</a></li>
            <li><a data-target="#TotePanel" data-toggle="collapse" data-parent="#PPAPanels">Totes</a></li>
        </ol>
    </div>
</div>
<div class="row">

    <div class="row" style="padding-top:5px;">
        <div class="col-md-6">
            <img src="~/Areas/Induction/Images/HelpProcPutBatches/processputssections1.PNG" style="width: 70%" alt="Process Put Aways - Panels" data-target="#BatchPanel" data-toggle="collapse" data-parent="#PPAPanels" />
        </div>
        <div class="col-md-6">
            <img src="~/Areas/Induction/Images/HelpProcPutBatches/processputssections2.PNG" style="width: 70%" alt="Process Put Aways - Panels" data-target="#AssignPanel" data-toggle="collapse" data-parent="#PPAPanels" />
        </div>
    </div>
    <div class="row" style="padding-top:5px;">
        <div class="col-md-12">
            <img src="~/Areas/Induction/Images/HelpProcPutBatches/processputssections3.PNG" style="width: 100%" alt="Process Put Aways - Panels" data-target="#TotePanel" data-toggle="collapse" data-parent="#PPAPanels" />
        </div>
    </div>

        <!--<div class="col-md-12">-->
        <!--<img src="~/Areas/Induction/Images/HelpProcPutBatches/processputssections.PNG" alt="Process Put Aways - Panels" usemap="#processputmap" />-->        
        <!--</div>-->
    </div>
<div class="panel-group" id="PPAPanels">
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-target="#BatchPanel" data-parent="#PPAPanels">
                <h3 class="panel-title">
                    1 | Choose Batch <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="BatchPanel">
            @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Batch/Panels/ChooseBatch.vbhtml")
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-target="#AssignPanel" data-parent="#PPAPanels">
                <h3 class="panel-title">
                    2 | Assign Transaction To Batch/Tote <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="AssignPanel">
            @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Batch/Panels/AssignTransaction.vbhtml")
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-target="#TotePanel" data-parent="#PPAPanels">
                <h3 class="panel-title">
                    3 | Totes <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="TotePanel">
            @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Batch/Panels/Totes.vbhtml")
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-target="#Reels" data-parent="#PPAPanels">
                <h3 class="panel-title">
                    4 | Reels <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="Reels">
            @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Batch/TransactionModal/Reels.vbhtml")
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-target="#NonReels" data-parent="#PPAPanels">
                <h3 class="panel-title">
                    5 | Non-Reels <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="NonReels">
            @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Batch/TransactionModal/Non-Reels.vbhtml")
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-target="#Transactions" data-parent="#PPAPanels">
                <h3 class="panel-title">
                    6 | Transactions Modal <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-body collapse accordion-toggle" id="Transactions">
            @Html.partial("~/Areas/Induction/Views/IMHelp/ProcessPutAways/Batch/TransactionModal/Transaction.vbhtml")
        </div>
    </div>
</div>
<map name="processputmap">
    <area shape="rect" coords="0, 36, 315, 156" data-target="#BatchPanel" data-toggle="collapse" data-parent="#PPAPanels" />
    <area shape="rect" coords="0, 170, 315, 480" data-target="#AssignPanel" data-toggle="collapse" data-parent="#PPAPanels" />
    <area shape="rect" coords="332, 34, 1314, 204" data-target="#TotePanel" data-toggle="collapse" data-parent="#PPAPanels" />
</map>