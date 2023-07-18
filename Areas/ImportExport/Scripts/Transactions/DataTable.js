// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var dt = {}, tableFilters = {};
$(document).ready(function () {
    function getFilterString(selectedTable) {
        if (tableFilters.hasOwnProperty(selectedTable) && tableFilters[selectedTable] != undefined) {
            return tableFilters[selectedTable].getFilterString();
        } else {
            return '1=1';
        };
    };

    function buildColumnMap(table) {
        var map = {};
        var tds = $('[data-table-name="' + table + '"]').find('thead').children('tr').children();
        for (var x = 0; x < tds.length; x++) {
            var $t = $(tds[x]);
            map[$t.html()] = $t.data('field-type');
        };
        return map;
    };

    var initialized = false;
    var tables = $('[data-table-name]');
    for (var x = 0; x < tables.length; x++) {
        var $t = $(tables[x]);
        var tableName = $t.data('table-name');

        dt[tableName] = $t.DataTable({
            dom: 'trip',
            processing: true,
            serverSide: true,
            order: [
                [
                    0, 'desc'
                ]
            ],
            ajax: {
                url: "/IETransactions/GetIETransactionsTable",
                data: function (d) {
                    var selectedTable = initialized ? $('#TableSelect').val() : tableName;
                    var sDate = new Date($('#SDate').val());
                    var eDate = new Date($('#EDate').val());
                    var min = new Date('1/1/1900'), max = new Date('12/31/9999');
                    var mkDateStr = function (dte) {
                        return String(dte.getMonth() + 1) + '/' + String(dte.getDate()) + '/' + String(dte.getFullYear());
                    };
                    if (new Date(sDate) < min) sDate = min;
                    if (new Date(eDate) > max) eDate = max;
                    if (new Date(sDate) > new Date(eDate)) sDate = min;
                    $('#SDate').val(mkDateStr(sDate));
                    $('#EDate').val(mkDateStr(eDate));
                    d.DictionaryData = {
                        'Table': selectedTable,
                        'Start Date': mkDateStr(sDate),
                        'End Date': mkDateStr(eDate),
                        'Plugin Where': getFilterString(selectedTable)
                    };
                }
            },
            language: {
                paginate: {
                    next: "<u>N</u>ext",
                    previous: "<u>P</u>revious"
                }
            }
        });

        tableFilters[tableName] = new FilterMenuTable({
            Selector: '[data-table-name="' + tableName + '"] tbody',
            columnIndexes: function () {
                var indicies = new Array();
                var tds = $t.children('thead').children('tr').children();
                for (var k = 0; k < tds.length; k++) {
                    indicies.push($(tds[k]).html());
                };
                return indicies;
            }(),
            columnMap: buildColumnMap(tableName),
            dataTable: dt[tableName]
        });
    };
    initialized = true;
    tables.wrap('<div style="overflow-x:scroll;"></div>');
});