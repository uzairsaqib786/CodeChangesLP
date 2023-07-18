// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $(document.body).on('click', '#printListIM', function () {
        if ($('tr.active').length > 0) {
            $('.print-range').hide();
            $('.print-selected').show();
        } else {
            $('.print-range').show();
            $('.print-selected').hide();
        };
        $('#print_modal').modal('show');
    });

    $(document.body).on('click', '#PrintRange', function () {
        if ($('.print-selected').is(':hidden')) {
            // actually print
            //reportsHub.server.printIMReport(0, $('#groupBy').data('toggles').active, $('#BeginLocation').val(), $('#EndLocation').val()).done(function () {
            //    $('#print_modal').modal('hide');
            //});
            title = 'Location Label';
            getLLPreviewOrPrint('/InventoryMap/printIMReport', {
                invMapID: 0,
                groupLikeLoc: $('#groupBy').data('toggles').active,
                startLoc: $('#BeginLocation').val(),
                endLoc: $('#EndLocation').val()
            }, true,'report', title);
        } else {
            // show the modal for range
            $('.print-range').show();
            $('.print-selected').hide();
        };
    });

    $(document.body).on('click', '#PrintSelected', function () {
        var row = $('tr.active');
        var id = row.find(':nth-child(' + (columns.indexOf('Inv Map ID') + 1) + ')').html();
        //reportsHub.server.printIMReport(id, false, '', '').done(function () {
        //    $('#print_modal').modal('hide');
        //});
        title = 'Location Label';
        getLLPreviewOrPrint('/InventoryMap/printIMReport', {
            invMapID: id,
            groupLikeLoc: false,
            startLoc: '',
            endLoc: ''
        }, true, 'report', title);
        $('#print_modal').modal('toggle');
    });

    $('#BeginLocation, #EndLocation').on('input', function () {
        getQtySelected($('#groupBy').data('toggles').active);
    });
    $('#groupBy').on('toggle', function (e, checked) {
        getQtySelected(checked);
    });

    function getQtySelected(checked) {
        globalHubConn.server.selQtySelected($('#BeginLocation').val(), $('#EndLocation').val(), checked).done(function (result) {
            if (result == -1) {
                // begin/end location wrong or not filled in or error occurred
                $('#QtySelected').html(0);
            } else {
                $('#QtySelected').html(result);
            };
        });
    };

    var begin = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getbegin
            url: ('/Typeahead/getLocationBegin?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#BeginLocation').val() + '&unique=' + $('#groupBy').data('toggles').active;
            },
            cache: false
        }
    });
    begin.initialize();

    $('#BeginLocation').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "begin",
        displayKey: 'Location',
        source: begin.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:100%;">Location</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{Location}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        $('#BeginLocation').trigger('input');
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')).css('left', 'auto');
    });

    var end = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getend
            url: ('/Typeahead/getLocationEnd?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#EndLocation').val() + '&unique=' + $('#groupBy').data('toggles').active + '&beginLoc=' + $('#BeginLocation').val();
            },
            cache: false
        }
    });

    end.initialize();

    $('#EndLocation').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "end",
        displayKey: 'Location',
        source: end.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:100%;">Location</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{Location}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        $('#EndLocation').trigger('input');

    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')).css('left', 'auto');
    });
});