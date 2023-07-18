// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#TestPrintList').click(function () {
        globalHubConn.server.testPrintFunc("list").done(function (data) {
            alert(data);
        });

    });


    $('#TestPrintLabel').click(function () {
        globalHubConn.server.testPrintFunc("label").done(function (data) {
            alert(data);
        });

    });

});