// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // add a resize window handler
    addResize(function () {
        $('#FP_Setup').css({
            'max-height': $(window).height() * 0.65,
            'overflow-y': 'scroll'
        });
    });

    $('#XferSetup').click(function () {
        $('#file_path_modal').modal('show');
        getFilePathSetup();
    });

    $(document.body).on('input', '.fp-row input', enableSave);
    $(document.body).on('change', '.fp-row input', enableSave);

    // enable save using context, must be used as a handler for an input, etc
    function enableSave() {
        var $this = $(this);
        // if we checked the active checkbox we need to validate it before we can enable the save button.  Custom handler for this is on click of .is-active
        if (!$this.hasClass('is-active')) {
            $this.parent().parent().parent().find('.save-ftp').removeAttr('disabled');
        };
    };

    // special check for enabling the save button or disallowing setting of the checkbox when certain conditions are not met
    $(document.body).on('click', '.is-active', function () {
        var $this = $(this);
        var transType = $this.parent().parent().parent().find('.trans-type').val().toLowerCase();
        var save = $this.parent().parent().parent().find('.save-ftp');
        if ($this.is(':checked') && transType == 'complete' && $('[name="ExportFileType"]').val().toLowerCase() != 'table') {
            save.attr('disabled', 'disabled');
            importExportHub.server.checkSetActiveFilePathSetup().done(function (result) {
                if (!result.success) {
                    MessageModal('Error', 'An error occurred while attempting to verify the validity of Active being set to true for Complete records.  Check the error log for details.');
                    $this.removeProp('checked');
                } else if (result.allowSet == 0) {
                    MessageModal('Error', 'You must first enter the Field Mapping to use when exporting the Complete transaction type.');
                    $this.removeProp('checked');
                };
                save.removeAttr('disabled');
            });
        } else save.removeAttr('disabled');
    });

    // save the record and disable the save button if successful
    $(document.body).on('click', '.save-ftp', function () {
        var row = $(this).parent().parent().parent();
        var data = getRowData(row);
        importExportHub.server.saveFilePathSetup(data).done(function (saved) {
            if (!saved) MessageModal('Error', 'An error occurred while attempting to save the file path setup entry.  Check the error log for details.');
            else {
                setFilePref(data['Export Filename']);
                row.find('.save-ftp').attr('disabled', 'disabled');
            };
        });
    });

    /*
        When saving we must check on other inputs that may be changed by the action.  With certain preferences we need to change their export filenames or import/export extensions
    */
    function setFilePref(exportFilename) {
        // if the preference for Export File Type is csv then we need to change each of them to match, EXCEPT inventory and inventory map
        var exportFileType = $('[name="ExportFileType"]').val().toLowerCase();
        if (exportFileType == 'csv') {
            var filenames = $('.export-filename');
            for (var x = 0; x < filenames.length; x++) {
                var fe = $(filenames[x]);
                var transType = fe.parent().parent().parent().find('.trans-type').val();
                if (transType != 'inventory' && transType != 'inventory map') {
                    fe.val(exportFilename);
                };
            };
        };

        // if the preference for Export File Type is fixed field then we need to change each of the transaction types (pick, put away, count) to use csv as the export extension
        if (exportFileType != 'fixed field') {
            var fileExts = $('.export-ext');
            for (var x = 0; x < fileExts.length; x++) {
                var fe = $(fileExts[x]);
                var transType = fe.parent().parent().parent().find('.trans-type').val();
                if (['pick', 'put away', 'count'].indexOf(transType) > -1) {
                    fe.val('csv');
                };
            };
        };

        // if the preference for Import File Type is fixed field then we need to set each of the transaction types (pick, put away, count) to csv as the export extension
        var importFileType = $('[name="ImportFileType"]').val().toLowerCase();
        if (importFileType != 'fixed field') {
            var fileExts = $('.import-ext');
            for (var x = 0; x < fileExts.length; x++) {
                var fe = $(fileExts[x]);
                var transType = fe.parent().parent().parent().find('.trans-type').val();
                if (['pick', 'put away', 'count'].indexOf(transType) > -1) {
                    fe.val('csv');
                };
            };
        };
    };

    /*
        Gets a dictionary structure from the jQuery selector parameter row.
    */
    function getRowData(row) {
        var cols = ['Transaction Type', 'xfer Append', 'Import File Path', 'Active', 'File Extension Default', 'Host Type', 'Export to File', 'Export File Path', 'Export Filename', 'Export Extension'];
        var data = {};
        for (var x = 0; x < cols.length; x++) {
            data[cols[x]] = getValue(row.find('[data-input="' + (x + 1) + '"]'));
        };
        return data;
    };

    /*
        Returns the value of the jQuery selector parameter input.
            If the input is a checkbox: returns boolean
            If the input is an input: returns value
            Otherwise returns the html of the element
    */
    function getValue(input) {
        if (input.attr('type') == 'checkbox') return input.is(':checked');
        else if (input.attr('type') == 'text') return input.val();
        else return input.html();
    };

    /*
        Initializes the file path setup modal
    */
    function getFilePathSetup() {
        importExportHub.server.getXferFilePathSetup().done(function (result) {
            var fp = $('#FP_Setup');
            fp.html('');
            for (var x = 0; x < result.length; x++) {
                fp.append(mkPathRow(result[x]));
            };
            fp.find('[data-toggle="tooltip"]').tooltip();
        });
    };

    /*
        Makes an html representation of a row from the dictionary provided in the parameter row.
    */
    function mkPathRow(row) {
        var transType = setNullEmptyStr(row['Transaction Type']);
        var importDisabled = '';
        switch (transType.toLowerCase()) {
            case 'adjustment':
            case 'location change':
            case 'complete':
                importDisabled = 'disabled="disabled"';
        };
        return '<div class="fp-row multiline-striped">\
                    <div class="row">\
                        <div class="col-md-2">\
                            <label>Transaction Type</label>\
                            <input type="text" class="form-control input-sm trans-type" disabled="disabled" value="' + transType + '" data-input="1" maxlength="50" />\
                        </div>\
                        <div class="col-md-2">\
                            <label>Use Date/Time for Filenames</label>\
                            <input type="checkbox" class="form-control datetime-file input-sm" ' + (setNullEmptyStr(row['xfer Append']) == 'true' ? 'checked' : '') + ' data-input="2" />\
                        </div>\
                        <div class="col-md-4">\
                            <label>Import File Path</label>\
                            <input type="text" class="form-control input-sm import-path" ' + importDisabled + ' value="' + setNullEmptyStr(row['Import File Path']) + '" data-input="3" maxlength="255" />\
                        </div>\
                        <div class="col-md-2">\
                            <label>Active</label>\
                            <input type="checkbox" class="form-control input-sm is-active" ' + (setNullEmptyStr(row['Active']) == 'true' ? 'checked': '') + ' data-input="4" />\
                        </div>\
                        <div class="col-md-1">\
                            <label>Import Ext.</label>\
                            <input type="text" class="form-control input-sm import-ext" value="' + setNullEmptyStr(row['File Extension Default']) + '" data-input="5" maxlength="50" />\
                        </div>\
                    </div>\
                    <div class="row">\
                        <div class="col-md-2">\
                            <label>Host Type</label>\
                            <input type="text" class="form-control input-sm host-type" value="' + setNullEmptyStr(row['Host Type']) + '" data-input="6" maxlength="50" />\
                        </div>\
                        <div class="col-md-2">\
                            <label>Export to File/Table</label>\
                            <input type="checkbox" class="form-control input-sm export-filetable" ' + (setNullEmptyStr(row['Export to File']) == 'true' ? 'checked' : '') + ' data-input="7" maxlength="255" />\
                        </div>\
                        <div class="col-md-4">\
                            <label>Export File Path</label>\
                            <input type="text" class="form-control input-sm export-path" ' + (transType.toLowerCase() == 'inventory' ? 'disabled="disabled"' : '') + ' value="' + setNullEmptyStr(row['Export File Path']) + '" data-input="8" maxlength="255" />\
                        </div>\
                        <div class="col-md-2">\
                            <label>Export Filename</label>\
                            <input type="text" class="form-control input-sm export-filename" value="' + setNullEmptyStr(row['Export Filename']) + '" data-input="9" maxlength="255" />\
                        </div>\
                        <div class="col-md-1">\
                            <label>Export Ext.</label>\
                            <input type="text" class="form-control input-sm export-ext" value="' + setNullEmptyStr(row['Export Extension']) + '" data-input="10" maxlength="3" />\
                        </div>\
                        <div class="col-md-1">\
                            <label style="visibility:hidden;" class="btn-block">Save</label>\
                            <button class="btn btn-primary btn-sm save-ftp" disabled="disabled" data-toggle="tooltip" data-original-title="Save Changes" data-placement="top"><span class="glyphicon glyphicon-floppy-disk"></span></button>\
                        </div>\
                    </div>\
                </div>';
    };
});