// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#NextSerial, #NextTote, #MaxPutAway').on('focusout', function () {
        if (this.value.trim() == '') { this.value = 0; };
    });
    $('#NextTote, #MaxPutAway').on('input', function () {
        setNumericInRange($(this), 0, SqlLimits.numerics.int.max);
    });

    $('#NextSerial').on('input', function () {
        setNumericInRange($(this), 0, SqlLimits.numerics.bigint.max);
    });

    $('#DomainAuth').on('toggle', function (e, checked) {
        var $NTLM = $('#NTLM_div');
        if (checked) {
            $NTLM.show();
        } else {
            $NTLM.hide();
        };
    });

    $('#ReelTracking').on('change', function () {
        var $this = $(this);
        if ($this.val().trim().toLowerCase() == 'dynamic') {
            $('#DynReelWIP_div').show();
        } else {
            $('#DynReelWIP_div').hide();
        };
    });

    $('#PickType').on('change', function () {
        if ($(this).val() == 'Parallel Pick') {
            $('#Carousel_div').hide();
        } else {
            $('#Carousel_div').show();
        };
    });

    var p1Timer = mkTimer(function () {
        saveGeneralPreferences(1);
    }, 500);
    var p2Timer = mkTimer(function () {
        saveGeneralPreferences(2);
    }, 500);
    var p3Timer = mkTimer(function () {
        saveGeneralPreferences(3);
    }, 500);
    var p4Timer = mkTimer(function () {
        saveGeneralPreferences(4);
    }, 500);

    $('#panel1').on('input', 'input', function () {
        p1Timer.startTimer();
    });
    $('#panel2').on('toggle', '.toggles', function () {
        p2Timer.startTimer();
    });
    $('#panel3').on('input', 'input', function () {
        p3Timer.startTimer();
    });
    $('#panel3').on('toggle', '.toggles', function () {
        p3Timer.startTimer();
    });
    $('#panel3').on('change', 'select', function () {
        p3Timer.startTimer();
    });
    $('#panel4').on('toggle', '.toggles', function () {
        p4Timer.startTimer();
    });
    $('#panel4').on('change', 'select', function () {
        p4Timer.startTimer();
    });
});

function getGeneralPreferences() {
    preferencesHub.server.getCompanyInfo().done(function (info) {
        for (var x = 0; x < info.length; x++) {
            if (info[x].toLowerCase() == 'false') {
                info[x] = false;
            } else if (info[x].toLowerCase() == 'true') {
                info[x] = true;
            };
        };
        $('#cname').val(info[0]);
        $('#address').val(info[1]);
        $('#city').val(info[2]);
        $('#state').val(info[3]);

        $('#EarlyBreakTime').val(info[29]);
        $('#EarlyBreakDuration').val(parseInt(info[30]));
        $('#MidBreakTime').val(info[31]);
        $('#MidBreakDuration').val(parseInt(info[32]));
        $('#LateBreakTime').val(info[33]);
        $('#LateBreakDuration').val(parseInt(info[34]));


        $('#DomainAuth').data('toggles').setValue(info[4]);
        $('#NTLM').data('toggles').setValue(info[5]);
        if (info[4]) {
            $('#NTLM_div').show();
        } else {
            $('#NTLM_div').hide();
        };

        $('#orderManifest').data('toggles').setValue(info[6]);
        $('#FIFO').data('toggles').setValue(info[7]);
        $('#CheckTote').data('toggles').setValue(info[8]);
        $('#Replenish').data('toggles').setValue(info[9]);

        $('#PickLabels').data('toggles').setValue(info[10]);
        $('#ShortPickFind').data('toggles').setValue(info[11]);
        $('#ZeroLocationQty').data('toggles').setValue(info[12]);
        $('#RequestPutAwayLabels').data('toggles').setValue(info[13]);

        $('#CarouselBatchID').data('toggles').setValue(info[14]);
        $('#BulkBatchID').data('toggles').setValue(info[15]);
        $('#DynReelWIP').data('toggles').setValue(info[16]);
        $('#ReelTracking').val(info[17]);
        if (info[17].trim().toLowerCase() == 'dynamic') {
            $('#DynReelWIP_div').show();
        } else {
            $('#DynReelWIP_div').hide();
        };

        $('#MultiBatch').data('toggles').setValue(info[18]);
        $('#ConfirmInvChanges').data('toggles').setValue(info[19]);
        $('#OtTempToOtPending').data('toggles').setValue(info[28]);
        $('#DistinctKitOrders').data('toggles').setValue(info[35]);
        $('#PrintReplenPutLabels').data('toggles').setValue(info[36]);
        $('#CreateTransQuarantine').data('toggles').setValue(info[37]);

        $('#ShowTransQty').val(info[20]);
        $('#NextTote').val(info[21]);

        $('#NextSerial').val(info[22]);
        $('#MaxPutAway').val(info[23]);
        $('#PickType').val(info[24]);

        $('#OrderSort').val(info[25]);
        $('#CartonDisplay').val(info[26]);
        $('#DisplayImage').data('toggles').setValue(info[27]);

});
};

function getPanel1() {
    return [
        $('#cname').val(),
        $('#address').val(),
        $('#city').val(),
        $('#state').val(),
        $('#EarlyBreakTime').val(),
        $('#EarlyBreakDuration').val(),
        $('#MidBreakTime').val(),
        $('#MidBreakDuration').val(),
        $('#LateBreakTime').val(),
        $('#LateBreakDuration').val()
    ];
};

function getPanel2() {
    return [
         $('#DomainAuth').data('toggles').active,
         $('#NTLM').data('toggles').active
    ];
};

function getPanel3() {
    return [
        $('#orderManifest').data('toggles').active,
        $('#FIFO').data('toggles').active,
        $('#CheckTote').data('toggles').active,
        $('#Replenish').data('toggles').active,

        $('#PickLabels').data('toggles').active,
        $('#ShortPickFind').data('toggles').active,
        $('#ZeroLocationQty').data('toggles').active,
        $('#RequestPutAwayLabels').data('toggles').active,

        $('#CarouselBatchID').data('toggles').active,
        $('#BulkBatchID').data('toggles').active,
        $('#DynReelWIP').data('toggles').active,
        $('#ReelTracking').val(),

        $('#MultiBatch').data('toggles').active,
        $('#ConfirmInvChanges').data('toggles').active,
        $('#ShowTransQty').val(),
        $('#NextTote').val(),

        $('#NextSerial').val(),
        $('#MaxPutAway').val(),
        $('#PickType').val(),
        $('#OtTempToOtPending').data('toggles').active,
        $('#DistinctKitOrders').data('toggles').active,
        $('#PrintReplenPutLabels').data('toggles').active,
        $('#CreateTransQuarantine').data('toggles').active
    ];
};

function getPanel4() {
    return [
        $('#OrderSort').val(),
        $('#CartonDisplay').val(),
        $('#DisplayImage').data('toggles').active
    ];
};

function saveGeneralPreferences(panel) {
    var prefs;
    switch (panel) {
        case 1:
            prefs = getPanel1();
            break;
        case 2:
            prefs = getPanel2();
            break;
        case 3:
            prefs = getPanel3();
            if (!$.isNumeric(prefs[15]) || prefs[15] < 0 || !$.isNumeric(prefs[16]) || prefs[16] < 0) {
                return false;
            };
            break;
        case 4:
            prefs = getPanel4();
            break;
    };
    preferencesHub.server.saveGeneralPreferences(prefs, panel);
};