<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row top-spacer">
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPutTote Then
                            @<b>Auto Put Away Tote ID's: </b>@<input type="checkbox" checked="checked" id="AutoPutAwayToteID" />
                        Else
                            @<b>Auto Put Away Tote ID's: </b>@<input type="checkbox" id="AutoPutAwayToteID" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.SplitShortPut Then
                            @<b>Split Short Put Away: </b>@<input type="checkbox" checked="checked" id="SplitShortPut" />
                        Else
                            @<b>Split Short Put Away: </b>@<input type="checkbox" id="SplitShortPut" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.CarBatchPuts Then
                            @<b>Carousel Batch ID's- Put Aways: </b>@<input type="checkbox" checked="checked" id="CarBatchPutAway" />
                        Else
                            @<b>Carousel Batch ID's- Put Aways: </b>@<input type="checkbox" id="CarBatchPutAway" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.OffCarBatchPuts Then
                            @<b>Off Carousel Batch ID's- Put Aways: </b>@<input type="checkbox" checked="checked" id="OffCarBatchPutAway" />
                        Else
                            @<b>Off Carousel Batch ID's- Put Aways: </b>@<input type="checkbox" id="OffCarBatchPutAway" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.CreatePutAdjusts Then
                            @<b>Create Put Away Adjustments: </b>@<input type="checkbox" checked="checked" id="CreatePutAdjusts" />
                        Else
                            @<b>Create Put Away Adjustments: </b>@<input type="checkbox" id="CreatePutAdjusts" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        <label>Default Put Away Scan Type</label>
                        <select id="DefaultType" class="form-control">
                            @For Each opt As String In {"Any", "Item Number", "Serial Number", "Lot Number", "Host Transaction ID", "Scan Code", "Supplier Item ID"}
                                If Model.Prefs.PutScanType.ToString().ToLower() = opt.ToLower() Then
                                @<option selected="selected">@opt</option>
                                Else
                                @<option>@opt</option>
                                End If
                            Next
                        </select>
                    </div>
                    <div class="col-md-12 top-spacer">
                        <b>Default Put Away Priority: </b><input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" id="DefPutAwayPrior" class="form-control" value="@Model.Prefs.DefPutPrior" />
                    </div>
                    <div class="col-md-12 top-spacer">
                        <b>Default Put Away Quantity: </b><input type="text" oninput="setNumericInRange($(this), 0, SqlLimits.numerics.int.max)" id="DefPutAwayQuant" class="form-control" value="@Model.Prefs.DefPutQuant" />
                    </div>
                    <div class="col-md-12 top-spacer">
                        <strong>Put Away Induction Screen:</strong>
                        @If CInt(Model.Prefs.PickBatchQuant) > 20 Then
                            @<select class="form-control" disabled id="PutInductScreen">
                                <option value="Unlimited Positions" selected>Unlimited Positions</option>
                                <option value="20 Tote Matrix">20 Tote Matrix</option>
                            </select>
                        Else
                            @<select class="form-control" id="PutInductScreen">
                                @If Model.Prefs.PutInductScreen = "Unlimited Positions" Then
                                    @<option value="Unlimited Positions" selected>Unlimited Positions</option>
                                    @<option value="20 Tote Matrix">20 Tote Matrix</option>
                                Else
                                    @<option value="Unlimited Positions">Unlimited Positions</option>
                                    @<option value="20 Tote Matrix" selected>20 Tote Matrix</option>
                                End If
                            </select>
                        End If
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>