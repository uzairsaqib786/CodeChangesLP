// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var pager;

$(document).ready(function () {
    // transaction list paging plugin
    pager = STPaging({
        isTable: false,
        PerPage: 5,
        ID: "#trans_container",
        RowSelector: '.transaction',
        Source: batchHub.server.getTransactionsForTote,
        RowFunction: function (elem) {
            return makeTransaction(elem[0], elem[1], elem[3], elem[9], elem[6], elem[5], elem[2], elem[4], elem[7], elem[8], elem[10]);
        },
        NextID: '#TransNext',
        PrevID: '#TransPrev',
        getExtraParams: function () {
            return [
                $('#Item').val(),
                $('#InputType').val(),
                TransFilter.getFilterString()
            ];
        },
        PageID: '#STPage',
        processExtraData: function (result) {
            if (result.success) {
                $('#SubCategory').val(result.extraData.subCategory);
                $('#trans_input_type').val(result.extraData.inputType);
                $('#ItemNumber').val(result.extraData.itemNumber);
                $('#trans_item').html(result.extraData.itemNumber);
                $('#trans_desc').html(result.extraData.itemDescription);
                $('#Description').val(result.extraData.itemDescription);
            } else {
                MessageModal('Error', 'There was an error while trying to get the next page of transactions.  Check the error log for details.');
                $('#trans_modal').modal('hide');
            };
        },
        waitInterval: 500
    });
    pager.init();
    // make an html transaction representation
    function makeTransaction(otid, order, htid, wh, loc, sn, ln, qty, uf1, uf2, zone) {
        return '<div class="row transaction top-spacer">\
                    <input type="hidden" class="zone" value="' + (zone) + '" />\
                    <div class="col-xs-2">\
                        <label>Order Number</label>\
                        <input type="text" data-colname="[Order Number]" class="form-control input-sm order-number" readonly="readonly" value="' + (order) + '" />\
                    </div>\
                    <div class="col-xs-2">\
                        <label>Host Transaction ID</label>\
                        <input type="text" data-colname="[Host Transaction ID]" class="form-control input-sm htid" readonly="readonly" value="' + (htid) + '" />\
                    </div>\
                    <div class="col-xs-1">\
                        <label>Warehouse</label>\
                        <input type="text" data-colname="[Warehouse]" class="form-control input-sm whse" readonly="readonly" value="' + (wh) + '" />\
                    </div>\
                    <div class="col-xs-1">\
                        <label>Location</label>\
                        <input type="text" data-colname="[Location]" class="form-control input-sm location" readonly="readonly" value="' + (loc) + '" />\
                    </div>\
                    <div class="col-xs-2">\
                        <label>Serial Number</label>\
                        <input type="text" data-colname="[Serial Number]" class="form-control serial-number input-sm" readonly="readonly" value="' + (sn) + '" />\
                    </div>\
                    <div class="col-xs-2">\
                        <label>Lot Number</label>\
                        <input type="text" data-colname="[Lot Number]" class="form-control input-sm lot" readonly="readonly" value="' + (ln) + '" />\
                    </div>\
                    <div class="col-xs-1">\
                        <label>Trans. Qty</label>\
                        <input type="text" data-colname="[Transaction Quantity]" class="form-control input-sm qty" readonly="readonly" value="' + (qty) + '" />\
                    </div>\
                    <div class="col-xs-1">\
                        <label style="visibility:hidden;">Select</label>\
                        <button type="button" class="btn btn-primary btn-block select-transaction" name="' + otid + '">Select</button>\
                    </div>\
                    <div class="col-xs-6">\
                        <label>' + $('#UF1').siblings('label').html() + '</label>\
                        <input type="text" data-colname="[User Field1]" class="form-control input-sm uf1" readonly="readonly" value="' + (uf1) + '" />\
                    </div>\
                    <div class="col-xs-6">\
                        <label>' + $('#UF2').siblings('label').html() + '</label>\
                        <input type="text" data-colname="[User Field2]" class="form-control input-sm uf2" readonly="readonly" value="' + (uf2) + '" />\
                    </div>\
                </div>';
    };
});