// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var PickBatchTransTable;
var CompPickBatchHub = $.connection.compPickBatchHub;

$(document).ready(function () {

    $('#BatchPickID').focus();

    PickBatchTransTable = $('#PickBatchTransTable').DataTable({
        'dom': 'trip',
        'processing': true,
        'serverSide': true,
        'ajax': {
            'url': "/IM/CompPickBatch/GetPickBatchTransTable",
            'data': function (d) {
                d.batchid = $('#BatchPickID').val();
                d.toteid = $('#ToteID').val();
                d.filter = "";
            }
        },
        'language': {
            'paginate': {
                'next': 'Next',
                'previous': 'Previous'
            }
        },
        'paging': true,
        'drawCallback': function () {
            $('#ShortTrans').attr('disabled', 'disabled');
            $('#BlossTote').attr('disabled', 'disabled');
            $('#CompleteTrans').attr('disabled', 'disabled');

            //Check if batch has rows
            if (!$('#PickBatchTransTable tbody tr td').hasClass('dataTables_empty')) {
                $('#ToteCol').show();
                $('#BlossTote').show();

                $('#CompleteBatch').removeAttr('disabled');

                if ($('#ToteID').val() != "") {
                    $('#BlossTote').removeAttr('disabled');
                } else {
                    $('#ToteID').focus();
                };

            } else {
                if ($('#BatchPickID').val() != "" && $('#ToteID').val() == "") {
                    MessageModal("No Rows", "No open transactions for the entered batch", function () { $('#BatchPickID').val('').focus(); });
                } else if ($('#BatchPickID').val() != "" && $('#ToteID').val() !="") {
                    MessageModal("No Rows", "No open transaction for that tote in the batch",
                        function () {
                            $('#ToteID').val('');
                            var e = jQuery.Event("keyup", { keyCode: 13 });
                            $('#BatchPickID').trigger(e);
                    });
                };

                $('#CompleteBatch').attr('disabled', 'disabled');
            };
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false
            }
        ]
    });

    $("#PickBatchTransTable").on('click', 'tr', function () {
        var $this = $(this);

        if ($this.hasClass('active')) {
            $this.removeClass('active');
            $('#CompleteTrans').attr('disabled', 'disabled');
            $('#ShortTrans').attr('disabled', 'disabled');
        } else {
            $("#PickBatchTransTable tbody tr").removeClass('active');
            $this.addClass("active");
            $('#CompleteTrans').removeAttr('disabled');
            $('#ShortTrans').removeAttr('disabled');
        };

    });

    $('#ClearScreen').click(function () {
        $('#ShortTrans').attr('disabled', 'disabled');
        $('#BlossTote').attr('disabled', 'disabled').hide();
        $('#CompleteTrans').attr('disabled', 'disabled');
        $('#CompleteBatch').attr('disabled', 'disabled');
        $('#BatchPickID').val('');
        $('#ToteCol').hide();
        $('#ToteID').val('');
        PickBatchTransTable.clear().draw();
    });


    $('#BlossTote').click(function () {
        var BlossTbl = $('#blossomdata');
        BlossTbl.empty();

        var TableData = PickBatchTransTable.rows($("#PickBatchTransTable tbody tr"));

        //Table Rows
        $.each(TableData.data(), function (i, TableLine) {
                var row = '<tr data-id="' + TableLine[0] + '">';
                row += makeCell(TableLine[3], "ItemNum");
                row += makeCell(TableLine[5], "TransQty");
                row += makeCell('<input type="number" min="0" max="' + TableLine[5] + '" class="form-control BlossQty"/>', 'BlossQty');
                row += '</tr>';
                BlossTbl.append(row);

        });

        if ($('#blossomdata tr').length > 0) {
            $('#BlossomModalTitle').text("Blossom Tote: " + $('#ToteID').val())
            $('#BlossomModal').modal('show');
        };
    });

    $('#CompleteTrans').click(function () {
        var SelectRowData = PickBatchTransTable.rows($("#PickBatchTransTable tbody tr.active")).data()[0];
        var OTID = SelectRowData[0];

        MessageModal("Complete Transaction", "Complete this transaction?", undefined, undefined,
                    function () {
                        CompPickBatchHub.server.completeTrans(OTID).done(function (success) {
                            if (success) {
                                PickBatchTransTable.clear().draw();
                            } else {
                                MessageModal("Error", "An error occured completing this transaction");
                            };
                        });
                    }
                );

    });


    $('#ShortTrans').click(function () {
        $('#CompPickBatchShortModal').modal('show');

        //Set fields in modal display
        var SelectRowData = PickBatchTransTable.rows($("#PickBatchTransTable tbody tr.active")).data()[0];
        $('#ShortOTID').val(SelectRowData[0]);
        $('#ShortOrdNum').val(SelectRowData[1]);
        $('#ShortToteID').val(SelectRowData[2]);
        $('#ShortItemNum').val(SelectRowData[3]);
        $('#ShortLoc').val(SelectRowData[6]);
        $('#ShortTransQty').val(SelectRowData[5]);

    });

    $('#CompleteBatch').click(function () {
        MessageModal("Complete Batch", "Complete all remaining in this batch?", undefined, undefined,
                   function () {
                       CompPickBatchHub.server.completeBatch($('#BatchPickID').val()).done(function (success) {
                           if (success) {
                               $('#ClearScreen').click();
                           } else {
                               MessageModal("Error", "An error occured completing this transaction");
                           };
                       });
                   }
               );
    });


    $('#BatchPickID').keyup(function (e) {
        $('#ToteCol').hide();
        $('#ToteID').val('');

        if (e.keyCode === 13 && $(this).val() != "") {
            PickBatchTransTable.clear().draw();
        };
    });

    $('#ToteID').keyup(function (e) {
        if (e.keyCode === 13 && $(this).val() != "") {
            PickBatchTransTable.clear().draw();
        };
    });


});

function makeCell(inner, name) {
    return '<td style="font-size:x-large;" name="' + name + '">' + inner + '</td>';
};