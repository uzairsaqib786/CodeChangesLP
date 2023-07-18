// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // add a window handler to resize the container appropriately
    addResize(function() {
        $('#ScheduleContainer').css({
            'max-height': $(window).height() * 0.65,
            'overflow-y': 'scroll'
        });
    });

    // show the scheduler modal
    $('#invMapExportSched').click(function () {
        $('#invmap_scheduler_modal').modal('show');
        var sch = $('#ScheduleContainer');
        sch.html('');
        importExportHub.server.getIEInvMapExportConfig().done(function (result) {
            for (var x = 0; x < result.rows.length; x++) {
                sch.append(mkExportRow(result.rows[x]));
            };
            // javascript uses a date format MM/DD/YYYY"T"HH:mm:ss.mmmm, so we want to remove the literal T from the string to display it properly
            $('#LastExport').val(result.last.replace('T', ' '));
            // activate the dynamically created tooltips because the original call to activate them does not apply to new instances
            sch.find('[data-toggle="tooltip"]').tooltip();
        });
    });

    /*
        Gets a single row of data from the parameter row.
        Parameters:
            row: jQuery selector on the <div class="row"> element which represents an entire row of the scheduler
        Returns:
            JS Associative Array which is interpreted by the server side as a dictionary(string, string).
            The keys available are: 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday', 'Export Hour', 'Export Minute', 'Export AMPM'
    */
    function getRowData(row) {
        var kids = row.children();
        var data = {};
        var props = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday', 'Export Hour', 'Export Minute', 'Export AMPM'];
        for (var x = 0; x < 7; x++) {
            data[props[x]] = $(kids[x]).find('input').prop('checked');
        };
        for (var x = 7; x < 9; x++) {
            data[props[x]] = $(kids[x]).find('input').val();
        };
        data[props[9]] = $(kids[9]).find('select').val();
        data['EMS_ID'] = row.data('id');
        return data;
    };

    /*
        Enables the save button for a particular row.  Can only be used with context because of the use of this, so it can only be attached to an event on an input/select/button two levels down from the parent row
    */
    function enableSave() {
        $(this).parent().parent().find('.save-export').removeAttr('disabled');
    };

    $(document.body).on('change', '.day,.am-pm', enableSave);
    $(document.body).on('input', '.export-minute,.export-hour', enableSave);

    // adds a new definition for an export for inventory map
    $('#AddNewExport').click(function () {
        $('#ScheduleContainer').append(mkExportRow({
            'Monday': false, 'Tuesday': false, 'Wednesday': false, 'Thursday': false, 'Friday': false, 'Saturday': false, 'Sunday': false,
            'Export Hour': '1', 'Export Minute': '00', 'Export AMPM': 'AM', 'EMS_ID': -1
        }));
        $(this).attr('disabled', 'disabled');
    });

    // saves the export definition on a button click
    $(document.body).on('click', '.save-export', function () {
        var $this = $(this);
        var row = getRowData($this.parent().parent());
        importExportHub.server.saveInvMapSchedule(row).done(function (saved) {
            if (parseInt(saved) <= 0) MessageModal('Error', 'An error occurred while attempting to save the record.  Check the error log for details.');
            else if (parseInt(row['EMS_ID']) <= 0) {
                $this.parent().parent().data('id', saved);
                $this.attr('disabled', 'disabled');
                $('#AddNewExport').removeAttr('disabled');
            };
        });
    });

    // deletes an export definition after receiving user confirmation
    $(document.body).on('click', '.delete-export', function () {
        if (confirm('Click OK to delete this export record.')) {
            var row = $(this).parent().parent();
            var id = row.data('id');
            if (id > 0) {
                importExportHub.server.delIEInvMapSchedule(id).done(function (deleted) {
                    if (!deleted) MessageModal('Error', 'An error occurred while attempting to delete the record.');
                    else row.remove();
                });
            } else {
                row.remove();
                $('#AddNewExport').removeAttr('disabled');
            };
        };
    });

    /*
        Handles the focusout event on the export hour field.  There is another handler on each of these which restricts input through the use of setNumericInRange which limits input to being
            an integer between 0 and 23.  Once the user has left the input we want to check in this function whether we need to convert the input time from military/24 hour time to 12 hour time.
    */
    $(document.body).on('focusout', '.export-hour', function () {
        var $this = $(this);
        var currVal = $this.val().trim();
        // if the value is left empty then we will default to 12.
        if (currVal == '') currVal = '12';
        // if the value is in military format (> 12) then we want to set it to the standard 12 hour format.
        // > 12 then we must subtract 12 and make it PM
        if (parseInt(currVal) > 12) {
            currVal = (parseInt(currVal) - 12);
            $this.parent().parent().find('.am-pm').val('PM');
        // 0 is a special case, midnight 0 -> 12 AM
        } else if (parseInt(currVal) == 0) {
            currVal = 12;
            $this.parent().parent().find('.am-pm').val('AM');
        };
        // set the new (or unchanged, valid) value
        $this.val(currVal);
    });

    // format the export minute field to look like "mm" instead of just "m" for minute.
    $(document.body).on('focusout', '.export-minute', function () {
        var $this = $(this);
        // if the value is empty then we default to 00
        if ($this.val().trim() == '') $this.val('00');
        // if the value is numeric and the value is less than 10 and the value length is 1 then we need to prepend a 0 in order to fit our defined format
        else if ($.isNumeric($this.val()) && parseInt($this.val()) < 10 && String($this.val()).length == 1) $this.val('0' + $this.val());
    });

    /*
        Creates an html representation of a row of the export definition.
        Parameters:
            row: Associative Array / Dictionary(string, object) which defines a row's properties.
                The keys available are: 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday', 'Export Hour', 'Export Minute', 'Export AMPM'
    */
    function mkExportRow(row) {
        var days = '';
        var week = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
        for (var x = 0; x < week.length; x++) {
            days += ('<div class="col-md-1">\
                        <input type="checkbox" class="form-control input-sm day ' + week[x].substring(0, 3).toLowerCase() + '" ' + (row[week[x]] ? 'checked="checked"' : '') + '" />\
                    </div>');
        };
        return '<div class="row top-spacer multiline-striped" data-id="' + row['EMS_ID'] + '">' + days +
                    '<div class="col-md-1">\
                        <input type="text" class="form-control input-sm export-hour" value="' + row['Export Hour'] + '" oninput="setNumericInRange($(this), 0, 23);" maxlength="2" />\
                    </div>\
                    <div class="col-md-1">\
                        <input type="text" class="form-control input-sm export-minute" value="' + row['Export Minute'] + '" oninput="setNumericInRange($(this), 0, 60);" maxlength="2" />\
                    </div>\
                    <div class="col-md-1">\
                        <select class="form-control input-sm am-pm">\
                            <option ' + (row['Export AMPM'].toLowerCase() == 'am' ? 'selected="selected"': '') + '>AM</option>\
                            <option ' + (row['Export AMPM'].toLowerCase() == 'pm' ? 'selected="selected"' : '') + '>PM</option>\
                        </select>\
                    </div>\
                    <div class="col-md-1">\
                        <button class="btn btn-danger delete-export" data-toggle="tooltip" data-placement="left" data-original-title="Delete Record"><span class="glyphicon glyphicon-remove"></span></button>\
                    </div>\
                    <div class="col-md-1">\
                        <button class="btn btn-primary save-export" disabled="disabled" data-toggle="tooltip" data-placement="left" data-original-title="Save Record"><span class="glyphicon glyphicon-floppy-disk"></span></button>\
                    </div>\
                </div>';
    };
});