// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var empStatsTable;
var employeeStats = $.connection.employeeStatsHub;

// builds the string form of the selected employees for use in the table data selection process
var getEmployeeString = function () {
    var usersString = "";
    var count = 1;
    var itemCount = $(".ui-selected").length;
    $(".ui-selected").each(function () {
        var index = $(this).data("val");
        if (count == 1 && count != itemCount) {
            usersString += index + "', "
        }
        else if (count == itemCount && count != 1) {
            usersString += "'" + index
        }
        else if (count == itemCount) {
            usersString += index
        }
        else {
            usersString += "'" + index + "', "
        }
        count++;
    });
    return usersString;
}



function SelectSelectableElement(selectableContainer, elementsToSelect) {
    // add unselecting class to all elements in the styleboard canvas except the ones to select
    $(".ui-selected", selectableContainer).not(elementsToSelect).removeClass("ui-selected").addClass("ui-unselecting");

    // add ui-selecting class to the elements to select
    $(elementsToSelect).not(".ui-selected").addClass("ui-selecting");

    // trigger the mouse stop event (this will select all .ui-selecting elements, and deselect all .ui-unselecting elements)
    selectableContainer.data("ui-selectable")._mouseStop(null);
}

function ClearElements(selectableContainer) {
    $(".ui-selected", selectableContainer).removeClass("ui-selected").addClass("ui-unselecting");
    selectableContainer.data("ui-selectable")._mouseStop(null);
}

$(document).ready(function () {
    //Initializes Employee Selection
    $("#selectable").selectable({
        stop: function (event, ui) {
            empStatsTable.draw();
        }
    });
    // initialize employee stats table
    empStatsTable = $('#empStatsTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "ordering": false,
        "columnDefs": [
            { 'width': "100px", 'visible': false, "targets": 0 }, { "targets": 1 }
        ],
        "ajax": {
            "url": "/Employees/employeeStatInfo",
            "data": function (d) {

                d.users = getEmployeeString();  //insert list of users here
                var sDate = $('#sDateFilterEmploy').val();
                if (new Date(sDate) < new Date('1/1/1900') || new Date(sDate) > new Date('1/1/3000'))
                    sDate = getCurrentDate();
                var eDate = $('#eDateFilterEmploy').val();
                if (new Date(eDate) < new Date('1/1/1900') || new Date(eDate) > new Date('1/1/3000'))
                    eDate = getCurrentDate();
                d.sDate = sDate;
                d.eDate = eDate;
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });

    // handles overflow of the table by wrapping it
    $("#empStatsTable").wrap('<div style="overflow-x:scroll"></div>');
    // handles paging length changes
    $('#pageLength3').change(function () {
        empStatsTable.page.len($(this).val());
        empStatsTable.draw();
    });

    // selects all employees
    $('#selEmployees').click(function () {
        SelectSelectableElement($('#selectable'), $('#selectable li'));
        empStatsTable.draw();
    });

    // clears the selected employees
    $('#clearSelEmployees').click(function () {
        ClearElements($('#selectable'));
        empStatsTable.draw();
    });

    // on switch to the statistics tab get the employee data
    $('a').on('shown.bs.tab', function (e) {
        if ($(e.target).attr("href") == "#statistics") {
            employeeStats.server.getEmployees().done(function (data) {
                $('#selectable').html("");
                for (i = 0; i < data.length; i++) {
                    $('#selectable').append("<li  class=" + '"text-center ui-widget-content"' + " data-val='" + data[i][0] + "'>" + data[i][1] + "</li>")
                }
                $("#selectable").selectable("destroy");
                $("#selectable").selectable({
                    stop: function (event, ui) {
                        empStatsTable.draw();
                    }
                });
            });
        }
    });
    // print by user
    $(document.body).on('click', '#printStatsUser', function () {
        getLLPreviewOrPrint('/Admin/Employees/printEmployeeStatsReport', {
            startDate: $('#sDateFilterEmploy').val(),
            endDate: $('#eDateFilterEmploy').val(),
            checkVal: 'user',
            userList: getEmployeeString()
        }, true,'report', 'Employee Stats Report');
    });
    // print by date
    $(document.body).on('click', '#printStatsDate', function () {
        getLLPreviewOrPrint('/Admin/Employees/printEmployeeStatsReport', {
            startDate: $('#sDateFilterEmploy').val(),
            endDate: $('#eDateFilterEmploy').val(),
            checkVal: 'date',
            userList: getEmployeeString()
        }, true,'report', 'Employee Stats Report');
    });

    // redraws the employee stats table when the date filters change
    $('#sDateFilterEmploy,#eDateFilterEmploy').on('input', function () {
        empStatsTable.draw();
    });
    $('#sDateFilterEmploy,#eDateFilterEmploy').on('change', function () {
        empStatsTable.draw();
    });

    $('#setDate').click(function () {
        $('#sDateFilterEmploy').data("DateTimePicker").setDate(setToToday());
        $('#eDateFilterEmploy').data("DateTimePicker").setDate(setToToday());
        empStatsTable.draw();
    });
});


   