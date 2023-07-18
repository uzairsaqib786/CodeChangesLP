// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// Groups that are built into PickPro and cannot be deleted
var defaultGroups = ['Administrator', 'Bulk Operator', 'Car/Off Operator', 'Carousel Operator', 'Fork Truck Operator', 'PickPro Admin', 'Supervisor'];

// Handles default column sequence rearrangement
jQuery(function ($) {
    // draggable definitions here
    var panelList = $('#assigned, #unassigned');
    // initialize the sorting plugin
    panelList.sortable({
        // prevents sorting on .static <li>s
        items: ':not(.static)',
        helper: 'clone',
        // links two individual <ul>
        connectWith: '.linked',
        start: function (e, ui) {
            $('#currentEmployeeGroup').attr('disabled', 'disabled');
            $('#saveGroup').removeAttr('disabled');
            $('#updateGroup').attr('disabled', 'disabled');
        }
    }).disableSelection();

    $(document.body).on('click', '#unassigned li, #assigned li', function () {
        $('#currentEmployeeGroup').attr('disabled', 'disabled');
        $('#saveGroup').removeAttr('disabled');
        $('#updateGroup').attr('disabled', 'disabled');
        var $this = $(this);
        if ($this.hasClass("static")) { return false; };
        var destination = $this.parent().attr('id') == 'assigned' ? $('#unassigned') : $('#assigned');
        // don't move the column labels
        var stored = $this.text();
        $this.remove();
        destination.append("<li class='btn btn-primary btn-sm'>" + stored + "</li>");
        // make sure all lists are updated when there is a change
        $('#unassigned, #assigned').trigger("sortupdate");
    });

    // adds all rights to the selected employee group
    $('#addAll').click(function () {
        var assigned = $('#assigned');
        var $this;
        $('#unassigned li:not(.static)').each(function () {
            $this = $(this);
            assigned.append('<li class="btn btn-primary btn-sm">' + $this.text() + '</li>');
            $this.remove();
        });
        $('#currentEmployeeGroup').attr('disabled', 'disabled');
        $('#saveGroup').removeAttr('disabled');
        $('#updateGroup').attr('disabled', 'disabled');
        $('#assigned, #unassigned').trigger('sortupdate');
    });
    // removes all rights from the selected employee group
    $('#removeAll').click(function () {
        var unassigned = $('#unassigned');
        var $this;
        $('#assigned li:not(.static)').each(function () {
            $this = $(this);
            unassigned.append('<li class="btn btn-primary btn-sm">' + $this.text() + '</li>');
            $this.remove();
        });
        $('#currentEmployeeGroup').attr('disabled', 'disabled');
        $('#saveGroup').removeAttr('disabled');
        $('#updateGroup').attr('disabled', 'disabled');
        $('#assigned, #unassigned').trigger('sortupdate');
    });
});

