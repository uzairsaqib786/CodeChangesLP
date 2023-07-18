// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*
    Properties:
        top: holds the datatable for table (id = ImmediateResults)
        bottom: holds the datatable for table (id = ServiceResponse)
*/
var testTable = {};
$(document).ready(function () {
    /* Indicates whether the test has been started so that we can show the right area */
    var testRan = false;
    testTable.top = $('#ImmediateResults').DataTable({
        data: [], dom: 'trip',
        processing: true
    });
    testTable.bottom = $('#ServiceResponse').DataTable({
        data: [],
        createdRow: function (row, data, index) {
            $(row).attr('data-export-id', data[0]);
        },
        columnDefs: [
            {
                targets: [0],
                visible: false
            }
        ], 
        dom: 'trip',
        processing: true
    });
    $('#TestService').click(function () {
        var printers = BuildPrinters();
        var label = '';
        for (var x = 0; x < printers.Label.length; x++) {
            label += mkPrinterStr(printers.Label[x].name, printers.Label[x].address);
        };
        $('#TestLabel').html(label);
        var list = '';
        for (var x = 0; x < printers.List.length; x++) {
            list += mkPrinterStr(printers.List[x].name, printers.List[x].address);
        };
        $('#TestList').html(list);
        $('#TestSetup,#testservice_submit,#ServiceResponseFiller').show();
        $('#TestResults,#testservice_saveresults').hide();
        $('#testservice_modal').modal('show');
        testRan = false;
        testTable.top.clear().draw();
        testTable.bottom.clear().draw();
    });

    /* Makes an html representation of a printer for the user to select after it has been appended to its appropriate area. */
    function mkPrinterStr(name, address) {
        return '<div class="row"><div class="col-md-12" data-address="' + address + '"><label>' + name + '</label> <input type="checkbox" class="form-control" /></div></div>';
    };

    /* 
        Makes an object for printers with Properties:
            Label: Array({address: String, name: String})
            List: Array({address: String, name: String})
    */
    function BuildPrinters() {
        var printers = {
            Label: new Array(),
            List: new Array()
        };
        $.each($('#printerconfig_container').children('.row'), function (i, e) {
            var e$ = $(e);
            var printer = {
                address: e$.find('.pAdd').val(),
                name: e$.find('.pName').val()
            };
            if (e$.find('.Label').data('toggles').active) {
                printers.Label.push(printer);
            } else {
                printers.List.push(printer);
            };
        });
        return printers;
    };

    // selects all printers of the specified kind
    $('#SelectLabels,#SelectLists').click(function () {
        var $this = $(this);
        var $which;
        if ($this.attr('ID') == 'SelectLabels') {
            $which = $('#TestLabel');
        } else {
            $which = $('#TestList');
        };
        $which.find('input[type="checkbox"]').prop('checked', $this.prop('checked'));
    });

    // starts the print service test
    $('#testservice_submit').click(function () {
        if (confirm('Ensure that all printers are paused that have been selected.  Open the selected printer queues.  After the printers are paused click OK.')) {
            if (confirm('Click OK to begin testing.  Click Cancel if the printers selected are not paused and you do not wish to physically print all test pages.')) {
                testRan = true;
                var whichType;
                var p = $('#TestPrinters').data('toggles').active;
                var e = $('#TestExport').data('toggles').active;
                if (p && e) {
                    whichType = 3 // print AND export
                } else if (p) {
                    whichType = 1 // just print
                } else {
                    whichType = 2 // just export
                };
                var printers = new Array();
                var printerBool = new Array();
                $.each($('#TestLabel,#TestList').children('.row'), function (i, v) {
                    var v$ = $(v);
                    if (v$.find('input[type="checkbox"]').prop('checked')) {
                        printers.push(v$.find('.col-md-12').data('address'));
                        printerBool.push(v$.parent().attr('id') == 'TestLabel');
                    };
                });
                $('#TestSetup,#testservice_submit').hide();
                $('#TestResults,#testservice_saveresults').show();
                config.server.testPrint($('#OutputType').val(), $('#ApplicationName').val(), $('#AllWSIDs').data('toggles').active, whichType, printers, printerBool).done(function (results) {
                    var errors = results.Errors;
                    for (var k = 0; k < errors.length; k++) {
                        testTable.top.row.add(formatError(errors[k])).draw();
                    };
                    var expected = results.ExpectedResults;
                    for (var k = 0; k < expected.length; k++) {
                        testTable.bottom.row.add(formatResult(expected[k])).draw();
                    };
                });
            };
        };
    });

    /*
        Formats an export result for use in the bottom datatable
    */
    function formatResult(result) {
        if (result.PrintDataReceived == null) { // export
            var file = result.ExportDataReceived.ExportPath;
            var sp = result.ExportDataReceived.SPName;
            var WSID = result.ExportDataReceived.WSID;
            $('#ServiceResponseFiller').hide();
            return [result.ExportID, 'No', '???', WSID, file, result.Printer, '', sp];
        };
    };

    /* Formats an error for the top datatable */
    function formatError(e) {
        return [e.file, e.Type, e.Message, e.area, e.sp];
    };

    // try to delete our test files if the test was run
    $('#testservice_dismiss').click(function () {
        if (testRan) {
            MessageModal('Warning', 'Attempting to delete leftover files that were generated as a result of the test export/print.', undefined, function () {
                config.server.deleteLeftovers().done(function (deleted) {
                    if (!deleted) {
                        $('#Message_label').text('Error');
                        $('#Display_Message').text('Deletion failed.  An error occurred during the delete process.  Check the error log for details.');
                    } else {
                        $('#Message_Modal').modal('hide');
                    };
                });
            });
        };
    });
});

/* Called by SignalR's invocation in GlobalConfigHub when the service invokes its test print complete method. */
config.client.testPrintResult = function (result) {
    var ex = result.CaughtException;
    var exMessage = (ex != null ? ex.Message : '');

    var trClass = '';
    if (result.DataSetIsNothing || result.RecordsInDS < 0) {
        exMessage = 'DataSet = Nothing. ' + (exMessage.trim() == '' ? '' : ('Other errors: ' + exMessage));
        trClass = 'danger';
    } else if (result.RecordsInDS == 0) {
        exMessage = 'No records returned in DataSet.  ' + (exMessage.trim() == '' ? '' : ('Other errors: ' + exMessage));
        trClass = 'warning';
    } else if (result.Success) {
        trClass = 'success';
    } else {
        trClass = 'danger';
    };
    $('#ServiceResponseFiller').hide();
    var success = '';
    if (result.Success) {
        success = 'true';
    } else if (result.RecordsInDS == 0) {
        success = '???';
    } else {
        success = 'false';
    };
    if (result.PrintDataReceived == null) { // export
        var file = result.ExportDataReceived.ExportPath;
        var sp = result.ExportDataReceived.SPName;
        var WSID = result.ExportDataReceived.WSID;
        testTable.bottom.row('[data-export-id="' + result.ExportID + '"]').data([result.ExportID, 'Yes', success, WSID, file, result.Printer, exMessage, sp]).draw();
    } else {
        var file = result.PrintDataReceived.ReportLocation;
        var sp = result.PrintDataReceived.DataSPName;
        var WSID = result.PrintDataReceived.WSID;
        testTable.bottom.row.add([result.ExportID, 'Yes', success, WSID, file, result.Printer, exMessage, sp]).draw();
    };
};