// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#TransType').change(function () {
        requeryBatchDeleteDrop();
        getTableData();
    });

    $('#Append').click(function () {
        var rows, $this = $(this), data = leftBMTable.data();
        var max = parseInt($this.attr('name')) - rightBMTable.data().length;
        if (max > data.length) {
            max = data.length;
        };

        for (var x = 0; x < max; x++) {
            var y = leftBMTable.rows()[0][0];
            var RowData = leftBMTable.row(y).data();
            rightBMTable.row.add(RowData).draw();
            leftBMTable.row(y).remove().draw(false);
        };
        
    });

    $('#RemoveAll').click(function () {
        leftBMTable.rows.add(rightBMTable.data()).draw();
        rightBMTable.clear().draw();
    });

    $('#AutoBatch').click(function () {
        var clicks = '';
        $.each(leftBMTable.cells(undefined, 3).data(), function (index, elem) {
            if (elem.toLowerCase() == 'true') {
                clicks += '#leftBMTable tbody tr:nth-child(' + (index + 1) + '),';
            };
        });
        $(clicks.substring(0, clicks.length - 1)).click();
    });

    $('#leftBMTable').on('click', '.detail', function () {
        if ($('#seeOrderStatus').val().trim().toLowerCase() == 'true') {
            window.location.href = '/Transactions?viewToShow=1&OrderStatusOrder=' + $(this).attr('name') + "&App=Admin";
        } else {
            switchToOS($(this).attr('name'), $('#TransType').val());
        };
    });

    $('#Create').click(function () {
        var data = rightBMTable.data(), nextBID = $('#NextBatchID');
        if (data.length == 0) {
            alert('No orders selected');
        } else if (nextBID.val().trim() == '') {
            alert('Batch ID must be specified!');
            $('#NextBatchID').focus();
        } else {
            ShowBatchManModal("Click OK to create a new batch order for the selected orders.","Create",false);
        };
    });

    $('#DeleteSelBatch').click(function () {
        if ($('#DeleteBatchDrop').val() != "") {
        $('#batchmanDelete_modal').modal('show');
        };
    });

    $('#batchmanDelete_Tote').click(function () {
        deleteBatches($('#DeleteBatchDrop').val(), 0, $('#TransType').val());
    });

    $('#batchmanDelete_NoTote').click(function () {
        deleteBatches($('#DeleteBatchDrop').val(), 1, $('#TransType').val());
    });

    $('#DeleteAll').click(function () {
        ShowBatchManModal("Are you sure you want to delete all batches with a transaction type of " + $('#TransType').val() + "?","DeleteAll", true)
    });

    $('#osBack').click(function () {
        $('#tableDiv').hide();
        $('#mainDiv').show();
    });

    $(document.body).on('click', '#PrintReport', function () {
        ShowBatchManModal("Click Ok to print a Batch Report for the selected orders","PrintReport",false)
    });

    $(document.body).on('click', '#PrintLabels', function () {
        ShowBatchManModal("Click Ok to print item labels for the selected batch orders", "PrintLabels", false)
    });

    $('#BMOrderScan').on("keypress", function (e) {
        if (e.which === 13) {
            var Found = false;

            leftBMTable.rows().eq(0).each(function (x) {
                if (Found || leftBMTable.data().length == 0) {
                    return false;
                };

                if (leftBMTable.row(x).data()[0].toLowerCase() == $('#BMOrderScan').val().toLowerCase()) {

                    var RowData = leftBMTable.row(x).data();
                    leftBMTable.row(x).remove().draw(false);
                    rightBMTable.row.add(RowData).draw();

                    Found = true;
                    return false;
                };
            });

            $('#BMOrderScan').val('');

            if (!Found) {
                MessageModal("Order Not Found", "The entered order was not found within the order selection list table display.")
            };

        };
    });

});