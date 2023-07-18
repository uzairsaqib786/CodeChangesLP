// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var invMasterTable;

// modal trigger function in inventory master modals.js
var triggerModal;
var invMasterTypeAhead;

var clearAll = function (SC) {
    // start top (immediately visible fields)
    if (SC) { $('#stockCode').typeahead('val','').trigger('input').focus() };
    $('#pZone').val('').trigger('change');
    $('input[type="text"]:not(#stockCode, #pZone), select:not(#pageLength), textarea').val('');
    $('#OTcount, #THcount, #RPcount, #totQty, #allPicks, #allPuts, #allWip').html('0');
    $('#quarAlert, #kitContainer').html('');
    $('input[type="checkbox"]:not(#splitCase, #FIFO)').removeAttr('checked');
    $('#FIFO').removeAttr('checked').trigger('change');
    $('#splitCase').prop('checked', 'checked');
    $('#UpdateItemNum').val("")
    $('scanCodeContainer').html('<div class="row"><div class="col-md-6"><div class="row"><div class="col-md-6"><input maxlength="50" type="text" class="form-control" placeholder="Scan Code" id="newScan_sc" /></div>\
                                <div class="col-md-6"><input maxlength="50" type="text" class="form-control" placeholder="Scan Type" id="newScan_st" /></div></div></div>\
                                <div class="col-md-4"><div class="row"><div class="col-md-4"><select class="form-control" id="newScan_range"><option value="Yes">Yes</option><option value="No">No</option></select>\
                                </div><div class="col-md-4"><input maxlength="9" type="text" class="form-control" placeholder="Start Position" id="newScan_sp" /></div>\
                                <div class="col-md-4"><input maxlength="9" type="text" class="form-control" placeholder="Code Length" id="newScan_cl" /></div></div></div>\
                                <div class="col-md-2"><div class="pull-right"><button type="button" class="btn btn-primary save-scan" id="newScan_save">Save</button></div></div></div></div>');
    invMasterTable.draw();
};

var createAlert = function (instanceID, message, alertclasses, dismissable, title, alertID) {
    var dismiss = '';
    if (dismissable) { dismiss = '<button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>'; };
    var alert = '<div id="' + instanceID + '" class="alert ' + alertclasses + '" role="alert">' +
                dismiss +
                title + ' ' + message + '</div>';

    // replace any old alerts with their new message
    if ($('#' + instanceID).length != 0) {
        $('#' + instanceID).remove();
    };
    // make the most recent alert the top one
    $(alertID).prepend(alert);
};

var deleteAlert = function (instanceID) {
    // delete alert
    if ($('#' + instanceID).length != 0) {
        $('#' + instanceID).remove();
    };
};

var createTypeAhead = function (name, id) {
    invMasterTypeAhead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            url: ('/InventoryMaster/' + name + 'TypeAhead?query=%QUERY'),
            replace: function () {
                return '/InventoryMaster/' + name + 'TypeAhead?stockCode=' + document.getElementById('stockCode').value;
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                    //var type = typeof dataObj;
                    //if (type == 'string') {
                    //    return { Value: dataObj };
                    //}
                    //else { return dataObj; }
                });
            }
        },
        cache: false
    });
    invMasterTypeAhead.initialize();
    //change this to id
    $(id).typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "invMasterTypeAhead",
        displayKey: 'ItemNumber',
        source: invMasterTypeAhead.ttAdapter(),
        templates: {
            header: '<p style="width:25%;" class="typeahead-header">Item Num/Supp Item/S Code</p><p style="width:25%;" class="typeahead-header">Description</p><p style="width:25%;" class="typeahead-header">Category</p><p style="width:25%;" class="typeahead-header">Sub Category</p>',
            suggestion: Handlebars.compile("<p style='width:25%;' class='typeahead-row'>{{ItemNumber}}</p><p style='width:25%;' class='typeahead-row'>{{Description}}</p><p style='width:25%;' class='typeahead-row'>{{Category}}</p><p style='width:25%;' class='typeahead-row'>{{SubCategory}}</p>")
        }
    }).on('typeahead:selected', function (obj, data, name) {
        var e = jQuery.Event('keyup');
        e.which = 13;
        $(id).trigger('input').trigger(e);
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', $this.css('width'));
    });
};

$(document).ready(function () {
    var invTableTimer = mkTimer(function () {
        invMasterTable.draw();
    }, 200);
    $('.disable-inputs input, .disable-inputs button, .disable-inputs select, .disable-inputs textarea').attr('disabled', 'disabled');
    createTypeAhead('stockCode', '#stockCode');

    triggerItemNum();
    disableArrows();

    $('#clearButton').click(function () {
        $('#stockCode').typeahead('val', '');
        $('#stockCode').removeAttr('disabled');
        //$('#saveButton, #deleteButton, #quarantineButton').attr('disabled', 'disabled');
    });

    /*
        PARTIAL VIEW JS (small partials only, others have separate file)
    */
    // Locations partial
    $('#refreshTable').click(function () {
        invTableTimer.startTimer();
    });
    invMasterTable = $('#invMasterTable').DataTable({
        "dom": "trip",
        "processing": true,
        "serverSide": true,
        "createdRow": function (row, data, index) {
            // warehouse denotes quarantined
            if (data[1].indexOf('Quarantine') != -1) { $(row).addClass('quarantine'); };
        },
        "ajax": {
            // function call to InventoryMasterController
            "url": "/InventoryMaster/locationsTable",
            "data": function (d) {
                // filter data d.var = something, cast to a tableobjectsent on the vb side
                d.itemNumber = $('#UpdateItemNum').val();
            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });
    $('#invMasterTable').wrap('<div style="overflow-x:scroll"></div>');
    //Handles Page Length Selector for this table
    $('#pageLength').change(function () {
        invMasterTable.page.len($(this).val());
        invMasterTable.draw();
    });
    $('#UpdateItemNum').click(function () {
        $('#UpdateItemNumModal').modal('show');
        $('#OldItemNumUpdate').val($('#UpdateItemNum').val());
    });

    //  Set Item Number placeholder.
    $('#stockCode').attr("placeholder", $('#UpdateItemNum').val());

    // Updates the Item Number Lookup placeholder.
    $('#nextItemNum, #previousItemNum').on('click', function () {
        setTimeout(function () {
            $('#stockCode').attr("placeholder", $('#UpdateItemNum').val());
        }, 200);
    });

    /*****************************
    Details partial
    ******************************/

    $('#OTbutton').click(function () {
        // go to open transactions
        window.open("/Transactions?popup=true&viewtoShow=2&ItemNumber=" + $('#UpdateItemNum').val() + "&App=" + $('#AppName').val(), '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
        //window.location.href = "/Transactions?viewtoShow=2&ItemNumber=" + $('#stockCode').val() + "&App=" + $('#AppName').val();
    });

    $('#THbutton').click(function () {
        // go to transaction history
        window.open("/Transactions?popup=true&viewtoShow=3&ItemNumber=" + $('#UpdateItemNum').val() + "&App=" + $('#AppName').val(), '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
        //window.location.href = "/Transactions?viewtoShow=3&ItemNumber=" + $('#stockCode').val() + "&App=" + $('#AppName').val();
    });

    $('#Reprocbutton').click(function () {
        // go to reprocess
        window.open("/Transactions?popup=true&viewtoShow=4&ItemNumber=" + $('#UpdateItemNum').val() + "&App=" + $('#AppName').val(), '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
        //window.location.href = "/Transactions?viewtoShow=4&ItemNumber=" + $('#stockCode').val() + "&App=" + $('#AppName').val();
    });

    $('#reorderPoint, #reorderQty, #replenishPoint, #replenishLevel').on('focusout', function () {
        if (this.value == null || this.value.trim() == '') {
            this.value = 0;
        };
    });
    // numerics >= 0
    $('#avgWeight, #sampleQty, #minScaleQty, #uCost, #reorderPoint, #reorderQty, #replenishPoint, #replenishLevel').on('input', function () {
        setNumericInRange($(this), 0, null);
    });
});

function triggerItemNum() {
    if ($('#stockCode').val() == '') {
        setTimeout(function () {
            invMasterHub.server.previousItemNum($('#UpdateItemNum').val(),
                                             (InvMasterFilter == "" ? "1=1" : InvMasterFilter.getFilterString()), 0).done(function (mess) {
                                                 $('#UpdateItemNum').val(mess);
                                                 stockCode();
                                                 disableArrows();
                                             });
            $('#saveButton').attr('disabled', 'disabled');
            $('#stockCode').removeAttr('disabled');
        }, 500);
        
    } else {
        $('#stockCode').trigger('input');
        setTimeout(function () {
            $('#stockCode').typeahead('val','').typeahead('close');
            disableArrows();
        }, 100);
    }
};


function disableArrows() {
    if (parseInt($('#ItemNumPos').html()) == 1) {
        $('#previousItemNum').attr('disabled', 'disabled')
    } else {
        $('#previousItemNum').removeAttr('disabled');
    };

    if (parseInt($('#ItemNumPos').html()) == parseInt($('#ItemNumTotal').html())) {
        $('#nextItemNum').attr('disabled', 'disabled')
    } else {
        $('#nextItemNum').removeAttr('disabled');
    };
};