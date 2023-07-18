// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var DeAllocFilter = "";
$(document).ready(function () {
    //Number, Text, Date, Bool
    DeAllocFilter = new FilterMenuTable({
        Selector: '#orderTable',
        columnIndexes: ["Order Number", "Item Number", "Description", "Priority", "Transaction Quantity", "Unit of Measure", "Batch ID", "Transaction Type"],
        dataTable: itemTable,
        columnMap: function () {
            var colMap = [];
            colMap["Order Number"] = "Text"
            colMap["Item Number"] = "Text"
            colMap["Description"] = "Text"
            colMap["Priority"] = "Number"
            colMap["Transaction Quantity"] = "Number"
            colMap["Unit of Measure"] = "Text"
            colMap["Batch ID"] = "Text"
            colMap["Transaction Type"] = "Text"
            return colMap
        }()
    })
});