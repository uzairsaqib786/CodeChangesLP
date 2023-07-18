// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var invMapTable;
var invMapHub = $.connection.inventoryMapHub;
var columns = new Array();
$(document).ready(function () {
    // initialization
    $.each($('#selection').children(), function (index, element) {
        if (index != 0) {
            columns.push($(element).attr('value'));
        };
    });

    // plugin initialization (datatables, typeaheads have separate file)
    $('.toggles').toggles({
        width: 60,
        height: 25
    });
    // Modal Init
    // prevent ESC
    $('#invmap_modal').modal({ keyboard: false, show: false });
    

    // event listeners
    $('#editInv, #addItem').click(function () {
        showModal(this.id == 'editInv');
    });
    $('#goToInvMaster').click(function () {
        window.location.href = "/InventoryMaster?ItemNumber=" + $('tr.active').find('td:nth-child(' + (columns.indexOf('Item Number') + 1) + ')').html() + '&App=' + $('#AppName').val();
    });
    $('#goToTransHist').click(function () {
        window.location.href = "/transactions?viewtoShow=3&Location=" + $('tr.active').find('td:nth-child(' + (columns.indexOf('Location Number') + 1) + ')').html() + '&App=' + $('#AppName').val();
    })
    $('#columnSequence').click(function () {
        document.getElementById("colSequenceForm").submit();
    });
    // filters
    $('#searchString').on('change', function () {
        invMapTable.draw();
    });

    function clearAllFilters() {
        InvMapFilterMen.clearFilter();
        $('#searchString').val('').trigger('input');
    }

    $('#allRecords').click(function () {
        $('#viewButton').html('<u>V</u>iew All Locations<span class="caret"></span>');
        clearAllFilters();
        $('#viewButton').attr('value', '')
        invMapTable.draw();
    });
    $('#openRecords').click(function () {
        $('#viewButton').html('View <u>O</u>pen Locations<span class="caret"></span>');
        clearAllFilters();
        $('#viewButton').attr('value', $(this).attr('value'))
        invMapTable.draw();
    });
    $('#quarRecords').click(function (e) {
        $('#viewButton').html('View Quaran<u>t</u>ined Locations<span class="caret"></span>');
        clearAllFilters(); 
        $('#viewButton').attr('value', $(this).attr('value'))
        invMapTable.draw();
    });
    // end filters

    $('#Zone, #Carousel, #Row, #Shelf, #Bin').on('input', function () {
        var locationNumber = $('#Zone').val() + $('#Carousel').val() + $('#Row').val() + $('#Shelf').val() + $('#Bin').val();
        $('#LocNum').val(locationNumber);
    });
    $('#Zone, #LocName').on('input', function () {
        setLocationZone(false);
    });

    // integer fields
    $('#Carousel').on('input', function () {
        setNumericInRange($(this), 1, 8);
    });
    $('#AltLight, #LaserX, #LaserY').on('input', function () {
        setNumericInRange($(this), SqlLimits.numerics.int);
    });
    $('#ItemQty').on('input', function () {
        setNumericInRange($(this), 0, SqlLimits.numerics.int.max);
    });
    $('#AltLight, #LaserX, #LaserY, #ItemQty, #SerNum, #LotNum').on('focusout', function () {
        if (this.value.trim() == '') { this.value = 0; };
    });
    $('#MaxQty').on('input', function () {
        var min = $('#MinQty'), max = $(this);
        if (min.val().trim() == '') {
            min.val(0);
        };
        setNumericInRange(max, min.val(), null);
    });
    $('#MinQty').on('input', function () {
        var min = $(this), max = $('#MaxQty');
        if (max.val().trim() == '') {
            setNumericInRange(min, 0, null);
            max.val(min.val());
        } else {
            setNumericInRange(min, 0, max.val());
        };
    });
    $('#MaxQty, #MinQty').on('focusout', function () {
        var max = $('#MaxQty'), min = $('#MinQty');
        if (min.val().trim() == '') {
            min.val(0);
        };
        if (max.val().trim() == '') {
            max.val(min.val());
        };
    });
    // end integers
    $('#invmap_modal').on('hide.bs.modal', function () {
        updateTableRow();
    });

    $(document.body).on('click', '#printSelectedModal', function () {
        $('#PrintSelected').trigger('click');
    });

    $('#ItemNumber').on('focusout', function () {
        if (this.value.trim() == '') {
            $('#Description, #UoM, #ExpDate, #PutDate, #UF1, #UF2, #Revision').val('');
        } else {
            var item = $('#ItemNumber');
            invMapHub.server.getItemDetails(item.val(), $('#Zone').val()).done(function (details) {
                if (details.length == 0) {
                    item.val('').trigger('input').blur();
                    $('#Description, #UoM, #ExpDate, #PutDate, #UF1, #UF2, #Revision').val('');
                } else {
                    $('#Description').val(details[0]);
                    $('#UoM').val(details[1]);
                    $('#MaxQty').val(details[2]);
                    $('#MinQty').val(details[3]);
                    item.data('date_sensitive', details[4]);
                    item.data('warehouse_sensitive', details[5]);

                    var match = '';
                    if ($('#CellSz').val() != details[6] && details[6] != '') {
                        match += 'Cell Size';
                    };
                    if ($('#Velocity').val() != details[7] && details[7] != '') {
                        if (match != '') { match += ', Velocity Code'; } else { match += 'Velocity Code'; };
                    };
                    if (match != '') {
                        alert('Provided ' + match + ' do not match Inventory Master.  Expecting Cell Size: ' + details[6] + ' and Velocity Code: ' + details[7] + ' for specified Item and Zone');
                    };
                };
            });
        };
    });

    $('#DateSensitive').on('toggle', function (e, checked) {
        // check if item is date sensitive or empty and count open transactions
        var item = $('#ItemNumber'), alerted = false, value = false;

        if (item.val().trim() != '') {
            if (item.data('date_sensitive').toLowerCase() == 'true' && !checked) {
                alert('Date Sensitive cannot be set to No because the item assigned to this location is date sensitive');
                value = true;
                alerted = true;
            } else if (item.data('date_sensitive').toLowerCase() == 'false' && checked) {
                alert('Date Sensitive cannot be set to Yes because the item assigned to this location is not date sensitive');
                value = false;
                alerted = true;
            };
        };
        if (alerted) {
            setTimeout(function () {
                $('#DateSensitive').data('toggles').setValue(value);
            }, 0);
            return false;
        };
        // get count > 0 from open transactions
        invMapHub.server.dateSensitiveChange($('#LocNum').val()).done(function (conflict) {
            if (conflict && !alerted) {
                alert('Date Sensitive cannot be changed because there are open transactions at this location.');
                alerted = true;
            };

            if (alerted) {
                setTimeout(function () {
                    $('#DateSensitive').data('toggles').setValue(!checked);
                }, 0);
                return false;
            };
        });
    });

    $('#MasterLoc').on('toggle', function (e, checked) {
        if (checked) {
            $('#MInvMapID').val($('#InvMapID').val());
        } else {
            // get min master record's id
            invMapHub.server.selectMasterMapID($('#InvMapID').val(), $('#Warehouse').val(), $('#Zone').val(), $('#Carousel').val(),
                $('#Row').val(), $('#Shelf').val(), $('#Bin').val()).done(function (masterInvMapID) {
                    $('#MInvMapID').val(masterInvMapID);
            });
        };
    });

    // inventory map modifying listeners
    $('#clearItem').click(function () {
        if (confirm('Click OK to clear current Inventory Map record.')) {
            invMapHub.server.clearInventoryMap($('#InvMapID').val()).done(function (DynWhse) {
                $('#UoM, #PutDate, #UF1, #UF2, #ItemNumber, #ExpDate, #Description').val('');
                $('#MaxQty, #MinQty, #ItemQty, #Revision, #SerNum, #LotNum').val(0);
                $('#Dedicated').data('toggles').setValue(false);
                if (DynWhse) {
                    $('#Warehouse').val('');
                };
                updateTableRow();
            });
        };
    });
    $('#duplicateItem').click(function () {
        if (confirm('Click OK to duplicate the selected Inventory Map entry.')) {
            invMapHub.server.duplicateItem($('#InvMapID').val()).done(function () {
                invMapTable.draw();
                $('#invmap_modal').modal('hide');
            });
        };
    });
    $('#quarItem').click(function () {
        var $this = $(this), text = $this.text().toLowerCase();

        if (confirm('Click OK to ' + text + ' current entry.')) {
            if (text == 'quarantine') {
                $this.text('Unquarantine');
                invMapHub.server.quarantineInventory($('#InvMapID').val()).done(function () {
                    invMapTable.draw();
                    $('#invmap_modal').modal('hide');
                });
            } else {
                $this.text('Quarantine');
                invMapHub.server.unQuarantineInventory($('#InvMapID').val()).done(function () {
                    invMapTable.draw();
                    $('#invmap_modal').modal('hide');
                });
            };
            
        };
    });

    $('#quarItemMain').click(function () {
        var row = $('tr.active');
        var InvMapID = row.find(':nth-child(' + (columns.indexOf('Inv Map ID') + 1) + ')').html();
        var $this = $(this), text = $this.text().toLowerCase();


        if (confirm('Click OK to ' + text + ' current entry.')) {
            if (text == 'quarantine') {
                invMapHub.server.quarantineInventory(InvMapID).done(function () {
                    invMapTable.draw();
                    
                });
            } else {
                
                invMapHub.server.unQuarantineInventory(InvMapID).done(function () {
                    invMapTable.draw();
                    $this.text('Quarantine')
                    
                });
            };

        };

    });


    $('#deleteItem, #deleteItemModal').click(function () {
        if (confirm('Click OK to delete selected Inventory Map entry.')) {
            var id = 0;
            var qty = 0;

            if (this.id == 'deleteItem') {
                //table
                id = $('tr.active').find(':nth-child(' + (columns.indexOf('Inv Map ID') + 1) + ')').html();
                qty = $('tr.active').find(':nth-child(' + (columns.indexOf('Item Quantity') + 1) + ')').html();
            } else {
                //modal
                id = $('#InvMapID').val();
                qty = $('#ItemQty').val();
            };
            var modal = $('#invmap_modal');
            if (id == 0) {
                modal.modal('hide');
            } else {
                if (parseInt(qty) > 0) {
                    //cant delete
                    MessageModal('Unable to Delete Location', 'This location currently has a positive item quantity and cannot be deleted');
                } else {
                    invMapHub.server.deleteInvMapID(id).done(function (rslt) {
                        if (rslt) {
                            //deleted
                            if (modal.hasClass('in')) {
                                modal.modal('hide');
                            };
                            invMapTable.draw(false);
                        } else {
                            //not deleted
                            MessageModal('Unable to Delete Location', 'This location has open transactions assigned to it and cannot be deleted');
                        };
                    });
                };
                
            };
        };
    });
    $('#invmap_submit').click(function () {
        saveInvMapEntry();
    });


    //Opens the order status screen for the given order number
    $(document.body).on('click', '#TransHistBtn', function () {
        //go to order status here for the corresponding order number
        var $handle = $(window.open("/Transactions?viewtoShow=3&App=" + $('#AppName').val() + '&popup=true', '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'));
        try {
            $handle[0].focus();
        } catch (e) {
            MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.');
        };
    });

    // end inventory map modifying listeners

    // function definitions
    function showModal(edit) {
        var modal = $('#invmap_modal');

        if (edit) {
            var row = $('tr.active'), QtyPick = $('#QtyPick'), QtyPut = $('#QtyPut'), ItemQty = $('#ItemQty'), ItemNumber = $('#ItemNumber');
            var InvMapID = row.find(':nth-child(' + (columns.indexOf('Inv Map ID') + 1) + ')').html();
            $('#ItemNumber, #LocName, #Zone, #Carousel, #Row, #Shelf, #Bin, #clearItem').attr('disabled', 'disabled');
            invMapTable.draw(false).one('draw.dt', function () {
                var r = invMapTable
                    // gets inventory map table datatables column for inv map id
                    .column(columns.indexOf('Inv Map ID'))
                    // gets the data in the column for inv map id
                    .data()
                    // gets the index of the row we want to reselect
                    .indexOf(InvMapID);
                // triggers the click on the row we want
                $('#invMapTable tbody').find('tr:nth-child(' + (r + 1) + ')').trigger('click');
                // get the new data from after the draw event
                row = $('tr.active');

                QtyPick.val(row.find(':nth-child(' + (columns.indexOf('Quantity Allocated Pick') + 1) + ')').html());
                QtyPut.val(row.find(':nth-child(' + (columns.indexOf('Quantity Allocated Put Away') + 1) + ')').html());
                ItemQty.val(row.find(':nth-child(' + (columns.indexOf('Item Quantity') + 1) + ')').html());
                
                // stuff is allocated so no item change or loc change
                if ((ItemQty.val().trim() != '0' && ItemQty.val().trim() != '') || (QtyPick.val().trim() != '' && QtyPick.val().trim() != '0') || (QtyPut.val().trim() != '' && QtyPut.val().trim() != '0')) {
                    $('#ItemNumber, #LocName, #Zone, #Carousel, #Row, #Shelf, #Bin, #clearItem').attr('disabled', 'disabled');
                }else if($('#AppName').val() == 'OM'){
                    $('#ItemNumber, #LocName, #Zone, #Carousel, #Row, #Shelf, #Bin, #clearItem').attr('disabled', 'disabled');
                } else {
                        $('#ItemNumber, #LocName, #Zone, #Carousel, #Row, #Shelf, #Bin, #clearItem').removeAttr('disabled');
                };
                $('#LocNum').val(row.find(':nth-child(' + (columns.indexOf('Location Number') + 1) + ')').html());
                $('#LocName').typeahead('val',row.find(':nth-child(' + (columns.indexOf('Location') + 1) + ')').html());
                $('#LaserX').val(row.find(':nth-child(' + (columns.indexOf('Laser X') + 1) + ')').html());
                $('#LaserY').val(row.find(':nth-child(' + (columns.indexOf('Laser Y') + 1) + ')').html());

                var warehouse = $('#Warehouse');
                warehouse.val(row.find(':nth-child(' + (columns.indexOf('Warehouse') + 1) + ')').html());
                if (warehouse.val().toLowerCase().indexOf('quarantine') > -1) {
                    $('#quarItem').text('Unquarantine');
                } else {
                    $('#quarItem').text('Quarantine');
                };
                $('#Zone').typeahead('val',row.find(':nth-child(' + (columns.indexOf('Zone') + 1) + ')').html());
                $('#Carousel').val(row.find(':nth-child(' + (columns.indexOf('Carousel') + 1) + ')').html());
                $('#Row').val(row.find(':nth-child(' + (columns.indexOf('Row') + 1) + ')').html());
                $('#Shelf').val(row.find(':nth-child(' + (columns.indexOf('Shelf') + 1) + ')').html());
                $('#Bin').val(row.find(':nth-child(' + (columns.indexOf('Bin') + 1) + ')').html());

                $('#UoM').val(row.find(':nth-child(' + (columns.indexOf('Unit of Measure') + 1) + ')').html());
                $('#CellSz').val(row.find(':nth-child(' + (columns.indexOf('Cell Size') + 1) + ')').html());
                $('#Velocity').val(row.find(':nth-child(' + (columns.indexOf('Velocity Code') + 1) + ')').html());
                $('#AltLight').val(row.find(':nth-child(' + (columns.indexOf('Alternate Light') + 1) + ')').html());

                $('#UF1').val(row.find(':nth-child(' + (columns.indexOf('User Field1') + 1) + ')').html());
                $('#UF2').val(row.find(':nth-child(' + (columns.indexOf('User Field2') + 1) + ')').html());
                $('#InvMapID').val(InvMapID);
                $('#MInvMapID').val(row.find(':nth-child(' + (columns.indexOf('Master Inv Map ID') + 1) + ')').html());

                var masterLoc = row.find(':nth-child(' + (columns.indexOf('Master Location') + 1) + ')').html().toLowerCase() == 'true';
                var dedicated = row.find(':nth-child(' + (columns.indexOf('Dedicated') + 1) + ')').html().toLowerCase() == 'true';
                var ds = row.find(':nth-child(' + (columns.indexOf('Date Sensitive') + 1) + ')').html().toLowerCase() == 'true';
                $('#MasterLoc').data('toggles').setValue(masterLoc);
                $('#Dedicated').data('toggles').setValue(dedicated);
                $('#DateSensitive').data('toggles').setValue(ds);

                ItemNumber.val(row.find(':nth-child(' + (columns.indexOf('Item Number') + 1) + ')').html());
                invMapHub.server.getDateWarehouseSensitive(ItemNumber.val()).done(function (dwObj) {
                    ItemNumber.data('warehouse_sensitive', dwObj.WarehouseSensitive.toString());
                    ItemNumber.data('date_sensitive', dwObj.DateSensitive.toString());
                });
                $('#Description').val(row.find(':nth-child(' + (columns.indexOf('Description') + 1) + ')').html());

                $('#MaxQty').val(row.find(':nth-child(' + (columns.indexOf('Maximum Quantity') + 1) + ')').html());
                $('#MinQty').val(row.find(':nth-child(' + (columns.indexOf('Min Quantity') + 1) + ')').html());
                $('#PutDate').val(row.find(':nth-child(' + (columns.indexOf('Put Away Date') + 1) + ')').html());

                $('#SerNum').val(row.find(':nth-child(' + (columns.indexOf('Serial Number') + 1) + ')').html());
                $('#LotNum').val(row.find(':nth-child(' + (columns.indexOf('Lot Number') + 1) + ')').html());
                $('#Revision').val(row.find(':nth-child(' + (columns.indexOf('Revision') + 1) + ')').html());
                $('#ExpDate').val(row.find(':nth-child(' + (columns.indexOf('Expiration Date') + 1) + ')').html());

                
                setLocationZone(true);
                $('.modal-hide').show();
            });
        } else {
            modal.find('input, select').val('');
            $('#LaserX, #LaserY, #AltLight, #QtyPut, #QtyPick, #ItemQty, #MaxQty, #MinQty, #SerNum, #LotNum, #Revision, #InvMapID, #MInvMapID').val(0);
            modal.find('.toggles').each(function (index, target) {
                $(target).data('toggles').setValue(false);
            });
            $('#ItemNumber, #LocName, #Zone, #Carousel, #Row, #Shelf, #Bin').removeAttr('disabled');
            setLocationZone(false);
            $('.modal-hide').hide();
        };

        modal.modal('show');
    };

    function validateInvMapEntry() {
        var zone = $('#Zone');
        // if zone hasn't been selected or if the typeahead hasn't been used to set location/zone
        if (zone.val().trim() == '' || $('.forced-typeahead').hasClass('has-warning')) {
            return 'zone';
        };

        var item = $('#ItemNumber'), dateSensitive = $('#DateSensitive'), warehouse = $('#Warehouse'), velocity = $('#Velocity');
        if (item.val().trim() != '') {
            // if warehouse sensitive with no warehouse
            if (item.data('warehouse_sensitive').toLowerCase() == 'true' && warehouse.val().trim() == '') {
                return 'warehouse';
            };

            // if the date sensitive checkbox is enabled
            if (dateSensitive.data('toggles').active) {
                // and the item is NOT date sensitive
                if (item.data('date_sensitive').toLowerCase() != 'true') {
                    return 'date sensitive - ds';
                };
            } else {
                // if the date sensitive checkbox is not checked AND the item IS date sensitive
                if (item.data('date_sensitive').toLowerCase() == 'true') {
                    return 'date sensitive - item';
                };
            };
        };
    };

    /*
        Refreshes the table's data and reclicks the previously selected entry
    */
    function updateTableRow(mapID) {
        if (mapID == null) {
            mapID = $('#InvMapID').val();
        };
        // maintain the current record, but update it
        invMapTable.draw(false).one('draw.dt', function () {
            var row = invMapTable
                // gets inventory map table datatables column for inv map id
                .column(columns.indexOf('Inv Map ID'))
                // gets the data in the column for inv map id
                .data()
                // gets the index of the row we want to reselect
                .indexOf(mapID);
            // triggers the click on the row we want
            $('#invMapTable tbody').find('tr:nth-child(' + (row + 1) + ')').trigger('click');
        });
    };
    /*
        Returns js object with all editable properties for saving
    */
    function getEntry() {
        return {
            InvMapID: $('#InvMapID').val(), MasterLocation: $('#MasterLoc').data('toggles').active, MasterInvMapID: $('#MInvMapID').val(),
            Location: $('#LocName').val(), LaserX: $('#LaserX').val(), LaserY: $('#LaserY').val(),
            Warehouse: $('#Warehouse').val(), Zone: $('#Zone').val(), Carousel: $('#Carousel').val(), Row: $('#Row').val(), Shelf: $('#Shelf').val(), Bin: $('#Bin').val(),
            CellSize: $('#CellSz').val(), VelocityCode: $('#Velocity').val(), AltLight: $('#AltLight').val(),
            UF1: $('#UF1').val(), UF2: $('#UF2').val(),
            InvMapID: $('#InvMapID').val(), MasterInvMapID: $('#MInvMapID').val(),
            Dedicated: $('#Dedicated').data('toggles').active, DateSensitive: $('#DateSensitive').data('toggles').active,
            ItemNumber: $('#ItemNumber').val(), MaxQty: $('#MaxQty').val(), MinQty: $('#MinQty').val(),
            SerialNumber: $('#SerNum').val(), LotNumber: $('#LotNum').val(), Revision: $('#Revision').val()
        };
    };

    /*
        Saves a new or editted Inventory map entry
    */
    function saveInvMapEntry() {
        var valid = validateInvMapEntry();
        if (valid != null) {
            switch (valid) {
                case 'warehouse':
                    // the item is warehouse sensitive and warehouse is not specified.
                    alert('Selected item is warehouse sensitive.  Please specify a warehouse.');
                    break;
                case 'date sensitive - item':
                    alert('Item is date sensitive.  Please set date sensitive before saving.');
                    break;
                case 'date sensitive - ds':
                    alert('Date sensitive is checked and the item placed in this location is not date sensitive.  Please uncheck date sensitive before saving.');
                    break;
                case 'zone':
                    alert('Zone and Location need to be set via the dropdown in order to save.');
                    break;
            };
            return false;
        };

        var entry = getEntry();
        // new entry
        if (entry.InvMapID == 0) {
            invMapHub.server.addNewInvMap(entry).done(function () {
                invMapTable.draw();
                $('#invmap_modal').modal('hide');
            });
        } // edit entry
        else {
            invMapHub.server.editInvMap(entry);
        };
    };
    // end function definitions
});

function toggleDisabledButtons(disable) {
    var controls = $('#goToToggle, #editInv, #deleteItem, #adjustQuantity, #quarItemMain');
    if (disable) {
        controls.attr('disabled', 'disabled');
    } else {
        controls.removeAttr('disabled');
    };
};

function quarantineButton() {
    var row = $('tr.active');
    var quarantineStatus = row.find(':nth-child(' + (columns.indexOf('Warehouse') + 1) + ')').html().toLowerCase();
    var buttonText = $('#quarItemMain');

    if (quarantineStatus == 'quarantine') {
        buttonText.text('Unquarantine')
    } else {
        buttonText.text('Quarantine')
    };

};

function setLocationZone(selected) {
    if (selected) {
        $('.forced-typeahead').addClass('has-success').removeClass('has-warning').children('.glyphicon').addClass('glyphicon-ok').removeClass('glyphicon-warning-sign');
    } else {
        $('.forced-typeahead').removeClass('has-success').addClass('has-warning').children('.glyphicon').removeClass('glyphicon-ok').addClass('glyphicon-warning-sign');
    };
};

invMapHub.client.tableUpdated = function (data) {
    var onScreenIndex = invMapTable
                // gets inventory map table datatables column for inv map id
                .column(columns.indexOf('Inv Map ID'))
                // gets the data in the column for inv map id
                .data()
                // gets the index of the row we want to reselect
                .indexOf(data.invMapId.toString());
    if (onScreenIndex >= 0) {
        var tr = invMapTable.$('tr').eq(onScreenIndex);
        if (!tr.hasClass('active')) {
            tr.addClass('Updating');
            //Update Row is user isnt on the row to be updated
            for (var x = 0; x < data.columns.length; x++) {
                console.log(data.columns[x] + ' ' + data.update[x] + ' ' + columns.indexOf(data.columns[x]))
                tr.find('td').eq(columns.indexOf(data.columns[x]))
                    .html(data.update[x]);
            };
            tr.removeClass('Updating');
        };
    };
};