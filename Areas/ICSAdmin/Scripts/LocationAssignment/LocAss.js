// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    //Prevent ESC key
    $('#LocAssCountModal').modal({ keyboard: false, show: false });
    //shows the modal in order to select the desired transaction type
    $('#LocAssCountModal').modal('show');

    //grabs count table data
    $('#LocAssCount').click(function () {
        getCountTableData();
    });

    //grabs pick table data
    $('#LocAssPick').click(function () {
        getPickTableData();
    });

    //grabs put away table data
    $('#LocAssPutAway').click(function () {
        getPutAwayTableData();
    });

    //clear the right count table
    $('#CountClearRightLocAssDT').click(function () {
        leftCountTable.rows.add(rightCountTable.data()).draw();
        rightCountTable.clear().draw();
    });

    //clear the right pick table
    $('#PickClearRightLocAssDT').click(function () {
        leftPickTable.rows.add(rightPickTable.data()).draw();
        rightPickTable.clear().draw();
    });

    //clear the right put away table
    $('#PutAwayClearRightLocAssDT').click(function () {
        leftPutAwayTable.rows.add(rightPutAwayTable.data()).draw();
        rightPutAwayTable.clear().draw();
    });

    $('#LLPreview_submit').click(function () {
        $('#LocAssCountModal').modal('show');
    });

    $('#ViewPrintModal').click(function () {
        $('#LocAssCountModal').modal('show');
    });

    function getTableData(table) {
        var data = [];
        switch (table.toLowerCase().replace(' ', '')) {
            case 'count':
                data = rightCountTable.column(0).data();
                break;
            case 'putaway':
                data = rightPutAwayTable.column(0).data();
                break;
            case 'pick':
                data = rightPickTable.column(0).data();
                break;
        };
        // need this because for some reason the datatable.column(x).data() call returns an "array" with additional properties.  The additional properties caused an overflow on the stack
        // this fixes it for some reason
        var returndata = [];
        for (var x = 0; x < data.length; x++) {
            returndata.push(data[x]);
        }
        return returndata;
    };
    $('#CountLocationAssignment, #PickLocationAssignment, #PutAwayLocationAssignment').click(function () {
        var id = this.id.toLowerCase().replace('locationassignment', '');
        var Orders = getTableData(id);

        if (Orders.length == 0) {
            MessageModal('No Orders Selected', 'There were no orders selected for location assignment marking', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
        } else {
            //Function location in locasshub.js
            InsertOrdersForProcess(id, Orders);
        };

    });
});