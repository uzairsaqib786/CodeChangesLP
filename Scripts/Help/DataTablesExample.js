// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {
    var states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana', 'Maine',
        'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota',
        'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'];
    var dataSet = new Array();
    for (var x = 1; x <= 50; x++) {
        dataSet.push([x, 'Item from supplier in ' + states[x - 1], states[x - 1].substring(0, 3).toUpperCase() + Math.floor(Math.random() * (50000) + 1000)]);
    };

    var dt = $('#DataTableExample').dataTable({
        data: dataSet,
        paging: true,
        processing: true,
        dom: 'trip'
    });

    $('#DataTableExample').wrap('<div style="overflow-x:scroll;"></div>');

    $('#DataTableExample tbody').on('click', 'tr', function () {
        var active = $('#DataTableExample tbody tr.active');
        var $this = $(this);
        if (active.length > 0 && $this.hasClass('active')) {
            active.removeClass('active');
            $('#FakePrint, #FakeDelete').attr('disabled', 'disabled');
        } else {
            active.removeClass('active');
            $this.addClass('active');
            $('#FakePrint, #FakeDelete').removeAttr('disabled');
        };
    });

    $('#FakePrint').click(function () {
        alert('If this table was intended to be used in PickPro the button you just clicked would have printed the report related to the data contained in the table.');
    });

    $('#FakeDelete').click(function () {
        alert('If this table was intended to be used in PickPro the button you just clicked would have prompted you to delete the selected row from the database.');
    });
});