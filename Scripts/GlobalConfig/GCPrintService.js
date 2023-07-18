// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // hub is config

    $('#ServiceToggle').click(function () {
        var $this = $(this);
        if($this.text() == "Start Print Service")
        {
            WaitForService();
            config.server.startService().done(function (success) {
                ServiceStatus('start', success);
            });
        }
        else {
            WaitForService();
            config.server.stopService().done(function (success) {
                ServiceStatus('stop', success);
            });
        }
    })

    $('#StartService').click(function () {
        WaitForService();
        config.server.startService().done(function (success) {
            ServiceStatus('start', success);
        });
    });

    $('#RestartService').click(function () {
        WaitForService();
        config.server.restartService().done(function (success) {
            ServiceStatus('restart', success);
        });
    });

    $('#StopService').click(function () {
        WaitForService();
        config.server.stopService().done(function (success) {
            ServiceStatus('stop', success);
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
        $('#ServiceToggle, #RestartService').attr('disabled', 'disabled');
        $('#Status').css({ color: 'black' }).html('Pending');
    };

    function setOnline() {
        $('#Status').css({ color: 'green' }).html('Online');
        $('#ServiceToggle').text("Stop Print Service").removeClass('btn-primary').addClass('btn-danger');
        $('#RestartService, #ServiceToggle').removeAttr('disabled');
    };
    
    function setOffline() {
        $('#Status').css({ color: 'red' }).html('Offline');
        $('#ServiceToggle').text('Start Print Service').removeClass('btn-danger').addClass('btn-primary').removeAttr('disabled');
        $('#RestartService').attr('disabled', 'disabled');
    };
});