// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var invDetailHub = $.connection.inventoryDetailHub;

var typingtime;

var createTypeAheadItemNum = function () {
    invDetailTypeAhead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/InventoryDetail/itemNumberTypeahead?itemNum=' + $('#ItemNumTA').val()),
            replace: function () {
                return '/InventoryDetail/itemNumberTypeahead?itemNum=' + $('#ItemNumTA').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return { value: dataObj };
                });
            },
            cache: false
        }
    });
    invDetailTypeAhead.initialize();
    $('#ItemNumTA').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "invDetailTypeAhead",
        displayKey: 'value',
        source: invDetailTypeAhead.ttAdapter()
    }).on('typeahead:selected', function (obj, data, name) {
        $('#ItemNumTA').trigger('input');
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "600px");
    });
};

var selItemNumData = function () {
    
    invDetailTypeAhead.clearRemoteCache();
    var sender = $('#ItemNumTA');
    invDetailHub.server.selectItemNumberInfo(sender.val()).done(function (itemData) {
        $('#ItemNumDetail').val(itemData.itemnum);
        $('#DescriptionDetail').val(itemData.description);
        $('#SupplierItemIDDetail').val(itemData.supplierid);
        $('#CarouselCellSizeDetail').val(itemData.carouselcellsize);
        $('#CategoryDetail').val(itemData.category);
        $('#CarouselVelocityDetail').val(itemData.carouselvelocity);
        $('#SubCategoryDetail').val(itemData.subcategory);
        $('#CarouselMinQtyDetail').val(itemData.carouselminqty);
        $('#UnitOfMeasureDetail').val(itemData.unitofmeasure);
        $('#CarouselMaxQtyDetail').val(itemData.carouselmaxqty);
        $('#ReorderPointDetail').val(itemData.reorderpoint);
        $('#BulkCellSizeDetail').val(itemData.bulkcellsize);
        $('#ReorderQuantityDetail').val(itemData.reorderquantity);
        $('#BulkVelocityDetail').val(itemData.bulkvelocity);
        $('#ReplenishmentPointDetail').val(itemData.replenishmentpoint);
        $('#BulkMinQtyDetail').val(itemData.bulkminqty);
        $('#ReplenishmentLevelDetail').val(itemData.replenishmentlevel);
        $('#BulkMaxQtyDetail').val(itemData.bulkmaxqty);
        if (itemData.fifo == "1") {
            $('#FIFODetail').val("Yes");
        } else {
            $('#FIFODetail').val("No");
        };
        $('#CFCellSizeDetail').val(itemData.cfcellsize);
        if (itemData.datesensitive == "1") {
            $('#DateSensitiveDetail').val("Yes");
        } else {
            $('#DateSensitiveDetail').val("No");
        };
        $('#CFVelocityDetail').val(itemData.cfvelocity);
        if (itemData.warehousesensitive == "1") {
            $('#WarehouseSensitiveDetail').val("Yes");
        } else {
            $('#WarehouseSensitiveDetail').val("No");
        };
        $('#CFMinQtyDetail').val(itemData.cfminqty);
        $('#PrimaryPickZoneDetail').val(itemData.primarypickzone);
        $('#CFMaxQtyDetail').val(itemData.cfmaxqty);
        $('#SecondaryPickZoneDetail').val(itemData.secondarypickzone);
        if (itemData.includeinautortsupdate == "1") {
            $('#IncludeInAutoRTSUpdateDetail').val("Yes");
        } else {
            $('#IncludeInAutoRTSUpdateDetail').val("No");
        };
        $('#PickFenceQtyDetail').val(itemData.pickfenceqty);
        $('#AvgPieceWeightDetail').val(itemData.avgpieceweight);
        if (itemData.splitcase == "1") {
            $('#SplitCaseDetail').val("Yes");
        } else {
            $('#SplitCaseDetail').val("No");
        }
        $('#SampleQuantityDetail').val(itemData.samplequantity);
        $('#CaseQuantityDetail').val(itemData.casequantity);
        if (itemData.usescale == "1") {
            $('#UseScaleDetail').val("Yes");
        } else {
            $('#UseScaleDetail').val("No");
        };
        if (itemData.active == "1") {
            $('#ActiveDetail').val("Yes");
        } else {
            $('#ActiveDetail').val("No");
        };
        $('#MinUseScaleQuantityDetail').val(itemData.minusescalequantity);
        $('#PickSequenceDetail').val(itemData.picksequence);
        $('#MinimumRTSReelQuantityDetail').val(itemData.minimumrtsreelquantity);
        $('#UnitCostDetail').val(itemData.unitcost);
        $('#SupplierNumberDetail').val(itemData.suppliernumber);
        $('#ManufacturerDetail').val(itemData.manufacturer);
        $('#ModelDetail').val(itemData.model);
        $('#SpecialFeaturesDetail').val(itemData.specialfeatures);
    });
};

var clearDetails = function () {
    $('input[type="text"]').val('');
    $('#ItemNumTA').typeahead('val','').focus();
};

$(document).ready(function () {
    var updateInterval;
    createTypeAheadItemNum();

    $('#ItemNumTA').on('input', function () {
        var newTime = new Date().getTime();
        if (newTime - typingtime > 200) {
            selItemNumData();
        }
        else {
            clearTimeout(updateInterval);
            updateInterval = setTimeout(function () {
                selItemNumData();
            }, 200)
        };
        typingtime = newTime;
    });

    $('#ItemNumTA').focus();
    $('#clearButtDetail').on('click', function () {
        clearDetails();
    })
});