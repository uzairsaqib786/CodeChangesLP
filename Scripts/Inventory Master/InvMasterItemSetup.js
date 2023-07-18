// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // Item Setup
    $('#FIFO').change(function () {
        if ($(this).is(':checked')) {
            $('#FIFOshow').show();
        } else {
            $('#FIFOshow').hide();
        };
    });
    $('#pZone').change(function () {
        var sZone = $('#sZone');
        if (this.value.trim() != '' && this.value != null) { sZone.removeAttr('disabled'); } else { sZone.attr('disabled', 'disabled').val(' '); };
    });
    $('#pFenceQty, #caseQty, #pSequence').on('focusout', function () {
        if (this.value == null || this.value == '') { this.value = 0; };
    });

    $('#pFenceQty, #caseQty, #pSequence').on('input', function () {
        setNumericInRange($(this), 0, null);
    });

    $('#carMinQty, #carMaxQty').on('focusout', function () {
        MinMaxMatch($('#carMinQty'), $('#carMaxQty'));
    });
    $('#bulkMinQty, #bulkMaxQty').on('focusout', function () {
        MinMaxMatch($('#bulkMinQty'), $('#bulkMaxQty'));
    });
    $('#cartonMinQty, #cartonMaxQty').on('focusout', function () {
        MinMaxMatch($('#cartonMinQty'), $('#cartonMaxQty'));
    });

    function MinMaxMatch(min, max) {
        if (min.val().trim() == '') { min.val(0); };
        if (max.val().trim() == '') { max.val(0); };
        if (min.val() > max.val()) { min.val(max.val()); };
    };

    $('#carMinQty, #carMaxQty, #bulkMinQty, #bulkMaxQty, #cartonMinQty, #cartonMaxQty').on('input', function () {
        setNumericInRange($(this), 0, null);
    });
});