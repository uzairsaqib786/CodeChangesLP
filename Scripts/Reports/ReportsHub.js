// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022


function getSelectedReportData(report) {
    var $fields = $('#Field1, #Field2, #Field3, #Field4, #Field5, #Field6');
    $fields.attr("disabled", 'disabled');
    reportsHub.server.getSelectedReportData(report).done(function (details) {
        var opts = '<option> </option>';
        for (var y = 0; y < details.fields.length; y++) {
            opts += '<option>' + details.fields[y] + '</option>';
        };

        $('#PrintReport, #ExportReport, #PreviewReport').removeAttr('disabled');
        $('#FilterContainer :input').removeAttr('disabled');
        $fields.removeAttr('disabled').html(opts);

        for (var x = 0; x < details.reportData.length; x++) {
            if (x < 4) {
                $('#RT' + (x + 1)).val(details.reportData[x]);
            } else if (x < 10) {
                $('#Field' + (x - 3)).val(details.reportData[x]);
                checkDisableExpression($('#Field'+(x-3)).val(), '#Exp1' + (x-3))
            } else if (x < 16) {
                $('#ExpType' + (x - 9)).val(details.reportData[x]);
                checkSelectVal($('#ExpType' + (x - 9)));
            } else if (x < 22) {
                $('#Exp1' + (x - 15)).val(details.reportData[x]);
            } else if (x < 28) {
                $('#Exp2' + (x - 21)).val(details.reportData[x]);
            };
        };

    });
};

function saveTitles() {
    var report = $('#SelectedReport').val();
    var titles = [
        $('#RT1').val(), $('#RT2').val(), $('#RT3').val(), $('#RT4').val()
    ];
    reportsHub.server.saveReportTitles(report, titles).done(function () {
        
    });
};

function saveFieldsAndExpressions() {
    var report = $('#SelectedReport').val();
    var fields = new Array();
    var exps = new Array();
    var f, e;
    for (var x = 1; x <= 6; x++) {
        f = $('#Field' + x).val();
        if (f == null) { f = '' };
        fields.push(f);
        e = $('#ExpType' + x).val();
        if (e == null) { e = '' };
        exps.push(e);
    };

    reportsHub.server.saveReportFieldsExps(report, fields, exps).done(function () {

    });
};

function saveValues() {
    var report = $('#SelectedReport').val();
    var v1 = new Array();
    var v2 = new Array();

    for (var x = 1; x <= 6; x++) {
        v1.push($('#Exp1' + x).val());
        v2.push($('#Exp2' + x).val());
    };
    reportsHub.server.saveFieldValues(report, v1, v2).done(function () {

    });
};

function printCustomReport(report) {
    var where = new Array();

    //Puts where clause pieces into array
    for (var x = 1; x <= 6; x++) {
        where.push([
            $('#Field' + x).val(), $('#ExpType' + x).val(), $('#Exp1' + x).val(), $('#Exp2' + x).val()
        ]);
    };

    reportsHub.server.printCustomReport(report, where);
};

function getReportDetails() {
    var report = $('#ReportTitleTable .active');
    if (report.length > 0) {
        var filename = report.children('td').attr('data-filename');
        reportsHub.server.getReportDetails(filename).done(function (result) {
            toggleDisabled(false);
            // if system report/label
            if (result[4].toLowerCase() == 'true') {
                $('#ReportDescription, #ReportOutputType, #ReportDelete').attr('disabled', 'disabled');
            } else {
                $('#ReportDescription, #ReportOutputType, #ReportDelete').removeAttr('disabled');
            };
            $('#ReportDescription').val(result[0]);
            $('#ReportTestData').val(result[1]);
            $('#ReportFilename').val(result[2]);
            $('#ReportOutputType').val(result[3]);
            $('#SystemOrCustom').val(result[4].toLowerCase() == 'true' ? 'System' : 'Custom');
            $('#DesignDataType').val(result[5]);
            $('#ReportExportFilename, #ExportFilename').val(result[6]);
        });
    };
};

function saveInputs() {
    var oldFilename = $('#ReportTitleTable tr.active td').attr('data-filename');
    var newFilename = $('#ReportFilename').val();
    var description = $('#ReportDescription').val();
    var dataSource = $('#ReportTestData').val();
    var output = $('#ReportOutputType').val();
    var testDataType = $('#DesignDataType').val();
    var exportFilename = $('#ReportExportFilename').val();

    reportsHub.server.updateReportDetails(oldFilename, newFilename, description, dataSource, output, testDataType, exportFilename).done(function () {
        $('[data-filename="' + oldFilename + '"]').attr('data-filename', newFilename).text(description);
    });
};

function deleteReport(keepFile) {
    var filename = $('#ReportFilename').val();
    reportsHub.server.deleteReport(filename, keepFile).done(function (success) {
        if (!success) {
            alert('Unexpected error occurred.  If this persists please contact Scott Tech for support.');
        } else {
            $('#ReportTitleTable tr.active').remove();
            $('#ReportInputs').find('input, select').val('');
            toggleDisabled(true);
            $('#deletereport_modal').modal('hide');
        };
    });
};

function deleteReportFromModal(filename) {
    reportsHub.server.deleteReport(filename, false).done(function (success) {
        if (!success) {
            alert('Unexpected error occurred.  If this persists please contact Scott Tech for support.');
        } else {
            $('[data-filename="' + filename + '"]').parent().remove();
            $('#addnewdesign_submit').click();
        };
    });
};

function restoreDesign(all, filename) {
    if (filename.trim() == '') { return; };
    var obj = {
        description: $('#NewDescription').val(),
        filename: filename,
        dataSource: $('#NewDesignTestData').val(),
        dataType: $('#NewDesignDataType').val(),
        outputType: $('#NewOutputType').val() == 'Report' ? 2 : 1,
        exportFilename: $('#NewExportFilename').val()
    };
    reportsHub.server.restoreDesign(all, obj).done(function (success) {
        if (!success) {
            alert('Unknown error occurred during design restoration.  Please contact Scott Tech for support if this persists.');
        } else {
            $('#ReportTitleTable').append('<tr><td data-filename="' + obj.filename + '">' + obj.description + '</td></tr>');
            $('#addnewdesign_modal').modal('hide').one('hidden.bs.modal', function () {
                $('#ReportTitleTable').find('[data-filename="' + obj.filename + '"]').click();
            });
        };
    });
};