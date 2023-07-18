<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-body">
        <div class="col-md-4 bottom-spacer">
            <button class="btn btn-primary btn-block" id="FTPSettings">Manage FTP Settings</button>
        </div>
        <div class="col-md-8">
            <div class="panel panel-info">
                <div class="panel-body white-bg">
                    <div class="row bottom-spacer">
                        <label class="col-md-4">FTP Import Work Folder</label>
                        <div class="col-md-8">
                            <input type="text" class="form-control" name="FTPImport" value="@Model("FTP Import Work Folder")"/>
                        </div>
                    </div>
                    <div class="row bottom-spacer">
                        <label class="col-md-4">FTP Export Work Folder</label>
                        <div class="col-md-8">
                            <input type="text" class="form-control" name="FTPExport" value="@Model("FTP Export Work Folder")" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>