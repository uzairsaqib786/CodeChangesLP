// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

function changeConfigString() {


    if ($("input[name*='ServerName']").val().trim() == "" || $("input[name*='DBName']").val().trim() == "") {
        MessageModal("Empty Fields", "Please make sure the database and server text fields are not empty");
        return;
    };

    var Conf1 = confirm("Do you want to proceed with changing the config database? Press OK to continue with this change.");

    if (Conf1) {
        var Conf2 = confirm("You are changing the sql connection to now point to server: " + $("input[name*='ServerName']").val() + " and database: " + $("input[name*='DBName']").val() + ". Press OK to proceed with this change.");

        if (Conf2) {

            if (confirm("This is the final confirmation you want to proceed. Pressing OK will change the config connection string. If the values are incorrect this will cause issues within the website. Press OK to finalize this change.")) {
                console.log('hit me again')
                document.getElementById('dbConfigForm').submit();
            };
        };
    };

};


$(document).ready(function () {
    $('input[name="ServerName"], input[name="DBName"]').on('input', function () {
        var server = $('input[name="ServerName"]').val();
        var DB = $('input[name="DBName"]').val();
        if(server == '' || DB == ''){
            $('#SubmitConfig').attr('disabled', 'disabled')
        }
        else {
            $('#SubmitConfig').removeAttr('disabled')
        }
    })
})
