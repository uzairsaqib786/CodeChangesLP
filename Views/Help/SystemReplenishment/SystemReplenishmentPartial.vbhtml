<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
Layout = nothing
End Code
@modeltype PickPro_web.AliasModel
<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="SysReplenAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenAccordion" data-target="#SysReplenOverview">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SysReplenOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This is the <strong>System Replenishments</strong> page.  It allows the user to restock forward locations from back locations by moving varied amounts of stock of selected items
                            from calculated ideal back locations to calculated ideal forward locations.  If there is a lack of quantity in back locations the transaction will be put into the Reprocess Queue
                            to be dealt with when there is an available quantity of the item.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenAccordion" data-target="#SysReplenNew">
                        <h3 class="panel-title">
                            New System Replenishments
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SysReplenNew">
                    @Html.Partial("~/Views/Help/SystemReplenishment/SysReplenNew.vbhtml", model)
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#SysReplenAccordion" data-target="#SysReplenCurrent">
                        <h3 class="panel-title">
                            Current System Replenishments
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SysReplenCurrent">
                    @Html.Partial("~/Views/Help/SystemReplenishment/SysReplenCurrent.vbhtml", model)
                </div>
            </div>
        </div>
    </div>
</div>