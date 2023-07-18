// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OrganizeWorkTable;
var OrganizeWorkFilterMen = "";
var OrganizeWorkCols = [];
var OrganizeWorkIgnoreCols = [];
var RefreshIntervalOW;
var orgWorkHub = $.connection.organizeWorkHub;
$(document).ready(function () {
    var TimerFocusVal;
    //starts the refresh timer on the databale
    RefreshIntervalOW = setInterval(function () {
        OrganizeWorkTable.draw();
        getTotalCount();
    }, parseInt($('#RefreshMinsOW').val()) * 60000);

    //initialize datatable
    OrganizeWorkTable = $('#OrganizeWorkTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'columnDefs': [
            {
                'targets': [0],
                'visible': false
            },
            {
                'targets': [4, 5, 6, 7, 8, 9],
                'orderable': false
            }
        ],
        "ajax": {
            "url": "/WM/OrganizeWork/getOrgainzeWorkTable",
            "data": function (d) {
                d.filter = (OrganizeWorkFilterMen == "" ? "" : OrganizeWorkFilterMen.getFilterString());
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
    //initialize filter for datatable
    OrganizeWorkCols = ["Range Name", "Start Location", "End Location", "Open Picks", "Open Put Aways", 
                          "Open Counts", "Need Workers Picks", "Need Workers Put Aways", "Need Workers Counts",
                          "Multi Worker Range", "Active"];
    OrganizeWorkIgnoreCols= [4, 5, 6, 7, 8, 9]
    OrganizeWorkFilterMen = new FilterMenuTable({
        Selector: '#OrganizeWorkTable',
        columnIndexes: OrganizeWorkCols,
        dataTable: OrganizeWorkTable,
        ignoreColumns: OrganizeWorkIgnoreCols,
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
    //on filter change re darwtable
    $('#OrganizeWorkTable').on('filterChange', function () {
        OrganizeWorkTable.draw();
    });
    //enable disbale edit button
    $('#OrganizeWorkTable tbody').on('click', 'tr', function () {
        $this = $(this)
        if (!$this.hasClass('active')) {
            $('#OrganizeWorkTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            $('#EditLocRangeOW').removeAttr('disabled');
        } else {
            $this.removeClass('active');
            $('#EditLocRangeOW').attr('disabled', 'disabled');
        };
    });
    //updating refesh button below. gets the focus value and checks if changed. if it is checks for empty and then updates if it is not empty
    $('#RefreshMinsOW').focus(function () {
        TimerFocusVal = $('#RefreshMinsOW').val();
    });
    $('#RefreshMinsOW').blur(function () {
        if ($('#RefreshMinsOW').val() != TimerFocusVal) {
            if ($('#RefreshMinsOW').val() == "") {
                MessageModal("Warning", "Need a refresh time value");
                $('#RefreshMinsOW').val(TimerFocusVal);
            } else {
                orgWorkHub.server.updateRefreshTimer($('#RefreshMinsOW').val()).done(function (mess) {
                    if (!mess) {
                        MessageModal("Error", "An error has occurred updating the refresh timer.");
                        $('#RefreshMinsOW').val(TimerFocusVal);
                    } else {
                        OrganizeWorkTable.draw();
                        getTotalCount();
                        RefreshIntervalOW = setInterval(function () {
                            OrganizeWorkTable.draw();
                            getTotalCount();
                        }, parseInt($('#RefreshMinsOW').val()) * 60000);
                    }
                });
            };
        };

    });

    //redraws the table
    $('#RefreshLocRanges').click(function () {
        OrganizeWorkTable.draw();
        getTotalCount();
    });

    //opens the assign work modal
    $('#AssignWork').click(function () {
        $('#AssignWork_Modal').modal('show');
    });
    //on close of the assign work modal redraw the table
    $('#tote_dismiss').click(function () {
        OrganizeWorkTable.draw();
        getTotalCount();
    });

});


function getTotalCount() {
    orgWorkHub.server.refreshTotalCount().done(function (opencount) {
        $('#ToteOpenLinesOW').val(opencount);
    });
};