// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var config = $.connection.globalConfigHub;

$(document).ready(function () {
    $('#newConnection').click(function () {
        $('#connectionDiv').append('<div class="row"> \
            <div class="col-md-3"><input name="New" class="form-control cName" value="" maxlength="50" /></div> \
            <div class="col-md-3"><input name="New" class="form-control db" value="" maxlength="50" /></div> \
            <div class="col-md-3"><input name="New" class="form-control server" maxlength="50" value="" /></div> \
            <div class="col-md-1"><button disabled name="New" class="btn btn-block btn-primary save-connection"><span class="glyphicon glyphicon-floppy-disk" style="margin-bottom:5px;"></span></button></div> \
            <div class="col-md-1"><button disabled name="New" class="btn btn-block btn-warning sqlauth-connection">Set SQL Auth</button></div> \
            <div class= "col-md-1"><button name="New" class="btn btn-block btn-danger delete-connection" style="margin-bottom:5px;"><span class="glyphicon glyphicon-remove"></span></button></div ></div > ');
        $(this).attr('disabled', 'disabled');
    })

    $(document.body).on('input', '.cName, .db, .server', function () {
        var $this = $(this);
        var connName = $this.attr('name');
        var name = $('.cName[name="' + connName + '"]').val();
        var db = $('.db[name="' + connName + '"]').val();
        var server = $('.server[name="' + connName + '"]').val();
        if (name != "" && db != "" && server != "") {
            $('[name="' + connName + '"].save-connection').removeAttr('disabled')
        } else {
            $('[name="' + connName + '"].save-connection').attr('disabled', 'disabled');
        };
    });

    $(document.body).on('click', '.sqlauth-connection', function () {
        var $this = $(this);
        var connName = $this.attr('name');
        $('#SQLAuthConnectionName').html(connName);
        $('#SQLAuthModal').modal('show');

    });


    $(document.body).on('click', '.delete-connection', function () {
        var $this = $(this);
        var connName = $this.attr('name');
        if (confirm("Are you sure you wish to delete this connnection string?")) {
            if (connName == 'New') {
                $this.parent().parent().remove();
                $('#newConnection').removeAttr('disabled');
            } else {
                config.server.deleteConnection(connName).done(function (data) {
                    if (data) {
                        $this.parent().parent().remove();
                    }
                    else {
                        alert("An Error Occured while trying to delete the Connection");
                    };
                });
            };
        };
        
    });

    $(document.body).on('click', '.save-connection', function () {
        var $this = $(this);
        var connName = $this.attr('name');
        var newValue = $('.cName[name="' + connName + '"]').val();
        console.log(connName)
        config.server.saveConnection(connName, $('.cName[name="' + connName + '"]').val(), $('.db[name="' + connName + '"]').val(), $('.server[name="' + connName + '"]').val()).done(function (data) {
            if (data == "SUCCESS") {
                $this.attr('disabled', 'disabled');
                $('.cName[name="' + connName + '"]').attr('name', newValue);
                $('.db[name="' + connName + '"]').attr('name', newValue);
                $('.server[name="' + connName + '"]').attr('name', newValue);
                $('.delete-connection[name="' + connName + '"]').attr('name', newValue);
                $('.sqlauth-connection[name="' + connName + '"]').attr('name', newValue);

                $this.attr('name', newValue);
                if (connName == 'New') {
                    $('#newConnection').removeAttr('disabled');
                    $('.sqlauth-connection[name="' + newValue + '"]').removeAttr('disabled');
                };
            } else if (data == "EXISTS") {
                alert('A Connection by this name already exists');
            } else {
                alert("An Error Occured while trying to save the Connection");
            };
        });
    });

    $('#LAconnectionDiv').change(function () {
        if ($(this).val() != "") {
            var newConnName = $(this).val()
            config.server.setLAConnectionString($(this).val()).done(function () {
                $('#lblLAConn').html(newConnName)
            })
        }
    });
})