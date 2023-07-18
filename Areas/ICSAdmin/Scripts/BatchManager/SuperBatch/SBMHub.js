// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

function clearSuperBatches() {
    bmHub.server.clearSuperBatches().done(function () {
        leftSBMTable.draw();
        rightSBMTable.draw();
    });
};

function createSuperBatch() {
    bmHub.server.createSuperBatch($('#Priorities').val(), $('#Groups').val()).done(function () {
        leftSBMTable.draw();
        rightSBMTable.draw();
    });
};