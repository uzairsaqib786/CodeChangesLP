// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var activeTab = 'detailstab';
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) { activeTab = this.id; });

    $(document.body).on('keyup', function (e) {
        if (activeModal) { return false; };
        // if there is no active text box, etc.
        if (document.activeElement.tagName.toLowerCase() != 'input' && document.activeElement.tagName.toLowerCase() != 'textarea') {
            switch (e.keyCode) {
                case 65:
                    // A Add record
                    $('#addButton').trigger('click');
                    break;
                case 67:
                    // C Clear
                    $('#clearButton').trigger('click');
                    break;
                case 68:
                    // D delete item
                    $('#deleteButton').trigger('click');
                    break;
                case 69:
                    // E details tab
                    $('.nav-tabs a[href="#details"]').tab('show');
                    break;
                case 75:
                    // K Kit Items tab
                    $('.nav-tabs a[href="#kit"]').tab('show');
                    break;
                case 76:
                    // L Locations tab
                    $('.nav-tabs a[href="#locations"]').tab('show');
                    break;
                case 79:
                    // O other tab
                    $('.nav-tabs a[href="#other"]').tab('show');
                    break;
                case 81:
                    // Q (un-)quarantine
                    $('#quarantineButton').trigger('click');
                    break;
                case 71:
                    // G reel tracking tab
                    $('.nav-tabs a[href="#reel"]').tab('show');
                    break;
                case 83:
                    // S Scan Codes tab
                    $('.nav-tabs a[href="#scan"]').tab('show');
                    break;
                case 73:
                    // I Item Setup tab
                    $('.nav-tabs a[href="#item"]').tab('show');
                    break;
                case 87:
                    // W weigh tab
                    $('.nav-tabs a[href="#weigh"]').tab('show');
                    break;
                case 72:
                    // H View History button
                    if (activeTab = 'detailstab') { $('#THbutton').trigger('click') };
                    break;
                case 86:
                    // V View Open button
                    if (activeTab == 'detailstab') { $('#OTbutton').trigger('click') };
                    break;
                case 82:
                    // R View Reprocess
                    if (activeTab == 'detailstab') { $('#Reprocbutton').trigger('click') };
                    break;
                case 85:
                    // U Update Reel RTS min
                    if (activeTab == 'reeltab') { $('#updateRTSReel').trigger('click'); };
                    break;
            };
        };
    });
});