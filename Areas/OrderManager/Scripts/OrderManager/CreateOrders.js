// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OMHub = $.connection.orderManagerHub;
var CreateTable;
var OrderManCreateCols = [];
var otcreatecount;
$(document).ready(function () {
    var pd = $('#PrintDirect').val() == 'true';
    //getting the current column sequence
    $.each($('#OMCreateColumns').children(), function (index, element) {
        OrderManCreateCols.push($(element).attr('value'));
    });
    //setting up the data table
    CreateOrdersTable = $("#CreateOrdersTable").DataTable({
        "dom": 'trip',
        "processing": true,
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
        "paging": true
    });
    $("#CreateOrdersTable").wrap('<div style="overflow-x:scroll;"></div>');
    //disables and sets OrderManCreateCols to active depending on the click
    $('#CreateOrdersTable tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('#CreateOrdersTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            $('#CreateOdersEditTrans').removeAttr('disabled');
        } else {
            $this.removeClass('active');
            $('#CreateOdersEditTrans').attr('disabled', 'disabled');
        };
    });
    //delete viewed button
    $('#CreateOrdersDelete').click(function () {
        if (CreateOrdersTable.page.info().recordsDisplay== 0) {
            MessageModal("Warning", "There are currently no records within the table")
        } else {
            var ids = [];
            var conf = confirm("Are you sure you want to delete these records?")
            if (conf) {
                for (var x = 0; x < CreateOrdersTable.rows({ search: 'applied' }).data().length; x++) {
                    ids.push(CreateOrdersTable.row(x, { search: 'applied' }).data()[OrderManCreateCols.indexOf("ID")])
                };
                //hub function
                OMHub.server.deleteOTPend(ids).done(function (mess) {
                    if (mess == "Error") {
                        MessageModal("Error", "An error has occurred while deleting the viewed records")
                    } else {
                        getCreateOrdersTableData();
                        $('#CreateOrdersSearch').val("");
                        $('#CreateOrdersSearch').trigger('keyup');
                    };
                });
            };
           
        };
    });

    $('#CreateOrdersTable').on('filterChange', function () {
        //call redraw table hub funciton
        getCreateOrdersTableData();
    });

    //print viewed button
    $(document.body).on('click', '#CreateOrdersPrint', function () {
        var tabIDS=[];
        for (var x = 0; x < CreateOrdersTable.rows({ search: 'applied' }).data().length; x++) {
            tabIDS.push(CreateOrdersTable.row(x, { search: 'applied' }).data()[OrderManCreateCols.indexOf("ID")])
        };
        getLLPreviewOrPrint('/OM/OrderManager/PrintReleaseOrders', {
            tabIDs: tabIDS,
            View: '',
            Table: '',
            Page: "Create Orders",
            PrintDirect: pd
        }, pd,'report', 'Print Order Manager');
    });
    //search bar top of screen, fills in table on key
    $('#CreateOrdersSearch').on('keyup', function () {
        if (this.value.length == 0) {
            CreateOrdersTable.columns(OrderManCreateCols.indexOf($('#CreateOrdersSearchCol').val())).search("").draw();
        } else {
            CreateOrdersTable.columns(OrderManCreateCols.indexOf($('#CreateOrdersSearchCol').val())).search("^" + this.value + "$", true, false).draw();
        }

    });
    //typeahead
    var CreateOrderNum = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/OM/OrderManager/CreateOrderTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#CreateOrdersOrderNum').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    CreateOrderNum.initialize();

    $('#CreateOrdersOrderNum').typeahead({
        hint: false,
        highlight: false,
        minLength: 1
    }, {
        name: "CreateOrderNum",
        displayKey: 'ordernum',
        source: CreateOrderNum.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Order Number</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:100%;">{{ordernum}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        getCreateOrdersTableData();
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', "600px");
    });
    //opens modal that contains all the user fields, can update
    $('#CreateOrdersUserFields').click(function () {
        OMHub.server.selUserFieldData().done(function (returnData) {
            $('#UserField1TXT').val(returnData.UserField1)
            $('#UserField2TXT').val(returnData.UserField2)
            $('#UserField3TXT').val(returnData.UserField3)
            $('#UserField4TXT').val(returnData.UserField4)
            $('#UserField5TXT').val(returnData.UserField5)
            $('#UserField6TXT').val(returnData.UserField6)
            $('#UserField7TXT').val(returnData.UserField7)
            $('#UserField8TXT').val(returnData.UserField8)
            $('#UserField9TXT').val(returnData.UserField9)
            $('#UserField10TXT').val(returnData.UserField10)
            $('#UserFieldDataModal').modal('show');
        });
        
    });

    $('#CreateOrdersRelease').click(function () {
        if ($('#AllowInProc').val() == "False") {
            if (otcreatecount) {
                MessageModal("Release Transactions", "You may not release an Order that is already in progress")
                return
            }
        } 
        var conf = confirm("Release all orders for this order number?");
       
        if (conf) {
            OMHub.server.releaseOrders($('#CreateOrdersOrderNum').val(), "Create Orders").done(function (data) {
                if (data == "Error") {
                    MessageModal("Error", "An Error Occured while releasing orders. Check the Event Log for more info")
                } else {
                    $('#CreateOrdersOrderNum').val("");
                    getCreateOrdersTableData();
                }
            })
        };
    });

});
//function to get the data for the table
function getCreateOrdersTableData() {
    OMHub.server.selectCreateOrdersDT($('#CreateOrdersOrderNum').val(), (OrderManCreateFilterMen == "" ? "1=1" : OrderManCreateFilterMen.getFilterString())).done(function (data) {
        CreateOrdersTable.clear();
        CreateOrdersTable.rows.add(data.info).draw();
        $('#CreateOdersAddTrans').removeAttr('disabled');
        otcreatecount = data.otcount;
    });
};