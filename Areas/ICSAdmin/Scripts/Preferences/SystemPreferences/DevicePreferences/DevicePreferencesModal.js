// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var deviceID = 0;
    $('#UpdateAllInterface, #UpdateAllInterfaceZone').click(function () {
        var $this = $(this);
        var m;
        var zone= $('#devprefs_zone').val(), hostport = $('#COMPort').val(), baud = $('#Baud').val(), parity = $('#Parity').val(), word = $('#WordLength').val(), stopbit = $('#StopBit').val();
        if ($this.attr('id') == 'UpdateAllInterface') {
            m = 'Click OK to update all devices with Com Port: ' + hostport;
        } else {
            m = 'Click OK to update all devices with Com Port: ' + hostport + ' and Zone: ' + zone;
        };
        if (confirm(m)) {
            preferencesHub.server.updateAllOrZoneDevicePreferences(zone, hostport, baud, parity, word, stopbit);
        };
    });

    $('#devprefs_zone').change(function () {
        disableCommSettings();
    });

    $('#EditSelectedDevice').click(function () {
        deviceID = getDevPrefsDeviceID();
        enableCommSettings();
        launchDevPrefsModal();
    });

    $('#AddNewDevice').click(function () {
        deviceID = 0;
        disableCommSettings();
        launchDevPrefsModal();
    });

    $('#devpref_delete').click(function () {
        if (deviceID == 0 || deleteDevice(deviceID)) {
            $('#devpref_modal').modal('hide');
        };
    });

    function launchDevPrefsModal() {
        getDevicePreferenceByID(deviceID);
        $('#devpref_modal').modal('show');
    };

    $('#devprefs_ctype, #devprefs_type, #devprefs_model').change(function () {
        if (deviceID != 0) {
            showDPTypeFields();
        };
    });

    $('#SetWS').click(function () {
        $('#WSName').val($('#WSID').val());
    });

    $('#devprefs_address').on('input', function () {
        setNumericInRange($(this), SqlLimits.numerics.smallint);
    });

    $('#devprefs_positions, #devprefs_characters, #HostPort').on('input', function () {
        setNumericInRange($(this), SqlLimits.numerics.int);
    });

    $('#devpref_save, #devpref_saveclose').click(function () {
        saveDevicePreferenceByID(deviceID);
        if ($(this).attr('id') == 'devpref_saveclose') {
            $('#devpref_modal').modal('hide');
        } else {
            enableCommSettings();
        };
    });
    function saveDevicePreferenceByID() {
        var returnValue = 0;
        var zone = $('#devprefs_zone'), type = $('#devprefs_type'), num = $('#devprefs_devnum'), model = $('#devprefs_model'),
                    ctype = $('#devprefs_ctype'), port = $('#devprefs_port'), arrow = $('#devprefs_arrow'), light = $('#devprefs_light'),
                    laser = $('#devprefs_laser').data('toggles'), lightnum = $('#devprefs_lightnum'), address = $('#devprefs_address'),
                    positions = $('#devprefs_positions'), characters = $('#devprefs_characters'), pairkey = $('#PairKey');

        if (address.val().trim() == '') {
            address.val(0);
        };
        if (positions.val().trim() == '') {
            positions.val(0);
        };
        if (characters.val().trim() == '') {
            characters.val(0);
        };
        if (lightnum.val() == null || lightnum.val().trim() == '') {
            lightnum.val(0);
        };

        if (zone.val() == null) {
            postDevicePreferenceAlert('Zone cannot be left blank.');
        } else if (type.val() == null) {
            postDevicePreferenceAlert('Device Type cannot be left blank.');
        } else if (num.val() == null) {
            postDevicePreferenceAlert('Device Number cannot be left blank.');
        } else {
            // new entry
            var preferences = [
                    zone.val(), type.val(), num.val(), model.val(), ctype.val(), port.val(), arrow.val(), light.val(), laser.active, lightnum.val(),
                    address.val(), positions.val(), characters.val(), pairkey.val()
            ];
            // edit entry
            var shown = showDPTypeFields();
            if (deviceID != 0) {
                switch (shown) {
                    case 'WMI JMIF':
                        // JMIF carousel
                        preferences = preferences.concat([$('#JMIF').val()]);
                    case 'WMI':
                    case 'WMI SETUP':
                        // standard WMI
                        preferences = preferences.concat([$('#HostIP').val(), $('#HostPort').val(), $('#WSName').val()]);
                        break;
                    case 'OTHER':
                        // other
                        preferences = preferences.concat([$('#COMPort').val(), $('#Baud').val(), $('#Parity').val(), $('#WordLength').val(), $('#StopBit').val()]);
                        break;
                    case 'IPTI':
                        // ipti has save buttons on an individual basis
                        break;
                };
            };
            preferencesHub.server.saveDevicePreference(preferences, shown, deviceID).done(function (savedDeviceID) {
                enableCommSettings();
                devPrefTable.draw();
                if (savedDeviceID != 0) {
                    deviceID = savedDeviceID;
                    getDevicePreferenceByID(deviceID);
                };
            });
        };
    };
});

function disableCommSettings() {
    var comm = $('#CommHeader');
    comm.find('.alert-warning').remove();
    comm.prepend('<div class="alert alert-warning alert-custom" role="alert">Device Details needs to be saved before edits can be made to Comm Settings.</div>');
    $('#IPTI, #WMI, #OtherInterface').find('input, select').attr('disabled', 'disabled');
    hideDPTypeFields();
};

function enableCommSettings() {
    $('#CommHeader').find('.alert.alert-warning').remove();
    $('#IPTI, #WMI, #OtherInterface').find('input, select').removeAttr('disabled');
    if (showDPTypeFields() == 'IPTI') {

    };
};

function hideDPTypeFields() {
    $('#IPTI, #WMI, #OtherInterface').hide();
};

function showDPTypeFields() {
    var ctype = $('#devprefs_ctype').val();
    var IPTI = $('#IPTI'), WMI = $('#WMI'), Other = $('#OtherInterface'), setup = $('#SetupTilt_div'), JMIF = $('#JMIF_row');
    var WMIControllers = ['SISHorizontalCarousel', 'WMIC3000', 'Sapient', 'SapientShuttleNR', 'SapientShuttle', 'KardexTIC', 'SISLightMaster', 'SISLightMasterTB', 'SISCartSB','SISMultiSB', 'JMIFShuttle'];

    var shown = '';
    if ($('#devprefs_type').val() == 'Light Tree' && $('#devprefs_model').val() == 'IPTI') {
        IPTI.show();
        WMI.hide();
        Other.hide();
        shown = 'IPTI';
    } else if (WMIControllers.indexOf(ctype) >= 0) {
        shown = 'WMI';
        WMI.show();
        if (ctype == 'JMIFShuttle') {
            shown += ' JMIF';
            JMIF.show();
            setup.hide();
        } else if (ctype.toLowerCase().indexOf('sapientshuttle') >= 0) {
            shown += ' SETUP';
            setup.show();
            JMIF.hide();
        } else {
            setup.hide();
            JMIF.hide();
        }
        IPTI.hide();
        Other.hide();
    } else {
        Other.show();
        IPTI.hide();
        WMI.hide();
        shown = 'OTHER';
    };
    return shown;
};

function getDevPrefsDeviceID() {
    var activeRow = $('tr.active');
    if (activeRow.length > 0) {
        var row = devPrefTable.row(activeRow).data();
        return row[row.length - 1];
    } else {
        return 0;
    };
};

function getDevicePreferenceByID(deviceID) {
    preferencesHub.server.getDeviceInformation(deviceID).done(function (details) {
        // populate zone dropdown
        var zone = $('#devprefs_zone');
        zone.html('');
        $.each(details.zones, function (index, element) {
            zone.append('<option>' + element + '</option>');
        });
        // populate controller type dropdown
        var ctype = $('#devprefs_ctype');
        ctype.html('');
        $.each(details.controllers, function (index, element) {
            ctype.append('<option>' + element + '</option>');
        });
        // populate device model dropdown
        var model = $('#devprefs_model');
        model.html('');
        $.each(details.devModel, function (index, element) {
            model.append('<option>' + element + '</option>');
        });

        if (deviceID == 0) {
            $('#devpref_modal').find('input, select').val('');
            $('#devprefs_id').html('Device ID: (new)');
        } else {
            // populate device preferences
            $('#devprefs_id').html('Device ID: ' + deviceID);
            zone.val(details.device[0]);
            $('#devprefs_type').val(details.device[1]);
            $('#devprefs_devnum').val(details.device[2]);
            model.val(details.device[3]);
            ctype.val(details.device[4]);
            $('#devprefs_port').val(details.device[5]);
            $('#devprefs_arrow').val(details.device[6]);
            $('#devprefs_light').val(details.device[7]);
            $('#devprefs_laser').data('toggles').setValue(details.device[8].toLowerCase() == 'true' ? true : false);
            $('#devprefs_lightnum').val(details.device[9]);
            $('#devprefs_address').val(details.device[10]);
            $('#devprefs_positions').val(details.device[11]);
            $('#devprefs_characters').val(details.device[12]);

            // set IPTI data
            $('#IPTI_Container').html('');
            $.each(details.IPTI, function (index, obj) {
                appendToIPTI(obj.display, obj.bay, obj.baydisp, obj.comm, false);
            });

            // set WMI data
            $('#HostIP').val(details.device[13]);
            $('#HostPort').val(details.device[14]);
            $('#WSName').val(details.device[15]);
            // baud rate?
            $('#JMIF').val(details.device[16]);

            // set other data
            // also baud rate
            $('#Baud').val(details.device[16]);
            $('#COMPort').val(details.device[17]);
            $('#Parity').val(details.device[18]);
            $('#WordLength').val(details.device[19]);
            $('#StopBit').val(details.device[20]);

            $('#PairKey').val(details.device[21]);

            // reenable button if it was disabled
            $('#addNewIPTI').removeAttr('disabled');
            // show the proper fields
            showDPTypeFields();
        };
    });
};

function postDevicePreferenceAlert(message) {
    $('#devpref_modal .modal-body').prepend('<div class="alert alert-warning alert-custom details" role="alert">' + message + '</div>');
};

function deleteDevicePreferenceAlert() {
    $('#devpref_modal .modal_body').find('.alert.alert-warning.details').remove();
};