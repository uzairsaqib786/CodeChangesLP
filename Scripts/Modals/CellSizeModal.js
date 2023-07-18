// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the cell size modal
var trigger_cell_modal;

// posts an alert to the cell size modal
var postCellAlert = function (message) {
    $('#cell_alerts').html('<div class="alert alert-warning alert-custom" role="alert"><strong>Conflict:</strong> ' + message + '</div>');
};

// deletes alerts from the cell size modal
var deleteCellAlert = function () {
    $('#cell_alerts').html('');
};

$(document).ready(function () {
    var IsDrop;
    var SelVal="";
    var OldSelVal;

    trigger_cell_modal = function (sender) {
        if ($('.activeRow').length > 0) {
            $('#cell_sender').val('.activeRow input[name="' + sender.attr('name') + '"]');
        } else {
            $('#cell_sender').val('#' + sender.attr('id'));
        };
        var appendstring = '';
        globalHubConn.server.getCellSizes().done(function (cellObj) {
            for (var x = 0; x < cellObj.cellsize.length; x++) {
                appendstring += '<div class="cell_container" style="padding-top:10px;"><div class="row">' +
                    '<div class="col-md-2" name="cell_input"><input maxlength="50" type="text" class="form-control input-cell" value="' + cellObj.cellsize[x] + '" name="' + cellObj.cellsize[x] + '"></div>' +
                    '<div class="col-md-7" name="celltype_input"><input maxlength="50" type="text" class="form-control input-celltype" value="' + cellObj.celltype[x] + '" name="' + cellObj.celltype[x] + '"></div>' +
                    '<div class="col-md-1" name="remove"><button type="button" data-toggle="tooltip" data-placement="top" title="Remove" class="btn btn-danger remove-cell" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                    '<div class="col-md-1" name="set"><button data-dismiss="modal" type="button" data-toggle="tooltip" data-placement="top" title="Set" class="btn btn-primary set-cell" data-original-title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' +
                    '<div class="col-md-1" name="save"><button disabled="disabled" type="button" data-toggle="tooltip" data-placement="top" title="Save" class="btn btn-primary save-cell" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
                    '</div></div>';
            };
            $('#cell_container').html(appendstring);
            $('#cell_modal').modal('show');
        });
    };

    $('#cell_modal').on('hidden.bs.modal', function () {
        var $orig = $($('#cell_sender').val());

        if (IsDrop) {
            $orig.empty();
            $orig.append('<option value=""></option>')
            globalHubConn.server.getCellSizes().done(function (cellObj) {
                for (var x = 0; x < cellObj.cellsize.length; x++) {
                    $orig.append('<option value="' + cellObj.cellsize[x] + '">' + cellObj.cellsize[x] + '</option>')
                };
                if (SelVal != "") {
                    $orig.val(SelVal).trigger('change');
                } else {
                    $orig.val(OldSelVal).trigger('change');
                };
            });
        };
    });

    // launches the cell size modal
    $(document.body).on('click', '.cell-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            //Check if input or dropdown
            if ($this.siblings('input').length > 0) {
                IsDrop = false;
                trigger_cell_modal($this.siblings('input'));
            } else {
                IsDrop = true;
                OldSelVal = $this.siblings('select').val();
                trigger_cell_modal($this.siblings('select'));
            };

        } else {
            trigger_cell_modal($this);
        };
    });

    // disables add new functionality for cell size modal when the cell size modal is dismissed
    $(document.body).on('click', '#cell_dismiss', function () {
        $('#cell_add').removeAttr('disabled');
        deleteCellAlert();
    });

    // set the sender's value to the selected cell size's value
    $(document.body).on('click', '.set-cell', function () {
        var $this = $(this);
        var value = $this.parent().siblings('[name="cell_input"]').children().val();
        var $orig = $($('#cell_sender').val());
        SelVal = value; //Added in order to set the value of the dropdown after redraw
        $orig.val(value);
        $orig.trigger('change').trigger('input');
        if ($('.activeRow').length > 0) {
            $('.activeRow input[name="Cell Size"]').trigger('input');
        } else if ($('#addRow').length > 0) {
            $('#addRow input[name="Cell Size"]').trigger('input');
        };
        deleteCellAlert();
    });

    // deletes the specified cell size
    $(document.body).on('click', '.remove-cell', function () {
        var $this = $(this);
        var value = $this.parent().siblings('[name="cell_input"]').children().val();
        var container = $this.parent().parent().parent();

        if ($this.hasClass('new-cell')) {
            container.remove();
            $('#cell_add').removeAttr('disabled');
            deleteCellAlert();
        } else {
            var result = confirm('Click OK to delete ' + value + ' from Cell Sizes.');
            if (result) {
                globalHubConn.server.deleteCell(value).done(function () {
                    container.remove();
                });
            };
        };
    });
    
    // saves a cell size
    $(document.body).on('click', '.save-cell', function () {
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        var cell = $this.parent().siblings('[name="cell_input"]').children();
        var newcell = cell.val();
        var oldcell = cell.attr('name');
        var ctype = $this.parent().siblings('[name="celltype_input"]').children().val();
        var set = $this.parent().siblings('[name="set"]').children();
        var remove = $this.parent().siblings('[name="remove"]').children();
        
        globalHubConn.server.saveCell(oldcell == null ? '' : oldcell, newcell, ctype).done(function () {
            set.removeAttr('disabled');
            remove.removeAttr('disabled');
            cell.attr('name', newcell);
            if ($this.hasClass('new-cell')) {
                $('#cell_add').removeAttr('disabled');
                $('.new-cell').removeClass('new-cell');
            };
        });
        
    });

    // adds a new cell size
    $(document.body).on('click', '#cell_add', function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = '<div class="cell_container" style="padding-top:10px;"><div class="row">' +
                    '<div class="col-md-2" name="cell_input"><input maxlength="50" type="text" class="form-control input-cell new-cell" name=""></div>' +
                    '<div class="col-md-7" name="celltype_input"><input maxlength="50" type="text" class="form-control input-celltype new-cell" name=""></div>' +
                    '<div class="col-md-1" name="remove"><button type="button" data-toggle="tooltip" data-placement="top" title="Remove" class="btn btn-danger remove-cell new-cell" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                    '<div class="col-md-1" name="set"><button disabled="disabled" data-dismiss="modal" type="button" data-toggle="tooltip" data-placement="top" title="Set" class="btn btn-primary set-cell new-cell" data-original-title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' +
                    '<div class="col-md-1" name="save"><button disabled="disabled" type="button" data-toggle="tooltip" data-placement="top" title="Save" class="btn btn-primary save-cell new-cell" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
                    '</div></div>';
        $('#cell_container').append(appendstring);
        $('.input-cell.new-cell').focus();
    });

    $(document.body).on('input', '.input-celltype', function () {
        var $this = $(this);
        var save = $this.parent().siblings('[name="save"]').children();
        save.removeAttr('disabled');
    })

    // handles enabling/disabling the ability to save a cell size
    $(document.body).on('input', '.input-cell', function () {
        deleteCellAlert();
        var $this = $(this);
        var save = $this.parent().siblings('[name="save"]').children();
        var remove = $this.parent().siblings('[name="remove"]').children();
        var set = $this.parent().siblings('[name="set"]').children();
        var enable = true;

        $this.addClass('input-edit');
        if (!$this.hasClass('new-cell')) {
            if ($this.attr('name').toLowerCase() != $this.val().toLowerCase() && $this.val().toLowerCase() != '') {
                $('.input-cell:not(.input-edit)').each(function () {
                    if ($this.val().toLowerCase() == $(this).val().toLowerCase() || $this.val().toLowerCase() == $(this).attr('name').toLowerCase()) {
                        enable = false;
                        postCellAlert('Cell Size already exists.  Ensure any pending changes are saved before attempting to save this entry.');
                    };
                });
            } else {
                enable = false;
            };
        } else {
            if ($this.val() != '') {
                $('.input-cell:not(.input-edit)').each(function () {
                    if ($this.val().toLowerCase() == $(this).val().toLowerCase() || $this.val().toLowerCase() == $(this).attr('name').toLowerCase()) {
                        enable = false;
                        postCellAlert('Cell Size already exists.  Ensure any pending changes are saved before attempting to save this entry.');
                    };
                });
            } else {
                enable = false;
            };
        };
        $this.removeClass('input-edit');
        if (enable) {
            if (!$this.hasClass('new-cell')) {
                remove.attr('disabled', 'disabled');
            };
            set.attr('disabled', 'disabled');
            save.removeAttr('disabled');
        } else {
            save.attr('disabled', 'disabled');
        };
    });
});