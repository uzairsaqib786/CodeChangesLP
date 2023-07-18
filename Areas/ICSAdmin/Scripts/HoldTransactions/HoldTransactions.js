// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var holdTransHub = $.connection.holdTransactionsHub;
var holdTransTable;
var typingtime; var updateInterval;
var httypeahead;

$(document).ready(function () {
    // Handles Item Number/Order Number filters being swapped
    $('#EntryType').on('change', function () {
        var $this = $(this);
        var label = $('#ItemOrder');
        if ($this.val() == 'Order Number') {
            label.attr('placeholder', 'Order Number');
            label.parent().siblings('label').html('Order Number');
            $('#holdBy').text('ALL by Order Number');
        } else {
            label.attr('placeholder', label.attr('name'));
            label.parent().siblings('label').html(label.attr('name'));
            $('#holdBy').text('ALL by ' + label.attr('name'));
        };
        $('#ItemOrder').val('').trigger('input');
        holdTransTable.draw();
    });

    // Handles change on the reel/non-reel/both radio button filters
    $("input:radio[name ='reel']").change(function () {
        $('#ItemOrder').val('').trigger('input').blur();
        holdTransTable.draw();
    });

    // redirects to reprocess with hold transactions specified
    $('#viewReprocess').click(function () {
        window.location.href = '/Transactions?viewToShow=4&Holds=True';
    });

    // Handles input on the item and order number fields
    $('#ItemOrder').on('input', function () {
        if ($(this).val() == '') {
            $(this).typeahead('val','')
        }
        var newTime = new Date().getTime();
        if (newTime - typingtime > 200) {
            holdTransTable.draw();
        }
        else {
            clearTimeout(updateInterval);
            updateInterval = setTimeout(function () { holdTransTable.draw(); }, 200);
        }
        typingtime = newTime;
    });

    // initializes the datatable
    holdTransTable = $('#holdTransactionsTable').DataTable({
        "dom": "trip",
        "processing": true,
        "serverSide": true,
        "ajax": {
            // function call to HoldTransactionsController
            "url": "/Admin/HoldTransactions/HoldTransactionsTable",
            "data": function (d) {
                d.reel = $("input:radio[name ='reel']:checked").val();
                d.entryFilter = $('#EntryType').val();
                d.OrderItem = $('#ItemOrder').val();
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });

    // handles changing the length of each page in the pagination functionality of datatables plugin
    $('#pageLength').change(function () {
        holdTransTable.page.len($(this).val());
        holdTransTable.draw();
    });

    // item number suggestions engine
    httypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/Admin/HoldTransactions/holdTransactionsOrderItem?type='),
            replace: function (url, uriEncodedQuery) {
                return url += $('#EntryType').val() + '&orderitem=' + $('#ItemOrder').val() + '&reel=' + $("input:radio[name ='managerelradio']:checked").val();
            },
            filter: function (data) {
                return data;
            },
            cache: false
        }
    });
    httypeahead.initialize();

    // order number/item number typeahead
    $('#ItemOrder').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "item",
        displayKey: 'Number',
        source: httypeahead.ttAdapter(),
        templates: {
            header: '<p style="width:50%;" class="typeahead-header">Order/' + $('#ItemOrder').attr('name') + '</p>',
            suggestion: Handlebars.compile("<p style='width:50%;' class='typeahead-row'>{{Number}}</p><p style='width:50%;' class='typeahead-row'>{{Info}}</p>")
        }
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', $(this).css('width')).css('left','auto');
    }).on('typeahead:selected', function (obj, datum, name) {
        holdTransTable.draw();
    });
});