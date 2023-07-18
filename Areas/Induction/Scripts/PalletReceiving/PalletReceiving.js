// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var PalletHub = $.connection.palletReceivingHub

$(document).ready(function () {

    $('#PalletToteID').keyup(function (e) {
        if (e.keyCode === 13) {
            $('#PalletItemNum').focus();
        };
    });

    $('#PalletItemNum').keyup(function (e) {
        if (e.keyCode === 13) {
            $('#PalletQuant').focus();
        };
    });

    $('#ProcessPallet').click(function () {
        if ($('#PalletQuant').val() == "" || $('#PalletToteID').val() == "" || $('#PalletItemNum').val() == "") {
            MessageModal('Fields Missing', 'Not all the fields were filled out. Please fill them out');
        } else if ($('#PalletQuant').val() <= 0) {
            MessageModal('Invalid Quantity', 'An invalid quantity was entered. Please enter a quantity greater than 0');
        } else {
            PalletHub.server.validateTote($('#PalletToteID').val()).done(function (ToteValid) { //Valid tote
                if (ToteValid) {
                    PalletHub.server.validateItem($('#PalletItemNum').val()).done(function (ItemValid) { //Valid item
                        if (ItemValid) {
                            PalletHub.server.processPallet($('#PalletToteID').val(), $('#PalletItemNum').val(), $('#PalletQuant').val()).done(function (Succ) {
                                if (Succ) {
                                    MessageModal('Pallet Processed', 'Pallet was processed',
                                        function () {
                                            $('#PalletQuant').val("");
                                            $('#PalletToteID').val("").focus();
                                            $('#PalletItemNum').val("");
                                    }, undefined, undefined);
                                } else {
                                    MessageModal('Error', 'An error occurred processing this pallet setup');
                                };
                            });
                        } else {
                            MessageModal('Invalid Item Entered', 'This item does not exist in Inventory');
                        };
                    });
                } else {
                    MessageModal('Invalid Tote Entered', 'This tote id already exists in Open Transactions');
                };
            });
        };
    });

});