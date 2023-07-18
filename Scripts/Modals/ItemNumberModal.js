// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the item number modal
var trigger_item_modal;

$(document).ready(function () {
    var itemsender;
    trigger_item_modal = function () {
        var iteminput = $('#input-item');
        iteminput.val(itemsender.val()).trigger('input');
        $('#item_modal').modal('show');
    };

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

    $('#input-item').typeahead({
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

    // launches the item number modal
    $(document.body).on('click', '.item-modal', function () {
        itemsender = $(this);
        trigger_item_modal();
    });

    // handles submission of the item number
    $(document.body).on('click', '#item_submit', function () {
        var item = $('#input-item').val();
        itemsender.val(item);
        itemsender.trigger('input');
    });
});