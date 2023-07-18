// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // hub is config

    $('#CCSIFToggle').click(function () {
        var $this = $(this);
        if($this.text() == "Start CCSIF Service")
        {
            WaitForService();
            config.server.startCCSIF().done(function (success) {
                ServiceStatus('start', success);
            });
        }
        else {
            WaitForService();
            config.server.stopCCSIF().done(function (success) {
                ServiceStatus('stop', success);
            });
        }
    })

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
        $('#CCSIFToggle').attr('disabled', 'disabled');
        $('#CCSIFStatus').css({ color: 'black' }).html('Pending');
    };

    function setOnline() {
        $('#CCSIFStatus').css({ color: 'green' }).html('Online');
        $('#CCSIFToggle').text("Stop CCSIF Service").removeClass('btn-primary').addClass('btn-danger');
        $('#CCSIFToggle').removeAttr('disabled');
    };
    
    function setOffline() {
        $('#CCSIFStatus').css({ color: 'red' }).html('Offline');
        $('#CCSIFToggle').text('Start CCSIF Service').removeClass('btn-danger').addClass('btn-primary').removeAttr('disabled');
    };
});