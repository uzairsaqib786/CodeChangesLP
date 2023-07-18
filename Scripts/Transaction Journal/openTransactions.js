// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/******************************************************************
    Open Transactions
******************************************************************/
var OTOrderNumber;
var OpenTransCols = new Array();
$(document).ready(function () {
    $('#pageLength2').val(100);

    var openTableTimer = mkTimer(function () {
        $('#selectedOrderNumber, #selectedTransType').val("");
        openTable.draw();
    }, 200);

    $.each($('#selection2').children(), function (index, element) {
        if (index != 0) {
            OpenTransCols.push($(element).attr('value'));
        };
    });

    $('#goToInvMaster').click(function () {
        document.getElementById('submitInv').submit();
    });

    $(document.body).on('click', '#previewCycleCountReport', function () {
        previewCycleCountReport();
    });

    // takes care of suggestions of order numbers in open transactions
     OTOrderNumber = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // calls TransactionsController function nextOrdersOpen
            url: '/Transactions/nextOrdersOpen?query=%QUERY',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            cache: false
        }
    });
    OTOrderNumber.initialize();
    $('#openTransTypeAhead .typeahead').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "OTOrderNumber",
        displayKey: 'value',
        source: OTOrderNumber.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Order Number</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:100%;">{{value}}</p>')
        }
    }).on('typeahead:selected', function () { $('#orderNumberFilterOpen').trigger("input").blur(); })
    .on('typeahead:opened',function(){$(this).siblings('.tt-dropdown-menu').css('width',$(this).css('width'))});

    $('#sDateFilterOpen, #eDateFilterOpen').on('input', function () {
        openTableTimer.startTimer();
    });

    //  handles paging 20 pages to the right.
    $('#page20RightOT').on('click', function () {
        var lastPage = $('#openTransTable_next').prev().children('a').eq(0).html();
        if (($('#openTransTable').DataTable().page() + 20) >= lastPage) {
            $('#openTransTable').DataTable().page(lastPage-1).draw(false);
        } else {
            $('#openTransTable').DataTable().page($('#openTransTable').DataTable().page() + 20).draw(false);
        }
        $('#page20RightOT').blur();
    });

    // handles paging 20 pages to the left.
    $('#page20LeftOT').on('click', function () {
        var firstPage = 0;
        if (($('#openTransTable').DataTable().page() - 20) <= firstPage) {
            $('#openTransTable').DataTable().page(firstPage).draw(false);
        } else {
            $('#openTransTable').DataTable().page($('#openTransTable').DataTable().page() - 20).draw(false);
        }
        $('#page20LeftOT').blur();

    });

    /*****************************************************************************
        Order Number Listener: Double Click on Order number in table will re-direct you to Order Status Page
    *****************************************************************************/
    $(document.body).on('click', '#goToOrderStatus', function () {
        var value = $('.active .ordNumbClick').html();
        $('#toteidFilterOrder').val("");
        var press = jQuery.Event("keydown");
        press.ctrlKey = false;
        press.which = 13;
        press.keyCode = 13;
        $('#ordernumFilterOrder').typeahead('val',value)
        $('#orderStatusView').trigger('click');
        $('#searchString1').val('');
        $('#ordernumFilterOrder').trigger(press).blur();
    });

    /**************************************************************************************************************
        Open Transactions fields
    **************************************************************************************************************/
    // select element for search field (dropdown), transaction type filter (all, adjustment, complete, count, loc change, etc.)
    $('#selection2, #transFilterOpen').change(function () {
        openTableTimer.startTimer();
    });
    // set column sequence button
    $('#setDefaultOpen').click(function () {
        // set up all the needed data for keeping filters the same when you come back to the page from column sequence
        $('#sDateOpen').val($('#sDateFilterOpen').val());
        $('#eDateOpen').val($('#eDateFilterOpen').val());
        $('#orderNumberOpen').val($('#orderNumberFilterOpen').val());
        $('#toteIDOpen').val($('#toteIDFilterOpen').val());
        $('#transactType').val($('#transFilterOpen').val());
        $('#transactStat').val($('#statusFilterOpen').val());
        document.getElementById("openDefaultForm").submit();
    });

    // transaction status all, open or completed, end date filter for open transactions , start date filter for open transactions
    $('#statusFilterOpen,#eDateFilterOpen,#sDateFilterOpen').change(function () {
        openTableTimer.startTimer();
    });
    // reset to today's date 
    $('#resetDateOpen').click(function () {
        $('#sDateFilterOpen').data("DateTimePicker").setDate(setToToday());
        $('#eDateFilterOpen').data("DateTimePicker").setDate(setToToday());
        openTableTimer.startTimer();
    });
    // filters by the order number
    $('#orderNumberFilterOpen').on("input", function () {
        OTOrderNumber.clearRemoteCache();
        var $this = $(this);
        if ($this.val() == '') {
            $this.typeahead('val', '');
        };
        openTableTimer.startTimer();
    });
    // filters by the tote id
    $('#toteIDFilterOpen').on("input", function () {
        openTableTimer.startTimer();
    });
    // handles Table Click Events for Selecting Rows to be Deleted
    $('#openTransTable tbody').on('click', 'tr', function () {
        var $this = $(this); if ($this.children('td').hasClass('dataTables_empty')) { return };
        if ($this.hasClass('active')) {
            $this.removeClass('active');
            $('#goTo').attr('disabled', 'disabled');
            clearDeleteSelected();
            $('#ItemNumber, #selectedTransType').val('');
        }
        else {
            openTable.$('tr.active').removeClass('active');
            $('#goTo').removeAttr('disabled');
            $this.addClass('active');
            $('#selectedOrderNumber').val(openTable.cell('.active', $('#orderNumber').val()).data());
            $('#selectedTransType').val(openTable.cell('.active', $('#transactionType').val()).data());
            $('#ItemNumber').val(openTable.cell('.active', $('#itemNumber').val()).data());
        }
    });

    // right click delete button handling
    //$("#openTransTable tbody").contextMenu({
    //    menuSelector: "#contextMenu",
    //    menuSelected: function (invokedOn, selectedMenu) {
    //        var msg = "You selected the menu item '" + selectedMenu.text() +
    //            "' on the value '" + invokedOn.text() + "'";
    //        alert(msg);
    //    }
    //});

    function clearDeleteSelected() {
        $('#selectedOrderNumber, #selectedTransType').val("");
    };

    function previewCycleCountReport() {
        var url = '/CustomReports/PreviewReport/';
        var title = 'Cycle Count Report';
        var reportName = 'Cycle Count';
        var reportTitles = new Array('', '', '', '');
        var fields = new Array();
        var type = 0;
        var obj = {
            'desiredFormat': 'preview',
            'reportName': reportName,
            'reportTitles': reportTitles,
            'fields': fields,
            'backFilename': '',
            'type': type
        };
        // If you don't have any object fields, then you have to have 6 empty fields or the buildWhereClause function throwns an error.
        for (var x = 0; x < 6; x++) {
            fields.push('','','','')
        }

        getLLPreviewOrPrint(url, obj, false, 'report', title)
    };
});