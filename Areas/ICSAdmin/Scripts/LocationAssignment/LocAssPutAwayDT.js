// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var leftPutAwayTable, rightPutAwayTable;

$(document).ready(function () {
    //initialize left datatable
    leftPutAwayTable = $('#leftPutAwayTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
          {
              'targets': ["all"],
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
    //initialize right datatable
    rightPutAwayTable = $('#rightPutAwayTable').DataTable({
        "dom": 'trip',
        "processing": true,
        'columnDefs': [
            {
                'targets': ["all"],
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

    //handles clicking left table records
    $('#leftPutAwayTable tbody').on('click', 'tr', function () {
        var left = leftPutAwayTable.row(this).data();
        leftPutAwayTable.row(this).remove().draw();
        rightPutAwayTable.row.add(left).draw();
    });
    //handles clicking right table records
    $('#rightPutAwayTable tbody').on('click', 'tr', function () {
        var right = rightPutAwayTable.row(this).data();
        rightPutAwayTable.row(this).remove().draw();
        leftPutAwayTable.row.add(right).draw();
    });

    $('#SelectPuts').click(function () {
        var data = leftPutAwayTable.rows().data();
        leftPutAwayTable.clear().draw();
        rightPutAwayTable.rows.add(data).draw();
    });
});