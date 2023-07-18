// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/******************************************************************************************
    Order Status
******************************************************************************************/
var completedOSDate;
var OrderNumber;
var OrderStatCols = new Array();
$(document).ready(function () {

    $('#pageLength1').val(100);

    var orderStatusTimer = mkTimer(function () {
        orderStatTable.draw();
    }, 200);
    $.each($('#selection1').children(), function (index, element) {
        OrderStatCols.push($(element).attr('value'));
    });

    $(document.body).on('click', '#previewList', function () {
        previewOSList();
    });

    // takes care of autocompletion/suggestions of order numbers in order status
    OrderNumber = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in TransactionsController nextOrders
            url: '/Transactions/nextOrders?query=%QUERY',
            filter: function (list) {
                return $.map(list, function (orderDateObj) {
                    return orderDateObj;
                });
            },
            cache: false
        }
    });

    OrderNumber.initialize();

    $('#orderTypeAhead .typeahead').typeahead({
        hint: false,
        highlight: false,
        minLength: 1
    }, {
        name: "OrderNumber",
        displayKey: 'ordernum',
        source: OrderNumber.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Order Number</p><p class="typeahead-header" style="width:50%;">Completed Date</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:50%;">{{ordernum}}</p><p class="typeahead-row" style="width:50%;">{{compdate}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        $('#ordernumFilterOrder')
            .parents('div.form-group.forced-typeahead').addClass('has-success').removeClass('has-warning')
            .children('span.glyphicon').addClass('glyphicon-ok').removeClass('glyphicon-warning-sign');
        // reset filters, data for new records to come in on selected
        $('#toteCheck').attr("checked", false); $('#toteidFilterOrder').val("");
        OrderNumber.clearRemoteCache();
        clearInputs();
        completedOSDate= datum.compdate
        orderStatTable.draw();
        //insert check for ship complete
        orderStats.server.selShipComp($('#ordernumFilterOrder').val()).done(function (mess) {
            if (mess == "") {
                $('#shippingComplete').attr('disabled', 'disabled');
            } else {
                $('#shippingComplete').removeAttr('disabled');
            }
        });
        return
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "600px");
    });

    $(document.body).on('click','.MultiDate_Confirm', function () {
        $('#ordernumFilterOrder')
           .parents('div.form-group.forced-typeahead').addClass('has-success').removeClass('has-warning')
           .children('span.glyphicon').addClass('glyphicon-ok').removeClass('glyphicon-warning-sign');
        $('#toteCheck').attr("checked", false); $('#toteidFilterOrder').val("");
        completedOSDate = $(this).html() != 'Entire Order' ? $(this).html() : "";
        clearInputs();
        console.log(completedOSDate);
        orderStatTable.draw();
        $('#MultiDateModal').modal("hide");
    })

    //If input is scanned into the typeahead, validate the Order Number and show  and multi date Orders
    $('#ordernumFilterOrder').keydown(function (e) {
        if (e.which == 13) {
            var $this = $(this)
            //Checks against server to see if there is multiple dates for this order
            orderStats.server.scanValidateOrder($this.val()).done(function (result) {
                if (result.length >= 1) {
                    //If single date, get order info
                    if (result.length == 1) {
                        $this
                           .parents('div.form-group.forced-typeahead').addClass('has-success').removeClass('has-warning')
                           .children('span.glyphicon').addClass('glyphicon-ok').removeClass('glyphicon-warning-sign');
                        $('#toteCheck').attr("checked", false); $('#toteidFilterOrder').val("");
                        completedOSDate = result[x];
                        clearInputs();
                        $('#ordernumFilterOrder').select();
                        $('#ordernumFilterOrder.typeahead').typeahead('close');
                        orderStatTable.draw();
                        //insert check for ship complete
                        orderStats.server.selShipComp($('#ordernumFilterOrder').val()).done(function (mess) {
                            if (mess == "") {
                                $('#shippingComplete').attr('disabled', 'disabled');
                            } else {
                                $('#shippingComplete').removeAttr('disabled');
                            }
                        });
                        //If multiple dates, show multi date modal
                    } else {
                        var $MultiDateBody = $('#MultiDate_Body');
                        $MultiDateBody.html('');
                        for (var x = 0; x < result.length; x++) {
                            $MultiDateBody.append("<div class='row'><div class='col-md-4 col-md-offset-4'><button style='margin-bottom:10px;' class='MultiDate_Confirm btn btn-primary btn-block'>" + result[x] + "</button></div></div>")
                        }
                        $('#MultiDate_OrderNum').html($this.val())
                        $('#MultiDateModal').modal("show");
                    }
                    //If Order does not exist
                } else {
                    $('#ordernumFilterOrder').blur().typeahead('val', '')
                    MessageModal("Order Status", "Order does not exist")
                    orderStatTable.draw();
                    $('#ordernumFilterOrder').focus();
                }
            })

        }
    });


    /*******************************************************************************************************************************
       Order Status fields
   *******************************************************************************************************************************/
    // select element for search field (dropdown)
    $('#selection1').change(function () {
        orderStatusTimer.startTimer();
    });
    // reset the check/success indicator on the input text box when there is a change to what's inside it
    $('#ordernumFilterOrder').on("input", function () {
        if ($(this).val().length == 0)
        {
            $('#ordernumFilterOrder').typeahead('val',"")
        }
        $(this)
            .parents('div.form-group.forced-typeahead').removeClass('has-success').addClass('has-warning')
            .children('span.glyphicon').removeClass('glyphicon-ok').addClass('glyphicon-warning-sign');
        clearInputs();
        OrderNumber.clearRemoteCache();
    });
    //filter whan a tote id is inputtted
    //automatically filters by tote id 
    $('#toteidFilterOrder').keydown(function (e) {
        if (e.which == 13) {
            if ($(this).val() == "") {
                $('#toteCheck').attr("checked", false);
            } else {
                $('#toteCheck').attr("checked", true);
            };
            $('#ordernumFilterOrder').val("");
            clearInputs();
            orderStatusTimer.startTimer();
        };
    });

    $('#toteidFilterOrder').keydown(function (e) {
        if (e.which == 13) {
            var $this = $(this);
            $this.select();
        };
    });

    //clear ordernumber and tote id 
    //reset all table values 
    $('#toteClearButton').click(function () {
        $('#ordernumFilterOrder').val("").trigger("input");
        clearInputs();
        $('#toteidFilterOrder').val("");
        $('#shippingComplete').attr('disabled', 'disabled')
        orderStatusTimer.startTimer();
    });

    //Redraws the table depending on the Filter by Tote Paramter
    $('#toteCheck').click(function () {
        orderStatusTimer.startTimer();
    });

    //Clears Order Status Data that is displayed
    function clearInputs() {
        $('#carOnTable, #carOffTable').html("");
        $('#ordType, #curStat').text("None");
        $('#totLines, #reprocLines, #complLines, #openLines').html("0");
    };

    //Allows you to change the priority of an order
    $("#ChangePriority").click(function () {
        //set value of current prioroity to $('#enter id here').val(orderStatTable.row(0).data()[10])
        $('#OrderNumber').val($('#ordernumFilterOrder').val())
        $('#Priority').val(orderStatTable.row(0).data()[10])
        $('#UpdatePriorityModal').modal('show')
    });

    //Fixes style on search typeahead that causes it to go to a new line
    $('.twitter-typeahead').css('display', 'inline');

    function previewOSList() {
        var url = '/CustomReports/PreviewReport/';
        var title = 'Order Status List';
        var reportName = 'Order Status';
        var reportTitles = new Array('', '', '', '');
        var fields = new Array();
        var type = 0;

        var objFields = new Array();
        if ($('#ordernumFilterOrder').val() != '') {
            objFields.push({ 'field': 'Order Number', 'exptype': '=', 'expone': $('#ordernumFilterOrder').val(), 'exptwo': '' });
        } else if ($('#toteidFilterOrder').val() != '') {
            objFields.push({ 'field': 'Tote ID', 'exptype': '=', 'expone': $('#toteidFilterOrder').val(), 'exptwo': '' });
        };
        while (objFields.length != 6) {
            objFields.push({ 'field': '', 'exptype': '', 'expone': '', 'exptwo': '' });
        }
        
        var obj = {
            'desiredFormat': 'preview',
            'reportName': reportName,
            'reportTitles': reportTitles,
            'fields': fields,
            'objFields': objFields,
            'backFilename': '',
            'type': type
        };
        // Obj has to be deserialized in order to return JSON
        obj.objFields = JSON.stringify(obj.objFields);

        getLLPreviewOrPrint(url, obj, false, 'report', title)
    };
});