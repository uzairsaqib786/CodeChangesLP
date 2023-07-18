// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    invMapTable = $('#invMapTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [
            [
                $('thead tr th:contains("Location Number")').index(), 'asc'
            ]
        ],
        "lengthMenu": [20],
        //Handles Qurantined Items
        "createdRow": function (row, data, index) {
            // handles quarantined items
            if (data[columns.indexOf('Warehouse')].indexOf("Quarantine") >= 0) {
                $(row).addClass("quarantine");
            };
        },
        "ajax": {
            //Function that grabs Table Data
            "url": "/InventoryMap/invMap",
            "data": function (d) {
                d.searchString = $('#searchString').val();
                d.OQA = $('#viewButton').attr('value');
                d.searchColumn = $('#selection').val();
                d.filter = (InvMapFilterMen == "" ? "" :InvMapFilterMen.getFilterString());
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    }).on('draw', function () {
        toggleDisabledButtons(true);
    });

    $('#pageLength').change(function () {
        invMapTable.page.len($(this).val());
        invMapTable.draw();
    });
    $('#invMapTable').wrap('<div style="overflow-x:scroll;"></div>');

    $('#selection').on('change', function () {
        invMapTable.draw();
    });

    $('#invMapTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('tr.active').removeClass('active');
            $this.addClass('active');
            toggleDisabledButtons(false);
            quarantineButton();
        } else {
            toggleDisabledButtons(true);
            $this.removeClass('active');
        };
    });

});