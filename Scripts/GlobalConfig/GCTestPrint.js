// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $(document.body).on('click', '.test-print', function () {
        var $this = $(this);
        var row = $this.parent().parent();
        row.find('.save-printer').click();
        var name = row.find('input.pName').val();
        var address = row.find('input.pAdd').val();
        var isLabel = row.find('.toggles').data('toggles').active;
        if (name.trim() == '' || address.trim() == '') {
            alert('Must specify name and address to print!');
        } else if (confirm('Click OK to test print.')) {
            globalHubConn.server.testPrint(name, address, isLabel);
        };
    });
});