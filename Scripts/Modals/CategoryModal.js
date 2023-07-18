// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

// initializes and launches the category modal
var trigger_category_modal;

// posts a category modal alert
var postCategoryAlert = function (message) {
    $('#category_alerts').html('<div class="alert alert-warning alert-custom" role="alert"><strong>Conflict:</strong> ' + message + '</div>');
};

// deletes all category modal alerts
var deleteCategoryAlert = function () {
    $('#category_alerts').html('');
};

$(document).ready(function () {

    // handles printing a categories report and hides the modal after the print is sent
    $('#print_categories').click(function () {
        //reportsHub.server.printCategoriesReport().done(function () {
        //    $('#category_modal').modal('hide');
        //});

        title = 'Categories Report';
        getLLPreviewOrPrint('/CustomReports/printCategoriesReport', {

        }, true,'report', title);
    });

    // contains the sending input for setting values
    var categorysender;

    trigger_category_modal = function (category, subcategory) {
        var appendstring = '';
        globalHubConn.server.getCategories().done(function (categoryObj) {
            for (var x = 0; x < categoryObj.categories.length; x++) {
                appendstring += '<div class="category_container" style="padding-top:10px;"><div class="row">' +
                    '<div class="col-md-5" name="category_input"><input maxlength="50" type="text" class="form-control input-category" value="' + categoryObj.categories[x] + '" name="' + categoryObj.categories[x] + '"></div>' +
                    '<div class="col-md-4" name="subcategory_input"><input maxlength="50" type="text" class="form-control input-subcategory" value="' + categoryObj.subcategories[x] + '" name="' + categoryObj.subcategories[x] + '"></div>' +
                    '<div class="col-md-1" name="remove"><button type="button" data-toggle="tooltip" data-placement="top" title="Remove" class="btn btn-danger remove-category" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' + 
                    '<div class="col-md-1" name="set"><button data-dismiss="modal" type="button" data-toggle="tooltip" data-placement="top" title="Set" class="btn btn-primary set-category" data-original-title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' + 
                    '<div class="col-md-1" name="save"><button disabled="disabled" type="button" data-toggle="tooltip" data-placement="top" title="Save" class="btn btn-primary save-category" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' + 
                    '</div></div>';
            };
            $('#category_container').html(appendstring);
            $('#category_modal').modal('show');
        });
    };

    // handles dismissing the categories modal
    $(document.body).on('click', '#category_dismiss', function () {
        $('#category_add').removeAttr('disabled');
    });

    // launches the category modal
    $(document.body).on('click', '.category-modal, .subcategory-modal', function () {
        var $this = $(this);
        if ($this.hasClass('glyphicon')) {
            categorysender = $this.siblings('input');
        } else {
            categorysender = $this;
        };
        trigger_category_modal();
    });

    // sets the sender's value to the selected value
    $(document.body).on('click', '.set-category', function () {
        var $this = $(this);
        var category = $this.parent().siblings('[name="category_input"]').children().val();
        var subcategory = $this.parent().siblings('[name="subcategory_input"]').children().val();

        if ($('#Category').length > 0) {
            $('#Category').val(category);
            $('#Sub-Category').val(subcategory);
        } else {
            var split = categorysender.attr('name').split(" ");
            if (split[0] == 'Category') {
                categorysender.val(category);
                $('input[name="Sub Category ' + split[1] + '"]').val(subcategory);
            } else {
                categorysender.val(subcategory);
                $('input[name="Category ' + split[2] + '"]').val(category);
            };
            categorysender.trigger('input');
        };
    });

    // adds a new category
    $(document.body).on('click', '#category_add', function () {
        $(this).attr('disabled', 'disabled').tooltip('hide');
        var appendstring = '<div class="category_container" style="padding-top:10px;"><div class="row">' +
                    '<div class="col-md-5" name="category_input"><input maxlength="50" type="text" class="form-control input-category new-category"></div>' +
                    '<div class="col-md-4" name="subcategory_input"><input maxlength="50" type="text" class="form-control input-subcategory new-category"></div>' +
                    '<div class="col-md-1" name="remove"><button type="button" data-toggle="tooltip" data-placement="top" title="Remove" class="btn btn-danger remove-category new-category" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button></div>' +
                    '<div class="col-md-1" name="set"><button disabled="disabled" data-dismiss="modal" type="button" data-toggle="tooltip" data-placement="top" title="Set" class="btn btn-primary set-category new-category" data-original-title="Set as value"><span class="glyphicon glyphicon-edit"></span></button></div>' +
                    '<div class="col-md-1" name="save"><button disabled="disabled" type="button" data-toggle="tooltip" data-placement="top" title="Save" class="btn btn-primary save-category new-category" data-original-title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></button></div>' +
                    '</div></div>';
        $('#category_container').append(appendstring);
        $('.new-category').focus();
    });

    // handles enabling/disabling save for categories/subs
    $(document.body).on('input', '.input-category, .input-subcategory', function () {
        deleteCategoryAlert();
        var enable = true;
        var $this = $(this);
        var cat = $this.parent().parent().children('[name="category_input"]').children();
        var sub = $this.parent().parent().children('[name="subcategory_input"]').children();
        var save = $this.parent().siblings('[name="save"]').children();
        var set = $this.parent().siblings('[name="set"]').children();
        var remove = $this.parent().siblings('[name="remove"]').children();

        if (!$this.hasClass('new-category')) {
            if (cat.attr('name').toLowerCase() == cat.val().toLowerCase() && sub.attr('name').toLowerCase() == sub.val().toLowerCase() || cat.val() == '') {
                save.attr('disabled', 'disabled');
                set.removeAttr('disabled');
                remove.removeAttr('disabled');
                return false;
            };
        };

        cat.addClass('input-edit');
        sub.addClass('input-edit');

        var c;
        var s;
        var $t;
        $('.input-category:not(.input-edit)').each(function () {
            $t = $(this);
            c = $t;
            s = $t.parent().siblings('[name="subcategory_input"]').children();
            
            if (cat.val().toLowerCase() == c.val().toLowerCase() || cat.val().toLowerCase() == c.attr('name').toLowerCase()) {
                if (sub.val().toLowerCase() == s.val().toLowerCase() || sub.val().toLowerCase() == s.attr('name').toLowerCase()) {
                    postCategoryAlert('Category cannot be saved.  Category matches another entry.  Save any pending changes before attempting to save this entry.');
                    save.attr('disabled', 'disabled');
                    enable = false;
                };
            };
        });
        if (enable) {
            set.attr('disabled', 'disabled');
            if (!$this.hasClass('new-category')) {
                remove.attr('disabled', 'disabled');
            };
            save.removeAttr('disabled');
        };

        cat.removeClass('input-edit');
        sub.removeClass('input-edit');
    });

    // deletes a category/subcategory combination
    $(document.body).on('click', '.remove-category', function () {
        var $this = $(this);
        $this.attr('disabled', 'disabled');
        var container = $this.parent().parent().parent();

        var category = $this.parent().siblings('[name="category_input"]').children().attr('name');
        var subcategory = $this.parent().siblings('[name="subcategory_input"]').children().attr('name');

        if ($this.hasClass('new-category')) {
            container.remove();
            $('#category_add').removeAttr('disabled');
        } else {
            var result = confirm('Click OK to delete category ' + category + ' with sub-category ' + subcategory);
            if (result) {
                globalHubConn.server.deleteCategory(category, subcategory).done(function (success) {
                    if (success) {
                        container.remove();
                    } else {
                        $this.removeAttr('disabled');
                        alert('Delete failed!  Please try again.');
                    };
                });
            } else {
                $this.removeAttr('disabled');
            };
        };
    });

    // handles saving a category/subcategory combination
    $(document.body).on('click', '.save-category', function () {
        var $this = $(this);
        var category = $this.parent().siblings('[name="category_input"]').children();
        var subcategory = $this.parent().siblings('[name="subcategory_input"]').children();
        var set = $this.parent().siblings('[name="set"]').children();
        if ($this.hasClass('new-category')) {
            globalHubConn.server.saveCategory(category.val(), subcategory.val(), '', '').done(function (result) {
                if (result) {
                    category.attr('name', category.val());
                    subcategory.attr('name', subcategory.val());
                    $('.new-category').removeClass('new-category');
                    $('#category_add').removeAttr('disabled');
                    set.removeAttr('disabled');
                    $this.attr('disabled', 'disabled');
                } else {
                    alert('Category not saved!  Please try again.');
                };
            });
        } else {
            globalHubConn.server.saveCategory(category.val(), subcategory.val(), category.attr('name'), subcategory.attr('name')).done(function (result) {
                if (result) {
                    category.attr('name', category.val());
                    subcategory.attr('name', subcategory.val());
                    set.removeAttr('disabled');
                    $this.attr('disabled', 'disabled');
                } else {
                    alert('Category not saved!  Please try again.');
                };
            });
        };
    });
});