// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var locAssHub = $.connection.locationAssignmentHub;


function getCountTableData() {
    locAssHub.server.getCountTableData().done(function (dataSet) {
        leftCountTable.clear();
        leftCountTable.rows.add(dataSet).draw();
        rightCountTable.clear().draw();
        refreshCountVals();
    });
};

function getPickTableData() {
    locAssHub.server.getPickTableData($('#OrderNumberSearch').val()).done(function (dataSet) {
        leftPickTable.clear();
        leftPickTable.rows.add(dataSet).draw();
        rightPickTable.clear().draw();
        refreshCountVals();
    });
};

function getPutAwayTableData() {
    locAssHub.server.getPutAwayTableData().done(function (dataSet) {
        leftPutAwayTable.clear();
        leftPutAwayTable.rows.add(dataSet).draw();
        rightPutAwayTable.clear().draw();
        refreshCountVals();
    });
};

function refreshCountVals() {
    locAssHub.server.getLocAssTransTypeCount().done(function (countdata) {
        for (i = 0; i < countdata.length; i++) {
            console.log(countdata);
            if (countdata[i].Type == "Count") {
                var num = countdata[i].Count.toString();
                document.getElementById("LocAssCount").innerHTML = 'Count <span class="label label-default">' + num + '</span>';
            } else if (countdata[i].Type == "Pick") {
                var num = countdata[i].Count.toString();
                document.getElementById("LocAssPick").innerHTML = 'Pick <span class="label label-default">' + num + '</span>';
            } else {
                var num = countdata[i].Count.toString();
                document.getElementById("LocAssPutAway").innerHTML = 'Put Away <span class="label label-default">' + num + '</span>';
            }
        }
    });
};

function InsertOrdersForProcess(TransType, Orders) {

    MessageModal('Mark Selected Orders for ' + TransType.toUpperCase() + ' Location Assignment?', 'Do you want to mark these orders for location assignment?', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; },
                    function () {
                        locAssHub.server.insertOrdersForLocAss(TransType, Orders).done(function (rslt) {
                            if (rslt) {
                                switch (TransType.toLowerCase()) {
                                    case 'count':
                                        getCountTableData();
                                        break;
                                    case 'putaway':
                                        getPutAwayTableData();
                                        break;
                                    case 'pick':
                                        getPickTableData();
                                        break;
                                };
                            } else {
                                MessageModal('Error', 'There was an error marking these orders for location assignment', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
                            };
                        });
                    }
                );
};