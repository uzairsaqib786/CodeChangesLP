// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var cMPreferencesHub = $.connection.cMPreferencesHub;


$(document).ready(function () {
    //auto save for consolidation preferences
    var cmPrefsTimer = mkTimer(function () {
        saveCMPreferences();
    }, 600);
    //auto save for shipping preferences
    var cmShipTimer = mkTimer(function () {
        saveCMShipPreferences();
    }, 600);
    
    //hide preferences based on which fields are checked
    if ($('#allowShip').prop('checked')) {
        $('#OtherShipPrefTab').show();
    } else {
        $('#freight').attr('checked', false);
        $('#freight1').attr('checked', false);
        $('#freight2').attr('checked', false);
        $('#weight').attr('checked', false);
        $('#length').attr('checked', false);
        $('#width').attr('checked', false);
        $('#height').attr('checked', false),
        $('#cube').attr('checked', false)
        $('#OtherShipPrefTab').hide();
    };

    if ($('#allowPack').prop('checked')) {
        $('#afterPackingCheck').show();
    } else {
        $('#confirmPack').attr('checked', false);
        $('#afterPackingCheck').hide();
    };

    if ($('#confirmPack').prop('checked')) {
        $('#restOf').show();
    } else {
        $('#printCont').attr('checked', false);
        $('#printOrd').attr('checked', false);
        $('#printContLabel').attr('checked', false);
        $('#contID').attr('checked', false);
        $('#contIDText').val("");
        $('#confirmQTY').attr('checked', false);
        $('#restOf').hide();
    }
    //auto save functionality for cmprefs
    $("#PrefPanel").on("change input", "input[type='checkbox'], select, input[type='text']", function () {
        cmPrefsTimer.startTimer()
    });

    $("#ShipPrefPanel").on("change input", "input[type='checkbox'], select, input[type='text']", function(){
        if ($('#allowShip').prop('checked')) {
            $('#OtherShipPrefTab').show();
        } else {
            $('#freight').attr('checked', false);
            $('#freight1').attr('checked', false);
            $('#freight2').attr('checked', false);
            $('#weight').attr('checked', false);
            $('#length').attr('checked', false);
            $('#width').attr('checked', false);
            $('#height').attr('checked', false),
            $('#cube').attr('checked', false)
            $('#OtherShipPrefTab').hide();
        };

        if ($('#allowPack').prop('checked')) {
            $('#afterPackingCheck').show();
        } else {
            $('#confirmPack').attr('checked', false);
            $('#afterPackingCheck').hide();
        };

        if ($('#confirmPack').prop('checked')) {
            $('#restOf').show();
        } else {
            $('#printCont').attr('checked', false);
            $('#printOrd').attr('checked', false);
            $('#printContLabel').attr('checked', false);
            $('#contID').attr('checked', false);
            $('#contIDText').val("");
            $('#confirmQTY').attr('checked', false);
            $('#restOf').hide();
        }

        cmShipTimer.startTimer();
    });
    //auto save for ship prefs
    $("#OtherShipPrefTab").on("change input", "input[type='checkbox'], select, input[type='text']", function () {
        cmShipTimer.startTimer();
    });

    $("#EmailPackSlip").click(saveEmailSlip);

    

});

function saveCMPreferences() {
    console.log("You have done it")
    cMPreferencesHub.server.updateCMPrefsPrefs($('#backOrders').prop('checked'), $('#DefPackList').val(), $('#DefLookType').val(), $('#VerifyItems').val(), 
                                                $('#BlindVerify').val(), $('#PrintVerified').val(), $('#PrintUnVerified').val(),
                                                $('#PackListSort').val(),$('#nonPickpro').prop('checked'),$('#validateStagingLocs').prop('checked')).done(function (returnMessage) {
                                                    if (returnMessage=="Error") {
                                                        MessageModal("Error", "Error has occured");
                                                    }
                                                });
};
function saveCMShipPreferences() {
    cMPreferencesHub.server.updCMPrefsShipPrefs($('#allowPack').prop('checked'), $('#confirmPack').prop('checked'), $('#printCont').prop('checked'),
                                                $('#printOrd').prop('checked'), $('#printContLabel').prop('checked'), $('#contID').prop('checked'),
                                                $('#contIDText').val(), $('#confirmQTY').prop('checked'), $('#freight').prop('checked'),
                                                $('#freight1').prop('checked'), $('#freight2').prop('checked'), $('#weight').prop('checked'),
                                                $('#length').prop('checked'), $('#width').prop('checked'), $('#height').prop('checked'),
                                                $('#cube').prop('checked'), $('#allowShip').prop('checked')).done(function (mess) {
                                                    if (mess == "Error") {
                                                        MessageModal("Error", "Error has occured")
                                                    }
                                                })
};

function saveEmailSlip() {
    cMPreferencesHub.server.updSystPrefsEmailSlip($('#EmailPackSlip').prop('checked')).done(function (mess) {
        if (mess == "Error") {
            MessageModal("Error", "Error has occured")
        }
    })
}
