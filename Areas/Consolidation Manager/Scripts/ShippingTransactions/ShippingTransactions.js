// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var shipTransHub = $.connection.shippingTransactionsHub
var ShipTransTable;
$(document).ready(function () {
    //initialize datatable
    ShipTransTable = $('#ShipTtansactionsTable').DataTable({
        "dom": 'trip',
        "processing": true,
        'columnDefs': [
            {
                'targets': [0],
                'visible': false
            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "lengthMenu": [20]
    });
    //handles click event on the table records
    $('#ShipTtansactionsTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('#ShipTtansactionsTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            enableTableButts();
        } else {
            $this.removeClass('active');
            disableTableButts();
        }
    });
    //initialize typeahead on item number
    var ItemNumTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getbeginToLocation
            url: ('/CM/Consolidation/shipTranTA?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#ShipItemNumber').val() + '&orderNum=' + $('#ShipOrderNumber').val();
                console.log(url);
            },
            cache: false
        }
    });
    ItemNumTA.initialize();
    $("#ShipItemNumber").typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "lookUp",
        displayKey: 'ItemNumber',
        source: ItemNumTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Item Number</p><p class="typeahead-header" style="width:50%;">Description</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:50%;">{{ItemNumber}}</p><p class="typeahead-row" style="width:50%;">{{Description}}</p>')
        }
    }).on('typeahead:selected', function (obj, data, name) {
        var matchRows = ShipTransTable.$('td:eq(0):contains("' + data.ItemNumber + '")');
        var numRows = ShipTransTable.row(matchRows.parent()).index();
        var pageNum = Math.floor(numRows / ShipTransTable.page.len());
        ShipTransTable.page(pageNum).draw(false);
        matchRows.first().trigger('click');
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', '450px');

    });
    //Printing report
    $('#ShipPrintList').click(function () {
        var orderNum = $('#ShipOrderNumber').val();
        //reportsHub.server.printShipOrderPL(orderNum);
        title = 'Location Label';
        getLLPreviewOrPrint('/CM/Shipping/PrintShipOrderPL', {
            OrderNum: orderNum
        }, true,'report', title)
    });
    //printing label
    $('#ShipPrintItemLabel').click(function () {
        var id = ShipTransTable.row($('#ShipTtansactionsTable tbody tr.active')[0]).data()[0];
        //reportsHub.server.printShipTransLabel(id);
        title = 'Location Label';
        getLLPreviewOrPrint('/CM/ShippingTransactions/PrintShipTransLabel', {
            id: id 
        }, true,'label', title)
    });

    // edit these
    $('#ShipSplitLine').click(function () {
        openSplitLine('ShipTtansactionsTable', ShipTransTable, '4', '5', '7', 'ShipTrans');
    });

    $('#ShipEditQuantity').click(function () {
        openAdjustQuant('ShipTtansactionsTable', ShipTransTable, '7');
    });


    $('#ShipEditContainerID').click(function () {
        openContainerIDModal('ShipTtansactionsTable', ShipTransTable, '6');
    });

    //end of editing 

    $('#ShipUpdateToteID').on("keydown", function (e) {
        if (e.which == 13 && $('#ShipUpdateToteID').val()  != "") {
            $('#ToteIDButt').click();
        };
    });


    $('#ToteIDButt').click(function () {
        toteID = $('#ShipUpdateToteID').val();
        var noExists = false;
        for (var x = 0; x < ShipTransTable.rows().data().length; x++) {
            var tabTote = ShipTransTable.row(x).data()[3];
            if (toteID == tabTote) {
                $('#ToteIDUpdateModal').modal('show');
                $('#UpdateToteID').val(toteID);
                $('#UpdateOrderNumber').val($('#ShipOrderNumber').val());
                noExists = false;
                break;
            } else {
                noExists = true;
            }
        };
        if (noExists) {
            MessageModal("Tote ID Update", "The given Tote ID is not contained within this order number");
        };
    });
    //complete packing for ordernumber
    $('#ShipCompletePacking').click(function () {
        shipTransHub.server.selCountOpenTransactionsTemp($('#ShipOrderNumber').val()).done(function (counts) {
            if (counts == -1) {
                MessageModal("Error", "An error has occurred");
            } else if (counts == 0) {
                //do for no temp
                var conf = confirm("Are you sure you want to update this order number as complete for packing?");
                if (conf) {
                    shipTransHub.server.updCompletePacking($('#ShipOrderNumber').val()).done(function (mess) {
                        if (mess == "Fail") {
                            MessageModal("Tote ID Update", "An error has occurred");
                        } else {
                            history.back();
                        };
                    });
                };
            } else {
                //do for temp
                var conf = confirm("Are you sure you want to update this order number as complete for packing?");
                if (conf) {
                    var otherConf = confirm("Back orders exist for this order number. Still continue pack complete?")
                    if (otherConf) {
                        shipTransHub.server.updCompletePacking($('#ShipOrderNumber').val()).done(function (mess) {
                            if (mess == "Fail") {
                                MessageModal("Tote ID Update", "An error has occurred");
                            } else {
                                history.back();
                            };
                        });
                    };
                };
            };
        });
    });
});

function enableTableButts() {
    $('#ShipSplitLine').removeAttr('disabled');
    $('#ShipPrintItemLabel').removeAttr('disabled');
    $('#ShipEditQuantity').removeAttr('disabled');
    $('#ShipEditContainerID').removeAttr('disabled');
};

function disableTableButts() {
    $('#ShipSplitLine').attr('disabled', 'disabled');
    $('#ShipPrintItemLabel').attr('disabled', 'disabled');
    $('#ShipEditQuantity').attr('disabled', 'disabled');
    $('#ShipEditContainerID').attr('disabled', 'disabled');
};