// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var CrossDockFilter = {};
var TransFilter = {};

$(document).ready(function () {
    // custom filtering for the transaction list
    TransFilter = new FilterMenu({
        Selector: '#trans_container',
        NumberSelector: '#trans_container .qty',
        TextSelector: '#trans_container .input-sm:not(.qty)'
    });

    // reset the page plugin because we've changed the filter
    $('#trans_container').on('filterChange', function () {
        pager.currentPage = 0;
        pager.populate();
    });

    // clear the filter on the transaction plugin instance
    $('#TransClearFilter').click(function () {
        TransFilter.clearFilter();
    });

    // custom filtering for the cross dock transaction list
    CrossDockFilter = new FilterMenu({
        Selector: '#cd_container',
        NumberSelector: '#cd_container .cd-tqty, #cd_container .cd-cqty, #cd_container .cd-priority',
        TextSelector: '#cd_container .cd-tote, #cd_container .cd-htid, #cd_container .cd-order',
        DateSelector: '#cd_container .cd-import, #cd_container .cd-required'
    });

    // reset the page plugin because we've changed the filter
    $('#cd_container').on('filterChange', function () {
        cd_pager.currentPage = 0;
        cd_pager.populate();
    });

    // clear the filter on the cross dock plugin
    $('#CDClearFilter').click(function () {
        CrossDockFilter.clearFilter();
    });
});