<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
            <h3 class="panel-title">
                Global Configuration
            </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                The Global Config page is made up of global settings for Pick Pro.  It includes the ability to set the Global User account and displays a list of connected users.
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                It also includes the ability to set up and change connections to particular production databases and networked printers.
            </div>
        </div>
        <div class="panel-group" id="GCParent">
            @Html.Partial("~/Views/Help/GlobalConfig/GCHome.vbhtml")
            @Html.Partial("~/Views/Help/GlobalConfig/GCConnectionSetup.vbhtml")
            @Html.Partial("~/Views/Help/GlobalConfig/GCPrinterSetup.vbhtml")
            @Html.Partial("~/Views/Help/GlobalConfig/GCLicensing.vbhtml")
            @Html.Partial("~/Views/Help/GlobalConfig/GCWSApps.vbhtml")
        </div>
    </div>
</div>