// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    // make the automated pod information resize to prevent too much data from being on the screen at once and preventing the necessity of scrolling to reach the open picks, etc.
    addResize(function () {
        $('#allocatedTableScroll').css('max-height', $(window).height() / 2);
    });
});