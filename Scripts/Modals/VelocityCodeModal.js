// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// Handles initialization of the velocity code modal and then launches it
var trigger_velocity_modal;

// Posts an alert at the top of the velocity code modal
var postVelocityAlert = function (message) {
    $('#velocity_alerts').html('<div class="alert alert-warning alert-custom" role="alert"><strong>Conflict: </strong>' + message + '</div>');
};

// deletes all velocity code modal alerts
var deleteVelocityAlert = function() {
    $('#velocity_alerts').html('');
};

$(document).ready(function () {
    // stores the sending input box, so when set is called it can put the value back where it should go.
    var velsender;
    var IsDrop;
    var SelVal = "";
    var OldSelVal;

    trigger_velocity_modal = function () {
        var appendstring = '';
        globalHubConn.server.getVelocityCodes().done(function (result) {
            for (var x = 0; x < result.length; x++) {
                appendstring += '<div class="velocity_container" style="padding-top:10px;"><div class="row">' +
                    '<div class="col-md-9" name="value"><input maxlength="50" name="' + result[x] + '" value="' + result[x] + '" class="form-control input-velocity" /></div>' +
                    '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger remove-velocity" data-toggle="tooltip" data-placement="top" title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                    '<div class="col-md-1" name="set"><button type="button" class="btn btn-primary set-velocity" data-dismiss="modal" data-toggle="tooltip" data-placement="top" title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' +
                    '<div class="col-md-1" name="save"><button disabled="disabled" type="button" class="btn btn-primary save-velocity" data-toggle="tooltip" data-placement="top" title="Save Unit of Measure"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
                    '</div></div>';
            };
            $('#velocity_container').html(appendstring);
            $('#velocity_modal').modal('show');
        });
    };


    $('#velocity_modal').on('hidden.bs.modal', function () {
        if (IsDrop) {
            velsender.empty();
            velsender.append('<option value=""></option>')
            globalHubConn.server.getVelocityCodes().done(function (result) {
                for (var x = 0; x < result.length; x++) {
                    velsender.append('<option value="' + result[x] + '">' + result[x] + '</option>')
                };
                if (SelVal != "") {
                    velsender.val(SelVal).trigger('change');
                } else {
                    velsender.val(OldSelVal).trigger('change');
                };
            });
        };
    });

    // Handles launching the modal from either the input text box or the neighboring span/i tag
    $(document.body).on('click', '.velocity-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            //Check if input or dropdown
            if ($this.siblings('input').length > 0) {
                IsDrop = false;
                velsender = $this.siblings('input');
            } else {
                IsDrop = true;
                OldSelVal = $this.siblings('select').val();
                velsender = $this.siblings('select');
            };
        } else {
            velsender = $this;
        }
        trigger_velocity_modal();
    });

    // adds a new velocity code
    $(document.body).on('click', '#velocity_add', function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = '<div class="velocity_container" style="padding-top:10px;"><div class="row"><div class="col-md-9" name="value"><input type="text" class="input-velocity form-control new-velocity" name="" maxlength="50"></div>' +
            '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger remove-velocity new-velocity" data-toggle="tooltip" data-placement="top" title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
            '<div class="col-md-1" name="set"><button disabled="disabled" type="button" class="btn btn-primary set-velocity new-velocity" data-dismiss="modal" data-toggle="tooltip" data-placement="top" title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' +
            '<div class="col-md-1" name="save"><button disabled="disabled" type="button" class="btn btn-primary save-velocity new-velocity" data-toggle="tooltip" data-placement="top" title="Save Unit of Measure"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
            '</div></div>';
        $('#velocity_container').append(appendstring);
        $('input.new-velocity').focus();
    });

    // sets the velocity code sender's value
    $(document.body).on('click', '.set-velocity', function () {
        var value = $(this).parent().siblings('[name="value"]').children().val();
        SelVal = value; //for when dropdown gets reset
        velsender.val(value);
        velsender.trigger('change');
        velsender.trigger('input');
        deleteVelocityAlert();
    });

    //Handles closing the Velocity Code Modal without any selection
    $('#velocity_dismiss').click(function () {
        deleteVelocityAlert();
    })

    // deletes a velocity code
    $(document.body).on('click', '.remove-velocity', function () {
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        var velocity = $this.parent().siblings('[name="value"]').children();
        var container = $this.parent().parent().parent();
        if ($this.hasClass('new-velocity')) {
            container.remove();
            $('#velocity_add').removeAttr('disabled');
        } else {
            var result = confirm('Click OK to delete velocity code ' + velocity.attr('name'));
            if (result) {
                globalHubConn.server.deleteVelocityCode(velocity.attr('name')).done(function (success) {
                    if (success) {
                        container.remove();
                    } else {
                        alert('Delete failed!  Please try again.');
                        $this.removeAttr('disabled');
                    };
                });
            };
        };
    });

    // updates or saves a velocity code
    $(document.body).on('click', '.save-velocity', function () {
        var $this = $(this);
        var remove = $this.parent().siblings('[name="remove"]').children();
        var set = $this.parent().siblings('[name="set"]').children();
        var velocity = $this.parent().siblings('[name="value"]').children();
        var name = velocity.attr('name') !== 'undefined' ? velocity.attr('name') : '';

        globalHubConn.server.saveVelocityCode(velocity.val(), name).done(function (result) {
            if (result) {
                if ($this.hasClass('new-velocity')) {
                    $('.new-velocity').removeClass('new-velocity');
                    $('#velocity_add').removeAttr('disabled');
                };
                $this.attr('disabled', 'disabled');
                set.removeAttr('disabled');
                remove.removeAttr('disabled');
                velocity.attr('name', velocity.val());
            } else {
                alert('Velocity not saved!  Please try again.');
            };
        });
    });

    // enables or disables save functionality for velocity codes
    $(document.body).on('input', '.input-velocity', function () {
        var $this = $(this);
        var save = $this.parent().siblings('[name="save"]').children();
        var remove = $this.parent().siblings('[name="remove"]').children();
        var set = $this.parent().siblings('[name="set"]').children();

        if ($this.val() == '') {
            save.attr('disabled', 'disabled');
            remove.attr('disabled', 'disabled');
            set.attr('disabled', 'disabled');
            return false;
        } else {
            if ($this.attr('name') == $this.val()) {
                save.attr('disabled', 'disabled');
                remove.removeAttr('disabled');
                set.removeAttr('disabled');
            } else {
                $this.addClass('input-edit');

                var conflict = false;
                $('.input-velocity:not(.input-edit)').each(function () {
                    if ($this.val() == $(this).val() || $this.val() == $(this).attr('name')) {
                        save.attr('disabled', 'disabled');
                        conflict = true;
                    }
                });
                if (!conflict) {
                    save.removeAttr('disabled');
                    remove.attr('disabled', 'disabled');
                    set.attr('disabled', 'disabled');
                    deleteVelocityAlert();
                } else {
                    postVelocityAlert('Velocity cannot be saved!  Another velocity code matches the current.  Please save any pending changes before attempting to save this entry.');
                };
                $this.removeClass('input-edit');
            };
        };
    });
});