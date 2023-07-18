// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var confPackSelectTable;
$(document).ready(function () {
    //initialize datatable
    confPackSelectTable = $('#ConfPackSelectTable').DataTable({
        "dom": 'trip',
        'columnDefs': [
            {
                'targets': [0],
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
    //table click functionality for the clicked row of the table
    $('#ConfPackSelectTable tbody').on('click', 'tr', function () {
        //insert functionality here either popiing modal or doing query
        var tr = $(this)
        var id = confPackSelectTable.row(tr[0]).data()[0];
        confPackHub.server.updateConfPackProcModal(id, $('#ConfPackSelectOrderNum').val(), $('#ConfPackSelectContID').val(), "").done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "An error has occurred");
            } else if (mess == "Modal") {
                //show modal here
                confPackHub.server.selConfPackProcModal(id).done(function (dataset) {
                    for (var x = 0; x < confPackTransTable.rows().data().length; x++) {
                        var tabID = confPackTransTable.row(x).data()[0];
                        if (id == tabID) {
                            if (!confPackTransTable.row(x).nodes().to$().hasClass('active')){
                                confPackTransTable.row(x).nodes().to$().click();
                            }
                        };
                    };
                    $('#ConfPackProcessModal').modal('show');
                    $('#ContIDProcModal').val($('#ConfPackSelectContID').val());
                    confPackProcTable.clear().draw();
                    confPackProcTable.rows.add(dataset).draw();
                });
            } else {
                //edit table
                for (var x = 0; x < confPackTransTable.rows().data().length; x++) {
                    var tabID = confPackTransTable.row(x).data()[0];
                    if (id == tabID) {
                        confPackTransTable.row(x).nodes().to$().click();

                    };
                };
                var tr = $('#ShipTransTable tbody tr.active');
                $('#ShipTransTable tbody tr.active').find(':nth-child(5)').html($('#ConfPackSelectContID').val());
                $('#ShipTransTable tbody tr.active').find(':nth-child(7)').html('True');
                confPackTransTable.row(tr[0]).invalidate().draw();
                //remove items from modal table here



                //auto print
                if ($('#AutoContLabel').val().toString() == "True") {
                    //reportsHub.server.printConfPackLabel($('#ConfPackSelectOrderNum').val(), $('#ConfPackSelectContID').val());
                    title = 'Container ID Label';
                    getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackLabel', {
                        OrderNum: $('#ConfPackSelectOrderNum').val(),
                        contID: $('#ConfPackSelectContID').val()
                    }, true,'label', title)
                };

                if ($('#AutoContPL').val().toString() == "True") {
                    //reportsHub.server.printConfPackPrintCont($('#ConfPackSelectOrderNum').val(), $('#ConfPackSelectContID').val());
                    title = 'Packing List - Container';
                    getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPrintCont', {
                        OrderNum: $('#ConfPackSelectOrderNum').val(),
                        contID: $('#ConfPackSelectContID').val()
                    }, true,'report', title)
                };

                if ($('#AutoOrderPL').val().toString() == "True") {
                    //reportsHub.server.printConfPackPackList($('#ConfPackSelectOrderNum').val());
                    title = 'Packing List - Container Order';
                    getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPackList', {
                        OrderNum: $('#ConfPackSelectOrderNum').val()
                    }, true,'report', title)
                };
            };
        });
    });

});