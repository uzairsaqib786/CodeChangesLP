// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var ToteTransManTable;
var ToteTransHub = $.connection.toteTransactionsManagerHub
var batchHub = $.connection.processPutAwaysHub;

$(document).ready(function () {
    var pd = $('#PrintDir').val() == 'true';
    //datatable
    ToteTransManTable = $("#ToteTransManTable").DataTable({
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
            "url": "/IM/ToteTransactionManager/ToteTransManDT",
            "data": function (d) {
                d.batchid = $('#ToteTransManBatchID').val();
                d.filter = (ToteTransManFilterMen == "" ? "" : ToteTransManFilterMen.getFilterString()); 
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
    //DataTable row click 
    $('#ToteTransManTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('#ToteTransManTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            EnableToteTransManButts();
        } else {
            $this.removeClass('active');
            DisableToteTransManButts();
        };
    });
    //on filter change redraw table
    $('#ToteTransManTable').on('filterChange', function () {
        ToteTransManTable.draw();
    });

    //batch pick id typeahead
    var BatchIDTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/IM/ToteTransactionManager/CreateBatchPickIDTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#ToteTransManBatchID').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    BatchIDTA.initialize();

    $('#ToteTransManBatchID').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "BatchIDTA",
        displayKey: 'BatchID',
        source: BatchIDTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Batch Pick ID</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:100%;">{{BatchID}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        ToteTransManTable.draw();
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', "300px").css('left', 'auto');
    });

    //View all records
    $('#ViewAllToteTransMan').click(function () {
        $('#ToteTransManBatchID').typeahead('val',"");
        ToteTransManTable.draw();
    });

    //redraw table when batchid is changed
    $('#ToteTransManBatchID').on('change', function () {
        ToteTransManTable.draw();
    })

    //clears info for all pick tote tramsactions
    $('#ClearPickToteInfo').click(function () {
        var conf = confirm("Are you sure you want to clear the info for all pick batches?")
        if (conf) {
            ToteTransHub.server.clearPickToteInfo().done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred clearing the info for pick totes")
                } else {
                    $('#ViewAllToteTransMan').click();
                };
            });
        };
    });
    //deletes the highlighted batch
    $('#ClearBatchButt').click(function () {
        var batchid = ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[1];
        var toteid = ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[3];
        var transtype = ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[5];
        var conf = confirm("Are you sure you want to clear this batch or tote id?");
        
        if (conf) {
            DeleteBatchModal(batchid, toteid, transtype, ToteTransManTable, 'Tote Trans Man');
            $('#DelAll').hide();
        };
    });

    //printing functionality below
    $(document.body).on('click', '#OffCarListButt', function () {        
        getLLPreviewOrPrint('/IM/ToteTransactionManager/PrintPrevOffCarList', {
            ToteID:ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[3],
            TransType:ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[5],
            PrintDirect:pd
        }, pd,'report', "Off Carousel List Preview");
    });

    $(document.body).on('click', '#ToteContentButt', function () {
        getLLPreviewOrPrint('/ToteTransactionManager/PrintPrevToteContents/', {
            ToteID:ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[3],
            ZoneLabel:ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[4],
            TransType:ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[5],
            PrintDirect: pd
        }, pd,'report', "Tote Contents Preview");
    });

    $(document.body).on('click', '#ToteLabelButt', function () {
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintPrevToteContentsLabel', {
            ToteID: ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[3],
            ZoneLabel: ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[4],
            TransType: ToteTransManTable.row($('#ToteTransManTable tbody tr.active')[0]).data()[5],
            printDirect: pd,
            ID: -2,
            BatchID: ''
        }, pd,'label', "Tote Label Preview");
    });
});

function EnableToteTransManButts() {
    $('#ClearBatchButt').removeAttr('disabled');
    $('#OffCarListButt').removeAttr('disabled');
    $('#ToteContentButt').removeAttr('disabled');
    $('#ToteLabelButt').removeAttr('disabled');
};

function DisableToteTransManButts() {
    $('#ClearBatchButt').attr('disabled', 'disabled');
    $('#OffCarListButt').attr('disabled', 'disabled');
    $('#ToteContentButt').attr('disabled', 'disabled');
    $('#ToteLabelButt').attr('disabled', 'disabled');
};