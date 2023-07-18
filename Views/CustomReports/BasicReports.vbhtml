<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Report Detail</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12" style="margin-bottom:5px;">
                        <button disabled type="button" class="btn btn-primary Print-Report" id="PrintReport" data-toggle="tooltip" data-original-title="Print"><span class="glyphicon glyphicon-print"></span></button>
                        <button disabled type="button" class="btn btn-primary" id="ExportReport" data-toggle="tooltip" data-original-title="Export"><span class="glyphicon glyphicon-export"></span></button>
                        <button disabled type="button" class="btn btn-primary" id="PreviewReport" data-toggle="tooltip" data-original-title="Preview (Top ~50 pages only)"><span class="glyphicon glyphicon-share"></span></button>
                    </div>
                    <div class="col-md-12">
                        <label>Choose Report</label>
                        <select class="form-control" id="SelectedReport">
                            <option value=" "> </option>
                            @For Each opt In Model.reports
                                @<option value="@opt">@opt</option>
                            Next
                        </select>
                    </div>
                    <div class="col-md-12">
                        <label>Report Title #1</label>
                        <input type="text" class="form-control" id="RT1" placeholder="Report Title 1" maxlength="50" />
                    </div>
                    <div class="col-md-12">
                        <label>Report Title #2</label>
                        <input type="text" class="form-control" id="RT2" placeholder="Report Title 2" maxlength="50" />
                    </div>
                    <div class="col-md-12">
                        <label>Report Title #3</label>
                        <input type="text" class="form-control" id="RT3" placeholder="Report Title 3" maxlength="50" />
                    </div>
                    <div class="col-md-12">
                        <label>Report Title #4</label>
                        <input type="text" class="form-control" id="RT4" placeholder="Report Title 4" maxlength="50" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-9">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Report Filters
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <label>Field(s) to filter on</label>
                    </div>
                    <div class="col-md-2">
                        <label>Expression Type</label>
                    </div>
                    <div class="col-md-3">
                        <label>Value to test in expression</label>
                    </div>
                    <div class="col-md-4">
                        <label style="visibility:hidden;" id="SecondValueLabel">Second value to test in expression</label>
                    </div>
                </div>
                <div id="FilterContainer">
                    @For x As Integer = 1 To 6
                        @<div class="row FilterContainer" style="padding-top:5px;" id="FilterContainer@(x)">
                            <div class="col-md-2">
                                <select class="form-control ExpField" id="Field@(x)"></select>
                            </div>
                            <div class="col-md-2">
                                <select class="form-control ExpField" id="ExpType@(x)">
                                    <option value=""> </option>
                                    <option value="=">= (Equals)</option>
                                    <option value=">">> (Greater Than)</option>
                                    <option value="<">< (Less Than)</option>
                                    <option value=">=">>= (Greater Than or Equal)</option>
                                    <option value="<="><= (Less Than or Equal)</option>
                                    <option value="<>"><> (Not Equal)</option>
                                    <option value="LIKE">LIKE (Matches value with wildcards)</option>
                                    <option value="NOT LIKE">NOT LIKE (Matches value with wildcards)</option>
                                    <option value="NULL">NULL (Empty/Blank)</option>
                                    <option value="NOT NULL">NOT NULL (Not Empty/Blank)</option>
                                    <option value="BETWEEN">BETWEEN</option>
                                    <option value="NOT BETWEEN">NOT BETWEEN</option>
                                    <option value="IN">IN (In list like 1, 2, 3, 4)</option>
                                    <option value="NOT IN">NOT IN (Not in list like 1, 2, 3, 4)</option>
                                </select>
                            </div>
                            <div class="col-md-3">
                                <input type="text" class="form-control ExpValue" placeholder="Expression value" id="Exp1@(x)" maxlength="50" />
                            </div>
                            <div style="visibility:hidden;" class="col-md-1 text-center">
                                AND
                            </div>
                            <div style="visibility:hidden;" class="col-md-3">
                                <input type="text" class="form-control ExpValue" placeholder="Second expression value" id="Exp2@(x)" maxlength="50" />
                            </div>
                            <div class="col-md-1">
                                <button type="button" class="btn btn-primary" data-toggle="tooltip" title="Clear Filter" name="removeFilter"><span class="glyphicon glyphicon-remove"></span></button>
                            </div>
                        </div>
                    Next
                </div>
            </div>
        </div>
    </div>
</div>