<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object

<div class="modal fade" id="LocAssCountModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="LocAssCountModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="LocAssCountLabel">Location Assignment Quantities</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        @For Each trans In Model.CountData
                            If trans.Type = "Count" Then
                                @<button type="button" class="btn btn-primary" id="GoLocAssCount" data-dismiss="modal">Count <span class="badge">@trans.Count.ToString</span></button>
                            End If
                        Next
                    </div>
                    <div class="col-md-4">
                        @For Each trans In Model.CountData
                            If trans.Type = "Pick" Then
                                @<button type="button" class="btn btn-primary" id="GoLocAssPick" data-dismiss="modal">Pick <span class="badge">@trans.Count.ToString</span></button>
                            End If
                        Next
                    </div>
                    <div class="col-md-4">
                        @For Each trans In Model.CountData
                            If trans.Type = "Put Away" Then
                                @<button type="button" class="btn btn-primary" id="GoLocAssPutAway" data-dismiss="modal">Put Away <span class="badge">@trans.Count.ToString</span></button>
                            End If
                        Next
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/LocAssCountModal.js"></script>
