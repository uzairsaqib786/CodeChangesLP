<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Preferences"
    ViewData("PageName") = " &nbsp; | &nbsp; Preferences"
End Code
<div class="container-fluid">
    <ul class="nav nav-tabs" role="tablist">
        <li class=""><a href="#workstation" role="tab" data-toggle="tab">Workstation Preferences</a></li>
        <li class="active"><a href="#system" role="tab" data-toggle="tab">System Preferences</a></li>
    </ul>
    <div class="tab-content">
        <div class="tab-pane" id="workstation">
            @Html.Partial("WorkstationPreferences")
        </div>
        <div class="tab-pane active" id="system">
           @Html.Partial("SystemPreferences")
        </div>
    </div>
</div>
@Html.Partial("LogoModal", Model.CompanyLogo)
<link href="~/Content/toggles.css" rel="stylesheet" />
<link href="~/Content/toggles-modern.css" rel="stylesheet" />
<script src="~/Scripts/toggles.min.js"></script>
<script src="~/Scripts/uploader/jquery.form.js"></script>
<script src="~/Scripts/uploader/jquery.uploadfile.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/CompanySetup/CompanySetup.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/CompanySetup/CompanyLogo.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/FieldNameMappings/FieldNameMappings.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/LookupList/UFSetup.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/LookupList/AdjustmentLookup.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/LookupList/ToteSetup.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/BulkHandheldSettings/BulkHandheldSettings.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/LocationZones/LocationZones.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/ScanVerifySetup/ScanVerifySetup.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/WorkstationPreferences/Setup/WorkstationSetup.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/WorkstationPreferences/BulkZones/BulkZones.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/WorkstationPreferences/PODSetup/PODSetup.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/WorkstationPreferences/PickLevels/PickLevels.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/WorkstationPreferences/SortBar/SortBarSetup.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/DevicePreferences/DevicePreferences.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/WorkstationPreferences/Misc/MiscellaneousSettings.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/DevicePreferences/DevicePreferencesModal.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/DevicePreferences/IPTI.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/DevicePreferences/TrayTilt.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/SystemPreferences/LightTree/LightTree.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Preferences/Preferences.js"></script>