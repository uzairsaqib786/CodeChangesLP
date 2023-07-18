// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var sysRepHub = $.connection.systemReplenishmentHub;
var reprocessHub = $.connection.reprocessTransactionsHub;
var sysRepNewTable;
var SystReplenNewCols= new Array();
$(document).ready(function () {
    $.each($('#columnSelectionNew').children(), function (index, element) {
        if (index != 0) {
            SystReplenNewCols.push($(element).attr('value'));
        };
    });

    var typingtime, newTime, updateInterval;
    sysRepNewTable = $('#systemReplenishmentNewOrders').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "columnDefs": [
            { 'visible': false, "targets": 12 }
        ],
        "ajax": {
            "url": "/Admin/SystemReplenishment/newReplenishmentOrdersTable",
            'data': function (d) {
                d.searchString = $('#searchStringNew').val().trim();
                d.searchColumn = $('#columnSelectionNew').val().trim();
                d.reorder = $('#Reorder').val() == 'reorder';
                //add filter here
                d.filter = (SystReplenNewFilterMen == "" ? "" : SystReplenNewFilterMen.getFilterString());
            }
        },
        'createdRow': function (row, data, dataIndex) {
            var $row = $(row);
            var tQtyInput = $row.children(':nth-child(10)');
            var replenish = $row.children(':nth-child(11)').addClass('filter-ignore'), replenishBool = data[10].toLowerCase().trim() == 'true';
            var exists = $row.children(':nth-child(12)').addClass('filter-ignore'), existsBool = data[11].toLowerCase().trim() == 'true';
            var transQty = data[9], availQty = data[6];
            var rp_id = data[12];
            replenish.html('<input type="checkbox" class="replenish checkbox-large" name="' + rp_id + '" ' + (replenishBool ? 'checked="checked"' : '') + '" />');
            exists.html('<input type="checkbox" name="' + rp_id + '" class="exists checkbox-large" disabled="disabled" ' + (existsBool ? 'checked="checked"' : '') + '" />');
            tQtyInput.html('<div class="form-group has-feedback" style="margin-bottom:0px;">\
                                <input type="text" class="form-control trans-qty modal-launch-style" readonly="readonly" name="' + rp_id + '" value="' + tQtyInput.html() + '" />\
                                <i class="glyphicon glyphicon-resize-full form-control-feedback trans-qty"></i>\
                            </div>');
            if (existsBool || parseInt(transQty) == 0 || parseInt(availQty) == 0) {
                replenish.children('input').attr('disabled', 'disabled');
            };
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    }).on('draw', function (e, settings) {
        $('#goToInvMaster').attr('disabled', 'disabled');
    }).on('xhr', function (e, settings, jsonObj) {
        $('#numberSelectedNew').val(jsonObj.extraData);
    });
    $('#systemReplenishmentNewOrders').wrap('<div style="overflow-x:scroll"></div>');

    function redrawTable(reset) {
        newTime = new Date().getTime();
        if (newTime - typingtime > 200) {
            sysRepNewTable.draw(reset);
        }
        else {
            clearTimeout(updateInterval);
            updateInterval = setTimeout(function () { sysRepNewTable.draw(reset); }, 200);
        };
        typingtime = newTime;
    };

    $('a[href="#newOrders"]').on('show.bs.tab', function () {
        redrawTable(true);
    });

    $('#systemReplenishmentNewOrders').on('input', '.trans-qty', function () {
        var $this = $(this), replenishQty = $this.parent().parent().find('td:nth-child(8)').html(), availableQty = $this.parent().parent().find('td:nth-child(7)').html();
        var high = replenishQty > availableQty ? availableQty : replenishQty;
        var rp_id = $this.attr('name');
        setNumericInRange($this, 0, high);
    });

    $('#columnSelectionNew').change(function () {
        redrawTable(true);
    });

    $('#searchStringNew').on('input', function () {
        if ($('#columnSelectionNew').val().trim() != '') {
            redrawTable(true);
        } else {
            $(this).val('');
        };
    });

    $('#Reorder').change(function () {
        redrawTable(true);
    });

    $('#pageLengthNew').change(function () {
        sysRepNewTable.page.len($(this).val());
        redrawTable(true);
    });

    var SysReplnNew = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: '/Admin/SystemReplenishment/getSystemReplenishmentNewTypeahead?query=',
            replace: function (url, uriEncodedQuery) {
                return url + uriEncodedQuery + '&column=' + $('#columnSelectionNew').val().trim();
            },
            filter: function (list) {
                return $.map(list, function (column) { return { value: column }; });
            },
            cache: false
        }
    });
    SysReplnNew.initialize();
    $('#searchStringNew').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "SysReplnNew",
        displayKey: 'value',
        source: SysReplnNew.ttAdapter()
    }).on('typeahead:selected', function () {
        redrawTable(true);
    }).parent().css({ 'display': 'inline' });


    $('#systemReplenishmentNewOrders').on('click', '.replenish', function (e) {
        e.stopImmediatePropagation();
        var $this = $(this);
        var rp_id = $this.attr('name'), checked = $this.prop('checked');
        var numberSelected = $('#numberSelectedNew');
        sysRepHub.server.updateReplenishmentInclude(rp_id, checked).done(function () {
            numberSelected.val(parseInt(numberSelected.val()) + (checked ? 1 : -1));
        });
    });

    $('#selectNew, #unselectNew').click(function () {
        var $this = $(this);
        var checked = $this.attr('id') == 'selectNew', reorder = $('#Reorder').val() == 'reorder';
        if (confirm('Click OK to ' + (checked ? 'mark' : 'unmark') + ' ' + (reorder ? 'Re-Order' : 'all') + ' entries.')) {
            sysRepHub.server.updateReplenishmentIncludeAll(checked, reorder, $('#searchStringNew').val().trim(), $('#columnSelectionNew').val().trim(), (SystReplenNewFilterMen == "" ? "" : SystReplenNewFilterMen.getFilterString())).done(function () {
                redrawTable(false);
            });
        };
    });

    $('#goToInvMaster').click(function () {
        //window.location.href = "/InventoryMaster?ItemNumber=" + $('tr.active td:first').html() + '&App=' + $('#AppName').val();
        InvMastPopUp($('tr.active td:first').html());
    });

    $('#systemReplenishmentNewOrders').on('click', 'tr', function () {
        var active = $('tr.active'), $this = $(this);
        if (active.length > 0 && !$this.hasClass('active')) {
            active.removeClass('active');
            $this.addClass('active');
            $('#goToInvMaster').removeAttr('disabled');
        } else if ($this.hasClass('active')) {
            $this.removeClass('active');
            $('#goToInvMaster').attr('disabled', 'disabled');
        } else {
            $this.addClass('active');
            $('#goToInvMaster').removeAttr('disabled');
        };
    });

    $('#systemReplenishmentNewOrders').on('dblclick', 'td', function () {
        var $this = $(this);
        var item = $this.text();

        if ($this.index() == 0 && item != "No data available in table") {
            InvMastPopUp(item);
        };
    });

    $('#ViewAllItems').click(function () {
        $('#searchStringNew').val('');
        $('#columnSelectionNew').val('');
        redrawTable(true);
    });

    $('#ViewSelectedItems').click(function () {
        $('#searchStringNew').val('True');
        $('#columnSelectionNew').val('Replenish');
        redrawTable(true);
    });

    $('#Kanban').on('change', createNewList);

    $('#createNew').click(createNewList);

    function createNewList() {
        if (confirm('Click OK to create a new replenishment list.')) {
            $('#ItemNumbersToFilter').val('');
            var kb = $('#Kanban').prop('checked');
            sysRepHub.server.createNewReplenishments(kb).done(function () {
                redrawTable(true);
            });
        } else if (this.id.toLowerCase() == 'kanban') {
            var $this = $(this);
            $this.prop('checked', !$this.prop('checked'));
        };
        if ($('#Kanban').prop('checked'))
            $('#KanbanBanner').show();
        else
            $('#KanbanBanner').hide();
    };

    $(document.body).on('click', '#processNew', function () {
        if (confirm('Click OK to create replenishment orders for all selected items.')) {
            // process and pass control to the JS function sysRepHub.client.updateReplenishmentStatus.
            sysRepHub.server.processReplenishments($('#Kanban').prop('checked'));
        };
    });

    $(document.body).on('click', '#printNew', function () {
        if (confirm('Click OK to print a replenishment report.')) {
            //reportsHub.server.printNewReplenishmentReport($('#Reorder').val() == 'reorder');
            title = 'New Replen Report';
            getLLPreviewOrPrint('/Admin/SystemReplenishment/printNewReplenishmentReport', {
                reorder: $('#Reorder').val() == 'reorder'
            }, true,'report', title)
        };
    });

    function printReprocessReport() {
        //reprocessHub.server.printReprocessTransactions(false, 0, '', '', '', '', '');
        title = 'Reprocess Transactions Report';
        getLLPreviewOrPrint('/Transactions/printReprocessTransactions', {
            history: false,
            ID: 0,
            OrderNumber: '',
            ItemNumber: '',
            reason: '',
            message: '',
            datestamp: ''
        }, true,'report', title)
    };

    sysRepHub.client.updatedReplenishments = function () {
        if ($('.container-fluid ul.nav.nav-tabs li.active a').attr('href') == '#newOrders') {
            redrawTable(false);
        };
    };

    sysRepHub.client.updateReplenishmentStatus = function (numLeft, transStatus, rpDone) {
        var processNew = $('#processNew');
        processNew.html('Processing').attr('disabled', 'disabled');
        var replenStatusModal = $('#replen_status_modal');
        // prevent ESC key
        replenStatusModal.modal({ keyboard: false, show: false });
        if (!replenStatusModal.hasClass('in') && rpDone == null) {
            replenStatusModal.modal('show');
        };
        $('#ReplenishmentsRemaining').val(numLeft);
        $('#ReplenishmentStatus').html(transStatus);
        if (rpDone != null) {
            if (rpDone == 'stopped') {
                alert('Replenishments stopped.  Transactions were deleted.  The Event Log contains details about the action taken.');
            } else if (rpDone == 'done') {
                alert('Replenishments finished processing.');
            } else if (rpDone == 'reprocess') {
                if (confirm('Replenishments finished.  There are reprocess transactions due to the replenishment process.  Click OK to print a reprocess report now.')) {
                    printReprocessReport();
                };
            } else if (rpDone == 'no replenishments') {
                alert('There were no replenishments to process.');
            } else {
                alert('Unknown Error in system replenishments: ' + rpDone);
            };
            replenStatusModal.modal('hide').one('hidden.bs.modal', function () {
                redrawTable(false);
                processNew.html('Process Replenishments').removeAttr('disabled');
            });
        };
    };

    $('#StopReplenishments').click(function () {
        if (confirm('Click OK to stop the replenishment process.  This will delete all progress made towards creating replenishment transactions and any created transactions from this process.')) {
            sysRepHub.server.haltReplenishments();
        };
    });

    $('#ItemNumberFilterButt').click(function () {
        $('#ItemNumberFilterModal').modal('show');
    });

});

function InvMastPopUp(Item) {
    var $handle = $(window.open('/InventoryMaster?App=Admin&popup=True&ItemNumber=' + Item, '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'));

    try {
        $handle[0].focus();
        $handle.one('load', function () {
            $handle.one('unload', function () {
                //Empty for now
            });
        });
    } catch (e) {
        console.log('Exception: ', e)
        MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.', undefined, function () { document.getElementById("Message_Modal").style.zIndex = "1061"; });
    };
};