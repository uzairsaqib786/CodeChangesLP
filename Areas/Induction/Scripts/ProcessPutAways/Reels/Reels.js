// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    
    addResize(function () { $('#reel_container').css('max-height', $(window).height() * 0.65); });
    // evenly divide the number of parts over the number of reels to create and assign each portion to a batch
    // then update how many are unassigned to a reel
    $('#CreateReels').click(function () {
        var parts = $('#TotalParts').val();
        var reels = $('#NumReels').val();
        var partsPerReel = Math.floor(parts / reels);
        var container = $('#reel_container');
        container.html(null);
        batchHub.server.getNextSerialNumber(reels).done(function (nextSN) {
            for (var x = 0; x < reels; x++) {
                container.append(makeReel('', partsPerReel, $('#HReelOrderNumber').val(), $('#HLotNumber').val(), $('#HUF1').val(), $('#HUF2').val(), $('#HWarehouse').val(), $('#HExpDate').val(), $('#HNotes').val()));
            };
            container.find('[data-toggle="tooltip"]').tooltip();
            updateRemaining();
            $('#reel_container').find('input.serial-number:text[value=""]').filter(':first').focus();
        });
    });
    
    $(document.body).on('keyup', '.serial-number', function (e) {
        if (e.keyCode !== 13) {
            return;
        }
        $(this).parent().parent().next().find('.serial-number').select();
    });

    // complete the reels.
    $('#reeloverview_submit').click(function () {
        if (confirm('Click OK to create these reels.')) {  
            if ($('#reel_container').html().trim() == '') {
                MessageModal('Error', 'You must provide at least one reel transaction in order to create reels.');
            } else {
                var numUnassigned = parseInt($('#PartsRemaining').val());
                if (numUnassigned != 0) {
                    if (!confirm('There are ' + (numUnassigned < 0 ? 'more' : 'fewer') + ' parts assigned to these reels than total parts selected.  Click OK to continue or Cancel to edit the number of parts in each reel.')) {
                        return;
                    };
                };
                var reels = [];
                var rc$;
                var SNs = [];
                var sn = '';
                // ensure that there are no current duplicates in the html.
                $.each($('#reel_container').children(), function (index, element) {
                    rc$ = $(element);
                    sn = rc$.find('.serial-number').val().trim();
                    if (sn == '') {
                        MessageModal('Error', 'You must provide a serial number for each reel transaction.');
                        return;
                    } else if (SNs.indexOf(sn) != -1) {
                        MessageModal('Error', 'You must provide a unique serial number for each reel transaction.  Serial ' + sn + ' is duplicated.');
                        return;
                    };
                    SNs.push(rc$.find('.serial-number').val());
                    reels.push([rc$.find('.serial-number').val(), rc$.find('.reel-qty').val(),
                        rc$.find('.order-number').val(), rc$.find('.lot-number').val(), rc$.find('.uf1').val(),
                        rc$.find('.uf2').val(), rc$.find('.warehouse').val(), rc$.find('.exp-date').val(), rc$.find('.notes').val()]);
                });
                // make sure there are no duplicates/allocations in open transactions or inventory map.
                batchHub.server.checkValidSN(SNs).done(function (validList) {
                    var errs = '';
                    for (var x = 0; x < validList.length; x++) {
                        if (!validList[x].valid) {
                            errs += (SNs[x] + ' is invalid because it is already allocated ' + (validList[x].reason == 'OT' ? 'to a Put Away in Open Transactions.' : 'in Inventory Map') + '<br>');
                        };
                    };
                    if (errs != '') {
                        MessageModal('Error', 'The following serial numbers have problems and could not be assigned.<br><br>' + errs);
                    } else {
                        // create the reel transactions
                        batchHub.server.createReels($('#ReelItem').val(), reels).done(function (otids) {
                            if (otids.length <= 0) {
                                MessageModal('Error', 'There was an error while attempting to save the new reels.  See the error log for details.');
                            } else {
                                if (confirm('Click OK to print labels now.')) {
                                    var pd = ($('#PrintDirect').val().toLowerCase() == 'true');
                                    printReels(otids, '', '', '', pd);
                                    if (pd) {
                                        while (!confirm('Click OK if the labels printed correctly.  Click Cancel to reprint the labels.')) {
                                            printReels(otids, '', '', '');
                                        };
                                    };
                                };
                                $('#reeloverview_modal').modal('hide');
                                $('#Item').val(SNs[0]);
                                $('#AssignTrans').trigger('click');
                            };
                        });
                    };                    
                });
            };
        };
    });

    // print reel labels
    function printReels(otids, sn, order, item, pd) {
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintReelLabels/', {
            OTIDs:otids,
            SN: sn,
            Order: order,
            Item: item,
            printDirect: pd
        }, pd,'label', 'Reel Label Preview');
    };


    $('#ReelQty').on('keyup', function (e) {
        if (e.keyCode !== 13) {
            return;
        }
        $('#reeldetail_submit').trigger('click');
    });

    // set the order number, number in a single reel, etc.
    $('#reeldetail_submit').click(function () {
        var order = $('#ReelOrder').val();
        if (order.trim() == '') { order = $('#HReelOrderNumber').val(); };
        var lot = $('#ReelLot').val();
        if (lot.trim() == '') { lot = 0; };
        var qty = $('#ReelQty').val();
        if (qty.trim() == '') {
            MessageModal('Error', 'You must provide a quantity for this reel.');
            return;
        };
        var whse = $('#ReelWarehouse').prop('disabled');
        var warehouse = $('#ReelWarehouse').val();
        if (!whse && (warehouse.trim() == '' || warehouse.trim().toLowerCase() == 'none')) {
            MessageModal('Error', 'This item is warehouse sensitive.  Assign a warehouse to the reel in order to create the transaction.', function () {
                $('#ReelWarehouse').click();
            });
            return;
        };
        // if we're setting the data for a transaction that has already been generated by the auto generate reels button then:
        if (typeof (detailOpenedBy) != 'undefined' && detailOpenedBy.hasClass('edit-reel')) {
            detailOpenedBy.siblings('.order-number').val(order);
            detailOpenedBy.siblings('.lot-number').val(lot);
            detailOpenedBy.siblings('.uf1').val($('#ReelUF1').val());
            detailOpenedBy.siblings('.uf2').val($('#ReelUF2').val());
            detailOpenedBy.siblings('.warehouse').val($('#ReelWarehouse').val());
            detailOpenedBy.siblings('.exp-date').val($('#ReelExpDate').val());
            detailOpenedBy.siblings('.notes').val($('#ReelNotes').val());
            detailOpenedBy.parent().parent().find('.reel-qty').val($('#ReelQty').val());
        } else { // we have not set up the reel yet, so we need to initialize the hidden inputs for all reels.
            $('#HReelOrderNumber').val(order);
            $('#HLotNumber').val(lot);
            $('#HUF1').val($('#ReelUF1').val());
            $('#HUF2').val($('#ReelUF2').val());
            $('#HWarehouse').val($('#ReelWarehouse').val());
            $('#HExpDate').val($('#ReelExpDate').val());
            $('#HNotes').val($('#ReelNotes').val());
            $('#TotalParts,#PartsRemaining').val($('#ReelQty').val());
        };
        $('#reeldetail_modal').modal('hide');
        $('#NumReels').select();

    });

    $('#ReelQty').on('input', function () {
        setNumericInRange($(this), 1, null);
    });

    $('#TotalParts').on('input', function () {
        $('#ReelQty,#NumReels').trigger('input');
        updateRemaining();
    });

    // launch the warehouse modal
    $('#ReelWarehouse,.reel-warehouse').click(function () {
        var $this = $(this);
        if ($this.hasClass('reel-warehouse')) {
            $this = $('#ReelWarehouse');
        };
        if ($this.attr('disabled') != 'disabled') {
            trigger_warehouse_modal($this, function () {
                $('#ReelQty').focus();
            });
        };
    });

    $(document.body).on('click', '.print-reel', function () {
        var $this = $(this);
        var row = $this.parent().parent();
        printReels([], row.find('.serial-number').val(), $this.siblings('.order-number').val(), $('#ReelItem').val(), $('#PrintDirect').val().toLowerCase() == 'true');
    });

    // edit a reel transaction
    var detailOpenedBy;
    $(document.body).on('click', '.edit-reel', function () {
        var $this = $(this);
        detailOpenedBy = $this;
        $('#ReelOrder').val($this.siblings('.order-number').val());
        $('#ReelLot').val($this.siblings('.lot-number').val());
        $('#ReelUF1').val($this.siblings('.uf1').val());
        $('#ReelUF2').val($this.siblings('.uf2').val());
        $('#ReelWarehouse').val($this.siblings('.warehouse').val());
        $('#ReelExpDate').val($this.siblings('.exp-date').val());
        $('#ReelNotes').val($this.siblings('.notes').val());
        $('#ReelQty').val($this.parent().parent().find('.reel-qty').val());
        $('#reeldetail_modal').modal('show');
    });
    
    $('#NumReels').on('input', function () {
        setNumericInRange($(this), 1, $('#TotalParts').val());
    });
    
    $('#NumReels').on('keyup', function (e) {
        if (e.which !== 13) {
            return;
        }
        $('#CreateReels').trigger('click');
    });
    
    $(document.body).on('input', '.reel-qty', function () {
        updateRemaining();
    });

    $(document.body).on('click', '.next-serial', function () {
        var $this = $(this);
        batchHub.server.getNextSerialNumber(1).done(function (nextSN) {
            $this.parent().parent().find('input.serial-number').val((nextSN) + '-RT');
        });
    });

    // update the qty remaining of the reel parts so that the user knows how many are left to allocate to a particular serial number
    function updateRemaining() {
        var total = parseInt($('#TotalParts').val());
        var counted = 0;
        $('#reel_container').find('input.reel-qty').each(function (i, e) {
            if ($.isNumeric(e.value)) {
                counted += parseInt(e.value);
            };
        });
        $('#PartsRemaining').val(parseInt(total - counted));
    };

    // make an html reel
    function makeReel(sn, qty, order, ln, uf1, uf2, wh, exp, notes) {
        return '<div class="row top-spacer">\
                    <div class="col-md-3">\
                        <input type="text" placeholder="Serial Number" class="form-control input-sm serial-number" value="' + (sn) + '" />\
                    </div>\
                    <div class="col-md-2">\
                        <button type="button" class="btn btn-primary next-serial">Next Serial Number</button>\
                    </div>\
                    <div class="col-md-1">\
                        <input placeholder="Part Qty" type="text" class="form-control input-sm reel-qty" value="' + (qty) + '" />\
                    </div>\
                    <div class="col-md-6">\
                        <button data-toggle="tooltip" data-placement="left" data-original-title="Print Reel Label" type="button" class="btn btn-primary print-reel"><span class="glyphicon glyphicon-print"></span></button>\
                        <button data-toggle="tooltip" data-placement="right" data-original-title="Edit Details" type="button" class="btn btn-primary edit-reel"><span class="glyphicon glyphicon-pencil"></span></button>\
                        <input type="hidden" class="order-number" value="' + (order) + '" /><input type="hidden" class="lot-number" value="' + (ln) + '" /><input type="hidden" class="uf1" value="' + uf1 + '" />\
                        <input type="hidden" class="uf2" value="' + (uf2) + '" /><input type="hidden" class="warehouse" value="' + (wh) + '" /><input type="hidden" class="exp-date" value="' + exp + '" />\
                        <input type="hidden" class="notes" value="' + (notes) + '" />\
                    </div>\
                </div>';
    };
});