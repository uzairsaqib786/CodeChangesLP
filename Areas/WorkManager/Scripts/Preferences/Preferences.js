// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var prefsHub;
var LocationRangesTable;
var LocationRangesFilterMen = "";
var LocationRangesCols = [];
var WorkerRangesTable;
var WorkerRangesFilterMen = "";
var WorkerRangesCols = [];
var UsedWorkerRangeEditButt = 0;

/**********************************************************************************************************************
        Begin Common Code
**********************************************************************************************************************/
$('.toggles').toggles({
    width: 60,
    height: 25,
    text: {
        on: 'Yes',
        off: 'No'
    }
});
$(document).ready(function () {
    prefsHub = $.connection.wMPreferencesHub;
    var printDirect = $('#PrintDirect').data('toggles').active;
    /**********************************************************************************************************************
        End Common Code
    **********************************************************************************************************************/
    /**********************************************************************************************************************
        Begin General Settings code
    **********************************************************************************************************************/
    $('#PrintDirect').data('toggles').setValue($('#PrintDirect').data('start-value'));

    var generalTimer = mkTimer(function () {
        var pd = toSqlBool($('#PrintDirect').data('toggles').active);
        prefsHub.server.saveGeneralSettings($('#Pick').val(), $('#Put').val(), $('#Count').val(), $('#CustomApp').val(), $('#CustomAppDescription').val(), pd).done(function (saved) {
            if (!saved) {
                MessageModal('Error', 'There was an error while saving the general settings section.  Please remake any changes to the settings to retry.');
            };
        });
    });

    $('#PrintDirect').on('toggle', function () {
        generalTimer.startTimer();
    });

    $('#General input, #General textarea').on('input', function () {
        generalTimer.startTimer();
    });

    $('#OpenApp').click(function () {
        location.href = $('#CustomApp').val();
    });

    $('#Pick,#Put,#Count').on('input', function () {
        setNumericInRange($(this), 1, SqlLimits.numerics.int.max);
    });
    /**********************************************************************************************************************
        End General Settings code
    **********************************************************************************************************************/

    /**********************************************************************************************************************
       Begin Batch Settings code
    **********************************************************************************************************************/
    //start of pick
    //builds the result string for the result display. also checks to make sure no duplicates are selected
    $('#PickSort1, #PickSort2, #PickSort3, #PickSort4').change(function () {
        var $this = $(this);
        var id = $this.attr("id");
        var checkID = "";
        var sortString = "";
        if ($this.val() != "") {
            for (var i = 1; i < 5; i++) {
                checkID = "PickSort" + i.toString()
                if (id != checkID) {
                    if ($('#' + checkID).val() != "") {
                        if ($this.val() == $('#' + checkID).val()) {
                            MessageModal("Warning", "There a are duplicate columns selected. ");
                            $this.val("");
                            $('#PickSeqTempResult').val("");
                            return;
                        };
                    };
                };
            };
        };
        for (var i = 1; i < 5; i++) {
            checkID = "#PickSort" + i.toString()
            if (i == 1) {
                if ($(checkID).val() != "") {
                    sortString = $(checkID).val() + ", ";
                };
            } else if (i == 4) {
                if ($(checkID).val() != "") {
                    sortString = sortString + $(checkID).val();
                };
            } else {
                if ($(checkID).val() != "") {
                    sortString = sortString + $(checkID).val() + ", ";
                };
            };
        };
        $('#PickSeqTempResult').val(sortString);
    });
    //sets the result string as the new default
    $('#SavePickSortResult').click(function () {
        var sortString = $('#PickSeqTempResult').val();
        var lastChar = sortString.substr(-2);
        if (lastChar == ", ") {
            sortString = sortString.substring(0, sortString.length - 2);
        };
        prefsHub.server.saveDefaultSorts(sortString, "", "", 1).done(function (mess) {
            if (!mess) {
                MessageModal("Error", "An error has occurred saving this default pick sort");
            } else {
                $('#PickSort1, #PickSort2, #PickSort3, #PickSort4, #PickSeqTempResult').val("");
                $('#DefaultPickSort').val(sortString);
            };
        });
    });
    //end of pick

    //start of put
    //builds the result string for the result display. also checks to make sure no duplicates are selected
    $('#PutSort1, #PutSort2, #PutSort3, #PutSort4').change(function () {
        var $this = $(this);
        var id = $this.attr("id");
        var checkID = "";
        var sortString = "";
        if ($this.val() != "") {
            for (var i = 1; i < 5; i++) {
                checkID = "PutSort" + i.toString()
                if (id != checkID) {
                    if ($('#' + checkID).val() != "") {
                        if ($this.val() == $('#' + checkID).val()) {
                            MessageModal("Warning", "There a are duplicate columns selected. ");
                            $this.val("");
                            $('#PutSeqTempResult').val("");
                            return;
                        };
                    };
                };
            };
        };
        for (var i = 1; i < 5; i++) {
            checkID = "#PutSort" + i.toString()
            if (i == 1) {
                if ($(checkID).val() != "") {
                    sortString = $(checkID).val() + ", ";
                };
            } else if (i == 4) {
                if ($(checkID).val() != "") {
                    sortString = sortString + $(checkID).val();
                };
            } else {
                if ($(checkID).val() != "") {
                    sortString = sortString + $(checkID).val() + ", ";
                };
            };
        };
        $('#PutSeqTempResult').val(sortString);
    });
    //sets the result string as the new default
    $('#SavePutSortResult').click(function () {
        var sortString = $('#PutSeqTempResult').val();
        var lastChar = sortString.substr(-2);
        if (lastChar == ", ") {
            sortString = sortString.substring(0, sortString.length - 2);
        };
        prefsHub.server.saveDefaultSorts("", sortString, "", 2).done(function (mess) {
            if (!mess) {
                MessageModal("Error", "An error has occurred saving this default put sort");
            } else {
                $('#PutSort1, #PutSort2, #PutSort3, #PutSort4, #PutSeqTempResult').val("");
                $('#DefaultPutSort').val(sortString);
            };
        });
    });
    //end of put

    //start of count
    //builds the result string for the result display. also checks to make sure no duplicates are selected
    $('#CountSort1, #CountSort2, #CountSort3, #CountSort4').change(function () {
        var $this = $(this);
        var id = $this.attr("id");
        var checkID = "";
        var sortString = "";
        if ($this.val() != "") {
            for (var i = 1; i < 5; i++) {
                checkID = "CountSort" + i.toString()
                if (id != checkID) {
                    if ($('#' + checkID).val() != "") {
                        if ($this.val() == $('#' + checkID).val()) {
                            MessageModal("Warning", "There a are duplicate columns selected. ");
                            $this.val("");
                            $('#CountSeqTempResult').val("");
                            return;
                        };
                    };
                };
            };
        };
        for (var i = 1; i < 5; i++) {
            checkID = "#CountSort" + i.toString()
            if (i == 1) {
                if ($(checkID).val() != "") {
                    sortString = $(checkID).val() + ", ";
                };
            } else if (i == 4) {
                if ($(checkID).val() != "") {
                    sortString = sortString + $(checkID).val();
                };
            } else {
                if ($(checkID).val() != "") {
                    sortString = sortString + $(checkID).val() + ", ";
                };
            };
        };
        $('#CountSeqTempResult').val(sortString);
    });
    //sets the result string as the new default
    $('#SaveCountSortResult').click(function () {
        var sortString = $('#CountSeqTempResult').val();
        var lastChar = sortString.substr(-2);
        if (lastChar == ", ") {
            sortString = sortString.substring(0, sortString.length - 2);
        };
        prefsHub.server.saveDefaultSorts("", "", sortString, 3).done(function (mess) {
            if (!mess) {
                MessageModal("Error", "An error has occurred saving this default count sort");
            } else {
                $('#CountSort1, #CountSort2, #CountSort3, #CountSort4, #CountSeqTempResult').val("");
                $('#DefaultCountSort').val(sortString);
            };
        });
    });
    //end of count


    /**********************************************************************************************************************
        End Batch Settings code
    **********************************************************************************************************************/

    /**********************************************************************************************************************
        Begin WM Location Ranges code
    **********************************************************************************************************************/
    //initialize the datatable
    LocationRangesTable = $('#LocationRangesTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'columnDefs': [
            {
                'targets': [0],
                'visible': false
            }
        ],
        "ajax": {
            "url": "/WM/Preferences/getWMLocationRangesTable",
            "data": function (d) {
                d.filter = (LocationRangesFilterMen == "" ? "" : LocationRangesFilterMen.getFilterString());
            }

        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "paging": true
    });
    //initialize the filter on the table
    LocationRangesCols = ["Range Name", "Start Location", "End Location", "Multi Worker Range", "Active"];
    LocationRangesFilterMen = new FilterMenuTable({
        Selector: '#LocationRangesTable',
        columnIndexes: LocationRangesCols,
        dataTable: LocationRangesTable,
        columnMap: function () {
            var colMap = [];
            colMap["Range Name"] = "Text"
            colMap["Start Location"] = "Text"
            colMap["End Location"] = "Text"
            colMap["Multi Worker Range"] = "Bool"
            colMap["Active"] = "Bool"
            return colMap;
        }()
    });
    //on filter change redraw the table
    $('#LocationRangesTable').on('filterChange', function () {
        LocationRangesTable.draw();
    });

    //enable and disable butttons when tbale rows are clicked
    $('#LocationRangesTable tbody').on('click', 'tr', function () {
        $this = $(this)
        if (!$this.hasClass('active')) {
            $('#LocationRangesTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            $('#EditLocationRange').removeAttr('disabled');
        } else {
            $this.removeClass('active');
            $('#EditLocationRange').attr('disabled', 'disabled');
        };
    });

    //refreshes the location ranges, and then redraws the tbale
    $('#RefreshLocationRanges').click(function () {
        var conf = confirm("Are you sure you want to refresh these location ranges? Some location ranges may be deleted")
        if (conf && confirm("WARNING DOING THIS WILL DELETE ALL USER RANGES AND ASSIGNED WORK. Continue?")) {
            prefsHub.server.refreshLocationRanges().done(function (mess) {
                if (!mess) {
                    MessageModal("Error", "An error has occurred refreshing the location range data")
                } else {
                    LocationRangesTable.draw();
                    $('#EditLocationRange').attr('disabled', 'disabled');
                };
            });
        };
    });


    /**********************************************************************************************************************
        End WM Location Ranges code
    **********************************************************************************************************************/


    /**********************************************************************************************************************
        Begin Worker Ranges code
    **********************************************************************************************************************/
    //datatabale initialization
    WorkerRangesTable = $('#WorkerRangesTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'columnDefs': [
            {
                'targets': [0],
                'visible': false
            }
        ],
        "ajax": {
            "url": "/WM/Preferences/getWMWorkerRangesTable",
            "data": function (d) {
                d.filter = (WorkerRangesFilterMen == "" ? "" : WorkerRangesFilterMen.getFilterString());
            }

        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "paging": true
    });
    //initialize filter
    WorkerRangesCols = ["Username", "First Name", "Last Name", "Range Name", "Start Location", "End Location", "Active", "Date Stamp"];
    WorkerRangesFilterMen = new FilterMenuTable({
        Selector: '#WorkerRangesTable',
        columnIndexes: WorkerRangesCols,
        dataTable: WorkerRangesTable,
        columnMap: function () {
            var colMap = [];
            colMap["Username"] = "Text"
            colMap["First Name"] = "TExt"
            colMap["Last Name"] = "Text"
            colMap["Range Name"] = "Text"
            colMap["Start Location"] = "Text"
            colMap["End Location"] = "Text"
            colMap["Active"] = "Bool"
            colMap["Date Stamp"] = "Date"
            return colMap;
        }()
    });
    //on filter change redraw the table 
    $('#WorkerRangesTable').on('filterChange', function () {
        WorkerRangesTable.draw();
    });
    //opens the edit modal and sets the values to the values of the click row
    $('#EditWorkerRange').click(function () {
        //variable used to make sure the name data does not change when the typeahead tiggers are called
        UsedWorkerRangeEditButt = 2;

        $('#EditWorkRangeLastName').val(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[3]);
        $('#EditWorkRangeLastName').trigger('input');
        $('#EditWorkRangeLastName').trigger('typeahead:selected');


        $('#EditWorkRangeFirstName').val(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[2]);
        $('#EditWorkRangeUsername').val(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[1]);
        $('#EditWorkRangeActive').val(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[7]);

        $('#EditWorkRangeRangeName').val(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[4]);
        $('#EditWorkRangeRangeName').trigger('input');
        $('#EditWorkRangeRangeName').trigger('typeahead:selected');


        $('#EditWorkRangeStartLocation').val(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[5]);
        $('#EditWorkRangeEndLocation').val(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[6]);
        //timeout to close the typeahead selection
        setTimeout(function () {
            $('#EditWorkRangeRangeName').typeahead('close');
            $('#EditWorkRangeLastName').typeahead('close');
        }, 500);

        $('#WorkerRangeEditModal').modal('show');
    });
    //enables and disables buttons 
    $('#WorkerRangesTable tbody').on('click', 'tr', function () {
        $this = $(this)
        if (!$this.hasClass('active')) {
            $('#WorkerRangesTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            $('#EditWorkerRange').removeAttr('disabled');
            $('#DeleteWorkerRange').removeAttr('disabled');
        } else {
            $this.removeClass('active');
            $('#EditWorkerRange').attr('disabled', 'disabled');
            $('#DeleteWorkerRange').attr('disabled', 'disabled');
        };
    });
    //initialize typeahead
    var createLastNameAddNewTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/WM/Preferences/getWMUsersTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#SelNewWorkerRange').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });

    createLastNameAddNewTA.initialize();
    $('#SelNewWorkerRange').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "createLastNameAddNewTA",
        displayKey: 'LastName',
        source: createLastNameAddNewTA.ttAdapter(),
        templates: {
            header: '<p style="width:34%;" class="typeahead-header">Last Name</p><p style="width:33%;" class="typeahead-header">First Name</p><p style="width:33%;" class="typeahead-header">Username</p>',
            suggestion: Handlebars.compile("<p style='width:34%;' class='typeahead-row'>{{LastName}}</p><p style='width:33%;' class='typeahead-row'>{{FirstName}}</p><p style='width:33%;' class='typeahead-row'>{{Username}}</p>")
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        //on selected inserts the a new record for the selected worker
        var conf = confirm("Add a range for this worker?");
        if (conf) {
            prefsHub.server.insertWMUserRange($('#SelNewWorkerRange').val()).done(function (mess) {
                if (!mess) {
                    MessageModal("Error", "An error has ocurred inserting thr worker")
                } else {
                    $('#SelNewWorkerRange').val("");
                    WorkerRangesTable.draw();
                };
            });
        } else {
            $('#SelNewWorkerRange').val("");
        };
        
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $(this).siblings('.tt-dropdown-menu').css('width', $this.css('width'));
    });
    //Deletes the selcted worker range
    $('#DeleteWorkerRange').click(function () {
        var conf = confirm("Are you sure you want to delete this worker range?");

        if (conf) {
            prefsHub.server.deleteWorkerRange(WorkerRangesTable.row($('#WorkerRangesTable tbody tr.active')[0]).data()[0]).done(function (mess) {
                if (!mess) {
                    MessageModal("Error", "An error has occurred deleteing this worker range")
                } else {
                    $('#DeleteWorkerRange').attr('disabled', 'disabled');
                    WorkerRangesTable.draw();
                };
            });
        };
    });
    //prints all worker ranges
    $(document.body).on('click', '#PrintWorkerRanges', function () {
        getLLPreviewOrPrint('/WM/Preferences/printWMUserRanges', {printDirect:printDirect}, printDirect,'report', 'WM User Ranges');
    });

    /**********************************************************************************************************************
        End Worker Ranges code
    **********************************************************************************************************************/
});