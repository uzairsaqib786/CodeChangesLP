<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype List(Of String)
@Code
    ViewData("Title") = "Move Items"
    ViewData("PageName") = "| Move Items"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-5">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Location Details</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <button id="ValidateMove" class="btn btn-block btn-primary">Create Move Transactions</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>Required Date</label>
                            <input id="MoveReqDate" type="text" class="form-control date-picker input-sm" value="@Now.AddDays(1).Date.ToString" maxlength="50" />
                        </div>
                        <div class="col-md-2">
                            <label>Priority</label>
                            <input oninput="setNumericInRange($(this), SqlLimits.numerics.smallint)" id="MovePriority" type="text" class="form-control input-sm" />
                        </div>
                        <div class="col-md-3">
                            <label>Move Qty</label>
                            <input oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="MoveQty" type="text" class="form-control input-sm" />
                        </div>
                        <div class="col-md-3">
                            <br /><label>Dedicate?</label>
                            <div id="DedicateToggle" class="toggles toggle-modern pull-right" data-toggle-ontext="Yes" data-toggle-offtext="No"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <legend>Move From &nbsp; &nbsp; &nbsp; <strong id="MoveFrom_Dedicated"></strong></legend>
                                </div>
                            </div>
                           <div class="row">
                               <div class="col-md-6">
                                   <div class="row">
                                       <div class="col-md-12">
                                           <label>Warehouse</label>
                                           <input id="MoveFrom_Warehouse" disabled="disabled" type="text" class="form-control input-sm" maxlength="50" />
                                       </div>
                                   </div>
                                   <div class="row">
                                       <div class="col-md-12">
                                           <label>Item Number</label>
                                           <input id="MoveFrom_ItemNumber" disabled="disabled" type="text" class="form-control input-sm" maxlength="50" />
                                       </div>
                                   </div>
                                   <div class="row">
                                       <div class="col-md-12">
                                           <label>Item Qty</label>
                                           <input id="MoveFrom_Qty" disabled="disabled" type="text" class="form-control input-sm" />
                                       </div>
                                   </div>
                                   <div class="row">
                                       <div class="col-md-12">
                                           <label>Lot Number</label>
                                           <input id="MoveFrom_LotNumber" disabled="disabled" type="text" class="form-control input-sm" />
                                       </div>
                                   </div>
                               </div>
                               <div class="col-md-6">
                                   <div class="row">
                                       <div class="col-md-12">
                                           <label>Location</label>
                                           <input id="MoveFrom_Location" disabled="disabled" type="text" class="form-control input-sm" />
                                       </div>
                                   </div>
                                   <div class="row">
                                       <div class="col-md-12">
                                           <label>Description</label>
                                           <input id="MoveFrom_Description" disabled="disabled" type="text" class="form-control input-sm" />
                                       </div>
                                   </div>
                                   <div class="row">
                                       <div class="col-md-12">
                                           <label>Cell Size</label>
                                           <input id="MoveFrom_CellSize" disabled="disabled" type="text" class="form-control input-sm" />
                                       </div>
                                   </div>
                                   <div class="row">
                                       <div class="col-md-12">
                                           <label>Serial Num.</label>
                                           <input id="MoveFrom_SerialNum" disabled="disabled" type="text" class="form-control input-sm" />
                                       </div>
                                   </div>
                               </div>
                           </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <legend>Move To &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <strong id="MoveTo_Dedicated"></strong></legend>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Warehouse</label>
                                        <input id="MoveTo_Warehouse" disabled="disabled" type="text" class="form-control input-sm" maxlength="50" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Item Number</label>
                                        <input id="MoveTo_ItemNumber" disabled="disabled" type="text" class="form-control input-sm" maxlength="50" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Item Qty</label>
                                        <input id="MoveTo_Qty" disabled="disabled" type="text" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Lot Number</label>
                                        <input id="MoveTo_LotNumber" disabled="disabled" type="text" class="form-control input-sm" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Location</label>
                                        <input id="MoveTo_Location" disabled="disabled" type="text" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Description</label>
                                        <input id="MoveTo_Description" disabled="disabled" type="text" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Cell Size</label>
                                        <input id="MoveTo_CellSize" disabled="disabled" type="text" class="form-control input-sm" />
                                    </div>
                                </div> 
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Serial Num.</label>
                                        <input id="MoveTo_SerialNum" disabled="disabled" type="text" class="form-control input-sm" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <label>Qty to Fill Location</label>
                                <input disabled="disabled" class="form-control input-sm" id="MoveTo_FillQty"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-7">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Move Locations</h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active"><a href="#MoveFromTab" role="tab" data-toggle="tab">Move From</a></li>
                        <li ><a href="#MoveToTab" role="tab"  data-toggle="tab">Move To</a></li>
                        <li><label id="ItemLabel" style="margin-top:5px;">Item Number to Move </label><input style="width:auto; display:inline;" type="text" id="ItemNumber" class="input-sm form-control" /></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="MoveFromTab" style="margin-top:5px;">
                            <table style="background:white;" class="table table-hover table-condensed table-bordered table-striped" id="MoveFromTable">
                                <thead>
                                    <tr>
                                        @For Each column In Model
                                            @<th>@column</th>
                                        Next
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div class="tab-pane" id="MoveToTab" style="margin-top:5px;">
                            <div id="MoveFromNotSelected" class="alert alert-warning"><strong>You may only select Move to Location after you have selected a Move From Location</strong></div>
                            <div class="checkbox">
                                <label>View All Locations?<input id="ViewAllMoveTo" type="checkbox" /></label>
                            </div>
                            
                            <table style="background:white;" class="table table-hover table-condensed table-bordered table-striped" id="MoveToTable">
                                <thead>
                                    <tr>
                                        @For Each column In Model
                                            @<th>@column</th>
                                        Next
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <select hidden id="MoveItemsCol">
        @For Each column In Model
            @<option value="@column">@column</option>
        Next
    </select>
    @Html.Partial("~/Areas/ICSAdmin/Views/MoveItems/MoveItemsModal.vbhtml")
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<link href="~/Content/toggles.css" rel="stylesheet" />
<link href="~/Content/toggles-modern.css" rel="stylesheet" />
<script src="~/Scripts/toggles.min.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/MoveItems/MoveItemsDatatable.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/MoveItems/MoveItemsFilters.js"></script>
