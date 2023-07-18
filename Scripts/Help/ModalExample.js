// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    $('#ExampleModalButton, #ExampleModalInput, i.glyphicon.glyphicon-resize-full').click(function () {
        $('#example_modal').modal('show');
    });

    $('#example_submit').click(function () {
        $('#ExampleModalInput').val($('.modal.in').find('input').val());
        $('#example_modal').modal('hide');
    });
});