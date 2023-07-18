// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var OrderToteConflictCallBack;
var OrderToteConflictVal;
$(document).ready(function () {
    $('#valIsOrder').click(function () {
        $('#OrderToteConflictModal').modal('hide');
        OrderToteConflictCallBack('Order Number', OrderToteConflictVal);
    })
    $('#valIsTote').click(function () {
        $('#OrderToteConflictModal').modal('hide');
        OrderToteConflictCallBack('Tote ID', OrderToteConflictVal);
    })
})
//shows conflict modal if a order number and tote id match
function ShowOrderToteConflictModal(Value,CallBackFnc) {
    $('#OrderToteConflictModal').modal('show');
    OrderToteConflictCallBack = CallBackFnc;
    OrderToteConflictVal = Value;
}