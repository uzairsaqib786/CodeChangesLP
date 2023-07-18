// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#PrinterConfig').click(function () {
        globalHubConn.server.getAllPrintersandWSPrinter().done(function (data) {
            $('#ReportPrintSel, #LabelPrintSel').html("")
            $('#ReportPrintSel, #LabelPrintSel').append("<option>No Printer</option>")
            for (var x = 0; x < data.AllPrinters.length; x++) {
                if (data.AllPrinters[x].label !== 'Able to Print Labels') {
                    $('#ReportPrintSel').append("<option value='"+data.AllPrinters[x].printeradd+"'>" + data.AllPrinters[x].printer + "</option>")
                } else {
                    $('#LabelPrintSel').append("<option value='" + data.AllPrinters[x].printeradd + "'>" + data.AllPrinters[x].printer + "</option>")
                }
            }
            $('#ReportPrintSel').val(data.WSPrinters.ReportPrinter)
            $('#LabelPrintSel').val(data.WSPrinters.LabelPrinter)
            $('#PrintConfig_Modal').modal('show');
        })
       
    })

    $('#PrintConfig_submit').click(function () {
        var ReportPrinter = $('#ReportPrintSel').val();
        var LabelPrinter = $('#LabelPrintSel').val();
        globalHubConn.server.updateWSPrinters(ReportPrinter, LabelPrinter);
    })
})