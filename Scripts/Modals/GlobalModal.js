// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var activeModal = false;

$(document).ready(function () {
    // Handles modal activity.  Shortcuts are not enabled when there is an active modal.
    $(document.body).on('show.bs.modal', '.modal', function () {
        activeModal = true;
    });

    // Gives focus to the first visible enabled input in the modal's body.
    $(document.body).on('shown.bs.modal', '.modal', function () {
        //$('.modal.in .modal-body input:visible:not([readonly], [disabled], .no-focus):first').focus();
    });

    // Handles modal activity.  Shortcuts are not enabled when there is an active modal.
    $(document.body).on('hide.bs.modal', '.modal', function () {
        activeModal = false;
    });

    // Handles disabled modal inputs.  If the input is disabled then so is the <i> or <span> with the class glyphicon
    // by preventing the event from propagating.  stopImmediatePropagation() will stop the event before it executes at any
    // level in the DOM
    $(document.body).on('click', '.glyphicon', function (e) {
        if ($(this).siblings('input').attr('disabled') == 'disabled') {
            e.stopImmediatePropagation();
        };
    });
});