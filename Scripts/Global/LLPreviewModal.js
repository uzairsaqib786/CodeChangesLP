// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

$(document).ready(function () {

    $('#Message_Modal').on('hidden.bs.modal', function () {

    })

    $('#Message_Modal').on('shown.bs.modal', function (e) {
        $('#Message_submit').focus();
        e.stopImmediatePropagation();
    })

    $('#Message_submit').click(function () {

    });
});

reportsHub.client.exportFinished = function (fileName, desiredFormat, fExists) {
    if ($('#ExportStatus').length > 0) {
        $('#ExportStatus').parent().hide();
    };
    var props = [fileName, desiredFormat, fExists];
    if (fExists) {
        if (desiredFormat != "preview") {
           window.location.href = '/api/LLExport/GetFile?fileName=' + fileName + '&WSID=' + $('#WSID').val();
        };
    } else {
        MessageModal('Warning', 'An error may have occurred during the attempted export or preview.  No file was created either because the provided SQL query/Stored Procedure contained no data or there was an error.  Check the error log and verify that the SQL query returns records for the filters applied.');
    };
};

var generatedPreview;

/*
    Parameters:
        baseURL: /controller/function, same as ajax
        data: {properties: values}, same as ajax data attribute
        printDirect: print or preview (true for print physical copy)
        reportType: Either 'label' or 'report', to verify printer has been assigned for specified type
    Optional parameters:
        title: window or messagemodal title
        closeCallback: function to call after the preview window has been closed
        callbackArgs: arguments to give to the closeCallback function
    Returns: $handle - reference to the window we opened (or null if there was no window created)
*/
function getLLPreviewOrPrint(baseURL, data, printDirect, reportType, title, closeCallback, callbackArgs) {
    if (data.hasOwnProperty('popup')) {
        delete data['popup'];
    };

    var url = baseURL + '?popup=True&';
    for (var prop in data) {
        if (data.hasOwnProperty(prop)) {
            url += (prop + '=' + data[prop] + '&');
        };
    };
    url = url.substr(0, url.length - 1);
    if (printDirect) {
        if ($('#PrintServiceOnline').text() == "Offline" || $('#Status').text() == 'Offline') {
            alert("The print service is currently offline");
            return;
        }
        else if ($('#ReportPrinter').val() == 'No Printer' && reportType == 'report') {
            alert("You must have a Printer Assigned to print Reports ");
            return;
        }
        else if ($('#LabelPrinter').val() == 'No Printer' && reportType == 'label') {
            alert("You must have a Printer Assigned to print Labels");
            return;
        }
        else {
            $(this).addClass("Printing");
            addPrintMessage("Printing Started", "print-start")
        };
    }
    if (printDirect) {
        $.ajax({
            url: url,
            type: "POST",
            data: data,
            success: function (printed) {
                if (printDirect) {
                    if (printed) {
                        if (typeof closeCallback == 'function') {
                            MessageModal('Alert: ' + title, 'Print Complete.', closeCallback(callbackArgs));
                        } else {
                            MessageModal('Alert: ' + title, 'Print Complete.');
                        };
                    } else {
                        MessageModal('Warning: ' + title, 'An error occurred.  The requested print action may not have completed correctly.');
                    };
                } else {
                    if (printed == false) {
                        MessageModal('Error', 'There was an error during the export/preview process.');
                    } else {
                        generatedPreview = printed;
                    };
                };
            },
            error: function (xhr, ajaxOptions, thrownError) {
                MessageModal('Warning ' + title, 'An error occurred.  The requested print action may not have completed correctly.');
            }
        });
    } else {
        var $handle = $(window.open(url, '_blank', 'width=' + screen.width + ',height=' + screen.height + ',toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'));
        try {
            $handle[0].focus();
        } catch (e) {
            console.log('Exception: ', e)
            MessageModal('Error', 'Pop-ups appear to be blocked for this workstation.  Please check your settings and ensure that the browser allows pop-ups for this site.');
        };
        if (typeof closeCallback == 'function') {

            $handle.one('load', function () {
                console.log($handle)
                $handle.one('unload', function () {
                    console.log($handle)
                    closeCallback(callbackArgs);
                });
            });
        };
        return $handle;
    }
    
    return null;
};