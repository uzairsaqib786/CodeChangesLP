// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var batchHub = $.connection.processPutAwaysHub;
var BatchIDTA;
$(document).ready(function () {
    $('a[href="#ProcessTab"]').click(function () {
        batchTimer.startTimer();
    });
    var BatchIDTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller get item
            url: ('/IM/ProcessPutAways/BatchIDTypeahead?BatchID='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#BatchID').val();
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

    $('#BatchID').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "BatchID",
        displayKey: 'BatchID',
        source: BatchIDTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Batch ID</p><p class="typeahead-header" style="width:50%;">Zone Label</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:50%;">{{BatchID}}</p><p class="typeahead-row" style="width:50%;">{{ZoneLabel}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        toteDT.draw();
        $('#Item').focus();
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '650px').css('left', 'auto')
    });

    $('#Item').on('keyup', function (e) {
        if (e.keyCode == 13) {
            $('#AssignTrans').click();
        };
    });

    $('#Item').on('input', function () {
        var $this = $(this);
        $this.attr('name', 'not-applied');
        if ($this.val().trim() != '') {
            $('#AssignTrans').removeAttr('disabled');
        } else {
            $('#AssignTrans').attr('disabled', 'disabled');
        };
    });

    $(document.body).on('click', '#PrintOffCar', function () {
        printOffCarList($('#BatchID').val(), '', false);
    });

    // print a list of put aways that are off carousel
    function printOffCarList(batchID) {
        var pd = ($('#PrintDirect').val().toLowerCase() == 'true');
        
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintOffCarList/', {
            batchID: batchID,
            printDirect: pd
        }, pd,'report', 'Off Carousel Put Away List Preview', "", "");
    };

    // complete our batch
    $('#CompleteBatch').click(function () {
        if (confirm('Click OK to complete this batch.')) {
            var batchID = $('#BatchID');
            batchHub.server.completeBatch(batchID.val()).done(function (batch) {
                if (batch.Success) {
                    if ($('#AutoPrintOffCarPutList').val() == 'true' || confirm('Click OK to print an Off-Carousel Put Away List.')) {
                        printOffCarList(batchID.val());
                    };

                    var bid = $('#BatchID');
                    var bids = $('#BatchIDSetup');
                    if (bids.val() == bid.val()) {
                        bids.typeahead('val', '');
                        $('[name="tote-id"]').val('');
                        $('#BatchStatus').val('Not Processed');
                        $('#AssignedZones').val('');
                        $('#batch_totes_container').html("");
                    };

                    if (batch.NextBatch != '') {
                        setupClicked = false;
                        var e = jQuery.Event("keyup");
                        e.which = 13;
                        e.keyCode = 13;
                        bid.typeahead('val', batch.NextBatch);
                        bid.trigger(e);
                    } else {
                        bid.typeahead('val', '');
                        $('#NextLoc').val("");
                        $('#NextPosition').val("");
                        $('#NextCell').val("");
                        $('a[href="#SetupTab"]').click();
                    };

                } else {
                    MessageModal('Error', 'An error occurred while attempting to complete the current batch.  Check the error log for details.');
                };
            });
        };
    });

    // make sure that our typing doesn't cause an issue with too many queries
    var batchTimer = mkTimer(function () {
        toteDT.draw();
    }, 200);
    $('#BatchID').on('input', function () {
        batchTimer.startTimer();
    });

    $('#BatchID').on('keyup', function (e) {
        if (e.keyCode == 13) {
            $('#BatchID').focusout();
        };
    });

    $('#BatchID').focusout(function () {
        if ($(this).val().trim() != '') {
            setTimeout(function () {
                if (setupClicked) {
                    setupClicked = false;
                    $('#BatchID').typeahead('val', '');
                } else {
                    batchHub.server.batchExists($('#BatchID').val()).done(function (exists) {
                        if (exists) {
                            $('#Item').focus();
                        } else {
                            MessageModal('Warning', 'Invalid Batch ID entered. <br> ' +
                                                    'This Batch ID either does not exists or is assigned to a different workstation. <br> ' +
                                                    'Use the Tote Setup tab to create a new batch or choose an existing batch for this workstation.', function () {
                                                    });
                            $('#BatchID').val('');
                            $(document.body).find('#BatchID').focus()
                        };
                    });
                };
            }, 100);
        } else {
            setupClicked = false;
        };
        batchTimer.startTimer();
    });

    var setupClicked = false;
    $(document.body).on('click', 'a[href="#SetupTab"]', function () {
        setupClicked = true;
    });

    $('#SelectPosition, #SelectTote').on('keyup', function (e) {
        if (e.keyCode == 13) {
            $('#GoSelected').click();
        };
    });

    // clicks the next tote that has space available in the batch
    $('#GoTo').click(function () {
        var np = $('#NextPosition').val();
        if (np.trim() != '') {
            var r = toteDT.column(0).data().indexOf(np);
            if (r >= 0) {
                var r$ = toteDT.row(r).nodes().to$();
                if (!r$.hasClass('active')) {
                    r$.click();
                };
            };
        };
    });

    // get the tote id of the specified position
    $('#SelectPosition').on('input', function () {
        var tote = $('#TotesTable_info').html();
        var $this = $(this);
        tote = tote.substring(tote.indexOf('to') + 2, tote.indexOf('of')).trim();
        if (tote == 0) {
            $this.val(null);
        } else {
            setNumericInRange($this, 1, tote);
        };
        if ($this.val().trim() != '') {
            var r = toteDT.column(0).data().indexOf($this.val());
            if (r >= 0) { $('#SelectTote').val(toteDT.row(r).data()[1]); } else { $('SelectTote').val(null) };
        } else {
            $('#SelectTote').val(null);
        };
    });

    // get the position of the specified tote
    $('#SelectTote').on('input', function () {
        var $this = $(this);
        if ($this.val().trim() == '') {
            $('#SelectPosition').val(null);
        } else {
            var r = toteDT.column(1).data().indexOf($this.val());
            if (r >= 0) {
                $('#SelectPosition').val(toteDT.row(r).data()[0]);
            } else {
                $('#SelectPosition').val(null);
            };
        };
    });

    // show the contents of the tote
    $('#ViewToteTransInfo').click(function () {
        if ($('#TotesTable tbody tr.active').length > 0) {
            var openWindow = window.open("/IM/ProcessPutAways/ToteTransView?popup=True&BatchID=" + $('#BatchID').val() + "&ToteNum=" + toteDT.row($('#TotesTable tbody tr.active')[0]).data()[0] + "&ToteID=" + toteDT.row($('#TotesTable tbody tr.active')[0]).data()[1] + "&ZoneLabel=" + toteDT.row($('#TotesTable tbody tr.active')[0]).data()[4], '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
            var $handle = $(openWindow);
            try {
                //Changed to user Signalr hub call on page close, as $handle events did not work properly in Internet Explorer
                //$handle[0].focus();
                //$handle.one('load', function () {
                //    $handle.one('unload', function () {
                //        toteDT.draw();
                //    });
                //});
            } catch (e) {
                console.log('Exception: ', e)
                MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.');
            };
        };
    });

    //Event gets raised when a change is made to a Tote in this batch from another screen
    batchHub.client.refreshTotes = function(){
        toteDT.draw();
    }

    // mark a tote as full by changing its cells
    $('#MarkToteFullButt').click(function () {
        var totenum = toteDT.row($('#TotesTable tbody tr.active')[0]).data()[0];
        var cell = parseInt(toteDT.row($('#TotesTable tbody tr.active')[0]).data()[3]);
        var batchid = $('#BatchID').val();
        if (confirm('Click OK to mark this tote as being full.')) {
            batchHub.server.markToteFull(totenum, cell, batchid).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred marking this tote as full");
                } else {
                    toteDT.draw();
                };
            });
        };
    });

    // highlight the selected tote
    $('#GoSelected').click(function () {
        var p = $('#SelectPosition').val();
        if (parseInt(p) > 0) {
            var r = toteDT.column(0).data().indexOf(p);
            var r$ = toteDT.row(r).nodes().to$();
            if (!r$.hasClass('active')) {
                r$.click();
            };
            $('#SelectPosition, #SelectTote').val(null);
        } else {
            MessageModal('Error', 'The selected position and/or tote ID was not found in the table.');
        };
    });

    // open the delete/deallocate/clear batch/tote modal
    $('#DeleteBatch').click(function () {
        var tote = '';
        if ($('tr.active').length > 0) {
            tote = toteDT.row('tr.active').data()[1];
        };
        DeleteBatchModal($('#BatchID').val(), tote, 'Put Away', toteDT, 'Process', function (closer) {
            if (closer != undefined && closer != null) {
                if (closer.toLowerCase() == 'executedelete' || closer.toLowerCase() == 'delall') {
                    $('#batch_modal').one('hidden.bs.modal', function () {
                        var bid = $('#BatchID');
                        var bids = $('#BatchIDSetup');
                        if (bids.val() == bid.val()) {
                            bids.typeahead('val', '');
                            $('[name="tote-id"]').val('');
                            $('#BatchStatus').val('Not Processed');
                            $('#AssignedZones').val('');
                        };
                        bid.typeahead('val', '');
                        //BatchIDTASetup.clearRemoteCache();
                        //BatchIDTA.clearRemoteCache();
                        bid.focus();
                    });
                };
            };
        });
    });
});