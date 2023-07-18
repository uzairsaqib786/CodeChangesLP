// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var PickToteOrdersTable;
var PickToteTransTable;
$(document).ready(function () {
    var AllOrders = false;
    var cleared = false;
    addResize(function () { $('#PickBatchOrderByContainer,#PickBatchFilterContainer,#PickBatchZoneContainer').css('max-height', $(window).height() * 0.65); });
    
    //datatables
    PickToteOrdersTable = $("#PickToteOrdersTable").DataTable({
        "dom": 'trip',
        "processing": true,
        createdRow: function (row, data, index) {
            $('[name="OrderNumber"]').each(function () {
                var $currentIter = $(this);
                if ($currentIter.val() == data[0]) {
                    $(row).addClass('success');
                }
            })
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "columnDefs": [
            { "orderable": false, "targets": [0, 1, 2] }
        ],
        "order": []
    });
    $('#PickToteOrdersTable').wrap('<div style="overflow-x:scroll;"></div>');

    PickToteTransTable = $("#PickToteTransTable").DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/IM/PickToteSetup/PickToteTransDT",
            "data": function (d) {
                var orders = [];
                if (AllOrders) {
                    //push all order numbers
                    for (var x = 0; x < PickToteOrdersTable.rows().data().length; x++) {
                        orders.push(PickToteOrdersTable.row(x).data()[0]);
                    };
                    console.log("all")
                } else {
                    //push only the highlighted one
                    //check to make sure something was clicked. val is undefined if nothing is clicked
                    if ($('#PickToteOrdersTable tbody tr.active').val() !== undefined) {
                        orders.push(PickToteOrdersTable.row($('#PickToteOrdersTable tbody tr.active')[0]).data()[0]);
                    };
                    console.log("single")
                };
                var orderstring = ""
                for (var i = 0; i <= orders.length - 1; i++) {
                    if (i == 0) {
                        orderstring = orders[i];
                    } else {
                        orderstring += ", " + orders[i];
                    };
                };
                if (orderstring == "") {
                    orderstring = "EAGLES";
                };
                d.ordernum= orderstring;
                d.filter = (PickToteManTransFilterMen == "" ? "" : PickToteManTransFilterMen.getFilterString());

            }
            
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        },
        "paging": true
    });
    //on click event for orders table rows
    $("#PickToteOrdersTable").on('click', 'tr', function () {
        //append records here
        var $this = $(this);
        if (!$this.hasClass('active')) {
            AllOrders = false;
            $('#PickToteOrdersTable tbody tr.active').removeClass('active');
            $this.addClass('active');
            PickToteTransTable.draw();
            $('#SelectOrder').removeAttr('disabled');
        } else {
            $this.removeClass('active');
            $('#SelectOrder').attr('disabled', 'disabled');
            PickToteTransTable.clear().draw();
        };
    });
    //displays all orders in the trans table
    $('#ViewAllOrders').click(function () {
        AllOrders = true;
        $('#PickToteOrdersTable tbody tr.active').removeClass('active');
        PickToteTransTable.draw();
    });
    //displays all orders in the trans table
    $('#ViewSelectedOrders').click(function () {
        AllOrders = false;
        PickToteTransTable.draw();
    });
    //will fill the order numbers in the tote setup with all displayed orde rnumbers in the orders table
    $('#SelectAll').click(function () {
        var Orders = $('[name="OrderNumber"]');
        var ordernums = [];
        var priorities=[];
        for (var x = 0; x < PickToteOrdersTable.rows().data().length; x++) {
            ordernums.push(PickToteOrdersTable.row(x).data()[0]);
            priorities.push(PickToteOrdersTable.row(x).data()[2])
        };
        $('#PickToteOrdersTable tbody tr').addClass("success");
        if (ordernums.length > 0) {
            $.each(Orders, function (i, v) {
                if (i < ordernums.length) {
                    this.value = ordernums[i];
                    $(this).parent().parent().children().find('[name="Priority"]').val(priorities[i]);
                } else {
                    return false;
                };
            });
        } else {
            MessageModal("Warning", "Select a zone or filter in order to populate order numbers");
        };
    });
    $('#ClearAll').click(function () {
        $('[name="Clear"]').click();
        for (var x = 0; x < PickToteOrdersTable.rows().data().length; x++) {
            $('#PickToteOrdersTable tbody tr').removeClass("success");
        };
    })
    //will fill the order number in the tote setup with the selected order number
    $('#SelectOrder').click(function () {
        var exists =false
        var Orders = $('[name="OrderNumber"]');
        var selectedOrder = PickToteOrdersTable.row($('#PickToteOrdersTable tbody tr.active')[0]).data()[0];
        var selectedPriorty = PickToteOrdersTable.row($('#PickToteOrdersTable tbody tr.active')[0]).data()[2];
        if ($('#PickToteOrdersTable tbody tr.active').val() !== undefined) {
            $.each(Orders, function (i, v) {
                if (this.value == selectedOrder) {
                    exists = true
                    return false
                };
            });

            if (exists) {
                alert("Order " + selectedOrder + " is already selected for this batch")
                //Orders[0].value = PickToteOrdersTable.row($('#PickToteOrdersTable tbody tr.active')[0]).data()[0];
            } else {
                $.each(Orders, function (i, v) {
                    if (this.value == "") {
                        this.value = selectedOrder;
                        $(this).parent().parent().children().find('[name="Priority"]').val(selectedPriorty);
                        return false
                    };
                });
                $('#PickToteOrdersTable tbody tr.active').addClass("success");
            };
        } else {
            MessageModal("Warning", "Please click on the desired order number from the table")
        };
    });

    //redraws the table to account for the filter
    $('#PickToteTransTable').on('filterChange', function () {
        PickToteTransTable.draw();
    });

    //start of filter stuff
    //filter typeahead
    var FilterTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/IM/PickToteSetup/CreateFilterTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#SelFilterVal').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    FilterTA.initialize();

    $('#SelFilterVal').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "FilterTA",
        displayKey: 'Filter',
        source: FilterTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Filter</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:100%;">{{Filter}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        //select data for the page
        GetFilterData();
        $('#AddOrderBy').removeAttr('disabled');
        $('#AddFilterRow').removeAttr('disabled');
        $('#RenameFilter').removeAttr('disabled');
        $('#SetDefault').removeAttr('disabled');
        $('#DeleteFilter').removeAttr('disabled');
        $('#AddFilterRow').removeAttr('disabled');
        $('#AddOrderBy').removeAttr('disabled');
        FillTableOrders($('#SelFilterVal').val(), "", "", false);
        PickToteTransTable.clear();
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', "300px");
    });

    $('#SelFilterVal').on('input', function () {
        $('#PickBatchFilterContainer').html("");
        $('#PickBatchOrderByContainer').html("");
        $('#AddOrderBy').attr('disabled', 'disabled');
        $('#AddFilterRow').attr('disabled', 'disabled');
        $('#RenameFilter').attr('disabled', 'disabled');
        $('#SetDefault').attr('disabled', 'disabled');
        $('#DeleteFilter').attr('disabled', 'disabled');
        $('#AddFilterRow').attr('disabled', 'disabled');
        $('#AddOrderBy').attr('disabled', 'disabled');
    });

    //changes between the filter and zone view
    $('[name="BatchBy"]').change(function () {
        var sel = $('[name="BatchBy"]:checked').val();
        if (sel == "Filter") {
            $('#BatchFilterTab').click();
            $('#BatchFilterTab').show();
            $('#BatchZoneTab').hide();
            $('#FilterTab').show();
        } else if (sel == "Zone") {
            $('#BatchZoneTab').click();
            $('#BatchZoneTab').show();
            SelectZones();
            $('#BatchFilterTab').hide();
            $('#ZoneTab').show();
        };
    });

    //adds a new row for the filer
    $('#AddFilterRow').click(function () {
        if ($('#SelFilterVal').val() != "") {
            InsertNewFilter();
        } else {
            MessageModal("Warning", "Need to either select a filter or add a new one in order to add filter rows")
        };
        
    });

    //inserts the new row into the filter table
    $(document.body).on('click', '[name="NewSaveFilterRow"]', function() {
        //insert record here on success change name of save and delete eamble add
        $this = $(this);
        var p = $this.parent().parent().children();
        var seq = p.find('[name="Sequence"]').val();
        var field = p.find('[name="FilterField"]').val();
        var crit = p.find('[name="Criteria"]').val();
        var val = p.find('[name="Value"]').val();
        var andor = p.find('[name="AndOr"]').val();
        if ((crit == 'Contains Data' || crit == 'Has No Data') || val != "") {
            //save fiter here
            PickToteHub.server.insertNewFilterRow(seq, field, crit, val, andor, $('#SelFilterVal').val()).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred adding this filter");
                } else {
                    $this.attr("name", "SaveFilterRow");
                    $this.attr("disabled", "disabled");
                    p.find('[name="NewDeleteFilterRow"]').attr("name", "DeleteFilterRow");
                    $('#AddFilterRow').removeAttr('disabled');
                    FilterTA.clearRemoteCache();
                    FilterTA.initialize(true);
                    $('#SelFilterVal').trigger('typeahead:selected');
                };
            });
        } else {
            MessageModal("Error", "Some of the inputs are missing values. Cannot add row to filter")
        };
    });

    //deletes the new row
    $(document.body).on('click', '[name="NewDeleteFilterRow"]', function () {
        //remove row from page, not delete since not record yet enable add
        $this = $(this);
        var p = $this.parent().parent()
        p.remove();
        $('#AddFilterRow').removeAttr('disabled');
    });

    //adds a new row for the order by
    $('#AddOrderBy').click(function () {
        if ($('#SelFilterVal').val() != "") {
            InsertNewOrder();
        } else {
            MessageModal("Warning", "Need to either select a filter or add a new one in order to add order by rows")
        };
    });

    //tells that the order sequence was editted and checks value
    $(document.body).on('blur', '[name="OrderSeq"]', function () {
        $this = $(this);
        $this.addClass("Edited")
        if ($this.val() == "") {
            MessageModal("Error", "Please enter a sequence for the order by row");
        } else {
            var seqs = $('[name="OrderSeq"]');
            var largest=0;
            //gets the largets sequence
            $.each(seqs, function (i, v) {
                if (this.value > largest) {
                    largest= parseInt(this.value);
                };
            });
            //checks for conflicts with the sequence
            $.each(seqs, function (i, v) {
                if (parseInt(this.value) == parseInt($this.val()) && !$(this).hasClass("Edited")) {
                    MessageModal("Warning", "Can't have conflicting sequences within the order rows. A new sequence has been provided");
                    $this.val(largest + 1);
                    $this.removeClass("Edited");
                };
            });
            $this.removeClass("Edited");
        };
        
    });
    
    //inserts the new order by into the tale
    $(document.body).on('click', '[name="NewSaveOrderRow"]', function () {
        $this = $(this);
        var p = $this.parent().parent().children();
        var seq = p.find('[name="OrderSeq"]').val();
        var field = p.find('[name="OrderField"]').val();
        var order = p.find('[name="SortOrder"]').val();

        if (seq == "") {
            MessageModal("Error", "Please enter a sequence for the order by row");
        } else {
            PickToteHub.server.insertNewOrderRow($('#SelFilterVal').val(), field, order, seq).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred saving this order row");
                } else {
                    $this.attr("name", "SaveOrderRow");
                    $this.data("id", mess);
                    $this.attr("disabled", "disabled");
                    p.find('[name="NewDeleteOrderRow"]').data("id", mess);
                    p.find('[name="NewDeleteOrderRow"]').attr("name", "DeleteOrderRow");
                    $('#AddOrderBy').removeAttr('disabled');
                };
            });
        };
    });

    //deletes the new row
    $(document.body).on('click', '[name="NewDeleteOrderRow"]', function () {
        //remove row from page, not delete since not record yet enable add
        $this = $(this);
        var p = $this.parent().parent()
        p.remove();
        $('#AddOrderBy').removeAttr('disabled');
    });

    //enable save button for filters
    $(document.body).on('input', '[name="Value"]', function () {
        var $this = $(this);
        $this.parent().siblings().children('[name="SaveFilterRow"]').removeAttr('disabled');
    });
    
    $(document.body).on('change', '[name="Criteria"], [name="AndOr"], [name="FilterField"]', function () {
        var $this = $(this);
        $this.parent().siblings().children('[name="SaveFilterRow"]').removeAttr('disabled');
        if ($(this).attr('name') == 'Criteria') {
            if ($(this).val() == 'Contains Data' || $(this).val() == 'Has No Data') {
                $(this).parent().parent().find('input[name="Value"]').val('')
                $(this).parent().parent().find('input[name="Value"]').attr('disabled', 'disabled')
            } else {
                $(this).parent().parent().find('input[name="Value"]').removeAttr('disabled')
            }
        } 
    });

    //enable save button for order by
    $(document.body).on('input', '[name="OrderSeq"]', function () {
        var $this = $(this);
        $this.parent().siblings().children('[name="SaveOrderRow"]').removeAttr('disabled');
    });

    $(document.body).on('change', '[name="OrderField"], [name="SortOrder"]', function () {
        var $this = $(this);
        $this.parent().siblings().children('[name="SaveOrderRow"]').removeAttr('disabled');
    });


    //saves any changes made to the row
    $(document.body).on('click', '[name="SaveFilterRow"]', function () {
        $this = $(this);
        var p = $this.parent().parent().children();
        var seq = p.find('[name="Sequence"]').val();
        var field = p.find('[name="FilterField"]').val();
        var crit = p.find('[name="Criteria"]').val();
        var val = p.find('[name="Value"]').val();
        var andor = p.find('[name="AndOr"]').val();

        if ((crit != 'Contains Data' && crit != 'Has No Data') && val == "") {
            MessageModal("Warning", "Enter in a value to be checked")
        } else {
            PickToteHub.server.updatePickBatchFilter(seq, field, crit, val, andor, $('#SelFilterVal').val()).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred updating this filter row")
                } else {
                    $this.attr("disabled", "disabled");
                };
            });
        };
    });

    //saves changes made ot the order by row
    $(document.body).on('click', '[name="SaveOrderRow"]', function () {
        $this = $(this);
        var p = $this.parent().parent().children();
        var seq = p.find('[name="OrderSeq"]').val();
        var field = p.find('[name="OrderField"]').val();
        var order = p.find('[name="SortOrder"]').val();
        var id = $this.data("id");

        if (seq == "") {
            MessageModal("Error", "Please enter a sequence for the order by row");
        } else {
            PickToteHub.server.updatePickBatchOrder(field, order, seq, id).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred saving this order by row");
                } else {
                    $this.attr("disabled", "disabled");
                }
            });
        };
    });

    //deletes desired row form the page and table
    $(document.body).on('click', '[name="DeleteFilterRow"]', function () {
        $this = $(this);
        var p = $this.parent().parent().children();
        var seq = p.find('[name="Sequence"]').val();
        var conf = confirm("are you sure you want to delete this filter row?")
        if (conf) {
            PickToteHub.server.deletePickBatchFilter($('#SelFilterVal').val(), seq).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred deleteing this filter row");
                } else {
                    p.remove();
                    FilterTA.clearRemoteCache();
                    FilterTA.initialize(true);
                    if ($('#PickBatchFilterContainer').html() == "") {
                        disableStuff();
                        FilterTA.clearRemoteCache();
                        FilterTA.initialize(true);
                    };
                };
            });
        };
    });
    //Deletes the desired row from the table
    $(document.body).on('click', '[name="DeleteOrderRow"]', function () {
        $this = $(this);
        var p = $this.parent().parent().children();
        var id = $this.data("id");
        var conf = confirm("Are you sure you want to delete this order by row?");
        if (conf) {
            PickToteHub.server.deletePickBatchOrder(id).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred deleteing this order row");
                } else {
                    p.remove();
                }
            });
        };
    });

    //renames the filter to the new value
    $('#RenameFilter').click(function () {
        //do something with renameing a filter
        var OldFilter = $('#SelFilterVal').val();
        var NewFilter = prompt("Enter the new name for the filter " + OldFilter);
        if (NewFilter != null && NewFilter!="") {
            PickToteHub.server.renameFilter(OldFilter, NewFilter).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has occurred renaming this filter")
                } else {
                    $('#SelFilterVal').val(NewFilter);
                };
            });
        };
    });

    //views the filter that is currently set to the default
    $('#ViewDefault').click(function () {
        //view default filter
        PickToteHub.server.selectDefaultFilter().done(function (def) {
            if (def == "Error") {
                MessageModal("Error", "An error has occurred getting the default filter")
            } else {
                if (def == "") {
                    MessageModal("Warning", "No filter is marked as default");
                } else {
                    $('#SelFilterVal').val(def);
                    $("#SelFilterVal").trigger('typeahead:selected');
                };
            };
        });
    });

    //sets the currently dipslayed filter as the defult
    $('#SetDefault').click(function () {
        //set current filter as default
        var conf = confirm("Mark this filter as the default one?");
        if (conf) {
            $('#ClearDefault').click();
            setTimeout(function () {
                if (cleared) {
                    PickToteHub.server.markDefaultFilter($('#SelFilterVal').val()).done(function (mess) {
                        if (mess == "Error") {
                            MessageModal("Error", "An error has occurred setting the default filter")
                        };
                    });
                } else {
                    MessageModal("Warning", "You must clear the default in order to set a new one")
                };
                cleared = false;
            }, 100);
        };
    });

    //unmarks the default filter
    $('#ClearDefault').click(function () {
        //unmark filter as default. check this logic in access
        var conf = confirm("Clear the default filter?");

        if (conf) {
            PickToteHub.server.clearDefaultFilter().done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has ocurred clearing the default filter");
                } else {
                    cleared = true;
                }
            });
        } else {
            cleared = false;
        };
    });

    //deletes the selected filter
    $('#DeleteFilter').click(function () {
        //deletes the filter
        var conf = confirm("Are you sure you want to delete this filter?");

        if (conf) {
            PickToteHub.server.deletePickBatchFilterBatch($('#SelFilterVal').val()).done(function (mess) {
                if (mess == "Error") {
                    MessageModal("Error", "An error has ocurred deleting this filter")
                } else {
                    //clear stuff here
                    disableStuff();
                    FilterTA.clearRemoteCache();
                    FilterTA.initialize(true);
                }
            });
        };
    });

    //adds a new filter
    $('#AddNewFilter').click(function () {
        //inserts a new filter
        var NewFilter = prompt("Enter the new filter title");
        if (NewFilter != null) {
            $('#SelFilterVal').val(NewFilter);
            $('#AddFilterRow').removeAttr('disabled');
            $('#AddOrderBy').removeAttr('disabled');
            $('#PickBatchFilterContainer').html("");
            $('#PickBatchOrderByContainer').html("");
            $('#AddFilterRow').click();
        };
    });

    //end of pick filter stuff

    //start of zone stuff
    //marks a zone as default
    $(document.body).on('click', '[name="DefaultZone"]', function () {
        $this = $(this);
        var p = $this.parent().parent().children();
        var zone = p.find('[name="Zone"]').val();
        var batchtype = p.find('[name="BatchType"]').val();
        PickToteHub.server.markZoneDefault(zone, batchtype).done(function (mess) {
            if (mess == "Error") {
                MessageModal("Error", "An error occurred while marking this zone as the default")
            } else {
                var zones = $('[name="Zones"]');
                $('#PickBatchZoneContainer').find(".has-success").removeClass("has-success");

                p.addClass("has-success")
            };
        });

    });
    //populates the roders table with orders contained in this zone and batch type
    $(document.body).on('click', '[name="ViewZone"]', function () {
        $this = $(this)
        var p = $this.parent().parent().children();
        var zone = p.find('[name="Zone"]').val();
        var batchtype = p.find('[name="BatchType"]').val();
        FillTableOrders("", zone, batchtype, false);
        $('#BatchResultsTab').click();
    });

    $(document.body).on('click', '[name="ViewZoneRP"]', function () {
        $this = $(this)
        var p = $this.parent().parent().children();
        var zone = p.find('[name="Zone"]').val();
        var batchtype = p.find('[name="BatchType"]').val();
        console.log(zone + batchtype)
        FillTableOrders("", zone, batchtype, true);
        $('#BatchResultsTab').click();
    });

    //end of zone stuff


    $('#tote_dismiss').click(function () {
        disableStuff();
        FilterTA.clearRemoteCache();
        FilterTA.initialize(true);
    });

});

//populates the list of selectable zones
function SelectZones() {
    $('#PickBatchZoneContainer').html('')
    PickToteHub.server.selectPickBatchZones().done(function (data) {
        for (var i = 0; i < data.length ; i++) {
            $('#PickBatchZoneContainer').append(
                 '<div class="row top-spacer">' +
                            '<div class="col-md-2">' +
                                '<input type="text" class="form-control" value="' + data[i].BatchZone + '" name="Zone" readonly="readonly"  />' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<input type="text" class="form-control" readonly="readonly" value= "' + data[i].BatchType + '" name="BatchType"  />' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<input type="text" class="form-control" readonly="readonly" value= "' + data[i].TotalOrders + '" name="TotalOrders"  />' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<input type="text" class="form-control" readonly="readonly" value= "' + data[i].TotalLocations + '" name="TotalLocations"  />' +
                            '</div>' +
                            '<div class="col-md-4">' +
                                '<button type="button" class="btn btn-primary" name="DefaultZone">Default</button> ' +
                                '<button type="button" class="btn btn-primary" name="ViewZone">View Non-RP Orders</button> ' +
                                '<button type="button" class="btn btn-primary" name="ViewZoneRP">View RP Orders</button> ' +
                            '</div>' +
                        '</div>');
        };
    });
    
};

//Gets the filter data for the selected filter
function GetFilterData() {
    $('#PickBatchFilterContainer').html("");
    $('#PickBatchOrderByContainer').html("");
    PickToteHub.server.selectFilterData($('#SelFilterVal').val()).done(function (data) {
        //data is object containing 2 lists of object
        var FieldCols = ["Emergency", "Host Transaction ID", "Import Date", "Item Number", "Notes", "Order Number", "Priority", "Required Date",
                        "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8",
                        "User Field9", "Warehouse", "Zone"];
        var CritOpts = ["Equals", "Not Equal To", "Greater Than", "Greater or Equal", "Less or Equal", "Is Like", "Contains Data", "Has No Data"];
        var AndOr = ["And", "Or"];
        var Sort = ["DESC", "ASC"];
        //create drop downs below
        for (var i = 0; i <= data.FilterData.length - 1; i++) {
            var optionstring = "";
            var critstring = "";
            var andorstring = "";
            for (var x = 0; x <= FieldCols.length - 1; x++) {
                if (data.FilterData[i].OTField == FieldCols[x]) {
                    optionstring += '<option selected value="' + FieldCols[x] + '">' + FieldCols[x] + '</option>'
                } else {
                    optionstring += '<option value="' + FieldCols[x] + '">' + FieldCols[x] + '</option>'
                };
            };
            for (var x = 0; x <= CritOpts.length - 1; x++) {
                if (data.FilterData[i].OTCrit == CritOpts[x]) {
                    critstring += '<option selected value="' + CritOpts[x] + '">' + CritOpts[x] + '</option>'
                } else {
                    critstring += '<option value="' + CritOpts[x] + '">' + CritOpts[x] + '</option>'
                };
            };
            for (var z = 0; z <= AndOr.length - 1; z++) {
                if (data.FilterData[i].OTAndOr == AndOr[z]) {
                    andorstring += '<option selected value="' + AndOr[z] + '">' + AndOr[z] + '</option>'
                } else {
                    andorstring += '<option value="' + AndOr[z] + '">' + AndOr[z] + '</option>'
                };
            };
            //appends a row for evyer filter row
            $('#PickBatchFilterContainer').append(
                        '<div class="row top-spacer">' +
                            '<div class="col-md-1">' +
                                '<input type="text" class="form-control" oninput="setNumeric($(this))" value="' + data.FilterData[i].OTSeq + '" name="Sequence" readonly="readonly"  />' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<select name="FilterField" class="form-control">' + optionstring + '</select>' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<select name="Criteria" class="form-control">' + critstring + '</select>' +
                            '</div>' +
                            '<div class="col-md-3">' +
                                '<input type="text" class="form-control" value= "' + data.FilterData[i].OTValue + '" name="Value" placeholder="Value" />' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                 '<select name="AndOr" class="form-control">' + andorstring + '</select>' +
                            '</div>' +
                             '<div class="col-md-2">' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" title="Save Changes Filter Row" disabled class="btn btn-primary" name="SaveFilterRow"><span class="glyphicon glyphicon-floppy-disk"></span></button> ' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" title="Delete Filter Row" class="btn btn-danger" name="DeleteFilterRow"><span class="glyphicon glyphicon-remove"></span></button> ' +
                            '</div>' +
                        '</div>');
        };
        for (var i = 0; i <= data.OrderData.length -1; i++) {
            var optionstring = "";
            var sortstring = "";

            //build drop downs below
            for (var x = 0; x < FieldCols.length; x++) {
                if (data.OrderData[i].Field == FieldCols[x]) {
                    optionstring += '<option selected value="' + FieldCols[x] + '">' + FieldCols[x] + '</option>'
                } else {
                    optionstring += '<option value="' + FieldCols[x] + '">' + FieldCols[x] + '</option>'
                };
            };
            for (var x = 0; x <= Sort.length; x++) {
                if (data.OrderData[i].Order == Sort[x]) {
                    sortstring += '<option selected value="' + Sort[x] + '">' + Sort[x] + '</option>'
                } else {
                    sortstring += '<option value="' + Sort[x] + '">' + Sort[x] + '</option>'
                };
            };
            //for eveyr order by row append a row
            $('#PickBatchOrderByContainer').append(
                        '<div class="row top-spacer">' +
                            '<div class="col-md-2">' +
                                '<input type="text" class="form-control" oninput="setNumeric($(this))" value="' + data.OrderData[i].Seq + '" name="OrderSeq" placeholder="Sequence" />' +
                            '</div>' +
                            '<div class="col-md-4">' +
                                '<select name="OrderField" class="form-control">' + optionstring + '</select>' +
                            '</div>' +
                            '<div class="col-md-3">' +
                                '<select name="SortOrder" class="form-control">' + sortstring + '</select>' +
                            '</div>' +
                            '<div class="col-md-3">' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" disabled data-id="' + data.OrderData[i].ID + '" title="Save Changes Order Row" class="btn btn-primary" name="SaveOrderRow"><span class="glyphicon glyphicon-floppy-disk"></span></button> ' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" data-id="' + data.OrderData[i].ID + '" title="Delete Order Row" class="btn btn-danger" name="DeleteOrderRow"><span class="glyphicon glyphicon-remove"></span></button> ' +
                            '</div>' +
                        '</div>');
        };
    });
};

//inserts anew filter row
function InsertNewFilter() {
    $('#AddFilterRow').attr('disabled', 'disabled');
    var optionstring = "";
    var critstring = "";
    var andorstring = "";

    var FieldCols = ["Emergency", "Host Transaction ID", "Import Date", "Item Number", "Notes", "Order Number", "Priority", "Required Date",
                        "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8",
                        "User Field9", "Warehouse", "Zone"];
    var CritOpts = ["Equals", "Not Equal To", "Greater Than", "Greater or Equal", "Less or Equal", "Is Like", "Contains Data", "Has No Data"];
    var AndOr = ["And", "Or"];

    for (var x = 0; x < FieldCols.length; x++) {
        optionstring += '<option value="' + FieldCols[x] + '">' + FieldCols[x] + '</option>'
    };
    for (var x = 0; x < CritOpts.length; x++) {
        critstring += '<option value="' + CritOpts[x] + '">' + CritOpts[x] + '</option>'
    };
    for (var z = 0; z < AndOr.length; z++) {
        andorstring += '<option value="' + AndOr[z] + '">' + AndOr[z] + '</option>'
    };
    var counter = 0;
    var NumSeqs = $('[name="Sequence"]');
    $.each(NumSeqs, function (i, v) {
        counter+=1;
    });
    //inserts a new filter row
    $('#PickBatchFilterContainer').append(
                        '<div class="row top-spacer">' +
                            '<div class="col-md-1">' +
                                '<input type="text" class="form-control" oninput="setNumeric($(this))" value="' + (counter+1) + '" name="Sequence" readonly="readonly" />' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<select name="FilterField" class="form-control">' + optionstring + '</select>' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                '<select name="Criteria" class="form-control">' + critstring + '</select>' +
                            '</div>' +
                            '<div class="col-md-3">' +
                                '<input type="text" class="form-control" name="Value" placeholder="Value" />' +
                            '</div>' +
                            '<div class="col-md-2">' +
                                 '<select name="AndOr" class="form-control">' + andorstring + '</select>' +
                            '</div>' +
                             '<div class="col-md-2">' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" title="Save Changes Filter Row" class="btn btn-primary" name="NewSaveFilterRow"><span class="glyphicon glyphicon-floppy-disk"></span></button> ' +
                                '<button type="button" data-toggle="tooltip" data-placement="top" title="Delete Filter Row" class="btn btn-danger" name="NewDeleteFilterRow"><span class="glyphicon glyphicon-remove"></span></button> ' +
                            '</div>' +
                        '</div>');
};

//inserts anew order by row
function InsertNewOrder() {
    $('#AddOrderBy').attr('disabled', 'disabled');
    var optionstring = "";
    var sortstring = "";

    var FieldCols = ["Emergency", "Host Transaction ID", "Import Date", "Item Number", "Notes", "Order Number", "Priority", "Required Date",
                        "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8",
                        "User Field9", "Warehouse", "Zone"];
    var Sort = ["DESC", "ASC"];
    for (var x = 0; x < FieldCols.length - 1; x++) {
        optionstring += '<option value="' + FieldCols[x] + '">' + FieldCols[x] + '</option>'
    };
    for (var x = 0; x <= Sort.length - 1; x++) {
        sortstring += '<option value="' + Sort[x] + '">' + Sort[x] + '</option>'
    };
    var counter = 0;
    var NumSeqs = $('[name="OrderSeq"]');
    $.each(NumSeqs, function (i, v) {
        counter += 1;
    });
    //inserts a new order by row
    $('#PickBatchOrderByContainer').append(
                '<div class="row top-spacer">' +
                    '<div class="col-md-2">' +
                        '<input type="text" class="form-control" oninput="setNumeric($(this))" value="' + (counter+1) + '" name="OrderSeq" placeholder="Sequence" />' +
                    '</div>' +
                    '<div class="col-md-4">' +
                        '<select name="OrderField" class="form-control">' + optionstring + '</select>' +
                    '</div>' +
                    '<div class="col-md-3">' +
                        '<select name="SortOrder" class="form-control">' + sortstring + '</select>' +
                    '</div>' +
                    '<div class="col-md-3">' +
                        '<button type="button" data-toggle="tooltip" data-placement="top" title="Save Changes Order Row" class="btn btn-primary" name="NewSaveOrderRow"><span class="glyphicon glyphicon-floppy-disk"></span></button> ' +
                        '<button type="button" data-toggle="tooltip" data-placement="top" title="Delete Order Row" class="btn btn-danger" name="NewDeleteOrderRow"><span class="glyphicon glyphicon-remove"></span></button> ' +
                    '</div>' +
                '</div>');
};

//fills the orders table depeding on if filter or zone is selected
function FillTableOrders(filter, zone, type, RP) {
    if (filter !="") {
        PickToteHub.server.selectOrdersFilterZone(filter, "", "", 0, 0, false).done(function (data) {
            if (data.length > 0) {
                if (data[0][0] == "Error") {
                    MessageModal("Error", "An error has occurred using the selected filter. Make sure the fields are filled in with their desired values");
                } else {
                    PickToteOrdersTable.clear().draw();
                    PickToteOrdersTable.rows.add(data).draw();
                    PickToteTransTable.clear().draw();
                };
            } else {
                PickToteOrdersTable.clear().draw();
                PickToteTransTable.clear().draw();
            };
        });
    } else {
        //zone
        PickToteHub.server.selectOrdersFilterZone("", zone, type, 0, 0, RP).done(function (data) {
            if (data.length > 0) {
                if (data[0][0] == "Error") {
                    MessageModal("Error", "An error has occurred using the selected zone");
                } else {
                    PickToteOrdersTable.clear().draw();
                    PickToteOrdersTable.rows.add(data).draw();
                    PickToteTransTable.clear().draw();
                };
            } else {
                PickToteOrdersTable.clear().draw();
                PickToteTransTable.draw();
            };
        });
    };
};

function disableStuff() {
    $('#SelFilterVal').val("");
    $('#PickBatchFilterContainer').html("");
    $('#PickBatchOrderByContainer').html("");
    $('#AddOrderBy').attr('disabled', 'disabled');
    $('#AddFilterRow').attr('disabled', 'disabled');
    $('#RenameFilter').attr('disabled', 'disabled');
    $('#SetDefault').attr('disabled', 'disabled');
    $('#DeleteFilter').attr('disabled', 'disabled');
    $('#AddFilterRow').attr('disabled', 'disabled');
    $('#AddOrderBy').attr('disabled', 'disabled');
};