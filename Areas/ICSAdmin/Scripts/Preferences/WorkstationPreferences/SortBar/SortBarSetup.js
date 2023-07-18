// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var sbTimer = mkTimer(function () {
        saveSortBar();
    }, 300);

    $('.SortBar').on('input', function () {
        setNumericInRange($(this), 0, 20);
        if (this.value.trim() == '') { this.value = 0; };
        sbTimer.startTimer();
    });
});

function getSortBar() {
    preferencesHub.server.getSortBar().done(function (config) {
        for (var x = 0; x < config.length; x++) {
            if (!$.isNumeric(config[x]) || config[x] < 0 || config[x] > 20) {
                $('#T' + (x + 1)).val(0).attr('name', 0);
            } else {
                $('#T' + (x + 1)).val(config[x]).attr('name', config[x]);
            };
        };
    });
};

function saveSortBar() {
    var settings = new Array();
    var old = new Array();
    var T;
    var same = true;
    for (var x = 1; x < 21; x++) {
        T = $('#T' + x);
        settings.push(T.val());
        old.push(T.attr('name'));
        if (T.val() != T.attr('name')) {
            same = false;
        };
        T.attr('name', T.val());
    };
    if (!same) {
        preferencesHub.server.saveSortBar(settings, old);
    };
};