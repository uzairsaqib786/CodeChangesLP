// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var importExportHub = $.connection.importExportHub;

var hash = document.location.hash;
var prefix = "tab_";
if (hash) {
    $('.nav-tabs a[href=' + hash.replace(prefix, "") + ']').tab('show');
}
else {
    $('.nav-tabs a[href=#status]').tab('show');
}

// Change hash for page-reload
$('.nav-tabs a').on('shown.bs.tab', function (e) {
    window.location.hash = e.target.hash.replace("#", "#" + prefix);
});

$(document).ready(function () {
    $('.datepicker').datetimepicker();

    $.connection.hub.start().done(function () {
        getStatus();
        getLastImportExport();
        getUnallocatedTransactionsCount();
        getPurgeTables();
        getXferModifyFields();
    });

    var gettingPickCount = false;
    var gettingPutCount = false;
    var gettingCountCount = false;

    $('#assigncountsbtn').click(function () {
        importExportHub.server.execLACounts().done(function (data) {
            gettingCountCount = true;
            getUnallocatedTransactionsCount();
        });
    });
    $('#assignpicksbtn').click(function () {
        importExportHub.server.execLAPicks().done(function (data) {
            gettingPickCount = true;
            getUnallocatedTransactionsCount();
        });
    });
    $('#assignputsbtn').click(function () {
        importExportHub.server.execLAPuts().done(function (data) {
            gettingPutCount = true;
            getUnallocatedTransactionsCount();
        });
    });

    function getStatus() {
        spinOn();
        importExportHub.server.getStatus().done(function (data) {
            $('#importcount>span').text(data['Import Count']);
            $('#openpicks>span').text(data['Open Picks']);
            $('#completedpicks>span').text(data['Completed Picks']);
            $('#openputaways>span').text(data['Open Puts']);
            $('#closedputaways>span').text(data['Closed Puts']);
            $('#opencounts>span').text(data['Open Counts']);
            $('#closedcounts>span').text(data['Closed Counts']);
            $('#adjustments>span').text(data['Adjustments']);
            $('#locationchanges>span').text(data['Location Changes']);
            $('#reprocess>span').text(data['ReProcess']);
            spinOff();
        });
    }

    function getLastImportExport() {
        importExportHub.server.getLastImportExport().done(function (data) {
            $('#lastimport').text(data['LastImport']);
            $('#lastexport').text(data['LastExport']);
        });
    }

    function getUnallocatedTransactionsCount() {
        importExportHub.server.getUnallocatedTransactionsCount().done(function (data) {
            var pickCount = parseInt(data['Picks']);
            var putCount = parseInt(data['Put Aways']);
            var countCount = parseInt(data['Counts']);
            $('#unallocatedpicks').text(pickCount);
            $('#unallocatedputaways').text(putCount);
            $('#unallocatedcounts').text(countCount);
            if (pickCount <= 0) gettingPickCount = false;
            if (putCount <= 0) gettingPutCount = false;
            if (countCount <= 0) gettingCountCount = false;
            // if still looking for any of these, keep checking
            if (gettingPickCount || gettingPutCount || gettingCountCount) window.setTimeout(getUnallocatedTransactionsCount(), 1500);
        });
    }

    function getPurgeTables() {
        importExportHub.server.getPurgeTables().done(function (data) {
            var $purgeTBody = $('#purgetable>tbody');
            $purgeTBody.empty();
            $.each(data, function (k, v) {
                var row = '<tr>';
                row += '<td>' + v['TableName'] + '</td>';
                row += '<td><input name="' + (v['PurgeID']) + '.PurgeDays" class="form-control" type="number" value="' + v['Purge Days'] + '"></td>';
                row += '<td><input name="' + (v['PurgeID']) + '.Archive" type="checkbox" ' + (v['Archive'] ? 'checked' : '') + '></td>';
                $purgeTBody.append(row);
            });
        });
    }

    function getXferModifyFields() {
        importExportHub.server.getXferModifyFields().done(function (data) {
            var $invModifyFields = $('#invmodifytable>tbody');
            $invModifyFields.empty();
            $.each(data, function (k, v) {
                var row = '<tr>';
                row += '<td>' + v['Fieldname'] + '</td>';
                row += '<td><input type="checkbox" name="' + (v['MID']) + '.Modify"' + (v['Modify Flag'] ? 'checked' : '') + '></td>';
                $invModifyFields.append(row);
            });
        });
    }

    $(window).unload(function () {
        if ($('.tab-pane.active input:focus').length > 0)
            saveTab();
    });

    $('.tab-pane').on('change', ':input', saveTab);

    function saveTab() {
        var $activePane = $('.tab-pane.active');
        console.log("Saving tab: " + $activePane[0].id);

        switch ($activePane[0].id) {
            case "systemsettings":
                importExportHub.server.updateSystemSettings(createModel($activePane));
                break;
            case "transfersettings":
                importExportHub.server.updateTransferSettings(createModel($activePane));
                break;
            case "assignlocations":
                importExportHub.server.updateAssignLocations(createModel($activePane));
                break;
            case "managedata":
                importExportHub.server.updateManageData(createModel($activePane));
                if ($('[name="ExportInvMapType"]').val() == "File") {
                    $('#ExpInvMap').removeAttr('disabled', 'disabled');
                } else {
                    $('#ExpInvMap').attr('disabled', 'disabled');
                };

                if ($('[name="ImportInvMapType"]').val() == "File") {
                    $('#ImpInvMap').removeAttr('disabled', 'disabled');
                } else {
                    $('#ImpInvMap').attr('disabled', 'disabled');
                };

                if ($('[name="ExportInvType"]').val() == "File") {
                    $('#ExpInv').removeAttr('disabled');
                } else {
                    $('#ExpInv').attr('disabled', 'disabled');
                };

                if ($('[name="ImportInvType"]').val() == "File") {
                    $('#ImpInv').removeAttr('disabled');
                } else {
                    $('#ImpInv').attr('disabled', 'disabled');
                };

                if ($('[name="ExportEmpType"]').val() == "File") {
                    $('#ExpEmps').removeAttr('disabled');
                } else {
                    $('#ExpEmps').attr('disabled', 'disabled');
                };

                if ($('[name="ImportEmpType"]').val() == "File") {
                    $('#ImpEmps').removeAttr('disabled');
                } else {
                    $('#ImpEmps').attr('disabled', 'disabled');
                };

                break;
            case "archivepurge":
                importExportHub.server.updateArchivePurge(createArray(createModel($activePane)));
                break;
            case "invfields":
                importExportHub.server.updateInvFields(createArray(createModel($activePane)));
                break;
            case "filebackup":
                importExportHub.server.updateFileBackup(createModel($activePane));
                break;
            case "ftp":
                importExportHub.server.updateFTP(createModel($activePane));
                break;
            case "inventory":
                importExportHub.server.updateInventory(createModel($activePane));
                break;
        }
    }

    function createModel($activePane) {
        var selector = 'input, select'; //ignores buttons which :input does not do

        var model = {};

        $activePane.find(selector).each(function () {
            if ($(this).is('input:checkbox')) {
                model[this.name] = $(this).is(':checked');
            }
            else model[this.name] = this.value;
        });

        console.log(model);
        return model;
    }

    function createArray(model) {
        var ret = {};

        // Create the model objects
        $.each(model, function (name, val) {
            var splt = name.split(".");
            var idx = splt[0];
            var field = splt[1];
            if (ret[idx] === undefined) ret[idx] = {};
            ret[idx][field] = val;
        });

        // Add the objects to list
        ret = $.map(ret, function (val, idx) {
            val['ID'] = idx;
            return val;
        });
        return ret;
    }

    $('#statusRefresh').click(function (e) {
        getStatus();
    });
    $('#eventLog').click(function () { location.href = '/EventLog?App=IE'; });
    $('#transJournal').click(function () { location.href = '/Transactions?App=IE&viewToShow=2'; });

    $('#FTP_Close').click(function () {
        // switch tabs before activating the button to launch a modal
        $('#tablist').children(':nth-child(9)').children().click();
        $('#FTPSettings').click();
    });

    function spinOn() {
        $('.statusVal>span').text('');
        $('.statusVal>i').show();
    }

    function spinOff() {
        $('.statusVal>i').hide();
    }

    // moves old qualifying records from open transactions and other tables to their respective history table.  Moves old history table records to their respective archive tables.
    // deletes from the original and history tables where they were moved and deletes any old archive records.  "Old" is defined by the user on the purge tab in days to say how long records should be kept.
    $('#PurgeNow').click(function () {
        if (confirm('Click OK to begin the purge process.')) {
            var $this = $(this);
            $this.attr('disabled', 'disabled');
            importExportHub.server.purgeHistory().done(function (result) {
                if (!result) MessageModal('Error', 'An error occurred during the purge and archive process.  Contact Scott Tech if the problem persists.');
                $this.removeAttr('disabled');
            });
        };
    });
});
