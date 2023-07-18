// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var cMPreferencesHub = $.connection.cMPreferencesHub;
/*
    Sets up and then launches the carrier modal
*/
var trigger_carriers_modal;

/*
    Post an alert to the carrier modal with the messsage passed as a parameter
*/
var postCarriersAlert = function (message) {
    $('#CarriersAlerts').html('<div class="alert alert-warning alert-custom" role="alert"><strong>Conflict: </strong>' + message + '</div>');
};

// deletes all alerts from the carrier modal
var deleteCarriersAlert = function () {
    $('#CarriersAlerts').html('');
};

$(document).ready(function () {
    trigger_carriers_modal = function (sender) {
        if ($('.activeRow').length > 0) {
            $('#CarriersSender').val('.activeRow input[name="Carriers"]');
        } else {
            if (sender.hasClass('glyphicon')) {
                $('#CarriersSender').val('#' + sender.siblings('input').attr('id'));
            } else {
                $('#CarriersSender').val('#' + sender.attr('id'));
            };
        };
        var appendstring = '';
        //appends every carrier to the modal
        cMPreferencesHub.server.selectCarriers().done(function (result) {
            for (var x = 0; x < result.length; x++) {
                appendstring += '<div class="row" style="padding-top:10px;">' +
                    '<div class="col-md-10" name="value"><input maxlength="50" name="' + result[x] + '" value="' + result[x] + '" class="form-control input-carrier" /></div>' +
                    '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger remove-carrier" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                    '<div class="col-md-1" name="save"><button disabled="disabled" type="button" class="btn btn-primary save-carrier" data-toggle="tooltip" data-placement="top" data-original-title="Save Carrier"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
                    '</div>';
            };
            $('#CarriersContainer').html(appendstring);
            $('#CarriersModal').modal('show');
        });
    };

    // calls modal launching function on click
    $(document.body).on('click', '#Carriers', function () {
        trigger_carriers_modal($(this));
    });

    // adds a new carrier entry
    $(document.body).on('click', '#CarriersAdd', function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = '<div class="carrier_container" style="padding-top:10px;"><div class="row"><div class="col-md-10" name="value"><input type="text" class="input-carrier form-control new-carrier" name="" maxlength="50"></div>' +
            '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger remove-carrier new-carrier" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
            '<div class="col-md-1" name="save"><button disabled="disabled" type="button" class="btn btn-primary save-carrier new-carrier" data-toggle="tooltip" data-placement="top" data-original-title="Save Carrier"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
            '</div></div>';
        $('#CarriersContainer').append(appendstring);
        $('input.new-carrier').focus();
    });


    //Handles closing the Carrier modal without any changes
    $('#CarriersDismiss').click(function () {
        $('#CarriersAdd').removeAttr('disabled')
        deleteCarriersAlert();
    })

    // removes a carrier
    $(document.body).on('click', '.remove-carrier', function () {
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        var carrier = $this.parent().siblings('[name="value"]').children();
        var container = $this.parent().parent();
        if ($this.hasClass('new-carrier')) {
            container.remove();
            $('#CarriersAdd').removeAttr('disabled');
        } else {
            var result = confirm('Click OK to delete carrier ' + carrier.attr('name'));
            if (result) {
                cMPreferencesHub.server.deleteCarrier(carrier.attr('name')).done(function (success) {
                    if (success == "") {
                        container.remove();
                    } else {
                        alert('Delete failed!  Please try again.');
                        $this.removeAttr('disabled');
                    };
                });
            };
        };
    });

    // updates or saves a carrier entry
    $(document.body).on('click', '.save-carrier', function () {
        var $this = $(this);
        var remove = $this.parent().siblings('[name="remove"]').children();
        var set = $this.parent().siblings('[name="set"]').children();
        var carrier = $this.parent().siblings('[name="value"]').children();
        var name = carrier.attr('name') !== 'undefined' ? carrier.attr('name') : '';
        cMPreferencesHub.server.saveCarrier(carrier.val(), name).done(function (result) {
            if (result=="") {
                if ($this.hasClass('new-carrier')) {
                    $('.new-carrier').removeClass('new-carrier');
                    $('#CarriersAdd').removeAttr('disabled');
                };
                $this.attr('disabled', 'disabled');
                set.removeAttr('disabled');
                remove.removeAttr('disabled');
                carrier.attr('name', carrier.val());
            } else {
                alert('Carrier not saved!  Please try again.');
            };
        });
    });

    // handles input on a carrier input.  Either disables or enables save functionality based on value of input
    $(document.body).on('input', '.input-carrier', function (e) {
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
            } else {
                $this.addClass('input-edit');

                var conflict = false;
                $('.input-carrier:not(.input-edit)').each(function () {
                    if ($this.val().toLowerCase() == $(this).val().toLowerCase() || $this.val().toLowerCase() == $(this).attr('name').toLowerCase()) {
                        save.attr('disabled', 'disabled');
                        conflict = true;
                    }
                });
                if (!conflict) {
                    save.removeAttr('disabled');
                    remove.removeAttr('disabled', 'disabled');
                    deleteCarriersAlert();
                } else {
                    postCarriersAlert('Carrier cannot be saved!  Another carrier matches the current.  Please save any pending changes before attempting to save this entry.');
                };
                $this.removeClass('input-edit');
            };
        };
    });
});