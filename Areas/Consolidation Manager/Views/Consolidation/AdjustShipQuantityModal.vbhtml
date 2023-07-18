<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object

<div class="modal fade" id="AdjustShipQuantModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="AdjustShipQuantModal" aria-hidden="true">
     <div class="modal-dialog" style ="width:900px;">
         <div class="modal-content">
             <div class="modal-header">
                 <h4 class="modal-title" id="AdjustShipQuantLabel">Adjust Shipping Quantity</h4>
             </div>
             <div class="modal-body">
                 <div class="row">
                     <div class="col-md-4">
                         <label>New Quantity</label>
                         <input type="text" maxlength="50" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" class="form-control" id="AdjustShipQuant" />
                     </div>
                     <div class="col-md-4">
                         <label>Reason For Adjustment</label>
                         <input class="form-control" list="Reasons" id="AdjustReason" />
                         <datalist id="Reasons">
                             @For Each reason In Model.reasons
                                 @<option value="@reason">@reason</option>
                             Next
                         </datalist>

                         @*<select class="form-control" id="AdjustReason">
                             <option value=""></option>
                             @For Each reason In Model.reasons
                                 @<option value="@reason">@reason</option>
                             Next
                         </select>*@
                     </div>
                     <div class="col-md-1" style="padding-top:25px;">
                         <button type="button" data-toggle="tooltip" title="Clear Reason" data-placement="top" class="btn btn-danger" id="ClearReason"><span class="glyphicon glyphicon-remove"></span></button>
                     </div>
                     <div class="col-md-3" style="padding-top:25px;">
                         <button type="button" disabled="disabled" class="btn btn-block btn-primary" data-dismiss="modal" id="AdjustShipQuantSave">Save</button>
                     </div>
                 </div>
             </div>
             <div class="modal-footer">
                 <div class="row">
                     <div class="col-md-offset-10 col-md-2">
                         <button type="button" class="btn btn-default" data-dismiss="modal" id="AdjustShipQuantCloseDismiss">Close</button>
                     </div>
                 </div>
             </div>
         </div>
     </div>
 </div>    
<script src="~/Areas/Consolidation Manager/Scripts/Consolidation/AdjustShipQuantityModal.js"></script>
