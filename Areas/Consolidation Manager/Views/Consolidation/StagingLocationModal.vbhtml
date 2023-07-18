<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
    Dim isStandAlone As Boolean = Model
End Code
<div class="modal fade" id="StagingLocsModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="ItemSelectedModal" aria-hidden="true">
    <div class="modal-dialog full-tablet">
        <div class="modal-content max-height-tablet">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-6">
                        @If isStandAlone Then
                            @<h4 class="modal-title" id="StagingLocsLabel">Order Number or Tote ID</h4>
                        Else
                            @<h4 class="modal-title" id="StagingLocsLabel">Order Number</h4>
                        End If
                    </div>
                    <div class="col-md-6">

                        @If isStandAlone Then
                            @<div class="input-group">
                                 <input id="StagingLocsOrderNum" class="form-control" maxlength="50" />
                                <span class="input-group-btn">
                                    <button class="btn btn-danger" title="Clear Order Number" data-toggle="tooltip" onclick="$('#StagingLocsOrderNum').val('').trigger('input').focus();"><span class="glyphicon glyphicon-remove"></span></button>
                                </span>
                            </div>
                        Else
                            @<input id="StagingLocsOrderNum" class="form-control" disabled="disabled" maxlength="50" />
                        End If
                    </div>
                </div>

            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-xs-5">
                                <label>Tote ID</label>
                                <input id="stagingToteID" class="form-control" maxlength="50" />
                            </div>
                            <div class="col-xs-5">
                                <label>Staging Location</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="StagingContainer">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-offset-10 col-md-2">
                    <button type="button" class="btn btn-danger" id="btnUnstageAll">Unstage All</button>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        @If isStandAlone Then
                            @<a class="btn btn-default" href="/CM">Close</a>
                        Else
                            @<button type="button" class="btn btn-default" data-dismiss="modal" id="StagingLocsDismiss">Close</button>
                        End If
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/Consolidation/StagingLocations.js"></script>