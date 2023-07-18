// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var FromPageTable;
var FromPage;
$(document).ready(function () {
    // our radio buttons changed so either we changed batch/tote or clear/deallocate, update the string
    $('input[type="radio"]').change(function () {
        $('#DelCmd').val(mkDelCmdStr());
    });

    // confirm deletion and then deallocate/clear the batch/tote with the specified parameters
    $('#ExecuteDelete').click(function () {
        if (confirm('Click OK to ' + $('#DelCmd').val() + '.')) {
            var closer = this.id;
            batchHub.server.deleteToteOrBatch($('#clearBatch').is(':checked'), $('#DelBatchID').val(), $('#DelToteID').val(), $('#DelTransType').val(),
                $('[name="ClearType"]').val() == 'D', FromPage).done(function (deleted) {
                    if (!deleted) {
                        MessageModal('Error', 'An error occurred during the deletion process.');
                    } else {
                        closeModal(closer);
                    };
            });
        };
    });

    // clear and deallocate all batches in the current workstation
    $('#DelAll').click(function () {
        if (confirm('Click OK to clear and deallocate all batches.')) {
            var closer = this.id;
            batchHub.server.deleteAllBatches().done(function (deleted) {
                if (!deleted) {
                    MessageModal('Error', 'An error occurred during deletion of batches');
                } else {
                    closeModal(closer);
                };
            });
        };
    });

    $('#batch_dismiss').click(function () {
        closeModal(this.id);
    });

    function closeModal(closer) {
        var closeCallback = $('#batch_modal').data('close-callback');
        if (typeof closeCallback == 'function') {
            closeCallback(closer);
            $('#batch_modal').data('close-callback', null);
        };
        $('#batch_modal').modal('hide');
        FromPageTable.draw();
    };
});

// launch the modal with the proper context
function DeleteBatchModal(batchID, toteID, transType, tableid, frompage, closeCallback) {
    FromPageTable = tableid;
    FromPage = frompage;
    if (batchID.trim() != '' || toteID.trim() != '') {
        if (toteID.trim() == '') {
            $('#clearTote').attr('disabled', 'disabled');
        } else {
            $('#clearTote').removeAttr('disabled');
            if (batchID.trim() == '') {
                $('#clearTote').click();
            };
        };
        if (batchID.trim() == '') {
            $('#clearBatch').attr('disabled', 'disabled');
        } else {
            $('#clearBatch').removeAttr('disabled').click();
        };
        if (batchID.trim() == '') { batchID = 'No Batch Selected' };
        if (toteID.trim() == '') { toteID = 'No Tote Selected' };
        $('#DelBatchID').val(batchID);
        $('#DelToteID').val(toteID);
        $('#DelTransType').val(transType);
        $('#DelCmd').val(mkDelCmdStr);
        $('#batch_modal').modal('show');
        if (typeof closeCallback == 'function') {
            $('#batch_modal').data('close-callback', closeCallback);
        };
    };
};

// make the string message that we will show the user concerning what will happen when they click Clear/DeAllocate
function mkDelCmdStr() {
    var delCmdStr = $('input[name="ClearType"]:checked').val() == 'D' ? 'Clear & DeAllocate ' : 'Clear ';
    delCmdStr += $('input[name="BatchOrTote"]:checked').val() == 'B' ? 'Batch ' + $('#DelBatchID').val() : 'Tote ' + $('#DelToteID').val();
    return delCmdStr;
};