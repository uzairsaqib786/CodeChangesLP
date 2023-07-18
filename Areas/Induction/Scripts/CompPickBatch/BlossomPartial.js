// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    $('#BlossomModal').on('shown.bs.modal', function () {
        $('#BlossToteIDScan').val('').focus();
        $('#SubmitBlossom').attr('disabled', 'disabled');
    });

    $('#BlossToteIDScan').on('focusout', function () {
        if ($('#BlossToteIDScan').val() != "") {
            CompPickBatchHub.server.validateToteID($('#BlossToteIDScan').val()).done(function (valid) {
                if (!valid) {
                    $('#SubmitBlossom').attr('disabled', 'disabled');
                    MessageModal("Invalid Tote", 'This tote is currently assigned to another open order');
                    $('#BlossToteIDScan').val('').focus();
                    return false
                } else {
                    $('#SubmitBlossom').removeAttr('disabled');
                };
            });
        } else {
            $('#SubmitBlossom').attr('disabled', 'disabled');
        };
    });

    $(document.body).on('focusout', '.BlossQty', function () {
        var $tr = $(this).parents('tr');
        var transQty = parseInt($tr.find('td[name="TransQty"]').text());
        var newqty = $(this).val();

        if (newqty != "" && (newqty < 0 || newqty > transQty)) {
            $(this).val('').focus();
            MessageModal("Invalid Quantity Entered", 'Invalid Quantity Entered');
        };
    });

    $('#SubmitBlossom').click(function () {
        var type = $('#BlossomModalTitle').text();
        var OTIDs = [];
        var BlossTbl = $('#blossomdata');

        BlossTbl.each(function () {
            //each qty column
            $(this).find('td[name="BlossQty"]').each(function () {
                var $tr = $(this).parents('tr');
                var id = $tr.data('id');
                var transQty = parseInt($tr.find('td[name="TransQty"]').text());
                var blossqty = $(this).find('.BlossQty').val();

                if (blossqty != "") {
                    var OTID = [parseInt(id), transQty, parseInt(blossqty)];
                    OTIDs.push(OTID);
                } else {
                    var OTID = [parseInt(id), transQty, 0];
                    OTIDs.push(OTID);
                };
            });
        });

        MessageModal("Perform Blossom?", "Perform a blossom wiht the current setup? This will complete the original tote with the quantities entered, and assign any remaining quantities to the new tote.",
            undefined, undefined,
            function () {
                CompPickBatchHub.server.blossomTote(OTIDs, $('#BlossToteIDScan').val()).done(function (success) {
                    if (success) {
                        //after split is performed: close modal and redraw table
                        $('#ToteID').val($('#BlossToteIDScan').val());
                        PickBatchTransTable.clear().draw();
                        $('#BlossomModal').modal('hide');
                    } else {
                        MessageModal("Error", "An error occured when blossoming this tote");
                    };
                });
            }
        );


     });

});