// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var prefsHub = $.connection.wMPreferencesHub;
    //initialize typeahead for last name
    var createLastNameTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/WM/Preferences/getWMUsersTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#EditWorkRangeLastName').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });

    createLastNameTA.initialize();
    $('#EditWorkRangeLastName').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "createLastNameTA",
        displayKey: 'LastName',
        source: createLastNameTA.ttAdapter(),
        templates: {
            header: '<p style="width:34%;" class="typeahead-header">Last Name</p><p style="width:33%;" class="typeahead-header">First Name</p><p style="width:33%;" class="typeahead-header">Username</p>',
            suggestion: Handlebars.compile("<p style='width:34%;' class='typeahead-row'>{{LastName}}</p><p style='width:33%;' class='typeahead-row'>{{FirstName}}</p><p style='width:33%;' class='typeahead-row'>{{Username}}</p>")
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        if (UsedWorkerRangeEditButt == 2) {
            console.log("Not filling name", UsedWorkerRangeEditButt);
            UsedWorkerRangeEditButt = UsedWorkerRangeEditButt-1
        } else {
            fillNameData();
        };
        
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $(this).siblings('.tt-dropdown-menu').css('width', "600px");
    });

    //initialize typeahead for range name
    var createRangeNameTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/WM/Preferences/getWMLocRangeRangeNameTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#EditWorkRangeRangeName').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });

    createRangeNameTA.initialize();
    $('#EditWorkRangeRangeName').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "createRangeNameTA",
        displayKey: 'RangeName',
        source: createRangeNameTA.ttAdapter(),
        templates: {
            header: '<p style="width:34%;" class="typeahead-header">Range Name</p><p style="width:33%;" class="typeahead-header">Start Location</p><p style="width:33%;" class="typeahead-header">End Location</p>',
            suggestion: Handlebars.compile("<p style='width:34%;' class='typeahead-row'>{{RangeName}}</p><p style='width:33%;' class='typeahead-row'>{{StartLocation}}</p><p style='width:33%;' class='typeahead-row'>{{EndLocation}}</p>")
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        if (UsedWorkerRangeEditButt == 1) {
            console.log("Not filling range", UsedWorkerRangeEditButt);
            UsedWorkerRangeEditButt = UsedWorkerRangeEditButt - 1
        } else {
            fillRangeData();
        };

    }).on('typeahead:opened', function () {
        var $this = $(this);
        $(this).siblings('.tt-dropdown-menu').css('width', "600px");
    });
    
    //updates the selected work range witht he given setup and redraws the table
    $('#EditWorkRangeSave').click(function () {
        //check for no empty values
        if ($('#EditWorkRangeUsername').val() == "" || $('#EditWorkRangeFirstName').val() == "" || $('#EditWorkRangeLastName').val() == "" ||
            $('#EditWorkRangeRangeName').val() == "" || $('#EditWorkRangeStartLocation').val() == "" || $('#EditWorkRangeEndLocation').val() == "") {
            MessageModal("Warning", "All fields must be filled out in order to update this range")
        } else {
            prefsHub.server.updateUserRange(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[0], $('#EditWorkRangeUsername').val(),
                                                                  $('#EditWorkRangeFirstName').val(), $('#EditWorkRangeLastName').val(),
                                                                  $('#EditWorkRangeRangeName').val(), $('#EditWorkRangeStartLocation').val(),
                                                                  $('#EditWorkRangeEndLocation').val(), $('#EditWorkRangeActive').val()).done(function (mess) {
                                                                      if (!mess) {
                                                                          MessageModal("Error", "An error has occurred updating the desired range")
                                                                      } else {
                                                                          $('#EditWorkRangeUsername').val("");
                                                                          $('#EditWorkRangeFirstName').val("");
                                                                          $('#EditWorkRangeLastName').val("");
                                                                          $('#EditWorkRangeRangeName').val("");
                                                                          $('#EditWorkRangeStartLocation').val("");
                                                                          $('#EditWorkRangeEndLocation').val("");
                                                                          WorkerRangesTable.draw();
                                                                      };
                                                                  });
        };
    });
});
//gets the data of a user based on the last name field selected 
function fillNameData() {
    //this will fill out the name data when the last name typeahead is selected
    prefsHub.server.getWMUserData($('#EditWorkRangeLastName').val()).done(function (data) {
        $('#EditWorkRangeFirstName').val(data[0].FirstName);
        $('#EditWorkRangeUsername').val(data[0].Username);
    });
};

//gets the data of a range based on the range name thatw as selected
function fillRangeData() {
    //this will fill out the range data when a range name typeahead is selected
    prefsHub.server.getWMLocRangeData($('#EditWorkRangeRangeName').val()).done(function (data) {
        $('#EditWorkRangeStartLocation').val(data[0].StartLocation);
        $('#EditWorkRangeEndLocation').val(data[0].EndLocation);
    });
};