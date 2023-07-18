// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the supplier item id modal
var trigger_supitem_modal;

$(document).ready(function () {
    // supplier item id sending input element
    var supsender;

    trigger_supitem_modal = function () {
        $('#input-supplieritemid').typeahead('val',supsender.val());
        $('#supplieritemid_modal').modal('show');
    };

    var supplieritemid = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getsupplieritemid
            url: ('/Typeahead/getSupplierItemID?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    supplieritemid.initialize();

    var display = '<p class="typeahead-row " style="width:33%;">{{SupplierItemID}}</p><p class="typeahead-row " style="width:33%;">{{Description}}</p><p class="typeahead-row" style="width:33%">{{ItemNumber}}</p>';

    $('#input-supplieritemid').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "supplieritemid",
        displayKey: 'SupplierItemID',
        source: supplieritemid.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:33%;">Supplier Item ID</p><p class="typeahead-header " style="width:33%;">Description</p><p class="typeahead-header " style="width:33%;">Item Number</p>',
            suggestion: Handlebars.compile(display)
        }
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "600px").css('left','auto');
    });

    $('#input-supplieritemid').on('input', function () {
        if ($(this).val() == '') {
            $(this).typeahead('val', '');
        }
    });

    // launches the supplier item id modal
    $(document.body).on('click', '.supplieritemid-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        supsender = $this;
        trigger_supitem_modal();
    });

    // handles setting a supplier item id
    $(document.body).on('click', '#supplieritemid_submit', function () {
        var supplieritemid = $('#input-supplieritemid').val();
        supsender.val(supplieritemid);
        supsender.trigger('input');
    });
});