// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var context = $('#showTest').html().toLowerCase().replace('pickpro', '').trim();
    var initPage = $('#initialPage').val().toLowerCase();
    if (initPage.trim() == '') initPage = 'help';
    loadPartial(initPage, $('#subPage').val().trim());
    $('#' + initPage).parent('li').addClass('active');

    // handles hiding accordions for the help page
    $(document.body).on('hide.bs.collapse', '.accordion-toggle', function (e) {
        var span = $(this).siblings('.panel-heading').children().children().children();
        span.removeClass('accordion-caret-up');
        span.addClass('accordion-caret-down');
        e.stopPropagation();
    });

    // handles expanding accordions for the help page
    $(document.body).on('show.bs.collapse', '.accordion-toggle', function (e) {
        var span = $(this).siblings('.panel-heading').children().children().children();
        span.removeClass('accordion-caret-down');
        span.addClass('accordion-caret-up');
        e.stopPropagation();
    });
    
    $('.request-partial').click(function () {
        $('li.active').removeClass('active');
        $(this).parent('li').addClass('active');
        // recreate the url of the page we are on and replace the querystring var initialPage to be what we have currently selected.
        var newurl = window.location.protocol + '//' + window.location.host + window.location.pathname + '?initialPage=' + this.id;
        // push the new tab into history so that the back button will allow us to actually handle going "back" to another tab.
        window.history.pushState({ path: newurl }, '', newurl);
        // load the newly requested help page as a partial via ajax
        loadPartial(this.id, '');
    });

    // this event fires when there is a backward/forward browser click (or backspace, etc.)
    window.onpopstate = function (e) {
        // get the "current" state (which is the new state after the back/forward action)
        var id = getQueryVariable('initialPage');
        // load the current state's help page
        loadPartial(id, '');
        // reset the tabs so that the proper one is highlighted.
        $('#HelpSelector').find('.active').removeClass('active');
        $('#' + id).parent('li').addClass('active');
    };

    function loadPartial(id, subArea) {
        var partialURL = '/' + context + 'Help/' + id + (subArea != '' ? '?viewToShow=' + subArea : '');
        $('#HelpContent').html('<div class="text-center"><h1>Loading...</h1></div>').load(partialURL, function (response, status, xhr) {
            if (status == 'error') {
                MessageModal('Error', 'Help page may not be available.  An error occurred during loading.  Please contact Scott Tech if the issue persists.', function () {
                    $('#help').click(); // try to load the main help page because it failed to load the requested one.
                });
            } else {
                var t = $('.toggles');

                // if there are objects with class toggles present AND the jQuery plugin registration was completed (plugin is loaded)
                if (t.length > 0 && typeof $.fn.toggles == 'function') {
                    t.toggles({
                        width: 60,
                        height: 25
                    });
                };

                $('[data-toggle="tooltip"]').tooltip();
            };
        });
    };

    addResize(function () {
        $('#HelpSelector').css({ 'max-height': $(window).height() * 0.85 });
    });
});