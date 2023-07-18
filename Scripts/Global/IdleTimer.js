// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var inactiveTime = new Date().getTime();
var popTime = $('#idlePopTime').val();
var shutTime = $('#idleShutTime').val()
var isPopActive = false;

$(document).ready(function () {
    //Code to handle Idleing
    $(document.body).on('click , dblclick , change , toggle , focus , keydown', function (e) {
        inactiveTime = new Date().getTime();
    });

    setInterval(function () {
        if (popTime != 0) {
            var currentTime = new Date().getTime();
            if (currentTime - inactiveTime >= popTime && !isPopActive) {
                console.log("Idle Pop Up should happen here");
                isPopActive = true;
            };
            if (currentTime - inactiveTime >= shutTime && isPopActive) {
                console.log("User is Signed out");
            };
        };
    }, 3000);
});