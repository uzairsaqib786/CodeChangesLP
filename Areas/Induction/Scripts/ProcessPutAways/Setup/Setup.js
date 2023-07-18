// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var batchHub = $.connection.processPutAwaysHub;
var BatchIDTASetup;
$(document).ready(function () {
    addResize(function () { $('#batch_totes_container').css('max-height', $(window).height() * 0.65); });
    // if we started with a batch already in the queue we should load that one.
    if ($('#BatchID').val().trim() != '') {
        var e = jQuery.Event('keyup');
        e.which = 13;
        $('#BatchID').trigger(e);
        $('#Item').focus();
    };
    // setup the totes for a new batch (or existing one that we have just selected)
    function setupTotes(numTotes, defaultCells, defaultIDs) {
        if ($.isNumeric(numTotes)) {
            var btc = $('#batch_totes_container');
            btc.children().remove();
            for (var x = 1; x <= numTotes; x++) {
                btc.append(makeTote(x, defaultCells, '', ''));
            };
        };
    };

    $('#ScanNextTote').on('keypress', function (e) {
        if (e.which == 13) {
            var isEntered = false;
            var setEle;
            $('.tote-modal.form-control').each(function () {
                var $this = $(this);
                if ($this.val() == $('#ScanNextTote').val()) {
                    alert("This tote is already set in a batch")
                    isEntered = true
                    if (setEle != undefined) {
                        setEle.val("")
                    }
                }
                if ($this.val() == "" && !isEntered) {
                    setEle = $this
                    $this.val($('#ScanNextTote').val());
                    isEntered = true;
                }
            })
            $('#ScanNextTote').val("")
        }
    })

    // make sure that the cell number of a tote is an integer >= 0
    $('#batch_totes_container').on('input', '[name="cells"]', function () {
        setNumericInRange($(this), 0, null);
    });

    // launch the tote modal to set a tote belonging to the current batch.
    $(document.body).on('click', '.tote-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input.tote-modal');
        };
        launchToteModal($this, $('#batch_totes_container'), '.tote-modal', $this.parent().parent().parent().find('input[name="cells"]'));
    });

    // create a new batch
    $('#NewBatch').click(function () {
        makeSetup();
        $('#ScanNextTote').focus();
    });

    // launch the zone modal so that we can assign zones to the current batch
    $('#AssignZones').click(function () {
        launchZoneLabel($('#BatchIDSetup').val());
    });

    // close the zone modal because we have set the zones
    $('#zonelabel_submit').click(function () {
        var label = 'Zones: ';
        var rows = $('[name="zone"]').parent().parent();
        $.each(rows, function (index, element) {
            var e = $(element);
            if (e.find('.active').length > 0) {
                if (e.find('.toggles').data('toggles').active) {
                    label += e.find('[name="zone"]').val() + ' ';
                };
            };
        });
        $('#AssignedZones').val(label.trim());
    });

    //select all available zones
    $('#zonelabel_selectall').click(function () {
        var rows = $('[name="zone"]').parent().parent();
        $.each(rows, function (index, element) {
            var e = $(element);
            if (e.find('.toggles').length > 0) {
                e.find('.toggles').data('toggles').setValue(true);
            };
        });
    });

    //select non staging
    $('#zonelabel_selectnonstaging').click(function () {
        var count = 0;
        var rows = $('[name="zone"]').parent().parent();
        $.each(rows, function (index, element) {
            var e = $(element);
            if (e.find('.toggles').length > 0 && e.find('[name="stage"]').val() == "No") {
                e.find('.toggles').data('toggles').setValue(true);
                count += 1;
            };
        });
        if (count == 0) {
            MessageModal("Warning", "No non-staging zones");
        };
    });

    //select staging
    $('#zonelabel_selectstaging').click(function () {
        var count = 0;
        var rows = $('[name="zone"]').parent().parent();
        $.each(rows, function (index, element) {
            var e = $(element);
            if (e.find('.toggles').length > 0 && e.find('[name="stage"]').val() == "Yes") {
                e.find('.toggles').data('toggles').setValue(true);
                count += 1;
            };
        });
        if (count == 0) {
            MessageModal("Warning", "No staging zones");
        };
    });

    // start a new batch with an auto generated id
    $('#NewBatchWID').click(function () {
        if ($('#batch_totes_container').html().trim() == '' || confirm('Click OK to start a new batch and discard any changes to the current batch.')) {
            batchHub.server.getNextBatchID().done(function (batch) {
                $('#BatchIDSetup').val(batch);
                setupTotes($('#NumTotes').val(), $('#DefaultCells').val(), false);
                $('#BatchStatus').val('Not Processed');
                if ($('#AutoPutToteIDs').val() == 'true') {
                    $('#AssignNextIDAll').click();
                };
                $('#AssignZones').click();
            });
        };
    });

    // start a new batch
    function makeSetup() {
        if ($('#BatchIDSetup').val().trim() == '') {
            MessageModal('Error', 'You must assign a Batch ID before creating a new batch.');
        } else {
            if ($('#batch_totes_container').html().trim() == '') {
                setupTotes($('#NumTotes').val(), $('#DefaultCells').val(), false);
                $('#BatchStatus').val('Not Processed');
                if ($('#AutoPutToteIDs').val() == 'true') {
                    $('#AssignNextIDAll').click();
                };
            } else if (confirm('Click OK to start a new batch and discard any changes to the current batch.')) {
                $('#BatchStatus').val('Not Processed');
                $('#BatchIDSetup, #AssignedZones').val('');
                $('#batch_totes_container').html('');
            };
        };
    };

    // assign all totes the next id field from system preferences incremented by the number of totes assigned before the individual
    $('#AssignNextIDAll').click(function () {
        batchHub.server.getNextTote().done(function (tote) {
            var totes = $('[name="tote-id"]:not(.locked)');
            $.each(totes, function (i, v) {
                this.value = tote + i;
            });
            batchHub.server.updateNextTote(totes.length + tote).done(function (updated) {
                if (!updated) {
                    MessageModal('Error', 'An error occurred while attempting to save the new "Next Tote ID"');
                };
            });
        });
    });

    // assign the next tote id from system preferences and update it
    $(document.body).on('click', '[name="NextID"]', function () {
        var $this = $(this);
        batchHub.server.getNextTote().done(function (tote) {
            $this.parent().parent().find('[name="tote-id"]').val(tote);
            $this.parent().parent().find('[name="cells"]').val($('#DefaultCells').val());
            batchHub.server.updateNextTote(tote + 1).done(function (updated) {
                if (!updated) {
                    MessageModal('Error', 'An error occurred while attempting to save the new "Next Tote ID"');
                };
            });
        });
    });

    // clear the tote
    $(document.body).on('click', '[name="Clear"]', function () {
        var p = $(this).parent().parent().children();
        p.find('[name="tote-id"]').val('');
        p.children('[name="cells"]').val($('#DefaultCells').val());
    });

    // update the cells of every tote to default
    $('#UpdateToDefault').click(function () {
        if (confirm('Click OK to update all totes (except allocated ones) to have their default cell count.')) {
            $('[name="cells"]:not(.locked)').val($('#DefaultCells').val());
        };
    });

    // process a batch so that it appears in the tote setup queue table
    $('#Process').click(function () {
        var batch = $('#BatchIDSetup').val();
        var zones = $('#AssignedZones').val();
        if (batch.trim() == '') {
            MessageModal('Error', 'You must provide a Batch ID.');
        } else if (zones.trim() == '' || zones.trim().toLowerCase() == 'zones:') {
            MessageModal('Error', 'You must select one or more zones.  If there are no zones available for selection check your Location Zones settings and/or delete or deallocate a batch to free up a zone.');
        } else {
            var totes = $('#batch_totes_container .row');
            // build the tote definitions in a csv format
            if (totes.length != 0) {
                var ids = '';
                var cells = '';
                var pos = '';
                var e;
                $.each(totes, function (index, elem) {
                    e = $(elem);
                    var i = e.find('[name="tote-id"]');
                    var c = e.find('[name="cells"]');
                    var p = e.find('[name="position"]');
                    if (!$.isNumeric(c.val()) || c.val() < 0) { c.val($('#DefaultCells').val()); };
                    if (i.val().trim() != '') {
                        ids += i.val() + ',';
                        cells += c.val() + ',';
                        pos += p.val() + ',';
                    };
                });
                ids = ids.substring(0, ids.length - 1);
                cells = cells.substring(0, cells.length - 1);
                pos = pos.substring(0, pos.length - 1);
                batchHub.server.validateTotes(ids).done(function (InvalidTote) {
                    switch (InvalidTote) {
                        case "Error":
                            //Error Validating
                            MessageModal("Error", "An error has occurred validating the current tote setup. Batch not created. Please try again");
                            break;
                        case "":
                            batchHub.server.processBatch(batch, zones, [ids, cells, pos]).done(function (processed) {
                                BatchIDTASetup.clearRemoteCache();
                                if (!processed) {
                                    MessageModal('Error', 'An error occurred while creating or updating the batch.');
                                } else {
                                    $('#BatchStatus').val('Processed');
                                    $('#PrintToteLabels').removeAttr('disabled');

                                    if ($('#AutoPrintToteIDLabels').val() == 'true') {
                                        $('#PrintToteLabels').click()
                                    };

                                    if (confirm('Batch processed!  Click OK to move onto the next step or cancel to remain on this screen to create/edit more batches.')) {
                                        var e = jQuery.Event('keyup');
                                        e.which = 13;
                                        $('#BatchID').typeahead('val', $('#BatchIDSetup').val());
                                        $('#BatchID').trigger(e);
                                        $('a[href="#ProcessTab"').click();
                                        $('#Item').focus();
                                    };
                                };
                            });

                            break;
                        default:
                            MessageModal("Invalid Tote ID", "The tote id " + InvalidTote + " already exists in Open Transactions. Please select another tote",
                                function () {
                                    $.each(totes, function (index, elem) {
                                        e = $(elem);
                                        var i = e.find('[name="tote-id"]');
                                        if (i.val().trim() == InvalidTote) {
                                            i.focus();
                                            return false;
                                        };
                                    });
                                });
                            break;
                    };
                });
            };
        };
    });

    $('#BatchIDSetup').on('keyup', function (e) {
        if (e.keyCode == 13) {
            setupTimer.startTimer();
        };
    });

    BatchIDTASetup = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/IM/ProcessPutAways/BatchIDTypeahead?BatchID='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#BatchIDSetup').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    BatchIDTASetup.initialize();

    $('#BatchIDSetup').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "BatchIDSetup",
        displayKey: 'BatchID',
        source: BatchIDTASetup.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Batch ID</p><p class="typeahead-header" style="width:50%;">Zone Label</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:50%;">{{BatchID}}</p><p class="typeahead-row" style="width:50%;">{{ZoneLabel}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        updateBatchStatus();
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', "650px").css('left', 'auto');
    });

    var setupTimer = mkTimer(updateBatchStatus, 600);
    $('#BatchIDSetup').on('input', function () {
        setupTimer.startTimer();
    });

    // determines whether a batch is already in the tote setup queue
    function updateBatchStatus() {
        $('#Process, #NewBatch').attr('disabled', 'disabled');
        $('#batch_totes_container').children('h3').remove();
        batchHub.server.getBatchTotes($('#BatchIDSetup').val()).done(function (totes) {
            if (totes.length > 0) {
                $('#BatchStatus').val('Processed');
                $('#PrintToteLabels').removeAttr('disabled');
                var btc = $('#batch_totes_container');
                btc.children().remove();
                if (totes[0].wsname == 'self') {
                    $('#Process, #NewBatch').removeAttr('disabled');
                    var i = 0;
                    for (var x = 1; x <= $('#NumTotes').val() ; x++) {
                        if (totes.length == i) {
                            btc.append(makeTote(x, $('#DefaultCells').val(), '', ''));
                        } else if (totes[i].position == x) {
                            btc.append(makeTote(totes[i].position, totes[i].cells, totes[i].tote, totes[i].tlocked));
                            i++;
                        } else {
                            btc.append(makeTote(x, $('#DefaultCells').val(), '', ''));
                        };
                    };
                    $('[data-toggle="tooltip"]').tooltip();
                    $('#AssignedZones').val(totes[0].zonelabel);
                } else {
                    btc.append('<h3>Selected batch belongs to another workstation (' + totes[0].wsname + ') and cannot be edited from this workstation.</h3>');
                    $('#AssignedZones').val('');
                };
            } else {
                $('#AssignedZones').val('');
                var btc = $('#batch_totes_container');
                btc.children().remove();
                $('#BatchStatus').val('Not Processed');
                $('#PrintToteLabels').attr('disabled', 'disabled');
                $('#Process, #NewBatch').removeAttr('disabled');
            };
        });
    };
});

// make an html tote
function makeTote(position, cells, toteID, tl) {
    return '<div class="row top-spacer">' +
                    '<div class="col-md-1">' +
                        '<input type="text" class="form-control" value="' + position + '" name="position" readonly="readonly"  />' +
                    '</div>' +
                    '<div class="col-md-1">' +
                        '<input type="text" class="form-control ' + tl + '" value="' + cells + '" name="cells"  ' + ((tl != '') ? 'readonly="readonly"' : '') + ' />' +
                    '</div>' +
                    '<div class="col-md-3">' +
                        '<div class="form-group has-feedback" style="margin-bottom:0px;">' +
                            '<input type="text" class="form-control modal-launch-style tote-modal ' + tl + '" name="tote-id" value="' + toteID + '" readonly="readonly" />' +
                            '<i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style tote-modal"></i>' +
                        '</div>' +
                    '</div>' +
                    '<div class="col-md-7">' +
                        '<button type="button" data-toggle="tooltip" data-original-title="Clear Tote" data-placement="left" class="btn btn-danger" name="Clear" ' + ((tl != '') ? 'disabled="disabled"' : '') + '><span class="glyphicon glyphicon-remove"></span></button> ' +
                        '<button type="button" data-toggle="tooltip" data-original-title="Print Tote Label" data-placement="left" class="btn btn-primary Print-Report" name="ToteLabels"><span class="glyphicon glyphicon-print"></span></button> ' +
                        '<button type="button" class="btn btn-primary" name="NextID" ' + ((tl != '') ? 'disabled="disabled"' : '') + '>Assign Next ID</button> ' +
                        ((tl != '') ? '<button data-toggle="tooltip" data-placement="right" data-original-title="Allocated in Open Transactions" type="button" class="btn btn-warning">' +
                            '<span class="glyphicon glyphicon-flag"></span></button>' : '') +
                    '</div>' +
                '</div>';
};