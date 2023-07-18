// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$.fn.numpad.defaults.gridTpl = '<table class="table modal-content"></table>';
$.fn.numpad.defaults.backgroundTpl = '<div class="modal-backdrop in"></div>';
$.fn.numpad.defaults.displayTpl = '<input type="text" class="form-control" />';
$.fn.numpad.defaults.buttonNumberTpl = '<button type="button" class="btn btn-default"></button>';
$.fn.numpad.defaults.buttonFunctionTpl = '<button type="button" class="btn" style="width: 100%;"></button>';
$.fn.numpad.defaults.onKeypadCreate = function () { $(this).find('.done').addClass('btn-primary'); };

$(document).ready(function () {
    $('[data-numpad]').each(function (i, e) {
        var e$ = $(e);
        e$.numpad({
            target: $(e$.data('input-target')),
            positionX: "center"
        });
    });
});