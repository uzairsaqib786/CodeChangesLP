// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var shipTransHub = $.connection.shippingTransactionsHub;
var tableIDQuant, tableRefQuant, shipIndexQuant
$(document).ready(function () {
    //Forces user to give reason for adjustment
    $('#AdjustReason').change(function () {
        if ($('#AdjustShipQuant').val() == "") {
            $('#AdjustShipQuantSave').attr('disabled', 'disabled');
        }
        else {
            if ($('#AdjustReason').val() == "") {
                $('#AdjustShipQuantSave').attr('disabled', 'disabled');
            }
            else {
                $('#AdjustShipQuantSave').removeAttr('disabled');
            }
        }
    });

    $('#AdjustReason').on('input', function () {
        if ($('#AdjustShipQuant').val() == "") {
            $('#AdjustShipQuantSave').attr('disabled', 'disabled');
        }
        else {
            if ($('#AdjustReason').val() == "") {
                $('#AdjustShipQuantSave').attr('disabled', 'disabled');
            }
            else {
                $('#AdjustShipQuantSave').removeAttr('disabled');
            }
        }
    });

    $('#ClearReason').click(function () {
        $('#AdjustReason').val('').focus();
        $('#AdjustShipQuantSave').attr('disabled', 'disabled');
    });

    //input event 
    $('#AdjustShipQuant').on('input', function () {
        if ($('#AdjustShipQuant').val() == "") {
            $('#AdjustShipQuantSave').attr('disabled', 'disabled');
        }
        else{
            if ($('#AdjustReason').val() == "") {
                $('#AdjustShipQuantSave').attr('disabled', 'disabled');
            }
            else{
                $('#AdjustShipQuantSave').removeAttr('disabled');
            }
        }
    });
    //saves the new quantity
    $('#AdjustShipQuantSave').click(function () {
        var stid = tableRefQuant.row($('#' + tableIDQuant + ' tbody tr.active')[0]).data()[0];
        shipTransHub.server.updShipQTYShipTrans(stid, $('#AdjustShipQuant').val(), $('#AdjustReason').val()).done(function (mess) {
            if (mess == 'Fail') {
                MessageModel("Error", "Error has occured")
            } else {

                var x = document.getElementById("Reasons");
                var Exists = false;
                for (var i = 0; i < x.options.length; i++) {
                    if (x.options[i].value == $('#AdjustReason').val()) {
                        Exists = true;
                        break;
                    };
                };

                if (!Exists) {
                    $('#Reasons').append('<option value="' + $('#AdjustReason').val() + '">')
                };

                var tr = $('#' + tableIDQuant + ' tbody tr.active');
                var quant = parseInt($('#AdjustShipQuant').val());
                $('#AdjustShipQuant, #AdjustReason').val('');
                $('#' + tableIDQuant + ' tbody tr.active').find(':nth-child(' + shipIndexQuant + ')').html(quant);
                tableRefQuant.row(tr[0]).invalidate();
                tableRefQuant.draw();
            };
        });
    });

    $('#AdjustShipQuantModal').on('hidden.bs.modal', function () {
        $('#AdjustShipQuantModal').trigger('custom_close');
        $('#AdjustShipQuantModal').off('custom_close');
    });

});
//opens the modal and sets the variables. Allows it to be used accross multiple pages
function openAdjustQuant(tableID, tableRef, shipIndex, OnCloseFunction) {
    $('#AdjustShipQuantModal').modal('show');
    tableIDQuant = tableID;
    tableRefQuant = tableRef;
    shipIndexQuant = shipIndex;

    if (OnCloseFunction != undefined) {
        $('#AdjustShipQuantModal').on('custom_close', OnCloseFunction);
    };
};