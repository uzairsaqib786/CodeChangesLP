<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Dictionary(Of String, object)
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
                        <a href="#General" aria-controls="General" role="tab" data-toggle="tab">General Settings</a>
                    </li>
                    <li role="presentation">
                        <a href="#Batch" aria-controls="Batch" role="tab" data-toggle="tab">Batch Settings</a>
                    </li>
                    <li role="presentation">
                        <a href="#WorkerRanges" aria-controls="WorkerRanges" role="tab" data-toggle="tab">Worker Ranges</a>
                    </li>
                    <li role="presentation">
                        <a href="#LocationRanges" aria-controls="LocationRanges" role="tab" data-toggle="tab">Location Ranges</a>
                    </li>
                    <li role="presentation">
                        <a href="#WMUsers" aria-controls="WMUsers" role="tab" data-toggle="tab">WM Users</a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active" id="General">
                        @Html.Partial("~/Areas/WorkManager/Views/Preferences/TabPartials/General.vbhtml")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="Batch">
                        @Html.Partial("~/Areas/WorkManager/Views/Preferences/TabPartials/Batch.vbhtml")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="WorkerRanges">
                        @Html.Partial("~/Areas/WorkManager/Views/Preferences/TabPartials/WorkerRanges.vbhtml")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="LocationRanges">
                        @Html.Partial("~/Areas/WorkManager/Views/Preferences/TabPartials/LocationRanges.vbhtml")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="WMUsers">
                        @Html.Partial("~/Areas/WorkManager/Views/Preferences/TabPartials/WMUsers.vbhtml")
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<link href="~/Content/toggles.css" rel="stylesheet" />
<link href="~/Content/toggles-modern.css" rel="stylesheet" />
<script src="~/Scripts/toggles.min.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/FilterMenu/FilterMenu.js"></script>
<script src="~/Areas/WorkManager/Scripts/Preferences/Preferences.js"></script>
<script src="~/Areas/WorkManager/Scripts/Preferences/WMUsers.js"></script>
@Html.Partial("~/Areas/WorkManager/Views/Modals/LocRangeAddEditModal.vbhtml")
@Html.Partial("~/Areas/WorkManager/Views/Preferences/WorkRangeEditModal.vbhtml")