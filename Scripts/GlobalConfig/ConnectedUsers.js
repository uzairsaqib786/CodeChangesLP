// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

config.client.removeConnected = function (id) {
    $('#' + id).remove();
};

config.client.addConnected = function (name, WSID) {
    $('#ConnectedTable').append('<tr id=' + WSID + '><td>' + name + '</td><td>' + WSID + '</td></tr>');
};