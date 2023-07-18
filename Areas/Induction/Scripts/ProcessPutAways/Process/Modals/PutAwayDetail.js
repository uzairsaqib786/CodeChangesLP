// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    addResize(function () { $('#trans_container').css({ 'max-height': $(window).height() * 0.65 }); });
    
    //On modal close clear zone, inv mpa id, item number, and order number to prevent old data from causing issues
    $('#trans_modal').on('hidden.bs.modal', function () {
        $('#Zone').val('');
        $('#InvMapID').val('');
        $('#ItemNumber').val('');
        $('#OTID').val('');
    });

    $('#trans_dismiss').click(function () {
        //Call reserve with a negative inv map id in order to only clear
        batchHub.server.reserveLocation(-1, '', false);
    });

    // complete the transaction
    $('#trans_set').click(function () {
        var invmapid = $('#InvMapID').val();
        var warehouse = $('#Warehouse').val();
        if (warehouse.trim().toLowerCase() == 'none') { warehouse = ''; };
        var aqty = $('#NumberToAssign').val().trim();
        var tqty = $('#TransQty').val().trim();
        if (tqty == '') { tqty = aqty };
        var validZone = ($('#modal_zones').html().trim().toLowerCase().split(' ').indexOf($('#Zone').val().toLowerCase()) >= 0);
        if (invmapid.trim() == '' || parseInt(invmapid) <= 0) {
            MessageModal('Error', 'You must select a location for this transaction before it can be processed.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else if (aqty == '' || ($.isNumeric(aqty) && parseInt(aqty) <= 0)) {
            MessageModal('Error', 'Batch Put Away Quantity must be greater than 0.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else if (warehouse.trim() == '' && $('#WhseSens').is(':checked')) {
            MessageModal('Error', 'This item is warehouse sensitive.  You must provide a warehouse.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else if ($('#DateSens').is(':checked') && $('#ExpDate').val().trim() == '') {
            MessageModal('Error', 'This item is date sensitive.  You must provide an expiration date.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else if ($('#BadContainer').val() == 'true') {
            MessageModal('Error', 'The selected location needs to be corrected by a supervisor.  You may choose a different location to continue.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else if (!validZone) {
            zoneBatch($('#Zone').val(), $('#OTID').val());
        } else if ($('#AssignToPosition option:selected').text() == null || !$.isNumeric($('#AssignToPosition option:selected').text())) {
            MessageModal('Error', 'You must select a tote to put this transaction in.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else {
            var item = $('#ItemNumber').val();
            var description = $('#Description').val();

            //Commenting out container logic
            //if ($('#SubCategory').val().trim().toLowerCase() == 'reel tracking' && $('#ReelLogic').val().toLowerCase() != 'dynamic') {
            //    batchHub.server.isDedicated(item, invmapid).done(function (dedicated) {
            //        if (!dedicated || $('#IsDedicated').val() != 'true') {
            //            MessageModal('Error', 'This item number must have its location dedicated due to reel tracking before proceeding.', function () {
            //                launchDedicatedModal();
            //            }, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
            //        } else {
            //            taskComplete(true, item, description, aqty, tqty, warehouse, invmapid);
            //        };
            //    });
            //} else {
            //    taskComplete(false, item, description, aqty, tqty, warehouse, invmapid);
            //};

            taskComplete(false, item, description, aqty, tqty, warehouse, invmapid);
        };
    });

    // continue completion process of transaction after validation
    function taskComplete(requireDedicated, item, description, aqty, tqty, whse, invmapid) {
        if (item == "" || item === undefined) {
            MessageModal('Blank Item Number', 'The item number field is blank. Please rescan it.',
                         function () { $('#trans_modal').modal('hide'); $('#Item').val("").focus(); },
                         function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else {
            // check if there is a chance for cross docking
            batchHub.server.getCrossDock(1, 5, [item, whse, '1=1']).done(function (crossDock) {
                if (crossDock.pages.length > 0 && confirm('Cross Dock opportunity!  Click OK to view backorder transactions for the item you are putting away.')) {
                    setupCrossDockModal(crossDock.pages, crossDock.extraData.allocated, crossDock.extraData.backorder, item, description, tqty);
                    cd_pager.currentPage = 0;
                    cd_pager.populate();
                    $('#crossdock_modal').modal('show');
                } else if (confirm('Click OK to complete this transaction and assign it to the selected batch and tote.')) {
                    var otid = $('#OTID').val();
                    var splitQty = 0;
                    $('#trans_set').attr('disabled', 'disabled');
                    if ($('#SplitShortPut').val() == 'true' && parseInt(aqty) < parseInt(tqty) && $.isNumeric(aqty) && $.isNumeric(tqty) && otid.trim() != '') {
                        if (confirm('This transaction quantity is greater than the assigned quantity.  Click OK if you will receive more of this order/item.  Click Cancel to mark this transaction as received short.')) {
                            splitQty = parseInt(tqty) - parseInt(aqty);
                        };
                    };
                    var sn = $('#SerialNumber').val().trim();
                    if (sn == '') { sn = '0'; };
                    var ln = $('#LotNum').val().trim();
                    if (ln == '') { ln = '0'; };
                    var edate = $('#ExpDate').val().trim();
                    var batch = $('#BatchID').val();
                    var cell = parseInt(toteDT.row('.active').data()[3]);
                    console.log(cell)
                    batchHub.server.taskComplete(otid, splitQty, aqty, $('#AssignToTote').val(), batch, item, $('#UF1').val(), $('#UF2').val(), ln, sn, $('#AssignToPosition').children(':selected').html(),
                        cell, whse, edate, ($('#RTS').is(':checked') ? 'Return' : ''), $('#Zone').val(), $('#Carousel').val(), $('#Row').val(), $('#Shelf').val(), $('#Bin').val(), invmapid, $('#MaxQty').val(),
                        //($('#SubCategory').val() == 'reel tracking'), commenting out reel tracking
                        false,
                        requireDedicated, $('#OrderNumber').val()).done(function (newOTID) {
                            toteDT.draw();
                            if (newOTID <= 0 && newOTID != -2 ) {
                                $('#trans_set').removeAttr('disabled');
                                MessageModal('Error', 'There was an error while trying to save the current transaction to batch (' + batch + ').  Check the error log for details.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });

                            } else if (newOTID == -2) {
                                $('#trans_set').removeAttr('disabled');
                                MessageModal('Invalid Location', 'The selected location is no longer available. A new location will be found', function () { $('#FindLocation').click(); }, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
                            } else {
                                $('#OTID').val(newOTID);
                                var ID = $('#OTID').val();
                                if ($('#AutoPrintToteLabels').val() == 'true') {
                                    autoPrintToteLabels(ID);
                                } else {
                                    $('#trans_set').removeAttr('disabled');
                                    $('.modal.in').modal('hide');
                                    $('#Item').focus();
                                    //console.log($('#TotesTable tbody tr.active').length)
                                    //if ($('#TotesTable tbody tr.active').length <= 0 && confirm('All totes are full.  Click OK to complete this batch or Cancel to edit this batch.')) {
                                    //    $('#CompleteBatch').click();
                                    //};
                                };
                            };
                        });
                };
            });
        };
    };

    function autoPrintToteLabels(ID) {
        var pd = ($('#PrintDirect').val().toLowerCase() == 'true');
        var numLabels = 1;
        if ($('#ReqNumPutLabels').val() == 'true' && pd) {
            var isSet = false;
            var currval = -1;
            while (!isSet) {
                currval = prompt('Enter the number of labels to print or click cancel to skip printing labels.');
                if (currval == null) {
                    isSet = true;
                    numLabels = 0;
                } else if ($.isNumeric(currval)) {
                    isSet = true;
                    numLabels = currval;
                };
            };
        };
        if (numLabels > 0) {
            if (!pd) {
                getLLPreviewOrPrint('/IM/ProcessPutAways/printPutAwayItemLabels/', {
                    OTID: ID,
                    printDirect: pd
                }, pd, 'label', 'Put Away Label Preview', function () {
                    $('#trans_set').removeAttr('disabled');
                    $('.modal.in').modal('hide');
                    $('#Item').focus();
                    toteDT.draw();
                    //if ($('#TotesTable tbody tr.active').length <= 0 && confirm('All totes are full.  Click OK to complete this batch or Cancel to edit this batch.')) {
                    //    $('#CompleteBatch').click();
                    //};
                });
            } else {
                for (var i = 0; i < numLabels; i++) {
                    getLLPreviewOrPrint('/IM/ProcessPutAways/printPutAwayItemLabels/', {
                        OTID: ID,
                        printDirect: pd
                    }, pd, 'label', 'Put Away Label Preview');
                };
                $('#trans_set').removeAttr('disabled');
                $('.modal.in').modal('hide');
                $('#Item').focus();
                //if ($('#TotesTable tbody tr.active').length <= 0 && confirm('All totes are full.  Click OK to complete this batch or Cancel to edit this batch.')) {
                //    $('#CompleteBatch').click();
                //};
            };
        } else {
            $('#trans_set').removeAttr('disabled');
            $('.modal.in').modal('hide');
            $('#Item').focus();
            //if ($('#TotesTable tbody tr.active').length <= 0 && confirm('All totes are full.  Click OK to complete this batch or Cancel to edit this batch.')) {
            //    $('#CompleteBatch').click();
            //};
        };
    };

    $('#ClearTrans').click(function () {
        var uf1 = $('#UF1').siblings('label').html();
        var uf2 = $('#UF2').siblings('label').html();
        if (confirm('Click OK to clear serial number, lot number, expiration date, warehouse, ' + (uf1) + ', and ' + (uf2))) {
            $('#UF1,#UF2,#ExpDate,#LotNum,#SerialNumber').val('');
            $('#Warehouse').val('None');
        };
    });

    $('#UpdateInvMaster').click(function () {
        if (confirm('Click OK to save current cell sizes and velocity codes for this item to the inventory master.')) {
            batchHub.server.updateInventoryMaster($('#ItemNumber').val(), $('#CCell').val(), $('#BCell').val(), $('#FCell').val(), $('#CVel').val(), $('#BVel').val(), $('#FVel').val(),$('#PrimZone').val(), $('#SecZone').val()).done(function (updated) {
                if (!updated) {
                    MessageModal('Error', 'An error occurred during the save process.  See the error log for details.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
                };
            });
        };
    });

    // updates the shelf (where row and zone match) as being full
    $('#FullShelf').click(function () {
        var z = $('#Zone').val(), r = $('#Row').val();
        if (z.trim() != '' && r.trim() != '') {
            if (confirm('Click OK to update this shelf as being full.')) {
                batchHub.server.updateShelfFull(z, r).done(function (updated) {
                    if (!updated) {
                        MessageModal('Error', 'An error occurred while updating the shelf.  Check the error log for details.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
                    };
                });
            };
        } else {
            MessageModal('Error', 'There is no location selected to mark the shelf as full.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        };
    });

    // opens the inventory master in a popup and captures the close event.  On close it updates the item details of the item we're dealing with in case the user made changes to the inventory master.
    $('#ViewInvMaster').click(function () {
        var item = $('#ItemNumber').val();
        var $handle = $(window.open('/InventoryMaster?App=IM&popup=True&ItemNumber=' + item, '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'));

        try {
            $handle[0].focus();
            $handle.one('load', function () {
                $handle.one('unload', function () {
                    var CellStr = "";
                    var VelStr = "";
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

                            batchHub.server.getItemDetails($('#OTID').val(), item).done(function (details) {
                                if (details.Succ == false) {
                                    MessageModal('Error', 'There was an error getting the info for this item. Please scan it again', function () { $('#trans_modal').modal('hide'); $('#Item').val("").focus(); }, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
                                } else {
                                    $('#SubCategory').val(details.Detail[2]);
                                    $('#SupplierItemID').val(details.Detail[4]);
                                    $('#Description').val(details.Detail[5]);
                                    if (details.Detail[8].toLowerCase() == 'true') {
                                        $('#WhseSens').prop('checked', 'checked');
                                        $('#Warehouse').val('').attr('disabled', 'disabled');
                                    } else {
                                        $('#WhseSens').removeProp('checked');
                                        $('#Warehouse').removeAttr('disabled');
                                    };
                                    if (details.Detail[9].toLowerCase() == 'true') {
                                        $('#DateSens').prop('checked', 'checked');
                                    } else {
                                        $('#DateSens').removeProp('checked');
                                    };
                                    if (details.Detail[10].toLowerCase() == 'true') {
                                        $('#FIFO').prop('checked', 'checked');
                                    } else {
                                        $('#FIFO').removeProp('checked');
                                    };
                                    $('#UoM').val(details.Detail[11]);
                                    $('#CCell').val(details.Detail[15]);
                                    $('#BCell').val(details.Detail[16]);
                                    $('#FCell').val(details.Detail[17]);
                                    $('#CVel').val(details.Detail[20]);
                                    $('#BVel').val(details.Detail[21]);
                                    $('#FVel').val(details.Detail[22]);
                                    $('#PrimZone').val(details.Detail[37]);
                                    $('#SecZone').val(details.Detail[38]);
                                };
                            });
                        });
                    });
                });
            });
        } catch (e) {
            console.log('Exception: ', e)
            MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        };
    });

    $('#NumberToAssign').on('input', function () {
        setNumericInRange($(this), 1, null);
    });

    //$('.cell-size').click(function () {
    //    var $this = $(this);
    //    if ($this.hasClass('glyphicon')) {
    //        $this = $this.siblings('input');
    //    };
    //    trigger_cell_modal($this);
    //});

    $('#Warehouse, .warehouse').click(function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        if ($this.attr('disabled') == 'disabled') {
            return;
        };
        trigger_warehouse_modal($this);
    });

    $(document.body).on('click', '.set-warehouse', function () {
        if (!$('#reeldetail_modal').hasClass('in') && locationNoWarehouse == false) {
            $('#warehouse_modal').one('hidden.bs.modal', function () {
                $('#FindLocation').click();
            });
        };
        locationNoWarehouse = false;
    });

    $('#AssignToTote, #AssignToPosition').change(function () {
        var $this = $(this);
        $('#AssignToTote, #AssignToPosition').val($this.val());
        if ( $('#AssignToTote').find('option:selected').attr("name") <= 0) {
            alert("The tote you've selected is already marked as full. Putting the item in this tote will go over the defined cells.")
        }
        $('#OpenCells').val($this.children(':selected').attr('name'));
    });

    /*
        Clear any reservations that were not used in the unload event (no task complete)
    */
    $(window).on('beforeunload', function (e) {
        batchHub.server.clearLocationReservations();
        // returning something will cause the browser to prompt the user to leave the page in case this is at some point a desirable outcome.
    });

    // use LocAssPutAway to assign a location to this transaction
    $('#FindLocation').click(function () {
        var whse = $('#Warehouse').val().trim().toLowerCase();
        var aqty = $('#NumberToAssign').val().trim();

        if (aqty==0){
            aqty = aqty + 1;
        }

        if ($('#WhseSens').is(':checked') && (whse == 'none' || whse == '')) {
            MessageModal('Error', 'This item is warehouse sensitive and must be assigned a warehouse before the process can continue.', function () {
                $('#Warehouse').click();
            }, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else if ($('#FIFO').is(':checked') && $('#FIFODate').val().trim().toLowerCase() == 'expiration date' && $('#ExpDate').val().trim() == '') {
            MessageModal('Error', 'This item is marked as FIFO with Expiration Date as its FIFO Date.  You must provide an Expiration Date.', function () {
                $('#ExpDate').focus();
            }, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else if (aqty == '' || parseInt(aqty) <= 0) {
            MessageModal('Error', 'This put away transaction must have a quantity greater than 0.', function () {
                $('#NumberToAssign').focus();
            }, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else {
            var numberToAssign = $('#NumberToAssign');
            if (parseInt(numberToAssign.val()) <= 0) {
                numberToAssign.val(numberToAssign.data('default'));
            };
            $('#FindLocation,#AssignLocation,#FullShelf,#CCell,#CVel,#BCell,#BVel,#FCell,#FVel,#Warehouse,#trans_different,#trans_create,#trans_set').attr('disabled', 'disabled');
            $('#CVel,#BCell,#BVel,#FCell,#FVel,#Warehouse').siblings('.glyphicon').addClass('disabled');
            //MessageModal('Alert', 'Attempting to find a valid location for the selected transaction.', undefined, function () {
            //    document.getElementById("Message_Modal").style.zIndex = "1061";
                // check whether we should try to place the item in a forward location or go immediately to bulk
                batchHub.server.checkForwardLocations($('#ItemNumber').val()).done(function (fwdQty) {
                    var replenfwd = ($('#ReplenishForward').val() == 'true');

                    if ((replenfwd && parseInt(fwdQty) > 0)) {
                        replenfwd = false;
                        MessageModal("REPLENISHMENT OPPORTUNITY!",
                            'There is a need for ' + fwdQty.toString() + ' of item: ' + $('#ItemNumber').val() + '. Press OK to find a location needing replenishment. Otherwise press CANCEL to do a normal location search',
                            function () { GetLocation(replenfwd, fwdQty) }, function () { document.getElementById("Message_Modal").style.zIndex = "1062"; }, function () { replenfwd = true });
                    } else {
                        GetLocation(false, 0);
                    };
                });
            //});
        }

    });

    //var CellStr = "";
    //var VelStr = "";
    //globalHubConn.server.getVelocityCodes().done(function (result) {
    //    for (var x = 0; x < result.length; x++) {
    //        VelStr+='<option value="' + result[x] + '">' + result[x] + '</option>';
    //    };
    //    $('#FVel, #BVel, #CVel').append(VelStr);

    //    globalHubConn.server.getCellSizes().done(function (cellObj) {
    //        for (var x = 0; x < cellObj.cellsize.length; x++) {
    //            CellStr+='<option value="' + cellObj.cellsize[x] + '">' + cellObj.cellsize[x] + '</option>';
    //        };
    //        $('#CCell, #BCell, #FCell').append(CellStr);

    var ModalOpen = false;

    $(document.body).on('click', '.cell-modal, .velocity-modal', function () {
        ModalOpen = true;
    });

    $('#CCell').change(function () {
        var BCell = $('#BCell').val();
        var FCell = $('#FCell').val();

        if (ModalOpen) {
            ModalOpen = false;
            var CellStr = "";
            $('#BCell, #FCell').empty();
            $('#BCell, #FCell').append('<option value=""></option>');
            globalHubConn.server.getCellSizes().done(function (cellObj) {
                for (var x = 0; x < cellObj.cellsize.length; x++) {
                    CellStr += '<option value="' + cellObj.cellsize[x] + '">' + cellObj.cellsize[x] + '</option>';
                };
                $('#BCell, #FCell').append(CellStr);
                $('#BCell').val(BCell);
                $('#FCell').val(FCell);
            });
        };

    });

    $('#BCell').change(function () {
        var CCell = $('#CCell').val();
        var FCell = $('#FCell').val();

        if (ModalOpen) {
            ModalOpen = false;
            var CellStr = "";
            $('#CCell, #FCell').empty();
            $('#CCell, #FCell').append('<option value=""></option>');
            globalHubConn.server.getCellSizes().done(function (cellObj) {
                for (var x = 0; x < cellObj.cellsize.length; x++) {
                    CellStr += '<option value="' + cellObj.cellsize[x] + '">' + cellObj.cellsize[x] + '</option>';
                };
                $('#CCell, #FCell').append(CellStr);
                $('#CCell').val(CCell);
                $('#FCell').val(FCell);
            });
        };
    });

    $('#FCell').change(function () {
        var CCell = $('#CCell').val();
        var BCell = $('#BCell').val();

        if (ModalOpen) {
            ModalOpen = false;
            var CellStr = "";
            $('#CCell, #BCell').empty();
            $('#CCell, #BCell').append('<option value=""></option>');
            globalHubConn.server.getCellSizes().done(function (cellObj) {
                for (var x = 0; x < cellObj.cellsize.length; x++) {
                    CellStr += '<option value="' + cellObj.cellsize[x] + '">' + cellObj.cellsize[x] + '</option>';
                };
                $('#CCell, #BCell').append(CellStr);
                $('#CCell').val(CCell);
                $('#BCell').val(BCell);
            });
        };
    });

    $('#FVel').change(function () {
        var BVel = $('#BVel').val();
        var CVel = $('#CVel').val();

        if (ModalOpen) {
            ModalOpen = false;
            var VelStr = "";
            $('#BVel, #CVel').empty();
            $('#BVel, #CVel').append('<option value=""></option>');
            globalHubConn.server.getVelocityCodes().done(function (result) {
                for (var x = 0; x < result.length; x++) {
                    VelStr += '<option value="' + result[x] + '">' + result[x] + '</option>';
                };
                $('#BVel, #CVel').append(VelStr);
                $('#CVel').val(CVel);
                $('#BVel').val(BVel);
            });
        };
    });

    $('#BVel').change(function () {
        var FVel = $('#FVel').val();
        var CVel = $('#CVel').val();

        if (ModalOpen) {
            ModalOpen = false;
            var VelStr = "";
            $('#FVel, #CVel').empty();
            $('#FVel, #CVel').append('<option value=""></option>');
            globalHubConn.server.getVelocityCodes().done(function (result) {
                for (var x = 0; x < result.length; x++) {
                    VelStr += '<option value="' + result[x] + '">' + result[x] + '</option>';
                };
                $('#FVel, #CVel').append(VelStr);
                $('#CVel').val(CVel);
                $('#FVel').val(FVel);
            });
        };
    });

    $('#CVel').change(function () {
        var FVel = $('#FVel').val();
        var BVel = $('#BVel').val();

        if (ModalOpen) {
            ModalOpen = false;
            var VelStr = "";
            $('#FVel, #BVel').empty();
            $('#FVel, #BVel').append('<option value=""></option>');
            globalHubConn.server.getVelocityCodes().done(function (result) {
                for (var x = 0; x < result.length; x++) {
                    VelStr += '<option value="' + result[x] + '">' + result[x] + '</option>';
                };
                $('#FVel, #BVel').append(VelStr);
                $('#BVel').val(BVel);
                $('#FVel').val(FVel);
            });
        };
    });

});

function GetLocation(replenfwd, fwdQty) {

    if (replenfwd) {
        $('#ReplenCol').show();
        $('#ReplenNeeded').val(fwdQty);

        if (parseInt(fwdQty) < parseInt($('#NumberToAssign').val())) {
            $('#NumberToAssign').val(fwdQty);
        };

    } else {
        $('#ReplenCol').hide();
    };

    // identify the best location
    batchHub.server.findLocation($('#NumberToAssign').val(), $('#ItemNumber').val(), $('#CCell').val(), $('#CVel').val(), $('#BCell').val(), $('#BVel').val(), $('#FCell').val(), $('#FVel').val(),
        $('#Warehouse').val(), $('#DateSens').is(':checked'), $('#FIFO').is(':checked'),
        //($('#SubCategory').val().toLowerCase() == 'reel tracking'), Commented out reel tracking
        false,
        $('#LotNum').val(), $('#SerialNumber').val(),
        replenfwd, $('#Zone').val(), ($('#IsDedicated').val() == 'true'), $('#RTS').is(':checked'), $("#ExpDate").val(), $('#PrimZone').val(), $('#SecZone').val()).done(function (obj) {
            $('#FindLocation,#AssignLocation,#FullShelf,#CCell,#CVel,#BCell,#BVel,#FCell,#FVel,#Warehouse,#trans_different,#trans_create,#trans_set').removeAttr('disabled', 'disabled');
            $('#CVel,#BCell,#BVel,#FCell,#FVel,#Warehouse').siblings('.glyphicon').removeClass('disabled');
            if ($('#WhseSens').prop('checked')) {
                $('#Warehouse').removeAttr('disabled');
            } else {
                $('#Warehouse').attr('disabled', 'disabled');
            };
            if (obj.Success) {
                $('#IsDedicated').val('false');
                $('#BadContainer').val('false');
                $('#InvMapID').val(obj.InvMapID);
                $('#Bin').val(obj.Bin);
                $('#Carousel').val(obj.Carousel);
                $('#CellSize').val(obj.CellSz);
                $('#MaxQty').val(obj.LocMaxQty);
                $('#Qty').val(obj.LocQty);
                $('#QtyAlloc').val(obj.QtyAlloc);
                $('#Row').val(obj.Row);
                $('#Shelf').val(obj.Shelf);
                $('#Velocity').val(obj.VelCode);
                // if it's empty we're going to update it if the item is warehouse sensitive.  If it's not empty then we can set it.
                if (obj.Whse != '') {
                    $('#Warehouse').val(obj.Whse);
                };
                $('#Zone').val(obj.Zone);
                $('#Message_Modal').modal('hide').one('hidden.bs.modal', function () {
                    var validZone = ($('#modal_zones').html().trim().toLowerCase().split(' ').indexOf(obj.Zone.toLowerCase()) >= 0);
                    if (!validZone) {
                        zoneBatch(obj.Zone, $('#OTID').val());
                    };

                    //Commented out container logic
                    //else if (obj.RequireDedicate || $('#SubCategory').val().toLowerCase() == 'reel tracking') {
                    //    launchDedicatedModal();
                    //};
                });
                //$('#NumberToAssign').focus();
                $('#numpadQtyAssign').click();
            } else if (obj.Message == "No available locations found.") {
                $('#Message_label').text('Error');
                $('#Display_Message').text('No available locations were found for this item.');
            } else {
                $('#Message_label').text('Error');
                $('#Display_Message').text('An unknown error occurred during location assignment.  Check the error log for details.');
            };
        });
};