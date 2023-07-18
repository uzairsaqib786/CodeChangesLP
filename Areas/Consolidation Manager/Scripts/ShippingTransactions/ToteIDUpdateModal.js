// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    //disables or enables the update button
    $('#ContainerID').on('input', function () {
        if ($('#ContainerID').val() == '') {
            $('#ToteIDUpdateButt').attr('disabled', 'disabled');
        } else {
            $('#ToteIDUpdateButt').removeAttr('disabled');
        };
    });
    //updtes the tote id and wo;; redraw the table to show the desired change
    $('#ToteIDUpdateButt').click(function () {
        shipTransHub.server.updContIDShipTrans($('#UpdateToteID').val(), $('#UpdateOrderNumber').val(), $('#ContainerID').val()).done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Tote ID Update", "An error has occurred");
            } else {
                for (var x = 0; x < ShipTransTable.rows().data().length; x++) {
                    var tabTote = ShipTransTable.row(x).data()[3];
                    if (tabTote == $('#UpdateToteID').val()) {
                        var data = ShipTransTable.row(x).data();
                        data[6] = $('#ContainerID').val();
                        ShipTransTable.row(x).data(data).draw();
                    };
                };
            };
        });
    });
});