<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Integer
@Code
    Layout = nothing
End Code

<div class="panel-group" id="TransactionsAccordion">
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#TransactionsAccordion" data-target="#Transactions">
                <h4 class="panel-title">Transactions Overview <span class="accordion-caret-down"></span></h4>
            </a>
        </div>
        <div id="Transactions" class="panel-info collapse @(IIf(Model = 0, "in", "")) accordion-toggle">
            <div class="panel-body">
                @Html.Partial("~/Views/Help/Transactions/TransPartial.vbhtml")
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#TransactionsAccordion" data-target="#OS">
                <h4 class="panel-title">Order Status <span class="caret"></span></h4>
            </a>
        </div>
        <div id="OS" class="panel-info collapse @(IIf(Model = 1, "in", "")) accordion-toggle">
            <div class="panel-body">
                @Html.Partial("~/Views/Help/Transactions/OrderStatusPartial.vbhtml")
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#TransactionsAccordion" data-target="#OT">
                <h4 class="panel-title">Open Transactions <span class="caret"></span></h4>
            </a>
        </div>
        <div id="OT" class="panel-info collapse @(IIf(Model = 2, "in", "")) accordion-toggle">
            <div class="panel-body">
                @Html.Partial("~/Views/Help/Transactions/OpenTransactionsPartial.vbhtml")
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#TransactionsAccordion" data-target="#TH">
                <h4 class="panel-title">Transaction History <span class="caret"></span></h4>
            </a>
        </div>
        <div id="TH" class="panel-info collapse @(IIf(Model = 3, "in", "")) accordion-toggle">
            <div class="panel-body">
                @Html.Partial("~/Views/Help/Transactions/TransactionHistoryPartial.vbhtml")
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#TransactionsAccordion" data-target="#RP">
                <h4 class="panel-title">Reprocess Transactions <span class="caret"></span></h4>
            </a>
        </div>
        <div id="RP" class="panel-info collapse @(IIf(Model = 4, "in", "")) accordion-toggle">
            <div class="panel-body">
                @Html.Partial("~/Views/Help/Transactions/ReprocessPartial.vbhtml")
            </div>
        </div>
    </div>
</div>