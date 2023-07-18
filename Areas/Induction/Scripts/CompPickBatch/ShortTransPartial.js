// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    $('#ExecShortTran').click(function () {
        var ShrtQty= $('#ShortQty').val();

        if(ShrtQty != "" && parseInt(ShrtQty) >= 0 && parseInt(ShrtQty) < parseInt($('#ShortTransQty').val())){
            //ask to verify split here

            MessageModal("Process Short", "Short this transaction?", undefined, undefined,
                    function () {
                        CompPickBatchHub.server.shortTrans($('#ShortOTID').val(), ShrtQty, $('#ShortMethod').val()).done(function (success) {
                            if (success) {
                                //after split is performed: close modal and redraw table
                                PickBatchTransTable.clear().draw();
                                $('#CompPickBatchShortModal').modal('hide');
                            } else {
                                MessageModal("Error", "An error occured when shorting this transaction");
                            };
                        });
                    }
                );

        } else {
            $('#ShortQty').val('');
            MessageModal("Invalid Qty Entered", "Please enter a quantity that is greater than or equal to 0 and less than the transaction qty");
        };
    });

});