<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Organize Work"
    ViewData("PageName") = "&nbsp; | &nbsp; Organize Work"
End Code

<div class="container-fluid">
    <div class="row top-spacer">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Go to Page Buttons
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="col-md-4">
                        <form action="/WM/SelectWork" method="POST">
                            <input type="hidden" name="OrganizeWork" value="True" />
                            <button type="submit" class="btn btn-primary btn-block">Select Work</button>
                        </form>
                    </div>
                    <div class="col-md-4">
                        @If Model.Perms(2) Then
                            @<a href="/CustomReports?App=WM" class="btn btn-primary btn-block">Reports</a>
                        Else
                            @<button type="button" class="btn btn-primary btn-block" disabled>Reports</button>
                        End If
                    </div>
                    <div class="col-md-4">
                        @If Model.Perms(0) Then
                            @<a href="/WM/Preferences" class="btn btn-primary btn-block">Work Preferences</a>
                        Else
                            @<button type="button" class="btn btn-primary btn-block" disabled>Work Preferences</button>
                        End If
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Organize Location Ranges
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label>Total Open Lines</label>
                            <input type="text" id="ToteOpenLinesOW" disabled class="form-control" value="@Model.Info.OpenCount"/>
                        </div>
                        <div class="col-md-3">
                            <label>Auto Refresh Minutes</label>
                            <input type="text" id="RefreshMinsOW" oninput="setNumericInRange($(this), 1, SqlLimits.numerics.int.max)" class="form-control" value="@Model.Info.RefreshTime" />
                        </div>
                        <div class="col-md-2" style="padding-top:20px;">
                            <button type="button" data-toggle="tooltip" data-placement="top" title="Refresh Ranges" class="btn btn-primary btn-block" id="RefreshLocRanges"><span class="glyphicon glyphicon-refresh"></span></button>
                        </div>
                        <div class="col-md-2" style="padding-top:20px;">
                            <button type="button" class="btn btn-primary btn-block edit-modal" disabled id="EditLocRangeOW">Edit Location Range</button>
                        </div>
                        <div class="col-md-2" style="padding-top:20px;">
                            <button type="button" class="btn btn-primary btn-block" id="AssignWork">Assign Work</button>
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-12">
                            <table id="OrganizeWorkTable" class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                                <thead>
                                    <tr>
                                        <th class="text-center">ID</th>
                                        <th class="text-center">Range Name</th>
                                        <th class="text-center">Start Location</th>
                                        <th class="text-center">End Location</th>
                                        <th class="text-center">Open Picks</th>
                                        <th class="text-center">Open Put Aways</th>
                                        <th class="text-center">Open Counts</th>
                                        <th class="text-center">Need Workers Picks</th>
                                        <th class="text-center">Need Workers Put Aways</th>
                                        <th class="text-center">Need Workers Counts</th>
                                        <th class="text-center">Multi Worker Range</th>
                                        <th class="text-center">Active</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/WorkManager/Scripts/OrganizeWork/OrganizeWork.js"></script>
@Html.Partial("~/Areas/WorkManager/Views/Modals/LocRangeAddEditModal.vbhtml")
@Html.Partial("~/Areas/WorkManager/Views/Modals/AssignWork.vbhtml")