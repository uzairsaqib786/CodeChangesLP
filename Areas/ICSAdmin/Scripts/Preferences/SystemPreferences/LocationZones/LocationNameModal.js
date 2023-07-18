// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var sender;

    $(document.body).on('click', '.locname-modal', function () {
        var $this = $(this);
        sender = $this.hasClass('glyphicon') ? $this.siblings('input.locname') : $this;
        getLocationNames();
        $('#locname_modal').modal('show');
    });

    $(document.body).on('click', '.set-locname', function () {
        setLocationName($(this).parent().siblings().find('input.locname').val());
    });
    $('#locname_setnone').on('click', function () {
        setLocationName('');
    });

    $(document.body).on('click', '.remove-locname', function () {
        var $this = $(this);
        var container = $this.parents('.locname-container');
        var input = $this.parent().siblings().find('input.locname').attr('name');
        if (input == '' || input == null || input == 'undefined') {
            $('#addNewLocationName').removeAttr('disabled');
            container.remove();
        } else if (confirm('Click OK to delete Location Name ' + input)) {
            preferencesHub.server.deleteLocationName(input).done(function () {
                container.remove();
            });
        };
    });

    $('#addNewLocationName').click(function () {
        $(this).attr('disabled', 'disabled');
        var newLN = addLocationName('');
        newLN.find('.set-locname').attr('disabled', 'disabled');
    });

    $(document.body).on('click', '.save-locname', function () {
        saveLocationName($(this).parents('.locname-container').find('input.locname'));
    });

    $(document.body).on('input', '.locname', function () {
        var $this = $(this);
        var save = $this.parents('.locname-container').find('.save-locname');
        var set = $this.parents('.locname-container').find('.set-locname');
        console.log(save)
        $this.addClass('edit');
        var desiredVal = $this.val().trim().toLowerCase();
        var enable = true;
        var val;
        $('input.locname:not(.edit)').each(function (index, elem) {
            val = $(this);
            if (val.val().trim().toLowerCase() == desiredVal || val.attr('name').trim().toLowerCase() == desiredVal) {
                enable = false;
            };
        });
        $this.removeClass('edit');
        if (enable && $this.val() != $this.attr('name')) {
            set.attr('disabled', 'disabled');
            save.removeAttr('disabled');
        } else {
            set.removeAttr('disabled');
            save.attr('disabled', 'disabled');
        };
    });

    function setLocationName(value) {
        sender.val(value).trigger('input');
        $('#locname_modal').modal('hide');
    };
});

function getLocationNames() {
    preferencesHub.server.getLocationNames().done(function (locnames) {
        $('#locname_container').html('');
        $.each(locnames, function (index, elem) {
            addLocationName(elem);
        });
    });
};

function saveLocationName(location) {
    var oldValue = location.attr('name');
    var newValue = location.val().trim();
    preferencesHub.server.saveLocationName(oldValue, newValue).done(function () {
        if (oldValue == '') {
            $('#addNewLocationName').removeAttr('disabled');
        };
        location.parents('.locname-container').find('.set-locname').removeAttr('disabled');
        location.parents('.locname-container').find('.save-locname').attr('disabled', 'disabled');
        location.attr('name', newValue);
    });
};

function addLocationName(name) {
    var newLN = $('<div class="row locname-container" style="padding-top:5px;">\
                        <div class="col-md-9">\
                            <input type="text" class="form-control locname" placeholder="Location Name" name="' + name + '" value="' + name + '" />\
                        </div>\
                        <div class="col-md-1">\
                            <button type="button" class="btn btn-danger remove-locname pull-right"><span class="glyphicon glyphicon-remove"></span></button>\
                        </div>\
                        <div class="col-md-1">\
                            <button type="button" class="btn btn-primary set-locname pull-right"><span class="glyphicon glyphicon-edit"></span></button>\
                        </div>\
                        <div class="col-md-1">\
                            <button type="button" class="btn btn-primary save-locname pull-right" disabled="disabled"><span class="glyphicon glyphicon-floppy-disk"></span></button>\
                        </div>\
                    </div>')
    newLN.appendTo($('#locname_container'));

    return newLN;
};