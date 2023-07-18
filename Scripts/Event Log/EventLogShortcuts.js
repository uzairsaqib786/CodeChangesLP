// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document.body).on('keyup', function (e) {
    // if there is no active text box, etc.
    if (document.activeElement.tagName.toLowerCase() != 'input' && !activeModal) {
        switch (e.keyCode) {
            case 67:
                // C clear filters
                $('#clearFilters').trigger("click");
                break;
            case 68:
                // D Delete range of entries by date
                $('#deleteRange').trigger("click");
                break;
            case 69:
                // E Export range of entries by date
                $('#exportRange').trigger("click");
                break;
            case 73:
                // I trigger print dropdown
                $('#printButton').trigger("click");
                break;
            case 82:
                // R Refresh data
                $('#refreshTable').trigger('click');
        };
    };
});