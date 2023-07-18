// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // assign a transaction to a tote
    $('#AssignTrans').click(function () {

        var item$ = $('#Item');
        var itype = $('#InputType').val().trim().toLowerCase();
        // apply any necessary strip scan
        applyStrip(item$, itype);
        // if we have an item and a selected tote then we can continue.
        if ($('#TotesTable tbody tr.active').length > 0 && item$.val().trim() != '') {
            // initialize our totes to assign a transaction to
            InitializeToteToAssignTo();
            $('#modal_zones').html(toteDT.row(0).data()[4].replace('Zones:', '').trim());
            $('#modal_batch').html($('#BatchID').val());
            // get initial transaction data to decide whether the item is a reel and what type of input was passed to our function
            batchHub.server.getTransactionsForTote(1, 2, [item$.val(), itype, '1=1']).done(function (result) {
                // if there were no errors
                if (result.success) {
                    // if the item is a reel
                    if (result.extraData.subCategory.trim().toLowerCase() == 'reel tracking') {
                        // if the input type is a serial number
                        if (result.extraData.inputType.trim().toLowerCase() == 'serial number') {
                            // if our preference says that we MUST select the transaction
                            if ($('#SelectIfOne').val().trim().toLowerCase() == 'true') {
                                showTrans();
                            } else {
                                // show hot put away
                                showHPA({ otid: result.pages[0][0], item: '' });
                            };
                        } else {
                            // initialize the reels modal for a new reel
                            var d = new Date();
                            var now = String(d.getFullYear()) + String(d.getMonth() + 1) + String(d.getDate()) + String(d.getHours()) + String(d.getMinutes()) + String(d.getSeconds()) + '-IM';
                            openReels({ order: now, uf1: '', uf2: '', lot: 0, warehouse: '', expdate: '', notes: '' },
                                { number: result.extraData.itemNumber, numReels: 1, totalParts: 0, description: result.extraData.itemDescription, whseRequired: result.extraData.whseSens });
                        };
                    } else { // it's not a reel
                        // if there's more than one or there's one transaction and the select if one preference is used then show the transaction(s)
                        if (result.pages.length > 1 || (result.pages.length == 1 && $('#SelectIfOne').val().trim().toLowerCase() == 'true')) {
                            showTrans();
                        } else if (result.pages.length > 0) { // show hot put away because there's only one
                            showHPA({ otid: result.pages[0][0], item: '' });
                        } else { // show hot put away but make it a new hot put away
                            showHPA({ otid: 0, item: result.extraData.itemNumber });
                        };
                    };
                } else {
                    if (result.message.toLowerCase() == 'invalid scan code' && itype == 'any') {
                        MessageModal('Error', 'The input code provided was not recognized as an Item Number, Lot Number, Serial Number, Host Transaction ID, Scan Code or Supplier Item ID.');
                    } else if (result.message.toLowerCase() == 'new item') {
                        if (confirm('The input code provided was not recognized.  Click OK to add the item to inventory or cancel to return.')) {
                            // add the item, then try to use the input scan code again.
                            var $handle = $(window.open('/InventoryMaster?App=IM&popup=True&NewItem=True&ItemNumber=' + $('#Item').val(), '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'));
                            try {
                                $handle[0].focus();
                                $handle.one('load', function () {
                                    $handle.one('unload', function () {
                                        $('#AssignTrans').click();
                                    });
                                });
                            } catch (e) {
                                console.log('Exception: ', e)
                                MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.');
                            };
                        };
                    } else if (result.message.toLowerCase() == 'reel in open transactions') {
                        MessageModal('Error', 'The provided Serial Number is already allocated to a batch in Open Transactions and cannot be assigned to another.');
                    } else if (result.message.toLowerCase() == 'reel in inventory map') {
                        MessageModal('Error', 'The provided Serial Number is already allocated in the Inventory Map and cannot be inducted again.');
                    } else if (result.extraData != null && result.extraData.inputType.toLowerCase() != 'any') {
                        MessageModal('Error', 'The input code provided was not recognized as a valid ' + $('#InputType').val());
                    } else {
                        MessageModal('Error', 'An unknown error occurred.  Check the error log for details.');
                    };
                };
            });
        } else {
            if ($('#TotesTable tbody tr.active').length <= 0) {
                MessageModal('All Totes Filled', 'All Totes have been filled within this batch. Press OK to trigger a batch complete', undefined, undefined, function () { $('#CompleteBatch').click(); });
            };
        };
    });

    // if the preference is set to strip the scan code then apply the rule.
    function applyStrip(item$, itype) {
        if ($('#ApplyStrip').val().toLowerCase().trim() == 'true' && item$.attr('name') != 'applied') {
            var stripLen = $('#StripLength').val();
            if (stripLen.trim() == '') { stripLen = 0; };
            var i = item$.val().trim();
            if ($('#StripSide').val().toLowerCase().trim() == 'right') {
                i = i.substring(0, i.length - stripLen);
            } else {
                i = i.substring(stripLen, i.length);
            };
            item$.val(i);
            item$.attr('name', 'applied');
        };
    };

    // initialize the highlighted tote and other totes in the select dropdown lists so that the user may assign a tote to the transaction
    function InitializeToteToAssignTo() {
        var tote = $('#AssignToTote');
        var pos = $('#AssignToPosition');
        var selected = toteDT.row('.active').data()[1];
        tote.html(null);
        pos.html(null);
        for (var x = 0; x < toteDT.rows().data().length; x++) {
            var r = toteDT.row(x).data();
                tote.append('<option name="' + (r[2] - r[3]) + '" value="' + r[1] + '">' + r[1] + '</option>');
                pos.append('<option name="' + (r[2] - r[3]) + '" value="' + r[1] + '">' + r[0] + '</option>');
        };
        tote.val(selected);
        pos.val(selected);
        if (tote.find('option:selected').attr("name") <= 0) {
            alert("The tote you've selected is already marked as full. Putting the item in this tote will go over the defined cells.")
        }
        $('#OpenCells').val(pos.children(':selected').attr('name'));
    };

    // select a transaction and decide whether the current batch is appropriate for the transaction or not.
    $(document.body).on('click', '.select-transaction', function () {
        var $this = $(this);
        var id = $this.attr('name');
        var zone = $this.parent().parent().children('input.zone').val().trim();
        var validZone = ($('#modal_zones').html().trim().split(' ').indexOf(zone) >= 0);
        var sn = $this.parent().parent().find('input.serial-number').val().trim();
        var loc = $this.parent().parent().find('input.location').val().trim();
        if (validZone || loc == '') {
            showHPA({ otid: id, item: '' });
            if (loc == '') {
                $('#Warehouse,#FindLocation,#AssignLocation,#FullShelf').removeAttr('disabled');
                $('#Warehouse').siblings('.modal-launch-style').removeClass('disabled');
            } else {
                $('#Warehouse,#FindLocation,#AssignLocation,#FullShelf').attr('disabled', 'disabled');
                $('#Warehouse').siblings('.modal-launch-style').addClass('disabled');
            };
        } else {
            zoneBatch(zone, id);
        };
    });

    // create a new put away
    $('#trans_create').click(function () {
        if ($('#SubCategory').val().toLowerCase() == 'reel tracking') {
            $('#trans_modal').one('hidden.bs.modal', function () {
                var d = new Date();
                var now = String(d.getFullYear()) + String(d.getMonth() + 1) + String(d.getDate()) + String(d.getHours()) + String(d.getMinutes()) + String(d.getSeconds()) + '-IM';
                openReels({ order: now, uf1: '', uf2: '', lot: 0, warehouse: '', expdate: '', notes: '' },
                    { number: $('#ItemNumber').val(), numReels: 1, totalParts: 0, description: $('#Description').val(), whseRequired: $('#WhseSens').val() });
            });
            $('#trans_modal').modal('hide');
        } else {
            showHPA({ otid: 0, item: $('#ItemNumber').val() });
        };
    });

    // choose a different transaction
    $('#trans_different').click(function () {
        showTrans();
    });

    //When done processing a transaction, attempt to focus input value
    $('#trans_modal').on('hidden.bs.modal', function () {
        $('#Item').val("").focus();

        if ($('#TotesTable tbody tr.active').length <= 0) {
            MessageModal('All Totes Filled', 'All Totes have been filled within this batch. Press OK to trigger a batch complete', undefined, undefined, function () { $('#CompleteBatch').click(); });
        };
    });
});

// checks if the current batch is eligible to use the current location with the current item and zone.  if it is not then it will prompt the user to switch to an eligible batch or create a new one or change location
function zoneBatch(zone, otid) {
    batchHub.server.getBatchByZone(zone).done(function (batch) {
        if (batch.trim() != '') {
            //if (confirm('The currently selected batch does not contain zone: ' + (zone) + '.  Click OK to switch to batch: ' + (batch) + ' which does contain the zone.' +
            //            '  Click Cancel to choose a different location or transaction.')) {
            //    $('#BatchID').val(batch).trigger('input');
            //    toteDT.draw().one('draw.dt', function () {
            //        var tote = $('#AssignToTote');
            //        var pos = $('#AssignToPosition');
            //        var selected = toteDT.row('.active').data()[1];
            //        tote.html(null);
            //        pos.html(null);
            //        for (var x = 0; x < toteDT.rows().data().length; x++) {
            //            var r = toteDT.row(x).data();
            //            tote.append('<option name="' + (r[2] - r[3]) + '" value=' + r[1] + '>' + r[1] + '</option>');
            //            pos.append('<option name="' + (r[2] - r[3]) + '" value=' + r[1] + '>' + r[0] + '</option>');
            //        };
            //        tote.val(selected);
            //        pos.val(selected);
            //        if (tote.find('option:selected').attr("name") <= 0) {
            //            alert("The tote you've selected is already marked as full. Putting the item in this tote will go over the defined cells.")
            //        }
            //        $('#OpenCells').val(pos.children(':selected').attr('name'));
            //        $('#modal_zones').html(toteDT.row(0).data()[4].replace('Zones:', '').trim());
            //        $('#modal_batch').html($('#BatchID').val());
            //        $('.select-transaction[name="' + (otid) + '"]').click();
            //    });
            //} else {
            //    var whse = $('#Warehouse').val().trim().toLowerCase();
            //    if ($('#FindLocation').attr('disabled') == 'disabled') {
            //        $('#trans_different').click();
            //    } else if (!$('#batch_location_modal').hasClass('in')) {
            //        if ($('#WhseSens').is(':checked') && (whse == 'none' || whse == '')) {
            //            $('#Warehouse').click();
            //        } else if ($('#FIFO').is(':checked') && $('#ExpDate').val().trim() == '') {
            //            $('#ExpDate').focus();
            //        } else {
            //            $('#AssignLocation').click();
            //        };
            //    };
            //};

            $('#BatchID').val(batch).trigger('input').typeahead('close');
            toteDT.draw().one('draw.dt', function () {
                var tote = $('#AssignToTote');
                var pos = $('#AssignToPosition');
                var selected = toteDT.row('.active').data()[1];
                tote.html(null);
                pos.html(null);
                for (var x = 0; x < toteDT.rows().data().length; x++) {
                    var r = toteDT.row(x).data();
                    tote.append('<option name="' + (r[2] - r[3]) + '" value="' + r[1] + '">' + r[1] + '</option>');
                    pos.append('<option name="' + (r[2] - r[3]) + '" value="' + r[1] + '">' + r[0] + '</option>');
                };
                tote.val(selected);
                pos.val(selected);
                if (tote.find('option:selected').attr("name") <= 0) {
                    alert("The tote you've selected is already marked as full. Putting the item in this tote will go over the defined cells.")
                }
                $('#OpenCells').val(pos.children(':selected').attr('name'));
                $('#modal_zones').html(toteDT.row(0).data()[4].replace('Zones:', '').trim());
                $('#modal_batch').html($('#BatchID').val());
                $('.select-transaction[name="' + (otid) + '"]').click();
            });
        } else {
            if (confirm('There are no batches with this zone (' + zone + ') assigned.  Click OK to start a new batch or cancel to choose a different location/transaction.')) {
                $('.modal.in').modal('hide');
                $('a[href="#SetupTab"]').click();
            };
        };
    });
};

// deals with the new reels
function openReels(hv, item) {
    $('#reel_container').html('');
    $('#HReelOrderNumber,#ReelOrder').val(hv.order);
    $('#HLotNumber,#ReelLot').val(hv.lot);
    $('#HUF1,#ReelUF1').val(hv.uf1);
    $('#HUF2,#ReelUF2').val(hv.uf2);
    $('#HWarehouse,#ReelWarehouse').val(hv.warehouse).attr('name', (item.whseRequired ? 'required' : ''));
    if (item.whseRequired) {
        $('#ReelWarehouse').removeAttr('disabled');
    } else {
        $('#ReelWarehouse').val("").attr('disabled', 'disabled');
    };
    $('#HExpDate,#ReelExpDate').val(hv.expdate);
    $('#HNotes,#ReelNotes').val(hv.notes);
    $('#TotalParts').val(item.totalParts);
    $('#NumReels').val(item.numReels);
    $('#ReelItem').val(item.number);
    $('#ReelDescription').val(item.description);
    $('#reeloverview_modal').modal('show').one('shown.bs.modal', function () {
        $('#reeldetail_modal').modal('show').one('shown.bs.modal', function () {
            if (item.whseRequired) {
                $('#ReelWarehouse').click();
            };
            $('#ReelQty').val('').focus();
        });
    });
};

function PopCellVelDrops() {
   
    
    //var 

   
};

// show the hot put away area and get the details of the transaction
function showHPA(trans) {
    var CellStr = "";
    var VelStr = "";

    $('#trans_different').show();
    $('#OTID').val(trans.otid);

    $('#CCell, #BCell, #FCell, #FVel, #BVel, #CVel').empty();
    $('#CCell, #BCell, #FCell, #FVel, #BVel, #CVel').append('<option value=""></option>');

    

    globalHubConn.server.getVelocityCodes().done(function (result) {
        for (var x = 0; x < result.length; x++) {
            VelStr+='<option value="' + result[x] + '">' + result[x] + '</option>';
        };
        $('#FVel, #BVel, #CVel').append(VelStr);

        globalHubConn.server.getCellSizes().done(function (cellObj) {
            for (var x = 0; x < cellObj.cellsize.length; x++) {
                CellStr+='<option value="' + cellObj.cellsize[x] + '">' + cellObj.cellsize[x] + '</option>';
            };
            $('#CCell, #BCell, #FCell').append(CellStr);

            batchHub.server.getItemDetails(trans.otid, trans.item).done(function (details) {
                if (details.Succ == false) {
                    MessageModal('Error', 'There was an error getting the info for the scanned value. Please scan it again', function () { $('#trans_modal').modal('hide');  $('#Item').val("").focus(); });
                } else {
                    $.each([$('#OrderNumber'), $('#Category'), $('#SubCategory'), $('#ItemNumber'), $('#SupplierItemID'), $('#Description'), $('#UF1'),
                    $('#UF2'), $('#WhseSens'), $('#DateSens'), $('#FIFO'), $('#UoM'), $('#LotNum'), $('#ExpDate'), $('#SerialNumber'),
                    $('#CCell'), $('#BCell'), $('#FCell'), $('#TransQty,#NumberToAssign'), $('#Warehouse'), $('#CVel'), $('#BVel'), $('#FVel'), $('#Zone'),
                    $('#Carousel'), $('#Row'), $('#Shelf'), $('#Bin'), $('#CellSize'), $('#Velocity'), $('#Qty'), $('#MaxQty'), $('#QtyAlloc'),
                    $('#FIFODate'), $('#HNotes'), $('#InvMapID'), $('#IsDedicated'), $('#PrimZone'), $('#SecZone')], function (index, elem) {
                        if (elem.is(':checkbox')) {
                            elem.prop('checked', details.Detail[index].toLowerCase() == 'true');
                        } else {
                            elem.val(details.Detail[index]);
                        };
                    });
                    var numToAssign = $('#NumberToAssign');
                    if (numToAssign.val().trim() == '' || parseInt(numToAssign.val()) <= 0) {
                        numToAssign.val(numToAssign.data('default'));
                    };

                    //COmmented out container logic
                    //if (details.Detail[1].toLowerCase() == 'reel tracking') {
                    //    $('#ContainerLocations').show();
                    //} else {
                    //    $('#ContainerLocations').hide();
                    //};
                    $('#ContainerLocations').hide();

                    // if the transaction already exists
                    if (trans.otid > 0) {
                        $('#OrderNumber').attr('disabled', 'disabled');
                        // if location is not null (inv map id)
                        if (parseInt(details.Detail[details.length - 1]) > 0) {
                            $('#Warehouse,#FindLocation,#AssignLocation,#FullShelf').attr('disabled', 'disabled');
                            $('#Warehouse').siblings('.modal-launch-style').addClass('disabled');
                        } else {
                            $('#Warehouse,#FindLocation,#AssignLocation,#FullShelf').removeAttr('disabled');
                            $('#Warehouse').siblings('.modal-launch-style').removeClass('disabled');
                        };
                    } else {
                        $('#OrderNumber').removeAttr('disabled');
                        // item, new hpa
                        $('#Warehouse,#FindLocation,#AssignLocation,#FullShelf').removeAttr('disabled');
                        $('#Warehouse').siblings('.modal-launch-style').removeClass('disabled');
                    };
                    if ($('#WhseSens').prop('checked')) {
                        $('#Warehouse').removeAttr('disabled');
                        $('#Warehouse').siblings('.modal-launch-style').removeClass('disabled');
                    } else {
                        $('#Warehouse').val('').attr('disabled', 'disabled');
                        $('#Warehouse').siblings('.modal-launch-style').addClass('disabled');
                    };
                    if ($('#Zone').val().trim() == '') {
                        $('#FindLocation').click();
                    };

                    //only show on success
                    initTransModal();
                    $('#SelectTransaction').hide();
                    $('#PutAwayDetail').show();
                };
            });
        });
    });
};

// shows the transactions area
function showTrans() {
    $('#OTID').val('');
    initTransModal();
    $('#SelectTransaction').show();
    $('#PutAwayDetail').hide();
    $('#trans_different').hide();
};

// initializes the pager plugin and shows the modal if it isn't visible
function initTransModal() {
    var m = $('#trans_modal');
    
    if ($('#TransQty').val() != "") {
        $('#NumberToAssign').val(parseInt($('#TransQty').val()))
    };

    if (!m.hasClass('in')) {
        pager.populate(0);
        m.modal('show');
    };
};