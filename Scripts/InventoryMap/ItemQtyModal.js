// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var adjustReason = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
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
    adjustReason.initialize();

    $('#qtyreasonModal').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "adjustReason",
        displayKey: 'Value',
        source: adjustReason.ttAdapter()
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')).css("left", 'auto');
        $('.tt-dropdown-menu').css('width', "600px");
    });

    $('#qtySave').click(function () {
        var row = $('tr.active')
        var qty = $('#newQtyModal').val(), reason = $('#qtyreasonModal').val(), mapID = row.find(':nth-child(' + (columns.indexOf('Inv Map ID') + 1) + ')').html();
        if (reason.trim() == '') {
            alert('Must specify an adjustment reason!');
            return;
        };
        invMapHub.server.updateItemQuan(mapID, qty, reason).done(function () {
            $('#ItemQty').val(qty);
            row.find(':nth-child(' + (columns.indexOf('Item Quantity') + 1) + ')').html(qty)
            $('#iqModal').modal('hide');
        });
    });

    $(document.body).on('click', '.itemqty-modal', function () {
        if ($('#ItemNumber').val().trim() == '') { return false; };
        globalHubConn.server.selItemQtyData($('#InvMapID').val()).done(function (data) {
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