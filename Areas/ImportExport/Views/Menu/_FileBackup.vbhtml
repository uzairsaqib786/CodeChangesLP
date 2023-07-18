<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-body">
        <div class="alert alert-warning" role="alert">
            <strong>Note:</strong> The below location paths are as seen by the SQL server. EX: C: is the server's C: drive and NOT the local drive of PickProIE
        </div>
        <div class="panel panel-info">
            <div class="panel-body white-bg">
                <div class="row bottom-spacer">
                    <label class="col-md-3">Import File Backup Location</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" name="ImportFileBackupLoc" value="@Model("Import File Backup Location")" />
                    </div>
                    <div class="col-md-3 form-control-static">
                        <button class="btn btn-primary">Manage Import Files</button>
                    </div>
                </div>
                <div class="row bottom-spacer">
                    <label class="col-md-3">Export File Backup Location</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" name="ExportFileBackupLoc" value="@Model("Export File Backup Location")" />
                    </div>
                    <div class="col-md-3">
                        <button class="btn btn-primary">Manage Export Files</button>
                    </div>
                </div>
                <div class="row">
                    <label class="col-md-3">Backup By Day</label>
                    <div class="col-md-3">
                        <input type="checkbox" class="form-control-static" name="BackupByDay" @GlobalHTMLHelpers.Checked(Model("Backup By Day"))/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
