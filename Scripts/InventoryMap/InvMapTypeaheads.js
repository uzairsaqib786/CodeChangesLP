// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#LocName, #Zone').on('input', function () {
        if ($(this).val() == '') {
            $(this).typeahead('val', '');
        }
        locations.clearRemoteCache();
    });

    var itemTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/Typeahead/getItem?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    itemTypeahead.initialize();

    $('#ItemNumber').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "item",
        displayKey: 'ItemNumber',
        source: itemTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Item Number</p><p class="typeahead-header" style="width:50%;">Description</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:50%;">{{ItemNumber}}</p><p class="typeahead-row " style="width:50%;">{{Description}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '600px').css('left', 'auto')
    });

    var locations = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getLocations
            url: ('/InventoryMap/getInvMapLocZoneTypeahead?location='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#LocName').val() + '&zone=' + $('#Zone').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    var type = typeof dataObj;
                    if (type == 'string') {
                        return { Value: dataObj };
                    }
                    else { return dataObj; }
                });
            },
            cache: false
        }
    });
    locations.initialize();

    $('#LocName').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "locations",
        displayKey: 'Location',
        source: locations.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:50%;">Location</p><p class="typeahead-header " style="width:50%;">Zone</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:50%;">{{Location}}</p><p class="typeahead-row " style="width:50%;">{{Zone}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        $('#Zone').typeahead('val',data.Zone);
        setLocationZone(true);
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '500px')
    });

    $('#Zone').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "locations",
        displayKey: 'Zone',
        source: locations.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:50%;">Zone</p><p class="typeahead-header " style="width:50%;">Location</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:50%;">{{Zone}}<p class="typeahead-row " style="width:50%;">{{Location}}</p></p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        $('#LocName').typeahead('val',data.Location);
        setLocationZone(true);
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '500px')
    });

    var activeUF = 1;
    $('#UF1, #UF2').on('focus', function () {
        if (this.id == 'UF1') {
            activeUF = 1;
        } else {
            activeUF = 2;
        };
    });

    var ufs = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getUFs
            url: ('/Typeahead/getUFs?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#UF' + activeUF).val() + '&ufs=' + activeUF;
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    var type = typeof dataObj;
                    if (type == 'string') {
                        return { Value: dataObj };
                    }
                    else { return dataObj; }
                });
            },
            cache: false
        }
    });
    ufs.initialize();

    $('#UF1, #UF2').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "ufs",
        displayKey: 'Value',
        source: ufs.ttAdapter()
    }).on('typeahead:selected', function (obj, data, name) {
        // triggers input on uf1 or uf2 depending on which was sender
        $(obj.currentTarget).trigger('input');
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "600px");
    });


    var searchString = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getUFs
            url: ('/InventoryMap/getInvMapSearchStringTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + uriEncodedQuery + '&column=' + $('#selection').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    var type = typeof dataObj;
                    if (type == 'string') {
                        return { Value: dataObj };
                    }
                    else { return dataObj; }
                });
            },
            cache: false
        }
    });
    searchString.initialize();

    $('#searchString').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "searchString",
        displayKey: 'Value',
        source: searchString.ttAdapter()
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width'))
    }).on('typeahead:selected', function () {
        $('#searchString').trigger('input').blur();
    });

    $('.twitter-typeahead').css("display","inline")
});