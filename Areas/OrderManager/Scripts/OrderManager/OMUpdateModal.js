// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022


$(document).ready(function () {
    //update button for the order manager update records modal
    $('#OMUpdateModalButt').click(function () {
        //insert other modal show here
        $('#OMSelUpdateModal').modal('show');
        //enable checkboxes on second update modal here. 
        var tableDate = OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Required Date")];

        //var dateparts = tableDate.split('/')
        //var reqDate = new Date();
        //if (dateparts[0] < 10) {
        //    dateparts[0] = "0" + dateparts[0];
        //}
        //reqDate = String(dateparts[2]) + "-" + String(dateparts[0]) + "-" + String(dateparts[1]);

        //if value is changed enabled checkbox, else disable
        if ($('#OMUpdRequiredDate').val() != tableDate) {
            $('#OMCheckRequiredDate').removeAttr('disabled');
        } else {
            $('#OMCheckRequiredDate').attr('disabled', 'disabled');
            $('#OMCheckRequiredDate').removeAttr('checked');
        };

        if ($('#OMUpdNotes').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Notes")]) {
            $('#OMCheckNotes').removeAttr('disabled');
        } else {
            $('#OMCheckNotes').attr('disabled', 'disabled');
            $('#OMCheckNotes').removeAttr('checked');
        };

        if ($('#OMUpdPriority').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Priority")]) {
            $('#OMCheckPriority').removeAttr('disabled');
        } else {
            $('#OMCheckPriority').attr('disabled', 'disabled');
            $('#OMCheckPriority').removeAttr('checked');
        };

        if ($('#OMUpdUserField1').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field1")]) {
            $('#OMCheckUserField1').removeAttr('disabled');
        } else {
            $('#OMCheckUserField1').attr('disabled', 'disabled');
            $('#OMCheckUserField1').removeAttr('checked');
        };

        if ($('#OMUpdUserField2').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field2")]) {
            $('#OMCheckUserField2').removeAttr('disabled');
        } else {
            $('#OMCheckUserField2').attr('disabled', 'disabled');
            $('#OMCheckUserField2').removeAttr('checked');
        };

        if ($('#OMUpdUserField3').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field3")]) {
            $('#OMCheckUserField3').removeAttr('disabled');
        } else {
            $('#OMCheckUserField3').attr('disabled', 'disabled');
            $('#OMCheckUserField3').removeAttr('checked');
        };

        if ($('#OMUpdUserField4').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field4")]) {
            $('#OMCheckUserField4').removeAttr('disabled');
        } else {
            $('#OMCheckUserField4').attr('disabled', 'disabled');
            $('#OMCheckUserField4').removeAttr('checked');
        };

        if ($('#OMUpdUserField5').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field5")]) {
            $('#OMCheckUserField5').removeAttr('disabled');
        } else {
            $('#OMCheckUserField5').attr('disabled', 'disabled');
            $('#OMCheckUserField5').removeAttr('checked');
        };

        if ($('#OMUpdUserField6').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field6")]) {
            $('#OMCheckUserField6').removeAttr('disabled');
        } else {
            $('#OMCheckUserField6').attr('disabled', 'disabled');
            $('#OMCheckUserField6').removeAttr('checked');
        };

        if ($('#OMUpdUserField7').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field7")]) {
            $('#OMCheckUserField7').removeAttr('disabled');
        } else {
            $('#OMCheckUserField7').attr('disabled', 'disabled');
            $('#OMCheckUserField7').removeAttr('checked');
        };

        if ($('#OMUpdUserField8').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field8")]) {
            $('#OMCheckUserField8').removeAttr('disabled');
        } else {
            $('#OMCheckUserField8').attr('disabled', 'disabled');
            $('#OMCheckUserField8').removeAttr('checked');
        };

        if ($('#OMUpdUserField9').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field9")]) {
            $('#OMCheckUserField9').removeAttr('disabled');
        } else {
            $('#OMCheckUserField9').attr('disabled', 'disabled');
            $('#OMCheckUserField9').removeAttr('checked');
        };

        if ($('#OMUpdUserField10').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field10")]) {
            $('#OMCheckUserField10').removeAttr('disabled');
        } else {
            $('#OMCheckUserField10').attr('disabled', 'disabled');
            $('#OMCheckUserField10').removeAttr('checked');
        };

        if ($('#OMUpdEmergency').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Emergency")]) {
            $('#OMCheckEmergency').removeAttr('disabled');
        }else if ($('#OMUpdEmergency').val().toLowerCase() == 'true' && OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Emergency")] == '0') {
            $('#OMCheckEmergency').removeAttr('disabled');
        }else if ($('#OMUpdEmergency').val().toLowerCase() == 'false' != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Emergency")] == '1') {
            $('#OMCheckEmergency').removeAttr('disabled');
        } else {
            $('#OMCheckEmergency').attr('disabled', 'disabled');
            $('#OMCheckEmergency').removeAttr('checked');
        };

        if ($('#OMUpdLabel').val() != OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Label")]) {
            $('#OMCheckLabel').removeAttr('disabled');
        }else if ($('#OMUpdLabel').val()=='True' && OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Label")]=='0') {
            $('#OMCheckLabel').removeAttr('disabled');
        } else if ($('#OMUpdLabel').val() == 'False' && OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Label")] == '1') {
            $('#OMCheckLabel').removeAttr('disabled');
        } else {
            $('#OMCheckLabel').attr('disabled', 'disabled');
            $('#OMCheckLabel').removeAttr('checked');
        };
    });
});