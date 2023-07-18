<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
    Dim isStandAlone As Boolean = Model
End Code
<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">Staging Locations</h3>
    </div>
    <div class="panel-body">
        <div class="container-fluid">
            <div class="row">
                <div class="col-xs-12 col-sm-9 col-sm-offset-1 col-md-8 col-md-offset-2">
                    <div class="row">
                        <div class="col-xs-12">
                            <h4 class="modal-title" id="StagingLocsLabel">Order Number/Tote</h4>
                            <div class="input-group">
                                <input autofocus id="StagingLocsOrderNum" class="form-control" />
                                <span class="input-group-btn">
                                    <button class="btn btn-danger" data-toggle="tooltip" title="Clear Order/tote" onclick="$('#StagingLocsOrderNum').val('').trigger('input').focus();"><span class="glyphicon glyphicon-remove"></span></button>
                                </span>
                            </div>
                        </div>
                    </div>
                    @*<hr />*@
                    <div class="row" style="padding-top:10px">
                        <div class="col-xs-12">
                            <button type="button" class="btn btn-danger pull-right" id="btnUnstageAll">Unstage All</button>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-xs-4">
                            <label>Tote ID</label>
                        </div>
                        <div class="col-xs-8">
                            <label>Staging Location</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div id="StagingContainer">

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
@Html.Partial("MessageModal")
@Html.Partial("~/Areas/Consolidation Manager/Views/Consolidation/OrderToteConflictModal.vbhtml")
<script src="~/Areas/Consolidation Manager/Scripts/StagingLocations/StagingLocationsStandAlone.js"></script>