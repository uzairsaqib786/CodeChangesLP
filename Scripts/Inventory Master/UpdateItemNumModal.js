// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var invMasterHub = $.connection.inventoryMasterHub;

$(document).ready(function(){
    $('#NewItemNumUpdate').on('input', function () {
        if ($('#NewItemNumUpdate').val() == "") {
            $('#SaveNewItemNum').attr('disabled', 'disabled');
        } else {
            $('#SaveNewItemNum').removeAttr('disabled');
        };
    });

    $('#SaveNewItemNum').click(function () {
        $('#DuplicateItemNumRow').hide();
        var conf1 = confirm("Are you sure you wish to change this item number?");
        if (conf1) {
            var conf2 = confirm("Changing this item number will make records with the old item number out of date, and will impact other functions. Still continue?");
            if (conf2) {
                invMasterHub.server.updItemNumber($('#OldItemNumUpdate').val(), $('#NewItemNumUpdate').val()).done(function (mess) {
                    if (mess == "Error") {
                        MessageModal("Error", "An error has occurred updating the item number")
                    } else if (mess == "Exists") {
                        $('#DuplicateItemNumRow').show();
                    } else {
                        $('#DuplicateItemNumRow').hide();
                        $('#UpdateItemNumModal').modal('hide');
                        $('#stockCode').val($('#NewItemNumUpdate').val());
                        $('#stockcodeCopy').val($('#NewItemNumUpdate').val());
                        $('#UpdateItemNum').val($('#NewItemNumUpdate').val());
                        $('#stockCode').trigger('input');
                        $('#stockCode').typeahead('close');
                    };
                });
            };
        };
        
    });
})