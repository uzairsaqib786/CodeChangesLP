// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// Handles initialization and launching of the user fields modal
var trigger_userfields_modal;

// user field view
var UFView;
// user field transaction id
var UFTransaction;

$(document).ready(function () {
    var activeUF = 0;
    trigger_userfields_modal = function (sender, view) {
        UFView = view;
        
        // UFTransaction must be set to the transaction ID of the desired entry.

        globalHubConn.server.getUserFields(UFTransaction, view).done(function (result) {
            if (result.length == 10) {
                var curr;
                for (var x = 1; x <= 10; x++) {
                    curr = $('#UF' + x);
                    curr.val(result[x - 1]);
                    curr.attr('name', result[x - 1]);
                };
            };
        });
        $('#userfields_modal').modal('show');
    };

    // Handles click modal launch for manual transactions
    $(document.body).on('click', '.userfields-modal-manual', function () {
        trigger_userfields_modal($(this), 'Manual Transactions');
    });

    // handles input for enabling/disabling save
    $(document.body).on('input', '#UF1, #UF2, #UF3, #UF4, #UF5, #UF6, #UF7, #UF8, #UF9, #UF10', function () {
        if ($(this).val() != $(this).attr('name')) {
            $('#userfields_save, #userfields_sd').removeAttr('disabled');
        } else {
            $('#userfields_save, #userfields_sd').attr('disabled', 'disabled');
        };
    });

    // saves user fields
    $(document.body).on('click', '#userfields_save, #userfields_sd', function () {
        $('#userfields_save, #userfields_sd').attr('disabled', 'disabled');
        var userfields = Array();
        $('#UF1, #UF2, #UF3, #UF4, #UF5, #UF6, #UF7, #UF8, #UF9, #UF10').each(function () {
            userfields.push(this.value == null ? '' : this.value);
        });
        globalHubConn.server.saveUserFields(userfields, UFTransaction, UFView).done(function () {
            $('#UF1, #UF2, #UF3, #UF4, #UF5, #UF6, #UF7, #UF8, #UF9, #UF10').each(function () {
                var $this = $(this);
                $this.attr('name', $this.val());
            });
        });
    });

    $('#UF1').on('focus', function () {
        activeUF = 1;
    });

    $('#UF2').on('focus', function () {
        activeUF = 2;
    });

    // disables save when modal is closed
    $(document.body).on('click', '#userfields_dismiss', function () {
        $('#userfields_save, #userfields_sd').attr('disabled', 'disabled');
    });

    var ufs = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getUFs
            url: ('/Typeahead/getUFs?query='),
            replace: function(url, uriEncodedQuery) {
                return url + $('#UF' + activeUF).val() + '&ufs=' + activeUF;
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    var type = typeof dataObj;
                    if (type == 'string') {
                        return { Value: dataObj };
                    }
                    else { return dataObj; }
                });
            },
            cache: false
        }
    });
    ufs.initialize();

    $('#UF1, #UF2').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "ufs",
        displayKey: 'Value',
        source: ufs.ttAdapter()
    }).on('typeahead:selected', function (obj, data, name) {
        // triggers input on uf1 or uf2 depending on which was sender
        $(obj.currentTarget).trigger('input');
    });;
});