// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// handles initialization and launching of the unit of measure modal
var trigger_uom_modal;

// posts an alert to the unit of measure modal
var postUoMAlert = function (message) {
    $('#uom_alerts').html('<div class="alert alert-warning alert-custom" role="alert"><strong>Conflict:</strong> ' + message + '</div>');
};
// deletes alerts from unit of measure modal
var deleteUoMAlert = function () {
    $('#uom_alerts').html('');
};
// verifies lack of duplicate units of measure
var checkUoMDuplicates = function ($this) {
    deleteUoMAlert();
    $('.input-uom:not(.new-uom)').each(function () {
        if ($this.val() == $(this).val() || $this.val() == $(this).attr('name')) {
            postUoMAlert('New Unit of Measure cannot be added!  There is another Unit of Measure with the same value already.  This may be due to an unsaved Unit of Measure.  Please save any pending changes and try again.');
            return false;
        };
    });
    return true;
};

$(document).ready(function () {
    // contains the sending element (input) so that when set is called the value can be stored in the input
    var uomsender;

    trigger_uom_modal = function () {
        var appendstring = '';
        globalHubConn.server.getUnitsOfMeasure().done(function (result) {
            for (var x = 0; x < result.length; x++) {
                appendstring += '<div class="uom_container" style="padding-top:10px;"><div class="row">' +
                    '<div class="col-md-9" name="value"><input maxlength="50" name="' + result[x] + '" value="' + result[x] + '" class="form-control input-uom" /></div>' +
                    '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger remove-uom" data-toggle="tooltip" data-placement="top" title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                    '<div class="col-md-1" name="set"><button type="button" class="btn btn-primary set-uom" data-toggle="tooltip" data-placement="top" title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' +
                    '<div class="col-md-1" name="save"><button disabled="disabled" type="button" class="btn btn-primary save-uom" data-toggle="tooltip" data-placement="top" title="Save Unit of Measure"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
                    '</div></div>';
            };
            $('#uom_container').html(appendstring);
            $('#uom_modal').modal('show');
        });
    };

    // launches the unit of measure modal
    $(document.body).on('click', '.uom-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        uomsender = $this;
        trigger_uom_modal();
    });

    // handles the modal collapse event
    $(document.body).on('click', '#uom_dismiss', function () {
        $('#uom_container').html('');
        $('#uom_alerts').html('');
        $('#uom_add').removeAttr('disabled');
    });

    // sets a unit of measure to the sender
    $(document.body).on('click', '.set-uom', function () {
        var value = $(this).parent().siblings('[name="value"]').children().val();
        uomsender.val(value);
        uomsender.trigger('input');
        $('#uom_modal').modal('hide');
    });

    // deletes a unit of measure
    $(document.body).on('click', '.remove-uom', function () {
        var $this = $(this);
        if ($this.hasClass('new-uom')) {
            $this.parent().parent().parent().remove();
            $('#uom_add').removeAttr('disabled');
            return true;
        };
        var value = $(this).parent().siblings('[name="value"]').children().attr('name');
        var result = confirm('Click OK to delete ' + value + ' from Units of Measure.');
        if (result) {
            globalHubConn.server.deleteUoM(value).done(function (success) {
                if (success) {
                    $this.parent().parent().parent().remove();
                };
            });
        };
    });

    // adds a new unit of measure
    $(document.body).on('click', '#uom_add', function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = '<div class="uom_container" style="padding-top:10px;"><div class="row"><div class="col-md-9" name="value"><input type="text" class="input-uom form-control new-uom" maxlength="50"></div>' + 
            '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger remove-uom new-uom" data-toggle="tooltip" data-placement="top" title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' + 
            '<div class="col-md-1" name="set"><button disabled="disabled" type="button" class="btn btn-primary set-uom new-uom" data-toggle="tooltip" data-placement="top" title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' + 
            '<div class="col-md-1" name="save"><button disabled="disabled" type="button" class="btn btn-primary save-uom new-uom" data-toggle="tooltip" data-placement="top" title="Save Unit of Measure"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' + 
            '</div></div>';
        $('#uom_container').append(appendstring);
        $('input.new-uom').focus();
    });

    // handles enabling/disabling saving/setting units of measure
    $(document.body).on('input', '.input-uom', function () {
        var $this = $(this);
        var save = $this.parent().siblings('[name="save"]').children();
        var set = $this.parent().siblings('[name="set"]').children();
        var remove = $this.parent().siblings('[name="remove"]').children();

        if ($this.hasClass('new-uom') && checkUoMDuplicates($this) && $this.val() != '' && $('#uom_alerts').html() == '') {
            save.removeAttr('disabled');
            set.attr('disabled', 'disabled');
        } else if (!$this.hasClass('new-uom') && $this.val() != $this.attr('name') && $this.val() != '' && $('#uom_alerts').html() == '') {
            save.removeAttr('disabled');
            set.attr('disabled', 'disabled');
            remove.attr('disabled', 'disabled');
        } else {
            save.attr('disabled', 'disabled');
            set.removeAttr('disabled');
            remove.removeAttr('disabled');
        };
    });

    // handles editing a unit of measure
    $(document.body).on('click', '.save-uom:not(.new-uom)', function () {
        var $this = $(this);
        var input = $(this).parent().siblings('[name="value"]').children();
        var newvalue = input.val();
        var oldvalue = input.attr('name');

        $('.input-uom:not(.new-uom, this)').each(function () {
            if ($this.val() == $(this).val() || $this.val() == $(this).attr('name')) {
                postUoMAlert('Unit of Measure cannot be saved!  There is another Unit of Measure with the same value already.  This may be due to an unsaved Unit of Measure.  Please save any pending changes and try again.');
                return false;
            };
        });
        globalHubConn.server.updateUoM(oldvalue, newvalue).done(function (result) {
            if (result) {
                $this.attr('name', newvalue);
                $this.attr('disabled', 'disabled');
            } else {
                postUoMAlert('New Unit of Measure could not be saved!  Please try again.');
            };
        });
    });

    // handles saving a new unit of measure
    $(document.body).on('click', '.save-uom.new-uom', function () {
        var $this = $(this);
        var set = $(this).parent().siblings('[name="set"]').children();
        var input_box = $(this).parent().siblings('[name="value"]').children();
        var input = input_box.val();

        $('.input-uom:not(.new-uom, this)').each(function () {
            if ($this.val() == $(this).val() || $this.val() == $(this).attr('name')) {
                postUoMAlert('New Unit of Measure cannot be saved!  There is another Unit of Measure with the same value already.  This may be due to an unsaved Unit of Measure.  Please save any pending changes and try again.');
                return false;
            };
        });
        globalHubConn.server.saveUoM(input).done(function (result) {
            if (result) {
                input_box.attr('name', input);
                $this.attr('disabled', 'disabled');
                $('.new-uom').removeClass('new-uom');
                $('#uom_add').removeAttr('disabled');
                set.removeAttr('disabled');
            } else {
                postUoMAlert('New Unit of Measure could not be saved!  Please try again.');
            };
        });
    });
});