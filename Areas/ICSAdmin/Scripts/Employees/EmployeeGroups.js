// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var employeeGroups = $.connection.employeeGroupHub;

$(document).ready(function () {
    // assigns a height to the <ul>s of sortable data
    $('#assigned').parent().css('height', $(window).height() * 0.8);
    $('#unassigned').parent().css('height', $(window).height() * 0.8);

    // updates all employees of the selected group to the group's rights
    $('#updateGroup').click(function () {
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        employeeGroups.server.updateEmployeesInGroup($('#currentEmployeeGroup :selected').val()).done(function (success) {
            if (!success) {
                alert('Update failed.  Please try again.');
            } else {
                $this.removeAttr('disabled');
                alert('Employees in group updated.');
            };
        });
    });

    // saves a group's information
    $('#saveGroup').click(function () {
        var group = $('#currentEmployeeGroup :selected').val();
        var controls = Array();
        $.each($('#assigned').children(':not(.static)'), function () {
            controls.push($(this).text());
        });
        employeeGroups.server.insGroupsFunction(group, controls).done(function (success) {
            if (!success) {
                alert('Group function save failed.  Please try again.');
            } else {
                $('#currentEmployeeGroup').removeAttr('disabled');
                $('#saveGroup').attr('disabled', 'disabled');
                $('#updateGroup').removeAttr('disabled');
            };
        });
    });

    // deletes a group.  Groups that are default are not allowed to be deleted
    $('#delGroups').click(function () {
        var group = $('#currentEmployeeGroup :selected').val().trim();
        if (defaultGroups.indexOf(group) != -1) {
            return false;
        };
        var result = confirm('Click OK to delete ' + group + ' from Employee Groups.');
        if (result) {
            employeeGroups.server.delGroups(group).done(function (success) {
                if (success) {
                    var $this;
                    $('#currentEmployeeGroup').children().each(function () {
                        $this = $(this);
                        if ($this.text() == group) {
                            $this.remove();
                        };
                    });
                    var both = $('#assigned, #unassigned');
                    both.children(':not(.static)').remove();
                    both.trigger('sortupdate');
                    $('#addAll, #removeAll, #updateGroup, #printGroup, #saveGroup, #cloneGroup, #delGroups').attr('disabled', 'disabled');
                };
            });
        };
    });

    // launches the add new group modal
    $('#addGroup').click(function () {
        $('#addgroup_modal').modal('show');
    });

    // handles the submission of a new group
    $('#addgroup_submit').click(function () {
        var newGroup = $('#newGroupName').val();
        employeeGroups.server.insGroups(newGroup).done(function (success) {
            if (success) {
                $('#addgroup_alerts').html('');
                $('#currentEmployeeGroup').append('<option>' + newGroup + '</option>');
                $('#addgroup_modal').modal('hide');
            } else {
                $('#addgroup_alerts').html('<div class="alert alert-custom alert-warning" role="alert">Group name already exists.  Choose a unique name to save.</div>');
            };
        });
    });

    // launches the clone group modal
    $('#cloneGroup').click(function () {
        $('#clonegroup_alerts').html('');
        $('#newCloneGroupName').val("");
        $('#clonegroup_modal').modal('show');
    });

    // handles the submission of a clone group
    $('#clonegroup_submit').click(function () {
        var cloneGroup = $('#currentEmployeeGroup :selected').val().trim();
        var newGroup = $('#newCloneGroupName').val();
        if (newGroup == '') {
            $('#clonegroup_alerts').html('');
            $('#clonegroup_alerts').append('<div class="alert alert-warning alert-custom" role="alert">Please enter the group name.</div>');
        }
        else {
            employeeGroups.server.cloneGroups(cloneGroup, newGroup).done(function (success) {
                if (success) {
                    $('#clonegroup_alerts').html('');
                    $('#currentEmployeeGroup').append('<option>' + newGroup + '</option>');
                    $('#clonegroup_modal').modal('hide');
                } else {
                    $('#clonegroup_alerts').html('<div class="alert alert-custom alert-warning" role="alert">Group name already exists.  Choose a unique name to save.</div>');
                };
            });
        }
    });

    // prints by current employee group
    $(document.body).on('click', '#printByGroup', function () {
        var group = $('#currentEmployeeGroup :selected').val();
        if (group.trim() != '') {
            //reportsHub.server.printEmployeeGroup('group', $('#currentEmployeeGroup :selected').val());
            title = 'Employee Group  Report';
            getLLPreviewOrPrint('/Admin/Employees/printEmployeeGroup', {
                checkVal: 'group',
                group: $('#currentEmployeeGroup :selected').val()
            }, true, 'report', title)
        };
    });

    // prints all groups
    $(document.body).on('click', '#printByAll', function () {
        //reportsHub.server.printEmployeeGroup('all', $('#currentEmployeeGroup :selected').val());
        title = 'Employee Group  Report';
        getLLPreviewOrPrint('/Admin/Employees/printEmployeeGroup', {
            checkVal: 'all',
            group: $('#currentEmployeeGroup :selected').val()
        }, true, 'report', title)
    });

    // switches the selected employee group information
    $('#currentEmployeeGroup').on('change', function () {
        $('#assigned, #unassigned').children(':not(.static)').remove();
        var $this = $(this);
        if (!$this.children(':selected').val().trim() == '') {
            employeeGroups.server.getFunctionsByGroup($this.children(':selected').val()).done(function (functions) {
                var assign = $('#assigned');
                $.each(functions.assigned, function (index, element) {
                    assign.append('<li class="btn btn-sm btn-primary">' + element + '</li>');
                });
                var unassign = $('#unassigned');
                $.each(functions.unassigned, function (index, element) {
                    unassign.append('<li class="btn btn-sm btn-primary">' + element + '</li>');
                });
                $('#addAll, #removeAll, #updateGroup, #cloneGroup, #printGroup').removeAttr('disabled');
                if (defaultGroups.indexOf($this.children(':selected').val()) == -1) {
                    $('#delGroups').removeAttr('disabled');
                };
            });
        } else {
            $('#addAll, #removeAll, #updateGroup, #printGroup, #cloneGroup, #delGroups').attr('disabled', 'disabled');
        };
        $('#assigned, #unassigned').trigger('sortupdate');
    });

    // clears the employee group that was selected
    $('#clearAll').click(function () {
        $('#currentEmployeeGroup').val(' ');
        $('#assigned, #unassigned').children('li:not(.static)').remove();
        $('#addAll, #removeAll, #updateGroup, #printGroup, #delGroups, #cloneGroup, #saveGroup').attr('disabled', 'disabled');
        $('#currentEmployeeGroup').removeAttr('disabled');
    });
});
