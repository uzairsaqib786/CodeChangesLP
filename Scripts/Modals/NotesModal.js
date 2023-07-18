// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the notes modal
var trigger_notes_modal;

$(document).ready(function () {
    trigger_notes_modal = function (sender) {
        $('#notes_sender').val('#' + sender.attr('id'));
        var notes = $('#notes_textarea');
        notes.val(sender.val());

        if (sender.hasClass('notes-modal-edit')) {
            notes.removeAttr('readonly');
            $('#notes_submit').show();
        } else {
            notes.attr('readonly', 'readonly');
            $('#notes_submit').hide();
        };

        $('#notes_modal').modal('show');
    };

    // launches the notes modal as readonly or as editable
    $(document.body).on('click', '.notes-modal, .notes-modal-edit', function () {
        trigger_notes_modal($(this));
    });

    // handles an edited notes section and sets the sender to the modal modified value
    $(document.body).on('click', '#notes_submit', function () {
        $($('#notes_sender').val()).val($('#notes_textarea').val());
    });
});