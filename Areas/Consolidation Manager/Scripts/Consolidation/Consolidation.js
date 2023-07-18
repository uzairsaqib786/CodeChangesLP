// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var consolidationHub = $.connection.cMConsolidationHub;


var leftVerTable, rightVerTable, ToteTable, packListSort

$(document).ready(function () {
    packListSort = $('#PackListSort').val();

    function getBackOverride() {
        return 'override_back=' + encodeURIComponent("location.href='/CM/Consolidation?ordernumber=" + $('#TypeValue').val() + "'");
    };
    $('#HomePage').show();
    $('#PackConf').hide();
    $('#ShipTrans').hide();
    $('#preview').html("");

    var FilterType = ""

    var startSelectFilter = $('#SelectFilter').val();
    if (startSelectFilter == 2) {
        startSelectFilter = 1;
    } else {
        startSelectFilter = 2;
    }
    var sortBy = $('#SelectFilter').val()

    //initialize datatable
    leftVerTable = $("#UnVerifiedTable").DataTable({
        "aaSorting": [[sortBy, 'asc'], [3, 'asc']],
        "dom": 'trip',
        'columnDefs': [
            {
                'targets': [0, startSelectFilter, 4, 10],
                'visible': false
            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "lengthMenu": [20],
        "createdRow": function (row, data, index) {
            if ((data[7] == "Line Completed")) {
                $(row).addClass("success")
            } else if ((data[7] == "Waiting Reprocess")) {
                $(row).addClass("info");
                verifyLine(row);
            }
        }

    });
    //initialize datatable
    rightVerTable = $('#VerifiedTable').DataTable({
        "dom": 'trip',
        "processing": true,
        'columnDefs': [
            {
                'targets': [0, 4, 7, 10],
                'visible': false
            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "lengthMenu": [20],
        "createdRow": function (row, data, index) {
            if ((data[7] == "Line Completed")) {
                $(row).addClass("success");
            }
            else if ((data[7] == "Waiting Reprocess")) {
                $(row).addClass("info");
            }
        }

    });
    //initialize datatable
    ToteTable = $('#ToteIDInfoTable').DataTable({
        "dom": 'trp',
        "processing": true,
        'columnDefs': [
            {
                'targets': ["all"],
                'visible': false
            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "paging": false

    });
    //allows table to wrap inside of panel
    $('#ToteIDInfoTable').wrap("<div style='overflow-y:scroll;height:200px;background-color:white;border:1px #ddd solid'></dov>")

    $('#UnVerifiedTable tbody').on('click', 'tr', function () {
        verifyLine(this);
        //checks ot see if all lines have been verifed after the table row is removed
        setTimeout(function () {
            if (leftVerTable.rows().data().length == 0) {
                MessageModal("Alert", "You have consolidated all items in this order");
            };
        }, 100);
    });

    $('#UnVerifiedTable').wrap('<div style="overflow-x:scroll;"></div>');

    $('#VerifiedTable tbody').on('click', 'tr', function () {
        unVerifyLine(this);
    });
    $('#VerifiedTable').wrap('<div style="overflow-x:scroll;"></div>');

    $('#VerifyAll').click(function () {
        VerifyAll();

        setTimeout(function () {
            if (leftVerTable.rows().data().length == 0) {
                MessageModal("Alert", "You have consolidated all items in this order");
            };
        }, 100);
    });

    $('#UnVerifyAll').click(function () {
        unVerifyAll();
    });



    $('#TypeValue').on("keydown", function (e) {
        var value = $('#TypeValue').val();
        if (e.which == 13) {
            getTableData("", value);
        };
    });

    $('#ShippingButt').click(function () {
        //go to ship page here
        location.href = "/CM/Shipping?" + getBackOverride() + "&ordernumber=" + $('#TypeValue').val();
    })

    $('#NextOrder').click(function () {
        clearPageData();
        $('#TypeValue').focus();
        disableConButts();
    });

    $('#SelectFilter').change(function () {
        if ($('#SelectFilter').val() == "1") {
            //hide supplier item id
            leftVerTable.column(1).visible(true);
            leftVerTable.column(2).visible(false);
            itemSelectedTable.column(1).visible(true);
            itemSelectedTable.column(2).visible(false);
        } else if ($('#SelectFilter').val() == "2") {
            //hide item number
            leftVerTable.column(1).visible(false);
            leftVerTable.column(2).visible(true);
            itemSelectedTable.column(1).visible(false);
            itemSelectedTable.column(2).visible(true);
        } else {
            //hide supplier item id
            leftVerTable.column(1).visible(true);
            leftVerTable.column(2).visible(false);
            itemSelectedTable.column(1).visible(true);
            itemSelectedTable.column(2).visible(false);
        }
    });

    $('#PackingButt').click(function () {
        consolidationHub.server.selectConfirmPackingPref().done(function (pref) {
            if (pref) {
                //add page load stuff for confirm and packing
                location.href = "/CM/ConfirmAndPacking?" + getBackOverride() + "&ordernumber=" + $('#TypeValue').val();
            } else {
                //add page load stuff for shipping transactions
                location.href = "/CM/ShippingTransactions?" + getBackOverride() + "&ordernumber=" + $('#TypeValue').val();
            }
        });
    });

    function clearPageData() {
        leftVerTable.clear().draw();
        rightVerTable.clear().draw();
        $('#OpenCount, #CompletedCount, #BackOrderCount').text('0');
        $('#TypeValue').val('');
        ToteTable.clear().draw();
    }

    function getTableData(type, value) {
        var curValue = value;
        consolidationHub.server.getConsolidationData(type, value).done(function (tableData) {
            if (typeof tableData == 'string') {
                switch (tableData) {
                    case "DNE":
                        MessageModal("Consolidation", "The Order/Tote that you entered is invalid or no longer exists in the system.", function () {
                            $('#TypeValue').focus();
                        })
                        clearPageData();
                        break;
                    case "Conflict":
                        ShowOrderToteConflictModal(curValue, getTableData)
                        //MessageModal("Consolidation", "The Value you Entered matched a Tote and Order Number, select one to Continue")
                        break;
                    case "Error":
                        MessageModal("Consolidation Error", "An Error occured while retrieving data")
                        break;
                }
            }
            else {
                leftVerTable.clear();
                leftVerTable.rows.add(tableData.leftTable).draw();
                rightVerTable.clear();
                rightVerTable.rows.add(tableData.rightTable).draw();
                $('#OpenCount').text(tableData.openLines);
                $('#CompletedCount').text(tableData.completedLines)
                $('#BackOrderCount').text(tableData.reprocLines)
                ToteTable.clear();
                ToteTable.rows.add(tableData.toteTable).draw();
                $('#TypeValue').val(tableData.OrderNumber)
                var value = $('#TypeValue').val();
                consolidationHub.server.setShippingButt(value).done(function (set) {
                    if (set == 1) {
                        enableConButts();
                        $('#ShippingButt').removeAttr('disabled');
                    } else if (set == 0) {
                        enableConButts();
                        $('#ShippingButt').attr('disabled', 'disabled');
                    } else {
                        MessageModal("Error", "Error has occured");
                    };
                });
            }
        });
    };

    //Verify items Typeahead
    var lookUp = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getbeginToLocation
            url: ('/CM/Consolidation/consolItemTA?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#FilterValue').val() + '&col=' + $('#SelectFilter').val() + '&orderNum=' + $('#TypeValue').val();
            },
            cache: false
        }
    });

    lookUp.initialize();
    //Verify Items Typeahead
    $('#FilterValue').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "lookUp",
        displayKey: 'value',
        source: lookUp.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header " style="width:34%;">Value</p><p class="typeahead-header " style="width:33%;">Line Number</p><p class="typeahead-header " style="width:33%;">Line Status</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:34%;">{{value}}</p><p class="typeahead-row " style="width:33%;">{{lineNum}}</p><p class="typeahead-row " style="width:33%;">{{lineStatus}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        //on select move everything with selected thing to right table
        CheckDuplicatesForVerify(data.value)
        lookUp.clearRemoteCache();
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', '450px');
    });

    $('#FilterValue').on("keydown", function (e) {
        if (e.which == 13) {
            CheckDuplicatesForVerify($('#FilterValue').val());
            lookUp.clearRemoteCache();
        }
    });

    //handles moving a record bewteen the left and right tables
    function verifyLine(curRow) {
        var left = leftVerTable.row(curRow).data();
        var id = left[0];
        if (left[7] == "Not Completed" || left[7] == "Not Assigned") {
            MessageModal("Error", "The selected item has not yet been completed and can't be verified at this time");
        } else {
            consolidationHub.server.verifyItem(id).done(function (mess) {
                if (mess == "Fail") {
                    MessageModal("Error", "Error has occured");
                } else {
                    leftVerTable.row(curRow).remove().draw();
                    rightVerTable.row.add(left).draw();
                };
            });
        };
    };

    //handles moving a record between the right and left tables
    function unVerifyLine(curRow) {
        var right = rightVerTable.row(curRow).data();
        var id = right[0];
        consolidationHub.server.deleteVerifiedItem(id).done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "Error has occured");
            } else {
                rightVerTable.rows(curRow).remove().draw();
                leftVerTable.row.add(right).draw();
            };
        });
    };
    //handles verifying all records in the left table
    function VerifyAll() {
        var IDS = [];
        var rows = leftVerTable.rows().data();
        for (var x = 0; x < rows.length; x++) {
            if (leftVerTable.row(x).data()[7] != "Not Completed" && leftVerTable.row(x).data()[7] != "Not Assigned") {
                IDS.push(leftVerTable.row(x).data()[0]);
            };
        };
        consolidationHub.server.verifyAll(IDS).done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "Error has occured");
            } else {
                var z = [];
                for (var x = 0; x < rows.length; x++) {
                    if (IDS.indexOf(leftVerTable.row(x).data()[0]) != -1) {
                        var left = leftVerTable.row(x).data();
                        z.push(leftVerTable.row(x).index());
                        rightVerTable.row.add(left).draw();
                    };
                };
                leftVerTable.row(z).remove().draw();
            };
        });
    };

    //handles unverifying all records in the right table
    function unVerifyAll() {
        var IDS = [];
        var rows = rightVerTable.rows().data();
        for (var x = 0; x < rows.length; x++) {
            IDS.push(rightVerTable.row(x).data()[0]);
        };
        consolidationHub.server.unVerifyAll(IDS).done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "Error has occured");
            } else {
                leftVerTable.rows.add(rightVerTable.data()).draw();
                rightVerTable.clear().draw();
            };
        });
    }

    //When Scanning in an Item Number/Serial Code/Any Code checks to see if it exists for current table
    function CheckDuplicatesForVerify(val) {
        var columnIndex = $('#SelectFilter').val();
        var result;
        //0 = Any Code, loops over all codes from select box and finds first valid on in order
        if (columnIndex == 0) {
            var options = $('#SelectFilter option');
            options.each(function () {
                result = checkVerifyType($(this).val(), val)
                if (result.valueCount >= 1 || $(this).val() == 0) {
                    return false;
                }
            })
        } else {
            result = checkVerifyType(columnIndex, val)
        }
        if (result.valueCount > 1 && $('#VerifyItems').val() == "No" && $('#BlindVerify').val() == "No") {
            $('#ItemSelectedModal').modal('show');
            $('#IdentModal').val($('#TypeValue').val());
            $('#ColLabel').text($('#SelectFilter  option[value=' + $("#SelectFilter").val() + ']').text());
            $('#ColumnModal').val(val);
            consolidationHub.server.getItemSelectedData($('#TypeValue').val(), $('#SelectFilter  option[value=' + $("#SelectFilter").val() + ']').text(), val).done(function (dataset) {
                console.log(dataset);
                itemSelectedTable.clear();
                itemSelectedTable.rows.add(dataset).draw();
            });
        } else if (result.valueCount >= 1) {
            verifyLine(result.index)
        } else {
            MessageModal("Consolidation", "Item not in order or has already been consolidated.")
        }
    }

    function checkVerifyType(columnIndex, val) {
        var filterVal = $('#FilterValue').val().toLowerCase();
        $('#FilterValue').typeahead('val', '');
        if (val != undefined) {
            filterVal = val.toLowerCase();
        }
        var valueCount = 0;
        var index = -1;
        for (var x = 0; x < leftVerTable.rows().data().length; x++) {
            var currentColVal = leftVerTable.row(x).data()[columnIndex].toLowerCase()
            if (currentColVal == filterVal) {
                index = x;
                valueCount++;
            }
        }
        return { index: index, valueCount: valueCount }
    }

    $(document.body).on('click', '#PrintPack', function () {
        //print here 
        var ordernum = $('#TypeValue').val();
        if (leftVerTable.rows().data().length > 0) {
            if (confirm("There are still unverfied items. Coninue printing?")) {
                consolidationHub.server.showCMPackPrintModal(ordernum).done(function (result) {
                    if (result == "all") {
                        getLLPreviewOrPrint('/CM/Consolidation/PrintPrevCMPackList', {
                            OrderNum: ordernum,
                            Where: "all",
                            OrderBy: packListSort,
                            Print: 1
                        }, true,'report', 'CM Pack List');
                    } else if (result == "modal") {
                        showCmPackPrintModal(false, ordernum, packListSort);
                    } else {
                        MessageModal("Error", "Error has occured");
                    };
                });
            }
        } else {
            consolidationHub.server.showCMPackPrintModal(ordernum).done(function (result) {
                if (result == "all") {
                    getLLPreviewOrPrint('/CM/Consolidation/PrintPrevCMPackList', {
                        OrderNum: ordernum,
                        Where: "all",
                        OrderBy: packListSort,
                        Print: 1
                    }, true,'report', 'CM Pack List');
                } else if (result == "modal") {
                    showCmPackPrintModal(false, ordernum, packListSort);
                } else {
                    MessageModal("Error", "Error has occured");
                };
            });
        };
        if (parseInt($('#EmailSlip').val()) == 1) {
            consolidationHub.server.insertEmailTrans(ordernum, 0).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occured with the email process")
                } else if (mess != "") {
                    var conf = confirm("An email already exists for this order number. Send another one?");
                    if (conf) {
                        consolidationHub.server.insertEmailTrans(ordernum, 1).done(function (mess2) {
                            if (mess2 == "Error") {
                                MessageModal("Error", "An error has occured with the email process")
                            };
                        });
                    };
                };
            });
        };
    });

    //printing below
    $('#PreviewPack').click(function () {
        //preview stuff here
        var ordernum = $('#TypeValue').val();
        if (leftVerTable.rows().data().length > 0) {
            if (confirm("There are still unverfied items. Coninue the preview?")) {
                consolidationHub.server.showCMPackPrintModal(ordernum).done(function (result) {
                    if (result == "all") {
                        getLLPreviewOrPrint('/CM/Consolidation/PrintPrevCMPackList', {
                            OrderNum: ordernum,
                            Where: "all",
                            OrderBy: packListSort,
                            Print: 0
                        }, false,'report', 'CM Pack List');
                    } else if (result == "modal") {
                        showCmPackPrintModal(true, ordernum);
                    } else {
                        MessageModal("Error", "Error has occured");
                    };
                });
            };
        } else {
            consolidationHub.server.showCMPackPrintModal(ordernum).done(function (result) {
                if (result == "all") {
                    getLLPreviewOrPrint('/CM/Consolidation/PrintPrevCMPackList', {
                        OrderNum: ordernum,
                        Where: "all",
                        OrderBy: packListSort,
                        Print: 0
                    }, false,'report', 'CM Pack List');
                } else if (result == "modal") {
                    showCmPackPrintModal(true, ordernum);
                } else {
                    MessageModal("Error", "Error has occured");
                };
            });
        };
    });

    $(document.body).on('click', '#PrintNotVerified', function () {
        var ordernum = $('#TypeValue').val();
        if (leftVerTable.rows().data().length > 0) {
            getLLPreviewOrPrint('/CM/Consolidation/PrintPrevNotVerified', {
                OrderNum: ordernum,
                Print: 1
            }, true,'report', "Not Verified Preview");
        } else {
            MessageModal('Alert', "There are no unverfied items");
        };
    });

    $('#PreviewNotVerified').click(function () {
        var ordernum = $('#TypeValue').val();
        if (leftVerTable.rows().data().length > 0) {
            getLLPreviewOrPrint('/CM/Consolidation/PrintPrevNotVerified', {
                OrderNum: ordernum,
                Print: 0
            }, false,'report', "Not Verified Preview");
        } else {
            alert("There are no unverfied items");
        };
    });

    // prevents the hub function (called immediately) from being called too fast
    $.connection.hub.start().done(function () {
        if ($('#TypeValue').val() != '') {
            var ev = $.Event("keydown");
            ev.keyCode = ev.which = 13;
            $('#TypeValue').trigger(ev);
        }
    });


});

function enableConButts() {
    $('#NextOrder').removeAttr('disabled');
    $('#goTo').removeAttr('disabled');
    $('#StagingLocations').removeAttr('disabled');
    $('#PackingButt').removeAttr('disabled');
    $('#VerifyAll').removeAttr('disabled');
    $('#UnVerifyAll').removeAttr('disabled');
};

function disableConButts() {
    $('#NextOrder').attr('disabled', 'disabled');
    $('#goTo').attr('disabled', 'disabled');
    $('#StagingLocations').attr('disabled', 'disabled');
    $('#PackingButt').attr('disabled', 'disabled');
    $('#VerifyAll').attr('disabled', 'disabled');
    $('#UnVerifyAll').attr('disabled', 'disabled');
};
