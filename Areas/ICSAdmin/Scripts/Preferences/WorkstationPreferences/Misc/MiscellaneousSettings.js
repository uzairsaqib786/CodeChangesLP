// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var t1 = mkTimer(function () {
        saveMiscStockCount();
    }, 500);
    var t2 = mkTimer(function () {
        saveMiscSett();
    }, 500);

    $('#stockCountConfirmation').on('toggle', '.toggles', function () {
        t1.startTimer();
    });

    $('#otherSettings').on('toggle', '.toggles', function () {
        t2.startTimer();
    });

    $('#otherSettings').on('input', 'input', function () {
        setNumericInRange($('#idleMessTime'), 0, SqlLimits.numerics.int.max);
        setNumericInRange($('#idleMessShutDownTime'), 0, SqlLimits.numerics.int.max);
        t2.startTimer();
    });

    $('#inactiveCarousels').on('toggle', '.toggles', function (e, checked){
        var checkedAlert;
        var uncheckedAlert;
        var superAlertcheck;
        var superAlertuncheck;
        var $this = $(this);
        if (checked) {
            checkedAlert = confirm("Warning! Marking this carousel as inactive will quarantine all records in inventory map with this carousel and zone equal to this workstation's pod id. Press Okay to mark this carousel as inactive");
            if (checkedAlert) {
                superAlertcheck = confirm("Are you sure you wish to mark this carousel as inactive? This will QUARANTINE all records in Inventory Map with the same carousel and have a zone equal to the Pod ID. Press Okay to continue");
                if (superAlertcheck) {
                    saveInactiveCars($(this).data('name'), checked);
                } else {
                    setTimeout(function () {
                        $this.data('toggles').setValue(false);
                    }, 0);
                };
                
            } else {
                setTimeout(function () {
                    $this.data('toggles').setValue(false);
                }, 0);
            };
        } else {
            uncheckedAlert = confirm("Warning! Marking this carousel as active will unquarantine all records in inventory map with this carousel and zone equal to this workstation's pod id. Press Okay to mark this carousle as active");
            if (uncheckedAlert) {
                superAlertuncheck = confirm("Are you sure you wish to mark this carousel as active? This will UNQUARANTINE all records in Inventory Map with the same carousel and have a zone equal ot the Pod ID. Press Okay to continues");
                if (superAlertuncheck) {
                    saveInactiveCars($(this).data('name'), checked);
                } else {
                    setTimeout(function () {
                        $this.data('toggles').setValue(true);
                    }, 0);
                };
                
            } else {
                setTimeout(function () {
                    $this.data('toggles').setValue(true);
                }, 0);
            };
        };
    });

});


function getMiscSettings() {
    preferencesHub.server.selMiscSettings().done(function (settings) {
        if (settings.ccbatpik.toLowerCase() == "yes") {
            $('#carouselPick').data('toggles').setValue(true);
        } else {
            $('#carouselPick').data('toggles').setValue(false);
        };
        if (settings.ccbatput.toLowerCase() == "yes") {
            $('#carouselPut').data('toggles').setValue(true);
        } else {
            $('#carouselPut').data('toggles').setValue(false);
        };
        if (settings.cchotpik.toLowerCase() == "yes") {
            $('#hotPick').data('toggles').setValue(true);
        } else {
            $('#hotPick').data('toggles').setValue(false);
        };
        if (settings.cchotput.toLowerCase() == "yes") {
            $('#hotPut').data('toggles').setValue(true);
        } else {
            $('#hotPut').data('toggles').setValue(false);
        };

 
        $('#idleMessTime').val(settings.idlecheck);
        $('#idleMessShutDownTime').val(settings.idlemess);
        if (settings.hotpickordernum.toLowerCase() == "yes") {
            $('#forceOrderNumHotPick').data('toggles').setValue(true);
        } else {
            $('#forceOrderNumHotPick').data('toggles').setValue(false);
        };
        if (settings.hotputordernum.toLowerCase() == "yes") {
            $('#forceOrderNumHotPut').data('toggles').setValue(true);
        } else {
            $('#forceOrderNumHotPut').data('toggles').setValue(false);
        };

        $('#inactiveCars .Inactive_Cars').each(function (index, element) {
            if ($.inArray($(this).data('name').toString(), settings.inactivecars) == -1) {
                $(this).data('toggles').setValue(false);
            } else {
                $(this).data('toggles').setValue(true);
            };
        });
        
    });
};

function saveMiscSett() {
    preferencesHub.server.updateMiscellaneousInfo($('#idleMessTime').val(), $('#idleMessShutDownTime').val(),  $('#forceOrderNumHotPick').data('toggles').active,
         $('#forceOrderNumHotPut').data('toggles').active).done(function (success) {
        if (!success) {
            alert("Saving other settings failed")
        };
    });
};

function saveMiscStockCount() {
 
    preferencesHub.server.updateMiscStockCount($('#carouselPick').data('toggles').active, $('#carouselPut').data('toggles').active, $('#hotPick').data('toggles').active,
        $('#hotPut').data('toggles').active).done(function (success) {
        if (!success) {
            alert("Saving Stock Count information failed")
        };
    });
};

function saveInactiveCars(car, ident) {
    preferencesHub.server.addDeleteInactiveCarousels(car, ident).done(function (success) {
        if (!success) {
            alert("Failed to save Inactive Carousel Settings")
        };
    });
};