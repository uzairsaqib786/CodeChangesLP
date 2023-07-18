// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#username').focus();

    $('#LogonForm').submit(function (e, submitIt) {
        if (submitIt == null) {
            e.preventDefault();
        };
    });

    $('#username').on('keyup', function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            e.stopImmediatePropagation();
            if (this.value.trim() != '') {
                $('#password').focus();
            } else {
                this.focus();
            };
            return false;
        };
    });

    $('#password').on('keyup', function (e) {
        var password = $(this), username = $('#username');
        if (e.keyCode == 13) {
            e.preventDefault();
            if (password.val().trim() == '') {
                password.focus();
            } else if (username.val().trim() == '') {
                username.focus();
            } else {
                $('#LogonForm').trigger('submit', true);
            };
        };
    });

    $('#LogonSubmit').click(function () {
        $('#LogonForm').trigger('submit', true);
    });
});