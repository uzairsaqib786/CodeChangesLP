<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">
            FTP Settings
        </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <img src="~/Areas/ImportExport/Images/Help/ftp.png" alt="FTP Settings tab" />
            </div>
            <div class="col-md-12">
                The <strong>FTP Settings</strong> tab allows the user to configure FTP settings for individual sites and individual file types and locations.
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                Clicking the <button class="btn btn-primary">Manage FTP Settings</button> button will open the FTP Import/Export Settings modal.
            </div>
            <div class="col-md-12">
                @Html.partial("~/Areas/ImportExport/Views/IEHelp/Partials/SystemSettings/FTPImportExportSettings.vbhtml")
            </div>
        </div>
    </div>
</div>