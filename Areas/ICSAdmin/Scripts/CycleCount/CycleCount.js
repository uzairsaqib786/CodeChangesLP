// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var leftCycleCountTable
var rightCycleCountTable
var cycleCountHub = $.connection.cycleCountHub;

$(document).ready(function () {

    leftCycleCountTable = $('#CycleCountLeftTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
          {
              'targets': [2, 7],
              'visible': false
          }
        ],
        "lengthMenu": [20],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });
    rightCycleCountTable = $('#CycleCountRightTable').DataTable({
        "dom": 'trip',
        "lengthMenu": [20],
        'columnDefs': [
          {
              'targets': [1, 7],
              'visible': false
          }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });
    $('#CycleCountLeftTable, #CycleCountRightTable').wrap("<div style='overflow-x:scroll;'></div>")

    $('#CycleCountLeftTable tbody').on('click', 'tr', function () {
        if (leftCycleCountTable.rows().data().length > 0) {
            var row = $(this);
            var selectedRow = this;
            cycleCountHub.server.getCycleCountQty(row.find(':nth-child(1)').html(), row.find(':nth-child(3)').html()).done(function (qty) {
                if (qty == " Error") {
                    MessageModal("Error", "An error has occured");
                } else {
                    var left = leftCycleCountTable.row(selectedRow).data();
                    left[2] = qty;
                    leftCycleCountTable.row(selectedRow).remove().draw();
                    rightCycleCountTable.row.add(left).draw();
                    var quant = parseInt($('#TotLocs').val()) + parseInt(qty);
                    $('#TotLocs').val(quant);
                };
            });
        };
    });

    $('#CycleCountRightTable tbody').on('click', 'tr', function () {
        var right = rightCycleCountTable.row(this).data();
        var row = $(this);
        rightCycleCountTable.row(this).remove().draw();
        leftCycleCountTable.row.add(right).draw();
        var quant = parseInt($('#TotLocs').val()) - parseInt(row.find(':nth-child(2)').html());
        $('#TotLocs').val(quant);
    });

    $("#DeleteDiscrept").click(function () {
        var conf = confirm("Are you sure you want to delete all records?");
        if (conf) {
            cycleCountHub.server.deleteDiscrepancies().done(function (returnMessage) {
                if (returnMessage == "Error") {
                    MessageModal("Error", "An error has occured");
                }
            });
            leftCycleCountTable.rows().remove();
            leftCycleCountTable.draw();
        }


        
    });

    $('#AppendDiscButt').click(function () {
        MoveAllRight();
    });

    $('#ClearDiscButt').click(function () {
        leftCycleCountTable.rows.add(rightCycleCountTable.data()).draw();
        rightCycleCountTable.clear().draw();
        $('#TotLocs').val(0);
    });


    $('#CreateCountTrans').click(function () {
        var conf = confirm("Are you sure you want to create a transaction?")
        if (conf) {
            var colList = rightCycleCountTable.column(7).data();
            var idList = [];
            for (i = 0; i < colList.length; i++) {
                idList.push(colList[i]);
            }
            cycleCountHub.server.cycleCountCreateTrans(idList).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occured");
                } else {
                    leftCycleCountTable.rows().remove();
                    rightCycleCountTable.rows().remove();
                    leftCycleCountTable.draw();
                    rightCycleCountTable.draw();
                    $('#TotLocs').val(0);
                }
            });
        };
        
    });
    $('#ImportFieldMap').click(function () {
        $('#FieldMapModal').modal('show');
    });

    $('#ImportHostFile').click(function () {
        var conf = confirm("Are you sure you want to import host quanties? Doing so will delete all other discrepancies");
        if (conf) {
            cycleCountHub.server.importHostQtyFile().done(function (result) {
                if (result != "success") {
                    MessageModal("Error", result);
                } else {
                    location.reload();
                }
            });
        };
    });

    //Need to add printing here
    $(document.body).on('click', '#PrintDiscRep', function () {
        if ($('#PrintCCDiscDirect').prop('checked')) {
            getLLPreviewOrPrint('/Admin/CycleCount/PrintDiscReport', {
            }, true, 'report', 'Discrepancy List');
        } else {
            getLLPreviewOrPrint('/Admin/CycleCount/ExportDiscReport', {
            }, false, 'report', 'Discrepancy List');
        };
    });


});

function MoveAllRight() {
    var rowData = leftCycleCountTable.rows(0).data()
    if (rowData.length > 0) {
        cycleCountHub.server.getCycleCountQty(rowData[0][0], rowData[0][3]).done(function (qty) {
            var left = rowData[0];
            left[2] = qty;
            leftCycleCountTable.row(0).remove();
            rightCycleCountTable.row.add(left);
            rowData = "";
            var quant = parseInt($('#TotLocs').val()) + parseInt(qty);
            $('#TotLocs').val(quant);
            MoveAllRight();
        })
    } else {
        leftCycleCountTable.draw();
        rightCycleCountTable.draw();
    }
};

function cycleCountBackBtn() {
    alert("There are still items in your count queue.  Leaving this page will discard them. ");

};