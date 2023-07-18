// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    /*
    ****************************************
    REMOVE THIS LINE TO SHOW ALL WORKSTATION PREFERENCES 
    */
    $('#BulkZonesLink, #PodSetupLink, #SortBarLink, #PickLevelsLink, #CustAppsLink, #MiscSettLink').hide();
    /*
    REMOVE THIS LINE TO SHOW ALL WORKSTATION PREFERENCES 
    ****************************************
    */

    var s1Timer = mkTimer(function () {
        saveWorkstationSettings();
    }, 600);
    var s2Timer = mkTimer(function () {
        saveToteManagement();
    }, 600);
    var s3Timer = mkTimer(function () {
        saveLocAssFunctions();
    }, 600);
    $('#PrintPickLabelForEach').on('toggle', function (e, active) {
        if (active) {
            $('#PrintAllOnce_div').show();
        } else {
            $('#PrintAllOnce_div').hide();
        }
    });

    $('#Setup_1').on('input', 'input', function () {
        s1Timer.startTimer();
    });

    $('#Setup_1').on('toggle', '.toggles', function () {
        s1Timer.startTimer();
    });

    $('#Setup_1').on('change', 'select', function () {
        s1Timer.startTimer();
    });

    $('#Setup_2').on('input', 'input', function () {
        s2Timer.startTimer();
    });

    $('#Setup_2').on('toggle', '.toggles', function () {
        s2Timer.startTimer();
    });

    $('#Setup_2').on('change', 'select', function () {
        s2Timer.startTimer();
    });

    $('#Setup_3').on('input', 'input', function () {
        s3Timer.startTimer();
    });

    $('#Setup_3').on('toggle', '.toggles', function () {
        s3Timer.startTimer();
    });

    $('#Setup_3').on('change', 'select', function () {
        s3Timer.startTimer();
    });

    
    $('#PickToTotes').on('toggle', function (e, checked) {
        if (!checked) {
            $('#CarPickToteManifest').data('toggles').setValue(false);
            $('#OffCarPickToteManifest').data('toggles').setValue(false);
            $('#AutoPrintOffCar').data('toggles').setValue(false);
        };
        s2Timer.startTimer();
    });

    $('#CarPickToteManifest, #OffCarPickToteManifest, #AutoPrintOffCar').on('toggle', function (e, checked) {
        var $this = $(this)
        if (checked) {
            if ($('#PickToTotes').data('toggles').active) {
                $(this).data('toggles').setValue(true);
            } else {
                var checkVal = confirm("Pick To Totes needs to be on in order to use Manifests. Press okay to turn Pick To Totes and the desired Manifest on.")
                if (checkVal) {
                    $('#PickToTotes').data('toggles').setValue(true);
                    $(this).data('toggles').setValue(true);
                } else {
                    setTimeout(function () {
                        $this.data('toggles').setValue(false);
                    }, 0);
                };
            };
            
        };
        s2Timer.startTimer();
    });

    $('#PrintPickLabelForEach').on('toggle', function (e, checked) {
        if (!checked) {
            $('#PrintAllOnce').data('toggles').setValue(false);
        };
        s3Timer.startTimer();
    });

    $('#podNumbers').change(function () {
        if ($('#podNumbers').val() == "No") {
            saveCarSW(false);
                $('#BulkZonesLink, #PodSetupLink, #SortBarLink, #PickLevelsLink, #CustAppsLink, #MiscSettLink').hide();
        } else {
            //Remove this comment to enable showing of Workstation Preferences
            //$('#BulkZonesLink, #PodSetupLink, #SortBarLink, #PickLevelsLink, #CustAppsLink, #MiscSettLink').show();
            var check = confirm("Press Okay to turn Carousel Communications on")
            if (check) {
               saveCarSW(true);
            } else {
                saveCarSW(false);
            };
        };
        s1Timer.startTimer();
    });


    $('#connectionString').change(function () {
        if(confirm("Changing your linked database will cause you to be signed out. Would you like to continue?"))
        {
            //Do Things Here
            preferencesHub.server.changeConnectionString($(this).val()).done(function () {
                window.location.href = '/Logon/Logout'
            })
        }
        else {
            //Reset to old Value
            $(this).val($('#currentConnString').val());
        }
    })

    
});

function getWorkstationSetup() {
    preferencesHub.server.selectWorkstationSetupInfo().done(function (settings) {
        //Workstation Settings
        $('#podNumbers').val(settings.wssett.podid)
        if (settings.wssett.podid == "No")
        {
            $('#BulkZonesLink, #PodSetupLink, #SortBarLink, #PickLevelsLink, #CustAppsLink, #MiscSettLink').hide();
        }
        $('#cartonFlowNumbers').val(settings.wssett.cfid)
        if (settings.wssett.cfid == "No")
        {
            $('#BulkZonesLink, #PodSetupLink, #SortBarLink, #PickLevelsLink, #CustAppsLink, #MiscSettLink').hide();
        }
             
        else {
            //Remove this comment to enable showing of Workstation Preferences
            //$('#BulkZonesLink, #PodSetupLink, #SortBarLink, #PickLevelsLink, #CustAppsLink, #MiscSettLink').show();
        }
        if (settings.wssett.scanpicks) {
            $('#SVPicks').data('toggles').setValue(true);
        } else {
            $('#SVPicks').data('toggles').setValue(false);
        };

        if (settings.wssett.scancounts) {
            $('#SVCounts').data('toggles').setValue(true);
        } else {
            $('#SVCounts').data('toggles').setValue(false);
        };
        
        if (settings.wssett.scanputs) {
            $('#SVPuts').data('toggles').setValue(true);
        } else {
            $('#SVPuts').data('toggles').setValue(false);
        };

        if (settings.wssett.quickpick.toLowerCase() == 'yes') {
            $('#AllowQuick').data('toggles').setValue(true);
        } else {
            $('#AllowQuick').data('toggles').setValue(false);
        };

        if (settings.wssett.defquickpick.toLowerCase() == 'yes') {
            $('#DefaultQuick').data('toggles').setValue(true);
        } else {
            $('#DefaultQuick').data('toggles').setValue(false);
        };
         
        $('#reportPrinter').val(settings.wssett.printreploc) 
        $('#labelPrinter').val(settings.wssett.printlabloc) 

        //Tote Management
        $('#BatchHotPut').val(settings.toteman.batchhotput)

        if (settings.toteman.picktote) {
            $('#PickToTotes').data('toggles').setValue(true);
        } else {
            $('#PickToTotes').data('toggles').setValue(false);
        };

        if (settings.toteman.puttote) {
            $('#PutAwayFromTotes').data('toggles').setValue(true);
        } else {
            $('#PutAwayFromTotes').data('toggles').setValue(false);
        };

        if (settings.toteman.carmanifest.toLowerCase() == 'yes') {
            $('#CarPickToteManifest').data('toggles').setValue(true);
        } else {
            $('#CarPickToteManifest').data('toggles').setValue(false);
        };

        if (settings.toteman.offcarmanifest.toLowerCase() == 'yes') {
            $('#OffCarPickToteManifest').data('toggles').setValue(true);
        } else {
            $('#OffCarPickToteManifest').data('toggles').setValue(false);
        };

        if (settings.toteman.autoprintoffcar.toLowerCase() == 'yes') {
            $('#AutoPrintOffCar').data('toggles').setValue(true);
        } else {
            $('#AutoPrintOffCar').data('toggles').setValue(false);
        };

        if (settings.toteman.autoprinttote) {
            $('#AutoPrintTote').data('toggles').setValue(true);
        } else {
            $('#AutoPrintTote').data('toggles').setValue(false);
        };

        if (settings.toteman.batchput) {
            $('#BatchCarPut').data('toggles').setValue(true);
        } else {
            $('#BatchCarPut').data('toggles').setValue(false);
        };

        //Location Assignment Functions
        $('#PickLocAssigned').val(settings.locassfun.picksort);
        $('#DisplayBulkQty').val(settings.locassfun.qtybulk);

        if (settings.locassfun.ordersel) {
            $('#LocationAssignSingle').data('toggles').setValue(true);
        } else {
            $('#LocationAssignSingle').data('toggles').setValue(false);
        };

        if (settings.locassfun.printreprocrep) {
            $('#PrintReprocessAllocation').data('toggles').setValue(true);
        } else {
            $('#PrintReprocessAllocation').data('toggles').setValue(false);
        };

        if (settings.locassfun.autobackorder.toLowerCase()=='yes') {
            $('#AutoBackOrder').data('toggles').setValue(true);
        } else {
            $('#AutoBackOrder').data('toggles').setValue(false);
        };

        if (settings.locassfun.printpicklab) {
            $('#PrintPickLabelForEach').data('toggles').setValue(true);
            $('#PrintAllOnce_div').show();
        } else {
            $('#PrintPickLabelForEach').data('toggles').setValue(false);
            $('#PrintAllOnce_div').hide();
        };

        if (settings.locassfun.saploc.toLowerCase() == 'yes') {
            $('#CreateSAPLocChange').data('toggles').setValue(true);
        } else {
            $('#CreateSAPLocChange').data('toggles').setValue(false);
        };

        if (settings.locassfun.printbatch) {
            $('#PrintAllOnce').data('toggles').setValue(true);
        } else {
            $('#PrintAllOnce').data('toggles').setValue(false);
        };
    });
};

function saveWorkstationSettings() {
    var printRep = $('#reportPrinter').val();
    var printLab = $('#labelPrinter').val();

    if (!printRep) {
        printRep="No Printer"
    };

    if (!printLab) {
        printLab="No Printer"
    };
    preferencesHub.server.updateWorkStationSettings($('#cartonFlowNumbers').val(), $('#podNumbers').val(), $('#SVPicks').data('toggles').active, $('#SVCounts').data('toggles').active,
                                                    $('#SVPuts').data('toggles').active, printRep,
                                                   printLab, $('#AllowQuick').data('toggles').active, $('#DefaultQuick').data('toggles').active).done(function (success) {
                                                       if (!success) {
                                                           alert("Save failed an error occurred saving workstation settings");
                                                       };
                                                   });
                                                  
};

function saveToteManagement() {
    preferencesHub.server.updateToteManagement($('#PickToTotes').data('toggles').active, $('#PutAwayFromTotes').data('toggles').active, $('#AutoPrintTote').data('toggles').active,
                                                   $('#BatchCarPut').data('toggles').active, $('#BatchHotPut').val(), $('#CarPickToteManifest').data('toggles').active,
                                                   $('#OffCarPickToteManifest').data('toggles').active, $('#AutoPrintOffCar').data('toggles').active).done(function (success) {
                                                       if (!success) {
                                                           alert("Save failed an error occurred saving tote management");
                                                       };
                                                   });

};

function saveLocAssFunctions() {
    preferencesHub.server.updateLocationAssignmentFunctions($('#LocationAssignSingle').data('toggles').active, $('#PrintReprocessAllocation').data('toggles').active,
                                                            $('#PrintPickLabelForEach').data('toggles').active, $('#PrintAllOnce').data('toggles').active, $('#PickLocAssigned').val(),
                                                            $('#DisplayBulkQty').val(), $('#AutoBackOrder').data('toggles').active, $('#CreateSAPLocChange').data('toggles').active).done(function (success) {
                                                                if (!success) {
                                                                    alert("Save failed an error occurred saving location assignment functions");
                                                                };
                                                            });

};

function saveCarSW(carSW) {
    preferencesHub.server.updateCarSW(carSW).done(function (success) {
        if (!success) {
            alert("Saving the carousel workstation setting value failed")
        };
    });
}
