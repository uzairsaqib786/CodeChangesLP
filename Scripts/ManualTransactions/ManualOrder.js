// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    $('#ManOrderView').on('shown.bs.tab', function () {
        $('#GenOrderNumber').focus();
    });

    $('#GenOrderTransModal').on('shown.bs.modal', function (e) {
        $('#GenOrderTransItemNum').focus();
    })

    var GenOrder = $('#GenOrderNumber');
    var GenOrderNumber = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: '/ManualTransactions/getManualOrderTA?',
            replace: function (url, uriencodedstring) {
                return url + 'OrderNumber=' + $('#GenOrderNumber').val() + '&TransType=' + $('#GenOrderTransType').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    GenOrderNumber.initialize();

    GenOrder.typeahead({
        hint: false,
        highlight: false,
        minLength: 1
    }, {
        name: "GenOrderNumber",
        displayKey: 'OrderNumber',
        source: GenOrderNumber.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Order Number</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:100%;">{{OrderNumber}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width')).css('left', 'auto');
    }).on('typeahead:selected', function (obj, data, name) {
        var e = jQuery.Event("keypress");
        e.which = 13;
        e.keyCode = 13;
        GenOrder.trigger(e);
    });

    var TransTable = $('#GenOrderTransTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/ManualTransactions/getOrderTable",
            "data": function (d) {
                d.ordernum = $('#GenOrderDispOrder').val();
                d.transtype = $('#GenOrderDispTransType').val();
            }
        },
        "columnDefs": [
            { targets: [0], visible: false }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });
    $('#GenOrderTransTable').wrap('<div style="overflow-x:scroll"></div>');

    //on table draw denable/disable buttons
    TransTable.on('draw', function () {
        $('#GenEditTrans, #GenDeleteTrans').attr('disabled', 'disabled');
        if ($('#GenOrderTransTable').DataTable().data().length ==0) {
            $('#GenOrderToteID, #GenOrderPost, #GenDeleteOrder').attr('disabled', 'disabled')
        }else{
            $('#GenOrderToteID, #GenOrderPost, #GenDeleteOrder').removeAttr('disabled')
        };
    });

    // handles Table Click Events for Selecting Rows to be Deleted
    $('#GenOrderTransTable tbody').on('click', 'tr', function () {
        var $this = $(this); if ($this.children('td').hasClass('dataTables_empty')) { return };
        if ($this.hasClass('active')) {
            $this.removeClass('active');
            $('#GenEditTrans').attr('disabled', 'disabled');
            $('#GenDeleteTrans').attr('disabled', 'disabled');
        } else {
            TransTable.$('tr.active').removeClass('active');
            $('#GenEditTrans').removeAttr('disabled');
            $('#GenDeleteTrans').removeAttr('disabled');
            $this.addClass('active');
        };
    });

    $('#GenOrderNumber').on('keypress', function (e) {
        if (e.which == 13) {
            //check for mismatch trasnaction types
            MTHub.server.validateManOrder($(this).val(), $('#GenOrderTransType').val()).done(function (success) {
                if (success) {
                    $('#GenOrderDispOrder').val($('#GenOrderNumber').val());
                    $('#GenOrderDispTransType').val($('#GenOrderTransType').val());
                    $('#GenAddTrans').removeAttr('disabled');
                    TransTable.draw();
                } else {
                    $('#GenAddTrans').attr('disabled', 'disabled');
                    MessageModal("Invalid Order and Transaction Type", "Order: " + $('#GenOrderNumber').val() + ", already started with a different transaction type");
                };

                $('#GenOrderNumber').val('');
                $('#GenOrderNumber').typeahead('val', '');
            });
        };
    })

    $('#GenDeleteOrder').click(function () {
        MessageModal("Delete Order?", "Delete Order: " + $('#GenOrderDispOrder').val() + "? This will remove all manual transactions for this order", undefined, undefined,
            function () {
                MTHub.server.deleteManOrder($('#GenOrderDispOrder').val()).done(function (success) {
                    if (success) {
                        $('#GenOrderDispOrder, #GenOrderDispTransType, #GenOrderToteID').val('');
                        $('#GenAddTrans').attr('disabled', 'disabled');
                        $('#GenOrderNumber').focus();
                        GenOrderNumber.clearRemoteCache();
                        TransTable.draw();
                    } else {
                        MessageModal("Error Deleting Order", "There was an error deleting this order");
                    };
                });
        });
    });

    $('#GenDeleteTrans').click(function () {
        MessageModal("Delete Selected Transaction?", "Delete this transaction?", undefined, undefined,
            function () {
                var SelRowData = $('#GenOrderTransTable').DataTable().row($('tr.active')).data();
                MTHub.server.deleteTransaction(SelRowData[0]).done(function () {
                    GenOrderNumber.clearRemoteCache();
                    TransTable.draw();
                });
        });
    });

    $('#GenOrderPost').click(function () {
        MessageModal("Post Order with this Tote ID?", "This will move all transactions to Open Transactions for this order with the filled in tote id value.", undefined, undefined,
            function () {
                MTHub.server.postManOrder($('#GenOrderDispOrder').val(), $('#GenOrderToteID').val()).done(function (Succ) {
                    if (Succ) {
                        $('#GenOrderDispOrder, #GenOrderDispTransType, #GenOrderToteID').val('');
                        $('#GenAddTrans').attr('disabled', 'disabled');
                        $('#GenOrderNumber').focus();
                        GenOrderNumber.clearRemoteCache();
                        TransTable.draw();
                    } else {
                        MessageModal("Error Posting Order", "There was an error posting this order");
                    };
                });
            });
    });


    $('#GenAddTrans').click(function () {
        $('#GenOrderTransModalTitle').html('Add New Transaction');
        ClearModalFields();
        $('#GenOrderTransModal').modal('show');
    });

    $('#GenEditTrans').click(function () {
        $('#GenOrderTransModalTitle').html('Edit Selected Transaction');
        PopulateModalFields();
        $('#GenOrderTransModal').modal('show');
    });


    $('#GenOrderTransItemNum').on('keypress', function (e) {
        if (e.which == 13) {
            $('#GenOrderTransItemNum').blur();
        };
    });

    $('#GenOrderTransItemNum').on("blur", function() {
        if ($('#GenOrderTransItemNum').val() != "") {
            //validate/get item for entered value
            MTHub.server.selItemFromVal($('#GenOrderTransItemNum').val()).done(function (Item) {
                if (Item != "Invalid") {
                    $('#GenOrderTransItemNum').val(Item);
                    $('#GenOrderTransQty').focus();
                } else {
                    MessageModal("Invalid Value Entered", "No item could be found corresponding to the value: " + $('#GenOrderTransItemNum').val(), function () { $('#GenOrderTransItemNum').val('').focus(); });
                };

            });

        };
    });


    $('#GenOrderTransModalSave').click(function () {
        //validate info in required fields

        if ($('#GenOrderTransItemNum').val() == "" || $('#GenOrderTransQty').val() == "") {
            MessageModal("Required Info Needed", "Please Fill out the Item Number and Quantity", function () { $('#GenOrderTransItemNum').focus(); });

        } else {
            if ($('#GenOrderTransModalTitle').html() == 'Add New Transaction') {
                //adding trasnaction. add trans and then clear modal
                MTHub.server.insertTransForOrder($('#GenOrderDispOrder').val(), $('#GenOrderDispTransType').val(), $('#GenOrderTransItemNum').val(), $('#GenOrderTransQty').val(),
                                                 $('#GenOrderTransReqDate').val(), $('#GenOrderTransExpDate').val(), $('#GenOrderTransLineNum').val(),
                                                 $('#GenOrderTransLineSeq').val(), $('#GenOrderTransPriority').val(), $('#GenOrderTransToteNum').val(),
                                                 $('#GenOrderTransBatchPickID').val(), $('#GenOrderTransWarehouse').val(), $('#GenOrderTransLotNumber').val(),
                                                 $('#GenOrderTransSerialNumber').val(), $('#GenOrderTransHostTransID').val(), $('#GenOrderTransEmergency').val(),
                                                 $('#GenOrderTransNotes').val(), $('#GenOrderTransUserField1').val(), $('#GenOrderTransUserField2').val(),
                                                 $('#GenOrderTransUserField3').val(), $('#GenOrderTransUserField4').val(), $('#GenOrderTransUserField5').val(),
                                                 $('#GenOrderTransUserField6').val(), $('#GenOrderTransUserField7').val(), $('#GenOrderTransUserField8').val(),
                                                 $('#GenOrderTransUserField9').val(), $('#GenOrderTransUserField10').val()).done(function (Succ) {
                                                     if (Succ) {
                                                         ClearModalFields();
                                                         TransTable.draw();
                                                         GenOrderNumber.clearRemoteCache();
                                                         MessageModal("Transaction Added", "Tranasction was sucessfully added", function () { $('#GenOrderTransItemNum').focus(); });
                                                     } else {
                                                         MessageModal("Error Adding Transaction", "There was an error adding this transaction to the order. Please try again");
                                                     };
                                                 });

            } else {
                //editting trasnaction. edit trans and then close modal
                var SelRowData = $('#GenOrderTransTable').DataTable().row($('tr.active')).data();
                MTHub.server.updateTransForOrder(SelRowData[0], $('#GenOrderTransItemNum').val(), $('#GenOrderTransQty').val(),
                                                 $('#GenOrderTransReqDate').val(), $('#GenOrderTransExpDate').val(), $('#GenOrderTransLineNum').val(),
                                                 $('#GenOrderTransLineSeq').val(), $('#GenOrderTransPriority').val(), $('#GenOrderTransToteNum').val(),
                                                 $('#GenOrderTransBatchPickID').val(), $('#GenOrderTransWarehouse').val(), $('#GenOrderTransLotNumber').val(),
                                                 $('#GenOrderTransSerialNumber').val(), $('#GenOrderTransHostTransID').val(), $('#GenOrderTransEmergency').val(),
                                                 $('#GenOrderTransNotes').val(), $('#GenOrderTransUserField1').val(), $('#GenOrderTransUserField2').val(),
                                                 $('#GenOrderTransUserField3').val(), $('#GenOrderTransUserField4').val(), $('#GenOrderTransUserField5').val(),
                                                 $('#GenOrderTransUserField6').val(), $('#GenOrderTransUserField7').val(), $('#GenOrderTransUserField8').val(),
                                                 $('#GenOrderTransUserField9').val(), $('#GenOrderTransUserField10').val()).done(function (Succ) {
                                                     if (Succ) {
                                                         ClearModalFields();
                                                         TransTable.draw();
                                                         $('#GenOrderTransModal').modal('hide');
                                                     } else {
                                                         MessageModal("Error Updating Transaction", "There was an error updating this transaction. Please try again");
                                                     };
                                                 });
            };
        };

      
    });

});

function PopulateModalFields() {
    var SelRowData = $('#GenOrderTransTable').DataTable().row($('tr.active')).data();
    //First field is hidden ID column
    //populat fields with selected table record data
    $('#GenOrderTransItemNum').val(SelRowData[1]);
    $('#GenOrderTransQty').val(SelRowData[2]);
    $('#GenOrderTransLineNum').val(SelRowData[3]);
    $('#GenOrderTransLineSeq').val(SelRowData[4]);
    $('#GenOrderTransPriority').val(SelRowData[5]);
    $('#GenOrderTransReqDate').val(SelRowData[6]);
    $('#GenOrderTransLotNumber').val(SelRowData[7]);
    $('#GenOrderTransExpDate').val(SelRowData[8]);
    $('#GenOrderTransSerialNumber').val(SelRowData[9]);
    $('#GenOrderTransWarehouse').val(SelRowData[10]);
    $('#GenOrderTransBatchPickID').val(SelRowData[11]);
    $('#GenOrderTransNotes').val(SelRowData[12]);
    $('#GenOrderTransToteNum').val(SelRowData[13]);
    $('#GenOrderTransHostTransID').val(SelRowData[14]);

    if (SelRowData[15] == "False") {
        $('#GenOrderTransEmergency').val("No");
    } else {
        $('#GenOrderTransEmergency').val("Yes");
    };

    $('#GenOrderTransUserField1').val(SelRowData[16]);
    $('#GenOrderTransUserField2').val(SelRowData[17]);
    $('#GenOrderTransUserField3').val(SelRowData[18]);
    $('#GenOrderTransUserField4').val(SelRowData[19]);
    $('#GenOrderTransUserField5').val(SelRowData[20]);
    $('#GenOrderTransUserField6').val(SelRowData[21]);
    $('#GenOrderTransUserField7').val(SelRowData[22]);
    $('#GenOrderTransUserField8').val(SelRowData[23]);
    $('#GenOrderTransUserField9').val(SelRowData[24]);
    $('#GenOrderTransUserField10').val(SelRowData[25]);
};

function ClearModalFields() {
    //clear all fields in modal and focus item number input
    $('#GenOrderTransItemNum, #GenOrderTransQty').val('');
    $('#GenOrderTransReqDate, #GenOrderTransExpDate').val('');
    $('#GenOrderTransLineNum, #GenOrderTransLineSeq, #GenOrderTransPriority, #GenOrderTransToteNum').val('');
    $('#GenOrderTransBatchPickID, #GenOrderTransWarehouse, #GenOrderTransLotNumber, #GenOrderTransSerialNumber').val('');
    $('#GenOrderTransHostTransID, #GenOrderTransNotes').val('');
    $('#GenOrderTransEmergency').val('No');
    $('#GenOrderTransUserField1, #GenOrderTransUserField2, #GenOrderTransUserField3, #GenOrderTransUserField4').val('');
    $('#GenOrderTransUserField5, #GenOrderTransUserField6, #GenOrderTransUserField7, #GenOrderTransUserField8').val('');
    $('#GenOrderTransUserField9, #GenOrderTransUserField10').val('');
    $('#GenOrderTransItemNum').focus();
};