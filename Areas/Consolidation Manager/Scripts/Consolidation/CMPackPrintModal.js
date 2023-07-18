// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022


var prev;
var ordernum;
var packListSort;
$(document).ready(function () {

    packListSort = $('#PackListSort').val();

    //add click event for 2 buttons

    $('#PrintNewLines').click(function () {
        //set variable for new lines
        if (prev) {
            title = 'CM Pack List';
            getLLPreviewOrPrint('/CM/Consolidation/PrintPrevCMPackList', {
                OrderNum: ordernum,
                Where: "where",
                OrderBy: packListSort,
                Print: 0
            }, false,'report', title)
            $('#TopRow').hide();
            $('#ExitPrevCM').show();
            $('#PreviewRow').show();
        } else {
            title = 'CM Pack List';
            getLLPreviewOrPrint('/CM/Consolidation/PrintPrevCMPackList', {
                OrderNum: ordernum,
                Where: "where",
                OrderBy: packListSort,
                Print: 1
            }, true,'report', title)
            //call email stuff after print
        }
    });

    $('#PrintAllLines').click(function () {
        //set variable for all lines
        if (prev) {
            title = 'CM Pack List';
            getLLPreviewOrPrint('/CM/Consolidation/PrintPrevCMPackList', {
                OrderNum: ordernum,
                Where: "all",
                OrderBy: packListSort,
                Print: 0
            }, false,'report', title)
            $('#TopRow').hide();
            $('#ExitPrevCM').show();
            $('#PreviewRow').show();
        } else {
            title = 'CM Pack List';
            getLLPreviewOrPrint('/CM/Consolidation/PrintPrevCMPackList', {
                OrderNum: ordernum,
                Where: "all",
                OrderBy: packListSort,
                Print: 1
            }, true,'report', title)
            //call email stuff after print
        }
    });
});

function showCmPackPrintModal(preview, orderNumber) {
    prev = preview;
    ordernum = orderNumber;
    $('#CMPackPrintModal').modal('show');
};