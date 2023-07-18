// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#ReportAddNew').click(function () {
        var $this = $(this);
        // if not IE exit
        if ($this.hasClass('disabled-with-tooltips')) { return; };
        $('#addnewdesign_modal').modal('show').find('input, select').val('');
        $('#AddNewErrors, #AddNewColumns').html('');
        $('#AddNewFilePresent').hide();
    });

    $('#addnewdesign_submit').click(function () {
        saveNew();
    });

    $('#NewFilename').on('input', function () {
        setNoExtension($(this));
    });

    $('#NewFilename').on('focusout', function () {
        setExtension();
    });

    $('#NewOutputType').change(function () {
        setExtension();
    });

    $('#addnewdesign_modal .modal-body').on('input', 'input', function () {
        $('#AddNewAlert').hide();
    });
    $('#addnewdesign_modal .modal-body').on('change', 'select', function () {
        $('#AddNewAlert').hide();
    });

    $('#RestoreWS, #RestoreAll').click(function () {
        restoreDesign(this.id == 'RestoreAll', $('#CurrentFilename').val());
    });

    $('#DeleteExistingDesign').click(function () {
        // delete and don't keep the file
        if (confirm('Click OK to permanently delete the design file.')) {
            deleteReportFromModal($('#CurrentFilename').val());
        };
    });

    function setExtension() {
        var $output = $('#NewOutputType');
        var $filename = $('#NewFilename');
        setNoExtension($filename);
        if ($output.val() == 'Report' && $filename.val().trim() != '') {
            $filename.val($filename.val() + '.lst');
        } else if ($filename.val().trim() != '' && $output.val() == 'Label') {
            $filename.val($filename.val() + '.lbl');
        };
    };

    function saveNew() {
        var newParams = [
            $('#NewDescription').val(),
            $('#NewFilename').val(),
            $('#NewDesignTestData').val(),
            $('#NewDesignDataType').val(),
            $('#NewOutputType').val(),
            $('#NewExportFilename').val()
        ];
        var fields = [
            'Description', 'Filename', 'Test and Design Data', 'Test/Design Data Type', 'Output Type'
        ];
        var valid = true;
        for (var x = 0; x < newParams.length - 1; x++) {
            if (newParams[x].trim() == '') {
                alertNew(fields[x] + ' must not be left blank!');
                valid = false;
                break;
            };
        };

        if ($('[data-filename="' + newParams[1] + '"]').length > 0) {
            alertNew('Filename must be unique!');
            valid = false;
        };

        if (valid) {
            reportsHub.server.validateNewDesign(newParams).done(function (result) {
                if (!result.success) {
                    alert('Validation for adding a new report failed with an unknown error.  Please contact Scott Tech for support if this persists.');
                } else {
                    var appendstring = buildAppendString('File warnings:', result.fileObj.errs);
                    appendstring += buildAppendString('SQL warnings:', result.sqlObj.errs);
                    if (result.sqlObj.sqlError) {
                        appendstring += '<div class="row"><div class="col-md-12">There was an error with the stored procedure or sql string provided.  \
                            Please ensure that the procedure contains only valid statements and only returns one result set.\
                              If a stored procedure is provided it must contain either no parameters or acceptable defaults.</div></div>';
                    };
                    $('#AddNewErrors').html(appendstring);

                    var resultSets = result.sqlObj.numResultSets;
                    var resultSetString = '';
                    if (resultSets != 1) {
                        resultSetString = '<div class="row">\
                                                <div class="col-md-12">\
                                                    Number of result sets found: ' + resultSets + '.  ONE result set must be used.\
                                                </div>\
                                            </div>';
                    };

                    $('#AddNewColumns').html(buildAppendString('Columns in the first resultset:', result.sqlObj.columns)
                        + resultSetString);

                    // if the file is present already we need to deal with it before we can continue
                    if (result.fileObj.canAddFileToDefaultTable) {
                        $('#AddNewFilePresent').show();
                        $('#RestoreAll').show();
                        $('#CurrentFilename').val(newParams[1]);
                    } else if (result.fileObj.canAddFileToWSTable) {
                        $('#AddNewFilePresent').show();
                        $('#RestoreAll').hide();
                        $('#CurrentFilename').val(newParams[1]);
                    } // else we don't have a file already and we need to check that there are no errors that need to be dealt with
                    else {
                        $('#AddNewFilePresent').hide();
                        $('#RestoreAll').hide();
                        $('#CurrentFilename').val('');
                        // if the file doesn't already exist and we have a valid configuration then continue to the designer
                        if (appendstring.length == 0) {
                            var obj = {
                                description: $('#NewDescription').val(),
                                filename: $('#NewFilename').val(),
                                dataSource: $('#NewDesignTestData').val(),
                                dataType: $('#NewDesignDataType').val(),
                                outputType: $('#NewOutputType').val() == 'Report' ? 2 : 1,
                                exportFilename: $('#NewExportFilename').val(),
                                appName: $('#AppName').val()
                            };

                            $.ajax({
                                url: "/CustomReports/getLLDesignerNewDesign",
                                type: "POST",
                                data: obj,
                                success: function (result) {
                                    $('#addnewdesign_modal').on('hidden.bs.modal', function () {
                                        MessageModal('Designer', result);
                                    }).modal('hide');
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    alert(xhr.status);
                                }
                            });
                        };
                    };
                };
            });
        };
    };

    function buildAppendString(title, errors) {
        var appendstring = '';
        if (errors.length > 0) {
            appendstring += '<h4>' + title + '</h4><ul>';
            for (var y = 0; y < errors.length; y++) {
                appendstring += '<li>' + errors[y] + '</li>';
            };
            appendstring += '</ul>';
        };
        return appendstring;
    };

    function alertNew(message) {
        $('#AddNewAlert').show().html(message);
    };
});