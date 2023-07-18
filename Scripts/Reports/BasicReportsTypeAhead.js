// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var filter1, filter2, filter3, filter4, filter5, filter6;

function generateFilterTypeahead(filterName,ExpSelect,ColumnSelect) {
    window[filterName] = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 20,
        remote: {
            // call function in controller getLocations
            url: ('/Typeahead/getReportFilterTypeahead?query='),
            replace: function (url, uriEncodedQuery) {
                return url + $(ExpSelect).val() + '&report=' + $('#SelectedReport').val() + '&column=' + $(ColumnSelect).val();
            },
            filter: function (list) {
                return $.map(list, function (dataObj) {
                    return { Value: dataObj };
                });
            },
            cache: false
        }
    });

    window[filterName].initialize();

    $(ExpSelect).typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
        name: filterName,
        displayKey: 'Value',
        source: window[filterName].ttAdapter()
    }).on('typeahead:selected', function (obj, data, name) {
        saveValues()
    }).on('typeahead:opened', function () {
        //$('.tt-dropdown-menu').css('width', "600px");
    });
}

$(document).ready(function () {
    generateFilterTypeahead('filter1', '#Exp11', '#Field1');
    generateFilterTypeahead('filter2', '#Exp12', '#Field2');
    generateFilterTypeahead('filter3', '#Exp13', '#Field3');
    generateFilterTypeahead('filter4', '#Exp14', '#Field4');
    generateFilterTypeahead('filter5', '#Exp15', '#Field5');
    generateFilterTypeahead('filter6', '#Exp16', '#Field6');
    $('#Exp11, #Exp12, #Exp13, #Exp14, #Exp15, #Exp16').on('input', function () {
        var id = $(this).attr('id');
        var instance = id.substr(id.indexOf('1') + 1, id.length);
        window['filter' + instance].clearRemoteCache();
    })
})