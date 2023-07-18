// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var toteID;
var batchByOrder;

$(document).ready(function () {
    var pd = $('#PrintDirect').val() == 'true';

    if ($('#DefaultSBFilterTote').val() == 'True') {
        $('#displayBatchByOrderNum').hide();
        $('#orderNumFilter').prop('checked', false);
        $('#orderNumLabel').hide()
        $('#toteLabel').show()
        $('#displayBatchByTote').show();
        $('#toteFilter').prop('checked', true);
    }

    $(document).on('keydown click', '.ToteID, .BatchOrders', function (e) {
        var Row = $(this).closest('.row')
        var toBatch = Row.find('[name="BatchSize"]').val()
        var zone = Row.find('[name="Zone"]').val()
        toteID = Row.find('[name="ToteID"]').val()
        if ((e.which == 13 && $(e.target).attr('name') == 'ToteID') || $(e.target).attr('name') == 'BatchOrders') {
            if (toteID.trim().length == 0) {
                MessageModal("Super Batch", "Must enter a tote id to batch orders", function () { Row.find('[name="ToteID"]').focus() })
            } else if ($('#ConfirmSuperBatch').val().toLowerCase() == 'true') {
                MessageModal("Super Batch Confirm", "Are you sure you want to batch " + toBatch + " orders from Zone " + zone + " in Tote " + toteID, undefined, undefined,
                function () {
                    if ($('#toteFilter').prop('checked') == true) {
                        batchByOrder = 0;
                        CreateSuperBatch(zone, toBatch, toteID, Row, "");

                    } else if ($('#orderNumFilter').prop('checked')) {
                        batchByOrder = 1;
                        CreateSuperBatch(zone, toBatch, toteID, Row, "");

                    } else {
                        batchByOrder = 2;
                        CreateSuperBatch(zone, toBatch, toteID, Row, $('#BatchItemNum').val());
                    };
                });
            } else {
                if ($('#toteFilter').prop('checked') == true) {
                    batchByOrder = 0;
                    CreateSuperBatch(zone, toBatch, toteID, Row,"");

                } else if ($('#orderNumFilter').prop('checked')) {
                    batchByOrder = 1;
                    CreateSuperBatch(zone, toBatch, toteID, Row, "");

                } else {
                    batchByOrder = 2;
                    CreateSuperBatch(zone, toBatch, toteID, Row, $('#BatchItemNum').val());
                };
                insTotePrintTable(toteID);
            }
        }
    });

    // handles filtering Super batch by either Order Number or Tote ID
    $('#toteFilter, #orderNumFilter, #ItemNumFilter').on('click', function () {
        $('#toteFilter, #orderNumFilter').prop('checked', false);
        $(this).prop('checked', true);
        $('#displayBatchByTote, #displayBatchByOrderNum, #displayBatchByItemNum').hide();

        if ($('#toteFilter').prop('checked') == true) {
            $('#displayBatchByTote').show();
            $('#orderNumLabel').hide();
            $('#toteLabel').show();

            $('#BatchToteContainer').html("");
            var AppendString = ""
            $.ajax({
                method: "POST",
                url: "/SuperBatch/SelectItemZoneData",
                data: {
                    Type: "Tote", ItemNumber: ""
                },
                success: function (ZoneData) {
                    for (var x = 0; x < ZoneData.length; x++) {
                        AppendString += '<div class="row top-spacer"><div Class="col-md-2"><input name = "Zone" Class="form-control text-center" type="text" disabled="disabled" ReadOnly  value="' + ZoneData[x].Zone + '")"/></div>' +
                             '<div Class="col-md-2"><input name = "SingleLineOrders" Class="form-control text-center" type="text" disabled="disabled" ReadOnly value="' + ZoneData[x].TotalTrans + '")" /></div>' +
                             '<div Class="col-md-3"><input name = "BatchSize" Class="form-control text-center" type="number" min="2" value="' + $('#BatchSizePref').val() + '" /></div>' +
                             '<div Class="col-md-3"><input name = "ToteID" Class="form-control text-center ToteID" type="text"  value="" /></div>' +
                             '<div Class="col-md-2"><Button name = "BatchOrders" Class="btn btn-primary btn-block BatchOrders">Batch Orders</button></div></div>'
                    };
                    $('#BatchToteContainer').html(AppendString);
                },
                error: function (xhr, status, errorThrown) {
                    alert(xhr.responseText);
                }
            });

        } else if ($('#orderNumFilter').prop('checked')) {
            $('#displayBatchByOrderNum').show();
            $('#displayBatchByItemNum').hide();
            $('#toteLabel').hide();
            $('#orderNumLabel').show();

            $('#BatchOrderContainer').html("");
            var AppendString = ""
            $.ajax({
                method: "POST",
                url: "/SuperBatch/SelectItemZoneData",
                data: {
                    Type:"Order", ItemNumber: ""
                },
                success: function (ZoneData) {
                    for (var x = 0; x < ZoneData.length; x++) {
                        AppendString += '<div class="row top-spacer"><div Class="col-md-2"><input name = "Zone" Class="form-control text-center" type="text" disabled="disabled" ReadOnly  value="' + ZoneData[x].Zone + '")"/></div>' +
                             '<div Class="col-md-2"><input name = "SingleLineOrders" Class="form-control text-center" type="text" disabled="disabled" ReadOnly value="' + ZoneData[x].TotalTrans + '")" /></div>' +
                             '<div Class="col-md-3"><input name = "BatchSize" Class="form-control text-center" type="number" min="2" value="' + $('#BatchSizePref').val() + '" /></div>' +
                             '<div Class="col-md-3"><input name = "ToteID" Class="form-control text-center ToteID" type="text"  value="" /></div>' +
                             '<div Class="col-md-2"><Button name = "BatchOrders" Class="btn btn-primary btn-block BatchOrders">Batch Orders</button></div></div>'
                    };
                    $('#BatchOrderContainer').html(AppendString);
                },
                error: function (xhr, status, errorThrown) {
                    alert(xhr.responseText);
                }
            });

        } else {
            $('#displayBatchByItemNum').show();
        };

    });

    $('#BatchItemNum').on('change', function () {
        $('#BatchItemNumContainer').html("");
        var AppendString=""
        $.ajax({
            method: "POST",
            url: "/SuperBatch/SelectItemZoneData",
            data: {
                Type: "Item", ItemNumber: $('#BatchItemNum').val()
            },
            success: function (ZoneData) {
                for (var x = 0; x < ZoneData.length; x++) {
                    AppendString += '<div class="row top-spacer"><div Class="col-md-2"><input name = "Zone" Class="form-control text-center" type="text" disabled="disabled" ReadOnly  value="' + ZoneData[x].Zone + '")"/></div>' + 
                         '<div Class="col-md-2"><input name = "SingleLineOrders" Class="form-control text-center" type="text" disabled="disabled" ReadOnly value="' + ZoneData[x].TotalTrans + '")" /></div>' +
                         '<div Class="col-md-3"><input name = "BatchSize" Class="form-control text-center" type="number" min="2" value="' + $('#BatchSizePref').val() + '" /></div>' +
                         '<div Class="col-md-3"><input name = "ToteID" Class="form-control text-center ToteID" type="text"  value="" /></div>' +
                         '<div Class="col-md-2"><Button name = "BatchOrders" Class="btn btn-primary btn-block BatchOrders">Batch Orders</button></div></div>'
                };
                $('#BatchItemNumContainer').html(AppendString);
            },
            error: function (xhr, status, errorThrown) {
                alert(xhr.responseText);
            }
        });
    });

    $(document.body).on('change', 'input', function () {
        if ($(this).attr('name') == 'BatchSize' && $(this).val() < 2) {
            MessageModal('Orders to Super Batch', 'Batch Size must be greater than 1')
            $(this).val('2')
        }
    })

    $('#PrintBatchButton').on('click', function () {
        if ($('#PrintBatchInput').val() != "") {
            getLLPreviewOrPrint('/IM/SuperBatch/PrintSuperBatchLabel', {
                ToteID: $('#PrintBatchInput').val(),
                printDirect: pd
            }, pd, 'label', "Batch Pick ID Label");
        } else {
            alert("Please Select a Batch ID to Print");
        };
    });

    $('#PrintOrderButton').on('click', function () {
        if ($('#PrintBatchInput').val() != "") {
            getLLPreviewOrPrint('/IM/SuperBatch/PrintSuperBatchOrderLabel', {
                ToteID: $('#PrintBatchInput').val(),
                printDirect: pd
            }, pd, 'label', "Batch Orders Label");
        } else {
            alert("Please Select a Batch ID to Print");
        };
    });

    $('#PrintCaseButton').on('click', function () {
        if ($('#PrintBatchInput').val() != "") {
            getLLPreviewOrPrint('/IM/SuperBatch/PrintPrevInZoneCaseLabelToteID/', {
                ToteID: $('#PrintBatchInput').val(),
                printDirect: pd
            }, pd, 'label', "Case Label");
        } else {
            alert("Please Select a Batch ID to Print");
        };
    });

    function CreateSuperBatch(zone, toBatch, toteID, row, ItemNum) {
        $.ajax({
            method: "POST",
            url: "/SuperBatch/CreateSuperBatch",
            data: {
                Zone: zone, ToBatch: toBatch, ToteID: toteID, ItemNum: ItemNum, batchByOrder: batchByOrder
            },
            success: function (response) {
                if (response == "Error") {
                    MessageModal("Super Batch Error","An Error Occured while trying to create a Super Batch")
                } else {

                    $('#PrintBatchInput').append($('<option>', {
                        value: toteID,
                        text: toteID
                    }));

                    row.find('[name="ToteID"]').val("")
                    var orders = row.find('[name="SingleLineOrders"]')
                    var batchSize = row.find('[name="BatchSize"]')
                    if (response <= 0) {
                        row.remove();
                    } else {
                        orders.val(response)
                    }

                    PrintSuperBatchLabels(toteID);

                    insTotePrintTable(toteID);
                }
                
            },
            error: function (xhr, status, errorThrown) {
                alert(xhr.responseText);
            }
        });
    }

    function insTotePrintTable(toteID) {
        $.ajax({
            method: "POST",
            url: "/SuperBatch/insTotePrintTable/",
            data: {
                toteID: toteID
            },
            success: function (response) {
                if (response == "Error") {
                    MessageModal("Super Batch Error","An Error Occured while trying to insert a Super Batch into the Totes Printed table.")
                }
            },
            error: function (xhr, status, errorThrown) {
                alert(xhr.responseText);
            }
        });
    };

    function PrintSuperBatchLabels(toteID) {
        //check preferences here to auto print
        if ($('#AutoPrintBatchLabel').val() == 'true') {
            getLLPreviewOrPrint('/IM/SuperBatch/PrintSuperBatchLabel', {
                ToteID: toteID,
                printDirect: pd
            }, pd, 'label', "Batch Pick ID Label", function () {
                if ($('#AutoPrintOrderLabels').val() == 'true') {
                    getLLPreviewOrPrint('/IM/SuperBatch/PrintSuperBatchOrderLabel', {
                        ToteID: toteID,
                        printDirect: pd
                    }, pd, 'label', "Batch Orders Label", function () {
                        if ($('#AutoPrintCaseLabel').val() == 'true') {
                            getLLPreviewOrPrint('/IM/SuperBatch/PrintPrevInZoneCaseLabelToteID/', {
                                ToteID: toteID,
                                printDirect: pd
                            }, pd, 'label', "Case Label");
                        };
                    });
                };
            });
        } else if ($('#AutoPrintOrderLabels').val() == 'true') {
            getLLPreviewOrPrint('/IM/SuperBatch/PrintSuperBatchOrderLabel', {
                ToteID: toteID,
                printDirect: pd
            }, pd, 'label', "Batch Orders Label", function () {
                if ($('#AutoPrintCaseLabel').val() == 'true') {
                    getLLPreviewOrPrint('/IM/SuperBatch/PrintPrevInZoneCaseLabelToteID/', {
                        ToteID: toteID,
                        printDirect: pd
                    }, pd, 'label', "Case Label");
                };
            });
        } else if ($('#AutoPrintCaseLabel').val() == 'true') {
            getLLPreviewOrPrint('/IM/SuperBatch/PrintPrevInZoneCaseLabelToteID/', {
                ToteID: toteID,
                printDirect: pd
            }, pd, 'label', "Case Label");
        };

        //if ($('#AutoPrintOrderLabels').val() == 'true') {
        //    getLLPreviewOrPrint('/IM/SuperBatch/PrintSuperBatchOrderLabel', {
        //        ToteID: toteID,
        //        printDirect: pd
        //    }, pd, 'label', "Batch Orders Label");
        //};
    };

    $('#SBReqDateStatusButt').click(function () {
        $('#SBReqDateModal').modal('show');
    });

    //  --Add this code and call function to disable Batch Orders button when ther is only 1 Single line order in a zone.
    //function disableButtons() {
    //    var numRows = $('#displayBatchByOrderNum').find('[name="BatchOrders"]').length
    //    var singleLineVal = $('#displayBatchByOrderNum').find('[name="SingleLineOrders"]').first()
    //    if (numRows > 0) {
    //        for (var i = 0; i < numRows; i++) {
    //            if (singleLineVal.val() == 1) {
    //                singleLineVal.parent().parent().find('[name="BatchOrders"]').attr('disabled', true)
    //            }
    //            singleLineVal = singleLineVal.parent().parent().next().find('[name="SingleLineOrders"]')
    //        }
    //    }
    //    var numRows = $('#displayBatchByTote').find('[name="BatchOrders"]').length
    //    var singleLineVal = $('#displayBatchByTote').find('[name="SingleLineOrders"]').first()
    //    if (numRows > 0) {
    //        for (var i = 0; i < numRows; i++) {
    //            if (singleLineVal.val() == 1) {
    //                singleLineVal.parent().parent().find('[name="BatchOrders"]').attr('disabled', true)
    //            }
    //            singleLineVal = singleLineVal.parent().parent().next().find('[name="SingleLineOrders"]')
    //        }
    //    }
    //}
})