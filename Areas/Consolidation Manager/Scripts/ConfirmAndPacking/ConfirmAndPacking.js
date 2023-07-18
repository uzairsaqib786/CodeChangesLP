// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var confPackHub = $.connection.confirmAndPackingHub;

var confPackTransTable, confPackToteTable


$(document).ready(function () {
    //intitialize datatable 
    confPackTransTable = $("#ShipTransTable").DataTable({
        "dom": 'trip',
        'columnDefs': [
            {
                'targets': [0, 7],
                'visible': false
            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "lengthMenu": [20]
    });

    confPackToteTable = $('#ToteInfoTable').DataTable({
        "dom": 'trp',
        "processing": true,
        'columnDefs': [
            {
                'targets': ["all"],
                'visible': false
            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "lengthMenu": [3]
    });
    //marks clicked rows as active
    $('#ShipTransTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('#ShipTransTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            enButts();
        } else {
            $this.removeClass('active');
            disButts();
        }
    });
    //next container id click event
    $('#NextContID').click(function () {
        confPackHub.server.selContIDConfirmPack($('#ConfPackOrderNum').val()).done(function (containerID) {
            if (containerID == '') {
                MessageModal("Error", "An error has occurred");
            } else {
                console.log($('#ConfPackOrderNum').val() + " " + $('#ContID').val());
                if ($('#AutoContPL').val().toString() == "True") {
                    getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPrintCont', {
                        OrderNum: $('#ConfPackOrderNum').val(),
                        contID: $('#ContID').val()
                    }, true,'report',  'Packing List - Container')
                };
                $('#ContID').val(containerID);
            };
        });
    });
    //unpack desired location
    $('#UnPack').click(function () {
        var tr = $('#ShipTransTable tbody tr.active');
        var id = confPackTransTable.row(tr[0]).data()[0];
        confPackHub.server.updShipTransUnPack(id).done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "An error has occurred");
            } else {
                $('#ShipTransTable tbody tr.active').find(':nth-child(5)').html('');
                $('#ShipTransTable tbody tr.active').find(':nth-child(7)').html('False');
                confPackTransTable.row(tr[0]).invalidate().draw();
            };
        });
    });
    //confirm all transactions
    $('#ConfirmAll').click(function () {
        var conf = confirm("Confirm All transactions? This will mark this entire order as confirmed and packed.")
        if (conf) {
            confPackHub.server.confirmAllConfPack($('#ConfPackOrderNum').val(), $('#ContID').val()).done(function (mess) {
                if (mess == "Fail") {
                    MessageModal("Error", "An error has occurred");
                } else {
                    //check preferences for auto printing
                    if ($('#AutoContLabel').val().toString() == "True") {
                        getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackLabel', {
                            OrderNum: $('#ConfPackOrderNum').val(),
                            contID: $('#ContID').val()
                        }, true,'label', 'Container ID Label')
                    };

                    if ($('#AutoContPL').val().toString() == "True") {
                        getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPrintCont', {
                            OrderNum: $('#ConfPackOrderNum').val(),
                            contID: $('#ContID').val()
                        }, true,'report', 'Packing List - Container')
                    };

                    if ($('#AutoOrderPL').val().toString() == "True") {
                        getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPackList', {
                            OrderNum: $('#ConfPackOrderNum').val()
                        }, true,'report', 'Packing List - Container Order');
                    };
                    location.reload();
                };
            });
        };
    });
    //on enter will confirm and pack desired item  number or
    //will pop modal if more than one item number record exists for desired scan value
    $('#ScanItem').on("keydown", function (e) {
        var index;
        if (e.which == 13) {
            var searchCount = 0;
            var id;
            var contID;
            for (var x = 0; x < confPackTransTable.rows().data().length; x++) {
                var itemNum = confPackTransTable.row(x).data()[1];
                var complete = confPackTransTable.row(x).data()[7];
                if ($('#ScanItem').val().toLowerCase() == itemNum.toLowerCase() && complete=="False") {
                    searchCount += 1;
                    id = confPackTransTable.row(x).data()[0];
                    index = x;
                };
            };
            if (searchCount == 0) {
                MessageModal("Item Number Issue", "The desired item number was not found or is already confirmed and packed");
            } else if (searchCount == 1) {
                //do for single item 
                //check conditions form item number and order number
                //have to check preference if modal will be displayed
                //have to replicate quant modal done functionality
                confPackHub.server.updateConfPackProcModal(id, $('#ConfPackOrderNum').val(), $('#ContID').val(), "").done(function (mess) {
                    if (mess == "Fail") {
                        MessageModal("Error", "An error has occurred");
                    } else if (mess == "Modal") {
                        //show modal here
                        confPackHub.server.selConfPackProcModal(id).done(function (dataset) {
                            confPackTransTable.row(index).nodes().to$().click();
                            $('#ConfPackProcessModal').modal('show');
                            $('#ContIDProcModal').val($('#ContID').val());
                            confPackProcTable.clear();
                            confPackProcTable.rows.add(dataset).draw();
                        });
                    } else {
                        //redraw table filling in container id and setting completed to true
                        var tr = $('#ShipTransTable tbody tr.active');
                        confPackTransTable.row(index).nodes().to$().click();
                        $('#ShipTransTable tbody tr.active').find(':nth-child(5)').html($('#ContID').val());
                        $('#ShipTransTable tbody tr.active').find(':nth-child(7)').html('True');
                        confPackTransTable.row(tr[0]).invalidate().draw();

                        if ($('#AutoContLabel').val().toString() == "True") {
                            //reportsHub.server.printConfPackLabel($('#ConfPackOrderNum').val(), $('#ContID').val());
                            title = 'Container ID Label';
                            getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackLabel', {
                                OrderNum: $('#ConfPackOrderNum').val(),
                                contID: $('#ContID').val()
                            }, true,'label', title)
                        };

                        if ($('#AutoContPL').val().toString() == "True") {
                            //reportsHub.server.printConfPackPrintCont($('#ConfPackOrderNum').val(), $('#ContID').val());
                            title = 'Packing List - Container';
                            getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPrintCont', {
                                OrderNum: $('#ConfPackOrderNum').val(),
                                contID: $('#ContID').val()
                            }, true,'report', title)
                        };

                        if ($('#AutoOrderPL').val().toString() == "True") {
                            //reportsHub.server.printConfPackPackList($('#ConfPackOrderNum').val());
                            title = 'Packing List - Container Order';
                            getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPackList', {
                                OrderNum: $('#ConfPackOrderNum').val()
                            }, true,'report', title)
                        };

                        if (confPackTransTable.rows().data().length == 1) {
                            ConfirmedPacked();
                        };
                    };
                });
            } else {
                //pop select modal
                //within modal execute quant modal functionality
                confPackHub.server.selConfPackSelectDT($('#ConfPackOrderNum').val(), $('#ScanItem').val()).done(function (dataset) {
                    $('#ConfPackSelectModal').modal('show');
                    $('#ConfPackSelectOrderNum').val($('#ConfPackOrderNum').val());
                    $('#ConfPackSelectContID').val($('#ContID').val());
                    confPackSelectTable.clear().draw();
                    confPackSelectTable.rows.add(dataset).draw();
                });
            };
        };
    });
    //printing below
    $(document.body).on('click', '#PrintPackList', function () {
        var orderNum = $('#ConfPackOrderNum').val();
        getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPrintCont', {
            OrderNum: $('#ConfPackOrderNum').val(),
            contID: $('#ContID').val()
        }, true,'report', 'Packing List - Container');
    });
    $('.ContIDPrint').click(function () {
        var orderNum = $('#ConfPackOrderNum').val(), contID = $('#ContIDPrint').val();
        switch ($(this).text()) {
            case "Print List":
                getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPrintCont', {
                    OrderNum: $('#ConfPackOrderNum').val(),
                    contID: $('#ContID').val()
                }, true,'report', 'Packing List - Container');
                break;
            case "Print Label":
                getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackLabel', {
                    OrderNum: $('#ConfPackOrderNum').val(),
                    contID: $('#ContID').val()
                }, true,'label', 'Container ID Label');
                break;
            case "Print Both":
                getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPrintCont', {
                    OrderNum: $('#ConfPackOrderNum').val(),
                    contID: $('#ContID').val()
                }, true,'report', 'Packing List - Container');
                getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackLabel', {
                    OrderNum: $('#ConfPackOrderNum').val(),
                    contID: $('#ContID').val()
                }, true,'label', 'Container ID Label');
                break;
        }
    });
    $(document.body).on('click', '#ItemLabel', function () {
        var orderNum = $('#ConfPackOrderNum').val()
        var tr = $('#ShipTransTable tbody tr.active');
        var id = confPackTransTable.row(tr[0]).data()[0];
        getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackItemLabel', {
            orderNum: orderNum,
            ST_ID: id
        }, true,'label', 'Item Label - Packing');
    });

    confPackTransTable.on('draw', function () {
        $('#ContIDPrint').empty();
        confPackHub.server.selectConfPackContIDDrop($('#ConfPackOrderNum').val()).done(function (Conts) {
            var OptString = '<option value="" selected="selected"></option>';

            for (var i = 0; i < Conts.length; i++) {
                OptString += '<option value="' + Conts[i] + '">' + Conts[i] + '</option>';
            };

            $('#ContIDPrint').html(OptString);

        })

    });

});
//disable buttons
function disButts() {
    $('#ItemLabel').attr('disabled', 'disabled');
    $('#UnPack').attr('disabled', 'disabled');
};
//enable buttons
function enButts() {
    $('#ItemLabel').removeAttr('disabled');
    if ($('#ContID').prop('disabled')) {
        $('#UnPack').attr('disabled', 'disabled');
    } else {
        $('#UnPack').removeAttr('disabled');
    };
};
//will disable and clear values if ordernumber is marked as confirmed and packed
function ConfirmedPacked() {
    $('#ConfirmAll').attr('disabled', 'disabled');
    $('#ContID').val('');
    $('#ContID').attr('disabled', 'disabled');
    $('#NextContID').attr('disabled', 'disabled');
    $('#ScanItem').attr('disabled', 'disabled');
    $('#UnPack').attr('disabled', 'disabled');
};

