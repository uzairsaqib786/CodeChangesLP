// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var PickToteHub = $.connection.pickToteSetupHub
$(document).ready(function () {
    var pd = $('#PrintDirect').val() == 'true';
    AppendToteIDRows();
    var prevOrderNumFocus = "";
    var prevOrderNumInput = "";
    var PrevBatchFocus = "";
    //addResize(function () { $('#PickBatchContainer').css('max-height', $(window).height() * 0.65); });

    //refreshes the count of avaible transactions
    $('#RefreshOrderCounts').click(function () {
        PickToteHub.server.selectCountInfo().done(function (data) {
            $('#MixedZonesCount').val(data.Mixed);
            $('#CarouselCount').val(data.Car);
            $('#OffCarouselCount').val(data.OffCar);
        });
    });
    //gets a new bacth
    $('#NewPickBatch').click(function () {
        NewBatchConf();
    });

    $(window).on('beforeunload', function (e) {
        return 'You are currently processing a pick batch';
    });

    //gets a new bacth with the next available batch id
    $('#NewPickBatchID').click(function () {
        var useConfig = NewBatchConf();
        if (useConfig != "not done") {
            PickToteHub.server.nextBatchID().done(function (id) {
                $('#PickBatchID').val(id);
                //check for auto fills 
                if (!useConfig) {
                    if ($('#UsePickBatchManager').val() == "false") {
                        if ($('#AutoPickTote').val() == "true") {
                            AutoFillTotes();
                        };
                        if ($('#AutoPickOrder').val() == "true") {
                            AutoFillOrders();
                        };
                    } else {
                        if ($('#DefFilter').val() == "true") {
                            //use def filter
                            PickToteHub.server.selectOrdersFilterZone("", "", "", 1, 0, false).done(function (data) {
                                if (data[0] == "Error") {
                                    MessageModal("Error", "An error has occurred using the default filter. Please make sure that a filter is makred as default and that it filters on the right data");
                                } else {
                                    //fill totes table
                                    var Orders = $('[name="OrderNumber"]');
                                    var counter;
                                    counter = data.length
                                    if (data.length != 0) {
                                        $.each(Orders, function (i, v) {
                                            if (counter > 0) {
                                                this.value = data[data.length - counter];
                                                counter = counter - 1;
                                            } else {
                                                this.value = ""
                                            };
                                        });
                                    };
                                };
                            });
                        } else if ($('#DefZone').val() == "true") {
                            //use def zone
                            PickToteHub.server.selectOrdersFilterZone("", "", "", 0, 1, false).done(function (data) {
                                if (data[0] == "Error") {
                                    MessageModal("Error", "An error has occurred using the default filter. Please make sure that a filter is makred as default and that it filters on the right data");
                                } else {
                                    //fill totes table
                                    var Orders = $('[name="OrderNumber"]');
                                    var counter;
                                    counter = data.length
                                    if (data.length != 0) {
                                        $.each(Orders, function (i, v) {
                                            if (counter > 0) {
                                                this.value = data[data.length - counter];
                                                counter = counter - 1;
                                            } else {
                                                this.value = ""
                                            };
                                        });
                                    };
                                };
                            });
                        } else {
                            //if no def filter or def zone open pick batch manager modal
                            $('#PickBatchManager').click();
                        };
                    };
                };
            });
        };
    });
    //sets the previous batch id to confirm that it is not a new one
    $('#PickBatchID').focus(function () {
        PrevBatchFocus = $('#PickBatchID').val();
    });

    //if batch id is new then check for auto filling totes and orders
    $('#PickBatchID').blur(function () {
        if ($('#PickBatchID').val() != "") {
            if (PrevBatchFocus != $('#PickBatchID').val()) {
                if ($('#UsePickBatchManager').val() == "false") {
                    if ($('#AutoPickTote').val() == "true") {
                        AutoFillTotes();
                    };
                    if ($('#AutoPickOrder').val() == "true") {
                        AutoFillOrders();
                    };
                } else {
                    if ($('#DefFilter').val() == "true") {
                        PickToteHub.server.selectOrdersFilterZone("", "", "", 1, 0, false).done(function (data) {
                            if (data[0] == "Error") {
                                MessageModal("Error", "An error has occurred using the default filter. Please make sure that a filter is makred as default and that it filters on the right data");
                            } else {
                                //fill totes table
                                var Orders = $('[name="OrderNumber"]');
                                var counter;
                                counter = data.length
                                if (data.length != 0) {
                                    $.each(Orders, function (i, v) {
                                        if (counter > 0) {
                                            this.value = data[data.length - counter];
                                            counter = counter - 1;
                                        } else {
                                            this.value = ""
                                        };
                                    });
                                };
                            };
                        });
                    } else if ($('#DefZone').val() == "true") {
                        PickToteHub.server.selectOrdersFilterZone("", "", "", 0, 1, false).done(function (data) {
                            if (data[0] == "Error") {
                                MessageModal("Error", "An error has occurred using the default filter. Please make sure that a filter is makred as default and that it filters on the right data");
                            } else {
                                //fill totes table
                                var Orders = $('[name="OrderNumber"]');
                                var counter;
                                counter = data.length
                                if (data.length != 0) {
                                    $.each(Orders, function (i, v) {
                                        if (counter > 0) {
                                            this.value = data[data.length - counter];
                                            counter = counter - 1;
                                        } else {
                                            this.value = ""
                                        };
                                    });
                                };
                            };
                        });
                    } else {
                        $('#PickBatchManager').click();
                    };
                };
            };
        } else {
            //if batch id was cleared and not transfered clicked on when initially empty
            if (PrevBatchFocus != "") {
                $('#NewPickBatch').click();
            };
        };
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



    //goes to next tote id field
    $(document.body).on('keyup', '[name="ToteID"]', function (e) {
        var $this = $(this);
        if (e.keyCode == 13) {
            $this.parent().parent().next().children().find('[name="ToteID"]').focus();
        };
    });

    //sets the previous order number to make sure it was not changed on the focus
    $(document.body).on('focus', '[name="OrderNumber"]', function () {
        var $this = $(this);
        if ($this.val() != "") {
            prevOrderNumFocus = $this.val();
        };
    });
    //set the previous order number to the one that is being entered
    $(document.body).on('input', '[name="OrderNumber"]', function () {
        var $this = $(this);
        if ($this.val() != "") {
            prevOrderNumInput = $this.val();
        };
    });
    //checks whether order number is valid and will confirm that the user wants to change it
    $(document.body).on('blur', '[name="OrderNumber"]', function () {
        var prevordernumfocus = prevOrderNumFocus;
        var $this = $(this);
        var dupCount = 0
        $('[name="OrderNumber"]').each(function () {
            var $currentIter = $(this);
            if ($currentIter.val() == $this.val() && $currentIter.val() != "") {
                dupCount += 1;
            }
        });
        if (dupCount > 1) {
            alert("Each order can only be assigned to a single tote in a batch ")
            $this.val("").focus();
            return;
        };

        //check if order number user wants to change order number
        if (prevOrderNumInput != "" && prevordernumfocus != "") {
            var conf = confirm("Leave the original order number? Press OK to leave the original order number");
            if (conf) {
                $this.val(prevordernumfocus);
            } else {
                //verify that new order number is allowed
                if (prevordernumfocus != "" || prevOrderNumInput != "") {
                    PickToteHub.server.validateOrderNumber($this.val()).done(function (valid) {
                        if (valid == "Invalid") {
                            MessageModal("Invalid Order Number", "This is not a valid order number for this pick batch");
                            $this.val(prevordernumfocus);
                        } else {
                            $this.parent().parent().children().find('[name="Priority"]').val(valid);
                        };
                    });
                };
            };
        } else if ($this.val() != "") {
            PickToteHub.server.validateOrderNumber($this.val()).done(function (valid) {
                if (valid == "Invalid") {
                    MessageModal("Invalid Order Number", "This is not a vaild order number for this pick batch");
                    $this.val(prevordernumfocus);
                } else {
                    $this.parent().parent().children().find('[name="Priority"]').val(valid);
                };
            });
        };
        prevOrderNumFocus = "";
        prevOrderNumInput = "";
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
    })
    $(document.body).on('click', '#ClearAllTotes', function () {
        $('[name="ToteID"]').val("");
    })
    //empties the desired tote id and order number
    $(document.body).on('click', '[name="Clear"]', function () {
        var p = $(this).parent().parent().children();
        p.find('[name="OrderNumber"]').val('');
        p.find('[name="Priority"]').val('');
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

                    console.log($handle[0].document);

                    $handle[0].$('#ordernumFilterOrder').typeahead('val', ordernum);
                    $handle[0].$('#ordernumFilterOrder').trigger(press).blur();
                    $handle[0].focus();
                }, 5000);
               
            } catch (e) {
                MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.');
            };
        };
    });
    //Opens the pick batch manager modal
    $('#PickBatchManager').click(function () {
        if ($('#PickBatchID').val() != "") {
            $('#PickToteManModal').modal('show');
            $('#UseZone').prop('checked', false);
            $('#UseFilter').prop('checked', false);
            $('#BatchFilterTab').hide();
            $('#BatchZoneTab').hide();
            $('#FilterTab').hide();
            $('#ZoneTab').hide();
            PickToteOrdersTable.clear().draw();
            PickToteTransTable.clear().draw();

            if ($('#AutoPickTote').val() === "true") {
                $('#FillAllToteID').click();
            };
            //check which option to show on load
            if ($('#DefFilter').val() == "true") {
                $('#BatchFilterTab').click();
                $('#BatchFilterTab').show();
                $('#BatchZoneTab').hide();
                $('#UseFilter').prop('checked', true);
                $('#UseZone').prop('checked', false);
            };
            if ($('#DefZone').val() == "true") {
                $('#BatchZoneTab').click();
                $('#BatchZoneTab').show();
                SelectZones();
                $('#BatchFilterTab').hide();
                $('#UseFilter').prop('checked', false);
                $('#UseZone').prop('checked', true);
            };
        } else {
            MessageModal("Warning", "Batch ID cannot be empty when opening the pick batch manager");
        };

    });
    //proccsses the current tote setup
    $('#PickProcess').click(function () {
        //process the current pick bacth configuration
        var totes = $('[name="ToteID"]');
        var Pos = $('[name="Position"]');
        var Orders = $('[name="OrderNumber"]');
        var toteempty = true
        var conf = confirm("Process the current tote setup?");
        if (conf) {
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
                        //call process tote setup function
                        PickToteHub.server.processPickToteSetup(PositionList, ToteList, OrderList, $('#PickBatchID').val(), 0).done(function (mess) {
                            if (mess == "Error") {
                                MessageModal("Error", "An error has occurred processing the current tote setup")
                            } else {
                                var batchid = $('#PickBatchID').val();
                                var printData = {
                                    Positions: PositionList,
                                    ToteIDs: ToteList,
                                    OrderNums: OrderList,
                                    BatchID: $('#PickBatchID').val(),
                                    printDirect: pd
                                };
                                var autoPrintExecuted = $('#AutoPrintPickToteLabs').val() == "true";
                                if (autoPrintExecuted) {
                                    getLLPreviewOrPrint('/IM/PickToteSetup/printPrevPickToteLabel', printData, pd, 'label', "Tote Label Preview", function () {
                                        if ($('#AutoPrintOffCarPickList').val() == "true") {
                                            getLLPreviewOrPrint('/IM/PickToteSetup/printPrevOffCarPickList', printData, pd, 'report', "Off Caorusel Pick List", function () {

                                            });
                                        };
                                    });
                                } else if ($('#AutoPrintOffCarPickList').val() == "true") {
                                    getLLPreviewOrPrint('/IM/PickToteSetup/printPrevOffCarPickList', printData, pd, 'report', "Off Caorusel Pick List", function () {
                                        if ($('#AutoPrintCaseLabel').val() == "true") {
                                            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneCaseLabel/', {
                                                BatchID: batchid,
                                                printDirect: pd
                                            }, pd, 'label', "Case Label");
                                        };
                                    });
                                } else if ($('#AutoPrintCaseLabel').val() == "true") {
                                    getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneCaseLabel/', {
                                        BatchID: batchid,
                                        printDirect: pd
                                    }, pd, 'label', "Case Label");
                                };

                                //if ($('#AutoPrintOffCarPickList').val() == "true" && !autoPrintExecuted) {
                                //    getLLPreviewOrPrint('/IM/PickToteSetup/printPrevOffCarPickList', printData, pd, 'report', "Off Caorusel Pick List");
                                //};

                                $('#PrintPickBatchInput').append($('<option>', {
                                    value: $('#PickBatchID').val(),
                                    text: $('#PickBatchID').val()
                                }));

                                //refresh the counts to show how mnay transactions of each type are left
                                $('#RefreshOrderCounts').click();
                                $('#PickBatchContainer').html("");
                                AppendToteIDRows();
                                $('#PickBatchID').val('');
                            };
                        });
                    };
                };
            };
        };
    });
    //printing below
    $(document.body).on('click', '#PrintToteLabs', function () {
        //print
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
                getLLPreviewOrPrint('/IM/PickToteSetup/printPrevIMPickToteLabelButt/', {
                    Positions: PositionList,
                    ToteIDs: ToteList,
                    OrderNums: OrderList,
                    printDirect: pd
                }, pd, 'label', "Tote Label Preview");
            };
        };
    });

    $(document.body).on('click', '#PrintPickLabels', function () {
        //print
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
                    getLLPreviewOrPrint('/IM/PickToteSetup/printPrevIMPickItemLabel', {
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
        //print
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
                            getLLPreviewOrPrint('/IM/PickToteSetup/printPrevIMPickList', {
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
        //check if fields filled out for desired print button
        if (missing) {
            MessageModal("Warning", "Missing data from the desired print row")
        } else {
            getLLPreviewOrPrint('/IM/PickToteSetup/printPrevIMPickItemLabel', {
                Positions: PositionList,
                ToteIDs: ToteList,
                OrderNums: OrderList,
                BatchID: $('#PickBatchID').val(),
                printDirect: pd
            }, pd, 'label', "Off Carousel Pick List");
        };
    });

    //printing below
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
            getLLPreviewOrPrint('/IM/PickToteSetup/printPrevIMPickToteLabelButt/', {
                Positions: PositionList,
                ToteIDs: ToteList,
                OrderNums: OrderList,
                printDirect: pd
            }, pd, 'label', "Tote Label Preview");
        };

    });

    $('#PrintBatchLocLabel').click(function () {
        if ($('#PrintPickBatchInput').val() != "") {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchToteLabel/', {
                BatchID: $('#PrintPickBatchInput').val(),
                printDirect: pd
            }, pd, 'label', "Batch Tote Label");
        } else {
            alert("Please select a Batch ID to print");
        };
    });

    $('#PrintBatchPickLabel').click(function () {
        if ($('#PrintPickBatchInput').val() != "") {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchItemLabel/', {
                BatchID: $('#PrintPickBatchInput').val(),
                printDirect: pd
            }, pd, 'label', "Batch Item Label");
        } else {
            alert("Please select a Batch ID to print");
        };
    });

    $('#PrintBatchPickList').click(function () {
        if ($('#PrintPickBatchInput').val() != "") {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevIMPickBatchList/', {
                BatchID: $('#PrintPickBatchInput').val(),
                printDirect: pd
            }, pd, 'label', "Batch Pick List");
        } else {
            alert("Please select a Batch ID to print");
        };
    });

    $('#PrintCaseLabel').click(function () {
        if ($('#PrintPickBatchInput').val() != "") {
            getLLPreviewOrPrint('/IM/PickToteSetup/PrintPrevInZoneCaseLabel/', {
                BatchID: $('#PrintPickBatchInput').val(),
                printDirect: pd
            }, pd, 'label', "Case Label");

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
                            '<input type="text" class="form-control" name="OrderNumber" placeholder="Order Number" />' +
                        '</div>' +
                        '<div class="col-md-1">' +
                            '<input type="text" class="form-control" name="Priority" readonly="readonly"  />' +
                        '</div>' +
                        '<div class="col-md-4">' +
                            '<button type="button" data-toggle="tooltip" data-original-title="Clear Tote" data-placement="left" class="btn btn-danger" name="Clear"><span class="glyphicon glyphicon-remove"></span></button> ' +
                            '<div style="display:inline-block" class="dropdown"><button id="goTo" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button><ul class="dropdown-menu" role="menu"><li><a name="totelabel" class="Print-Label">Print Tote Label</a></li><li><a name="ItemLabels" class="Print-Label">Print Pick Labels</a></li></ul></div> ' +
                            '<button type="button" class="btn btn-primary" name="ViewOrder">View Order</button> ' +
                            '<button type="button" class="btn btn-primary" name="NextToteID">Next Tote ID</button> ' +
                        '</div>' +
                    '</div>')
    };
};
//gets a new batch setup
function NewBatchConf() {
    var conf = confirm("Create a new batch?")

    if (conf) {
        var conf2 = confirm("Press OK to create a new Tote Setup. Press Cancel to keep the current Tote Setup");
        if (!conf2) {
            $('#PickBatchID').val("");
            return true;
        } else {
            $('#PickBatchContainer').html("");
            AppendToteIDRows();
            $('#PickBatchID').val("");
            return false;
        };
    } else {
        return "not done";
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

//auto fills order numbers
function AutoFillOrders() {
    var Orders = $('[name="OrderNumber"]');
    var type = $('[name="pickzone"]:checked').val();
    var counter;
    PickToteHub.server.fillOrderNums(type).done(function (data) {
        counter = data.length
        if (data.length != 0) {
            $.each(Orders, function (i, v) {
                if (counter > 0) {
                    //decrements the counter in order to get the desired order of the order numbers
                    this.value = data[data.length - counter];
                    counter = counter - 1;
                } else {
                    this.value = ""
                };
            });
        } else {
            MessageModal("No Order Numbers", "No order numbers exist for the selected pick zone")
        };
    });
};