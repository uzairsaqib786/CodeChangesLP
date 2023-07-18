// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*

This plugin is designed to be used in place of a datatable or similar structure where a multi-line approach or custom design is needed.
------------------------------------------------------------------------------------------------------------------------------------
Parameters:
isTable: currently unused
PerPage: number of records to display on a single page
perPageSelector: tag that will allow the plugin to call $(perPageSelector).val() to set the PerPage variable when there is a .change event fired on $(perPageSelector)
ID: ID attribute of the paging container
ID$: $(ID)
NextID: ID attribute of the paging plugin's next page button
NextID$: $(NextID)
PrevID: ID attribute of the paging plugin's previous page button
PrevID$: $(PrevID)
RowSelector: class or other selector which can select each of the page's individual entries/records in a .each situation.
currentPage: integer page number of the plugin
RowFunction: row builder function that has access to the entire resultset provided to the plugin.  Build your individual HTML elements here.  (Comparable to a <tr> in a datatables situation)
getPageData: ajax or signalR function which gets the data for the plugin.  First parameter must be start record, second parameter must be end record, third parameter is an array containing the elements defined in the getExtraParams function
getExtraParams: function to be overridden to get extra parameters in a list structure for use in the function to get data
processExtraData: function that allows the user to manipulate the entirety of the data returned to the plugin by a call.
PageID: ID attribute of the display label (IE: "Showing 1 to 10 of 25 records." type label)
PageID$: $(PageID)
waitInterval: interval in ms that determines how spaced out requests to getPageData need to be (default/minimum 100)
sourceType: string (signalr, ajax, or function) representing what type of call we are going to make.  signalr and ajax are handled the same way (use of <function>.done(function(result){ <do stuff> });)
    function is used under the assumption that the response is synchronous (usually that the data is already stored in the js and we just need to page it)
------------------------------------------------------------------------------------------------------------------------------------
Internal use variables:
newTime: used with waitInterval to ensure that queries do not take place too often.
typingTime: used with waitInterval to ensure that queries do not take place too often.
updateInterval: used with waitInterval to ensure that queries do not take place too often.
currentData: the data that is loaded in the plugin at the current time.  Does not update with the DOM.
querying: whether there is a query being actively run.
------------------------------------------------------------------------------------------------------------------------------------
Methods:
init: should be called immediately after configuration, will give debug messages if there are problems with id parameters, etc.
page(which): moves the plugin forward ('next') or backwards (anything else) in pages.
populate: refreshes the data for the plugin with getPageData.  Expects the RowFunction to create rows with the data provided by the getPageData method and the processExtraData function is called.
numRows: Counts the number of instances of RowSelector in the container.  currentData.page.length should, in most cases provide the same result.
------------------------------------------------------------------------------------------------------------------------------------
Usage:
// This plugin has only been tested with SignalR functions, but in principal should work with Ajax calls without alteration.

// The getExtraParams method can return any data structure.  It could be a list or object or even just a string.  This will only be important for the server side implementation where you 
// will need to receive that data type and deal with it effectively.  Objects passed from javascript must not be anonymous in VB otherwise they may not parse correctly or may be treated as a string.

// The paging plugin expects a response from the hub or ajax call that is structured like this:
// var response = {
//     pages: Array(Array()),
//     success: boolean,
//     message: string,
//     numRecords: integer (total number of records, whether they were filtered out or included),
//     filteredRecords: integer (number of records that can currently be seen by paging on the plugin, that were not filtered out),
//     extraData: anonymous object type.  Can be literally anything or nothing.  Not used by the plugin, but available to the processExtraData function via result.extraData and in the currentData property.
//     (other properties): Any other properties that are provided will not be used by the plugin, but will be available to the processExtraData function.
// };
// The model STPagingResult.vb has been created for the express purpose of usage as a server response for this plugin. It is stored in the PickProWeb Project.

var pager = STPaging({
        isTable: false,
        PerPage: 5,
        ID: "#container",
        RowSelector: '.row',
        Source: someHub.server.someFunction,
        RowFunction: function (row) {
            return makeRow(row)
        },
        NextID: '#Next',
        PrevID: '#Prev',
        getExtraParams: function () {
            return getSomeParams;
        },
        PageID: '#Page',
        processExtraData: function (result) {
            if (!result.success) {
                MessageModal('Error', 'There was an error while trying to get the next page of transactions.  Check the error log for details.');
            } else {
                $('#container').find('.row:first').addClass('active-div');
            };
        },
        waitInterval: 500
    });
// don't call this until the document is ready!
pager.init(); 

*/

function STPaging(params) {
    var me = {};
    me.isTable = params.isTable;
    me.PerPage = params.PerPage;
    me.perPageSelector = params.perPageSelector;
    // id of the paging container
    me.ID = params.ID;
    me.ID$ = $(me.ID);
    // next button
    me.NextID = params.NextID;
    me.Next$ = $(me.NextID);
    // previous button
    me.PrevID = params.PrevID;
    me.Prev$ = $(me.PrevID);
    // jquery selector for a "row" element
    me.RowSelector = params.RowSelector;
    me.currentPage = -1;
    // formats the provided data into a "row"
    me.RowFunction = params.RowFunction;
    // gets the paging data to display, MUST be a signalr function.
    me.getPageData = params.Source;
    // function to get list of additional parameters needed for the hub function call
    me.getExtraParams = params.getExtraParams;
    // function to handle additional returned data besides the paged aspects
    me.processExtraData = params.processExtraData;
    // id of the span that should contain the "page # of total pages..." label
    me.PageID = params.PageID;
    me.PageID$ = $(me.PageID);
    me.waitInterval = params.waitInterval;
    me.newTime = 0;
    me.typingTime = 0;
    me.updateInterval = 0;
    me.currentData = { numRecords: 0 };
    me.querying = false;
    me.sourceType = params.sourceType;

    $(document.body).on('click', me.NextID, function () {
        me.page('next');
    });

    $(document.body).on('click', me.PrevID, function () {
        me.page('previous');
    });

    me.nextPage = function () {
        me.currentPage += 1;
        me.populate();
    };
    me.previousPage = function () {
        me.currentPage -= 1;
        me.populate();
    };

    me.page = function (which) {
        me.newTime = new Date().getTime();
        if (me.newTime - me.typingTime > me.waitInterval) {
            if (which == 'next') {
                me.nextPage();
            } else {
                me.previousPage();
            };
        } else {
            clearTimeout(me.updateInterval);
            me.updateInterval = setTimeout(function () {
                if (which == 'next') {
                    me.nextPage();
                } else {
                    me.previousPage();
                };
            }, me.waitInterval);
        };
        me.typingTime = me.newTime;
    };

    me.populate = function (pageNumber) {
        me.querying = true;
        if (pageNumber != 'undefined' && $.isNumeric(pageNumber) && parseInt(pageNumber) >= 0) {
            me.currentPage = pageNumber;
        };
        var lbound = (me.currentPage * me.PerPage) + 1;
        var ubound = ((me.currentPage + 1) * me.PerPage);
        me.Next$.attr('disabled', 'disabled');
        me.Prev$.attr('disabled', 'disabled');
        if (me.sourceType == 'function') {
            if (me.getExtraParams != null) {
                me.deferredAction(me.getPageData(lbound, ubound, me.getExtraParams()), lbound, ubound);
            } else {
                me.deferredAction(me.getPageData(lbound, ubound), lbound, ubound);
            };
        } else {
            if (me.getExtraParams != null) {
                me.getPageData(lbound, ubound, me.getExtraParams()).done(function (result) {
                    me.deferredAction(result, lbound, ubound);
                });
            } else {
                me.getPageData(lbound, ubound).done(function (result) {
                    me.deferredAction(result, lbound, ubound);
                });
            };
        };
    };

    me.deferredAction = function (result, lbound, ubound) {
        me.currentData = result;
        me.querying = false;
        if (me.currentPage > 0) {
            me.Prev$.removeAttr('disabled');
        };
        if (result.filteredRecords - ubound > 0) {
            me.Next$.removeAttr('disabled');
        };
        if (result.filteredRecords >= 1) {
            var showStr = 'Showing ' + (lbound) + ' to ' + (ubound > result.filteredRecords ? result.filteredRecords : ubound) + ' of ' + (result.filteredRecords) + ' records';
            showStr += (result.filteredRecords != result.numRecords ? ' (filtered from ' + result.numRecords + '.)' : '.');
            me.PageID$.html(showStr);
        } else {
            me.PageID$.html('No matching records found.' + (result.numRecords > 0 ? ' (filtered from ' + result.numRecords + ')' : ''));
        };

        me.ID$.html('');
        for (var x = 0; x < result.pages.length; x++) {
            var r = me.RowFunction(result.pages[x]);
            $(r).addClass('multiline-striped').appendTo(me.ID$);
        };
        me.ID$.find('[data-toggle="tooltip"]').tooltip();
        me.processExtraData(result);
        // scroll to the top of the div if there is a container to scroll
        if (me.ID$.innerHeight() < me.ID$[0].scrollHeight) {
            me.ID$.animate({ scrollTop: me.ID$.children(me.RowSelector + ':first').position().top }, 'slow');
        };
    };

    me.numRows = function () {
        return me.ID$.children(me.RowSelector).length;
    };

    me.init = function () {
        if ($(me.perPageSelector).length > 0) {
            $(document.body).on('change', me.perPageSelector, function () {
                if ($.isNumeric(this.value) && parseInt(this.value) > 0) {
                    me.PerPage = this.value;
                };
                me.populate(0);
            });
        };
        if (me.Next$.length <= 0) {
            me.Next$ = $('<button></button>');
            console.log('Next button ID not provided or not found.');
        };
        if (me.Prev$.length <= 0) {
            me.Prev$ = $('<button></button>');
            console.log('Previous button ID not provided or not found.');
        };
        if (me.PageID$.length <= 0) {
            me.PageID$ = $('<label></label>');
            console.log('Page ID label not provided or not found.');
        };
        if (me.waitInterval < 100  || !$.isNumeric(me.waitInterval)) {
            me.waitInterval = 100;
        };
    };
    return me;
};