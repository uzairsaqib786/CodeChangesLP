// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var userDT;
var UserFilter;

$(document).ready(function () {
    // set whether we have recently saved the user's details or if there are new changes / require save before changing the selected user
    $('#WMUsers').on('input', 'input:not(#UserToSelect)', function () {
        if ($('#UserToSelect').parents('.has-feedback').hasClass('has-warning')) {
            userTimer.isUpToDate = true;
        } else {
            userTimer.isUpToDate = false;
        };
    });

    // require an update for the user before they can switch to another
    $('#WMUsers').on('toggle', '.toggles', function () {
        if ($('#UserToSelect').parents('.has-feedback').hasClass('has-warning')) {
            userTimer.isUpToDate = true;
        } else {
            userTimer.isUpToDate = false;
        };
    });

    // numeric variables
    $('#DOPick,#MOPick,#MLPick, #DOPut,#MOPut,#MLPut, #DOCount,#MOCount,#MLCount').on('input', function () {
        setNumericInRange($(this), 0, null);
    });

    // define the columns and indexes for the custom filter datatable plugin.
    UserFilter = new FilterMenuTable({
        Selector: '#WMUserTable tbody',
        columnIndexes: [
            'Username', 'First Name', 'Last Name', 'Allow Picks', 'Auto Batch Picks', 'Default Pick Orders', 'Max Pick Orders',
            'Max Pick Lines', 'Default Pick Sort', 'Allow Puts', 'Auto Batch Put Aways', 'Default Put Orders', 'Max Put Orders', 'Max Put Lines',
            'Default Put Sort', 'Allow Counts', 'Auto Batch Counts', 'Default Count Orders', 'Max Count Orders', 'Max Count Lines',
            'Default Count Sort', 'Organize Work', 'Reports', 'WM Settings', 'Active', 'Date Stamp'
        ],
        columnMap: function () {
            var colMap = [];
            colMap['Username'] = 'Text';
            colMap['First Name'] = 'Text';
            colMap['Last Name'] = 'Text';
            var cols = ['Default Pick Orders', 'Max Pick Orders', 'Max Pick Lines', 'Default Put Orders', 'Max Put Orders', 'Max Put Lines',
                'Default Count Orders', 'Max Count Orders', 'Max Count Lines'];
            for (var i = 0; i < cols.length; i++) { colMap[cols[i]] = 'Number'; };
            cols = ['Active', 'WM Settings', 'Organize Work', 'Reports', 'Allow Picks', 'Auto Batch Picks', 'Allow Puts', 'Auto Batch Put Aways', 'Allow Counts', 'Auto Batch Counts'];
            for (var i = 0; i < cols.length; i++) { colMap[cols[i]] = 'Bool'; };
            return colMap;
        }(),
        dataTable: userDT,
        ignoreColumns: [

        ]
    });

    // define the user datatable
    userDT = $('#WMUserTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [
            [
                0, 'asc'
            ]
        ],
        columnDefs: [
            {
                targets: [3, 4, 5, 6],
                visible: false
            }
        ],
        "ajax": {
            "url": "/WM/Preferences/GetWMUsersTable",
            "data": function (d) {
                d.entryFilter = UserFilter.getFilterString()
            }
        },
        'paging': true,
        pageLength: 10
    });

    $('#WMUserTable').on('filterChange', function () {
        userDT.draw();
    });

    // select a user if the current user is empty or has been saved.
    $('#WMUserTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            var active = $('#WMUserTable tbody').find('tr.active');
            var sameUser = $this.children(':first').html() == $('#Username').val();
            if (!sameUser && !userTimer.isUpToDate && confirm('There are pending changes for this user.  Click OK to save these changes.  Click Cancel to discard the changes.')) {
                userTimer.timerExpiredAction();
            };
            active.removeClass('active');
            $this.addClass('active');
            if (!sameUser) {
                var userArray = userDT.row('.active').data();
                var data = {
                    un: userArray[0],
                    fn: userArray[1],
                    ln: userArray[2],
                    extraData: userArray.slice(3, userArray.length)
                };
                setUsername(data);
            };
        };
    });

    // define the user typeahead for work manager
    var userTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/WM/Preferences/GetEmployeeTA?lastname='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#UserToSelect').val();
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

    $('#UserToSelect').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "userTypeahead",
        displayKey: 'ln',
        source: userTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:33%;">Last Name</p><p class="typeahead-header" style="width:33%">First Name</p><p class="typeahead-header" style="width:33%">Username</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:33%;">{{ln}}</p><p class="typeahead-row" style="width:33%;">{{fn}}</p><p class="typeahead-row" style="width:33%;">{{un}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        if (!userTimer.isUpToDate && confirm('There are pending changes for this user.  Click OK to save these changes.  Click Cancel to discard the changes.')) {
            userTimer.timerExpiredAction();
            $('#UserToSelect').trigger('input').focus();
        } else {
            setUsername(data);
            var row = $('#WMUserTable').find('tr:nth-child(' + (userDT.column(0).data().indexOf(data.un) + 1) + ')'); // + 1 because nth-child is 1-indexed
            if (row.length > 0) {
                row.click();
            };
        };
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width')).css('height', '300px').css('left', 'auto');
    });

    // assign a selected username and its related details to the html inputs, if the worker is new ask the user if they wish to save.
    function setUsername(data) {
        if (data.extraData.length > 0 && data.extraData[1] != '') {
            $('#UserToSelect').parents('.has-warning').removeClass('has-warning').addClass('has-success')
                .find('.glyphicon-warning-sign').removeClass('glyphicon-warning-sign').addClass('glyphicon-ok').attr('data-original-title', 'A worker is selected.').tooltip('fixTitle');
            var u$ = $('#WMUsers');
            u$.find('.toggles').removeClass('disabled');
            u$.find('input:not(#UserToSelect,#FirstName,#Username),button').removeAttr('disabled');
            var selectStr = [$('#Active'), $('#WMSettings'), $('#OrganizeWork'), $('#Reports')];
            $.each(['Pick', 'Put', 'Count'], function (iType, vType) {
                $.each(['AT', 'AB', 'DO', 'MO', 'ML'], function (iCol, vCol) {
                    selectStr.push($('#' + vCol + vType));
                });
            });
            $.each(selectStr, function (i, v) {
                if (v.hasClass('toggles')) {
                    v.data('toggles').setValue(data.extraData[i].toLowerCase() == 'true');
                } else {
                    v.val(data.extraData[i]);
                };
            });
            $('#FirstName').val(data.fn);
            $('#Username').val(data.un);
            $('#UserToSelect').val(data.ln);
        } else if (confirm('The selected user is not currently in WM Users.  Click OK to add this user.')) {
            prefsHub.server.addNewWorker(data.un).done(function (added) {
                if (!added) {
                    MessageModal('Error', 'There was an error while attempting to add ' + data.un + ' to the WM Users table.');
                } else {
                    $('#UserToSelect').trigger('input').focus();
                    userDT.draw();
                };
            });
        };
    };

    // require a username to be selected from the typeahead before changes are allowed.
    function clearUsername() {
        $('#UserToSelect').parents('.has-success').removeClass('has-success').addClass('has-warning')
            .find('.glyphicon-ok').removeClass('glyphicon-ok').addClass('glyphicon-warning-sign').attr('data-original-title', 'You must select a worker from the dropdown.').tooltip('fixTitle');
        var u$ = $('#WMUsers');
        u$.find('.toggles').each(function (i, e) {
            $(e).addClass('disabled').data('toggles').setValue(false);
        });
        u$.find('input:not(#UserToSelect)').attr('disabled', 'disabled').val('');
        u$.find('button').attr('disabled', 'disabled');
    };

    // save or clear depending on user input and whether the user is up to date.
    $('#UserToSelect').on('input', function () {
        if (!userTimer.isUpToDate && confirm('There are pending changes for this user.  Click OK to save these changes.  Click Cancel to discard the changes.')) {
            userTimer.timerExpiredAction();
        } else {
            clearUsername();
        };
    });

    // define a timer object.  Saves on the timer with the provided function parameter when .startTimer() is called.  Saves immediately with .timerExpiredAction()
    var userTimer = mkTimer(function () {
        if ($('#UserToSelect').parents('.has-feedback').hasClass('has-warning')) {
            MessageModal('Error', 'You must select a worker before the record can be saved.');
            $('#UserToSelect').trigger('input').focus();
        } else {
            var user = $('#Username').val();
            var active = toSqlBool($('#Active').data('toggles').active);
            var wm = toSqlBool($('#WMSettings').data('toggles').active)
            var organize = toSqlBool($('#OrganizeWork').data('toggles').active)
            var reports = toSqlBool($('#Reports').data('toggles').active);
            var picks = new Array();
            var puts = new Array();
            var counts = new Array();
            var s, v;
            $.each(['Pick', 'Put', 'Count'], function (iType, vType) {
                $.each(['AT', 'AB', 'DO', 'MO', 'ML'], function (iCol, vCol) {
                    s = $('#' + vCol + vType);
                    v = toSqlBool((s.hasClass('toggles') ? s.data('toggles').active : s.val()));
                    switch (vType) {
                        case 'Pick':
                            picks.push(v);
                            break;
                        case 'Put':
                            puts.push(v);
                            break;
                        case 'Count':
                            counts.push(v);
                            break;
                    };
                });
            });
            $('#SaveUser').attr('disabled', 'disabled');
            prefsHub.server.saveWMUser(user, active, wm, organize, reports, picks, puts, counts).done(function (saved) {
                $('#SaveUser').removeAttr('disabled');
                if (!saved) {
                    MessageModal('Error', 'There was an error while attempting to save the worker.  Check the error log for details.');
                } else {
                    userTimer.isUpToDate = true;
                };
                $('#UserToSelect').trigger('input').focus();
                userTA.clearRemoteCache();
                userDT.draw();
            });
        };
    });

    // save the user, but don't allow the user to click the button too often to overload the server with queries.
    $('#SaveUser').click(function () {
        userTimer.startTimer();
    });

    // remove a worker from work manager, they will remain in the main Employees table
    $('#RemoveWorker').click(function () {
        var username = $('#Username').val();
        $(this).attr('disabled', 'disabled');
        if (confirm('Click OK to remove worker (user: ' + username + ', last: ' + $('#UserToSelect').val() + ', first: ' + $('#FirstName').val() + ') from the Work Manager user list.')) {
            prefsHub.server.removeWorker(username).done(function (removed) {
                if (!removed) {
                    MessageModal('Error', 'There was an error while attempting to remove the worker: ' + $('#Username').val() + '.  Check the error log for details');
                };
                clearUsername();
                userDT.draw();
            });
        };
    });

    // clears batches associated with the selected user
    $('#ClearBatch').click(function () {
        var username = $('#Username').val();
        $(this).attr('disabled', 'disabled');
        if (confirm('Click OK to clear the Batch IDs assigned to the current worker (' + username + ')')) {
            var pick = toSqlBool($('#ATPick').data('toggles').active);
            var put = toSqlBool($('#ATPut').data('toggles').active);
            var count = toSqlBool($('#ATCount').data('toggles').active);
            prefsHub.server.clearBatches(username, pick, put, count).done(function (cleared) {
                if (!cleared) {
                    MessageModal('Error', 'There was an error while attempting to clear the batches for user: ' + username + '.  Check the error log for details.');
                };
                $('#ClearBatch').removeAttr('disabled');
            });
        };
    });

    $(document.body).on('click', '#PrintAll,#PrintWorker', function () {
        var user = $('#Username').val();
        if (this.id == 'PrintAll' || user == '') {
            user = 'all';
        };
        var pd = $('#PrintDirect').data('toggles').active;
        getLLPreviewOrPrint('/WM/Preferences/PrintWorker', {
            printDirect: pd,
            userToPrint: user
        }, pd,'report', 'Worker Detail');
    });
});