<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "System Replenishment"
    ViewData("PageName") = "&nbsp; | &nbsp; System Replenishment"
End Code

<div class="container-fluid">
    <ul class="nav nav-tabs" role="tablist">
        <li class="active"><a href="#newOrders" role="tab" data-toggle="tab">New Orders</a></li>
        <li><a id="currenOrderTab" href="#currentOrder" role="tab" data-toggle="tab">Current Orders</a></li>
    </ul>
    <div class="tab-content" style="margin-top:5px;">
        <div class="tab-pane active" id="newOrders">
            @Html.Partial("NewOrders")
        </div>
        <div class="tab-pane" id="currentOrder">
            @Html.Partial("CurrentOrders", Model)
        </div>
    </div>
</div>
<script src="~/Scripts/toggles.min.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/SystemReplenishment/NewReplenishments.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/SystemReplenishment/CurrentReplenishments.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/SystemReplenishment/SystReplenFilters.js"></script>