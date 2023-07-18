// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $(document.body).on('click', '#addNewScan', function () {
        document.getElementById('scanCodeContainer').insertAdjacentHTML('beforeend', '<div class="row" style="padding-top:10px;" id="newScanContainer"><div class="col-md-6"><div class="row">\
            <div class="col-md-6">\
            <input maxlength="50" type="text" class="form-control scan-edit inv-text" data-colname="[Scan Codes].[Scan Code]" placeholder="Scan Code" id="newScan_sc"></div><div class="col-md-6">\
            <div class="form-group has-feedback"><input maxlength="50" type="text" class="form-control modal-launch-style scan-modal scan-edit inv-text" data-colname="[Scan Codes].[Scan Type]" readonly id="newScan_st">\
            <i style="top:0;" class="glyphicon glyphicon-resize-full form-control-feedback scan-modal modal-launch-style"></i>\
            </div></div></div></div>\
            <div class="col-md-4"><div class="row"><div class="col-md-4"><select id="newScan_select" class="form-control scan-edit inv-text" data-colname="[Scan Codes].[Scan Range]"><option value="Yes">Yes</option>\
            <option value="No" selected="selected">No</option></select></div><div class="col-md-4"><input maxlength="9" type="text" class="form-control scan-edit inv-int" data-colname="[Scan Codes].[Start Position]" value="0" placeholder="Start Position" id="newScan_sp">\
            </div><div class="col-md-4"><input maxlength="9" type="text" class="form-control scan-edit inv-int" data-colname="[Scan Codes].[Code Length]" placeholder="Code Length" value="0" id="newScan_cl"></div></div></div>\
            <div class="col-md-1"><div class="pull-right"><button type="button" class="btn btn-danger remove-scan" id="newScan_remove" style="margin-left:5px;">\
            <span class="glyphicon glyphicon-remove"></span></button></div></div><div class="col-md-1"><div class="pull-right">\
            <button disabled="disabled" type="button" class="btn btn-primary save-scan" id="newScan_save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div></div></div>');
        $(this).attr('disabled', 'disabled');
    });

    // save enable
    $(document.body).on('input', '.scan-edit', function () {
        switch (this.id.split('_')[1]) {
            case 'sp':
            case 'cl':
                setNumericInRange($(this), 0, null);
                break;
            case 'select':
                if (this.value != 'Yes' && this.value != 'No') { this.value = 'Yes'; };
                break;
        };
        $('#' + this.id.split('_')[0] + '_save').removeAttr('disabled');
    });

    $(document.body).on('change', 'select .scan-edit', function () {
        $('#' + this.id.replace('_select', '_save')).removeAttr('disabled');
    });

    // save listeners
    $(document.body).on('click', '#newScan_save', function () {
        var scan = document.getElementById('newScan_sc').value;
        if (scan == '' || scan == null) { createAlert('scannull_alert', 'Scan Code must be specified in order to add to scan codes.', 'alert-warning', true, '<strong>Conflict:</strong> ', '#scancode_alerts'); } else { deleteAlert('scannull_alert'); };
        var stype = document.getElementById('newScan_st').value;
        var select = document.getElementById('newScan_select');
        var srange = select.options[select.selectedIndex].value;
        var sposition = document.getElementById('newScan_sp').value;
        var clength = document.getElementById('newScan_cl').value;
        var item = document.getElementById('UpdateItemNum').value;

        var currentID;
        var breakOut = false;
        $('#scanCodeContainer').children().each(function (index, childElem) {
            currentID = $(childElem).attr('id').split('_')[0];
            if (scan == $('#' + currentID + '_sc').val() &&
                srange == $('#' + currentID + '_select').val() &&
                sposition == $('#' + currentID + '_sp').val() &&
                clength == $('#' + currentID + '_cl').val()) {
                createAlert('savescanfail_alert', 'New Scan Code not saved!  Ensure that the scan code being added is not a duplicate and try again.', 'alert-warning', true, '<strong>Alert: </strong> ', '#scancode_alerts');
                breakOut = true;
            };
        });

        if (breakOut) {
            return;
        };

        invMasterHub.server.addScanCode(item, scan, stype, srange, sposition, clength).done(function (saved) {
            if (saved) {
                var index = scanCodeData['Scan Codes'].length;
                $('#newScanContainer').attr('id', index + '_container');
                $('#addNewScan').removeAttr('disabled');
                $('#newScan_sc').attr('id', index + '_sc');
                $('#newScan_st').attr('id', + index + '_st');
                $('#newScan_select').attr('id', index + '_select');
                $('#newScan_sp').attr('id', index + '_sp');
                $('#newScan_cl').attr('id', index + '_cl');
                $('#newScan_remove').attr('id', index + '_remove');
                $('#newScan_save').attr('disabled', 'disabled');
                $('#newScan_save').attr('id', index + '_save');
                deleteAlert('savescanfail_alert');
                scanCodeData['Scan Codes'].push(scan);
                scanCodeData['Scan Types'].push(stype);
                scanCodeData['Start Position'].push(sposition);
                scanCodeData['Code Length'].push(clength);
                scanCodeData['Scan Range'].push(srange);
            } else { createAlert('savescanfail_alert', 'New Scan Code not saved!  Ensure that the scan code being added is not a duplicate and try again.', 'alert-warning', true, '<strong>Alert: </strong> ', '#scancode_alerts'); };
        });
    });

    $(document.body).on('click', '.save-scan:not(#newScan_save)', function () {
        var index = this.id.split('_')[0];
        var item = document.getElementById('UpdateItemNum').value;
        // new values
        var newscan = document.getElementById(index + '_sc').value;
        var scantype = document.getElementById(index + '_st').value;
        var newrange = document.getElementById(index + '_select').value;
        var newstart = document.getElementById(index + '_sp').value;
        var codelength = document.getElementById(index + '_cl').value;
        // old values
        var oldscan = scanCodeData['Scan Codes'][index];
        var oldrange = scanCodeData['Scan Range'][index];
        var oldstart = scanCodeData['Start Position'][index];
        var oldcodelength = scanCodeData['Code Length'][index];

        if (newscan == '' || newscan == null) {
            createAlert('savescanfail_alert', 'Scan code not saved!  Scan code field must not be empty.', 'alert-warning', true, '<strong>Alert: </strong> ', '#scancode_alerts');
            return false;
        };
        if (scantype == '' || scantype == null) {
            createAlert('savescanfail_alert', 'Scan code not saved!  Scan type field must not be empty.', 'alert-warning', true, '<strong>Alert: </strong> ', '#scancode_alerts');
            return false;
        };
        if (newstart == '' || newstart == null || isNaN(parseInt(newstart, 10)) || !$.isNumeric(newstart)) {
            createAlert('savescanfail_alert', 'Scan code not saved!  Start position field must be an integer.', 'alert-warning', true, '<strong>Alert: </strong> ', '#scancode_alerts');
            return false;
        };
        if (codelength == '' || codelength == null || isNaN(codelength) || !$.isNumeric(codelength)) {
            createAlert('savescanfail_alert', 'Scan code not saved!  Scan code field must not be empty.', 'alert-warning', true, '<strong>Alert: </strong> ', '#scancode_alerts');
            return false;
        } else {
            deleteAlert('savescanfail_alert');
        };

        var currentID;
        var breakOut = false;
        $('#scanCodeContainer').children(':not(#' + index + '_container)').each(function (index, childElem) {
            currentID = $(childElem).attr('id').split('_')[0];
            if (newscan == $('#' + currentID + '_sc').val() &&
                newrange == $('#' + currentID + '_select').val() &&
                newstart == $('#' + currentID + '_sp').val() &&
                codelength == $('#' + currentID + '_cl').val()) {
                createAlert('savescanfail_alert', 'Scan Code not saved!  Ensure that the scan code being added is not a duplicate and try again.', 'alert-warning', true, '<strong>Alert: </strong> ', '#scancode_alerts');
                breakOut = true;
            };
        });

        if (breakOut) {
            return;
        };

        invMasterHub.server.updateScanCode(item, oldscan, newscan, scantype, oldrange, newrange, oldstart, newstart, oldcodelength, codelength).done(function (success) {
            if (success) {
                scanCodeData['Scan Codes'][index] = newscan;
                scanCodeData['Scan Range'][index] = newrange;
                scanCodeData['Start Position'][index] = newstart;
                scanCodeData['Code Length'][index] = codelength;
                scanCodeData['Scan Types'][index] = scantype;
                deleteAlert('savescanfail_alert');
                $('#' + index + '_save').attr('disabled', 'disabled');
            } else { createAlert('savescanfail_alert', 'Scan code not saved!  Please try again.', 'alert-warning', true, '<strong>Alert: </strong> ', '#scancode_alerts'); };
        });
    });

    // remove listeners
    $(document.body).on('click', '#newScan_remove', function () {
        $('#addNewScan').removeAttr('disabled');
        $('#newScanContainer').remove();
    });
    $(document.body).on('click', '.remove-scan:not(#newScan_remove)', function () {
        var item = document.getElementById('UpdateItemNum').value;
        var scode = document.getElementById(this.id.replace('_remove', '_sc')).value;
        var container = this.id.split('_')[0] + '_container';
        var result = confirm('Click OK to delete scan type ' + scode + ' from item ' + item);
        if (result) {
            invMasterHub.server.deleteScanCode(item, scode).done(function (success) {
                if (success) {
                    $('#' + container).remove();
                } else {
                    alert('Delete failed.  Please try again.');
                };
            });
        };
    });

    /*
        MODAL Scan Type
    */
    // remove scan code type
    $(document.body).on('click', '.remove-stype', function () {
        if (this.id == 'newStype_delete') { $('#newStype_container').remove(); $('#addScanType').removeAttr('disabled'); return false; };
        var index = this.id.split('_')[0];
        var stype = document.getElementById(index + '_stmodal').value;
        var result = confirm('Click OK to delete Scan Type ' + stype);
        if (result) {
            invMasterHub.server.deletePopUp(['Scan Type', stype]).done(function () {
                $('#' + index + '_stcontainer').remove();
            });
        };
    });

    // set scan code type
    $(document.body).on('click', '.set-stype', function () {
        var sender = document.getElementById('modalSender').value;
        var index = this.id.split('_')[0];
        document.getElementById(sender).value = document.getElementById(index + '_stmodal').value;
        activeModal = false;
        $('#myModal').modal('toggle');
        $('#' + sender).trigger('input');
    });

    // save scan code type
    $(document.body).on('click', '.save-stype', function () {
        var index = this.id.split('_')[0];
        var params;
        if (this.id == 'newStype_save') {
            params = ['Scan Type', 'New', document.getElementById('newStype_input').value];
        } else {
            params = ['Scan Type', document.getElementById(index + '_stmodal').attributes["name"].value, document.getElementById(index + '_stmodal').value];
        };
        var newType = params[1] == 'New';
        invMasterHub.server.updatePopUp(params).done(function (success) {
            if (success) {
                if (newType) {
                    var newIndex = $('#modalScanCodes_container').children().size();
                    while ($('#' + newIndex + '_stmodal').length > 0) { newIndex++ };
                    $('#newStype_container').attr('id', newIndex + '_stcontainer');
                    $('#newStype_input').attr('id', newIndex + '_stmodal');
                    $('#' + newIndex + '_stmodal').attr('name', params[2]);
                    $('#newStype_save').attr('id', newIndex + '_stsave');
                    $('#newStype_set').attr('id', newIndex + '_stset');
                    $('#' + newIndex + '_stset').removeAttr('disabled');
                    $('#' + newIndex + '_stset').addClass('set-stype');
                    $('#' + newIndex + '_stsave').attr('disabled', 'disabled');
                    $('#newStype_delete').attr('id', newIndex + '_stremove');
                    $('#addScanType').removeAttr('disabled');
                } else {
                    $('#' + index + '_stsave').attr('disabled', 'disabled');
                    $('#' + index + '_stmodal').attr('name', params[2]);
                };
            };
        });
    });

    // add new line to scan code types
    $(document.body).on('click', '#addScanType', function () {
        document.getElementById('modalScanCodes_container').insertAdjacentHTML('beforeend', "<div class='row' style='margin-bottom:5px;' id='newStype_container'><div class='col-md-9'><input id='newStype_input' class='form-control modalInput' value='' maxlength='50'/></div><div class='col-md-1'><button id='newStype_delete' type='button' data-toggle='tooltip' data-placement='right' title='Delete' class='btn btn-danger remove-stype'><span class='glyphicon glyphicon-remove'></span></button></div><div class='col-md-1'><button disabled id='newStype_set' type='button' data-toggle='tooltip' data-placement='right' title='Set as Value' class='btn btn-primary'><span class='glyphicon glyphicon-edit'></span></button></div><div class='col-md-1'><button disabled  id='newStype_save' type='button' data-toggle='tooltip' data-placement='right' title='Save Changes' class='btn btn-primary save-stype'><span class='glyphicon glyphicon-floppy-disk'></span></button></div></div>");
        $('#addScanType').attr('disabled', 'disabled');
    });
});