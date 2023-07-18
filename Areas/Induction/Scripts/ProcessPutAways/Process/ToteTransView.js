// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var ToteTransViewTable;
var ToteTransViewHub = $.connection.toteTransViewHub
var batchHub = $.connection.processPutAwaysHub;
$(document).ready(function () {
    var pd = $('#TTViewPrintDir').val() == 'true';
    //initialize datatable
    ToteTransViewTable = $("#ToteTransViewTable").DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'columnDefs': [
            {
                'targets': [0],
                'visible': false
            }
        ],
        "ajax": {
            "url": "/IM/ProcessPutAways/getToteTransViewTable",
            "data": function (d) {
                d.batchid = $('#TTViewBatchID').val();
                d.totenum = $('#TTViewToteNum').val();
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "paging": true
    });
    //datatable row click event
    $('#ToteTransViewTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('#ToteTransViewTabletbody tr.active').removeClass('active');
            $this.addClass('active');
            EnableButts();
        } else {
            $this.removeClass('active')
            DisableButts();
        };
    });
    //clears item from this tote id
    $('#TTViewClearItem').click(function () {
        var conf = confirm("Clear this transaction from this tote?");
        if (conf) {
            var id = ToteTransViewTable.row($('#ToteTransViewTable tbody tr.active')[0]).data()[0];
            ToteTransViewHub.server.clearItemFromTote(id).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has ocurred clearing this item from the tote")
                } else {
                    ToteTransViewTable.draw();
                    setTimeout(function () {
                        if (ToteTransViewTable.page.info().recordsTotal == 0) {
                            location.href = "/IM/ProcessPutAways";
                        };
                    }, 100);
                };
            });
        };
    });
    //clears and deallocates item from tote id
    $('#TTViewDeAlloItem').click(function () {
        var conf = confirm("Clear AND DeAllocate this transaction from the tote?");
        if (conf) {
            var id = ToteTransViewTable.row($('#ToteTransViewTable tbody tr.active')[0]).data()[0];
            ToteTransViewHub.server.deAlloItemFromTote(id).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has ocurred clearing and deallocating this item from the tote")
                } else {
                    ToteTransViewTable.draw();
                    setTimeout(function () {
                        if (ToteTransViewTable.page.info().recordsTotal == 0) {
                            location.href = "/IM/ProcessPutAways";
                        };
                    }, 100);
                };
            });
        };
    });
    //marks this tote as full
    $('#TTViewFullTote').click(function () {
        var totenum = $('#TTViewToteNum').val();
        var cell = ToteTransViewTable.page.info().recordsTotal;
        var batchid = $('#TTViewBatchID').val();
        ToteTransViewHub.server.markToteAsFull(totenum, cell, batchid).done(function (mess) {
            if (mess == "Error") {
                MessageModal("Error", "An error has ocurred marking this tote as full");
            } else {
                ToteTransViewTable.draw();
            };
        });
    });
    //clears the tote
    $('#TTViewClearTote').click(function () {
        //calls the delete modal function
        var batchid = $('#TTViewBatchID').val();
        var toteid = $('#TTViewToteID').val();
        DeleteBatchModal(batchid, toteid, "Put Away", ToteTransViewTable, 'Tote Trans View');
        $('#DelAll').attr('disabled', 'disabled');
        $('#clearTote').click();
        $('#clearBatch').attr('disabled', 'disabled');
    });

    //printing below
    $('#TTViewSingleItemLabel').click(function () {
        var id = ToteTransViewTable.row($('#ToteTransViewTable tbody tr.active')[0]).data()[0];
        if ($('#TTViewPrintDir').val()) {
            //call function for printing using number value
            for (i = 1; i <= parseInt($('#PrintNumCopies').val()) ; i++) {
                printPrevToteItemLabel(id, "", 0);
            };
        } else {
            printPrevToteItemLabel(id, "", 0);
        };
    });

    $('#TTViewToteLabel').click(function () {
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintPrevToteContentsLabel', {
            ToteID:"",
            ZoneLabel:$('#TTViewZoneLab').val(),
            TransType:"Put Away",
            printDirect: pd,
            ID:ToteTransViewTable.row($('#ToteTransViewTable tbody tr:first')[0]).data()[0], 
            BatchID:$('#TTViewBatchID').val()
        }, pd,'label', "Tote Label Preview");
    });

    $('#TTViewItemLabel').click(function () {
        printPrevToteItemLabel(-1, $('#TTViewBatchID').val(), $('#TTViewToteNum').val());
    });

    function printPrevToteItemLabel(ID, batchID, toteNum) {
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintPrevToteItemLabel', {
            ID: ID,
            ToteNum: toteNum,
            PrintDirect: pd,
            BatchID: batchID
        }, pd,'label', 'Put Away Label');
    };

    $('#TTViewToteContents').click(function () {
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintPrevToteTransViewCont', {
            BatchID: $('#TTViewBatchID').val(), 
            ToteNum: $('#TTViewToteNum').val(),
            PrintDirect: pd
        }, pd,'report', 'Tote Contents');
    });
});

function EnableButts() {
    $('#TTViewClearItem').removeAttr('disabled');
    $('#TTViewDeAlloItem').removeAttr('disabled');
    $('#TTViewSingleItemLabel').removeAttr('disabled');
};

function DisableButts() {
    $('#TTViewClearItem').attr('disabled', 'disabled');
    $('#TTViewDeAlloItem').attr('disabled', 'disabled');
    $('#TTViewSingleItemLabel').attr('disabled', 'disabled');
};