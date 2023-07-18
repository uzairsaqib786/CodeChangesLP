// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OMHub = $.connection.orderManagerHub;
var OMTable;
var OrderManCols = [];
var curSearchCol = $('#OMSearchCol').val();
$(document).ready(function () {
    var pd  = $('#PrintDirect').val() == 'true';
var otCount;
    $('#OMVal1D').datetimepicker({
        pickTime: false,
        minDate: '01/01/1900'
    });
    $('#OMVal2D').datetimepicker({
        pickTime: false,
        minDate: '01/01/1900'
    });

    //gets current column sequence
    $.each($('#OMColumns').children(), function (index, element) {
            OrderManCols.push($(element).attr('value'));
    });
    //creating the data table
    $('#OMVal2').hide();
    OMTable = $("#OMDataTable").DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'columnDefs': [
            {

            }
        ],
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "createdRow": function (row, data, index) {
            if ((data[OrderManCols.indexOf("Transaction Type")] == "Pick" && parseInt(data[OrderManCols.indexOf("Transaction Quantity")]) > parseInt(data[OrderManCols.indexOf("Available Quantity")]))) {
                $(row).addClass("danger");
            }
        },
        "ajax": {
            "url": "/OM/OrderManager/orderManDT",
            "data": function (d) {
                d.searchColumn = $('#OMSearchCol').val(),
                d.searchString = $('#OMSearch').val()
            },
        },
        "paging": true
    }).on('xhr.dt', function (e, settings, json) {
        otCount = json.extraData
    });

    $("#OMDataTable").wrap('<div style="overflow-x:scroll;"></div>');
    //setting active or disabled depending on the click
    $('#OMDataTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('#OMDataTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            //enableTableButts();
            $('#OMUpdateButt').removeAttr('disabled');
        } else {
            $this.removeClass('active');
            //disableTableButts();
            $('#OMUpdateButt').attr('disabled', 'disabled');
        };
    });

    $('#OMDataTable').on('filterChange', function () {
        //call redraw table hub funciton
        getOMTableData();
    });

    $('#addEditOrder').click(function () {
        location.href = "/OM/OrderManager/CreateOrders";
    });
    //opens modal
    $('#OMUpdateButt').click(function () {
        //open update modal here
        $('#OMUpdateModal').modal('show');
        //set prepopulated values for first update modal here. change these when column sequence stuff is added
        $('#OMUpdOrderNumber').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Order Number")]);
        $('#OMUpdItemNumber').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Item Number")]);
        $('#OMUpdSuppItemID').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Supplier Item ID")]);

        //getting the data from the OMDataTable to populate modal
        var tableDate = OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Required Date")];

        //var dateparts = tableDate.split('/');
        //var reqDate = new Date();
        //if (dateparts[0] < 10) {
        //    dateparts[0] = "0" + dateparts[0];
        //};
        //if (dateparts[1] < 10) {
        //    dateparts[1] = "0" + dateparts[1];
        //};
        //reqDate = String(dateparts[2]) + "-" + String(dateparts[0]) + "-" + String(dateparts[1]);

        $('#OMUpdRequiredDate').val(tableDate);

        $('#OMUpdNotes').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Notes")]);
        $('#OMUpdPriority').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Priority")]);
        $('#OMUpdUserField1').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field1")]);
        $('#OMUpdUserField2').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field2")]);
        $('#OMUpdUserField3').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field3")]);
        $('#OMUpdUserField4').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field4")]);
        $('#OMUpdUserField5').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field5")]);
        $('#OMUpdUserField6').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field6")]);
        $('#OMUpdUserField7').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field7")]);
        $('#OMUpdUserField8').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field8")]);
        $('#OMUpdUserField9').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field9")]);
        $('#OMUpdUserField10').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("User Field10")]);

        if (OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Emergency")] == "0") {
            $('#OMUpdEmergency').val("False")
        } else if (OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Emergency")] == "1") {
            $('#OMUpdEmergency').val("True")
        } else {
            $('#OMUpdEmergency').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Emergency")]);
        };

        if (OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Label")] == "0") {
            $('#OMUpdLabel').val("False")
        } else if (OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Label")] == "1") {
            $('#OMUpdLabel').val("True")
        } else {
            $('#OMUpdLabel').val(OMTable.row($('#OMDataTable tbody tr.active')[0]).data()[OrderManCols.indexOf("Label")]);
        };
        
    });
    //the delete button for the OMDataTable
    $('#delViewed').click(function () {
        if ($('#OrderType').val() == 'Open') {
            MessageModal("Warning", "You can only delete pending transactions")
        } else {
            //call function here
            var conf = confirm("Are you sure you wish to delete all viewed orders?")
            if (conf) {
                OMHub.server.deleteOMOTPend($('#ViewType').val()).done(function (mess) {
                    if (mess == "Error") {
                        MessageModal("Error", "An error has occurred while deleting records")
                    } else {
                        clearSearch();
                        OrderManFilterMen.clearFilter();
                    };
                });
            };
        };
    });
    //the print button
    $(document.body).on('click', '#printViewed', function () {
        getLLPreviewOrPrint('/OM/OrderManager/PrintReleaseOrders', {
            tabIDs: [],
            View: $('#ViewType').val(),
            Table: $('#OrderType').val(),
            Page: "Order Manager",
            PrintDirect: pd
        }, pd,'report', 'Print Order Manager');
    });

    $('[name="OMWhereClause"]').change(function () {
        if ($('[name="OMWhereClause"]:checked').val() == "Between") {
            if ($('#OMColumn').val().indexOf('Date') > -1) {
                $('#OMVal2D, #OMValLabel').show();
        } else {
                $('#OMVal2, #OMValLabel').show();
            }
            
        } else {
                $('#OMVal2, #OMValLabel').hide();
                $('#OMVal2D, #OMValLabel').hide();
        };
    });

    //Handles searching through table by column
    $('#OMSearch').on('input', function () {
        OMTable.draw();
    });


    //Re-filters if search column changed
    $('#OMSearchCol').change(function () {
        OMTable.draw();
    })

    //Opens the order status screen for the given order number
    $(document.body).on('click', '#orderStat', function () {
        //go to order status here for the corresponding order number
        var ordernum = $('#OMVal1').val();
        var SearchBy = $('#OMColumn').val();
        var orderNumIndex = 0;
        var orderNumVal = "";

        if ((ordernum == "" || SearchBy != "Order Number") && $('#OMDataTable tbody').find('.active').length == 0) {
            MessageModal("Error", "You must select an Order Number to view the order status.")
        } else {
            if ($('#OMDataTable tbody').find('.active').length == 0) {
                var $handle = $(window.open("/Transactions?viewtoShow=1&OrderStatusOrder=" + ordernum + "&App=" + $('#AppName').val() + '&popup=true', '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'));
            } else {
                for (var i = 0; i < $('#OMDataTable thead tr th').length; i++) {
                    console.log($('#OMDataTable thead tr th:eq(' + i + ')').text())
                    if ($('#OMDataTable thead tr th:eq(' + i + ')').text() == "Order Number") {
                        orderNumIndex = i
                    } 
                } 
                orderNumVal = $('#OMDataTable tbody .active td:eq(' + orderNumIndex + ')').text()
                var $handle = $(window.open("/Transactions?viewtoShow=1&OrderStatusOrder=" + orderNumVal + "&App=" + $('#AppName').val() + '&popup=true', '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'));

            }
            try {
                $handle[0].focus();
            } catch (e) {
                MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.');
            };
        };
    });

    //if data entered for both values and click enter call display records button
    $('#OMVal1, #OMVal2').on('keyup', function (e) {
        if (e.which == 13) {
            $('#OMDispVals').click();
        }
    })
    $('#OMVal1D, #OMVal2D').on('change', function (e) {
            $('#OMDispVals').click();
    })

    //Hides and Shows Correct Inputs depending on if Date or Text input is selected
    $('#OMColumn').change(function () {
        if (this.value.indexOf('Date') > -1) {
            $('#OMVal1').hide();
            $('#OMVal1D').show();
            if ($('[name="OMWhereClause"]:checked').val() == "Between") {
                $('#OMVal2').hide();
                $('#OMVal2D').show();
            }
        } else {
            $('#OMVal1').show();
            $('#OMVal1D').hide();
            if ($('[name="OMWhereClause"]:checked').val() == "Between") {
                $('#OMVal2D').hide();
                $('#OMVal2').show();
            }
        }
        
    })

    //Re-querys Data for the Table
    $('#OMDispVals').click(function () {
        if (($('#OMColumn').val() == "Import Date" || $('#OMColumn').val() == "Required Date" || $('#OMColumn').val() == "Priority") && $('[name="OMWhereClause"]:checked').val() == "Like") {
            MessageModal("Warning", "Cannot use the 'Like' option with Required Date, Import Date, or Priority column options")
        } else {
            getOMTableData();
        };
    });

    //Releases orders from Open Transactions pending to Open Transactions
    $('#releaseViewed').click(function () {
        if (OMTable.rows({ search: 'applied' }).data().length == 0) {
            MessageModal("Release Transactions", "No Transactions match your current filters to release")
            return
        }
        if ($('#OrderType').val() == 'Open') {
            MessageModal("Release Transactions", "This orders you are viewing have already been released")
            return
        }
        if ($('#AllowInProc').val() == "False") {
            if (otCount) {
                MessageModal("Release Transactions","You may not release an Order that is already in progress")
                return
            }
        }
        if ($('#ReleasePartial').val() == 'False' && ($('#OMDataTable_info').text().indexOf('filtered') > -1) || OrderManFilterMen.getFilterString() != '1=1') {
            var confirmresponse = confirm("Cannot Release Partial Orders. If you would like to release the entire order, click Ok. Otherwise click cancel")
            if (!confirmresponse) {
                clearSearch();
                OrderManFilterMan.clearFilter();
            }
        } else {
            var confirmresponse = confirm("Press ok to release currently Viewed Orders")
        }
        
        if (confirmresponse) {
            OMHub.server.releaseOrders($('#ViewType').val(), "Order Manager").done(function (data) {
                if (data == "Error") {
                    MessageModal("Error", "An Error Occured while releasing orders. Check the Event Log for more info")
                } else {
                    getOMTableData();
                    clearSearch();
                }
            })
        }
    })

    //Refreshes table data on Dropdown list change
    $('#OMtransType, #ViewType, #OrderType').change(function () {
        getOMTableData();
    })
    //gets the table data
    function getOMTableData() {
        //this needs to be changed big time
        var val1;
        var val2;
        if ($('#OMColumn').val().indexOf('Date') > -1) {
            val1 = $('#OMVal1D').val();
            val2 = $('#OMVal2D').val();
        } else {
            val1 = $('#OMVal1').val();
            val2 = $('#OMVal2').val();
        }
        //calls function for data
        OMHub.server.fillOrderManTempData($('#OMColumn').val(), val1, val2, $('[name="OMWhereClause"]:checked').val(),
                                          $('#OMtransType').val(), $('#ViewType').val(), $('#OrderType').val(), $('#maxOrders').val(),
                                          (OrderManFilterMen == "" ? "" : OrderManFilterMen.getFilterString())).done(function (mess) {
                                              if (mess == "Error") {
                                                  MessageModal("Error", "An error has occurred populating the temp table")
                                              } else {
                                                  OMTable.draw(); 
                                              }
                                              });
    }
    //clear search
    function clearSearch() {
        $('#OMSearch').val('');
        OMTable.columns(OrderManCols.indexOf($('#OMSearchCol').val())).search("").draw();
    }


});
//table id
function getTableIDs() {
    var ids = []
    var indexLook = -1
    if ($('#ViewType').val() == 'Headers') {
        indexLook = OrderManCols.indexOf("Order Number")
    } else {
        indexLook = OrderManCols.indexOf("ID")
    }
    for (var x = 0; x < OMTable.rows({ search: 'applied' }).data().length; x++) {
        console.log(OMTable.row(x, { search: 'applied' }).data()[indexLook])
        ids.push(OMTable.row(x, { search: 'applied' }).data()[indexLook])
    };
    return ids
}
