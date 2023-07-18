// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    $('#PrintReplenLabelAll').click(function () {
        title = 'Replenishment Labels';
        getLLPreviewOrPrint('/Admin/SystemReplenishment/printReplenishmentReportLabels', {
            searchString: $('#curOrderSearch').val(),
            searchCol: $('#curOrdersColumns').val(),
            status: $('#status').val(),
            ident: 'Labels',
            PrintAll: 1
        }, true,'label', title)
    });

    $('#PrintReplenLabelNotPrinted').click(function () {
        title = 'Replenishment Labels';
        getLLPreviewOrPrint('/Admin/SystemReplenishment/printReplenishmentReportLabels', {
            searchString: $('#curOrderSearch').val(),
            searchCol: $('#curOrdersColumns').val(),
            status: $('#status').val(),
            ident: 'Labels',
            PrintAll: 0
        }, true,'label', title)
    });

    $('#PrintReplenLabelFiltered').click(function () {
        title = 'Replenishment Labels';
        getLLPreviewOrPrint('/Admin/SystemReplenishment/printReplenishmentReportLabels', {
            searchString: $('#curOrderSearch').val(),
            searchCol: $('#curOrdersColumns').val(),
            status: $('#status').val(),
            ident: 'Labels',
            filter: (SystReplenCurrFilterMen == "" ? "" : SystReplenCurrFilterMen.getFilterString()),
            PrintAll: 2,
            Sort: $('#CurrDispSort').val()
        }, true, 'label', title)
    });


});