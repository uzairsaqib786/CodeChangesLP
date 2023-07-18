// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var CurrItems = ''

    $('#ItemNumberFilterModal').on('shown.bs.modal', function (e) {
        $('#ItemNumbersToFilter').focus();
        CurrItems = $('#ItemNumbersToFilter').val();
    });

    $('#ItemNumberFilterModalDismiss').click(function () {
        $('#ItemNumbersToFilter').val(CurrItems);
    });

    $('#ItemNumberFilterModalSubmit').click(function () {
        //Add to table and redraw table
        var ItemsStr = $('#ItemNumbersToFilter').val().replace(/[\n\r]/g, ',');

        while (ItemsStr.slice(ItemsStr.length - 1) == ',') {
            ItemsStr = ItemsStr.slice(0, -1); //cut off trailing comma
        };

        //if (ItemsStr.slice(ItemsStr.length-1) ==','){
        //    ItemsStr = ItemsStr.slice(0, -1); //cut off trailing comma
        //};

        sysRepHub.server.addFilterItemNums(ItemsStr).done(function (rslt) {
            if (rslt) {
                sysRepNewTable.draw();
                $('#ItemNumberFilterModal').modal('hide');
            } else {
                MessageModal("Error", "An error occured adding these items as a filter. Please try again.")
            };
        });
        
    });

    
});