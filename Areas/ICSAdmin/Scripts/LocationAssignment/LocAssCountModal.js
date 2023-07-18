// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#GoLocAssCount').click(function () {
        $('#LocAssCount').click();
    });

    $('#GoLocAssPick').click(function () {
        $('#LocAssPick').click();
    });

    $('#GoLocAssPutAway').click(function () {
        $('#LocAssPutAway').click();
    });
    //prints non fpz report
    $('#LocAssPrint').click(function () {
        //reportsHub.server.previewLocAssPickShort();
        //LLPreviewModal("Pick Short Preview");
        getLLPreviewOrPrint('/Admin/LocationAssignment/PreviewLocAssPickShort', { }, false,'report', 'Pick Short Preview');
    });
    //prints fpz report
    $('#LocAssPrintFPZ').click(function () {
        //reportsHub.server.previewLocAssPickShortFPZ();
        //LLPreviewModal("FPZ Preview");
        getLLPreviewOrPrint('/Admin/LocationAssignment/PreviewLocAssPickShortFPZ', { }, false,'report', 'FPZ Preview');
    });

    $('#modalBackBtn').click(function () {
        window.history.back();
    });
}); 