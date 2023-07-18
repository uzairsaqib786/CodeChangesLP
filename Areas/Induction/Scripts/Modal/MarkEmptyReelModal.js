// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var MenuHub = $.connection.iMMenuHub;
$(document).ready(function () {
   
    $('#MarkEmptyReelModal').on('shown.bs.modal', function (e) {
        $('#EmptySerialNumberScan').focus();
    });

    $('#MarkEmptyReelModal').on('hidden.bs.modal', function (e) {
        //clear mdoal display here
        $('#EmptyReelSerialNumbersTableBody').html("");
    });
   
    
    $(document.body).on('click', '.remove-SerialNum', function (e) {
        var $this = $(this);
        //parent 1: td, parent 2: tr
        $this.parent().parent().remove();
    });


    $('#EmptySerialNumberScan').on('keypress', function (e) {
        if (e.which == 13) {
            $('#SerialNumberScanAlert').hide();

            if ($(this).val() == '') {
                DisplayAlert('Please enter a serial number');
                return;
            };

            $('#LastSerialNumberScan').val($(this).val());

            for (var i = 1; i <= $('#EmptyReelSerialNumbersTableBody tr').length; i++) {
                var row = $('#EmptyReelSerialNumbersTableBody').children('tr:nth-child(' + i.toString() + ')');
                //console.log(row.children('td:nth-child(1)').html());
                if ($('#EmptySerialNumberScan').val() == row.children('td:nth-child(1)').html()) {
                    $('#EmptySerialNumberScan').val('');
                    DisplayAlert('Serial Number already scanned');
                    return;
                };
            };

            //Add validation here
            MenuHub.server.validateSerialNumber($('#EmptySerialNumberScan').val()).done(function (rslt) {
                switch (rslt) {
                    case "Error":
                        DisplayAlert('There was an error validating serial number');
                        $('#EmptySerialNumberScan').val('');
                        break;
                    case "Invalid":
                        DisplayAlert('Serial Number Does Not Exist');
                        $('#EmptySerialNumberScan').val('');
                        break;
                    case "Valid":
                        AppendTableRow($('#EmptySerialNumberScan').val());
                        $('#EmptySerialNumberScan').val('');
                        break;
                };

            });
        };
    });


    $('#MarkReelsAsEmptyButt').click(function () {
        MessageModal('Marked Scanned Reels as Empty?', 'You are about to mark the scanned reels as empty. This will delete ALL current open transactions associated with the scanned reels.', function () { $('#EmptySerialNumberScan').focus(); }, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; },
            function () {
                $('#EmptySerialNumberScan').focus();
                var Serials = [];
                for (var i = 1; i <= $('#EmptyReelSerialNumbersTableBody tr').length; i++) {
                    var row = $('#EmptyReelSerialNumbersTableBody').children('tr:nth-child(' + i.toString() + ')');
                    Serials.push(row.children('td:nth-child(1)').html());
                };

                MenuHub.server.deleteSerialNumberOpenTrans(Serials).done(function (rslt) {
                    if (rslt) {
                        $('#EmptyReelSerialNumbersTableBody').html("");
                    } else {
                        MessageModal("Error", "An error occurred while clearing the serial numbers")
                    };
                });
            }
        );
    });

});

function AppendTableRow(SerialNum) {
    var row = '<tr>';
    row += '<td>' + SerialNum + '</td>';
    row += '<td><button type="button" class="btn btn-block btn-danger remove-SerialNum"><span class="glyphicon glyphicon-remove"></span></button></td>';
    row += '</tr>';
    $('#EmptyReelSerialNumbersTableBody').append(row);
};


function DisplayAlert(Message) {
    $('#SerialNumberScanAlert').html(Message);
    $('#SerialNumberScanAlert').show();
};