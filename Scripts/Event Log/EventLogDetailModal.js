// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    function trigger_el_modal (sender) {
        var eID = eventLogTable.row('tr.active').data()[8];
        $('#modalEventID').html(eID);
        $('#modalEventType').html(sender.find('td').eq(4).text());
        $('#modalEventLocation').html('Getting data...');
        $('#modalEventCode').html(sender.find('td').eq(2).text());
        $('#modalTransID').html(sender.find('td').eq(7).text());
        $('#modalUser').html(sender.find('td').eq(3).text());
        $('#modalDate').html(sender.find('td').eq(0).text());
        $('#modalMessage').html('Getting data...');
        $('#modalNotes').html('Getting data...');
        eventLog.server.modalInfo(eID).done(function (results) {
            $('#modalMessage').html(results[0]);
            $('#modalNotes').html(results[1]);
            $('#modalEventLocation').html(results[2]);
            $('#EventLogDetailModal').modal('show');
        });
    };

    // handles selecting a row and triggering the modal
    $('#eventTable tbody').on('click', 'tr', function () {
        var sender = $(this);
        var active = $('tr.active');
        if (!active.is(sender)) {
            active.removeClass('active');
            sender.addClass('active');
        } else {
            trigger_el_modal(sender);
        };
    });

    // handles deleting a specific event from the event log
    $('#modalDelete').click(function () {
        var eID = $('#modalEventID').text();
        var prompt = confirm('Click OK to delete the selected event.');
        if (prompt) {
            eventLog.server.deleteSelected(eID).done(function () {
                $('#EventLogDetailModal').modal('hide');
                eventLogTable.draw();
            });
        };
    });
});