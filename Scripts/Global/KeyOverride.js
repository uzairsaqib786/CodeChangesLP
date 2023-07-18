// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

function avoidInvalidKeyStorkes(evtArg) {
    var evt = (document.all ? window.event : evtArg);
    var KEYCODE = (document.all ? window.event.keyCode : evtArg.which);

    var element = (document.all ? window.event.srcElement : evtArg.target);

    // keycode 112 = F1 = Go to Help Page
    if (KEYCODE == "112") {
        document.onhelp = function () {
            return (false);
        };
        window.onhelp = function () {
            return (false);
        };
        evt.returnValue = false;
        evt.keyCode = 0;
        evt.preventDefault();
        evt.stopPropagation();
        var pageName = $('#PageName').text();
        if (pageName.indexOf('|') > -1) {
            pageName = pageName.split('|')[1];
        };
        pageName = pageName.trim().toLowerCase();
        while (pageName.indexOf(' ') > -1) {
            pageName = pageName.replace(' ', '');
        };
        if (pageName.toLowerCase() == 'menu') { pageName = ''; };
        // if this is not a top level menu and the page has an app associated with it.
        if ($('#AppName').length > 0) {
            var prefix = '/' + $('#AppName').val();
            // if the page is shared or it is admin then we do not want a prefix to the url
            // this checks if the querystring data contains App, /IM/preferences will return false, while /Transactions?App=IM will return 'IM'
            if (getQueryVariable('App') != false || prefix.toLowerCase() == '/admin') { prefix = ''; };
            var initIndex = ['orderstatus', 'opentransactions', 'transactionhistory', 'reprocesstransactions'].indexOf(pageName);
            if (initIndex > -1) {
                window.location.href = '/Help?initialPage=transactions&viewToShow=' + (initIndex + 1);
            } else {
                // we want to go to <app>/<app>help?initialPage... but if it is admin (no prefix) then we need a leading slash (/help?initialPage...), so that we navigate correctly.
                window.location.href = prefix + (prefix == '' ? '/' : prefix) + 'Help?initialPage=' + pageName;
            };
        };
    } else if (KEYCODE == '8' && document.activeElement.tagName.toLowerCase() != 'input' && document.activeElement.tagName.toLowerCase() != 'textarea') {
        evt.returnValue = false;
        evt.keyCode = 0;
        evt.preventDefault();
        evt.stopPropagation();
    };
    window.status = "Done";
};


if (window.document.addEventListener) {
    window.document.addEventListener("keydown", avoidInvalidKeyStorkes, false);
} else {
    window.document.attachEvent("onkeydown", avoidInvalidKeyStorkes);
    document.captureEvents(Event.KEYDOWN);
};