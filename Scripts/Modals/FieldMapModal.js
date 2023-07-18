// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var cycleCountHub = $.connection.cycleCountHub;
$(document).ready(function () {
    $('.field-map-input').on('input', function () {
        var ID = $(this).data('id');
        var startPos = $(ID + "_start").val();
        var len = $(ID + "_length").val();
        $(ID + "_end").val(parseInt(startPos) + parseInt(len) - 1);
        if (startPos == "0" && len == "0") {
            $(ID + "_end").val(0);
        };
    });

    $('#FieldMapModalSave').click(function () {
        cycleCountHub.server.updateFieldMapModel(
                                                 $("#Warehouse_start").val(), $("#Warehouse_length").val(),
                                                 $("#Warehouse_end").val(), $("#Warehouse_pad").prop('checked'),
                                                 $("#Warehouse_format").val(),
                                                 $("#ExpirationDate_start").val(), $("#ExpirationDate_length").val(),
                                                 $("#ExpirationDate_end").val(), $("#ExpirationDate_pad").prop('checked'),
                                                 $("#ExpirationDate_format").val(),
                                                 $("#ItemNumber_start").val(), $("#ItemNumber_length").val(),
                                                 $("#ItemNumber_end").val(), $("#ItemNumber_pad").prop('checked'),
                                                 $("#ItemNumber_format").val(),
                                                 $("#LotNumber_start").val(), $("#LotNumber_length").val(),
                                                 $("#LotNumber_end").val(), $("#LotNumber_pad").prop('checked'),
                                                 $("#LotNumber_format").val(),
                                                 $("#SerialNumber_start").val(), $("#SerialNumber_length").val(),
                                                 $("#SerialNumber_end").val(), $("#SerialNumber_pad").prop('checked'),
                                                 $("#SerialNumber_format").val(),
                                                 $("#HostQuantity_start").val(), $("#HostQuantity_length").val(),
                                                 $("#HostQuantity_end").val(), $("#HostQuantity_pad").prop('checked'),
                                                 $("#HostQuantity_format").val(),
                                                 $('#FieldMapFilePath').val(), $('#FieldMapFileExten').val(),
                                                 $("#FieldMapActive").prop('checked'), $('#FieldMapBackup').val()
                                                 ).done(function (mess) {
                                                     if (mess == "Error") {
                                                         $('#FieldMapModal').modal('hide');
                                                         MessageModal("Error", "An error has occured");
                                                     };
                                                 });
    })
});