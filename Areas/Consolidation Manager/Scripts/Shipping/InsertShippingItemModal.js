// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    //enables adn disables the add button 
    $('#InsertShippingItemContID').on('input', function () {
        if ($('#InsertShippingItemContID').val() == "") {
            $('#AddShippingItem').attr('disabled', 'disabled');
        } else {
            $('#AddShippingItem').removeAttr('disabled');
        };
    });
    //adds the shipping item and reloads the page
    $('#AddShippingItem').click(function () {
        shipHub.server.addShippingItem($('#InsertShippingItemOrderNumber').val(), $('#InsertShippingItemContID').val()).done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "An error has occurred");
            } else {
                location.reload();
            };
        });
    });
});