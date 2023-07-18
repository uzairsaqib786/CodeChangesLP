// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var MTHub = $.connection.manualTransactionsHub;

var currentMTValues;

$(document).ready(function () {
    $.connection.hub.start().done(function () {
        // tell the modal what transaction it will be saving.  See UserFieldsModal.js in the modals section for variable definition.
        $(document.body).on('click', '.userfields-modal-manual', function () {
            UFTransaction = ManualTransactionID;
        });

        $(document.body).on('click', '#deleteTransaction', function () {
            ordernumber.clearRemoteCache();
            var result = confirm('Click OK to delete the current manual transaction.');
            if (result) {
                MTHub.server.deleteTransaction(ManualTransactionID).done(function () {
                    ManualTransactionID = 0;
                    clearMTInputs();
                });
            };
        });

        // prints a manual transaction label
        $(document.body).on('click', '#PrintMT', function () {
            //reportsHub.server.printMTLabel(ManualTransactionID);
            title = 'Manual Transaction Label';
            getLLPreviewOrPrint('/ManualTransactions/printMTLabel', {
                transactionID: ManualTransactionID
            }, true,'label', title)
        });

        // posts transaction(s) to the open transactions table
        $('#PostDelete, #PostSave').click(function () {
            var $this = $(this);
            var save = $this.attr('id') == 'PostSave' ? true : false;
            var transType = $('#TransactionType').val();
            var transQty = $('#TransQty').val();
            var warehouse = $('#Warehouse');
            var lotNumber = $('#LotNumber').val();
            var serialNumber = $('#SerialNumber').val();

            var valid = validatePicksPuts(lotNumber, serialNumber, transType, warehouse, transQty);
            if (!valid) {
                return false;
            };
            var emergency = $('#Emergency').data('toggles').active;
            if (save) {
                var s = confirm('Click OK to post and save the temporary transaction.');
                if (s) {
                    MTHub.server.postTransaction(ManualTransactionID, false).done(function (success) {
                        $('#SaveTransaction').trigger('click');
                        if (success) {                           
                            var order = $('#OrderNumber');
                            order.removeAttr('disabled');
                            clearMTInputs();
                        } else {
                            alert('An error occured while posting transaction.');
                            populateManualTransaction();
                        };
                    });
                };
            } else {
                var transDelete = confirm('Click OK to post and delete the temporary transaction.');
                if (transDelete) {
                    MTHub.server.postTransaction(ManualTransactionID, transDelete).done(function (success) {
                        if (success) {
                            var order = $('#OrderNumber');
                            order.removeAttr('disabled');
                            order.val('');
                            ordernumber.clearRemoteCache();
                            clearMTInputs();                         
                        };
                    });
                };
            };
        });

        // saves a temporary transaction
        $('#SaveTransaction').click(function () {
            var transType = $('#TransactionType').val();
            var transQty = $('#TransQty').val();
            var warehouse = $('#Warehouse');
            var lotNumber = $('#LotNumber').val();
            var serialNumber = $('#SerialNumber').val();
            var zone = $('#Zone').html();if (zone == '-') {zone = '';};
            var shelf = $('#Shelf').html();if (shelf == '-') {shelf = '';};
            var carousel = $('#Carousel').html(); if (carousel == '-') { carousel = '' };
            var row = $('#Row').html(); if (row == '-') { row = '' };
            var bin = $('#Bin').html(); if (bin == '-') { bin = '' };

            var valid = validatePicksPuts(lotNumber, serialNumber, transType, warehouse, transQty);
            if (!valid) {
                return false;
            };

            var params = [$('#ItemNumber').val(), transType, $('#ExpirationDate').val(),
                $('#Revision').val(), $('#ItemDescription').val(), lotNumber, $('#UnitOfMeasure').val(), $('#Notes').val(), serialNumber,
                $('#RequiredDate').val(), $('#LineNumber').val(), transQty, $('#Priority').val(), $('#LineSequence').val(), $('#HostTransID').val(),
                $('#BatchPickID').val(), $('#Emergency').data('toggles').active, warehouse.val(), $('#ToteID').val(),
                zone, shelf, carousel, row, bin, $('#InvMapID').html()
            ];
            MTHub.server.saveTransaction(params, ManualTransactionID).done(function (success) {
                if (!success) {
                    alert('Another user has made changes to this entry.  The entry will be refreshed with the new data.  Make any necessary changes and save again.');
                }
                populateManualTransaction();
                $('#OrderNumber, .manual-modal').removeAttr('disabled');            
            });
        });

        /*
            Populates the Manual Transactions fields from the database
            ID - selected entry's ID in the database
        */
        populateManualTransaction = function () {
            MTHub.server.getTransactionInfo(ManualTransactionID).done(function (result) {
                console.log(result)
                $('#ItemNumber').val(result[0]["Item Number"]);
                $('#TransactionType').val(result[0]["Transaction Type"]);
                $('#SupplierItemID').val(result[0]["Supplier Item ID"]);
                $('#ExpirationDate').val(result[0]["Expiration Date"]);
                $('#RequiredDate').val(result[0]["Required Date"]);
                
                $('#Revision').val(result[0]["Revision"]);
                $('#ItemDescription').val(result[0]["Description"]);
                $('#LotNumber').val(result[0]["Lot Number"]);
                $('#UnitOfMeasure').val(result[0]["Unit of Measure"]);

                $('#Notes').val(result[0]["Notes"]);
                $('#SerialNumber').val(result[0]["Serial Number"]);
                $('#LineNumber').val(result[0]["Line Number"]);

                $('#TransQty').val(result[0]["Transaction Quantity"]);
                $('#Priority').val(result[0]["Priority"]);
                $('#LineSequence').val(result[0]["Line Sequence"]);
                $('#HostTransID').val(result[0]["Host Transaction ID"]);

                $('#BatchPickID').val(result[0]["Batch Pick ID"]);
                if (result[0]["Emergency"] != undefined && result[0]["Emergency"] != null) {
                        $('#Emergency').data('toggles').toggle(result[0]["Emergency"]);
                } else {
                    $('#Emergency').data('toggles').toggle(false);
                };

                
                $('#Warehouse').val(result[0]["Warehouse"]);
                $('#ToteID').val(result[0]["Tote ID"]);
                $('#Zone').html(result[0]["Zone"]);
                $('#Shelf').html(result[0]["Shelf"]);

                $('#Carousel').html(result[0]["Carousel"]);
                $('#Row').html(result[0]["Row"]);
                $('#Bin').html(result[0]["Bin"]);
                $('#InvMapID').html(result[0]["Inv Map ID"]);
                console.log(result)
                // Warehouse Sensitive field
                result[0]["Warehouse Sensitive"] ? $('#Warehouse').addClass('required') : $('#Warehouse').removeClass('required');
                $('#LocQtyTot').html(result[1]["Total Quantity"] == '' ? '0' : result[1]["Total Quantity"]);
                if (result[2] != undefined) {
                    $('#LocQtyPick').html(result[2]["Quantity Allocated Pick"] == '' ? '0' : result[1]["Quantity Allocated Pick"]);
                    $('#LocQtyPut').html(result[2]["Quantity Allocated Put Away"] == '' ? '0' : result[1]["Quantity Allocated Put Away"]);
                }
                $('.container-fluid input, .userfields-modal-manual, .container-fluid select, #SaveTransaction, #PrintMT, #deleteTransaction, #PostTransaction').removeAttr('disabled');
                $('i.modal-launch-style').removeClass('disabled');
                currentMTValues = result;
            });
        };
    });
});

/*
    validates trans type of picks and puts and takes care of lot number and serial number when they are left blank.
*/
var validatePicksPuts = function (lotNumber, serialNumber, transType, warehouse, transQty) {

    if (lotNumber.trim() == '') {
        lotNumber = 0;
    };

    if (serialNumber.trim() == '') {
        serialNumber = 0;
    };

    if (transType == null || transType == undefined) {
        postMTAlert('<strong>Conflict:</strong> Transaction Type must be specified.');
        return false;
    } else {
        deleteMTAlert();
    };

    if (transType.toLowerCase() == 'pick' || transType.toLowerCase() == 'put away') {
        if (!$.isNumeric(transQty) || transQty <= 0) {
            postMTAlert('<strong>Conflict:</strong> Transaction Quantity must be a positive integer for transaction type ' + transType);
            return false;
        } else if (warehouse.hasClass('required') && warehouse.val() == '') {
            postMTAlert('<strong>Conflict:</strong> Specified Item Number must have a warehouse');
            return false;
        } else {
            deleteMTAlert();
        };
    };
    if (transType.toLowerCase() == 'pick') {
        var totLocQty = $('#LocQtyTot').text();
        var pick = $('#LocQtyPick').text();
        pick = pick == '-' ? 0 : pick;
        var transactionQty = $('#TransQty').val();
        console.log(totLocQty - pick)
        if (transactionQty > totLocQty - pick && $('#InvMapID').html() != 0) {
            var result = confirm('Transaction Quantity is greater than available quantity at the selected location.  Click OK to continue saving.');
            if (!result) {
                return false;
            };
        };
    };
    return true;
};