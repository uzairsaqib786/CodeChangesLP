// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California',
      'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii',
      'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana',
      'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota',
      'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
      'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota',
      'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island',
      'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
      'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
];

$(document).ready(function () {

    var dropdown = '';
    $.each(states, function (i, elem) {
        dropdown += '<option>' + elem + '</option>';
    });
    $('#TraditionalDropDown').html(dropdown);

    var item = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        local: $.map(states, function (state) { return { value: state }; }),
    });
    item.initialize();

    var display = '<p class="typeahead-row " style="width:50%;">{{ItemNumber}}</p><p class="typeahead-row " style="width:50%;">{{Description}}</p>';

    $('#TypeaheadExample').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: "item",
        displayKey: 'value',
        source: item.ttAdapter()
    }).on('typeahead:opened', function () {
        $('.tt-dropdown-menu').css('width', "600px");
    });

    
});