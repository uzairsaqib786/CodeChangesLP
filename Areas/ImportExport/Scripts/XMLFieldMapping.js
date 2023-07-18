// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var manageDataHub = $.connection.manageDataHub;

    $('#XMLFieldMap').click(function () {
        $('#XMLFieldContainer').html('');
        $('#addNewXMLNode').removeAttr('disabled');
        $('#XMLFieldMapModal').modal('show');
    });

    $('#XMLFieldMapModal').on('show.bs.modal', function (e) {
        var appendstring = '';
        var delbutt = '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger RemoveXMLNode" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>';
        var savebutt = '<div class="col-md-1" name="save"><button type="button" class="btn btn-primary SaveXMlNode" data-toggle="tooltip" data-placement="top" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>';
        //Read current xml settings and append them here. Column and type disabled, node bale ot be editted
        manageDataHub.server.selPickOTXMLData().done(function (data) {
            for (var x = 0; x < data.length; x++) {
                appendstring += '<div class="XMLNodeCont" style="padding-top:1%;" id="' + data[x].ID + '">\
                            <div class="row">\
                                <div class="col-md-3">\
                                    <input type="text" readonly="readonly" placeholder="Open Trans Column" class="form-control XMlNodeOTCol" value="' + data[x].OTField + '"/>\
                                </div>\
                                <div class="col-md-3">\
                                    <input type="text" placeholder="XML Node" class="form-control XMlNodeNode" value="' + data[x].XMLNode + '"/>\
                                </div>\
                                 <div class="col-md-3">\
                                    <input type="text" placeholder="Field Type" readonly="readonly" class="form-control XMLNodeFieldType" value="' + data[x].Field + '"/>\
                                </div>' +
                                delbutt +
                                savebutt +
                            '</div>\
                        </div>';
            };
            $('#XMLFieldContainer').append(appendstring);
        });
    });

    //Need to handle adding new one
    $('#addNewXMLNode').click(function () {
        $(this).attr('disabled', 'disabled');
        var appendstring = '';
        var typeopts = '<option value="Text">Text</option><option value="Number">Number</option><option value="Date">Date</option>';
        var typedrop = '<div class="col-md-3" name="value"><select class="XMLFieldType form-control NewXMLNode" name="" >' + typeopts + '</select></div>';

        var delbutt = '<div class="col-md-1" name="remove"><button type="button" class="btn btn-danger RemoveXMLNode NewXMLNode" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>';
        var savebutt = '<div class="col-md-1" name="save"><button type="button" class="btn btn-primary SaveXMlNode NewXMLNode" data-toggle="tooltip" data-placement="top" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>';
                               
        appendstring = '<div class="XMLNodeCont" style="padding-top:1%;">\
                            <div class="row">\
                                <div class="col-md-3">\
                                    <input type="text" placeholder="Open Trans Column" class="form-control XMlNodeOTCol"/>\
                                </div>\
                                <div class="col-md-3">\
                                    <input type="text" placeholder="XML Node" class="form-control XMlNodeNode"/>\
                                </div>' +
                                typedrop +
                                delbutt +
                                savebutt+
                            '</div>\
                        </div>';

        $('#XMLFieldContainer').append(appendstring);
    });

    //Need to handle deleting one
    $(document.body).on('click', '.RemoveXMLNode', function () {
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        var container = $this.parent().parent().parent();
        var Node = $this.parent().siblings().children('.XMlNodeNode');

        if ($this.hasClass('NewXMLNode')) {
            container.remove();
            $('#addNewXMLNode').removeAttr('disabled');
        } else {
            //delete here
            MessageModal("Remove Node", "Remove Node " + Node.val() + "?", undefined, undefined,
                  function () {
                      manageDataHub.server.delPickOTXMLData(container.attr('id')).done(function (success) {
                          if (success) {
                              container.remove();
                          } else {
                              MessageModal("Remove Failed", "Failed to remove this node");
                              $this.removeAttr('disabled');
                          };
                      });
                  }
              );
            $this.removeAttr('disabled');
        };
    });

    $(document.body).on('click', '.SaveXMlNode', function () {
        var $this = $(this);
        var remove = $this.parent().siblings('[name="remove"]').children();
        var container = $this.parent().parent().parent();

        var Field = $this.parent().siblings().children('.XMlNodeOTCol');
        var Node = $this.parent().siblings().children('.XMlNodeNode');
        var Type = $this.parent().siblings().children('.XMLFieldType');

        if ($this.hasClass('NewXMLNode')) {
            manageDataHub.server.insPickOTXMLData(Field.val(), Node.val(), Type.val()).done(function (rslt) {
                if (rslt != -1) {
                    $this.removeClass('NewXMLNode');
                    container.attr('id', rslt);
                    $('#addNewXMLNode').removeAttr('disabled');
                    remove.removeClass('NewXMLNode');
                    Field.attr('readonly', 'readonly');
                    Type.attr('disabled', 'disabled');
                } else {
                    MessageModal("Insert Failed", "Failed to insert this XML Node");
                };
            });
        } else {
            //update here
            manageDataHub.server.updPickOTXMLData(container.attr('id'), Node.val()).done(function (rslt) {
                if (!rslt) {
                    MessageModal("Update Failed", "Failed to update this XML Node");
                };
            });
        };
    });
});