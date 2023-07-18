// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var consolidationHub = $.connection.cMConsolidationHub;
var appendstring = ""
$(document).ready(function () {
    // Initializes all tooltips
    $('[data-toggle="tooltip"]').tooltip();

    //handles filling the modal with the staged locations
    $("#StagingLocations").click(function () {
        $("#StagingContainer").html("")
        $("#StagingLocsOrderNum").val($('#TypeValue').val())
        var rows = ToteTable.rows().data();
        //Only Display if totes exist
        if (rows.length > 0) {
            for (var x = 0; x < rows.length ; x++) {
                var row = ToteTable.row(x).data();
                appendStagingRow(row);
            };
            $("#StagingLocsModal").modal("show");
        }

    });
    //eitehr sets focus to the tote id or ordernumber text box
    $('#StagingLocsModal').on('shown.bs.modal', function (e) {
        if ($('#StagingLocsOrderNum').prop('disabled') == true) {
            $('#stagingToteID').focus();
        } else {
            $('#StagingLocsOrderNum').focus();
            e.stopImmediatePropagation();
        }

    })

    $('#StagingLocsModal').on('hidden.bs.modal', function () {
        $('#TypeValue').focus();
    })

    $('#stagingToteID').keypress(function (e) {
        if (e.which == 13) {
            //Find the Input box for this tote
            $('.col-xs-5 input[value="' + $(this).val() + '"]').parent().parent().find('.location-input').focus();
            $(this).val('');
        }

    })

    $('#StagingLocsOrderNum').on('input', function () {
        $("#StagingContainer").html("");
    })
    //for an order number will populate the modal with available location
    $('#StagingLocsOrderNum').on('keypress', function (e) {
        $("#StagingContainer").html("")
        if (e.which == 13) {
            var inputVal = $(this).val();
            consolidationHub.server.getConsolidationData("", inputVal).done(function (data) {
                console.log(data);
                if (typeof data == 'string') {
                    switch (data) {
                        case "DNE":
                            MessageModal("Consolidation", "The Order/Tote that you entered is invalid or no longer exists in the system.", function () {
                                $('#StagingLocsOrderNum').val('').focus();
                            })
                            break;
                        case "DNENP":
                            $('#StagingLocsOrderNum').val('')
                            var promptResponse = prompt("Order/Tote was not found in the system, enter an order number to correspond to the Tote value scanned")
                            if (promptResponse != null) {
                                $('#StagingLocsOrderNum').val(promptResponse)
                                appendStagingRow([inputVal, "", ""]);
                            }
                            break;
                        case "Conflict":
                            //MessageModal("Staging Locations","You have a conflicting Tote ID and Order Number")
                            ShowOrderToteConflictModal(inputVal, function getStagingInfo(Type, OrderToteConflictVal) {
                                consolidationHub.server.getConsolidationData(Type, OrderToteConflictVal).done(function (data) {
                                    console.log(data);
                                    if (typeof data == 'string') {
                                        switch (data) {
                                            case "DNE":
                                                MessageModal("Consolidation", "The Order/Tote that you entered is invalid or no longer exists in the system.", function () {
                                                    $('#StagingLocsOrderNum').val('').focus();
                                                })
                                                break;
                                            case "Conflict":
                                                ShowOrderToteConflictModal(value, getTableData)
                                                //MessageModal("Consolidation", "The Value you Entered matched a Tote and Order Number, select one to Continue")
                                                break;
                                            case "Error":
                                                MessageModal("Consolidation Error", "An Error occured while retrieving data")
                                                break;
                                        }
                                    }
                                    else {
                                        $('#StagingLocsOrderNum').val(data.OrderNumber)
                                        for (var x = 0; x < data.toteTable.length; x++) {
                                            appendStagingRow(data.toteTable[x], data.OrderNumber, OrderToteConflictVal);
                                        }
                                        //If Tote ID was scanned, automatically select that tote, otherwise focus Tote Scan input
                                        if (data.OrderNumber != OrderToteConflictVal) {
                                            $('#StagingContainer').find('input[value="' + OrderToteConflictVal + '"]').parent().siblings('[name="location"]').children().focus();
                                        } else {
                                            $('#stagingToteID').focus();
                                        }
                                    }
                                })
                            })
                            break;
                        case "Error":
                            MessageModal("Consolidation Error", "An Error occured while retrieving data")
                            break;
                    }
                }
                else {
                    $('#StagingLocsOrderNum').val(data.OrderNumber)
                    for (var x = 0; x < data.toteTable.length; x++) {
                        appendStagingRow(data.toteTable[x], data.OrderNumber, inputVal);
                    }
                    //If Tote ID was scanned, automatically select that tote, otherwise focus Tote Scan input
                    if (data.OrderNumber != inputVal) {
                        $('#StagingContainer').find('input[value="' + inputVal + '"]').parent().siblings('[name="location"]').children().focus();
                    } else {
                        $('#stagingToteID').focus();
                    }
                }
            })
        }
    })

    $(document.body).on('keydown', '.location-input', function (e) {
        if (e.which == 13) {
            saveToteStagingLocation(this)
            $('#StagingLocsOrderNum').select()
        }
    })

    $(document.body).on('focus', '.location-input', function (e) {
        $(this).val("")
        setPopoverVal($(this), $(this).data('val'))

    })

    $(document.body).on('focusout', '.location-input', function (e) {
        $(this).val($(this).data('val'))
    })

    $(document.body).on('click', '.save-location', function () {
        saveToteStagingLocation(this)
    })

    function setPopoverVal(element, value) {
        var popover = element.attr('data-content', 'Current Loc: ' + value).data('bs.popover');
        popover.setContent();
        popover.$tip.addClass(popover.options.placement);
    }

    //clears desired staging location
    $(document.body).on('click', '.clear-location', function () {
        var toteID = $(this).parent().siblings('[name="toteID"]').children().val();
        var location = $(this).parent().parent().find('.location-input').val();
        var locInput = $(this).parent().siblings('[name="location"]').children()
        consolidationHub.server.updateStagingLocation($('#StagingLocsOrderNum').val(), toteID, location, 1).done(function (mess) {
            if (mess[0] == "Fail") {
                MessageModal("Consolidation", "Error Has Occured");
            } else if (mess[0] == "Redirect") {
                window.location.href = "/Logon/";
            } else {
                setPopoverVal(locInput, "")
                locInput.val("")
                locInput.data('val', $(locInput).val())
                if (typeof ToteTable != 'undefined') {
                    for (var x = 0; x < ToteTable.rows().data().length; x++) {
                        var tote = ToteTable.row(x).data()[0];
                        if (tote == toteID) {
                            ToteTable.row(x).data()[2] = ''; //location
                            ToteTable.row(x).data()[3] = ''; //by
                            ToteTable.row(x).data()[4] = ''; //date
                            ToteTable.row(x).invalidate();
                            ToteTable.draw();
                            break;
                        }
                    }
                }
            }
        });
    });

    $('#btnUnstageAll').on('click', function () {
        var clearLoc = $('.clear-location')
        clearLoc.each(function () {
            $(this).trigger('click')
        });
    });

    //saves desired staging location
    function saveToteStagingLocation(element) {
        var toteID = $(element).parent().siblings('[name="toteID"]').children().val();
        var location = $(element).parent().parent().find('.location-input').val();
        consolidationHub.server.updateStagingLocation($('#StagingLocsOrderNum').val(), toteID, location, 0).done(function (mess) {
            if (mess[0] == "Fail") {
                MessageModal("Consolidation", "Error Has Occured");
            } else if (mess[0] == 'INVALID') {
                MessageModal("Staging", "The Location entered was not valid", function () { $(element).val("").focus() })
            } else if (mess[0] == "Redirect") {
                window.location.href = "/Logon/";
            } else {
                if (typeof ToteTable != 'undefined') {
                    for (var x = 0; x < ToteTable.rows().data().length; x++) {
                        var tote = ToteTable.row(x).data()[0];
                        if (tote == toteID) {
                            ToteTable.row(x).data()[2] = location; //location
                            ToteTable.row(x).data()[3] = mess[0]; //by
                            ToteTable.row(x).data()[4] = mess[1]; //date
                            ToteTable.row(x).invalidate();
                            ToteTable.draw();
                            break;
                        }
                    }
                }
                setPopoverVal($(element), $(element).val())
                $(element).data('val', $(element).val())
                $('#stagingToteID').focus();
            }
        });

    }


})
//appends each staging location to the modal
function appendStagingRow(row) {
    appendstring = '<div class="row" style="margin-top:10px;">' +
               '<div class="col-xs-4" name="toteID"><input disabled=disabled maxlength="50" name="" value="' + row[0] + '" class="form-control" /></div>' +
               '<div class="col-xs-5" name="location"><input data-trigger="focus" data-container="body" data-content="Current Loc: ' + row[2] + '" data-placement="top" maxlength="50" name="" value="' + row[2] + '" class="form-control location-input" data-val="' + row[2] + '" onfocus="this.value = this.value" /></div>' +
               '<div class="col-xs-3" name="remove"><button type="button" class="btn btn-danger clear-location" data-toggle="tooltip" data-placement="top" title="Clear Location">Unstage</div>';
    if ($('#StagingLocsOrderNum').prop('disabled')) {
        appendstring += '<div class="col-xs-1" name="save"><button type="button" class="btn btn-primary save-location" data-toggle="tooltip" data-placement="top" data-original-title="Save Carrier"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>';
    }
    appendstring += '</div>';

    $("#StagingContainer").append(appendstring);
    $('#StagingContainer').find('[data-toggle="tooltip"]').tooltip();
    $('.location-input').popover()
}