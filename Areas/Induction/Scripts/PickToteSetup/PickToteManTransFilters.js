// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

//column sequence
var PickToteManTransCols = ["Order Number", "Item Number", "Transaction Quantity", "Location", "Completed Quantity", "Description",
                            "Import Date", "Priority", "Required Date", "Line Number", "Line Sequence", "Serial Number", "Lot Number",
                            "Expiration Date", "Completed Date", "Completed By", "Batch Pick ID", "Unit of Measure", "User Field1",
                            "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8",
                            "User Field9", "User Field10", "Revision", "Tote ID", "Tote Number", "Cell", "Host Transaction ID",
                            "ID", "Zone", "Carousel", "Row", "Shelf", "Bin", "Warehouse", "Inv Map ID", "Import By", "Import Filename",
                            "Notes", "Emergency", "Master Record", "Master Record ID", "Export Batch ID", "Export Date", "Exported By", "Status Code"]
var PickToteManTransFilterMen = "";

$(document).ready(function () {
    //initialize the filter on the transactions table in the modal 
    PickToteManTransFilterMen = new FilterMenuTable({
        Selector: "#PickToteTransTable", //table id
        columnIndexes: PickToteManTransCols, //column list
        dataTable: PickToteTransTable, //table variable
        columnMap: function () {
            var colMap = [];
            //append each column name the type it is 
            colMap["Order Number"] = "Text"
            colMap["Item Number"] = "Text"
            colMap["Transaction Quantity"] = "Number"
            colMap["Location"] = "Text"
            colMap["Completed Quantity"] = "Number"
            colMap["Description"] = "Text"
            colMap["Import Date"] = "Date"
            colMap["Priority"] = "Number"
            colMap["Required Date"] = "Date"
            colMap["Line Number"] = "Number"
            colMap["Line Sequence"] = "Number"
            colMap["Serial Number"] = "Text"
            colMap["Lot Number"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["Completed Date"] = "Date"
            colMap["Completed By"] = "Text"
            colMap["Batch Pick ID"] = "Text"
            colMap["Unit of Measure"] = "Text"
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
            colMap["Revision"] = "Text"
            colMap["Tote ID"] = "Text"
            colMap["Tote Number"]="Number"
            colMap["Cell"] = "Text"
            colMap["Host Transaction ID"] = "Text"
            colMap["ID"]="Number"
            colMap["Zone"] = "Text"
            colMap["Carousel"] = "Text"
            colMap["Row"] = "Text"
            colMap["Shelf"] = "Text"
            colMap["Bin"] = "Text"
            colMap["Warehouse"] = "Text"
            colMap["Inv Map ID"] = "Number"
            colMap["Import By"] = "Text"
            colMap["Import Filename"] = "Text"
            colMap["Notes"] = "Text"
            colMap["Emergency"]="Bool"
            colMap["Master Record"]="Bool"
            colMap["Master Record ID"]="Number"
            colMap["Export Batch ID"] = "Text"
            colMap["Export Date"]="Date"
            colMap["Exported By"] = "Text"
            colMap["Status Code"] = "Text"
            return colMap;
        }()
    });

});
