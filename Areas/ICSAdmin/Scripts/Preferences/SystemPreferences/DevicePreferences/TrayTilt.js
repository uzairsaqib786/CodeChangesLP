// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var trayTimer = mkTimer(function (tilt) {
        saveTrayTilt(tilt.zone, tilt.carousel, tilt.tray, tilt.checked);
    }, 200);
    addResize(function () {
        $('#tray_container').css({
            'max-height': $(window).height() * 0.6,
            'overflow-y': 'scroll'
        });
    });

    $('#SetupTrayTilt').click(function () {
        $('#devpref_modal').modal('hide').one('hidden.bs.modal', function () {
            getTrayTilt();
            $('#tray_modal').modal('show');
        });
    });

    $('#tray_dismiss').click(function () {
        closeTray();
    });

    $('#tray_container').on('toggle', '.tray-tilt.toggles', function (e, checked) {
        var $this = $(this);
        trayTimer.startTimer({
            zone: $this.parent().siblings().find('.tray-zone').html(),
            carousel: $this.parent().siblings().find('.tray-car').html(),
            tray: $this.parent().siblings().find('.tray').html(),
            checked: checked
        });
    });

    $('#tray_all, #tray_alloff').click(function () {
        var value = $(this).attr('id') == 'tray_all';
        var zone = $('.tray-zone:first').html();
        var buttons = $('#tray_all, #tray_alloff');
        buttons.attr('disabled', 'disabled');
        preferencesHub.server.saveTrayToggleAll(zone, value).done(function () {
            $('.tray-container .toggles').each(function (index, tog) {
                $(tog).data('toggles').setValue(value);
            });
            buttons.removeAttr('disabled');
        });
    });
});

function saveTrayTilt(zone, carousel, tray, checked) {
    preferencesHub.server.saveTrayTilt(zone, carousel, tray, checked);
};

function closeTray() {
    $('#tray_modal').modal('hide').one('hidden.bs.modal', function () {
        $('#devpref_modal').modal('show');
    });
};

function appendToTilt(zone, carousel, tray, tilt) {
    $('#tray_container').append('<div class="row tray-container" style="padding-top: 10px; padding-bottom: 10px; border-bottom:2px solid;">\
                            <div class="col-md-3">\
                                <span class="tray-zone">' + zone + '</span>\
                            </div>\
                            <div class="col-md-3">\
                                <span class="tray-car">' + carousel + '</span>\
                            </div>\
                            <div class="col-md-3">\
                                <span class="tray">' + tray + '</span>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="toggles toggle-modern tray-tilt" data-toggle-on="' + tilt + '"></div>\
                            </div>\
                        </div>');
};

function getTrayTilt() {
    var zone = $('#devprefs_zone').val();
    preferencesHub.server.getTrayTilt(zone).done(function (trays) {
        var container = $('#tray_container');
        container.html('');
        $.each(trays, function (index, obj) {
            appendToTilt(zone, obj.carousel, obj.tray, obj.tilt);
        });
        container.find('.toggles').toggles({
            width: 60,
            height: 25
        });
    });
};