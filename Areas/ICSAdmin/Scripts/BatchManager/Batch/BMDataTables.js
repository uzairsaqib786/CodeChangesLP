// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var leftBMTable, rightBMTable, osAltTable;
$(document).ready(function () {
    leftBMTable = $('#leftBMTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
          {
              'targets': [-2, -3],
              'visible': false
          }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        'createdRow': function (row, data, index) {
            $(row).children(':last').html('<button type="button" class="btn btn-primary detail" name="' + data[0] + '">View Detail</button>');
        }
    });

    rightBMTable = $('#rightBMTable').DataTable({
        "dom": 'trip',
        "processing": true,
        'columnDefs': [
            {
                'targets': [1, 2, 3, 5],
                'visible': false
            },
            {
                'targets': [4],
                'sortable': false
            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    }).on('draw', function (e, settings) {
        $.each($('#rightBMTable tbody tr td:nth-child(' + 2 + ')'), function (i, elem) {
            rightBMTable.cell($('#rightBMTable tbody tr:nth-child(' + (i + 1) + ') td:nth-child(' + 2 + ')')).data(i + 1);
        });
    });

    $('#leftBMTable tbody').on('click', 'tr', function (e) {
        if ($(e.target).hasClass('detail')) { return; };
        var left = leftBMTable.row(this).data();
        leftBMTable.row(this).remove().draw(false);
        rightBMTable.row.add(left).draw();
    });

    $('#rightBMTable tbody').on('click', 'tr', function () {
        var right = rightBMTable.row(this).data();
        rightBMTable.row(this).remove().draw();
        leftBMTable.row.add(right).draw(false);
    });

    osAltTable = $('#OSAlt').DataTable({
        "dom": 'trip',
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });

    $('#OSAlt').wrap('<div style="overflow-x:scroll"></div>');
});