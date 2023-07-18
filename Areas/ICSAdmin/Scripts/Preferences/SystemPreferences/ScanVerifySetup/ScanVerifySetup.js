// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var activeInvMapID = 0;
    // initializes the datatable
    var scanVerifyTable = $('#ScanVerifyTable').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Admin/Preferences/getScanVerifyTable"
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    });

    $('#ScanVerifyTable').wrap('<div style="overflow-x:scroll"></div>');
    $('#SV_Container').css({ 'height': $(window).height() * 0.26, 'overflow-y' : 'scroll'});

    $('#ScanVerifyTable').on('click', 'tr', function () {
        var $this = $(this);
        activeInvMapID = $this.find('td:last').html();
        getScanVerify($this, activeInvMapID);
    });

    $('#addNewSV').click(function () {
        newSV($('#SV_Container'), $(this));
    });
    $('#addNewModalSV').click(function () {
        newSV($('#SVModal_Container'), $(this));
    });

    $('#SV_Container, #SVModal_Container').on('click', '.save-SV', function () {
        saveSV($(this), activeInvMapID);
    });

    $('#SV_Container, #SVModal_Container').on('click', '.remove-SV', function () {
        deleteSV($(this), activeInvMapID);
    });

    $('#SVDefaults').click(function () {
        activeInvMapID = 0;
        $('#ScanVerifyTable tr.active').removeClass('active');
        $('#SV_Container').html('');
        getModalSV();
        $('#scanverify_modal').modal('show');
    });
});

function deleteSV($remove, activeID) {
    var container = $remove.parents('.SVContainer');
    var ttype = container.find('.ttype select').attr('name');
    var seq = container.find('.seq input').attr('name');
    var field = container.find('.field select').attr('name');
    if ($remove.siblings('.save-SV').hasClass('new')) {
        container.remove();
        if (activeID != 0) {
            $('#addNewSV').removeAttr('disabled')
        } else {
            $('#addNewModalSV').removeAttr('disabled');
        };
    } else if (confirm('Click OK to delete the selected Scan Verification entry.')) {
        preferencesHub.server.deleteScanVerify(activeID, ttype, seq, field).done(function () {
            container.remove();
        });
    };
};

function saveSV($save, activeID) {
    var container = $save.parents('.SVContainer');
    container.find('.alert.alert-warning').remove();
    var SVContainer = $save.parent().siblings('.col-md-10');
    var ttype = SVContainer.find('.ttype select');
    var seq = SVContainer.find('.seq input');
    var field = SVContainer.find('.field select');
    var vtype = SVContainer.find('.vtype select');
    var start = SVContainer.find('.start input');
    var end = SVContainer.find('.end input');

    if (ttype.val() == ttype.attr('name') && seq.val() == seq.attr('name') && field.val() == field.attr('name') && vtype.val() == vtype.attr('name')
            && start.val() == start.attr('name') && end.val() == end.attr('name')) {
        return false;
    };

    if (!$.isNumeric(seq.val()) || seq.val() < 0) {
        container.prepend('<div class="alert alert-warning alert-custom" role="alert">Scan Sequence must be an integer 0 or greater.</div>');
        return false;
    } else if (!$.isNumeric(start.val()) || start.val() < 0) {
        container.prepend('<div class="alert alert-warning alert-custom" role="alert">Verify String Start must be an integer 0 or greater.</div>');
        return false;
    } else if (!$.isNumeric(end.val()) || end.val() < 0) {
        container.prepend('<div class="alert alert-warning alert-custom" role="alert">Verify String Length must be an integer 0 or greater.</div>');
        return false;
    } else {
        var enable = true;
        container.addClass('edit');
        var curr;
        var ottype; var oseq; var ofield;
        var active = activeID != 0 ? '#SV_Container' : '#SVModal_Container';
        $(active + ' .SVContainer:not(.edit)').each(function (index, element) {
            curr = $(this);
            ottype = curr.find('.ttype select').val().trim().toLowerCase();
            oseq = curr.find('.seq input').val().trim();
            ofield = curr.find('.field select').val().trim().toLowerCase();

            if (activeID != 0 && ottype == ttype.val().trim().toLowerCase() && oseq == seq.val() && ofield == field.val().trim().toLowerCase()) {
                enable = false;
            } else if (activeID == 0 && ottype == ttype.val().trim().toLowerCase() && ofield == field.val().trim().toLowerCase()) {
                enable = false;
            };
        });
        container.removeClass('edit');
        if (enable) {
            var prefs = [
                ttype.val(),
                seq.val(),
                field.val(),
                vtype.val(),
                start.val(),
                end.val(),
                ttype.attr('name'),
                seq.attr('name'),
                field.attr('name')
            ];
            preferencesHub.server.saveScanVerify(activeID, prefs).done(function () {
                ttype.attr('name', ttype.val());
                seq.attr('name', seq.val());
                field.attr('name', field.val());
                vtype.attr('name', vtype.val());
                start.attr('name', start.val());
                end.attr('name', end.val());
                if ($save.hasClass('new')) {
                    $save.removeClass('new');
                    if (activeID != 0) {
                        $('#addNewSV').removeAttr('disabled')
                    } else {
                        $('#addNewModalSV').removeAttr('disabled');
                    };
                };
            });
        } else if (activeID != 0) {
            container.prepend('<div class="alert alert-warning alert-custom" role="alert">Scan Verification not saved.  Entry is not unique.  Only one transaction type per scan sequence per field is allowed for custom settings.</div>');
        } else {
            container.prepend('<div class="alert alert-warning alert-custom" role="alert">Scan Verification not saved.  Entry is not unique.  Only one transaction type per field is allowed for defaults.</div>');
        };
    };
};

function newSV(container, $this) {
    $this.attr('disabled', 'disabled');
    appendScanVerify('', 0, '', '', 0, 0, true, container);
};

function getScanVerify($this, activeID) {
    var container = $('#SV_Container');
    container.html('');
    if ($this.hasClass('active')) {
        $this.removeClass('active');
        $('#addNewSV').attr('disabled', 'disabled');
    } else {
        $('#ScanVerifyTable tr.active').removeClass('active');
        $this.addClass('active');
        $('#addNewSV').removeAttr('disabled');
        preferencesHub.server.getScanVerify(activeID).done(function (SVEntries) {
            $.each(SVEntries, function (index, obj) {
                appendScanVerify(obj.TransType, obj.ScanSequence, obj.Field, obj.VType, obj.Start, obj.End, false, container);
            });
        });
    };
};

function appendScanVerify(ttype, seq, field, vtype, start, end, newEntry, container) {
    var SVEntry = $('<div class="row SVContainer" style="padding-top:10px;">\
                            <div class="col-md-10">\
                                <div class="row">\
                                    <div class="col-md-2 ttype">\
                                        <select class="form-control">\
                                            <option value="Pick">Pick</option>\
                                            <option value="Put Away">Put Away</option>\
                                            <option value="Count">Count</option>\
                                        </select>\
                                    </div>\
                                    <div class="col-md-2 seq">\
                                        <input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" class="form-control" placeholder="Scan Sequence" value="' + seq + '" name="' + seq + '" />\
                                    </div>\
                                    <div class="col-md-2 field">\
                                        <select class="form-control">\
                                            \
                                        </select>\
                                    </div>\
                                    <div class="col-md-2 vtype">\
                                        <select class="form-control">\
                                            <option value="All">All</option>\
                                            <option value="Range">Range</option>\
                                        </select>\
                                    </div>\
                                    <div class="col-md-2 start">\
                                        <input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" class="form-control" placeholder="Start" value="' + start + '" name="' + start + '" />\
                                    </div>\
                                    <div class="col-md-2 end">\
                                        <input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" class="form-control" placeholder="End" value="' + end + '" name="' + end + '" />\
                                    </div>\
                                </div>\
                            </div>\
                            <div class="col-md-2">\
                                <button type="button" class="btn btn-primary save-SV ' + (newEntry ? 'new' : '') + '" data-toggle="tooltip" data-placement="left" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button>\
                                <button type="button" class="btn btn-danger remove-SV" data-toggle="tooltip" data-placement="right" data-original-title="Remove"><span class="glyphicon glyphicon-remove"></span></button>\
                            </div>\
                        </div>');
    SVEntry.find('.field select').html($('#SVNames').html()).val(field).attr('name', field);
    SVEntry.find('.ttype select').val(ttype).attr('name', ttype);
    SVEntry.find('.vtype select').val(vtype).attr('name', vtype);
    SVEntry.find('.save-SV, .remove-SV').tooltip();
    SVEntry.appendTo(container);
};

function getModalSV() {
    var container = $('#SVModal_Container');
    preferencesHub.server.getSVDefaults().done(function (defaults) {
        container.html('');
        $.each(defaults, function (index, obj) {
            appendScanVerify(obj.TransType, obj.ScanSequence, obj.Field, obj.VType, obj.Start, obj.End, false, container);
        });
    });
};