// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// broadcaster client function which alters what the user sees when a control is updated to the most up to date value
employees.client.controlUpdated = function (control, checked) {
    if (checked) {
        $('[name="' + control + '"]').prop("checked", "checked");
    } else {
        $('[name="' + control + '"]').removeAttr("checked");
    };
};

$(document).ready(function () {
    // adds a control to a user
    $('#controlNameSubmit').click(function () {
        employees.server.submitControlName($('#userName').val(), $('#controlNameTypeahead').val()).done(function (result) {
            $('#controlAlert').html('');
            if (result == "DNE") {
                $('#controlAlert').append('<div class="alert alert-warning alert-custom" role="alert">Function ' + $('#controlNameTypeahead').val() + ' does not exists</div>');
                return;
            }
            else if (result == "EXISTS") {
                $('#controlAlert').append('<div class="alert alert-warning alert-custom" role="alert">Function ' + $('#controlNameTypeahead').val() + ' already exists for this user</div>');
                return;
            };
            $('#functions').append('<div class="row"><div class="col-md-11"><input disabled type="text" class="form-control"  value="' + $('#controlNameTypeahead').val() + '"/></div><div class="col-md-1"><button style="margin-bottom:5px;" class="btn btn-danger pull-right deleteControl"> <span class="glyphicon glyphicon-remove"></span></button></div></div>')
            $('#controlNameModal').modal("hide");
        });
    });
    // adds all controls to a user
    $('#addAllControls').click(function () {
        employees.server.addAllControls($('#userName').val(), $('#accessLevel').val()).done(function (result) {
            $('#functions').html("");
            result.forEach(function (current) {
                $('#functions').append('<div class="row"><div class="col-md-11"><input disabled type="text" class="form-control"  value="' + current + '"/></div><div class="col-md-1"><button style="margin-bottom:5px;" class="btn btn-danger pull-right deleteControl"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
            });
        });
    });
    // removes a control from an employee
    $(document.body).on('click', '.deleteControl', function () {
        var $this = $(this);
        var result = confirm("Are you sure you want to delete " + $this.parent().siblings('.col-md-11').children().val() + " from Employee " + $('#firstName').val() + " " + $("#lastName").val());
        if (result) {
            employees.server.deleteControl($('#userName').val(), $this.parent().siblings('.col-md-11').children().val()).done(function () {
                $this.parent().parent().remove();
            })
        };

    });

    // adds a group to a user
    $('#groupNameSubmit').click(function () {
        if ($('#groupNameTypeahead').val() === '') {
            $('#groupAlert').html('');
            $('#groupAlert').append('<div class="alert alert-warning alert-custom" role="alert">Please select any group.</div>');
        }
        else {
            employees.server.submitGroupName($('#userName').val(), $('#groupNameTypeahead').val()).done(function (result) {
                $('#groupAlert').html('');
                if (result == "DNE") {
                    $('#groupAlert').append('<div class="alert alert-warning alert-custom" role="alert">Group ' + $('#groupNameTypeahead').val() + ' does not exists</div>');
                    return;
                }
                else if (result == "EXISTS") {
                    $('#groupAlert').append('<div class="alert alert-warning alert-custom" role="alert">Group ' + $('#groupNameTypeahead').val() + ' already exists for this user</div>');
                    return;
                }
                $('#userGroups').append('<div class="row"><div class="col-md-11"><input disabled type="text" class="form-control"  value="' + $('#groupNameTypeahead').val() + '"/></div><div class="col-md-1"><button style="margin-bottom:5px;" class="btn btn-danger pull-right deleteGroup"> <span class="glyphicon glyphicon-remove"></span></button></div></div>')
                $('#groupModal').modal("hide");
            });
        }
    });

    // removes a group from an user
    $(document.body).on('click', '.deleteGroup', function () {
        var $this = $(this);
        var result = confirm("Are you sure you want to delete " + $this.parent().siblings('.col-md-11').children().val() + " from Employee " + $('#firstName').val() + " " + $("#lastName").val());
        if (result) {
            employees.server.deleteGroup($('#userName').val(), $this.parent().siblings('.col-md-11').children().val()).done(function () {
                $this.parent().parent().remove();
            })
        };
    });

    // adds a new zone to the bulkpro settings - zones tab under employees
    $('#addNewZone').click(function () {
        $(this).attr("disabled", "disabled");
        employees.server.getAllZones().done(function (result) {
            var appendString = ""
            appendString += '<div class="row"><div class="col-md-9"><select id="New_zone" class="form-control zoneFill">';
            appendString += '<option></option>'
            result.forEach(function (currentZ) {
                appendString += '<option value="' + currentZ + '">' + currentZ + '</option>';
            })
            appendString += '</select></div><div class="col-md-1"><button id="New_save" disabled class="btn btn-primary zoneSave" style="margin-bottom:5px;"><span class="glyphicon glyphicon-floppy-disk"></span></button></div><div class="col-md-1"><button id="New_delete" class="btn btn-danger zoneDelete" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-remove"></span></button></div></div>';
            $('#zonesAppend').append(appendString);
        });
    })

    //Handles Changes on a zone select 
    $(document.body).on('change', '.zoneFill', function () {
        var $this = $(this);
        var selectString = '#' + $this.attr('id').substring(0, $this.attr('id').indexOf('_'));
        console.log($(selectString + '_save'))
        $(selectString + '_save').removeAttr("disabled");
    });

    // saves a zone in bulkpro settings - zones in employees
    $(document.body).on('click', '.zoneSave', function () {
        var $this = $(this);
        var oldValue = $this.attr('id').substring(0, $this.attr('id').indexOf('_'));
        var newValue = $('#' + oldValue + '_zone').val();
        var userName = $('#userName').val();
        employees.server.updateEmployeeZone(userName, oldValue, newValue).done(function (result) {
            $('#zoneAlert').html('');
            if (!result) {
                $('#zoneAlert').append('<div class="alert alert-warning alert-custom" role="alert">Zone ' + newValue + ' already exists for this user</div>');
            }
            else {
                $('#zoneAlert').html('');
                if (oldValue == "New") {
                    $('#addNewZone').removeAttr('disabled');
                }
                $('#' + oldValue + '_zone').attr('id', newValue + '_zone')
                $('#' + oldValue + '_save').attr('id', newValue + '_save').attr("disabled", 'disabled');
                $('#' + oldValue + '_delete').attr('id', newValue + '_delete')
            }
        })
    });
    // deletes an employee zone
    $(document.body).on('click', '.zoneDelete', function () {
        var $this = $(this);
        var value = $this.attr('id').substring(0, $this.attr('id').indexOf('_'));
        console.log(value)
        if (value == 'New') {
            $this.parent().parent().remove();
            $("#addNewZone").removeAttr("disabled");
            $('#zoneAlert').html('');
        }
        else {
            var userName = $('#userName').val();
            var result = confirm("Click okay to delete zone " + value + " from the employee " + userName)
            if (result) {
                employees.server.deleteEmployeeZone(userName, value).done(function (result) {
                    if (!result) {
                        alert("Failed to delete zone")
                    }
                    else {
                        $this.parent().parent().remove();
                    };
                });
            };
        };
    });

    //Handles Pick Level Section
    $(document.body).on('input', '.shelfInput', function () {
        var $this = $(this);
        setNumericInRange($this, 1, 99);
        var ident = $this.attr('id').substring(0, $this.attr('id').indexOf('_'));
        if ($this.attr('id').indexOf('eShelf') >= 0 && $this.val().length > 0) {
            if ($('#' + ident + '_sShelf').val().length > 0) {
                $('#' + ident + '_save').removeAttr('disabled');

            }
            else {
                $('#' + ident + '_save').attr('disabled', 'disabled');
            }
        }
        else if ($this.val().length > 0) {
            if ($('#' + ident + '_eShelf').val().length > 0) {
                $('#' + ident + '_save').removeAttr('disabled');
            }
            else {
                $('#' + ident + '_save').attr('disabled', 'disabled');
            }
        }
        else {
            $('#' + ident + '_save').attr('disabled', 'disabled');
        };
    });

    $(document.body).on('input', '.carInput', function () {
        var $this = $(this);
        setNumericInRange($this, 1, 99);
        var ident = $this.attr('id').substring(0, $this.attr('id').indexOf('_'));
        if ($this.attr('id').indexOf('eCar') >= 0 && $this.val().length > 0) {
            if ($('#' + ident + '_sCar').val().length > 0) {
                $('#' + ident + '_save').removeAttr('disabled');

            }
            else {
                $('#' + ident + '_save').attr('disabled', 'disabled');
            }
        }
        else if ($this.val().length > 0) {
            if ($('#' + ident + '_eCar').val().length > 0) {
                $('#' + ident + '_save').removeAttr('disabled');
            }
            else {
                $('#' + ident + '_save').attr('disabled', 'disabled');
            }
        }
        else {
            $('#' + ident + '_save').attr('disabled', 'disabled');
        };
    });


    // adds a new employee pick level
    $('#addPickLevel').click(function () {
        var pickLevel = $('#pickLevelsAppend').children()
        if (pickLevel.length == 0)
        {
            $('#pickLevelsAppend').append('<div class="row"><div class="col-md-2"><input id="New_Level" disabled type="text" class="form-control" value="1"/></div><div class="col-md-2"><input id="New_sCar" maxlength="2" type="text" class="form-control carInput" value="1"/></div><div class="col-md-2"><input id="New_eCar" maxlength="2" type="text" class="form-control carInput" value="99"/></div><div class="col-md-2"><input id="New_sShelf" maxlength="2" type="text" class="form-control shelfInput"/></div><div class="col-md-2"><input id="New_eShelf" type="text" class="form-control shelfInput" maxlength="2" /></div><div class="col-md-1"><button id="New_save" disabled class="btn btn-primary shelfSave" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-floppy-disk"></span></button></div><div class="col-md-1"><button id="New_delete" class="btn btn-danger shelfDelete" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
        }
        else {
            var newval = parseInt(pickLevel.last().children().children().val()) + 1;
            $('#pickLevelsAppend').append('<div class="row"><div class="col-md-2"><input id="New_Level" disabled type="text" class="form-control" value="' + newval +'"/></div><div class="col-md-2"><input id="New_sCar" maxlength="2" type="text" class="form-control carInput" value="1"/></div><div class="col-md-2"><input id="New_eCar" maxlength="2" type="text" class="form-control carInput" value="99"/></div><div class="col-md-2"><input id="New_sShelf" maxlength="2" type="text" class="form-control shelfInput"/></div><div class="col-md-2"><input id="New_eShelf" maxlength="2" type="text" class="form-control shelfInput"/></div><div class="col-md-1"><button id="New_save" disabled class="btn btn-primary shelfSave" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-floppy-disk"></span></button></div><div class="col-md-1"><button id="New_delete" class="btn btn-danger shelfDelete" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
        }
        $(this).attr('disabled', 'disabled');
    });
    // saves a shelf in the employee pick levels tab under employees
    $(document.body).on('click', '.shelfSave', function () {
        var $this = $(this);
        var value = $this.attr('id').substring(0, $this.attr('id').indexOf('_'));
        var sShelf = $('#'+value+'_sShelf').val();
        var eShelf = $('#' + value + '_eShelf').val();
        var sCar = $('#' + value + '_sCar').val();
        var eCar = $('#' + value + '_eCar').val();
        var level = "1"
        var userName = $('#userName').val();
        
        if (value == "New")
        {
            level = $('#New_Level').val();
        }
        employees.server.addUpdatePickLevel(value, userName, level, sShelf, eShelf, sCar, eCar).done(function (result) {
            $('#pickLevelAlert').html('');
            if (!result.fail) {
                $('#pickLevelAlert').append('<div class="alert alert-warning alert-custom" role="alert">Pick Level ' + sShelf + ' already exists for this user</div>');
            }
            else if (value == "New") {
                $('#pickLevelAlert').html('');
                $('#addPickLevel').removeAttr('disabled');
                $('#' + value + '_Level').attr('id', result.ID + '_Level');
                $('#' + value + '_sShelf').attr('id', result.ID + '_sShelf');
                $('#' + value + '_eShelf').attr('id', result.ID + '_eShelf');
                $('#' + value + '_sCar').attr('id', result.ID + '_sCar');
                $('#' + value + '_eCar').attr('id', result.ID + '_eCar');
                $('#' + value + '_save').attr('id', result.ID + '_save').attr("disabled", 'disabled');
                $('#' + value + '_delete').attr('id', result.ID + '_delete')
            }
            else {
                $('#' + value + '_save').attr("disabled", 'disabled');
            };
        });
    });

    // deletes an employee pick level shelf
    $(document.body).on('click', '.shelfDelete', function () {
        var $this = $(this);
        var value = $this.attr('id').substring(0, $this.attr('id').indexOf('_'));
        if (value == "New") {
            $this.parent().parent().remove();
            $('#addPickLevel').removeAttr("disabled");
        }
        else {
            var okayed = confirm("Are you sure you wish to delete the pick level?")
            if (okayed) {
                employees.server.deletePickLevel(value).done(function (result) {
                    if (!result) {
                        alert("Action not executed");
                    }
                    else {
                        $this.parent().parent().remove();
                    };
                });
            };
        };
    });

    // saves an employee's information
    $('#saveButton').click(function () {
        employeeTypeahead.clearRemoteCache();
        var firstName = $('#firstName').val();
        var MI = $('#MI').val();
        var lastName = $('#lastName').val();
        var username = $('#userName').val();
        var password = $('#password').val();
        var accessLevel = $('#accessLevel').val();
        var group = $('#group').val();
        var email = $('#email').val();
        var maxOrders = $('#maxOrders').val();
        var active = $('#active').prop('checked');
        employees.server.saveChanges(firstName, MI, lastName, username, password, accessLevel, group, email, maxOrders, active).done(function (result) {
            if (result === 'Success') {
                $('#saveButton').attr('disabled', 'disabled');
                $('#employeeLookup').removeAttr('disabled');
            }  else {
                alert(result);
            };
            //if (!result) {
            //    alert("Failed to update user")
            //} else {
            //    $('#saveButton').attr('disabled', 'disabled');
            //    $('#employeeLookup').removeAttr('disabled');
            //};
        });
    });

    // prints employees
    $(document.body).on('click', '#printButton', function () {
        //reportsHub.server.printEmployees();
        title = 'Employee  Report';
        getLLPreviewOrPrint('/Admin/Employees/printEmployees', {
        }, true, 'report', title)
    });

    // deletes an employee
    $('#deleteButton').click(function () {
        employeeTypeahead.clearRemoteCache();
        var userName = $('#userName').val();
        var result = confirm("Are you sure you want to delete this employee?");
        if (result) {
            employees.server.deleteEmployee(userName).done(function (success) {
                if (!success) {
                    alert("Failed to Delete User");
                } else {
                    $('#employeeLookup').val("").trigger("input");
                };
            });
        };
    });

    //Handles Button Access checkbox clicks
    $('.controlChange').click(function () {
        var $this = $(this);
        var newVal = $this.prop("checked");
        var controlToChange = $this.attr("name");
        employees.server.updateControl(controlToChange, newVal);
    });

});