// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var ftpSettingsHub = $.connection.fTPSettingsHub;
$(document).ready(function () {

    //Resizes the modal to fit screen
    addResize(function () {
        $('#FTPSettingsContainer').css({
            'max-height': $(window).height() * 0.65
        });
    });

    var passwordRef;

    //show modal
    $('#FTPSettings').click(function () {
        $('#FTPSettingsModal').modal('show');
        getSettings();
    });

    //on an input enable the desired save button
    $(document.body).on('input', '[name="Type"], [name="Location"], [name="Username"], [name="Password"], [name="Filename"], [name="Extension"], [name="Ready"]', function () {
        var $this = $(this);
        if ($this.hasClass("export")) {
            $this.parent().siblings().children('[name="SaveExportRow"]').removeAttr('disabled');
        } else {
            $this.parent().siblings().children('[name="SaveImportRow"]').removeAttr('disabled');
        };
        
    });

    //on a chnage enable the save button
    $(document.body).on('change', '[name="FTP"]', function () {
        var $this = $(this);
        if ($this.hasClass("export")) {
            $this.parent().siblings().children('[name="SaveExportRow"]').removeAttr('disabled');
        } else {
            $this.parent().siblings().children('[name="SaveImportRow"]').removeAttr('disabled');
        };
    });

    //Handles saving the desired import row
    $(document.body).on('click', '[name="SaveImportRow"]', function () {
        $this = $(this);
        var p = $this.parent().parent().children();
        var oldTransType = $this.data("transtype");
        var TransType = p.find('[name="Type"]').val();
        var Ftp = p.find('[name="FTP"]').prop('checked');
        var Location = p.find('[name="Location"]').val();
        var Username = p.find('[name="Username"]').val();
        var Password = p.find('[name="Password"]').val();
        var Filename = p.find('[name="Filename"]').val();
        var Extension = p.find('[name="Extension"]').val();
        var Ready = p.find('[name="Ready"]').val();

        if (TransType != "") {
            ftpSettingsHub.server.updImportSett(oldTransType, TransType, Ftp, Location, Username, Password, Filename, Extension, Ready).done(function (mess) {
                if (!mess) {
                    MessageModal("Error", "An error has occurred updating this import record")
                } else {
                    $this.data("transtype", TransType);
                    $this.attr('disabled', 'disabled');
                };
            });
        } else {
            MessageModal("Warning", "Please enter a type for this import record");
        };
    });

    //Handles saving he desired export row
    $(document.body).on('click', '[name="SaveExportRow"]', function () {
        $this = $(this);
        var p = $this.parent().parent().children();
        var oldHostType = $this.data("hosttype");
        var HostType = p.find('[name="Type"]').val();
        var Ftp = p.find('[name="FTP"]').prop('checked');
        var Location = p.find('[name="Location"]').val();
        var Username = p.find('[name="Username"]').val();
        var Password = p.find('[name="Password"]').val();
        var Filename = p.find('[name="Filename"]').val();
        var Extension = p.find('[name="Extension"]').val();
        var Ready = p.find('[name="Ready"]').val();

        if (HostType != "") {
            ftpSettingsHub.server.updExportSett(oldHostType, HostType, Ftp, Location, Username, Password, Filename, Extension, Ready).done(function (mess) {
                if (!mess) {
                    MessageModal("Error", "An error has occurred updating this import record")
                } else {
                    $this.data("hosttype", HostType);
                    $this.attr('disabled', 'disabled');
                };
            });
        } else {
            MessageModal("Warning", "Please enter a type for this import record");
        };
    });


    //on password field click show password modal
    $(document.body).on('click', '[name="Password"]', function () {
        //show modal
        $('#FTPPasswordModal').modal('show');
        //set the password reference in order to keep track of the cliced password
        passwordRef = $(this);
        //if passwrd is empty disable clearing password button
        if ($(this).val() == "") {
            $('#ClearFTPPassword').attr('disabled', 'disabled');
        } else {
            $('#ClearFTPPassword').removeAttr('disabled');
        }
    });

    //enable save button if both passwrd fields arewe filled in
    $('#FTPNewPassword').on('input', function () {
        $this = $(this);
        if ($this.val() != "" && $('#FTPConfPassword').val() != "") {
            $('#SaveFTPPassword').removeAttr('disabled');
        } else {
            $('#SaveFTPPassword').attr('disabled', 'disabled');
        };
    });
    
    //enable save button if both passwrd fields are filled in
    $('#FTPConfPassword').on('input', function () {
        $this = $(this);
        if ($this.val() != "" && $('#FTPNewPassword').val() != "") {
            $('#SaveFTPPassword').removeAttr('disabled');
        } else {
            $('#SaveFTPPassword').attr('disabled', 'disabled');
        };
    });

    //Clears the desired password field and enables the save button for that row
    $('#ClearFTPPassword').click(function () {
        var conf = confirm("Clear the password for this import/export?");
        var finds;

        if (passwordRef.hasClass('export')) {
            finds = '[name="SaveExportRow"]'
        } else {
            finds = '[name="SaveImportRow"]'
        };

        if (conf) {
            passwordRef.val("");
            passwordRef.parent().parent().children().find(finds).removeAttr('disabled');
            $('#FTPConfPassword').val("");
            $('#FTPNewPassword').val("");
            $('#SaveFTPPassword').attr('disabled', 'disabled');
            $('#FTPPasswordModal').modal('hide');
        };
    });

    //Handles setting the new password value for the clicked password text box
    $('#SaveFTPPassword').click(function () {
        var finds;

        if (passwordRef.hasClass('export')) {
            finds = '[name="SaveExportRow"]'
        } else {
            finds = '[name="SaveImportRow"]'
        };

        if ($('#FTPConfPassword').val() == $('#FTPNewPassword').val()) {
            passwordRef.val($('#FTPConfPassword').val());
            passwordRef.parent().parent().children().find(finds).removeAttr('disabled');

            $('#FTPConfPassword').val("");
            $('#FTPNewPassword').val("");
            $('#SaveFTPPassword').attr('disabled', 'disabled');
            $('#FTPPasswordModal').modal('hide');
        } else {
            MessageModal("Warning", "The new password does not match the confirm password")
        };
    });

});

//Gets all the FTP Settings and populates the container with them
function getSettings() {
    $('#FTPSettingsContainer').html("");

    ftpSettingsHub.server.getFTPSettings().done(function (data) {
        var ImportData = data.Import;
        var ExportData = data.Export;
        var Icheck = "";
        var Echeck = "";
        var impDis="";
        var expDis = "";

        //Gives the values that disables the ftp checkbox  imports and exports
        var ImpDisArray = ["Adjustment", "Complete", "Location Change", "Shipping Complete"];
        var ExpDisArray = ["Audit", "Inventory"];

        //loop thorugh all the ftp settings records
        for (var i = 0; i < ImportData.length; i++) {
            //Handles setting the checked proeprty of the checkboxes
            if (ImportData[i].FTP == true) {
                Icheck = "checked";
            } else {
                Icheck = "";
            };
            if (ExportData[i].FTP == true) {
                Echeck = "checked";
            } else {
                Echeck = "";
            };

            //handles disabling the ftp checkbox
            if (ImpDisArray.indexOf(ImportData[i].Type) >= 0) {
                impDis = 'disabled';
            } else {
                impDis = "";
            };

            if (ExpDisArray.indexOf(ExportData[i].Type) >= 0) {
                expDis = 'disabled';
            } else {
                expDis = "";
            };

            //Appends each record to the container
            $('#FTPSettingsContainer').append(
                    '<div class="row top-spacer">' +
                            '<div class="col-md-2">' +
                                '<input type="text" name="Type" maxlength="50" class="form-control" ' + (ImportData[i].Type != "" ? 'value="' + ImportData[i].Type + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="checkbox" class=form-control ' + Icheck + ' name="FTP" ' + impDis + ' />' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="text" name="Location" maxlength="50" class="form-control" ' + (ImportData[i].Location != "" ? 'value="' + ImportData[i].Location + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="text" name="Username" maxlength="50" class="form-control" ' + (ImportData[i].Username != "" ? 'value="' + ImportData[i].Username + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<input type="password" name="Password" maxlength="130" readonly="readonly" class="form-control modal-launch-style" ' + (ImportData[i].Password != "" ? 'value="' + ImportData[i].Password + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<input type="text" name="Filename" maxlength="50" class="form-control" ' + (ImportData[i].Filename != "" ? 'value="' + ImportData[i].Filename + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="text" name="Extension" maxlength="5" class="form-control" ' + (ImportData[i].Extension != "" ? 'value="' + ImportData[i].Extension + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="text" name="Ready" maxlength="50" class="form-control" ' + (ImportData[i].Ready != "" ? 'value="' + ImportData[i].Ready + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" disabled data-transtype="' + ImportData[i].Type + '" title="Save Changes Import Row" class="btn btn-primary btn-block" name="SaveImportRow"><span class="glyphicon glyphicon-floppy-disk"></span></button> ' +
                            '</div>' +
                        '</div>' +
                        //Break for export
                        '<div class="row">' +
                            '<div class="col-md-2">' +
                                '<input type="text" name="Type" maxlength="50" class="form-control export" ' + (ExportData[i].Type != "" ? 'value="' + ExportData[i].Type + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="checkbox" class="export form-control" ' + Echeck + ' name="FTP" ' + expDis + ' />' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="text" name="Location" maxlength="50" class="form-control export" ' + (ExportData[i].Location != "" ? 'value="' + ExportData[i].Location + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="text" name="Username" maxlength="50" class="form-control export" ' + (ExportData[i].Username != "" ? 'value="' + ExportData[i].Username + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<input type="password" name="Password" maxlength="130" readonly="readonly" class="form-control export modal-launch-style" ' + (ExportData[i].Password != "" ? 'value="' + ExportData[i].Password + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<input type="text" name="Filename" maxlength="50" class="form-control export" ' + (ExportData[i].Filename != "" ? 'value="' + ExportData[i].Filename + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="text" name="Extension" maxlength="5" class="form-control export" ' + (ExportData[i].Extension != "" ? 'value="' + ExportData[i].Extension + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<input type="text" name="Ready" maxlength="50" class="form-control export" ' + (ExportData[i].Ready != "" ? 'value="' + ExportData[i].Ready + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-1">' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" disabled data-hosttype="' + ExportData[i].Type + '" title="Save Changes Export Row" class="btn btn-primary btn-block" name="SaveExportRow"><span class="glyphicon glyphicon-floppy-disk"></span></button> ' +
                            '</div>' +
                        '</div>');
        };
    });
};