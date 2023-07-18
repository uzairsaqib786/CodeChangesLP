// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022


var CountResultTable;
var CountQueueTable;
var cycleHub = $.connection.cycleCountHub;
var typingtime = new Date().getTime();
var updateInterval ;
$(document).ready(function () {
    //Instantiates all data for BatchResult Table
    CountResultTable = $('#CountResultTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
          {
              'targets': [1],
              'visible': false
          }
        ],
        'createdRow': function (row, data, index) {
            $(row).children(':first').html('<button type="button" class="btn btn-danger removeRow"><span class="glyphicon glyphicon-remove"></span></button>');
        }
    });
    $('#CountResultTable').wrap("<div style='overflow-x:scroll;'></div>'")



    //Changes the currently selected count filter
    $('#CountFilterType').change(function () {
        $('#CountResultInputs input[type="text"]').val('');
        $('#LocationRangeDiv, #ItemNumberDiv, #DescriptionDiv, #CategoryDiv, #NotCountedSinceDiv, #PickedRangeDiv, #PutAwayRangeDiv, #CostRangeDiv').hide();
        $('#InsccQueue').attr('disabled', 'disabled');
        CountResultTable.clear().draw();
        $($(this).val()).show();
    })

    $('#CountResultInputs input[type="text"], #IncludeEmpty, #IncludeOther').on('input change', function () {
        var newTime = new Date().getTime();
        if (newTime - typingtime > 200) {
            if ($(this).attr('id') == 'CostStart') {
                if ($('#CostEnd').val() != "") {
                    FillBatchResultTable();
                }
            } else if ($(this).attr('id') == 'CostEnd') {
                if ($('#CostStart').val() != "") {
                    FillBatchResultTable();
                }
            } else {
                FillBatchResultTable();
            }

        }
        else {
            clearTimeout(updateInterval);
            updateInterval = setTimeout(function () { FillBatchResultTable();; }, 200)
        }
        typingtime = newTime;

    })

    //Removes row from Batch Results Table
    $('#CountResultTable').on('click', 'tr td .removeRow', function () {
        CountResultTable.row($(this).parent().parent()).remove().draw();
    })

    //Inserts records into ccQueue Table
    $("#InsccQueue").click(function () {
        insertccQueueValues();
    });

    $('#DelIncomplete').click(function () {
        var ordernum = $('#CycleOrderNumber').val();
        var conf = confirm("Are you sure you want to delete all Incomplete count transactions for " + ordernum + "?")
        if (conf) {
            DeleteOrder(ordernum, 1);
        }
    })

    $('#DelAll').click(function () {
        var ordernum = $('#CycleOrderNumber').val();
        var conf = confirm("Are you sure you want to delete All count transactions for " + ordernum + "?");
        if (conf) {
            DeleteOrder(ordernum, 0);
        }
    })

    //Need to add printing here
    $('#PrintCycleCount').click(function () {
        var ordernum = $('#CycleOrderNumber').val();
        //reportsHub.server.exportCycleCountReport(ordernum);
        //LLPreviewModal("Cycle Count Preview");
        title = 'Cycle Count Report';

        if ($('#PrintCCDetailsDirect').prop('checked')) {
            getLLPreviewOrPrint('/Admin/CycleCount/PrintCycleCountReport', {
                orderNumber: ordernum
            }, true, 'report', title)
        } else {
            getLLPreviewOrPrint('/Admin/CycleCount/ExportCycleCountReport', {
                orderNumber: ordernum
            }, false, 'report', title)
        };
       
    })
});

//Makes call to server to fill Batch Result Table
function FillBatchResultTable() {
    cycleHub.server.getBatchResultTable(BuildBatchQueryObject()).done(function (dataSet) {
        if (typeof dataSet != 'string') {
            CountResultTable.clear();
            CountResultTable.rows.add(dataSet).draw();
            if (dataSet.length > 0) {
                $('#InsccQueue').removeAttr('disabled');
            } else {
                $('#InsccQueue').attr('disabled', 'disabled');
            }
        } else {
            CountResultTable.clear().draw();
            MessageModal("Error", "An Error Occured while retreiving the table, check the event log for more information")
        }

    });
}

//Builds data needed to be passed to sever for Batch Result Table
function BuildBatchQueryObject() {
    return {
        CountType: $('#CountFilterType').val(),
        FromLocation: $('#FromLocation').val(),
        ToLocation: $('#ToLocation').val(),
        IncludeEmpty: $('#IncludeEmpty').prop('checked'),
        IncludeOther: $('#IncludeOther').prop('checked'),
        FromItem: $('#FromItem').val(),
        ToItem: $('#ToItem').val(),
        Description: $('#Description').val(),
        Category: $('#Category').val(),
        SubCategory: $('#SubCategory').val(),
        NotCounted: $('#NotCounted').val() == '' ? '1/11/1111' : $('#NotCounted').val(),
        PickStart: $('#PickedStart').val() == '' ? '1/11/1111' : $('#PickedStart').val(),
        PickEnd: $('#PickedEnd').val() == '' ? '1/11/1111' : $('#PickedEnd').val(),
        PutAwayStart: $('#PutStart').val() == '' ? '1/11/1111' : $('#PutStart').val(),
        PutAwayEnd: $('#PutEnd').val() == '' ? '1/11/1111' : $('#PutEnd').val(),
        CostStart: $('#CostStart').val(),
        CostEnd: $('#CostEnd').val(),
        WarehouseFilter: $('#Warehouse').val()
    }
}

//Inserts all selected rows into the ccQueue Table
function insertccQueueValues() {
    var InvMapIDs = new Array();
    var data = CountResultTable.data();
    var finaliter = Math.floor(data.length / 1000);
    var curriter = 0;

    if (finaliter == 0) {
        for (var x = 0; x < data.length; x++) {
            InvMapIDs.push(data[x][1]);
            if (InvMapIDs.length == data.length) {
                cycleHub.server.insertccQueue(InvMapIDs).done(function (returnVal) {
                    if (returnVal != 'Done') {
                        MessageModal("Error", returnVal);
                    };
                    $('#CountQueue').click();
                    CountResultTable.clear().draw();
                });
            };
        };
    } else {
        for (var x = 0; x < data.length; x++) {
            InvMapIDs.push(data[x][1]);

            if (InvMapIDs.length == 1000) {
                cycleHub.server.insertccQueue(InvMapIDs).done(function (returnVal) {
                    curriter++;
                    if (curriter == finaliter) {
                        if (InvMapIDs.length > 0) {
                            cycleHub.server.insertccQueue(InvMapIDs).done(function (returnVal) {
                                if (returnVal != 'Done') {
                                    MessageModal("Error", returnVal);
                                };
                                $('#CountQueue').click();
                                CountResultTable.clear().draw();
                            });
                        } else {
                            $('#CountQueue').click();
                            CountResultTable.clear().draw();
                        };
                    };

                    if (returnVal != 'Done') {
                        MessageModal("Error", returnVal);
                        return;
                    };
                });

                InvMapIDs = [];
            };
        };
    };
};

function DeleteOrder(ordernumber, ident) {
    cycleHub.server.deleteCountOrders(ordernumber, ident).done(function (returnVal) {
        if (returnVal != "Ran") {
            MessageModal("Error", returnVal);
        } else {
            $('#CycleOrderNumber').val("");
            orderNumberTypeahead.clearRemoteCache();
        }
    });

}
function cycleCountBackBtn() {
    cycleHub.server.checkCountQueue().done(function (count) {
        if (count > 0) {
            var c = confirm("There are still items in your Count Queue.  Leaving this page will remove those items from the Queue.");
            if (c == true) {
                cycleHub.server.clearCountQueue()
                history.back()
            }
        } else {
            history.back()
        };
    });
};