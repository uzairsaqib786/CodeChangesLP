// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*
***********************
Filter Menu Plugin for Tables
***********************
This plugin allows you to filter results being displayed in a Table

The plugin Initializer uses 3 mandatory parameter and 2 optional parameters
Paramteres:
-Selector: Selector for the table you want to filter
-columnIndexes: Array of of column names used in your table in order
-columnMap: Object that Maps column names to their DataType(Text,Date,Number,Bool)
(Optional)
-dataTable: Datatable reference if you are using a Ajax-sourced datatable(If not provided, and filterChange event will be raised when a new filter is added)
(Optional)
-ignoreColumns: 0-based index of columns you do not want to be able to filter on(Alternatively, you can add the filter-ignore class to the table element you do not want to filter)
Example:
var TableFilter = new FilterMenuTable({
        Selector: '#TableID',
        columnIndexes: ["Column1","Column2","Column3","Column4"],
        columnMap: {"Column1":"Text","Column2":"Number","Column3":"Date","Column4":"Bool"},
        dataTable: dataTableReference,
        ignoreColumns: [0,1,2]
    })

If your table is an Ajax-Sourced Datatable, it will automatically refresh the table with the new data

If not an Ajax-Sourced Table, the filterChange event will be raised to allow you to re-query data as you see fit
Example:
$('#TableID').on('filterChange',function(){
    //Do something here when a filter changes
})
*/
function FilterMenuTable(params) {
    var me = this;

    this.selector = params.Selector

    this.filterString = ""

    this.getFilterString = function () {
        return me.filterString =="" ? "1=1" : me.filterString
    }

    this.clearFilter = function () {
        me.filterString = ""
        if (me.isDT) {
            me.DTRef.draw();
        } else {
            $(me.selector).trigger("filterChange")
        }
    }

    this.columnIndexes = params.columnIndexes

    this.columnMap = params.columnMap;

    this.ignoreColumns = function () {
        var cols = params.ignoreColumns
        var returnString = ""
        if (cols !== undefined) {
            for (var x = 0; x < cols.length; x++) {
                returnString += ':not(:nth-child(' + (cols[x] + 1) + '))'
            }
        }
       return returnString
    }()

    if (params.dataTable !== undefined) {
        this.isDT = true
        this.DTRef = params.dataTable
    } else {
        this.isDT = false
    }

    $(document.body).contextMenu({
        selector: params.Selector + ' tr td'+me.ignoreColumns+ ':not(.filter-ignore)',
        build: function ($trigger, e) {
            var colData = me.getColData($trigger)
            var dataType = colData.dataType;
            var menuItems = {
                "=": { name: "Equals" },
                "<>": { name: "Not Equals" }
            }
            if (dataType == 'Text') {
                menuItems['like'] = { name: "Like" }
                menuItems['not like'] = { name: "Not Like" }
                menuItems['Input filters'] = {
                    name: "Input Filters",
                    items: {
                        Equals: { name: "Equals" },
                        NotEquals: { name: "Not Equals" },
                        Begins: { name: "Begins" },
                        NotBegins: { name: "Does Not Begin" } ,
                        Ends: {name: "Ends With"},
                        NotEnds: {name: "Does Not End With"},
                        Contains: { name: "Contains" },
                        NotContains: {name:"Does Not Contain"}
                    }
                }
            } else if (dataType == 'Number') {
                menuItems['>='] = { name: "Greater than or Equal" }
                menuItems['<='] = { name: "Less than or Equal" }
                menuItems['Input filters'] = {
                    name: "Input Filters",
                    items: {
                        Equals: { name: "Equals" },
                        NotEquals: { name: "Not Equals" },
                        LessThan: { name: "Less Than" },
                        GreaterThan: { name: "Greater Than" },
                        Between: { name: "Between" }
                    }
                }
            } else if(dataType == 'Date'){
                menuItems['>'] = { name: "Greater than" }
                menuItems['<'] = { name: "Less than" }
                menuItems['Input filters'] = {
                    name: "Input Filters",
                    items: {
                        Equals: { name: "Equals" },
                        NotEquals: { name: "Not Equals" },
                        LessThan: { name: "Less Than" },
                        GreaterThan: { name: "Greater Than" },
                        Between: { name: "Between" }
                    }
                }
            }
            menuItems['clear'] = {name: "Clear Filters"}
            return {
                callback: function (key, options) {
                    var curEvent = window.event
                    if (curEvent.which != 3) {
                        if (key == 'clear') {
                            me.clearFilter()
                        } else {
                            me.SetFilter(key, options, this);
                        }
                        
                    }
                },
                items: menuItems
            }
        }
    })

    this.getColData = function ($element) {
        var index = $element.closest('td').index();
        var colName = me.columnIndexes[index];
        var colType = me.columnMap[colName];
        return {colName: colName, dataType: colType}
    }

    this.SetFilter = function (key, options, element) {
        var filterType = key;
        var $element = $(element);
        var colData = me.getColData($element)
        var colName = colData.colName;
        var dataType = colData.dataType;
       

        var value = $element.is('input') ? $element.val() : $element.text()
        var value2 = undefined;

        if (value.trim()=="") {
            value = $element.find('input').val();
        }

        switch (filterType) {
            case "Equals":
                filterType="="
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
        if (value !== null) {
            me.addFilter(colName, dataType, filterType, key, value, value2)
            if (me.isDT) {
                me.DTRef.draw();
            } else {
                $(me.selector).trigger("filterChange")
            }
        }
    }

    this.addFilter = function (colName, dataType, key, filterType, value1, value2) {
        if (dataType == 'Text' || dataType == 'Date') {
            if (filterType=="like" || filterType=="not like" || filterType == "Contains" || filterType == "NotContains") {
                value1 = "%" + value1 + "%"
            } else if(filterType == "Ends" || filterType == "NotEnds") {
                value1 = "%" + value1
            } else if (filterType == "Begins" || filterType == "NotBegins") {
                value1 = value1 + "%"
            }
            value1 = "'" + value1 + "'"
            if (value2 !== undefined) {
                value2 = "'" + value2 + "'"
            }
        } else if (dataType == 'Bool') {
            value1 = (value1 == 'True' ? '1' : '0')
        }
        if (value1.toLowerCase() === "'null'" || (value1 === "''" && key != '<>')) {
            var filterString = "ISNULL([" + colName + "],'') = ''"
        } else {
            if (dataType == 'Text') {
                var filterString = "ISNULL([" + colName + "],'') " + key + " " + value1 + (key == 'Between' ? (" and " + value2) : "")
            } else {
                var filterString = "[" + colName + "] " + key + " " + value1 + (key == 'Between' ? (" and " + value2) : "")
            }
           
        }
        if (value2 === undefined) {
            me.filterString += (me.filterString == "" ? filterString : (" AND " + filterString))
        } else {
            me.filterString += (me.filterString == "" ? filterString : (" AND " + filterString))
        }
        //alert(me.filterString)
    }
}


/*$(document.body).contextMenu({
    selector: '#showTest',
    callback: function (key, options) {
        alert(key)
    },
    items: {
        "=": { name: "Equals" },
        "<>": { name: "Not Equals" },
        "like": { name: "Like" },
        "not like": { name: "Not Like" }
    }
})*/