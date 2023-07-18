// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OrderManCreateFilterMen = "";
$(document).ready(function () {
    OrderManCreateFilterMen = new FilterMenuTable({
        Selector: "#CreateOrdersTable",
        columnIndexes: OrderManCreateCols,
        columnMap: function () {
            var colMap = [];
            colMap["Transaction Type"] = "Text"
            colMap["Order Number"] = "Text"
            colMap["Priority"] = "Number"
            colMap["Required Date"] = "Date"
            colMap["User Field1"] = "Text"
            colMap["User Field2"] = "Text"
            colMap["User Field3"] = "Text"
            colMap["User Field4"] = "Text"
            colMap["User Field5"] = "Text"
            colMap["User Field6"] = "Text"
            colMap["User Field7"] = "Text"
            colMap["User Field8"] = "Text"
            colMap["User Field9"] = "Text"
            colMap["User Field10"] = "Text"
            colMap["Item Number"] = "Text"
            colMap["Description"] = "Text"
            colMap["Line Number"] = "Number"
            colMap["Transaction Quantity"] = "Number"
            colMap["Warehouse"] = "Warehouse"
            colMap["Line Sequence"] = "Number"
            colMap["In Process"] = "Bool"
            colMap["Processing By"] = "Text"
            colMap["Unit of Measure"] = "Text"
            colMap["Import By"] = "Text"
            colMap["Import Filename"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["Lot Number"] = "Text"
            colMap["Serial Number"] = "Text"
            colMap["Notes"] = "Text"
            colMap["Revision"] = "Text"
            colMap["ID"] = "Number"
            colMap["Host Transaction ID"] = "Text"
            colMap["Emergency"] = "Bool"
            colMap["Label"] = "Bool"
            colMap["Batch Pick ID"] = "Text"
            colMap["Cell"] = "Text"
            return colMap
        }()
    });
});