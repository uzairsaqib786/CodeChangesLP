// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    var TPZones = [];
    var ZoneBH;

    $('#WSZonesModal').on('show.bs.modal', function (e) {
        var appendstring = '';
        PickToteHub.server.selectWSPickZones().done(function (Zones) {
            for (var x = 0; x < Zones.length; x++) {
                appendstring += '<div class="WSZoneCont" style="padding-top:10px;"><div class="row"><div class="col-md-9" name="value"><input readonly="readonly" class="form-control InputWSZone" maxlength="50" name="' + Zones[x] + '" value="' + Zones[x] + '" /></div>' +
                               '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger RemoveWSZone" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                                '</div></div>';
            };
            $('#WSZoneContainer').append(appendstring);
        });

        PickToteHub.server.selectLocationZones().done(function (ZoneList) {
            for (var x = 0; x < ZoneList.length; x++) {
                TPZones.push({Zone:ZoneList[x]});
            };
            
            ZoneBH = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Zone'),
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                limit: 20,
                local: TPZones
            });
            ZoneBH.initialize();

            $('#TAZone').typeahead({
                hint: false,
                highlight: false,
                minLength: 1
            }, {
                name: "ZoneBH",
                displayKey: 'Zone',
                source: ZoneBH.ttAdapter(),
                templates: {
                    suggestion: Handlebars.compile('<p class="typeahead-row" style="width:100%;">{{Zone}}</p>')
                    //suggestion: function (e) { return '<p class="typeahead-row">' + e.Zone + '</p>'; }
                }
            }).on('typeahead:selected', function (obj, datum, name) {
                
                AddZone(datum.Zone)
                $('#TAZone').typeahead('val', '');
                $('#TAZone').val('');
            }).on('typeahead:opened', function () {
                //Dont do anything on open. Leaving it for now just in case
            });
        });
    });

    $('#WSZonesModal').on('shown.bs.modal', function (e) {
        $('#TAZone').focus();
    });

    $('#TAZone').keyup(function (e) {
        var ZoneVal = $('#TAZone').val();
        var Exists=false
        if (e.keyCode === 13 && ZoneVal !="") {
            for (var x = 0; x < TPZones.length; x++) {
                if (TPZones[x].Zone == ZoneVal) {
                    Exists = true;
                };
            };

            if (Exists) {
                AddZone(ZoneVal);
            } else {
                MessageModal("Invalid Zone", "This zone does not exist", function () {
                    $('#TAZone').typeahead('val', '');
                    $('#TAZone').val('');
                    $('#TAZone').focus();
                });
            };

            $('#TAZone').typeahead('val', '');
            $('#TAZone').val('');
            $('#TAZone').focus();
        };
    });

    function AddZone(ZoneToAdd) {
        var conflict = false;

        $('.InputWSZone:not(.NewWSZone)').each(function () {
            if (ZoneToAdd == $(this).val()) {
                conflict = true;
                MessageModal("Zone Already Selected", "This Zone is already selected for this workstation", function () { $('#TAZone').focus(); });
            };
        });

        if (!conflict) {
            PickToteHub.server.insertWSPickZone(ZoneToAdd).done(function (success) {
                if (success) {
                    var appendstring = '<div class="WSZoneCont" style="padding-top:10px;"><div class="row"><div class="col-md-9" name="value"><input readonly="readonly" class="form-control InputWSZone" maxlength="50" name="' + ZoneToAdd + '" value="' + ZoneToAdd + '" /></div>' +
                        '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger RemoveWSZone" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                        '</div></div>';

                    $('#WSZoneContainer').append(appendstring);
                    $('#TAZone').focus();
                } else {
                    MessageModal("Save Failed", "Failed to save this zone", function () { $('#TAZone').focus(); });
                };
            });
        };

        
    };

    //$('#AddWSZone').click(function () {
    //    $(this).attr('disabled', 'disabled');
    //    var appendstring = '';

    //    var ZoneDrop = ''

    //    PickToteHub.server.selectLocationZones().done(function (ZoneList) {
    //        var Zones = '';
    //        for (var x = 0; x < ZoneList.length; x++) {
    //            Zones += '<option value="' + ZoneList[x] + '">' + ZoneList[x] + '</option>';
    //        };

    //        ZoneDrop= '<div class="col-md-9" name="value"><select class="InputWSZone form-control NewWSZone" name="" >' + Zones + '</select></div>';

    //        appendstring = '<div class="WSZoneCont" style="padding-top:10px;"><div class="row">' + ZoneDrop +
    //       '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger RemoveWSZone NewWSZone" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
    //       '<div class="col-md-1" name="save"><button type="button" class="btn btn-primary SaveWSZone NewWSZone" data-toggle="tooltip" data-placement="top" data-original-title="Save Zone"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
    //       '</div></div>';

    //        $('#WSZoneContainer').append(appendstring);

    //    });
    //});

    $('#ClearAllZones').click(function () {
        MessageModal("Remove Zones", "Remove All Zones  for this workstation?", function () { $('#TAZone').focus(); }, undefined,
            function () {
                PickToteHub.server.deleteAllWSPickZone().done(function (success) {
                    if (success) {
                        $('#WSZoneContainer').html('');
                    } else {
                        MessageModal("Remove Failed", "Failed to remove Zones from workstation", function () { $('#TAZone').focus(); });
                    };
                });
            }
        );
    });


    $(document.body).on('click', '.RemoveWSZone', function () {
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        var Zone = $this.parent().siblings('[name="value"]').children();
        var container = $this.parent().parent().parent();
        if ($this.hasClass('NewWSZone')) {
            container.remove();
            $('#AddWSZone').removeAttr('disabled');
        } else {
            MessageModal("Remove Zone", "Remove Zone " + Zone.attr('name') + " from picking for this workstation?", function () { $('#TAZone').focus(); }, undefined,
                function () {
                    PickToteHub.server.deleteWSPickZone(Zone.attr('name')).done(function (success) {
                        if (success) {
                            container.remove();
                        } else {
                            MessageModal("Remove Failed", "Failed to remove Zone: " + Zone.attr('name') + " from workstation", function () { $('#TAZone').focus(); });
                            $this.removeAttr('disabled');
                        };
                    });
                }
            );
            $this.removeAttr('disabled');
        };
    });

    //$(document.body).on('click', '.SaveWSZone', function () {
    //    var $this = $(this);
    //    var remove = $this.parent().siblings('[name="remove"]').children();
    //    var Zone = $this.parent().siblings('[name="value"]').children();
    //    var conflict=false;

    //    $('.InputWSZone:not(.NewWSZone)').each(function(){
    //        if(Zone.val() == $(this).val()){
    //            conflict=true;
    //            MessageModal("Zone Already Selected", "This Zone is already selected for this workstation");
    //        };
    //    });

    //    if(!conflict){
    //        PickToteHub.server.insertWSPickZone(Zone.val()).done(function (success) {
    //            if (success) {
    //                $('.NewWSZone').removeClass('NewWSZone');
    //                $('#AddWSZone').removeAttr('disabled');
    //                Zone.attr('disabled', 'disabled');
    //                Zone.attr('name', Zone.val());
    //                $this.remove();
    //            } else {
    //                MessageModal("Save Failed", "Failed to save this zone");
    //            };
    //        });
    //    };
    //});

    $('#WSZonesModalDismiss').click(function () {
        //$('#AddWSZone').removeAttr('disabled');

        var Zones='Zones: '

        $('.InputWSZone:not(.NewWSZone)').each(function () {
            Zones += $(this).val() + ' ';
        });

        Zones = Zones.substring(0, Zones.length - 1);

        $('#ZonesPanelTitle').html(Zones);
        $('#ClearAllOrders').click();
        $('#WSZoneContainer').html('');
    });


});