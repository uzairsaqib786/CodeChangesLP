// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var sender;
    $('#systemReplenishmentNewOrders').on('click', '.trans-qty', function (e) {
        var $this = $(this);
        if ($this.parents('tr').hasClass('active')) {
            e.stopImmediatePropagation();
        };
        sender = $this.hasClass('glyphicon') ? $this.siblings('input.trans-qty') : $this;
        var available = sender.parents('tr').find(':nth-child(7)').html();
        var replenishment = sender.parents('tr').find(':nth-child(8)').html();
        $('#availableQtyModal').val(available);
        $('#replenishmentQtyModal').val(replenishment);
        $('#transQtyModal').val(sender.val());
        $('#trans_qty_modal').modal('show');
    });

    $('#transQtyModal').on('input', function () {
        var available = $('#availableQtyModal').val(), replenishment = $('#replenishmentQtyModal').val();
        var high = available > replenishment ? replenishment : available;
        if (high > SqlLimits.numerics.int.max) {
            high = SqlLimits.numerics.int.max;
        };
        setNumericInRange($(this), 0, high);
    });

    $('#trans_qty_submit').click(function () {
        var value = $('#transQtyModal'), rp_id = sender.attr('name');
        if (value.val().trim() != '') {
            sysRepHub.server.editTransactionQtyReplenishmentQueue(rp_id, value.val()).done(function () {
                sender.val(value.val());
                $('#trans_qty_modal').modal('hide');
            });
        } else {
            value.val(sender.val());
            $('#trans_qty_modal').modal('hide');
        };
    });
});