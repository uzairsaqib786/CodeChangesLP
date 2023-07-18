// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var PickToteOrdersTable;
var PickToteTransTable;

$(document).ready(function () {

    //datatables
    PickToteOrdersTable = $("#PickToteOrdersTable").DataTable({
        "dom": 'trip',
        "processing": true,
        "ordering": false,
        //"order": [],
        createdRow: function (row, data, index) {
            $('[name="OrderNumber"]').each(function () {
                var $currentIter = $(this);
                if ($currentIter.val() == data[0]) {
                    $(row).addClass('success');
                }
            })
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });
    $('#PickToteOrdersTable').wrap('<div style="overflow-x:scroll;"></div>');

    PickToteTransTable = $("#PickToteTransTable").DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/IM/PickToteSetup/InZoneTransDT",
            "data": function (d) {
                var orders = [];
                //push only the highlighted one
                //check to make sure something was clicked. val is undefined if nothing is clicked
                if ($('#PickToteOrdersTable tbody tr.SelOrder.success').val() !== undefined) {
                    orders.push(PickToteOrdersTable.row($('#PickToteOrdersTable tbody tr.SelOrder.success')[0]).data()[0]);
                };

                var orderstring = ""
                for (var i = 0; i <= orders.length - 1; i++) {
                    if (i == 0) {
                        orderstring = orders[i];
                    } else {
                        orderstring += ", " + orders[i];
                    };
                };
                if (orderstring == "") {
                    orderstring = "EAGLES";
                };
                d.ordernum = orderstring;
                d.filter = "";

            }

        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "paging": true
    });
    //on click event for orders table rows
    $("#PickToteOrdersTable").on('click', 'tr', function () {
        //append records here
        var $this = $(this);
        var NoRoom = true;
        var Orders = $('[name="OrderNumber"]');


        if (!$this.hasClass('SelOrder')) {
            $('#PickToteOrdersTable tbody tr.SelOrder').removeClass('SelOrder');
            $this.addClass('SelOrder');
        } else {
            $this.removeClass('SelOrder');
        };

        var selectedOrder = $this[0].innerText;

        if ($this.hasClass('success')) { //unselect order
            $this.removeClass('success');
            $.each(Orders, function (i, v) {
                if (this.value == selectedOrder) {
                    this.value = "";
                    return false
                };
            });
            PickToteTransTable.clear().draw();
        } else { //Select Order
            $.each(Orders, function (i, v) {
                if (this.value == "") {
                    this.value = selectedOrder;
                    NoRoom = false;
                    return false
                };
            });

            if (NoRoom) {
                MessageModal("Batch is Filled", "No open totes in batch");
            } else {
                $('#PickToteOrdersTable tbody tr.SelOrder').addClass("success");
                PickToteTransTable.draw();
            };
        };

    });

    $('#InZoneModalDismiss').click(function () {
        PickToteOrdersTable.clear().draw();
        PickToteTransTable.clear().draw();
        $('#QueryOrderLabel').removeAttr('hidden');
    });

    
    $('#FillOrders').click(function () {
        var Orders = $('[name="OrderNumber"]');
        $.each(Orders, function (i, v) {
            if (this.value == "") {
                for (var x = 0; x < $('#PickToteOrdersTable tbody tr').length; x++) {
                    if (!$($('#PickToteOrdersTable tbody tr')[x]).hasClass('success')) {
                        this.value = PickToteOrdersTable.row($('#PickToteOrdersTable tbody tr')[x]).data()[0];
                        $($('#PickToteOrdersTable tbody tr')[x]).addClass('success');
                        break;
                    };
                };
            };
        });
    });

    $('#ClearAll').click(function () {
        $('[name="Clear"]').click();
        $('#PickToteOrdersTable tbody tr').removeClass("success");
        PickToteTransTable.clear().draw();
    });

});

//fills the orders table depeding on if filter or zone is selected
function FillInZoneTableOrders(OrderView) {
    //zone
    PickToteHub.server.selectOrdersInZone(OrderView).done(function (data) {
        if (data.length > 0) {
            if (data[0][0] == "Error") {
                MessageModal("Error", "An error has occurred using the selected zone");
            } else {
                PickToteOrdersTable.clear().draw();
                PickToteOrdersTable.rows.add(data).draw();
                PickToteTransTable.clear().draw();
            };
        } else {
            MessageModal("No orders", "Thre are no orders for your zone");
        };
        $('#QueryOrderLabel').attr('hidden', 'hidden');
    });
};