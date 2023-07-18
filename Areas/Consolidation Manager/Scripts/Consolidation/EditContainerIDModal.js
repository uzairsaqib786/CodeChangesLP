// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022


var shipTransHubCont = $.connection.shippingTransactionsHub;
var tableIDContID, tableRefContID, shipIndexContID

//opens the modal and sets the variables. Allows it to be used accross multiple pages
function openContainerIDModal(tableID, tableRef, shipIndex) {
    $('#EditContainerIDModal').modal('show');
    tableIDContID = tableID;
    tableRefContID = tableRef;
    shipIndexContID = shipIndex;
};

$(document).ready(function () {

    $('#EditContainerIDModal').on('shown.bs.modal', function (e) {
        $('#ContainerIDToSet').focus();
    })


    $('#ContainerIDToSet').on('input', function () {
        if ($('#ContainerIDToSet').val() == "") {
            $('#SetContainerIDButt').attr('disabled', 'disabled');
        }
        else {
            if ($('#ContainerIDToSet').val() == "") {
                $('#SetContainerIDButt').attr('disabled', 'disabled');
            }
            else {
                $('#SetContainerIDButt').removeAttr('disabled');
            }
        }
    });

    $('#ClearSetContainerID').click(function () {
        $('#ContainerIDToSet').val('').focus();
        $('#SetContainerIDButt').attr('disabled', 'disabled');
    });


    $('#SetContainerIDButt').click(function () {
        var stid = tableRefContID.row($('#' + tableIDContID + ' tbody tr.active')[0]).data()[0];
        shipTransHub.server.updContainerIdSingleShipTrans(stid, $('#ContainerIDToSet').val()).done(function (mess) {
            if (mess == 'Fail') {
                MessageModel("Error", "Error has occured")
            } else {
                var tr = $('#' + tableIDContID + ' tbody tr.active');
                $('#' + tableIDContID + ' tbody tr.active').find(':nth-child(' + shipIndexContID + ')').html($('#ContainerIDToSet').val());
                tableRefContID.row(tr[0]).invalidate();
                tableRefContID.draw();
                $('#ContainerIDToSet').val('');
            };
        });
    });


});