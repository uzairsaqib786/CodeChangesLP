// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var pd = $('#PrintDirect').val() == 'true';

    $('#BlossUseNextToteID').click(function () {
        PickToteHub.server.grabNextToteID().done(function (nextTote) {
            $('#NewBlossTote').val(nextTote);

            //sets the next tote id to the old one plus one
            PickToteHub.server.updateNextToteID(nextTote + 1).done(function (updated) {
                if (!updated) {
                    MessageModal('Error', 'An error occurred while attempting to save the new "Next Tote ID"');
                };
            });
        });
    });

    $('#SubmitBlossom').click(function () {
        if ($('#NewBlossTote').val() == "" ||  $('#OriginalBlossTote').val() == "") {
            MessageModal('Tote ID Not Supplied', 'Either the Old or New Tote ID was not supplied.');
        }else{
            var Tote = [];
            Tote.push($('#NewBlossTote').val());

            PickToteHub.server.validateTotes(Tote).done(function (ToteVal) {
                switch (ToteVal) {
                    case "Error":
                        MessageModal("Error", "An error has occurred validating the current tote setup");
                        break;
                    case "":
                        MessageModal("Perform Blossom", "Perform the blossom? This will move all open transaction lines from the old tote to the new tote.", undefined, undefined,
                            function () {
                                //call Blsssom function
                                PickToteHub.server.processBlossom($('#OriginalBlossTote').val(), $('#NewBlossTote').val()).done(function (batch) {
                                    if (batch == "Error") {
                                        MessageModal("Error", "An error has occurred processing the current tote setup");
                                    } else {

                                        if ($('#AutoPrintPickToteLabs').val() == "true") {
                                            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneBatchToteLabel/', {
                                                BatchID: batch,
                                                printDirect: pd
                                            }, pd, 'label', "Batch Tote Label");
                                        };

                                        if ($('#AutoPrintOffCarPickList').val() == "true") {
                                            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchList/', {
                                                BatchID: batch,
                                                printDirect: pd
                                            }, pd, 'label', "Batch Pick List");
                                        };

                                        //clear fields for next blossom
                                        $('#NewBlossTote').val("");
                                        $('#OriginalBlossTote').val("");
                                    };
                                });
                            }
                        );
                        break;
                    default:
                        MessageModal("Invalid Tote ID", "The tote id " + ToteVal + " already exists in Open Transactions. Please select another tote");
                        $('#NewBlossTote').val("");
                        break;
                };
            });

        };
    });
});