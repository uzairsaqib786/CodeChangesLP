// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    //BEGIN LOCATION
    var begin = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getbeginToLocation
            url: ('/Typeahead/getLocationBegin?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#FromLocation').val() + '&unique=true';
            },
            cache: false
        }
    });
    begin.initialize();

    $('#FromLocation').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "begin",
        displayKey: 'Location',
        source: begin.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:100%;">Location</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{Location}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        FillBatchResultTable();
        $('#FromLocation').trigger('input');
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')).css('left', 'auto');
    });

    //END LOCATION
    var end = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getend
            url: ('/Typeahead/getLocationEnd?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#ToLocation').val() + '&unique=true' + '&beginLoc=' + $('#FromLocation').val();
            },
            cache: false
        }
    });

    end.initialize();

    $('#ToLocation').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "end",
        displayKey: 'Location',
        source: end.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:100%;">Location</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{Location}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        FillBatchResultTable();
        $('#ToLocation').trigger('input');

    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', $(this).css('width')).css('left', 'auto');
    });

    //BEGIN ITEM
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

    $('#FromItem').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "item",
        displayKey: 'ItemNumber',
        source: itemTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Item Number</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{ItemNumber}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '400px');
    }).on('typeahead:selected', function (obj, data, name) {
        FillBatchResultTable();
    });


    //END ITEM
    var itemTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/Typeahead/getItem?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#ToItem').val() + '&beginItem=' + $('#FromItem').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    itemTypeahead.initialize();

    $('#ToItem').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "item",
        displayKey: 'ItemNumber',
        source: itemTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Item Number</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{ItemNumber}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '400px');
    }).on('typeahead:selected', function (obj, data, name) {
        FillBatchResultTable();
    });

    //Description
    var descriptionTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            //call controller function for description
            url: ('/Admin/CycleCount/getCycleCountDescriptionTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#Description').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    descriptionTypeahead.initialize();

    $('#Description').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "description",
        displayKey: 'Description',
        source: descriptionTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Description</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{Description}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '400px');
    }).on('typeahead:selected', function (obj, data, name) {
        FillBatchResultTable();
    });

    //Category
    var categoryTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            //call controller function for category
            url: ('/Admin/CycleCount/getCycleCountCategoryTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#Category').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    categoryTypeahead.initialize();

    $('#Category').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "category",
        displayKey: 'Category',
        source: categoryTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Category</p><p class="typeahead-header" style="width:50%;">SubCategory</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:50%;">{{Category}}</p><p class="typeahead-row " style="width:50%;">{{SubCategory}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '400px');
    }).on('typeahead:selected', function (obj, data, name) {
        $('#SubCategory').val(data.SubCategory);
        FillBatchResultTable();
    });

    //From Cost
    var fromCostTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            //call controller function for from cost
            url: ('/Admin/CycleCount/getCycleCountFromCostTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#CostStart').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    fromCostTypeahead.initialize();

    $('#CostStart').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "fromcost",
        displayKey: 'FromCost',
        source: fromCostTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">From Cost</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{FromCost}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '400px');
    }).on('typeahead:selected', function (obj, data, name) {
        $('#CostStart').trigger('input');
        if ($('#CostEnd').val() != "") {
            FillBatchResultTable();
        }
    });

    //To Cost
    var toCostTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            //call controller function for to cost
            url: ('/Admin/CycleCount/getCycleCountToCostTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#CostEnd').val() + '&fromCost=' + $('#CostStart').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    toCostTypeahead.initialize();

    $('#CostEnd').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "tocost",
        displayKey: 'ToCost',
        source: toCostTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">To Cost</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:100%;">{{ToCost}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '400px');
    }).on('typeahead:selected', function (obj, data, name) {
        $('#CostEnd').trigger('input');
        if ($('#CostStart').val() != "") {
            FillBatchResultTable();
        }
    });
});