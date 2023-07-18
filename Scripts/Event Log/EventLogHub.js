// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var eventLog = $.connection.eventLogHub;

$(document).ready(function () {
    // delete button handler
    $('#deleteRange').click(function () {
        var sDate = $('#sDateFilterEL').val();
        var eDate = $('#eDateFilterEL').val();
        var message = $('#messageFilterEL').val();
        var eLocation = $('#eLocationEL').val();
        var nStamp = $('#nStampEL').val();
        if (sDate == '' || eDate == '') {
            alert('Both date fields must be filled out!');
        } else if (sDate > eDate) {
            alert('Start date must be before end date!');
        } else if (confirm('Click OK to delete all Event Log entries with specified date, message, event location and name stamp filters.')) {
            eventLog.server.deleteRange(sDate, eDate, message, eLocation, nStamp).done(function (deleted) {
                if (!deleted) MessageModal('Error', 'An error occurred while attempting to delete the selected date range.  Check the error log for details.');
                eventLogTable.draw();
            });
        };
    });

    // handles printing the event log
    $(document.body).on('click', '#printRange, #printSelected, #modalPrint', function () {
        var sender = $(this);
        var eID;
        var nStamp = $('#nStampEL').val();
        var message = $('#messageFilterEL').val();
        var eLocation = $('#eLocationEL').val();
        var sDate = $('#sDateFilterEL').val();
        var eDate = $('#eDateFilterEL').val();

        if (sender.text() == 'Print Selected' || sender.text() == 'Print Event') {
            if ($('tr.active').length > 0) {
                eID = eventLogTable.row('tr.active').data()[8];
            } else {
                alert('No item selected to print!');
                return false;
            };
        } else {
            eID = -1;
        };
        title = 'Event Log Report';
        getLLPreviewOrPrint('/EventLog/printELReport', {
            sDate: sDate, 
            eDate: eDate, 
            eID: eID, 
            message: message, 
            eLocation: eLocation, 
            nStamp: nStamp
        }, true,'report', title);
    });
});