// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/****************************************************************************
    Table declarations
*****************************************************************************/ 
var currentView = "Order Status"
var openTransTempTable;
var orderStatTable;
var transTable;
var openTable;

//Sets Select Values for Column names
var setColumnNames = function (selector1,selector2) {
    $('#' + selector1).attr("id", "NoId");
    $('#selection').html("");
    $('#' + selector2 + ' option').each(function () {
        $('#selection').append("<option>"+$(this).val()+"</option");
    });
    $('#selection').attr("id", selector1);
}

var OrderNumberLoc = -1;

/************************************************************************************************
Controls Page Load Functions and Enabling DataTable Plugin
*************************************************************************************************/
$(document).ready(function () {
    
    var count = -1;
    $('#selection2').children().each(function () {
        var $this = $(this)
        if ($this.text() == "Order Number") {
            OrderNumberLoc = count;
        };
        count++;
    });

    // searchbox typeahead
    var searchTypeaheadOS = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: '/Transactions/nextSuggestionsTrans?query=%QUERY&column=',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            replace: function (url, query) {
                return '/Transactions/nextSuggestionsTrans?query=' + $('#searchString1').val() + '&table=1&column=' + $('#selection1').val();
            },
            cache: false
        }
    });
    searchTypeaheadOS.initialize();
    $('#searchString1.typeahead').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "searchTypeaheadOS",
        displayKey: 'value',
        source: searchTypeaheadOS.ttAdapter()
    }).on('typeahead:selected', function () { $('#searchString1').trigger('input'); $('#searchString1').trigger('focusout'); })
    .on('typeahead:opened', function () { $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')) });

    // searchbox typeahead
    var searchTypeaheadOT = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: '/Transactions/nextSuggestionsTrans?query=%QUERY&column=',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            replace: function (url, query) {
                return '/Transactions/nextSuggestionsTrans?query=' + $('#searchString2').val() + '&table=2&column=' + $('#selection2').val();
            },
            cache: false
        }
    });
    searchTypeaheadOT.initialize();
    $('#searchString2.typeahead').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "searchTypeaheadOT",
        displayKey: 'value',
        source: searchTypeaheadOT.ttAdapter()
    }).on('typeahead:selected', function () { $('#searchString2').trigger('input'); $('#searchString2').trigger('focusout'); })
    .on('typeahead:opened', function () { $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')) });

    // searchbox typeahead
    var searchTypeaheadTH = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: '/Transactions/nextSuggestionsTrans?query=%QUERY&column=',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            replace: function (url, query) {
                return '/Transactions/nextSuggestionsTrans?query=' + $('#searchString3').val() + '&table=3&column=' + $('#selection3').val();
            },
            cache: false
        }
    });
    searchTypeaheadTH.initialize();
    $('#searchString3.typeahead').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "searchTypeaheadTH",
        displayKey: 'value',
        source: searchTypeaheadTH.ttAdapter()
    }).on('typeahead:selected', function () { $('#searchString3').trigger('input'); $('#searchString3').trigger('focusout'); })
    .on('typeahead:opened', function () { $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')) });

    /*************************************************************************************************************
        Table initializations
    ***************************************************************************************************************/

    // order status
    orderStatTable = $('#data').DataTable({
        "dom": "trip",
        "iDisplayLength": 100,
        "processing": true,
        "serverSide": true,
        //Edit this to set Row Colors Depending on Status
        "createdRow": function (row, data, index) {
            if (data[37] == "open") {
                if (data[1] != "") {
                    $(row).addClass("success");
                } else {
                    $(row).addClass("warning");
                };
            } else {
                if (data[1] == "") {
                    $(row).addClass("danger");
                } else {
                    $(row).addClass("success");
                };
            };
            // set first column's bg color according to the type of transaction
            var td = $(row).find("td").eq(0);
            if (data[0].toLowerCase() == "complete") {
                td.addClass("transaction-complete");
            } else if (data[0].toLowerCase() == "count") {
                td.addClass("transaction-count");
            } else if (data[0].toLowerCase() == "location change") {
                td.addClass("transaction-locationChange");
            } else if (data[0].toLowerCase() == "pick") {
                td.addClass("transaction-pick");
            } else if (data[0].toLowerCase() == "put away") {
                td.addClass("transaction-putAway");
            } else if (data[0].toLowerCase() == "shipping") {
                td.addClass("transaction-shipping");
            } else if (data[0].toLowerCase() == "adjustment") {
                td.addClass("transaction-adjustment");
            } else {
                td.addClass("transaction-shippingComplete");
            };
        },
        "ajax": {
            // function called in TransactionsController
            "url": "/Transactions/orderStat",
            "data": function (d) {
                // object settings to be passed along
                    d.ordernum = $('#ordernumFilterOrder').val();
                    d.toteid = $('#toteidFilterOrder').val();
                    d.checkvalue = $('#toteCheck').prop('checked');
                    d.searchColumn = $('#selection1').val();
                    d.searchString = $('#searchString1').val();
                    d.compdate = completedOSDate
                    d.filter = (OrderStatFilterMen == "" ? "" : OrderStatFilterMen.getFilterString());
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });
    //Handles Page Length Selector for this table
    $('#pageLength1').change(function () {
        orderStatTable.page.len($(this).val());
        orderStatTable.draw();
    });
    //Handles Search Input for this table
    $('#searchString1').on('input', function (e) {
        if ($(this).val() == '') {
            $(this).typeahead('val', '');
        }
        orderStatTable.draw();
    });
    $('#data').wrap('<div style="overflow-x:scroll"></div>')
    //Tooltips
    $('#printList').tooltip();

    // Open Transactions Table
    openTable = $('#openTransTable').DataTable({
        "dom": "trip",
        "iDisplayLength": 100,
        "pagingType": "simple_numbers",
        "processing": true,
        "serverSide": true,
        //"scrollX": true,
        //To make sure that something appears telling that there oculd be an error if the table has no records
        "initComplete": function (settings, json) {
            if (!json.recordsTotal) {
                console.log("Empty Open Transactions Table. Either there was an error or no results for the current configuration");
            };
        },
        //Edit this to set Row Colors other than the default/bootstrap
        "createdRow": function (row, data, index) {
            if (OrderNumberLoc != -1) {
                $(row).find("td").eq(OrderNumberLoc).addClass("ordNumbClick");
            };
        },
        "ajax": {
            // function call to TransactionsController
            "url": "/Transactions/openTrans",
            "data": function (d) {
                    // object data
                    d.sDate = $('#sDateFilterOpen').val();
                    d.eDate = $('#eDateFilterOpen').val();
                    d.transStatus = $('#statusFilterOpen').val();
                    d.transType = $('#transFilterOpen').val();
                    d.searchColumn = $('#selection2').val();
                    d.searchString = $('#searchString2').val();
                    d.ordernum = $('#orderNumberFilterOpen').val();
                    d.toteid = $('#toteIDFilterOpen').val();
                    d.filter = (OpenTransFilterMen == "" ? "" : OpenTransFilterMen.getFilterString());
                    
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });
    //Handles Page Length Selector for this table
    $('#pageLength2').change(function () {
        openTable.page.len($(this).val());
        openTable.draw();
    });
    //Handles Search Input for this table
    $('#searchString2').on('input', function (e) {
        if ($(this).val() == '') {
            $(this).typeahead('val', '');
        }
        openTable.draw();
    });
    $('#setDefaultOpen').tooltip();
    $('#openTransTable').wrap('<div style="overflow-x:scroll"></div>');

    // Transaction History Table view
    transTable = $('#transHistTable').DataTable({
        "dom": "trip",
        "iDisplayLength": 100,
        "processing": true,
        "serverSide": true,
        "initComplete": function (settings, json) {
            //To make sure that something appears telling that there could be an error if the table has no records
            if (!json.recordsTotal) {
                console.log("Empty Transaction History Table. Either there was an error or no results for the current configuration");
            };
        },
        // change row color with the next line
        "createdRow": function (row, data, index) { /* $('td', row).addClass("success"); */ },
        "ajax": {
            // call to TransactionsController
            "url": "/Transactions/transHist",
            "data": function (d) {
                    // object data
                    d.sDate = $('#sDateFilterTrans').val();
                    d.eDate = $('#eDateFilterTrans').val();
                    d.searchColumn = $('#selection3').val();
                    d.searchString = $('#searchString3').val();
                    d.ordernum = $('#orderNumberFilterTrans').val();
                    d.filter = (TransHistFilterMen == "" ? "" : TransHistFilterMen.getFilterString())
                    
            }
        },
        "order": [[0, 'desc']],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });

    //Handles Page Length Selector for this table
    $('#pageLength3').change(function () {
        transTable.page.len($(this).val());
        transTable.draw();
    });
    //Handles Search Input for this table
    $('#searchString3').on('input', function (e) {
        if ($(this).val() == '') {
            $(this).typeahead('val', '');
        }
        transTable.draw();
    });
    $('#setDefaultHist').tooltip();
    $('#transHistTable').wrap('<div style="overflow-x:scroll"></div>');
})

//End DataTable Code

// Handling for each view selector click
$(document).on("click", '#orderStatusView, #openView, #histView, #reprocView, #reprocedView', function () {
    var $this = $(this);
    currentView = $this.text();
    $('#PageName').text('| ' + currentView);
    $('#selectedOrderNumber, #selectedTransType').val('');
    switch ($this.attr('id')) {
        case 'reprocView':
            openTransTempTable.columns.adjust().draw();
            getFilteredOrders($('#ROrder').val(), $('#RItem').val(), $('#hold').prop('checked'), 0);
            break;
        case 'histView':
            transTable.columns.adjust().draw();
            break;
        case 'openView':
            openTable.columns.adjust().draw();
        case 'orderStatusView':
            $('#ordernumFilterOrder').focus();
    };
});