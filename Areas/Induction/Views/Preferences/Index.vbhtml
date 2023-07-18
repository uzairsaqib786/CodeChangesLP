<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype object
@Code
    ViewData("Title") = "Preferences"
    ViewData("PageName") = "&nbsp; | &nbsp; Preferences"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div role="tabpanel">
                <ul class="nav nav-tabs" role="tablist">
                    <li role="presentation" class="active">
                        <a href="#SystemSettingsTab" aria-controls="SystemSettingsTab" role="tab" data-toggle="tab" id="PrefSystSett">System Settings</a>
                    </li>
                    <li role="presentation">
                        <a href="#PickSettingsTab" aria-controls="PickSettingsTab" role="tab" data-toggle="tab" id="PrefPickSett">Pick Settings</a>
                    </li>
                    <li role="presentation">
                        <a href="#PutSettingsTab" aria-controls="PutSettingsTab" role="tab" data-toggle="tab" id="PrefPutSett">Put Away Settings</a>
                    </li>
                    <li role="presentation">
                        <a href="#PrintSettingsTab" aria-controls="PrintSettingsTab" role="tab" data-toggle="tab" id="PrefPrintSett">Print Settings</a>
                    </li>
                    <li role="presentation">
                        <a href="#MiscSetupTab" aria-controls="MiscSetupTab" role="tab" data-toggle="tab" id="PrefMiscSetup">Misc Setup</a>
                    </li>
                    <li role="presentation">
                        <a href="#ReelTrackingTab" aria-controls="ReelTrackingTab" role="tab" data-toggle="tab" id="PrefReelTrack">Reel Tracking</a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active" id="SystemSettingsTab">
                        @Html.partial("SystemTab")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="PickSettingsTab">
                        @Html.partial("PickTab")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="PutSettingsTab">
                        @Html.partial("PutTab")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="PrintSettingsTab">
                        @Html.partial("PrintTab")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="MiscSetupTab">
                        @Html.partial("MiscTab")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="ReelTrackingTab">
                        @Html.partial("ReelTab")
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/Preferences/Preferences.js"></script>
