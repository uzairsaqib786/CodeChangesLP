// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var packTable, shipTable
$(document).ready(function () {
    packTable = $('#packInfoTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
            {
                'targets': [0],
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
    shipTable = $('#shipInfoTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
            {
                'targets': [0],
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

    $('#packInfoTable').wrap("<div style='overflow-y:scroll;height:200px;background-color:white;border:1px #ddd solid'></dov>")
    $('#shipInfoTable').wrap("<div style='overflow-y:scroll;height:200px;background-color:white;border:1px #ddd solid'></dov>")

    $('#packingList').click(function () {
        var orderNum = $('#shipInfoOrderNum').val();
        reportsHub.server.printShipOrderPL(orderNum);
    });
});