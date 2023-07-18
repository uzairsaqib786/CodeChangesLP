// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('.app').on('input', function () {
        $(this).parent().siblings().find('.appSave').removeAttr('disabled');
    })
    $('.appSave').on('click', function () {
        SaveValidateLicense(this);
        $(this).attr("disabled", "disabled");
    })
    $('.appDisplay, .appLicense, .appURL').on('keydown', function (e) {
        if (e.which == 13) {
            SaveValidateLicense(this);
        }
    })

    function SaveValidateLicense(element) {
        //Save Changes,Validate License string and update status/NumLicenses
        var AppInputs = $(element).parent().siblings();
        var LicenseString = $(element).hasClass("appLicense") ? $(element).val() : AppInputs.find('.appLicense').val();
        var AppName = AppInputs.find('.appName').val();
        var DisplayName = AppInputs.find('.appDisplay').val();
        var AppUrl = AppInputs.find('.appURL').val();
        config.server.saveValidateLicense(LicenseString, AppName, DisplayName, AppUrl).done(function (numLicenses) {
            if (numLicenses > 0) {
                AppInputs.find('.appValid').text('Valid').removeClass("text-danger").addClass('text-success');
            } else if (numLicenses == -1) {
                MessageModal("Error", "An Error Occured while trying to update you License. Check the Error Log for more information")
            }
            else {
                AppInputs.find('.appValid').text('Invalid').removeClass("text-success").addClass('text-danger');
                MessageModal("Invalid License", "The License you entered for " + DisplayName + " is Invalid. Contact Scotttech to resolve this Issue")
            }
            AppInputs.find('.appNumLicense').val(numLicenses)
        })
    }

    $(document.body).on('click', '#WSPermission tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('tr.active').removeClass('active');
            $this.addClass('active');
            $('#SelectedWS').text($this.text())
            getWSAppPermissions($this.children().data('wsid'));

        }
    })

    $('#ClientCertCopy').click(function () {
        if (confirm("WARNING: Installing this certificate on the improper workstation will result in duplicate workstations, which can end in data corruption. Only install this certificate on ONE workstation. Click ok to continue")) {
            if (confirm("Are you sure you would like to recieve a copy of this certificate? If installed on multiple PC's it may cause issues with the pickpro system. This should only be used to replace an existing workstation. Click ok to continue")) {
                if (confirm("Final warning: ONLY download this certificate and install it on a workstation you are replacing. Otherwise, it can lead to data corruption. Click ok to download the certificate")) {
                    window.location.href = '/api/LLExport/GetFile?fileName=client' + $('#SelectedWS').html().trim() + 'cert.pfx&WSID=Cert&cert=1';
                }
            }
        }
    })

    function getWSAppPermissions(WSID) {
        $('#WSApp tbody tr').removeClass('tr-disabled');
        $('[data-appname]').removeAttr('disabled').removeAttr('checked');
        $('[name="defaultapp"]').removeAttr('disabled').removeAttr('checked');
        config.server.selectWSApp(WSID).done(function (data) {
            for (var x = 0; x < data.WSApps.length; x++) {
                $('[data-appname="' + data.WSApps[x] + '"').prop('checked', true)
            }
            $('[name="defaultapp"][value="' + data.DefaultApp + '"]').prop('checked', true);
        });
    }

    function setDefaultApp(appName) {
        config.server.setDefaultApp(appName, $('tr.active').children().data('wsid')).done(function (data) {
            if (data == 'Error') {
                MessageModal("Error", "Error Occured while setting Default app for workstation. Check error log for more info")
            }
        });
    }

    $('[name="defaultapp"]').change(function () {
        setDefaultApp($(this).val())
    })
    $('#clearDefaultApp').click(function () {
        $('[name="defaultapp"]').removeAttr('checked');
        setDefaultApp('');
    })

    function getWSID() {
        return $('tr.active').children().data('wsid');
    }

    $('#deleteWorkstation').click(function () {
        if (confirm("WARNING: This will PERMANENTLY DELETE THIS WORKSTATION! Click ok to continue")) {
            var wsid = getWSID();
            config.server.deleteWS(wsid).done(function (data) {
                if (!data) {
                    // no data == success
                    $('#WSPermission').find('[data-wsid="' + wsid + '"]').remove();
                    var $first = $('#WSPermission').find('tr').first();
                    $first.click();
                }
            });
        }
    });

    $('[data-appname]').change(function () {
        var appname = $(this).data('appname');
        var hasPermission = $(this).prop('checked'); //boolean
        var WSID = $('tr.active').children().data('wsid');

        if (hasPermission) {
            //add here
            config.server.addWSApp(WSID, appname).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred adding this application to the wsid")
                };
            });
        } else {
            //delete here
            config.server.deleteWSApp(WSID, appname).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred removing this application from the wsid")
                };
            });
        };
    })
})