// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var leftCountTable, rightCountTable;

$(document).ready(function () {
    //initialize datatable
    leftCountTable = $('#leftCountTable').DataTable({
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
    //initialize datatable
    rightCountTable = $('#rightCountTable').DataTable({
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

    //handles clicking records on the left table
    $('#leftCountTable tbody').on('click', 'tr', function () {
        var left = leftCountTable.row(this).data();
        leftCountTable.row(this).remove().draw();
        rightCountTable.row.add(left).draw();
    });

    //handles clicking records on the right table
    $('#rightCountTable tbody').on('click', 'tr', function () {
        var right = rightCountTable.row(this).data();
        rightCountTable.row(this).remove().draw();
        leftCountTable.row.add(right).draw();
    });

    $('#SelectCounts').click(function () {
        var data = leftCountTable.rows().data();
        leftCountTable.clear().draw();
        rightCountTable.rows.add(data).draw();
    });
});