// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var exec = false;
    $('#Message_Modal').on('hidden.bs.modal', function () {
        if (exec) {
            $('#Message_submit').trigger('custom_ok').off('custom_ok');
        };
        $('#Message_Modal').trigger('custom_close');
        $('#Message_Modal').off('custom_close');
        //$(".modal-backdrop").remove();
    });

    $('#Message_Modal').on('shown.bs.modal', function (e) {
        $('#Message_Modal').trigger('custom_shown');
        $('#Message_Modal').off('custom_shown');
        $('#Message_submit').focus();
        //e.stopImmediatePropagation();
    })

    $('#Message_submit').click(function () {
        exec = true;
    });

    $('#Message_cancel').click(function () {
        $('#Message_submit').off('custom_ok');
    })
});
var customOnClose;
function MessageModal(Title, Message, onCloseFunction, onShownFunction, onSubmitFunction) {
    if (typeof Title === 'object') {
        $('#Message_label').html(Title.Title);
        $('#Display_Message').html(Title.Message);
        $('#Message_Modal').modal('show');
        
        if (Title.onCloseFunction != undefined) {
            $('#Message_Modal').on('custom_close', Title.onCloseFunction);
            customOnClose = Title.onCloseFunction;
        };
        if (Title.onShownFunction != undefined) {
            $('#Message_Modal').on('custom_shown', Title.onShownFunction);
        };
        if (Title.onSubmitFunction != undefined) {
            $('#Message_submit').on('custom_ok', Title.onSubmitFunction);
            $('#Message_cancel').show();
        } else {
            $('#Message_cancel').hide();
        };
    } else {
        $('#Message_label').html(Title);
        $('#Display_Message').html(Message);
        $('#Message_Modal').modal('show');

        if (onCloseFunction != undefined) {
            $('#Message_Modal').on('custom_close', onCloseFunction);
            customOnClose = onCloseFunction;
        };
        if (onShownFunction != undefined) {
            $('#Message_Modal').on('custom_shown', onShownFunction);
        };
        if (onSubmitFunction != undefined) {
            $('#Message_submit').on('custom_ok', onSubmitFunction);
            $('#Message_cancel').show();
        } else {
            $('#Message_cancel').hide();
        };
    }
   
}

