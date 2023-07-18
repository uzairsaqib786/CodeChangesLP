<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="addnewdesign_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="addnewdesign_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="addnewdesign_label">Add New Custom Report/Label Design</h4>
            </div>
            <div class="modal-body">
                <div class="alert alert-warning alert-custom" role="alert" id="AddNewAlert" style="display:none;"></div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Description</label>
                        <input type="text" class="form-control" id="NewDescription" placeholder="Report/Label Description" maxlength="255" />
                    </div>
                    <div class="col-md-6">
                        <label>Filename</label>
                        <input type="text" class="form-control" id="NewFilename" placeholder="Filename" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                            <label class="control-label">Test and Design Data</label>
                            <input type="text" readonly="readonly" class="form-control design-modal modal-launch-style"  id="NewDesignTestData">
                            <i class="glyphicon glyphicon-resize-full form-control-feedback design-modal modal-launch-style"></i>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label>Test/Design Data Type</label>
                        <select class="form-control" id="NewDesignDataType">
                            <option> </option>
                            <option value="SP">Stored Procedure</option>
                            <option value="SQL">Raw SQL</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Output Type</label>
                        <select class="form-control" id="NewOutputType">
                            <option>Report</option>
                            <option>Label</option>
                        </select>
                    </div>
                    <div class="col-md-6">
                        <label>Export Filename</label>
                        <input type="text" class="form-control" id="NewExportFilename" placeholder="Export Filename" maxlength="50" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="AddNewColumns">

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="AddNewErrors">

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="AddNewFilePresent" style="display:none;">
                        <input style="display:none;" id="CurrentFilename" />
                        <div class="row">
                            <div class="col-md-12">
                                The specified filename already exists.  Would you like to add the existing design back to the program or overwrite it?  You can also change the filename specified and resubmit.
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <button type="button" class="btn btn-primary btn-block" id="RestoreWS">Restore Design for this workstation</button>
                            </div>
                            <div class="col-md-6">
                                <button type="button" class="btn btn-primary btn-block" style="display:none;" id="RestoreAll">Restore Design for all workstations</button>
                            </div>
                        </div>
                        <div class="row" style="padding-top:15px;">
                            <div class="col-md-6">
                                <button type="button" class="btn btn-danger btn-block" id="DeleteExistingDesign">Delete Design (PERMANENT!)</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="addnewdesign_dismiss">Close</button>
                <button type="button" class="btn btn-primary" id="addnewdesign_submit">Validate, Save and Design</button>
            </div>
        </div>
    </div>
</div>
