// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var tableIDSelect, tableReference, orderQaunt, pickedQuant, shipQuant, tableRefPage;
var shipTransHub = $.connection.shippingTransactionsHub;
$(document).ready(function () {
    //handles enabling and disabling the savebutton
    $('#SplitLineQuant').on('input', function () {

        if (parseInt($('#SplitLineQuant').val()) > parseInt($('#' + tableIDSelect + ' tbody tr.active').find(':nth-child(4)').html())) {
            $('#SplitLineSave').attr('disabled', 'disabled');
        } else if ($('#SplitLineQuant').val() == '') {
            $('#SplitLineSave').attr('disabled', 'disabled');
        } else {
            $('#SplitLineSave').removeAttr('disabled');
        };
    });
    //handles saving the new split transaction and redrawing the table
    $('#SplitLineSave').click(function () {
        shipTransHub.server.splitLineTrans($('#SplitLineQuant').val(), tableReference.row($('#' + tableIDSelect + ' tbody tr.active')[0]).data()[0], tableRefPage).done(function (mess) {
            if (mess[0]=="Error") {
                MessageModal("Error", "Error has occured");
            } else {
                var tr = $('#' + tableIDSelect + ' tbody tr.active');
                var orderQ = parseInt(tableReference.row(tr[0]).data()[parseInt(orderQaunt)]) - parseInt($('#SplitLineQuant').val());
                var pickedQ = parseInt(tableReference.row(tr[0]).data()[parseInt(pickedQuant)]) - parseInt($('#SplitLineQuant').val());
                var shipQ = parseInt(tableReference.row(tr[0]).data()[parseInt(shipQuant)]) - parseInt($('#SplitLineQuant').val());
                $('#' + tableIDSelect + ' tbody tr.active').find(':nth-child(' + orderQaunt + ')').html(orderQ);
                $('#' + tableIDSelect + ' tbody tr.active').find(':nth-child(' + pickedQuant + ')').html(pickedQ);
                $('#' + tableIDSelect + ' tbody tr.active').find(':nth-child(' + shipQuant + ')').html(shipQ);
                tableReference.row(tr[0]).invalidate();
                tableReference.draw();
                tableReference.row.add(mess).draw();
            };
        });
    });

    $('#SplitLineModal').on('hidden.bs.modal', function () {
        $('#SplitLineModal').trigger('custom_close');
        $('#SplitLineModal').off('custom_close');
    });

});
//open the modal and sets the desired page values as this modal appear son multiple pages
function openSplitLine(tableID, tableRef, orderIndex, pickedIndex, shipIndex, page, OnCloseFunction) {
    $('#SplitLineModal').modal('show');
    tableIDSelect = tableID;
    tableReference = tableRef;
    orderQaunt = orderIndex;
    pickedQuant = pickedIndex;
    shipQuant = shipIndex;
    tableRefPage = page;

    if (OnCloseFunction != undefined) {
        $('#SplitLineModal').on('custom_close', OnCloseFunction);
    };

};