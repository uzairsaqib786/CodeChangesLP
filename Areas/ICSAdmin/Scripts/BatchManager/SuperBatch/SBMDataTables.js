// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var leftSBMTable, rightSBMTable;
$(document).ready(function () {
    leftSBMTable = $('#leftSuperBMTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'ordering': false,
        "ajax": {
            "url": "/BatchManager/getSBSingleLines"
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        'drawCallback': function (settings) {
            if (leftSBMTable.data().length > 0) {
                $('#SuperBatchCreate').removeAttr('disabled');
            } else {
                $('#SuperBatchCreate').attr('disabled', 'disabled');
            };
        }
    });

    rightSBMTable = $('#rightSuperBMTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'ordering': false,
        "ajax": {
            "url": "/BatchManager/getSBBatchedTables"
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        'drawCallback': function (settings) {
            if (rightSBMTable.data().length > 0) {
                $('#ClearSuperBatch').removeAttr('disabled');
            } else {
                $('#ClearSuperBatch').attr('disabled', 'disabled');
            };
        }
    });
});