<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Bulk Handheld Settings
                </h3>
            </div>
            <div class="panel-body">
                <div class="row" style="padding-bottom:15px;">
                    <div class="col-md-12">
                        <button id="uncheckBulkSettings" type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Uncheck All"><span class="glyphicon glyphicon-ban-circle"></span></button>
                        <button id="checkBulkSettings" type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Check All"><span class="glyphicon glyphicon-ok"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Pick Scan Verify Location</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="pickScanVerifyLocation"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Allow Close Box</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="allowCloseBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Pick Scan Verify @Model.aliases.ItemNumber</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="pickScanVerifyItemNumber"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Print Order Manifest</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="printOrderManifest"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Pick Scan Verify Tote ID</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="pickScanVerifyTote"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Print Tote Manifest</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="printToteManifest"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Show Count</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="showCount"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Demand Tote ID</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="demandToteID"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Count Scan Verify Location</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="countScanVerifyLocation"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Dynamic Spacing</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="dynamicSpacing"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Count Scan Verify @Model.aliases.ItemNumber</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="countScanVerifyItemNumber"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Complete Short Picks</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="completeShortPicks"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Put Scan Verify Location</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="putScanVerifyLocation"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Put Scan Verify @Model.aliases.ItemNumber</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="putScanVerifyItemNumber"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Count if Short</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="countIfShort"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Print Pick Labels</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="printPickLabels"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Print Put Labels</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="printPutLabels"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>SAP Adjustment</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="sapAdjustment"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>HPA Request Supplier</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="HPARequestSupplier"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Dynamic Warehousing</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="dynamicWH"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Directed HPA</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="DirectedHPA"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Allow Hot Pick</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="allowHP"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Pick Sequence Sort</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="pickSequenceSort"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Print Next Tote Label</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="printNextToteLabel"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Container Logic</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="containerLogic"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Carousel Put Away</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="CarouselPutAway"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Put Away Batch</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="putAwayBatch"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Use F2 Task Complete</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="useF2TaskComplete"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Change Inventory Master Cell Size on HPA</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="changeInvMastCellSizeonHPA"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>HPA Show Empty Locations</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="HPAShowEmptyLocs"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Count If Location Emptied</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="countIfLocEmptied"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Combine Same IMID Pick</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="combineSameIMIDPick"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Consolidation Delivery</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="ConsolidationDelivery"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Short Pick Find New Location</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="ShortPickFNL"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Zone Choice</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="zoneChoice"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Consolidation Staging</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="consolidationStaging"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Use Inventory Case Quantity</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="useInventoryCaseQuantity"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Combine OT Trans</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="combineOTTrans"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Allow Super Batch</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="allowSuperBatch"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Single Line Super Batch</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="singleLineSuperBatch"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>HPA Show Velocity Code</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="HPAShowVelocityCode"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Pick Scan Verify Quantity</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="pickScanVerifyQuantity"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Hot Move Multiple Items</label>
                            </div>
                            <div class="col-xs-6">
                                <div class="toggles toggle-modern pull-right" id="hotMoveMultipleItems"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>