// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#addNewIPTI').click(function () {
        $(this).attr('disabled', 'disabled');
        appendToIPTI('', '', '', '', true);
    });

    $('#IPTI_Container').on('click', '.save-IPTI', function () {
        saveIPTI($(this));
    });

    $('#IPTI_Container').on('click', '.remove-IPTI', function () {
        deleteIPTI($(this));
    });

    $('#IPTI_Container').on('input', 'input', function () {
        var $this = $(this);
        var container = $this.parents('.IPTI_Container');
        setNumeric($this);
        if ($this.val().trim() != $this.attr('name').trim() && $this.val().trim() != '') {
            container.find('.save-IPTI').removeAttr('disabled');
            container.find('.alert.alert-warning').remove();
        } else {
            container.find('.alert.alert-warning').remove();
        };
    });
});

function deleteIPTI($delete) {
    var container = $delete.parents('.IPTI_Container');
    var disp = container.find('input.display-number'), bay = container.find('input.bay-number'), baydisp = container.find('input.bay-display'), comm = container.find('input.comm'),
        zone = $('#devprefs_zone').val();
    if (container.hasClass('new-IPTI')) {
        container.remove();
        $('#addNewIPTI').removeAttr('disabled');
    } else if (confirm('Click OK to delete selected IPTI entry from zone ' + zone)) {
        preferencesHub.server.deleteIPTI(disp.attr('name'), bay.attr('name'), baydisp.attr('name'), comm.attr('name'), zone).done(function () {
            container.remove();
        });
    };
};

function appendToIPTI(disp, bay, baydisp, comm, newentry) {
    $('#IPTI_Container').append('<div style="padding-top:10px;" class="row IPTI_Container' + (newentry ? ' new-IPTI' : '') + '">\
                                    <div class="col-md-2">\
                                        <input type="text" class="form-control display-number" placeholder="Display Number" name="' + disp + '" value="' + disp + '" oninput="setNumericInRange($(this), SqlLimits.numerics.smallint)" />\
                                    </div>\
                                    <div class="col-md-2">\
                                        <input type="text" class="form-control bay-number" placeholder="Bay Number" name="' + bay + '" value="' + bay + '" oninput="setNumericInRange($(this), SqlLimits.numerics.smallint)" />\
                                    </div>\
                                    <div class="col-md-3">\
                                        <input type="text" class="form-control bay-display" placeholder="Bay Display Number" name="' + baydisp + '" value="' + baydisp + '" oninput="setNumericInRange($(this), SqlLimits.numerics.smallint)" />\
                                    </div>\
                                    <div class="col-md-3">\
                                        <input type="text" class="form-control comm" placeholder="Comm Port" name="' + comm + '" value="' + comm + '" oninput="setNumericInRange($(this), SqlLimits.numerics.smallint)" />\
                                    </div>\
                                    <div class="col-md-1">\
                                        <button disabled="disabled" type="button" class="btn btn-primary pull-right save-IPTI"><span class="glyphicon glyphicon-floppy-disk"></span></button>\
                                    </div>\
                                    <div class="col-md-1">\
                                        <button type="button" class="btn btn-danger pull-right remove-IPTI"><span class="glyphicon glyphicon-remove"></span></button>\
                                    </div>\
                                </div>');
};

function saveIPTI($save) {
    $save.attr('disabled', 'disabled');
    var container = $save.parents('.IPTI_Container');
    var disp = container.find('input.display-number'), bay = container.find('input.bay-number'), baydisp = container.find('input.bay-display'), comm = container.find('input.comm');

    container.addClass('edit');
    var other, odisp, obay, obaydisp, ocomm;
    var save = true;
    $('.IPTI_Container:not(.edit)').each(function (index, element) {
        other = $(this); odisp = other.find('.display-number'); obay = other.find('.bay-number'); obaydisp = other.find('.bay-display'); ocomm = other.find('.comm');

        if (
                (odisp.val() == disp.val() || odisp.attr('name') == disp.val()) &&
                (obay.val() == bay.val() || obay.attr('name') == bay.val()) &&
                (obaydisp.val() == baydisp.val() || obaydisp.attr('name') == baydisp.val()) &&
                (ocomm.val() == comm.val() || ocomm.attr('name') == comm.val())
            ) {
            save = false;
        };
    });
    container.removeClass('edit');
    if (!save) {
        container.prepend('<div class="alert alert-warning alert-custom" role="alert">Values are not unique.  Edit the entry before saving.</div>');
        return false;
    };

    var prefs = [
        disp.val().trim(), disp.attr('name') == '' ? -1 : disp.attr('name'),
        bay.val().trim(), bay.attr('name') == '' ? -1 : bay.attr('name'),
        baydisp.val().trim(), baydisp.attr('name') == '' ? -1 : baydisp.attr('name'),
        comm.val().trim(), comm.attr('name') == '' ? -1 : comm.attr('name')
    ];
    var zone = $('#devprefs_zone').val();
    preferencesHub.server.saveIPTI(prefs, zone, container.hasClass('new-IPTI')).done(function () {
        disp.attr('name', disp.val().trim());
        bay.attr('name', bay.val().trim());
        baydisp.attr('name', baydisp.val().trim());
        comm.attr('name', comm.val().trim());
        if (container.hasClass('new-IPTI')) {
            container.removeClass('new-IPTI');
            $('#addNewIPTI').removeAttr('disabled');
        };
    });
};