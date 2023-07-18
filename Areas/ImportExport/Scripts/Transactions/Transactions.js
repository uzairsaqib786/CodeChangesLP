// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var importExportHub = $.connection.importExportHub;

$(document).ready(function () {
    var dtTimer = mkTimer(function () {
        dt[$('#TableSelect').val()].draw();
    }, 200);

    $('#TableSelect').change(function () {
        var $this = $(this);
        showHide($this.val());
        dtTimer.startTimer();
    });

    $('#SDate,#EDate').on('input', function () {
        dtTimer.startTimer();
    });

    $('#DeleteTrans').click(function () {
        var $this = $(this);
        var type = $('#TableSelect').val();
        if (confirm('Click OK to delete current records from ' + type + '.')) {
            $this.attr('disabled', 'disabled');
            importExportHub.server.delIETransactions(type).done(function (result) {
                $this.removeAttr('disabled');
                if (!result) MessageModal('Error', 'An error occurred while attempting to delete from ' + type + '.  Check the error log for details');
                dtTimer.startTimer();
            });
        };
    });

    function showHide(table) {
        switch (table.toLowerCase()) {
            case 'import transactions':
                $('#DeleteTrans').text('Delete Import Transactions').show();
                break;
            case 'export transactions':
                $('#DeleteTrans').text('Delete Export Transactions').show();
                break;
            case 'import transactions history':
            case 'archive transaction history':
                $('#DeleteTrans').hide();
                break;
        };
        $('[data-table-div]').hide();
        $('[data-table-div="' + table.toTitleCase() + '"]').show();
    };
});