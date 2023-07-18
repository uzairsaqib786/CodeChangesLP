// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var MoveFromItemsFilterMen = "";
var MoveToItemsFilterMen = "";
$(document).ready(function () {
    MoveFromItemsFilterMen = new FilterMenuTable({
        Selector: '#MoveFromTable',
        columnIndexes: MoveItemsCols,
        dataTable: MoveFromTable,
        columnMap: function () {
            var colMap = [];
            colMap["Warehouse"] = "Text"
            colMap["Location Number"] = "Text"
            colMap["Velocity Code"] = "Text" //Golden Zone
            colMap["Item Number"] = "Text"
            colMap["Description"] = "Text"
            colMap["Item Quantity"] = "Number"
            colMap["Quantity Allocated Pick"] = "Number"
            colMap["Quantity Allocated Put Away"] = "Number"
            colMap["Zone"] = "Text"
            colMap["Carousel"] = "Text"
            colMap["Row"] = "Text"
            colMap["Shelf"] = "Text"
            colMap["Bin"] = "Text"
            colMap["Cell Size"] = "Text"
            colMap["Lot Number"] = "Text"
            colMap["Serial Number"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["Revision"] = "Text"
            colMap["Unit of Measure"] = "Text"
            colMap["Maximum Quantity"] = "Number"
            colMap["Put Away Date"] = "Date"
            colMap["User Field1"] = "Text"
            colMap["User Field2"] = "Text"
            colMap["Master Location"] = "Bool"
            colMap["Date Sensitive"]= "Bool"
            colMap["Dedicated"] = "Bool"
            colMap["Master Inv Map ID"] = "Number"
            colMap["Min Quantity"] = "Number"
            colMap["Inv Map ID"] = "Number"
            return colMap
        }()
    });

    MoveToItemsFilterMen = new FilterMenuTable({
        Selector: '#MoveToTable',
        columnIndexes: MoveItemsCols,
        dataTable: MoveToTable,
        columnMap: function () {
            var colMap = [];
            colMap["Warehouse"] = "Text"
            colMap["Location Number"] = "Text"
            colMap["Velocity Code"] = "Text" //Golden Zone
            colMap["Item Number"] = "Text"
            colMap["Description"] = "Text"
            colMap["Item Quantity"] = "Number"
            colMap["Quantity Allocated Pick"] = "Number"
            colMap["Quantity Allocated Put Away"] = "Number"
            colMap["Zone"] = "Text"
            colMap["Carousel"] = "Text"
            colMap["Row"] = "Text"
            colMap["Shelf"] = "Text"
            colMap["Bin"] = "Text"
            colMap["Cell Size"] = "Text"
            colMap["Lot Number"] = "Text"
            colMap["Serial Number"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["Revision"] = "Text"
            colMap["Unit of Measure"] = "Text"
            colMap["Maximum Quantity"] = "Number"
            colMap["Put Away Date"] = "Date"
            colMap["User Field1"] = "Text"
            colMap["User Field2"] = "Text"
            colMap["Master Location"] = "Bool"
            colMap["Date Sensitive"] = "Bool"
            colMap["Dedicated"] = "Bool"
            colMap["Master Inv Map ID"] = "Number"
            colMap["Min Quantity"] = "Number"
            colMap["Inv Map ID"] = "Number"
            return colMap
        }()
    });
});