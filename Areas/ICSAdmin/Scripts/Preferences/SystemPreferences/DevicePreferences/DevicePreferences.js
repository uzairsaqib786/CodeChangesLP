// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var devPrefTable;

$(document).ready(function () {
    var deviceTimer = mkTimer(function () {
        devPrefTable.draw();
    }, 500);

    $('#Dev_Qty, #Dev_QtyCF, #Dev_QtySB').on('input', function () {
        setNumericInRange($(this), 0, 9999);
    });

    $('#Dev_Carousel, #Dev_CarouselSpin').on('input', function () {
        setNumericInRange($(this), 1, 8);
    });

    $('#ZoneFilter').on('input', function () {
        deviceTimer.startTimer();
    });

    $('#ClearZone').click(function () {
        $('#ZoneFilter').val('').trigger('input');
    });

    $('#DevPrefsTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        setActiveTR($this);
    });

    $('#ToggleInterface').click(function () {
        alert('Hardware not done!');
        var status = $('#InterfaceStatus');
        var $this = $(this);
        var statusText = status.html() == 'Inactive' ? ['Active', 'Stop Interface'] : ['Inactive', 'Start Interface'];
        status.html(statusText[0]).toggleClass('label-danger').toggleClass('label-success');
        $this.html(statusText[1]).toggleClass('btn-danger').toggleClass('btn-primary');
    });

     devPrefTable = $('#DevPrefsTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
         "pageLength": 1000,
        "columnDefs": [
            { 'visible': false, "targets": 13 }
        ],
        "ajax": {
            "url": "/Admin/Preferences/getDevicePreferencesTable",
            'data': function (d) {
                d.zone = $('#ZoneFilter').val();
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    }).on('draw', function (e, settings) {
        $('#DeleteSelectedDevice, #EditSelectedDevice').attr('disabled', 'disabled');
    });

    $('#DevPrefsTable').wrap('<div style="overflow-x:scroll"></div>');

    $('#DeleteSelectedDevice').click(function () {
        var row = devPrefTable.row($('tr.active')).data();
        deleteDevice(row[row.length - 1]);
    });


    var DevicePreferenceEngineZone = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // calls TransactionsController function nextOrdersOpen
            url: 'Admin/Preferences/getZonesForDevPrefs?query=%QUERY',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            cache: false
        }
    });
    DevicePreferenceEngineZone.initialize();
    $('#ZoneFilter').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "DevicePreferenceEngineZone",
        displayKey: 'value',
        source: DevicePreferenceEngineZone.ttAdapter()
    }).on('typeahead:selected', function () { $('#ZoneFilter').trigger('input'); });

    var DevicePreferenceEngineUoM = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // calls TransactionsController function nextOrdersOpen
            url: 'Admin/Preferences/getUoMForDevPrefs?query=%QUERY',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            cache: false
        }
    });
    DevicePreferenceEngineUoM.initialize();
    $('#Dev_UoM').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "DevicePreferenceEngineUoM",
        displayKey: 'value',
        source: DevicePreferenceEngineUoM.ttAdapter()
    }).on('typeahead:selected', function () { $('#Dev_UoM').trigger('input'); });

    var DevicePreferenceEngineCell = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // calls TransactionsController function nextOrdersOpen
            url: 'Admin/Preferences/getCellSizeForDevPrefs?query=%QUERY',
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            cache: false
        }
    });
    DevicePreferenceEngineCell.initialize();
    $('#Dev_Cell, #Dev_CellCF').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "DevicePreferenceEngineCell",
        displayKey: 'value',
        source: DevicePreferenceEngineCell.ttAdapter()
    });
});

function deleteDevice(deviceID) {
    if (confirm('Click OK to delete selected device.')) {
        if (confirm('Are you sure?  This action cannot be undone.  Click OK to continue.')) {
            preferencesHub.server.deleteDevicePreference(deviceID).done(function () {
                devPrefTable.draw();
            });
            return true;
        };
    };
    return false;
};

function setActiveTR($this) {
    if ($this.hasClass('active')) {
        $this.removeClass('active');
        $('#DeleteSelectedDevice, #EditSelectedDevice').attr('disabled', 'disabled');
    } else {
        $('#DevPrefsTable .active').removeClass('active');
        $this.addClass('active');
        $('#DeleteSelectedDevice, #EditSelectedDevice').removeAttr('disabled');
    };
};

function getDevicePreferenceInfo() {
    preferencesHub.server.getDevicePreferencePODInfo().done(function (obj) {
        $('#Dev_POD').val(obj.connectedPOD);
        var cf = '';
        $.each(obj.CFZones, function (index, element) {
            cf += '<option>' + element + '</option>';
        });
        $('#Dev_CF').html(cf);
    });
};