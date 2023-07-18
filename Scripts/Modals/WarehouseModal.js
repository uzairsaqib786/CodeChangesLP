// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*
    Sets up and then launches the warehouse modal
*/
var trigger_warehouse_modal;

/*
    Post an alert to the warehouse modal with the messsage passed as a parameter
*/
var postWarehouseAlert = function (message) {
    $('#warehouse_alerts').html('<div class="alert alert-warning alert-custom" role="alert"><strong>Conflict: </strong>' + message + '</div>');
};

// deletes all alerts from the warehouse modal
var deleteWarehouseAlert = function () {
    $('#warehouse_alerts').html('');
};

$(document).ready(function () {
    trigger_warehouse_modal = function (sender, closeCallback) {
        if (typeof closeCallback == 'function') {
            $('#warehouse_modal').one('hidden.bs.modal', closeCallback);
        };
        if ($('.activeRow').length > 0) {
            $('#warehouse_sender').val('.activeRow input[name="Warehouse"]');
        } else {
            if (sender.hasClass('glyphicon')) {
                $('#warehouse_sender').val('#' + sender.siblings('input').attr('id'));
            } else {
                $('#warehouse_sender').val('#' + sender.attr('id'));
            };
        };
        var disableEdits = sender.data('no-edits');
        if (disableEdits == true) {
            $('#warehouse_add').attr('disabled', 'disabled');
        } else {
            $('#warehouse_add').removeAttr('disabled');
        };
        var appendstring = '';
        globalHubConn.server.getWarehouses().done(function (result) {
            for (var x = 0; x < result.length; x++) {
                appendstring += '<div class="warehouse_container" style="padding-top:10px;"><div class="row">' +
                    '<div class="col-md-9" name="value"><input ' + (disableEdits == true ? 'readonly="readonly" class="form-control input-warehouse set-warehouse"' : 'class="form-control input-warehouse"') + ' maxlength="50" name="' + result[x] + '" value="' + result[x] + '" /></div>' +
                    '<div class="col-md-1" name="remove"><button type="button" ' + (disableEdits == true ? 'disabled="disabled"' : '') + ' class="btn btn-danger remove-warehouse" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                    '<div class="col-md-1" name="set"><button type="button" class="btn btn-primary set-warehouse" data-dismiss="modal" data-toggle="tooltip" data-placement="top" data-original-title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' +
                    '<div class="col-md-1" name="save"><button disabled="disabled" type="button" class="btn btn-primary save-warehouse" data-toggle="tooltip" data-placement="top" data-original-title="Save Unit of Measure"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
                    '</div></div>';
            };
            $('#warehouse_container').html(appendstring);
            $('#warehouse_modal').modal('show');
        });
    };

    // calls modal launching function on click
    $(document.body).on('click', '.warehouse-modal', function () {
        trigger_warehouse_modal($(this));
    });

    // adds a new warehouse entry
    $(document.body).on('click', '#warehouse_add', function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = '<div class="warehouse_container" style="padding-top:10px;"><div class="row"><div class="col-md-9" name="value"><input type="text" class="input-warehouse form-control new-warehouse" name="" maxlength="50"></div>' +
            '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger remove-warehouse new-warehouse" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
            '<div class="col-md-1" name="set"><button disabled="disabled" type="button" class="btn btn-primary set-warehouse new-warehouse" data-toggle="tooltip" data-placement="top" data-original-title="Set as value" title=""><span class="glyphicon glyphicon-edit"></span></button></div>' +
            '<div class="col-md-1" name="save"><button disabled="disabled" type="button" class="btn btn-primary save-warehouse new-warehouse" data-toggle="tooltip" data-placement="top" data-original-title="Save Unit of Measure"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
            '</div></div>';
        $('#warehouse_container').append(appendstring);
        $('input.new-warehouse').focus();
    });

    // sets the value of the text box that launched the modal
    $(document.body).on('click', '.set-warehouse', function () {
        var value = $(this).parent().parent().find('[name="value"]').children().val();
        $($('#warehouse_sender').val()).val(value).trigger('input');
        deleteWarehouseAlert();
        $('#warehouse_modal').modal('hide');
    });

    $('#ClearWarehouse').click(function () {
        $($('#warehouse_sender').val()).val('').trigger('input');
        deleteWarehouseAlert();
        $('#warehouse_modal').modal('hide');
    });

    //Handles closing the warehouse modal without any changes
    $('#warehouse_dismiss').click(function () {
        deleteWarehouseAlert();
    });

    // removes a warehouse
    $(document.body).on('click', '.remove-warehouse', function () {
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        var warehouse = $this.parent().siblings('[name="value"]').children();
        var container = $this.parent().parent().parent();
        if ($this.hasClass('new-warehouse')) {
            container.remove();
            $('#warehouse_add').removeAttr('disabled');
        } else {
            var result = confirm('Click OK to delete warehouse ' + warehouse.attr('name'));
            if (result) {
                globalHubConn.server.deleteWarehouse(warehouse.attr('name')).done(function (success) {
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

    // updates or saves a warehouse entry
    $(document.body).on('click', '.save-warehouse', function () {
        var $this = $(this);
        var remove = $this.parent().siblings('[name="remove"]').children();
        var set = $this.parent().siblings('[name="set"]').children();
        var warehouse = $this.parent().siblings('[name="value"]').children();
        var name = warehouse.attr('name') !== 'undefined' ? warehouse.attr('name') : '';
        globalHubConn.server.saveWarehouse(warehouse.val(), name).done(function (result) {
            if (result) {
                if ($this.hasClass('new-warehouse')) {
                    $('.new-warehouse').removeClass('new-warehouse');
                    $('#warehouse_add').removeAttr('disabled');
                };
                $this.attr('disabled', 'disabled');
                set.removeAttr('disabled');
                remove.removeAttr('disabled');
                warehouse.attr('name', warehouse.val());
            } else {
                alert('Warehouse not saved!  Please try again.');
            };
        });
    });

    // handles input on a warehouse input.  Either disables or enables save functionality based on value of input
    $(document.body).on('input', '.input-warehouse', function (e) {
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
                $('.input-warehouse:not(.input-edit)').each(function () {
                    if ($this.val() == $(this).val() || $this.val() == $(this).attr('name')) {
                        save.attr('disabled', 'disabled');
                        conflict = true;
                    }
                });
                if (!conflict) {
                    save.removeAttr('disabled');
                    remove.attr('disabled', 'disabled');
                    set.attr('disabled', 'disabled');
                    deleteWarehouseAlert();
                } else {
                    postWarehouseAlert('Warehouse cannot be saved!  Another warehouse matches the current.  Please save any pending changes before attempting to save this entry.');
                };
                $this.removeClass('input-edit');
            };
        };
    });
});