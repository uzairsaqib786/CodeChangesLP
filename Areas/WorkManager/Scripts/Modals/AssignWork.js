// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var orgHub;

$(document).ready(function () {
    orgHub = $.connection.organizeWorkHub;
    
    // clears work for the currently selected user.
    $('#ClearWork').click(function () {
        if (confirm('Click OK to clear all current work for the user(s) selected.')) {
            orgHub.server.clearCurrentWork($('#AssignToWorker').data('username'), toSqlBool($('#AssignPicks').prop('checked')),
                toSqlBool($('#AssignPuts').prop('checked')), toSqlBool($('#AssignCounts').prop('checked'))).done(function (cleared) {
                    if (!cleared) {
                        MessageModal('Error', "There was an error while attempting to clear the user's assigned work.");
                    } else {
                        MessageModal('Alert', "The user's assigned current work has been cleared successfully.");
                    };
                });
        };
    });

    // clears the batch ids for the selected user
    $('#ClearBatchIDs').click(function () {
        if (confirm('Click OK to clear the selected user(s) batches.')) {
            orgHub.server.clearBatchIDs($('#AssignToWorker').data('username'), toSqlBool($('#AssignPicks').prop('checked')),
                toSqlBool($('#AssignPuts').prop('checked')), toSqlBool($('#AssignCounts').prop('checked'))).done(function (cleared) {
                    if (!cleared) {
                        MessageModal('Error', "There was an error while attempting to clear the batches.");
                    } else {
                        MessageModal('Alert', "The batches have been cleared successfully.");
                    };
                });
        };
    });

    // creates batches for the current user with(out) their username as <batch id>|<username>
    $('#BatchAndUsername,#BatchOnly').click(function () {
        var withUsername = ($(this).attr('ID') == 'BatchAndUsername');
        if (confirm('Click OK to create batches' + (withUsername ? ' with username' : '') + ' now.')) {
            orgHub.server.createBatch(withUsername, $('#AssignToWorker').data('username'), toSqlBool($('#AssignPicks').prop('checked')),
                toSqlBool($('#AssignPuts').prop('checked')), toSqlBool($('#AssignCounts').prop('checked')), ($('#CurrBatchAction').val() == 'cleared')).done(function (created) {
                    if (!created) {
                        MessageModal('Error', 'There was an error while attempting to create batches with the selected criteria.');
                    } else {
                        MessageModal('Alert', 'The batches were created successfully.');
                    };
                    if ($('#UnselectedTable').length > 0) {
                        UnselectedWorkDT.draw();
                        SelectedWorkDT.draw();
                    };
                });
       };
    });

    // user typeahead for work manager users
    var userTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/WM/OrganizeWork/GetWorkerTA?worker='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#AssignToWorker').val() + '&all=True';
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    userTA.initialize();

    $('#AssignToWorker').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "userTypeahead",
        displayKey: 'name',
        source: userTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Name</p><p class="typeahead-header" style="width:50%">Username</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:50%;">{{name}}</p><p class="typeahead-row" style="width:50%;">{{username}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        updateStatus(data.username);
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width')).css('height', '300px').css('left', 'auto');
    });

    $('#AssignToWorker').on('input', function () {
        updateStatus(null);
    });

    // set whether the "selected" user is valid or if the user needs to explicitly click on a typeahead entry again.
    function updateStatus(username) {
        var a = $('#AssignToWorker');
        a.data('username', username);
        if (username == null) {
            $('#ClearWork,#ClearBatchIDs,#BatchAndUsername,#BatchOnly').attr('disabled', 'disabled');
            a.parents('.has-success').addClass('has-warning').removeClass('has-success')
                .find('.glyphicon-ok').addClass('glyphicon-warning-sign').removeClass('glyphicon-ok').attr('data-original-title', 'You must select a worker from the dropdown.').tooltip('fixTitle');
        } else {
            $('#ClearWork,#ClearBatchIDs,#BatchAndUsername,#BatchOnly').removeAttr('disabled', 'disabled');
            a.parents('.has-warning').removeClass('has-warning').addClass('has-success')
                .find('.glyphicon-warning-sign').removeClass('glyphicon-warning-sign').addClass('glyphicon-ok').attr('data-original-title', 'A worker is selected').tooltip('fixTitle');
        };
    };
});