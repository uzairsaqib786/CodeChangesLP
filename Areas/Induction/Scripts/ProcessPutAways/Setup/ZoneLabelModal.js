// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// setup and launch the zone label modal, so that we can set which zones are available or already allocated to a batch.
function launchZoneLabel(batchID) {
    if (batchID.trim() != '') {
        var container = $('#zone_container');
        container.children().remove();
        batchHub.server.getAvailableZones(batchID).done(function (zones) {
            for (var x = 0; x < zones.length; x++) {
                container.append('<div class="row top-spacer">\
                                    <div class="col-md-2">\
                                        <input type="text" class="form-control" name="zone" readonly="readonly" value="' + zones[x].zone + '" />\
                                    </div>\
                                    <div class="col-md-3">\
                                        <input type="text" class="form-control" name="location" readonly="readonly" value="' + zones[x].location + '" />\
                                    </div>\
                                    <div class="col-md-3">\
                                        <input type="text" class="form-control" name="loctype" readonly="readonly" value="' + zones[x].loctype + '" />\
                                    </div>\
                                    <div class="col-md-2">\
                                        <input type="text" class="form-control" name="stage" readonly="readonly" value="' + zones[x].staging + '" />\
                                    </div>\
                                    <div class="col-md-2">\
                                        <div class="toggles toggle-modern"></div>\
                                    </div>\
                                </div>');
                var t = container.find('.toggles:last');
                if (zones[x].available || zones[x].selected) {
                    t.toggles({
                        width: 60,
                        height: 25,
                        text: {
                            on: 'Yes',
                            off: 'No'
                        }
                    });
                    t.data('toggles').setValue(zones[x].selected);
                } else {
                    t = t.parent();
                    t.children().remove();
                    t.html('<span class="label label-default">Assigned to Another Batch</span>');
                };
            };
            $('#zonelabel_modal').modal('show');
        });
    };
};

$("#zonelabel_modal").on('hidden.bs.modal', function () {
    $('#ScanNextTote').focus();
})