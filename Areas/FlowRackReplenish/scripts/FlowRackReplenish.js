// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var FlowRackHub = $.connection.flowRackReplenishHub;

$(document).ready(function () {

    $('#LocationRow, #submitBtn, #itemQtyRow, #alertarea').hide();

    var locationSuggestions = [];


    $(document.body).on('focus', '.tt-input', function () {
        $(this).val('');
        triggerDownClick($(this));
    });

    // Displays the current carton flow zone that the user is in.
    $.connection.hub.start().done(function () {
        FlowRackHub.server.getWSLoc().done(function (data) {
            if (data != "No" && data != null) {
                $('#CurrentZone').append("<strong style='padding-left:15px'>" + data + "</strong>")
            }
            else {
                $('#CurrentZone').append("This workstation is not assigned to a zone");
            }
        });
    });


    //Event handler for focusing on element hitting the enter key
    $('#itemnumscan, #itemLocation, #itemQty').keypress(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger("enterKey");
        }
    });


    $('#itemQty').bind("enterKey", function () {
        updateItemQty();
    });

    $('#submitBtn').bind("click", function () {
        updateItemQty();
    });

    $('#itemQty').change(function () {
        if ($('#itemQty').val() !== 0) {
            $('#submitBtn').show();
            $(this).focus();
        }
        if ($('#itemQty').val() === 0 || $('#itemQty').val() === '') {
            $('#submitBtn').hide();
        }


        
    });


    $('#clearitemnum').on("click", function () {
        clearAllFields();
    });

    $('#itemnumscan').on("change", function () {
        if ($('#itemnumscan').val() === '') {
            clearAllFields();
        } else {
            clearAllFields();
        }
    });

    $('#itemLocation').on("change", function () {
        clearQtyField();
    });

    $('#itemLocation').bind('enterKey', function () {
        onLocationSelected($('#itemLocation').val());
    });
 
    // finds item and displays typeahead for all locations that items exists as well as open locations.
    $('#itemnumscan').unbind('enterkey').bind("enterKey", function (e) {
        FlowRackHub.server.getCFData($('#itemnumscan').val()).done(function (data) {
            if (data != null) {
                $('#itemnumscan').val(data);
                FlowRackHub.server.getItemLoc($('#itemnumscan').val()).done(function (data) {
                    $('#alertarea').empty().hide();
                    if (data.length < 1) {
                        locationSuggestions = [];
                        FlowRackHub.server.getOpenLoc().done(function (data) {
                            if (data.length < 1) {
                                locationSuggestions = [];
                                $('#alertarea').append("There are no open locations.").show();
                                $('#LocationRow').hide();
                                $('#itemLocation').val('').typeahead('destroy');
                            }
                            else {
                                $('#alertarea').append("No Locations found for Item Number, Scan or Select an open Location").show();
                                locationSuggestions = data;
                                initTypeAhead();
                                $('#LocationRow').show();
                                $('#itemLocation').focus().select();
                            }
                        });
                    }
                    else {
                        locationSuggestions = data;
                        initTypeAhead();
                    }
                    $('#LocationRow').show();
                    $('#itemLocation').focus().select();

                });

            } else {
                $('#itemnumscan').val('');
                MessageModal("Warning", "This item does not exist in Inventory Master for this carton flow zone.");
                clearAllFields();
            }
        });
    });

    function findLocationsTH(q, cb) {
        return cb(locationSuggestions);
    }

    // Initiates typeahead for location field.
    function initTypeAhead() {
        $('#LocationRow .typeahead').typeahead({
            hint: false,
            highlight: false,
            minLength: 0
        }, {
            name: "FRRLocations",
            displayKey: 'Location',
            source: findLocationsTH,
            templates: {
                header: '<p class="typeahead-header" style="width:50%;">Location</p><p class="typeahead-header" style="width:50%;">Item Quantity</p>',
                suggestion: Handlebars.compile('<p class="typeahead-row" style="width:50%;">{{Location}}</p><p class="typeahead-row" style="width:50%;">{{ItemQuantity}}</p>')
            }
        }).on('typeahead:opened', function () {
            $('.tt-dropdown-menu').css('width', '550px').css('left', 'auto');
        }).off('typeahead:selected').on('typeahead:selected', function (obj, data, name) {
            // What to do when location is selected
            onLocationSelected($('#itemLocation').val());
        });
    }



    // Location selected, verify, then show qty field
    function onLocationSelected(location) {
        FlowRackHub.server.verifyItemLoc($('#itemnumscan').val(), location).done(function (data) {
            if (data) {
                $('#alertarea').empty().hide();
                $('#itemQtyRow').show();
                $('#itemQty').val('').focus();
                $('#itemQtyNumPad').click();
            } else {
                clearLocationField();
                $('#LocationRow').hide();
                $('#itemnumscan').focus();
                MessageModal("Warning", "Location unavailable");
            }
        });
    };

    function triggerDownClick(element) {
        ev = $.Event("keydown");
        ev.keyCode = ev.which = 40;
        element.trigger(ev);
    };

    // Validates item number, location and quantity and then updates database.
    function updateItemQty() {
        if ($('#itemQty').val() <= 0) {
            MessageModal("Warning", "Quantity can not be negative");
            $('#itemQty').val('');
            $('#itemQty').focus();
        }
        if ($('#itemQty').val() === '') {
            MessageModal("Warning", "Please enter a quantity");
            $('#itemQty').focus();
        }

        else {
            FlowRackHub.server.verifyItemQty($('#itemnumscan').val(), $('#itemLocation').val(), $('#itemQty').val()).done(function (data) {
                if (data) {
                    FlowRackHub.server.updateItemQty($('#itemnumscan').val(), $('#itemLocation').val(), $('#itemQty').val()).done(function () {
                        resetForm();
                    });
                } else {
                    MessageModal("Warning", "The quantity was not entered due to an error in the Inventory Map");
                    $('#itemQty').val('');
                    $('#itemQty').select();
                };
            });
        }

    }


    // Reset form back to default values.
    function resetForm() {
        $('#submitBtn').hide();
        $('#itemQty').val('');
        $('#itemQtyRow').hide();
        $('#itemLocation').val('');
        $('#LocationRow').hide();
        $('#itemnumscan').val('');
        $('#itemnumscan').focus();
        $('#itemLocation').val('').typeahead('destroy');
    };



    //Clears the Enter Quantity field and hides it.
    function clearQtyField() {
        $('#submitBtn').hide();
        $('#itemQty').val('');
        $('#itemQtyRow').hide();
    }

    //Clears all fields below the Select Location field
    function clearLocationField() {
        $('#submitBtn').hide();
        $('#itemQty').val('');
        $('#itemQtyRow').hide();
        $('#itemLocation').val('');
        $('#itemLocation').val('').typeahead('destroy');
        $('#alertarea').empty().hide();

    }

// Clears/hides1-110 all fields except for Scan Item
    function clearAllFields () {
        $('#submitBtn').hide();
        $('#itemQty').val('');
        $('#itemQtyRow').hide();
        $('#itemLocation').val('');
        $('#LocationRow').hide();
        $('#itemnumscan').val('');
        $('#itemnumscan').focus();
        $('#alertarea').empty().hide();
        $('#itemLocation').val('').typeahead('destroy');

    }

});