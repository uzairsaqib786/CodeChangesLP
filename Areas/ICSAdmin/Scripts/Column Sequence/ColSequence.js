// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/******************************************************************
    Column Sequence
*****************************************************************/

// Reference the auto-generated proxy for the hub. 
var defaultColumn = $.connection.columnSequence;
// check if user wants to leave the page before the defaults have been saved
var unsaved = false;

$(document).ready(function () {
    // get the order of columns in order so that it can be set on the VB.net/SQL side
    $('#saveDefaultColumnSequence').click(function () {
        unsaved = false;
        var defaultArray = [];
        /*
            Custom Handeling for Inventory Map, as all fields are always needed
        */
        if ($('#tableName').val() == "Inventory Map" || $('#tableName').val() == "Order Manager" || $('#tableName').val() == "Order Manager Create" || $('#tableName').val() == "Open Transactions Temp" || $('#tableName').val() == "ReProcessed") {
            var count = 0;
            $('#defaultList li').each(function () {
                count++;
            });
            if (count > 1) {
                var defaultList = $('#defaultList');
                $('#draggableList li:not(.static), #draggablePanelList li:not(.static)').each(function () {
                    // don't move the labels
                    var $this = $(this);
                    defaultList.append("<li class='btn btn-primary btn-sm'>" + $this.text() + "</li>");
                    $this.remove();
                });
                $('#draggablePanelList, #defaultList, #draggableList').trigger("sortupdate");
            };
        };
        $('#defaultList li:not(.static)').each(function () {
            defaultArray.push($(this).text().trim());
        });
        // VB.net call
        defaultColumn.server.saveColumns(defaultArray, $('#tableName').val()).done(function () {
            alert("Column Sequence Saved!");
            document.getElementById('backToPage').submit();
        });
    });

    // deletes all user defaults from the database by view (OT or TH)
    $('#delTJColSeq').click(function () {
        var result = confirm("Delete user default column sequence for " + $('#currentUser').val() + "?");
        if (result) {
            defaultColumn.server.clearAllColumns($('#tableName').val()).done(function () {
                //document.getElementById('reload').submit();
                location.reload();
            });
        };
    });

    // get default column sequence from the table by user and view
    $('#restoreDefaultColumnSequence').click(function () {
        //document.getElementById('reload').submit();
        location.reload();
    });

    // return to previous view after check if there's any unsaved changes
    $('#backToTransactions').click(function () {
        if (!unsaved || confirm("There are unsaved columns in the list.  Return to " + $('#tableName').val() + " anyway?"))
            document.getElementById('backToPage').submit();
    });
    // keyboard shortcuts for Column Sequence
    $(document.body).on('keydown', function (e) {
        if (document.activeElement.tagName.toLowerCase() != 'input') {
            switch (e.keyCode) {
                case 65:
                    // A autofill columns
                    $('#autofillColumns').trigger("click");
                    break;
                case 66:
                    // B back to transactions
                    $('#backToTransactions').trigger("click");
                    break;
                case 67:
                    // C clear all
                    $('#delTJColSeq').trigger("click");
                    break;
                case 82:
                    // R restore to current default column sequence
                    $('#restoreDefaultColumnSequence').trigger("click");
                    break;
                case 83:
                    // S save column sequence
                    $('#saveDefaultColumnSequence').trigger("click");
                    break;
            };
        };
    });
});