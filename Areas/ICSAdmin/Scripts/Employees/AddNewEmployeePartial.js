// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022


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
    // triggers the add new employee modal
    $('#addButton').click(function () {
        deleteEmployeeAlert();
        $('#em_username').val($('#employeeLookup').val());
        $('#em_lastname').val("");
        $('#em_firstname').val("");
        $('#em_middleinitial').val("");
        $('#em_password').val("");
        $('#addemployee_modal').modal('show');
    });

    // saves a new employee from the modal
    $('#addemployee_submit').click(function () {
        var ln = $('#em_lastname').val();
        var fn = $('#em_firstname').val();
        var mi = $('#em_middleinitial').val();
        var username = $('#em_username').val();
        var access = $('#em_access').val();
        var password = $('#em_password').val();

        deleteEmployeeAlert();
        if (ln == '') {
            postEmployeeAlert('Last Name may not be left blank.');
        } else if (username == '') {
            postEmployeeAlert('Username may not be left blank.');
        } else if (password == '') {
            postEmployeeAlert('Password may not be left blank.');
        } else {
            employees.server.saveNewEmployee(ln, fn, mi, username, access, password).done(function (exists) {
                if (exists == 'Success') {
                    employeeTypeahead.clearRemoteCache();
                    $('#addemployee_modal').modal('hide');
                    //createAlert('useradd_sucess', 'User added successfully!', 'useradd_alert');
                } else if (exists == 'User already exists') {
                    postEmployeeAlert('Username already exists. Please choose a different username.');
                } else {
                    postEmployeeAlert(exists);
                };
            });
        };
    });
});

// posts an employee alert
function postEmployeeAlert(message) {
    $('#addemployee_alerts').html('<div class="alert alert-warning alert-custom" role="alert">' + message + '</div>');
};


// deletes employee alerts
function deleteEmployeeAlert() {
    $('#addemployee_alerts div.alert').remove();
};