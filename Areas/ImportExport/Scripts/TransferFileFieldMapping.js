// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // add a handler to the window so that when we resize the container resizes too
    addResize(function () {
        $('#XferContainer').css({
            'max-height': $(window).height() * 0.65,
            'overflow-y': 'scroll'
        });
    });

    // gets the data attribute xfer-type from #XferTransField
    function getXferType() {
        return $('#XferTransField').children(':selected').data('xfer-type').toLowerCase();
    };

    // enable the save buttons when there has been a change
    $(document.body).on('input', '#XferContainer input', enableSave);
    $(document.body).on('change', '#XferContainer input:checkbox', enableSave);

    function enableSave() {
        $(this).parent().parent().find('.save-mapping').removeAttr('disabled');
    };

    // calculate the end position based on input and assign it
    $(document.body).on('input', '.start-position,.field-length', function () {
        var $this = $(this);
        var row = $this.parent().parent();
        var sp = row.find('.start-position').val();
        var fl = row.find('.field-length').val();
        if ($.isNumeric(sp) && $.isNumeric(fl)) {
            row.find('.end-position').val(parseInt(sp) + parseInt(fl) - 1);
        };
    });

    // save the mapping
    $(document.body).on('click', '.save-mapping', function () {
        var $this = $(this);
        var row = $this.parent().parent();
        importExportHub.server.updateFieldMappings(getSingleRow(row), $('#XferTransField').val(), getXferType(), row.data('new')).done(function (result) {
            if (!result) MessageModal('Error', 'An error occurred while attempting to save the field mapping.  Check the error log for details.');
            else {
                $this.attr('disabled', 'disabled');
                row.data('new', false);
            };
        });
    });

    // remove a blank entry
    $(document.body).on('click', '.clear-blank', function () {
        if (confirm('Click OK to remove this mapping.')) {
            var row = $(this).parent().parent();
            // if it's a new entry then we don't have to make a hub call to delete it
            if (row.data('new')) row.remove();
            else {
                importExportHub.server.deleteXferMap(getSingleRow(row), $('#XferTransField').val(), getXferType()).done(function (deleted) {
                    if (deleted) row.remove(); 
                    else MessageModal('Error', 'An error occurred while attempting to delete the row');
                });
            };
        };
    });

    $('#transFieldMap').click(function () {
        $('#transfer_field_modal').modal('show');
    });

    $('#UpdateAllMaps').click(function () {
        var transType = $('#XferTransField').val().toLowerCase();
        var xferType = getXferType();
        // no audits, inventory and inventory map have special handling server side, all other transaction types are handled the same way.
        if (transType != 'audit' && transType != '' && transType != '(none)') {
            importExportHub.server.updateMapAll(transType, xferType).done(function (updated) {
                if (!updated) MessageModal('Error', 'An error occurred while attempting to update all other maps. Check the error log for details.');
            });
        };
    });

    // add a placeholder, blank row so that non PickPro fields can be ignored
    $('#AddBlank').click(function () {
        var xfer = $('#XferContainer');
        xfer.prepend(mkXferRow({
            'Xfer Fieldname': 'Blank',
            'Start Position': 0,
            'Field Length': 0,
            'End Position': 0,
            'Pad Character': '',
            'Pad From Left': false,
            'Field Type': 'Text',
            'Import Format': '',
            'Export Format': ''
        }, true));
        if (getXferType() == 'import') $('.import-hide').hide();
        else $('.import-hide').show();
    });

    // previous-value is used to make sure that switching between transaction types doesn't leave unsaved changes
    $('#XferTransField').data('previous-value', '(None)');
    $('#XferTransField').change(function () {
        var $this = $(this);
        var newValue = $this.val();
        var type = getXferType();
        if ($('.save-mapping:not([disabled])').length > 0 && !confirm('There are unsaved changes.  Click OK to discard these changes and switch to the selected transaction type.')) {
            $this.val($this.data('previous-value'));
            return false;
        } else {
            $this.data('previous-value', newValue);
            var xfer = $('#XferContainer');
            xfer.html('');
            importExportHub.server.getFieldMappings(newValue, type).done(function (result) {
                for (var x = 0; x < result.length; x++) {
                    xfer.append(mkXferRow(result[x], false));
                };
                xfer.find('[data-toggle="tooltip"]').tooltip();
                $('#IEFormatLabel').text(type.toTitleCase() + ' Format');
                if (type == 'import') $('.import-hide').hide();
                else $('.import-hide').show();
            });
        };
    });

    // sort our entries based on the id of the element clicked
    $('#sortAlpha,#sortPosition').click(function () {
        var rows = getXferRows();
        rows.sort(this.id == 'sortAlpha' ? sortAlpha : sortPosition);
        var xfer = $('#XferContainer');
        xfer.html('');
        for (var x = 0; x < rows.length; x++) {
            xfer.append(mkXferRow(rows[x], false));
        };
        xfer.find('[data-toggle="tooltip"]').tooltip();
    });

    // sort alphabetically and then numerically based on start position
    function sortAlpha(a, b) {
        var fA = a['Xfer Fieldname'];
        var fB = b['Xfer Fieldname'];
        if (fA > fB) return 1;
        else if (fA < fB) return -1;
        else {
            var sA = a['Start Position'];
            var sB = b['Start Position'];
            if (sA > sB) return 1;
            else if (sA < sB) return -1;
            else return 0;
        };
    };

    // sort numerically on start position and then alphabetically
    function sortPosition(a, b) {
        var sA = a['Start Position'];
        var sB = b['Start Position'];
        if (sA > sB) return 1;
        else if (sA < sB) return -1;
        else {
            var fA = a['Xfer Fieldname'];
            var fB = b['Xfer Fieldname'];
            if (fA > fB) return 1;
            else if (fA < fB) return -1;
            else return 0;
        };
    };

    /*
        Gets a single row's data in a dictionary structure
        Params: row: jQuery selector of the html element representing a row
        Returns: Dictionary(string, string) of columns with their values
        Available columns: 'Xfer Fieldname', 'Start Position', 'Field Length', 'End Position', 'Pad Character', 'Pad From Left', 'Field Type', 'Import Format', 'Export Format'
            'New Entry' - Not a sql server variable, only used to determine whether to insert or update in sql
    */
    function getSingleRow(row) {
        var cols = ['Xfer Fieldname', 'Start Position', 'Field Length', 'End Position', 'Pad Character', 'Pad From Left', 'Field Type'];
        var item = {};
        for (var y = 0; y < cols.length; y++) {
            if (y == 0 || y == 6) {
                item[cols[y]] = row.children(':nth-child(' + (y + 1) + ')').html();
            } else if (y == 5) {
                item[cols[y]] = row.children(':nth-child(' + (y + 1) + ')').children().is(':checked');
            } else {
                item[cols[y]] = row.children(':nth-child(' + (y + 1) + ')').children().val();
            };
        };
        var format = row.find('.ie-format').val();
        if (getXferType() == 'import') {
            item['Import Format'] = format;
            item['Export Format'] = null;
        } else {
            item['Export Format'] = format;
            item['Import Format'] = null;
        };
        item['New Entry'] = row.data('new');
        return item;
    };

    /*
        Gets data for all rows that are currently loaded.  Used for sorting.
    */
    function getXferRows() {
        var items = new Array();
        var rows = $('#XferContainer').children();
        for (var x = 0; x < rows.length; x++) {
            items.push(getSingleRow($(rows[x])));
        };
        return items;
    };

    /*
        Makes an html representation of a row from the data provided in the row parameter
        Params: row: dictionary(string, object)
                isNew: boolean, whether the row is a new (blank) entry or is an existing one
    */
    function mkXferRow(row, isNew) {
        var isBlank = setNullEmptyStr(row['Xfer Fieldname']).toLowerCase() == 'blank';
        return '<div class="row multiline-striped ' + (isBlank ? 'blank' : '') + '" data-new="' + isNew + '"><div class="col-md-1">' +
                     setNullEmptyStr(row['Xfer Fieldname']) + 
                 '</div>\
                <div class="col-md-1">\
                    <input type="text" class="form-control input-sm start-position" oninput="setNumericInRange($(this), 0, SqlLimits.numerics.int)" value="' + setNullEmptyStr(row['Start Position']) + '" />\
                </div>\
                <div class="col-md-1">\
                    <input type="text" class="form-control input-sm field-length" oninput="setNumericInRange($(this), 0, SqlLimits.numerics.int)" value="' + setNullEmptyStr(row['Field Length']) + '" />\
                </div>\
                <div class="col-md-1">\
                    <input type="text" class="form-control input-sm end-position" disabled="disabled" value="' + setNullEmptyStr(row['End Position']) + '" />\
                </div>\
                <div class="col-md-1 import-hide">\
                    <input type="text" class="form-control input-sm pad-char" value="' + setNullEmptyStr(row['Pad Character']) + '" maxlength="1" />\
                </div>\
                <div class="col-md-1">\
                    <input type="checkbox" class="form-control input-sm pad-left" ' + (setNullEmptyStr(row['Pad From Left']).toLowerCase() == 'true' ? 'checked="checked"' : '') + ' />\
                </div>\
                <div class="col-md-2">' + 
                    setNullEmptyStr(row['Field Type']) + 
                '</div>\
                <div class="col-md-2">\
                    <input type="text" class="form-control input-sm ie-format" value="' + (getXferType() == 'export' ? setNullEmptyStr(row['Export Format']) : setNullEmptyStr(row['Import Format'])) + '" maxlength="50" />\
                </div>\
                <div class="col-md-1">\
                    ' + (isBlank ? '<button class="btn btn-danger clear-blank" data-toggle="tooltip" data-placement="top" data-original-title="Clear Blank"><span class="glyphicon glyphicon-remove"></span></button>' : '') + 
                '</div>\
                <div class="col-md-1">\
                    <button class="btn btn-primary save-mapping" disabled="disabled"><span class="glyphicon glyphicon-floppy-disk"></span></button>\
                </div>\
            </div>';
    };
});