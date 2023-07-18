// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var prefsHub = $.connection.wMPreferencesHub;
    var id = ""
    var RangeNameFocus = "";
    //initialize typeahead for star location
    var createStartLocTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/WM/Preferences/getStartLocRangeTA?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#LocRangeStart').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });

    createStartLocTA.initialize();
    $('#LocRangeStart').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "createStartLocTA",
        displayKey: 'StartLoc',
        source: createStartLocTA.ttAdapter(),
        templates: {
            header: '<p style="width:34%;" class="typeahead-header">Start Location</p><p style="width:33%;" class="typeahead-header">Type</p><p style="width:33%;" class="typeahead-header">Location</p>',
            suggestion: Handlebars.compile("<p style='width:34%;' class='typeahead-row'>{{StartLoc}}</p><p style='width:33%;' class='typeahead-row'>{{Type}}</p><p style='width:33%;' class='typeahead-row'>{{Location}}</p>")
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $(this).siblings('.tt-dropdown-menu').css('width', $this.css('width'));
    });
    //initialize typeahead for end location
    var createEndLocTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/WM/Preferences/getEndLocRangeTA?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#LocRangeEnd').val() + "&StartRange=" + $('#LocRangeStart').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });

    createEndLocTA.initialize();
    $('#LocRangeEnd').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "createEndLocTA",
        displayKey: 'EndLoc',
        source: createEndLocTA.ttAdapter(),
        templates: {
            header: '<p style="width:34%;" class="typeahead-header">End Location</p><p style="width:33%;" class="typeahead-header">Type</p><p style="width:33%;" class="typeahead-header">Location</p>',
            suggestion: Handlebars.compile("<p style='width:34%;' class='typeahead-row'>{{EndLoc}}</p><p style='width:33%;' class='typeahead-row'>{{Type}}</p><p style='width:33%;' class='typeahead-row'>{{Location}}</p>")
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $(this).siblings('.tt-dropdown-menu').css('width', $this.css('width'));
    });

    //opens modal on button that ahs edit-modal class
    //depding on the id either fill modal or keep empty
    $(document.body).on('click', '.edit-modal', function () {
        $('#LocRangeAddEditModal').modal('show');
        id = this.id;
        if (id == "AddLocationRange") {
            $('#LocRangeAddEditLabel').html("Add New Location Range");
            $('#LocRangeName').val("");
            $('#LocRangeStart').val("");
            $('#LocRangeEnd').val("");
        } else if (id == "EditLocationRange") {
            $('#LocRangeAddEditLabel').html("Edit Location Range");
            $('#LocRangeName').val(LocationRangesTable.row($('#LocationRangesTable tbody tr.active')[0]).data()[1]);
            $('#LocRangeMultiWorker').val(LocationRangesTable.row($('#LocationRangesTable tbody tr.active')[0]).data()[4]);
            $('#LocRangeActive').val(LocationRangesTable.row($('#LocationRangesTable tbody tr.active')[0]).data()[5]);

            $('#LocRangeStart').val(LocationRangesTable.row($('#LocationRangesTable tbody tr.active')[0]).data()[2]);
            $('#LocRangeStart').trigger('input');
            $('#LocRangeStart').typeahead('close');

            $('#LocRangeEnd').val(LocationRangesTable.row($('#LocationRangesTable tbody tr.active')[0]).data()[3]);
            $('#LocRangeEnd').trigger('input');
            $('#LocRangeEnd').typeahead('close');
        } else if (id == "EditLocRangeOW") {
            $('#LocRangeAddEditLabel').html("Edit Location Range");
            console.log(OrganizeWorkTable.row($('#OrganizeWorkTable tbody tr.active')[0]).data()[11])

            $('#LocRangeName').val(OrganizeWorkTable.row($('#OrganizeWorkTable tbody tr.active')[0]).data()[1]);
            $('#LocRangeMultiWorker').val(OrganizeWorkTable.row($('#OrganizeWorkTable tbody tr.active')[0]).data()[10]);
            $('#LocRangeActive').val(OrganizeWorkTable.row($('#OrganizeWorkTable tbody tr.active')[0]).data()[11]);

            $('#LocRangeStart').val(OrganizeWorkTable.row($('#OrganizeWorkTable tbody tr.active')[0]).data()[2]);
            $('#LocRangeStart').trigger('input');
            $('#LocRangeStart').typeahead('close');

            $('#LocRangeEnd').val(OrganizeWorkTable.row($('#OrganizeWorkTable tbody tr.active')[0]).data()[3]);
            $('#LocRangeEnd').trigger('input');
            $('#LocRangeEnd').typeahead('close');
        };
    });

    
    //check location range name 
    $('#LocRangeName').focus(function () {
        RangeNameFocus = $(this).val();
    });
    $('#LocRangeName').blur(function () {
        if ($('#LocRangeName').val() != RangeNameFocus) {
            //confirm that the range name does not exist already
            prefsHub.server.verifyLocationRangeName($('#LocRangeName').val()).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred checking the new range name");
                } else if (mess == "Conflict") {
                    MessageModal("Warning", "The range name " + $('#LocRangeName').val() + " already exists");
                    $('#LocRangeName').val(RangeNameFocus);
                };
            });
        };
    });

    //handles adding or updating desired location range after finsishing will redraw designated table by the id
    $('#AddEditLocRangeSave').click(function () {
        if (id == "AddLocationRange") {
            if ($('#LocRangeName').val() == "" || $('#LocRangeStart').val() == "" || $('#LocRangeEnd').val() == "") {
                MessageModal("Warning", "One of the fields are empty. PLease fill in all the fields");
            } else {
                prefsHub.server.createLocationRange($('#LocRangeName').val(), $('#LocRangeStart').val(), $('#LocRangeEnd').val(),
                                                $('#LocRangeMultiWorker').val(), $('#LocRangeActive').val()).done(function (mess) {
                                                    if (!mess) {
                                                        MessageModal("Error", "An error has occurred adding the new location range")
                                                    } else {
                                                        LocationRangesTable.draw();
                                                        $('#EditLocationRange').attr('disabled', 'disabled');
                                                    };
                                                });
            };
        } else if (id == "EditLocationRange") {
            if ($('#LocRangeName').val() == "" || $('#LocRangeStart').val() == "" || $('#LocRangeEnd').val() == "") {
                MessageModal("Warning", "One of the fields are empty. PLease fill in all the fields");
            } else {
                prefsHub.server.updateLocationRange(LocationRangesTable.row($('#LocationRangesTable tbody tr.active')[0]).data()[0],
                                                $('#LocRangeName').val(), $('#LocRangeStart').val(), $('#LocRangeEnd').val(),
                                                $('#LocRangeMultiWorker').val(), $('#LocRangeActive').val()).done(function (mess) {
                                                    if (!mess) {
                                                        MessageModal("Error", "An error has occurred updating this location range")
                                                    } else {
                                                        LocationRangesTable.draw();
                                                        $('#EditLocationRange').attr('disabled', 'disabled');
                                                    };
                                                });
            };
        } else if (id == "EditLocRangeOW") {
            if ($('#LocRangeName').val() == "" || $('#LocRangeStart').val() == "" || $('#LocRangeEnd').val() == "") {
                MessageModal("Warning", "One of the fields are empty. PLease fill in all the fields");
            } else {
                prefsHub.server.updateLocationRange(OrganizeWorkTable.row($('#OrganizeWorkTable tbody tr.active')[0]).data()[0],
                                                $('#LocRangeName').val(), $('#LocRangeStart').val(), $('#LocRangeEnd').val(),
                                                $('#LocRangeMultiWorker').val(), $('#LocRangeActive').val()).done(function (mess) {
                                                    if (!mess) {
                                                        MessageModal("Error", "An error has occurred updating this location range")
                                                    } else {
                                                        OrganizeWorkTable.draw();
                                                        getTotalCount();
                                                        $('#EditLocRangeOW').attr('disabled', 'disabled');
                                                    };
                                                });
            };
        };
        
    });
});