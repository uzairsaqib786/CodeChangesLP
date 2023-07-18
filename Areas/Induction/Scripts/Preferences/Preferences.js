// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var IMPrefHub = $.connection.iMPreferencesHub
$(document).ready(function () {
    //auto save system settings
    var sysSettTimer = mkTimer(saveIMSystemSetts, 200);
    function autoSaveIMSystSetts() {
        sysSettTimer.startTimer();
    };
    //auto save print settings
    var printSettTimer = mkTimer(saveIMPrintSetts, 200);
    function autoSaveIMPrintSetts() {
        printSettTimer.startTimer();
    };
    //auto save misc setup
    var miscSettTimer = mkTimer(saveIMMiscSetup, 200);
    function autoSaveIMMiscSetup() {
        miscSettTimer.startTimer();
    };
    //auto save rts user data
    var rtsSettTimer = mkTimer(saveRTSUserData, 200);
    function autoSaveRTSUserData() {
        rtsSettTimer.startTimer();
    };
    //triggers preference change in order to limit 20 tote matrix to only 20 totes
    $('#DefBatchQty').on('input', function () {
        if (parseInt($('#DefBatchQty').val()) > 20) {
            $('#PutInductScreen').val("Unlimited Positions");
            autoSaveIMSystSetts(); 
            $('#PutInductScreen').attr('disabled', 'disabled');
        } else {
            $('#PutInductScreen').removeAttr('disabled');
        };
    });
    //hide show strip scan options
    $('#StripScan').change(function () {
        if ($('#StripScan').prop('checked')) {
            $('#StripScanRow').show();
        } else {
            $('#StripScanRow').hide();
        };
    });
    //enable/disable track inductionlocation fields
    $('#TrackInductLoc').change(function () {
        if ($('#TrackInductLoc').prop('checked')) {
            $('#InductLoc').removeAttr('disabled');
            $('#UseCompName').removeAttr('disabled');
        } else {
            $('#InductLoc').attr('disabled', 'disabled');
            $('#UseCompName').attr('disabled', 'disabled');
        };
    });
    //gets the computer name that is designated to the wsid
    $('#UseCompName').click(function () {
        IMPrefHub.server.getCompName().done(function (name) {
            if (name == "Error") {
                MessageModal("Error", "An error has occurred trying to get your computer name")
            } else {
                $('#InductLoc').val(name);
                autoSaveIMMiscSetup();
            };
        });
    });

    //autosaves
    $('#SystemSettingsTab,#PickSettingsTab,#PutSettingsTab').on("change input", "input[type='checkbox'], select, input[type='text'], input[type='radio']", autoSaveIMSystSetts);

    $('#PrintSettingsTab').on("change input", "input[type='checkbox'], select, input[type='text']", autoSaveIMPrintSetts);

    $('#MiscSetupTab').on("change input", "input[type='checkbox'], select, input[type='text'], input[type='number']", autoSaveIMMiscSetup);

    $('#ReelTrackingTab').on("change input", "input[type='checkbox'], select, input[type='text']", autoSaveRTSUserData);

    $('#DefaultSuperBatchSize').on('change')

});

function saveIMSystemSetts() {
    IMPrefHub.server.updIMSytemSettings($('#AutoSelPickOrders').prop('checked'), $('#OrderSort').val(), $('#AutoPickToteID').prop('checked'), 
                                        $('#CarPickToteID').prop('checked'), $('#OffCarPickToteID').prop('checked'), $('#UsePickBatchMan').prop('checked'), 
                                        $('#UseDefFilter').prop('checked'), $('#UseDefZone').prop('checked'), $('#AutoPutAwayToteID').prop('checked'), 
                                        $('#DefPutAwayPrior').val(), $('#DefPutAwayQuant').val(), $('#DefBatchQty').val(), $('#DefCells').val(), 
                                        $('#SplitShortPut').prop('checked'), $('#SelIfOne').prop('checked'), $('#PutInductScreen').val(), 
                                        $('#ValTotes').prop('checked'), $('#CarBatchPick').prop('checked'), $('#CarBatchPutAway').prop('checked'),
                                        $('#OffCarBatchPick').prop('checked'), $('#OffCarBatchPutAway').prop('checked'), $('#AutoForwardReplen').prop('checked'),
                                        $('#CreateItemMaster').prop('checked'), $('#SAPLocTrans').prop('checked'), $('#CreatePutAdjusts').prop('checked'), 
                                        $('#StripScan').prop('checked'), $('#StripSide').val(), $('#StripNumber').val(), $('#DefaultType').val(),
                                        $('#UseInZonePickScreen').prop('checked'), $('#AutoPrintCaseLabel').prop('checked'), $('#ShortMethod').val()).done(function (mess) {
                                            if (mess == "Error") {
                                                MessageModal("Error", "An error has occurred saving the system settings preferences")
                                            };
                                        });
};

function saveIMPrintSetts() {
    IMPrefHub.server.updIMPrintSettings($('#AutoPrintCrossDock').prop('checked'), $('#AutoPrintPick').prop('checked'), $('#PickLabelsOnePer').prop('checked'), 
                                        $('#AutoPrintPickTote').prop('checked'), $('#AutoPrintPutTote').prop('checked'),
                                        $('#AutoPrintOffCarPickList').prop('checked'), $('#AutoPrintOffCarPutList').prop('checked'),
                                        $('#AutoPrintPutLabel').prop('checked'), $('#ReqNumPutLabels').prop('checked'), $('#MaxNumPutLabels').val(), 
                                        $('#PrintDirect').prop('checked'), $('#AutoPrintPickBatchList').prop('checked')).done(function (mess) {
                                            if (mess == "Error") {
                                                MessageModal("Error", "An error has occurred saving the print setings preferences")
                                            };
                                        })
};

function saveIMMiscSetup() {
    var superBatchSize = $('#DefaultSuperBatchSize').val()
    if ($('#DefaultSuperBatchSize').val() < 2) {
        superBatchSize = 2
    }
    IMPrefHub.server.updIMMiscSetup($('#TrackInductLoc').prop('checked'), $('#InductLoc').val(), $('#StageUsingBulkPro').prop('checked'),
                                    $('#StageVelCode').val(), superBatchSize, $('#ConfirmSuperBatch').prop('checked'),
                                    $('#superBatchFilt').val()).done(function (mess) {
                                        if (mess == "Error") {
                                            MessageModal("Error", "An error has occurred saving the misc setup preferences")
                                        }
                                    });
};

function saveRTSUserData() {
    IMPrefHub.server.updRTSUserData($('#ReelUser1').val(), $('#ReelUser2').val(), $('#ReelUser3').val(), $('#ReelUser4').val(), $('#ReelUser5').val(),
                                $('#ReelUser6').val(), $('#ReelUser7').val(), $('#ReelUser8').val(), $('#ReelUser9').val(), $('#ReelUser10').val(),
                                $('#OrderNumPrefix').val()).done(function (mess) {
                                    if (mess == "Error") {
                                        MessageModal("Error", "Ann error has occurred saving the user field data")
                                    };
                                });
};
