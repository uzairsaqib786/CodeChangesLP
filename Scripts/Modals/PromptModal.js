// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*
    Use: provides a singleton style instance of a customizable modal.
    Parameters:
        pHeader: HTML to include as the modal title.
        pBody: Array of objects representing inputs or textareas containing the following properties:
            rows: optional integer which provides the number of rows to provide in a textarea if it is necessary.  Where possible an <input /> is used instead (row = 1),
            textarea: optional boolean which provides an override to rows.  If you have a single row and desire a textarea anyways this should be true,
            value: optional value of the textarea or input tag that will be appended to the modal,
            classes: optional classes to be applied to the <input /> or <textarea>.  All input tags automatically have class form-control.  All textareas automatically have classes form-control and no-horizontal,
            label: optional <label> contents for the input defined.
        pFooter: Array of objects representing buttons containing the following properties:
            classes: optional classes to add to the button (btn-default, btn-primary, etc.) Default: btn,
            onclick: optional function to apply to the button's click (can use this to set a value, etc.),
            dismiss: optional boolean (default: false) which assigns data-dismiss="modal" equivalent to cause the modal to close when the button is clicked,
            text: text or html that should be on the button (example: "Submit" or <span class="glyphicon glyphicon-floppy-disk"></span> (save btn))
        pOpenCallback: function to call when the modal has been completely shown.
        pCloseCallback: function to call when the modal has been completely hidden.

*/
function PromptModal(pHeader, pBody, pFooter, pOpenCallback, pClosecallback, modalCSS) {
    // setup
    var modal = $('#prompt_modal');
    if (modal.hasClass('in')) return false;
    // callbacks
    modal.data('close-callback', pClosecallback);
    if (typeof pOpenCallback == 'function')
        modal.one('shown.bs.modal', pOpenCallback);
    modal.one('hidden.bs.modal', function () {
        if (typeof modal.data('close-callback') == 'function')
            modal.data('close-callback')();
    });

    // css
    for (var prop in modalCSS) {
        if (modalCSS.hasOwnProperty(prop))
            modal.children('.modal-dialog').css(prop, modalCSS[prop]);
    };

    // title
    $('#prompt_label').html(pHeader);
    
    // body
    var mkBody = {
        make: function(obj) {
            if (this.getRows(obj) > 1 || this.getTextarea(obj)) return this.textarea(obj); else if (!this.getTextOnly(obj)) return this.input(obj); else return this.textPrompt(obj);
        },
        input: function(obj) {
            return (this.getLabel(obj) + '<input data-index="' + obj.index + '" class="form-control ' + this.getClasses(obj) + '" value="' + this.getValue(obj) + '" />');
        },
        textarea: function(obj) {
            return (this.getLabel(obj) + '<textarea data-index="' + obj.index + '" class="form-control no-horizontal ' + this.getClasses(obj) + '" rows="' + this.getRows(obj) + '">' + this.getValue(obj) + '</textarea>');
        },
        textPrompt: function(obj) {
            return (this.getTextOnly(obj));
        },
        getTextOnly: function (obj) { return getOwnProp(obj, 'text-only', false); },
        getRows: function (obj) { return getOwnProp(obj, 'rows', 1); },
        getTextarea: function (obj) { return getOwnProp(obj, 'textarea', false); },
        getValue: function (obj) { return getOwnProp(obj, 'value', '')},
        getClasses: function (obj) { return getOwnProp(obj, 'classes', ''); },
        getLabel: function (obj) {
            var label = getOwnProp(obj, 'label', '');
            if (label.trim() != '') return '<label>' + label + '</label>'; else return '';
        }
    };
    var content = $('#prompt_content');
    content.html('');
    for (var x = 0; x < pBody.length; x++) {
        pBody[x].index = x;
        content.append(mkBody.make(pBody[x]));
    };

    // footer
    var footer = $('#prompt_footer');
    footer.html('');
    var mkFooter = {
        getClasses: function (obj) { return getOwnProp(obj, 'classes', ''); },
        getClick: function (obj) {
            var click = getOwnProp(obj, 'onclick', function () { });
            if (typeof click == 'function') return click;
            else return function () { };
        },
        getDismiss: function (obj) { return getOwnProp(obj, 'dismiss', false); },
        getText: function (obj) { return getOwnProp(obj, 'text', ''); },
        button: function (obj) {
            var $btn = $('<button class="btn ' + (this.getClasses(obj)) + '" data-btn-index="' + obj.index + '">' + this.getText(obj) + '</button>');
            $btn.on('click', this.getClick(obj));
            $btn.appendTo(footer);
            if (this.getDismiss(obj) == true) $btn.on('click', function () {
                $('#prompt_modal').modal('hide');
            });
        }
    };

    for (var x = 0; x < pFooter.length; x++) {
        pFooter[x].index = x;
        mkFooter.button(pFooter[x]);
    };

    modal.modal('show');
};