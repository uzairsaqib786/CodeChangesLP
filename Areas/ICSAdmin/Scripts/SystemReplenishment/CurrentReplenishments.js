// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var sysRepHub = $.connection.systemReplenishmentHub;
var currReplenTable;

//Custom Sorting Variables/Functions
var sortcolumnName = "Transaction Type"
var direction = "desc"
var tableRef;
var initSorting;
var toggleDirection = function () {
    if (direction == "asc") {
        direction = 'desc';
    }
    else {
        direction = "asc";
    }
}
initSorting = function (newTableRef,tableID) {
    tableRef = newTableRef;
    activeTh = $('#'+tableID+' .sorting_desc');
    $(document.body).on('click', '#'+tableID+' th', function (e) {
        if (this === activeTh.get(0)) {
            activeTh.removeClass('sorting_' + direction);
            toggleDirection();
            activeTh.addClass('sorting_' + direction);
            tableRef.draw();
        }
        else {
            activeTh.removeClass('sorting_' + direction);
            activeTh.addClass('sorting');
            activeTh = $(this);
            activeTh.removeClass('sorting');
            activeTh.addClass('sorting_desc');
            direction = "desc";
            sortcolumnName = activeTh.text();
            tableRef.draw();
        }
    })
}
var SystReplenCurrCols = new Array();
$(document).ready(function () {
    $.each($('#curOrdersColumns').children(), function (index, element) {
        if (index != 0) {
            SystReplenCurrCols.push($(element).attr('value'));
        };
    });

    $('a[href="#currentOrder"]').on('show.bs.tab', function () {
        redrawTableCurrent(true);
    });
    var typingtime, newTime, updateInterval;
    currReplenTable = $('#currentOrders').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "ordering": false,
        "columnDefs": [
            { 'width': "100px"}, { "targets": 1 }
        ],
        "ajax": {
            "url": "/SystemReplenishment/systRepTable",
            "data": function (d) {
                d.searchString = $('#curOrderSearch').val();
                d.searchColumn = $('#curOrdersColumns').val();
                d.status = $('#status').val();
                d.sortColumn = sortcolumnName;
                d.sortDirection = direction
                //add filter here
                d.filter = (SystReplenCurrFilterMen == "" ? "" : SystReplenCurrFilterMen.getFilterString());
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    }).on('xhr', function (e, settings, jsonObj) {
        $('#pickCount').val(jsonObj.extraData.pickcount);
        $('#putCount').val(jsonObj.extraData.putcount);
    });
    initSorting(currReplenTable, 'currentOrders');
    $('#currentOrders').wrap('<div style="overflow-x:scroll"></div>');

    $('#curOrderslength').change(function () {
        currReplenTable.page.len($(this).val());
        redrawTableCurrent(true);
    });
    $('#status').change(function () {
        redrawTableCurrent(true);
    })

    $("currenOrderTab").click(function () {
        sysRepHub.server.grabCountInfos().done(function (counts) {
            $("pickCount").val(counts.pickcount);
            $("putCount").val(counts.putcount);
        });
        redrawTableCurrent(true);
    });

    $('#currentOrders').on('click', 'tr', function () {
        var active = $('tr.active'), $this = $(this);
        if (active.length > 0 && !$this.hasClass('active')) {
            active.removeClass('active');
            $this.addClass('active');
        } else if ($this.hasClass('active')) {
            $this.removeClass('active');
        } else {
            $this.addClass('active');
        };
    });

    $(document.body).on('click', '#printReport', function () {
        //reportsHub.server.printReplenishmentReportLabels($('#curOrderSearch').val(), $('#curOrdersColumns').val(), $('#status').val(), "Orders");
        title = 'Replenishment Orders Report';
        getLLPreviewOrPrint('/Admin/SystemReplenishment/printReplenishmentReportLabels', {
            searchString: $('#curOrderSearch').val(), 
            searchCol: $('#curOrdersColumns').val(), 
            status: $('#status').val(), 
            ident: 'Orders',
            filter: (SystReplenCurrFilterMen == "" ? "" : SystReplenCurrFilterMen.getFilterString())
        }, true,'report', title)
    });

    $(document.body).on('click', '#printLabel', function () {
        //reportsHub.server.printReplenishmentReportLabels($('#curOrderSearch').val(), $('#curOrdersColumns').val(), $('#status').val(), "Labels");

        //title = 'RReplenishment Labels';
        //getLLPreviewOrPrint('/Admin/SystemReplenishment/printReplenishmentReportLabels', {
        //    searchString: $('#curOrderSearch').val(),
        //    searchCol: $('#curOrdersColumns').val(),
        //    status: $('#status').val(),
        //    ident: 'Labels'
        //}, true,'label', title)

        $('#ReplenLabelPrintModal').modal('show');
    });

    $('#curOrdersColumns').change(function () {
        redrawTableCurrent(true);
    });

    $('#curOrderSearch').on("input", function () {
        if ($('#curOrdersColumns').val().trim() != '') {
            redrawTableCurrent(true);
        };
    });

    $('#ViewAllCurrent').click(function () {
        SystReplenCurrFilterMen.clearFilter();
    });

    $('#ViewNotPrintedCurrent').click(function () {
        //clear filter string first. avoid clear string function because that triggers redraw
        SystReplenCurrFilterMen.filterString = "";
        SystReplenCurrFilterMen.addFilter("Print Date", "Date", "=", "Equals", '', undefined);
        redrawTableCurrent(true);
    });

    $('#currentOrders').on('dblclick', 'td', function () {
        var $this = $(this);
        var item = $this.text();

        if ($this.index() == 0 && item != "No data available in table") {
            InvMastPopUp(item); //Function located in New Replen js file
        };
    });

    function redrawTableCurrent(reset) {
        newTime = new Date().getTime();
        if (newTime - typingtime > 200) {
            currReplenTable.draw(reset);
        }
        else {
            clearTimeout(updateInterval);
            updateInterval = setTimeout(function () { currReplenTable.draw(reset); }, 200);
        };
        typingtime = newTime;
    };

    var currentTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in TransactionsController nextOrders
            url: '/SystemReplenishment/currReplenSearchDropDown?searchString=',
            replace: function (url, uriEncodedQuery) {
                return url + uriEncodedQuery + '&searchCol=' + $('#curOrdersColumns').val();
            },
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            cache: false
        }
    });
    currentTypeahead.initialize();
    $('#curOrderSearch').typeahead({
        minlength: 0,
        hint: false,
        highlight: false
    }, {
        name: "currentTypeahead",
        displayKey: 'value',
        source: currentTypeahead.ttAdapter()
    }).on('typeahead:selected', function () {
        redrawTableCurrent(true);
    }).parent().css({'display': 'inline'});

    $("#deleteAll").click(function () {
        var confirmDelete = confirm("Are you sure you want to delete all records");
        if (confirmDelete) {
            sysRepHub.server.deleteReplenishmentsBy("All", "", "", "", "", "").done(function (success) {
                if (!success) {
                    console.log("Deleting all records has failed")
                } else {
                    redrawTableCurrent(true);
                };
            });
        };
    });

    $("#deleteShown").click(function () {
        var confirmDelete = confirm("Are you sure you want to delete all records that are currently dipslayed");
        if (confirmDelete) {
            sysRepHub.server.deleteReplenishmentsBy("Shown", "", "", $('#curOrderSearch').val(), $('#curOrdersColumns').val(), $('#status').val()).done(function (success) {
                if (!success) {
                    console.log("Deleting all displayed records has failed")
                } else {
                    redrawTableCurrent(true);
                };
            });
        };
    });

    $("#deleteSelectedOrder").click(function () {
        var Order = $('#currentOrders tr.active td:eq(12)').html();

        if (Order === undefined) {
            MessageModal('No Row Selected', 'No row was selected in the table. Please select a transaction of the order you want to delete.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else {
            MessageModal('Delete Order: ' + Order, 'Delete ALL transactions for order: ' + Order + '. This will delete all trasnactions, not just the selected one', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; }, 
                    function () {
                        sysRepHub.server.deleteReplenishmentsBy("Shown", "", "", Order, "Order Number", "All").done(function (success) {
                                if (!success) {
                                    alert("Deleting order has failed");
                                } else {
                                    redrawTableCurrent(true);
                                };
                            });
                    }
                );
        };
    });

    
});



