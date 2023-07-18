<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Process Put Aways"
    ViewData("PageName") = "| Process Put Aways"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

<div class="container-fluid">
    <div class="row" id="MainContent">
        <input type="hidden" id="ReqNumPutLabels" value="@Model.preferences.ReqNumPutLabels.ToString().ToLower()" />
        <input type="hidden" id="AutoPrintToteLabels" value="@Model.preferences.AutoPrintPutLabels.ToString().ToLower()" />
        <input type="hidden" id="AutoPrintOffCarPutList" value="@Model.preferences.AutoPrintOffCarPutList.ToString().ToLower()" />
        <input type="hidden" id="ReelLogic" value="@Model.ReelLogic.ToString().Trim().ToLower()" />
        <input type="hidden" id="ReplenishForward" value="@Model.preferences.AutoForReplen.tostring().Trim().ToLower()">
        <input type="hidden" id="PrintDirect" value="@Model.preferences.PrintDirect.ToString().Trim().ToLower()" />
        <input type="hidden" id="SplitShortPut" value="@Model.preferences.SplitShortPut.ToString().Trim().ToLower()" />
        <input type="hidden" id="SelectIfOne" value="@Model.preferences.SelIfOne.ToString().Trim().ToLower()" />
        <input type="hidden" id="AutoPutToteIDs" value="@Model.preferences.AutoPutTote.ToString().Trim().ToLower()" />
        <input type="hidden" id="AutoPrintToteIDLabels" value="@Model.preferences.AutoPrintPutTote.ToString().Trim().ToLower()" />
        <div class="col-md-12">
            <ul class="nav nav-tabs" role="tablist">
                @If Model.topBatch.ToString().Trim() <> "" Then
                    @<li><a href="#SetupTab" role="tab" data-toggle="tab">Tote Setup</a></li>
                    @<li class ="active"><a href="#ProcessTab" role="tab" data-toggle="tab">Process Put Aways</a></li>
                Else
                    @<li class="active"><a href="#SetupTab" role="tab" data-toggle="tab">Tote Setup</a></li>
                    @<li><a href="#ProcessTab" role="tab" data-toggle="tab">Process Put Aways</a></li>
                End If
            </ul>
            <div class="tab-content">
                @If Model.topBatch.ToString().Trim() <> "" Then
                    @<div class="tab-pane" id="SetupTab">@Html.Partial("~/Areas/Induction/Views/ProcessPutAways/Setup/ToteSetupPartial.vbhtml")</div>
                    @<div class="tab-pane active" id="ProcessTab">@Html.Partial("~/Areas/Induction/Views/ProcessPutAways/Process/ProcessPartial.vbhtml")</div>
                Else
                    @<div class="tab-pane active" id="SetupTab">@Html.Partial("~/Areas/Induction/Views/ProcessPutAways/Setup/ToteSetupPartial.vbhtml")</div>
                    @<div class="tab-pane" id="ProcessTab">@Html.Partial("~/Areas/Induction/Views/ProcessPutAways/Process/ProcessPartial.vbhtml")</div>
                End If
            </div>
        </div>
    </div>
</div>
<script>
    //Prompts user before letting them leave/close the Put Away Batch screen
    $(window).on('beforeunload', function(e) {
        return 'You are in the middle of Processing a Put Away Batch';
    });
</script>
@Html.Partial("~/Areas/Induction/Views/ProcessPutAways/Setup/TotesModalPartial.vbhtml")