// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    addResize(function () { $('#AdjustmentSetup_Content').css('max-height', $(window).height() * 0.65); });

    $('#AdjustAdd').click(function () {
        $(this).attr('disabled', 'disabled').tooltip('hide');
        $('#AdjustmentSetup_Content').append('<div class="row AdjustContainer" style="padding-top:10px;"><div class="col-md-6"><input type="text" class="form-control new-adjust adjust" maxlength="255" name="" /></div>' +
            '<div class="col-md-2"><button type="button" class="btn btn-primary save-adjust" disabled="disabled"><span class="glyphicon glyphicon-floppy-disk"></span></button>' +
            ' <button type="button" class="btn btn-danger remove-adjust"><span class="glyphicon glyphicon-remove"></span></button></div></div>');
        $('.new-adjust').focus();
    });

    $('#AdjustmentSetup_Content').on('input', 'input.adjust', function () {
        var $this = $(this);
        var save = $this.parent().siblings().find('button.save-adjust');
        var value = $this.val().trim().toLowerCase();
        var name = $this.attr('name').trim().toLowerCase();
        var enable = true;
        if (name != value && $this.val().trim() != '') {
            $this.addClass('edit');
            var $each;
            $.each($('.adjust:not(.edit)'), function (index, element) {
                $each = $(this);
                if (value == $each.val().trim().toLowerCase() || $each.val().trim().toLowerCase() == value) {
                    enable = false;
                };
            });
            if (enable) {
                deleteAdjustMessage();
                save.removeAttr('disabled');
            } else {
                postAdjustMessage('Adjustment Reason is a duplicate.  Save other edited fields and ensure it is not a duplicate before saving.');
                save.attr('disabled', 'disabled');
            };
            $this.removeClass('edit');
        } else {
            save.attr('disabled', 'disabled');
        };
    });

    $('#LookupListSetupNav a[href="#adjustmentSetup"]').on('show.bs.tab', function (e) {
        e.stopImmediatePropagation();
        getAdjustmentLookup();
    });

    $(document.body).on('click', '.remove-adjust', function () {
        deleteAdjustMessage();
        var add = $('#AdjustAdd');
        var $this = $(this);
        var $input = $this.parent().siblings().find('input');
        var container = $this.parents('.AdjustContainer');
        if ($input.hasClass('new-adjust')) {
            container.remove();
            add.removeAttr('disabled');
        } else {
            var result = confirm('Click OK to delete ' + $input.attr('name').trim() + ' from the Adjustment Reason lookup list.');
            if (result) {
                preferencesHub.server.deleteAdjustReasonEntry($input.attr('name')).done(function () {
                    container.remove();
                });
            };
        };
    });

    $(document.body).on('click', '.save-adjust', function () {
        var $this = $(this);
        var input = $this.parent().siblings().find('input');
        var value = input.val();
        var name = input.attr('name');
        var remove = $this.parent().siblings().find('button.remove-adjust');
        preferencesHub.server.saveAdjustmentReasonEntry(name, value).done(function () {
            if (input.hasClass('new-adjust')) {
                input.removeClass('nnew-adjust');
                $('#AdjustAdd').removeAttr('disabled');
            };
            input.attr('name', value);
            $this.attr('disabled', 'disabled');
            remove.removeAttr('disabled');
        });
    });
});

function getAdjustmentLookup() {
    var container = $('#AdjustmentSetup_Content');
    preferencesHub.server.getAdjustmentLookup().done(function (adjustments) {
        container.html('');
        $.each(adjustments, function (index, value) {
            container.append('<div class="row AdjustContainer" style="padding-top:10px;"><div class="col-md-6"><input type="text" class="form-control adjust" name="' + value + '" value="' + value + '" maxlength="255" /></div>' +
                    '<div class="col-md-2"><button type="button" class="btn btn-primary save-adjust" disabled="disabled"><span class="glyphicon glyphicon-floppy-disk"></span></button>' +
                    ' <button type="button" class="btn btn-danger remove-adjust"><span class="glyphicon glyphicon-remove"></span></button></div></div>');
        });
    });
};

function deleteAdjustMessage() {
    $('#AdjustAlerts').html('');
};

function postAdjustMessage(message) {
    $('#AdjustAlerts').html('<div class="alert alert-warning alert-custom" role="alert">' + message + '</div>');
};