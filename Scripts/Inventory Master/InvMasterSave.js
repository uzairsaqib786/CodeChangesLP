// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

/*
    Handles saving all item information.  This excludes Scan Codes, Reel Tracking and Kit Items partial tabs.
*/

$(document).ready(function () {
    $('.inv-edit').on('input', function () {
        $('#stockCode').attr('disabled', 'disabled');
        document.getElementById('saveButton').disabled = false;
    });
    $('select.inv-edit').on('change', function () {
        $('#stockCode').attr('disabled', 'disabled');
        document.getElementById('saveButton').disabled = false;
    })
    $(document.body).on('click', '.inv-edit:checkbox', function () {
        document.getElementById('stockCode').disabled = true;
        document.getElementById('saveButton').disabled = false;
    });

    $(document.body).on('click', '#saveButton', function () {
        // updateInvMastAll
        var item = document.getElementById('UpdateItemNum').value, supplierItemID = document.getElementById('supplierID').value;
        var description = document.getElementById('itemDescription').value, category = document.getElementById('category').value;
        var subcategory = document.getElementById('subCategory').value, UoM = document.getElementById('UoM').value, repPoint = document.getElementById('replenishPoint').value;
        var repLevel = document.getElementById('replenishLevel').value, reorderPoint = document.getElementById('reorderPoint').value;
        var KanBanPoint = $('#KanBanReplenPoint').val(), KanBanLevel = $('#KanBanReplenLevel').val();
        var reorderQty = document.getElementById('reorderQty').value, dateSensitive = document.getElementById('dateSensitive').checked;
        var WHSensitive = document.getElementById('warehouseSensitive').checked, FIFO = document.getElementById('FIFO').checked;
        var FIFODate = document.getElementById('FIFODate').value, pZone = $('#pZone').val(), sZone = $('#sZone').val();
        var pFenceQty = document.getElementById('pFenceQty').value, splitCase = document.getElementById('splitCase').checked, caseQty = document.getElementById('caseQty').value;
        var pSequence = document.getElementById('pSequence').value, active = document.getElementById('activeCheckbox').checked, cell = document.getElementById('carCell').value;
        var velocityCode = document.getElementById('carVel').value, carMin = document.getElementById('carMinQty').value, carMax = document.getElementById('carMaxQty').value;
        
        var bulkCell = document.getElementById('bulkCell').value, bulkVel = document.getElementById('bulkVel').value, bulkMin = document.getElementById('bulkMinQty').value;
        var bulkMax = document.getElementById('bulkMaxQty').value;
        
        var cartonCell = document.getElementById('cartonCell').value, cartonVel = document.getElementById('cartonVel').value, cartonMin = document.getElementById('cartonMinQty').value;
        var cartonMax = document.getElementById('cartonMaxQty').value;

        var uCost = document.getElementById('uCost').value, supplierNumber = document.getElementById('supID').value, manuID = document.getElementById('manuID').value;
        var specfeat = document.getElementById('specialFeatures').value, scale = document.getElementById('useScale').checked, avgWeight = document.getElementById('avgWeight').value;
        var sample = document.getElementById('sampleQty').value, scaleQty = document.getElementById('minScaleQty').value;
        invMasterHub.server.updateInvMastAll(item, supplierItemID, description, category, subcategory, UoM, repPoint, repLevel, reorderPoint, reorderQty, dateSensitive, WHSensitive, FIFO,FIFODate, pZone, sZone, pFenceQty, splitCase, caseQty, pSequence, active, cell, velocityCode, carMin, carMax, bulkCell, bulkVel, bulkMin, bulkMax, cartonCell, cartonVel, cartonMin, cartonMax, uCost, supplierNumber, manuID, specfeat, scale, avgWeight, sample, scaleQty, currentData, KanBanPoint, KanBanLevel).done(function (success) {
            if (success) {
                document.getElementById('saveButton').disabled = true;
                document.getElementById('stockCode').disabled = false;
                currentData = new Array();
                $('.inv-edit').each(function () {
                    var $this = $(this);
                    if ($this.is(':checkbox')) {
                        currentData.push([$this.attr('id'), $this.prop('checked')]);
                    } else {
                        currentData.push([$this.attr('id'), ($this.val() == null ? '' : $this.val())]);
                    };
                });
            } else {
                stockCode();
                alert('Your changes have NOT been saved.  Either an Error Occured or Another user may have made edits to the current entry before you saved.  The page will refresh with the new data.  Make any changes necessary and resave.');
                $('#stockCode').trigger('input');
            };
        });
    });
});