// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the manual transactions order modal
var trigger_manualorder_modal;

$(document).ready(function () {
    trigger_manualorder_modal = function () {
        var iteminput = $('#item-manual');
        var item = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            limit: 20,
            remote: {
                // call function in controller getitem
                url: ('/Typeahead/getItem?query=%QUERY'),
                filter: function (list) {
                    return $.map(list, function (dataObj) {
                        return dataObj;
                    });
                },
                cache: false
            }
        });
        item.initialize();

        var display = '<p class="typeahead-row " style="width:50%;">{{ItemNumber}}</p><p class="typeahead-row " style="width:50%;">{{Description}}</p>';

        iteminput.typeahead({
            hint: false,
            highlight: true,
            minLength: 0
        }, {
            name: "item",
            displayKey: 'ItemNumber',
            source: item.ttAdapter(),
            templates: {
                header: '<p class="typeahead-header" style="width:50%;">Item Number</p><p class="typeahead-header" style="width:50%;">Description</p>',
                suggestion: Handlebars.compile(display)
            }
        }).on('typeahead:opened', function () {
            $('.tt-dropdown-menu').css('width', "600px");
        });
        $('#input-manual').val($('#OrderNumber').val());
        $('#manual_modal').modal('show');
    };

    // launches modal
    $(document.body).on('click', '.manual-modal', function () {
        trigger_manualorder_modal();
    });

    // gets data related to item location after an item number is specified
    $(document.body).on('focusout', '#item-manual', function () {
        var item = $(this).val();
        var locSelect = $('#setLocByQty');
        locSelect.attr('disabled', 'disabled');
        globalHubConn.server.itemExists(item).done(function (exists) {
            if (exists) {
                locSelect.val('Getting data...');
                $('#MTAlert').html('');;
                MTHub.server.getQty(item).done(function (result) {
                    var appendstring = '<option value="0" selected="selected">No Location</option>';
                    for (var x = 0; x < result.length; x++) {
                        appendstring += '<option value="' + result[x].mapID + '">' + result[x].qty + ' @ ' + result[x].location + '</option>';
                    };
                    locSelect.html(appendstring);
                    locSelect.removeAttr('disabled');
                });
            } else {
                postMTAlertModal('Item Number not found!');
            };
        });
    });

    // checks to ensure the order number in a new manual transaction is not blank
    $(document.body).on('input', '#input-manual', function () {
        if (this.value.trim() == '') {
            postMTAlertModal('Order Number cannot be left blank!');
        } else {
            deleteMTAlertModal();
        };
    });

    // Handles saving a new manual transaction from the modal
    $(document.body).on('click', '#manual_submit', function () {
        var orderNumber = $('#input-manual').val();
        var itemNumber = $('#item-manual').val();
        var transType = $('#transType-manual').val();

        if (orderNumber == '') {
            postMTAlertModal('Order Number may not be left blank!');
        } else if (itemNumber == '') {
            postMTAlertModal('Item Number may not be left blank!');
        } else if (transType == '') {
            postMTAlertModal('Transaction Type may not be left blank!');
        } else {
            globalHubConn.server.itemExists(itemNumber).done(function (exists) {
                var invMapID = $('#setLocByQty :selected').val();
                if (exists) {
                    deleteMTAlertModal();
                    MTHub.server.saveNewTransaction(orderNumber, itemNumber, transType, invMapID).done(function (result) {
                        var typeahead = $('.forced-typeahead');
                        var input = typeahead.find('input');
                        input.tooltip('destroy');
                        input.val(orderNumber);
                        input.trigger('input');
                        typeahead.addClass('has-success').removeClass('has-warning').children('.glyphicon').removeClass('glyphicon-warning-sign').addClass('glyphicon-ok');
                        ManualTransactionID = result;
                        populateManualTransaction();
                        $('#manual_modal').modal('hide');
                    });
                } else {
                    postMTAlertModal('Item Number not found!');
                };
            });
        };
    });
});

// posts an alert in the manual transactions modal
var postMTAlertModal = function (message) {
    $('#addNewMT_alerts').html('<div class="alert alert-warning alert-custom" role="alert">' + message + '</div>')
};

// deletes manual transactions alerts
var deleteMTAlertModal = function () {
    $('#addNewMT_alerts').children().remove();
};