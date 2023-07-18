// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var SBReqDatesTable;

$(document).ready(function () {

    SBReqDatesTable = $("#PickToteOrdersTable").DataTable({
        "dom": 'trip',
        "processing": true,
        "paging": false,
        "columnDefs": [
            { "orderable": false, "targets": [0, 1, 2] }
        ],
        "order": []
    });

    $('#PickToteOrdersTable').wrap('<div style="overflow-x:scroll;"></div>');

    $('#SBReqDateModal').on('shown.bs.modal', function () {
        $('#SBReDateRefresh').click();
    });


    $('#SBReDateRefresh').click(function () {
        $.ajax({
            method: "POST",
            url: "/SuperBatch/SelectReqDateData",
            success: function (Data) {
                SBReqDatesTable.clear().draw();
                SBReqDatesTable.rows.add(Data).draw();
            },
            error: function (xhr, status, errorThrown) {
                alert(xhr.responseText);
            }
        });
       
    });

});