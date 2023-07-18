// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document.body).on('keyup', function (e) {
    // if there is no active text box, etc.
    if (document.activeElement.tagName.toLowerCase() != 'input') {
        switch (e.keyCode) {
            case 65:
                // A automated pick
                break;
            case 66:
                // B bulk pick
                break;
            case 67:
                // C change diagnostic mode -- not implemented at the moment
                break;
            case 69:
                // E Automated Put Away
                break;
            case 72:
                // H Hot pick
                break;
            case 76:
                // L Bulk Count
                break;
            case 77:
                // M Admin Menu
                break;
            case 83:
                // S Spin to Location -- no implemented at the moment
                break;
            case 84:
                // T Automated Count
                break;
            case 85:
                // U Bulk Put Away
                break;
            case 87:
                // W Support Website -- not implemented at the moment
                break;
        }
    };
});