// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var sortColumnName = "Event ID";
var sortColumnOrder = "DESC";

var eventLogTable;
var eventTimer;

var createTypeAhead = function (viewname, id) {
    // suggestions engine for the typeahead
    var eventLogTypeAhead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/EventLog/eventLogTypeAhead?'),
            replace: function (url, uriencodedstring) {
                if ($('#dateIgnore:checkbox:checked').length > 0) {
                    var sDate = new Date();
                    sDate.setYear(1990);
                    var eDate = new Date();
                    return url + 'query=' + $(id).val() + '&columnName=' + viewname + '&sDate=' + sDate.toDateString() + '&eDate=' + eDate.toDateString();
                } else {
                    return url + 'query=' + $(id).val() + '&columnName=' + viewname + '&sDate=' + $('#sDateFilterEL').val() + '&eDate=' + $('#eDateFilterEL').val();
                }

            },
            filter: function (list) {

                return $.map(list, function (dataObj) {
                    var type = typeof dataObj;
                    if (type == 'string') {
                        return { Value: dataObj };
                    }
                    else { return dataObj; }
                });
            },
            cache: false
        }
    });
    eventLogTypeAhead.initialize();
    //change this to id
    $(id).typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "eventLogTypeAhead",
        displayKey: 'Value',
        source: eventLogTypeAhead.ttAdapter()

    }).on('typeahead:selected', function (obj, data, name) {
        $(id).trigger('input').blur();
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width')).css('height', '300px').css('left', 'auto')
    });
};

$(document).ready(function () {
     eventTimer = mkTimer(function () {
        eventLogTable.draw();
    }, 200);
})



$('#dateIgnore').change(function () {
    if ($(this).prop('checked')) {
        $('#sDateFilterEL, #eDateFilterEL').attr('disabled', 'disabled');
    } else {
        $('#sDateFilterEL, #eDateFilterEL').removeAttr('disabled');
    };
    eventTimer.startTimer();
});

var EventLogCols = new Array();
$(document).ready(function () {
    $.each($('#EventLogCols').children(), function (index, element) {
        EventLogCols.push($(element).attr('value'));
    });

    createTypeAhead('Message', '#messageFilterEL');
    createTypeAhead('Event Location', '#eLocationEL');
    createTypeAhead('Name Stamp', '#nStampEL');
    createTypeAhead('Event Type', '#EventType')
    createTypeAhead('Event Code', '#EventCode')


    //Instantiates Jquery Table object
    eventLogTable = $('#eventTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "autowidth": false,
        "pageLength": 20,
        "columnDefs": [
            { 'width': "100px", 'visible': false, "targets": 8 }, { "targets": 1 }
        ],
        "rowCallback": function (row, data) {
            var maxlength = 46;
            var message = $(row).find('td').eq(1);
            if (message.text().length > maxlength) {
                message.text(message.text().substring(0, maxlength) + '...');
            };

            var notes = $(row).find('td').eq(6);
            if (notes.text().length > maxlength) {
                notes.text(notes.text().substring(0, maxlength) + '...');
            };
            var eventLength = 18;
            var event = $(row).find('td').eq(5);
            if (event.text().length > eventLength) {
                event.text(event.text().substring(0, eventLength) + '...');
            };
        },
        "ajax": {
            //Function that grabs Table Data
            "url": "/EventLog/getEventLog",
            "data": function (d) {
                d.messageFilter = $('#messageFilterEL').val();
                d.eventLocation = $('#eLocationEL').val();
                d.nameStamp = $('#nStampEL').val();
                if ($('#dateIgnore:checkbox:checked').length > 0) {
                    var startDate = new Date();
                    startDate.setYear(1990);
                    d.sDate = startDate.toDateString();
                    d.eDate = new Date().toDateString();
                } else {
                    d.sDate = $('#sDateFilterEL').val();
                    d.eDate = $('#eDateFilterEL').val();
                }
                d.transType = $('#EventType').val();
                d.transStatus = $('#EventCode').val();
                //filter here
                d.filter = (EventLogFilterMen == "" ? "" : EventLogFilterMen.getFilterString());
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });

    // wrap the table so if there's more needed space we get it.
    $('#eventTable').wrap('<div style="overflow-x:scroll"></div>');

    //Handles Page Length Selector for this table
    $('#pageLength').change(function () {
        eventLogTable.page.len($(this).val());
        eventTimer.startTimer();
    });

    // handles all inputs requerying the table.  sDate/eDate only work here in chrome (second handler below doesn't cover Chrome).
    $('#messageFilterEL, #eLocationEL, #nStampEL, #sDateFilterEL, #eDateFilterEL, #EventType, #EventCode').on('input', function () {
        eventTimer.startTimer();
    });

    $('#eDateFilterEL, #sDateFilterEL').val(setToToday());
    eventTimer.startTimer();
    $('#eDateFilterEL, #sDateFilterEL').on('change', function () {
        eventTimer.startTimer();
    });

    // redraws the table with new data
    $('#refreshTable').click(function () {
        eventTimer.startTimer();
    });

    // resets all filters
    $('#clearFilters').click(function () {
        $('#messageFilterEL').val('');
        $('#eLocationEL').val('');
        $('#nStampEL').val('');
        $('#sDateFilterEL').data("DateTimePicker").setDate(setToToday());
        $('#eDateFilterEL').data("DateTimePicker").setDate(setToToday());
        eventTimer.startTimer();
    });

    $('#exportRange').click(function () {
        var sCompareDate = $('#sDateFilterEL').val().split("/")
        var eCompareDate = $('#eDateFilterEL').val().split("/")
        sCompareDate = sCompareDate[2] + sCompareDate[0] + sCompareDate[1]
        eCompareDate = eCompareDate[2] + eCompareDate[0] + eCompareDate[1]
        var sDate = $('#sDateFilterEL').val();
        var eDate = $('#eDateFilterEL').val();
        if (sDate == '' || eDate == '') { alert('Both date fields must be filled out!'); return false; };
        if (sCompareDate > eCompareDate) { alert('Start date must be before end date!'); return false; };

        $.ajax({
            url: '/EventLog/singleExport',
            type: "GET",
            data: {
                sDate: sDate,
                eDate: eDate,
                nStamp: $('#messageFilterEL').val(),
                eLocation: $('#eLocationEL').val(),
                message: $('#messageFilterEL').val()
            },
            success: function (printed) {
                if (!printed) MessageModal('Error', 'There was an error during the export/preview process.')
                else MessageModal('Success', 'Export Complete');

            },
            error: function (xhr, ajaxOptions, thrownError) {
                MessageModal('Warning ' + title, 'An error occurred.  The requested print action may not have completed correctly.');
            }
        });
    });
});

