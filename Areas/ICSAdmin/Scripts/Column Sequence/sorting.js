// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022



// Handles default column sequence rearrangement
jQuery(function ($) {

    // draggable definitions here
    var panelList = $('#draggablePanelList, #draggableList, #defaultList');
    // define the sortable class for those who use it.
    panelList.sortable({
        items: ':not(.static)',
        // prevents on click event from being triggered in firefox
        helper: 'clone',
        connectWith: ".together"
    }).disableSelection();


    // click events added
    // move from either of the unused default lists to the default list (right) which can then be saved
    $('#draggablePanelList, #draggableList').on("click", "li:not(.static)", function () {
        unsaved = true;
        var $this = $(this);
        $('#defaultList').append("<li class='btn btn-primary btn-sm'>" + $this.text() + "</li>");
        $this.remove();
        $('#defaultList, #draggablePanelList, #draggableList').trigger("sortupdate");
    });

    // take off the defined defaults list and return it to the unused (left side or middle)
    $('#defaultList').on('click', 'li:not(.static)', function () {
        unsaved = true;
        var $this = $(this);
        
        var leftList = $('#draggablePanelList li').length;
        var rightList = $('#draggableList li').length;

        // append it to the list with fewer elements on the unused defaults side (left/middle)
        if (leftList < rightList) {
            $('#draggablePanelList').append("<li class='btn btn-primary btn-sm'>" + $this.text() + "</li>");
        }
        else {
            $('#draggableList').append("<li class='btn btn-primary btn-sm'>" + $this.text() + "</li>");
        };
        $this.remove();
        $('#defaultList, #draggablePanelList, #draggableList').trigger("sortupdate");
    });

    $('#autofillColumns').click(function () {
        unsaved = true;
        var $this;
        $('#draggableList li:not(.static), #draggablePanelList li:not(.static)').each(function () {
            $this = $(this);
            $('#defaultList').append("<li class='btn btn-primary btn-sm'>" + $this.text() + "</li>");
            $this.remove();
        });
        
        $('#defaultList, #draggablePanelList, #draggableList').trigger("sortupdate");
    });
});

