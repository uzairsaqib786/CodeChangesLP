// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2021

var toteIdScanned = "";

$(document).ready(function () {
    $('#ToteIDScan').on('keypress', function (e) {

        if (e.which === 13) {
            toteIdScanned = $('#ToteIDScan').val();
            $('#ToteIDScan').val("");
            GetToteData(toteIdScanned, false);
        };
    });

    $(document.body).on('click', '.UpdQtyApply', function () {

        var $tr = $(this).parents('tr');
        var otid = $tr.data('id');
        var transQty = parseInt($tr.find('td[name="TransQty"]').text());
        var newQty = parseInt($(this).parents('.input-group').find('input').val());


        // validation
        if (newQty < 0) {
            alert('The Picked Quantity can not be less than zero');
        } else if (newQty > transQty) {
            alert('The Picked Quantity is greater than the Requested Quantity')
        } else {
            if (newQty < transQty) {
                displayConf("You have requested to update this line item with a short quantity. | You must press the ship short button after the update to complete this transaction. | Are you sure you want to update the picked quantity?", "Short Quantity", function () {
                    $.ajax({
                        url: "/MO/Menu/UpdateShortQuantity",
                        method: "POST",
                        data: {
                            'OTID': otid,
                            'Quant': newQty
                        },
                        success: function (updated) {
                            if (!updated) {
                                alert('There was an error updating the transaction');
                            } else {
                                $tr.find('td[name="CompQty"]').text(newQty);
                                $tr.find('td[name="Status"]').text('Short');
                                $tr.find('td[name="ShortQty"]').text(transQty - newQty);
                                $tr.find('td[name="UpdQtyBtn"]').children('.UpdQtyBtn').popover('hide');
                            };
                        }
                    });
                });
            } else {
                displayConf("You have requested to update this line item with a quantity equal to the transaction quantity. | This will complete the transaction. | Are you sure you want to perform this action?", "Complete Quantity", function () {
                    var Result = CompleteLine(otid, false);

                    Result.done(function (res) {
                        if (res) {
                            $tr.find('td[name="CompQty"]').text(transQty);
                            $tr.find('td[name="ShortQty"]').text(0);
                            $tr.find('td[name="Status"]').text('Complete');
                            $tr.find('td[name="Status"]').css('background-color', "green");
                            $tr.find('td[name="Status"]').css('color', "white");
                            $tr.find('td[name="UpdQtyBtn"]').children('.UpdQtyBtn').popover('hide');

                            $tr.find('td[name="UpdQtyBtn"]').children('.UpdQtyBtn').hide();
                            $tr.find('td[name="CompBtn"]').children('.CompBtn').hide();
                            $tr.find('td[name="ShipShortBtn"]').children('.ShipShortBtn').hide();
                            CheckStatus();
                            $('#ToteIDScan').focus();
                        };
                    });
                });
            };
        };
    });

    $(document.body).on('click', '.ShipShortBtn', function () {

        var $tr = $(this).parents('tr');
        var otid = $tr.data('id');

        displayConf("You have requested to ship this transaction short. | This will complete the transaction with the short quantity. | Do you want to perform this action?", "Ship Short", function () {
            var Result = CompleteLine(otid, true);

            Result.done(function (res) {
                if (res) {
                    $tr.find('td[name="Status"]').text('Ship Short');
                    $tr.find('td[name="Status"]').css('background-color', "green");
                    $tr.find('td[name="Status"]').css('color', "white");

                    $tr.find('td[name="UpdQtyBtn"]').children('.UpdQtyBtn').hide();
                    $tr.find('td[name="CompBtn"]').children('.CompBtn').hide();
                    $tr.find('td[name="ShipShortBtn"]').children('.ShipShortBtn').hide();
                    CheckStatus();
                    $('#ToteIDScan').focus();
                };
            });
        });
    });

    $(document.body).on('click', '.CompBtn', function () {

        var $tr = $(this).parents('tr');
        console.log($tr)
        var otid = $tr.data('id');
        var transQty = parseInt($tr.find('td[name="TransQty"]').text());

        displayConf("You have requested to complete this transaction. | This will complete the transaction with the Transaction Quantity. | Do you want to perform this action?", "Complete Transaction", function () {
            var Result = CompleteLine(otid, false);

            Result.done(function (res) {
                if (res) {
                    $tr.find('td[name="CompQty"]').text(transQty);
                    $tr.find('td[name="ShortQty"]').text(0);
                    $tr.find('td[name="Status"]').text('Complete');
                    $tr.find('td[name="Status"]').css('background-color', "green");
                    $tr.find('td[name="Status"]').css('color', "white");

                    $tr.find('td[name="UpdQtyBtn"]').children('.UpdQtyBtn').hide();
                    $tr.find('td[name="CompBtn"]').children('.CompBtn').hide();
                    $tr.find('td[name="ShipShortBtn"]').children('.ShipShortBtn').hide();
                    CheckStatus();
                    $('#ToteIDScan').focus();
                };
            });
        });
    });
    

    $('#Blossom, #BlossomComplete').click(function () {
        var id = $(this).attr('id');

        if (id == "Blossom") {
            $('#BlossomModalTitle').text("Blossom");
            $('#QtyColTitle').html('Old Tote Qty')
        } else {
            $('#BlossomModalTitle').text("Blossom Complete");
            $('#QtyColTitle').html('New Tote Qty');
        };

        GetToteData(toteIdScanned, true);
    });
    IsBlossCompAllowed();
});

function CompleteLine(OTID, ShipShort) {
    return $.ajax({
        url: "/MO/Menu/CompleteTrans",
        method: "POST",
        data: {
            'OTID': OTID,
            'ShipShort': ShipShort
        },
        success: function (updated) {
            if (!updated) {
                alert('There was an error updating the transaction');
                return false
            };
        }
    });
};

function CheckStatus() {
    var MarkTbl = $('#markoutdata');
    var analyze = false;

    //each row
    MarkTbl.each(function () {
        //each status column
        $(this).find('td[name="Status"]').each(function () {
            if ($(this).text() == "Short" || $(this).text() == "Missed") {
                //Keep analyze text
                analyze = true;
                return false;
            };
        });
    });

    //all processed change tote status to complete
    if (!analyze) {
        $('#ToteStatus').text("Complete");
        $('#ToteStatus').css('color', "green");
        $('#Blossom').attr('disabled', 'disabled');
        $('#BlossomComplete').attr('disabled', 'disabled');
    };
};

function IsBlossCompAllowed() {
    return $.ajax({
        url: "/MO/Menu/GetParamByParamName",
        method: "GET",
        data: {
            ParamName: "BlossomComplete"
        },
        success: function (BCAllowed) {
            if (BCAllowed != '1') {
                $("#BlossomComplete").hide();
            }
        }
    });
};

function GetToteData(ToteID, IsBloss) {

    $.ajax({
        url: "/MO/Menu/SelectToteData",
        method: "GET",
        data: {
            'ToteID': ToteID
        },
        success: function (data) {
            if (IsBloss) {
                DisplayBlossData(data);
            } else {
                DisplayToteData(data);
            };

        }
    });
};

function DisplayToteData(ToteData) {
    var MarkTbl = $('#markoutdata');

    MarkTbl.empty();
    $('#ToteIDScan').val("");
    $('#ToteIDScan').focus();
    $('#WeightStatus').text("");
    $('#ToteStatus').text("");
    $('#MarkoutStatus').hide();
    $("#Blossomed").hide();
    $('#OrderNum').text("");

    if (!ToteData.Data) {

        $('#Blossom').attr('disabled', 'disabled');
        $('#BlossomComplete').attr('disabled', 'disabled');
        $('#WeightPass').attr('disabled', 'disabled');

        alert('No records for Tote ID found');
    } else {
        $('#Blossom').removeAttr('disabled');
        $('#BlossomComplete').removeAttr('disabled');

        //Weight Status
        $('#WeightStatus').text(ToteData.WeightStatus);
        if (ToteData.WeightStatus == "Passed") {
            $('#WeightStatus').css('color', "green");
            $('#WeightPass').attr('disabled', 'disabled');
        } else {
            $('#WeightStatus').css('color', "red");
            $('#WeightPass').removeAttr('disabled');
        };

        //Tote Status
        $('#ToteStatus').text(ToteData.ToteStatus);
        if (ToteData.ToteStatus == "Complete") {
            $('#ToteStatus').css('color', "green");
            $('#Blossom').attr('disabled', 'disabled');
            $('#BlossomComplete').attr('disabled', 'disabled');
        } else {
            $('#ToteStatus').css('color', "black");
        }

        //Markout Status
        if (ToteData.MarkoutStatus !== "") {
            $('#MarkoutStatus').text(ToteData.MarkoutStatus);
            $('#MarkoutStatus').show();
        }

        if (ToteData.BlossomCount > 0) {
            //$("#BlossomCount").text(ToteData.BlossomCount)
            $("#Blossomed").show();
        }


        $('#OrderNum').text(ToteData.Data[0].OrderNum);

        //Table Rows
        $.each(ToteData.Data, function (i, ToteLine) {
            var row = '<tr data-id="' + ToteLine.Id + '">';
            row += makeCell(ToteLine.ToteId, "ToteID");
            row += makeCell(ToteLine.Item, "ItemNum");
            row += makeCell(ToteLine.Loc, "Location");
            row += makeCell(ToteLine.TransQty, "TransQty");
            row += makeCell(ToteLine.CompQty, "CompQty");
            row += makeCell(ToteLine.ShortQty, "ShortQty");
            row += makeCell(ToteLine.Status, "Status");

            if (ToteLine.Status == "Missed" || ToteLine.Status == "Short") {
                row += makeCell('<button type="button" style="font-size:x-large" class="btn btn-success CompBtn">Complete</button>', 'CompBtn');
                row += makeCell('<button type="button" style="font-size:x-large" class="btn btn-warning ShipShortBtn">Ship Short</button>', 'ShipShortBtn');
                row += makeCell('<button type="button" style="font-size:x-large" class="btn btn-info UpdQtyBtn">Update Quantity</button>', 'UpdQtyBtn');
            };

            row += '</tr>';
            MarkTbl.append(row);

        });

        $('.UpdQtyBtn').popover({
            html: true,
            content: '<div class="input-group"><input class="form-control" type="number" /><div class="input-group-btn"><button type="button" class="btn btn-success UpdQtyApply"><span class="glyphicon glyphicon-ok"></span></button></div></div>',
            placement: 'top'
        }).on('shown.bs.popover', function () {
            var tr = $(this).parents('tr');
            var td = $(this).parent('td');
            var poc = td.find('.popover-content input');
            var pickedQty = tr.find('td[name="CompQty"]');
            poc.val(pickedQty.text());
            poc.focus();
        });


    };
};

function makeCell(inner, name) {
    if (name == "ShortQty") {
        return '<td style="font-size:x-large;background-color:lightgreen" name="' + name + '">' + inner + '</td>';
    } else if (name == "Status") {
        if (inner == "Missed" || inner == "Short") {
            return '<td style="font-size:x-large;background-color:pink" name="' + name + '">' + inner + '</td>';
        } else {
            return '<td style="font-size:x-large;background-color:green;color:white" name="' + name + '">' + inner + '</td>';
        };
    } else {
        return '<td style="font-size:x-large;" name="' + name + '">' + inner + '</td>';
    };
};

function DisplayBlossData(ToteData) {
    var BlossTbl = $('#blossomdata');
    BlossTbl.empty();

    //Table Rows
    $.each(ToteData.Data, function (i, ToteLine) {
        console.log(ToteLine)
        if (ToteLine.Status == "Missed" || ToteLine.Status == "Short") {
            var row = '<tr data-id="' + ToteLine.Id + '">';
            row += makeCell(ToteLine.Item, "ItemNum");
            row += makeCell(ToteLine.TransQty, "TransQty");
            row += makeCell(ToteLine.CompQty, "CompQty");
            row += makeCell('<input type="number" style="font-size:x-large;" min="0" max="' + ToteLine.TransQty + '" class="form-control BlossQty"/>', 'BlossQty');
            row += '</tr>';
            BlossTbl.append(row);
        };

    });

    if ($('#blossomdata tr').length > 0) {
        $('#BlossomModal').modal('show');
    } else {
        alert('No Records to Blossom');
    };

};
