// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// selected datatable filter plugin instance
var SelectedWorkFilter;
// selected datatable instance
var SelectedWorkDT;
// unselected datatable filter plugin instance
var UnselectedWorkFilter;
// unselected datatable instance
var UnselectedWorkDT;
// connection to the work manager hub
var workHub;
$(document).ready(function () {
    var self = $('#WorkerTA').length == 0;
    var printDirect = ($('#PrintDirect').val() == 'true');
    workHub = $.connection.selectWorkHub;
    // object containing methods and properties related to the selected transactions
    var selected = {
        // gets the list of selected items in a csv string.  Single quotes surround entries only when quoted = true
        // used for IN clauses and reading from a string into a temporary table
        getSelectedString: function (quoted) {
            var str = '';
            for (var k = 0; k < this.selectedList.length; k++) {
                if (quoted) {
                    str += ("'" + this.selectedList[k] + "',");
                } else {
                    str += (this.selectedList[k] + ",");
                };
            };
            if (str == '') {
                return 'NULL';
            } else {
                return str.substring(0, str.length - 1);
            };
        },
        // the selected transactions are stored here, use remove/add Item in order to ensure that there are no duplicates in the selected list
        selectedList: new Array(),
        // removes the specified item from the selected list if it exists there
        removeItem: function (item) {
            var i = this.selectedList.indexOf(item);
            if (i > -1) {
                this.selectedList.splice(i, 1);
                this.filterChanged();
            };
        },
        // adds the specified item to the selected list if it doesn't exist there
        addItem: function (item) {
            if (this.selectedList.indexOf(item) < 0) {
                this.selectedList.push(item);
                this.filterChanged();
            };
        },
        // clears the selected list
        clearAll: function () {
            this.selectedList = new Array();
            this.filterChanged();
        },
        // fires a refresh event when there is self change in the selected list.  Only fires when official methods are used to add/remove from the selected list (addItem, removeItem)
        filterChanged: function () {
            this.timer.startTimer();
        },
        // instance of the timer plugin to refresh the count data and datatables
        timer: mkTimer(function () {
            SelectedWorkDT.draw();
            UnselectedWorkDT.draw();
            getCounts();
        }),
        // determines which transaction type is selected by the user
        getTransType: function () {
            var ttype = $('[name="PickPutCount"]:checked').attr('ID').toLowerCase().replace('s', '');
            if (ttype == 'put') {
                return 'put away'
            } else {
                return ttype;
            };
        },
        // determines whether the user has selected batches, totes or orders
        getBatchOrderTote: function () {
            var batch = $('[name="BatchOrderTote"]:checked')[0].id.toLowerCase();
            if (batch == 'batches') {
                return 'batch';
            } else if (batch == 'orders') {
                return 'order';
            } else {
                return 'tote';
            };
        },
        // executes the print request to the print service for a preview or direct print depending on the selected options and preferences
        print: function (which) {
            var transType = this.getTransType();
            var batchOrderTote = this.getBatchOrderTote();
            var selStr = this.getSelectedString(false);
            var endUrl = '';
            var title;
            var reportType = '';
            switch (which.toLowerCase()) {
                case 'work':
                    title = 'Work List - ' + transType.toTitleCase() + 's';
                    endUrl = 'PrintWMWorkList';
                    reportType = 'report'
                    break;
                case 'item':
                    title = 'Item Label';
                    endUrl = 'PrintWMItemLabel';
                    reportType = 'report';
                    break;
                case 'tote':
                    title = 'Tote Label';
                    endUrl = 'printWMToteLabel';
                    reportType = 'report';
            };
            getLLPreviewOrPrint('/WM/SelectWork/' + endUrl, {
                transType: transType,
                batchOrderTote: batchOrderTote,
                selected: selStr,
                printDirect: printDirect
            }, printDirect,reportType, title);
        },
        getSelectedUser: function () {
            if (self) {
                return $('#currentUser').val();
            } else {
                var worker = $('#WorkerID').val();
                if (worker.trim() == '') {
                    return 'all';
                } else {
                    return worker;
                };
            };
        },
        showCtrls: function (isBatch) {
            if (isBatch) {
                $('#ClearBatch,#AssignToMe').show();
                $('#AssignBatch,#SelectNext').hide();
            } else {
                $('#ClearBatch,#AssignToMe').hide();
                $('#AssignBatch,#SelectNext').show();
            };
            // Assigned To column, show/hide
            SelectedWorkDT.column(5).visible(isBatch);
            UnselectedWorkDT.column(5).visible(isBatch);
            this.clearAll();
        }
    };
    // run the textbox update on the nearest sibling
    $('[name="PickPutCount"],[name="BatchOrderTote"]').click(function () {
        $(this).siblings('.radio-click').click();
    });

    // check the nearest radio button and determine what the new query parameters will need to be in order to draw/show/hide relevant elements
    $('.radio-click').click(function () {
        var $this = $(this);
        if ($this.data('work-title') != undefined) {
            $('[name="WorkType"]').html($this.attr('data-work-title'));
        };
        var radio = $this.siblings('[type="radio"]');
        if (radio.prop('checked') == 'checked') {
            radio.removeProp('checked');
        } else {
            radio.prop('checked', 'checked');
        };
        var qty = $('[name="PickPutCount"]:checked').data('next-x');
        switch ($this.attr('ID')) {
            case 'BatchLines':
                selected.showCtrls(true);
                break;
            case 'OrderLines':
            case 'ToteLines':
                selected.showCtrls(false);
                break;
            case 'PickLines':
                $('#SelectNext').html('Select Next ' + qty);
                $('#PrintTote').show();
                break;
            case 'PutLines':
                $('#SelectNext').html('Select Next ' + qty);
                $('#PrintTote').show();
                break;
            case 'CountLines':
                $('#PrintTote').hide();
                $('#SelectNext').html('Select Next ' + qty);
                break;
        };
        selected.clearAll();
    });

    // get the six top level count box values
    function getCounts() {
        var transtype = $('[name="PickPutCount"]:checked').attr('ID').toLowerCase().replace('s', '');
        if (transtype == "put") {
            transtype = "put away";
        };
        workHub.server.getWorkCounts(transtype).done(function (counts) {
            if (counts.length != 6) {
                MessageModal('Error', 'There was an error while attempting to get the counts of Picks, Put Aways, Counts, Batches, Orders and Totes.  Check the error log for details.');
            } else {
                $.each([$('#CountLines'), $('#PickLines'), $('#PutLines'), $('#BatchLines'), $('#OrderLines'), $('#ToteLines')], function (i, v) {
                    v.val(counts[i]);
                });
            };
        });
    };

    // clear the selected batches
    $('#ClearBatch').click(function () {
        if (confirm('Click OK to clear the selected batches.')) {
            workHub.server.clearBatches(selected.getSelectedString(false)).done(function (cleared) {
                if (!cleared) {
                    MessageModal('Error', 'There was an error while attempting to clear the selected batches.');
                } else {
                    selected.clearAll();
                };
            });
        };
    });

    // refresh the data and clear any items that were selected
    $('#Refresh').click(function () {
        if (confirm('Click OK to deselect the selected transaction lines and refresh the data.')) {
            selected.clearAll();
        };
    });

    // initialize the assign work modal for use with the current user only.
    $('#Assign').click(function () {
        $('#AssignWork_Modal').modal('show');
        if (self) { // if we did not have a referrer of /WM/OrganizeWork then we want to disallow edits to other users
            $('#AssignToWorker').val($('#currentUser').val()).data('username', $('#currentUser').val()).attr('disabled', 'disabled').parents('.has-warning').removeClass('has-warning').addClass('has-success')
                .find('.glyphicon-warning-sign').addClass('glyphicon-ok').removeClass('glyphicon-warning-sign');
        };
        $('#ClearWork,#ClearBatchIDs,#BatchAndUsername,#BatchOnly').removeAttr('disabled');
    });

    // add the next x (preference) entries to the selected list.
    $('#SelectNext').click(function () {
        var $this = $(this);
        var qty = $this.html().replace('Select Next', '').trim();
        if ($.isNumeric(qty)) {
            $.each($('#UnselectedTable tbody').children('tr'), function (i, v) {
                if (i >= qty) {
                    return false; // exit for loop
                } else {
                    selected.addItem($(v).children(':nth-child(2)').html());
                };
            });
        };
    });

    // assigns the selected (eligible) transactions to a batch
    $('#AssignBatch').click(function () {
        if (confirm('Click OK to assign the selected eligible transactions to a batch.')) {
            var isTote = toSqlBool($('#Totes').prop('checked') == 'checked' || $('#Totes').prop('checked') == true);
            var ttype = selected.getTransType();
            workHub.server.assignBatchID(selected.getSelectedString(false), ttype, isTote).done(function (assigned) {
                if (!assigned) {
                    MessageModal('Error', 'There was an error while attempting to assign the selected transactions to a batch.');
                } else {
                    // simulate a change to the selected object so that we can get the new data
                    selected.filterChanged();
                };
            });
        };
    });

    // assigns a batch to the current or selected user.
    $('#AssignToMe').click(function () {
        var user = selected.getSelectedUser();
        var batches = selected.selectedList;
        if (user != 'all' && batches.length > 0 && confirm('Click OK to assign the selected batches to ' + (self ? 'yourself.' : 'the specified worker.'))) {
            workHub.server.assignBatchToUser(user, batches).done(function (assigned) {
                if (!assigned) {
                    MessageModal('Error', 'An error occurred while attempting to assign the selected batches to the selected user.  Check the error log for details.');
                } else {
                    selected.clearAll();
                };
            });
        };
    });

    // execute a print command
    $(document.body).on('click', '#PrintWork, #PrintItem, #PrintTote', function () {
        var which = $(this).attr('id').toLowerCase().replace('print', '');
        selected.print(which);
    });

    $(document.body).on('click', '.detail', function (e) {
        e.stopPropagation();
        var $row = $(this).parents('tr');
        launchDetailModal($row.children(':first').html(), selected.getBatchOrderTote(), $row.children(':nth-child(2)').html(), selected.getSelectedUser());
    });

    /******************************************************
        WM Users Typeahead for selecting a user
    *******************************************************/
    // define the user typeahead for work manager
    // ensure that we don't try to attach a handler for a non-existant element
    if (!self) {
        var userTA = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            limit: 20,
            remote: {
                url: ('/WM/OrganizeWork/GetWorkerTA?worker='),
                replace: function (url, uriEncodedQuery) {
                    return url + $('#WorkerTA').val() + '&all=False';
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

        $('#WorkerTA').typeahead({
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
            $('#WorkerID').val(data.username);
            $('#WorkerTA').val(data.name).parents('.has-warning').removeClass('has-warning').addClass('has-success')
                .find('.glyphicon-warning-sign').removeClass('glyphicon-warning-sign').addClass('glyphicon-ok')
                .attr('data-original-title', 'A worker is selected.').tooltip('fixTitle');
            selected.clearAll();
        }).on('typeahead:opened', function () {
            var $this = $(this);
            $this.siblings('.tt-dropdown-menu').css('width', $this.css('width')).css('height', '300px').css('left', 'auto');
        });

        $('#WorkerTA').on('input', function () {
            $('#WorkerTA').parents('.has-success').removeClass('has-success').addClass('has-warning')
                .find('.glyphicon-ok').removeClass('glyphicon-ok').addClass('glyphicon-warning-sign')
                .attr('data-original-title', 'You must select a worker from the dropdown.').tooltip('fixTitle');
        });

        $('#WorkerID').val($('#currentUser').val());
        $('#WorkerTA').val('(self)').parents('.has-warning').removeClass('has-warning').addClass('has-success')
                .find('.glyphicon-warning-sign').removeClass('glyphicon-warning-sign').addClass('glyphicon-ok')
                .attr('data-original-title', 'A worker is selected.').tooltip('fixTitle');
    };

    /******************************************************************************************************************************************************
        Selected For Processing DataTable
    ******************************************************************************************************************************************************/

    $('#SelectedTable,#UnselectedTable').on('filterChange', function () {
        selected.filterChanged();
    });

    $(document.body).on('click', '#SelectedTable tr,#UnselectedTable tr', function () {
        var $this = $(this);
        if ($this.parents('table').attr('ID') == 'SelectedTable') {
            selected.removeItem($this.children(':nth-child(1)').html());
        } else {
            selected.addItem($this.children(':nth-child(2)').html());
        };
    });

    SelectedWorkFilter = new FilterMenuTable({
        Selector: '#SelectedTable tbody',
        columnIndexes: [
            'Column 2', 'Lines', 'Username'
        ],
        columnMap: function () {
            var colMap = [];
            colMap['Transaction Type'] = 'Text';
            colMap['Column 2'] = 'Text';
            colMap['Required Date'] = 'Date';
            colMap['Priority'] = 'Number';
            colMap['Lines'] = 'Number';
            colMap['Username'] = 'Text';
            return colMap;
        }(),
        dataTable: SelectedWorkDT,
        ignoreColumns: [
            1
        ]
    });

    SelectedWorkDT = $('#SelectedTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [],
        columnDefs: [
            {
                targets: [0, 2, 3],
                visible: false
            },
            {
                targets: [4, 6], // no order on [Lines] or [Detail] columns
                orderable:false
            }
        ],
        "ajax": {
            //Function that grabs Table Data
            "url": "/WM/SelectWork/GetSelectWorkTables",
            "data": function (d) {
                d.which = 'selected';
                d.entryFilter = SelectedWorkFilter.getFilterString();
                d.selected = selected.getSelectedString(true);
                d.transType = selected.getTransType();
                d.batchid = selected.getBatchOrderTote();
                d.users = selected.getSelectedUser();
            }
        },
        'paging': true,
        pageLength: 10,
        createdRow: function (row, data, index) {
            var td= $(row).children(':last');
            td.html('<button class="btn btn-primary btn-xs detail"><span class="glyphicon glyphicon-share"></span></button>');
            td.addClass('filter-ignore');
        }
    });

    /******************************************************************************************************************************************************
        Not Selected For Processing DataTable
    ******************************************************************************************************************************************************/
    UnselectedWorkFilter = new FilterMenuTable({
        Selector: '#UnselectedTable tbody',
        columnIndexes: [
            'Transaction Type', 'Column 2', 'Required Date', 'Priority', 'Lines', 'Username'
        ],
        columnMap: function () {
            var colMap = [];
            colMap['Transaction Type'] = 'Text';
            colMap['Column 2'] = 'Text';
            colMap['Required Date'] = 'Date';
            colMap['Priority'] = 'Number';
            colMap['Lines'] = 'Number';
            colMap['Username'] = 'Text';
            return colMap;
        }(),
        dataTable: UnselectedWorkDT,
        ignoreColumns: [
            4
        ]
    });

    UnselectedWorkDT = $('#UnselectedTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [],
        "ajax": {
            //Function that grabs Table Data
            "url": "/WM/SelectWork/GetSelectWorkTables",
            "data": function (d) {
                d.which = 'unselected';
                d.entryFilter = UnselectedWorkFilter.getFilterString();
                d.selected = selected.getSelectedString(true);
                d.transType = selected.getTransType();
                d.batchid = selected.getBatchOrderTote();
                d.users = selected.getSelectedUser();
            }
        },
        columnDefs: [
            {
                targets: [4, 6],// no order on [Lines] or [Detail] columns
                orderable:false
            }
        ],
        'paging': true,
        pageLength: 10,
        createdRow: function (row, data, index) {
            var td = $(row).children(':last');
            td.html('<button class="btn btn-primary btn-xs detail"><span class="glyphicon glyphicon-share"></span></button>');
            td.addClass('filter-ignore');
        }
    });
});