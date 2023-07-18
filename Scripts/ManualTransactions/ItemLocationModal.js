// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $(document.body).on('click', '#manualitem_submit', function () {
        var item = $('#itemmanual-modal').val();
        globalHubConn.server.getItemInfo(item).done(function (itemObj) {
            $('#ItemDescription').val(itemObj.description);
            $('#SupplierItemID').val(itemObj.supplierItemID);
            $('#ExpirationDate').prop('checked', itemObj.expDate);
            var warehouse = $('#Warehouse');
            var whBool = true;
            if (itemObj.warehousesensitive === "False") {
                whBool = false
            }
            whBool ? warehouse.addClass('required') : warehouse.removeClass('required');
    
        });
    });
    $('#itemLocation').on('focus', function () {
        $(this).typeahead('val', '');
    })

    // shows the modal and initializes it
    $(document.body).on('click', '#ItemNumber, .ItemNumberID', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        var item = $this.val();

        if (item == '') { $('#itemLocation').attr('disabled', 'disabled') }
        else{$('#itemLocation').removeAttr('disabled')}
        var locSelect = $('#setLocByQty-manualmodal');
        
        if ($('#itemmanualmodal').typeahead != undefined)
        {
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
            var iteminput = $('#itemmanual-modal');
            iteminput.typeahead({
                hint: false,
                highlight: true,
                minLength: 1
            }, {
                name: "item",
                displayKey: 'ItemNumber',
                source: item.ttAdapter(),
                templates: {
                    header: '<p class="typeahead-header" style="width:50%;">Item Number</p><p class="typeahead-header" style="width:50%;">Description</p>',
                    suggestion: Handlebars.compile(display)
                }
            }).on('typeahead:opened', function () {
                $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width'))
            }).on('typeahead:selected', function () {
                $('#itemLocation').removeAttr('disabled', 'disabled').typeahead('val', '').click();
            });


            var Location = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                limit: 200,
                remote: {
                    // call function in controller getitem
                    url: ('/ManualTransactions/getLocations?query='),
                    replace: function (url, uriEncodedQuery) {
                        return url + $('#itemmanual-modal').val();
                    },
                    filter: function (list) {
                        return $.map(list, function (dataObj) {
                            return dataObj;
                        });
                    },
                    cache: false
                }
            });
            Location.initialize();

            var display2 = '<p class="typeahead-row" style="width:33%;">{{mapID}}</p><p class="typeahead-row " style="width:33%;">{{qty}}</p><p class="typeahead-row " style="width:33%;">{{location}}</p>';
            var locationInput = $('#itemLocation');
            locationInput.typeahead({
                hint: false,
                highlight: true,
                minLength: 0
            }, {
                name: "location",
                displayKey: 'mapID',
                source: Location.ttAdapter(),
                templates: {
                    header: '<p class="typeahead-header" style="width:33%;">Inv Map ID</p><p class="typeahead-header" style="width:33%;">Qty</p><p class="typeahead-header" style="width:33%;">Location</p>',
                    suggestion: Handlebars.compile(display2)
                }
            }).on('typeahead:opened', function () {
                $(this).siblings('.tt-dropdown-menu').css('width',$(this).css('width'))
            }).on('typeahead:selected', function () {
                $(this).trigger('input').blur();
                Location.clearRemoteCache();
            });
        }
        $('#itemmanual-modal').typeahead('val',$this.val());
        $('#manualitem_modal').modal('show');
    });

    // toggles modal if the item exists when the submit button is clicked.  Queries for other data related to the item number and sets it
    $(document.body).on('click', '#manualitem_submit', function () {
        var item = $('#itemmanual-modal').val();
        globalHubConn.server.itemExists(item).done(function (exists) {
            if (exists) {
                $('#manualitem_modal').modal('hide');
                $('#itemMT_alerts').html('');
                $('#ItemNumber').val(item);
                var invMapID = $('#itemLocation').val();
                if (invMapID == '') {
                    invMapID = 0
                }
                if ($.isNumeric(invMapID) && invMapID > 0) {
                    $('#InvMapID').html(invMapID);
                    MTHub.server.getLocationData(invMapID).done(function (result) {
                        $('#Zone').html(result.Zone);
                        $('#Carousel').html(result.Carousel);
                        $('#Row').html(result.Row);
                        $('#Shelf').html(result.Shelf);
                        $('#Bin').html(result.Bin);

                        $('#LocQtyTot').html(result.TotalQty);
                        $('#LocQtyPick').html(result.PickQty);
                        $('#LocQtyPut').html(result.PutQty);
                    });
                };
            } else {
                $('#itemMT_alerts').html('<div class="alert alert-warning alert-custom" role="alert">Item Number not found!</div>');
            };
        });
    });

    // gets location data after an item number has been selected
    $(document.body).on('focusout', '#itemmanual-modal', function () {
        var item = $(this).val();
        globalHubConn.server.itemExists(item).done(function (exists) {
            if (exists) {
                $('#itemMT_alerts').html('');
                $('#itemLocation').removeAttr('disabled', 'disabled').click();
            } else {
                $('#itemMT_alerts').html('<div class="alert alert-warning alert-custom" role="alert">Item Number not found!</div>');
                $('#itemLocation').attr('disabled', 'disabled')
            };
        });
    });
    $(document.body).on('input', '#itemmanual-modal', function () {
        if ($(this).val().length == 0) {
            $(this).typeahead('val','').click();
        }
    })

   
});