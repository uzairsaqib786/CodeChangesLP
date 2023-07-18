// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

function switchToOS(order, transType) {
    bmHub.server.getDetailView(order, transType).done(function (data) {
        $('#osOrderNumber').val(order);
        $('#osTransType').val(transType);
        osAltTable.clear().rows.add(data).draw();
        $('#mainDiv').hide();
        $('#tableDiv').show();
    });
};