// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var shipHub= $.connection.shippingHub

$(document).ready(function () {
    //save click check class
    //updates the cube field whenever the any of the measurement field change
    $(document.body).on('input', '.height, .length, .width', function () {
        updateCubeField($(this).prop('name'));
    });
    //handels deleting based on name of delete button
    $(document.body).on('click', '.delete-item', function () {
        var id = $(this).prop('name');
        var orderNum = $('#ShippingOrderNumber').val();
        var contId = $('#' + id + '_ContID').val();
        var carrier = $('#' + id + '_Carrier').val();
        var trackNum = $('#' + id + '_TrackNum').val();
        shipHub.server.deleteShipmentItem(id, orderNum, contId, carrier, trackNum).done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "An error has occurred");
            } else {
                location.reload();
            }
        });
    });
    //handels saving based on the name of save button
    $(document.body).on('click', '.save-item', function () {
        var id = $(this).prop('name');
        var carrier = $('#' + id + '_Carrier').val();
        var trackNum = $('#' + id + '_TrackNum').val();
        var freight = $('#' + id + '_Freight').val();
        var freight1 = $('#' + id + '_Freight1').val();
        var freight2 = $('#' + id + '_Freight2').val();
        var weight = $('#' + id + '_Weight').val();
        var length = $('#' + id + '_Length').val();
        var width = $('#' + id + '_Width').val();
        var height = $('#' + id + '_Height').val();
        var cube = $('#' + id + '_Cube').val();
        shipHub.server.updateShipmentItem(id, carrier, trackNum, freight, freight1, freight2, weight, length, width,
                                          height, cube).done(function (mess) {
                                              if (mess == "Fail") {
                                                  MessageModal("Error", "An error has occurred");
                                              };
                                          });
    });
    //opens the add new modal
    $('#ShippingAddNew').click(function () {
        $('#InsertShippingItemModal').modal('show');
        $('#InsertShippingItemOrderNumber').val($('#ShippingOrderNumber').val());
    });

    $(document.body).on('click', '#ShippingPrintAll', function () {
        var orderNum = $('#ShippingOrderNumber').val();
        getLLPreviewOrPrint('/CM/Shipping/PrintShipOrderPL', {
            OrderNum: orderNum
        }, true,'report', 'CM OrderPL');
    });

    $(document.body).on('click', '.print-list', function () {
        var orderNum = $('#ShippingOrderNumber').val();
        var id = $(this).prop('name');
        var contId = $('#' + id + '_ContID').val();
        getLLPreviewOrPrint('/CM/Shipping/PrintShipContPL', {
            OrderNum: orderNum, 
            ContId: contId
        }, true,'report', 'CM ContPL');
    })
    //will complete the shippment fo the order number
    $('#ShippingCompShip').click(function () {
        var conf = confirm("Are you sure you wish to complete this shipment?");
        if (conf) {
            shipHub.server.selOTTempCount($('#ShippingOrderNumber').val()).done(function (count) {
                if (count == -1) {
                    MessageModal("Error", "An error has occurred");
                } else if (count == 0) {
                    //call function to complete shipment
                    shipHub.server.completeShipment($('#ShippingOrderNumber').val()).done(function (mess) {
                        if (mess == "Fail") {
                            MessageModal("Tote ID Update", "An error has occurred");
                        };
                    });
                } else {
                    //for temp
                    var otherconf = confirm("Back Orders exist for this order number. Still complete shipment?");
                    if (otherconf) {
                        shipHub.server.completeShipment($('#ShippingOrderNumber').val()).done(function (mess) {
                            if (mess == "Fail") {
                                MessageModal("Tote ID Update", "An error has occurred");
                            };
                        });
                    };
                };
            });
        };
    });

});
//sets the cube field
function updateCubeField(id) {
    $('#' + id + '_Cube').val(parseFloat((parseFloat($('#' + id + '_Length').val()) * parseFloat($('#' + id + '_Width').val()) * parseFloat($('#' + id + '_Height').val()))) / 1728);
}