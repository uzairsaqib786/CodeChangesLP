// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the scan code type modal
var trigger_scan_modal;

// posts an alert to the modal
var postSTAlert = function (message) {
    $('#st_alerts').html('<div class="alert alert-warning alert-custom" role="alert"><strong>Conflict:</strong> ' + message + '</div>');
};

// deletes all alerts from the modal
var deleteSTAlert = function () {
    $('#st_alerts').html('');
};

$(document).ready(function () {
    // sender for scan type to set the value of
    var scansender;

    trigger_scan_modal = function () {
        deleteSTAlert();
        var appendstring = '';
        globalHubConn.server.getScanTypes().done(function (result) {
            for (var x = 0; x < result.length; x++) {
                appendstring += '<div class="scan_container" style="padding-top:5px;">' +
                    '<div class="row">' +
                    '<div class="col-md-9" name="scantype_input"><input type="text" value="' + result[x] + '" name="' + result[x] + '" class="form-control" maxlength="50"></div>' +
                    '<div class="col-md-1" name="scantype_remove"><button type="button" class="btn btn-danger scantype-remove"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                    '<div class="col-md-1" name="scantype_set"><button type="button" data-dismiss="modal" class="btn btn-primary scantype-set"><span class="glyphicon glyphicon-edit"></span></button></div>' +
                    '<div class="col-md-1" name="scantype_save"><button disabled="disabled" type="button" class="btn btn-primary scantype-save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
                    '</div></div>';
            };
            $('#scan_modal_container').html(appendstring);
        });

        $('#scan_modal').modal('show');
    };

    // Handles launching the scan type modal
    $(document.body).on('click', '.scan-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            scansender = $this.siblings('input');
        } else {
            scansender = $this;
        };

        trigger_scan_modal();
    });

    // sets the sender's value to the selected value
    $(document.body).on('click', '.scantype-set', function () {
        var st = $(this).parent().siblings('[name="scantype_input"]').children('input').val();
        scansender.val(st);
        scansender.trigger('input');
    });

    // adds a new scan type
    $(document.body).on('click', '#scantype_add', function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = '<div class="scan_container" style="padding-top:5px;"><div class="row"><div class="col-md-9" name="scantype_input"><input type="text" class="form-control new-scantype" maxlength="50" /></div>' +
            '<div class="col-md-1" name="scantype_remove"><button type="button" class="btn btn-danger scantype-remove new-scantype"><span class="glyphicon glyphicon-remove"></span></button></div>' +
            '<div class="col-md-1" name="scantype_set"><button type="button" data-dismiss="modal" class="btn btn-primary scantype-set new-scantype" disabled="disabled"><span class="glyphicon glyphicon-edit"></span></button></div>' +
            '<div class="col-md-1" name="scantype_save"><button disabled="disabled" type="button" class="btn btn-primary scantype-save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
            '</div></div>';
        $('#scan_modal_container').append(appendstring);
        $('.new-scantype').focus();
    });

    // deletes a scan type
    $(document.body).on('click', '.scantype-remove', function () {
        var $this = $(this);
        var scantype = $this.parent().siblings('[name="scantype_input"]').find('input').val();
        var container = $this.parent().parent().parent();
        if ($this.hasClass('new-scantype')) {
            container.remove();
            $('#scantype_add').removeAttr('disabled');
        } else {
            var result = confirm('Click OK to delete Scan Type ' + scantype + '.');
            if (result) {
                globalHubConn.server.deleteScanType(scantype).done(function () {
                    container.remove();
                });
            };
        };
    });

    // handles enabling/disabling save for scan types
    $(document.body).on('input', '[name="scantype_input"] input', function () {
        deleteSTAlert();
        var $this = $(this);
        $this.addClass('scantype-edit');
        var save = $this.parent().siblings('[name="scantype_save"]').children();

        if ($this.attr('name') == $this.val() || $this.val() == '') {
            save.attr('disabled', 'disabled');
        } else {
            var enable = true;
            $('[name="scantype_input"] input:not(.scantype-edit)').each(function () {
                if ($this.val() == $(this).attr('name') || $(this).val() == $this.val()) {
                    enable = false;
                };
            });
            if (enable) {
                save.removeAttr('disabled');
            } else {
                save.attr('disabled', 'disabled');
                postSTAlert('Scan Type already exists.  Please save any pending changes before attempting to save this entry.');
            };
        };
    });

    // saves a new or edited scan type
    $(document.body).on('click', '.scantype-save', function () {
        var $this = $(this);
        var scantype = $this.parent().siblings('[name="scantype_input"]').find('input');
        $this.attr('disabled', 'disabled');
        var oldst = typeof scantype.attr('name') !== 'undefined' ? scantype.attr('name') : '';
        console.log(oldst + scantype.val())
        globalHubConn.server.saveScanType(scantype.val(), oldst).done(function () {
            scantype.attr('name', scantype.val());
            if (scantype.hasClass('new-scantype')) {
                $('.new-scantype').removeClass('new-scantype');
                $('#scantype_add').removeAttr('disabled');
                $this.parent().siblings('[name="scantype_set"]').find('button').removeAttr('disabled');
            };
        });
    });
});