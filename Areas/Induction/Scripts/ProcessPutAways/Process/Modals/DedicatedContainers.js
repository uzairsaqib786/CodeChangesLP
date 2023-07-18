// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var locDT;
var otherlocDT;
$(document).ready(function () {
    // top table for dedicated containers
    locDT = $('#loc_table').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [
            [
                0, 'asc'
            ]
        ],
        "createdRow": function (row, data, index) {
            $('#container_locnum').val(data[0]);
            $('#forceBulk').html('Force to ' + (data[1].toLowerCase() == 'no' ? 'Carousel' : 'Bulk'));
        },
        "ajax": {
            "url": "/IM/ProcessPutAways/GetContainerTables",
            "data": function (d) {
                d.invMapID = $('#InvMapID').val();
                d.itemNumber = '';
                d.location = '';
                d.top = true;
            }
        },
        pageLength: 5
    }).on('draw', function () {
        $('#container_item').val($('#ItemNumber').val());
    });
    $('#loc_table').wrap('<div style="max-height:' + $(window).height() * 0.4 + ';overflow-y:scroll;"></div>');

    // bottom table for dedicated containers
    otherlocDT = $('#other_loc_table').DataTable({
        "dom": 'trip',
        "processing": true,
        "serverSide": true,
        'order': [
            [
                0, 'asc'
            ]
        ],
        "createdRow": function (row, data, index) {

        },
        "ajax": {
            "url": "/IM/ProcessPutAways/GetContainerTables",
            "data": function (d) {
                d.invMapID = 0;
                d.itemNumber = $('#container_item').val();
                d.location = $('#container_locnum').val();
                d.top = false;
            }
        },
        pageLength: 5
    }).on('draw', function () {

    });
    $('#other_loc_table').wrap('<div style="max-height:' + $(window).height() * 0.4 + ';overflow-y:scroll;"></div>');

    // forces location placement to bulk or carousel
    $('#forceBulk').click(function () {
        var $this = $(this);
        if ($this.html().trim().toLowerCase() == 'force to bulk') {
            $('#CVel').val('XXX');
        } else {
            $('#BVel').val('XXX');
        };
    });

    // marks the container as full
    $('#forceFull').click(function () {
        if (confirm('Click OK to set the current container as being full.')) {
            batchHub.server.markContainerFull(location).done(function (marked) {
                if (!marked) {
                    MessageModal('Error', 'There was an error while attempting to update the container as being full.  Check the error log for details.');
                };
            });
        };
    });

    // dedicates a location to a particular item (and every other location with the same reel location number from vinventorymap)
    $('#dedicateLocs').click(function () {
        if (confirm('Click OK to dedicate this location to the selected item.')) {
            var inv = $('#InvMapID').val();
            batchHub.server.checkBadContainer(inv).done(function (container) {
                if (container.BadContainer) {
                    MessageModal('Error', 'Reel Tracking Dedicate Container not allowed.  Please immediately forward container location: ' + (container.ReelLocNum) + ' to your supervisor for correction.');
                    $('#BadContainer').val('true');
                } else {
                    $('#BadContainer').val('false');
                    batchHub.server.dedicateLocation(inv, $('#ItemNumber').val(), $('#Warehouse').val()).done(function (dedicated) {
                        $('#IsDedicated').val(String(dedicated).toLowerCase());
                        if (!dedicated) {
                            MessageModal('Error', 'There was an error during the attempt to dedicate the location specified.  Check the error log for details.');
                        } else {
                            $('#dedicated_modal').modal('hide');
                        };
                    });
                };
            });
        };
    });

    // opens the dedicated modal for a reel transaction
    $('#ContainerLocations').click(function () {
        var inv = $('#InvMapID').val().trim();
        if (inv != '' && parseInt(inv) != 0) {
            launchDedicatedModal();
        } else {
            MessageModal('Error', 'You must assign a location to this transaction before viewing the rest of the container.');
        };
    });
});

// opens the dedicate location modal
function launchDedicatedModal() {
    batchHub.server.getLocationName($('#InvMapID').val()).done(function (locname) {
        $('#dedicatedItem').val($('#ItemNumber').val());
        $('#dedicatedLoc').val(locname);
        $('#dedicatedWhse').val($('#Warehouse').val());
        $('#dedicatedVel').val($('#Velocity').val());
        $('#dedicatedCell').val($('#CellSize').val());
        locDT.draw().one('draw.dt', function () {
            otherlocDT.draw();
        });
        $('#dedicated_modal').modal('show');
    });
}