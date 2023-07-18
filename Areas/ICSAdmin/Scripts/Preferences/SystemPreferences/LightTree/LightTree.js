// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var altLightTimer = mkTimer(function ($this) {
        saveAltLight($this);
    }, 200);

    var lightTree = mkTimer(function () {
        getLightTree();
    }, 200);

    $('#LT_Reset').click(function () {
        if (confirm('Click OK to reset all alternate light positions in this zone and carousel to 0.')) {
            var zone = $('#LT_Zone').val(), carousel = $('#LT_Carousel').val();
            preferencesHub.server.resetAltLight(zone, carousel).done(function () {
                $('input.alt-light').val(0);
            });
        };
    });

    $('#LT_Container').css({
        'height': $(window).height() * 0.575,
        'overflow-y' : 'scroll'
    });

    $('#LT_Shelf').on('toggle', function (e, checked) {
        lightTree.startTimer();
    });

    $('#LT_Bin').on('change', function () {
        lightTree.startTimer();
    });

    $('#LT_Carousel, #LT_Zone').on('change', function () {
        var carousel = $('#LT_Carousel'), zone = $('#LT_Zone');
        setNumericInRange(carousel, 1, 8);
        if (carousel.val().trim() != '' && zone.val().trim() != '') {
            $('#LT_Reset').removeAttr('disabled');
        } else {
            $('#LT_Reset').attr('disabled', 'disabled');
        };
        lightTree.startTimer();
    });

    $('#LT_Refresh').click(function () {
        lightTree.startTimer();
    });

    $('#LT_Container').on('click', '.up, .down', function () {
        var $this = $(this);
        incDecAltLight($this, $this.hasClass('up'));
    });

    $('#LT_Container').on('click', '.reset', function () {
        $(this).parent().siblings().find('input.alt-light').val(0).trigger('input');
    });

    $('#LT_Container').on('input', '.alt-light', function () {
        altLightTimer.startTimer($(this));
    });

    $('#LT_Container').on('click', '.update-zone', function () {
        var $this = $(this);
        var light = $this.parent().siblings().find('input.alt-light').val(), zone = $('#LT_Zone').val(), carousel = $('#LT_Carousel').val(),
            shelf = $this.parent().siblings().find('.shelf').val();
        if (confirm('Click OK to update all alternate light positions to ' + light + ' in zone ' + zone + ' and carousel ' + carousel + ' and shelf ' + shelf)) {
            preferencesHub.server.saveAltLightAll(zone, carousel, shelf, light);
        };
    });
});

function saveAltLight($this) {
    var zone = $('#LT_Zone'), carousel = $('#LT_Carousel'), bin = $('#LT_Bin');
    var alt = $this.val(), shelf = $this.parent().siblings().find('.shelf').val();
    if (alt.trim() == '') {
        return false;
    };
    preferencesHub.server.saveAltLight(zone.val(), carousel.val(), bin.val(), shelf, alt);
};

function incDecAltLight($this, increment) {
    var altlight = $this.parent().siblings().find('.alt-light');
    var shelf = $this.parent().siblings().find('.shelf');

    if (altlight.val().trim() == '') {
        altlight.val(0);
    } else if (altlight.val() == 0 && increment) {
        altlight.val(parseInt(shelf.val()) + 1);
    } else {
        altlight.val(parseInt(altlight.val()) + (increment ? 1 : -1));
    };
    altlight.trigger('input');
};

function appendLightTree(shelf, light) {
    var appended = $('<div class="row" style="padding-top:10px;">\
                    <div class="col-md-3">\
                        <input type="text" class="form-control shelf" disabled="disabled" value="' + shelf + '" />\
                    </div>\
                    <div class="col-md-3">\
                        <input type="text" class="form-control alt-light" placeholder="Alternate Light Position" value="' + light + '" />\
                    </div>\
                    <div class="col-md-6">\
                        <button type="button" class="btn btn-primary up" data-toggle="tooltip" data-original-title="Add One" data-placement="left"><span class="glyphicon glyphicon-arrow-up"></span></button> \
                        <button type="button" class="btn btn-primary down" data-toggle="tooltip" data-original-title="Subtract One" data-placement="left"><span class="glyphicon glyphicon-arrow-down"></span></button> \
                        <button type="button" class="btn btn-warning reset" data-toggle="tooltip" data-original-title="Reset to 0" data-placement="left">Reset</button> \
                        <button type="button" class="btn btn-warning update-zone">Update All Shelves in Zone</button>\
                    </div>\
                </div>');
    appended.find('[data-toggle="tooltip"]').tooltip();
    appended.appendTo($('#LT_Container'));
};

function getLightTree() {
    var zone = $('#LT_Zone').val(), carousel = $('#LT_Carousel').val(), bin = $('#LT_Bin').val();

    preferencesHub.server.getLightTrees(zone, carousel, bin, $('#LT_Shelf').data('toggles').active).done(function (trees) {
        $('#LT_Container').html('');
        $.each(trees, function (index, value) {
            appendLightTree(trees[index].shelf, trees[index].light);
        });
    });
};