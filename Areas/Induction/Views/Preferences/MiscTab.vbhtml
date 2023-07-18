<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row top-spacer">
    <div class="col-md-6">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        @If Model.Prefs.TrackInductTrans Then
                            @<b>Track Induction Location: </b>@<input type="checkbox" checked="checked" id="TrackInductLoc" />
                        Else
                            @<b>Track Induction Location: </b>@<input type="checkbox" id="TrackInductLoc" />
                        End If
                    </div>
                    <div class="col-md-4">
                        @If Model.Prefs.TrackInductTrans Then
                            @<b>Induction Location: </b>@<input type="text" id="InductLoc" class="form-control" value="@Model.Prefs.InductLoc" />
                        Else
                            @<b>Induction Location: </b>@<input type="text" id="InductLoc" class="form-control" disabled="disabled" value="@Model.Prefs.InductLoc" />
                        End If
                    </div>
                    <div class="col-md-4" style="padding-top:20px;">
                        @If Model.Prefs.TrackInductTrans Then
                            @<button type="button" class="btn btn-primary btn-block" id="UseCompName">Use Computer Name</button>
                        Else
                            @<button type="button" class="btn btn-primary btn-block" disabled id="UseCompName">Use Computer Name</button>
                        End If

                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-4">
                        @If Model.Prefs.StageBulkPro Then
                            @<b>Stage Using BulkPro: </b>@<input type="checkbox" checked="checked" id="StageUsingBulkPro" />
                        Else
                            @<b>Stage Using BulkPro: </b>@<input type="checkbox" id="StageUsingBulkPro" />
                        End If
                    </div>
                    <div class="col-md-4">
                        <b>Stage Velocity Code: </b><input type="text" id="StageVelCode" class="form-control" value="@Model.Prefs.StageVelCode" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-4">
                        <b>Confirm Super Batch: </b><input type="checkbox" id="ConfirmSuperBatch" @(If(Model.Prefs.ConfirmSuperBatch, "Checked=""Checked""", "")) />
                    </div>
                    <div class="col-md-4">
                        <b>Default Super Batch Size: </b><input type="number" class="form-control" id="DefaultSuperBatchSize" oninput="setNumericInRange($(this), 0, 99999,false)" value="@Model.Prefs.DefaultSuperBatch" />
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-4">
                        <label>Super Batch Filter: </label>
                    </div>
                    <div class="col-md-4">
                        <label>Default Filter: </label>
                        <select class="form-control" id="superBatchFilt">
                            @If Model.Prefs.SBByToteID Then
                                @<option value="1">Tote ID</option>
                                @<option value="0">Order Number</option>
                            Else
                                @<option value="0">Order Number</option>
                                @<option value="1">Tote ID</option>
                            End If
                        </select>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>