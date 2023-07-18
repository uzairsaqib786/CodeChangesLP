// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*
***********************
Filter Menu Plugin for Non-Table Pages
***********************
This plugin allows you to filter results being displayed on a page the rely on individual inputs/text elements

The plugin Initializer uses 1 mandatory parameter and 4 optional parameters
Paramteres:
-Selector: The selector you want the filterChange event raised on
(Optional)
-NumberSelector: The selector used to show the Number Filter Menu
(Optional)
-TextSelector: The selector used to show the Text Filter Menu
(Optional)
-DateSelector: The selector used to show the Date Filter Menu
(Optional)
-BoolSelector: The selector used to show the Boolean Filter Menu
Example:
var FilterPlugin = new FilterMenu({
        Selector: '#RefreshData',
        NumberSelector: '.num',
        DateSelector: '.date',
        TextSelector: '.text',
        BoolSelector: '.bool'
    })

Any Element you wish to filter on needs data-colname attribute which names the column you are filtering on
Example:
<input type='text' class='num' data-colname='SomeTableColumn'/>

To Refresh your data when a filter has changed, listen for a filterChange event
Example:
$('#RefreshData').on('filterChange',function(){
    //Do something here when a filter changes
})
*/
function FilterMenu(params) {
    var me = this;

    this.selector = params.Selector

    this.filterString = ""

    this.getFilterString = function () {
        return me.filterString == "" ? "1=1" : me.filterString
    }

    this.clearFilter = function () {
        me.filterString = ""
        $(me.selector).trigger("filterChange")
    }

    this.evalShowMenuCondition = params.ShowFilterCondition

    if (params.NumberSelector !== undefined) {
        $(document.body).contextMenu({
            selector: params.NumberSelector,
            callback: function (key, options) {
                var curEvent = window.event
                if (curEvent.which != 3) {
                    if (key == 'clear') {
                        me.clearFilter()
                    } else {
                        me.SetFilter(key, options, 'Number', this);
                    }

                }
            },
            items: {
                "=": { name: "Equals" },
                "<>": { name: "Not Equals" },
                ">=": { name: "Greater than or Equal" },
                "<=": { name: "Less Than or Equals" },
                "Input filters": {
                    name: "Input Filters",
                    items: {
                        Equals: { name: "Equals" },
                        NotEquals: { name: "Not Equals" },
                        LessThan: { name: "Less Than" },
                        GreaterThan: { name: "Greater Than" },
                        Between: { name: "Between" }
                    }
                },
                'clear': { name: "Clear Filters" }
            },
            events: {
                show: checkShowMenu
            }
        })
    }
    if (params.TextSelector !== undefined) {
        $(document.body).contextMenu({
            selector: params.TextSelector,
            callback: function (key, options) {
                var curEvent = window.event
                if (curEvent.which != 3) {
                    if (key == 'clear') {
                        me.clearFilter()
                    } else {
                        me.SetFilter(key, options, 'Text', this);
                    }

                }
            },
            items: {
                "=": { name: "Equals" },
                "<>": { name: "Not Equals" },
                "like": { name: "Like" },
                "not like": { name: "Not Like" },
                "Input filters": {
                    name: "Input Filters",
                    items: {
                        Equals: { name: "Equals" },
                        NotEquals: { name: "Not Equals" },
                        Begins: { name: "Begins" },
                        NotBegins: { name: "Does not Begin" },
                        Ends: { name: "Ends with" },
                        NotEnds: { name: "Does not End with" },
                        Contains: { name: "Contains" },
                        NotContains: { name: "Does not Contain" }
                    }
                },
                'clear': { name: "Clear Filters" }
            },
            events: {
                show: checkShowMenu
            }
        })
    }
    if (params.DateSelector !== undefined) {
        $(document.body).contextMenu({
            selector: params.DateSelector,
            callback: function (key, options) {
                var curEvent = window.event
                if (curEvent.which != 3) {
                    if (key == 'clear') {
                        me.clearFilter()
                    } else {
                        me.SetFilter(key, options, 'Date', this);
                    }

                }
            },
            items: {
                "=": { name: "Equals" },
                "<>": { name: "Not Equals" },
                ">": { name: "After" },
                "<": { name: "Before" },
                "Input filters": {
                    name: "Input Filters",
                    items: {
                        Equals: { name: "Equals" },
                        NotEquals: { name: "Not Equals" },
                        LessThan: { name: "Less Than" },
                        GreaterThan: { name: "Greater Than" },
                        Between: { name: "Between" }
                    }
                },
                'clear': { name: "Clear Filters" }
            },
            events: {
                show: checkShowMenu
            }

        })
    }
    if (params.BoolSelector !== undefined) {
        $(document.body).contextMenu({
            selector: params.BoolSelector,
            callback: function (key, options) {
                var curEvent = window.event
                if (curEvent.which != 3) {
                    if (key == 'clear') {
                        me.clearFilter()
                    } else {
                        me.SetFilter(key, options, 'Bool', this);
                    }
                }
            },
            items: {
                "=": { name: "Equals" },
                "<>": { name: "Not Equals" },
                'clear': { name: "Clear Filters" }
            },
            events: {
                show: checkShowMenu
            }
        })
    }

    function checkShowMenu(options) {
        if (me.evalShowMenuCondition != undefined) {
            return me.evalShowMenuCondition()
        }
        return true;
    }

    this.SetFilter = function (key, options, dataType, element) {
        var filterType = key;
        var $element = $(element);
        var value = ($element.is('input') || $element.is('select')) ? $element.val() : $element.text()
        if ($element.is(':checkbox')) {
            value = $element.prop('checked')
        }
        var value2 = undefined;
        var colName = $element.data('colname')
        switch (filterType) {
            case "Equals":
                filterType = "="
                value = prompt(colName + " is equal to (mm/dd/yy hh:MM:ss for dates)", undefined)
                break;
            case "NotEquals":
                filterType = "<>"
                value = prompt(colName + " is not equal to (mm/dd/yy hh:MM:ss for dates)", undefined)
                break;
            case "Contains":
                filterType = "like"
                value = prompt(colName + " contains", undefined)
                break;
            case "NotContains":
                filterType = "not like"
                value = prompt(colName + " does not contain", undefined)
                break;
            case "LessThan":
                filterType = "<="
                value = prompt(colName + " is less than (mm/dd/yy hh:MM:ss for dates)", undefined)
                break;
            case "GreaterThan":
                filterType = ">="
                value = prompt(colName + " is greater than (mm/dd/yy hh:MM:ss for dates)", undefined)
                break;
            case "Begins":
                filterType = "like"
                value = prompt(colName + " begins with", undefined)
                break;
            case "NotBegins":
                filterType = "not like"
                value = prompt(colName + " does not begin with", undefined)
                break;
            case "Ends":
                filterType = "like"
                value = prompt(colName + " ends with", undefined)
                break;
            case "NotEnds":
                filterType = "not like"
                value = prompt(colName + " does not end with", undefined)
                break;
            case "Between":
                filterType = "Between"
                value = prompt(colName + " Start Value (mm/dd/yy hh:MM:ss for dates)", undefined)
                value2 = prompt(colName + " End Value (mm/dd/yy hh:MM:ss for dates)", undefined)
                if (value !== null && value2 === null) {
                    MessageModal("Table Filter", "You must supply a start and end Date")
                    return;
                }
                break;
        }
        me.addFilter($element.data('colname'), dataType, filterType, key, value, value2)
        $(me.selector).trigger("filterChange")
    }

    this.addFilter = function (colName, dataType, key, filterType, value1, value2) {
        if (dataType == 'Text' || dataType == 'Date') {
            if (filterType == "like" || filterType == "not like" || filterType == "Contains" || filterType == "NotContains") {
                value1 = "%" + value1 + "%"
            } else if (filterType == "Ends" || filterType == "NotEnds") {
                value1 = "%" + value1
            } else if (filterType == "Begins" || filterType == "NotBegins") {
                value1 = value1 + "%"
            }
            value1 = "'" + value1 + "'"
            if (value2 !== undefined) {
                value2 = "'" + value2 + "'"
            }
        } else if (dataType == 'Bool') {
            value1 = (String(value1).toLowerCase() == 'true' ? '1' : '0')
        }
        if (value1.toLowerCase() === "'null'" || value1 === "''") {
            var filterString = "ISNULL(" + colName + ",'') = ''"
        } else {
            var filterString =  colName + " " + key + " " + value1 + (key == 'Between' ? (" and " + value2) : "")
        }
        if (value2 === undefined) {
            me.filterString += (me.filterString == "" ? filterString : (" AND " + filterString))
        } else {
            me.filterString += (me.filterString == "" ? filterString : (" AND " + filterString))
        }
        console.log(me.filterString);
    }
}