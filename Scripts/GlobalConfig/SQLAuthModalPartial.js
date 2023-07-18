// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    $('#SQLAuthModal').on('shown.bs.modal', function (e) {
        //Get data for textboxes here
        config.server.selectConnectionUserPass($('#SQLAuthConnectionName').html()).done(function (Info) {
            $('#SQLAuthUser').val(Info.User);
            $('#SQLAuthPass').val(Info.Pass);
        });
    });


    $('#ClearLoginInfo').click(function () {
        $('#SQLAuthUser').val("");
        $('#SQLAuthPass').val("");
    });


    $('#SaveSQLAuth').click(function () {
        var user = $('#SQLAuthUser').val();
        var pass = $('#SQLAuthPass').val();

        var conf
        if (user == "" || pass == "") {
            //Windows Auth
            conf = confirm("Username and/or Password are empty. This will set this connection for Windows Authentication. Press OK to set this");
        } else {
            //SQL Auth
            conf = confirm("Username and Password are set. This will set this connection for SQL Authentication. Press OK to set this");
        };

        if (conf) {
            //Update username and passwrod to values
            config.server.updateConnectionUserPass($('#SQLAuthConnectionName').html(), user, pass).done(function (Rslt) {
                if (Rslt) {
                    $('#SQLAuthModalClose').click();
                } else {
                    alert('There was an error updating the login info for this connection');
                };
            });
        };
    });

});