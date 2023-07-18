// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var reportTitleTimer = mkTimer(function () {
        saveTitles();
    }, 600);
    var fieldExpTimer = mkTimer(function () {
        saveFieldsAndExpressions();
    }, 600);
    var expValueTimer = mkTimer(function () {
        saveValues();
    }, 600);
    $('#FilterContainer :input').attr('disabled', 'disabled')
    $('#RT1, #RT2, #RT3, #RT4').on('input', function () {
        reportTitleTimer.startTimer();
    });
    $('.ExpField').change(function () {
        fieldExpTimer.startTimer();
    });    
    $('.ExpValue').on('input', function () {
        expValueTimer.startTimer();
    });

    //Clears a filter for the current report
    $('[name="removeFilter"]').click(function () {
        var $this = $(this);
        $this.parents('.FilterContainer').find('.ExpValue').val('').trigger('input');
        $this.parents('.FilterContainer').find('.ExpField').val('').trigger('change');
    });

    //Makes ajax request to Preview a report
    $('#PreviewReport').click(function () {
        PreviewOrExport('preview', true);
    });

    $('#ClosePreview').click(function () {
        $('#contents').show();
        $('#preview').html('');
        $('#previewContents').hide();
    });

    $('[name="removeFilter"]').click(function () {
        $(this).parent().parent().find('input, select').val('');
        $(this).parent().parent().find('input').typeahead('close');
    });

    $('#ExportReport').click(function () {
        $('#export_modal').modal('show');
    });

    $(document.body).on('click', '#ExportType .btn', function () {
        PreviewOrExport($(this).text().toLowerCase(), $('a[href="#basicReports"]').parent().hasClass('active'));
        $('#export_modal').modal('hide');
    });

    // Shows/hides extra fields on expression type change
    $('#ExpType1, #ExpType2, #ExpType3, #ExpType4, #ExpType5, #ExpType6').change(function () {
        checkSelectVal($(this));
    });
        
    $('#Field1, #Field2, #Field3, #Field4, #Field5, #Field6').change(function () {
        var id = $(this).attr('id');
        var instance = id.substr(id.indexOf('d') + 1, id.length);
        $('#Exp1' +instance).val('');
        checkDisableExpression($(this).val(), '#Exp1' + instance);
    })

    $('#SelectedReport').change(function () {
        var $this = $(this);
        if ($this.val().trim() != '') {
            getSelectedReportData($this.val());
        } else {
            $('#PrintReport, #ExportReport, #PreviewReport').attr('disabled', 'disabled');
            $('#FilterContainer :input').attr('disabled', 'disabled').val('');
        };
        $('#ExportFilename').val($this.val());
    });

    $(document.body).on('click', '#PrintReport', function () {
        var report = $('#SelectedReport').val();
        if (report.trim() == '') {
            alert('You must select a report to print!');
        } else if (confirm('Click OK to print a ' + report + ' with the current filters.')) {
            printCustomReport(report);
        };
    });

});


function checkDisableExpression(value,selector) {
    if (value== '') {
        $(selector).attr('disabled', 'disabled');
    }
    else {
        $(selector).removeAttr('disabled');
    }
}

function checkSelectVal(select) {
    if (select.val() == "BETWEEN" || select.val() == "NOT BETWEEN") {
        var parent = select.parent().parent();
        parent.children().eq(3).css("visibility", "visible");
        parent.children().eq(4).css("visibility", "visible");
    } else {
        var parent = select.parent().parent();
        parent.children().eq(3).css("visibility", "hidden");
        parent.children().eq(4).css("visibility", "hidden");
    };
};

/*
    Gathers the data for the selected report and send the command to either export ir preview the report
*/
function PreviewOrExport(format, basic) {
    var fields = new Array();
    var objFields = new Array();
    var reportName = '';
    var reportTitles = new Array();
    var type;
    if (basic) {
        for (var x = 1; x <= 6; x++) {
            objFields.push({ 'field': $('#Field'.concat(x.toString())).val(), 'exptype': $('#ExpType'.concat(x.toString())).val(), 'expone': $('#Exp1'.concat(x.toString())).val(), 'exptwo': $('#Exp2'.concat(x.toString())).val() });
            fields.push([$('#Field'.concat(x.toString())).val(), $('#ExpType'.concat(x.toString())).val(), $('#Exp1'.concat(x.toString())).val(), $('#Exp2'.concat(x.toString())).val()]);
        };

        reportName = $('#SelectedReport').val();
        reportTitles = [
            $('#RT1').val(), $('#RT2').val(), $('#RT3').val(), $('#RT4').val()
        ];
        type = 0;
    } else {
        reportName = $('#ReportTitleTable tr.active td').attr('data-filename');
        if ($('#ReportOutputType').val() == 'Report') {
            type = 2;
        } else {
            type = 1;
        };
    };
    var exportFilename = $('#ExportFilename');
    setNoExtension(exportFilename);
    var obj = {
        'desiredFormat': format,
        'reportName': reportName,
        'reportTitles': reportTitles,
        'fields': fields,
        'backFilename': exportFilename.val(),
        'type': type,
        'objFields': objFields
    };
    obj.objFields = JSON.stringify(obj.objFields); //serializes the filter data so that it can be passed over url
    if (format == 'preview') {
        getLLPreviewOrPrint('/CustomReports/PreviewReport/', obj, false,'report', 'Tote Contents');
    } else {
        reportsHub.server.exportReport(obj);
    };
};
