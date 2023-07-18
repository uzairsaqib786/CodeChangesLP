<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Report, Label or Export Description</h3>
            </div>
            <div class="panel-body" style="max-height:800px; overflow-y:scroll;">
                <div class="row">
                    <div class="col-md-9 col-md-offset-3">
                        <label class="radio-inline">
                            <input type="radio" id="SystemCustomReports" name="CustReportsType" checked="checked" oninput="ToggleReportType()"/>
                            System
                        </label>
                        <label class="radio-inline">
                            <input type="radio" id="UserCustomReports" name="CustReportsType" oninput="ToggleReportType()"/>
                            User Created
                        </label>
                    </div>
                </div>
                <div class="row" style="padding-top:3%">
                    <div class="col-md-12">
                        <table class="table table-bordered table-condensed table-hover table-striped" id="ReportTitleTable" style="background-color:white;">
                            <tbody id="SysBody">
                                @For Each reportObj In Model.ReportTitles.Sys
                                    @<tr><td data-filename="@reportObj.Filename">@reportObj.Title</td></tr>
                                Next
                            </tbody>
                            <tbody id="UserBody" hidden="hidden">
                                @For Each reportObj In Model.ReportTitles.User
                                    @<tr><td data-filename="@reportObj.Filename">@reportObj.Title</td></tr>
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-9">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Report or Label Details
                </h3>
            </div>
            <div class="panel-body" id="ReportInputs">
                <div class="row">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary" id="ReportAddNew" data-toggle="tooltip" data-placement="top" data-original-title="Add New"><span class="glyphicon glyphicon-plus"></span></button>                       
                        <button disabled="disabled" type="button" class="btn btn-primary" id="ReportExport" data-toggle="tooltip" data-placement="top" data-original-title="Export"><span class="glyphicon glyphicon-export"></span></button>
                        <button disabled="disabled" type="button" class="btn btn-primary Print-Report" id="ReportPrint" data-toggle="tooltip" data-placement="top" data-original-title="Print"><span class="glyphicon glyphicon-print"></span></button>
                        <button disabled="disabled" type="button" class="btn btn-success" id="ReportDesign" data-toggle="tooltip" data-placement="top" data-original-title="Design"><span class="glyphicon glyphicon-pencil"></span></button>                     
                        <button disabled="disabled" type="button" class="btn btn-danger" id="ReportDelete" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-trash"></span></button>
                        <button disabled="disabled" type="button" class="btn btn-primary" id="PushReport" data-toggle="tooltip" data-placement="top" data-original-title="Push designer changes to all">Push Changes</button>
                        <button disabled="disabled" type="button" class="btn btn-primary" id="ImportFile" data-toggle="tooltip" data-placement="top" data-original-title="Import Report or Label File">Import</button>
                        <input type="file" style="display: none" id="ImportFileVal" accept=".lst,.lbl"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                            <label class="control-label">Test and Design Data</label>
                            <input disabled="disabled" type="text" readonly="readonly" class="form-control design-modal modal-launch-style"  id="ReportTestData">
                            <i class="glyphicon glyphicon-resize-full form-control-feedback design-modal modal-launch-style"></i>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label>Test/Design Data Type</label>
                        <select class="form-control" id="DesignDataType" disabled="disabled">
                            <option></option>
                            <option value="SP">Stored Procedure</option>
                            <option value="SQL">Raw SQL</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>System or Custom Report/Label</label>
                        <input type="text" class="form-control" disabled="disabled" id="SystemOrCustom" />
                    </div>
                    <div class="col-md-6">
                        <label>File Name</label>
                        <input disabled="disabled" type="text" class="form-control" id="ReportFilename" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Description</label>
                        <input type="text" class="form-control" id="ReportDescription" disabled="disabled" maxlength="255" />
                    </div>
                    <div class="col-md-6">
                        <label>Output Type</label>
                        <select class="form-control" id="ReportOutputType" disabled="disabled">
                            <option>Report</option>
                            <option>Label</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Export Filename</label>
                        <input type="text" class="form-control" id="ReportExportFilename" disabled="disabled" maxlength="50" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>