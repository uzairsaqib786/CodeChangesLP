// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var holdsender;
    $('#holdSelected').hide();
    /*
        Handles launching the hold transactions modal
    */
    $('#holdSelected, #holdBy').click(function () {
        var $this = $(this);
        holdsender = $this.attr('id') == 'holdSelected' ? 'selected' : 'all';
        if (holdsender == 'selected' && $('#holdTransactionsTable tr.active').length == 0) {
            return false;
        } else if ($('#ItemOrder').val().trim() == '') {
            return false;
        } else {
            $('#deallocate_modal').modal('show');
        };
    });

    /*
        Handles deallocation call from the modal submit button
    */
    $('#deallocate_submit').click(function () {
        var reason = $('#deallocate_textarea');
        if (reason.val().trim() == '') {
            $('#deallocate_alerts').html('<div class="alert alert-warning alert-custom" role="alert">A reason for the deallocation must be specified!</div>');
        } else {
            $('#deallocate_alerts').html('');
            var itemorder = $('#ItemOrder');
            var id = $('#holdTransactionsTable tr.active').find('td:last').text();
            if (id == null || id.trim() == '' || holdsender == 'all') {
                id = 0;
            };
            var reel = $('[name="reel"]:checked').val();
            holdTransHub.server.deallocateTransactions(reel, itemorder.val(), $('#EntryType').val() == 'Order Number', reason.val(), id).done(function () {
                holdTransTable.draw();
                httypeahead.clearRemoteCache();
                $('#deallocate_modal').modal('hide');
            });
        };
    });

    /*
        Toggles active class on a row in the hold transactions table for selecting datas
    */
    $(document.body).on('click', '#holdTransactionsTable tr', function () {
        var $this = $(this);
        if ($this.hasClass('active')) {
            $this.removeClass('active');
            $('#holdSelected').hide();
        } else {
            $('tr.active').removeClass('active');
            $this.addClass('active');
            $('#holdSelected').show();
        };
    });
});