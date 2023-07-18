// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// Handles location setting in inventory map
var trigger_locations_invmap_modal;

// Gets the zones associated with the location specified for the modal.
var fillZone = function (activeZone) {
    globalHubConn.server.getZones($('#input-location').val()).done(function (results) {
        var selectZone = $('#ZoneSelect');
        selectZone.html('');
        $(results).each(function (index, element) {
            if (activeZone == element) {
                selectZone.append('<option selected="selected">' + element + '</option>');
            } else {
                selectZone.append('<option>' + element + '</option>');
            };
        });
        selectZone.removeAttr('disabled');
    });
};

$(document).ready(function () {

    trigger_locations_invmap_modal = function (sender) {
        if ($('.activeRow').length > 0) {
            $('#location_sender').val('.activeRow input[name="' + sender.attr('name') + '"]');
        } else {
            $('#location_sender').val('input[name="' + sender.attr('name') + '"]');
        };
        var locations = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            limit: 20,
            remote: {
                // call function in controller getLocations
                url: ('/Typeahead/getLocations?query=%QUERY'),
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

        var display = '<p class="typeahead-row " style="width:50%;">{{Location}}</p><p class="typeahead-row " style="width:50%;">{{Zone}}</p>';

        $('#input-location').typeahead({
            hint: false,
            highlight: true,
            minLength: 0
        }, {
            name: "locations",
            displayKey: 'Location',
            source: locations.ttAdapter(),
            templates: {
                header: '<p class="typeahead-header " style="width:50%;">Location</p><p class="typeahead-header " style="width:50%;">Zone</p>',
                suggestion: Handlebars.compile(display)
            }
        }).on('typeahead:selected', function (obj, data, name) {
            fillZone(data.Zone);
        }).on('typeahead:opened', function () {
            $('.tt-dropdown-menu').css('width', "600px");
        });
        $('#location_modal').modal('show');
    };

    // triggers the modal
    $(document.body).on('click', '.location-modal', function () {
        trigger_locations_invmap_modal($(this));
    });

    // disables the zone input on location input as they are tied to each other
    $(document.body).on('input', '#input-location', function () {
        $('#ZoneSelect').attr('disabled', 'disabled');
    });
    
    // handles getting zones for the specified location
    $(document.body).on('focusout', '#input-location', function () {
        fillZone($('#ZoneSelect').val());
    });

    // handles submission of a zone/location combination
    $(document.body).on('click', '#location_submit', function () {
        var zone = $('#ZoneSelect').val();
        var location = $('#input-location').val();
        
        if ($('.activeRow').length > 0) {
            $('.activeRow input[name="Location"]').val(location);
            $('.activeRow input[name="Zone"]').val(zone);
            $('.activeRow input[name="Location"], .activeRow input[name="Zone"]').trigger('input');
            return true;
        };
        // use if IDs used for location/zone
        var one = $($('#location_sender').val());
        var two;
        if (one.attr('name') == 'Location') {
            two = one.parent().parent().siblings().children().find('input[name="Zone"]');
            two.val(zone);
            one.val(location);
        } else {
            two = one.parent().parent().siblings().children().find('input[name="Location"]');
            one.val(zone);
            two.val(location);
        };
        one.trigger('input'); two.trigger('input');
    });
});