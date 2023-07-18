<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType Dictionary(Of String, Object)
@Code
    ViewData("Title") = "Main Menu"
End Code
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs info" role="tablist" id="tablist">
                <li role="presentation">
                    <a href="#status" area-controls="status" role="tab" data-toggle="tab">
                        Status
                    </a>
                </li>
                <li role="presentation">
                    <a href="#systemsettings" area-controls="systemsettings" role="tab" data-toggle="tab">
                        System Settings
                    </a>
                </li>
                <li role="presentation">
                    <a href="#transfersettings" area-controls="transfersettings" role="tab" data-toggle="tab">
                        Transfer Settings
                    </a>
                </li>
                <li role="presentation">
                    <a href="#assignlocations" area-controls="assignlocations" role="tab" data-toggle="tab">
                        Assign Locations
                    </a>
                </li>
                <li role="presentation">
                    <a href="#managedata" area-controls="managedata" role="tab" data-toggle="tab">
                        Manage Data
                    </a>
                </li>
                <li role="presentation">
                    <a href="#archivepurge" area-controls="archivepurge" role="tab" data-toggle="tab">
                        Archive/Purge
                    </a>
                </li>
                <li role="presentation">
                    <a href="#invfields" area-controls="invfields" role="tab" data-toggle="tab">
                        Inv Fields to Modify
                    </a>
                </li>
                <li role="presentation">
                    <a href="#filebackup" area-controls="filebackup" role="tab" data-toggle="tab">
                        File Backup
                    </a>
                </li>
                <li role="presentation">
                    <a href="#ftp" area-controls="ftp" role="tab" data-toggle="tab">
                        FTP
                    </a>
                </li>
                <li role="presentation">
                    <a href="#inventory" area-controls="inventory" role="tab" data-toggle="tab">
                        Inventory
                    </a>
                </li>
            </ul>
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane" id="status">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_Status.vbhtml")
                </div>
                <div role="tabpanel" class="tab-pane" id="systemsettings">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_SystemSettings.vbhtml", Model)
                </div>
                <div role="tabpanel" class="tab-pane" id="transfersettings">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_TransferSettings.vbhtml", Model)
                </div>
                <div role="tabpanel" class="tab-pane" id="assignlocations">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_AssignLocations.vbhtml")
                </div>
                <div role="tabpanel" class="tab-pane" id="managedata">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_ManageData.vbhtml")
                </div>
                <div role="tabpanel" class="tab-pane" id="archivepurge">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_ArchivePurge.vbhtml")
                </div>
                <div role="tabpanel" class="tab-pane" id="invfields">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_InvFields.vbhtml")
                </div>
                <div role="tabpanel" class="tab-pane" id="filebackup">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_FileBackup.vbhtml")
                </div>
                <div role="tabpanel" class="tab-pane" id="ftp">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_FTP.vbhtml")
                </div>
                <div role="tabpanel" class="tab-pane" id="inventory">
                    @Html.Partial("~/Areas/ImportExport/Views/Menu/_Inventory.vbhtml")
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
@Html.Partial("~/Views/ModalPartials/CellSizeModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/VelocityCodeModalPartial.vbhtml")
@Html.partial("~/Areas/ImportExport/Views/Menu/_TransferFieldMapping.vbhtml")
@Html.partial("~/Areas/ImportExport/Views/Menu/_TransferFilePathSetup.vbhtml")
@Html.Partial("~/Areas/ImportExport/Views/Menu/FTPSettingsPartial.vbhtml")
<script src="~/Areas/ImportExport/Scripts/FTPSettings.js"></script>
@Html.Partial("~/Areas/ImportExport/Views/Menu/FTPPasswordPartial.vbhtml")
@Html.partial("~/Areas/ImportExport/Views/Menu/_InventoryMapExportScheduler.vbhtml")
<script src="~/Areas/ImportExport/Scripts/TransferSettings.js"></script>
<script src="~/Areas/ImportExport/Scripts/ManageData.js"></script>
@Html.partial("~/Areas/ImportExport/Views/Menu/XferFileFieldMap.vbhtml")
@Html.partial("~/Areas/ImportExport/Views/Menu/XMLFieldMapping.vbhtml")
@Html.partial("~/Areas/ImportExport/Views/Menu/TableDataManager.vbhtml")