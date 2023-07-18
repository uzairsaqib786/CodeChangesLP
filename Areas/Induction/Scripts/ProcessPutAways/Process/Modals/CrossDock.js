// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var cd_pager;
var printDirect = false;
$(document).ready(function () {
    printDirect = ($('#PrintDirect').val().toLowerCase() == 'true');

    // closes the cross dock modal and clicks the button to restart the process
    $('#crossdock_submit').click(function () {
        $('#crossdock_modal').on('hidden.bs.modal', function () {
            $('#trans_set').click();
        });
        $('#crossdock_modal').modal('hide');
    });

    addResize(function () { $('#cd_container').css('max-height', $(window).height() * 0.5); });
    // cross dock paging plugin instance
    cd_pager = STPaging({
        isTable: false,
        PerPage: 100,
        ID: "#cd_container",
        RowSelector: '.cd',
        Source: batchHub.server.getCrossDock,
        RowFunction: function (cd) {
            return makeCrossDock(cd[0], cd[1], cd[2], cd[3], cd[4], cd[5], cd[6], cd[7], cd.slice(8, 18), cd[18]);
        },
        NextID: '#CDNext',
        PrevID: '#CDPrev',
        getExtraParams: function () {
            return [$('#CrossDockItem').val(), $('#Warehouse').val(), CrossDockFilter.getFilterString()];
        },
        PageID: '#CDPage',
        processExtraData: function (result) {
            if (!result.success) {
                MessageModal('Error', 'There was an error while trying to get the next page of transactions.  Check the error log for details.');
            } else {
                $('#cd_container').find('.cd:first').addClass('active-div');
            };
        },
        waitInterval: 500
    });
    cd_pager.init();

    // assigns new tote id to the highlighted transaction
    $('#CDNextTote').click(function () {
        batchHub.server.getNextTote().done(function (nexttote) {
            $('#cd_container .cd.active-div').find('.cd-tote').val((nexttote) + '-RT');
            batchHub.server.updateNextTote((nexttote + 1)).done(function (updated) {
                if (!updated) {
                    MessageModal('Error', 'Updating the next tote was unsuccessful.  Please check the error log for details.');
                };
            });
        });
    });

    // opens the user field modal for cross docking
    $('#CDUF').click(function () {
        var selected = $('#cd_container').find('div.cd.active-div');
        for (var x = 1; x <= 10; x++) {
            $('#CDUF' + x).val(selected.find('input.uf' + x).val());
        };
        $('#userfields_modal').modal('show');
    });

    $('#CDCompleteAllAvail').click(function () {
        // Find all html elements with class "cd" inside of the element with id "cd_container"
        const $transactionElements = $('#cd_container .cd');
        var availableQty = parseInt($('#CrossDockQty').val());

        // Loop over each transaction element and find the input with class data-colname="[Transaction Quantity]"
        $transactionElements.each(function () {
            $('#CrossDockQty').val(availableQty);
            if (availableQty === 0) {
                return;
            }
            const $transactionQuantityInput = $(this).find('[data-colname="[Transaction Quantity]"]');
            const $completedQuantityInput = $(this).find('[data-colname="[Completed Quantity]"]');
            const $transactionQuantity = parseInt($transactionQuantityInput.val());

            // Copy the value from the input with class data-colname="[Transaction Quantity]"
            // and populate the input with class data-colname="[Completed Quantity]"
            if (availableQty >= $transactionQuantity) {
                availableQty -= $transactionQuantity;
                $completedQuantityInput.val($transactionQuantityInput.val());
            } else {
                return;
            }
        });

        var $noToteIds = $transactionElements.filter(function () {
            return  $(this).find('[data-colname="[Tote ID]"]').val() === '' &&
                    $(this).find('[data-colname="[Completed Quantity]"]').val() !== '0';
        });

        if ($noToteIds.length > 0) {
            // If not, ask user if they would like these to auto populate the tote IDs
            const shouldAutoPopulate =
                confirm('Some transaction elements do not have a tote ID. Would you like to auto-populate these?');
            if (!shouldAutoPopulate) {
                return;
            }
            var nextTote;
            batchHub.server.getNextTote().done(function(tote) {
                nextTote = tote;
                batchHub.server.updateNextTote(tote + $noToteIds.length);

                $.each($noToteIds,
                    function() {
                        const $element = $(this);
                        $element.find('[data-colname="[Tote ID]"]').val(nextTote);
                        nextTote++;
                    });
                completeCrossDock($transactionElements, availableQty);
            });
        } else {
            completeCrossDock($transactionElements, availableQty);
        }
    });

    function completeCrossDock($transactionElements, availableQty) {
        const $recordsToUpdate = $transactionElements.filter(function () {
            return $(this).find('[data-colname="[Completed Quantity]"]').val() !== '0';
        });

        if ($recordsToUpdate.length === 0) {
            return;
        }
        
        batchHub.server.nextBatchID().done(function (batchId) {

            const crossDockRecords = [];

            const data = [];
            const positionList = [];
            const toteList = [];
            const orderList = [];
            var position = 1;
            $recordsToUpdate.each(function () {
                const id = $(this).find('.otid').val();
                const toteId = $(this).find('[data-colname="[Tote ID]"]').val();
                const orderNumber = $(this).find('[data-colname="[Order Number]"]').val();
                const completeQty = $(this).find('[data-colname="[Completed Quantity]"]').val();

                crossDockRecords.push({
                    'Id': id,
                    'Location': 'Cross Dock',
                    'ToteId': toteId,
                    'CompletedQuantity': completeQty,
                    'BatchId': batchId
                });

            });
            const requestData = {
                stringRecords: JSON.stringify(crossDockRecords),
                Direct: printDirect
            }

            getLLPreviewOrPrint('/IM/ProcessPutAways/CompleteCrossDocks', requestData, printDirect, 'list', 'Cross Dock Pick List');
            while (printDirect && !confirm('Press OK if print was successful')) {
                MessageModal("Print Request", "Resending Print");
                getLLPreviewOrPrint('/IM/ProcessPutAways/CompleteCrossDocks', requestData, printDirect, 'list', 'Cross Dock Pick List');
            };
            $('#NumberToAssign').val(availableQty);
            $.each($recordsToUpdate, function() {
                $(this).closest(".cd").remove();
            });
            if (availableQty === 0) {
                $('#crossdock_modal').modal('hide');
            }

        });
    }

    // sets the user fields and collapses the modal
    $('#userfields_submit').click(function () {
        var selected = $('#cd_container').find('div.cd.active-div');
        for (var x = 1; x <= 10; x++) {
            selected.find('input.uf' + x).val($('#CDUF' + x).val());
        };
        $('#userfields_modal').modal('hide');
    });

    // prints an item or tote label
    $('#PrintItem,#PrintTote').click(function () {
        var selected = $('#cd_container').children('.cd.active-div');
        var pd = ($('#PrintDirect').val().toLowerCase() == 'true');
        var otid = selected.children('.otid').val();
        var tote = (this.id == 'PrintTote');
        var toteID = selected.find('input.cd-tote').val();
        var completedQty = selected.find('input.cd-cqty').val();

        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintCrossDock', {
            tote: tote,
            rpid: otid,
            zonelabel: ('Zones: ' + $('#modal_zones').html().trim()),
            toteid: toteID,
            completedQuantity: completedQty,
            printDirect: pd
        }, pd,'label', 'Cross Dock Label Preview');
    });

    // opens order status with the reprocess transaction selected
    $('#ViewOS').click(function () {
        var os = window.open('/Transactions?viewToShow=1&App=IM&popup=True&OrderStatusOrder=' + ($('#cd_container').children('div.active-div').find('.cd-order').val()), '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
        try {
            os.focus();
        } catch (e) {
            console.log('Exception: ', e)
            MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.');
        };
    });

    // shows the reprocess transaction detail
    $('#ViewRP').click(function () {
        var otid = $('div.cd.active-div').children('.otid').val();
        batchHub.server.getRPDetail(otid).done(function (details) {
            var list = [
                $('#RPOrder'), $('#RPItem'), $('#RPTransType'), $('#RPLine'), $('#RPDescription'), $('#RPNotes'), $('#RPImportDate'), $('#RPImportBy'), $('#RPFilename'), $('#RPRequired'),
                $('#RPUoM'), $('#RPLot'), $('#RPExpDate'), $('#RPSerialNumber'), $('#RPRevision'), $('#RPWarehouse'), $('#RPLocation'), $('#RPTransQty'), $('#RPPriority'), $('#RPLabel'),
                $('#RPHTID'), $('#RPEmergency')
            ];
            for (var x = 1; x <= 10; x++) {
                list.push($('#RPUF' + (x)));
            };
            list.push($('#RPReason'));
            list.push($('#RPMessage'));
            $.each(list, function (i, e) {
                e.val(details[i]);
            });
            $('#rp_modal').modal('show');
        });
    });

    // completes a pick transaction
    $('#CDComplete').click(function () {
        var selected = $('#cd_container .cd.active-div');        
        var tote = selected.find('input.cd-tote');
        var cqty = selected.find('input.cd-cqty').val().trim();
        var tqty = $('#CrossDockQty').val();
        if ($('#SubCategory').val().toLowerCase() == 'reel tracking' && parseInt(cqty) != parseInt(tqty)) {
            MessageModal('Error', 'Reel tracking items may not be split at this time.  The quantities must be the same or the assignment cannot take place.');
        } else if (tote.val().trim() == '' && !confirm('Click OK to proceed without a tote ID.  Click Cancel to provide a tote ID.')) {
            // if cancel, js is short circuiting, so it does not prompt if it does not need to.
            tote.focus();
        } else {
            if (confirm('Click OK to complete this Cross Dock transaction.  The pick transaction will be posted as completed for the displayed quantity.  A matching put away record will be created for the Cross Dock quantity.')) {
                var pick = selected.find('.cd-tqty').val();
                var reel = $('#SubCategory').val().toLowerCase() == 'reel tracking';
                var ser = $('#SerialNumber').val(), htid = selected.find('.cd-htid').val(), rpid = selected.find('.otid').val(), otid = $('#OTID').val();
                var item = $('#ItemNumber').val(), uf1 = $('#UF1').val(), toteID = tote.val(), order = selected.find('.cd-order').val();
                var invmapid = $('#InvMapID').val(), whse = $('#Warehouse').val(), batch = $('#modal_batch').html();
                batchHub.server.completePick(cqty, tqty, reel, ser, htid, rpid, otid, item, uf1, toteID, order, invmapid, whse, batch).done(function (OTRecID) {
                    var cd = $('#CrossDockQty');
                    cd.val(parseInt(cd.val()) - parseInt($('#cd_container .cd.active-div').find('input.cd-cqty').val()));
                    $('#TransQty').val(cd.val());
                    $('#NumberToAssign').val(cd.val());
                    cd_pager.populate();
                    var opening = false;

                    console.log(OTRecID);
                    if ($('#AutoPrintCrossDock').val().toLowerCase() == 'true') {
                        if ($('#PrintDirect').val().toLowerCase() == 'true') {
                            AutoPrintCrossDock(true, OTRecID);
                            while (!confirm('Click OK if the tote label printed correctly.')) {
                                AutoPrintCrossDock(true, OTRecID);
                            };
                            AutoPrintCrossDock(false, OTRecID);
                            while (!confirm('Click OK if the item label printed correctly.')) {
                                AutoPrintCrossDock(false, OTRecID);
                            };
                        } else {
                            opening = true;
                            LLPreviewModal('Cross Dock Label Preview', 'preview');
                        };
                    };
                    if ($('#LLPreview_Modal').hasClass('in') || opening) {
                        $('#LLPreview_Modal').one('hidden.bs.modal', function () {
                            if ($('#SubCategory').val().toLowerCase() == 'reel tracking' || parseInt($('#CrossDockQty').val()) <= 0) {
                                $('.modal.in').modal('hide');
                            };
                        });
                    } else {
                        if ($('#SubCategory').val().toLowerCase() == 'reel tracking' || parseInt(cd.val()) <= 0) {
                            $('.modal.in').modal('hide');
                        };
                    };
                });
            };
        };
    });

    // highlight a different cross dock transaction
    $(document.body).on('click', 'div.cd', function () {
        var $this = $(this);
        if (!$this.hasClass('active-div')) {
            var selected = $('div.cd.active-div');
            
            if (selected.length > 0) {
                selected.removeClass('active-div');
                //var qty = selected.find('input.cd-cqty').val();
                //if (parseInt(qty) != 0) {
                //    MessageModal('Error', 'You must mark your current transaction as complete before allocating to another.  If you wish to cancel the allocation set the transaction quantity to 0.');
                //    return;
                //};
            };
            $this.addClass('active-div');
        };
    });

    // start a tote assignment
    $(document.body).on('click', '.cd-tote', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        launchToteModal($this, $('<div></div>'), '', $('<input />'));
    });

    // restricts the values for the completed quantity field
    $(document.body).on('input', '.cd-cqty', function () {
        var $this = $(this);
        var cdqty = $('#CrossDockQty').val();
        setNumericInRange($this, 0, cdqty);
        if ($('#SubCategory').val().toLowerCase() == 'reel tracking') {
            if ($this.val() != cdqty && cdqty != 0) {
                if (confirm('This item is a reel tracking item.  The full quantity must be allocated to the transaction.  Click OK to set the quantity to the full reel quantity or Cancel to set the quantity to 0.')) {
                    $this.val(cdqty);
                } else {
                    $this.val(0);
                };
            };
        };
    });

    $(document.body).on('keypress', '.cd-cqty', function (e) {
        if (e.which == 13) {
            $(this).focusout();
        };
    });

    $(document.body).on('focusout', '.cd-cqty', function () {
        var tote = $(this).parent().parent().parent('.cd').find('input.cd-tote');
        if (tote.val().trim() == '') {
            tote.click();
        };
    });
});

function AutoPrintCrossDock(isTote, OTID) {
    var pd = ($('#PrintDirect').val().toLowerCase() == 'true');
    var otid = OTID
    getLLPreviewOrPrint('/IM/ProcessPutAways/AutoPrintCrossDock', {
        tote: isTote,
        otid: otid,
        zonelabel: ('Zones: ' + $('#modal_zones').html().trim()),
        printDirect: pd
    }, pd, 'label', 'Cross Dock Label Preview');
};

function getToteId() {
    return $.ajax({
        type: 'GET',
        url: '/IM/ProcessPutAways/GetAndUpdateNextTote'
    });
}

// initializes the cross dock modal
function setupCrossDockModal(cd, allocated, backorder, item, description, qtyAvail) {
    $('#CrossDockItem').val(item);
    $('#CrossDockDescription').val(description);
    $('#CrossDockQty').val(qtyAvail);
    $('#Backorder').val(backorder);
    $('#Allocated').val(allocated);
    var c = $('#cd_container');
    for (var x = 0; x < cd.length; x++) {
        c.append(makeCrossDock(cd[x][0], cd[x][1], cd[x][2], cd[x][3], cd[x][4], cd[x][5], cd[x][6], cd[x][7], cd[x].slice(8, 18), cd[x][18]));
    };
};

// makes a cross dock transaction in html
function makeCrossDock(order, htid, idate, rdate, priority, tqty, cqty, tote, ufs, rpid) {
    var uf = '<input type="hidden" class="otid" value="' + (rpid) + '" />';
    for (var i = 0; i < ufs.length; i++) {
        uf += '<input type="hidden" class="uf' + (i + 1) + '" value="' + ufs[i] + '" />'
    };
    return '<div class="cd">' + uf + '\
                <div class="row">\
                    <div class="col-md-3">\
                        <label>Order Number</label>\
                        <input type="text" data-colname="[Order Number]" class="form-control input-sm cd-order" readonly="readonly"  value="' + (order) + '" />\
                    </div>\
                    <div class="col-md-3">\
                        <label>Host Trans. ID</label>\
                        <input type="text" data-colname="[Host Transaction ID]" class="form-control input-sm cd-htid" readonly="readonly"  value="' + (htid) + '" />\
                    </div>\
                    <div class="col-md-3">\
                        <label>Trans. Qty</label>\
                        <input type="text" data-colname="[Transaction Quantity]" class="form-control input-sm cd-tqty" readonly="readonly" value="' + (tqty) + '" />\
                    </div>\
                    <div class="col-md-3">\
                        <label>Completed Qty</label>\
                        <input type="text" data-colname="[Completed Quantity]" class="form-control input-sm cd-cqty" placeholder="Completed Qty" value="' + (cqty) + '" />\
                    </div>\
                </div>\
                <div class="row">\
                    <div class="col-md-3">\
                        <label>Import Date</label>\
                        <input type="text" data-colname="[Import Date]" class="form-control input-sm cd-import" readonly="readonly"  value="' + (idate) + '" />\
                    </div>\
                    <div class="col-md-3">\
                        <label>Required Date</label>\
                        <input type="text" data-colname="[Required Date]" class="form-control input-sm cd-required" readonly="readonly"  value="' + (rdate) + '" />\
                    </div>\
                    <div class="col-md-3">\
                        <label>Priority</label>\
                        <input type="text" data-colname="[Priority]" class="form-control input-sm cd-priority" readonly="readonly" value="' + (priority) + '" />\
                    </div>\
                    <div class="col-md-3">\
                        <div class="form-group has-feedback">\
                            <label>Tote ID</label>\
                            <input type="text" data-colname="[Tote ID]" class="form-control input-sm cd-tote modal-launch-style" readonly="readonly"  value="' + (tote) + '" />\
                            <i class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style cd-tote"></i>\
                        </div>\
                    </div>\
                </div>\
            </div>';
};