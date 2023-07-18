// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('.toggles').toggles({
        width: 60,
        height: 25
    });
    //config to use hub 
    $(document.body).on('click', '#printer_add', function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = $('<div class="printerconfig_container" style="padding-top:10px;"><div class="row">\
                   <div class="col-md-4"><input type="text" class="form-control input-printer new-printer pName" name="New_printerinput" value="" maxlength="50"></div>\
                   <div class="col-md-4"><input type="text" class="form-control input-printeradd new-printer pAdd" name="New_addinput" value="" maxlength="100"></div>\
                   <div class="col-md-2"> <div class="toggles toggle-modern Label" data-toggle-ontext="Yes" data-toggle-offtext="No" data-toggle-on="' + false +'" data-name="label_input"></div></div>\
                   <div class="col-md-1"><button type="button" data-toggle="tooltip" data-placement="top" title="Remove" name="New" class="btn btn-danger remove-printer new-printer" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>\
                   <div class="col-md-1"><button disabled type="button" data-toggle="tooltip" data-placement="top" title="Save" name="New" class="btn btn-primary save-printer new-printer" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>\
                   </div></div>');
        appendstring.find('.toggles').toggles({
            width: 60,
            height: 25
        });
        $('#printerconfig_container').append(appendstring);
    });

    $(document.body).on('input', '.pName, .pAdd', function () {
        var $this = $(this);
        var printer = $this.attr('name').substr(0, $this.attr('name').indexOf('_'));
        var name = $('[name="' + printer + '_name"]').val();
        var add = $('[name="' + printer + '_add"]').val();
        if (name != "" && add != "") {
            $('[name="' + printer + '"].save-printer').removeAttr('disabled');
        }
        else {
            $('[name="' + printer + '"].save-printer').attr('disabled', 'disabled');
        };
    });

    $(document.body).on('toggle', '.toggles', function () {
        var $this = $(this);
        var printer = $this.data('name');
        if (printer != "") {
            $('[name="' + printer + '"].save-printer').removeAttr('disabled')
        } else {
            $('[name="' + printer + '"].save-printer').attr('disabled', 'disabled');
        };
    });


    $(document.body).on('click', '.remove-printer', function () {
        var $this = $(this);
        var printer = $this.attr('name');

        if (confirm("Are you sure you wish to delete this printer: " + printer + "?")) {
            if (printer == "New") {
                $this.parent().parent().remove();
                $('#printer_add').removeAttr('disabled');
            } else {
                config.server.deletePrinter(printer).done(function (data) {
                    if (!data) {
                        alert("Delete Failed")
                    } else {
                        $this.parent().parent().remove();
                    }
                })
            };
        };
    });

    $(document.body).on('click', '.save-printer', function () {
        var $this = $(this);
        var printer = $this.attr('name')
        
        if (printer == "New") {
            config.server.insertNewPrinter($('[name="New_printerinput"]').val(), $('[name="New_addinput"]').val(), $this.parent().siblings().children('.toggles').data('toggles').active).done(function (data) {
                if (data == '') {
                    $('#printer_add').removeAttr('disabled');
                    $this.attr('disabled', 'disabled');

                    var newName = $('[name="New_printerinput"]');
                    newName.attr('name', newName.val() + '_name')
                    $('[name="New_addinput"]').attr('name', newName.val() + '_add');
                    $this.parent().siblings().children('.toggles').data('name', newName.val());
                    $this.parent().siblings().children('.remove-printer').attr('name', newName.val());
                    $this.attr('name', newName.val());

                } else {
                    alert(data);
                };
            });
        } else {
            config.server.updateCurrentPrinter(printer, $('[name="' + printer + '_name"]').val(), $('[name="' + printer + '_add"]').val(), $this.parent().siblings().children('.toggles').data('toggles').active).done(function (data) {
                if (data != '') {
                    alert(data);
                } else {
                    $this.attr('disabled', 'disabled');
                };
            });
        };
    })

});

