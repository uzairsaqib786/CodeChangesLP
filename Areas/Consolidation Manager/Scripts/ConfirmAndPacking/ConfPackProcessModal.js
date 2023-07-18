// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var confPackProcTable;
var confPackHub = $.connection.confirmAndPackingHub;
$(document).ready(function () {
    //initialize the datatable
    confPackProcTable = $('#ProcessModalTable').DataTable({
        "dom": 'trp',
        "processing": true,
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
        "paging": false

    });
    //for splitting a transaction
    $('#SplitLineProcModal').click(function () {
        openSplitLine('ShipTransTable', confPackTransTable, '3', '4', '6', 'ConfPack',
            function () {
                var id = confPackTransTable.row($('#ShipTransTable tbody tr.active')[0]).data()[0]
                confPackHub.server.selConfPackProcModal(id).done(function (dataset) {
                    confPackProcTable.clear().draw();
                    confPackProcTable.rows.add(dataset).draw();
                });
            });
    });

    //adjustment reason
    $('#AdjustQauntModal').click(function () {
        openAdjustQuant('ShipTransTable', confPackTransTable, '6',
            function () {
                var id = confPackTransTable.row($('#ShipTransTable tbody tr.active')[0]).data()[0]
                confPackHub.server.selConfPackProcModal(id).done(function (dataset) {
                    confPackProcTable.clear().draw();
                    confPackProcTable.rows.add(dataset).draw();
                });
            });
    });

    $('#ItemLabelModal').click(function () {
        var id = confPackProcTable.row(0).data()[0];
        //reportsHub.server.printConfPackItemLabel($('#ConfPackOrderNum').val(), id);
        title = 'Item Label - Packing';
        getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackItemLabel', {
            orderNum: $('#ConfPackOrderNum').val(),
            ST_ID: id
        }, true,'label', title)
    });
    //will update the desired record(s) and go thorugh confirm proccess
    $('#DoneModal').click(function () {
        var id = confPackProcTable.row(0).data()[0];
        confPackHub.server.updateConfPackProcModal(id, $('#ConfPackOrderNum').val(), $('#ContIDProcModal').val(), "From_Modal").done(function (mess) {
            if (mess == "Fail") {
                MessageModal("Error", "An error has occurred");
            } else {
                //edit table
                var tr = $('#ShipTransTable tbody tr.active');
                $('#ShipTransTable tbody tr.active').find(':nth-child(5)').html($('#ContIDProcModal').val());
                $('#ShipTransTable tbody tr.active').find(':nth-child(7)').html('True');
                confPackTransTable.row(tr[0]).invalidate().draw();

                //auto printing
                if ($('#AutoContLabel').val().toString() == "True") {
                    //reportsHub.server.printConfPackLabel($('#ConfPackOrderNum').val(), $('#ContIDProcModal').val());
                    title = 'Container ID Label';
                    getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackLabel', {
                        OrderNum: $('#ConfPackOrderNum').val(),
                        contID: $('#ContIDProcModal').val()
                    }, true,'label', title)
                };

                if ($('#AutoContPL').val().toString() == "True") {
                    //reportsHub.server.printConfPackPrintCont($('#ConfPackOrderNum').val(), $('#ContIDProcModal').val());
                    title = 'Packing List - Container';
                    getLLPreviewOrPrint('/CM/ConfirmAndPacking/PrintConfPackPrintCont', {
                        OrderNum: $('#ConfPackOrderNum').val(),
                        contID: $('#ContIDProcModal').val()
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
    });

    $('#ConfPackProcessModal').on('hidden.bs.modal', function (e) {
        if ($('#ConfPackSelectModal').hasClass('show') || $('#ConfPackSelectModal').hasClass('in')) {
            confPackHub.server.selConfPackSelectDT($('#ConfPackOrderNum').val(), $('#ScanItem').val()).done(function (dataset) {
                confPackSelectTable.clear().draw();
                confPackSelectTable.rows.add(dataset).draw();
            });
        };
    });

});