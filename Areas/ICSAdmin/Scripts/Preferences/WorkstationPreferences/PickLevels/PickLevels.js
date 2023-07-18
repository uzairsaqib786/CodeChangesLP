// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#addNewPickLevel').click(function () {
        $(this).attr('disabled', 'disabled');
        appendPickLevel({ 'picklevel': '(new)', 'startshelf': 1, 'endshelf': 99 }, $('#PickContainer'), true, 1);
    });

    $('#PickContainer').on('click', '.shelfDelete', function () {
        var $this = $(this);
        var container = $this.parents('.row.PickContainer');
        var pick = $this.parent().siblings().find('.pick').val();
        if (pick == '(new)') {
            container.remove();
            $('#addNewPickLevel').removeAttr('disabled');
        } else if (confirm('Click OK to delete Pick Level #' + pick)) {
            preferencesHub.server.deletePickLevel(pick).done(function (deleted) {
                if (deleted) {
                    container.remove();
                } else {
                    alert('An error occurred during the deletion.  Please try again.');
                };
            });
        };
    });

    $('#PickContainer').on('input', '.pick-start, .pick-end', function () {
        var $this = $(this);
        setNumericInRange($this, 1, 99);
        var other;
        if ($this.hasClass('pick-start')) {
            other = $this.parent().siblings().find('.pick-end');
        } else {
            other = $this.parent().siblings().find('.pick-start');
        };
        var save = $this.parent().siblings().find('.shelfSave');
        if ($this.attr('name') != $this.val() || other.val() != other.attr('name')) {
            save.removeAttr('disabled');
        } else {
            save.attr('disabled', 'disabled');
        };
    });

    $('#PickContainer').on('click', '.shelfSave', function () {
        savePickLevel($(this));
       
    });

    $(document.body).on('input', '.pick-start, .pick-end', function () {
        setNumeric($(this), 1, 99)
    })
});


function savePickLevel($save) {
    var pick = $save.parent().siblings().find('input.pick');
    var start = pick.parent().siblings().find('input.pick-start');
    var end = pick.parent().siblings().find('input.pick-end');
    preferencesHub.server.addUpdatePickLevel(pick.val(), start.val(), end.val()).done(function (pickLevel) {
        start.attr('name', start.val());
        end.attr('name', end.val());
        $save.attr('disabled', 'disabled');
        if (pick.hasClass('new-pick')) {
            pick.removeClass('new-pick');
            $('#addNewPickLevel').removeAttr('disabled');
            pick.val(pickLevel).attr('name', pickLevel);
        };
    });
};

function appendPickLevel(element, container, newEntry, last) {
    var entry = $('<div class="row PickContainer" style="padding-top:10px;">\
        <div class="col-md-3">\
            <input disabled  type="text" class="form-control pick' + (newEntry ? ' new-pick' : '') + '" value="' + element.picklevel + '" name="' + element.picklevel + '" /> </div>\
        <div class="col-md-3">\
            <input type="text" class="form-control pick-start" value="' + element.startshelf + '" name="' + element.startshelf + '"  /> </div>\
        <div class="col-md-3">\
            <input type="text" class="form-control pick-end" value="' + element.endshelf + '" name="' + element.endshelf + '" /> </div>\
        <div class="col-md-3">\
	        <button data-toggle="tooltip" data-placement="top" data-original-title="Save" ' + (newEntry ? '' : 'disabled="disabled"') + ' class="btn btn-primary shelfSave"> <span class="glyphicon glyphicon-floppy-disk"></span></button>\
            ' + (last != 0 ? '<button data-toggle="tooltip" data-placement="top" data-original-title="Remove" class="btn btn-danger shelfDelete">\
            <span class="glyphicon glyphicon-remove"></span></button>' : '\
            <button data-toggle="tooltip" data-placement="top" data-original-title="Last entry cannot be deleted!" type="button" class="btn btn-warning">\
            <span class="glyphicon glyphicon-warning-sign"></span></button>') + '\
        </div> </div>');
    entry.find('[data-toggle="tooltip"]').tooltip();
    entry.appendTo(container);
};

function getPickLevels() {
    var container = $('#PickContainer');
    $('#addNewPickLevel').removeAttr('disabled');
    preferencesHub.server.selPickLevels().done(function (pickLevels) {
        container.html('');
        $.each(pickLevels, function (index, element) {
            appendPickLevel(element, container, false, index);
        });
    });
};