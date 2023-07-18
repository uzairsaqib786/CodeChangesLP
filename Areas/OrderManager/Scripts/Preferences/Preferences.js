// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var omPreferenceHub = $.connection.oMPreferencesHub;

$(document).ready(function () {
    //auto save for the order manager preferences
    var omTimer = mkTimer(function () {
        omPreferenceHub.server.updateOMPreferences($('#MaxOrdersRet').val(), $('#allowInProcOrders').prop('checked'), $('#allowIndivdOrders').prop('checked'),
                                                  $('#DefUserFields').prop('checked'), $('#CustReportsApp').val(), $('#CustReportsMenuApp').val(),
                                                  $('#CustReportsMenuText').val(), $('#PrintDirect').prop('checked')).done(function (updated) {
                                                      if (!updated) {
                                                          MessageModal("Error", "Error has occured");
                                                      };
                                                  });
    }, 600);
    //on change call auto save
    $("#OMPrefPanel").on("change input", "input[type='checkbox'], select, input[type='text']", function () {
        omTimer.startTimer();
    });
    
});