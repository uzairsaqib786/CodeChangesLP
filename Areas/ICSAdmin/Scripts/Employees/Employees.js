// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var employeeTypeahead;
var controlNameTypeahead;
var groupNameTypeahead;
var employees = $.connection.employeesHub;

/*
    Swaps selector's classes from toRemove to toAdd
*/
var swapClass = function (selector, toRemove, toAdd) {
    selector.removeClass(toRemove);
    selector.addClass(toAdd);
};

$(document).ready(function () {
    $('#maxOrders').on('input, focusout', function (e) {
        if (e.type == 'input') {
            setNumericInRange($(this), 0, SqlLimits.numerics.smallint.max);
        } else if (this.value.trim() == '') {
            this.value = 0;
        };
    });

    // clears all fields
    var clearAllFields = function () {
        $('#saveButton').attr('disabled', 'disabled');
        $('#firstName').val("").attr('disabled', 'disabled');
        $('#lastName').val("").attr('disabled', 'disabled');
        $('#MI').val("").attr('disabled', 'disabled');
        $('#userName').val("").attr('disabled', 'disabled');
        $('#password').val("").attr('disabled', 'disabled');
        $('#accessLevel').val("").attr('disabled', 'disabled');
        $('#group').val("").attr('disabled', 'disabled');
        $('#email').val("").attr('disabled', 'disabled');
        $('#maxOrders').val("").attr('disabled', 'disabled');
        $('#active').removeAttr("Checked").attr('disabled', 'disabled');
        $('#functions').html("");
        $('#userGroups').html("");
        $('#zonesAppend').html("");
        $('#locationsAppend').html("");
        $('#pickLevelsAppend').html("");
        $('#addAllControls').attr('disabled', 'disabled');
        $('#addNewFunction').attr('disabled', 'disabled');
        $('#addNewGroup').attr('disabled', 'disabled');
        $('#addPickLevel').attr('disabled', 'disabled');
        $('#addNewZone').attr('disabled', 'disabled');
        $('#addLocation').attr('disabled', 'disabled');
        $('#deleteButton').attr('disabled', 'disabled');
    };

    // creates the employee typeahead suggestion engine
    employeeTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/Admin/Employees/employeeLookup?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });

    employeeTypeahead.initialize();

    // initialize typeahead
    $('#employeeLookup').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "employeeTypeahead",
        displayKey: 'lastName',
        source: employeeTypeahead.ttAdapter(),
        templates: {
            header: '<p style="width:25%;" class="typeahead-header">Last Name</p><p style="width:25%;" class="typeahead-header">First Name</p><p style="width:25%;" class="typeahead-header">MI</p><p style="width:25%;" class="typeahead-header">Username</p>',
            suggestion: Handlebars.compile("<p style='width:25%;' class='typeahead-row'>{{lastName}}</p><p style='width:25%;' class='typeahead-row'>{{firstName}}</p><p style='width:25%;' class='typeahead-row'>{{mInitial}}</p><p style='width:25%;' class='typeahead-row'>{{userName}}</p>")
        }
    }).on('typeahead:selected', function (obj, data, name) {
        var typeahead = $('.forced-typeahead');
        var input = typeahead.find('input');
        input.tooltip('destroy');
        swapClass(typeahead, 'has-warning', 'has-success');
        swapClass(typeahead.find('span.glyphicon'), 'glyphicon-warning-sign', 'glyphicon-ok');

        clearAllFields();
        $('#addNewZone').removeAttr('disabled');
        $('#addLocation').removeAttr('disabled');
        $('#addPickLevel').removeAttr('disabled');
        $('#addAllControls').removeAttr('disabled');
        $('#addNewFunction').removeAttr('disabled');
        $('#addNewGroup').removeAttr('disabled');
        $('#deleteButton').removeAttr('disabled');
        $('#firstName').val(data.firstName).removeAttr('disabled');
        $('#lastName').val(data.lastName).removeAttr('disabled');
        $('#MI').val(data.mInitial).removeAttr('disabled');
        $('#userName').val(data.userName)
        $('#password').val(data.password).removeAttr('disabled');
        $('#accessLevel').val(data.access).removeAttr('disabled');
        $('#group').val(data.group).removeAttr('disabled');
        $('#email').val(data.email).removeAttr('disabled');
        $('#maxOrders').val(data.maxOrders).removeAttr('disabled');
        if (data.active) {
            $('#active').attr("Checked", "Checked").removeAttr('disabled');
        }
        else {
            $('#active').removeAttr("Checked").removeAttr('disabled');
        }
        employees.server.getEmployeeData(data.userName).done(function (retData) {
            retData.functionsAllowed.forEach(function (current) {
                $('#functions').append('<div class="row"><div class="col-md-11"><input disabled type="text" class="form-control" maxlength="50" value="' + current + '"/></div><div class="col-md-1"><button style="margin-bottom:5px;" class="btn btn-danger pull-right deleteControl"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
            });
            retData.groupsAllowed.forEach(function (current) {
                $('#userGroups').append('<div class="row"><div class="col-md-11"><input disabled type="text" class="form-control" maxlength="50" value="' + current + '"/></div><div class="col-md-1"><button style="margin-bottom:5px;" class="btn btn-danger pull-right deleteGroup"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
            });
            retData.employeePickLevels.forEach(function (current) {
                $('#pickLevelsAppend').append('<div class="row"><div class="col-xs-2 "><input disabled type="text" class="form-control" maxlength="50" value="' + current.Item1 + '"/></div><div class="col-xs-2"><input id="' + current.Item6 + '_sCar" maxlength="2" type="text" class="form-control carInput" value="' + current.Item2 + '"/></div><div class="col-xs-2"><input id="' + current.Item6 + '_eCar" maxlength="2" type="text" class="form-control carInput" value="' + current.Item3 + '"/></div><div class="col-xs-2"><input id="' + current.Item6 + '_sShelf" maxlength="2" type="text" class="form-control shelfInput" value="' + current.Item4 + '"/></div><div class="col-xs-2"><input id="' + current.Item6 + '_eShelf" maxlength="2" type="text" class="form-control shelfInput" value="' + current.Item5 + '"/></div><div class="col-xs-1"><button id="' + current.Item6 + '_save" disabled class="btn btn-primary shelfSave" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-floppy-disk"></span></button></div><div class="col-xs-1"><button id="' + current.Item6 + '_delete" class="btn btn-danger shelfDelete" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
            });
            retData.bulkProZones.forEach(function (current) {
                var appendString = '<div class="row"><div class="col-xs-9"><select id="' + current + '_zone" class="form-control zoneFill">'
                retData.allZones.forEach(function (currentZ) {
                    if (currentZ == current) {
                        appendString += '<option selected value="' + currentZ + '">' + currentZ + '</option>';
                    } else {
                        appendString += '<option value="' + currentZ + '">' + currentZ + '</option>';
                    };
                });
                appendString += '</select></div><div class="col-xs-1"><button id="' + current + '_save" disabled class="btn btn-primary zoneSave" style="margin-bottom:5px;"><span class="glyphicon glyphicon-floppy-disk"></span></button></div><div class="col-xs-1"><button id="' + current + '_delete" class="btn btn-danger zoneDelete" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-remove"></span></button></div></div>';
                $('#zonesAppend').append(appendString)
            });
            retData.bulkProRangeAssign.forEach(function (current) {
                $('#locationsAppend').append('<div class="row"><div class="col-xs-5"><div class="form-group has-feedback" style="margin-bottom:0px;"><input class="form-control modal-launch-style slocation-modal" readonly type="text" value="' + current.Item1 + '" name="' + current.Item1 + '" /><i class="glyphicon glyphicon-resize-full form-control-feedback slocation-modal modal-launch-style" style="top:0px;"></i></div></div><div class="col-xs-5"><div class="form-group has-feedback" style="margin-bottom:0px;"><input class="form-control modal-launch-style elocation-modal" readonly type="text" value="' + current.Item2 + '" name="' + current.Item2 + '" /><i class="glyphicon glyphicon-resize-full form-control-feedback elocation-modal modal-launch-style" style="top:0px;"></i></div></div><div class="col-xs-1"><button class="btn btn-danger remove-locationrange" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
            });
            $('#employeeInfo').show();
        });
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "600px");
    });
    // control name suggestion engine
    controlNameTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 60,
        remote: {
            url: ('/Admin/Employees/getControlNames?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });

    controlNameTypeahead.initialize();

    // control name typeahead
    $('#controlNameTypeahead').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "controlNameTypeahead",
        displayKey: 'Item1',
        source: controlNameTypeahead.ttAdapter(),
        templates: {
            header: '<p style="width:50%;" class="typeahead-header">Control Name</p><p style="width:50%;" class="typeahead-header">Function</p>',
            suggestion: Handlebars.compile("<p style='width:50%;' class='typeahead-row'>{{Item1}}</p><p style='width:50%;' class='typeahead-row'>{{Item2}}</p>")
        }
    }).on('typeahead:selected', function (obj, data, name) {

    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "700px");
        });

    // group name suggestion engine
    groupNameTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 60,
        remote: {
            url: ('/Admin/Employees/getGroupNames?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });

    groupNameTypeahead.initialize();

    // control name typeahead
    $('#groupNameTypeahead').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
            name: "groupNameTypeahead",
            displayKey: 'Item1',
            source: groupNameTypeahead.ttAdapter(),
            templates: {
                header: '<p style="width:50%;" class="typeahead-header">Group Name</p>',
                suggestion: Handlebars.compile("<p style='width:100%;' class='typeahead-row'>{{Item1}}</p>")
            }
        }).on('typeahead:selected', function (obj, data, name) {

        }).on('typeahead:opened', function () {
            $('.tt-dropdown-menu').css('width', "700px");
        });

    // forces user to use the typeahead to edit data
    $('#employeeLookup').on('input', function () {
        employeeTypeahead.clearRemoteCache();
        var typeahead = $('.forced-typeahead');
        var input = typeahead.find('input');
        input.attr('title', 'You must Select an Employee from the dropdown to continue');
        input.tooltip();
        swapClass(typeahead, 'has-success', 'has-warning');
        swapClass(typeahead.find('span.glyphicon'), 'glyphicon-ok', 'glyphicon-warning-sign');
        clearAllFields();
    });

    // handles change of the active field checkbox
    $(".employeeChange").on('input change', function () {
        $('#saveButton').removeAttr("disabled");
        $('#employeeLookup').attr("disabled", "disabled");
    });

    // clears the employee lookup input boxes
    $('#clearButton').click(function () {
        employeeTypeahead.clearRemoteCache();
        var $lookup = $('#employeeLookup');
        $lookup.val("");
        $lookup.trigger('input');
        $('#employeeInfo').hide();
        $lookup.removeAttr("disabled");
    });

    // clears the employee lookup input boxes
    $('#addNewGroup').click(function () {
        var $lookup = $('#groupNameTypeahead');
        $lookup.val("");
        $lookup.trigger('input');
        $('#groupAlert').html('');
    });

    // handles changing the employee's group
    $('#group').change(function () {
        $('#groupChange_message').text("Would you like to change this employee's functions allowed to the defaults for " + $(this).val() + '?');
        $('#groupChange_modal').modal('show');
    });

    // changes the employee's functions allowed to their groups default
    $('#groupAccept').click(function () {
        employees.server.updStaffAccessUserGroup($('#userName').val(), $('#group').val()).done(function (data) {
            $('#functions').html('');
            data.forEach(function (current) {
                $('#functions').append('<div class="row"><div class="col-md-11"><input disabled type="text" class="form-control"  value="' + current + '"/></div><div class="col-md-1"><button style="margin-bottom:5px;" class="btn btn-danger pull-right deleteControl"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
            });
            $('#groupChange_modal').modal('hide');
        });
    });
});