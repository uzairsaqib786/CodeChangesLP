// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var InvMasterFilter = "";
$(document).ready(function () {
    //Number, Text, Date
    //alertnate light is number
    //laser x and y are ints
    //velocity code is text
    //all others check the inv map table design on sql
    InvMasterFilter = new FilterMenu({
        Selector: '#stockcodeCopy',
        NumberSelector: '.inv-num',
        DateSelector: '.inv-date',
        TextSelector: '.inv-text',
        BoolSelector: '.inv-bool',
        ShowFilterCondition: function () {
            return $('#saveButton').is(":disabled")
        }
    });
});
