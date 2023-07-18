// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var currentModalFunction = ""
var countOptionSelected = ""
$(document).ready(function () {
    //Instantiates all data for ccQueue Table
    CountQueueTable = $('#CountQueueTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [
            [
                0, 'desc'
            ]
        ],
        "lengthMenu": [20],
        'columnDefs': [
         {
             'targets': [1],
             'visible': false
         }
        ],
        'createdRow': function (row, data, index) {
            $(row).children(':first').html('<button type="button" class="btn btn-danger removeRow"><span class="glyphicon glyphicon-remove"></span></button>');
        },
        "ajax": {
            //Function that grabs Table Data
            "url": "/CycleCount/ccQueue",
            "data": function (d) {
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    }).on('xhr.dt', function (e, settings, json) {
        //alert(json.extraData)
        document.getElementById("CountQueue").innerHTML = 'Count Queue <span class="label label-default">' + json.extraData + '</span>';
    });
    $('#CountQueueTable').wrap("<div style='overflow-x:scroll;'></div>'")
    $('#pageLength').change(function () {
        CountQueueTable.page.len($(this).val());
        CountQueueTable.draw();
    })

    //Handelers for Modal buttons for Creating Counts/Deleting all from ccQueue
    $("#CountBatch_submit").click(function () {
        countOptionSelected = true;
    })
    $("#CountBatch_cancel").click(function () {
        countOptionSelected = false;
    })
    $('#CountBatch_Modal').on('hidden.bs.modal', function () {
        if (currentModalFunction == "Create") {
            if (countOptionSelected) {
                cycleHub.server.createCountRecords().done(function (result) {
                    if (result == "Error") {
                        MessageModal("Error", "Error Occured while creating Count records, check event log for more information")
                    } else {
                        $('#cCounts').click();
                        CountQueueTable.draw(false)
                    }
                })
            } 
        } else {
            if (countOptionSelected) {
                cycleHub.server.removeccQueueAll().done(function (result) {
                    if (result == "Error") {
                        MessageModal("Error", "An Error Occured while trying to remove all data, check the event log for more information")
                    } else {
                        CountQueueTable.draw(false)
                    }
                })
            }
        }
    })

    //Create count transactions from Queue
    $('#CreateCounts').click(function () {
        currentModalFunction = "Create"
        $('#CountBatch_Message').html("Would you like to create count transactions for these locations?")
        $('#CountBatch_Modal').modal("show")
    })

    //Deletes all records from ccQueue
    $('#DeleteccQueue').click(function () {
        currentModalFunction = "Delete"
        $('#CountBatch_Message').html("Would you like to delete all records from the Queue?")
        $('#CountBatch_Modal').modal("show")
    })

    //Removes row from ccQueue Table
    $('#CountQueueTable').on('click', 'tr td .removeRow', function () {
        var invMapID = CountQueueTable.row($(this).parent().parent()).data()[1]
        cycleHub.server.removeccQueueRow(invMapID).done(function (result) {
            if (result != "Error") {
                CountQueueTable.draw(false);
            } else {
                MessageModal("Error", "An Error Occured while trying to remove this row, check the event log for more information")
            }
        })
    })

    //Handles actions required when clicking Count Queue tab
    $('#CountQueue').click(function () {
        CountQueueTable.draw(false)
    });
})
