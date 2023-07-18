// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/********************************************************************************************************************
    Keyboard Shortcuts for Transactions View
********************************************************************************************************************/
$(document.body).on('keyup', function (e) {
    // if there is no active text box, etc.
    if (document.activeElement.tagName.toLowerCase() != 'input') {
        // current view, needs to be split and trimmed in order to get just "Open", etc.
        var viewCurr = $('#currentViewTitle').text().trim().split(" ", 1)[0];

        /* Individual view handling */
        switch (viewCurr) {
            /*****************************
                Open Transactions
            ******************************/
            case "Open":
                switch (e.keyCode) {
                    case 72:
                        // H Send to hist
                        $('#sendCompletedToHist').trigger("click");
                        break;
                    case 83:
                        // S column sequence
                        $('#setDefaultOpen').trigger("click");
                        break;
                    case 84:
                        // T reset to today's date
                        $('#resetDateOpen').trigger("click");
                        break;
                    case 89:
                        // Y transactions history
                        $('#histView').trigger("click");
                        break;
                };
                break;
                /* ********************************
                    Transaction History 
                **********************************/
            case "Transaction":
                switch (e.keyCode) {
                    case 83:
                        // S column sequence
                        $('#setDefaultHist').trigger("click");
                        break;
                    case 84:
                        // T reset to today's date
                        $('#resetDateTrans').trigger("click");
                        break;
                };
                break;
                /******************************************
                    Order Status
                *******************************************/
            case "Order":
                switch (e.keyCode) {
                    case 67:
                        // C clear order/tote
                        if ($('#toteClearButton').attr('disabled') != 'disabled') { $('#toteClearButton').trigger("click"); };
                        break;
                };
                break;
        };
    };
});