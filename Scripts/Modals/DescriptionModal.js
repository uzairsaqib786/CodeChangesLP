// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the item description modal
var trigger_description_modal;

$(document).ready(function () {
    // holds the sender to set value for when the submit button is clicked if modal is an edit
    var descriptionsender;

    trigger_description_modal = function (edit) {
        if (edit) {
            $('#descEdit_label').removeAttr('hidden');
            $('#description_submit').show();
            $('#description_textarea').removeAttr('readonly');
        } else {
            $('#descEdit_label').attr('hidden', 'hidden');
            $('#description_submit').hide();
            $('#description_textarea').attr('readonly', 'readonly');
        };
        var description_modal = $('#description_textarea');
        description_modal.val(descriptionsender.val());
        $('#description_modal').modal('show');
    };

    // launches the description modal without editing capabilities
    $(document.body).on('click', '.description-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        descriptionsender = $this;
        trigger_description_modal(false);
    });

    // launches the description modal with editing capabilities
    $(document.body).on('click', '.description-modal-edit', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            $this = $this.siblings('input');
        };
        descriptionsender = $this;
        trigger_description_modal(true);
    });

    // Handles submission of a new/edited item description
    $(document.body).on('click', '#description_submit', function () {
        descriptionsender.val($('#description_textarea').val());
        descriptionsender.trigger('input');
    });
});