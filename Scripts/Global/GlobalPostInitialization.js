// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*
    Purpose: Creates an encapsulated setTimeout to rate limit save and refresh functions (like DataTable.draw() or someHub.server.functionCall()) to prevent problems with network, sql or signalR overload.
    Parameters:
        timerExpiredCallback: function which will be executed once the timeout has occurred.  (like DataTable.draw() or some save function)
        timeOutInms: the amount of time in milliseconds that the timer should wait for.  (1000 = wait 1 second before calling timerExpiredCallback).
    Methods:
        timerExpiredAction: timerExpiredCallback parameter.  Calling this will cause an immediate execution of the callback.
        startTimer: 
            Description: start the timeout (or reset it if it is already set) to count down the time until timerExpiredAction is automatically called by the instance.
            Parameters: <object> of anonymous type.  This parameter is provided to timerExpiredAction (timerExpiredCallback).
    Properties:
        User Can Alter:
            isUpToDate: boolean type which indicates whether there are pending changes on an input to be saved.  YOU must set this to false when the startTimer's normal trigger is dirtied.  100% optional and only indicates whether YOU have made a change since the last time that the plugin timeout expired.
        User Should Not Alter:
            newTime: current time when startTimer is called.
            typingTime: difference in time between newTime and the previous value of newTime.
            updateInterval: instance of setTimeout with a timeout value of timeOutTime
            timeOutTime: timeOutInms if provided.  If the value is not provided this is 100 ms.  Used to set the timeout time for startTimer().
*/
function mkTimer(timerExpiredCallback, timeOutInms) {
    var me = {
        newTime: 0,
        typingTime: 0,
        updateInterval: 0,
        timerExpiredAction: timerExpiredCallback,
        isUpToDate: true
    };
    me.startTimer = function (callbackParams) {
        me.newTime = new Date().getTime();
        if (me.newTime - me.typingTime > me.timeOutTime) {
            me.timerExpiredAction(callbackParams);
            me.isUpToDate = true;
        } else {
            clearTimeout(me.updateInterval);
            me.updateInterval = setTimeout(function () {
                me.timerExpiredAction(callbackParams);
                me.isUpToDate = true;
            }, me.timeOutTime);
        };
        me.typingTime = me.newTime;
    };
    if (timeOutInms != undefined && $.isNumeric(timeOutInms)) {
        me.timeOutTime = timeOutInms;
    } else {
        me.timeOutTime = 500;
    };
    return me;
};

/*
    Adds a resize handler on the window when provided a function to call and immediately calls the function to cause a resize.  If the parameter passed is not a function it will log (to the console) the error and type of the parameter.
*/
function addResize(resize) {
    var rType = typeof resize;
    if (rType == 'function') {
        resize();
        $(window).resize(resize);
    } else {
        console.log('addResize called with a non-function parameter.  The parameter is required to be a function.  Passed type: ' + rType);
    };
};
/*
    Parameters: value: Boolean / bool castable value
    Returns: 1 if the value is evaluated to true, else 0.
*/
function toSqlBool(value) {
    return (value ? 1 : 0);
};

/*
    Usage: var a = <some string>.toTitleCase();
    Example:
        var a = 'heLlO theRe ThiS is SoMe mEssed Up TeXT';
        a = a.toTitleCase();
        console.log(a);
        // prints 'Hello There This Is Some Messed Up Text'
*/
String.prototype.toTitleCase = function () {
    var title = '';
    var valSplit = this.split(' ');
    for (var k = 0; k < valSplit.length; k++) {
        title += (valSplit[k].charAt(0).toUpperCase() + valSplit[k].substring(1, valSplit[k].length).toLowerCase()) + ' ';
    };
    return title.trim();
};
/*
    Gets a specified query string variable by name.
    Example:  
        URL: /IM/Inventory?App=IM
        Call: getQueryVariable('App') --> 'IM'
*/
function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0].toLowerCase() == variable.toLowerCase()) { return pair[1]; }
    }
    return (false);
};

/*****************************************
    Object to indicate SQL Server's limits when it comes to different field types.
    Properties:
        numerics: Standard SQL Server integer type fields.
        text: Non-Standard (PickPro custom) typical field sizes for NVARCHAR fields.
            large: like [Description] field in [Inventory]
            standard: like [Username] in [Employees], etc.
*****************************************/
var SqlLimits = {
    numerics: {
        bigint: {
            min: -9223372036854775808,
            max: 9223372036854775807
        },
        int: {
            min: -2147483648,
            max: 2147483647
        },
        smallint: {
            min: -32768,
            max: 32767
        },
        tinyint: {
            min: 0,
            max: 255
        },
        // float & real: unrealistic it could ever go this high, will truncate if necessary, more likely to see an overflow in vb than sql server
    },
    text: {
        large: {
            min: 0,
            max: 255
        },
        standard: {
            min: 0,
            max: 50
        }
    }
};

$(document).ready(function () {
    // prevent typeahead from wrapping to the next line when next to another input (like in the datatables' search box)
    $('.twitter-typeahead').css('display', 'inline');

    // prevent disabled tabs from executing their click functions
    $(".nav-tabs a[data-toggle=tab]").on("click", function (e) {
        if ($(this).hasClass("disabled")) {
            e.preventDefault();
            return false;
        };
    });

   

    /*
        Activates all datepickers with the date-picker class
    */
    $('.date-picker').datetimepicker({
        pickTime: false,
        minDate: '01/01/1900'
    });

    $.each($('.date-picker'), function (index, value) {
        var v$ = $(value);
        var dateToSet = v$.data('init-date');
        if (dateToSet != undefined) {
            v$.data("DateTimePicker").setDate(dateToSet);
        };
    });
});