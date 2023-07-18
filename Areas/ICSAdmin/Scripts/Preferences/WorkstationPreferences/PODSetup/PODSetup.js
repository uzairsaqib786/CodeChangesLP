// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var podTimer = mkTimer(function () {
        savePODSetup();
    }, 300);

    var podContainerTimer = mkTimer(function (POD) {
        savePOD(POD.zone, POD.maxorder, POD.ident);
    }, 1000);

    $('#Use20Matrix').on('toggle', function (e, checked) {
        var check = false;
        $('#PODContainer .Max').each(function (index, element) {
            if ($(element).val() > 20 && checked) {
                alert("You have existing orders over 20, in order to use 20 Position Matrix you need to set these to be lesss than 20");
                check = true;
            };
        });
        if (check) {
            setTimeout(function () {
                $('#Use20Matrix').data('toggles').setValue(false);
            }, 0);
        };
        podTimer.startTimer();
    });

    $('#PODPrefs').on('input', 'input', function () {
        setNumericInRange($('#cibDelay'), SqlLimits.numerics.int);
        podTimer.startTimer();
    });

    $('#PODPrefs').on('toggle', '.toggles', function () {
        podTimer.startTimer();
    });

    $('#PODPrefs').on('change', 'select', function () {
        podTimer.startTimer();
    });

    $('#PODContainer').on('toggle', '.toggles', function (e, checked) {
        var $this = $(this);
        podContainerTimer.startTimer({ zone: $this.data('name'), maxorder: $this.parent().siblings().find('.Max').val(), ident: $this.data('toggles').active });
        if (checked) {
            $this.parent().siblings().find('.Max').removeAttr('disabled');
        } else {
            $this.parent().siblings().find('.Max').attr('disabled', 'disabled');
        };
    });

    $('#PODContainer').on('input', 'input', function (e) {
        var $this = $(this);
        setNumericInRange($this, 0, SqlLimits.numerics.int.max);
        if ($this.val().trim() == '') { $this.val(0); };
        if ($('#Use20Matrix').data('toggles').active && parseInt($this.val()) > 20) {
            if (confirm("You have set an order greater than 20 with the Use 20 Matrix setting enabled. Press Okay to set this to No")) {
                $('#Use20Matrix').data('toggles').setValue(false);
                savePODSetup();
                podContainerTimer.startTimer({ zone: $this.parent().siblings().find('.Pod_Zone').data('name'), maxorder: $this.val(), ident: $this.parent().siblings().find('.Pod_Zone').data('toggles').active });
            };
        } else {
            podContainerTimer.startTimer({ zone: $this.parent().siblings().find('.Pod_Zone').data('name'), maxorder: $this.val(), ident: $this.parent().siblings().find('.Pod_Zone').data('toggles').active });
        };
    });
    
    [ 'Red', 'Green', 'Blue' ].forEach((color) => {
        $('#OSFilter'+color+'Field').change(function () {
            setOSFilters();
        });
        $('#OSFilter' + color + 'Value').change(function () {
            setOSFilters();
        });
    });
});

function getPODSetup() {
    preferencesHub.server.selPodSetupInfo().done(function (settings) {
        $('#ChooseCarSpinSort').val(settings.spinsort);
        $('#CartonFlowSequence').val(settings.cfseq);
        if (settings.carsw.toLowerCase() == "yes") {
            $('#CarWorkstation').data('toggles').setValue(true);
        } else {
            $('#CarWorkstation').data('toggles').setValue(false);
        };
        if (settings.pos20.toLowerCase() == "yes") {
            $('#Use20Matrix').data('toggles').setValue(true);
        } else {
            $('#Use20Matrix').data('toggles').setValue(false);
        };
        if (settings.onlt.toLowerCase() == "yes") {
            $('#ItemNumLightTree').data('toggles').setValue(true);
        } else {
            $('#ItemNumLightTree').data('toggles').setValue(false);
        };
        if (settings.pullwhenfull.toLowerCase() == "yes") {
            $('#PullWhenFull').data('toggles').setValue(true);
        } else {
            $('#PullWhenFull').data('toggles').setValue(false);
        };
        if (settings.ignoretc == "Yes") {
            $('#IgnoreTaskComplete').data('toggles').setValue(true);
        } else {
            $('#IgnoreTaskComplete').data('toggles').setValue(false);
        }
        $('#cibDelay').val(settings.cibdelay);
        preferencesHub.server.selAllPodZones().done(function (PodZones) {
            var container = $('#PODContainer');
            container.html('');
            var entries;
            $.each(PodZones, function (index, element) {
                if (element.ispodzone) {
                    entries = $('<div class="row">\
                            <div class="col-md-5">\
                               <input style="margin-top:5px;" disabled  type="text" class="form-control" value="' + element.zone + '" /> </div>\
                        <div class="col-md-5">\
                            <input style="margin-top:5px;" type="text" class="form-control Max" value="' + element.maxquants + '" /> </div>\
                        <div class="col-md-2">\
                            <div style="margin-top:7px;" class="toggles toggle-modern Pod_Zone" data-toggle-ontext="Yes" data-toggle-offtext="No" data-toggle-on="' + element.ispodzone + '" data-name="' + element.zone + '"></div>\
                        </div> </div>');
                } else {
                    entries = $('<div class="row">\
                            <div class="col-md-5">\
                               <input style="margin-top:5px;" disabled  type="text" class="form-control" value=" ' + element.zone + '" /> </div>\
                        <div class="col-md-5">\
                            <input style="margin-top:5px;" disabled type="text" class="form-control Max" value=" ' + element.maxquants + '" /> </div>\
                        <div class="col-md-2">\
                            <div style="margin-top:7px;" class="toggles toggle-modern Pod_Zone" data-toggle-ontext="Yes" data-toggle-offtext="No" data-toggle-on="' + element.ispodzone + '" data-name="' + element.zone + '"></div>\
                        </div> </div>');
                };
                
                entries.find('.toggles').toggles({
                    width: 60,
                    height: 25
                });
                entries.appendTo(container);
            });
        });
    });
};

function savePODSetup() {
   
    preferencesHub.server.updatePodSetupInfo($('#CarWorkstation').data('toggles').active, $('#ChooseCarSpinSort').val(), $('#CartonFlowSequence').val(), $('#cibDelay').val(), $('#Use20Matrix').data('toggles').active,
                                             $('#ItemNumLightTree').data('toggles').active, $('#PullWhenFull').data('toggles').active, $('#IgnoreTaskComplete').data('toggles').active).done(function (success) {
        if (!success) {
            alert("Save Failed, an error occured saving the preferences")
        };
    });
};

function savePOD(zone, maxorder, ident) {
    if (maxorder.length < 1) {
        maxorder="0"
    };
    preferencesHub.server.addDeletePodZones(zone, maxorder, ident).done(function (success) {
        if (!success) {
            alert("Operation failed. Saving the pod zones has failed")
        };
    });
};

function getOSFilters() {
    preferencesHub.server.getOSFilters().done((filters) => {
        if (!filters) {
            alert("Could not retrieve any filter values");
        } else {
            filters.forEach((f) => {
                $("#OSFilter" + f.color + "Value").val(f.criteria);
                if (f.field==null) {
                    document.getElementById("OSFilter" + f.color + "Field").selectedIndex = -1;
                } else {
                    var filterSelection = document.getElementById("OSFilter" + f.color + "Field").options.namedItem(f.color + "_" + f.field)
                    if (filterSelection == null) {
                        document.getElementById("OSFilter" + f.color + "Field").selectedIndex = -1;
                    }
                    else {
                        document.getElementById("OSFilter" + f.color + "Field").selectedIndex = filterSelection.index;

                    }
                }
                
            });
        }
    });
}
function setOSFilters() {
    var params = [];
    ["Red", "Green", "Blue"].forEach((color) => {
        var field = $("#OSFilter" + color + "Field").find(":selected").text().replace(/\s+/g, '');
        var criteria = $("#OSFilter" + color + "Value").val()
        params.push([color,field,criteria])
    })
    preferencesHub.server.setOSFilters(params).done((success) => {
        if (!success) {
            alert("Set OS Filter - Operation Failed. Could not save the parameters");
        }
    });
}