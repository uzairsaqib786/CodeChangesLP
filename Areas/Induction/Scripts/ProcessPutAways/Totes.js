// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var lby;
$(document).ready(function () {
    var pd = $('#PrintDirect').val() == 'true';
    // limited the container's length so that we don't scroll the modal, only the container
    addResize(function () { $('#tote_container').css('max-height', $(window).height() * 0.65); });
    // show or hide the unmanaged totes area so that the user can(not) assign a tote that the system does not have details stored for.
    $('#ManageTotes').val().toLowerCase() == 'true' ? $('#ManageTotes').parent().hide() : $('#ManageTotes').parent().show();
    // want to make sure that the assigned number of cells makes sense for a tote that is not stored.
    $(document.body).on('input', '[name="modal_cells"], #UnmanagedCells', function () {
        setNumericInRange($(this), 0, null);
    });

    // enable or disable the set buttons for an unmanaged tote.
    $('#UnmanagedCells, #UnmanagedTote').on('input', function () {
        var c$ = $('#UnmanagedCells');
        var t$ = $('#UnmanagedTote');
        if (c$.val().trim() == '' || t$.val().trim() == '') {
            $('.set-tote-other').attr('disabled', 'disabled');
        } else {
            $('.set-tote-other').removeAttr('disabled');
        };
    });

    // capture the enter key to set the unmanaged tote when there was focus in the cells or tote field.
    $('#UnmanagedTote, #UnmanagedCells').on('keyup', function (e) {
        if (e.keyCode == 13) {
            $('.set-tote-other').click();
        };
    });

    // enables or disables the save, set and print buttons when there is unsaved input on a tote that is managed.
    $(document.body).on('input', '[name="modal_cells"],[name="modal_tote-id"]', function () {
        var $this = $(this);
        $this.parent().parent().find('.print-tote,.set-tote').attr('disabled', 'disabled');
        $this.parent().parent().find('.save-tote').removeAttr('disabled');
    });

    // adds a new managed tote.
    $('#tote_add').click(function () {
        $('#tote_container').append(makeToteModal('new-tote', '', 0, $('#ShowButtons').val(), false, lby));
        //sets focus to the new tote field.
        console.log($('#tote_container .row.top-spacer:nth-last-child(1) [name="modal_tote-id"]').focus())
        $(this).attr('disabled', 'disabled');
    });

    // removes a managed tote
    $(document.body).on('click','.remove-tote', function () {
        var tote = $(this).parent().parent().attr('name');
        if (tote != 'new-tote') {
            if (confirm('Click OK to delete the Tote with ID ' + tote)) {
                toteHub.server.deleteTote(tote).done(function (deleted) {
                    if (deleted) {
                        $('[name="' + tote + '"]').remove();
                    } else {
                        MessageModal('Error', 'There was an error deleting the tote.  Please try again.');
                    };
                });
            };
        } else {
            $('[name="new-tote"]').remove();
            $('#tote_add').removeAttr('disabled');
        };
    });

    // saves changes to a tote
    $(document.body).on('click', '.save-tote', function () {
        //insert save here
        var $this = $(this);
        
        var oldToteID = $this.parent().parent().attr('name');
        var tote = $this.parent().parent().children().children('[name="modal_tote-id"]');
        tote.addClass('being-set');
        var newToteID = tote.val();
        var cell = $this.parent().parent().children().children('[name="modal_cells"]').val();
        var duplicate = false;
        $.each($('[name="modal_tote-id"]:not(.being-set)'), function (i, v) {
            if ($(v).val() == newToteID) {
                duplicate = true;
            };
        });
        tote.removeClass('being-set');
        if (duplicate) {
            MessageModal('Error', 'The tote ID is duplicated in another managed tote, so it cannot be saved.  Edit the duplicated totes before attempting to save again.');
        } else {
            if (oldToteID != "new-tote") {
                // update tote
                toteHub.server.updateTotes(newToteID, cell, oldToteID).done(function (mess) {
                    if (mess == "Error") {
                        MessageModal("Error", "An error has occurred updating the tote id");
                    } else {
                        $this.parent().parent().attr('name', newToteID);
                        $this.parent().parent().find('.print-tote,.set-tote').removeAttr('disabled');
                        $this.attr('disabled', 'disabled');
                    };
                });
            } else {
                // addTote
                toteHub.server.addTote(newToteID, cell).done(function (added) {
                    if (added) {
                        $this.parent().parent().attr('name', newToteID);
                        $('#tote_add').removeAttr('disabled');
                        $this.parent().parent().find('.print-tote,.set-tote').removeAttr('disabled');
                        $this.attr('disabled', 'disabled');
                    } else {
                        MessageModal('Error', 'There was an error while saving.  Please try again.');
                    };
                });
            };
        };
    });

    // prints a specific tote label
    $(document.body).on('click', '.print-tote', function () {
        var PrintToteID = $(this).parent().parent().attr('name');
        printPrevToteFunc("tote", PrintToteID)
    });

    //from toteid typeahead
    var FromToteIDTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/IM/Menu/CreateFromToteIDTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#FromToteID').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    FromToteIDTA.initialize();

    $('#FromToteID').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "FromToteIDTA",
        displayKey: 'ToteID',
        source: FromToteIDTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Tote ID</p><p class="typeahead-header" style="width:50%;">Cells</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:50%;">{{ToteID}}</p><p class="typeahead-row" style="width:50%;">{{Cell}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        $('#ToToteID').removeAttr('disabled');
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', "300px");
    });

    //to toteid typeahead
    var ToToteIDTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/IM/Menu/CreateToToteIDTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#ToToteID').val() + "&FromTote=" + $('#FromToteID').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    ToToteIDTA.initialize();

    $('#ToToteID').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "ToToteIDTA",
        displayKey: 'ToteID',
        source: ToToteIDTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Tote ID</p><p class="typeahead-header" style="width:50%;">Cells</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:50%;">{{ToteID}}</p><p class="typeahead-row" style="width:50%;">{{Cell}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        $('#TotePrintRange').removeAttr('disabled');
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', "300px");
    });

    // disable the input and button if we haven't chosen a starting position yet
    $('#FromToteID').on('input', function () {
        $('#ToToteID,#TotePrintRange').attr('disabled', 'disabled');
        $('#ToToteID').val('');
    });

    // disable the print button if we haven't chosen an end position yet
    $('#ToToteID').on('input', function () {
        $('#TotePrintRange').attr('disabled', 'disabled');
    });

    // print a range of tote labels
    $('#TotePrintRange').click(function () {
        printPrevToteFunc("Range", "", $('#FromToteID').val(), $('#ToToteID').val(), '');
    });

    // clear out our inputs when we close.
    $('#tote_modal').on('hidden.bs.modal', function () {
        $('#tote_container').html("");
        $('#TotePrintRange').attr('disabled', 'disabled');
        $('#FromToteID').val("");
        $('#ToToteID').val("");
        $('#ToToteID').attr('disabled', 'disabeld');
    });

    // print a tote label
    $(document.body).on('click', '[name="ToteLabels"]', function () {
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintPrevToteContentsLabel', {
            ToteID:$(this).parent().parent().find('input.modal-launch-style').val(),
            ZoneLabel:"",
            TransType:"Put Away",
            printDirect: pd,
            ID:-1, 
            BatchID: $('#BatchIDSetup').val()
        }, pd,'label', "Tote Label Preview");
    });

    // print all tote labels in the batch
    $('#PrintToteLabels').click(function () {
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintPrevToteContentsLabel', {
            ToteID: -1,
            ZoneLabel: "",
            TransType: "Put Away",
            printDirect: pd,
            ID: -1,
            BatchID: $('#BatchIDSetup').val()
        }, pd, 'label', "Tote Label Preview");
    });

    function printPrevToteFunc(Type, ToteID, sTote, eTote, batch) {
        var ident = 0;
        if (Type.toLowerCase() == "tote") {
            //print single tote id
            sTote = '';
            eTote = '';
            batch = '';
        } else if (Type.toLowerCase() == 'batch') {
            // print batch
            ToteID = '';
            sTote = '';
            eTote = '';
        } else {
            //print range tote id
            ident = 1;
            ToteID = '';
            batch = '';
        };
        getLLPreviewOrPrint('/IM/ProcessPutAways/PrintPrevToteManLabel', {
            ToteID:ToteID,
            Ident:ident,
            FromTote:sTote,
            ToTote:eTote,
            PrintDirect: pd,
            BatchID: batch
        }, pd,'label', 'Print Tote Label');
    };

    // set a tote from the modal
    $(document.body).on('click', '.set-tote, .set-tote-other', function () {
        var tote = '';
        var cells = 0;
        var $this = $(this);
        if ($this.hasClass('set-tote-other')) {
            tote = $('#UnmanagedTote').val();
            cells = $('#UnmanagedCells').val();
        } else {
            var $row = $this.parent().parent();
            tote = $row.find('[name="modal_tote-id"]').val();
            cells = $row.find('[name="modal_cells"]').val();
        };
        var tm = $('#tote_modal');
        // we stored the "sender" in the data-setID attribute of the input element, so we'll add a class to it so that we can differentiate between it and others.
        tm.data('setID').addClass('being-set');
        var pos = 0;
        // for each of the totes in the batch, check the value.  If it is not the element we are setting we must not have duplicates.
        $.each(tm.data('container').children(), function (index, elem) {
            var $t = $(elem).find('input' + tm.data('tote') + ':not(.being-set)');
            if ($t.length > 0) {
                if ($t.val().toLowerCase() == tote) {
                    pos = $(elem).find('input[name="position"]').val();
                };
            };
        });
        tm.data('setID').removeClass('being-set');
        if (pos == 0) {
            tm.data('setID').val(tote);
            tm.data('setCells').parent().parent().parent().find('[name="cells"]').val(cells);
            tm.modal('hide');
        } else {
            MessageModal('Error', 'Cannot set the selected tote because it is already set in the batch.');
        };
    });
});

var toteHub = $.connection.toteManagerHub;

// start the tote modal preinitialized.
function launchToteModal(launchedBy, container, tote_selector, cells) {
    lby = launchedBy;
    var tm = $('#tote_modal');
    toteHub.server.getTotes().done(function (totes) {
        var showSaveRemove = $('#ShowButtons').val().toLowerCase() == 'true';
        // totes = list of object .ToteID, .Cell, .Allocated
        for (var x = 0; x < totes.length; x++) {
            $('#tote_container').append(makeToteModal(totes[x].ToteID, totes[x].ToteID, totes[x].Cell, showSaveRemove, totes[x].Allocated, launchedBy));
        };
        if (showSaveRemove) {
            $('#tote_add').show();
        } else {
            $('#tote_add').hide();
        };
        if (launchedBy == "MenuButt") {
            $('#UnmanagedRow').attr('hidden', 'hidden');
        } else {
            $('#UnmanagedRow').removeAttr('hidden');
            $('#UnmanagedTote').val('');
            $('#UnmanagedCells').val($('#DefaultCells').val());
            
            tm.data('setID', launchedBy);
            tm.data('container', container);
            tm.data('tote', tote_selector);
            tm.data('setCells', cells);
        };
        tm.modal('show').one("shown.bs.modal", function () {
            $('#UnmanagedTote').focus();
        });
    });
};

// make an html element for the tote parameters provided
function makeToteModal(toteName, toteID, cells, showSaveRemove, isAllocated, launched) {
    return "<div class='row top-spacer' name='" + toteName + "'><div class='col-md-6'><input placeholder='Tote ID' type='text' name='modal_tote-id' class='form-control' value='" + toteID + "' /></div>" +
           "<div class='col-md-2'><input placeholder='Cells' type='text' name='modal_cells' class='form-control' value='" + cells + "' /></div>" +
           (showSaveRemove && !isAllocated ? "<div class='col-md-1'><button type='button' class='btn btn-danger remove-tote'><span class='glyphicon glyphicon-remove'></span></button></div>" : "") +
           "<div class='col-md-1'><button type='button' class='btn btn-primary print-tote'><span class='glyphicon glyphicon-print'></span></button></div>" +
           (showSaveRemove && !isAllocated ? "<div class='col-md-1'><button disabled='disabled' type='button' class='btn btn-primary save-tote'><span class='glyphicon glyphicon-floppy-disk'></span></button></div>" : "") +
           "<div class='col-md-1'>" +
               (isAllocated ? '<span class="label label-default">Allocated</span>' : (launched != "MenuButt" ? "<button type='button' class='btn btn-primary set-tote'><span class='glyphicon glyphicon-edit'></span></button>" : "")) +
           "</div>" +
       "</div>";
};