<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->


@Code
    ViewData("Title") = "Batch Manager"
    ViewData("PageName") = "&nbsp; | &nbsp; Batch Manager"
End Code
<div class="container-fluid">
    <div role="tabpanel">
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#batch" aria-controls="batch" role="tab" data-toggle="tab">Batch</a></li>
            <li role="presentation"><a href="#super" aria-controls="super" role="tab" data-toggle="tab">Super Batch</a></li>
        </ul>
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="batch">
                @Html.Partial("~/Areas/ICSAdmin/Views/BatchManager/Batch/BatchPartial.vbhtml")
            </div>
            <div role="tabpanel" class="tab-pane" id="super">
                @Html.Partial("~/Areas/ICSAdmin/Views/BatchManager/SuperBatch/SuperBatchPartial.vbhtml")
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/ICSAdmin/Views/BatchManager/Batch/BatchManModal.vbhtml")
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/BatchManager/Batch/BM.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/BatchManager/Batch/BatchManModal.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/BatchManager/Batch/BMDataTables.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/BatchManager/Batch/BMHub.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/BatchManager/Batch/PickModal.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/BatchManager/SuperBatch/SBM.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/BatchManager/SuperBatch/SBMDataTables.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/BatchManager/SuperBatch/SBMHub.js"></script>