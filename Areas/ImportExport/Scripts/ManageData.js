// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022



$(document).ready(function () {
    //Handles opening the desired export inventory modal 
    $('#ExpInvMapManager').click(function () {
        //if table open up the export option for the inventory map data manager modal
        if ($('[name="ExportInvMapType"]').val() == "Table") {
            $('#InventoryMapTableSection').show();
            $('#InventoryTableSection').hide();
            $('#InvMapDataSelect option[value="export"]').attr('selected', 'selected');
            $('#InvMapDataSelect').trigger('change');
            $('#TableDataManagerModal').modal('show');
            $('#TableDataManagerLabel').html('Inventory Map Tables');
        } else {
            var file = $('[name="InvMapExportFile"]').val();
            //if the file ends in xls or xlsx then open the field map modal for inventory map
            if (file.substr(file.length - 3) == "xls" || file.substr(file.length - 4) == "xlsx") {
                $('#XferFileFieldMapTable option[value="Inventory Map"]').attr('selected', 'selected');
                $('#XferFileFieldMapTable').trigger('change');
                $('#XferFileFieldMapModal').modal('show');
            //open the transfer file field mapping modal for the export inventory map
            } else {
                $('#XferTransField option[value="Inventory Map"]').filter('[data-xfer-type="Export"]').attr('selected', 'selected');
                $('#XferTransField').trigger('change');
                $('#transfer_field_modal').modal('show');
            }
        };
    });
    //Handles opeing up the desired import inventory map modal
    $('#ImpInvMapManager').click(function () {
        //Opens the import option for the inventory map data manager modal
        if ($('[name="ImportInvMapType"]').val() == "Table") {
            $('#InventoryMapTableSection').show();
            $('#InventoryTableSection').hide();
            $('#InvMapDataSelect option[value="import"]').attr('selected', 'selected');
            $('#InvMapDataSelect').trigger('change');
            $('#TableDataManagerModal').modal('show');
            $('#TableDataManagerLabel').html('Inventory Map Tables');
        //Opens the transfer file field mappping modal for the import inventory map
        } else {
            $('#XferFileFieldMapTable option[value="Inventory Map"]').attr('selected', 'selected');
            $('#XferFileFieldMapTable').trigger('change');
            $('#XferFileFieldMapModal').modal('show');
        };
    });
    
    //Handles opening the desired export inventory modal
    $('#ExpInvManager').click(function () {
        //Opens the export option for the inventory data manager modal
        if ($('[name="ExportInvType"]').val() == "Table") {
            $('#InventoryTableSection').show();
            $('#InventoryMapTableSection').hide();
            $('#InvDataSelect option[value="export"]').attr('selected', 'selected');
            $('#InvDataSelect').trigger('change');
            $('#TableDataManagerModal').modal('show');
            $('#TableDataManagerLabel').html('Inventory Tables');
        } else {
            var file = $('[name="InvExportFile"]').val();
            //if the filename ends in xls or xlsx then open the xfer field field map modal for the inventory table
            if (file.substr(file.length - 3) == "xls" || file.substr(file.length - 4) == "xlsx") {
                $('#XferFileFieldMapTable option[value="Inventory"]').attr('selected', 'selected');
                $('#XferFileFieldMapTable').trigger('change');
                $('#XferFileFieldMapModal').modal('show');
            //Opens the xfer transfer file field mapping modal for the export inventory
            } else {
                $('#XferTransField option[value="Inventory"]').filter('[data-xfer-type="Export"]').attr('selected', 'selected');
                $('#XferTransField').trigger('change');
                $('#transfer_field_modal').modal('show');
            };
        };
    });

    //Handles opening the desireed import invenotry modal
    $('#ImpInvManager').click(function () {
        //Opens the import option for the inventory data manager modal
        if ($('[name="ImportInvType"]').val() == "Table") {
            $('#InventoryTableSection').show();
            $('#InventoryMapTableSection').hide();
            $('#InvDataSelect option[value="import"]').attr('selected', 'selected');
            $('#InvDataSelect').trigger('change');
            $('#TableDataManagerModal').modal('show');
            $('#TableDataManagerLabel').html('Inventory Tables');
        } else {
            //Opens the xfer transfer fiele field mapping modal for the import inventory
            $('#XferFileFieldMapTable option[value="Inventory"]').attr('selected', 'selected');
            $('#XferFileFieldMapTable').trigger('change');
            $('#XferFileFieldMapModal').modal('show');
        };
    });

    //Handles exporting the inventory map
    $('#ExpInvMap').click(function () {
        manageDataHub.server.exportInventoryMap($('[name="InvMapExportFile"]').val()).done(function (result) {
            if (!result.success) MessageModal('Error', 'An error occurred while attempting to export the Inventory Map.');
        });
    });

    //Handles importing the inventory map
    $('#ImpInvMap').click(function () {
        MessageModal({
            Title: "Import Inventory Map",
            Message: "Do you want to continue with importing new Inventory Map Records?",
            onSubmitFunction: function () {
                var formData = new FormData();
                formData.append('file', $('#InvMapFileSelect')[0].files[0])
                console
                $.ajax({
                    url: '/IE/Menu/ImportInvMapCSV',  //server script to process data
                    type: 'POST',
                    // Ajax events
                    success: completeHandler = function (data) {
                        if (data == true) {
                            alert("Inventory Map Import has completed")
                        } else {
                            alert("An Error occured during Inventory Map Import, check the event log for more information")
                        }
                    },
                    error: errorHandler = function () {
                        alert("Something went wrong!");
                    },
                    // Form data
                    data: formData,
                    // Options to tell jQuery not to process data or worry about the content-type
                    cache: false,
                    contentType: false,
                    processData: false
                }, 'json');
            }
        })
       
    });

    //Handles exporting the inventory table
    $('#ExpInv').click(function () {
        manageDataHub.server.exportInventory($('[name="InvMapExportFile"]').val()).done(function (result) {
            if (!result.success) MessageModal('Error', 'An error occurred while attempting to export the Inventory.');
        });
    });

    //Handles importing the inventory table
    $('#ImpInv').click(function () {
        
        MessageModal({Title : "Import Inventory",
            Message: "Do you want to continue with importing new Inventory Records?",
            onSubmitFunction: function () {
                var formData = new FormData();
                formData.append('file', $('#InvFileSelect')[0].files[0])
                console
                $.ajax({
                    url: '/IE/Menu/ImportInventoryCSV',  //server script to process data
                    type: 'POST',
                    // Ajax events
                    success: completeHandler = function (data) {
                        if (data == true) {
                            alert("Inventory Import has completed")
                        } else {
                            alert("An Error occured during Inventory Import, check the event log for more information")
                        }
                    },
                    error: errorHandler = function () {
                        alert("An Error occured during Inventory Import, check the event log for more information");
                    },
                    // Form data
                    data: formData,
                    // Options to tell jQuery not to process data or worry about the content-type
                    cache: false,
                    contentType: false,
                    processData: false
                }, 'json');
            }
        })
       
    });
});