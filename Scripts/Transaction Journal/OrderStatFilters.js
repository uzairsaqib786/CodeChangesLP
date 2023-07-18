// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OrderStatFilterMen = "";
$(document).ready(function () {
    //Number, Date, Bool, Text
    OrderStatFilterMen = new FilterMenuTable({
        Selector: '#data',
        columnIndexes: OrderStatCols,
        dataTable: orderStatTable,
        columnMap: function () {
            var colMap = [];
            colMap["Type"] = "Text" //Transaction Type
            colMap["Completed Date"] = "Number"
            colMap["Location"] = "Text"
            colMap["Transaction Quantity"] = "Number"
            colMap["Item Number"] = "Text"
            colMap["Line Number"] = "Number"
            colMap["Required Date"] = "Date"
            colMap["Description"] = "Text"
            colMap["Completed Quantity"] = "Number"
            colMap["Tote ID"] = "Text"
            colMap["Priority"] = "Number"
            colMap["Completed By"] = "Text"
            colMap["Unit of Measure"] = "Text"
            colMap["Lot Number"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["Serial Number"] = "Text"
            colMap["Revision"] = "Text"
            colMap["Warehouse"] = "Text"
            colMap["Import Date"] = "Date"
            colMap["Batch Id"] = "Text" //Batch Pick ID
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
            colMap["Tote Number"] = "Number"
            colMap["Cell"] = "Text"
            colMap["Host Transaction ID"] = "Text"
            colMap["Zone"] = "Text"
            colMap["Emergency"] = "Bool"
            colMap["Date Verified"] = "Date"
            colMap["Verified By"] = "Text"
            colMap["ID"] = "Number"
            return colMap
        }()
    })
})