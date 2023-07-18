// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var BarcodeScannerEvents
$(document).ready(function () {

     BarcodeScannerEvents = {
        register: function (input) {
            input.data("Count", 0)
        },
        initialize: function () {
            $(document).on({
                keyup: $.proxy(this._keyup, this)
            });
            $(document).on({
                input: $.proxy(this._input, this)
            })
        },
        _timeuotHandler: 0,
        _inputString: '',
        _keyup: function (e) {

            if (this._timeuotHandler) {
                clearTimeout(this._timeuotHandler);
                this._inputString += String.fromCharCode(e.which);
            }

            this._timeuotHandler = setTimeout($.proxy(function () {
                if (this._inputString.length <= 3) {
                    this._inputString = '';
                    return;
                }

                $(e.target).trigger('scanRecieved', this._inputString);

                this._inputString = '';

            }, this), 20);
        },
        _input: function (e) {
            var target = $(e.target);
            var oldLength = target.data("Count");
            var currentLength = target.val().length;
            if (oldLength + 2 < currentLength) {
                target.trigger('scanRecieved', target.val());
            }
            target.data("Count", currentLength)
        }
    }
    
    BarcodeScannerEvents.initialize()
    
})

/*
Every Input that you want to listen for scan events on, must have the 2 following functions applied to it

$(document).ready(function () {
    BarcodeScannerEvents.register($('[name="Location"]'))
    $('[name="Location"]').on('scanRecieved', function (event,String) {
        //Include whatever you want to happen for the current event inside this code block
        alert("Scan Recieved")
    })
})*/
