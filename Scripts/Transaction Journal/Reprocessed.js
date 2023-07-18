// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    var reprocessedTable = $('#reprocedTransTable').DataTable({
        "dom": "trip",
        "iDisplayLength": 100,
        "processing": true,
        "serverSide": true,

        "ajax": {
            // function call to TransactionsController
            "url": "/Transactions/reprocedTrans",
            "data": function (d) {
                d.searchColumn = $('#selection5').val();
                d.searchString = $('#searchString5').val();
            },
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });

    $('#pageLength5').change(function () {
        reprocessedTable.page.len($(this).val());
        reprocessedTable.draw();
    });

    $('#reprocedTransTable').wrap('<div style="overflow-x:scroll"></div>');

    //typeahead for search input box
    var searchTypeaheadReproced = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: '/Transactions/nextSuggestionsTrans?query=%QUERY&column=',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            replace: function (url, query) {
                return '/Transactions/nextSuggestionsTrans?query=' + $('#searchString5').val() + '&table=6&column=' + $('#selection5').val();
            },
            cache: false
        }
    });
    searchTypeaheadReproced.initialize();
    $('#searchString5.typeahead').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "searchTypeaheadReproced",
        displayKey: 'value',
        source: searchTypeaheadReproced.ttAdapter()
    }).on('typeahead:selected', function () { $('#searchString5').trigger('input'); $('#searchString5').trigger('focusout'); })
    .on('typeahead:opened', function () { $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')) });

    //Handles Search Input for this table
    $('#searchString5').on('input', function (e) {
        if ($(this).val() == '') {
            $(this).typeahead('val', '');
        }
        reprocessedTable.draw();
    });

    $('#setDefaultReproced').click(function () {
        document.getElementById("reprocedDefaultForm").submit();
    });
});