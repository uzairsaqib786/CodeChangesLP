// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

//var OMHub = $.connection.orderManagerHub;

$(document).ready(function () {
    //Modal appears after you select update records, then select update from order manager update records modal
    //update button
    $('#OMSelUpdateButt').click(function () {
        //call update function here. chenge these to refelct column sequence
        var id= OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("ID")];
        var ordernumber= OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Order Number")];
        var ordernumbers = [];

        for (var x = 0; x < OMTable.rows().data().length; x++) {
            if ($.inArray(OMTable.row(x).data()[OrderManCols.indexOf("Order Number")], ordernumbers) == -1) {
                ordernumbers.push(OMTable.row(x).data()[OrderManCols.indexOf("Order Number")])
            };
        };
        var val1;
        var val2;
        if ($('#OMColumn').val().indexOf('Date') > -1) {
            val1 = $('#OMVal1D').val();
            val2 = $('#OMVal2D').val();
        } else {
            val1 = $('#OMVal1').val();
            val2 = $('#OMVal2').val();
        }
        //function call
        OMHub.server.updateOrderManagerRecords($('#ViewType').val(), $('#OrderType').val(), id,
                                               $('#OMUpdRequiredDate').val(), $('#OMUpdNotes').val(), $('#OMUpdPriority').val(),
                                               $('#OMUpdUserField1').val(), $('#OMUpdUserField2').val(), $('#OMUpdUserField3').val(),
                                               $('#OMUpdUserField4').val(), $('#OMUpdUserField5').val(), $('#OMUpdUserField6').val(),
                                               $('#OMUpdUserField7').val(), $('#OMUpdUserField8').val(), $('#OMUpdUserField9').val(),
                                               $('#OMUpdUserField10').val(), $('#OMUpdEmergency').val(), $('#OMUpdLabel').val(),
                                               $('#OMCheckRequiredDate').prop('checked'), $('#OMCheckNotes').prop('checked'),
                                               $('#OMCheckPriority').prop('checked'), $('#OMCheckUserField1').prop('checked'),
                                               $('#OMCheckUserField2').prop('checked'), $('#OMCheckUserField3').prop('checked'),
                                               $('#OMCheckUserField4').prop('checked'), $('#OMCheckUserField5').prop('checked'),
                                               $('#OMCheckUserField6').prop('checked'), $('#OMCheckUserField7').prop('checked'),
                                               $('#OMCheckUserField8').prop('checked'), $('#OMCheckUserField9').prop('checked'),
                                               $('#OMCheckUserField10').prop('checked'), $('#OMCheckEmergency').prop('checked'),
                                               $('#OMCheckLabel').prop('checked')).done(function (mess) {
                                                   if (mess == "Error") {
                                                       MessageModal("Error", "An error has occurred when updating the data")
                                                   } else {
                                                       //to redraw the table
                                                       $('#OMDispVals').click();
                                                       $('#OMUpdateModal').modal('hide');
                                                       $('#OMUpdateButt').attr('disabled', 'disabled');
                                                   };
                                               });
    });
});
