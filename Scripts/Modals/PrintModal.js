// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the print modal for inv map
var trigger_print_invmap_modal;
// returns the number of locations selected by the current range of locations and the unique checkbox
var qtySelectedQuery;
// switches the print modal depending on selection and modal sender
var switchPrintView;

$(document).ready(function () {
    trigger_print_invmap_modal = function () {
        $('#print_label').html('Print Label(s)');
        if ($('.activeRow').length > 0) {
            var selected = $('.activeRow input[name="Location"]').val();
            $('#print_body').html('<strong>Would you like to print the selected location (' + selected + '), or a range of locations?');
            appendstring = '<button class="btn btn-primary" id="PrintSelected">Print Selected</button><button class="btn btn-primary" id="PrintChangeRange">Print Range</button><button class="btn btn-primary" data-dismiss="modal">Cancel</button>';
            $('#print_footer').html(appendstring);
        } else {
            switchPrintView();
        };
        $('#print_modal').modal('show');
    };

    // handles triggering the inv map print modal
    $(document.body).on('click', '.print-invmap', function () {
        trigger_print_invmap_modal();
    });

    // Handles printing a range or a selected item
    $(document.body).on('click', '#PrintSelected, #PrintRange', function () {
        var groupLikeLoc = false;
        var invMapID = $('.activeRow input[name="Inv Map ID"]').val();
        var startLoc = $('#BeginLocationPrint').val();
        var endLoc = $('#EndLocationPrint').val();

        // if we were sent from a single entry or from a range of labels
        if ($('#PrintGroupBy').length == 0) {
            groupedLikeLoc = false;
            startLoc = "";
            endLoc = "";
        } else {
            groupLikeLoc = $('#PrintGroupBy').prop("checked");
            invMapID = -1;
        };

        inventoryMap.server.printIMReport(invMapID, groupLikeLoc, startLoc, endLoc).done(function () {
            $('#print_modal').modal('hide');
        });
    });

    // Handles switching the modal's data to a range of locations as opposed to a single entry
    $(document.body).on('click', '#PrintChangeRange', function () {
        switchPrintView();
    });

    // recalculates the selected number of entries to print location labels for
    $(document.body).on('input', '#BeginLocationPrint, #EndLocationPrint', function () { qtySelectedQuery(); });
    $(document.body).on('change', '#PrintGroupBy', function () { qtySelectedQuery(); });
});

qtySelectedQuery = function () {
    var unique = $('#PrintGroupBy').prop("checked");
    var beginLoc = $('#BeginLocationPrint').val();
    var endLoc = $('#EndLocationPrint').val();
    $('#PrintQtySelected').html('Getting data...');
    globalHubConn.server.selQtySelected(beginLoc, endLoc, unique).done(function (result) {
        if (result == -1) {
            // begin/end location wrong or not filled in or error occurred
            $('#PrintQtySelected').html(0);
        } else {
            $('#PrintQtySelected').html(result);
        };
    });
};

switchPrintView = function () {
    var appendstring = '<div class="row">' +
                '<div class="col-md-6"><label>Begin Location</label><input type="text" class="form-control" id="BeginLocationPrint" maxlength="50" /></div>' +
                '<div class="col-md-6"><label>End Location</label><input type="text" class="form-control" id="EndLocationPrint" maxlength="50" /></div>' +
                '</div>' +
                '<div class="row">' +
                '<div class="col-md-6"><label>Qty Selected</label> <span id="PrintQtySelected" class="label label-default">0</span></div>' +
                '<div class="col-md-6"><label>Group by Location <input type="checkbox" id="PrintGroupBy" /></label></div>' +
                '</div>';
    $('#print_body').html(appendstring);

    appendstring = '<button class="btn btn-primary" id="PrintRange">Print</button><button class="btn btn-primary" data-dismiss="modal">Cancel</button>';
    $('#print_footer').html(appendstring);

    var begin = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getbegin
            url: ('/Typeahead/getLocationBegin?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#BeginLocationPrint').val() + '&unique=' + $('#PrintGroupBy').prop('checked');
            },
            cache: false
        }
    });
    begin.initialize();

    $('#BeginLocationPrint').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "begin",
        displayKey: 'Location',
        source: begin.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:100%;">Location</p>'
        }
    }).on('typeahead:selected', function (obj, data, name) {
        $('#BeginLocationPrint').trigger('input');
    });;

    var end = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getend
            url: ('/Typeahead/getLocationEnd?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#EndLocationPrint').val() + '&unique=' + $('#PrintGroupBy').prop('checked') + '&beginLoc=' + $('#BeginLocationPrint').val();
            },
            cache: false
        }
    });

    end.initialize();

    $('#EndLocationPrint').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "end",
        displayKey: 'Location',
        source: end.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:100%;">Location</p>'
        }
    }).on('typeahead:selected', function (obj, data, name) {
        $('#EndLocationPrint').trigger('input');
    });
};