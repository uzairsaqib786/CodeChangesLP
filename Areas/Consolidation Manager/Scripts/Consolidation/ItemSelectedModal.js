// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var itemSelectedTable;

$(document).ready(function () {
    //initialize datatable
    var startSelectFilter = $('#SelectFilter').val();
    if (startSelectFilter == 2) {
        startSelectFilter = 1;
    } else {
        startSelectFilter = 2;
    }
    itemSelectedTable = $('#ItemSelectedTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
            {
                'targets': [0, startSelectFilter],
                'visible': false
            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "lengthMenu": [20]
    });
    $('#ItemSelectedTable').wrap('<div style="overflow-x:scroll;"></div>');
    //will verify all selected items
    $('#ItemSelectedTable').on('click', 'tr', function () {
        //do something here with the id
        var selItem = itemSelectedTable.row(this).data();
        var id = selItem[0];
        consolidationHub.server.verifyItem(id).done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "Error has occured");
            } else {
                for (var x = 0; x < leftVerTable.rows().data().length; x++) {
                    var tabID = leftVerTable.row(x).data()[0];
                    if (tabID == id) {
                        var moveRow = leftVerTable.row(x).data();
                        leftVerTable.row(x).remove().draw();
                        rightVerTable.row.add(moveRow).draw();
                        $('#ItemSelectedModal').modal('hide');
                        break;
                    };
                };
            };
        });
    });

    $('#btnVerifyAll').on('click', function () {

        var data = itemSelectedTable.rows().data()
        data.each(function (value, index) {
            var id = value[0];
            consolidationHub.server.verifyItem(id).done(function (mess) {
                if (mess == "Fail") {
                    MessageModal("Error", "Error has occured");
                } else {
                    for (var x = 0; x < leftVerTable.rows().data().length; x++) {
                        var tabID = leftVerTable.row(x).data()[0];
                        if (tabID == id) {
                            var moveRow = leftVerTable.row(x).data();
                            leftVerTable.row(x).remove().draw();
                            rightVerTable.row.add(moveRow).draw();
                            $('#ItemSelectedModal').modal('hide');
                            break;
                        };
                    };
                };
            });
        })
    })

});