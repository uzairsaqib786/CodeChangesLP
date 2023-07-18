// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // initializes the typeaheads for employee locations
    emTypeaheads();
    // starting location input for an employee
    var startlocationsender;
    // ending location input for an employee
    var endlocationsender;
    // adds a new location range for the specified employee
    $(document.body).on('click', '#addLocation', function () {
        startlocationsender = null;
        endlocationsender = null;

        $('#emlocations_start').val(null);
        $('#emlocations_end').val(null);
        $('#emlocations_modal').modal('show');
    });

    // launches the location modal with data from the sending input
    $(document.body).on('click', '.slocation-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        $this.addClass('edit');
        startlocationsender = $this;
        endlocationsender = $this.parent().parent().siblings().find('input.elocation-modal');

        $('#emlocations_start').val(startlocationsender.val());
        $('#emlocations_end').val(endlocationsender.val());
        $('#emlocations_modal').modal('show');
    });
    $(document.body).on('click', '.elocation-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        $this.addClass('edit');
        startlocationsender = $this.parent().parent().siblings().find('input.slocation-modal');
        endlocationsender = $this;

        $('#emlocations_start').val(startlocationsender.val());
        $('#emlocations_end').val(endlocationsender.val());
        $('#emlocations_modal').modal('show');
    });
    // Handles saving a new or edited set of locations for a user
    $(document.body).on('click', '#emlocations_submit', function () {
        var sl = $('#emlocations_start').val();
        var el = $('#emlocations_end').val();
        var username = $('#userName').val();

        var save = true;
        $('input.slocation-modal:not(.edit)').each(function () {
            var $this = $(this);
            var slocation = $this.attr('name');
            var elocation = $this.parent().parent().siblings().find('input.elocation-modal').attr('name');
            if (sl == slocation && el == elocation) {
                save = false;
            };
        });

        if (save) {
            deleteEMLocationsAlert();
            if (startlocationsender == null) {
                employees.server.addEmployeeLocation(username, sl, el).done(function () {
                    $('#locationsAppend').append('<div class="row"><div class="col-md-5"><div class="form-group has-feedback" style="margin-bottom:0px;"><input class="form-control modal-launch-style slocation-modal" readonly type="text" value="' + sl + '" name="' + sl + '"/><i class="glyphicon glyphicon-resize-full form-control-feedback slocation-modal modal-launch-style" style="top:0px;"></i></div></div><div class="col-md-5"><div class="form-group has-feedback" style="margin-bottom:0px;"><input class="form-control modal-launch-style elocation-modal" readonly type="text" value="' + el + '" name="' + el + '" /><i class="glyphicon glyphicon-resize-full form-control-feedback elocation-modal modal-launch-style" style="top:0px;"></i></div></div><div class="col-md-1"><button class="btn btn-danger remove-locationrange" style="margin-bottom:5px;"> <span class="glyphicon glyphicon-remove"></span></button></div></div>');
                    $('#emlocations_modal').modal('hide');
                });
            } else {
                var osl = startlocationsender.attr('name');
                var oel = endlocationsender.attr('name');

                if (sl == osl && el == oel) {
                    $('#emlocations_modal').modal('hide');
                    return true;
                };

                employees.server.updateEmployeeLocation(username, osl, sl, oel, el).done(function () {
                    startlocationsender.attr('name', sl);
                    startlocationsender.val(sl);
                    endlocationsender.attr('name', el);
                    endlocationsender.val(el);

                    $('.slocation-modal.edit').removeClass('edit');
                    $('#emlocations_modal').modal('hide');
                });
            };
        } else {
            postEMLocationsAlert('The range specified already exists for this user.  Please specify a unique range.');
        };
    });

    // deletes a specified range of locations
    $(document.body).on('click', '.remove-locationrange', function () {
        var $this = $(this);
        var sl = $this.parent().siblings().find('input.slocation-modal').val();
        var el = $this.parent().siblings().find('input.elocation-modal').val();
        var username = $('#userName').val();

        var result = confirm('Click OK to delete the location range starting at ' + sl + ' and ending at ' + el + ' for user ' + username);
        if (result) {
            employees.server.deleteEmployeeLocation(username, sl, el).done(function () {
                $this.parent().parent().remove();
            });
        };
    });
});

// posts an employee locations alert
function postEMLocationsAlert(message) {
    $('#emlocations_alerts').html('<div class="alert alert-warning alert-custom" role="alert">' + message + '</div>');
};

// deletes employee locations alerts
function deleteEMLocationsAlert() {
    $('#emlocations_alerts').html('');
};

// initializes the employee locations typeaheads
function emTypeaheads() {
    var begin = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getbegin
            url: ('/Typeahead/getLocationBegin?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#emlocations_start').val() + '&unique=true';
            },
            cache: false
        }
    });
    begin.initialize();

    $('#emlocations_start').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "begin",
        displayKey: 'Location',
        source: begin.ttAdapter()
    });

    var end = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getend
            url: ('/Typeahead/getLocationEnd?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#emlocations_end').val() + '&unique=true&beginLoc=' + $('#emlocations_start').val();
            },
            cache: false
        }
    });

    end.initialize();

    $('#emlocations_end').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "end",
        displayKey: 'Location',
        source: end.ttAdapter()
    });
};