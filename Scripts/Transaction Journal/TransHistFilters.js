// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var TransHistFilterMen = "";
$(document).ready(function () {
    //Number, Text, Date, Bool
    TransHistFilterMen = new FilterMenuTable({
        Selector: '#transHistTable',
        columnIndexes: TransHistCols,
        dataTable: transTable,
        columnMap: function () {
            var colMap=[];
            colMap["TH_ID"]= "Number"
            colMap["ID"]= "Number"
            colMap["Import Date"]= "Date"
            colMap["Import By"]= "Text"
            colMap["Import Filename"]= "Text"
            colMap["Transaction Type"] = "Text"
            colMap["Order Number"] = "Text"
            colMap["Priority"] = "Number"
            colMap["Item Number"] = "Text"
            colMap["Revision"] = "Text"
            colMap["Lot Number"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["Serial Number"] = "Text"
            colMap["Description"] = "Text"
            colMap["Transaction Quantity"] = "Number"
            colMap["Location"] = "Text"
            colMap["Warehouse"] = "Text"
            colMap["Zone"] = "Text"
            colMap["Carousel"] = "Text"
            colMap["Row"] = "Text"
            colMap["Shelf"] = "Text"
            colMap["Bin"] = "Text"
            colMap["Completed Date"] = "Date"
            colMap["Completed By"] = "Text"
            colMap["Completed Quantity"] = "Number"
            colMap["Batch Pick ID"] = "Text"
            colMap["Notes"] = "Text"
            colMap["Export File Name"] = "Text"
            colMap["Export Date"] = "Date"
            colMap["Exported By"] = "Text"
            colMap["Export Batch ID"] = "Text"
            colMap["Line Number"] = "Number"
            colMap["Line Sequence"]= "Number"
            colMap["Table Type"] = "Text"
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
            colMap["Unit of Measure"] = "Text"
            colMap["Required Date"] = "Date"
            colMap["Status Code"] = "Text"
            colMap["Master Record"] = "Bool"
            colMap["Master Record ID"] = "Number"
            colMap["Inv Map ID"] = "Number"
            colMap["Label"] = "Bool"
            colMap["In Process"] = "Bool"
            colMap["Tote ID"] = "Text"
            colMap["Tote Number"] = "Number"
            colMap["Cell"] = "Text"
            colMap["Host Transaction ID"] = "Text"
            colMap["Emergency"] = "Bool"
            return colMap
        }()
    });
});