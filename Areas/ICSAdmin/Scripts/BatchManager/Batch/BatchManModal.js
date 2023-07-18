// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var BatchManModal_batch = ''
var BatchManModal_nextID = ''
var BatchManModal_transType = ''
var BatchManModal_OKClick = false
var BatchManModal_CurrentMessage = ''

function ShowBatchManModal(message, state, yesNo) {
    BatchManModal_CurrentMessage = state
    $('#BatchMan_Message').text(message)
    if(yesNo)
    {
        $('#BatchMan_Close').text("Yes")
        $('#BatchMan_Close').text("No")
    } else {
        $('#BatchMan_Close').text("Ok")
        $('#BatchMan_Close').text("Close")
    }
    $('#BatchManModal').modal('show');
}

$('#BatchManModal').on('shown.bs.modal', function () {
    $('#BatchMan_OK').focus();
})

//Id not Pick to Totes, Modal will be displayed. If ok is clicked, creates the batch
$('#BatchMan_OK').click(function () {
    switch (BatchManModal_CurrentMessage) {
        case "LabelsCheck":
            BatchManModal_OKClick = false
            ServerCreateBatch(BatchManModal_batch, BatchManModal_nextID, BatchManModal_transType)
            break;
        case "Create":
            BatchManModal_OKClick = true
            break;
        case "DeleteAll":
            deleteBatches($('#DeleteBatchDrop').val(), 2, $('#TransType').val());
            getTableData();
            requeryBatchDeleteDrop()
            break;
        case "PrintReport":
            printBatchReport();
            break;
        case "PrintLabels":
            printBatchLabel();
            break;
        default:
            alert(BatchManModal_CurrentMessage)
    }

})
$('#BatchManModal').on('hidden.bs.modal', function () {
    if (BatchManModal_OKClick) {
        var orders = new Array();
        $.each(rightBMTable.data(), function (i, elem) {
            orders.push([elem[0], (i + 1)]);
        });
        createBatch(orders, $('#NextBatchID').val(), $('#TransType').val());
    }
})