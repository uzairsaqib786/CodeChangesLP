// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var MenuTable = $('#allocatedTransTable').DataTable({
    "dom": 'tri',
    "paging": false
})

// populates the table fields
/*var populateTable = function (data) {
    // data is a list of list of string for <tr>s, <td>s
    var dataString = "";
    for (var i = 0; i < data.length; i++) {
        dataString += "<tr>";
        for (var k = 0; k < data[i].length; k++) {
            dataString += "<td class='text-center'>" + data[i][k] + "</td>";
        };
        dataString += "</tr>";
    };
    // set the data into the table
    $('#allocatedTransTBody').html(dataString);
};*/

var populateTable = function (data) {
    MenuTable.clear()
    for (var x = 0; x < data.length; x++) {
        MenuTable.row.add(data[x])
    }
    MenuTable.draw()
}

// populates the count fields below the table
var populateCounts = function(data) {
    $('#openPicks').text(data[0]);
    $('#compPicks').text(data[1]);
    $('#openPuts').text(data[2]);
    $('#compPuts').text(data[3]);
    $('#openCounts').text(data[4]);
    $('#compCounts').text(data[5]);
    $('#adjust').text(data[6]);
    $('#reproc').text(data[7]);
};

$(document).ready(function () {
    var menuHub = $.connection.menuHub;
    // refreshes the table and count data every 30 seconds
    setInterval(function () {
        menuHub.server.updateInfo().done(function (data) {
            populateTable(data.table);
            populateCounts(data.counts);
        });
    }, 10000);

    // handles toggling diagnostic mode
    $('#toggleDiagnostic').click(function () {
        $('#toggleDiagnostic').attr('disabled', 'disabled');
        // if diag mode is on
        if ($('#diagMode').hasClass('label-success')) {
            menuHub.server.toggleDiagMode(false).done(function () {
                $('#diagMode').removeClass('label-success');
                $('#diagMode').addClass('label-danger');
                $('#diagMode').text('Off');
                $('#toggleDiagnostic').removeAttr('disabled');
            });
        } else {
            menuHub.server.toggleDiagMode(true).done(function () {
                $('#diagMode').removeClass('label-danger');
                $('#diagMode').addClass('label-success');
                $('#diagMode').text('On');
                $('#toggleDiagnostic').removeAttr('disabled');
            });
        };
    });
});