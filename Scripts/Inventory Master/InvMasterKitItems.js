// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#addKitItem').click(function () {
        $('<div class="row" id="newKitContainer" style="padding-top:15px;"><div class="col-md-2">\
            <div class="form-group has-feedback" style="margin-bottom:0px;"><input readonly type="text" class="form-control modal-launch-style item-modal inv-text" data-colname="[Kit Inventory].[Item Number]" id="kitItem_new" /><i style="top:0;" class="glyphicon glyphicon-resize-full form-control-feedback item-modal modal-launch-style"></i></div></div>\
            <div class="col-md-3"><div class="form-group has-feedback" style="margin-bottom:0px;"><input readonly class="no-horizontal form-control description-modal modal-launch-style inv-text" data-colname="[Inventory].[Description]" rows="1"  id="kitDesc_new" /><i style="top:0;" class="glyphicon glyphicon-resize-full form-control-feedback description-modal modal-launch-style"></i></div>\
            </div><div class="col-md-3"><input type="text" class="form-control inv-text" data-colname="[Kit Inventory].[Special Features]" placeholder="Special Features" id="kitSpecFeats_new"/>\
            </div><div class="col-md-2"><input type="text" class="form-control inv-num" data-colname="[Kit Inventory].[Kit Quantity]" placeholder="Kit Quantity" id="kitQty_new" maxlength="9" /></div>\
            <div class="col-md-1"><div class="pull-right">\
            <button id="newKit_remove" type="button" data-toggle="tooltip" data-placement="top" title="" class="btn btn-danger remove-kit" data-original-title="Delete">\
            <span class="glyphicon glyphicon-remove"></span></button></div></div><div class="col-md-1"><div class="pull-right">\
            <button disabled="disabled" id="newKit_save" type="button" data-toggle="tooltip" data-placement="right" title="" class="btn btn-primary save-kit" data-original-title="Save Changes">\
            <span class="glyphicon glyphicon-floppy-disk"></span></button></div></div></div>').appendTo($('#kitContainer'));
        $(this).attr('disabled', 'disabled');
    });

    $(document.body).on('click', '#newKit_remove', function () {
        $('#addKitItem').removeAttr('disabled');
        $('#newKitContainer').remove();
    });

    $(document.body).on('input', '#kitQty_new', function () {
        setNumericInRange($(this), 1, null);
        if (this.value.trim() != '') {
            $('#kitItem_new').trigger('input');
        };
    });
    $(document.body).on('focusout', '#kitQty_new', function () {
        if (this.value.trim() == '') { this.value = 1; };
    });

    $(document.body).on('input', '#kitDesc_new, #kitItem_new', function () {
        var item = document.getElementById('kitItem_new').value;
        invMasterHub.server.selDescription(item).done(function (result) {
            if (result == 'ERROR: ITEM NOT FOUND.') { $('#newKit_save').attr('disabled', 'disabled'); } else { $('#newKit_save').removeAttr('disabled'); };
        });
    });
    
    // save a new kit
    $(document.body).on('click', '#newKit_save', function () {
        var item = document.getElementById('kitItem_new').value;
        if (item.trim() == '' || item == null) { createAlert('itemnull_alert', 'Item Number must be specified in order to add to a kit.', 'alert-warning', true, '<strong>Conflict:</strong> ', '#kit_alerts'); } else { deleteAlert('itemnull_alert'); };
        var kit = document.getElementById('UpdateItemNum').value;
        var qty = document.getElementById('kitQty_new').value;
        var specfeats = document.getElementById('kitSpecFeats_new').value;
        invMasterHub.server.addKit(kit, item, qty, specfeats).done(function (saved) {
            if (saved) {
                $('#newKitContainer').attr('id', item + '_container');
                $('#addKitItem').removeAttr('disabled');
                $('#kitItem_new').attr('id', 'kitItem_' + item);
                $('#kitItem_' + item).attr('name', 'kit-edit');
                $('#kitDesc_new').attr('id', 'kitDesc_' + item);
                $('#kitDesc_' + item).attr('name', 'kit-edit');
                $('#kitSpecFeats_new').attr('id', 'kitSpecFeats_' + item);
                $('#kitSpecFeats_' + item).attr('name', 'kit-edit');
                $('#kitQty_new').attr('id', 'kitQty_' + item);
                $('#kitQty_' + item).attr('name', 'kit-edit');
                $('#newKit_remove').attr('id', item + '_remove');
                $('#newKit_save').attr('id', item + '_save');
                $('#' + item + '_save').attr('disabled', 'disabled');
                deleteAlert('savefail_alert');
            } else { createAlert('savefail_alert', 'New Kit Item not saved!  Try again.', 'alert-danger', true, '<strong>Alert:</strong> ', '#kit_alerts'); };
        });
    });

    // save an edited kit
    // enable save button for individual item
    $(document.body).on('input', 'input[name="kit-edit"]', function () {
        if (this.id.split('_')[0] == 'kitQty') {
            setNumericInRange($(this), 1, null);
        };
        if (this.id.split('_')[0] == 'kitItem' && this.value.trim() == '' || this.value == null) {
            createAlert('kititem_alert', 'Kit Item may not be left blank.', 'alert-warning', true, '<strong>Conflict: </strong> ', '#kit_alerts');
            return false;
        } else { deleteAlert('kititem_alert'); };
        $('#' + this.id.split('_')[1] + '_save').removeAttr('disabled');
    });

    $('.container-fluid').on('focusout', 'input[name="kit-edit"]', function () {
        if (this.id.split('_')[0] == 'kitQty' && this.value.trim() == '') {
            this.value = 1;
        };
    });

    // kit save listener
    $(document.body).on('click', '.save-kit:not(#newKit_save)', function () {
        var sender = $(this);
        sender.attr('disabled', 'disabled');
        var kit = document.getElementById('UpdateItemNum').value;
        var idpart = this.id.split('_')[0];
        var item = document.getElementById('kitItem_' + idpart).value;
        var qty = document.getElementById('kitQty_' + idpart).value;
        var specfeats = document.getElementById('kitSpecFeats_' + idpart).value;
        invMasterHub.server.validateKit(kit, item).done(function (result) {
            if (result) {
                invMasterHub.server.updateKit(kit, idpart, item, qty, specfeats).done(function () {
                    if (idpart != item) {
                        $('#kitItem_' + idpart).attr('id', 'kitItem_' + item);
                        $('#kitDesc_' + idpart).attr('id', 'kitDesc_' + item);
                        $('#kitSpecFeats_' + idpart).attr('id', 'kitSpecFeats_' + item);
                        $('#kitQty_' + idpart).attr('id', 'kitQty_' + item);
                        $('#' + idpart + '_save').attr('id', item + '_save');
                        $('#' + idpart + '_remove').attr('id', item + '_remove');
                    };
                });
                deleteAlert('kititem_alert');
            } else {
                sender.removeAttr('disabled');
                createAlert('kititem_alert', 'Kit and Item must not be interrelated. ' + item + ' contains ' + kit + ', so ' + kit + ' cannot contain ' + item + '.', 'alert-warning', true, '<strong>Conflict: </strong> ', '#kit_alerts');
            };
        });
    });

    // kit remove listener
    $(document.body).on('click', '.remove-kit:not(#newKit_remove)', function () {
        var item = this.id.split('_')[0];
        var kit = document.getElementById('UpdateItemNum').value;
        var result = confirm('Click OK to remove item number ' + item + ' from kit ' + kit + '.  This will only remove the item from the kit, not the item from Inventory.');
        if (result) {
            invMasterHub.server.deleteKit(kit, item).done(function () {
                $('#' + item + '_container').remove();
            });
        };
    });

    $(document.body).on('input', '[name="kit-edit"].item-modal, #kitItem_new', function () {
        var item = $(this);
        var save = item.parent().parent().siblings().find('.save-kit');
        deleteAlert('kititem_alert');
        if ($('#kitItem_' + item.val()).length > 0 && item.attr('id') != 'kitItem_' + item.val()) {
            save.attr('disabled', 'disabled');
            createAlert('kititem_alert', 'Item ' + item.val() + ' already exists in kit.', 'alert-warning', true, '<strong>Conflict: </strong> ', '#kit_alerts');
        } else if (item.val().toLowerCase() == $('#UpdateItemNum').val().toLowerCase()) {
            save.attr('disabled', 'disabled');
            createAlert('kititem_alert', 'Item ' + item.val() + ' cannot belong to itself in a kit.', 'alert-warning', true, '<strong>Conflict: </strong> ', '#kit_alerts');
        } else {
            var description = item.parent().parent().siblings().find('input.description-modal');
            description.val('Getting data...');
            globalHubConn.server.getItemInfo(item.val()).done(function (result) {
                if (typeof result.description === 'undefined') {
                    save.attr('disabled', 'disabled');
                    createAlert('kititem_alert', 'Item ' + item.val() + ' not found.', 'alert-warning', true, '<strong>Conflict: </strong> ', '#kit_alerts');
                    description.val('ITEM NOT FOUND.');
                } else {
                    save.removeAttr('disabled');
                    deleteAlert('kititem_alert');
                    description.val(result.description);
                };
            });
        };
    });
});