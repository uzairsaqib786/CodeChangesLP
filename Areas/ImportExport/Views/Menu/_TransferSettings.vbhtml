<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType Dictionary(Of String, Object)
<div class="panel panel-info">
    <div class="panel-body">
        <div class="row">
            <div class="col-md-6 bottom-spacer">
                <div class="panel panel-info">
                    <div class="panel-body white-bg">
                        <div class="col-md-6">
                            <button class="btn btn-primary btn-xl btn-block" id="ImportAll">Import All</button>
                        </div>
                        <div class="col-md-6">
                            <label>Import Job Type</label>
                            <select class="form-control" name="ImportJobType">
                                @GlobalHTMLHelpers.SelectOption(Model("Import Job Type"), {"SP", "Job"})
                            </select>
                        </div>
                        <div class="col-md-6">
                            <button class="btn btn-primary btn-xl btn-block" id="ExportAll">Export All</button>
                        </div>
                        <div class="col-md-6">
                            <label>Export Job Type</label>
                            <select class="form-control" name="ExportJobType">
                                @GlobalHTMLHelpers.SelectOption(Model("Export Job Type"), {"SP", "Job"})
                            </select>
                        </div>
                    </div>
                </div>
                <div class="panel panel-info">
                    <div class="panel-body white-bg">
                        <div class="row">
                            <label class="col-md-6">Export Date Range</label>
                            <div class="col-md-6">
                                <input type="checkbox" class="form-control-static" name="ExportDateRange" @GlobalHTMLHelpers.Checked(Model("Export Date Range")) />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label style="margin-left:2em">From</label>
                            </div>
                            <div class="col-md-6">
                                <input type="text" class="datepicker form-control" name="ExportFromDate" value="@Model("Export From Date")"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label style="margin-left:2em">To</label>
                            </div>
                            <div class="col-md-6">
                                <input type="text" class="datepicker form-control" name="ExportToDate" value="@Model("Export To Date")" />
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-6">Export Type</label>
                            <div class="col-md-6">
                                <select class="form-control" name="ExportType">
                                    @Code Dim allTypes As Boolean = Model("Export All Types") End Code
                                    <option @(If(allTypes, "selected", ""))>All</option>
                                    @GlobalHTMLHelpers.SelectOption(Model("Export Type"), Model("Export Trans Types").ToArray(), Not allTypes)
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
            <div class="col-md-6 bottom-spacer">
                <div class="col-md-12 bottom-spacer">
                    <a href="/IE/IETransactions" type="button" class="btn btn-xl btn-block btn-primary">Import/Export Transactions</a>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <!-- Had to double up for spacing to line up with above col-->
                            <div class="panel panel-info">
                                <div class="panel-body white-bg">
                                    <div class="col-md-12 text-center">
                                        <label>Transaction Types to Export</label>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <label class="col-md-6">Hot Picks</label>
                                            <div class="col-md-6">
                                                <input type="checkbox" class="form-control-static" name="ExportHotPicks" @GlobalHTMLHelpers.Checked(Model("Export Hot Picks")) />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-md-6">Adjustments</label>
                                            <div class="col-md-6">
                                                <input type="checkbox" class="form-control-static" name="ExportAdjustments" @GlobalHTMLHelpers.Checked(Model("Export Adjustments")) />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-md-6">Hot Put Aways</label>
                                            <div class="col-md-6">
                                                <input type="checkbox" class="form-control-static" name="ExportHotPutAways" @GlobalHTMLHelpers.Checked(Model("Export Hot Put Aways")) />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-md-6">Moves</label>
                                            <div class="col-md-6">
                                                <input type="checkbox" class="form-control-static" name="VerifyLocations" @GlobalHTMLHelpers.Checked(Model("Verify Locations")) />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <label class="col-md-6">Complete</label>
                                            <div class="col-md-6">
                                                <input type="checkbox" class="form-control-static" name="Complete" @GlobalHTMLHelpers.Checked(Model("Export Complete Transactions")) />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-md-6">Shipping Complete</label>
                                            <div class="col-md-6">
                                                <input type="checkbox" class="form-control-static" name="ShippingComplete" @GlobalHTMLHelpers.Checked(Model("Export Shipping Complete Transactions")) />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-md-6">Shipping</label>
                                            <div class="col-md-6">
                                                <input type="checkbox" class="form-control-static" name="Shipping" @GlobalHTMLHelpers.Checked(Model("Row Map")) />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-md-6">Shipping Transactions</label>
                                            <div class="col-md-6">
                                                <input type="checkbox" class="form-control-static" name="ShippingTransactions" @GlobalHTMLHelpers.Checked(Model("Shelf Map")) />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-body white-bg">
                                    <div class="row">
                                        <label class="col-md-6">Wait for Split Transactions</label>
                                        <div class="col-md-6">
                                            <input type="checkbox" class="form-control-static" name="WaitForSplit" @GlobalHTMLHelpers.Checked(Model("Wait for Split Transactions")) />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-6">Recombine Split Transactions</label>
                                        <div class="col-md-6">
                                            <input type="checkbox" class="form-control-static" name="RecombineSplit" @GlobalHTMLHelpers.Checked(Model("Recombine Split Transactions")) />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <label class="col-md-6">Hold Pick Until Ship Complete</label>
                                        <div class="col-md-6">
                                            <input type="checkbox" class="form-control-static" name="HoldPickUntilShipComplete" @GlobalHTMLHelpers.Checked(Model("Hold Pick Until Ship Complete")) />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-6">Hold Pick Until Tote Complete</label>
                                        <div class="col-md-6">
                                            <input type="checkbox" class="form-control-static" name="HoldPickUntilToteComplete" @GlobalHTMLHelpers.Checked(Model("Hold Pick Until Tote Complete")) />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-6">Hold Pick Until Order Complete</label>
                                        <div class="col-md-6">
                                            <input type="checkbox" class="form-control-static" name="HoldPickUntilOrderComplete" @GlobalHTMLHelpers.Checked(Model("Hold Pick Until Order Complete")) />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
