<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row" style="margin-top:5px;">
    <div class="col-md-2">
        <ul class="nav nav-pills nav-stacked" style="padding-right:10px;border-right: 2px solid grey;" role="tablist">
            <li class="active"><a href="#setup" data-toggle="tab">General Setup</a></li>
            <li><a href="#locationzones" data-toggle="tab">Location Zones</a></li>
            <li><a href="#ufs" data-toggle="tab">Field Name Mapping</a></li>
            <li><a href="#bpsettings" data-toggle="tab">Bulk Handheld Settings</a></li>
            <li><a href="#lookupList" data-toggle="tab">Lookup Lists</a></li>
            <li><a href="#scanVerifySetup" data-toggle="tab">Scan Verification Setup</a></li>
            <li><a href="#devPrefs" data-toggle="tab">Device Preferences</a></li>
            <li><a href="#lightTreeSetup" data-toggle="tab">Light Tree Setup</a></li>
        </ul>
    </div>
    <div class="col-md-10">
        <div class="tab-content">
            <div class="tab-pane active" id="setup">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/CompanySetupPartial.vbhtml")
            </div>
            <div class="tab-pane" id="locationzones">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/LocationZonePartial.vbhtml")
            </div>
            <div class="tab-pane" id="ufs">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/UserFieldsPartial.vbhtml")
            </div>
            <div class="tab-pane" id="bpsettings">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/HandheldSettingsPartial.vbhtml")
            </div>
            <div class="tab-pane" id="lookupList">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/LookupListsPartial.vbhtml")
            </div>
            <div class="tab-pane" id="devPrefs">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/DevicePreferencesPartial.vbhtml")
            </div>
            <div class="tab-pane" id="scanVerifySetup">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/ScanVerifySetup.vbhtml")
            </div>
            <div class="tab-pane" id="lightTreeSetup">
                @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/LightTreeSetup.vbhtml")
            </div>
        </div>
    </div>
</div>
