// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var toteDT;
$(document).ready(function () {
    var first = true;
    toteDT = $('#TotesTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [
            [
                0, 'asc'
            ]
        ],
        "createdRow": function (row, data, index) {
            if (parseInt(data[2]) <= parseInt(data[3])) {
                $(row).addClass('success');
            } else {
                if (first) {
                    first = false;
                    $('#NextLoc').val(data[1]);
                    $('#NextPosition').val(data[0]);
                    $('#NextCell').val(parseInt(data[3]) + 1);
                    $(row).addClass('active');
                };
                $(row).addClass('warning');
            };
        },
        "ajax": {
            //Function that grabs Table Data
            "url": "/IM/ProcessPutAways/GetTotesTable",
            "data": function (d) {
                d.entryFilter = $('#BatchID').val()
            }
        },
        'paging': false,
        drawCallback: function (settings) {
            enableToteControls();
        }
    }).on('draw', function () {
        first = true;
    });
    $('#TotesTable').wrap('<div style="max-height:' + $(window).height() * 0.8 + ';overflow-y:scroll;"></div>');

    $('#TotesTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if ($('#TotesTable tbody tr td.dataTables_empty').length == 0) {
            if (!$this.hasClass('active')) {
                $('tr.active').removeClass('active');
                $this.addClass('active');
                enableToteControls();
                $('#SelectPosition').val($('tr.active td:nth-child(1)').html())
                $('#SelectTote').val($('tr.active td:nth-child(2)').html())
            };
        };
    });

    function enableToteControls() {
        if ($('#TotesTable tbody tr.active').length > 0) {
            $('#MarkToteFullButt').removeAttr('disabled');
            if (parseInt(toteDT.row($('#TotesTable tbody tr.active')[0]).data()[3]) > 0) {
                $('#ViewToteTransInfo,#PrintOffCar').removeAttr('disabled');
            } else {
                $('#ViewToteTransInfo,#PrintOffCar').attr('disabled', 'disabled');
            };
        } else {
            $('#MarkToteFullButt').attr('disabled', 'disabled');
        };
    };
});