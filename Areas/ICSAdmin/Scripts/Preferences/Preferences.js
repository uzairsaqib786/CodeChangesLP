// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var preferencesHub = $.connection.preferencesHub;

$(document).ready(function () {
    $('.toggles').toggles({
        width: 60,
        height: 25
    });
    
    $('a[href="#system"], a[href="#workstation"]').on('show.bs.tab', function () {
        var $this = $(this);
        if ($this.attr('href') == '#system') {
            getSysPrefs($('#system li.active a'));
        } else {
            getWSPrefs($('#workstation li.active a'));
        };
    });

    $('#system a').on('show.bs.tab', function () {
        getSysPrefs($(this));
    });

    $('#workstation a').on('show.bs.tab', function () {
        getWSPrefs($(this));
    });

    $.connection.hub.start().done(function () {
        getOSFilters();
        getGeneralPreferences();
        $('#PageLoad').hide();
        $('#PageLoadDone').show();
    });
});

function getWSPrefs($this) {
    switch ($this.attr('href')) {
        case '#WorkstationInfo':
            getWorkstationSetup();
            break;
        case '#BulkZones':
            getBulkZones();
            break;
        case '#PODSetup':
            getPODSetup();
            break;
        case '#SortBar':
            getSortBar();
            break;
        case '#PickLevels':
            getPickLevels();
            break;
        case '#CustApps':
            console.log('not done');
            break;
        case '#MiscSett':
            getMiscSettings();
            break;
    };
};

function getSysPrefs($this) {
    switch ($this.attr('href')) {
        case '#setup':
            getGeneralPreferences();
            break;
        case '#locationzones':
            getLocationZones();
            break;
        case '#ufs':
            getFieldNames();
            break;
        case '#bpsettings':
            getBulkSettings();
            break;
        case '#lookupList':
            getActiveLookupListTab();
            break;
        case '#devPrefs':
            getDevicePreferenceInfo();
            break;
    };
};

function getActiveLookupListTab() {
    var uf = getActiveUF();
    if (uf == 1 || uf == 2) {
        getUFSuggestions(uf);
    } else {
        var adjustment = $('#LookupListSetupNav').children('.active').children().attr('href') == '#adjustmentSetup';
        if (adjustment) {
            getAdjustmentLookup();
        } else {
            getToteSetup();
        };
    };
};