// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var manageDataHub = $.connection.manageDataHub;
var ManageDataInvMapTable;
var ManageDataInvMapFilterMen = "";
var ManageDataInvMapCols = [];
var ManageDataInvTable;
var ManageDataInvFilterMen = "";
var ManageDataInvCols = [];
$(document).ready(function () {
    //Inventory Map section
    //Datatable declaration
    ManageDataInvMapTable = $('#InvMapDataManTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'columnDefs': [
            {

            }
        ],
        "ajax": {
            "url": "/IE/Menu/getManageDataInvMapTable",
            "data": function (d) {
                d.filter = (ManageDataInvMapFilterMen == "" ? "" : ManageDataInvMapFilterMen.getFilterString());
                d.location = $('#InvMapDataSelect').val();
            }

        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "paging": true
    });
    //handles horizontal scroll bar
    $('#InvMapDataManTable').wrap('<div style="overflow-x:scroll;"></div>');
    //Adds the columns to a list for the filter
    $.each($('#InvMapDataManCols').children(), function (index, element) {
        ManageDataInvMapCols.push($(element).attr('value'));
    });
    //Initialize the inventory mpa filter
    ManageDataInvMapFilterMen = new FilterMenuTable({
        Selector: '#InvMapDataManTable',
        columnIndexes: ManageDataInvMapCols,
        dataTable: ManageDataInvMapTable,
        columnMap: function () {
            var colMap = [];
            colMap["Inv Map ID"] = "Number"
            colMap["Transaction Type"] = "Text"
            colMap["Location ID"] = "Number"
            colMap["Warehouse"] = "Text"
            colMap["Location Number"] = "Text"
            colMap["Cell Size"] = "Text"
            colMap["Golden Zone"] = "Text"
            colMap["Zone"] = "Text"
            colMap["Carousel"] = "Text"
            colMap["Row"] = "Text"
            colMap["Shelf"] = "Text"
            colMap["Bin"] = "Text"
            colMap["Item Number"] = "Text"
            colMap["Description"] = "Text"
            colMap["Item Quantity"] = "Number"
            colMap["Unit of Measure"] = "Text"
            colMap["Put Away Date"] = "Date"
            colMap["Maximum Quantity"] = "Number"
            colMap["Revision"] = "Text"
            colMap["Serial Number"] = "Text"
            colMap["Lot Number"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["User Field1"] = "Text"
            colMap["User Field2"] = "Text"
            colMap["Date Sensitive"] = "Bool"
            colMap["Dedicated"] = "Bool"
            return colMap
        }()
    });
    //On filter change event redraw the table
    $('#InvMapDataManTable').on('filterChange', function () {
        ManageDataInvMapTable.draw();
    });

    //When desired table is changed get new data from the selected table
    $('#InvMapDataSelect').change(function () {
        var $this = $(this);
        //current inventory mpa
        if ($this.val() == "current") {
            $('#InvMapDelete').attr('disabled', 'disabled');
            $('#GetInvMapLocs').attr('disabled', 'disabled');
            $('#ImportInvMapLocs').attr('disabled', 'disabled');
            ManageDataInvMapTable.draw();
        //import inventory map
        } else if ($this.val() == "import") {
            $('#InvMapDelete').removeAttr('disabled');
            $('#GetInvMapLocs').attr('disabled', 'disabled');
            $('#ImportInvMapLocs').removeAttr('disabled');
            ManageDataInvMapTable.draw();
        //export inventory map
        } else if ($this.val() == "export") {
            $('#InvMapDelete').removeAttr('disabled');
            $('#GetInvMapLocs').removeAttr('disabled');
            $('#ImportInvMapLocs').attr('disabled', 'disabled');
            ManageDataInvMapTable.draw();
        } else {
            //archive and audit
            $('#InvMapDelete').removeAttr('disabled');
            $('#GetInvMapLocs').attr('disabled', 'disabled');
            $('#ImportInvMapLocs').attr('disabled', 'disabled');
            ManageDataInvMapTable.draw();
        };
    });

    //handles deleting displayed records
    $('#InvMapDelete').click(function () {
        if (ManageDataInvMapTable.page.info().recordsDisplay == 0) {
            MessageModal("Warning", "There are no records to delete")
        } else {
            var ids = [];
            var conf = confirm("Delete the currently displayed records for the selected table?");
            if (conf) {
                //gets all displayed records by the inv mmap id to ensure only dipslayed records
                for (var x = 0; x < ManageDataInvMapTable.rows({ search: 'applied' }).data().length; x++) {
                    ids.push(ManageDataInvMapTable.row(x, { search: 'applied' }).data()[ManageDataInvMapCols.indexOf("Inv Map ID")])
                };
                manageDataHub.server.deleteInvMapDataMan($('#InvMapDataSelect').val(), ids).done(function (mess) {
                    if (!mess) {
                        MessageModal("Error", "An error has occurred deleting these records")
                    } else {
                        ManageDataInvMapTable.draw();
                    };
                });
            };
        };
    });
    //populates the export inv map with current inv map records 
    $('#GetInvMapLocs').click(function () {
        var conf = confirm("Copy Inventory Map Locations for exporting?");
        if (conf) {
            var conf2 = confirm("Press OK for ALL locations. Press Cancel for ONLY locations with an item");
            //all locations
            if (conf2) {
                manageDataHub.server.getInvMapLocations(1).done(function (mess) {
                    if (!mess) {
                        MessageModal("Error", "An error has occurred copying these locations")
                    } else {
                        ManageDataInvMapTable.draw();
                    };
                });
                //only locations with an item number
            } else {
                manageDataHub.server.getInvMapLocations(0).done(function (mess) {
                    if (!mess) {
                        MessageModal("Error", "An error has occurred copying these locations")
                    } else {
                        ManageDataInvMapTable.draw();
                    };
                });
            };
        };
    });
    //Handles importing inventory map locations
    $('#ImportInvMapLocs').click(function () {
        var autoImp = $('#AutoImportCheck').is(':checked');
        manageDataHub.server.importInvMapLocModal(autoImp).done(function (mess) {
            if (mess != "Imported" && mess != "Error") {
                MessageModal("Warning", mess);
            } else if (mess == "Error") {
                MessageModal("Error", "An error has occurred importing the locations");
            } else {
                ManageDataInvMapFilterMen = "";
                ManageDataInvMapTable.draw();
            };
        });
    });

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Inventory section
    //Datatable initialization
    ManageDataInvTable = $('#InvDataManTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'columnDefs': [
            {

            }
        ],
        "ajax": {
            "url": "/IE/Menu/getManageDataInvTable",
            "data": function (d) {
                d.filter = (ManageDataInvFilterMen == "" ? "" : ManageDataInvFilterMen.getFilterString());
                d.location = $('#InvDataSelect').val();
            }

        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "paging": true
    });
    //Enables horizontal scroll bar
    $('#InvDataManTable').wrap('<div style="overflow-x:scroll;"></div>');

    //Adds the columsn to the column list
    $.each($('#InvDataManCols').children(), function (index, element) {
        ManageDataInvCols.push($(element).attr('value'));
    });

    //Filter Initialization
    ManageDataInvFilterMen = new FilterMenuTable({
        Selector: '#InvDataManTable',
        columnIndexes: ManageDataInvCols,
        dataTable: ManageDataInvTable,
        columnMap: function () {
            var colMap = [];
            colMap["Transaction Type"] = "Text"
            colMap["Item Number"] = "Text"
            colMap["Supplier Item ID"] = "Text"
            colMap["Description"] = "Text"
            colMap["Category"] = "Text"
            colMap["Sub Category"] = "Text"
            colMap["Primary Zone"] = "Text"
            colMap["Supplier Number"] = "Text"
            colMap["Manufacturer"] = "Text"
            colMap["Model"] = "Text"
            colMap["Special Features"] = "Text"
            colMap["Reorder Point"] = "Number"
            colMap["Reorder Quantity"] = "Number"
            colMap["Unit of Measure"] = "Text"
            colMap["Secondary Zone"] = "Text"
            colMap["Carousel Cell Size"] = "Text"
            colMap["Carousel Velocity"] = "Text"
            colMap["FIFO"] = "Bool"
            colMap["Active"] = "Bool"
            colMap["Date Sensitive"] = "Bool"
            colMap["Warehouse Sensitive"] = "Bool"
            colMap["Carousel Min Qty"] = "Number"
            colMap["Carousel Max Qty"] = "Number"
            colMap["Pick Fence Qty"] = "Number"
            colMap["Split Case"] = "Bool"
            colMap["Case Quantity"] = "Number"
            colMap["Unit Cost"] = "Number"
            colMap["Bulk Cell Size"] = "Text"
            colMap["Bulk Velocity"] = "Text"
            colMap["Bulk Min Qty"] = "Number"
            colMap["Bulk Max Qty"] = "Number"
            colMap["CF Cell Size"] = "Text"
            colMap["CF Velocity"] = "Text"
            colMap["CF Min Qty"] = "Number"
            colMap["CF Max Qty"] = "Number"
            colMap["Replenishment Point"] = "Number"
            colMap["Replenishment Level"] = "Number"
            colMap["Avg Piece Weight"] = "Number"
            colMap["Sample Quantity"] = "Number"
            colMap["Use Scale"] = "Bool"
            colMap["Min Use Scale Quantity"] = "Number"
            colMap["Kit Item Number"] = "Text"
            colMap["Kit Quantity"] = "Number"
            colMap["Scan Code"] = "Text"
            colMap["Scan Type"] = "Text"
            colMap["Scan Range"] = "Text"
            colMap["Start Position"] = "Number"
            colMap["Code Length"] = "Number"
            colMap["Minimum RTS Reel Quantity"] = "Number"
            colMap["Pick Sequence"] = "Number"
            colMap["Include In Auto RTS Update"] = "Bool"
            return colMap
        }()
    });
    //Redraw the table on the filter change
    $('#InvDataManTable').on('filterChange', function () {
        ManageDataInvTable.draw();
    });

    //Enable and disable buttons depedning on table selected and redraw table
    $('#InvDataSelect').change(function () {
        var $this = $(this);
        //current
        if ($this.val() == "current") {
            $('#InvDelete').attr('disabled', 'disabled');
            $('#GetInvRecs').attr('disabled', 'disabled');
            $('#ProcInvRecs').attr('disabled', 'disabled');
            $('#ExpScanCodes').attr('disabled', 'disabled');
            ManageDataInvTable.draw();
        //scan codes
        } else if ($this.val() == "scan codes") {
            $('#InvDelete').removeAttr('disabled');
            $('#GetInvRecs').attr('disabled', 'disabled');
            $('#ProcInvRecs').attr('disabled', 'disabled');
            $('#ExpScanCodes').removeAttr('disabled');
            ManageDataInvTable.draw();
        //import
        } else if ($this.val() == "import") {
            $('#InvDelete').removeAttr('disabled');
            $('#GetInvRecs').attr('disabled', 'disabled');
            $('#ProcInvRecs').removeAttr('disabled');
            $('#ExpScanCodes').attr('disabled', 'disabled');
            ManageDataInvTable.draw();
        //export
        } else if ($this.val() == "export") {
            $('#InvDelete').removeAttr('disabled');
            $('#GetInvRecs').removeAttr('disabled');
            $('#ProcInvRecs').attr('disabled', 'disabled');
            $('#ExpScanCodes').attr('disabled', 'disabled');
            ManageDataInvTable.draw();
        //kit and archive
        } else {
            $('#InvDelete').removeAttr('disabled');
            $('#GetInvRecs').attr('disabled', 'disabled');
            $('#ProcInvRecs').attr('disabled', 'disabled');
            $('#ExpScanCodes').attr('disabled', 'disabled');
            ManageDataInvTable.draw();
        };
    });

    //Hanldes deleting displayed records. If scan codes or kit are displayed it uses the filter
    $('#InvDelete').click(function () {
        if (ManageDataInvTable.page.info().recordsDisplay == 0) {
            MessageModal("Warning", "There are no records to delete")
        } else {
            var itemnums = [];
            var conf = confirm("Delete the currently displayed records for the selected table?");
            if (conf) {
                //populates the list with the displayed records
                for (var x = 0; x < ManageDataInvTable.rows({ search: 'applied' }).data().length; x++) {
                    itemnums.push(ManageDataInvTable.row(x, { search: 'applied' }).data()[ManageDataInvCols.indexOf("Item Number")])
                };
                manageDataHub.server.deleteInvDataMan($('#InvDataSelect').val(), itemnums, (ManageDataInvFilterMen == "" ? "" : ManageDataInvFilterMen.getFilterString())).done(function (mess) {
                    if (!mess) {
                        MessageModal("Error", "An error has occurred deleting these records")
                    } else {
                        ManageDataInvTable.draw();
                    };
                });
            };
        };
    });

    //Copies the reocrds the current inventory table to the export inventory table
    $('#GetInvRecs').click(function () {
        var conf = confirm("Copy Inventory records for exporting?")
        if (conf) {
            manageDataHub.server.getInvRecords().done(function (mess) {
                if (!mess) {
                    MessageModal("Error", "An error has occurred copying these records")
                } else {
                    ManageDataInvTable.draw();
                };
            });
        };
    });

    //Processes (imports) the import inventory items.  
    $('#ProcInvRecs').click(function () {
        var autoImp = $('#AutoImportCheck').is(':checked');
        manageDataHub.server.procInventoryRecordsModal(autoImp).done(function (mess) {
            if (mess != "Proccessed" && mess != "Error") {
                MessageModal("Warning", mess);
            } else if (mess == "Error") {
                MessageModal("Error", "An error has occurred importing the locations");
            } else {
                ManageDataInvFilterMen = "";
                ManageDataInvTable.draw();
            };
        });
    });

    //Exports the displayed scan codes. Uses the filter to get the desired scan codes
    $('#ExpScanCodes').click(function () {
        var conf = confirm("Copy the displayed Scan Codes for exporting?");

        if (conf) {
            manageDataHub.server.exportScanCodes((ManageDataInvFilterMen == "" ? "" : ManageDataInvFilterMen.getFilterString())).done(function (mess) {
                if (!mess) {
                    MessageModal("Error", "An error has occurred copying the displayed scan codes")
                };
            });
        };
    });
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
});