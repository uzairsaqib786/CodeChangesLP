// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OrderManFilterMen = "";

$(document).ready(function () {
    OrderManFilterMen = new FilterMenuTable({
        Selector: "#OMDataTable",
        columnIndexes: OrderManCols,
        columnMap: function(){
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
            colMap["Allocated Picks"] = "Number"
            colMap["Allocated Puts"] = "Number"
            colMap["Available Quantity"] = "Number"
            colMap["Stock Quantity"] = "Number"
            colMap["Warehouse"] = "Text"
            colMap["Zone"] = "Text"
            colMap["Line Sequence"] = "Number"
            colMap["Tote ID"] = "Text"
            colMap["Tote Number"] = "Number"
            colMap["Unit of Measure"] = "Text"
            colMap["Batch Pick ID"] = "Text"
            colMap["Category"] = "Text"
            colMap["Sub Category"] = "Text"
            colMap["Import By"] = "Text"
            colMap["Import Date"] = "Date"
            colMap["Import Filename"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["Lot Number"] = "Text"
            colMap["Serial Number"] = "Text"
            colMap["Notes"] = "Text"
            colMap["Revision"] = "Text"
            colMap["Supplier Item ID"] = "Text"
            colMap["ID"] = "Number"
            colMap["Host Transaction ID"] = "Text"
            colMap["Emergency"] = "Bool"
            colMap["Location"]= "Text"
            colMap["Label"] = "Bool"
            colMap["Cell"] = "Text"
            return colMap
        }()
    });
});