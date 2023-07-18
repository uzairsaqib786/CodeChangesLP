// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OMHub = $.connection.orderManagerHub;

$(document).ready(function () {
    //for default user fields button
    $('#CreateOrderSave').click(function () {
        var UF1 = $('#UserField1TXT').val()
        var UF2 = $('#UserField2TXT').val()
        var UF3 = $('#UserField3TXT').val()
        var UF4 = $('#UserField4TXT').val()
        var UF5 = $('#UserField5TXT').val()
        var UF6 = $('#UserField6TXT').val()
        var UF7 = $('#UserField7TXT').val()
        var UF8 = $('#UserField8TXT').val()
        var UF9 = $('#UserField9TXT').val()
        var UF10 = $('#UserField10TXT').val()
        //calls hub function for updating
        OMHub.server.updUserFieldData(UF1, UF2, UF3, UF4, UF5, UF6, UF7, UF8, UF9, UF10).done(function (mess) {
            if (mess == 'Error') {
                MessageModal("Error", "An error has occured while updating the user fields")
            }
        });

    });
});