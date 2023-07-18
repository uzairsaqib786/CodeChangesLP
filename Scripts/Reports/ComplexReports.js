// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var reportTimer = mkTimer(function () {
        saveInputs();
    }, 200);
    $('#ReportTitleTable').on('click', 'tr:not(.disabled)', function () {
        var $this = $(this);

        clearReportDetails();

        if ($this.hasClass('active')) {
            $('#ReportTitleTable tr.active').removeClass('active');
            toggleDisabled(true);
            $('#ReportTestData, #DesignDataType, #ReportDescription, #ReportOutputType, #ReportExportFilename, #PushReport, #ImportFile').attr('disabled', 'disabled');
        } else {
            $('#ReportTitleTable tr.active').removeClass('active');
            $('#ReportTestData, #DesignDataType, #ReportDescription, #ReportOutputType, #ReportExportFilename, #PushReport, #ImportFile').removeAttr('disabled');
            $this.addClass('active');
        };
       
        getReportDetails();
    });

    function clearReportDetails() {
        $('#ReportInputs').find('input, select').val('');
    };

    $('.design-modal').click(function () {
        if (!$('#addnewdesign_modal').hasClass('in')) {
            $('#design_textarea').val($('#ReportTestData').val());
        } else {
            $('#design_textarea').val('');
        };
        $('#design_modal').modal('show');
    });

    $('#design_submit').click(function () {
        // if we're in the add new modal
        if ($('#addnewdesign_modal').hasClass('in')) {
            $('#NewDesignTestData').val($('#design_textarea').val());
        } // else we're already working with an existing design
        else {
            $('#ReportTestData').val($('#design_textarea').val()).trigger('input');
        };
    });

    $('#DesignDataType, #ReportOutputType').change(function () {
        reportTimer.startTimer();
    });

    $('#ReportInputs').on('input', 'input', function () {
        reportTimer.startTimer();
    });
    
    $('#ReportDesign').click(function () {
        if ($('#DesignDataType').val() == '') {
            alert("You must have a Design Data Type assigned before designing a report");
        } else if ($('#ReportTestData').val() == '') {
            alert("You must have Report Test Data assigned");
        } else if ($('#ReportFilename').val() == '') {
            alert("You must have a Report File name assigned");
        } else {
            // if ($(this).hasClass('disabled-with-tooltips')) { return; }; // no longer need this because listlabel21 supports launching the designer from a custom uri
            var obj = {
                desiredFormat: 'design',
                reportName: $('#ReportTitleTable tr.active td').attr('data-filename'), // actually the filename, not the reportname
                fields: new Array(),
                reportTitles: new Array(),
                type: $('#ReportOutputType').val() == 'Report' ? 2 : 1
            };

            $.ajax({
                url: "/CustomReports/getLLDesigner",
                type: "POST",
                data: obj,
                success: function (result) {
                    MessageModal('Designer', result,undefined, function () {
                        setTimeout(function () { $('#Message_Modal').modal('hide') }, 20000);
                    });
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    MessageModal('Error', xhr.responseText);
                }
            });
        };
    });

    $('#ReportExport').click(function () {
        if (validatePrintOrExport()) {
            $('#export_modal').modal('show');
        };
    });

    $(document.body).on('click', '#ReportPrint', function () {
        if (validatePrintOrExport()) {
            reportsHub.server.printCustomReport($('#ReportFilename').val().trim(), null);
        };
    });

    $('#PushReport').click(function () {
        var conf = confirm("Do you wish to give all workstations your version of this report?");
        if (conf) {
            reportsHub.server.pushReportChanges($('#ReportFilename').val()).done(function (mess) {
                if (mess == "error") {
                    MessageModal("Error", "Error has occured while pushing changes to the other worksations");
                } else {
                    MessageModal("Success", "Changes have been successfully pushed to the other workstations");
                };
            });
        };
    });

    $('#ImportFile').click(function () {
        $('#ImportFileVal').click();
    });

    $('#ImportFileVal').change(function () {
        var fileVal = $(this).val();
        if (!fileVal) return; // cancelled the upload, don't worry about it
        var fileName = fileVal.substring(fileVal.lastIndexOf("\\") + 1);
        var reportFileName = $('#ReportFilename').val();
        if (fileName === reportFileName) {
            // Try to upload this file
            var fileData = new FormData();
            fileData.append("fileData", this.files[0]);
            $.ajax({
                url: "/CustomReports/importFile",
                type: "POST",
                data: fileData,
                contentType: false,
                processData: false,
                success: function (result) {
                    if (result === "") MessageModal("Success", "File successfully uploaded");
                    else MessageModal("Error", result);
                    $(this).val('');
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    MessageModal('Error', xhr.responseText);
                    $(this).val('');
                }
            });
        }
        else {
            MessageModal("Error", "Uploaded filename \""+ fileName +"\" must match report filename \"" + reportFileName + "\"");
        }
    });

    function validatePrintOrExport() {
        var designType = $('#DesignDataType').val().trim().toUpperCase();
        var outputType = $('#ReportOutputType').val().trim().toUpperCase();
        if ($('#ReportTestData').val().trim() == '' ||
            (designType != 'SQL' && designType != 'SP') ||
            (outputType != 'REPORT' && outputType != 'LABEL')) {

            MessageModal('Warning', 'Test and Design Data, Test/Design Data Type and Output Type must be specified!');
            return false;
        } else {
            return true;
        };
    };

    $('#ReportExportFilename').on('input', function () {
        setNoExtension($(this));
    });

    $('#ExportFilename').on('input', function () {
        // if the user is on the complex reports tab, then we want to save the export type
        var value = this.value;
        if (!$('a[href="#basicReports"]').parent().hasClass('active')) {
            $('#ReportExportFilename').val(value).trigger('input');
        };
    });
});

function toggleDisabled(disable) {
    if (disable) {
        $('#ReportDesign:not(.disabled), #ReportExport, #ReportPrint, #ReportDelete, #PushReport, #ImportFile').attr('disabled', 'disabled');
    } else {
        $('#ReportDesign, #ReportExport, #ReportPrint, #ReportDelete, #PushReport, #ImportFile').removeAttr('disabled');
    };
};

function ToggleReportType() {
    if ($('#SystemCustomReports').prop('checked')) {
        $('#SysBody').removeAttr('hidden');
        $('#UserBody').attr('hidden', 'hidden');
    } else {
        $('#UserBody').removeAttr('hidden');
        $('#SysBody').attr('hidden', 'hidden');
    };
};