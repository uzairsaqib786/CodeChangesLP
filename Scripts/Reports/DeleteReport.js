// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#ReportDelete').click(function () {
        $('#deletereport_modal').modal('show');
    });

    $('#DeleteDeleteFile,#DeleteKeepFile').click(function () {
        var keepFile = this.id == 'DeleteKeepFile';
        if (confirm('Click OK to delete the reference to the selected file ' + (keepFile ? 'without deleting the actual file.' : 'and delete the actual file.'))) {
            deleteReport(keepFile);
        };
    });
});