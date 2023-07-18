// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var bzTimer = mkTimer(function ($this) {
        saveBulkZone($this.data('name'), $this.data('toggles').active);
    }, 200);
    $('#zones').on('toggle', '.toggles',function(e, checked){
        bzTimer.startTimer($(this));
    });
});

function getBulkZones() {
    var Zones = $('#zones');
    preferencesHub.server.selAllBulkZones().done(function (BulkZones) {
        Zones.html('');
        var entries;
        $.each(BulkZones, function (index, element) {
            entries = $('<div class="row" style="padding-top:10px;">\
                            <div class="col-md-5">\
                               <input disabled  type="text" class="form-control" value=" ' + element.zone + '" /> </div>\
                        <div class="col-md-5">\
                            <input disabled type="text" class="form-control" value=" ' + element.locname + '" /> </div>\
                        <div class="col-md-2">\
                            <div class="toggles toggle-modern Bulk_Zone" data-toggle-ontext="Yes" data-toggle-offtext="No" data-toggle-on="' + element.isbulk + '" data-name="' + element.zone + '"></div>\
                        </div> </div>');
            entries.find('.toggles').toggles({
                width: 60,
                height: 25
            });
            entries.appendTo(Zones);
        });
    });
};

function saveBulkZone(zone, ident) {
    preferencesHub.server.addDeleteBulkZones(zone, ident).done(function (success) {
        if (!success) {
            alert("Operation Failed. There was an error with the desired operation.");
        };
    });
};