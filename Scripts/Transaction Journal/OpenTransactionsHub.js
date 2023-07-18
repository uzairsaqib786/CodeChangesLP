// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*****************************************************************************
    Open Transactions Hub
*****************************************************************************/
// Reference the auto-generated proxy for the hub. 
var OpenTransactionsConnection = $.connection.openTransactionsHub;

// connection established in global hub
$(document).ready(function () {
    $(document.body).on('click', '#cycleCountReport', function () {
        if (confirm('Click OK to print a cycle count report.')) {
            //reportsHub.server.printCycleCountReport();
            title = 'Cycle Count Report';
            getLLPreviewOrPrint('/Transactions/printCycleCountReport', {
            }, true,'report', title)
        };
    });

    // handles delete button for individual entry (Selected ONLY)
    $('#deleteSelected').click(function () {
        // if no selected row don't do anything, else confirm deletion
        if (openTable.cell('.active', $('#transactionType').val()).data() == null ||
                openTable.cell('.active', $('#orderNumber').val()).data() == null ||
                openTable.cell('.active', $('#transID').val()).data() == null) {
            alert("No selected row to delete!");
        } else if (confirm("Delete entry with ID: " + openTable.cell('.active', $('#transID').val()).data() + "?")) {
            $('#selectedOrderNumber, #selectedTransType').val("");
            var transType = openTable.cell('.active', $('#transactionType').val()).data();
            var orderNumber = openTable.cell('.active', $('#orderNumber').val()).data();
            var transID = openTable.cell('.active', $('#transID').val()).data();
            var itemNumber = openTable.cell('.active', $('#itemNumber').val()).data();
            var lineNumber = openTable.cell('.active', $('#lineNumber').val()).data();
            OpenTransactionsConnection.server.deleteOrder(transType, orderNumber, transID, itemNumber, lineNumber).done(function () {
                openTable.draw();
            });
        };
    });
    // delete button for ALL by Transaction Type
    $('#deleteByTransType').click(function () {
        if (openTable.cell('.active', $('#transactionType').val()).data() == null ||
                openTable.cell('.active', $('#orderNumber').val()).data() == null ||
                openTable.cell('.active', $('#transID').val()).data() == null) {
            alert("No selected order to delete by!");
        } else if (confirm("Delete order: " + openTable.cell('.active', $('#orderNumber').val()).data() + " where transaction type is: " + openTable.cell('.active', $('#transactionType').val()).data() + "?")) {
            $('#selectedOrderNumber, #selectedTransType').val("");
            var transType = openTable.cell('.active', $('#transactionType').val()).data();
            var orderNumber = openTable.cell('.active', $('#orderNumber').val()).data();

            var itemNumber = openTable.cell('.active', $('#itemNumber').val()).data();
            var lineNumber = openTable.cell('.active', $('#lineNumber').val()).data();
            // pass -1 as ID to ensure deletion of order number with trans type, not just single entry
            OpenTransactionsConnection.server.deleteOrder(transType, orderNumber, -1, '', '').done(function () {
                openTable.draw();
            });
        };
    });
    // send completed to history button handler (completed dates in OT get moved to TH).  Refreshes the table upon completion
    $('#sendCompletedToHist').click(function () {
        if (confirm("Send all completed transactions in Open Transactions to history?")) {
            $('#selectedOrderNumber, #selectedTransType').val("");
            OpenTransactionsConnection.server.sendCompletedToTH().done(function (numSent) {
                openTable.draw();
                if (numSent > 0) { alert(numSent + " completed transactions moved to history."); }
                else { alert("No completed transactions in Open Transactions.  No transactions moved to history."); };
            });
        };
    });
});