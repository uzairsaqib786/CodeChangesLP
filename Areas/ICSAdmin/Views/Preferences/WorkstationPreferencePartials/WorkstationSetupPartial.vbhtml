<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<div class="row" id="PageLoad">
    <div class="col-md-12">
        <label style="font-size:30px;" class="text-center">Loading...</label>
    </div>
</div>
<div class="row" hidden id="PageLoadDone">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="col-md-4" id="Setup_1">
                    <div class="page-header" style="margin-top:0px;">
                        <h3 class="text-center">Workstation Settings</h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <label>POD Number Connected To</label>
                                    <select class="form-control" id="podNumbers">
                                        @For Each entry In Model.PODs
                                            @<option value="@entry.zone">@(entry.zone & " | " & entry.locname) </option>
                                        Next
                                    </select>
                                </div>
                                <div class="col-md-6">
                                    <label>Current Linked Database</label>
                                    <input type="hidden" id="currentConnString" value="@Session("ConnectionString")" />
                                    <select id="connectionString" class="form-control">
                                        @For Each conn In Model.ConnectionStrings
                                            If conn.name = Session("ConnectionString") Then
                                            @<option selected="selected">@conn.name</option>
                                            Else
                                            @<option>@conn.name</option>
                                            End If
                                        Next
                                    </select>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <div class="col-xs-6">
                                    <label>Scan Verify Picks</label>
                                </div>
                                <div class="col-xs-6">
                                    <div class="toggles toggle-modern pull-right" id="SVPicks"></div>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <div class="col-xs-6">
                                    <label>Scan Verify Counts</label>
                                </div>
                                <div class="col-xs-6">
                                    <div class="toggles toggle-modern pull-right" id="SVCounts"></div>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <div class="col-xs-6">
                                    <label>Scan Verify Put Aways</label>
                                </div>
                                <div class="col-xs-6">
                                    <div class="toggles toggle-modern pull-right" id="SVPuts"></div>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <div class="col-xs-6">
                                    <label>Allow Quick Picks</label>
                                </div>
                                <div class="col-xs-6">
                                    <div class="toggles toggle-modern pull-right" id="AllowQuick"></div>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <div class="col-xs-6">
                                    <label>Default Quick Picks</label>
                                </div>
                                <div class="col-xs-6">
                                    <div class="toggles toggle-modern pull-right" id="DefaultQuick"></div>
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <div class="col-md-6">
                                    <label>Print Report Location</label>
                                    <select class="form-control" id="reportPrinter">
                                        <option value="No Printer">No Printer</option>
                                        @For Each entry In Model.printers
                                            If entry.label <> "Able to Print Labels" Then
                                                @<option value="@entry.printeradd"> @(entry.printer)</option>
                                            End If
                                        Next
                                    </select> 
                                </div>
                                <div class="col-md-6">
                                    <label>Print Label Location</label>
                                    <select class="form-control" id="labelPrinter">
                                        <option value="No Printer">No Printer</option>
                                        @For Each entry In Model.printers
                                            If entry.label = "Able to Print Labels" Then
                                                @<option value="@entry.printeradd"> @(entry.printer)</option>
                                            End If
                                        Next
                                    </select> 
                                </div>
                            </div>
                            <div class="row" style="padding-top:10px;">
                                <label>Carton Flow Zone Connected To:</label>
                                <select class="form-control" id="cartonFlowNumbers">
                                    @For Each entry In Model.CFZones
                                        @<option value="@entry.zone">@(entry.zone & " | " & entry.locname) </option>
                                    Next
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4" id="Setup_2">
                    <div class="page-header" style="margin-top:0px;">
                        <h3 class="text-center">Tote Management</h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Batch Hot Put Away</label>
                                            <select class="form-control" id="BatchHotPut">
                                                <option value="Operator Directed">Operator Directed</option>
                                                <option value="Quick Pick">Quick Pick</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Pick To Totes</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="PickToTotes"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Put Away From Totes</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="PutAwayFromTotes"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Carousel Pick Tote Manifest</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="CarPickToteManifest"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6" style="padding-top:10px;">
                                            <label>Off Carousel Pick Tote Manifest</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="OffCarPickToteManifest"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Auto Print Off Carousel Pick Tote Manifest</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="AutoPrintOffCar"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Auto Print Tote Labels</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="AutoPrintTote"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Batch Carousel Put Away</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="BatchCarPut"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4" id="Setup_3">
                    <div class="page-header" style="margin-top:0px;">
                        <h3 class="text-center">Location Assignment Functions</h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Pick Locations Assigned By</label>
                                            <select class="form-control" id="PickLocAssigned">
                                                <option value="Item Quantity">Item Quantity</option>
                                                <option value="Zone">Zone</option>
                                                <option value="Location">Location</option>
                                                <option value="Least Quantity">Least Quantity</option>
                                            </select>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Display Bulk Counts Quantity</label>
                                            <select class="form-control" id="DisplayBulkQty">
                                                <option value="Display Always">Display Always</option>
                                                <option value="Never Display">Never Display</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Location Assign Single Order Selection</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="LocationAssignSingle"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Print Reprocess Report After Allocation</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="PrintReprocessAllocation"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Auto Complete Back Order</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="AutoBackOrder"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6" style="padding-top:10px;">
                                            <label>Create SAP Location Change Transactions</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="CreateSAPLocChange"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px;">
                                        <div class="col-xs-6">
                                            <label>Print Pick Label For Each Pick</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="PrintPickLabelForEach"></div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:10px; display:none;" id="PrintAllOnce_div">
                                        <div class="col-xs-6">
                                            <label>Print All At Once</label>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="toggles toggle-modern pull-right" id="PrintAllOnce"></div>
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

