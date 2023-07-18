<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType Dictionary(Of String, Object)
<div class="panel panel-info">
    <div class="panel-body">
        <div class="col-md-3">
            <button class="btn btn-primary btn-xl btn-block bottom-spacer" id="assigncountsbtn">Assign Counts Now</button>
            <button class="btn btn-primary btn-xl btn-block bottom-spacer" id="assignpicksbtn">Assign Picks Now</button>
            <button class="btn btn-primary btn-xl btn-block bottom-spacer" id="assignputsbtn">Assign Put Aways Now</button>
            <div class="panel panel-info">
                <div class="panel-body white-bg text-center text-lg">
                    <div class="col-md-12">
                        <label>Unallocated Transactions</label>
                        <hr />

                    </div>
                    <div class="col-md-12" style="padding-top:15px;padding-bottom:10px">
                        <div class="col-md-12">
                            <label>Picks</label>
                        </div>
                        <div class="col-md-12">
                            <span id="unallocatedpicks"></span>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding-top:15px;padding-bottom:10px">
                        <div class="col-md-12">
                            <label>Put Aways</label>
                        </div>
                        <div class="col-md-12">
                            <span id="unallocatedputaways"></span>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding-top:15px;padding-bottom:10px">
                        <div class="col-md-12">
                            <label>Counts</label>
                        </div>
                        <div class="col-md-12">
                            <span id="unallocatedcounts"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-9">
            <div class="panel panel-info">
                <div class="panel-body white-bg">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <label>Auto Location Assignment</label>
                            <hr />
                        </div>
                        <div class="col-md-4">
                            <label class="col-md-4">Picks</label>
                            <div class="col-md-2">
                                <input type="checkbox" name="AutoLocPicks" @GlobalHTMLHelpers.Checked(Model("AutoLocPicks")) />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="col-md-4">Put Aways</label>
                            <div class="col-md-2">
                                <input type="checkbox" name="AutoLocPutAways" @GlobalHTMLHelpers.Checked(Model("AutoLocPutAways")) />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="col-md-4">Counts</label>
                            <div class="col-md-2">
                                <input type="checkbox" name="AutoLocCounts" @GlobalHTMLHelpers.Checked(Model("AutoLocCounts")) />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-body white-bg">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <label class="col-md-6">Pick Locations Assigned By</label>
                                <div class="col-md-6">
                                    <select class="form-control" name="LocAssignPickSort">
                                        @GlobalHTMLHelpers.SelectOption(Model("LocAssPickSort"), {"Item Quantity", "Zone", "Location", "Least Quantity"})
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-6">Dynamic Warehouse Locations</label>
                                <div class="col-md-6 form-control-static">
                                    <input type="checkbox" name="DynamicWHLoc" @GlobalHTMLHelpers.Checked(Model("Dynamic Warehouse Locations"))>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <label class="col-md-6">Auto Complete Back Order</label>
                                <div class="col-md-6 form-control-static">
                                    <input type="checkbox" name="AutoCompleteBackorders" @GlobalHTMLHelpers.Checked(Model("Auto Complete Backorders")) />
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-6">Print Reprocess Report After Location Assignment</label>
                                <div class="col-md-6 form-control-static">
                                    <input type="checkbox" name="PrintReprocessReport" @GlobalHTMLHelpers.Checked(Model("Print Reprocess Report")) />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>