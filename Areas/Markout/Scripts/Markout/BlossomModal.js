// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2021

$(document).ready(function () {

    $('#BlossomModal').on('shown.bs.modal', function () {
        $('#BlossToteIDScan').val('').focus();
        $('#SubmitBlossom').attr('disabled', 'disabled');
    });

    $('#BlossToteIDScan').on('focusout', function () {

        if ($('#BlossToteIDScan').val() != "") {
            $.ajax({
                url: "/MO/Menu/ValidTote",
                method: "POST",
                data: {
                    'ToteID': $('#BlossToteIDScan').val()
                },
                success: function (valid) {
                    if (!valid) {
                        $('#SubmitBlossom').attr('disabled', 'disabled');
                        alert('Invalid Tote. This tote already has open transactions assigned to it.');
                        $('#BlossToteIDScan').val('').focus();
                        return false
                    } else {
                        $('#SubmitBlossom').removeAttr('disabled');
                    };
                }
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
            alert('Invalid Quantity Entered');
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
                var blossqty = $(this).find('.BlossQty').val();

                if (blossqty != "") {
                    var OTID = [parseInt(id), parseInt(blossqty)];
                    OTIDs.push(OTID)
                };
            });
        });


        if (type == "Blossom") {
            displayConf("You have requested to blossom. | This will complete the original tote with quantities entered, | and assign the remaining quantities to the new tote. | Do you want to perform this action?", "Blossom", function () {
                $.ajax({
                    url: "/MO/Menu/BlossTote",
                    method: "POST",
                    data: {
                        'OTIDs': OTIDs,
                        'NewTote': $('#BlossToteIDScan').val(),
                        'IsBlossComp': false
                    },
                    success: function (updated) {
                        if (!updated) {
                            alert('There was an error blossoming this tote');
                        } else {
                            var e = jQuery.Event("keypress");
                            e.which = 13;
                            e.keyCode = 13;
                            $("#ToteIDScan").trigger(e);

                            $('#BlossomModal').modal('hide');
                        };
                    }
                });
            });
        } else {
            displayConf("You have requested to blossom complete. | This will complete the new tote with quantities entered, and update the remaining quantities of the old tote. | Do you want to perform this action?", "Blossom Complete", function () {
                $.ajax({
                    url: "/MO/Menu/BlossTote",
                    method: "POST",
                    data: {
                        'OTIDs': OTIDs,
                        'NewTote': $('#BlossToteIDScan').val(),
                        'IsBlossComp': true
                    },
                    success: function (updated) {
                        if (!updated) {
                            alert('There was an error blossoming this tote');
                        } else {
                            var e = jQuery.Event("keypress");
                            e.which = 13;
                            e.keyCode = 13;
                            $("#ToteIDScan").trigger(e);

                            $('#BlossomModal').modal('hide');
                        };
                    }
                });
            });
        };


    });


    $('#BlossomModalDismiss').click(function () {
        $('#BlossomModal').modal('hide');
    });

});