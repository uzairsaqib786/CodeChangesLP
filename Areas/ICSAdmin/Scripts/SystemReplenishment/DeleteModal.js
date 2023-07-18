// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var trigger_delete__modal;

var startDeleteView;

$(document).ready(function () {
    startDeleteView();
    trigger_delete_modal = function () {
        $('#delete_modal').modal('show');
    };

    $(document.body).on('click', '#deleteRange', function () {
        trigger_delete_modal();
    });

    $(document.body).on('click', '#deleteRangeSubmit', function () {
        var start = $('#BeginDelete').val();
        var end = $('#EndDelete').val();
        var filter = $('#deleteColumn').val();

        sysRepHub.server.deleteReplenishmentsBy(filter, start, end, "", "", "").done(function (success) {
            $('#delete_modal').modal('hide');
            if (!success) {
                alert("Deleting by range has failed")
            } else {
                currReplenTable.draw(true)
            };
        });
    });
});

startDeleteView = function () {
    var begin = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('BeginLocation'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getbegin
            url: ('/Admin/SystemReplenishment/getDeleteBegin?query='),
            replace: function (url, uriEncodedQuery) {
                return url + uriEncodedQuery + '&delCol=' + $('#deleteColumn').val();
            },
            filter: function (list) {
                return $.map(list, function (column) { return { BeginLocation: column }; });
            },
            cache: false
        }
    });
    begin.initialize();

    $('#BeginDelete').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "begin",
        displayKey: 'BeginLocation',
        source: begin.ttAdapter()
    }).on('typeahead:selected', function (obj, data, name) {
        $('#BeginDelete').trigger('input');
    }).parent().css({ 'display': 'inline' });

    var end = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('EndLocation'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getend
            url: ('/Admin/SystemReplenishment/getDeleteEnd?query='),
            replace: function (url, uriEncodedQuery) {
                return url + uriEncodedQuery + '&begin=' + $('#BeginDelete').val() + '&delCol=' + $('#deleteColumn').val();
            },
            filter: function (list) {
                return $.map(list, function (column) { return { EndLocation: column }; });
            },
            cache: false
        }
    });

    end.initialize();

    $('#EndDelete').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "end",
        displayKey: 'EndLocation',
        source: end.ttAdapter()
    }).on('typeahead:selected', function (obj, data, name) {
        $('#EndDelete').trigger('input');
    }).parent().css({ 'display': 'inline' });
};