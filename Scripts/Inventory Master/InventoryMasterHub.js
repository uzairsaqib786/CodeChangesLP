// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var invMasterHub = $.connection.inventoryMasterHub;

// associative array for updating scan codes against the db
var scanCodeData = { 'Scan Codes': new Array(), 'Scan Types': new Array(), 'Start Position': new Array(), 'Code Length': new Array(), 'Scan Range': new Array() };

// stores data on load to compare against the server during a save.
var currentData = Array();

var stockCode;

var postQuarantineAlert = function () {
    createAlert('quar_alert', '<div style="color:#fff;" class="text-center">Item is Quarantined!</div>', 'quarantine alert-dismissable alert-custom', false, '', '#quarAlert');
};

$(document).ready(function () {
    function getBackOverride() {
        return '&override_back=' + encodeURIComponent("location.href='/InventoryMaster?ItemNumber=" + $('#UpdateItemNum').val() + "&App=" + $('#AppName').val() + "'");
    };
    $('#goToInvMap').click(function () {
        window.location.href = "/InventoryMap?ItemNumber=" + $('#UpdateItemNum').val() + "&App=" + $('#AppName').val() + getBackOverride();
    });

    // Reel Tracking partial
    $('#minReelQty').on('focusout', function () {
        if (this.value == null || this.value == '') { this.value = 0; };
    });
    $('#minReelQty').on('input', function () {
        setNumericInRange($(this), 0, null);
    });

    //filter event handler if needed
    $('#stockcodeCopy').on('filterChange', function () {
        //adjust count info here
        invMasterHub.server.getItemNumCount($('#UpdateItemNum').val(),
                                           (InvMasterFilter == "" ? "1=1" : InvMasterFilter.getFilterString())).done(function (data) {
                                               if (data.ItemNumber != '') {
                                                   var e = jQuery.Event('keyup');
                                                   e.which = 13;
                                                   $('#stockcodeCopy').val(data.ItemNumber);
                                                   $('#stockCode').typeahead('val', data.ItemNumber).trigger(e);
                                               } else if (data.Pos == 0) {
                                                   alert("No records exist for this filter");
                                                   InvMasterFilter = "";
                                                   $('#stockcodeCopy').trigger('filterChange');
                                               } else {
                                                   $('#ItemNumPos').html(data.Pos);
                                                   $('#ItemNumTotal').html(data.Total);
                                                   disableArrows();
                                               };
                                           });

    });

    $('#previousItemNum').click(function () {
        var canMove = true;
        if (!$('#saveButton').is(':disabled')) {
            canMove = confirm("You have Unsaved changes for this Item, Click ok to discard these changes and move to the previous item.")
        }
        if (canMove) {
            invMasterHub.server.previousItemNum($('#UpdateItemNum').val(),
                                            (InvMasterFilter == "" ? "1=1" : InvMasterFilter.getFilterString()),1).done(function (mess) {
                                                $('#UpdateItemNum').val(mess);
                                                $('#stockCode').val("")
                                                $('#ItemNumPos').html(parseInt($('#ItemNumPos').html()) - 1);
                                                stockCode();
                                                disableArrows();
                                            });
            $('#saveButton').attr('disabled', 'disabled');
            $('#stockCode').removeAttr('disabled');
        }

    });

    $('#nextItemNum').click(function () {
        var canMove = true;
        if (!$('#saveButton').is(':disabled')) {
            canMove = confirm("You have Unsaved changes for this Item, Click ok to discard these changes and move to the next item.")
        }
        if (canMove) {
            invMasterHub.server.nextItemNum($('#UpdateItemNum').val(),
                                            (InvMasterFilter == "" ? "1=1" : InvMasterFilter.getFilterString())).done(function (mess) {
                                                $('#UpdateItemNum').val(mess);
                                                $('#stockCode').val("")
                                                $('#ItemNumPos').html(parseInt($('#ItemNumPos').html()) + 1);
                                                stockCode();
                                                disableArrows();
                                            });
            $('#saveButton').attr('disabled', 'disabled');
            $('#stockCode').removeAttr('disabled');
        }
    });

    $('#RTSUpdate').change(function () {
        var sender = $(this);
        var item = document.getElementById('stockCode').value;
        var qty = document.getElementById('minReelQty').value;
        var include = this.checked;
        sender.attr('disabled', 'disabled');
        invMasterHub.server.updateReelQuant(item, qty, include).done(function (success) {
            if (!success) { alert('Changes not saved!  Please reenter the information.'); };
            sender.removeAttr('disabled');
        });
    });

    // End Reel Tracking Partial

    $('#quarantineButton').click(function () {
        var sender = $(this);
        var unquar = (sender.text() == 'Un-Quarantine Item');
        var item = document.getElementById('stockCode').value;
        if (item.trim() == '') { return false; };
        if (unquar) {
            $('#quarantineLabel').html("Un-<u>Q</u>uarantine Item");
            $('#quarMessage').html("<strong>Click submit to un-quarantine item " + item + ".</strong>");
            $('#quarRP').parent().show();
        } else {
            $('#quarantineLabel').html("<u>Q</u>uarantine Item");
            $('#quarMessage').html("<strong>Click submit to quarantine item " + item + ".  This will cause any allocated orders and locations with this item to be deallocated.</strong>");
            $('#quarRP').parent().hide();
        };

        $('#quarantineModal').modal("show");
    });

    $('#quarantineClick').click(function () {
        var item = document.getElementById('stockCode').value;
        var sender = $('#quarantineButton');
        var unquar = (sender.text() == 'Un-Quarantine Item');
        sender.attr('disabled', 'disabled');
        invMasterHub.server.quarantineItem(item, unquar, document.getElementById('quarRP').checked).done(function () {
            if (!unquar) {
                postQuarantineAlert();
                sender.html('Un-<u>Q</u>uarantine Item');
            } else {
                sender.html('<u>Q</u>uarantine Item');
                deleteAlert('quar_alert');
            };
            sender.removeAttr('disabled');
            invMasterTable.draw();
            $('#quarantineModal').modal('hide');
        });
    });

    $('#addButton').click(function () {
        $('#newItemNumber').val(document.getElementById('stockCode').value);
        $('#newDescription').val("");
        $('#itemAlert').html('');
        $('#addItemModal').modal("show").one('shown.bs.modal',function(){$('#newItemNumber').focus()});
        currentModal = "";
    });

    $('#newItemSave').click(function () {
        var newItemNumber = $('#newItemNumber').val();
        var newDescription = $('#newDescription').val();
        if (newItemNumber == null || newItemNumber == '') { $("#itemAlert").html('<div class="alert alert-warning alert-dismissible" id="itemExists_alert" role="alert"> <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button><strong>Item cannot be left blank.</strong></div>'); return false; };
        if (newDescription != "") {
            invMasterHub.server.addNewItem(newItemNumber, newDescription).done(function (valid) {
                if (valid) {
                    $('#stockCode').typeahead('val', newItemNumber);
                    var e = jQuery.Event('keyup');
                    e.which = 13;
                    $('#stockCode').trigger('input').trigger(e);
                    $('#itemDescription').val(newDescription);
                    $('#addItemModal').modal("hide");
                    $('#itemAlert').html('');
                } else {
                    //Alert Saying Item number already exists
                    $("#itemAlert").html('<div class="alert alert-warning alert-dismissible" id="itemExists_alert" role="alert"> <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button><strong>Item entered already exists.</strong></div>');
                };
            });
        } else {
            MessageModal("Warning", "A description is needed for the new item");
            $('#newDescription').focus();
        };
    });

    $('#deleteButton').click(function () {
        var sender = $(this);
        var item = document.getElementById('stockCode').value;
        if (item.trim() == '') { return false; };

        if (confirm('Click OK to delete item: ' + item + '.')) {
            sender.attr('disabled', 'disabled');
            invMasterHub.server.deleteItem(item).done(function (success) {
                if (!success) { alert('Delete failed!  Item exists in Inventory Map.  Please deallocate item from Inventory Map location(s) before deleting.'); }
                else {
                    $('#stockcodeCopy').trigger('filterChange');
                };
                sender.removeAttr('disabled');
            });
        };
    });

    $(document.body).on('click', '#printKit', function () {
        //reportsHub.server.printKitReport(document.getElementById('stockCode').value);
        title = 'Inventory Kit Report';
        getLLPreviewOrPrint('/InventoryMaster/printKitReport', {
            kitItemNumber: document.getElementById('stockCode').value
        }, true,'report', title);
    });

    $(window).on('beforeunload', function (e) {
        if (!$('#saveButton').is(':disabled')) {
            return 'You have unsaved changes for the current item, are you sure you want to leave?';
        }
        
    });

    stockCode = function () {
        invMasterTypeAhead.clearRemoteCache();
        var sender = $('#UpdateItemNum');
        $('#kitDisplay').hide();
        if (sender.val().length == 0) {
            sender.typeahead('val', '')
            clearAll(false);
            $('.disable-inputs input, .disable-inputs button, .disable-inputs select, .disable-inputs textarea').attr('disabled', 'disabled');
            $('#saveButton,#deleteButton,#quarantineButton, #previousItemNum, #nextItemNum').attr('disabled', 'disabled');
            $('#goToInvMap').attr('disabled', 'disabled');
            return false;
        }
        invMasterHub.server.selInvMasterData(sender.val()).done(function (DataObjList) {
            /* 
                start top data
            */
            // if what was returned was empty
            if (DataObjList.length == 1) {
                clearAll(false);
                $('#kitDisplay').hide()
                $('.disable-inputs input, .disable-inputs button, .disable-inputs select, .disable-inputs textarea').attr('disabled', 'disabled');
                $('#saveButton,#deleteButton,#quarantineButton').attr('disabled', 'disabled');
                $('#goToInvMap').attr('disabled', 'disabled');
                return false;
            };
            $('#goToInvMap').removeAttr('disabled');
            $('.disable-inputs input, .disable-inputs button, .disable-inputs select, .disable-inputs textarea').removeAttr('disabled');
            $('#quarantineButton,#deleteButton, #previousItemNum, #nextItemNum').removeAttr('disabled');
            disableArrows();
            var top = DataObjList[0];
            document.getElementById('supplierID').value = top.SupplyItemID;
            document.getElementById('itemDescription').value = top.Description;
            document.getElementById('category').value = top.Category;
            document.getElementById('subCategory').value = top.SubCategory;
            document.getElementById('replenishPoint').value = top.RepPoint;
            document.getElementById('replenishLevel').value = top.RepLevel;
            document.getElementById('reorderPoint').value = top.ReorPoint;
            document.getElementById('reorderQty').value = top.ReorQuant;
            document.getElementById('totQty').innerHTML = top.TotalQuant;
            document.getElementById('allPicks').innerHTML = top.TotalPicks;
            document.getElementById('allPuts').innerHTML = top.TotalPuts;
            document.getElementById('allWip').innerHTML = top.TotalWip;
            document.getElementById('UoM').value = top.UnitMeas;
            $('#KanBanReplenPoint').val(top.KanBanPoint);
            $('#KanBanReplenLevel').val(top.KanBanLevel);
            /* 
                start item setup data 
            */
            var itemSetup = DataObjList[1];
            // checkboxes
            document.getElementById('dateSensitive').checked = itemSetup.DateSense;
            document.getElementById('warehouseSensitive').checked = itemSetup.WareSense;
            document.getElementById('FIFO').checked = itemSetup.FIFO; $('#FIFO').trigger('change'); // hide div for textboxes if condition
            document.getElementById('FIFODate').value = itemSetup.FIFODate;
            document.getElementById('activeCheckbox').checked = itemSetup.Active;
            document.getElementById('splitCase').checked = itemSetup.SplitCase;
            // other item setup
            // zones
            var pZone = $('#pZone');
            var sZone = $('#sZone');
            pZone.val(itemSetup.PrimeZone.toLowerCase());
            if (pZone.val() != null && pZone.val().trim() != '') { sZone.removeAttr('disabled'); } else { sZone.attr('disabled', 'disabled').val(' '); };
            sZone.val(itemSetup.SecZone.toLowerCase());

            document.getElementById('pFenceQty').value = itemSetup.PickFenceQty;
            document.getElementById('caseQty').value = itemSetup.CaseQuant;
            document.getElementById('pSequence').value = itemSetup.PickSeq;
            // CAROUSEL
            document.getElementById('carCell').value = itemSetup.CarCell;
            document.getElementById('carVel').value = itemSetup.CarVel;
            document.getElementById('carMinQty').value = itemSetup.CarMin;
            document.getElementById('carMaxQty').value = itemSetup.CarMax;
            // BULK
            document.getElementById('bulkCell').value = itemSetup.BulkCell;
            document.getElementById('bulkVel').value = itemSetup.BulkVel;
            document.getElementById('bulkMinQty').value = itemSetup.BulkMin;
            document.getElementById('bulkMaxQty').value = itemSetup.BulkMax;
            // CARTON FLOW
            document.getElementById('cartonCell').value = itemSetup.CfCell;
            document.getElementById('cartonVel').value = itemSetup.CfVel;
            document.getElementById('cartonMinQty').value = itemSetup.CfMin;
            document.getElementById('cartonMaxQty').value = itemSetup.CfMax;
            // Open, completed, reprocess
            document.getElementById('RPcount').innerHTML = itemSetup.ReProcQuan;
            document.getElementById('OTcount').innerHTML = itemSetup.OpenQuan;
            document.getElementById('THcount').innerHTML = itemSetup.HistQuan;
            //disable/enable buttons 
            if (parseInt(itemSetup.ReProcQuan) == 0) {
                $('#Reprocbutton').attr('disabled', 'disabled');
            } else {
                $('#Reprocbutton').removeAttr('disabled');
            };
            if (parseInt(itemSetup.OpenQuan) == 0) {
                $('#OTbutton').attr('disabled', 'disabled');
            } else {
                $('#OTbutton').removeAttr('disabled');
            };
            if (parseInt(itemSetup.HistQuan) == 0) {
                $('#THbutton').attr('disabled', 'disabled');
            } else {
                $('#THbutton').removeAttr('disabled');
            };
            /*
                Kit Items
            */
            var kitItems = DataObjList[2];
            var newString = '';
            var appendString = '';

            if (kitItems.ItemNum.length == 0) {
                appendString = newString;
            } else {
                $('#kitDisplay').show();
                for (var x = 0; x < kitItems.ItemNum.length; x++) {
                    appendString += '<div class="row" style="padding-top:10px;" id="' + kitItems.ItemNum[x] + '_container"><div class="col-md-2"><div class="form-group has-feedback" style="margin-bottom:0px;"><input readonly type="text" class="form-control modal-launch-style item-modal inv-text" data-colname="[Kit Inventory].[Item Number]" name="kit-edit"  maxlength="50" id="kitItem_' + kitItems.ItemNum[x] + '" value="' + kitItems.ItemNum[x] + '" /><i style="top:0;" class="glyphicon glyphicon-resize-full form-control-feedback item-modal modal-launch-style"></i></div></div>' +
                                '<div class="col-md-3"><div class="form-group has-feedback" style="margin-bottom:0px;"><input readonly class="no-horizontal form-control modal-launch-style description-modal inv-text" data-colname="[Inventory].[Description]"  maxlength="255" rows="1" id="kitDesc_' + kitItems.ItemNum[x] + '" value="' + kitItems.Desc[x] + '" /><i style="top:0;" class="glyphicon glyphicon-resize-full form-control-feedback item-modal modal-launch-style"></i></div></div>' +
                                '<div class="col-md-3"><input type="text" class="form-control inv-text SpecFeats" name="kit-edit" placeholder="Special Features" data-colname="[Kit Inventory].[Special Features]" id="kitSpecFeats_' + kitItems.ItemNum[x] + '" value="' + kitItems.KitSpecFeats[x] + '" /></div>' +
                                '<div class="col-md-2"><input type="text" class="form-control inv-num" name="kit-edit" placeholder="Kit Quantity" maxlength="9" data-colname="[Kit Inventory].[Kit Quantity]" id="kitQty_' + kitItems.ItemNum[x] + '" value="' + kitItems.KitQuan[x] + '" /></div>' +
                                '<div class="col-md-1"><div class="pull-right"><button type="button" class="btn btn-danger remove-kit" data-toggle="tooltip" data-placement="top" title="Delete"  id="' + kitItems.ItemNum[x] + '_remove" style="margin-left:5px;"><span class="glyphicon glyphicon-remove"></span></button></div></div>' +
                                '<div class="col-md-1"><div class="pull-right"><button disabled="disabled" type="button" class="btn btn-primary save-kit" data-toggle="tooltip" data-placement="top" title="Save" id="' + kitItems.ItemNum[x] + '_save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div></div></div>';
                };
            };
            document.getElementById('kitContainer').innerHTML = appendString;
            $('.remove-kit,.save-kit,#addKitItem,.set-kit,#printKit').tooltip();
            /*
                Scan Codes
            */
            var scanCodes = DataObjList[3];
            var newString = '';
            var appendScanString = '';

            if (scanCodes.ScanCode.length == 0) {
                appendScanString = newString;
            } else {
                scanCodeData['Scan Codes'] = scanCodes.ScanCode;
                scanCodeData['Scan Types'] = scanCodes.ScanType;
                scanCodeData['Start Position'] = scanCodes.StartPos;
                scanCodeData['Code Length'] = scanCodes.CodeLen;
                scanCodeData['Scan Range'] = scanCodes.ScanRange;
                for (var x = 0; x < scanCodes.ScanCode.length; x++) {
                    var option;
                    if (scanCodes.ScanRange[x] == 'Yes') {
                        option = '<option selected="selected" value="Yes">Yes</option><option value="No">No</option>';
                    } else { option = '<option value="Yes">Yes</option><option selected="selected" value="No">No</option>'; };

                    appendScanString += '<div class="row" style="padding-top:10px;" id="' + x + '_container"><div class="col-md-6"><div class="row"><div class="col-md-6"><input maxlength="50" type="text" class="form-control scan-edit inv-text" data-colname="[Scan Codes].[Scan Code]" placeholder="Scan Code" value="' + scanCodes.ScanCode[x] + '" id="' + x + '_sc" /></div>' +
                            '<div class="col-md-6"><div class="form-group has-feedback"><input readonly maxlength="50" type="text" class="form-control modal-launch-style scan-modal scan-edit inv-text" data-colname="[Scan Codes].[Scan Type]"  id="' + x + '_st" value="' + scanCodes.ScanType[x] + '" /><i style="top:0;" class="glyphicon glyphicon-resize-full form-control-feedback scan-modal modal-launch-style"></i></div></div></div></div>' +
                            '<div class="col-md-4"><div class="row"><div class="col-md-4"><select class="form-control scan-edit inv-text" data-colname="[Scan Codes].[Scan Range]" id="' + x + '_select">' + option + '</select>' +
                            '</div><div class="col-md-4"><input maxlength="9" type="text" class="form-control scan-edit inv-num" data-colname="[Scan Codes].[Start Position]" placeholder="Start Position" id="' + x + '_sp" value="' + scanCodes.StartPos[x] + '" /></div>' +
                            '<div class="col-md-4"><input maxlength="9" type="text" class="form-control scan-edit inv-num" data-colname="[Scan Codes].[Code Length]" placeholder="Code Length" id="' + x + '_cl" value="' + scanCodes.CodeLen[x] + '" /></div></div></div>' +
                            '<div class="col-md-1"><div class="pull-right"><button type="button" class="btn btn-danger remove-scan" id="' + x + '_remove" style="margin-left:5px;"><span class="glyphicon glyphicon-remove"></span></button></div></div>' +
                            '<div class="col-md-1"><div class="pull-right"><button disabled="disabled" type="button" class="btn btn-primary save-scan" id="' + x + '_save" name="' + x + '_index"><span class="glyphicon glyphicon-floppy-disk"></span></button></div></div></div>';
                };
            };
            document.getElementById('scanCodeContainer').innerHTML = appendScanString;
            /*
                Other
            */
            var otherTab = DataObjList[4];
            document.getElementById('uCost').value = otherTab.UnitCost;
            document.getElementById('supID').value = otherTab.SupplierID;
            document.getElementById('manuID').value = otherTab.ManufactID;
            document.getElementById('specialFeatures').value = otherTab.SpecFeats;
            /*
                Reel Tracking
            */
            var reelTab = DataObjList[5];
            document.getElementById('minReelQty').value = reelTab.MinRTSReelQuan;
            document.getElementById('RTSUpdate').checked = reelTab.IncAutoUpd;
            /*
                Weigh Scale
            */
            var weighTab = DataObjList[6];
            document.getElementById('useScale').checked = weighTab.UseScale;
            document.getElementById('avgWeight').value = weighTab.AvgPieceWeight;
            document.getElementById('sampleQty').value = weighTab.SampleQuan;
            document.getElementById('minScaleQty').value = weighTab.MinUseScale;

            if (DataObjList[7].Quarantined) {
                $('#quarantineButton').text('Un-Quarantine Item');
                postQuarantineAlert();
            } else {
                $('#quarantineButton').text('Quarantine Item');
                deleteAlert('quar_alert');
            };

            invMasterTable.draw();
            currentData = new Array();
            $('.inv-edit').each(function () {
                var $this = $(this);
                if ($this.is(':checkbox')) {
                    currentData.push([$this.attr('id'), $this.prop('checked')]);
                } else {
                    currentData.push([$this.attr('id'), ($this.val() == null ? '' : $this.val())]);
                };
            });
        });
    };


    $('#stockCode').on('keyup', function (e) {
        if (e.which == '13') {
            $('#stockCode').typeahead('close')
            var $this = $('#stockCode');
            //run hub function to convert value to the item number of the selected value
            //if none exists do what item exists does
            $.ajax({
                url: "/InventoryMaster/convertToItemNum",
                type: "POST",
                data: { Val: $this.val() },
                success: function (item) {
                    if (item != "") {
                        $('#stockCode').val(item);
                        var Item = $this.val();

                        globalHubConn.server.itemExists(Item).done(function (data) {
                            if (!data) {
                                $this.typeahead('val', '');
                                MessageModal("Inventory", "Item " + Item + " Does not exist!", function () {
                                    $('#stockCode').focus()
                                });
                            } else {
                                $('#UpdateItemNum').val(Item);
                                invMasterHub.server.getItemNumCount($('#stockCode').val(),
                                                                    (InvMasterFilter == "" ? "1=1" : InvMasterFilter.getFilterString())).done(function (data) {
                                                                        $('#ItemNumPos').html(data.Pos);
                                                                        $('#ItemNumTotal').html(data.Total);
                                                                        disableArrows();
                                                                    });
                                stockCode();
                                disableArrows();
                            };
                        });
                    } else {
                        $this.typeahead('val', '');
                        MessageModal("Inventory", "Value " + Item + " Does not exist!", function () {
                            $('#stockCode').focus()
                        })
                    };
                }
            });
        };
    });

    // prevents the hub function (called immediately) from being called too fast
    $.connection.hub.start().done(function () {
        // handles a request from open transactions filling in the item number field (stock code)
        stockCode();
        disableArrows();
        var newitem = $('#stockcodeCopy').val();
        $('#stockcodeCopy').trigger('filterChange');
        setTimeout(function () {
            if ($('#NewItem').val() == 'true' && $('#AppName').val().toUpperCase() == 'IM') {
                $('#addItemModal').modal('show');
                $('#newItemNumber').val(newitem);
                $('#newDescription').val('');
            };
        }, 1000);
    });
});