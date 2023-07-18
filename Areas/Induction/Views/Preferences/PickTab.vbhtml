<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row top-spacer">
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        @If Model.Prefs.AutoPickOrderSel Then
                            @<b>Auto Select Pick Orders: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if orders are automatically selected when creating a pick batch" checked="checked" id="AutoSelPickOrders" />
                        Else
                            @<b>Auto Select Pick Orders: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if orders are automatically selected when creating a pick batch" id="AutoSelPickOrders" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPickTote Then
                            @<b>Auto Pick Tote ID's: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if tote id's are automatically generated for pick batches" checked="checked" id="AutoPickToteID" />
                        Else
                            @<b>Auto Pick Tote ID's: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if tote id's are automatically generated for pick batches" id="AutoPickToteID" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.CarTotePicks Then
                            @<b>Carousel Pick Tote ID's: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if tote ids are required for pick transactions" checked="checked" id="CarPickToteID" />
                        Else
                            @<b>Carousel Pick Tote ID's: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if tote ids are required for pick transactions" id="CarPickToteID" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.OffCarTotePicks Then
                            @<b>Off Carousel Pick Tote ID's: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if tote ids are required for pick transactions" checked="checked" id="OffCarPickToteID" />
                        Else
                            @<b>Off Carousel Pick Tote ID's: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if tote ids are required for pick transactions" id="OffCarPickToteID" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.UseBatchMan Then
                            @<b>Use Pick Batch Manager: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if pick batch manager is used to select pick orders" checked="checked" id="UsePickBatchMan" />
                        Else
                            @<b>Use Pick Batch Manager: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if pick batch manager is used to select pick orders" id="UsePickBatchMan" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.CarBatchPicks Then
                            @<b>Carousel Batch ID's- Picks: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if Pick Batch ID is automatically generated" checked="checked" id="CarBatchPick" />
                        Else
                            @<b>Carousel Batch ID's- Picks: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if Pick Batch ID is automatically generated" id="CarBatchPick" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.OffCarBatchPicks Then
                            @<b>Off Carousel Batch ID's- Picks: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if Pick Batch ID is automatically generated" checked="checked" id="OffCarBatchPick" />
                        Else
                            @<b>Off Carousel Batch ID's- Picks: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if Pick Batch ID is automatically generated" id="OffCarBatchPick" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        <strong>Pick Order Sort: </strong><select class="form-control" data-toggle="tooltip" title="Default sort used for selecting pick batch orders" id="OrderSort">
                            @code
                                Dim OrderSortList As List(Of String) = New List(Of String) From {"Order Number Sequence",
                                                                             "Import Date and Order Number",
                                                                             "Import Date and Priority",
                                                                             "Import File Sequence",
                                                                             "Priority and Import Date",
                                                                             "Required Date and Priority"}
                                For Each x In OrderSortList
                                    If Model.Prefs.OrderSort = x Then
                                @<option value="@x" selected>@x</option>
                                    Else
                                @<option value="@x">@x</option>
                                    End If
                                Next

                            End Code
                        </select>
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.UseInZonePickScreen Then
                            @<b>Use In Zone Pick Screen: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if the In Zone Pick screen is used when processing picks" checked="checked" id="UseInZonePickScreen" />
                        Else
                            @<b>Use In Zone Pick Screen: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if the In Zone Pick screen is used when processing picks" id="UseInZonePickScreen" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPrintCaseLabel Then
                            @<b>Auto Print Case Label:</b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if the Case Label will be automatically printed" checked="checked" id="AutoPrintCaseLabel" />
                        Else
                            @<b>Auto Print Case Label:</b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if the Case Label will be automatically printed" id="AutoPrintCaseLabel" />
                        End If
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>