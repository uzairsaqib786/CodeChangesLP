// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    addResize(function () {
        $('#UF1Setup_Content, #UF2Setup_Content').css('max-height', $(window).height() * 0.65);
    });

    $('#UF1Add, #UF2Add').click(function () {
        var $this = $(this).tooltip('hide');
        $this.attr('disabled', 'disabled');
        var uf = $this.attr('id') == 'UF1Add' ? 1 : 2;
        var container = $('#UF' + uf + 'Setup_Content');
        var appendstring = '<div class="row UFContainer" style="padding-top:10px;"><div class="col-md-6"><input type="text" class="form-control new-uf uf-' + uf + '" maxlength="255" name="" /></div>' +
            '<div class="col-md-2"><button type="button" class="btn btn-primary save-uf" disabled="disabled" data-toggle="tooltip" data-placement="left" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button>' +
            ' <button type="button" class="btn btn-danger remove-uf" data-toggle="tooltip" data-placement="right" data-original-title="Remove"><span class="glyphicon glyphicon-remove"></span></button></div></div>';
        container.append(appendstring);
        container.find('.save-uf, .remove-uf').tooltip();
        $('.uf-'+uf+'.new-uf').focus()
    });

    $(document.body).on('click', '.remove-uf', function () {
        deleteUFMessage();
        var uf = getActiveUF();
        var add = $('#UF' + uf + 'Add');
        var $this = $(this);
        var $input = $this.parent().siblings().find('input');
        var container = $this.parents('.UFContainer');
        if ($input.hasClass('new-uf')) {
            container.remove();
            add.removeAttr('disabled');
        } else {
            var result = confirm('Click OK to delete ' + $input.attr('name').trim() + ' from the User Field ' + uf + ' lookup list.');
            if (result) {
                preferencesHub.server.deleteUFEntry($input.attr('name'), uf).done(function () {
                    container.remove();
                });
            };
        };
    });

    $(document.body).on('click', '.save-uf', function () {
        var $this = $(this);
        var input = $this.parent().siblings().find('input');
        var value = input.val();
        var name = input.attr('name');
        var remove = $this.parent().siblings().find('button.remove-uf');
        var uf = getActiveUF();
        preferencesHub.server.saveUFLookupEntry(name, value, uf).done(function () {
            if (input.hasClass('new-uf')) {
                input.removeClass('new-uf');
                $('#UF' + uf + 'Add').removeAttr('disabled');
            };            
            input.attr('name', value);
            $this.attr('disabled', 'disabled');
            remove.removeAttr('disabled');
        });
    });

    $('#UF1Setup_Content, #UF2Setup_Content').on('input', 'input.uf-1, input.uf-2', function () {
        var $this = $(this);
        var uf = $this.hasClass('uf-1') ? 1 : 2;
        var save = $this.parent().siblings().find('button.save-uf');
        var value = $this.val().trim().toLowerCase();
        var name = $this.attr('name').trim().toLowerCase();
        var enable = true;
        if (name != value && $this.val().trim() != '') {
            $this.addClass('edit');
            var $each;
            $.each($('.uf-' + uf + ':not(.edit)'), function (index, element) {
                $each = $(this);
                if (value == $each.val().trim().toLowerCase() || $each.val().trim().toLowerCase() == value) {
                    enable = false;
                };
            });
            if (enable) {
                deleteUFMessage();
                save.removeAttr('disabled');
            } else {
                postUFMessage('Field is a duplicate.  Save other edited fields and ensure it is not a duplicate before saving.');
                save.attr('disabled', 'disabled');
            };
            $this.removeClass('edit');
        } else {
            save.attr('disabled', 'disabled');
        };
    });

    $('#LookupListSetupNav a[href="#user1Setup"], #LookupListSetupNav a[href="#user2Setup"]').on('show.bs.tab', function (e) {
        e.stopImmediatePropagation();
        var target = $(e.target).attr('href');
        if (target == '#user1Setup') {
            getUFSuggestions(1);
        } else if (target == '#user2Setup') {
            getUFSuggestions(2);
        };
    });
});

function getActiveUF() {
    var id = $('#LookupListSetupNav li.active a').attr('href');
    if (id == '#user1Setup') {
        return 1;
    } else if (id == '#user2Setup') {
        return 2;
    } else {
        return 0;
    };
};

function getUFSuggestions(uf) {
    deleteUFMessage();
    if (uf == 1 || uf == 2) {
        preferencesHub.server.getUserFieldLookupList(uf).done(function (result) {
            var container = $('#UF' + uf + 'Setup_Content');
            container.html('');
            $.each(result, function (index, value) {
                container.append('<div class="row UFContainer" style="padding-top:10px;"><div class="col-md-6"><input type="text" class="form-control uf-' + uf + '" name="' + value + '" value="' + value + '" maxlength="255" /></div>' +
                    '<div class="col-md-2"><button type="button" data-toggle="tooltip" data-placement="left" data-original-title="Save" class="btn btn-primary save-uf" disabled="disabled"><span class="glyphicon glyphicon-floppy-disk"></span></button>' +
                    ' <button type="button" class="btn btn-danger remove-uf" data-toggle="tooltip" data-placement="right" data-original-title="Remove"><span class="glyphicon glyphicon-remove"></span></button></div></div>');
            });
            container.find('.save-uf, .remove-uf').tooltip();
        });
    };
};

function deleteUFMessage() {
    var activeUF = getActiveUF();
    if (activeUF == 1 || activeUF == 2) {
        $('#UF' + activeUF + 'Alerts').html('');
    };
};

function postUFMessage(message) {
    var activeUF = getActiveUF();
    if (activeUF == 1 || activeUF == 2) {
        $('#UF' + activeUF + 'Alerts').html('<div class="alert alert-warning alert-custom" role="alert">' + message + '</div>');
    };
};