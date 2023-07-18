// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var InvMapFilterMen = "";
$(document).ready(function () {
    //Number, Text, Date
    //alertnate light is number
    //laser x and y are ints
    //velocity code is text
    //all others check the inv map table design on sql
    InvMapFilterMen = new FilterMenuTable({
        Selector: '#invMapTable',
        columnIndexes: columns,
        dataTable: invMapTable,
        columnMap: function () {
            var colMap = [];
            colMap["Inv Map ID"] = "Number"
            colMap["Alternate Light"] = "Number"
            colMap["Location"] = "Text"
            colMap["Warehouse"] = "Text"
            colMap["Zone"] = "Text"
            colMap["Carousel"] = "Text"
            colMap["Row"] = "Text"
            colMap["Shelf"] = "Number"
            colMap["Bin"] = "Text"
            colMap["Item Number"] = "Text"
            colMap["Revision"] = "Text"
            colMap["Serial Number"] = "Text"
            colMap["Lot Number"] = "Text"
            colMap["Expiration Date"] = "Date"
            colMap["Description"] = "Text"
            colMap["Item Quantity"] = "Number"
            colMap["Unit of Measure"] = "Text"
            colMap["Maximum Quantity"] = "Number"
            colMap["Cell Size"] = "Text"
            colMap["Velocity Code"] = "Text"
            colMap["Put Away Date"] = "Date"
            colMap["User Field1"] = "Text"
            colMap["User Field2"] = "Text"
            //bit fields
            colMap["Master Location"] = "Bool"
            colMap["Date Sensitive"] = "Bool"
            colMap["Dedicated"] = "Bool"
            //end of bit fields
            colMap["Master Inv Map ID"] = "Number"
            colMap["Quantity Allocated Pick"] = "Number"
            colMap["Quantity Allocated Put Away"] = "Number"
            colMap["Min Quantity"] = "Number"
            colMap["Laser X"] = "Number"
            colMap["Laser Y"] = "Number"
            colMap["Location Number"]= "Text"
            return colMap
        }()
    })
})
