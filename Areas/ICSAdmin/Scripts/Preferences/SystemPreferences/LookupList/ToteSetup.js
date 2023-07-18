// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    addResize(function () {
        $('#ToteSetup_Content').css('max-height', $(window).height() * 0.65);
    });

    $('#ToteClear').click(function () {
        if (confirm('Click OK to clear ALL tote information for incomplete transactions.')) {
            if (confirm('Are you sure?  Click OK to confirm clearing all tote information for incomplete transactions.')) {
                preferencesHub.server.clearTotes();
            };
        };
    });

    $('#ToteAdd').click(function () {
        $(this).attr('disabled', 'disabled').tooltip('hide');
        $('<div class="row ToteContainer" style="padding-top:10px;"><div class="col-md-3"><input type="text" class="form-control tote new-tote" name="" maxlength="50" /></div>' +
        '<div class="col-md-3"><input type="text" class="form-control cell" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" /></div>' +
        '<div class="col-md-2"><button type="button" class="btn btn-primary save-tote" disabled="disabled" data-toggle="tooltip" data-placement="left" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button>' +
        ' <button type="button" class="btn btn-danger remove-tote" data-toggle="tooltip" data-placement="right" data-original-title="Remove"><span class="glyphicon glyphicon-remove"></span></button></div></div>').appendTo($('#ToteSetup_Content')).find('.remove-tote, .save-tote').tooltip();
        $('.new-tote').focus();
    });

    $('#ToteSetup_Content').on('input', 'input.tote, input.cell', function () {
        verifySave($(this));
    });

    $('#LookupListSetupNav a[href="#toteSetup"]').on('show.bs.tab', function (e) {
        e.stopImmediatePropagation();
        getToteSetup();
    });

    $(document.body).on('click', '.remove-tote', function () {
        deleteToteMessage();
        var add = $('#ToteAdd');
        var $this = $(this);
        var $input = $this.parent().siblings().find('input.tote');
        var container = $this.parents('.ToteContainer');
        if ($input.hasClass('new-tote')) {
            container.remove();
            add.removeAttr('disabled');
        } else {
            var result = confirm('Click OK to delete ' + $input.attr('name').trim() + ' from the Tote list.');
            if (result) {
                preferencesHub.server.deleteToteEntry($input.attr('name')).done(function () {
                    container.remove();
                });
            };
        };
    });

    $(document.body).on('click', '.save-tote', function () {
        var $this = $(this);
        var tote = $this.parent().siblings().find('input.tote');
        var cell = $this.parent().siblings().find('input.cell');
        var value = tote.val();
        var name = tote.attr('name');
        var remove = $this.parent().siblings().find('button.remove-tote');
        if (verifySave(tote)) {
            preferencesHub.server.saveTote(name, value, cell.val()).done(function () {
                if (tote.hasClass('new-tote')) {
                    tote.removeClass('new-tote');
                    $('#ToteAdd').removeAttr('disabled');
                };
                tote.attr('name', value);
                cell.attr('name', cell.val());
                $this.attr('disabled', 'disabled');
                remove.removeAttr('disabled');
            });
        };
    });
});

function getToteSetup() {
    var container = $('#ToteSetup_Content');
    preferencesHub.server.getToteSetup().done(function (totes) {
        container.html('');
        $.each(totes, function (index, obj) {
            container.append('<div class="row ToteContainer" style="padding-top:10px;"><div class="col-md-3"><input type="text" class="form-control tote" name="' + obj.ToteID + '" value="' + obj.ToteID + '" maxlength="50" /></div>' +
                    '<div class="col-md-3"><input type="text" class="form-control cell" name="' + obj.Cells + '" value="' + obj.Cells + '" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" /></div>' +
                    '<div class="col-md-2"><button type="button" class="btn btn-primary save-tote" disabled="disabled" data-toggle="tooltip" data-placement="left" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button>' +
                    ' <button type="button" class="btn btn-danger remove-tote" data-toggle="tooltip" data-placement="right" data-original-title="Remove"><span class="glyphicon glyphicon-remove"></span></button></div></div>');
        });
        container.find('.remove-tote, .save-tote').tooltip();
    });
};

function deleteToteMessage() {
    $('#ToteAlerts').html('');
};

function postToteMessage(message) {
    $('#ToteAlerts').html('<div class="alert alert-warning alert-custom" role="alert">' + message + '</div>');
};

function verifySave($this) {
    var tote;
    var cell;
    var save = $this.parent().siblings().find('button.save-tote');
    if ($this.hasClass('tote')) {
        tote = $this;
        cell = $this.parent().siblings().find('input.cell');
    } else {
        cell = $this;
        tote = $this.parent().siblings().find('input.tote');
    };
    if ((tote.val().trim().toLowerCase() == tote.attr('name').trim().toLowerCase() && cell.val() == cell.attr('name')) || tote.val().trim() == '' || cell.val() == '') {
        deleteToteMessage();
        save.attr('disabled', 'disabled');
        return true;
    };
    tote.addClass('edit');
    var toteVal = tote.val().trim().toLowerCase();
    var current;
    var enable = true;
    $.each($('input.tote:not(.edit)'), function (index, element) {
        current = $(this);
        if (current.val() == toteVal || current.attr('name') == toteVal) {
            enable = false;
        };
    });
    if (enable) {
        if ($.isNumeric(cell.val()) && cell.val() >= 0) {
            deleteToteMessage();
            save.removeAttr('disabled');
        } else {
            save.attr('disabled', 'disabled');
            postToteMessage('Cells must be a positive integer.');
        };
    } else {
        save.attr('disabled', 'disabled');
        postToteMessage('Tote must be unique.  Another entry matches it.  Please save any pending totes and try again.');
    };
    $this.removeClass('edit');
    return enable;
};