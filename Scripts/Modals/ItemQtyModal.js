// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the item quantity modal
var trigger_item_qty_modal;

$(document).ready(function () {
    // quantity sender to set the value for
    var qtysender;

    trigger_item_qty_modal = function () {
        var invMapID = $('.activeRow input[name="Inv Map ID"]').val();
        if (invMapID <= 0) {
            invMapID = -1;
        };

        getTypeahead();

        globalHubConn.server.selItemQtyData(invMapID).done(function (data) {
            $('#itemNumModal').val(data[0]);
            $('#descriptionModal').val(data[1]);
            $('#newQtyModal').val("");
            $('#qtyreasonModal').val("");
            $('#currentLocModal').val(data[2]);
            $('#locZoneModal').val(data[3]);
            $('#qtyPickModal').val(data[4]);
            $('#locationModal').val('' + data[5] + ' ' + data[6] + ' ' + data[7] + ' ' + data[8] + ' ' + data[9]);
            $('#qtyPutModal').val(data[10]);
            $('#maxQtyCurrent').val($('.activeRow input[name="Maximum Quantity"]').val());
            $('#minQtyCurrent').val($('.activeRow input[name="Min Quantity"]').val());
            $('#iqModal').modal('show');
        });
    };
    
    // launches the item quantity modal
    $(document.body).on('click', '.itemqty-modal', function (e) {
        var $this = $(this);
        if($('.activeRow input[name="Item Number"]').val() == "" || $('#isAdmin').val() != "True")
        {
            return;
        }
        if ($this.val() == 0) {
        } else if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
            if ($this.val() == 0) {
            };
        };
        qtysender = $this;
        trigger_item_qty_modal();
    });

    // handles saving the item quantity specified
    $(document.body).on('click', '#qtySave', function () {
        var qty = $('#newQtyModal').val();
        var reason = $('#qtyreasonModal').val();
        var mapID = $('.activeRow input[name="Inv Map ID"]').val();

        if (!$.isNumeric(qty) || qty < 0) {
            $('#modalAlert').html('<div class="alert alert-warning alert-custom" role="alert">Quantity must be a non-negative integer.</div>');
        } else {
            if (reason == '') {
                $('#modalAlert').html('<div class="alert alert-warning alert-custom" role="alert">You must specify a reason for the adjustment.</div>');
            } else {
                $('#modalAlert').html('');
                alert(mapID, qty, reason)
                invMapHub.server.updateItemQuan(mapID, qty, reason).done(function () {
                    alert("Ay")
                    qtysender.val(qty);
                    $('#iqModal').modal('hide');
                });
            };
        };
    });

    function getTypeahead() {
        var locations = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            limit: 20,
            remote: {
                // call function in controller getLocations
                url: ('/Typeahead/getAdjustmentReasons?query=%QUERY'),
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
        locations.initialize();

        $('#qtyreasonModal').typeahead({
            hint: false,
            highlight: true,
            minLength: 0
        }, {
            name: "locations",
            displayKey: 'Value',
            source: locations.ttAdapter()
        }).on('typeahead:opened', function () {
            $('.tt-dropdown-menu').css('width', "600px");
        });
    };

    $(document.body).on('click', '#adjustQuantity', function () {
        var row = $('tr.active')
        var InvMapID = row.find(':nth-child(' + (columns.indexOf('Inv Map ID') + 1) + ')').html();
        var ItemNumber = row.find(':nth-child(' + (columns.indexOf('Item Number') + 1) + ')').html();
        if (ItemNumber == '') { return false; };
        globalHubConn.server.selItemQtyData(InvMapID).done(function (data) {
            if (data[0].trim() == '') {
                alert('No item found at the location specified.  Ensure that the entry selected has been saved since an item was assigned to it.');
                return false;
            };
            $('#itemNumModal').val(data[0]);
            $('#descriptionModal').val(data[1]);
            $('#newQtyModal').val(0);
            $('#qtyreasonModal').val("");
            $('#currentLocModal').val(data[2]);
            $('#locZoneModal').val(data[3]);
            $('#qtyPickModal').val(data[4]);
            $('#locationModal').val('' + data[5] + ' ' + data[6] + ' ' + data[7] + ' ' + data[8] + ' ' + data[9]);
            $('#qtyPutModal').val(data[10]);
            $('#maxQtyCurrent').val($('#MaxQty').val());
            $('#minQtyCurrent').val($('#MinQty').val());
            $('#iqModal').modal('show');
        });
    });
});