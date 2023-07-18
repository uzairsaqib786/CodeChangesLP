// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

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
    });

    $('#AddWSZone').click(function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = '';

        var ZoneDrop = ''

        PickToteHub.server.selectLocationZones().done(function (ZoneList) {
            var Zones = '';
            for (var x = 0; x < ZoneList.length; x++) {
                Zones += '<option value="' + ZoneList[x] + '">' + ZoneList[x] + '</option>';
            };

            ZoneDrop= '<div class="col-md-9" name="value"><select class="InputWSZone form-control NewWSZone" name="" >' + Zones + '</select></div>';

            appendstring = '<div class="WSZoneCont" style="padding-top:10px;"><div class="row">' + ZoneDrop +
           '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger RemoveWSZone NewWSZone" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
           '<div class="col-md-1" name="save"><button type="button" class="btn btn-primary SaveWSZone NewWSZone" data-toggle="tooltip" data-placement="top" data-original-title="Save Zone"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
           '</div></div>';

            $('#WSZoneContainer').append(appendstring);

        });
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
            MessageModal("Remove Zone", "Remove Zone " + Zone.attr('name') + " from picking for this workstation?", undefined, undefined,
                function () {
                    PickToteHub.server.deleteWSPickZone(Zone.attr('name')).done(function (success) {
                        if (success) {
                            container.remove();
                        } else {
                            MessageModal("Remove Failed", "Failed to remove Zone: " + Zone.attr('name') + " from workstation");
                            $this.removeAttr('disabled');
                        };
                    });
                }
            );
            $this.removeAttr('disabled');
        };
    });

    $(document.body).on('click', '.SaveWSZone', function () {
        var $this = $(this);
        var remove = $this.parent().siblings('[name="remove"]').children();
        var Zone = $this.parent().siblings('[name="value"]').children();
        var conflict=false;

        $('.InputWSZone:not(.NewWSZone)').each(function(){
            if(Zone.val() == $(this).val()){
                conflict=true;
                MessageModal("Zone Already Selected", "This Zone is already selected for this workstation");
            };
        });

        if(!conflict){
            PickToteHub.server.insertWSPickZone(Zone.val()).done(function (success) {
                if (success) {
                    $('.NewWSZone').removeClass('NewWSZone');
                    $('#AddWSZone').removeAttr('disabled');
                    Zone.attr('disabled', 'disabled');
                    Zone.attr('name', Zone.val());
                    $this.remove();
                } else {
                    MessageModal("Save Failed", "Failed to save this zone");
                };
            });
        };
    });


    $('#WSZonesModalDismiss').click(function () {
        $('#AddWSZone').removeAttr('disabled');

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