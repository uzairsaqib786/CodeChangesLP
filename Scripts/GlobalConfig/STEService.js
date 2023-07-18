// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // hub is config

    $('#STEServiceToggle').click(function () {
        var $this = $(this);
        if ($this.text() == "Start STE Service") {
            WaitForService();
            config.server.startSTEService().done(function (success) {
                ServiceStatus('start', success);
            });
        }
        else {
            WaitForService();
            config.server.stopSTEService().done(function (success) {
                ServiceStatus('stop', success);
            });
        }
    });

    $('#STERestartService').click(function () {
        WaitForService();
        config.server.restartService().done(function (success) {
            ServiceStatus('restart', success);
        });
    });


    function ServiceStatus(changeType, success) {
        if (changeType == 'start' || changeType == 'restart') {
            if (success) {
                setOnline();
                alert('Service ' + changeType + ' was successful.');
            } else {
                setOffline();
                alert('Service ' + changeType + ' was unsuccessful.  Please try again or contact Scott Tech for support.');
            };
        } else {
            setOffline();
            if (success) {
                alert('Service stop was successful.');
            } else {
                alert('Service stop encountered an error.  Please try again or contact Scott Tech for support.');
            };
        };
    };

    function WaitForService() {
        $('#STEServiceToggle, #STERestartService').attr('disabled', 'disabled');
        $('#STEStatus').css({ color: 'black' }).html('Pending');
    };

    function setOnline() {
        $('#STEStatus').css({ color: 'green' }).html('Online');
        $('#STEServiceToggle').text("Stop STE Service").removeClass('btn-primary').addClass('btn-danger');
        $('#STERestartService, #STEServiceToggle').removeAttr('disabled');
    };

    function setOffline() {
        $('#STEStatus').css({ color: 'red' }).html('Offline');
        $('#STEServiceToggle').text('Start STE Service').removeClass('btn-danger').addClass('btn-primary').removeAttr('disabled');
        $('#STERestartService').attr('disabled', 'disabled');
    };
});