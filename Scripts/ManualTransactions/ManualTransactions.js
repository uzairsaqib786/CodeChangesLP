// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// Gets transaction info by manual transaction id
var populateManualTransaction;
// initialize the id of the selected transaction
var ManualTransactionID = 0;
// data-original-title
var orderMTTitle = 'You must select an item from the suggestions provided in order to edit a transaction.';

$(document).ready(function () {
    $('#ManTranView').on('shown.bs.tab', function () {
        $('#OrderNumber').focus();
    });

    // emergency checkbox
    $('.toggles').toggles({
        width: 60,
        height: 25
    });
    // disable all inputs on load
    $('.container-fluid #ManTran input:not(#OrderNumber), .userfields-modal-manual, .container-fluid #ManTran select, #SaveTransaction, #PrintMT, #deleteTransaction, #PostTransaction').attr('disabled', 'disabled');
    initializeTypeahead();

    $("#OrderNumber").focus();
    // verify number inputs
    $('#Priority, #LineNumber, #LineSequence').on('input', function (e) {
        setNumeric($(this));
    });
    $('#Priority, #LineNumber, #LineSequence').on('focusout', function (e) {
        if (this.value.trim() == '') {
            this.value = 0;
        };
    });
    $('#TransQty').on('input', function (e) {
        setNumericInRange($(this), 1, SqlLimits.numerics.int.max);
    });
    $('#TransQty').on('focusout', function (e) {
        if (this.value.trim() == '') {
            this.value = 0;
        };
    });

    // clear the filters and data
    $('#ClearOrder').click(function () {
        clearMTInputs();
        var order = $('#OrderNumber, .manual-modal');
        order.removeAttr('disabled');
        $('#OrderNumber').typeahead('val','').focus();
    });


    $('#manual_submit').click(function () {
        ordernumber.clearRemoteCache();
        clearMTInputs();
    })

    $('#OrderNumber').on('input', function () {
        var orderNum = $('#OrderNumber')
        ordernumber.clearRemoteCache();
        if ($(this).val() == '') {
            $(this).typeahead('val', '')
        }
        clearMTInputs()
    });

    // disables order number and add new modal when there are changes made to select tags
    $('.container-fluid #ManTran input:not(#OrderNumber), .container-fluid #ManTran select').on('input', function () {
        $('#OrderNumber, .manual-modal').attr('disabled', 'disabled');
    });

    // gets data for manual transactions when a new supplier item id is selected
    $(document.body).on('click', '#supplieritemid_submit', function () {
        var supplier = $('#input-supplieritemid').val();
        var item = $('#ItemNumber');
        var expDate = $('#ExpirationDate');
        var description = $('#ItemDescription');
        var uom = $('#UnitOfMeasure');
        var warehouse = $('#Warehouse');

        globalHubConn.server.getSupplierItemIDInfo(supplier).done(function (result) {
            item.val(result.ItemNumber);
            expDate.prop('checked', result.expDate);
            description.val(result.Description);
            result.warehousesensitive ? warehouse.addClass('required') : warehouse.removeClass('required');
            if (result.uom != uom.val()) {
                if (uom.val() == '') {
                    uom.val(result.uom);
                } else {
                    alert('Unit of Measure does not match Inventory Master.  (Expecting ' + result.uom + ')');
                };
            };
        });
    });
});
// suggestion engine for the typeaheads
var ordernumber;
// initializes the typeaheads
function initializeTypeahead() {
    var order = $('#OrderNumber');
    ordernumber = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/ManualTransactions/getManualTransactions?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    ordernumber.initialize();

    var display = '<p class="typeahead-row " style="width:22%;">{{OrderNumber}}</p>\
        <p class="typeahead-row " style="width:22%;">{{TransactionType}}</p>\
        <p class="typeahead-row " style="width:22%;">{{ItemNumber}}</p>\
        <p class="typeahead-row " style="width:22%;">{{TransQty}}</p>';

    var header = '<p class="typeahead-header" style="width:22%;">Order Number</p>\
        <p class="typeahead-header" style="width:22%;">Trans. Type</p>\
        <p class="typeahead-header" style="width:22%;">Item Number</p>\
        <p class="typeahead-header" style="width:22%;">Trans. Quantity</p>';

    order.typeahead({
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
        $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width'));
    }).on('typeahead:selected', function (obj, data, name) {
        var typeahead = $('.forced-typeahead');
        var input = typeahead.find('input');
        input.tooltip('destroy');
        typeahead.addClass('has-success').removeClass('has-warning').children('.glyphicon').removeClass('glyphicon-warning-sign').addClass('glyphicon-ok');
        ManualTransactionID = data.ID;
        populateManualTransaction();
    });
};
// clears the inputs in manual transactions
function clearMTInputs() {
    deleteMTAlert();
    var typeahead = $('.forced-typeahead');
    var input = typeahead.find('input');
    input.attr('title', orderMTTitle);
    input.tooltip();
    typeahead.removeClass('has-success').addClass('has-warning').children('.glyphicon').addClass('glyphicon-warning-sign').removeClass('glyphicon-ok');
    $('.loc,.container-fluid #ManTran input:not(#OrderNumber), .container-fluid #ManTran span.label-default, .container-fluid #ManTran select, #SaveTransaction, #PrintMT, #deleteTransaction, #PostTransaction').each(function () {
        var $this = $(this);
        if ($this.attr('[type="checkbox"]')) {
            $this.prop('checked', false);
            $this.attr('disabled', 'disabled');
        } else if ($this.hasClass('loc')) {
            $this.html('');
        }else {
            $this.val('');
            $this.attr('disabled', 'disabled');
        };
    });
    $('.userfields-modal-manual').attr('disabled', 'disabled');
    $('i.modal-launch-style').addClass('disabled');
};

// posts an alert to the manual transactions alerts
var postMTAlert = function(message) {
    $('#MTAlerts').html('<div class="alert alert-warning alert-custom" role="alert">' + message + '</div>');
};

// deletes alerts from manual transactions alerts
var deleteMTAlert = function () {
    $('#MTAlerts').html('');
};
