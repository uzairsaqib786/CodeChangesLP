// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var MoveFromTable;
var MoveToTable;
var MaxMoveQty;
var dedicateMoveTo = false, undedicateMoveFrom = false;
var MoveItemsCols = new Array();
$(document).ready(function () {
    $('.toggles').toggles({
        'height': 25,
        'width': 60
    });

    $.each($('#MoveItemsCol').children(), function (index, element) {
        MoveItemsCols.push($(element).attr('value'));
    });


    MoveFromTable = InitMoveTable("#MoveFromTable")
    MoveToTable = InitMoveTable("#MoveToTable")
    InitClickableTable("#MoveFromTable")
    InitClickableTable("#MoveToTable")


    var itemTypeahead = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getitem
            url: ('/Typeahead/getItem?query=%QUERY'),
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return dataObj;
                });
            },
            cache: false
        }
    });
    itemTypeahead.initialize();

    $('#ItemNumber').typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: "item",
        displayKey: 'ItemNumber',
        source: itemTypeahead.ttAdapter(),
        templates: {
            header: '<p class="typeahead-header" style="width:50%;">Item Number</p><p class="typeahead-header" style="width:50%;">Description</p>',
            suggestion: Handlebars.compile('<p class="typeahead-row " style="width:50%;">{{ItemNumber}}</p><p class="typeahead-row " style="width:50%;">{{Description}}</p>')
        }
    }).on('typeahead:opened', function () {
        var $this = $(this);
        $this.siblings('.tt-dropdown-menu').css('width', '400px');
    }).on('typeahead:selected', function (obj, data, name) {
        MoveFromTable.draw();
    });

    $('#ItemNumber').focus().click();
})

var typingtime;
var updateInterval;
var newTime;

//When Item Selector is typed in, redraw tables and clear data to requery
$('#ItemNumber').on('input', function () {
    if ($(this).val() == '') {
        $(this).typeahead('val', '');
    }
    newTime = new Date().getTime();
    if (newTime - typingtime > 500) {
        MoveFromTable.draw();
        SelectedRow(false, '#MoveFromTable');
        SelectedRow(false, '#MoveToTable');
    }
    else {
        clearTimeout(updateInterval);
        updateInterval = setTimeout(function () {
            MoveFromTable.draw();
            SelectedRow(false, '#MoveFromTable');
            SelectedRow(false, '#MoveToTable');
        }, 500);
    };
    typingtime = newTime;
    
   
})

//When MoveFrom table is redrawn, clear all Location Information
$('#MoveFromTable').on('draw.dt', function () {
    SelectedRow(false, '#MoveFromTable');
    SelectedRow(false, '#MoveToTable');
})

$('#ViewAllMoveTo').change(function () {
    SelectedRow(false, '#MoveToTable');
    MoveToTable.draw();
})

$('#ValidateMove').click(function () {
    var MoveQty = $('#MoveQty').val();
    dedicateMoveTo = false;
    undedicateMoveFrom = false;
    if (MoveQty == '' || MoveQty <= 0) {
        ShowMoveModal("ZeroQty");
        return
    } else if (MoveQty > MaxMoveQty) {
        ShowMoveModal("MaxMove")
        return
    }
    var MoveFromDedicated = $('#MoveFrom_Dedicated').text(), MoveToDedicated = $('#MoveTo_Dedicated').val()

    if ($('#DedicateToggle').data('toggles').active) {
        ShowMoveModal("Dedicate");
        return;
    }
    if (MoveFromDedicated == 'Dedicated') {
        ShowMoveModal("Un-Dedicate");
        return;
    }
    callCreateMoveTrans();
})
//Handles clearing or setting data when a row is selected or deselected from either table
function SelectedRow(isSelected, selector) {
    var inputSelector = selector.substring(0, selector.indexOf("Table"))
    var row = $(selector + ' tbody tr.active')

    if (isSelected) {
        var CurrentQty = row.find(':nth-child(6)').html();
        var MaxQty = row.find(':nth-child(20)').html();
        var QtyAllocPut = row.find(':nth-child(8)').html();
        var QtyAllocPick = row.find(':nth-child(7)').html();
        var FillQty = CurrentQty - MaxQty - QtyAllocPut;
        var Dedicated = row.find(':nth-child(26)').html();
        var Warehouse = row.find(':nth-child(1)').html();
        var Location = row.find(':nth-child(2)').html();
        var ItemNumber = row.find(':nth-child(4)').html();
        var Description = row.find(':nth-child(5)').html();
        var CellSize = row.find(':nth-child(14)').html();
        var LotNum = row.find(':nth-child(15)').html();
        var SerialNum = row.find(':nth-child(16)').html();

        if (FillQty < 0) {
            FillQty = 0
        }
        //Only Happens if Row was selected on From Table
        if (inputSelector == '#MoveFrom') {
            $('#MoveFromNotSelected').hide();
            $('#MoveToTable').addClass("table-hover")
            $('#MovePriority').val("0").removeAttr("disabled");
            MaxMoveQty = CurrentQty - QtyAllocPick
            if (MaxMoveQty <= 0) {
                ShowMoveModal("MaxAlloc")
                $(selector + ' tbody tr.active').removeClass('active');
                SelectedRow(false, selector);
                return;
            } else if (QtyAllocPick > 0) {
                ShowMoveModal("MoveCap")
                $('#MoveQty').val(MaxMoveQty).removeAttr("disabled");
            }
            else {
                $('#MoveQty').val(MaxMoveQty).removeAttr("disabled");
            }
        }
        if (inputSelector == '#MoveTo') {
            $('#ValidateMove').removeAttr('disabled');
        }
        $(inputSelector + '_Warehouse').val(Warehouse);
        $(inputSelector + '_Location').val(Location);
        $(inputSelector + '_ItemNumber').val(ItemNumber);
        $(inputSelector + '_Description').val(Description);
        $(inputSelector + '_Qty').val(CurrentQty);
        $(inputSelector + '_CellSize').val(CellSize);
        $(inputSelector + '_LotNumber').val(LotNum);
        $(inputSelector + '_SerialNum').val(SerialNum);
        $(inputSelector + '_FillQty').val(FillQty);

        if (Dedicated == 'True') {
            $(inputSelector + '_Dedicated').text("Dedicated").addClass("text-success").removeClass("text-danger");
            $('#DedicateToggle').data('toggles').setValue(true);
        } else {
            $(inputSelector + '_Dedicated').text("Not Dedicated").addClass("text-danger").removeClass("text-success");
            $('#DedicateToggle').data('toggles').setValue(false);
        }
        if (inputSelector == '#MoveFrom') { MoveToTable.draw() };
    } else {
        if (inputSelector == '#MoveFrom') {
            $('#MoveFromNotSelected').show();
            $('#MoveToTable').removeClass("table-hover");
            $('#MovePriority').val("0").attr('disabled', 'disabled');
            MoveToTable.draw();
            SelectedRow(false, '#MoveToTable');
            MaxMoveQty = 0;
            $('#MoveQty').val(MaxMoveQty).attr("disabled", "disabled");
        }
        if (inputSelector == '#MoveTo') {
            $('#ValidateMove').attr('disabled', 'disabled');
        }
        $(inputSelector + '_Warehouse').val("");
        $(inputSelector + '_Location').val("");
        $(inputSelector + '_ItemNumber').val("");
        $(inputSelector + '_Description').val("");
        $(inputSelector + '_Qty').val("");
        $(inputSelector + '_CellSize').val("");
        $(inputSelector + '_Dedicated').text("").removeClass("text-success").removeClass("text-danger");
        $(inputSelector + '_FillQty').val("");
        $(inputSelector + '_LotNumber').val("");
        $(inputSelector + '_SerialNum').val("");
    }
}


//Initalizes each table to be clickabled
function InitClickableTable(selector) {
    $(selector + ' tbody').on('click', 'tr', function () {
        var $this = $(this);
        if (selector == '#MoveToTable' && $('#MoveFromTable tbody tr.active').length == 0) {
            return;
        } else if (selector == '#MoveToTable' && $('#MoveToTable_processing').css('display') == 'block') {
            return;
        }
        if (!$this.hasClass('active')) {
            $(selector + ' tbody tr.active').removeClass('active');
            $this.addClass('active');
            SelectedRow(true, selector)
        } else {
            $this.removeClass('active');
            SelectedRow(false, selector)
        };
    });
}

//Initializes Table Plugin for each table
function InitMoveTable(selector) {
    var holdTable = $(selector).DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [
            [
                0, 'asc'
            ]
        ],
        "lengthMenu": [15],
        "createdRow": function (row, data, index) {
            //If Location has Allocated Quantity, mark it
            if (parseInt(data[6]) > 0) {
                $(row).addClass("danger");
            };
        },
        "ajax": {
            //Function that grabs Table Data
            "url": "/MoveItems/MoveItemsTable",
            "data": function (d) {
                var row = $('#MoveFromTable tbody tr.active')
                if (selector.indexOf("To") > 0) {
                    d.searchString = row.find(':nth-child(4)').html()
                    d.InvMapID = row.find(':nth-child(29)').html()
                    //filter for move to
                    d.filter = (MoveToItemsFilterMen == "" ? "" : MoveToItemsFilterMen.getFilterString());
                    if ($('#ViewAllMoveTo').prop('checked') == true || row.length == 0) {
                        d.OQA = 'All'
                    } else { d.OQA = 'NOA' }
                } else {
                    d.searchString = $('#ItemNumber').val();
                    d.InvMapID = -1
                    d.OQA = 'NOA'
                    //filter for move from
                    d.filter = (MoveFromItemsFilterMen == "" ? "" : MoveFromItemsFilterMen.getFilterString());
                }
                d.nameStamp = selector.substring(1, selector.indexOf("Table"))
                d.Cellsize = $('#MoveFrom_CellSize').val()
                d.Warehouse = $('#MoveFrom_Warehouse').val()
                

            }
        },
        "language": {
            "paginate": {
                "next": "<u>N</u>ext",
                "previous": "<u>P</u>revious"
            }
        }
    })
    $(selector).wrap('<div style="overflow-x:scroll;"></div>');
    return holdTable;
}