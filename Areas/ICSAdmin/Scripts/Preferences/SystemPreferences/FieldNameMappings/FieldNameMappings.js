// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var fieldTimer = mkTimer(function () {
        saveFieldNames();
    }, 500);

    $('#UF1, #UF2, #UF3, #UF4, #UF5, #UF6, #UF7, #UF8, #UF9, #UF10, #itemNumberAlias, #UoMAlias').on('input', function () {
        fieldTimer.startTimer();
    });

    $('#resetFieldNames').click(function () {
        $('#ufs input').val('').trigger('input');
    });
});

function getFieldNames() {
    preferencesHub.server.getFieldNames().done(function (aliases) {
        $('#itemNumberAlias').val(aliases.ItemNumber);
        $('#UoMAlias').val(aliases.UoM);
        $('#UF1').val(aliases.UserFields[0]);
        $('#UF2').val(aliases.UserFields[1]);
        $('#UF3').val(aliases.UserFields[2]);
        $('#UF4').val(aliases.UserFields[3]);
        $('#UF5').val(aliases.UserFields[4]);
        $('#UF6').val(aliases.UserFields[5]);
        $('#UF7').val(aliases.UserFields[6]);
        $('#UF8').val(aliases.UserFields[7]);
        $('#UF9').val(aliases.UserFields[8]);
        $('#UF10').val(aliases.UserFields[9]);
    });
};

function saveFieldNames() {
    var itemAlias = $('#itemNumberAlias').val();
    var uomAlias = $('#UoMAlias').val();
    var ufs = [
        $('#UF1').val(),
        $('#UF2').val(),
        $('#UF3').val(),
        $('#UF4').val(),
        $('#UF5').val(),
        $('#UF6').val(),
        $('#UF7').val(),
        $('#UF8').val(),
        $('#UF9').val(),
        $('#UF10').val(),
    ];
    preferencesHub.server.saveFieldNames(itemAlias, uomAlias, ufs);
};