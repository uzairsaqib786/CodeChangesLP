// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

triggerModal = function (name, sender) {
    var viewName = name
    var bodyString = "";
    
    currentModal = viewName;

    //bloodhounds
    var editInputs = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in InventoryMapController editInput
            url: ('/InventoryMaster/stockCodeTypeAhead?stockCode='),
            replace: function () {
                return '/InventoryMaster/stockCodeTypeAhead?stockCode=' + $('input[name="Item Number_input"]').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    var type = typeof dataObj;
                    if (type == 'string') {
                        return { Value: dataObj };
                    }
                    else { return dataObj; }
                });
            },
            cache: false
        }
    });
    editInputs.initialize();
    var footerString = '<button type="button" class="btn btn-default modalClose" data-dismiss="modal">Close</button>';
    if (viewName == "Description" || viewName == "Description_RO" || viewName == "Item Number" || viewName == 'Return to Stock Reel Quantity') {
        $('.modal-header').html('<h3 class="modal-title" id="myModalLabel">Edit ' + viewName + '</h3>');
        var outString = "";
        var headerString = "";

        switch (currentModal) {
            case "Description":
                bodyString = (document.getElementById('stockCode').value != '' ? '<h3>Description for Item Number: ' + document.getElementById('stockCode').value + '</h3>' : '') + '<textarea maxlength="255" rows="5" class="form-control no-horizontal" id="description_textarea">' + sender.val() + '</textarea>';
                currentModal = viewName;
                footerString += '<button type="button" class="btn btn-primary modalClose" id="description_submit" data-dismiss="modal">Submit</button>';
                break;
            case "Description_RO":
                bodyString = sender.val();
                currentModal = viewName;
                var item = sender[0].id.split('_')[1];
                if (item != 'new') { $('.modal-header').html('<h3>Description for Item Number ' + item + ':</h3>'); } else { $('.modal-header').html('<h3>Description for new Kit Item:</h3>'); };
                break;
            case "Item Number":
                outString = "<p style='width:50%;' class='typeahead-row'>{{ItemNumber}}</p><p style='width:50%;' class='typeahead-row'>{{Description}}</p>";
                headerString = "<p style='width:50%;' class='typeahead-header'>Item Number</p><p style='width:50%;' class='typeahead-header'>Description</p>";
                bodyString += "<input id='modalSender' type='hidden' value='" + sender[0].id + "' /><div class='row'><div id='itemNumber_alerts'></div><div class='col-md-4 col-md-offset-4 text-center'><label>Item Number</label></div></div>"
                bodyString += "<div class='row' style='margin-bottom:5px;'><div class='col-md-4 col-md-offset-4'><input maxlength='50' name='Item Number_input' class='form-control modalInput' value='" + sender.val() + "'/></div></div>";
                footerString += '<button type="button" class="btn btn-primary modalClose" id="item_submit">Submit</button>';
                break;
            case 'Return to Stock Reel Quantity':
                currentModal = viewName;
                bodyString = '<div class="row"><div class="col-md-12"><div id="RTS_alerts"></div></div></div><div class="row"><div class="col-md-6"><strong>Minimum Dollar Amount to RTS</strong></div><div class="col-md-6"><strong>RTS Threshold Max Qty</strong></div></div>' + 
                            '<div class="row"><div class="col-md-6"><div class="input-group"><span class="input-group-addon">$</span><input maxlength="11" type="text" class="form-control" placeholder="Dollar Amount" id="minDollarRTS" value="0"></div></div><div class="col-md-6"><input type="text" id="thresholdQty" placeholder="Maximum Quantity" class="form-control" maxlength="9" value="0"></div></div>';
                footerString += '<button type="button" class="btn btn-primary modalClose" disabled="disabled" id="modalRTSSubmit">Submit</button>';
                break;
        }
        $("#myModal .modal-body").html(bodyString);

        $('#myModal .modal-footer').html(footerString);
        $('#myModal').modal('show');
        if (viewName == 'Item Number') {
            $('input[name="Item Number_input"]').typeahead({
                hint: false,
                highlight: true,
                minLength: 1
            }, {
                name: "editInputs",
                displayKey: 'ItemNumber',
                source: editInputs.ttAdapter(),
                templates: {
                    header: headerString,
                    suggestion: Handlebars.compile(outString)
                }
            })
        };
    }
    else {
        invMasterHub.server.getEditView(viewName).done(function (data) {
            $('.modal-header.dynamic').html('<h3 class="modal-title" id="myModalLabel">Add/Edit ' + viewName == 'Category' ? 'Categorie' : viewName + 's</h4>');
            bodyString += "<div id='saveModalAlert'></div>"
            switch (currentModal) {
                case "Cell Size":
                    bodyString += "<input id='modalSender' type='hidden' value='" + sender[0].id + "' /><div class='row' style='margin-bottom: 10px;'><div class='col-md-2'><strong>Cell Size</strong></div> <div class='col-md-8'><strong>Cell Type</strong></div><div class='col-md-offset-1 col-md-1'><button id='addLinePopUp' type='button' data-toggle='tooltip' data-placement='top' title='Add New Cell Size' class='btn btn-primary'><span class='glyphicon glyphicon-plus'></span></button></div></div>"
                    for (var x = 0; x < data.length; x++) {
                        bodyString += "<div class='row' style='margin-bottom:5px;'><div class='col-md-2'><input maxlength='50' name='" + data[x].CellSize + "_input' class='form-control modalInput' value='" + data[x].CellSize + "'/></div><div class='col-md-7'><input name='" + data[x].CellSize + "_description' class='form-control modalInput' value='" + data[x].CellType + "'/></div><div class='col-md-1'><button name='" + data[x].CellSize + "_delete' type='button' data-toggle='tooltip' data-placement='top' title='Delete' class='btn btn-danger remove'><span class='glyphicon glyphicon-remove'></span></button></div><div class='col-md-1'><button  name='" + data[x].CellSize + "_set' type='button' data-toggle='tooltip' data-placement='right' title='Set as Value' class='btn btn-primary set'><span class='glyphicon glyphicon-edit'></span></button></div><div class='col-md-1'><button disabled name='" + data[x].CellSize + "_save' type='button' data-toggle='tooltip' data-placement='right' title='Save Changes' class='btn btn-primary save'><span class='glyphicon glyphicon-floppy-disk'></span></button></div></div>";
                    };
                    break;
                case "Velocity Code":
                    bodyString += "<input id='modalSender' type='hidden' value='" + sender[0].id + "' /><div class='row' style='margin-bottom: 10px;'><div class='col-md-10'><strong>Velocity Code</strong></div><div class='col-md-offset-1 col-md-1'><button id='addLinePopUp' type='button' data-toggle='tooltip' data-placement='top' title='Add New Velocity Code' class='btn btn-primary'><span class='glyphicon glyphicon-plus'></span></button></div></div>"
                    for (var x = 0; x < data.length; x++) {
                        bodyString += "<div class='row' style='margin-bottom:5px;'><div class='col-md-9'><input maxlength='50' name='" + data[x] + "_input' class='form-control modalInput' value='" + data[x] + "'/></div><div class='col-md-1'><button name='" + data[x] + "_delete' type='button' data-toggle='tooltip' data-placement='top' title='Delete' class='btn btn-danger remove'><span class='glyphicon glyphicon-remove'></span></button></div><div class='col-md-1'><button name='" + data[x] + "_set' type='button' data-toggle='tooltip' data-placement='right' title='Set as Value' class='btn btn-primary set'><span class='glyphicon glyphicon-edit'></span></button></div><div class='col-md-1'><button disabled  name='" + data[x] + "_save' type='button' data-toggle='tooltip' data-placement='right' title='Save Changes' class='btn btn-primary save'><span class='glyphicon glyphicon-floppy-disk'></span></button></div></div>";
                    };
                    break;
                case "Scan Type":
                    bodyString += "<input id='modalSender' type='hidden' value='" + sender[0].id + "' /><div class='row' style='margin-bottom: 10px;'><div class='col-md-10'><strong>Scan Type</strong></div><div class='col-md-offset-1 col-md-1'><button id='addScanType' type='button' data-toggle='tooltip' data-placement='top' title='Add New Scan Type' class='btn btn-primary'><span class='glyphicon glyphicon-plus'></span></button></div></div><div id='modalScanCodes_container'>";
                    for (var x = 0; x < data.length; x++) {
                        bodyString += "<div class='row' id='" + x + "_stcontainer' style='margin-bottom:5px;'><div class='col-md-9'><input maxlength='50' id='" + x + "_stmodal' name='" + data[x] + "' class='form-control modalInput' value='" + data[x] + "'/></div><div class='col-md-1'><button id='" + x + "_stremove' type='button' data-toggle='tooltip' data-placement='top' title='Delete' class='btn btn-danger remove-stype'><span class='glyphicon glyphicon-remove'></span></button></div><div class='col-md-1'><button id='" + x + "_stset' type='button' data-toggle='tooltip' data-placement='right' title='Set as Value' class='btn btn-primary set-stype'><span class='glyphicon glyphicon-edit'></span></button></div><div class='col-md-1'><button disabled id='" + x + "_stsave' type='button' data-toggle='tooltip' data-placement='right' title='Save Changes' class='btn btn-primary save-stype'><span class='glyphicon glyphicon-floppy-disk'></span></button></div></div>";
                    };
                    bodyString += '</div>';
                    break;
                case "Unit of Measure":
                    bodyString += "<input id='modalSender' type='hidden' value='" + sender[0].id + "' /><div class='row' style='margin-bottom: 10px;'><div class='col-md-10'><strong>Unit of Measure </strong></div><div class='col-md-offset-1 col-md-1'><button id='addUoM' type='button' data-toggle='tooltip' data-placement='top' title='Add New Unit of Measure' class='btn btn-primary'><span class='glyphicon glyphicon-plus'></span></button></div></div><div id='modalUnitMeasure_container'>";
                    for (var x = 0; x < data.length; x++) {
                        bodyString += "<div class='row' id='" + x + "_umcontainer' style='margin-bottom:5px;'><div class='col-md-9'><input maxlength='50' id='" + x + "_ummodal' name='" + data[x] + "' class='form-control modalInput' value='" + data[x] + "'/></div><div class='col-md-1'><button id='" + x + "_umremove' type='button' data-toggle='tooltip' data-placement='top' title='Delete' class='btn btn-danger remove-UM'><span class='glyphicon glyphicon-remove'></span></button></div><div class='col-md-1'><button id='" + x + "_umset' type='button' data-toggle='tooltip' data-placement='right' title='Set as Value' class='btn btn-primary set-UM'><span class='glyphicon glyphicon-edit'></span></button></div><div class='col-md-1'><button disabled id='" + x + "_umsave' type='button' data-toggle='tooltip' data-placement='right' title='Save Changes' class='btn btn-primary save-UM'><span class='glyphicon glyphicon-floppy-disk'></span></button></div></div>";
                    };
                    bodyString += '</div>';
                    break;
                case "Category":
                    $('.modal-header.dynamic').html('<h3 class="modal-title" id="myModalLabel">Add/Edit Categories</h4>');
                    bodyString += "<input id='modalSender' type='hidden' value='" + sender[0].id + "' /><div class='row' style='margin-bottom: 10px;'><div class='col-md-5'><strong>Category </strong></div><div class='col-md-5'><strong>Sub Category</strong></div><div class=col-md-1></div><div class='col-md-1'><button id='addCSC' type='button' data-toggle='tooltip' data-placement='top' title='Add New Category/Sub-Category' class='btn btn-primary'><span class='glyphicon glyphicon-plus'></span></button></div></div><div id='modalCategory_container'>";
                    for (var x = 0; x < data.length; x++) {
                        bodyString += "<div class='row' id='" + x + "_csccontainer' style='margin-bottom:5px;'><div class='col-md-5'><input maxlength='50' id='" + x + "_cmodal' name='" + data[x].Category + "' class='form-control cscModalInput' value='" + data[x].Category + "'/></div><div class='col-md-4'><input maxlength='50' id='" + x + "_scmodal' name='" + data[x].SubCategory + "' class='form-control cscModalInput' value='" + data[x].SubCategory + "'/></div><div class='col-md-1'><button id='" + x + "_cscremove' type='button' data-toggle='tooltip' data-placement='top' title='Delete' class='btn btn-danger remove-csc'><span class='glyphicon glyphicon-remove'></span></button></div><div class='col-md-1'><button id='" + x + "_cscset' type='button' data-toggle='tooltip' data-placement='right' title='Set as Value' class='btn btn-primary set-csc'><span class='glyphicon glyphicon-edit'></span></button></div><div class='col-md-1'><button disabled id='" + x + "_cscsave' type='button' data-toggle='tooltip' data-placement='right' title='Save Changes' class='btn btn-primary save-csc'><span class='glyphicon glyphicon-floppy-disk'></span></button></div></div>";
                    };
                    bodyString += '</div>';
                    footerString = '<div class="row"><div class="col-md-offset-10 col-md-1"><button id="printCategories" type="button" data-toggle="tooltip" data-placement="top" title="Print Categories" class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button></div><div class="col-md-1"><button type="button" class="btn btn-default modalClose" data-dismiss="modal">Close</button></div></div>';
                    break;
            };
            $("#myModal .modal-body").html(bodyString);
            $('#myModal .modal-footer').html(footerString);
            $('.remove-csc,.save-csc,#addCSC,.set-csc,#printCategories').tooltip();
            $('#myModal').modal('show');
        });
        
    };

};

$(document).ready(function () {
    $('#updateRTSReel').click(function () {
        triggerModal('Return to Stock Reel Quantity', $(this));
    });

    $(document.body).on('input', '#minDollarRTS, #thresholdQty', function () {
        setNumericInRange($(this), 0, null);
        if (this.value.trim() != '') {
            $('#modalRTSSubmit').removeAttr('disabled');
        };
    });

    $(document.body).on('focusout', '#minDollarRTS, #thresholdQty', function () {
        if (this.value.trim() == '') {
            this.value = 0;
        };
    });

    $(document.body).on('click', '#modalRTSSubmit', function () {
        var minDollar = $('#minDollarRTS');
        var thresh = $('#thresholdQty');
        if (minDollar.val().trim() == '') { minDollar.val(0); };
        if (thresh.val().trim() == '') { thresh.val(0); };
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        invMasterHub.server.updateReelAll(minDollar.val(), thresh.val()).done(function (success) {
            if (success) {
                invMasterHub.server.refreshRTS(document.getElementById('stockCode').value).done(function (result) {
                    $('#minReelQty').val(result);
                    $('#myModal').modal('hide');
                    deleteAlert('updateRTSfailed');
                });
            } else {
                createAlert('updateRTSfailed', 'Update failed.  Try again.', 'alert-warning', true, '<strong>Alert:</strong> ', '#RTS_alerts');
            };
            $this.removeAttr('disabled');
        });
    });
});