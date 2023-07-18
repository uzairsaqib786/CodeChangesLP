// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// creates an alert in the specified area (alert ID) and clears any present there first
var createAlert = function (instanceID, message, alertID) {
    // clear any alerts from before
    $('#' + alertID).html('');
    // build the new alert
    var alert = '<div id="' + instanceID + '" class="alert alert-warning alert-dismissible alert-custom" role="alert">\
        <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>\
        <strong>Message:</strong> ' + message + '</div>';
    // make the most recent alert the top one
    $('#' + alertID).prepend(alert);
};

$(document).ready(function () {
    $('#username').focus();



    $('#username').on('keyup', function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            e.stopImmediatePropagation();
            if (this.value.trim() != '') {
                $('#password').focus();
            } else {
                this.focus();
            };
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

    // handles modal launch, modal focus and prefilled values
    $('#changePass').click(function () {
        var user = $('#username').val();

        $('#myModal').modal('show');
        $('#user_passwordChange').val(user);
        if (user == '') {
            $('#user_passwordChange').trigger('focus');
        } else {
            $('#pass_passwordChange').trigger('focus');
        };
    });

    var logonHub = $.connection.logonHub;

    $.connection.hub.start().done(function () {
        // handles the save new password submission
        $('#passwordChange_submit').click(function () {
            var user = $('#user_passwordChange').val();
            var pass = $('#password_passwordChange').val();
            var npass = $('#newPassword_passwordChange').val();
            var npass2 = $('#newPassword2_passwordChange').val();
            if (user == '') {
                createAlert('user_empty', 'Username field may not be left blank!', 'changepass_alert');
            } else if (pass == '') {
                createAlert('pass_empty', 'Old password field may not be left blank!', 'changepass_alert');
            } else if (npass == '') {
                createAlert('npass_empty', 'New password field may not be left blank!', 'changepass_alert');
            } else if (npass2 == '') {
                createAlert('npass2_empty', 'New password confirmation field may not be left blank!', 'changepass_alert');
            } else if (npass != npass2) {
                createAlert('pass_match', 'New password and confirm password fields do not match!', 'changepass_alert');
            } else if (npass == pass) {
                createAlert('pass_diff', 'New password must be different than old password!', 'changepass_alert');
            } else {
                logonHub.server.changePassword(user, pass, npass).done(function (returned) {
                    if (returned == 'Wrong Password') {
                        createAlert('pass_incorrect', 'Entered old password is incorrect!', 'changepass_alert');
                    } else if (returned == 'Password Change Failed' || returned == 'Failure') {
                        createAlert('pass_failed', 'Password change failed!  Please try again.', 'changepass_alert');
                    } else if (returned == 'Password Complexity' || returned == 'Failure') {
                        createAlert('pass_failed', 'Your password not meet the character length or complexity.', 'changepass_alert');
                    } else if (returned == 'Success') {
                        $('#myModal').modal('hide');
                        createAlert('pass_sucess', 'Password changed successfully!', 'logon_alert');
                    } else {
                        createAlert('unknown_error', 'Unknown error occurred.  Please try again.', 'changepass_alert');
                    };
                });
            };
        });

        $('.connectionName').click(function () {
            logonHub.server.setConnectionString($(this).text()).done(function () {
                window.location.href = "/"
            })
        });
    });
});
