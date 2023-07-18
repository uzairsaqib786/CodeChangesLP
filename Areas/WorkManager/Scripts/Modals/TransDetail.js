// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var tdTable;

$(document).ready(function () {
    var tdFilter;
    tdFilter = new FilterMenuTable({
        Selector: '#TransDetailTable tbody',
        columnIndexes: [
            'Transaction Type', 'Batch Pick ID', 'Order Number', 'Line Number', 'Item Number', 'Location', 'Description', 'Required Date', 'Priority', 'Unit of Measure', 'Lot Number', 'Expiration Date', 
			'Serial Number', 'Revision', 'Transaction Quantity', 'Tote ID', 'Tote Number', 'Warehouse', 'Zone', 'Carousel', 'Row', 'Shelf', 'Bin', 'Inv Map ID', 'Notes', 'Label', 'User Field1', 
			'User Field2', 'User Field3', 'User Field4', 'User Field5', 'User Field6', 'User Field7', 'User Field8', 'User Field9', 'User Field10', 'Cell', 'Host Transaction ID', 'Emergency'
        ],
        columnMap: function () {
            var colMap = [];
            var defs = {
                ints: {
                    cols: ['Line Number', 'Priority', 'Transaction Quantity', 'Tote Number', 'Inv Map ID'],
                    val: 'Number'
                },
                bools: {
                    cols: ['Emergency'],
                    val: 'Bool'
                },
                texts: {
                    cols: ['Transaction Type', 'Batch Pick ID', 'Order Number', 'Item Number', 'Location', 'Description', 'Unit of Measure', 'Lot Number', 'Serial Number', 'Revision', 'Tote ID',
                        'Warehouse', 'Zone', 'Carousel', 'Row', 'Shelf', 'Bin', 'Notes', 'Label', 'User Field1', 'User Field2', 'User Field3', 'User Field4', 'User Field5', 'User Field6', 'User Field7',
                        'User Field8', 'User Field9', 'User Field10', 'Cell', 'Host Transaction ID'],
                    val: 'Text'
                },
                dates: {
                    cols: ['Required Date', 'Expiration Date'],
                    val: 'Date'
                }
            };
            for (var prop in defs) {
                for (var x = 0; x < defs[prop].cols.length; x++) {
                    colMap[defs[prop].cols[x]] = defs[prop].val;
                };
            };
            
            return colMap;
        }(),
        dataTable: tdTable,
        ignoreColumns: []
    });

    tdTable = $('#TransDetailTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [
            [0, 'asc']
        ],
        "ajax": {
            //Function that grabs Table Data
            "url": "/WM/SelectWork/GetTransDetailTable",
            "data": function (d) {
                var launchData = $('#TransDetailTable').data('launch');
                if (typeof launchData == 'undefined' || launchData == 'undefined') {
                    launchData = {
                        transType: '',
                        batchOrderTote: '',
                        botValue: '',
                        selectedUser: ''
                    };
                };
                d.entryFilter = tdFilter.getFilterString();
                d.transType = launchData.transType;
                d.batchid = launchData.batchOrderTote;
                d.ordernum = launchData.botValue;
                d.users = launchData.selectedUser;
            }
        },
        'paging': true,
        pageLength: 10
    });

    $('#TransDetailTable').wrap('<div id="td_wrapper" style="overflow-x:scroll;"></div>').on('filterChange', function () {
        tdTable.draw();
    });
});

function launchDetailModal(transType, batchOrderTote, botValue, user) {
    $('#TransDetailTable').data('launch', {
        transType: transType,
        batchOrderTote: batchOrderTote,
        botValue: botValue,
        selectedUser: user
    });
    $('#TransDetail_Modal').modal('show');
    tdTable.draw();
};