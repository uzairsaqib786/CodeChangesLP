// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// datatable
// item and order number typeaheads
var ritem; var rorder; var hold; var id; var Reprocess; var Complete; var History;

// hub connection
var RPHub = $.connection.reprocessTransactionsHub;
// id of the selected transaction from the right hand tabbed menu.  0 if none is selected
var rightID = 0;
$(document).ready(function () {
    // typeahead suggestion engine source
    var typeaheadsrc = 'getReprocessTypeahead';

    // set default page length to 100.
    $('#pageLength4').val(100);
    
    var numColumns;

    // Alter the Notes modal to use with transactions as it is the same, just with different names
    $('#notes_label').text('Reason Message');
    $('#notes_textarea').attr('placeholder', 'Reason Message');

    // Handles all printing buttons
    $(document.body).on('click', '#PrintSelected, #PrintAll, #PrintReason, #PrintMessage, #PrintDate, #PrintItem, #PrintOrder', function () {
        printFromReprocess($(this), true);
    });


    // Handles all print preview buttons
    $(document.body).on('click', '#PrintPreviewSelected, #PrintPreviewAll, #PrintPreviewReason, #PrintPreviewMessage, #PrintPreviewDate, #PrintPreviewItem, #PrintPreviewOrder', function () {
        printFromReprocess($(this), false);
    });

    /*
        Handles setting the reprocess, post as complete and send to history fields to a single value all at once on button click.
    */
    $('#reprocessAll, #completeAll, #historyAll, #reprocessNone, #completeNone, #historyNone').click(function () {
        var $this = $(this);
        var mark;
        var which;
        switch ($this.attr('id')) {
            case 'reprocessAll':
                mark = true;
                which = 'Reprocess';
                break;
            case 'completeAll':
                mark = true;
                which = 'Post as Complete';
                break;
            case 'historyAll':
                mark = true;
                which = 'Send to History';
                break;
            case 'reprocessNone':
                mark = false;
                which = 'Reprocess';
                break;
            case 'completeNone':
                mark = false;
                which = 'Post as Complete';
                break;
            case 'historyNone':
                mark = false;
                which = 'Send to History';
                break;
        };
        var result = confirm('Click OK to ' + (mark ? 'mark' : 'unmark') + ' ALL transactions as ' + which + '.');
        if (result) {
            RPHub.server.setAll(which, mark).done(function () {
                getOrdersToPost();
                openTransTempTable.draw();
            });
            $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
            $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').prop('checked', false);
            $('#reprocTransTable tr.active').removeClass('active');
        };
    });

    // initialize data table.  Hides fields [Reason Message], [Reason], [Date Stamp], [Name Stamp], [Reprocess], [Post as Complete], [Send to Reprocess History].
    openTransTempTable = $('#reprocTransTable').DataTable({
        "dom": "trip",
        "iDisplayLength": 100,
        "processing": true,
        "serverSide": true,
        "initComplete": function (settings, json) {
            //To make sure that something appears telling that there could be an error if the table has no records
            if (!json.recordsTotal) {
                console.log("Empty Reprocess Table. Either there was an error or no results for the current configuration");
                //window.location.reload(true);
            };
        },
        "columnDefs": [
                   { 'width': "100px", 'visible': false, "targets": [] }
        ],

        createdRow: function (row, data, index) {
            var $row = $(row);
            numColumns = $('#reprocTransTable thead th').length;
            $row.children('td:nth-last-child(1)').attr('name', data[numColumns - 1]).html(data[numColumns - 1]);

        },
        "ajax": {
            // function call to TransactionsController
            "url": "/Transactions/reprocTrans",
            "data": function (d) {
                d.ordernum = $('#ROrder').val();
                d.itemNumber = $('#RItem').val();
                d.hold = hold;
                d.searchColumn = $('#selection4').val();
                d.searchString = $('#searchString4').val();
            },
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });

    //typeahead for search input box
    var searchTypeaheadReproc = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: '/Transactions/nextSuggestionsTrans?query=%QUERY&column=',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            replace: function (url, query) {
                if ($('#reprocess').prop('checked')) {
                    //OT Temp
                    return '/Transactions/nextSuggestionsTrans?query=' + $('#searchString4').val() + '&table=4&column=' + $('#selection4').val();
                } else {
                    //OT Temp History
                    return '/Transactions/nextSuggestionsTrans?query=' + $('#searchString4').val() + '&table=5&column=' + $('#selection4').val();
                };
            },
            cache: false
        }
    });
    searchTypeaheadReproc.initialize();
    $('#searchString4.typeahead').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "searchTypeaheadReproc",
        displayKey: 'value',
        source: searchTypeaheadReproc.ttAdapter()
    }).on('typeahead:selected', function () { $('#searchString4').trigger('input'); $('#searchString4').trigger('focusout'); })
    .on('typeahead:opened', function () { $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')) });

    //Cancels alert message that pops up when toggling 'Records to View' and 'Reason filter'
    alert = function () { };


    //  Allows pagination buttons to always be visible.
    $('#reprocTransTable').wrap('<div style="overflow-x:scroll"></div>');

    //Handles Page Length Selector for this table
    $('#pageLength4').on('change', function () {
        openTransTempTable.page.len($(this).val());
        openTransTempTable.draw();

    });

    $('#searchString4').on('input', function () {
        if ($('#selection4').val() != '') {
            openTransTempTable.draw();
        }
        if ($('#searchString4').val() == '') {
            openTransTempTable.draw();
        }
    });


    //  disables the ability to check any Reprocess Choices while a row in the table isn't highlighted.
    $('#reprocTransTable_paginate').on('click', function () {
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').prop('checked', false);
    });

    // Handles all delete buttons
    $('#RPdeleteSelected, #RPdeleteAll, #RPdeleteReason, #RPdeleteMessage, #RPdeleteDate, #RPdeleteItem, #RPdeleteOrder, #RPdeleteReplenishment').click(function () {
        deleteFromReprocess($(this));
    });


    $('#PostTransactions').click(function () {
        postTransactions();
    })

    // Handles toggling between the reprocess view and the reprocess history view
    $('input[name="HistOrReprocess"]').click(function () {
        toggleView($(this));
    });

    // Handles toggling between hold transactions only and all transactions
    $('input[name="HoldOrAll"]').click(function () {
        if ($('#hold').prop('checked')) {
            hold = 1;
        } else {
            hold = 0;
        };
        openTransTempTable.ajax.url('/Transactions/reprocTrans').load();
    });

    // Turns off the print and delete buttons and selected transaction area when focus/active class on the table is lost.
    $('#reprocTransTable thead tr').on('click', function () {
        $('#selectedTransaction, .OrderSelectedOnly').hide();
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').prop('checked', false);
    });

    // Handles clicking a reprocess, complete or history transaction entry on the right side to select a specific order entry from reprocess transactions
    $('#reprocessBody, #completeBody, #historyBody').on('click', 'tr', function () {
        var $this = $(this);
        var ID = $this.find('button').attr('name');
        var order = $this.find('td:first').text();
        var item = $this.find(':nth-child(2)').text();
        if ($('#reprocTransTable tbody tr').hasClass('active')) {
            $('#reprocTransTable tbody tr').removeClass('active');
        };
        if ($this.hasClass('active')) {
            $this.removeClass('active');
        } else {
            $('#ROrder').val(order).trigger('input', ID).trigger('blur');
            $('#RItem').val(item).trigger('input', ID).trigger('blur');
            getFilteredOrders(order, item, $('#hold').prop('checked'), ID);
            $this.addClass('active');
        };
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').prop('checked', false);
    });

    //Reloads the table after a order has been selected as Reprocess, Complete or History
    $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').on('click', function () {
        openTransTempTable.ajax.url('/Transactions/reprocTrans').load();

    });

    /*
        Handles capturing the redraw event of the datatables instance.  
        This is used to select a particular entry in the datatable.  If it isn't used the async ajax call for datatables may not return values in time
        for the active class to be added to a particular selected entry in the table
    */
    $('#reprocTransTable').on('draw.dt', function () {
        if (rightID != 0) {
            $(this).find('td[name="' + rightID + '"]').parent().trigger('click');
            rightID = 0;
        };
        $('#selectedTransaction').hide();

    });

    /*
        Handles clicking a remove button for the right hand side menu and stops the event from propagating up the DOM to the <tr>
    */
    $('#reprocessBody, #completeBody, #historyBody').on('click', 'tr button', function (e) {
        e.stopImmediatePropagation();
        var $this = $(this);
        var field = $this.parents('tbody').attr('id').split('Body')[0];
        var ID = $this.attr('name');
        var reprocess = 0; var postcomplete = 0; var sendhist = 0;
        setReprocessInclude(ID, reprocess, postcomplete, sendhist, field);
        $('input[type="checkbox"][data-id="' + ID + '"][name="' + field + '"]').prop('checked', false);
        if (!$('#reprocTransTable tr.active').hasClass('active')) {
            $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').prop('checked', false);
            $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
        };
});

    /*
         Handles the toggling of a checkbox in the datatable.
     */
    $('#reprocessChoices').on('click', 'input[type="checkbox"]', function () {
        var $this = $(this);
        var reprocess = $('#reprocessCheckBox').prop("checked") ? 1 : 0;
        var postcomplete = $('#completeCheckBox').prop("checked") ? 1 : 0;
        var sendhist = $('#historyCheckBox').prop("checked") ? 1 : 0;
        if (reprocess == 1 && postcomplete == 1) {
            if ($this.attr('name') == 'reprocess') {
                postcomplete = 0;
                $('#completeCheckBox').prop("checked", false);
            } else {
                reprocess = 0;
                $('#reprocessCheckBox').prop("checked", false);
            };
        };
        setReprocessInclude(id, reprocess, postcomplete, sendhist, '');
        Reprocess = 'false';
        Complete = 'false';
        History = 'false';
    });

    /*
        Clears the item number and order number input boxes and requerries for all unfiltered orders
    */
    $('#clearInputs').click(function () {
        $('#ROrder, #RItem, #selection4, #searchString4').val('');
        $('#all').prop('checked', 'checked');
        $('#reprocess').prop('checked', 'checked');
        PageReset();
    });

    $('#selection4').on('change', function () {
        $('#searchString4').val('');
    });

    /*
        Handles inputs on the order number and item number filters.  Optional parameter ID handles a case where inputs need to be triggered
            because of the typeahead, but requerrying for new filtered orders is not necessary.
    */
    $('#ROrder, #RItem').on('input', function (e, ID) {
        if (ID > 0) {
            return false;
        } else if ($('#ROrder').val() == '') {
            $('#RItem').val('');
            openTransTempTable.draw();
        } else {
            var order = $('#ROrder').val().trim();
            var item = $('#RItem').val().trim();
            getFilteredOrders(order, item, $('#hold').prop('checked'), 0);
            return true;
        };
    });

    // handles paging 20 pages to the right.
    $('#page20RightRT').on('click', function () {
        var lastPage = $('#reprocTransTable_next').prev().children('a').eq(0).html();
        if (($('#reprocTransTable').DataTable().page() + 20) >= lastPage) {
            $('#reprocTransTable').DataTable().page(lastPage - 1).draw(false);
        } else {
            $('#reprocTransTable').DataTable().page($('#reprocTransTable').DataTable().page() + 20).draw(false);
        }
        $('#page20RightRT').blur();
    });

    // handles paging 20 pages to the left.
    $('#page20LeftRT').on('click', function () {
        var firstPage = 0;
        if (($('#reprocTransTable').DataTable().page() - 20) <= firstPage) {
            $('#reprocTransTable').DataTable().page(firstPage).draw(false);
        } else {
            $('#reprocTransTable').DataTable().page($('#reprocTransTable').DataTable().page() - 20).draw(false);
        }
        $('#page20LeftRT').blur();

    });

    /*
        Handles selecting a particular row in the datatable
    */
    $('#reprocTransTable tbody').on('click', 'tr', function (e) {
        var $this = $(this);
        if ($this.children('td').text() != 'No matching records found' && e.target.tagName != 'INPUT') {
            if (!$this.hasClass('active')) {
                $('#reprocTransTable tr.active').removeClass('active');
                $this.addClass('active');
                id = openTransTempTable.cell('tr.active', numColumns-1).data();
                RPHub.server.getReprocTransData(id).done(function (data) {
                    $('#RReasonMessage').val(data["Reason Message"]);
                    $('#RReason').val(data["Reason"]);
                    $('#createdOn').val(data["Date Stamp"]);
                    $('#createdBy').val(data["Name Stamp"]);
                    Reprocess = data["Reprocess"];
                    Complete = data["Post as Complete"];
                    History = data["Send to History"];
                    $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', false);
                    $('#selectedTransaction, .OrderSelectedOnly').show();
                    $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('data-id', id)
                    if (Reprocess) {
                        $('#reprocessCheckBox').prop('checked', true);
                    } else {
                        $('#reprocessCheckBox').prop('checked', false);
                    };
                    if (Complete) {
                        $('#completeCheckBox').prop('checked', true);
                    } else {
                        $('#completeCheckBox').prop('checked', false);
                    };
                    if (History) {
                        $('#historyCheckBox').prop('checked', true);
                    } else {
                        $('#historyCheckBox').prop('checked', false);
                    };
                });
            } else {
                $this.removeClass('active');
                $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
                $('#selectedTransaction, .OrderSelectedOnly').hide();
            };
        };
    });
    
    /*
    Disables the ability to select a Reprocess Choice when the page loads.
    */
    $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);

    /*
     Initialize the suggestion engine for the typeaheads
    */
    ordernumber = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/Typeahead/'),
            replace: function (url, uriEncodedQuery) {
                return url + typeaheadsrc + '?ItemNumber=' + $('#RItem').val() + '&OrderNumber=' + $('#ROrder').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    ordernumber.initialize();

    // set the display and headers for the first typeahead
    var display = '<p class="typeahead-row " style="width:20%;">{{OrderNumber}}</p>' +
        '<p class="typeahead-row" style="width:20%">{{DateField}}</p>' +
        '<p class="typeahead-row " style="width:20%;">{{TransType}}</p>' +
        '<p class="typeahead-row " style="width:20%;">{{ItemNumber}}</p>' +
        '<p class="typeahead-row " style="width:20%;">{{Qty}}</p>';

    var header = '<p class="typeahead-header" style="width:20%;">Order Number</p>' +
        '<p class="typeahead-header" style="width:20%;">Date</p>' +
        '<p class="typeahead-header" style="width:20%;">Transaction Type</p>' +
        '<p class="typeahead-header" style="width:20%;">Item Number</p>' +
        '<p class="typeahead-header" style="width:20%;">Transaction Quantity</p>';

    // declare the order number typeahead
    rorder = $('#ROrder').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "ordernumber",
        displayKey: 'OrderNumber',
        source: ordernumber.ttAdapter(),
        templates: {
            header: header,
            suggestion: Handlebars.compile(display)
        }
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "600px");
    }).on('typeahead:selected', function (obj, data, name) {
        getFilteredOrders($('#ROrder').val(), $('#RItem').val(), $('#hold').prop('checked'), 0);
        openTransTempTable.draw();
    });

    // change the display and headers for the second typeahead
    display = '<p class="typeahead-row " style="width:20%;">{{ItemNumber}}</p>' +
       '<p class="typeahead-row" style="width:20%">{{DateField}}</p>' +
        '<p class="typeahead-row " style="width:20%;">{{TransType}}</p>' +
        '<p class="typeahead-row " style="width:20%;">{{OrderNumber}}</p>' +
        '<p class="typeahead-row " style="width:20%;">{{Qty}}</p>';

    header = '<p class="typeahead-header" style="width:20%;">Item Number</p>' +
        '<p class="typeahead-header" style="width:20%;">Date</p>' +
        '<p class="typeahead-header" style="width:20%;">Transaction Type</p>' +
        '<p class="typeahead-header" style="width:20%;">Order Number</p>' +
        '<p class="typeahead-header" style="width:20%;">Transaction Quantity</p>';

    // declares the Item Number typeahead
    ritem = $('#RItem').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "itemnumber",
        displayKey: 'ItemNumber',
        source: ordernumber.ttAdapter(),
        templates: {
            header: header,
            suggestion: Handlebars.compile(display)
        }
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "600px");
    }).on('typeahead:selected', function (obj, data, name) {
        getFilteredOrders($('#ROrder').val(), $('#RItem').val(), $('#hold').prop('checked'), 0);
        openTransTempTable.draw();
    });
    // ensures that the hub is started before calling hub functions
    $.connection.hub.start().done(function () {
        getOrdersToPost();
        getFilteredOrders($('#ROrder').val(), $('#RItem').val(), $('#hold').prop('checked'), 0);
    });

    $("#reprocessChoices").css("background", "rgb(230, 247, 255)");

    $('#ROrder').on('focus', function () {
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').prop('checked', false);
        $('#reprocTransTable tr.active').removeClass('active');
    });

    $('#RItem').on('focus', function () {
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').prop('checked', false);
        $('#reprocTransTable tr.active').removeClass('active');
    });

    //Opens the column sequence page for the reporcess page and saves the information that's on the page 
    $('#setDefaultReproc').click(function () {
        var RecView = "";
        if ($('#reprocess').is(':checked')) {
            RecView = "reprocess";
        } else {
            RecView = "history";
        };

        var ReasFilt = "";
        if ($('#all').is(':checked')) {
            ReasFilt = "none";
        } else {
            ReasFilt = "hold";
        };

        $('#RecToView').val(RecView);
        $('#ReasFilt').val(ReasFilt);
        $('#OrderNumTemp').val($('#ROrder').val());
        $('#ItemNumTemp').val($('#RItem').val());
        document.getElementById("reprocDefaultForm").submit();
    });

    //Has the page reset to the state it was before the column sequence button was presssed
    PageReset();

    //Enables the reason filter buttons
    $('#reprocess').click(function () {
        $('#hold').removeAttr('disabled');
        $('#all').removeAttr('disabled');
    });
    //Disables the reason filter buttons
    $('#hist').click(function () {
        $('#hold').attr('disabled', 'disabled');
        $('#all').attr('disabled', 'disabled');
    });

});

/*
    Resets page with the data that was used befoer the column sequence button was pressed
*/
function PageReset() {
    if ($('#hist').is(':checked')) {
        $('#all').click();
        $('#hist').trigger('click');
    } else {
        $('#reprocess').trigger('click');
        if ($('#all').is(':checked')) {
            $('#all').click();
        } else {
            $('#hold').click();
        };
    };
    
};

/*
    Posts that transactions that are in the Selected Orders table.
*/
function postTransactions() {
    RPHub.server.postReprocessTransactions().done(function (response) {
        if (response == 'Error') {
            MessageModal('Error', 'There was an error posting the designated reprocess records', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else {
            MessageModal('Success', 'Transactions Succesfully Posted', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        }
        $('#clearInputs').click();
        getOrdersToPost();
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').attr('disabled', true);
        $('#reprocessCheckBox, #completeCheckBox, #historyCheckBox').prop('checked', false);
    })
};

/*
    Sets the specified ID in the reprocess transactions table to the value specified in reprocess, postcomplete or sendhist based on the value of field
*/
function setReprocessInclude(ID, reprocess, postcomplete, sendhist, field) {
    RPHub.server.setReprocessTransactionsInclude(ID, reprocess, postcomplete, sendhist, field).done(function () {
    });
};

/*
    Hold as boolean true = holds only, false = all, ID indicates a row on the right side filter was clicked and must be used to select a particular row in the datatable
*/
function getFilteredOrders(ordernumber, itemnumber, hold, ID) {
    $('#reprocessBody, #completeBody, #historyBody').children('.active').removeClass('active');
    $('.OrderSelectedOnly').hide();
    var order = $('#ROrder').val().trim();
    var item = $('#RItem').val().trim();
    if (order == '') {
        $('.OrderOnly').hide();
    } else {
        $('.OrderOnly').show();
    };
    if (item == '') {
        $('.ItemOnly').hide();
    } else {
        $('.ItemOnly').show();
    };
    
    RPHub.server.getFilteredOrders(itemnumber, ordernumber, hold, $('#hist').prop('checked')).done(function (orders) {
        if (ID != 0) {
            rightID = ID;
            openTransTempTable.draw();
        };
    });
};

/*
    Gets the orders to populate the right hand side tabs.  Transactions marked as reprocess, post as complete or send to history will appear in the tabs.
*/
function getOrdersToPost() {
    RPHub.server.getOrdersToPost().done(function (orders) {
        var reprocess = $('#reprocessBody');
        var complete = $('#completeBody');
        var history = $('#historyBody');
        reprocess.html(''); complete.html(''); history.html('');
        $.each(orders.Reprocess, function (index, obj) {
            reprocess.append('<tr><td>' + obj.OrderNumber + '</td><td>' + obj.ItemNumber + '</td><td><button type="button" data-toggle="tooltip" data-placement="top" data-original-title="Remove" class="btn btn-danger" name="' + obj.ID + '"><span class="glyphicon glyphicon-remove"></span></button></td></tr>');
        });
        $.each(orders.Complete, function (index, obj) {
            complete.append('<tr><td>' + obj.OrderNumber + '</td><td>' + obj.ItemNumber + '</td><td><button type="button" data-toggle="tooltip" data-placement="top" data-original-title="Remove" class="btn btn-danger" name="' + obj.ID + '"><span class="glyphicon glyphicon-remove"></span></button></td></tr>');
        });
        $.each(orders.History, function (index, obj) {
            history.append('<tr><td>' + obj.OrderNumber + '</td><td>' + obj.ItemNumber + '</td><td><button type="button" data-toggle="tooltip" data-placement="top" data-original-title="Remove" class="btn btn-danger" name="' + obj.ID + '"><span class="glyphicon glyphicon-remove"></span></button></td></tr>');
        });
        $('#reprocessBody [data-toggle="tooltip"], #completeBody [data-toggle="tooltip"], #historyBody [data-toggle="tooltip"]').tooltip();
        $('#reprocessTab').html(orders.ReprocessCount);
        $('#completeTab').html(orders.CompleteCount);
        $('#historyTab').html(orders.HistoryCount);
        $('#reprocessCount').html(orders.ReprocessCount);
        $('#completeCount').html(orders.CompleteCount);
        $('#historyCount').html(orders.HistoryCount);
    });
};

/*
    Broadcaster client function to force clients to update to the latest data if someone else changes one of the post as ... fields
*/
RPHub.client.updatePostOrders = function () {
    getOrdersToPost();
};

/*
    Toggles between the reprocess transactions view and the reprocess history transactions view
*/
function toggleView($this) {
    if ($this.attr('id') == 'hist') {
        //need to hide these
        //columnSeq.Remove("Reason Message")
        //columnSeq.Remove("Reason")
        //columnSeq.Remove("Date Stamp")
        //columnSeq.Remove("Name Stamp")

        $.each(openTransTempTable.columns()[0], function (index, value) {
            //Get column name from datatable
            var ColName = openTransTempTable.column($(this)).header().textContent

            //If it is hideable column. Hide it
            if (ColName == "Reason Message" || ColName == "Reason" || ColName == "Date Stamp" || ColName == "Name Stamp") {
                openTransTempTable.column($(this)).visible(false);
            };

        });
        
        openTransTempTable.ajax.url('/Transactions/reprocHistTrans').load();
        typeaheadsrc = 'getReprocessHistTypeahead';
        $('#reprocessChoicesPanel').hide();
        $('#OrdersToPost').hide();
        $('#reprocTableSize').removeClass('col-md-9');
        $('#reprocTableSize').addClass('col-md-12');
        $('#PostTransactions').hide();
        $('#selectedTransaction').hide();
        $('#editTransaction').html('View This Transaction <span class="glyphicon glyphicon-resize-full"></span>');
        $('#reprocess_modal').find('legend').hide();
        $('#reprocess_submit').hide();
        $('#reprocess_modal').find('.enabled').attr('disabled', 'disabled');
        $('#reprocess_dismiss').text('Close');
    } else {
        //need to show these
        //columnSeq.Remove("Reason Message")
        //columnSeq.Remove("Reason")
        //columnSeq.Remove("Date Stamp")
        //columnSeq.Remove("Name Stamp")

        $.each(openTransTempTable.columns()[0], function (index, value) {
            //Get column name from datatable
            var ColName = openTransTempTable.column($(this)).header().textContent

            //If it is hideable column. Hide it
            if (ColName == "Reason Message" || ColName == "Reason" || ColName == "Date Stamp" || ColName == "Name Stamp") {
                openTransTempTable.column($(this)).visible(true);
            };

        });

        openTransTempTable.ajax.url('/Transactions/reprocTrans').load();
        typeaheadsrc = 'getReprocessTypeahead';
        $('#reprocTableSize').removeClass('col-md-12');
        $('#reprocTableSize').addClass('col-md-9');
        $('#reprocessChoicesPanel').show();
        $('#OrdersToPost').show();
        $('#PostTransactions').show();
        $('#reprocessPanel').show();
        $('#editTransaction').html('Edit This Transaction <span class="glyphicon glyphicon-resize-full"></span>');
        $('#reprocess_modal').find('legend').show();
        $('#reprocess_submit').show();
        $('#reprocess_dismiss').text('Close without Saving Changes');
        $('#reprocess_modal').find('.enabled').removeAttr('disabled');
    };
    $('#reprocTransTable').css('width', "100%");
    //getFilteredOrders($('#ROrder').val(), $('#RItem').val(), $('#hold').prop('checked'), 0);
    openTransTempTable.columns.adjust().draw();
};

/*
    Handles deleting transactions from reprocess
*/
function deleteFromReprocess($sender) {
    var toDelete = $sender.attr('id').split('RPdelete')[1].toLowerCase();
    var id = 0;
    var reason = '';
    var message = '';
    var dateStamp = '';
    var item = '';
    var order = '';
    var replenishment = false;
    var prompt = 'Click OK to delete *ALL* reprocess transactions.';
    switch (toDelete) {
        case 'selected':
            id = $('#reprocTransTable .active td:nth-last-child(1)').attr('name');
            prompt = 'Click OK to delete selected transaction only.';
            break;
        case 'reason':
            reason = $('#RReason').val();
            prompt = 'Click OK to delete all transactions with selected reason.';
            break;
        case 'message':
            message = $('#RReasonMessage').val();
            prompt = 'Click OK to delete all transactions with selected reason message.';
            break;
        case 'date':
            dateStamp = $('#createdOn').val();
            prompt = 'Click OK to delete all transactions with selected date/time.';
            break;
        case 'item':
            item = $('#RItem').val();
            prompt = 'Click OK to delete all transactions with selected item number.';
            break;
        case 'order':
            order = $('#ROrder').val();
            prompt = 'Click OK to delete all transactions with selected order number.';
            break;
        case 'replenishment':
            prompt = 'Click OK to delete all replenishment transactions.';
            replenishment = true;
            break;
    };
    var result = confirm(prompt);
    if (result) {
        RPHub.server.deleteReprocessTransactions($('#hist').prop('checked'), id, reason, message, dateStamp, item, order, replenishment).done(function () {
            openTransTempTable.draw();
            getOrdersToPost();
            getFilteredOrders($('#ROrder').val(), $('#RItem').val(), $('#hold').prop('checked'), 0);
            ordernumber.clearRemoteCache();
        });
    };
};

/*
    Handles printing from reprocess
*/
function printFromReprocess($sender, printOrPreview) {
    if (printOrPreview) {
        var toPrint = $sender.attr('id').toLowerCase().split('print')[1]
    } else {
        var toPrint = $sender.attr('id').toLowerCase().split('printpreview')[1]
    };
    var obj;
    var objFields = new Array();

    var id = 0;
    var reason = '';
    var message = '';
    var dateStamp = '';
    var order = '';
    var item = '';
    var history = $('#hist').prop('checked');
    switch (toPrint) {
        case 'selected':
            id = $('#reprocTransTable .active td:nth-last-child(1)').attr('name');
            objFields.push({ 'field': 'ID', 'exptype': '=', 'expone': id, 'exptwo': '' });
            break;
        case 'reason':
            objFields.push({ 'field': 'Reason', 'exptype': '=', 'expone': $('#RReason').val(), 'exptwo': '' });
            reason = $('#RReason').val();
            break;
        case 'message':
            objFields.push({ 'field': 'Reason Message', 'exptype': '=', 'expone': $('#RReasonMessage').val(), 'exptwo': '' });
            message = $('#RReasonMessage').val();
            break;
        case 'date':
            objFields.push({ 'field': 'Date Stamp', 'exptype': '=', 'expone': $('#createdOn').val(), 'exptwo': '' });
            dateStamp = $('#createdOn').val();
            break;
        case 'order':
            objFields.push({ 'field': 'Order Number', 'exptype': '=', 'expone': $('#ROrder').val(), 'exptwo': '' });
            order = $('#ROrder').val();
            break;
        case 'item':
            objFields.push({ 'field': 'Item Number', 'exptype': '=', 'expone': $('#RItem').val(), 'exptwo': '' });
            item = $('#RItem').val();
            break;
    };
    // PreviewReport in CustomReports expects there to be 6 obj in objFields.
    for (var x = 0; x < 5; x++) {
        objFields.push({ 'field': '', 'exptype': '', 'expone': '', 'exptwo': '' });
    };
   
    title = 'Reprocess Transactions Report';
    if (printOrPreview) {
        var url = '/Transactions/printReprocessTransactions';
        obj = {
            history: history,
            ID: id,
            OrderNumber: order,
            ItemNumber: item,
            reason: reason,
            message: message,
            datestamp: dateStamp
        }
    } else {
        var reportName = 'Reprocess Transactions';
        var reportTitles = new Array('', '', '', '');
        var fields = new Array();
        var type = 0;
        var url = '/CustomReports/PreviewReport/'
        obj = {
            'desiredFormat': 'preview',
            'reportName': reportName,
            'reportTitles': reportTitles,
            'fields': fields,
            'objFields': objFields,
            'backFilename': '',
            'type': type
        };
        // Obj has to be deserialized in order to return JSON
        obj.objFields = JSON.stringify(obj.objFields); 
    }
    getLLPreviewOrPrint(url, obj, printOrPreview, 'report', title)
};
