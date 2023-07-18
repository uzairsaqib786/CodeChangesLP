// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var manageDataHub = $.connection.manageDataHub;
$(document).ready(function () {
   //Resizes the modal ot fit the screen better
    addResize(function () {
        $('#XferFileFieldMapContainer').css({
            'max-height': $(window).height() * 0.65,
            'overflow-y': 'scroll'
        });
    });

    //Enables ave button on an input
    $(document.body).on('input', '[name="Field"]', function () {
        var $this = $(this);
        $this.parent().siblings().children('[name="SaveChanges"]').removeAttr('disabled');
    });

    //Sets the field value to the system value
    $(document.body).on('click', '[name="UseSystemName"]', function () {
        var $this = $(this);
        var system = $this.parent().siblings().children('[name="System"]').val()
        $this.parent().siblings().children('[name="Field"]').val(system);
        $this.parent().siblings().children('[name="Field"]').trigger('input');
    });

    //Clears the corresponfing field value
    $(document.body).on('click', '[name="Clear"]', function () {
        var $this = $(this);
        if ($this.parent().siblings().children('[name="Field"]').val() != "") {
            $this.parent().siblings().children('[name="Field"]').val("");
            $this.parent().siblings().children('[name="Field"]').trigger('input');
        };
    });

    //Saves any changes made to the desired row
    $(document.body).on('click', '[name="SaveChanges"]', function () {
        var $this = $(this);
        var table = $('#XferFileFieldMapTable').val();
        var field= $this.parent().siblings().children('[name="Field"]').val();
        var sytemfield= $this.parent().siblings().children('[name="System"]').val();
        manageDataHub.server.updateXferFileField(table, sytemfield, field).done(function (result) {
            if (!result) {
                MessageModal("Error", "An error has occurred updating the desired field");
            } else {
                $this.attr('disabled', 'disabled');
            };
        });
        
    });

    //Changes the datatset when the table is changed
    $('#XferFileFieldMapTable').change(function () {
        FillFieldMapContainer();
    });

});

//Populates the contaier with the desired data for the selected table
function FillFieldMapContainer() {
    manageDataHub.server.getXferFileFieldMapData($('#XferFileFieldMapTable').val()).done(function (data) {
        if (data[0].field == "Error") {
            MessageModal("Error", "An error has occurred getting the file field mappings for the selected table")
        } else {
            $('#XferFileFieldMapContainer').html("");

            //Loops over all the records
            for (var i = 0; i < data.length; i++) {
                $('#XferFileFieldMapContainer').append(
                        '<div class="row top-spacer" style="padding-bottom:1px;">' +
                            '<div class="col-md-5">' +
                               '<input type="text" name="System" readonly="readonly" class="form-control" value="' + data[i].system + '"' + '/>' +
                            '</div>' +
                            '<div class="col-md-5">' +
                                '<input type="text" name="Field" maxlength="50" class="form-control" ' + (data[i].field != "" ? 'value="' + data[i].field + '"' : "") + '/>' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<button type="button" class="btn btn-primary" name="UseSystemName">Use System Name</button> ' +
                                '<button type="button" class="btn btn-primary" name="Clear">Clear</button> ' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" disabled title="Save Changes" class="btn btn-primary" name="SaveChanges"><span class="glyphicon glyphicon-floppy-disk"></span></button> ' +
                            '</div>' +
                        '</div>'
                    );
            };
        };
    });
};

