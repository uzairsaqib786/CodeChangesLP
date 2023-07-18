// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var currentDisplayMove = ''
$(document).ready(function () {
   
    $('#Move_Modal').on('hidden.bs.modal', function () {
        switch (currentDisplayMove) {
            case "Dedicate":
                if ($('#MoveFrom_Dedicated').text() == "Dedicated") {
                    ShowMoveModal("Un-Dedicate")
                } else { callCreateMoveTrans() }
                break;
        }
    })

    $('#Move_Modal').on('shown.bs.modal', function () {
        $('#Hold_submit').focus();
    })

    $('#Move_submit').click(function () {
        switch (currentDisplayMove) {
            case "Un-Dedicate":
                undedicateMoveFrom = true;
                callCreateMoveTrans();
                break;
            case "Dedicate":
                dedicateMoveTo = true;
                break;
        }
    });

    $('#Move_dismiss').click(function () {
        switch (currentDisplayMove) {
            case "Un-Dedicate":
                undedicateMoveFrom = false;
                callCreateMoveTrans();
                break;
            case "Dedicate":
                dedicateMoveTo = false;
                break;
        }
    });

});
function ShowMoveModal(toDisplay) {
    currentDisplayMove = toDisplay
    switch (toDisplay) {
        case "Un-Dedicate":
            $('#Move_Message').html("<strong>Would you like to Undedicate your move from Location?</strong>")
            MoveModalButtons(true);
            break;
        case "Dedicate":
            $('#Move_Message').html("<strong>Would you like to Dedicate your move to Location?</strong>")
            MoveModalButtons(true);
            break;
        case "ZeroQty":
            $('#Move_Message').html("<strong>You must specify a Qty greater than 0 to create Move Transactions</strong>")
            MoveModalButtons(false);
            break;
        case "MaxMove":
            $('#Move_Message').html("<strong>You must specify a Qty less than the Available Qty of " + MaxMoveQty + " to create Move transactions</strong>")
            MoveModalButtons(false);
            break;
        case "Error":
            $('#Move_Message').html("<strong>An Error occured while creating move Transactions. Check the Event log for More information</strong>")
            MoveModalButtons(false);
            break;
        case "MoveCap":
            $('#Move_Message').html("<strong>Cannot Move more than " + MaxMoveQty + " because there are currently Picks allocated to this Location. Deallocate these Transactions if you would like to move more than " + MaxMoveQty + ".</strong>")
            MoveModalButtons(false)
            break;
        case "MaxAlloc":
            $('#Move_Message').html("<strong>Your Allocations for the Location exceed or match the current qty. To move from this location, de-allocate transactions to free up inventory</strong>")
            MoveModalButtons(false)
            break;
    }
    $("#Move_Modal").modal('toggle');
}

function MoveModalButtons(isYesNo) {
    if (isYesNo) {
        $('#Move_submit').text("Yes");
        $('#Move_dismiss').show();
    } else {
        $('#Move_submit').text("Ok");
        $('#Move_dismiss').hide();
    }
}

function callCreateMoveTrans() {
    $.ajax({
        method: "POST",
        url: "/MoveItems/CreateMoveTransactions",
        data: {
            MoveFromID: parseInt($('#MoveFromTable tbody tr.active').find(':nth-child(29)').html()), MoveToID: parseInt($('#MoveToTable tbody tr.active').find(':nth-child(29)').html()), MoveFromItemNumber: $('#MoveFrom_ItemNumber').val(),
            MoveToItemNumber: $('#MoveTo_ItemNumber').val(), MoveToZone: $('#MoveFromTable tbody tr.active').find(':nth-child(9)').html(), MoveQty: $('#MoveQty').val(),
            ReqDate: $('#MoveReqDate').val(), Priority: $('#MovePriority').val(), dedicateMoveTo: dedicateMoveTo, unDedicateMoveFrom: undedicateMoveFrom
        },
        success: function (response) {
            if (response == "Error") {
                ShowMoveModal("Error")
            }
            MoveFromTable.draw();
        },
        error: function (xhr, status, errorThrown) {
            alert(xhr.responseText);
        }
    });
}