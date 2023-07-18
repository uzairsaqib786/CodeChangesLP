// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022


$(document).ready(function () {
    var bulkTimer = mkTimer(function () {
        var settings = [
            $('#pickScanVerifyLocation').data('toggles').active,
            $('#allowCloseBox').data('toggles').active,
            $('#pickScanVerifyItemNumber').data('toggles').active,
            $('#printOrderManifest').data('toggles').active,
            $('#pickScanVerifyTote').data('toggles').active,
            $('#printToteManifest').data('toggles').active,
            $('#showCount').data('toggles').active,
            $('#demandToteID').data('toggles').active,
            $('#countScanVerifyLocation').data('toggles').active,
            $('#dynamicSpacing').data('toggles').active,
            $('#countScanVerifyItemNumber').data('toggles').active,
            $('#completeShortPicks').data('toggles').active,
            $('#putScanVerifyLocation').data('toggles').active,
            $('#putScanVerifyItemNumber').data('toggles').active,
            $('#countIfShort').data('toggles').active,
            $('#printPickLabels').data('toggles').active,
            $('#printPutLabels').data('toggles').active,
            $('#sapAdjustment').data('toggles').active,
            $('#HPARequestSupplier').data('toggles').active,
            $('#dynamicWH').data('toggles').active,
            $('#DirectedHPA').data('toggles').active,
            $('#allowHP').data('toggles').active,
            $('#pickSequenceSort').data('toggles').active,
            $('#printNextToteLabel').data('toggles').active,
            $('#containerLogic').data('toggles').active,
            $('#putAwayBatch').data('toggles').active,
            $('#useF2TaskComplete').data('toggles').active,
            $('#changeInvMastCellSizeonHPA').data('toggles').active,
            $('#HPAShowEmptyLocs').data('toggles').active,
            $('#countIfLocEmptied').data('toggles').active,
            $('#combineSameIMIDPick').data('toggles').active,
            $('#CarouselPutAway').data('toggles').active,
            $('#ConsolidationDelivery').data('toggles').active,
            $('#ShortPickFNL').data('toggles').active,
            $('#zoneChoice').data('toggles').active,
            $('#consolidationStaging').data('toggles').active,
            $('#useInventoryCaseQuantity').data('toggles').active,
            $('#combineOTTrans').data('toggles').active,
            $('#allowSuperBatch').data('toggles').active,
            $('#singleLineSuperBatch').data('toggles').active,
            $('#HPAShowVelocityCode').data('toggles').active,
            $('#pickScanVerifyQuantity').data('toggles').active,
            $('#hotMoveMultipleItems').data('toggles').active

    ];

        preferencesHub.server.saveBulkSettings(settings);
    }, 600);

    $('#checkBulkSettings, #uncheckBulkSettings').click(function () {
        var state = $(this).attr('id') == 'checkBulkSettings';
        $('#bpsettings .toggles').each(function (index, element) {
            $(this).data('toggles').toggle(state);
        });
    });

    $('#bpsettings .toggles').on('toggle', function (e, checked) {
        bulkTimer.startTimer();
    });
});

function getBulkSettings() {
    preferencesHub.server.getBulkSettings().done(function (bulkSettings) {

        $('#pickScanVerifyLocation').data('toggles').setValue(bulkSettings[0]),
        $('#allowCloseBox').data('toggles').setValue(bulkSettings[1]),
        $('#pickScanVerifyItemNumber').data('toggles').setValue(bulkSettings[2]),
        $('#printOrderManifest').data('toggles').setValue(bulkSettings[3]),
        $('#pickScanVerifyTote').data('toggles').setValue(bulkSettings[4]),
        $('#printToteManifest').data('toggles').setValue(bulkSettings[5]),
        $('#showCount').data('toggles').setValue(bulkSettings[6]),
        $('#demandToteID').data('toggles').setValue(bulkSettings[7]),
        $('#countScanVerifyLocation').data('toggles').setValue(bulkSettings[8]),
        $('#dynamicSpacing').data('toggles').setValue(bulkSettings[9]),
        $('#countScanVerifyItemNumber').data('toggles').setValue(bulkSettings[10]),
        $('#completeShortPicks').data('toggles').setValue(bulkSettings[11]),
        $('#putScanVerifyLocation').data('toggles').setValue(bulkSettings[12]),
        $('#putScanVerifyItemNumber').data('toggles').setValue(bulkSettings[13]),
        $('#countIfShort').data('toggles').setValue(bulkSettings[14]),
        $('#printPickLabels').data('toggles').setValue(bulkSettings[15]),
        $('#printPutLabels').data('toggles').setValue(bulkSettings[16]),
        $('#sapAdjustment').data('toggles').setValue(bulkSettings[17]),
        $('#HPARequestSupplier').data('toggles').setValue(bulkSettings[18]),
        $('#dynamicWH').data('toggles').setValue(bulkSettings[19]),
        $('#DirectedHPA').data('toggles').setValue(bulkSettings[20]),
        $('#allowHP').data('toggles').setValue(bulkSettings[21]),
        $('#pickSequenceSort').data('toggles').setValue(bulkSettings[22]),
        $('#printNextToteLabel').data('toggles').setValue(bulkSettings[23]),
        $('#containerLogic').data('toggles').setValue(bulkSettings[24]),
        $('#CarouselPutAway').data('toggles').setValue(bulkSettings[25]),
        $('#putAwayBatch').data('toggles').setValue(bulkSettings[26]),
        $('#useF2TaskComplete').data('toggles').setValue(bulkSettings[27]),
        $('#changeInvMastCellSizeonHPA').data('toggles').setValue(bulkSettings[28]),
        $('#HPAShowEmptyLocs').data('toggles').setValue(bulkSettings[29]),
        $('#countIfLocEmptied').data('toggles').setValue(bulkSettings[30]),
        $('#combineSameIMIDPick').data('toggles').setValue(bulkSettings[31]),
        $('#ConsolidationDelivery').data('toggles').setValue(bulkSettings[32]),
        $('#ShortPickFNL').data('toggles').setValue(bulkSettings[33])
        $('#zoneChoice').data('toggles').setValue(bulkSettings[34])
        $('#consolidationStaging').data('toggles').setValue(bulkSettings[35])
        $('#useInventoryCaseQuantity').data('toggles').setValue(bulkSettings[36])
        $('#combineOTTrans').data('toggles').setValue(bulkSettings[37])
        $('#allowSuperBatch').data('toggles').setValue(bulkSettings[38])
        $('#singleLineSuperBatch').data('toggles').setValue(bulkSettings[39])
        $('#HPAShowVelocityCode').data('toggles').setValue(bulkSettings[40])
        $('#pickScanVerifyQuantity').data('toggles').setValue(bulkSettings[41])
        $('#hotMoveMultipleItems').data('toggles').setValue(bulkSettings[42])

    });
};