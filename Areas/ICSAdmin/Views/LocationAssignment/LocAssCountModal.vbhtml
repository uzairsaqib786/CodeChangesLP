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
                            If trans.Type.ToString().ToLower() = "count" AndAlso Not Model.AutoLocCounts Then
                                @<button type="button" class="btn btn-primary" id="GoLocAssCount" data-dismiss="modal">Count <span class="badge">@trans.Count.ToString</span></button>
                            End If
                        Next
                    </div>
                    <div class="col-md-4">
                        @For Each trans In Model.CountData
                            If trans.Type.ToString().ToLower() = "pick" AndAlso Not Model.AutoLocPicks Then
                                @<button type="button" class="btn btn-primary" id="GoLocAssPick" data-dismiss="modal">Pick <span class="badge">@trans.Count.ToString</span></button>
                            End If
                        Next
                    </div>
                    <div class="col-md-4">
                        @For Each trans In Model.CountData
                            If trans.Type.ToString().ToLower() = "put away" AndAlso Not Model.AutoLocPuts Then
                                @<button type="button" class="btn btn-primary" id="GoLocAssPutAway" data-dismiss="modal">Put Away <span class="badge">@trans.Count.ToString</span></button>
                            End If
                        Next
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-6">
                        <button class="btn btn-primary btn-block" id="LocAssPrint">Print Pick Shortage</button>
                    </div>
                    <div class="col-md-6">
                        <button class="btn btn-primary btn-block" id="LocAssPrintFPZ">Print Pick Shortage Forward Pick Zones</button>
                    </div>
                </div>
                <div class="row" style="padding-top:20px;">
                    <div class="col-md-3 col-md-offset-6">
                        <button type="button" class="btn btn-default btn-block" data-dismiss="modal" id="DismissLocAssModal">Close</button>
                    </div>
                    <div class="col-md-3 pull-right">
                        <button class="btn btn-primary btn-block" id="modalBackBtn">Leave Page</button>
                    </div>
                </div>    
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ICSAdmin/Scripts/LocationAssignment/LocAssCountModal.js"></script>
