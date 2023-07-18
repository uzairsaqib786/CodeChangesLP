// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/***************************************************
    Transaction History
****************************************************/
var THOrderNumber;
var TransHistCols = new Array();
$(document).ready(function () {
    $('#pageLength3').val(100);
    var transHistTimer = mkTimer(function () { transTable.draw(); }, 200);
    $.each($('#selection3').children(), function (index, element) {
        if (index != 0) {
            TransHistCols.push($(element).attr('value'));
        };
    });

    // takes care of order number suggestions for transaction history table
    THOrderNumber = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // calls TransactionsController function nextOrdersTrans
            url: '/Transactions/nextOrdersTrans?query=%QUERY',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            cache: false
        }
    });
    THOrderNumber.initialize();
    $('#transTypeAhead .typeahead').typeahead({
        hint: false,
        highlight: false,
        minLength: 1
    }, {
        name: "THOrderNumber",
        displayKey: 'value',
        source: THOrderNumber.ttAdapter()
    }).on('typeahead:selected', function () { $('#orderNumberFilterTrans').trigger("input").blur(); })
    .on('typeahead:opened',function(){$(this).siblings('.tt-dropdown-menu').css('width',$(this).css('width')).css('left','auto')});

    $('#sDateFilterTrans,#eDateFilterTrans').on('input', function () {
        transHistTimer.startTimer();
    });

    $('#eDateFilterTrans, #sDateFilterTrans').change(function () {
        transHistTimer.startTimer();
    });

    // handles paging 20 pages to the right.
    $('#page20RightTH').on('click', function () {
        var lastPage = $('#transHistTable_next').prev().children('a').eq(0).html();
        if (($('#transHistTable').DataTable().page() + 20) >= lastPage) {
            $('#transHistTable').DataTable().page(lastPage - 1).draw(false);
        } else {
            $('#transHistTable').DataTable().page($('#transHistTable').DataTable().page() + 20).draw(false);
        }
        $('#page20RightTH').blur();
    });

    // handles paging 20 pages to the left.
    $('#page20LeftTH').on('click', function () {
        var firstPage = 0;
        if (($('#transHistTable').DataTable().page() - 20) <= firstPage) {
            $('#transHistTable').DataTable().page(firstPage).draw(false);
        } else {
            $('#transHistTable').DataTable().page($('#transHistTable').DataTable().page() - 20).draw(false);
        }
        $('#page20LeftTH').blur();

    });

    /*********************************************************************************************************************
       Transaction History fields
   *********************************************************************************************************************/
    // select element for search field (dropdown)
    $('#selection3').change(function () {
        transHistTimer.startTimer();
    });
    // open the set column sequence view 
    $('#setDefaultHist').click(function () {
        $('#sDateTrans').val($('#sDateFilterTrans').val());
        $('#eDateTrans').val($('#eDateFilterTrans').val());
        $('#orderNumberTrans').val($('#orderNumberFilterTrans').val());
        document.getElementById("transDefaultForm").submit();
    });
    // filter by the order number when inputted 
    $('#orderNumberFilterTrans').on("input", function () {
        THOrderNumber.clearRemoteCache();
        if ($(this).val() == '') {
            $(this).typeahead('val', '');
        }
        transHistTimer.startTimer();
    });
    
    // reset to todays date for trans histroy when button is clicked
    $('#resetDateTrans').click(function () {
        $('#sDateFilterTrans').data("DateTimePicker").setDate(setToToday())
        $('#eDateFilterTrans').data("DateTimePicker").setDate(setToToday())
        transHistTimer.startTimer();
    });
});