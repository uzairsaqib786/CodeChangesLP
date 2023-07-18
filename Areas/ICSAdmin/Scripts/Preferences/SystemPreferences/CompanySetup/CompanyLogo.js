// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var uploadObj = $("#fileuploader").uploadFile({
        url: "/Preferences/CompanyLogoUpload",
        autoSubmit: false,
        multiple: false,
        showCancel: false,
        showAbort: false,
        showDelete: false,
        showProgress: false,
        showDone: false,
        showStatusAfterSuccess: false,
        showFileCounter: false,
        allowedTypes: '*', // type checking is in onSelect callback.  This option only allows for extension restriction, not mimetype restriction
        uploadButtonClass: 'btn btn-primary', // adds classes in place of the default, but the buttons are hidden anyway so this doesn't matter for visual purposes
        returnType: 'json',
        onSelect: function (file) {
            if (file == 'undefined') {

            }
            else if (file[0].type.indexOf('image') > -1) {
                $('#selectedImage').val(file[0].name);
                $('#upload').removeAttr('disabled');
            } else {
                $('#upload').attr('disabled', 'disabled');
                alert('Only image files may be used for the Company Logo field.  The type of file you entered was: ' + file[0].type);
            };
        },
        onSuccess: function (files, data, xhr) {
            if (data) {
                $('#selectedImage').val('');
                alert('Logo successfully saved!');
            } else {
                alert('Error: Logo not saved.  Ensure the file is a valid image.');
            };
        },
        onError: function (files, status, errMsg) {
            if ($('#selectedImage').val().trim() != '') {
                $('#upload').removeAttr('disabled');
            };
            alert('File was not uploaded due to an error.  Please try again.');
        }
    });

    $('#imageSelect, #selectedImage').click(function () {
        $('#uploader-wrapper').find('input[type="file"]:first').trigger('click');
    });

    $('#upload').click(function () {
        uploadObj.startUpload();
        $(this).attr('disabled', 'disabled');
    });

    $('#currentLogo').click(function () {
        getLogo();        
    });
});

function getLogo() {
    preferencesHub.server.getCompanyLogoExt().done(function (ext) {
        if (ext != "NO EXTENSION FOUND") {
            $('#modal_logo').html('<img src="../images/CompanyLogo/logo' + ext + '" />');
            $('#logo_modal').modal('show');
        } else {
            alert('No logo has been set!');
        };
    });
};