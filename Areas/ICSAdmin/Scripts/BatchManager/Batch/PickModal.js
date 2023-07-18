// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

function appendPickModal(order, toteNum) {
    var appendString = $('<div class="row tote-container" style="padding-top:10px;">\
            <div class="col-md-4 order">\
                <input type="text" class="form-control"  disabled="disabled" value="' + order + '" maxlength="50" />\
            </div>\
            <div class="col-md-2 tote-number">\
                <input type="text" class="form-control" disabled="disabled" value="' + toteNum + '" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" />\
            </div>\
            <div class="col-md-4 tote-id">\
                <input type="text" class="form-control" placeholder="Tote ID" maxlength="50" />\
            </div>\
            <div class="col-md-2 controls">\
                <button type="button" class="btn btn-danger clear" data-toggle="tooltip" data-original-title="Clear">\
                    <span class="glyphicon glyphicon-remove"></span>\
                </button>\
                <button type="button" class="btn btn-primary" data-toggle="tooltip" data-original-title="Print">\
                    <span class="glyphicon glyphicon-print"></span>\
                </button>\
            </div>\
        </div>').appendTo($('#ToteEntryContainer'));
    appendString.find('[data-toggle="tooltip"]').tooltip();
};

function showPickModal() {
    $('#ToteEntryContainer').html('');
    $.each(rightBMTable.data(), function (index, elem) {
        appendPickModal(elem[0], elem[4]);
    });
    $('#pick_modal').modal('show');
};

$(document).ready(function () {
    $('#pick_modal').on('click', '.clear', function () {
        $(this).parent().siblings('.tote-id').children().val('').focus();
    });

    $('#PickClear').click(function () {
        $('.tote-id').children().val('');
        $('.tote-id:first').focus();
    });

    $('#PickCreate').click(function () {
        if (confirm('Click OK to auto generate tote IDs for this batch of orders.')) {
            var nextTote = $('#NextToteID').val();
            $.each($('#ToteEntryContainer .tote-container .tote-id').children(), function (i, elem) {
                $(elem).val(parseInt(nextTote));
                nextTote = parseInt(nextTote) + 1;
            });
        };
    });

    $('#pick_modal').on('input', '.tote-id', function () {
        setNumericInRange($(this), 0, null);
    });

    $('#pick_submit').click(function () {
        var stop = false, orders = new Array(), $e, tote;
        $.each($('#ToteEntryContainer .tote-container'), function (i, elem) {
            $e = $(elem); tote = $e.find('.tote-id').children().val().trim();
            if (tote == '') {
                stop = true;
                return;
            } else {
                orders.push([$e.find('.order').children().val(), tote]);
            };
        });
        if (stop) {
            alert('All Tote IDs must be specified before submitting.');
        } else {
            updatePickToteIDs(orders);
        };
    });
});