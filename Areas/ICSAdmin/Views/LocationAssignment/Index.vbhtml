<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype object
@Code
    ViewData("Title") = "Location Assignment"
    ViewData("PageName") = "&nbsp; | &nbsp; Location Assignment"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div role="tabpanel">
                <ul class="nav nav-tabs" role="tablist">
                    <li role="presentation">
                        @If Model.AutoLocCounts Then
                            @<a href="#countTab" aria-controls="countTab" style="display:none" role="tab" data-toggle="tab" id="LocAssCount">
                            Count @For Each trans In Model.CountData
                                If trans.Type.ToString().ToLower() = "count" Then
                                    @<span class="label label-default">@trans.Count.ToString</span>
                                End If
                            Next
                        </a>
                        Else
                            @<a href="#countTab" aria-controls="countTab" role="tab" data-toggle="tab" id="LocAssCount">
                            Count @For Each trans In Model.CountData
                                If trans.Type.ToString().ToLower() = "count" Then
                                @<span class="label label-default">@trans.Count.ToString</span>
                                End If
                            Next
                        </a>
                        End If
                    </li>
                    <li role="presentation">
                        @If Model.AutoLocPicks Then
                            @<a href="#pickTab" aria-controls="pickTab" style="display:none"  role="tab" data-toggle="tab" id="LocAssPick">
                            Pick @For Each trans In Model.CountData
                                If trans.Type.ToString().ToLower() = "pick" Then
                                @<span class="label label-default">@trans.Count.ToString</span>
                                End If
                            Next
                        </a>
                        Else
                            @<a href="#pickTab" aria-controls="pickTab" role="tab" data-toggle="tab" id="LocAssPick">
                            Pick @For Each trans In Model.CountData
                                If trans.Type.ToString().ToLower() = "pick" Then
                                @<span class="label label-default">@trans.Count.ToString</span>
                                End If
                            Next
                        </a>
                        End If
                    </li>
                    <li role="presentation">
                        @If Model.AutoLocPuts Then
                            @<a href="#putawayTab" style="display:none"  aria-controls="putawayTab" role="tab" data-toggle="tab" id="LocAssPutAway">
                            Put Away @For Each trans In Model.CountData
                                If trans.Type.ToString().ToLower() = "put away" Then
                                @<span class="label label-default">@trans.Count.ToString</span>
                                End If
                            Next
                        </a>
                        Else
                            @<a href="#putawayTab" aria-controls="putawayTab" role="tab" data-toggle="tab" id="LocAssPutAway">
                            Put Away @For Each trans In Model.CountData
                                If trans.Type.ToString().ToLower() = "put away" Then
                                @<span class="label label-default">@trans.Count.ToString</span>
                                End If
                            Next
                        </a>
                        End If
                    </li>
                </ul>
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane" id="countTab">
                        @Html.Partial("~/Areas/ICSAdmin/Views/LocationAssignment/CountPartial.vbhtml")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="pickTab">
                        @Html.Partial("~/Areas/ICSAdmin/Views/LocationAssignment/PickPartial.vbhtml")
                    </div>
                    <div role="tabpanel" class="tab-pane" id="putawayTab">
                        @Html.Partial("~/Areas/ICSAdmin/Views/LocationAssignment/PutAwayPartial.vbhtml")
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/ICSAdmin/Views/LocationAssignment/LocAssCountModal.vbhtml")
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/LocationAssignment/LocAssCountDT.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/LocationAssignment/LocAssPickDT.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/LocationAssignment/LocAssPutAwayDT.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/LocationAssignment/LocAssHub.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/LocationAssignment/LocAss.js"></script>