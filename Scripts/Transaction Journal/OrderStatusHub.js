// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/***********************************************************************
    Order Status Hub
***********************************************************************/
// Reference the auto-generated proxy for the hub for Order Status 
var orderStats = $.connection.orderStatusHub;
// variable declarations
var totaAct = false;

// currently unused, will be used later on
orderStats.client.dataRecieved = function (data) {
    // the broadcaster event indicated the user needs to get new data for the order number specified by the data variable, 
    // so redraw if the order number that changed is the one the user is looking at.
    if ($('#ordernumFilterOrder').val() == data) {
        orderStatTable.draw(false);
    };
};

// display the data
orderStats.client.dispOrderStat = function (data) {
    /* This function will initially set the page values to the intial empty values
    and then when given an OrderstatInfo object will return all the information within it to 
    the webpage 
    */
    //Set initial values 
    if (data.toteID == $('#toteidFilterOrder').val()) {
        var carOnTable = $('#carOnTable');
        carOnTable.html('');
        var carOffTable = $('#carOffTable');
        carOffTable.html('');
        //Add data to the page 
        $('#ordType').text(data.type);
        $('#totLines').text(data.totalLines);
        $('#curStat').text(data.status);
        $('#complLines').text(data.compLines);
        $('#openLines').text(data.opLines);
        $('#reprocLines').text(data.reLines);

        var orderFilter = $('#ordernumFilterOrder');
        //changes the order number value in the page as the tote id changes for autofill
        if (data.orderNumber != "" && data.orderNumber != orderFilter.val() && $('#toteidFilterOrder').val() != "") {
            orderFilter.val(data.orderNumber);
            orderStatTable.draw();
            var e = jQuery.Event("keydown");
            e.which = 13; //enter keycode
            e.keyCode = 13;
            orderFilter.trigger(e);
        } else {
            //Builds the tables from the on and off carousle information 
            //Since list of list of string 2 for loops needed ot grab all the data and display it 
            //Both loops for the on and off do the exact same functionality 
            for (var x = 0; x < data.onData.length; x++) {
                var holdis = data.onData[x];
                var tBuild = '';
                tBuild += ('<tr>');
                for (var y = 0; y < holdis.length; y++) {
                    tBuild += '<td>' + holdis[y] + '</td>';
                };
                tBuild += '</tr>';
                carOnTable.append(tBuild);
            };
            for (var a = 0; a < data.offData.length; a++) {
                var holdiss = data.offData[a];
                var tBuilds = '';
                tBuilds += ('<tr>');
                for (var z = 0; z < holdiss.length; z++) {
                    tBuilds += '<td>' + holdiss[z] + '</td>';
                };
                tBuilds += '</tr>';
                carOffTable.append(tBuilds);
            };

        };
    };
};



$('#orderDeleteButton').click(function () {
    /* This function will call the delete function in the OrderStatusHub
    on the vb side. Upon clicking the delete button a propmpt will appear and 
    if okayed the ENTIRE order is deleted.
    */

    //Check to make sure there is a order number entered  
    if ($('#ordernumFilterOrder').val() != "") {
        var result = confirm("Click OK to delete ENTIRE ORDER");
        //If user confirmed delete request
        if (result) {
            orderStats.server.deleteOrder($('#ordernumFilterOrder').val(), $('#totLines').text()).done(function () {
                orderStatTable.draw();
            });

        };
    };

});
$(document.body).on('click', '#printList', function () {
    //reportsHub.server.printOSReport($('#ordernumFilterOrder').val(), $('#toteidFilterOrder').val());
    title = 'Order Status Report';
    getLLPreviewOrPrint('/Transactions/printOSReport', {
        orderNumber: $('#ordernumFilterOrder').val(),
        toteID: $('#toteidFilterOrder').val()
    }, true,'report', title);
});