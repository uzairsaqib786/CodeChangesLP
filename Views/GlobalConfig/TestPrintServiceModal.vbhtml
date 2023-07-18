<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="testservice_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="testservice_label" aria-hidden="true">
    <div class="modal-dialog" style="width:90%;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="testservice_label">Print Service Test</h4>
            </div>
            <div class="modal-body">
                <div class="row" id="TestSetup">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4">
                                <label>Test Printers</label>
                                <div class="toggles toggle-modern" data-toggle-ontext="Yes" data-toggle-offtext="No" id="TestPrinters"></div>
                            </div>
                            <div class="col-md-4">
                                <label>Test Export</label>
                                <div class="toggles toggle-modern" data-toggle-ontext="Yes" data-toggle-offtext="No" id="TestExport"></div>
                            </div>
                            <div class="col-md-4">
                                <label>Test All Workstations</label>
                                <div class="toggles toggle-modern" data-toggle-ontext="Yes" data-toggle-offtext="No" id="AllWSIDs"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Application to Test</label>
                                <select id="ApplicationName" class="form-control">
                                    <option value="all">All</option>
                                    @For Each PPApp In PickPro_Web.Config.AppLicenses
                                        @<option value="@PPApp.Value.Info.URL">@PPApp.Value.Info.DisplayName</option>
                                    Next
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label>Type of Reports</label>
                                <select id="OutputType" class="form-control">
                                    <option value="3">Both</option>
                                    <option value="1">Custom Reports</option>
                                    <option value="2">System Reports</option>
                                </select>
                            </div>
                        </div>
                        <div class="row top-spacer">
                            <div class="col-md-6">
                                <label>(Un)select All Label Printers for testing <input type="checkbox" id="SelectLabels" class="form-control" /></label>
                            </div>
                            <div class="col-md-6">
                                <label>(Un)select all Report Printers for testing <input type="checkbox" id="SelectLists" class="form-control" /></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6" id="TestLabel">

                            </div>
                            <div class="col-md-6" id="TestList">

                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="TestResults" style="display:none;">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <legend>Preliminary Results</legend> - The results seen here are aggregated before the print/export request is sent to the service and may indicate problems with data in the Lists and Labels table or server folder structure, etc.
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-condensed table-bordered table-striped" style="background-color:white;" id="ImmediateResults">
                                    <thead>
                                        <tr>
                                            <td>Filename</td>
                                            <td>Error Type</td>
                                            <td>Error Message</td>
                                            <td>Line Number</td>
                                            <td>Stored Procedure/SQL</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td colspan="6">Awaiting results...</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <legend>Service Response Results</legend> - The results seen here are aggregated by the print service during the print/export process.  They may indicate problems with the List and Label installation, configuration of the print service or stored procedures/sql used to generate the reports.
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-condensed table-bordered table-striped" style="background-color:white;" id="ServiceResponse">
                                    <thead>
                                        <tr>
                                            <td>Export ID</td>
                                            <td>Response Received</td>
                                            <td>Success</td>
                                            <td>Workstation</td>
                                            <td>Filename</td>
                                            <td>Printer</td>
                                            <td>Error Message</td>
                                            <td>Stored Procedure/SQL</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td colspan="6" id="ServiceResponseFiller">Awaiting results...</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-4 col-md-offset-8">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="testservice_dismiss">Close</button>
                        <button type="button" class="btn btn-primary" id="testservice_submit">Begin Test</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/GlobalConfig/PrintServiceTestModal.js"></script>