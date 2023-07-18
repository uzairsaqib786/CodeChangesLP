// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    addResize(function () { $('#STPagingExample').css('max-height', $(window).height() * 0.58); });

    var filterPlugin = new FilterMenu({
        Selector: '#STPagingExample',
        NumberSelector: '.num',
        TextSelector: '.text'
    });
    
    var pager = new STPaging({
        isTable: false,
        PerPage: 5,
        ID: "#STPagingExample",
        RowSelector: '.row',
        Source: globalHubConn.server.getSTPageExample,
        RowFunction: function (row) {
            var r = '<div class="row">';
            var labels = ['Batch Pick ID', 'Transaction Quantity', 'Item Number', 'Transaction Type'];
            var classes = ['text', 'num', 'text', 'text'];
            for (var x = 0; x < row.length; x++) {
                r += '<div class="col-md-4"><label>' + labels[x] + '</label><input data-colname="[' + labels[x] + ']" type="text" class="form-control input-sm ' + classes[x] + '" disabled="disabled" value="' + row[x] + '" /></div>';
            };
            r += '<div class="col-md-4"><label style="visibility:hidden;">Do something</label><button class="btn btn-primary btn-block">Do Something</button></div><div class="col-md-4"><label style="visibility:hidden;">Do something</label><button class="btn btn-primary btn-block">Do Something Else</button></div></div>';
            return r;
        },
        NextID: '#Next',
        PrevID: '#Prev',
        getExtraParams: function () {
            return filterPlugin.getFilterString()
        },
        PageID: '#Page',
        processExtraData: function (result) {
            if (!result.success) {
                MessageModal('Error', 'There was an error while trying to get the next page of transactions.  Check the error log for details.');
            } else {
                $('#container').find('.row:first').addClass('active-div');
            };
        },
        waitInterval: 500,
        perPageSelector: '#STPerPage'
    });
    
    // pager SignalR function was triggering too early, so we check the gv hubStarted in GlobalHub.js continually until we're connected and then clear the check.
    var hubCheck = setInterval(function () {
        if (hubStarted) {
            pager.init();
            pager.populate(0);
            clearInterval(hubCheck);
        };
    }, 100);

    $(pager.ID).on('filterChange', function () {
        pager.populate(0);
    });

    $('#Clear').click(function () {
        filterPlugin.clearFilter();
    });
});