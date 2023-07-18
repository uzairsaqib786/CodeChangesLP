<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Reports"
    ViewData("PageName") = " &nbsp; | &nbsp; Reports"
    Layout = PickPro_Web.GlobalFunctions.chooseLayoutFile(Model.app)
End Code
<div class="container-fluid">
    <div id="contents">
        <ul class="nav nav-tabs" role="tablist">
            @If Model.app.ToLower() = "admin" Then
                @<li class="active"><a href="#basicReports" role="tab" data-toggle="tab">Basic Reports & Labels</a></li>
                @<li><a href="#customReports" role="tab" data-toggle="tab">Custom Reports & Labels</a></li>
            Else
                @<li class="active"><a href="#customReports" role="tab" data-toggle="tab">Custom Reports & Labels</a></li>
            End If
        </ul>
        <div class="tab-content">
            @If Model.app.ToLower() = "admin" Then
                @<div class="tab-pane active" id="basicReports" style="padding-top:5px;">
                    @Html.Partial("BasicReports")
                </div>
                @<div class="tab-pane" id="customReports" style="padding-top:5px;">
                    @Html.Partial("DesignReports")
                </div>
            Else
                @<div class="tab-pane active" id="customReports" style="padding-top:5px;">
                    @Html.Partial("DesignReports")
                </div>
            End If
        </div>
    </div>
</div>
@Html.Partial("~/Views/CustomReports/ExportTypePartial.vbhtml")
@Html.Partial("~/Views/CustomReports/NewReportDesignPartial.vbhtml")
@Html.Partial("~/Views/CustomReports/TestDataPartial.vbhtml")
@Html.Partial("~/Views/CustomReports/DeleteReportModalPartial.vbhtml")
<script src="~/Scripts/Reports/BasicReports.js"></script>
<script src="~/Scripts/Reports/ReportsHub.js"></script>
<script src="~/Scripts/Reports/ComplexReports.js"></script>
<script src="~/Scripts/Reports/AddNewDesign.js"></script>
<script src="~/Scripts/Reports/BasicReportsTypeAhead.js"></script>
<script src="~/Scripts/Reports/DeleteReport.js"></script>