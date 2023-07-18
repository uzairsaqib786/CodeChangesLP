<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    Layout = Nothing
End Code

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info" style="margin-bottom:5px;">
            <div class="panel-heading">
                <a data-toggle="collapse" data-target="#BMO_Overview">
                    <h3 class="panel-title">
                        Overview <span class="accordion-caret-down"></span>
                    </h3>
                </a>
            </div>
            <div class="panel-body collapse accordion-toggle" id="BMO_Overview">
                <div class="row">
                    <div class="col-md-12">
                        The Batch and Super Batch Managers provide a way to combine orders and individual transactions into single combined batch orders.
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-group" id="BMOAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#BMOAccordion" data-target="#BM">
                        <h3 class="panel-title">
                            Batch Manager <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="BM">
                    @Html.Partial("~/Views/Help/BatchManager/BMPartial.vbhtml")
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#BMOAccordion" data-target="#SBM">
                        <h3 class="panel-title">
                            Super Batch Manager <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SBM">
                    @Html.Partial("~/Views/Help/BatchManager/SBMPartial.vbhtml")
                </div>
            </div>
        </div>
    </div>
</div>