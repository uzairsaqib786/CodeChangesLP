<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row" style="margin-top:5px;">
    <div class="col-md-2">
        <ul class="nav nav-pills nav-stacked" style="padding-right:10px;border-right: 2px solid grey;" role="tablist">
            <li class="active"><a href="#WorkstationInfo" data-toggle="tab">Workstation Setup</a></li>
            <li><a href="#BulkZones" data-toggle="tab">Bulk Zones</a></li>
            <li><a href="#PODSetup" data-toggle="tab">POD Setup</a></li>
            <li><a href="#SortBar" data-toggle="tab">Sort Bar Setup</a></li>
            <li><a href="#PickLevels" data-toggle="tab">Pick Levels</a></li>
            <li><a href="#CustApps" data-toggle="tab">Custom Apps</a></li>
            <li><a href="#MiscSett" data-toggle="tab">Miscellaneous Settings</a></li>
        </ul>
    </div>
    <div class="col-md-10">
        <div class="tab-content">
            <div class="tab-pane active" id="WorkstationInfo">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/WorkstationPreferencePartials/WorkstationSetupPartial.vbhtml")
            </div>
            <div class="tab-pane" id="BulkZones">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/WorkstationPreferencePartials/BulkZonesPartial.vbhtml")
            </div>
            <div class="tab-pane" id="PODSetup">
               @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/WorkstationPreferencePartials/PODSetupPartial.vbhtml")
            </div>
            <div class="tab-pane" id="SortBar">
               @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/WorkstationPreferencePartials/SortBarSetupPartial.vbhtml")
            </div>
            <div class="tab-pane" id="PickLevels">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/WorkstationPreferencePartials/PickLevelsPartial.vbhtml")
            </div>
            <div class="tab-pane" id="CustApps">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/WorkstationPreferencePartials/CustAppsPartial.vbhtml")
            </div>
            <div class="tab-pane" id="MiscSett">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/WorkstationPreferencePartials/MiscSettingsPartial.vbhtml")
            </div>
        </div>
    </div>
</div>