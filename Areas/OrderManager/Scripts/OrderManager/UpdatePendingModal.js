// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OMHub = $.connection.orderManagerHub;
var columns = [];
$(document).ready(function () {
    var buttclick;
    $.each($('#OMCreateColumns').children(), function (index, element) {
        columns.push($(element).attr('value'));
    });
    //all call same modal, but have different elements disabled, so if class .edit-modal is 
    //called it decided which type of the modal will be shown
    $(document.body).on('click', '.edit-modal', function () {
        buttclick = this.id
        OMHub.server.selUserFieldData().done(function (returnData) {
            $('#UpdatePendingModal').modal('show');
            if (buttclick == "CreateOdersEditTrans") {
                $('#UpdatePendingLabel').html("Updating a transaction for " + $('#CreateOrdersOrderNum').val());
                $('#OrderNumber').val($('#CreateOrdersOrderNum').val());
                $('#OrderNumber').attr('disabled', 'disabled');
                clearAutoFills();
                autofillModal();
            } else if (buttclick == "CreateOdersAddTrans") {
                $('#UpdatePendingLabel').html("Adding a new transaction for " + $('#CreateOrdersOrderNum').val());
                $('#OrderNumber').val($('#CreateOrdersOrderNum').val());
                $('#OrderNumber').attr('disabled', 'disabled');
                clearAutoFills();
                autofillModal();
            } else {
                clearAutoFills();
                $('#UserField1').val(returnData.UserField1);
                $('#UserField2').val(returnData.UserField2);
                $('#UserField3').val(returnData.UserField3);
                $('#UserField4').val(returnData.UserField4);
                $('#UserField5').val(returnData.UserField5);
                $('#UserField6').val(returnData.UserField6);
                $('#UserField7').val(returnData.UserField7);
                $('#UserField8').val(returnData.UserField8);
                $('#UserField9').val(returnData.UserField9);
                $('#UserField10').val(returnData.UserField10);
                $('#UpdatePendingLabel').html("Adding a new order number");
                $('#OrderNumber').val("");
                $('#OrderNumber').removeAttr('disabled');
            };
        });
    });
    //initialize typeahead
    var createOrderModTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/Typeahead/getItem?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#ItemNumber').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    createOrderModTA.initialize();
    $('#ItemNumber').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "createOrderModTA",
        displayKey: 'ItemNumber',
        source: createOrderModTA.ttAdapter(),
        templates: {
            header: '<p style="width:34%;" class="typeahead-header">Item Number</p><p style="width:33%;" class="typeahead-header">Description</p><p style="width:33%;" class="typeahead-header">Unit of Measure</p>',
            suggestion: Handlebars.compile("<p style='width:34%;' class='typeahead-row'>{{ItemNumber}}</p><p style='width:33%;' class='typeahead-row'>{{Description}}</p><p style='width:33%;' class='typeahead-row'>{{UnitOfMeasure}}</p>")
        }
    }).on('typeahead:selected', function (obj, data, name) {
        $('#description').val(data.Description);
        $('#UnitMeasure').val(data.UnitOfMeasure);
        $('#OMWarehouses').data('required', data.WarehouseSensitive);
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width'));
    });

    $('#ItemNumber').on('focusout keyup', function (e) {
        var $this = $(this);
        if (((e.type == 'keyup' && e.which == 13) || e.type == 'focusout' && e.originalEvent !== undefined) && this.value.length > 0) {
            OMHub.server.getItemData(this.value).done(function (data) {
                if (data.length == 0) {
                    MessageModal("Inventory", "Item " + $this.val() + " Does not exist!", function () {
                        $this.focus();
                    })
                    $this.typeahead('val', '');
                } else {
                    if (e.type != "focusout") {
                        $this.blur();
                    }
                    $('#description').val(data[0].Description);
                    $('#UnitMeasure').val(data[0].UnitOfMeasure);
                    $('#OMWarehouses').data('required', data[0].WarehouseSensitive);
                };
            });
        }
    });

    $('#LineNumber, #TransQTY, #priority').on('focusout', function () {
        if (!$.isNumeric(this.value)) {
            this.value = 0;
        };
    });

    $('#CreateOrdersSave').click(function () {
        var whse = $('#OMWarehouses');
        if ($('#OrderNumber').val().trim() == '' || $('#ItemNumber').val().trim() == '' || $('#TransTypeDropdown').val().trim() == '') {
            MessageModal("Warning", "Order Number, Item Number and Transaction Type must be completed in order to continue.");
        } else if (whse.val().trim() == '' && whse.data('required')) {
            MessageModal('Warning', 'The selected item is warehouse sensitive.  Please set a warehouse to continue.');
        } else if (parseInt($('#TransQTY').val()) <= 0) {
            MessageModal('Warning', 'The transaction quantity for this transaction must be greater than 0.');
        } else {
            if (buttclick == "CreateOdersEditTrans") {
                //call update function and redraw table
                var id = CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("ID")];
                OMHub.server.updateOTTemp(id, $('#TransTypeDropdown').val(), $('#OMWarehouses').val(), $('#ItemNumber').val(),
                                          $('#description').val(), $('#UnitMeasure').val(), $('#TransQTY').val(), $('#LineNumber').val(),
                                          $('#priority').val(), $('#ReqDate').val(), $('#hostTransID').val(), $('#emergency').prop('checked'),
                                          $('#printLabel').prop('checked'), $('#LotNumber').val(), $('#ExpDate').val(),
                                          $('#SerialNum').val(), $('#Revision').val(), $('#BatchPickID').val(), $('#ToteID').val(),
                                          $('#Cell').val(), $('#Notes').val(), $('#UserField1').val(), $('#UserField2').val(),
                                          $('#UserField3').val(), $('#UserField4').val(), $('#UserField5').val(), $('#UserField6').val(),
                                          $('#UserField7').val(), $('#UserField8').val(), $('#UserField9').val(), $('#UserField10').val(),
                                          $('#InProcess').prop('checked'), $('#ProcessBy').val(), $('#ImportBy').val(),
                                          $('#ImportDate').val(), $('#ImportFile').val()).done(function (mess) {
                                              if (mess == "Error") {
                                                  MessageModal("Error", "An error has occurred while updating this transaction");
                                              } else {
                                                  getCreateOrdersTableData();
                                                  $('#UpdatePendingModal').modal('hide');
                                              };
                                          });
            } else {
                OMHub.server.insertOTTemp($('#OrderNumber').val(), $('#TransTypeDropdown').val(), $('#OMWarehouses').val(), $('#ItemNumber').val(),
                                          $('#description').val(), $('#UnitMeasure').val(), $('#TransQTY').val(), $('#LineNumber').val(),
                                          $('#priority').val(), $('#ReqDate').val(), $('#hostTransID').val(), $('#emergency').prop('checked'),
                                          $('#printLabel').prop('checked'), $('#LotNumber').val(), $('#ExpDate').val(),
                                          $('#SerialNum').val(), $('#Revision').val(), $('#BatchPickID').val(), $('#ToteID').val(),
                                          $('#Cell').val(), $('#Notes').val(), $('#UserField1').val(), $('#UserField2').val(),
                                          $('#UserField3').val(), $('#UserField4').val(), $('#UserField5').val(), $('#UserField6').val(),
                                          $('#UserField7').val(), $('#UserField8').val(), $('#UserField9').val(), $('#UserField10').val(),
                                          $('#InProcess').prop('checked'), $('#ProcessBy').val(), $('#ImportBy').val(),
                                          $('#ImportDate').val(), $('#ImportFile').val()).done(function (mess) {
                                              if (mess == "Error") {
                                                  MessageModal("Error", "An error has occurred while updating this transaction");
                                              } else {
                                                  if (buttclick != "CreateOdersAddTrans") {
                                                      $('#CreateOrdersOrderNum').val($('#OrderNumber').val());
                                                  };
                                                  getCreateOrdersTableData();
                                                  $('#UpdatePendingModal').modal('hide');
                                              };
                                          });
            };
        };
    });
});
//function to get whichever type of date you want, ie. expiration date or required date
function getDateVals(column) {
    var tableDate = CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf(column)];
    if (tableDate.trim() == '') {
        return '';
    } else {
        var dateparts = tableDate.split('/');
        var importDate = new Date();
        if (dateparts[0] < 10) {
            dateparts[0] = "0" + dateparts[0];
        };
        if (dateparts[1] < 10) {
            dateparts[1] = "0" + dateparts[1];
        };
        reqDate = String(dateparts[2]) + "-" + String(dateparts[0]) + "-" + String(dateparts[1]);
        return reqDate;
    };
};
// once the modal appears, autofill the tables data
function autofillModal() {
    //set other values of selected column and set select val
    $('#TransTypeDropdown').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Transaction Type")]);
    $('#OMWarehouses').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Warehouse")]);
    $('#ProcessBy').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Processing By")]);
    $('#ImportBy').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Import By")]);
    $('#ImportDate').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Import Date")]);
    $('#ImportFile').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Import Filename")]);
    $('#ItemNumber').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Item Number")]);
    $('#LotNumber').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Lot Number")]);
    $('#SerialNum').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Serial Number")]);
    $('#Revision').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Revision")]);
    $('#ToteID').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Tote ID")]);
    $('#BatchPickID').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Batch Pick ID")]);
    $('#description').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Description")]);
    $('#ExpDate').val(getDateVals("Expiration Date"));
    $('#UnitMeasure').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Unit of Measure")]);
    $('#TransQTY').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Transaction Quantity")]);
    $('#LineNumber').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Line Number")]);
    $('#priority').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Priority")]);
    $('#hostTransID').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Host Transaction ID")]);
    $('#ReqDate').val(getDateVals("Required Date"));
    $('#Cell').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Cell")]);
    $('#Notes').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Notes")]);
    //for emergency checkbox
    if (CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Emergency")] == "True") {
        $('#emergency').attr('checked', 'checked');
    } else {
        $('#emergency').removeAttr('checked');
    };
    //for label
    if (CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("Label")] == "True") {
        $('#printLabel').attr('checked', 'checked');
    } else {
        $('#printLabel').removeAttr('checked');
    };
    //for in process
    if (CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("In Process")] == "True") {
        $('#InProcess').attr('checked', 'checked');
    } else {
        $('#InProcess').removeAttr('checked');
    };
    $('#UserField1').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field1")]);
    $('#UserField2').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field2")]);
    $('#UserField3').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field3")]);
    $('#UserField4').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field4")]);
    $('#UserField5').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field5")]);
    $('#UserField6').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field6")]);
    $('#UserField7').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field7")]);
    $('#UserField8').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field8")]);
    $('#UserField9').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field9")]);
    $('#UserField10').val(CreateOrdersTable.row($('#CreateOrdersTable tbody tr.active')[0]).data()[columns.indexOf("User Field10")]);
};
//clear all the data set to open string
function clearAutoFills() {
    //set other values of selected column and set select val
    $('#TransTypeDropdown').val("");
    $('#OMWarehouses').val("");
    //$('#ProcessBy').val("");
    //$('#ImportBy').val("");
    //$('#ImportDate').val("");
    //$('#ImportFile').val("");
    $('#ItemNumber').val("");
    $('#LotNumber').val("");
    $('#SerialNum').val("");
    $('#Revision').val("");
    $('#ToteID').val("");
    $('#BatchPickID').val("");
    $('#description').val("");
    $('#ExpDate').val("");
    $('#UnitMeasure').val("");
    $('#TransQTY').val("1");
    $('#LineNumber').val("0");
    $('#priority').val("0");
    $('#hostTransID').val("");
    $('#ReqDate').val("");
    $('#Cell').val("");
    $('#Notes').val("");
    //for emergency checkbox
    $('#emergency').removeAttr('checked');
    //for labels
    $('#printLabel').removeAttr('checked');
    //for in process
    $('#InProcess').removeAttr('checked');
    $('#UserField1').val("");
    $('#UserField2').val("");
    $('#UserField3').val("");
    $('#UserField4').val("");
    $('#UserField5').val("");
    $('#UserField6').val("");
    $('#UserField7').val("");
    $('#UserField8').val("");
    $('#UserField9').val("");
    $('#UserField10').val("");
};