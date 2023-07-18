// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var ToteTransManCols = new Array();
var ToteTransManFilterMen = "";
$(document).ready(function () {
    //gets the current column sequence for the tote transaction anager page
    $.each($('#ToteTransManCols').children(), function (index, element) {
        ToteTransManCols.push($(element).attr('value'));
    });
    //initializes the filtering for the totebtransactions manager table
    ToteTransManFilterMen = new FilterMenuTable({
        Selector: "#ToteTransManTable", //table id
        columnIndexes: ToteTransManCols, //table col list
        dataTable: ToteTransManTable, //table variable
        columnMap: function () {
            var colMap = [];
            //append to each column the type it is
            colMap["Batch Pick ID"] = "Text"
            colMap["Tote Number"] = "Number"
            colMap["Tote ID"] = "Text"
            colMap["Zone Label"] = "Text"
            colMap["Transaction Type"] = "Text"
            colMap["Host Transaction ID"] = "Text"
            return colMap
        }()
    });

});