// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*
    Stores the server's version of the selected transaction.  When saving this is compared against the database to see if
        another user has edited the particular transaction in the mean time.  If a user has then the data is reloaded before
        a save event is allowed to occur
*/
var reprocessTransactionValues;

$(document).ready(function () {
    var currentTransactionID;
    // Keeps serial number and lot number = 0 if there is no input
    $('#modal_sn, #modal_lot').on('focusout', function () {
        if (this.value == null || this.value == '') {
            this.value = 0;
        };
    });

    $('#modal_transqty, #modal_linenum, #modal_lineseq, #modal_priority').on('input', function () {
        setNumericInRange($(this), 0, SqlLimits.numerics.int.max);
    });

    // checks number inputs.
    //Commented out to only verify when submitting
    //$('#modal_transqty, #modal_linenum, #modal_lineseq, #modal_priority').on('focusout', function () {
    //    verifyNumbers();
    //});

    // launches the edit transaction modal
    $('#editTransaction').click(function () {
        //if ($('#hist').prop('checked')) {
        //    currentTransactionID = $('#reprocTransTable tr.active td:first').attr('name');
        //} else {
        //    currentTransactionID = $('tr.active input[data-id]').data('id');
        //};

        //Get ID from corresponding td name attribute
        currentTransactionID = $('#reprocTransTable tr.active td[name]').attr('name');


        getReprocessTransaction(currentTransactionID);
        $('#reprocess_modal').modal('show');
    });

    // handles attempting to save a transaction.  If the save is successful the modal is hidden, otherwise a warning is posted at the top of the modal
    $('#reprocess_submit').click(function () {
        if (!verifyNumbers()) {
            return false;
        } else {
            var currentValues = [
            $('#modal_transqty').val(),
            $('#modal_uom').val(),
            $('#modal_sn').val(),
            $('#modal_lot').val(),
            $('#modal_expdate').val(),
            $('#modal_revision').val(),
            $('#modal_reprocessnotes').val(),
            $('#modal_uf1').val(),
            $('#modal_uf2').val(),
            $('#modal_htid').val(),
            $('#modal_reqdate').val(),
            $('#modal_batch').val(),
            $('#modal_linenum').val(),
            $('#modal_lineseq').val(),
            $('#modal_priority').val(),
            $('#modal_label').prop('checked') ? 1 : 0,
            $('#modal_emergency').prop('checked') ? 1 : 0,
            $('#modal_warehouse').val()
            ];
            RPHub.server.saveTransaction(currentTransactionID, reprocessTransactionValues, currentValues).done(function (conflict) {
                if (conflict) {
                    getReprocessTransaction(currentTransactionID);
                    alert('Your changes have not been saved.  Another user made changes to this entry.  The data will be refreshed to reflect changes.  Make any necessary changes and resave.');
                } else {
                    $('#reprocess_modal').modal('hide');
                    openTransTempTable.draw();
                };
            });
        };
    });
});

/*
    Gets a particular transactions data by ID to edit in the modal.  Stores server values of the data in reprocessTransactionValues.
*/
function getReprocessTransaction(transID) {
    RPHub.server.getTransaction(transID, $('#hist').prop('checked')).done(function (transaction) {
        reprocessTransactionValues = transaction;

        $('#modal_transqty').val(transaction[0]);
        $('#modal_uom').val(transaction[1]);
        $('#modal_sn').val(transaction[2]);
        $('#modal_lot').val(transaction[3]);
        $('#modal_expdate').val(transaction[4]);
        $('#modal_revision').val(transaction[5]);
        $('#modal_reprocessnotes').val(transaction[6]);
        $('#modal_uf1').val(transaction[7]);
        $('#modal_uf2').val(transaction[8]);
        $('#modal_htid').val(transaction[9]);
        $('#modal_reqdate').val(transaction[10]);
        $('#modal_batch').val(transaction[11]);
        $('#modal_linenum').val(transaction[12]);
        $('#modal_lineseq').val(transaction[13]);
        $('#modal_priority').val(transaction[14]);
        $('#modal_label').prop('checked', transaction[15]);
        $('#modal_emergency').prop('checked', transaction[16]);
        $('#modal_warehouse').val(transaction[17]);

        $('#modal_order').val(transaction[18]);
        $('#modal_item').val(transaction[19]);
        $('#modal_transtype').val(transaction[20]);
        $('#modal_dt').val(transaction[21]);
        $('#modal_user').val(transaction[22]);
        $('#modal_zone').val(transaction[23]);
        $('#modal_carousel').val(transaction[24]);
        $('#modal_row').val(transaction[25]);
        $('#modal_shelf').val(transaction[26]);
        $('#modal_bin').val(transaction[27]);
        $('#modal_reason').val(transaction[28]);
        $('#modal_reasonmessage').val(transaction[29]);
        $('#modal_description').val(transaction[30]);
    });
};

/*
    Verifies that input in the modal was correct or valid for number fields
*/
function verifyNumbers() {
    var $this;
    var valid = true;
    $('#reprocess_alerts').html('');
    $.each($('#modal_transqty, #modal_linenum, #modal_lineseq, #modal_priority'), function (index, value) {
        $this = $(this);
        if (!$.isNumeric($this.val())) {
            $('#reprocess_alerts').html('<div class="alert alert-warning alert-custom" role="alert">Field must be a number.</div>');
            $this.focus();
            valid = false;
            return false; //break out of each
        } else if ($this.attr('id') == 'modal_transqty' && (!$.isNumeric(this.value) || this.value < 0)) {
            $('#reprocess_alerts').html('<div class="alert alert-warning alert-custom" role="alert">Field must be a non-negative number.</div>');
            $this.focus();
            valid = false;
            return false; //break out of each
        };
    });
    return valid;
};