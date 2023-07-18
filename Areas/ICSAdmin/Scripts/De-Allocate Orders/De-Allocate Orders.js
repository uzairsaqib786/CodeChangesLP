// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var selectedOrders = new Array();
var orderNumberTypeahead;
var itemNumberTypeahead;
var itemTable;
var tableInitWidth;
var DeAllocate = $.connection.deAllocateHub;
$(document).ready(function () {

    // deallocates all orders in open transactions
    $('#deAllocateAll').click(function () {
        if (confirm('Click OK to deallocate ALL orders in the Open Transactions table.')) {
            if (confirm('Are you sure?  Clicking OK will deallocate ALL orders!  Click OK to deallocate ALL orders.')) {
                DeAllocate.server.deallocate('ALL').done(function () {
                    $('#orderBody').children().remove();
                    selectedOrders = new Array();
                    itemTable.draw();
                    itemNumberTypeahead.clearRemoteCache();
                    orderNumberTypeahead.clearRemoteCache();
                });
            };
        };
    });

    // deallocates selected transactions from open transactions
    $('#deAllocateSelected').click(function () {
        if (confirm('Click OK to deallocate selected orders')) {
            var orders = '';
            $.each(selectedOrders, function (index, element) {
                orders += element + ',';
            });
            DeAllocate.server.deallocate(orders).done(function () {
                $.each($('#orderBody tr td input[type="checkbox"]:checked'), function (index, element) {
                    $(this).parent().parent().remove();
                });
                itemTable.draw();
                itemNumberTypeahead.clearRemoteCache();
                orderNumberTypeahead.clearRemoteCache();
            });
        };
    });

    // order number typeahead engine
    orderNumberTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 60,
        remote: {
            url: ('/Admin/DeAllocateOrders/getAllocatedOrders?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return { Value: dataObj };
                });
            },
            cache: false
        }
    });

    orderNumberTypeahead.initialize();

    $('#orderLookup').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "orderNumberTypeahead",
        displayKey: 'Value',
        source: orderNumberTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Order Number</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{Value}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        getAllOrders();
    }).on('typeahead:opened', function () {
        var $this = $(this)
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width'));
    });;
    // item number typeahead engine
    itemNumberTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 60,
        remote: {
            url: ('/Admin/DeAllocateOrders/getAllocatedItems?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return { Value: dataObj };
                });
            },
            cache: false
        }
    });

    itemNumberTypeahead.initialize();

    $('#itemLookup').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "itemNumberTypeahead",
        displayKey: 'Value',
        source: itemNumberTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Item Number</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{Value}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        getAllOrders();
    }).on('typeahead:opened', function () {
        var $this = $(this)
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width'));
    });
    // Gets filtered data for the datatable for deallocation
    $('#itemLookup,#orderLookup').on('input', function () {
        var $this = $(this);
        if ($this.val() == '') {
            $this.typeahead('val', '')
        }
        getAllOrders();
    });
    // initializes the datatable
    itemTable = $('#orderTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Admin/DeAllocateOrders/getOrderItemsTable",
            "data": function (d) {
                d.ordernum = $('tr.active td:first').text();
                d.nameStamp = $("input:radio[name ='showTrans']:checked").val();
                d.transType = $("input:radio[name ='transType']:checked").val();
                d.filter = (DeAllocFilter == "" ? "" : DeAllocFilter.getFilterString())
            }
        }, "createdRow": function (row, data, index) {
            var $row = $(row);
            var view = $("input:radio[name ='showTrans']:checked").val();
            var inputString = ""
            if (view == 'spec') {
                inputString = "<input type='checkbox' checked class='selectOrder' name='" + data[0] + "' disabled='disabled'/>"
            } else {
                inputString = "<input type='checkbox' checked class='selectOrder' name='" + data[0] + "'/>"
            }
            if (selectedOrders.indexOf(data[0]) == -1) {
                inputString = inputString.replace('checked', '')
            }
            $row.children('td:nth-child(9)').html(inputString).addClass('filter-ignore')
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });
    tableInitWidth = $('#orderTable').css('width');
    $('#orderTable').wrap('<div style="overflow-x:scroll;"></div>');

    //Handles switching between order view and transaction view
    $("input:radio[name ='showTrans']").change(function () {
        if ($(this).val() == 'All') {
            $('#orderTable').css('width', "100%");
            $('#specOrdersDiv').hide()
            $('#showTransactionsDiv').addClass('col-md-12').removeClass('col-md-9');
            $('#itemLookup, #orderLookup, #deAllocateSelected').attr('disabled', 'disabled');
        } else {
            $('#orderTable').css('width', tableInitWidth);
            $('#specOrdersDiv').show()
            $('#showTransactionsDiv').addClass('col-md-9').removeClass('col-md-12');
            $('#itemLookup, #orderLookup').removeAttr('disabled');
            if ($("input:checked:not(input:radio)").length > 0) {
                $('#deAllocateSelected').removeAttr('disabled');
            }
        }
        DeAllocFilter.clearFilter();
        itemTable.columns.adjust().draw();
    });

    //Handles change of Transaction Type for Table
    $("input:radio[name ='transType']").change(function () {
        DeAllocFilter.clearFilter();
        getAllOrders()
    });

    // handles selecting or unselecting an order to deallocate
    $(document.body).on('click', '.selectOrder', function () {
        var $this = $(this);
        var orderNumber = $this.attr('name');
        if ($this.prop('checked')) {
            selectedOrders.push(orderNumber);
            $('#deAllocateSelected').removeAttr('disabled');
            $('input[type="checkbox"][name="' + orderNumber + '"]').prop('checked', true)
        } else {
            var index = selectedOrders.indexOf(orderNumber);
            $('input[type="checkbox"][name="' + orderNumber + '"]').prop('checked', false)
            if (index >= 0) {
                selectedOrders.splice(index, 1);
                if (selectedOrders.length == 0) {
                    $('#deAllocateSelected').attr('disabled', 'disabled');
                };
            };
        };
    });

    // handles selecting a row to deallocate
    $('#orders tbody').on('click', 'tr', function (e) {
        var $this = $(this);
        if (e.target.tagName != 'INPUT') {
            if ($this.hasClass('active')) {
                $this.removeClass('active');
            }
            else {
                $('tr.active').removeClass('active');
                $this.addClass('active');
            };
            DeAllocFilter.clearFilter();
            itemTable.draw();
        };
    });

    itemTable.page.len(15);
    itemTable.draw();

    function getAllOrders() {
        DeAllocate.server.getAllOrders($('#orderLookup').val(), $('#itemLookup').val(), $("input:radio[name ='transType']:checked").val()).done(function (data) {
            var orderTable = $('#orderBody');
            var checkedBoxes = $("input:checked:not(input:radio)");
            var holdRows = new Array();
            checkedBoxes.each(function () {
                holdRows.push($(this).parent().parent().clone());
            });
            orderTable.html('');
            holdRows.forEach(function (current) {
                orderTable.append(current);
            });
            data.forEach(function (current) {
                if (selectedOrders.indexOf(current) < 0) {
                    orderTable.append('<tr><td>' + current + '</td><td><input name="' + current + '" class="selectOrder" type="checkbox" /></td></tr>')
                };
            });
            itemTable.draw();
        });
    };
});