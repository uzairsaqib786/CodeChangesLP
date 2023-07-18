// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var leftPickTable, rightPickTable;
var orderSearchTimer;

$(document).ready(function () {
    //initialize datatable
    leftPickTable = $('#leftPickTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
          {
              'targets': [4,5,6],
              'visible': false
          }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "lengthMenu": [20],
        "createdRow": function (row, data, index) {
            if ((data[4] == "True")&(data[5]=="false")) {
                $(row).addClass("danger");
            }
            else if((data[5]=="True")){
                $(row).addClass("info")
            }
            else if ((data[6] == "True")) {
                $(row).addClass("success")
            }               
        }

    });
    //initialize right table 
    rightPickTable = $('#rightPickTable').DataTable({
        "dom": 'trip',
        "processing": true,
        'columnDefs': [
            {
                'targets': [4,5,6],
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

    $('#OrderNumberSearch').on('input', function() {
        if (orderSearchTimer)
            clearTimeout(orderSearchTimer);

        orderSearchTimer = setTimeout(function() {
            getPickTableData();
        }, 1000);
    });

    //handles clicking on a record in the left table 
    $('#leftPickTable tbody').on('click', 'tr', function () {
        var left = leftPickTable.row(this).data();
        leftPickTable.row(this).remove().draw();
        rightPickTable.row.add(left).draw();
    });
    //handles clicking on a record in the right table
    $('#rightPickTable tbody').on('click', 'tr', function () {
        var right = rightPickTable.row(this).data();
        rightPickTable.row(this).remove().draw();
        leftPickTable.row.add(right).draw();
    });

    $('#SelectPicks').click(function () {
        var data = leftPickTable.rows().data();
        leftPickTable.clear().draw();
        rightPickTable.rows.add(data).draw();
    });
});