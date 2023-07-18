// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var EventLogFilterMen = "";
$(document).ready(function () {
    EventLogFilterMen = new FilterMenuTable({
        Selector: '#eventTable',
        columnIndexes: EventLogCols,
        dataTable: eventLogTable,
        columnMap: function () {
            var colMap = [];
            colMap["Date"] = "Date" //Date Stamp
            colMap["Message"] = "Text"
            colMap["Event Code"] = "Text"
            colMap["Username"] = "Text" //Name Stamp
            colMap["Event Type"] = "Text"
            colMap["Event Location"] = "Text"
            colMap["Notes"] = "Text"
            colMap["Trans. ID"] = "Number" //Transaction ID
            return colMap
        }()
    });
});