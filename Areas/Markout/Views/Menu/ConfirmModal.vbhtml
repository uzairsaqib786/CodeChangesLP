<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2021-->

<div class="modal fade" id="dispConfirm" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <strong style="font-size:20px" id="ConfTitle"></strong>
                <button type="button" id="closeConfirmX" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <table>
                            <tr>
                                <td align="left" id="confirmMessage1" style="font-size:16px"></td>
                            </tr>
                            <tr>
                                <td align="left" id="confirmMessage2" style="font-size:16px"></td>
                            </tr>
                            <tr>
                                <td align="left" id="confirmMessage3" style="font-size:16px"></td>
                            </tr>
                            <tr>
                                <td align="left" id="confirmMessage4" style="font-size:16px"></td>
                            </tr>
                            <tr>
                                <td align="left" id="confirmMessage5" style="font-size:16px"></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button id="btYesConfirm" type="button" class="btn btn-default" tabindex="0">[Y]es</button>&nbsp;&nbsp;
                <button id="btNoConfirm" type="button" class="btn btn-default" tabindex="0">[N]o</button>
            </div>
        </div>
    </div>
</div>
<script>

    var dcFunctionToExecute = ''

    $(document).ready(function () {

        $('#dispConfirm, #btYesConfirm, #btNoConfirm').on('keydown', function (e) {

            if (e.which > 111 && e.which < 121) {
                //Stops the browser from performing the standard button click functionality
                e.preventDefault();
                e.stopPropagation();
            }
            switch (e.which) {
                case 13:
                    $(this).trigger('click');
                    break;
                case 89:
                case 121:
                    $('#btYesConfirm').trigger('click');
                    break;
                case 78:
                case 110:
                    $('#btNoConfirm').trigger('click');
                    break;
            }
        });


        $('#btYesConfirm').on('click', function () {
            dcFunctionToExecute = 'custom_yes';
            $('#dispConfirm').modal('hide');
        })

        $('#btNoConfirm').on('click', function () {
            dcFunctionToExecute = 'custom_no';
            $('#dispConfirm').modal('hide');
        })

        $('#dispConfirm').on('hidden.bs.modal', function () {
            $('#dispConfirm').trigger(dcFunctionToExecute);
            $('#dispConfirm').trigger('blur');
        });

    });

    function displayConf(message, title, onYesFunction, onNoFunction) {

        //change the messsage to the one passed in on the confMessage Partial
        $('#ConfTitle').html(title);
        changeConfMessage(message);
        $('#dispConfirm').off('custom_no').off('custom_yes');
        $("#dispConfirm").modal('show');

        if (onYesFunction != undefined) {
            $('#dispConfirm').on('custom_yes', onYesFunction);
        }

        if (onNoFunction != undefined) {
            $('#dispConfirm').on('custom_no', onNoFunction);
        }

    }


    function changeConfMessage(message) {
        $("#confirmMessage1").text("");
        $("#confirmMessage2").text("");
        $("#confirmMessage3").text("");
        $("#confirmMessage4").text("");
        $("#confirmMessage5").text("");
        msg = message;
        //transType = transactionType;
        //quantityEntered = qtyEntered;

        var linesArray = message.split("|");

        //alert(linesArray + " - " + message)

        for (i = 0; i < linesArray.length + 1; i++) {
            switch (i) {
                case 0:
                    $("#confirmMessage1").text(linesArray[i - 1]);
                    break;
                case 1:
                    $("#confirmMessage2").text(linesArray[i - 1]);
                    break;
                case 2:
                    $("#confirmMessage3").text(linesArray[i - 1]);
                    break;
                case 3:
                    $("#confirmMessage4").text(linesArray[i - 1]);
                    break;
                case 4:
                    $("#confirmMessage5").text(linesArray[i - 1]);
                    break;
            }
        }
    }

</script>
