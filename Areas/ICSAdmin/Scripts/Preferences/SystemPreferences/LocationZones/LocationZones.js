// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var lzTimer = mkTimer(function (zone) {
        saveLocationZone(zone);
    }, 200);

    // handles hiding accordions
    $(document.body).on('hide.bs.collapse', '.accordion-toggle', function (e) {
        var span = $(this).siblings('.panel-heading').children().children().children();
        span.removeClass('accordion-caret-up');
        span.addClass('accordion-caret-down');
        e.stopPropagation();
    });

    // handles expanding accordions
    $(document.body).on('show.bs.collapse', '.accordion-toggle', function (e) {
        var span = $(this).siblings('.panel-heading').children().children().children();
        span.removeClass('accordion-caret-down');
        span.addClass('accordion-caret-up');
        e.stopPropagation();
    });

    $(document.body).on('click', '.remove-lz', function () {
        var $this = $(this);
        var zone = $this.attr('name');
        var container = $('#zone-' + zone).parent().parent();
        if (confirm('Click OK to delete Zone: ' + zone)) {
            preferencesHub.server.deleteLocationZone(zone).done(function (deleted) {
                if (deleted) {
                    $('#locationZoneList [name="' + zone + '"]').parent().remove()
                    container.remove();
                } else {
                    alert('Location Zone ' + zone + ' cannot be deleted because there are allocated quantities in an Inventory Map location matching the zone');
                };
            });
        };
    });

    $('#addNewLocationZone').click(function () {
        $(this).attr('disabled', 'disabled');
        $('#newLocationZone, #submitNewLZ, #removeNewLZ').show();
    });

    $('#newLocationZone').on('input', function () {
        var value = this.value.trim().toLowerCase();
        var enable = true;
        $('input[name="zone"]').each(function (index, elem) {
            if (this.value.trim().toLowerCase() == value) {
                enable = false;
            };
        });
        if (enable && value != '') {
            deleteLZAlert($('#LocationZonesAccordion').parents('.panel-body'));
            $('#submitNewLZ').removeAttr('disabled');
        } else {
            if (value != '') {
                postLZAlert($('#LocationZonesAccordion').parents('.panel-body'), 'Zone would be a duplicate and cannot be added.');
            } else {
                deleteLZAlert($('#LocationZonesAccordion').parents('.panel-body'));
            };
            $('#submitNewLZ').attr('disabled', 'disabled');
        };
    });

    $('#submitNewLZ').click(function () {
        saveNewLocationZone($('#newLocationZone').val());
        $('#submitNewLZ, #newLocationZone, #removeNewLZ').hide();
        $('#addNewLocationZone').removeAttr('disabled');
    });

    $('#removeNewLZ').click(function () {
        $('#submitNewLZ, #newLocationZone, #removeNewLZ').hide();
        $('#addNewLocationZone').removeAttr('disabled');
    });

    $('#LocationZonesAccordion').on('input', 'input', function () {
        lzTimer.startTimer($(this).parents('.panel-body.accordion-toggle.in'));
    });
    $('#LocationZonesAccordion').on('change', 'select', function () {
        lzTimer.startTimer($(this).parents('.panel-body.accordion-toggle.in'));
    });
    $('#LocationZonesAccordion').on('toggle', '.toggles-zone', function () {
        var row = $(this).parents('.panel-body.accordion-toggle.in');
        var allocable = row.find('[data-column="Allocable"]');
        var kanban = row.find('[data-column="Kanban Zone"]');
        if (allocable.data('toggles').active && kanban.data('toggles').active) {
            PromptModal('Kanban Zone & Allow Pick Allocation Conflict', [
                {
                    'text-only': 'Kanban Zone and Allow Pick Allocation may not both be set.  Choose which to set to ON.'
                }
            ],
            [
                {
                    classes: 'btn-primary',
                    onclick: function () {
                        kanban.data('toggles').setValue(false)
                    },
                    dismiss: true,
                    text: 'Set "Allow Pick Allocation" ON'
                },
                {
                    classes: 'btn-primary',
                    onclick: function () {
                        allocable.data('toggles').setValue(false)
                    },
                    dismiss: true,
                    text: 'Set "Kanban Zone" ON'
                },
                {
                    classes: 'btn-primary',
                    onclick: function () {
                        allocable.data('toggles').setValue(false);
                        kanban.data('toggles').setValue(false);
                    },
                    dismiss: true,
                    text: 'Set Both to OFF'
                }
            ],
            null,
            function () {
                if (allocable.data('toggles').active && kanban.data('toggles').active) {
                    allocable.data('toggles').setValue(false);
                };
                lzTimer.startTimer(row);
            },
            {
                'width': 600
            });
        } else {
            lzTimer.startTimer(row);
        };
    });

    $('#LocationZonesAccordion').on('toggle', '[name="carousel"],[name="CartonFlow"],[name="IncludeCF"]', function (e, checked) {
        var $this = $(this);
        var active = $this.attr('name');
        var parent = $this.parents('.panel-body.accordion-toggle.in');
        var zone = parent.find('input[name="zone"]').val();
        var carousel = parent.find('[name="carousel"]').data('toggles');
        var cf = parent.find('[name="CartonFlow"]').data('toggles');
        var includeCf = parent.find('[name="IncludeCF"]').data('toggles');
        if (active == 'carousel' && checked) {
            cf.setValue(false);
            includeCf.setValue(false);
            alterParentZones(true, zone);
        }
        else if (active == 'carousel' && !checked) {
            alterParentZones(false, zone);
        } else if (active == 'CartonFlow' && checked) {
            alterParentZones(false, zone);
            carousel.setValue(false);
        } else if (active == 'IncludeCF' && checked) {
            alterParentZones(false, zone);
            cf.setValue(true);
            carousel.setValue(false);
        };
    });

    $('#LocationZonesAccordion').on('toggle', '[name="IncludeCF"]', function (e, checked) {
        if (checked) {
            $(this).parents('.panel-body.accordion-toggle.in').find('[name="ParentZone"]').removeAttr('disabled');
        } else {
            $(this).parents('.panel-body.accordion-toggle.in').find('[name="ParentZone"]').attr('disabled', 'disabled');
        }
    });

    $('#LocationZonesAccordion').on('focusout', '[name="Sequence"]', function () {
       // if (this.value.trim() == '') { this.value = 0; };
    });
    $('#LocationZonesAccordion').on('input', '[name="Sequence"]', function () {
       // setNumericInRange($(this), 0, null);
    });
});

function alterParentZones(add, item) {
    if (add) {
        $('[name="ParentZone"]').append('<option value="' + item + '">' + item + '</option>');
    } else {
        $('[name="ParentZone"] option[value="' + item + '"]').remove();
    };
};

function getLocationZones() {
    $('#LocationZonesAccordion').html('');
    preferencesHub.server.getLocationZones().done(function (loczones) {
        var zoneParents = new Array();
        var zoneParentSettings = new Array();
        var lzList = $('#locationZoneList');
        lzList.html('');
        $.each(loczones, function (index, list) {
            if (list['Carousel']) {
                zoneParents.push(list['Zone']);
            };
            lzList.append('<li><a name="' + list['Zone'] + '" class="remove-lz">Delete: ' + list['Zone'] + '</a></li>');
            zoneParentSettings.push([list['Parent Zone'], list['Zone']]);
            appendLocationZone(list);
        });

        alterParentZones(true, '');
        $.each(zoneParents, function (index, element) {
            alterParentZones(true, element);
        });

        for (var x = 0; x < zoneParentSettings.length; x++) {
            $('#parentzone-' + zoneParentSettings[x][1]).val(zoneParentSettings[x][0]);
        };
    });
};

function appendLocationZone(zoneDef) {
    // zone, carousel, cf, replenish, stage, includeCF, parent, includeBatch, CCS, Dyn, Allocable, name, l1, l2, l3, l4, seq
    var lz = $('#LocationZonesAccordion');
    var locnames = $('#Locations').html();

    var appended = $('<div class="row"><div class="col-md-12"><div class="panel panel-info" style="margin-bottom:5px;">\
                    <div class="panel-heading">\
                        <a data-toggle="collapse" data-target="#zone-' + zoneDef['Zone'] + '"><h3 class="panel-title">Zone: ' + zoneDef['Zone'] + ' <span class="accordion-caret-down"></span></h3></a>\
                    </div>\
                    <div class="panel-body collapse accordion-toggle" id="zone-' + zoneDef['Zone'] + '">\
                        <div class="row" style="padding-bottom:5px;">\
                            <div class="col-md-12">\
                                \
                            </div>\
                        </div>\
                        <div class="row">\
                            <div class="col-md-3">\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Carousel</label>\
                                        <div name="carousel" class="toggles-zone toggle-modern pull-right" data-column="Carousel" data-toggle-on="' + zoneDef['Carousel'] + '"></div>\
                                    </div>\
                                </div>\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Carton Flow</label>\
                                        <div name="CartonFlow" class="toggles-zone toggle-modern pull-right" data-column="Carton Flow" data-toggle-on="' + zoneDef['Carton Flow'] + '"></div>\
                                    </div>\
                                </div>\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Replenishment Source</label>\
                                        <div name="Replenishment" class="toggles-zone toggle-modern pull-right" data-column="Replenishment Zone" data-toggle-on="' + zoneDef['Replenishment Zone'] + '"></div>\
                                    </div>\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="row">\
                                    <div class="col-md-12">\
                                         <label>Staging Zone</label>\
                                        <div name="Staging" class="toggles-zone toggle-modern pull-right" data-column="Staging Zone" data-toggle-on="' + zoneDef['Staging Zone'] + '"></div>\
                                    </div>\
                                </div>\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Include Zone in Auto Batch</label>\
                                        <div name="IncludeBatch" class="toggles-zone toggle-modern pull-right" data-column="Include In Auto Batch" data-toggle-on="' + zoneDef['Include In Auto Batch'] + '"></div>\
                                    </div>\
                                </div>\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Include CF Carousel Pick</label>\
                                        <div name="IncludeCF" class="toggles-zone toggle-modern pull-right" data-column="Include CF Carousel Pick" data-toggle-on="' + zoneDef['Include CF Carousel Pick'] + '"></div>\
                                    </div>\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>CCS Auto Induct</label>\
                                        <div name="CCS" class="toggles-zone toggle-modern pull-right" data-column="Include In Transactions" data-toggle-on="' + zoneDef['Include In Transactions'] + '"></div>\
                                    </div>\
                                </div>\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Dynamic Warehouse</label>\
                                        <div name="Dynamic" class="toggles-zone toggle-modern pull-right" data-column="Dynamic Warehouse" data-toggle-on="' + zoneDef['Dynamic Warehouse'] + '"></div>\
                                    </div>\
                                </div>\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Allow Pick Allocation</label>\
                                        <div name="Allocable" class="toggles-zone toggle-modern pull-right" data-column="Allocable" data-toggle-on="' + zoneDef['Allocable'] + '"></div>\
                                    </div>\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                               <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Kanban Zone</label>\
                                        <div name="Kanban" class="toggles-zone toggle-modern pull-right" data-column="Kanban Zone" data-toggle-on="' + zoneDef['Kanban Zone'] + '"></div>\
                                    </div>\
                                </div>\
                                <div class="row">\
                                    <div class="col-md-12">\
                                        <label>Kanban Replenishment Zone</label>\
                                        <div name="KanbanReplen" class="toggles-zone toggle-modern pull-right" data-column="Kanban Replenishment Source Zone" data-toggle-on="' + zoneDef['Kanban Replenishment Zone'] + '"></div>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                        <div class="row">\
                            <div class="col-md-3" >\
                                <div class="input-group">\
                                          <span class="input-group-addon">Zone</span>\
                                          <input name="zone" type="text" class="form-control input-sm" data-column="Zone" value="' + zoneDef['Zone'] + '" maxlength="2">\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="form-group has-feedback input-group" style="margin-bottom:0px;">\
                                    <span class="input-group-addon">Location Name</span>\
                                    <input type="text" readonly="readonly" class="form-control locname-modal modal-launch-style input-sm" data-column="Location Name"  name="LocName" value="' + zoneDef['Location Name'] + '">\
                                    <i class="glyphicon glyphicon-resize-full form-control-feedback locname-modal modal-launch-style" style="top:0px;z-index:2;"></i>\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="input-group">\
                                          <span class="input-group-addon">Parent Zone</span>\
                                          <select id="parentzone-' + zoneDef['Zone'] + '" data-column="Parent Zone" name="ParentZone" class="form-control input-sm"></select>\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="input-group">\
                                          <span class="input-group-addon">Sequence</span>\
                                          <input name="Sequence" type="text" data-column="Sequence" value="' + (zoneDef['Sequence'] == null ? '' : zoneDef['Sequence']) + '" class="form-control input-sm" oninput="setNumericInRange($(this), SqlLimits.numerics.smallint,\'\',true)">\
                                </div>\
                            </div>\
                        </div>\
                        <div class="row" style="margin-top:5px;">\
                            <div class="col-md-3">\
                                <div class="input-group">\
                                          <span class="input-group-addon">Label1</span>\
                                          <input name="Label1" data-column="Label1" value="' + zoneDef['Label1'] + '" type="text" class="form-control input-sm" maxlength="10">\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="input-group">\
                                          <span class="input-group-addon">Label2</span>\
                                          <input name="Label2" data-column="Label2" value="' + zoneDef['Label2'] + '" type="text" class="form-control input-sm" maxlength="10">\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="input-group">\
                                          <span class="input-group-addon">Label3</span>\
                                          <input name="Label3" data-column="Label3" value="' + zoneDef['Label3'] + '" type="text" class="form-control input-sm" maxlength="10">\
                                </div>\
                            </div>\
                            <div class="col-md-3">\
                                <div class="input-group">\
                                          <span class="input-group-addon">Label4</span>\
                                          <input name="Label4" data-column="Label4" value="' + zoneDef['Label4'] + '" type="text" class="form-control input-sm" maxlength="10">\
                                </div>\
                            </div>\
                        </div>\
                    </div>\
                </div></div>\
            </div>');
    appended.find('.toggles-zone').toggles({
        width: 50,
        height: 20
    });

    appended.appendTo(lz);
};

function saveLocationZone(zone) {
    var oldZone = zone.attr('id').split('-')[1].trim();
    var newZone = zone.find('[name="zone"]').val().trim();
    var seq = zone.find('[name="Sequence"]').val().trim();
    if (newZone == '') {
        postLZAlert(zone, 'Zone may not be left blank.  Zone will not be saved until this is fixed.');
        return false;
    } else if (seq < 0 || !$.isNumeric(seq) || seq == '') {
        if (seq == '') {
            deleteLZAlert(zone);
        } else {
            postLZAlert(zone, 'Sequence must be an integer greater than or equal to 0.  Zone will not be saved until this is fixed.');
            return false;
        };
    } else {
        deleteLZAlert(zone);
    };
    var settings = {};
    var columns = ['Zone', 'Carousel', 'Carton Flow', 'Replenishment Zone', 'Staging Zone', 'Include CF Carousel Pick', 'Include In Auto Batch', 'Include In Transactions', 'Dynamic Warehouse', 'Allocable', 'Location Name', 'Label1', 'Label2', 'Label3', 'Label4', 'Sequence', 'Parent Zone', 'Kanban Zone', 'Kanban Replenishment Source Zone'];
    for (var x = 0; x < columns.length; x++) {
        var z = zone.find('[data-column="' + columns[x] + '"]');
        if (z.hasClass('toggles-zone'))
            settings[columns[x]] = z.data('toggles').active;
        else
            settings[columns[x]] = z.val();
    };
    var check = oldZone.toLowerCase() != newZone.toLowerCase();
    if (check) {
        var save = true;
        zone.find('input[name="zone"]').addClass('edit');
        $('input[name="zone"]:not(.edit)').each(function (index, element) {
            if (this.value.trim().toLowerCase() == newZone.toLowerCase()) {
                save = false;
            };
        });
        if (!save) {
            postLZAlert(zone, 'Zone is currently set to be a duplicate.  Zone will not be saved until this is fixed.');
            return false;
        } else {
            deleteLZAlert(zone);
        };
        zone.find('input[name="zone"]').removeClass('edit');
    } else {
        deleteLZAlert(zone);
    };
    preferencesHub.server.saveLocationZone(oldZone, settings).done(function () {
        if (check) {
            zone.attr('id', 'zone-' + newZone);
            zone.siblings('.panel-heading').find('a')
                .attr('data-target', '#zone-' + newZone)
                .html('<h3 class="panel-title">Zone: ' + newZone + ' <span class="accordion-caret-down"></span></h3>');
        };
    });
};

function postLZAlert(zone, message) {
    deleteLZAlert(zone);
    zone.prepend('<div class="alert alert-warning alert-custom" role="alert">' + message + '</div>');
};

function deleteLZAlert(zone) {
    zone.find('.alert.alert-warning.alert-custom').remove();
};

function saveNewLocationZone(zone) {
    preferencesHub.server.saveNewLocationZone(zone).done(function () {
        getLocationZones();
    });
};