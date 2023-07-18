<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType Dictionary(Of String, Object)
<div class="panel panel-info">
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12 bottom-spacer">
                <div class="col-md-3">
                    <button class="btn btn-primary btn-xl btn-block" id="transFieldMap">Transfer Field Mapping</button>
                </div>
                <div class="col-md-2">
                    <button class="btn btn-primary btn-xl btn-block" id="XMLFieldMap">XML Field Mapping</button>
                </div>
                <div class="col-md-2">
                    <button class="btn btn-primary btn-xl btn-block" id="XferSetup">Import/Export Path</button>
                </div>
                <div class="col-md-2">
                    <button class="btn btn-primary btn-xl btn-block" id="FTP_Close">FTP Import/Export Settings</button>
                </div>
                <div class="col-md-3">
                    <button class="btn btn-primary btn-xl btn-block" id="invMapExportSched">Inventory Map Export Scheduler</button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-body white-bg">
                        <div class="col-md-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-md-6">Import Transactions To</label>
                                    <div class="col-md-6">
                                        <select class="form-control" name="ImportTransactionsTo">
                                            @GlobalHTMLHelpers.SelectOption(Model("Import Table"), {"Open Transactions", "Open Transactions Pending"})
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-6">Import File Type</label>
                                    <div class="col-md-6">
                                        <select class="form-control" name="ImportFileType">
                                            @GlobalHTMLHelpers.SelectOption(Model("Import File Type"), {"CSV", "Fixed Field", "Table"})
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-6">Export File Type</label>
                                    <div class="col-md-6">
                                        <select class="form-control" name="ExportFileType">
                                            @GlobalHTMLHelpers.SelectOption(Model("Export File Type"), {"CSV", "Fixed Field", "Table"})
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-6">Automatic Import of Inventory Refresh File</label>
                                    <div class="form-control-static col-md-6">
                                        <input type="checkbox" name="AutoImportInv" @GlobalHTMLHelpers.Checked(Model("Automatic Import Inv Check"))/>
                                    </div>
                                </div>
                                <hr />
                                <div class="form-group">
                                    <label class="control-label col-md-6">Automatic Import</label>
                                    <div class="form-control-static col-md-6">
                                        <input type="checkbox" id="AutoImportCheck" name="AutoImport" @GlobalHTMLHelpers.Checked(Model("Automatic Import Check")) />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-6">Import Process Interval Minutes</label>
                                    <div class="col-md-6">
                                        <input type="number" name="ImportInterval" class="form-control" value="@Model("Import Filecheck Minutes")" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-6">Last Import</label>
                                    <div class="form-control-static col-md-6">
                                        <span id="lastimport"></span>
                                    </div>
                                </div>
                                <hr />
                                <div class="form-group">
                                    <label class="control-label col-md-6">Automatic Export</label>
                                    <div class="form-control-static col-md-6">
                                        <input type="checkbox" name="AutoExport" @GlobalHTMLHelpers.Checked(Model("Automatic Export Check"))/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-6">Export Process Interval Minutes</label>
                                    <div class="col-md-6">
                                        <input type="number" name="ExportInterval" class="form-control" value="@Model("Export Filecheck Minutes")" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-6">Last Export</label>
                                    <div class="form-control-static col-md-6">
                                        <span id="lastexport"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-body white-bg">
                        <div class="col-md-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-md-8">Create Files for Completed Transactions</label>
                                    <div class="form-control-static col-md-4">
                                        <input type="checkbox" name="ExportFiles" @GlobalHTMLHelpers.Checked(Model("Export Files")) />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-8">No Carriage Return</label>
                                    <div class="form-control-static col-md-4">
                                        <input type="checkbox" name="NoCarriageReturn" @GlobalHTMLHelpers.Checked(Model("NoCarriageReturn")) />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-8">Block Import Duplicates</label>
                                    <div class="form-control-static col-md-4">
                                        <input type="checkbox" name="CheckDupes" @GlobalHTMLHelpers.Checked(Model("Check Dupes")) />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-8">Days Back to Check for Duplicates</label>
                                    <div class="col-md-4">
                                        <input type="number" name="DupeDays" class="form-control" value="@Model("Dupe Days Check")"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-8">Trim User Fields</label>
                                    <div class="form-control-static col-md-4">
                                        <input type="checkbox" name="TrimUserFields" @GlobalHTMLHelpers.Checked(Model("Carousel Map"))/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-8">Trim Item Number</label>
                                    <div class="form-control-static col-md-4">
                                        <input type="checkbox" name="TrimItemNumber" @GlobalHTMLHelpers.Checked(Model("Trim Item Number"))/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>