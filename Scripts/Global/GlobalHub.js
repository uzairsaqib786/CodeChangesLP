// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var globalHubConn = $.connection.globalHub;
var reportsHub = $.connection.customReportsHub;
var hubStarted = false;
$(document).ready(function () {
    $('#goToAppMenu').click(function () {
        location.href = "/"
    })


    function SetSTEServiceStatus() {
        globalHubConn.server.checkSTEService().done(function (Status) {
            $('#STEServiceOnline').html(Status ? 'Online' : 'Offline').css('color', Status ? 'green' : 'red');
        });
    };

    setInterval(function () {
        globalHubConn.server.clientHeartbeat();
        SetSTEServiceStatus()
        //var date = new Date()
        //console.log('Minutes: ' + date.getMinutes() + ', Seconds: ' + date.getSeconds())
    }, 15000);

    //Passes username to SignalR Connection
    $.connection.hub.qs = {
        "currentUser": $('#currentUser').val(),
        "WSID": $('#WSID').val(),
        "ConnectionName": $('#ConnectionName').val(),
        "AppName": $("#AppName").val(),
        "CurrentPage": $('#PageUUID').val()
    };

    //Logs SignalR errors to JS Console
    $.connection.hub.error(function (error) {
        console.log('SignalR error: ' + error)
    });
    //Adds SignalR log messages to the console
    //$.connection.hub.logging = true;

    $(window).on('beforeunload', function(e) {
        globalHubConn.server.leavingPage()
    });


    $.connection.hub.start().done(function () {
        //Intial Heartbeat on Page Connect
        globalHubConn.server.clientHeartbeat();
        hubStarted = true;
        // handles starting and ending work session per user
        $(document.body).on('click', '#startWork', function () {
            var sender = $(this);
            sender.attr('disabled', 'disabled');
            globalHubConn.server.workClick(sender.text()).done(function () {
                if (sender.text() == 'Start Work') {
                    sender.css("color", "red");
                    sender.text('End Work');
                } else {
                    sender.css("color", "green");
                    sender.text('Start Work');
                };
                sender.removeAttr('disabled');
            });
        });
    });

    //Stops disabled links from redirecting you
    $('body').on('click', 'a.disabled', function (event) {
        event.preventDefault();
    });

    // Initializes all tooltips
    $('[data-toggle="tooltip"]').tooltip();

    /*
        Triggers typeahead dropdown on click of input
    */
    $(document.body).on('focus', '.tt-input', function () {
        triggerDownClick($(this));
    });

    $(document.body).on('input', '.tt-input', function () {
        if ($(this).val() == '') {
            $(this).typeahead('val', '');
        }
    })

    var paginateTimer = mkTimer(function (e) {
        paginateTable(e.keyCode);
    }, 200);

    $(document.body).on('keyup', function (e) {
        // if there is no active text box, etc.
        if (document.activeElement.tagName.toLowerCase() != 'input' && !activeModal) {
            paginateTimer.startTimer(e);
        } else if (document.activeElement.tagName.toLowerCase() == 'input') {
            // if there is an active input element (text box, etc) then on ENTER/RETURN press blur the element (eliminate focus)
            if (e.keyCode == 13) {
                //$(document.activeElement).blur();
            };
        };
    });

    $('#emergency_submit').click(function () {
        alert('Not implemented.  Need to figure out which to link to: Automated or Bulk Pick and those screens are not implemented.');
    });

    /*
       Allows Multiple Modals to be active at the same time
   */
    $('.modal').on('hidden.bs.modal', function (event) {
        $(this).removeClass('fv-modal-stack');
        $('body').data('fv_open_modals', $('body').data('fv_open_modals') - 1);
    });
    $('.modal').on('shown.bs.modal', function (event) {
        // keep track of the number of open modals
        if (typeof ($('body').data('fv_open_modals')) == 'undefined') {
            $('body').data('fv_open_modals', 0);
        };
        // if the z-index of this modal has been set, ignore.
        if ($(this).hasClass('fv-modal-stack')) {
            return;
        };
        $(this).addClass('fv-modal-stack');
        $('body').data('fv_open_modals', $('body').data('fv_open_modals') + 1);
        $(this).css('z-index', 1040 + (10 * $('body').data('fv_open_modals')));
        $('.modal-backdrop').not('.fv-modal-stack')
                .css('z-index', 1039 + (10 * $('body').data('fv_open_modals')));
        $('.modal-backdrop').not('fv-modal-stack')
                .addClass('fv-modal-stack');
    });

});

// Hub reconnected handler
$.connection.hub.reconnected(function () {
    console.log("Reconnected to SignalR");
});


//Reconnect to Hub If Disconnected
$.connection.hub.disconnected(function () {
    console.log("Disconnected from SignalR");
    setTimeout(function () {
        $.connection.hub.start();
    }, 5000); // Restart connection after 5 seconds.
});

/*
    Handles Keyboard shortcuts of N and P for the datatables plugin globally
*/
function paginateTable(keycode) {
    switch (keycode) {
        case 78:
            // N Next in any datatables instance
            $('.paginate_button.next:not(.disabled').trigger('click');
            break;
        case 80:
            // P Previous in any datatables instance
            $('.paginate_button.previous:not(.disabled').trigger('click');
            break;
    };
};

function addPrintMessage(message, id) {
    $("#print-alert").html('<div id="' + id + '" class="alert alert-info">Printing Started</div>');
    setTimeout(function () {
        $("#" + id).remove()
    }, 2000);
};

/*
    Updates print status
*/
globalHubConn.client.updatePrintStatus = function (statusMessage) {
    if ($('.Printing').length > 0) {
        addPrintMessage(statusMessage, "print-done");
        $('.Printing').removeClass('Printing');
    };
};;

/*
    Updates Print service status displayed to the user under Dropdown Menu
*/
globalHubConn.client.alertPrintServiceStateChange = function (state) {
    $('#PrintServiceOnline').html(state ? 'Online' : 'Offline').css('color', state ? 'green' : 'red');
};

/*
    Alerts users when there are emergency transactions that must be completed immediately and continues to do so until those tranactions are complete.
        Only alerts users at workstations whose zones/carousels match those of the emergency transactions
*/
globalHubConn.client.alertEmergencyOrders = function (data) {
    console.log('Emergency Order modal currently disabled');
    return;
    var emModal = $('#emergency_modal');
    if (emModal.hasClass('in') || $('#replen_status_modal').length > 0) {
        return true;
    };
    var modalPresent = $('.modal.in');

    var zone = $('#emergency_zone');
    var carousel = $('#emergency_car');

    zone.html('');
    carousel.html('');

    for (var x = 0; x < data.length; x++) {
        zone.append('<div class="alert alert-danger alert-custom" role="alert"><strong>' + data[x].Zone + '</strong></div>');
        carousel.append('<div class="alert alert-danger alert-custom" role="alert"><strong>' + data[x].Carousel + '</strong></div>');
    };

    if (modalPresent.length > 0) {
        modalPresent.modal('hide').one('hidden.bs.modal', function () {
            emModal.modal('show');
        });
    } else {
        emModal.modal('show');
    };
};

// returns the date from JS in format: mm/dd/yyyy hh:mm
function getCurrentDateTime() {
    var date = new Date();
    // JS month is 0 indexed
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var year = date.getFullYear();
    var hours = date.getHours();
    var minutes = date.getMinutes();

    if (hours > 12) { hours = date.getHours() - 12; };
    if (hours < 10) { hours = '0' + hours; };

    if (minutes < 10 && minutes != 0) {
        minutes = '0' + minutes;
    } else if (minutes == 0) { minutes = '00'; };
    return month + '/' + day + '/' + year + ' ' + hours + ':' + minutes;
};

// returns the date from JS as mm/dd/yyyy
function getCurrentDate() {
    var date = new Date();
    return date.getMonth() + 1 + '/' + date.getDate() + '/' + date.getFullYear();
};

//Date Code
Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});

function setToToday() {
    var date = new Date();

    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = month + "/" + day + "/" + year;
    return today;
};

//Allows only numeric strings in an input box
function setNumeric(element) {
    var value = element.val();
    var decimalVal = "";
    if (value.indexOf(".") == 0) {
        decimalVal = ".";
        value.substring(1, value.length - 1);
    };
    while (!$.isNumeric(value) && value.length > 0) {
        value = value.substring(0, value.length - 1);
    };
    element.val(decimalVal + value);
};


/*
    Allows only numeric strings within a specified range inside of an input box
    Parameters:
        Optional 1:
            Usage: setNumericInRange($(this), 0, 10);
            
            element: jQuery selector of the input tag to set
            low: numeric, low value of the input
            high: numeric, high value of the input
        Optional 2:
            Usage: setNumericInRange($(this), {low: 0, high: 10});
                (or: setNumericInRange($(this), SqlLimits.numerics.int);)

            element: jQuery selector of the input tag to set
            low: object containing a low and high property.  Should resemble something like this: low = {low: 1, high: 10};
            high: excluded from this call, you do not need to specify this value at all.
*/
function setNumericInRange(element, low, high, allowEmpty) {
    if (allowEmpty == undefined) {
        allowEmpty = false;
    }
    if (!low.hasOwnProperty('min')) {
        var value = element.val();
        while ((!$.isNumeric(value) && value.length > 0) || (parseInt(value) > high && high != null)) {
            value = value.substring(0, value.length - 1);
        };
        if (low != null && value < low && value.trim() != '') {
            value = low;
        };
        element.val(value);
    } else {
        var h = low.max;
        var l = low.min;
        var value = element.val();
        while ((!$.isNumeric(value) && value.length > 0) || (parseInt(value) > h && h != null)) {
            value = value.substring(0, value.length - 1);
        };
        if (l != null && value < l && value.trim() != '' && !allowEmpty) {
            value = l;
        };
        element.val(value);
    };
};

/*
    Allows only numeric, comma seperated values in input box provided by element (jQuery selector)
*/
function setNumericCommaSeparated(element) {
    var allowed = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ', ','], index = 0;
    while (index < element.val().length) {
        if (allowed.indexOf(element.val()[index]) == -1) {
            element.val(element.val().substring(0, index).trim());
        } else {
            index++;
        };
    };
};

/*
    Removes the Extension from a string typed into an input box
    (Disallows the use of the "." character.)
*/
function setNoExtension(element) {
    var value = element.val();
    var extensionIndex = value.indexOf('.');
    if (extensionIndex != -1) {
        element.val(value.substring(0, extensionIndex))
    };
};

/*
    Prevents the space character from being contained within the jQuery selector "element"
*/
function setNoSpaces(element) {
    var value = element.val();
    var spaceIndex = value.indexOf(' ');
    if (spaceIndex != -1) {
        element.val(value.substring(0, spaceIndex));
    };
};

/*
    Triggers a down arrow key down event on the jQuery selector parameter, element.
*/
function triggerDownClick(element) {
    ev = $.Event("keydown");
    ev.keyCode = ev.which = 40;
    element.trigger(ev);
};

/*
    Prevents the string "null" or "nothing" from being inserted into a particular element.  (Only come across this when server returns Nothing as an object type.)
*/
function setNullEmptyStr(value) {
    if (value == null) return ''
    else return String(value);
};

/*
    Determines whether the object provided by obj has the property specified by prop.  If it does not have the property then noPropReturnValue is substituted as the return value.
    The object property is accessed via bracket notation to get its property to avoid problems with spaces (obj[prop]), which means that it can also double as a standard array accessor.
    Strings, arrays and objects are all compatible with this function, depending on how they are structured.
*/
function getOwnProp(obj, prop, noPropReturnValue) {
    if (obj != undefined && obj != null && obj.hasOwnProperty(prop)) return obj[prop]; else return noPropReturnValue;
};