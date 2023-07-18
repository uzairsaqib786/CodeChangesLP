// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var transferSettingsHub = $.connection.transferSettingsHub;
$(document).ready(function () {

    //Handles the import all button on the transfer settings tab
    $('#ImportAll').click(function () {
        var conf = confirm("Import All?");
        if (conf) {
            transferSettingsHub.server.importExportAll("Import", $('[name="ImportJobType"]').val()).done(function (result) {
                if (!result) {
                    MessageModal("Error", "An error has occurred importing all records")
                };
            });
        };
    });

    //Handles the export all button on the transfer settings tab
    $('#ExportAll').click(function () {
        var conf = confirm("Export All?")
        if (conf) {
            transferSettingsHub.server.importExportAll("Export", $('[name="ExportJobType"]').val()).done(function (result) {
                if (!result) {
                    MessageModal("Error", "An error has occurred exporting all records")
                };
            });
        };
    });

});