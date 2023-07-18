// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OSHub = $.connection.orderStatusHub;
$(document).ready(function () {
    $('#Save').click(function () {
        OSHub.server.updOSPriority($('#OrderNumber').val(), $('#NewPriority').val()).done(function (mess) {
            if (mess == 'Error') {
                MessageModal("Error", "An error has occured while updating the priority")
            }
            else {
                orderStatTable.draw();
                $('#NewPriority').val("");
            }
        });
    });
});