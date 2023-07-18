<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">System Settings</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel-group" id="SystemAccordion">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-target="#Overview" data-parent="#SystemAccordion">
                                <h3 class="panel-title">Overview <span class="accordion-caret-down"></span></h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle in" id="Overview">
                            <div class="row">
                                <div class="col-md-12">
                                    <img src="~/Areas/ImportExport/Images/Help/system.png" alt="System Settings tab" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    The image above is the <strong>System Settings</strong> tab on the <strong>Import Export</strong> page.  Various settings and configuration options are available on this page.
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-12">
                                    The following buttons are available:
                                    <ul>
                                        <li><button class="btn btn-primary" data-toggle="collapse" data-parent="#SystemAccordion" data-target="#Mapping">Transfer Field Mapping</button></li>
                                        <li><button class="btn btn-primary" data-toggle="collapse" data-parent="#SystemAccordion" data-target="#IEPath">Import/Export Path</button></li>
                                        <li><button class="btn btn-primary" data-toggle="collapse" data-parent="#SystemAccordion" data-target="#FTP">FTP Import/Export Settings</button></li>
                                        <li><button class="btn btn-primary" data-toggle="collapse" data-parent="#SystemAccordion" data-target="#Scheduler">Inventory Map Export Scheduler</button></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-target="#Mapping" data-parent="#SystemAccordion">
                                <h3 class="panel-title">Transfer Field Mapping <span class="accordion-caret-down"></span></h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="Mapping">
                            @Html.partial("~/Areas/ImportExport/Views/IEHelp/Partials/SystemSettings/TransferFieldMapping.vbhtml")
                        </div>
                    </div>
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-target="#IEPath" data-parent="#SystemAccordion">
                                <h3 class="panel-title">Import/Export Path <span class="accordion-caret-down"></span></h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="IEPath">
                            @Html.partial("~/Areas/ImportExport/Views/IEHelp/Partials/SystemSettings/ImportExportPath.vbhtml")
                        </div>
                    </div>
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-target="#FTP" data-parent="#SystemAccordion">
                                <h3 class="panel-title">FTP Import/Export Settings <span class="accordion-caret-down"></span></h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="FTP">
                            @Html.partial("~/Areas/ImportExport/Views/IEHelp/Partials/SystemSettings/FTPImportExportSettings.vbhtml")
                        </div>
                    </div>
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-target="#Scheduler" data-parent="#SystemAccordion">
                                <h3 class="panel-title">Inventory Map Export Scheduler <span class="accordion-caret-down"></span></h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="Scheduler">
                            @Html.partial("~/Areas/ImportExport/Views/IEHelp/Partials/SystemSettings/InventoryMapExportScheduler.vbhtml")
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>