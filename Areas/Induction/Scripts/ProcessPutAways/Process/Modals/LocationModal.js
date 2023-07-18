// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var locationNoWarehouse = false;
$(document).ready(function () {
    var selected = { InvMapID: 0 };

    $('#batch_location_modal').on('hidden.bs.modal', function () {
        if (locationNoWarehouse == true) {
            trigger_warehouse_modal($('#Warehouse'));
        }
    })
    
    // launch the modal for location assignment
    $('#AssignLocation').click(function () {
        var whse = $('#Warehouse').val().trim().toLowerCase();
        if ($('#WhseSens').is(':checked') && (whse == 'none' || whse == '')) {
            MessageModal('Error', 'This item is warehouse sensitive and must be assigned a warehouse before the process can continue.', function () {
                $('#Warehouse').click();
            });
        } else if ($('#FIFO').is(':checked') && $('#FIFODate').val().trim().toLowerCase() == 'expiration date' && $('#ExpDate').val().trim() == '') {
            MessageModal('Error', 'This item is marked as FIFO with Expiration Date as its FIFO Date.  You must provide an Expiration Date.');
        } else {
            $('#batch_location_submit').attr('disabled', 'disabled');
            var locTA = $('#LocationTA');
            locTA.typeahead('val', '');
            $('#batch_location_modal').one('shown.bs.modal', function () {
                locTA.focus();
            });
            $('#batch_location_modal').modal('show');
        };
    });

    // sets the inv map id
    $('#batch_location_submit').click(function () {
        locationNoWarehouse = false
        if (selected.InvMapID == 0) {
            MessageModal('Error', 'Selected location is ambiguous.  Please select a location from the dropdown.');
        } else {
            var validZone = ($('#modal_zones').html().trim().split(' ').indexOf(selected.Zone) >= 0);
            if (validZone) {

                //Reserve Location 
                batchHub.server.reserveLocation(selected.InvMapID, $('#Zone').val(), ($('#IsDedicated').val() == 'true')).done(function (rslt) {
                    if (!rslt) {
                        MessageModal('Error', 'Selected location  is not able to be confirmed. Please try again');
                    } else {
                        $('#InvMapID').val(selected.InvMapID);
                        $('#Warehouse').val(selected.Warehouse);
                        $('#Zone').val(selected.Zone);
                        $('#Carousel').val(selected.Carousel);
                        $('#Row').val(selected.Row);
                        $('#Shelf').val(selected.Shelf);
                        $('#Bin').val(selected.Bin);
                        $('#CellSize').val(selected.CellSize);
                        $('#Velocity').val(selected.Velocity);
                        $('#Qty').val(selected.Qty);
                        $('#MaxQty').val(selected.Max);
                        $('#QtyAlloc').val(selected.QtyPut);
                        if (selected.Warehouse == "" && $('#WhseSens').is(':checked')) {
                            alert("The Location you selected does not have a warehouse, select a warehouse to continue")
                            locationNoWarehouse = true;
                        }
                        $('#batch_location_modal').modal('hide');
                    };
                });

                
            } else {
                zoneBatch(selected.Zone);
            };
        };
    });

    var batch_location = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            cache: false,
            url: ('/IM/ProcessPutAways/GetBatchLocationTypeahead?location='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#LocationTA').val() + '&warehouse=' + $('#Warehouse').val() + '&serial=' + $('#SerialNumber').val() +
                    '&lot=' + $('#LotNum').val() + '&ccell=' + $('#CCell').val() + '&cvel=' + $('#CVel').val() + '&bcell=' + $('#BCell').val() +
                    '&bvel=' + $('#BVel').val() + '&cfcell=' + $('#FCell').val() + '&cfvel=' + $('#FVel').val() + '&item=' + $('#ItemNumber').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            }
        }
    });
    
    batch_location.initialize();

    $('#LocationTA').typeahead({
        hint: false,
        highlight: false,
        minLength: 1
    }, {
        name: "LocationTA",
        displayKey: 'LocNum',
        source: batch_location.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:15%;">Loc. Number</p><p class="typeahead-header" style="width:7%;">Whse</p>\
                    <p class="typeahead-header" style="width:20%">Loc.</p><p class="typeahead-header" style="width:15%">Item #</p>\
                    <p class="typeahead-header" style="width:7%">Cell</p><p class="typeahead-header" style="width:7%">Velocity</p>\
                    <p class="typeahead-header" style="width:7%">Stock</p><p class="typeahead-header" style="width:7%">Max Qty</p>\
                    <p class="typeahead-header" style="width:7%">Qty Alloc</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:15%;">{{LocNum}}</p><p class="typeahead-row" style="width:7%;">{{Warehouse}}</p>\
                                            <p class="typeahead-row" style="width:20%">{{Location}}</p><p class="typeahead-row" style="width:15%">{{ItemNumber}}</p>\
                                            <p class="typeahead-row" style="width:7%">{{CellSize}}</p><p class="typeahead-row" style="width:7%">{{Velocity}}</p>\
                                            <p class="typeahead-row" style="width:7%">{{Qty}}</p><p class="typeahead-row" style="width:7%">{{Max}}</p>\
                                            <p class="typeahead-row" style="width:7%">{{QtyPut}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        $('#batch_location_submit').removeAttr('disabled');
        selected = datum;
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', '900px');
    });

    $('#LocationTA').on('input', function () {
        batch_location.clearRemoteCache();
        $('#batch_location_submit').attr('disabled', 'disabled');
        selected = { InvMapID: 0 };
    });
});