// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var omMenuHub = $.connection.oMMenuHub;

$(document).ready(function () {
    //refreshes the transaction count information
    $('#OMRefreshStat').click(function () {
        omMenuHub.server.getOMCountData().done(function (data) {
            $('#OMOpenPicks').val(data.OpenPicks);
            $('#OMCompPicks').val(data.CompPicks);
            $('#OMOpenPuts').val(data.OpenPuts);
            $('#OMCompPuts').val(data.CompPuts);
            $('#OMOpenCounts').val(data.OpenCounts);
            $('#OMCompCounts').val(data.CompCounts);
            $('#OMCompAdjust').val(data.CompAdjust);
            $('#OMCompLocChange').val(data.CompLocChange);
            $('#OMReprocCount').val(data.ReprocCount);
        });
    });
});