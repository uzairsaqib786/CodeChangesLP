<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="crossdock_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="crossdock_label" aria-hidden="true">
    <div class="modal-dialog" style="width:1000px;margin-top:10px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="crossdock_label">Cross Dock Transactions</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" id="AutoPrintCrossDock" value="@Model.preferences.AutoPrintDockLabel.ToString().ToLower()" />
                <div class="row">
                    <div class="col-md-3">
                        <label>Item Number</label>
                        <input type="text" class="form-control input-sm" id="CrossDockItem"  readonly="readonly" />
                    </div>
                    <div class="col-md-9">
                        <label>Description</label>
                        <textarea class="form-control no-horizontal input-sm" id="CrossDockDescription" rows="1" readonly="readonly"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Backorder Total</label>
                        <input type="text" class="form-control input-sm" readonly="readonly" id="Backorder"  />
                    </div>
                    <div class="col-md-4">
                        <label>Allocated Total</label>
                        <input type="text" class="form-control input-sm" readonly="readonly" id="Allocated"  />
                    </div>
                    <div class="col-md-4">
                        <label>Qty Available</label>
                        <input type="text" class="form-control input-sm" id="CrossDockQty"  readonly="readonly" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary" id="CDNextTote">Next Tote</button>
                        <button type="button" class="btn btn-primary" id="CDUF">User Fields</button>
                        <div class="btn-group">
                            <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                            <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                <li>
                                    <a href="#" id="PrintPickList">Print Pick List</a>
                                    <a href="#" id="PrintTote">Print Tote Label</a>
                                    <a href="#" id="PrintItem">Print Item Label</a>
                                </li>
                            </ul>
                        </div>
                        <div class="btn-group">
                            <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-share"></span> <span class="caret"></span></button>
                            <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                <li>
                                    <a href="#" id="ViewOS">View Order Status</a>
                                    <a href="#" id="ViewRP">View Reprocess</a>
                                </li>
                            </ul>
                        </div>
                        <button type="button" class="btn btn-primary" id="CDComplete">Complete Pick</button>
                        <button type="button" class="btn btn-primary" id="CDCompleteAllAvail">Complete All Available Picks</button>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-12" id="cd_container" style="overflow-y:scroll;">
                        
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label class="pull-right" id="CDPage">Showing 0 to 0 of 0 records.</label>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-md-1 col-md-offset-9">
                        <button type="button" class="btn btn-primary btn-block" data-toggle="tooltip" data-placement="top" data-original-title="Clear Filters" id="CDClearFilter"><span class="glyphicon glyphicon-refresh"></span></button>
                    </div>
                    <div class="col-md-1">
                        <button type="button" class="btn btn-primary btn-block" id="CDPrev"><span class="glyphicon glyphicon-arrow-left"></span></button>
                    </div>
                    <div class="col-md-1">
                        <button type="button" class="btn btn-primary btn-block" id="CDNext" disabled="disabled"><span class="glyphicon glyphicon-arrow-right"></span></button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="crossdock_dismiss">Cancel</button>
                <button type="button" class="btn btn-primary" id="crossdock_submit">Send Transaction to Tote/Batch</button>
            </div>
        </div>
    </div>
</div>
@Html.partial("~/Areas/Induction/Views/ProcessPutAways/Process/Modals/CrossDock/UserFields.vbhtml")
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/Modals/CrossDock.js"></script>