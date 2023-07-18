// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var PickToteHub = $.connection.pickToteSetupHub;
$(document).ready(function () {
    var pd = $('#PrintDirect').val() == 'true';
    AppendToteIDRows();

    //Typeaheads here for order number and batch
    var OrderTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/IM/PickToteSetup/OrderNumberTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#OrderNumberTA').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    OrderTA.initialize();

    $('#OrderNumberTA').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "OrderTA",
        displayKey: 'Order',
        source: OrderTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Order Number</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:100%;">{{Order}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
        var Orders = $('[name="OrderNumber"]');
        var exists;
        $.each(Orders, function (i, v) {
            if (this.value == "") {
                this.value = $('#OrderNumberTA').val();
                return false
            } else {
                if (this.value == $('#OrderNumberTA').val()) {
                    exists = true
                    return false
                };
            };
        });

        if (exists) {
            MessageModal("Invalid order", "Order " + $('#OrderNumberTA').val() + " is already selected for this batch");
        };

        ClearOrderTA();

    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', "300px");
    });


    var BatchTA = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/IM/PickToteSetup/BatchPickIDTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $('#BatchPickIDTA').val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    BatchTA.initialize();

    $('#BatchPickIDTA').typeahead({
        hint: false,
        highlight: false,
        minLength: 0
    }, {
        name: "BatchTA",
        displayKey: 'BatchID',
        source: BatchTA.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:100%;">Batch Pick ID</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row" style="width:100%;">{{BatchID}}</p>')
        }
    }).on('typeahead:selected', function (obj, datum, name) {
       
    }).on('typeahead:opened', function () {
        $(this).siblings('.tt-dropdown-menu').css('width', "300px");
    });
    //End of Typeaheads

    $(window).on('beforeunload', function (e) {
        return 'You are currently processing a pick batch';
    });

    $('#OrderNumberTA').keyup(function (e) {
        if (e.keyCode === 13) {
            var Orders = $('[name="OrderNumber"]');
            var exists = false;

            PickToteHub.server.validOrderForZone($('#OrderNumberTA').val()).done(function (valid) {
                if (valid) {
                    $.each(Orders, function (i, v) {
                        if (this.value == "") {
                            this.value = $('#OrderNumberTA').val();
                            return false
                        } else {
                            if (this.value == $('#OrderNumberTA').val()) {
                                exists = true
                                return false
                            };
                        };
                    });

                    if (exists) {
                        MessageModal("Invalid order", "Order " + $('#OrderNumberTA').val() + " is already selected for this batch");
                    };

                } else {
                    MessageModal("Invalid order", "Order " + $('#OrderNumberTA').val() + " does not have a line go to " + $('#ZonesPanelTitle').html());
                };

                ClearOrderTA();
            });
        };
    });

    $('#SelectWSZones').click(function () {
        $('#WSZonesModal').modal('show');
    });

    $('#ViewAllOrders').click(function () {
        FillInZoneTableOrders('All');
        $('#InZoneOrderModal').modal('show');
    });

    $('#ViewNonReplenOrders').click(function () {
        FillInZoneTableOrders('NonReplen');
        $('#InZoneOrderModal').modal('show');
    });

    $('#ViewReplenOrders').click(function () {
        FillInZoneTableOrders('Replen');
        $('#InZoneOrderModal').modal('show');
    });


    $('#BlossomTote').click(function () {
        $('#BlossomModal').modal('show');
    });

    $('#NewPickBatch').click(function () {
        MessageModal("New Batch", "Create a New Batch?", undefined, undefined,
            function () {
                $('#PickBatchContainer').html("");
                AppendToteIDRows();
                $('#PickBatchID').val("");
            }
        );
    });

    $('#NewPickBatchID').click(function () {
        MessageModal("New Batch", "Create a New Batch with the next Batch Pick ID?", undefined, undefined,
            function () {
                PickToteHub.server.nextBatchID().done(function (id) {
                    $('#PickBatchContainer').html("");
                    AppendToteIDRows();
                    if ($('#AutoPickTote').val() == "true") {
                        AutoFillTotes();
                    };
                    $('#PickBatchID').val(id);
                })
            }
        );
    });

    $('#FillAllToteID').click(function () {
        AutoFillTotes();
    });

    $('#FillNextToteID').click(function () {
        var count = 0
        PickToteHub.server.grabNextToteID().done(function (nextTote) {
            $('[name="ToteID"]').filter(function () {
                if (this.value == "" && count == 0) {
                    count += 1;
                    return !this.value;
                };
            }).val(nextTote);
            //sets the next tote id to the old one plus one
            PickToteHub.server.updateNextToteID(nextTote + 1).done(function (updated) {
                if (!updated) {
                    MessageModal('Error', 'An error occurred while attempting to save the new "Next Tote ID"');
                };
            });
        });
    });

    $(document.body).on('click', '[name="NextToteID"]', function () {
        $this = $(this);
        PickToteHub.server.grabNextToteID().done(function (nextTote) {
            $this.parent().siblings().find('[name="ToteID"]').val(nextTote);
            //sets the next tote id to the old one plus one
            PickToteHub.server.updateNextToteID(nextTote + 1).done(function (updated) {
                if (!updated) {
                    MessageModal('Error', 'An error occurred while attempting to save the new "Next Tote ID"');
                };
            });
        });
    });

    //goes to next tote id field
    $(document.body).on('keyup', '[name="ToteID"]', function (e) {
        var $this = $(this);
        if (e.keyCode == 13) {
            $this.parent().parent().next().children().find('[name="ToteID"]').focus();
        };
    });

    //checks to make sure tote id is new and not already in the list
    $(document.body).on('blur', '[name="ToteID"]', function () {
        var $this = $(this)
        var totes = $('[name="ToteID"]');
        var p = $this.parent().parent().children();
        var position = parseInt(p.find('[name="Position"]').val()) - 1;
        if ($this.val() != "") {
            //loop thorugh each tote id
            $.each(totes, function (i, v) {
                if (i != position) {
                    if (this.value == $this.val()) {
                        MessageModal("Conflict", "This tote id is already in the batch. Enter a new one");
                        $this.val('');
                    };
                };
            });
        };
    });


    $(document.body).on('click', '#ClearAllOrders', function () {
        $('[name="Clear"]').click();
    });

    $(document.body).on('click', '#ClearAllTotes', function () {
        $('[name="ToteID"]').val("");
    });

    //empties the desired tote id and order number
    $(document.body).on('click', '[name="Clear"]', function () {
        var p = $(this).parent().parent().children();
        p.find('[name="OrderNumber"]').val('');
    });

    //Opens the order status screen for the given order number
    $(document.body).on('click', '[name="ViewOrder"]', function () {
        //go to order status here for the corresponding order number
        var p = $(this).parent().parent().children();
        var ordernum = p.find('[name="OrderNumber"]').val();
        if (ordernum == "") {
            MessageModal("Error", "Please enter in an order number")
        } else {
            var $handle = $(window.open("/Transactions?viewtoShow=1&OrderStatusOrder=" + ordernum + "&App=" + $('#AppName').val() + '&popup=true', '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'));
            try {

                setTimeout(function () {
                    var press = jQuery.Event("keydown");
                    press.ctrlKey = false;
                    press.which = 13;
                    press.keyCode = 13;

                    console.log($handle[0].document)

                    $handle[0].$('#ordernumFilterOrder').typeahead('val', ordernum);
                    $handle[0].$('#ordernumFilterOrder').trigger(press).blur();
                    $handle[0].focus();
                }, 5000);

            } catch (e) {
                MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.');
            };
        };
    });

    //proccsses the current tote setup
    $('#PickProcess').click(function () {
        MessageModal("Process Batch", "Process the current setup?", undefined, undefined,
            function () {
                //process the current pick bacth configuration
                var totes = $('[name="ToteID"]');
                var Pos = $('[name="Position"]');
                var Orders = $('[name="OrderNumber"]');
                var toteempty = true;

                if ($('#PickBatchID').val() == "") {
                    MessageModal("Warning", "Please enter in a batch id to proccess");
                } else {
                    $.each(totes, function (i, v) {
                        if (this.value != "") {
                            toteempty = false;
                            return false
                        };
                    });
                    if (toteempty) {
                        MessageModal("Warning", "Please enter in at least 1 tote id to proccess");
                    } else {
                        var orderempty = true;
                        $.each(Orders, function (i, v) {
                            if (this.value != "") {
                                orderempty = false;
                                return false
                            };
                        });
                        if (orderempty) {
                            MessageModal("Warning", "Please enter in at least 1 order number to process");
                        } else {
                            //start processing
                            var counter = Pos.length
                            var PositionList = [];
                            var ToteList = [];
                            var OrderList = [];
                            //add values to each list
                            for (var i = 0; i <= counter - 1; i++) {
                                if (totes[i].value != "" && Orders[i].value != "") {
                                    PositionList.push(Pos[i].value);
                                    ToteList.push(totes[i].value)
                                    OrderList.push(Orders[i].value)
                                };
                            };

                            PickToteHub.server.validateTotes(ToteList).done(function (ToteVal) {
                                switch (ToteVal) {
                                    case "Error":
                                        MessageModal("Error", "An error has occurred validating the current tote setup");
                                        break;
                                    case "":
                                        //call process tote setup function
                                        PickToteHub.server.processInZoneSetup(PositionList, ToteList, OrderList, $('#PickBatchID').val()).done(function (mess) {
                                            if (mess == "Error") {
                                                MessageModal("Error", "An error has occurred processing the current tote setup");
                                            } else {
                                                //var printData = {
                                                //    Positions: PositionList,
                                                //    ToteIDs: ToteList,
                                                //    OrderNums: OrderList,
                                                //    BatchID: $('#PickBatchID').val(),
                                                //    printDirect: pd
                                                //};

                                                var BatchID = $('#PickBatchID').val();
                                                if ($('#AutoPrintPickToteLabs').val() == "true") {
                                                    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneBatchToteLabel/', {
                                                        BatchID: BatchID,
                                                        printDirect: pd
                                                    }, pd, 'label', "Batch Tote Label", function () {
                                                        if ($('#AutoPrintOffCarPickList').val() == "true") {
                                                            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchList/', {
                                                                BatchID: BatchID,
                                                                printDirect: pd
                                                            }, pd, 'label', "Batch Pick List", function () {
                                                                if ($('#AutoPrintCaseLabel').val() == "true") {
                                                                    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneCaseLabel/', {
                                                                        BatchID: BatchID,
                                                                        printDirect: pd
                                                                    }, pd, 'label', "Case Label", function () {
                                                                        if ($('#AutoPrintPickBatchList').val() == "true") {
                                                                            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevPickBatchList/', {
                                                                                BatchID: BatchID,
                                                                                printDirect: pd
                                                                            }, pd, 'list', "Pick Batch List");
                                                                        };
                                                                    });
                                                                };
                                                            });
                                                        };
                                                    });
                                                } else if ($('#AutoPrintOffCarPickList').val() == "true") {
                                                    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchList/', {
                                                        BatchID: BatchID,
                                                        printDirect: pd
                                                    }, pd, 'label', "Batch Pick List", function () {
                                                        if ($('#AutoPrintCaseLabel').val() == "true") {
                                                            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneCaseLabel/', {
                                                                BatchID: BatchID,
                                                                printDirect: pd
                                                            }, pd, 'label', "Case Label", function () {
                                                                if ($('#AutoPrintPickBatchList').val() == "true") {
                                                                    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevPickBatchList/', {
                                                                        BatchID: BatchID,
                                                                        printDirect: pd
                                                                    }, pd, 'list', "Pick Batch List");
                                                                };
                                                            });
                                                        };
                                                    });
                                                } else if ($('#AutoPrintCaseLabel').val() == "true") {
                                                    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneCaseLabel/', {
                                                        BatchID: BatchID,
                                                        printDirect: pd
                                                    }, pd, 'label', "Case Label", function () {
                                                        if ($('#AutoPrintPickBatchList').val() == "true") {
                                                            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevPickBatchList/', {
                                                                BatchID: BatchID,
                                                                printDirect: pd
                                                            }, pd, 'list', "Pick Batch List");
                                                        };
                                                    });
                                                } else if ($('#AutoPrintPickBatchList').val() == "true") {
                                                    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevPickBatchList/', {
                                                        BatchID: BatchID,
                                                        printDirect: pd
                                                    }, pd, 'list', "Pick Batch List");
                                                };

                                                //if ($('#AutoPrintOffCarPickList').val() == "true") {
                                                //    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchList/', {
                                                //        BatchID: $('#PickBatchID').val(),
                                                //        printDirect: pd
                                                //    }, pd, 'label', "Batch Pick List");
                                                //};

                                                //if ($('#AutoPrintCaseLabel').val() == "true") {
                                                //    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneCaseLabel/', {
                                                //        BatchID: $('#PickBatchID').val(),
                                                //        printDirect: pd
                                                //    }, pd, 'label', "Case Label");
                                                //};

                                                //if ($('#AutoPrintPickBatchList').val() == "true") {
                                                //    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevPickBatchList/', {
                                                //        BatchID: $('#PickBatchID').val(),
                                                //        printDirect: pd
                                                //    }, pd, 'list', "Pick Batch List");
                                                //};

                                                //refresh the counts to show how mnay transactions of each type are left
                                                $('#PickBatchContainer').html("");
                                                AppendToteIDRows();
                                                $('#PickBatchID').val('')
                                            };
                                        });
                                        break;
                                    default:
                                        MessageModal("Invalid Tote ID", "The tote id " + ToteVal + " already exists in Open Transactions. Please select another tote");
                                        break;
                                };
                            });
                        };
                    };
                };
            }
        );
    });

    //printing below
    $(document.body).on('click', '#PrintToteLabs', function () {
        var totes = $('[name="ToteID"]');
        var Pos = $('[name="Position"]');
        var Orders = $('[name="OrderNumber"]');
        var toteempty = true

        $.each(totes, function (i, v) {
            if (this.value != "") {
                toteempty = false;
                return false
            };
        });
        if (toteempty) {
            MessageModal("Warning", "Please enter in at least 1 tote id");
        } else {
            var orderempty = true;
            $.each(Orders, function (i, v) {
                if (this.value != "") {
                    orderempty = false;
                    return false
                };
            });
            if (orderempty) {
                MessageModal("Warning", "Please enter in at least 1 order number");
            } else {
                //start processing
                var counter = Pos.length
                var PositionList = [];
                var ToteList = [];
                var OrderList = [];
                //build lists
                for (var i = 0; i <= counter - 1; i++) {
                    if (totes[i].value != "" && Orders[i].value != "") {
                        PositionList.push(Pos[i].value);
                        ToteList.push(totes[i].value)
                        OrderList.push(Orders[i].value)
                    };
                };
                getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneToteLabel/', {
                    Positions: PositionList,
                    ToteIDs: ToteList,
                    OrderNums: OrderList,
                    printDirect: pd
                }, pd, 'label', "Tote Label Preview");
            };
        };
    });

    $(document.body).on('click', '#PrintPickLabels', function () {
        var totes = $('[name="ToteID"]');
        var Pos = $('[name="Position"]');
        var Orders = $('[name="OrderNumber"]');
        var toteempty = true

        if ($('#PickBatchID').val() == "") {
            MessageModal("Warning", "Please enter in a batch id");
        } else {
            $.each(totes, function (i, v) {
                if (this.value != "") {
                    toteempty = false;
                    return false
                };
            });
            if (toteempty) {
                MessageModal("Warning", "Please enter in at least 1 tote id");
            } else {
                var orderempty = true;
                $.each(Orders, function (i, v) {
                    if (this.value != "") {
                        orderempty = false;
                        return false
                    };
                });
                if (orderempty) {
                    MessageModal("Warning", "Please enter in at least 1 order number");
                } else {
                    var counter = Pos.length
                    var PositionList = [];
                    var ToteList = [];
                    var OrderList = [];
                    //build lists
                    for (var i = 0; i <= counter - 1; i++) {
                        if (totes[i].value != "" && Orders[i].value != "") {
                            PositionList.push(Pos[i].value);
                            ToteList.push(totes[i].value)
                            OrderList.push(Orders[i].value)
                        };
                    };
                    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneItemLabel', {
                        Positions: PositionList,
                        ToteIDs: ToteList,
                        OrderNums: OrderList,
                        BatchID: $('#PickBatchID').val(),
                        printDirect: pd
                    }, pd, 'label', "Off Carousel Pick List");
                };
            };
        };
    });

    $(document.body).on('click', '#PrintPickList', function () {
        var totes = $('[name="ToteID"]');
        var Pos = $('[name="Position"]');
        var Orders = $('[name="OrderNumber"]');
        var toteempty = true;
        $.each(totes, function (i, v) {
            if (this.value != "") {
                toteempty = false;
                return false
            };
        });
        if (toteempty) {
            MessageModal("Warning", "Please enter in at least 1 tote id");
        } else {
            var orderempty = true;
            $.each(Orders, function (i, v) {
                if (this.value != "") {
                    orderempty = false;
                    return false
                };
            });
            if (orderempty) {
                MessageModal("Warning", "Please enter in at least 1 order number");
            } else {
                var counter = Pos.length
                var PositionList = [];
                var ToteList = [];
                var OrderList = [];
                //build lists
                for (var i = 0; i <= counter - 1; i++) {
                    if (totes[i].value != "" && Orders[i].value != "") {
                        PositionList.push(Pos[i].value);
                        ToteList.push(totes[i].value);
                        OrderList.push(Orders[i].value);
                    };
                };
                //either print or preview
                PickToteHub.server.processPickToteSetup(PositionList, ToteList, OrderList, $('#PickBatchID').val(), 1).done(function (mess) {
                    if (mess == "Error") {
                        MessageModal("Error", "An error has occurred getting the count");
                    } else {
                        if (parseInt(mess) > 0) {
                            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZonePickList', {
                                Positions: PositionList,
                                ToteIDs: ToteList,
                                OrderNums: OrderList,
                                BatchID: $('#PickBatchID').val(),
                                printDirect: pd
                            }, pd, 'report', "Off Carousel Pick List");
                        } else {
                            MessageModal("Warning", "No Records to Print out for off carousel list");
                        };
                    };
                });
            };
        };
    });

    $(document.body).on('click', '[name="ItemLabels"]', function () {
        $this = $(this)
        //var p = $this.parent().parent().children();
        var p = $this.closest('div.row.top-spacer');
        var OrderList = [p.find('[name="OrderNumber"]').val()];
        var PositionList = [p.find('[name="Position"]').val()];
        var ToteList = [p.find('[name="ToteID"]').val()];
        var missing = OrderList.length != ToteList.length;
        console.log(OrderList);
        for (var x = 0; x < OrderList.length; x++) {
            if (OrderList[x] == '' || ToteList[x] == '') {
                missing = true;
            };
        };
        //check if fields filled out for desired print button
        if (missing) {
            MessageModal("Warning", "Missing data from the desired print row")
        } else {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneItemLabel', {
                Positions: PositionList,
                ToteIDs: ToteList,
                OrderNums: OrderList,
                BatchID: $('#PickBatchID').val(),
                printDirect: pd
            }, pd, 'label', "Off Carousel Pick List");
        };
    });

    $(document.body).on('click', '[name="totelabel"]', function () {
        //print stuff here
        $this = $(this)
        //var p = $this.parent().parent().children();
        var p = $this.closest('div.row.top-spacer');
        var OrderList = [p.find('[name="OrderNumber"]').val()];
        var PositionList = [p.find('[name="Position"]').val()];
        var ToteList = [p.find('[name="ToteID"]').val()];
        var missing = OrderList.length != ToteList.length;
        console.log(OrderList);
        for (var x = 0; x < OrderList.length; x++) {
            if (OrderList[x] == '' || ToteList[x] == '') {
                missing = true;
            };
        };
        if (missing) {
            MessageModal("Warning", "Missing data from the desired print row")
        } else {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneToteLabel/', {
                Positions: PositionList,
                ToteIDs: ToteList,
                OrderNums: OrderList,
                printDirect: pd
            }, pd, 'label', "Tote Label Preview");
        };

    });

    //ReprintBelow
    $('#PrintBatchLocLabel').click(function () {
        if ($('#BatchPickIDTA').val() != "") {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneBatchToteLabel/', {
                BatchID: $('#BatchPickIDTA').val(),
                printDirect: pd
            }, pd, 'label', "Batch Tote Label");
        } else {
            alert("Please select a Batch ID to print");
        };
    });

    $('#PrintBatchPickLabel').click(function () {
        if ($('#BatchPickIDTA').val() != "") {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchItemLabel/', {
                BatchID: $('#BatchPickIDTA').val(),
                printDirect: pd
            }, pd, 'label', "Batch Item Label");
        } else {
            alert("Please select a Batch ID to print");
        };
    });

    $('#PrintBatchPickList').click(function () {
        if ($('#BatchPickIDTA').val() != "") {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchList/', {
                BatchID: $('#BatchPickIDTA').val(),
                printDirect: pd
            }, pd, 'label', "Batch Pick List");
        } else {
            alert("Please select a Batch ID to print");
        };
    });


    $('#PrintCaseLabel').click(function () {
        if ($('#BatchPickIDTA').val() != "") {
            if ($('#AutoPrintCaseLabel').val() == "true") {
                getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneCaseLabel/', {
                    BatchID: $('#BatchPickIDTA').val(),
                    printDirect: pd
                }, pd, 'label', "Case Label");
            };
        } else {
            alert("Please select a Batch ID to print");
        };
    });

    $('#PrintPickBatchList').click(function () {
        if ($('#BatchPickIDTA').val() != "") {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevPickBatchList/', {
                BatchID: $('#BatchPickIDTA').val(),
                printDirect: pd
            }, pd, 'list', "Pick Batch List");
        } else {
            alert("Please select a Batch ID to print");
        };
    });


});

//appends rows for the tote id and order number
function AppendToteIDRows() {
    //for every batch qty value append a tote id line
    for (var i = 0; i < parseInt($('#BatchQty').val()) ; i++) {
        $('#PickBatchContainer').append(
             '<div class="row top-spacer">' +
                        '<div class="col-md-1">' +
                            '<input type="text" class="form-control" value="' + (i + 1) + '" name="Position" readonly="readonly"  />' +
                        '</div>' +
                        '<div class="col-md-3">' +
                            '<input type="text" class="form-control"  name="ToteID" placeholder="ToteID" />' +
                        '</div>' +
                        '<div class="col-md-3">' +
                            '<input type="text" class="form-control" disabled="disabled" name="OrderNumber" placeholder="Order Number" />' +
                        '</div>' +
                        '<div class="col-md-5">' +
                            '<button type="button" data-toggle="tooltip" title="Remove Order" data-placement="top" class="btn btn-danger" name="Clear"><span class="glyphicon glyphicon-remove"></span></button> ' +
                            '<div style="display:inline-block" class="dropdown"><button id="goTo" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button><ul class="dropdown-menu" role="menu"><li><a name="totelabel" class="Print-Label">Print Tote Label</a></li><li><a name="ItemLabels" class="Print-Label">Print Pick Labels</a></li></ul></div> ' +
                            '<button type="button" class="btn btn-primary" name="ViewOrder">View Order</button> ' +
                            '<button type="button" class="btn btn-primary" name="NextToteID">Next Tote ID</button> ' +
                        '</div>' +
                    '</div>')
    };
};

//autofills tote ids
function AutoFillTotes() {
    var totes = $('[name="ToteID"]');
    var counter = 0;
    //gets the next tote id 
    PickToteHub.server.grabNextToteID().done(function (nextTote) {
        $.each(totes, function (i, v) {
            if (this.value == "") {
                //increments tote id by the counter
                this.value = nextTote + counter;
                counter += 1;
            };
        });
        //sets the next tote id to the old one plus the counter value
        PickToteHub.server.updateNextToteID(nextTote + counter).done(function (updated) {
            if (!updated) {
                MessageModal('Error', 'An error occurred while attempting to save the new "Next Tote ID"');
            };
        });
    });
};

function ClearOrderTA() {
    $('#OrderNumberTA').val('');
    $('#OrderNumberTA').typeahead('val', '');
    $('#OrderNumberTA').typeahead('close');
};