// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var SystReplenNewFilterMen = "";
var SystReplenCurrFilterMen = "";
$(document).ready(function () {
    SystReplenNewFilterMen = new FilterMenuTable({
        Selector: '#systemReplenishmentNewOrders',
        columnIndexes: SystReplenNewCols,
        dataTable: sysRepNewTable,
        columnMap: function () {
            var colMap = [];
            colMap["Item Number"] = "Text" //Item Number
            colMap["Description"] = "Text"
            colMap["Warehouse"] = "Text"
            colMap["Stock Qty"] = "Number"
            colMap["Replenishment Point"] = "Number" //Replenishment Point
            colMap["Replenishment Level"] = "Number" //Replenishment Level
            colMap["Available Qty"] = "Number" //Available Qty
            colMap["Replenishment Qty"] = "Number" //Replenishment Qty
            colMap["Case Qty"] = "Number"
            colMap["Transaction Qty"] = "Number" //Transaction Qty
            colMap["Alloc Pick"] = "Number" 
            colMap["Alloc Put"] = "Number"
            return colMap
        }()
    });

    SystReplenCurrFilterMen = new FilterMenuTable({
        Selector: '#currentOrders',
        columnIndexes: SystReplenCurrCols,
        dataTable: currReplenTable,
        columnMap: function () {
            var colMap = [];
            colMap["Item Number"] = "Text"
            colMap["Trans Type"] = "Text" //Transaction Type
            colMap["Warehouse"] = "Text"
            colMap["Zone"] = "Text"
            colMap["Carsl"] = "Text" //Carousel
            colMap["Row"] = "Text"
            colMap["Shelf"] = "Text"
            colMap["Bin"] = "Text" //Row
            colMap["Cell"] = "Text" //Bin
            colMap["Lot Number"] = "Text"
            colMap["Trans Qty"] = "Number" //Transaction Quantity
            colMap["Description"] = "Text"
            colMap["Order Number"] = "Text"
            colMap["UofM"] = "Text" //Unit of Measure
            colMap["Batch Pick ID"] = "Text"
            colMap["Serial Number"] = "Text"
            colMap["Comp Date"] = "Date" //Completed Date
            colMap["Print Date"] = "Date"
            return colMap
        }()
    });
});