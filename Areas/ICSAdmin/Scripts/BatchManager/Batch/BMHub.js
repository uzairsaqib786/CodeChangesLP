// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var bmHub = $.connection.batchManagerHub;



function getTableData() {
    bmHub.server.getTableData($('#TransType').val()).done(function (dataSet) {
        leftBMTable.clear();
        leftBMTable.rows.add(dataSet).draw();
        rightBMTable.clear().draw();
    });
};

function createBatch(batch, nextID, transType) {
    BatchManModal_batch = batch
    BatchManModal_nextID = nextID
    BatchManModal_transType = transType
    ShowBatchManModal("If any desired labels have not been printed click Close to cancel the batch process.  If all desired labels for these items have been printed click OK.", "LabelsCheck", false);
    return;
   
};




function ServerCreateBatch(batch, nextID, transType) {
    bmHub.server.createBatch(batch, nextID, transType).done(function (settings) {

        if (settings[0] == "createBatchError") {
            MessageModal("Error Processing Batch", "There was an error creating the current batch. Please try again")
        } else {
            console.log('hit me')
            if ($('#PickToTotes').val().toLowerCase() == 'true' && transType.toLowerCase() == 'pick') {
                if ($('#AutoPrintLabels').val().toLowerCase() == 'true') {
                    printBatchLabel();
                };

                showPickModal();
            };
            $('#NextBatchID').val(settings[0]);
            $('#currentPickMode').val(settings[1]);
            $('#PickToTotes').val(settings[2]);
            $('#NextToteID').val(settings[3]);
            $('#AutoPrintLabels').val(settings[4]);
            if (settings[5].trim() == '') {
                settings[5] = 0;
            };
            $('#Append').attr('name', settings[5]);
            getTableData();
            requeryBatchDeleteDrop();
        };
    });
};


function deleteBatches(batchID, ident, transType) {
    if (!batchID) {
        batchID = ""
    };
    bmHub.server.deleteAllBatches(batchID, ident, transType).done(function (success) {
        if (!success) {
            alert("Deleteing the Desired Batch(es) has failed")
        } else {
            getTableData();
            requeryBatchDeleteDrop();
        };
    })
};

function requeryBatchDeleteDrop() {
    bmHub.server.grabBatchDeleteDrop($('#TransType').val()).done(function (batchList) {
        var appendString = '<option value="">' + "" +'</option>'
        for (i = 0; i <= batchList.length -1; i++) {
            appendString += '<option value="' + batchList[i] + '">' + batchList[i] + '</option>'
        };
        $('#DeleteBatchDrop').html(appendString);
    });
};

function updatePickToteIDs(orders) {
    bmHub.server.updatePickToteIDs(orders).done(function (rslt) {
        if (rslt) {
            $('#pick_modal').modal('hide');
        } else {
            MessageModal("Error Assigning Totes", "There was an error assigning totes to the orders. Please try again")
        };
    });
};

function printBatchReport(batch, transType, orders) {
    title = 'Batch Report';
    getLLPreviewOrPrint('/Admin/BatchManager/PrintBatchReport', {
        transType: transType,
        orders: orders,
        BatchID: batch
    }, true,'report', title)
};

function printBatchLabel() {
    var transType = $('#TransType').val(), bid = $('#NextBatchID').val();
    if (transType.trim().toLowerCase() == 'count') { alert('Cannot print labels for count transactions!'); }
    else if (bid.trim() == '') { alert('Batch ID must be specified!'); }
    else {
        var orders = '', data = rightBMTable.data();

        for (var x = 0; x < data.length; x++) {
            orders += data[x][0] + ',';
        };
        //reportsHub.server.printBatchLabel(transType, orders, bid);
        title = 'Batch Labels';
        getLLPreviewOrPrint('/Admin/BatchManager/PrintBatchLabel', {
            transType: transType, 
            orders: orders, 
            BatchID: bid
        }, true,'label', title)
    };
};

function printBatchReport() {
    var transType = $('#TransType').val(), bid = $('#NextBatchID').val();
    if (bid.trim() == '') { alert('Batch ID must be specified!'); }
    else {
        var orders = '', data = rightBMTable.data();

        for (var x = 0; x < data.length; x++) {
            orders += data[x][0] + ',';
        };
        getLLPreviewOrPrint('/Admin/BatchManager/PrintBatchReport', {
            transType: transType,
            orders:orders,
            batchID:bid
        }, true,'report', 'Batch Report');
    };
};

function switchToOS(order, transType) {
    bmHub.server.getDetailView(order, transType).done(function (data) {
        $('#osOrderNumber').val(order);
        $('#osTransType').val(transType);
        osAltTable.clear().rows.add(data).draw();
        $('#mainDiv').hide();
        $('#tableDiv').show();
    });
};