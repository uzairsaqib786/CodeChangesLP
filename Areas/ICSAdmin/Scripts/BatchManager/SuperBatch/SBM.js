// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#ClearPriorities').click(function () {
        $('#Priorities').val('');
    });

    $('#Priorities').on('input', function () {
        setNumericCommaSeparated($(this));
    });

    $('#Groups').on('focusout', function () {
        if (this.value.trim() == '') { this.value = 1; };
    });

    $('#Groups').on('input', function () {
        setNumericInRange($(this), 1, null);
    });

    $('#ClearSuperBatch').click(function () {
        if (confirm('Click OK to clear ALL incomplete super batches.')) {
            clearSuperBatches();
        };
    });

    $('#SuperBatchCreate').click(function () {
        if (confirm('Click OK to automatically batch orders together.')) {
            createSuperBatch();
        };
    });
});